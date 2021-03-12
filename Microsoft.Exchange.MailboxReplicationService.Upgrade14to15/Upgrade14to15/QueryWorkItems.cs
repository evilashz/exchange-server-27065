using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000082 RID: 130
	[DataContract(Name = "QueryWorkItems", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class QueryWorkItems : IExtensibleDataObject
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000421F File Offset: 0x0000241F
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00004227 File Offset: 0x00002427
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00004230 File Offset: 0x00002430
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00004238 File Offset: 0x00002438
		[DataMember]
		public string groupName
		{
			get
			{
				return this.groupNameField;
			}
			set
			{
				this.groupNameField = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00004241 File Offset: 0x00002441
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00004249 File Offset: 0x00002449
		[DataMember]
		public string tenantTier
		{
			get
			{
				return this.tenantTierField;
			}
			set
			{
				this.tenantTierField = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00004252 File Offset: 0x00002452
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000425A File Offset: 0x0000245A
		[DataMember]
		public string workItemType
		{
			get
			{
				return this.workItemTypeField;
			}
			set
			{
				this.workItemTypeField = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00004263 File Offset: 0x00002463
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000426B File Offset: 0x0000246B
		[DataMember(Order = 3)]
		public WorkItemStatus status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00004274 File Offset: 0x00002474
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000427C File Offset: 0x0000247C
		[DataMember(Order = 4)]
		public int pageSize
		{
			get
			{
				return this.pageSizeField;
			}
			set
			{
				this.pageSizeField = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00004285 File Offset: 0x00002485
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000428D File Offset: 0x0000248D
		[DataMember(Order = 5)]
		public byte[] bookmark
		{
			get
			{
				return this.bookmarkField;
			}
			set
			{
				this.bookmarkField = value;
			}
		}

		// Token: 0x04000180 RID: 384
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000181 RID: 385
		private string groupNameField;

		// Token: 0x04000182 RID: 386
		private string tenantTierField;

		// Token: 0x04000183 RID: 387
		private string workItemTypeField;

		// Token: 0x04000184 RID: 388
		private WorkItemStatus statusField;

		// Token: 0x04000185 RID: 389
		private int pageSizeField;

		// Token: 0x04000186 RID: 390
		private byte[] bookmarkField;
	}
}
