using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000069 RID: 105
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetTenantPhaseResponse", Namespace = "http://tempuri.org/")]
	public class GetTenantPhaseResponse : IExtensibleDataObject
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00003B49 File Offset: 0x00001D49
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00003B51 File Offset: 0x00001D51
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00003B5A File Offset: 0x00001D5A
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00003B62 File Offset: 0x00001D62
		[DataMember]
		public string GetTenantPhaseResult
		{
			get
			{
				return this.GetTenantPhaseResultField;
			}
			set
			{
				this.GetTenantPhaseResultField = value;
			}
		}

		// Token: 0x04000120 RID: 288
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000121 RID: 289
		private string GetTenantPhaseResultField;
	}
}
