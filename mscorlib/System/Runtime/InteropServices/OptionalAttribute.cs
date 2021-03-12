using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000904 RID: 2308
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x06005F18 RID: 24344 RVA: 0x00146FF7 File Offset: 0x001451F7
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x00147008 File Offset: 0x00145208
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOptional;
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x00147010 File Offset: 0x00145210
		[__DynamicallyInvokable]
		public OptionalAttribute()
		{
		}
	}
}
