using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000027 RID: 39
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeleteWorkItemResponse", Namespace = "http://tempuri.org/")]
	public class DeleteWorkItemResponse : IExtensibleDataObject
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002F29 File Offset: 0x00001129
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002F31 File Offset: 0x00001131
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

		// Token: 0x04000080 RID: 128
		private ExtensionDataObject extensionDataField;
	}
}
