// Copyright 2020 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Zenject;

namespace PhotonRaiseEvent
{
    public class RaiseEventInstaller : MonoInstaller<RaiseEventInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IRaiseEventReceiver>().To<RaiseEventReceiver>().AsSingle();
        }
    }
}