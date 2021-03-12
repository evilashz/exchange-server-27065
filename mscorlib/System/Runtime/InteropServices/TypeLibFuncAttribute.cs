using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008FA RID: 2298
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		// Token: 0x06005EFB RID: 24315 RVA: 0x00146D7B File Offset: 0x00144F7B
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x00146D8A File Offset: 0x00144F8A
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x00146D99 File Offset: 0x00144F99
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029ED RID: 10733
		internal TypeLibFuncFlags _val;
	}
}
