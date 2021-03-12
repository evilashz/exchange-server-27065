using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000042 RID: 66
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetUserApplicationData", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class SetUserApplicationData : IExtensibleDataObject
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000034B8 File Offset: 0x000016B8
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000034C0 File Offset: 0x000016C0
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000034C9 File Offset: 0x000016C9
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000034D1 File Offset: 0x000016D1
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000034DA File Offset: 0x000016DA
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000034E2 File Offset: 0x000016E2
		[DataMember]
		public Guid userId
		{
			get
			{
				return this.userIdField;
			}
			set
			{
				this.userIdField = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000034EB File Offset: 0x000016EB
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000034F3 File Offset: 0x000016F3
		[DataMember(Order = 2)]
		public XmlElement applicationData
		{
			get
			{
				return this.applicationDataField;
			}
			set
			{
				this.applicationDataField = value;
			}
		}

		// Token: 0x040000C7 RID: 199
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000C8 RID: 200
		private Guid tenantGuidField;

		// Token: 0x040000C9 RID: 201
		private Guid userIdField;

		// Token: 0x040000CA RID: 202
		private XmlElement applicationDataField;
	}
}
