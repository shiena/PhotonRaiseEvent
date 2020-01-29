// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections;
using PUN2Rx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PhotonRaiseEvent
{
    public class CubeSpinner : MonoBehaviour
    {
        [SerializeField] private Transform cubeTransform = default;

        [SerializeField] private Button buttonSpin = default;

        private IRaiseEventReceiver _receiver = default;

        [Inject]
        public void Init(IRaiseEventReceiver receiver)
        {
            _receiver = receiver;
        }

        private void Start()
        {
            _receiver.AsObservable(RaiseEventType.RollCube)
                .Subscribe(_ => { StartCoroutine(SpinCube()); })
                .AddTo(this);

            buttonSpin.interactable = false;
            buttonSpin.OnClickAsObservable()
                .Subscribe(_ => { RaiseEventSender.SpinCube(); })
                .AddTo(this);

            this.OnJoinedRoomAsObservable()
                .Subscribe(_ => { buttonSpin.interactable = true; })
                .AddTo(this);
            this.OnLeftRoomAsObservable()
                .Subscribe(_ => { buttonSpin.interactable = false; })
                .AddTo(this);
        }

        private IEnumerator SpinCube()
        {
            var wait = new WaitForFixedUpdate();
            var defaultRotate = cubeTransform.rotation;
            var spinSpeed = new Vector3(0f, 0f, 10f);
            while (true)
            {
                cubeTransform.Rotate(spinSpeed);
                if (cubeTransform.rotation == defaultRotate)
                {
                    yield break;
                }

                yield return wait;
            }
        }
    }
}