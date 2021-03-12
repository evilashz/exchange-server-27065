using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000951 RID: 2385
	[Serializable]
	public class DlpPolicy : DlpPolicyPresentationBase
	{
		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x0015EB28 File Offset: 0x0015CD28
		// (set) Token: 0x0600551A RID: 21786 RVA: 0x0015EB35 File Offset: 0x0015CD35
		public string Name
		{
			get
			{
				return this.dlpPolicyMetaData.Name;
			}
			set
			{
				this.dlpPolicyMetaData.Name = value;
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x0015EB43 File Offset: 0x0015CD43
		public string Version
		{
			get
			{
				return this.dlpPolicyMetaData.Version;
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x0015EB50 File Offset: 0x0015CD50
		public string ContentVersion
		{
			get
			{
				return this.dlpPolicyMetaData.ContentVersion;
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x0015EB5D File Offset: 0x0015CD5D
		// (set) Token: 0x0600551E RID: 21790 RVA: 0x0015EB6A File Offset: 0x0015CD6A
		public Guid ImmutableId
		{
			get
			{
				return this.dlpPolicyMetaData.ImmutableId;
			}
			set
			{
				this.dlpPolicyMetaData.ImmutableId = value;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x0015EB78 File Offset: 0x0015CD78
		[LocDescription(Strings.IDs.DlpPolicyStateDescription)]
		[LocDisplayName(Strings.IDs.DlpPolicyStateDisplayName)]
		public RuleState State
		{
			get
			{
				return this.dlpPolicyMetaData.State;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x0015EB85 File Offset: 0x0015CD85
		[LocDisplayName(Strings.IDs.DlpPolicyModeDisplayName)]
		[LocDescription(Strings.IDs.DlpPolicyModeDescription)]
		public RuleMode Mode
		{
			get
			{
				return this.dlpPolicyMetaData.Mode;
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06005521 RID: 21793 RVA: 0x0015EB92 File Offset: 0x0015CD92
		[LocDisplayName(Strings.IDs.DlpPolicyDescriptionDisplayName)]
		[LocDescription(Strings.IDs.DlpPolicyDescriptionDescription)]
		public string Description
		{
			get
			{
				return this.dlpPolicyMetaData.Description;
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x0015EB9F File Offset: 0x0015CD9F
		public string PublisherName
		{
			get
			{
				return this.dlpPolicyMetaData.PublisherName;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06005523 RID: 21795 RVA: 0x0015EBAC File Offset: 0x0015CDAC
		public string[] Keywords
		{
			get
			{
				return this.dlpPolicyMetaData.Keywords.ToArray();
			}
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0015EBBE File Offset: 0x0015CDBE
		public DlpPolicy() : this(null)
		{
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0015EBC8 File Offset: 0x0015CDC8
		internal DlpPolicy(ADComplianceProgram adDlpPolicy) : base(adDlpPolicy)
		{
			if (base.AdDlpPolicy != null)
			{
				base.AdDlpPolicy = base.AdDlpPolicy;
				this.dlpPolicyMetaData = DlpPolicyParser.ParseDlpPolicyInstance(base.AdDlpPolicy.TransportRulesXml);
				return;
			}
			base.AdDlpPolicy = new ADComplianceProgram();
			this.dlpPolicyMetaData = new DlpPolicyMetaData();
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06005526 RID: 21798 RVA: 0x0015EC1D File Offset: 0x0015CE1D
		internal ObjectSchema Schema
		{
			get
			{
				return DlpPolicy.schema;
			}
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0015EC24 File Offset: 0x0015CE24
		internal void SetAdDlpPolicyWithNoDlpXml(ADComplianceProgram adDlpPolicy)
		{
			base.AdDlpPolicy = adDlpPolicy;
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x0015EC30 File Offset: 0x0015CE30
		internal override void SuppressPiiData(PiiMap piiMap)
		{
			base.SuppressPiiData(piiMap);
			this.dlpPolicyMetaData.Name = (SuppressingPiiProperty.TryRedact(DlpPolicySchemaBase.Name, this.dlpPolicyMetaData.Name, piiMap) as string);
			this.dlpPolicyMetaData.Description = SuppressingPiiProperty.TryRedactValue<string>(DlpPolicySchemaBase.Description, this.dlpPolicyMetaData.Description);
			this.dlpPolicyMetaData.PublisherName = SuppressingPiiProperty.TryRedactValue<string>(DlpPolicySchemaBase.PublisherName, this.dlpPolicyMetaData.PublisherName);
			this.dlpPolicyMetaData.Keywords = SuppressingPiiProperty.TryRedactValue<List<string>>(DlpPolicySchemaBase.Keywords, this.dlpPolicyMetaData.Keywords);
		}

		// Token: 0x0400313D RID: 12605
		private readonly DlpPolicyMetaData dlpPolicyMetaData;

		// Token: 0x0400313E RID: 12606
		private static readonly DlpPolicySchemaBase schema = ObjectSchema.GetInstance<DlpPolicySchemaBase>();
	}
}
