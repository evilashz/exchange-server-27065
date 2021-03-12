using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E2 RID: 1250
	[DataContract]
	public class DeletedMailboxRow : BaseRow
	{
		// Token: 0x06003CF3 RID: 15603 RVA: 0x000B709C File Offset: 0x000B529C
		public DeletedMailboxRow(RemovedMailbox removedMailbox) : base(removedMailbox)
		{
			this.EmailAddress = (from x in removedMailbox.EmailAddresses
			where x.IsPrimaryAddress && x is SmtpProxyAddress
			select x).First<ProxyAddress>().AddressString;
			this.DeletionDate = removedMailbox.WhenChangedUTC.UtcToUserDateTimeString();
			this.DeletionDateTime = ((removedMailbox.WhenChangedUTC != null) ? removedMailbox.WhenChangedUTC.Value : DateTime.MinValue);
		}

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000B7124 File Offset: 0x000B5324
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x000B712C File Offset: 0x000B532C
		[DataMember]
		public string EmailAddress { get; protected set; }

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000B7135 File Offset: 0x000B5335
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000B713D File Offset: 0x000B533D
		[DataMember]
		public string DeletionDate { get; protected set; }

		// Token: 0x17002400 RID: 9216
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000B7146 File Offset: 0x000B5346
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000B714E File Offset: 0x000B534E
		public DateTime DeletionDateTime { get; protected set; }
	}
}
