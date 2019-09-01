﻿// This source code is a part of Koromo Copy Project.
// Copyright (C) 2019. dc-koromo. Licensed under the MIT Licence.

using Koromo_Copy.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koromo_Copy.Framework.Network
{
    public class NetContext
    {

    }

    public class Scheduler<T> where T : IComparable
    {
        UpdatableHeap<T> queue;
    }
}
