using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200005B RID: 91
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "MovePartnerTenantUpgradeDateResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class MovePartnerTenantUpgradeDateResponse : IExtensibleDataObject
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000038B9 File Offset: 0x00001AB9
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000038C1 File Offset: 0x00001AC1
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

		// Token: 0x04000100 RID: 256
		private ExtensionDataObject extensionDataField;
	}
}
