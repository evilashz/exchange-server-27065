using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200001C RID: 28
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RedirectTenantWorkloads", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class RedirectTenantWorkloads : IExtensibleDataObject
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002C8F File Offset: 0x00000E8F
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002C97 File Offset: 0x00000E97
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002CA0 File Offset: 0x00000EA0
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002CA8 File Offset: 0x00000EA8
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002CB1 File Offset: 0x00000EB1
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002CB9 File Offset: 0x00000EB9
		[DataMember]
		public string[] workloads
		{
			get
			{
				return this.workloadsField;
			}
			set
			{
				this.workloadsField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002CC2 File Offset: 0x00000EC2
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002CCA File Offset: 0x00000ECA
		[DataMember(Order = 2)]
		public string newTargetWorkload
		{
			get
			{
				return this.newTargetWorkloadField;
			}
			set
			{
				this.newTargetWorkloadField = value;
			}
		}

		// Token: 0x0400005E RID: 94
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400005F RID: 95
		private Guid tenantIdField;

		// Token: 0x04000060 RID: 96
		private string[] workloadsField;

		// Token: 0x04000061 RID: 97
		private string newTargetWorkloadField;
	}
}
