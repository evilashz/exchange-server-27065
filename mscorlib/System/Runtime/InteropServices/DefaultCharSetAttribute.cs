using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000912 RID: 2322
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		// Token: 0x06005F42 RID: 24386 RVA: 0x00147480 File Offset: 0x00145680
		[__DynamicallyInvokable]
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x0014748F File Offset: 0x0014568F
		[__DynamicallyInvokable]
		public CharSet CharSet
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x04002A75 RID: 10869
		internal CharSet _CharSet;
	}
}
