using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000088 RID: 136
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateGroup", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class UpdateGroup : IExtensibleDataObject
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000435F File Offset: 0x0000255F
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00004367 File Offset: 0x00002567
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00004370 File Offset: 0x00002570
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00004378 File Offset: 0x00002578
		[DataMember]
		public Group[] group
		{
			get
			{
				return this.groupField;
			}
			set
			{
				this.groupField = value;
			}
		}

		// Token: 0x04000190 RID: 400
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000191 RID: 401
		private Group[] groupField;
	}
}
