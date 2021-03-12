using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200014E RID: 334
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public sealed class PointerTypeAttribute : Attribute
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x00023AA6 File Offset: 0x00021CA6
		public PointerTypeAttribute(string pointerType)
		{
			this.pointerType = pointerType;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00023AB5 File Offset: 0x00021CB5
		public string InherentType
		{
			get
			{
				return this.pointerType;
			}
		}

		// Token: 0x0400066C RID: 1644
		private readonly string pointerType;
	}
}
