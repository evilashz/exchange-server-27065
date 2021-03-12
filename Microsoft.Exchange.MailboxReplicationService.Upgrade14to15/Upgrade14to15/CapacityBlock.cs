using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000010 RID: 16
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "CapacityBlock", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	public class CapacityBlock : IExtensibleDataObject
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002846 File Offset: 0x00000A46
		public CapacityBlock(DateTime startDate, int upgradeUnits)
		{
			this.StartDate = startDate;
			this.UpgradeUnits = upgradeUnits;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000285C File Offset: 0x00000A5C
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002864 File Offset: 0x00000A64
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000286D File Offset: 0x00000A6D
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002875 File Offset: 0x00000A75
		[DataMember]
		public DateTime StartDate
		{
			get
			{
				return this.StartDateField;
			}
			set
			{
				this.StartDateField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000287E File Offset: 0x00000A7E
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002886 File Offset: 0x00000A86
		[DataMember]
		public int UpgradeUnits
		{
			get
			{
				return this.UpgradeUnitsField;
			}
			set
			{
				this.UpgradeUnitsField = value;
			}
		}

		// Token: 0x04000026 RID: 38
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000027 RID: 39
		private DateTime StartDateField;

		// Token: 0x04000028 RID: 40
		private int UpgradeUnitsField;
	}
}
