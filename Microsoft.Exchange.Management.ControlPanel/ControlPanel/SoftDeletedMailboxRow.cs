using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E6 RID: 1254
	[DataContract]
	public class SoftDeletedMailboxRow : BaseRow
	{
		// Token: 0x06003D00 RID: 15616 RVA: 0x000B71A0 File Offset: 0x000B53A0
		public SoftDeletedMailboxRow(Mailbox softDeletedMailbox) : base(softDeletedMailbox)
		{
			this.EmailAddress = (from x in softDeletedMailbox.EmailAddresses
			where x.IsPrimaryAddress && x is SmtpProxyAddress
			select x).First<ProxyAddress>().AddressString;
			this.DeletionDate = softDeletedMailbox.WhenSoftDeleted.Value.ToUniversalTime().UtcToUserDateTimeString();
			this.DeletionDateTime = ((softDeletedMailbox.WhenSoftDeleted != null) ? softDeletedMailbox.WhenSoftDeleted.Value : DateTime.MinValue);
		}

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000B7238 File Offset: 0x000B5438
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000B7240 File Offset: 0x000B5440
		[DataMember]
		public string EmailAddress { get; protected set; }

		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000B7249 File Offset: 0x000B5449
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x000B7251 File Offset: 0x000B5451
		[DataMember]
		public string DeletionDate { get; protected set; }

		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000B725A File Offset: 0x000B545A
		// (set) Token: 0x06003D06 RID: 15622 RVA: 0x000B7262 File Offset: 0x000B5462
		public DateTime DeletionDateTime { get; protected set; }
	}
}
