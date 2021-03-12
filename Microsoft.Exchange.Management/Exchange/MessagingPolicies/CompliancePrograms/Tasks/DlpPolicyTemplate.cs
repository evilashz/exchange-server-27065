using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200095C RID: 2396
	[Serializable]
	public class DlpPolicyTemplate : DlpPolicyPresentationBase
	{
		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x060055A7 RID: 21927 RVA: 0x00160DEF File Offset: 0x0015EFEF
		// (set) Token: 0x060055A8 RID: 21928 RVA: 0x00160DF7 File Offset: 0x0015EFF7
		private CultureInfo CurrentCulture { get; set; }

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x00160E00 File Offset: 0x0015F000
		// (set) Token: 0x060055AA RID: 21930 RVA: 0x00160E0D File Offset: 0x0015F00D
		public string Name
		{
			get
			{
				return this.dlpTemplateMetaData.Name;
			}
			set
			{
				this.dlpTemplateMetaData.Name = value;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x00160E1B File Offset: 0x0015F01B
		public string Version
		{
			get
			{
				return this.dlpTemplateMetaData.Version;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x060055AC RID: 21932 RVA: 0x00160E28 File Offset: 0x0015F028
		public string ContentVersion
		{
			get
			{
				return this.dlpTemplateMetaData.ContentVersion;
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x060055AD RID: 21933 RVA: 0x00160E35 File Offset: 0x0015F035
		// (set) Token: 0x060055AE RID: 21934 RVA: 0x00160E42 File Offset: 0x0015F042
		public Guid ImmutableId
		{
			get
			{
				return this.dlpTemplateMetaData.ImmutableId;
			}
			set
			{
				this.dlpTemplateMetaData.ImmutableId = value;
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x060055AF RID: 21935 RVA: 0x00160E50 File Offset: 0x0015F050
		[LocDisplayName(Strings.IDs.DlpPolicyStateDisplayName)]
		[LocDescription(Strings.IDs.DlpPolicyStateDescription)]
		public RuleState State
		{
			get
			{
				return this.dlpTemplateMetaData.State;
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x060055B0 RID: 21936 RVA: 0x00160E5D File Offset: 0x0015F05D
		[LocDescription(Strings.IDs.DlpPolicyModeDescription)]
		[LocDisplayName(Strings.IDs.DlpPolicyModeDisplayName)]
		public RuleMode Mode
		{
			get
			{
				return this.dlpTemplateMetaData.Mode;
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x060055B1 RID: 21937 RVA: 0x00160E6A File Offset: 0x0015F06A
		[LocDisplayName(Strings.IDs.DlpPolicyDescriptionDisplayName)]
		[LocDescription(Strings.IDs.DlpPolicyDescriptionDescription)]
		public string Description
		{
			get
			{
				return DlpPolicyTemplateMetaData.GetLocalizedStringValue(this.dlpTemplateMetaData.LocalizedDescriptions, this.CurrentCulture);
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x060055B2 RID: 21938 RVA: 0x00160E82 File Offset: 0x0015F082
		public string PublisherName
		{
			get
			{
				return this.dlpTemplateMetaData.PublisherName;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x060055B3 RID: 21939 RVA: 0x00160E8F File Offset: 0x0015F08F
		public string LocalizedName
		{
			get
			{
				return DlpPolicyTemplateMetaData.GetLocalizedStringValue(this.dlpTemplateMetaData.LocalizedNames, this.CurrentCulture);
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x060055B4 RID: 21940 RVA: 0x00160EB5 File Offset: 0x0015F0B5
		public string[] Keywords
		{
			get
			{
				return (from keyword in this.dlpTemplateMetaData.LocalizedKeywords
				select DlpPolicyTemplateMetaData.GetLocalizedStringValue(keyword, this.CurrentCulture)).ToArray<string>();
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x060055B5 RID: 21941 RVA: 0x00160EE6 File Offset: 0x0015F0E6
		public MultiValuedProperty<string> RuleParameters
		{
			get
			{
				return (from parameter in this.dlpTemplateMetaData.RuleParameters
				select parameter.ToString(this.CurrentCulture)).ToArray<string>();
			}
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x00160F0E File Offset: 0x0015F10E
		public DlpPolicyTemplate() : base(new ADComplianceProgram())
		{
			this.CurrentCulture = DlpPolicyTemplateMetaData.DefaultCulture;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x00160F28 File Offset: 0x0015F128
		public DlpPolicyTemplate(ADComplianceProgram dlpPolicy, CultureInfo culture) : base(dlpPolicy)
		{
			if (base.AdDlpPolicy != null)
			{
				base.AdDlpPolicy = base.AdDlpPolicy;
				this.dlpTemplateMetaData = DlpPolicyParser.ParseDlpPolicyTemplate(base.AdDlpPolicy.TransportRulesXml);
			}
			else
			{
				base.AdDlpPolicy = new ADComplianceProgram();
				this.dlpTemplateMetaData = new DlpPolicyTemplateMetaData();
			}
			this.CurrentCulture = culture;
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x00160F85 File Offset: 0x0015F185
		private ObjectSchema ObjectSchema
		{
			get
			{
				return DlpPolicyTemplate.schema;
			}
		}

		// Token: 0x040031BB RID: 12731
		private readonly DlpPolicyTemplateMetaData dlpTemplateMetaData;

		// Token: 0x040031BC RID: 12732
		private static readonly DlpPolicyTemplateSchema schema = ObjectSchema.GetInstance<DlpPolicyTemplateSchema>();
	}
}
