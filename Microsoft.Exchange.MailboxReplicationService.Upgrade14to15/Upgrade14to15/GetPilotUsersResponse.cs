using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200006F RID: 111
	[DataContract(Name = "GetPilotUsersResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetPilotUsersResponse : IExtensibleDataObject
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00003C67 File Offset: 0x00001E67
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00003C6F File Offset: 0x00001E6F
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00003C78 File Offset: 0x00001E78
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00003C80 File Offset: 0x00001E80
		[DataMember]
		public UserWorkloadStatusInfo[] GetPilotUsersResult
		{
			get
			{
				return this.GetPilotUsersResultField;
			}
			set
			{
				this.GetPilotUsersResultField = value;
			}
		}

		// Token: 0x0400012E RID: 302
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400012F RID: 303
		private UserWorkloadStatusInfo[] GetPilotUsersResultField;
	}
}
