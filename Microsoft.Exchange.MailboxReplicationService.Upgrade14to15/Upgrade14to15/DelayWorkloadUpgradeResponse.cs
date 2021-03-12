using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200006B RID: 107
	[DebuggerStepThrough]
	[DataContract(Name = "DelayWorkloadUpgradeResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DelayWorkloadUpgradeResponse : IExtensibleDataObject
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00003BAE File Offset: 0x00001DAE
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00003BB6 File Offset: 0x00001DB6
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

		// Token: 0x04000125 RID: 293
		private ExtensionDataObject extensionDataField;
	}
}
