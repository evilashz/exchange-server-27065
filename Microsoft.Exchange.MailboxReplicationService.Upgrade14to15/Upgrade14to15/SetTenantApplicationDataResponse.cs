using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200003F RID: 63
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetTenantApplicationDataResponse", Namespace = "http://tempuri.org/")]
	public class SetTenantApplicationDataResponse : IExtensibleDataObject
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000343A File Offset: 0x0000163A
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00003442 File Offset: 0x00001642
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

		// Token: 0x040000C1 RID: 193
		private ExtensionDataObject extensionDataField;
	}
}
