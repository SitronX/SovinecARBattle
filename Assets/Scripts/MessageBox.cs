using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    // Start is called before the first frame update
    Action okClicked;
    Action backClicked;

    [SerializeField] TextMeshProUGUI questionTextMesh;
    [SerializeField] TextMeshProUGUI okTextMesh;
    [SerializeField] TextMeshProUGUI backTextMesh;

    private Animator messageAnimator;

    private void Start()
    {
        messageAnimator = GetComponent<Animator>();
    }

    public void ShowDialogue(string question_,string okText_,string back_text)
    {
        questionTextMesh.text = question_;
        okTextMesh.text = okText_;
        backTextMesh.text = back_text;
        messageAnimator.SetBool("Hidden", false);
    }

    public void OnOkClicked()
    {
        okClicked?.Invoke();
    }
    public void OnBackClicked()
    {
        backClicked?.Invoke();
    }

}
