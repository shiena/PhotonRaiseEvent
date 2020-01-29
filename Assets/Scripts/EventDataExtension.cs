// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;
using ExitGames.Client.Photon;

namespace PhotonRaiseEvent
{
    public static partial class EventDataExtension
    {
        public static T ToEnum<T>(this EventData data) where T : Enum
        {
            return (T) Enum.ToObject(typeof(T), data.CustomData);
        }
    }
}