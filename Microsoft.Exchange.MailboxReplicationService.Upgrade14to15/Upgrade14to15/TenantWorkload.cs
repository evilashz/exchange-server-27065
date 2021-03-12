using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000016 RID: 22
	[DataContract(Name = "TenantWorkload", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class TenantWorkload : IExtensibleDataObject
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002BA4 File Offset: 0x00000DA4
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002BAC File Offset: 0x00000DAC
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002BB5 File Offset: 0x00000DB5
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002BBD File Offset: 0x00000DBD
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002BC6 File Offset: 0x00000DC6
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002BCE File Offset: 0x00000DCE
		[DataMember]
		public string WorkloadName
		{
			get
			{
				return this.WorkloadNameField;
			}
			set
			{
				this.WorkloadNameField = value;
			}
		}

		// Token: 0x04000053 RID: 83
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000054 RID: 84
		private string GroupNameField;

		// Token: 0x04000055 RID: 85
		private string WorkloadNameField;
	}
}
