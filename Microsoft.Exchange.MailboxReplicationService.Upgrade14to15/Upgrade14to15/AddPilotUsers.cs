using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200005C RID: 92
	[DataContract(Name = "AddPilotUsers", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddPilotUsers : IExtensibleDataObject
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000038D2 File Offset: 0x00001AD2
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000038DA File Offset: 0x00001ADA
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000038E3 File Offset: 0x00001AE3
		// (set) Token: 0x06000234 RID: 564 RVA: 0x000038EB File Offset: 0x00001AEB
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000038F4 File Offset: 0x00001AF4
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000038FC File Offset: 0x00001AFC
		[DataMember]
		public UserId[] users
		{
			get
			{
				return this.usersField;
			}
			set
			{
				this.usersField = value;
			}
		}

		// Token: 0x04000101 RID: 257
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000102 RID: 258
		private Guid tenantIdField;

		// Token: 0x04000103 RID: 259
		private UserId[] usersField;
	}
}
