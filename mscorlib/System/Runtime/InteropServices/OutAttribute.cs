using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000903 RID: 2307
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x06005F15 RID: 24341 RVA: 0x00146FD6 File Offset: 0x001451D6
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x00146FE7 File Offset: 0x001451E7
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOut;
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x00146FEF File Offset: 0x001451EF
		[__DynamicallyInvokable]
		public OutAttribute()
		{
		}
	}
}
