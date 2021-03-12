using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000048 RID: 72
	[DataContract(Name = "GetEmailByTrackingId", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetEmailByTrackingId : IExtensibleDataObject
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000035D6 File Offset: 0x000017D6
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000035DE File Offset: 0x000017DE
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000035E7 File Offset: 0x000017E7
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000035EF File Offset: 0x000017EF
		[DataMember]
		public Guid trackingId
		{
			get
			{
				return this.trackingIdField;
			}
			set
			{
				this.trackingIdField = value;
			}
		}

		// Token: 0x040000D5 RID: 213
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D6 RID: 214
		private Guid trackingIdField;
	}
}
