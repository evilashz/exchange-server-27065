using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000079 RID: 121
	[DebuggerStepThrough]
	[DataContract(Name = "CancelTenantUpgradeResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class CancelTenantUpgradeResponse : IExtensibleDataObject
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00003F0A File Offset: 0x0000210A
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00003F12 File Offset: 0x00002112
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

		// Token: 0x04000151 RID: 337
		private ExtensionDataObject extensionDataField;
	}
}
