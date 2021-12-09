using System;

namespace Development.Global.Code.NightAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class LazyFindAttribute : Attribute { }
}