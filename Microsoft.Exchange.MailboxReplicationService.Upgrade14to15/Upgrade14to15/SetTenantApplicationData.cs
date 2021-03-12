using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200003E RID: 62
	[DebuggerStepThrough]
	[DataContract(Name = "SetTenantApplicationData", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SetTenantApplicationData : IExtensibleDataObject
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000033FF File Offset: 0x000015FF
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00003407 File Offset: 0x00001607
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003410 File Offset: 0x00001610
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00003418 File Offset: 0x00001618
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003421 File Offset: 0x00001621
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00003429 File Offset: 0x00001629
		[DataMember(Order = 1)]
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

		// Token: 0x040000BE RID: 190
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000BF RID: 191
		private Guid tenantGuidField;

		// Token: 0x040000C0 RID: 192
		private XmlElement applicationDataField;
	}
}
