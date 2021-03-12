using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C1 RID: 1217
	[DataContract]
	public class UMMailboxRow : BaseRow
	{
		// Token: 0x06003BD8 RID: 15320 RVA: 0x000B49F0 File Offset: 0x000B2BF0
		public UMMailboxRow(UMMailbox umMailbox) : base(umMailbox)
		{
			this.UMMailbox = umMailbox;
		}

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000B4A00 File Offset: 0x000B2C00
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000B4A08 File Offset: 0x000B2C08
		internal UMMailbox UMMailbox { get; private set; }
	}
}
