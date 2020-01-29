// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace PhotonRaiseEvent
{
    public partial class RaiseEventSender
    {
        public static bool SpinCube()
        {
            var raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All,
                CachingOption = EventCaching.DoNotCache
            };
            return PhotonNetwork.RaiseEvent((byte) RaiseEventType.RollCube, null, raiseEventOptions,
                SendOptions.SendReliable);
        }
    }
}