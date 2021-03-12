using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200003B RID: 59
	[DebuggerStepThrough]
	[DataContract(Name = "GetDeploymentIdResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetDeploymentIdResponse : IExtensibleDataObject
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003370 File Offset: 0x00001570
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00003378 File Offset: 0x00001578
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003381 File Offset: 0x00001581
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00003389 File Offset: 0x00001589
		[DataMember]
		public string GetDeploymentIdResult
		{
			get
			{
				return this.GetDeploymentIdResultField;
			}
			set
			{
				this.GetDeploymentIdResultField = value;
			}
		}

		// Token: 0x040000B7 RID: 183
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000B8 RID: 184
		private string GetDeploymentIdResultField;
	}
}
