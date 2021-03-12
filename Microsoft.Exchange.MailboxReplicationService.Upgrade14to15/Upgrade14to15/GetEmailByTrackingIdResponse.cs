using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000049 RID: 73
	[DebuggerStepThrough]
	[DataContract(Name = "GetEmailByTrackingIdResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetEmailByTrackingIdResponse : IExtensibleDataObject
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00003600 File Offset: 0x00001800
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00003608 File Offset: 0x00001808
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00003611 File Offset: 0x00001811
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00003619 File Offset: 0x00001819
		[DataMember]
		public EmailDefinition GetEmailByTrackingIdResult
		{
			get
			{
				return this.GetEmailByTrackingIdResultField;
			}
			set
			{
				this.GetEmailByTrackingIdResultField = value;
			}
		}

		// Token: 0x040000D7 RID: 215
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D8 RID: 216
		private EmailDefinition GetEmailByTrackingIdResultField;
	}
}
