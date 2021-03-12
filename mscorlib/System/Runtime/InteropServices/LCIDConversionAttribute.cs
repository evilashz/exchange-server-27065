using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008ED RID: 2285
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		// Token: 0x06005EE6 RID: 24294 RVA: 0x00146BD0 File Offset: 0x00144DD0
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06005EE7 RID: 24295 RVA: 0x00146BDF File Offset: 0x00144DDF
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029B8 RID: 10680
		internal int _val;
	}
}
