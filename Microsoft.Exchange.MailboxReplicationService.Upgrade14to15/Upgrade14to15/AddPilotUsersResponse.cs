using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200005D RID: 93
	[DataContract(Name = "AddPilotUsersResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class AddPilotUsersResponse : IExtensibleDataObject
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000390D File Offset: 0x00001B0D
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00003915 File Offset: 0x00001B15
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000391E File Offset: 0x00001B1E
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00003926 File Offset: 0x00001B26
		[DataMember]
		public int AddPilotUsersResult
		{
			get
			{
				return this.AddPilotUsersResultField;
			}
			set
			{
				this.AddPilotUsersResultField = value;
			}
		}

		// Token: 0x04000104 RID: 260
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000105 RID: 261
		private int AddPilotUsersResultField;
	}
}
