using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C2 RID: 194
	[AttributeUsage(AttributeTargets.Field)]
	internal class SKUCapabilityAttribute : Attribute
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x0002D272 File Offset: 0x0002B472
		public SKUCapabilityAttribute(string capabilityGuid)
		{
			this.Guid = new Guid(capabilityGuid);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002D286 File Offset: 0x0002B486
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0002D28E File Offset: 0x0002B48E
		public Guid Guid { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002D297 File Offset: 0x0002B497
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0002D29F File Offset: 0x0002B49F
		public bool AddOnSKU { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002D2A8 File Offset: 0x0002B4A8
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		public bool Free { get; set; }
	}
}
