using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000849 RID: 2121
	[DebuggerStepThrough]
	[DataContract(Name = "CompanyConfigurationSettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class CompanyConfigurationSettings : ConfigurationSettings
	{
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00065CEE File Offset: 0x00063EEE
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x00065CF6 File Offset: 0x00063EF6
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

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x00065CFF File Offset: 0x00063EFF
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x00065D07 File Offset: 0x00063F07
		[DataMember]
		internal InboundIPListConfig InboundIPList
		{
			get
			{
				return this.InboundIPListField;
			}
			set
			{
				this.InboundIPListField = value;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x00065D10 File Offset: 0x00063F10
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x00065D18 File Offset: 0x00063F18
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

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x00065D21 File Offset: 0x00063F21
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x00065D29 File Offset: 0x00063F29
		[DataMember]
		internal Guid? MicrosoftOnlineId
		{
			get
			{
				return this.MicrosoftOnlineIdField;
			}
			set
			{
				this.MicrosoftOnlineIdField = value;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x00065D32 File Offset: 0x00063F32
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x00065D3A File Offset: 0x00063F3A
		[DataMember]
		internal PropagationSettings PropagationSetting
		{
			get
			{
				return this.PropagationSettingField;
			}
			set
			{
				this.PropagationSettingField = value;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x00065D43 File Offset: 0x00063F43
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x00065D4B File Offset: 0x00063F4B
		[DataMember]
		internal int SeatCount
		{
			get
			{
				return this.SeatCountField;
			}
			set
			{
				this.SeatCountField = value;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x00065D54 File Offset: 0x00063F54
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x00065D5C File Offset: 0x00063F5C
		[DataMember]
		internal ServicePlan ServicePlanId
		{
			get
			{
				return this.ServicePlanIdField;
			}
			set
			{
				this.ServicePlanIdField = value;
			}
		}

		// Token: 0x0400275C RID: 10076
		[OptionalField]
		private Guid? CompanyGuidField;

		// Token: 0x0400275D RID: 10077
		[OptionalField]
		private InboundIPListConfig InboundIPListField;

		// Token: 0x0400275E RID: 10078
		[OptionalField]
		private InheritanceSettings InheritFromParentField;

		// Token: 0x0400275F RID: 10079
		[OptionalField]
		private Guid? MicrosoftOnlineIdField;

		// Token: 0x04002760 RID: 10080
		[OptionalField]
		private PropagationSettings PropagationSettingField;

		// Token: 0x04002761 RID: 10081
		[OptionalField]
		private int SeatCountField;

		// Token: 0x04002762 RID: 10082
		[OptionalField]
		private ServicePlan ServicePlanIdField;
	}
}
