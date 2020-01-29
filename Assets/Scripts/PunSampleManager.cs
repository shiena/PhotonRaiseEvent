// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using PUN2Rx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PhotonRaiseEvent
{
    public class PunSampleManager : MonoBehaviour
    {
        [SerializeField] private MeshRenderer cubeMesh = default;

        [SerializeField] private Button buttonRed = default;
        [SerializeField] private Button buttonBlue = default;
        [SerializeField] private Button buttonGreen = default;

        private IRaiseEventReceiver _receiver = default;

        [Inject]
        public void Init(IRaiseEventReceiver receiver)
        {
            _receiver = receiver;
        }

        private void Start()
        {
            _receiver.AsObservable(RaiseEventType.ChangeColor)
                .Select(data => data.ToEnum<CubeColor>())
                .Subscribe(ChangeColor)
                .AddTo(this);

            buttonRed.interactable = false;
            buttonBlue.interactable = false;
            buttonGreen.interactable = false;

            buttonRed.OnClickAsObservable()
                .Subscribe(_ => { RaiseEventSender.SendChangeColorEvent(CubeColor.Red); })
                .AddTo(this);
            buttonBlue.OnClickAsObservable()
                .Subscribe(_ => { RaiseEventSender.SendChangeColorEvent(CubeColor.Blue); })
                .AddTo(this);
            buttonGreen.OnClickAsObservable()
                .Subscribe(_ => { RaiseEventSender.SendChangeColorEvent(CubeColor.Green); })
                .AddTo(this);

            this.OnJoinedRoomAsObservable()
                .Subscribe(_ =>
                {
                    buttonRed.interactable = true;
                    buttonBlue.interactable = true;
                    buttonGreen.interactable = true;
                })
                .AddTo(this);

            this.OnLeftRoomAsObservable()
                .Subscribe(_ =>
                {
                    buttonRed.interactable = false;
                    buttonBlue.interactable = false;
                    buttonGreen.interactable = false;
                })
                .AddTo(this);
        }

        private void ChangeColor(CubeColor color)
        {
            Color change;
            switch (color)
            {
                case CubeColor.Red:
                    change = Color.red;
                    break;
                case CubeColor.Blue:
                    change = Color.blue;
                    break;
                case CubeColor.Green:
                    change = Color.green;
                    break;
                default:
                    return;
            }

            cubeMesh.material.color = change;
        }
    }
}