using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Demo.Collisions;

namespace Demo.Interfaces
{
    interface Tracker
    {
        BoundingShape b { get; set; }
        Vector2 position { get; set; }
        int width { get; set; }
        int height { get; set; }
    }
}
