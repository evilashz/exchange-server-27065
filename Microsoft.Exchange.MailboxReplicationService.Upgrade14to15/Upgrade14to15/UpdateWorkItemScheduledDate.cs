using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200002E RID: 46
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "UpdateWorkItemScheduledDate", Namespace = "http://tempuri.org/")]
	public class UpdateWorkItemScheduledDate : IExtensibleDataObject
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000302D File Offset: 0x0000122D
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003035 File Offset: 0x00001235
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000303E File Offset: 0x0000123E
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003046 File Offset: 0x00001246
		[DataMember]
		public string workItemId
		{
			get
			{
				return this.workItemIdField;
			}
			set
			{
				this.workItemIdField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000304F File Offset: 0x0000124F
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00003057 File Offset: 0x00001257
		[DataMember(Order = 1)]
		public DateTime scheduledDate
		{
			get
			{
				return this.scheduledDateField;
			}
			set
			{
				this.scheduledDateField = value;
			}
		}

		// Token: 0x0400008C RID: 140
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400008D RID: 141
		private string workItemIdField;

		// Token: 0x0400008E RID: 142
		private DateTime scheduledDateField;
	}
}
