using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C3D RID: 3133
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "FindDomainRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindDomainRequest : IExtensibleDataObject
	{
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x000B6E88 File Offset: 0x000B5088
		// (set) Token: 0x060044A5 RID: 17573 RVA: 0x000B6E90 File Offset: 0x000B5090
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

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x000B6E99 File Offset: 0x000B5099
		// (set) Token: 0x060044A7 RID: 17575 RVA: 0x000B6EA1 File Offset: 0x000B50A1
		[DataMember(IsRequired = true)]
		public DomainQuery Domain
		{
			get
			{
				return this.DomainField;
			}
			set
			{
				this.DomainField = value;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x000B6EAA File Offset: 0x000B50AA
		// (set) Token: 0x060044A9 RID: 17577 RVA: 0x000B6EB2 File Offset: 0x000B50B2
		[DataMember]
		public int ReadFlag
		{
			get
			{
				return this.ReadFlagField;
			}
			set
			{
				this.ReadFlagField = value;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000B6EBB File Offset: 0x000B50BB
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x000B6EC3 File Offset: 0x000B50C3
		[DataMember]
		public TenantQuery Tenant
		{
			get
			{
				return this.TenantField;
			}
			set
			{
				this.TenantField = value;
			}
		}

		// Token: 0x04003A0E RID: 14862
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A0F RID: 14863
		private DomainQuery DomainField;

		// Token: 0x04003A10 RID: 14864
		private int ReadFlagField;

		// Token: 0x04003A11 RID: 14865
		private TenantQuery TenantField;
	}
}
