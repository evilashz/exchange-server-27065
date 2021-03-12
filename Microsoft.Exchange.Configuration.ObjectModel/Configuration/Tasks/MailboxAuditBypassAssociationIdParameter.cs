using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	public class MailboxAuditBypassAssociationIdParameter : RecipientIdParameter
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00022BF1 File Offset: 0x00020DF1
		public MailboxAuditBypassAssociationIdParameter()
		{
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00022BF9 File Offset: 0x00020DF9
		public MailboxAuditBypassAssociationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00022C02 File Offset: 0x00020E02
		public MailboxAuditBypassAssociationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00022C0B File Offset: 0x00020E0B
		public MailboxAuditBypassAssociationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00022C14 File Offset: 0x00020E14
		public new static MailboxAuditBypassAssociationIdParameter Parse(string identity)
		{
			return new MailboxAuditBypassAssociationIdParameter(identity);
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00022C1C File Offset: 0x00020E1C
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailboxAuditBypassAssociationIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x04000285 RID: 645
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.Computer
		};
	}
}
