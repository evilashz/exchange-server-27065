using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000054 RID: 84
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "InvalidOperationFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
	[KnownType(typeof(CancelNotAllowedFault))]
	[DebuggerStepThrough]
	public class InvalidOperationFault : IExtensibleDataObject
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000037BD File Offset: 0x000019BD
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000037C5 File Offset: 0x000019C5
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

		// Token: 0x040000EC RID: 236
		private ExtensionDataObject extensionDataField;
	}
}
