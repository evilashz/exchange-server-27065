using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000011 RID: 17
	[DebuggerStepThrough]
	[DataContract(Name = "GroupBlackout", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GroupBlackout : IExtensibleDataObject
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000288F File Offset: 0x00000A8F
		public GroupBlackout(string groupName, BlackoutInterval[] intervals)
		{
			this.GroupName = groupName;
			this.Intervals = intervals;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028A5 File Offset: 0x00000AA5
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000028AD File Offset: 0x00000AAD
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000028B6 File Offset: 0x00000AB6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000028BE File Offset: 0x00000ABE
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028C7 File Offset: 0x00000AC7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000028CF File Offset: 0x00000ACF
		[DataMember]
		public BlackoutInterval[] Intervals
		{
			get
			{
				return this.IntervalsField;
			}
			set
			{
				this.IntervalsField = value;
			}
		}

		// Token: 0x04000029 RID: 41
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400002A RID: 42
		private string GroupNameField;

		// Token: 0x0400002B RID: 43
		private BlackoutInterval[] IntervalsField;
	}
}
