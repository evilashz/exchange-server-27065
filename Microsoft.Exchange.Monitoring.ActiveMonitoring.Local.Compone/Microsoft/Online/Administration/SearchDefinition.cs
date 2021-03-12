using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003B8 RID: 952
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(RoleMemberSearchDefinition))]
	[KnownType(typeof(UserSearchDefinition))]
	[KnownType(typeof(ServicePrincipalSearchDefinition))]
	[DebuggerStepThrough]
	[DataContract(Name = "SearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[KnownType(typeof(PartnerContractSearchDefinition))]
	[KnownType(typeof(ContactSearchDefinition))]
	[KnownType(typeof(GroupSearchDefinition))]
	[KnownType(typeof(GroupMemberSearchDefinition))]
	public class SearchDefinition : IExtensibleDataObject
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0008C40C File Offset: 0x0008A60C
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0008C414 File Offset: 0x0008A614
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

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0008C41D File Offset: 0x0008A61D
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0008C425 File Offset: 0x0008A625
		[DataMember]
		public int PageSize
		{
			get
			{
				return this.PageSizeField;
			}
			set
			{
				this.PageSizeField = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0008C42E File Offset: 0x0008A62E
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x0008C436 File Offset: 0x0008A636
		[DataMember]
		public string SearchString
		{
			get
			{
				return this.SearchStringField;
			}
			set
			{
				this.SearchStringField = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0008C43F File Offset: 0x0008A63F
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x0008C447 File Offset: 0x0008A647
		[DataMember]
		public SortDirection SortDirection
		{
			get
			{
				return this.SortDirectionField;
			}
			set
			{
				this.SortDirectionField = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0008C450 File Offset: 0x0008A650
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x0008C458 File Offset: 0x0008A658
		[DataMember]
		public SortField SortField
		{
			get
			{
				return this.SortFieldField;
			}
			set
			{
				this.SortFieldField = value;
			}
		}

		// Token: 0x0400103E RID: 4158
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400103F RID: 4159
		private int PageSizeField;

		// Token: 0x04001040 RID: 4160
		private string SearchStringField;

		// Token: 0x04001041 RID: 4161
		private SortDirection SortDirectionField;

		// Token: 0x04001042 RID: 4162
		private SortField SortFieldField;
	}
}
