using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090B RID: 2315
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		// Token: 0x06005F2E RID: 24366 RVA: 0x00147384 File Offset: 0x00145584
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x00147393 File Offset: 0x00145593
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A67 RID: 10855
		internal bool _val;
	}
}
