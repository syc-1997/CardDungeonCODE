
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����������
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource;

    private void Awake()
    {
        Instance = this;
    }

    //��ʼ��
    public void Init()
    {
        bgmSource =  gameObject.AddComponent<AudioSource>();
    }

    //����bgm
    public void PlayBGM(string name, bool isLoop = true)
    {
        //����bgm��������
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);

        bgmSource.clip = clip;//������Ƶ

        bgmSource.loop = isLoop;

        bgmSource.Play();
    }

    //������Ч
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);

        AudioSource.PlayClipAtPoint(clip, transform.position);//����
    }
}
