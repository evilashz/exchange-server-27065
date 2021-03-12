using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007CE RID: 1998
	[Cmdlet("Set", "MailboxJunkEmailConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxJunkEmailConfiguration : SetXsoObjectWithIdentityTaskBase<MailboxJunkEmailConfiguration>
	{
		// Token: 0x06004615 RID: 17941 RVA: 0x0011FFCE File Offset: 0x0011E1CE
		protected override void InternalValidate()
		{
			base.InternalValidate();
			base.VerifyIsWithinScopes((IRecipientSession)base.DataSession, this.DataObject, true, new DataAccessTask<ADUser>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x0011FFFA File Offset: 0x0011E1FA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxJunkEmailConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x0012000C File Offset: 0x0011E20C
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new MailboxJunkEmailConfigurationDataProvider(principal, base.TenantGlobalCatalogSession, "Set-MailboxJunkEmailConfiguration");
		}
	}
}
