using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000087 RID: 135
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateTenantReadinessResponse", Namespace = "http://tempuri.org/")]
	public class UpdateTenantReadinessResponse : IExtensibleDataObject
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00004346 File Offset: 0x00002546
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000434E File Offset: 0x0000254E
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

		// Token: 0x0400018F RID: 399
		private ExtensionDataObject extensionDataField;
	}
}
