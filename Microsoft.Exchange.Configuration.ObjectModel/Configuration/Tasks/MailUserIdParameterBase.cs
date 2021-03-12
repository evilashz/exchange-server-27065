using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	public abstract class MailUserIdParameterBase : RecipientIdParameter
	{
		// Token: 0x06000D69 RID: 3433 RVA: 0x000289CF File Offset: 0x00026BCF
		public MailUserIdParameterBase(string identity) : base(identity)
		{
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000289D8 File Offset: 0x00026BD8
		public MailUserIdParameterBase()
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000289E0 File Offset: 0x00026BE0
		public MailUserIdParameterBase(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000289E9 File Offset: 0x00026BE9
		public MailUserIdParameterBase(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000289F7 File Offset: 0x00026BF7
		public MailUserIdParameterBase(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00028A00 File Offset: 0x00026C00
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailUserIdParameterBase.AllowedRecipientTypes;
			}
		}

		// Token: 0x040002F6 RID: 758
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.MailUser
		};
	}
}
