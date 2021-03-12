using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000043 RID: 67
	[DebuggerStepThrough]
	[DataContract(Name = "SetUserApplicationDataResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SetUserApplicationDataResponse : IExtensibleDataObject
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00003504 File Offset: 0x00001704
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000350C File Offset: 0x0000170C
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

		// Token: 0x040000CB RID: 203
		private ExtensionDataObject extensionDataField;
	}
}
