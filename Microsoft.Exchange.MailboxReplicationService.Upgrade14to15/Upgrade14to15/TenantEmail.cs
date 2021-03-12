using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000015 RID: 21
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TenantEmail", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.ManagementService")]
	[DebuggerStepThrough]
	public class TenantEmail : IExtensibleDataObject
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002B60 File Offset: 0x00000D60
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002B69 File Offset: 0x00000D69
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002B71 File Offset: 0x00000D71
		[DataMember]
		public string Email
		{
			get
			{
				return this.EmailField;
			}
			set
			{
				this.EmailField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002B7A File Offset: 0x00000D7A
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002B82 File Offset: 0x00000D82
		[DataMember]
		public EmailType EmailType
		{
			get
			{
				return this.EmailTypeField;
			}
			set
			{
				this.EmailTypeField = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002B8B File Offset: 0x00000D8B
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002B93 File Offset: 0x00000D93
		[DataMember]
		public Guid TenantEmailId
		{
			get
			{
				return this.TenantEmailIdField;
			}
			set
			{
				this.TenantEmailIdField = value;
			}
		}

		// Token: 0x0400004F RID: 79
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000050 RID: 80
		private string EmailField;

		// Token: 0x04000051 RID: 81
		private EmailType EmailTypeField;

		// Token: 0x04000052 RID: 82
		private Guid TenantEmailIdField;
	}
}
