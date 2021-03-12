using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008FB RID: 2299
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVarAttribute : Attribute
	{
		// Token: 0x06005EFE RID: 24318 RVA: 0x00146DA1 File Offset: 0x00144FA1
		public TypeLibVarAttribute(TypeLibVarFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x00146DB0 File Offset: 0x00144FB0
		public TypeLibVarAttribute(short flags)
		{
			this._val = (TypeLibVarFlags)flags;
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x00146DBF File Offset: 0x00144FBF
		public TypeLibVarFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029EE RID: 10734
		internal TypeLibVarFlags _val;
	}
}
