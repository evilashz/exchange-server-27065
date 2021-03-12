using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x0200082B RID: 2091
	[Serializable]
	public abstract class RulePresentationObjectBase : IConfigurable, IVersionable
	{
		// Token: 0x06004858 RID: 18520 RVA: 0x0012950A File Offset: 0x0012770A
		public RulePresentationObjectBase()
		{
			this.transportRule = new TransportRule();
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00129524 File Offset: 0x00127724
		public RulePresentationObjectBase(TransportRule transportRule)
		{
			if (transportRule != null)
			{
				this.transportRule = transportRule;
				return;
			}
			this.transportRule = new TransportRule();
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x00129549 File Offset: 0x00127749
		public ObjectId Identity
		{
			get
			{
				return this.transportRule.Identity;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x00129556 File Offset: 0x00127756
		public string DistinguishedName
		{
			get
			{
				return this.transportRule.DistinguishedName;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x00129563 File Offset: 0x00127763
		public Guid Guid
		{
			get
			{
				return this.transportRule.Guid;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x0600485D RID: 18525 RVA: 0x00129570 File Offset: 0x00127770
		public Guid ImmutableId
		{
			get
			{
				return this.transportRule.ImmutableId;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x0012957D File Offset: 0x0012777D
		public OrganizationId OrganizationId
		{
			get
			{
				return this.transportRule.OrganizationId;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x0012958A File Offset: 0x0012778A
		// (set) Token: 0x06004860 RID: 18528 RVA: 0x00129597 File Offset: 0x00127797
		public string Name
		{
			get
			{
				return this.transportRule.Name;
			}
			set
			{
				this.transportRule.Name = value;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x001295A5 File Offset: 0x001277A5
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x001295AD File Offset: 0x001277AD
		public DateTime? WhenChanged
		{
			get
			{
				return this.transportRule.WhenChanged;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x001295BA File Offset: 0x001277BA
		ObjectSchema IVersionable.ObjectSchema
		{
			get
			{
				return ((IVersionable)this.transportRule).ObjectSchema;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x001295C7 File Offset: 0x001277C7
		bool IVersionable.ExchangeVersionUpgradeSupported
		{
			get
			{
				return this.transportRule.ExchangeVersionUpgradeSupported;
			}
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x001295D4 File Offset: 0x001277D4
		bool IVersionable.IsPropertyAccessible(PropertyDefinition propertyDefinition)
		{
			return ((IVersionable)this.transportRule).IsPropertyAccessible(propertyDefinition);
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x001295E2 File Offset: 0x001277E2
		public ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return this.transportRule.ExchangeVersion;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x001295EF File Offset: 0x001277EF
		internal string TransportRuleXml
		{
			get
			{
				return this.transportRule.Xml;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x001295FC File Offset: 0x001277FC
		internal ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.transportRule.MaximumSupportedExchangeObjectVersion;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x00129609 File Offset: 0x00127809
		ExchangeObjectVersion IVersionable.MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.MaximumSupportedExchangeObjectVersion;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x00129611 File Offset: 0x00127811
		internal bool IsReadOnly
		{
			get
			{
				return this.transportRule.IsReadOnly;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x0012961E File Offset: 0x0012781E
		bool IVersionable.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x0600486C RID: 18540 RVA: 0x00129626 File Offset: 0x00127826
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x00129629 File Offset: 0x00127829
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x00129659 File Offset: 0x00127859
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x0012965B File Offset: 0x0012785B
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x06004870 RID: 18544
		public abstract ValidationError[] Validate();

		// Token: 0x06004871 RID: 18545 RVA: 0x0012965D File Offset: 0x0012785D
		internal void SetTransportRule(TransportRule transportRule)
		{
			this.transportRule = transportRule;
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00129668 File Offset: 0x00127868
		internal virtual void SuppressPiiData(PiiMap piiMap)
		{
			this.transportRule.DistinguishedName = (SuppressingPiiProperty.TryRedact(RulePresentationObjectBaseSchema.DistinguishedName, this.transportRule.DistinguishedName, piiMap) as string);
			this.transportRule.Name = (SuppressingPiiProperty.TryRedact(RulePresentationObjectBaseSchema.Name, this.transportRule.Name, piiMap) as string);
		}

		// Token: 0x04002BFB RID: 11259
		private TransportRule transportRule;

		// Token: 0x04002BFC RID: 11260
		protected bool isValid = true;
	}
}
