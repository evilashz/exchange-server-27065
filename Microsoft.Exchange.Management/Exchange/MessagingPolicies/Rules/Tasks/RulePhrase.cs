using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B6C RID: 2924
	[Serializable]
	public abstract class RulePhrase : ConfigurableObject
	{
		// Token: 0x06006CAB RID: 27819 RVA: 0x001BD0D9 File Offset: 0x001BB2D9
		public RulePhrase() : base(new SimpleProviderPropertyBag())
		{
			this.Reset();
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x06006CAC RID: 27820 RVA: 0x001BD0EC File Offset: 0x001BB2EC
		// (set) Token: 0x06006CAD RID: 27821 RVA: 0x001BD103 File Offset: 0x001BB303
		public string Name
		{
			get
			{
				return (string)this.propertyBag[RulePhraseSchema.Name];
			}
			private set
			{
				this.propertyBag[RulePhraseSchema.Name] = value;
			}
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x06006CAE RID: 27822 RVA: 0x001BD116 File Offset: 0x001BB316
		// (set) Token: 0x06006CAF RID: 27823 RVA: 0x001BD12D File Offset: 0x001BB32D
		public int Rank
		{
			get
			{
				return (int)this.propertyBag[RulePhraseSchema.Rank];
			}
			private set
			{
				this.propertyBag[RulePhraseSchema.Rank] = value;
			}
		}

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x001BD145 File Offset: 0x001BB345
		// (set) Token: 0x06006CB1 RID: 27825 RVA: 0x001BD15C File Offset: 0x001BB35C
		public string LinkedDisplayText
		{
			get
			{
				return (string)this.propertyBag[RulePhraseSchema.LinkedDisplayText];
			}
			private set
			{
				this.propertyBag[RulePhraseSchema.LinkedDisplayText] = value;
			}
		}

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x001BD16F File Offset: 0x001BB36F
		// (set) Token: 0x06006CB3 RID: 27827 RVA: 0x001BD177 File Offset: 0x001BB377
		internal int MaxDescriptionListLength { get; set; }

		// Token: 0x06006CB4 RID: 27828 RVA: 0x001BD180 File Offset: 0x001BB380
		internal virtual void Reset()
		{
			this.MaxDescriptionListLength = 200;
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x06006CB5 RID: 27829
		internal abstract string Description { get; }

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x001BD18D File Offset: 0x001BB38D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RulePhrase.schema;
			}
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x001BD194 File Offset: 0x001BB394
		internal void SetReadOnlyProperties(string name, int rank, string linkedDisplayText)
		{
			this.Name = name;
			this.Rank = rank;
			this.LinkedDisplayText = linkedDisplayText;
			base.ResetChangeTracking();
		}

		// Token: 0x04003869 RID: 14441
		private static ObjectSchema schema = ObjectSchema.GetInstance<RulePhraseSchema>();

		// Token: 0x02000B6D RID: 2925
		internal class RulePhraseValidationError : ValidationError
		{
			// Token: 0x06006CB9 RID: 27833 RVA: 0x001BD1BD File Offset: 0x001BB3BD
			public RulePhraseValidationError(LocalizedString description, string name) : base(description)
			{
				this.name = name;
			}

			// Token: 0x170021F9 RID: 8697
			// (get) Token: 0x06006CBA RID: 27834 RVA: 0x001BD1CD File Offset: 0x001BB3CD
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x0400386B RID: 14443
			private readonly string name;
		}
	}
}
