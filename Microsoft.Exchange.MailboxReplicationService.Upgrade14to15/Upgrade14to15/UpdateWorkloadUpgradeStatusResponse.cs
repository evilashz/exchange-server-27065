using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000061 RID: 97
	[DebuggerStepThrough]
	[DataContract(Name = "UpdateWorkloadUpgradeStatusResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UpdateWorkloadUpgradeStatusResponse : IExtensibleDataObject
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000039D7 File Offset: 0x00001BD7
		// (set) Token: 0x06000251 RID: 593 RVA: 0x000039DF File Offset: 0x00001BDF
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

		// Token: 0x0400010E RID: 270
		private ExtensionDataObject extensionDataField;
	}
}
