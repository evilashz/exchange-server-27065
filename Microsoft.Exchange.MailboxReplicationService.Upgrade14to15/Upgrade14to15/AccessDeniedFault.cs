using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000052 RID: 82
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
	public class AccessDeniedFault : IExtensibleDataObject
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000377A File Offset: 0x0000197A
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00003782 File Offset: 0x00001982
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

		// Token: 0x040000E9 RID: 233
		private ExtensionDataObject extensionDataField;
	}
}
