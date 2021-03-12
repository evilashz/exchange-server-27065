using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000063 RID: 99
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateWorkloadPilotUpgradeStatusResponse", Namespace = "http://tempuri.org/")]
	public class UpdateWorkloadPilotUpgradeStatusResponse : IExtensibleDataObject
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00003A4D File Offset: 0x00001C4D
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00003A55 File Offset: 0x00001C55
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

		// Token: 0x04000114 RID: 276
		private ExtensionDataObject extensionDataField;
	}
}
