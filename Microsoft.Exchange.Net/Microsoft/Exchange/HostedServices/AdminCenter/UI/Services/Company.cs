using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000847 RID: 2119
	[DebuggerStepThrough]
	[DataContract(Name = "Company", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class Company : IExtensibleDataObject
	{
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x00065B79 File Offset: 0x00063D79
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x00065B81 File Offset: 0x00063D81
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

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x00065B8A File Offset: 0x00063D8A
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x00065B92 File Offset: 0x00063D92
		[DataMember]
		internal bool ActivationComplete
		{
			get
			{
				return this.ActivationCompleteField;
			}
			set
			{
				this.ActivationCompleteField = value;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x00065B9B File Offset: 0x00063D9B
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x00065BA3 File Offset: 0x00063DA3
		[DataMember]
		internal Guid? CompanyGuid
		{
			get
			{
				return this.CompanyGuidField;
			}
			set
			{
				this.CompanyGuidField = value;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x00065BAC File Offset: 0x00063DAC
		// (set) Token: 0x06002D34 RID: 11572 RVA: 0x00065BB4 File Offset: 0x00063DB4
		[DataMember]
		internal int CompanyId
		{
			get
			{
				return this.CompanyIdField;
			}
			set
			{
				this.CompanyIdField = value;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x00065BBD File Offset: 0x00063DBD
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x00065BC5 File Offset: 0x00063DC5
		[DataMember]
		internal int? ConfigurationId
		{
			get
			{
				return this.ConfigurationIdField;
			}
			set
			{
				this.ConfigurationIdField = value;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x00065BCE File Offset: 0x00063DCE
		// (set) Token: 0x06002D38 RID: 11576 RVA: 0x00065BD6 File Offset: 0x00063DD6
		[DataMember]
		internal DateTime DateCreated
		{
			get
			{
				return this.DateCreatedField;
			}
			set
			{
				this.DateCreatedField = value;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x00065BDF File Offset: 0x00063DDF
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x00065BE7 File Offset: 0x00063DE7
		[DataMember]
		internal InheritanceSettings InheritFromParent
		{
			get
			{
				return this.InheritFromParentField;
			}
			set
			{
				this.InheritFromParentField = value;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x00065BF0 File Offset: 0x00063DF0
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x00065BF8 File Offset: 0x00063DF8
		[DataMember]
		internal bool IsEnabled
		{
			get
			{
				return this.IsEnabledField;
			}
			set
			{
				this.IsEnabledField = value;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x00065C01 File Offset: 0x00063E01
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x00065C09 File Offset: 0x00063E09
		[DataMember]
		internal bool IsReseller
		{
			get
			{
				return this.IsResellerField;
			}
			set
			{
				this.IsResellerField = value;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x00065C12 File Offset: 0x00063E12
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x00065C1A File Offset: 0x00063E1A
		[DataMember]
		internal string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x00065C23 File Offset: 0x00063E23
		// (set) Token: 0x06002D42 RID: 11586 RVA: 0x00065C2B File Offset: 0x00063E2B
		[DataMember]
		internal int ParentCompanyId
		{
			get
			{
				return this.ParentCompanyIdField;
			}
			set
			{
				this.ParentCompanyIdField = value;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x00065C34 File Offset: 0x00063E34
		// (set) Token: 0x06002D44 RID: 11588 RVA: 0x00065C3C File Offset: 0x00063E3C
		[DataMember]
		internal CompanyConfigurationSettings Settings
		{
			get
			{
				return this.SettingsField;
			}
			set
			{
				this.SettingsField = value;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x00065C45 File Offset: 0x00063E45
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x00065C4D File Offset: 0x00063E4D
		[DataMember]
		internal TimeZone TimeZone
		{
			get
			{
				return this.TimeZoneField;
			}
			set
			{
				this.TimeZoneField = value;
			}
		}

		// Token: 0x04002747 RID: 10055
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002748 RID: 10056
		[OptionalField]
		private bool ActivationCompleteField;

		// Token: 0x04002749 RID: 10057
		[OptionalField]
		private Guid? CompanyGuidField;

		// Token: 0x0400274A RID: 10058
		[OptionalField]
		private int CompanyIdField;

		// Token: 0x0400274B RID: 10059
		[OptionalField]
		private int? ConfigurationIdField;

		// Token: 0x0400274C RID: 10060
		[OptionalField]
		private DateTime DateCreatedField;

		// Token: 0x0400274D RID: 10061
		[OptionalField]
		private InheritanceSettings InheritFromParentField;

		// Token: 0x0400274E RID: 10062
		[OptionalField]
		private bool IsEnabledField;

		// Token: 0x0400274F RID: 10063
		[OptionalField]
		private bool IsResellerField;

		// Token: 0x04002750 RID: 10064
		[OptionalField]
		private string NameField;

		// Token: 0x04002751 RID: 10065
		[OptionalField]
		private int ParentCompanyIdField;

		// Token: 0x04002752 RID: 10066
		[OptionalField]
		private CompanyConfigurationSettings SettingsField;

		// Token: 0x04002753 RID: 10067
		[OptionalField]
		private TimeZone TimeZoneField;
	}
}
