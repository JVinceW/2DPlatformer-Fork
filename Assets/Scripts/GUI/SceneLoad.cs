using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{

    static public SceneLoad instance;
    public delegate void OnMapLoaded(Vector3 vec);
    public OnMapLoaded onMapLoadedCallBack;

    Animator m_Anim;
    //Scene currentScene;
    //float timeDelay=1f;
    //float currenttime = 0;
    private void Awake()
    {
        Logs.LogD(SceneManager.GetActiveScene().name);
        GameObject[] objs = GameObject.FindGameObjectsWithTag(this.gameObject.tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("Loading", true);
    }
    // Start is called before the first frame update
    void Start()
    {
        //currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            if (m_Anim.GetBool("Loading") != false)
            {
                if(!IsInvoking())
                {
                    Invoke("Loaded", 2f);
                }
            }
        }
    }

    void Loaded()
    {
        SetPlayerPos().Wait();
        m_Anim.SetBool("Loading", false);
    }

    async Task SetPlayerPos()
    {
        GameObject go = GameObject.Find("PointAppear");
        if (go != null)
        {
            if(onMapLoadedCallBack != null)
            {
                onMapLoadedCallBack.Invoke(go.transform.position);
            }
        }
    }
    public void LoadScreen(int level)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(LoadAsyn(level));
    }
    IEnumerator LoadAsyn(int sceneIndex)
    {
        m_Anim.SetBool("Loading", true);
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            yield return null;
        }
        SetPlayerPos().Wait();
        yield return new WaitForSeconds(2);
        m_Anim.SetBool("Loading", false);
    }
}
