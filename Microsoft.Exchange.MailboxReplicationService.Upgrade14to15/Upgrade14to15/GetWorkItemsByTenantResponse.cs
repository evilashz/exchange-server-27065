using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200002B RID: 43
	[DataContract(Name = "GetWorkItemsByTenantResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetWorkItemsByTenantResponse : IExtensibleDataObject
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002FC0 File Offset: 0x000011C0
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00002FC8 File Offset: 0x000011C8
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00002FD9 File Offset: 0x000011D9
		[DataMember]
		public WorkItem[] GetWorkItemsByTenantResult
		{
			get
			{
				return this.GetWorkItemsByTenantResultField;
			}
			set
			{
				this.GetWorkItemsByTenantResultField = value;
			}
		}

		// Token: 0x04000087 RID: 135
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000088 RID: 136
		private WorkItem[] GetWorkItemsByTenantResultField;
	}
}
