using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000901 RID: 2305
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06005F0F RID: 24335 RVA: 0x00146F85 File Offset: 0x00145185
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x00146F9C File Offset: 0x0014519C
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x00146FAD File Offset: 0x001451AD
		[__DynamicallyInvokable]
		public PreserveSigAttribute()
		{
		}
	}
}
