using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000848 RID: 2120
	[KnownType(typeof(DomainConfigurationSettings))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "ConfigurationSettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[KnownType(typeof(CompanyConfigurationSettings))]
	[Serializable]
	internal class ConfigurationSettings : IExtensibleDataObject
	{
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x00065C5E File Offset: 0x00063E5E
		// (set) Token: 0x06002D49 RID: 11593 RVA: 0x00065C66 File Offset: 0x00063E66
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

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x00065C6F File Offset: 0x00063E6F
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x00065C77 File Offset: 0x00063E77
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

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x00065C80 File Offset: 0x00063E80
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x00065C88 File Offset: 0x00063E88
		[DataMember]
		internal EdgeBlockMode? DirectoryEdgeBlockMode
		{
			get
			{
				return this.DirectoryEdgeBlockModeField;
			}
			set
			{
				this.DirectoryEdgeBlockModeField = value;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x00065C91 File Offset: 0x00063E91
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x00065C99 File Offset: 0x00063E99
		[DataMember]
		internal IPListConfig InternalServerIPList
		{
			get
			{
				return this.InternalServerIPListField;
			}
			set
			{
				this.InternalServerIPListField = value;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x00065CA2 File Offset: 0x00063EA2
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x00065CAA File Offset: 0x00063EAA
		[DataMember]
		internal IPListConfig OnPremiseGatewayIPList
		{
			get
			{
				return this.OnPremiseGatewayIPListField;
			}
			set
			{
				this.OnPremiseGatewayIPListField = value;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x00065CB3 File Offset: 0x00063EB3
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x00065CBB File Offset: 0x00063EBB
		[DataMember]
		internal IPListConfig OutboundIPList
		{
			get
			{
				return this.OutboundIPListField;
			}
			set
			{
				this.OutboundIPListField = value;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x00065CC4 File Offset: 0x00063EC4
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x00065CCC File Offset: 0x00063ECC
		[DataMember]
		internal bool? RecipientLevelRouting
		{
			get
			{
				return this.RecipientLevelRoutingField;
			}
			set
			{
				this.RecipientLevelRoutingField = value;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x00065CD5 File Offset: 0x00063ED5
		// (set) Token: 0x06002D57 RID: 11607 RVA: 0x00065CDD File Offset: 0x00063EDD
		[DataMember]
		internal bool? SkipList
		{
			get
			{
				return this.SkipListField;
			}
			set
			{
				this.SkipListField = value;
			}
		}

		// Token: 0x04002754 RID: 10068
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002755 RID: 10069
		[OptionalField]
		private int CompanyIdField;

		// Token: 0x04002756 RID: 10070
		[OptionalField]
		private EdgeBlockMode? DirectoryEdgeBlockModeField;

		// Token: 0x04002757 RID: 10071
		[OptionalField]
		private IPListConfig InternalServerIPListField;

		// Token: 0x04002758 RID: 10072
		[OptionalField]
		private IPListConfig OnPremiseGatewayIPListField;

		// Token: 0x04002759 RID: 10073
		[OptionalField]
		private IPListConfig OutboundIPListField;

		// Token: 0x0400275A RID: 10074
		[OptionalField]
		private bool? RecipientLevelRoutingField;

		// Token: 0x0400275B RID: 10075
		[OptionalField]
		private bool? SkipListField;
	}
}
