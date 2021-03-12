using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003EC RID: 1004
	[DataContract]
	public class NonOwnerAccessDetailRow : BaseRow
	{
		// Token: 0x06003366 RID: 13158 RVA: 0x0009FDCF File Offset: 0x0009DFCF
		public NonOwnerAccessDetailRow(MailboxAuditLogEvent logEvent) : base(logEvent)
		{
			this.MailboxAuditLogEvent = logEvent;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x0009FDDF File Offset: 0x0009DFDF
		internal NonOwnerAccessDetailRow(Identity id, MailboxAuditLogEvent searchResult) : base(id, searchResult)
		{
			this.MailboxAuditLogEvent = searchResult;
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x0009FDF0 File Offset: 0x0009DFF0
		// (set) Token: 0x06003369 RID: 13161 RVA: 0x0009FDF8 File Offset: 0x0009DFF8
		public MailboxAuditLogEvent MailboxAuditLogEvent { get; private set; }
	}
}
