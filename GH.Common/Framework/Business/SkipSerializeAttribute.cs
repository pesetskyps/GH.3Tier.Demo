using System;

namespace GH.Common.Framework.Business
{

    /// <summary>
    /// Mark properties so that the marked properties won't be serialized
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SkipSerializeAttribute : Attribute
    {
    }
}
