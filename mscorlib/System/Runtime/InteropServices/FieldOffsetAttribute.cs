using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000909 RID: 2313
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x06005F28 RID: 24360 RVA: 0x00147300 File Offset: 0x00145500
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			int offset;
			if (field.DeclaringType != null && field.GetRuntimeModule().MetadataImport.GetFieldOffset(field.DeclaringType.MetadataToken, field.MetadataToken, out offset))
			{
				return new FieldOffsetAttribute(offset);
			}
			return null;
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0014734B File Offset: 0x0014554B
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return FieldOffsetAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x00147356 File Offset: 0x00145556
		[__DynamicallyInvokable]
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x00147365 File Offset: 0x00145565
		[__DynamicallyInvokable]
		public int Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A65 RID: 10853
		internal int _val;
	}
}
