using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000077 RID: 119
	[DataContract(Name = "PingResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class PingResponse : IExtensibleDataObject
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00003EA5 File Offset: 0x000020A5
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00003EAD File Offset: 0x000020AD
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00003EB6 File Offset: 0x000020B6
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00003EBE File Offset: 0x000020BE
		[DataMember]
		public string PingResult
		{
			get
			{
				return this.PingResultField;
			}
			set
			{
				this.PingResultField = value;
			}
		}

		// Token: 0x0400014C RID: 332
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400014D RID: 333
		private string PingResultField;
	}
}
