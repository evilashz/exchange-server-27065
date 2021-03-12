using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F71 RID: 3953
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangePrincipalMailboxOwnerAdapter : MailboxOwnerAdapter
	{
		// Token: 0x0600871E RID: 34590 RVA: 0x002506A3 File Offset: 0x0024E8A3
		public ExchangePrincipalMailboxOwnerAdapter(IExchangePrincipal principal, IConstraintProvider constraintProvider, RecipientTypeDetails recipientTypeDetails, LogonType logonType) : base(constraintProvider, recipientTypeDetails, logonType)
		{
			this.principal = principal;
		}

		// Token: 0x0600871F RID: 34591 RVA: 0x002506B6 File Offset: 0x0024E8B6
		protected override IGenericADUser CalculateGenericADUser()
		{
			return new GenericADUser(this.principal);
		}

		// Token: 0x04005A41 RID: 23105
		private readonly IExchangePrincipal principal;
	}
}
