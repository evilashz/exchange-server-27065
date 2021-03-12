using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B6 RID: 1974
	[Cmdlet("Clear", "TextMessagingAccount", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class ClearTextMessagingAccount : SetXsoObjectWithIdentityTaskBase<TextMessagingAccount>
	{
		// Token: 0x06004575 RID: 17781 RVA: 0x0011D33A File Offset: 0x0011B53A
		public override object GetDynamicParameters()
		{
			return null;
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x0011D33D File Offset: 0x0011B53D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageClearTextMessagingAccount(this.Identity.ToString());
			}
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x0011D34F File Offset: 0x0011B54F
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new VersionedXmlDataProvider(principal, userToken, "Clear-TextMessagingAccount");
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x0011D438 File Offset: 0x0011B638
		protected override void SaveXsoObject(IConfigDataProvider provider, IConfigurable dataObject)
		{
			Exception ex = null;
			try
			{
				TextMessagingHelper.SaveTextMessagingAccount((TextMessagingAccount)dataObject, (VersionedXmlDataProvider)provider, this.DataObject, (IRecipientSession)base.DataSession);
				MailboxTaskHelper.ProcessRecord(delegate
				{
					Rules inboxRules = ((VersionedXmlDataProvider)provider).MailboxSession.InboxRules;
					bool flag = false;
					foreach (Rule rule in inboxRules)
					{
						if (rule.IsEnabled)
						{
							bool flag2 = true;
							foreach (ActionBase actionBase in rule.Actions)
							{
								if (!(actionBase is SendSmsAlertToRecipientsAction) && !(actionBase is StopProcessingAction))
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								rule.IsEnabled = false;
								flag = true;
							}
						}
					}
					if (flag)
					{
						inboxRules.Save();
					}
				}, new MailboxTaskHelper.ThrowTerminatingErrorDelegate(base.WriteError), this.Identity);
			}
			catch (ObjectExistedException ex2)
			{
				ex = ex2;
			}
			catch (SaveConflictException ex3)
			{
				ex = ex3;
			}
			catch (ADObjectEntryAlreadyExistsException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				base.WriteError(new ConflictSavingException(this.DataObject.Identity.ToString(), ex), ErrorCategory.InvalidOperation, this.Identity);
			}
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x0011D50C File Offset: 0x0011B70C
		protected override void StampChangesOnXsoObject(IConfigurable dataObject)
		{
			TextMessagingAccount textMessagingAccount = (TextMessagingAccount)dataObject;
			textMessagingAccount.NotificationPhoneNumber = (E164Number)TextMessagingAccountSchema.NotificationPhoneNumber.DefaultValue;
			textMessagingAccount.CountryRegionId = (RegionInfo)TextMessagingAccountSchema.CountryRegionId.DefaultValue;
			textMessagingAccount.MobileOperatorId = (int)TextMessagingAccountSchema.MobileOperatorId.DefaultValue;
			textMessagingAccount.TextMessagingSettings.MachineToPersonMessagingPolicies.PossibleRecipients.Clear();
		}
	}
}
