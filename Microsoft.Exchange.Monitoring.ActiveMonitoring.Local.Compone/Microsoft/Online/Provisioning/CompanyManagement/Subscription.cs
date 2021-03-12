using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Provisioning.CompanyManagement
{
	// Token: 0x0200029E RID: 670
	[DebuggerStepThrough]
	[DataContract(Name = "Subscription", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class Subscription : IExtensibleDataObject
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x000858C0 File Offset: 0x00083AC0
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x000858C8 File Offset: 0x00083AC8
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

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000858D1 File Offset: 0x00083AD1
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x000858D9 File Offset: 0x00083AD9
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public Guid AccountId
		{
			get
			{
				return this.AccountIdField;
			}
			set
			{
				this.AccountIdField = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x000858E2 File Offset: 0x00083AE2
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x000858EA File Offset: 0x00083AEA
		[DataMember(IsRequired = true)]
		public int AllowedOverageUnits
		{
			get
			{
				return this.AllowedOverageUnitsField;
			}
			set
			{
				this.AllowedOverageUnitsField = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000858F3 File Offset: 0x00083AF3
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x000858FB File Offset: 0x00083AFB
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public Guid ContextId
		{
			get
			{
				return this.ContextIdField;
			}
			set
			{
				this.ContextIdField = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00085904 File Offset: 0x00083B04
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x0008590C File Offset: 0x00083B0C
		[DataMember(IsRequired = true)]
		public DateTime LifecycleNextStateChangeDate
		{
			get
			{
				return this.LifecycleNextStateChangeDateField;
			}
			set
			{
				this.LifecycleNextStateChangeDateField = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00085915 File Offset: 0x00083B15
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x0008591D File Offset: 0x00083B1D
		[DataMember(IsRequired = true)]
		public SubscriptionState LifecycleState
		{
			get
			{
				return this.LifecycleStateField;
			}
			set
			{
				this.LifecycleStateField = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00085926 File Offset: 0x00083B26
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x0008592E File Offset: 0x00083B2E
		[DataMember(EmitDefaultValue = false)]
		public string OfferType
		{
			get
			{
				return this.OfferTypeField;
			}
			set
			{
				this.OfferTypeField = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00085937 File Offset: 0x00083B37
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x0008593F File Offset: 0x00083B3F
		[DataMember]
		public string PartNumber
		{
			get
			{
				return this.PartNumberField;
			}
			set
			{
				this.PartNumberField = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00085948 File Offset: 0x00083B48
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x00085950 File Offset: 0x00083B50
		[DataMember(IsRequired = true)]
		public int PrepaidUnits
		{
			get
			{
				return this.PrepaidUnitsField;
			}
			set
			{
				this.PrepaidUnitsField = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00085959 File Offset: 0x00083B59
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x00085961 File Offset: 0x00083B61
		[DataMember]
		public Guid SkuId
		{
			get
			{
				return this.SkuIdField;
			}
			set
			{
				this.SkuIdField = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0008596A File Offset: 0x00083B6A
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x00085972 File Offset: 0x00083B72
		[DataMember(IsRequired = true)]
		public DateTime StartDate
		{
			get
			{
				return this.StartDateField;
			}
			set
			{
				this.StartDateField = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0008597B File Offset: 0x00083B7B
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x00085983 File Offset: 0x00083B83
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public Guid SubscriptionId
		{
			get
			{
				return this.SubscriptionIdField;
			}
			set
			{
				this.SubscriptionIdField = value;
			}
		}

		// Token: 0x04000E71 RID: 3697
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000E72 RID: 3698
		private Guid AccountIdField;

		// Token: 0x04000E73 RID: 3699
		private int AllowedOverageUnitsField;

		// Token: 0x04000E74 RID: 3700
		private Guid ContextIdField;

		// Token: 0x04000E75 RID: 3701
		private DateTime LifecycleNextStateChangeDateField;

		// Token: 0x04000E76 RID: 3702
		private SubscriptionState LifecycleStateField;

		// Token: 0x04000E77 RID: 3703
		private string OfferTypeField;

		// Token: 0x04000E78 RID: 3704
		private string PartNumberField;

		// Token: 0x04000E79 RID: 3705
		private int PrepaidUnitsField;

		// Token: 0x04000E7A RID: 3706
		private Guid SkuIdField;

		// Token: 0x04000E7B RID: 3707
		private DateTime StartDateField;

		// Token: 0x04000E7C RID: 3708
		private Guid SubscriptionIdField;
	}
}
