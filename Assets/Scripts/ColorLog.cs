// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PhotonRaiseEvent
{
    public class ColorLog : MonoBehaviour
    {
        [SerializeField] private Text log = default;

        private const int Limit = 10;
        private readonly Queue<CubeColor> _queue = new Queue<CubeColor>(Limit);

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
                .Subscribe(OnChangeColor)
                .AddTo(this);
        }

        private void OnChangeColor(CubeColor color)
        {
            _queue.Enqueue(color);
            if (_queue.Count > Limit)
            {
                _queue.Dequeue();
            }

            var stringBuilder = new StringBuilder(Limit);
            foreach (var cubeColor in _queue)
            {
                stringBuilder.AppendLine(cubeColor.ToString());
            }

            log.text = stringBuilder.ToString();
        }
    }
}