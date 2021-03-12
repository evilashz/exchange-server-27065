using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200006E RID: 110
	[DataContract(Name = "GetPilotUsers", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetPilotUsers : IExtensibleDataObject
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00003C3D File Offset: 0x00001E3D
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00003C45 File Offset: 0x00001E45
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00003C4E File Offset: 0x00001E4E
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00003C56 File Offset: 0x00001E56
		[DataMember]
		public Guid tenantId
		{
			get
			{
				return this.tenantIdField;
			}
			set
			{
				this.tenantIdField = value;
			}
		}

		// Token: 0x0400012C RID: 300
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400012D RID: 301
		private Guid tenantIdField;
	}
}
