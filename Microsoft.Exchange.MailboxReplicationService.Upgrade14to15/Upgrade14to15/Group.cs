using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200007B RID: 123
	[DataContract(Name = "Group", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class Group : IExtensibleDataObject
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00003FCF File Offset: 0x000021CF
		public Group(string groupName, DataCenterRegion regionName)
		{
			this.GroupName = groupName;
			this.RegionName = regionName;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003FE5 File Offset: 0x000021E5
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00003FED File Offset: 0x000021ED
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003FF6 File Offset: 0x000021F6
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00003FFE File Offset: 0x000021FE
		[DataMember]
		public string GroupName
		{
			get
			{
				return this.GroupNameField;
			}
			set
			{
				this.GroupNameField = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00004007 File Offset: 0x00002207
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000400F File Offset: 0x0000220F
		[DataMember]
		public DataCenterRegion RegionName
		{
			get
			{
				return this.RegionNameField;
			}
			set
			{
				this.RegionNameField = value;
			}
		}

		// Token: 0x04000159 RID: 345
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400015A RID: 346
		private string GroupNameField;

		// Token: 0x0400015B RID: 347
		private DataCenterRegion RegionNameField;
	}
}
