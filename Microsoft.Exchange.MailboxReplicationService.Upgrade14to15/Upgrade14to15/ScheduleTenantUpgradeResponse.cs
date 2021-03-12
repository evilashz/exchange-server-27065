using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000031 RID: 49
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ScheduleTenantUpgradeResponse", Namespace = "http://tempuri.org/")]
	public class ScheduleTenantUpgradeResponse : IExtensibleDataObject
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000030BC File Offset: 0x000012BC
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000030C4 File Offset: 0x000012C4
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

		// Token: 0x04000093 RID: 147
		private ExtensionDataObject extensionDataField;
	}
}
