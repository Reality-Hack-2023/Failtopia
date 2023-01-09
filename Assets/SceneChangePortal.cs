using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangePortal : MonoBehaviour
{
    [SerializeField] ParticleSystem ActivePS;
    public bool isActive = false;
    [SerializeField] string PortalJumpToScene = "Collaboration";
    public int WaitTime;
    [SerializeField] PinPad PinPadScript;

    private void Awake()
    {
        ActivePS.Stop();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoToScene(PortalJumpToScene);
        }
    }

    public void GoToScene(string scene)
    {
        Debug.Log("We made it to GoToScene");
        if (isActive)
        {
            Debug.Log("Should load scene portal jump");
            SceneManager.LoadScene(scene);
            isActive = false;
        }
        else
        {
            Debug.Log("Portal not active");
        }
    }

    public void ActivatePortal()
    {
        Debug.Log("Portal Activated");
        ActivePS.Play();
        StartCoroutine(PortalTimeOut());
    }

    private IEnumerator PortalTimeOut()
    {
        yield return new WaitForSeconds(WaitTime);
        isActive = false;
        ActivePS.Stop();
        Debug.Log("Portal Deactivated");
        PinPadScript.ClearKeysOut();
        
    }


}
