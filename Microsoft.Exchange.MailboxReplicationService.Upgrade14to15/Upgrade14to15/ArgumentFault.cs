using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000053 RID: 83
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
	public class ArgumentFault : IExtensibleDataObject
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00003793 File Offset: 0x00001993
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000379B File Offset: 0x0000199B
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000037AC File Offset: 0x000019AC
		[DataMember]
		public string ArgumentName
		{
			get
			{
				return this.ArgumentNameField;
			}
			set
			{
				this.ArgumentNameField = value;
			}
		}

		// Token: 0x040000EA RID: 234
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000EB RID: 235
		private string ArgumentNameField;
	}
}
