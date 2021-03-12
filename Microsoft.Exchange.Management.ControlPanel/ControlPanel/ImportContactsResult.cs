using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.Aggregation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002BA RID: 698
	[DataContract]
	public class ImportContactsResult : BaseRow
	{
		// Token: 0x06002C09 RID: 11273 RVA: 0x00088C5A File Offset: 0x00086E5A
		public ImportContactsResult(ImportContactListResult result) : base(result)
		{
			this.ImportedContactsResult = result;
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x00088C6A File Offset: 0x00086E6A
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x00088C72 File Offset: 0x00086E72
		public ImportContactListResult ImportedContactsResult { get; private set; }

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x00088C7B File Offset: 0x00086E7B
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x00088C88 File Offset: 0x00086E88
		[DataMember]
		public int ContactsImported
		{
			get
			{
				return this.ImportedContactsResult.ContactsImported;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
