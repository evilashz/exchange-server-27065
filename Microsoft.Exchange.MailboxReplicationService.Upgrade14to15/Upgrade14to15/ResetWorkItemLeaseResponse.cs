using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200002D RID: 45
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ResetWorkItemLeaseResponse", Namespace = "http://tempuri.org/")]
	public class ResetWorkItemLeaseResponse : IExtensibleDataObject
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003014 File Offset: 0x00001214
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000301C File Offset: 0x0000121C
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

		// Token: 0x0400008B RID: 139
		private ExtensionDataObject extensionDataField;
	}
}
