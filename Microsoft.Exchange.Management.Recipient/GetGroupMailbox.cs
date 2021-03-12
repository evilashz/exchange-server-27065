using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200004D RID: 77
	[Cmdlet("Get", "GroupMailbox", DefaultParameterSetName = "Identity")]
	public sealed class GetGroupMailbox : GetMailboxOrSyncMailbox
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00015389 File Offset: 0x00013589
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00015391 File Offset: 0x00013591
		private new SwitchParameter Arbitration { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001539A File Offset: 0x0001359A
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000153A2 File Offset: 0x000135A2
		private new SwitchParameter Archive { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000153AB File Offset: 0x000135AB
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000153B3 File Offset: 0x000135B3
		private new PSCredential Credential { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000153BC File Offset: 0x000135BC
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000153C4 File Offset: 0x000135C4
		private new SwitchParameter InactiveMailboxOnly { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000153CD File Offset: 0x000135CD
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000153D5 File Offset: 0x000135D5
		private new SwitchParameter IncludeSoftDeletedMailbox { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000153DE File Offset: 0x000135DE
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x000153E6 File Offset: 0x000135E6
		private new MailboxPlanIdParameter MailboxPlan { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000153EF File Offset: 0x000135EF
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000153F7 File Offset: 0x000135F7
		private new SwitchParameter Monitoring { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00015400 File Offset: 0x00013600
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00015408 File Offset: 0x00013608
		private new SwitchParameter PublicFolder { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00015411 File Offset: 0x00013611
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00015419 File Offset: 0x00013619
		private new RecipientTypeDetails[] RecipientTypeDetails { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00015422 File Offset: 0x00013622
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001542A File Offset: 0x0001362A
		private new SwitchParameter RemoteArchive { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00015433 File Offset: 0x00013633
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0001543B File Offset: 0x0001363B
		private new SwitchParameter SoftDeletedMailbox { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00015444 File Offset: 0x00013644
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001544C File Offset: 0x0001364C
		private new long UsnForReconciliationSearch { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00015455 File Offset: 0x00013655
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0001545D File Offset: 0x0001365D
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMembers { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00015466 File Offset: 0x00013666
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x0001546E File Offset: 0x0001366E
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludePermissionsVersion { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00015477 File Offset: 0x00013677
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0001547F File Offset: 0x0001367F
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMemberSyncStatus { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00015488 File Offset: 0x00013688
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00015490 File Offset: 0x00013690
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMailboxUrls { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00015499 File Offset: 0x00013699
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000154B0 File Offset: 0x000136B0
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNullOrEmpty]
		public RecipientIdParameter ExecutingUser
		{
			get
			{
				return (RecipientIdParameter)base.Fields["ExecutingUser"];
			}
			set
			{
				base.Fields["ExecutingUser"] = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000154C3 File Offset: 0x000136C3
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<GroupMailboxSchema>();
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000154CA File Offset: 0x000136CA
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetGroupMailbox.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000154D1 File Offset: 0x000136D1
		protected override string SystemAddressListRdn
		{
			get
			{
				return "GroupMailboxes(VLV)";
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000154E8 File Offset: 0x000136E8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser aduser = (ADUser)dataObject;
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			GroupMailbox groupMailbox = GroupMailbox.FromDataObject(aduser);
			if ((this.IncludeMemberSyncStatus || this.IncludePermissionsVersion) && CmdletProxy.TryToProxyOutputObject(groupMailbox, base.CurrentTaskContext, aduser, false, this.ConfirmationMessage, CmdletProxy.AppendIdentityToProxyCmdlet(aduser)))
			{
				return groupMailbox;
			}
			ExchangePrincipal exchangePrincipal = null;
			if (this.IncludeMailboxUrls)
			{
				try
				{
					exchangePrincipal = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
					MailboxUrls mailboxUrls = new MailboxUrls(exchangePrincipal, false);
					groupMailbox.InboxUrl = this.SuppressPiiDataAsNeeded(mailboxUrls.InboxUrl);
					groupMailbox.CalendarUrl = this.SuppressPiiDataAsNeeded(mailboxUrls.CalendarUrl);
					groupMailbox.PeopleUrl = this.SuppressPiiDataAsNeeded(mailboxUrls.PeopleUrl);
					groupMailbox.PhotoUrl = this.SuppressPiiDataAsNeeded(mailboxUrls.PhotoUrl);
				}
				catch (LocalizedException ex)
				{
					base.WriteWarning("Unable to get mailbox principal due exception: " + ex.Message);
				}
			}
			IdentityDetails[] ownersDetails = this.GetOwnersDetails(recipientSession, aduser);
			if (ownersDetails != null)
			{
				groupMailbox.OwnersDetails = ownersDetails;
			}
			ADRawEntry[] array = null;
			if (this.IncludeMembers)
			{
				array = this.GetMemberRawEntriesFromAD(recipientSession, aduser);
				IdentityDetails[] identityDetails = this.GetIdentityDetails(array);
				groupMailbox.MembersDetails = identityDetails;
				groupMailbox.Members = Array.ConvertAll<IdentityDetails, ADObjectId>(identityDetails, (IdentityDetails member) => member.Identity);
			}
			if (!this.IncludeMemberSyncStatus)
			{
				if (!this.IncludePermissionsVersion)
				{
					return groupMailbox;
				}
			}
			try
			{
				if (exchangePrincipal == null)
				{
					exchangePrincipal = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
				}
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-GroupMailbox"))
				{
					if (this.IncludeMemberSyncStatus)
					{
						if (array == null)
						{
							array = this.GetMemberRawEntriesFromAD(recipientSession, aduser);
						}
						ADObjectId[] membersInAD = Array.ConvertAll<ADRawEntry, ADObjectId>(array, (ADRawEntry member) => member.Id);
						ADObjectId[] membersFromMailbox = this.GetMembersFromMailbox(recipientSession, aduser, mailboxSession);
						groupMailbox.MembersSyncStatus = new GroupMailboxMembersSyncStatus(membersInAD, membersFromMailbox);
						if (base.NeedSuppressingPiiData)
						{
							groupMailbox.MembersSyncStatus.MembersInADOnly = SuppressingPiiData.Redact(groupMailbox.MembersSyncStatus.MembersInADOnly);
							groupMailbox.MembersSyncStatus.MembersInMailboxOnly = SuppressingPiiData.Redact(groupMailbox.MembersSyncStatus.MembersInMailboxOnly);
						}
					}
					if (this.IncludePermissionsVersion)
					{
						groupMailbox.PermissionsVersion = mailboxSession.Mailbox.GetValueOrDefault<int>(MailboxSchema.GroupMailboxPermissionsVersion, 0).ToString();
					}
				}
			}
			catch (LocalizedException ex2)
			{
				base.WriteWarning("Unable to retrieve data from group mailbox due exception: " + ex2.Message);
			}
			return groupMailbox;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001579C File Offset: 0x0001399C
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetGroupMailboxTaskModuleFactory();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000157A4 File Offset: 0x000139A4
		private IdentityDetails[] GetOwnersDetails(IRecipientSession recipientSession, ADUser adUser)
		{
			ADObjectId[] array = new List<ADObjectId>((MultiValuedProperty<ADObjectId>)adUser[GroupMailboxSchema.Owners]).ToArray();
			IdentityDetails[] result;
			try
			{
				Result<ADRawEntry>[] queryResults = recipientSession.ReadMultiple(array, IdentityDetails.Properties);
				result = this.GetIdentityDetails(queryResults, array);
			}
			catch (LocalizedException ex)
			{
				base.WriteWarning("Unable to retrieve owners details from directory due exception: " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00015810 File Offset: 0x00013A10
		private ADRawEntry[] GetMemberRawEntriesFromAD(IRecipientSession recipientSession, ADUser groupAdUser)
		{
			ADRawEntry[] result;
			try
			{
				result = UnifiedGroupADAccessLayer.GetAllGroupMembers(recipientSession, groupAdUser.Id, IdentityDetails.Properties, new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending), null, 0).ReadAllPages();
			}
			catch (LocalizedException ex)
			{
				base.WriteWarning("Unable to retrieve members details from directory due exception: " + ex.Message);
				result = new ADRawEntry[0];
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001587C File Offset: 0x00013A7C
		private ADObjectId[] GetMembersFromMailbox(IRecipientSession recipientSession, ADUser groupAdUser, MailboxSession mailboxSession)
		{
			ADRawEntry[] memberRawEntriesFromMailbox = this.GetMemberRawEntriesFromMailbox(recipientSession, groupAdUser, mailboxSession);
			return Array.ConvertAll<ADRawEntry, ADObjectId>(memberRawEntriesFromMailbox, (ADRawEntry member) => member.Id);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015930 File Offset: 0x00013B30
		private ADRawEntry[] GetMemberRawEntriesFromMailbox(IRecipientSession recipientSession, ADUser adUser, MailboxSession mailboxSession)
		{
			GroupMailboxLocator groupLocator = GroupMailboxLocator.Instantiate(recipientSession, adUser);
			List<string> exchangeLegacyDNs = new List<string>(10);
			GroupMailboxAccessLayer.Execute("Get-GroupMailbox", recipientSession, mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				IEnumerable<UserMailbox> members = accessLayer.GetMembers(groupLocator, false, null);
				foreach (UserMailbox userMailbox in members)
				{
					exchangeLegacyDNs.Add(userMailbox.Locator.LegacyDn);
				}
			});
			string[] array = exchangeLegacyDNs.ToArray();
			List<ADRawEntry> list = new List<ADRawEntry>(array.Length);
			try
			{
				Result<ADRawEntry>[] array2 = recipientSession.FindByExchangeLegacyDNs(array, IdentityDetails.Properties);
				for (int i = 0; i < array2.Length; i++)
				{
					Result<ADRawEntry> result = array2[i];
					if (result.Error != null)
					{
						this.WriteWarning(Strings.WarningUnableToResolveUser(array[i].ToString(), result.Error.ToString()));
					}
					else if (result.Data == null)
					{
						base.WriteVerbose(Strings.WarningUnableToResolveUser(array[i].ToString(), string.Empty));
					}
					else
					{
						list.Add(result.Data);
					}
				}
			}
			catch (LocalizedException ex)
			{
				base.WriteWarning("Unable to retrieve members details from mailbox due exception: " + ex.Message);
			}
			return list.ToArray();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015A48 File Offset: 0x00013C48
		private IdentityDetails[] GetIdentityDetails(Result<ADRawEntry>[] queryResults, object[] ids)
		{
			List<IdentityDetails> list = new List<IdentityDetails>(queryResults.Length);
			for (int i = 0; i < queryResults.Length; i++)
			{
				Result<ADRawEntry> result = queryResults[i];
				if (result.Error != null)
				{
					this.WriteWarning(Strings.WarningUnableToResolveUser(ids[i].ToString(), result.Error.ToString()));
				}
				else if (result.Data == null)
				{
					this.WriteWarning(Strings.WarningUnableToResolveUser(ids[i].ToString(), string.Empty));
				}
				else
				{
					IdentityDetails identityDetails = new IdentityDetails(result.Data);
					if (base.NeedSuppressingPiiData)
					{
						identityDetails = SuppressingPiiData.Redact(identityDetails);
					}
					list.Add(identityDetails);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015AF8 File Offset: 0x00013CF8
		private IdentityDetails[] GetIdentityDetails(ADRawEntry[] adRawEntries)
		{
			List<IdentityDetails> list = new List<IdentityDetails>(adRawEntries.Length);
			for (int i = 0; i < adRawEntries.Length; i++)
			{
				if (adRawEntries[i] != null)
				{
					IdentityDetails identityDetails = new IdentityDetails(adRawEntries[i]);
					if (base.NeedSuppressingPiiData)
					{
						identityDetails = SuppressingPiiData.Redact(identityDetails);
					}
					list.Add(identityDetails);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015B46 File Offset: 0x00013D46
		private string SuppressPiiDataAsNeeded(string value)
		{
			if (base.NeedSuppressingPiiData && value != null)
			{
				return SuppressingPiiData.Redact(value);
			}
			return value;
		}

		// Token: 0x04000122 RID: 290
		private const string ParameterExecutingUser = "ExecutingUser";

		// Token: 0x04000123 RID: 291
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.GroupMailbox
		};
	}
}
