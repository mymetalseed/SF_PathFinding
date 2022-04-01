using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// rcSpan³Ø
    /// </summary>
    public class rcSpanPool
    {
        public rcSpanPool? next = null;
        public rcSpan[] items = new rcSpan[Recast.RC_SPANS_PER_POOL];
        public rcSpanPool()
        {
            for(int i = 0; i < items.Length; ++i)
            {
                items[i] = new rcSpan();
            }
        }
    }
}