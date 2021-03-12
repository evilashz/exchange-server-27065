using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F2 RID: 242
	[SimpleConfiguration("TargetFolderMRU", "TargetFolderMRU")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TargetFolderMRUEntry
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0001CB70 File Offset: 0x0001AD70
		public TargetFolderMRUEntry()
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001CB78 File Offset: 0x0001AD78
		public TargetFolderMRUEntry(OwaStoreObjectId owaStoreObjectIdFolderId)
		{
			this.owaStoreObjectIdFolderId = owaStoreObjectIdFolderId.ToString();
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0001CB94 File Offset: 0x0001AD94
		[SimpleConfigurationProperty("folderId")]
		public string FolderId
		{
			get
			{
				return this.owaStoreObjectIdFolderId;
			}
			set
			{
				this.owaStoreObjectIdFolderId = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001CB9D File Offset: 0x0001AD9D
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0001CBA5 File Offset: 0x0001ADA5
		[SimpleConfigurationProperty("ewsFolderId")]
		[DataMember]
		public string EwsFolderIdEntry
		{
			get
			{
				return this.ewsFolderIdEntry;
			}
			set
			{
				this.ewsFolderIdEntry = value;
			}
		}

		// Token: 0x04000561 RID: 1377
		private string owaStoreObjectIdFolderId;

		// Token: 0x04000562 RID: 1378
		private string ewsFolderIdEntry;
	}
}
