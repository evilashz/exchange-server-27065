using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008FF RID: 2303
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x06005F0A RID: 24330 RVA: 0x00146F3E File Offset: 0x0014513E
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x00146F55 File Offset: 0x00145155
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x00146F66 File Offset: 0x00145166
		[__DynamicallyInvokable]
		public ComImportAttribute()
		{
		}
	}
}
