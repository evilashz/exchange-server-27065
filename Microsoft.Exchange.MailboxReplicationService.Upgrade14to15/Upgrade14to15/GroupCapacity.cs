using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200000F RID: 15
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GroupCapacity", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	[DebuggerStepThrough]
	public class GroupCapacity : IExtensibleDataObject
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000027FD File Offset: 0x000009FD
		public GroupCapacity(string groupName, CapacityBlock[] capacities)
		{
			this.GroupName = groupName;
			this.Capacities = capacities;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002813 File Offset: 0x00000A13
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000281B File Offset: 0x00000A1B
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002824 File Offset: 0x00000A24
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000282C File Offset: 0x00000A2C
		[DataMember]
		public CapacityBlock[] Capacities
		{
			get
			{
				return this.CapacitiesField;
			}
			set
			{
				this.CapacitiesField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002835 File Offset: 0x00000A35
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000283D File Offset: 0x00000A3D
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

		// Token: 0x04000023 RID: 35
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000024 RID: 36
		private CapacityBlock[] CapacitiesField;

		// Token: 0x04000025 RID: 37
		private string GroupNameField;
	}
}
