using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public class MailboxPlanIdParameter : RecipientIdParameter
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00022C4F File Offset: 0x00020E4F
		public MailboxPlanIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00022C58 File Offset: 0x00020E58
		public MailboxPlanIdParameter()
		{
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00022C60 File Offset: 0x00020E60
		public MailboxPlanIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00022C69 File Offset: 0x00020E69
		public MailboxPlanIdParameter(MailboxPlan mailboxPlan) : base(mailboxPlan.Id)
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00022C77 File Offset: 0x00020E77
		public MailboxPlanIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00022C80 File Offset: 0x00020E80
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailboxPlanIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00022C88 File Offset: 0x00020E88
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					MailboxPlanIdParameter.MailboxPlanFilter
				});
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00022CB3 File Offset: 0x00020EB3
		public new static MailboxPlanIdParameter Parse(string identity)
		{
			return new MailboxPlanIdParameter(identity);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00022CBB File Offset: 0x00020EBB
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxPlan(id);
		}

		// Token: 0x04000286 RID: 646
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox
		};

		// Token: 0x04000287 RID: 647
		private static ComparisonFilter MailboxPlanFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailboxPlan);
	}
}
