using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000062 RID: 98
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateWorkloadPilotUpgradeStatus", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class UpdateWorkloadPilotUpgradeStatus : IExtensibleDataObject
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000039F0 File Offset: 0x00001BF0
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000039F8 File Offset: 0x00001BF8
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00003A09 File Offset: 0x00001C09
		[DataMember]
		public Guid tenantId
		{
			get
			{
				return this.tenantIdField;
			}
			set
			{
				this.tenantIdField = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00003A12 File Offset: 0x00001C12
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00003A1A File Offset: 0x00001C1A
		[DataMember]
		public string workloadName
		{
			get
			{
				return this.workloadNameField;
			}
			set
			{
				this.workloadNameField = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00003A23 File Offset: 0x00001C23
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00003A2B File Offset: 0x00001C2B
		[DataMember(Order = 2)]
		public string pilotUserUpn
		{
			get
			{
				return this.pilotUserUpnField;
			}
			set
			{
				this.pilotUserUpnField = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00003A34 File Offset: 0x00001C34
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00003A3C File Offset: 0x00001C3C
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

		// Token: 0x0400010F RID: 271
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000110 RID: 272
		private Guid tenantIdField;

		// Token: 0x04000111 RID: 273
		private string workloadNameField;

		// Token: 0x04000112 RID: 274
		private string pilotUserUpnField;

		// Token: 0x04000113 RID: 275
		private WorkItemStatus statusField;
	}
}
