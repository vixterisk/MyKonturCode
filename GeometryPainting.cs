using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;
using System.Drawing;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        static Dictionary<Segment, Color> segmentColors = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color newColor)
        {
            if (!segmentColors.ContainsKey(segment))
                segmentColors.Add(segment, newColor);
            else
                segmentColors[segment] = newColor;
        }

        public static Color GetColor(this Segment segment)
        {
            if (!segmentColors.ContainsKey(segment))
                return Color.Black;
            return segmentColors[segment];
        }
    }
}
