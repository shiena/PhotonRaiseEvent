// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using UniRx;

namespace PhotonRaiseEvent
{
    public interface IRaiseEventReceiver
    {
        IObservable<EventData> AsObservable(RaiseEventType type);
    }

    public class RaiseEventReceiver : IRaiseEventReceiver
    {
        private readonly IObservable<EventData> _observable;

        public RaiseEventReceiver()
        {
            _observable = Observable.FromEvent<EventData>(
                h => PhotonNetwork.NetworkingClient.EventReceived += h,
                h => PhotonNetwork.NetworkingClient.EventReceived -= h
                )
                .Share();
        }

        public IObservable<EventData> AsObservable(RaiseEventType type)
        {
            return _observable.Where(data => data.Code == (byte) type);
        }
    }
}