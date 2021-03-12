using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000076 RID: 118
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Ping", Namespace = "http://tempuri.org/")]
	public class Ping : IExtensibleDataObject
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00003E8C File Offset: 0x0000208C
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00003E94 File Offset: 0x00002094
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

		// Token: 0x0400014B RID: 331
		private ExtensionDataObject extensionDataField;
	}
}
