using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace BitWave_Labs.AnimatedTextReveal
{
    public class AnimateText : MonoBehaviour
    {
        private enum FadeMode { FadeIn, FadeOut, FadeInAndOut }

        [SerializeField] private AnimatedTextReveal animatedTextReveal;
        [SerializeField] private GameObject continueIndicator;
        public List<string> lines;
        [SerializeField] private FadeMode fadeMode;
        [SerializeField] private bool fadeLastLine;

        private int _currentLineIndex = 0;
        private bool _isLineVisible = false;
        private bool _isAnimating = false;
        private bool if1alreadyStartCouroutine = false;

        [SerializeField] private GameObject textBar;
        [SerializeField] private GameObject ButtonEndConversation;
        /* private void Start()
        {
            if (continueIndicator != null) continueIndicator.SetActive(false);
        } */
        void OnEnable()
        {
            if1alreadyStartCouroutine = false;
            ButtonEndConversation.SetActive(false);
            if (continueIndicator != null) continueIndicator.SetActive(false);
            _currentLineIndex = 0;
            _isLineVisible = false;
            _isAnimating = false;
            // + 외부에서 line 갱신시키는것까지 나중에 추가해야한다 
            textBar.SetActive(true);
            InputSpace();
        }

        private void Update()
        {
            // 애니메이션 중이 아니고, 아직 출력할 줄이 남아있다면 실행
            if (Input.GetKeyDown(KeyCode.Space) && !_isAnimating)
            {
                InputSpace();
            }
        }
        private void InputSpace()
        {
            if (lines.Count == 1)
            {
                if (if1alreadyStartCouroutine)
                {
                    textBar.SetActive(false);
                    ButtonEndConversation.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                else if (_currentLineIndex == 0)
                {
                    StartCoroutine(HandleTextStep());
                    if1alreadyStartCouroutine = true;
                }
            }
            else
            {
                if (_currentLineIndex + 1 < lines.Count)// +1 해야 일치됨.
                {
                    StartCoroutine(HandleTextStep());
                }
                else
                {
                    textBar.SetActive(false);
                    ButtonEndConversation.SetActive(true);
                    this.gameObject.SetActive(false);
                }
            }
        }
        private IEnumerator HandleTextStep()
        {
            _isAnimating = true;
            // 1. [상태: 아무것도 안 보이는 중] -> 현재 줄 Fade In 시작
            if (!_isLineVisible)
            {
                if (continueIndicator != null) continueIndicator.SetActive(false);

                animatedTextReveal.TextMesh.text = lines[_currentLineIndex];
                animatedTextReveal.SetAllCharactersAlpha(0);

                if (fadeMode is FadeMode.FadeIn or FadeMode.FadeInAndOut)
                {
                    yield return StartCoroutine(animatedTextReveal.FadeText(true));
                }
                _isLineVisible = true;
                if (continueIndicator != null) continueIndicator.SetActive(true);
            }
            // 2. [상태: 글자가 보이는 중] -> 현재 줄 Fade Out 후, 즉시 다음 줄 Fade In
            else
            {
                if (continueIndicator != null) continueIndicator.SetActive(false);

                // 현재 줄 Fade Out
                if (fadeMode is FadeMode.FadeOut or FadeMode.FadeInAndOut)
                {
                    if (fadeLastLine || _currentLineIndex < lines.Count - 1)
                    {
                        yield return StartCoroutine(animatedTextReveal.FadeText(false));
                    }
                }

                // 다음 줄로 인덱스 증가
                _currentLineIndex++;
                _isLineVisible = false;

                // 다음 줄이 남아있다면 즉시 Fade In 실행
                if (_currentLineIndex < lines.Count)
                {
                    animatedTextReveal.TextMesh.text = lines[_currentLineIndex];
                    animatedTextReveal.SetAllCharactersAlpha(0);

                    if (fadeMode is FadeMode.FadeIn or FadeMode.FadeInAndOut)
                    {
                        yield return StartCoroutine(animatedTextReveal.FadeText(true));
                    }
                    _isLineVisible = true;
                    if (continueIndicator != null) continueIndicator.SetActive(true);
                }
            }
            _isAnimating = false;
        }
    }
}