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
        public static bool SendChangeColorEvent(CubeColor cubeColor)
        {
            var content = (int) cubeColor;
            var raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All,
                CachingOption = EventCaching.AddToRoomCache
            };
            return PhotonNetwork.RaiseEvent((byte) RaiseEventType.ChangeColor, content, raiseEventOptions,
                SendOptions.SendReliable);
        }
    }
}