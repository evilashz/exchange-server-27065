using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000050 RID: 80
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeleteConstraint", Namespace = "http://tempuri.org/")]
	public class DeleteConstraint : IExtensibleDataObject
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00003726 File Offset: 0x00001926
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000372E File Offset: 0x0000192E
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00003737 File Offset: 0x00001937
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000373F File Offset: 0x0000193F
		[DataMember]
		public string workloadName
		{
			get
			{
				return this.workloadNameField;
			}
			set
			{
				this.workloadNameField = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00003748 File Offset: 0x00001948
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00003750 File Offset: 0x00001950
		[DataMember(Order = 1)]
		public string[] constraintName
		{
			get
			{
				return this.constraintNameField;
			}
			set
			{
				this.constraintNameField = value;
			}
		}

		// Token: 0x040000E5 RID: 229
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000E6 RID: 230
		private string workloadNameField;

		// Token: 0x040000E7 RID: 231
		private string[] constraintNameField;
	}
}
