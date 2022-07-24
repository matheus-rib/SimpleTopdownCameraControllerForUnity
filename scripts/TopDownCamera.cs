using System.Diagnostics;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TopDownCamera : MonoBehaviour
{
  [Tooltip("The camera speed that will smoothly follow the target (Player). Leave it's value slightly slower than Target's speed for a better motion.")]
  public float speed = 10.0f;
  [Tooltip("The Gameobject which the camera will follow (place your Player Gameobject here).")]
  public Transform target;
  [Tooltip("Camera distance from the Target.")]
  public float distance = 15f;
  [Tooltip("Camera rotation around the Target.")]
  public float rotation = 45f;
  [Tooltip("If set to true, it will add transparency to Gameobjects between the Camera and the Target (Material's Surface Type needs to be set as 'Transparent'. If you are using URP, make sure your shader has the 'Transparent Receive Shadows' checked, so it can have shadows as an Opaque Gameobject).")]
  public bool fadeObjects = true;
  [Tooltip("The alpha that will be applied to the Gameobjects to be fade. It will only have effect if fadeObjects is set to true.")]
  [Range(0, 1)]
  public float alpha = 0.3f;
  [Tooltip("The distance between the Target and the Gameobject renderer to detect collision to fade the material. It will only have effect if fadeObjects is set to true.")]
  [Range(0, Mathf.Infinity)]
  public float maxDistance = 100f;

  private Vector3 offset;
  private List<Transform> previousHits = new List<Transform>();

  void Start()
  {
    // Set camera distance from player and rotation
    offset = new Vector3(0, distance, distance * -1);
    transform.rotation = Quaternion.Euler(rotation, 0, 0);
    transform.position = target.position + offset;
  }

  void Update()
  {
    // Makes the camera smoothly follows the Target.
    transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);

    //Start checking if object between Camera and Target.
    if (fadeObjects)
    {
      handleFadeObject();
    }
  }

  void handleFadeObject()
  {
    // If there is a Gameobject that was previously hit by the Raycast, set them back to Opaque.
    if (previousHits.Count > 0)
    {
      foreach (Transform previousHit in previousHits)
      {
        toOpaqueMode(previousHit.GetComponent<Renderer>().material);
      }
    }

    // Cast ray from camera.position to target.position and check if the specified layers are between them.
    Ray rayTarget = new Ray(transform.position, (target.position - transform.position).normalized);

    // Store all the Gameobjects being hit by the ray.
    RaycastHit[] hits = Physics.RaycastAll(rayTarget, maxDistance);

    if (hits.Length > 0)
    {
      handleOpacityHits(hits);
    }
  }

  void toOpaqueMode(Material material)
  {
    material.SetOverrideTag("RenderType", "");
    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    material.SetInt("_ZWrite", 1);
    material.DisableKeyword("_ALPHATEST_ON");
    material.DisableKeyword("_ALPHABLEND_ON");
    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    material.renderQueue = -1;
  }

  void toFadeMode(Material material)
  {
    material.SetOverrideTag("RenderType", "Transparent");
    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    material.SetInt("_ZWrite", 0);
    material.DisableKeyword("_ALPHATEST_ON");
    material.EnableKeyword("_ALPHABLEND_ON");
    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
  }

  void handleOpacityHits(RaycastHit[] hits)
  {
    foreach (RaycastHit hit in hits)
    {
      Transform objectHit = hit.transform;

      if (objectHit.GetComponent<Renderer>())
      {
        // Add to previousHit so it can be set to Opaque later
        previousHits.Add(objectHit);

        // Set the Transparency
        toFadeMode(objectHit.GetComponent<Renderer>().material);
        objectHit.GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
      }
    }
  }
}
