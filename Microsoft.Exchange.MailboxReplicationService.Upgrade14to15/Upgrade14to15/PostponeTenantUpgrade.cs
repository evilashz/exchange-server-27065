using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200005E RID: 94
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "PostponeTenantUpgrade", Namespace = "http://tempuri.org/")]
	public class PostponeTenantUpgrade : IExtensibleDataObject
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00003937 File Offset: 0x00001B37
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000393F File Offset: 0x00001B3F
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00003948 File Offset: 0x00001B48
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00003950 File Offset: 0x00001B50
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00003959 File Offset: 0x00001B59
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00003961 File Offset: 0x00001B61
		[DataMember]
		public string userUpn
		{
			get
			{
				return this.userUpnField;
			}
			set
			{
				this.userUpnField = value;
			}
		}

		// Token: 0x04000106 RID: 262
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000107 RID: 263
		private Guid tenantIdField;

		// Token: 0x04000108 RID: 264
		private string userUpnField;
	}
}
