using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200003C RID: 60
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetTenantApplicationData", Namespace = "http://tempuri.org/")]
	public class GetTenantApplicationData : IExtensibleDataObject
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000339A File Offset: 0x0000159A
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000033A2 File Offset: 0x000015A2
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000033AB File Offset: 0x000015AB
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000033B3 File Offset: 0x000015B3
		[DataMember]
		public Guid tenantGuid
		{
			get
			{
				return this.tenantGuidField;
			}
			set
			{
				this.tenantGuidField = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000033BC File Offset: 0x000015BC
		// (set) Token: 0x06000197 RID: 407 RVA: 0x000033C4 File Offset: 0x000015C4
		[DataMember(Order = 1)]
		public string applicationDataKey
		{
			get
			{
				return this.applicationDataKeyField;
			}
			set
			{
				this.applicationDataKeyField = value;
			}
		}

		// Token: 0x040000B9 RID: 185
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000BA RID: 186
		private Guid tenantGuidField;

		// Token: 0x040000BB RID: 187
		private string applicationDataKeyField;
	}
}
