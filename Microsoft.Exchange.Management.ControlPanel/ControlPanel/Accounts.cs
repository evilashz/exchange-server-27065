using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000289 RID: 649
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class Accounts : RecipientDataSourceService, IAccounts, IDataSourceService<MailboxFilter, MailboxRecipientRow, Account, SetAccount, NewAccount, RemoveAccount>, IEditListService<MailboxFilter, MailboxRecipientRow, Account, NewAccount, RemoveAccount>, IGetListService<MailboxFilter, MailboxRecipientRow>, INewObjectService<MailboxRecipientRow, NewAccount>, IRemoveObjectsService<RemoveAccount>, IEditObjectForListService<Account, SetAccount, MailboxRecipientRow>, IGetObjectService<Account>, IGetObjectForListService<MailboxRecipientRow>
	{
		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x0008459C File Offset: 0x0008279C
		protected virtual bool IsProfilePage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x0008459F File Offset: 0x0008279F
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:Organization")]
		public PowerShellResults<MailboxRecipientRow> GetList(MailboxFilter filter, SortOptions sort)
		{
			return base.GetList<MailboxRecipientRow, MailboxFilter>("Get-Recipient", filter, sort);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000845B0 File Offset: 0x000827B0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Self+Get-Mailbox?Identity@R:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization")]
		public PowerShellResults<Account> GetObject(Identity identity)
		{
			bool flag = identity != null;
			identity = (identity ?? Identity.FromExecutingUserId());
			PowerShellResults<Account> @object = base.GetObject<Account>("Get-Mailbox", identity);
			if (@object.SucceededWithValue)
			{
				@object.Value.IsProfilePage = this.IsProfilePage;
				PowerShellResults<User> powerShellResults = @object.MergeErrors<User>(base.GetObject<User>("Get-User", identity));
				if (powerShellResults.SucceededWithValue)
				{
					@object.Value.OrgPersonObject = powerShellResults.Value;
					if (RbacPrincipal.Current.IsInRole("MultiTenant+Mailbox+Get-MailboxStatistics?Identity@R:Organization") || (this.IsProfilePage && RbacPrincipal.Current.IsInRole("Mailbox+Get-MailboxStatistics?Identity@R:Self")))
					{
						PowerShellResults<MailboxStatistics> object2 = base.GetObject<MailboxStatistics>("Get-MailboxStatistics", identity, false);
						if (object2.SucceededWithValue)
						{
							@object.Value.Statistics = object2.Value;
						}
					}
					if (@object.Value.IsRoom && RbacPrincipal.Current.IsInRole("Get-CalendarProcessing?Identity@R:Organization"))
					{
						PowerShellResults<CalendarConfiguration> powerShellResults2 = @object.MergeErrors<CalendarConfiguration>(base.GetObject<CalendarConfiguration>("Get-CalendarProcessing", identity, false));
						if (powerShellResults2.SucceededWithValue)
						{
							@object.Value.CalendarConfiguration = powerShellResults2.Value;
						}
					}
					if (RbacPrincipal.Current.IsInRole("Get-SupervisionListEntry?Identity@R:Organization"))
					{
						SupervisionListEntryFilter filter = new SupervisionListEntryFilter
						{
							Identity = identity,
							Tag = null
						};
						PowerShellResults<SupervisionListEntryRow> powerShellResults3 = @object.MergeErrors<SupervisionListEntryRow>(base.GetList<SupervisionListEntryRow, SupervisionListEntryFilter>("Get-SupervisionListEntry", filter, null));
						if (powerShellResults3.Succeeded)
						{
							@object.Value.AllowedSenders = this.GetStringsWithTag(powerShellResults3.Output, "allow");
							@object.Value.BlockedSenders = this.GetStringsWithTag(powerShellResults3.Output, "reject");
						}
					}
				}
				if (RbacPrincipal.Current.IsInRole("Get-CasMailbox?Identity@R:Organization") || (!flag && RbacPrincipal.Current.IsInRole("Get-CasMailbox?Identity@R:Self")))
				{
					PowerShellResults<CASMailbox> powerShellResults4 = @object.MergeErrors<CASMailbox>(base.GetObject<CASMailbox>("Get-CasMailbox", identity, false));
					if (powerShellResults4.SucceededWithValue)
					{
						@object.Value.CasMailbox = powerShellResults4.Value;
					}
				}
			}
			return @object;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000847A3 File Offset: 0x000829A3
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity@R:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity@R:Organization")]
		public PowerShellResults<MailboxRecipientRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<MailboxRecipientRow>("Get-Recipient", identity);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000847B4 File Offset: 0x000829B4
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-Mailbox?Identity&RetentionPolicy@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity&LitigationHoldEnabled&RetentionComment&RetentionUrl@W:Organization+Set-Mailbox?Identity&LitigationHoldEnabled@W:Organization+Set-Mailbox?Identity&LitigationHoldEnabled@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Disable-UMMailbox?Identity&Confirm@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Self+Remove-SupervisionListEntry?Identity@W:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Organization+Remove-SupervisionListEntry?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Set-User?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Self+Get-Mailbox?Identity@R:Self+Set-User?Identity@W:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Self+Get-Mailbox?Identity@R:Self+Set-Mailbox?Identity@W:Self")]
		public PowerShellResults<MailboxRecipientRow> SetObject(Identity identity, SetAccount properties)
		{
			PowerShellResults<MailboxRecipientRow> powerShellResults = new PowerShellResults<MailboxRecipientRow>();
			bool flag = identity == null || StringComparer.OrdinalIgnoreCase.Equals(identity.RawIdentity, Identity.FromExecutingUserId().RawIdentity);
			identity = (identity ?? Identity.FromExecutingUserId());
			properties.FaultIfNull();
			if (properties.SetMailbox.EmailAddresses != null || properties.SetMailbox.MailTip != null || properties.SetMailbox.RoleAssignmentPolicy != null || properties.SetMailbox.RetentionPolicy != null || properties.SetMailbox.MailboxPlan != null || properties.SetMailbox.ResourceCapacity != null || properties.SetMailbox.LitigationHoldEnabled != null || properties.SetMailbox.RetentionComment != null || properties.SetMailbox.RetentionUrl != null)
			{
				if (properties.SetMailbox.EmailAddresses != null)
				{
					PowerShellResults<Account> @object = base.GetObject<Account>("Get-Mailbox", identity);
					powerShellResults.MergeErrors<Account>(@object);
					if (@object.HasValue)
					{
						properties.SetMailbox.UpdateEmailAddresses(@object.Output[0].Mailbox);
					}
				}
				if (properties.SetMailbox.ResourceCapacity != null && string.IsNullOrEmpty(properties.SetMailbox.ResourceCapacity))
				{
					properties.SetMailbox.ResourceCapacity = null;
				}
				if (properties.SetMailbox.RetentionPolicy != null && string.IsNullOrEmpty(properties.SetMailbox.RetentionPolicy))
				{
					properties.SetMailbox.RetentionPolicy = null;
				}
				powerShellResults.MergeErrors<Mailbox>(base.SetObject<Mailbox, SetMailbox>("Set-Mailbox", identity, properties.SetMailbox));
				if (powerShellResults.Failed)
				{
					return powerShellResults;
				}
			}
			bool flag2 = RbacPrincipal.Current.IsInRole("Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Organization+Remove-SupervisionListEntry?Identity@W:Organization") || RbacPrincipal.Current.IsInRole("Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Self+Remove-SupervisionListEntry?Identity@W:Self");
			if (flag2)
			{
				powerShellResults = this.UpdateSupervisionBlockAllowLists(identity, properties, powerShellResults);
				if (powerShellResults.Failed)
				{
					return powerShellResults;
				}
			}
			powerShellResults.MergeAll(base.SetObject<Account, SetAccount, MailboxRecipientRow>("Set-User", identity, properties));
			if (powerShellResults.SucceededWithValue && powerShellResults.Value.IsRoom)
			{
				properties.SetCalendarProcessing.UpdateResourceObjects();
				if (properties.AutomaticBooking != null || properties.ResourceDelegates != null)
				{
					powerShellResults.MergeErrors<CalendarConfiguration>(base.SetObject<CalendarConfiguration, SetCalendarProcessing>("Set-CalendarProcessing", identity, properties.SetCalendarProcessing));
				}
			}
			if (properties.EnableUM != null && properties.EnableUM == false)
			{
				powerShellResults.MergeErrors(new UMMailboxService().RemoveObjects(new Identity[]
				{
					identity
				}, null));
			}
			if (properties.EnableLitigationHold != null && properties.EnableLitigationHold == false)
			{
				PSCommand pscommand = new PSCommand();
				pscommand.AddCommand("Set-Mailbox");
				pscommand.AddParameter("Identity", identity);
				pscommand.AddParameter("LitigationHoldEnabled", false);
				powerShellResults.MergeErrors<Mailbox>(base.Invoke<Mailbox>(pscommand));
			}
			if (RbacPrincipal.Current.IsInRole("Set-CasMailbox?Identity@W:Self|Organization"))
			{
				powerShellResults.MergeErrors<CASMailbox>(base.SetObject<CASMailbox, SetCasMailbox>("Set-CasMailbox", identity, properties.SetCasMailbox));
			}
			if (flag && powerShellResults.SucceededWithValue)
			{
				RbacPrincipal.Current.Name = powerShellResults.Value.DisplayName;
			}
			return powerShellResults;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00084AE0 File Offset: 0x00082CE0
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-RemovedMailbox?Identity@R:Organization")]
		public virtual PowerShellResults<RecoverAccount> GetObjectForNew(Identity identity)
		{
			if (!(identity == null))
			{
				return base.GetObject<RecoverAccount>("Get-RemovedMailbox", identity, false);
			}
			return new PowerShellResults<RecoverAccount>();
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00084B08 File Offset: 0x00082D08
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-Mailbox?DisplayName&Name&Room&PrimarySmtpAddress@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-Mailbox?DisplayName&Password&Name&MicrosoftOnlineServicesID@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-Mailbox?DisplayName&Password&Name&WindowsLiveID@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "!LiveID+New-Mailbox?DisplayName&Password&Name&UserPrincipalName@W:Organization")]
		public virtual PowerShellResults<MailboxRecipientRow> NewObject(NewAccount properties)
		{
			PowerShellResults<MailboxRecipientRow> powerShellResults = base.NewObject<MailboxRecipientRow, NewAccount>("New-Mailbox", properties);
			if (powerShellResults.SucceededWithValue)
			{
				if (properties.Room == true)
				{
					properties.SetCalendarProcessing.UpdateResourceObjects();
					PowerShellResults<CalendarConfiguration> powerShellResults2 = base.SetObject<CalendarConfiguration, SetCalendarProcessing>("Set-CalendarProcessing", powerShellResults.Value.Identity, properties.SetCalendarProcessing);
					if (powerShellResults2.Failed)
					{
						powerShellResults.Warnings = powerShellResults.Warnings.Concat(new string[]
						{
							OwaOptionStrings.NewRoomCreationWarningText
						}).ToArray<string>();
						powerShellResults.Warnings = powerShellResults.Warnings.Concat(from x in powerShellResults2.ErrorRecords
						select x.Message).ToArray<string>();
						if (powerShellResults2.Warnings != null)
						{
							powerShellResults.Warnings = powerShellResults.Warnings.Concat(powerShellResults2.Warnings).ToArray<string>();
						}
					}
				}
				else if (powerShellResults.HasWarnings)
				{
					LocalizedString warningUnlicensedMailbox = Strings.WarningUnlicensedMailbox;
					if (powerShellResults.Warnings.Contains(warningUnlicensedMailbox))
					{
						List<string> list = powerShellResults.Warnings.ToList<string>();
						list.Remove(warningUnlicensedMailbox);
						powerShellResults.Warnings = list.ToArray();
					}
				}
				PowerShellResults<MailboxRecipientRow> objectForList = this.GetObjectForList(powerShellResults.Value.Identity);
				if (objectForList != null)
				{
					powerShellResults.Output = objectForList.Output;
				}
			}
			return powerShellResults;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00084C80 File Offset: 0x00082E80
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Remove-Mailbox?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Enterprise+Remove-Mailbox?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, RemoveAccount parameters)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Remove-Mailbox");
			return base.RemoveObjects(psCommand, identities, parameters);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x00084CA8 File Offset: 0x00082EA8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient@R:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity@R:Organization")]
		internal ReducedRecipient GetRecipientObject(Identity identity)
		{
			Identity identity2 = identity;
			ReducedRecipient result = null;
			if (null == identity2 && RbacPrincipal.Current.ExecutingUserId != null)
			{
				identity2 = Identity.FromExecutingUserId();
			}
			if (null != identity2)
			{
				PowerShellResults<MailboxRecipientRow> @object = base.GetObject<MailboxRecipientRow>("Get-Recipient", identity2, false);
				if (@object.SucceededWithValue)
				{
					result = @object.Value.Recipient;
				}
			}
			return result;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00084D24 File Offset: 0x00082F24
		private IEnumerable<string> GetStringsWithTag(SupervisionListEntryRow[] entries, string tag)
		{
			return from entry in entries
			where entry.Tag == tag
			select entry.EntryName;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00084D74 File Offset: 0x00082F74
		private PowerShellResults<MailboxRecipientRow> UpdateSupervisionBlockAllowLists(Identity identity, SetAccount properties, PowerShellResults<MailboxRecipientRow> results)
		{
			if (properties.AllowedSenders == null && properties.BlockedSenders == null)
			{
				return results;
			}
			SupervisionListEntryFilter filter = new SupervisionListEntryFilter
			{
				Identity = identity,
				Tag = null
			};
			PowerShellResults<SupervisionListEntryRow> powerShellResults = results.MergeErrors<SupervisionListEntryRow>(base.GetList<SupervisionListEntryRow, SupervisionListEntryFilter>("Get-SupervisionListEntry", filter, null));
			if (powerShellResults.Succeeded)
			{
				if (properties.AllowedSenders != null)
				{
					IEnumerable<string> stringsWithTag = this.GetStringsWithTag(powerShellResults.Output, "allow");
					results = this.UpdateSupervisionListForTag(identity, results, stringsWithTag, properties.AllowedSenders, "allow");
					if (results.Failed)
					{
						return results;
					}
				}
				if (properties.BlockedSenders != null)
				{
					IEnumerable<string> stringsWithTag2 = this.GetStringsWithTag(powerShellResults.Output, "reject");
					results = this.UpdateSupervisionListForTag(identity, results, stringsWithTag2, properties.BlockedSenders, "reject");
					if (results.Failed)
					{
						return results;
					}
				}
			}
			return results;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00084E40 File Offset: 0x00083040
		private PowerShellResults<MailboxRecipientRow> UpdateSupervisionListForTag(Identity identity, PowerShellResults<MailboxRecipientRow> results, IEnumerable<string> originalList, IEnumerable<string> newList, string tag)
		{
			if (newList == null)
			{
				newList = new List<string>();
			}
			IEnumerable<string> second = newList.Intersect(originalList);
			IEnumerable<string> enumerable = originalList.Except(second);
			IEnumerable<string> enumerable2 = newList.Except(second);
			NewSupervisionListEntry newSupervisionListEntry = new NewSupervisionListEntry();
			foreach (string entryName in enumerable2)
			{
				newSupervisionListEntry.Identity = identity;
				newSupervisionListEntry.EntryName = entryName;
				newSupervisionListEntry.Tag = tag;
				results.MergeErrors<SupervisionListEntry>(base.NewObject<SupervisionListEntry, NewSupervisionListEntry>("Add-SupervisionListEntry", newSupervisionListEntry));
				if (results.Failed)
				{
					return results;
				}
			}
			foreach (string value in enumerable)
			{
				PSCommand pscommand = new PSCommand().AddCommand("Remove-SupervisionListEntry");
				pscommand.AddParameter("Entry", value);
				pscommand.AddParameter("Tag", tag);
				results.MergeErrors(base.RemoveObjects(pscommand, new Identity[]
				{
					identity
				}, null));
				if (results.Failed)
				{
					return results;
				}
			}
			return results;
		}

		// Token: 0x04002126 RID: 8486
		internal const string GetSupervisionListEntryRole_Organization = "Get-SupervisionListEntry?Identity@R:Organization";

		// Token: 0x04002127 RID: 8487
		internal const string SetObjectRole_Organization_SetSupervisionListEntry = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Organization+Remove-SupervisionListEntry?Identity@W:Organization";

		// Token: 0x04002128 RID: 8488
		internal const string SetObjectRole_Self_SetSupervisionListEntry = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Self+Remove-SupervisionListEntry?Identity@W:Self";

		// Token: 0x04002129 RID: 8489
		internal const string GetObjectRole_Organization_GetLitigationHold = "Get-Mailbox?Identity@R:Organization";

		// Token: 0x0400212A RID: 8490
		internal const string SetObjectRole_Organization_EnableLitigationHold = "Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity&LitigationHoldEnabled&RetentionComment&RetentionUrl@W:Organization";

		// Token: 0x0400212B RID: 8491
		internal const string SetObjectRole_Organization_EditLitigationHold = "Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity&RetentionComment&RetentionUrl@W:Organization";

		// Token: 0x0400212C RID: 8492
		internal const string SetObjectRole_Organization_DisableLitigationHold = "Set-Mailbox?Identity&LitigationHoldEnabled@W:Organization";

		// Token: 0x0400212D RID: 8493
		private const string GetObjectRole_Organization_GetCalendarProcessing = "Get-CalendarProcessing?Identity@R:Organization";

		// Token: 0x0400212E RID: 8494
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:Organization";

		// Token: 0x0400212F RID: 8495
		private const string GetObjectRole_Self = "Get-User?Identity@R:Self+Get-Mailbox?Identity@R:Self";

		// Token: 0x04002130 RID: 8496
		private const string GetObjectRole_Organization = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization";

		// Token: 0x04002131 RID: 8497
		private const string GetObjectRole_Organization_GetMailboxStatistics = "MultiTenant+Mailbox+Get-MailboxStatistics?Identity@R:Organization";

		// Token: 0x04002132 RID: 8498
		private const string GetObjectRole_Self_GetMailboxStatistics = "Mailbox+Get-MailboxStatistics?Identity@R:Self";

		// Token: 0x04002133 RID: 8499
		private const string GetObjectForListRole_Org = "Get-Recipient?Identity@R:Organization";

		// Token: 0x04002134 RID: 8500
		private const string GetObjectForListRole_Self = "Get-Recipient?Identity@R:Self";

		// Token: 0x04002135 RID: 8501
		private const string SetObjectRole_Self_SetMailbox = "Get-User?Identity@R:Self+Get-Mailbox?Identity@R:Self+Set-Mailbox?Identity@W:Self";

		// Token: 0x04002136 RID: 8502
		private const string SetObjectRole_Self_SetUser = "Get-User?Identity@R:Self+Get-Mailbox?Identity@R:Self+Set-User?Identity@W:Self";

		// Token: 0x04002137 RID: 8503
		private const string SetObjectRole_Organization_SetMailbox = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity@W:Organization";

		// Token: 0x04002138 RID: 8504
		private const string SetObjectRole_Organization_SetUser = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Set-User?Identity@W:Organization";

		// Token: 0x04002139 RID: 8505
		private const string GetObjectForNewRole_MultiTenant = "MultiTenant+Get-RemovedMailbox?Identity@R:Organization";

		// Token: 0x0400213A RID: 8506
		private const string NewObjectRole_NonLiveID = "!LiveID+New-Mailbox?DisplayName&Password&Name&UserPrincipalName@W:Organization";

		// Token: 0x0400213B RID: 8507
		internal const string NewObjectRole_WLID = "MultiTenant+New-Mailbox?DisplayName&Password&Name&WindowsLiveID@W:Organization";

		// Token: 0x0400213C RID: 8508
		internal const string NewObjectRole_MOSID = "MultiTenant+New-Mailbox?DisplayName&Password&Name&MicrosoftOnlineServicesID@W:Organization";

		// Token: 0x0400213D RID: 8509
		private const string NewObjectRole_MultiTenant_Room = "MultiTenant+New-Mailbox?DisplayName&Name&Room&PrimarySmtpAddress@W:Organization";

		// Token: 0x0400213E RID: 8510
		private const string RemoveObjectRole_Enterprise = "Enterprise+Remove-Mailbox?Identity@W:Organization";

		// Token: 0x0400213F RID: 8511
		private const string RemoveObjectRole_MultiTenant = "MultiTenant+Remove-Mailbox?Identity@W:Organization";

		// Token: 0x04002140 RID: 8512
		private const string GetCasMailboxRole_Org = "Get-CasMailbox?Identity@R:Organization";

		// Token: 0x04002141 RID: 8513
		private const string GetCasMailboxRole_Self = "Get-CasMailbox?Identity@R:Self";

		// Token: 0x04002142 RID: 8514
		private const string SetCasMailboxRole = "Set-CasMailbox?Identity@W:Self|Organization";

		// Token: 0x04002143 RID: 8515
		private const string GetRecipientRole_Org = "Get-Recipient?Identity@R:Organization";

		// Token: 0x04002144 RID: 8516
		private const string GetRecipientRole_Self = "Get-Recipient@R:Self";

		// Token: 0x04002145 RID: 8517
		private const string SetObjectRole_Organization_SetLitigationHold = "Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity&LitigationHoldEnabled&RetentionComment&RetentionUrl@W:Organization+Set-Mailbox?Identity&LitigationHoldEnabled@W:Organization+Set-Mailbox?Identity&LitigationHoldEnabled@W:Organization";

		// Token: 0x04002146 RID: 8518
		private const string SetObjectRole_Organization_SetRetentionPolicy = "Set-Mailbox?Identity&RetentionPolicy@W:Organization";
	}
}
