using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// �߶ȳ���Span(�ϰ���?)
    /// </summary>
    public class rcSpan
    {
        public ushort smin;//��ǰspan������Сֵ
        public ushort smax;//��ǰspan�������ֵ
        public byte area;  //��ǰspan���
        public rcSpan? next = null; //���߿����һ����Span
    }
}