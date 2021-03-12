using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E6 RID: 998
	[DataContract]
	public class NonOwnerAccessResultRow : BaseRow
	{
		// Token: 0x06003346 RID: 13126 RVA: 0x0009F2C6 File Offset: 0x0009D4C6
		public NonOwnerAccessResultRow(MailboxAuditLogRecord searchResult)
		{
			this.NonOwnerAccessResult = searchResult;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x0009F2D5 File Offset: 0x0009D4D5
		internal NonOwnerAccessResultRow(Identity id, MailboxAuditLogRecord searchResult) : base(id, searchResult)
		{
			this.NonOwnerAccessResult = searchResult;
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x0009F2E6 File Offset: 0x0009D4E6
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x0009F2EE File Offset: 0x0009D4EE
		public MailboxAuditLogRecord NonOwnerAccessResult { get; private set; }

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x0009F2F7 File Offset: 0x0009D4F7
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x0009F304 File Offset: 0x0009D504
		[DataMember]
		public string Mailbox
		{
			get
			{
				return this.NonOwnerAccessResult.MailboxResolvedOwnerName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x0009F30C File Offset: 0x0009D50C
		// (set) Token: 0x0600334D RID: 13133 RVA: 0x0009F339 File Offset: 0x0009D539
		[DataMember]
		public string LastAccessed
		{
			get
			{
				return this.NonOwnerAccessResult.LastAccessed.Value.ToUniversalTime().UtcToUserDateTimeString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
