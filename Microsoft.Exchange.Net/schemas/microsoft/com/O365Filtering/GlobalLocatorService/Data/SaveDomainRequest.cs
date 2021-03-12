using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C40 RID: 3136
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "SaveDomainRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class SaveDomainRequest : IExtensibleDataObject
	{
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000B6F4A File Offset: 0x000B514A
		// (set) Token: 0x060044BC RID: 17596 RVA: 0x000B6F52 File Offset: 0x000B5152
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

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x000B6F5B File Offset: 0x000B515B
		// (set) Token: 0x060044BE RID: 17598 RVA: 0x000B6F63 File Offset: 0x000B5163
		[DataMember]
		public string CustomTag
		{
			get
			{
				return this.CustomTagField;
			}
			set
			{
				this.CustomTagField = value;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x000B6F6C File Offset: 0x000B516C
		// (set) Token: 0x060044C0 RID: 17600 RVA: 0x000B6F74 File Offset: 0x000B5174
		[DataMember(IsRequired = true)]
		public DomainInfo DomainInfo
		{
			get
			{
				return this.DomainInfoField;
			}
			set
			{
				this.DomainInfoField = value;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x000B6F7D File Offset: 0x000B517D
		// (set) Token: 0x060044C2 RID: 17602 RVA: 0x000B6F85 File Offset: 0x000B5185
		[DataMember]
		public DomainKeyType DomainKeyType
		{
			get
			{
				return this.DomainKeyTypeField;
			}
			set
			{
				this.DomainKeyTypeField = value;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x000B6F8E File Offset: 0x000B518E
		// (set) Token: 0x060044C4 RID: 17604 RVA: 0x000B6F96 File Offset: 0x000B5196
		[DataMember(IsRequired = true)]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x04003A18 RID: 14872
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A19 RID: 14873
		private string CustomTagField;

		// Token: 0x04003A1A RID: 14874
		private DomainInfo DomainInfoField;

		// Token: 0x04003A1B RID: 14875
		private DomainKeyType DomainKeyTypeField;

		// Token: 0x04003A1C RID: 14876
		private Guid TenantIdField;
	}
}
