using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ConfigureGroupMailbox
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00007178 File Offset: 0x00005378
		public ConfigureGroupMailbox(IRecipientSession adSession, ADUser group, ADUser executingUser, MailboxSession mailboxSession)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfInvalidValue<IRecipientSession>("adSession", adSession, (IRecipientSession session) => !session.ReadOnly);
			ArgumentValidator.ThrowIfNull("group", group);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfInvalidValue<MailboxSession>("mailboxSession", mailboxSession, (MailboxSession session) => session.MailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox);
			this.adSession = adSession;
			this.group = group;
			this.executingUser = executingUser;
			this.mailboxSession = mailboxSession;
			this.mailboxStoreTypeProvider = new MailboxStoreTypeProvider(this.group)
			{
				MailboxSession = this.mailboxSession
			};
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000723A File Offset: 0x0000543A
		private IExchangePrincipal MailboxPrincipal
		{
			get
			{
				return this.mailboxSession.MailboxOwner;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007294 File Offset: 0x00005494
		public static MailboxSession CreateMailboxSessionForConfiguration(ExchangePrincipal groupPrincipal, string domainController)
		{
			MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(groupPrincipal, CultureInfo.InvariantCulture, "Client=WebServices;Action=ConfigureGroupMailbox");
			mailboxSession.SetADRecipientSessionFactory((bool isReadonly, ConsistencyMode consistencyMode) => DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, isReadonly, consistencyMode, null, groupPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 127, "CreateMailboxSessionForConfiguration", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\GroupMailboxAccessLayer\\Commands\\ConfigureGroupMailbox.cs"));
			return mailboxSession;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007328 File Offset: 0x00005528
		public GroupMailboxConfigurationReport Execute(GroupMailboxConfigurationAction forceActionMask)
		{
			this.report = new GroupMailboxConfigurationReport();
			if (this.group.IsGroupMailboxConfigured && forceActionMask == GroupMailboxConfigurationAction.None)
			{
				return this.report;
			}
			Predicate<GroupMailboxConfigurationAction> condition = (GroupMailboxConfigurationAction action) => !this.group.IsGroupMailboxConfigured || forceActionMask.Contains(action);
			this.ExecuteIf(condition, GroupMailboxConfigurationAction.SetRegionalSettings, new Action(this.SetRegionalConfiguration));
			this.ExecuteIf(condition, GroupMailboxConfigurationAction.CreateDefaultFolders, new Action(this.CreateDefaultFolders));
			this.ExecuteIf((GroupMailboxConfigurationAction actionType) => !this.group.IsGroupMailboxConfigured, GroupMailboxConfigurationAction.SetInitialFolderPermissions, new Action(this.SetFolderPermissions));
			this.ExecuteIf(condition, GroupMailboxConfigurationAction.ConfigureCalendar, new Action(this.ConfigureCalendar));
			this.ExecuteIf((GroupMailboxConfigurationAction actionType) => !this.group.IsGroupMailboxConfigured, GroupMailboxConfigurationAction.GenerateGroupPhoto, new Action(this.UploadPhoto));
			this.ExecuteIf(condition, GroupMailboxConfigurationAction.SendWelcomeMessage, new Action(this.SendWelcomeMessage));
			this.MarkGroupConfigured();
			return this.report;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000745C File Offset: 0x0000565C
		private void UploadPhoto()
		{
			try
			{
				if (GroupMailboxDefaultPhotoUploader.IsFlightEnabled(this.mailboxSession))
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						GroupMailboxDefaultPhotoUploader groupMailboxDefaultPhotoUploader = new GroupMailboxDefaultPhotoUploader(this.adSession, this.mailboxSession, this.group);
						this.group.ThumbnailPhoto = groupMailboxDefaultPhotoUploader.Upload();
					}, (Exception e) => GrayException.IsSystemGrayException(e));
				}
			}
			catch (LocalizedException arg)
			{
				this.TraceAndReportWarning(new LocalizedString(string.Format("Unable to upload photo for group {0} Error {1}", this.group.PrimarySmtpAddress, arg)));
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000074E8 File Offset: 0x000056E8
		private void SetRegionalConfiguration()
		{
			MailboxRegionalConfiguration mailboxRegionalConfiguration = new MailboxRegionalConfiguration
			{
				TimeZone = ExTimeZoneValue.Parse("Pacific Standard Time"),
				Principal = this.MailboxPrincipal
			};
			mailboxRegionalConfiguration.Save(this.mailboxStoreTypeProvider);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007528 File Offset: 0x00005728
		private void ConfigureCalendar()
		{
			MailboxCalendarConfiguration mailboxCalendarConfiguration = new MailboxCalendarConfiguration
			{
				Principal = this.MailboxPrincipal,
				RemindersEnabled = false,
				ReminderSoundEnabled = false
			};
			mailboxCalendarConfiguration.Save(this.mailboxStoreTypeProvider);
			this.TraceDebug("Save settings to disable calendar reminder", new object[0]);
			using (CalendarConfigurationDataProvider calendarConfigurationDataProvider = new CalendarConfigurationDataProvider(this.mailboxSession))
			{
				CalendarConfiguration instance = new CalendarConfiguration
				{
					MailboxOwnerId = this.group.Id,
					RemoveForwardedMeetingNotifications = true,
					RemoveOldMeetingMessages = true
				};
				calendarConfigurationDataProvider.Save(instance);
				this.TraceDebug("Save settings to disable calendar forward notification.", new object[0]);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000075E0 File Offset: 0x000057E0
		private void CreateDefaultFolders()
		{
			int num = 0;
			foreach (DefaultFolderType defaultFolderType in this.mailboxSession.DefaultFolders)
			{
				if (!GroupMailboxPermissionHandler.IsFolderToBeIgnored(defaultFolderType))
				{
					this.TraceDebug("Getting DefaultFolderId, DefaultFolderType={0}", new object[]
					{
						defaultFolderType
					});
					if (this.mailboxSession.GetDefaultFolderId(defaultFolderType) == null)
					{
						this.TraceDebug("Creating Folder {0}", new object[]
						{
							defaultFolderType
						});
						this.mailboxSession.CreateDefaultFolder(defaultFolderType);
						num++;
					}
				}
			}
			this.report.FoldersCreatedCount = num;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000077F4 File Offset: 0x000059F4
		private void SetFolderPermissions()
		{
			ExternalUser externalUser = ExternalUser.CreateExternalUserForGroupMailbox(this.MailboxPrincipal.MailboxInfo.DisplayName, "Member@local", this.MailboxPrincipal.MailboxInfo.MailboxGuid, SecurityIdentity.GroupMailboxMemberType.Member);
			ExternalUser externalUser2 = ExternalUser.CreateExternalUserForGroupMailbox(this.MailboxPrincipal.MailboxInfo.DisplayName, "Owner@local", this.MailboxPrincipal.MailboxInfo.MailboxGuid, SecurityIdentity.GroupMailboxMemberType.Owner);
			using (ExternalUserCollection externalUsers = this.mailboxSession.GetExternalUsers())
			{
				if (!externalUsers.Contains(externalUser))
				{
					externalUsers.Add(externalUser);
				}
				if (!externalUsers.Contains(externalUser2))
				{
					externalUsers.Add(externalUser2);
				}
				externalUsers.Save();
				if (!externalUsers.Contains(externalUser))
				{
					throw new GroupMailboxFailedToAddExternalUserException(Strings.ErrorUnableToAddExternalUser(externalUser.Name));
				}
				if (!externalUsers.Contains(externalUser2))
				{
					throw new GroupMailboxFailedToAddExternalUserException(Strings.ErrorUnableToAddExternalUser(externalUser2.Name));
				}
				this.TraceDebug("Added external member user {0} to external user collection", new object[]
				{
					externalUser.Name
				});
				this.TraceDebug("Added external owner user {0} to external user collection", new object[]
				{
					externalUser2.Name
				});
			}
			PermissionSecurityPrincipal userSecurityPrincipal = new PermissionSecurityPrincipal(externalUser);
			PermissionSecurityPrincipal userSecurityPrincipal2 = new PermissionSecurityPrincipal(externalUser2);
			int num = 0;
			List<PermissionEntry> list = new List<PermissionEntry>(3);
			var array = new <>f__AnonymousType0<DefaultFolderType, MemberRights, MemberRights>[]
			{
				new
				{
					Folder = DefaultFolderType.MailboxAssociation,
					OwnerPermission = GroupMailboxPermissionHandler.MailboxAssociationPermission,
					MemberPermission = GroupMailboxPermissionHandler.MailboxAssociationPermission
				},
				new
				{
					Folder = DefaultFolderType.SearchFolders,
					OwnerPermission = (GroupMailboxPermissionHandler.SearchFolderPermission | GroupMailboxPermissionHandler.OwnerSpecificPermission),
					MemberPermission = GroupMailboxPermissionHandler.SearchFolderPermission
				},
				new
				{
					Folder = DefaultFolderType.Calendar,
					OwnerPermission = GroupMailboxPermissionHandler.CalendarFolderPermission,
					MemberPermission = GroupMailboxPermissionHandler.CalendarFolderPermission
				}
			};
			list.Add(new PermissionEntry(userSecurityPrincipal2, GroupMailboxPermissionHandler.ConfigurationFolderPermission));
			int num2;
			GroupMailboxPermissionHandler.AssignMemberRight(this.mailboxSession, list, DefaultFolderType.Configuration, out num2);
			num += num2;
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				var <>f__AnonymousType = array2[i];
				list.Clear();
				list.Add(new PermissionEntry(userSecurityPrincipal2, <>f__AnonymousType.OwnerPermission));
				list.Add(new PermissionEntry(userSecurityPrincipal, <>f__AnonymousType.MemberPermission));
				if (!GroupMailboxPermissionHandler.AssignMemberRight(this.mailboxSession, list, <>f__AnonymousType.Folder, out num2))
				{
					throw new GroupMailboxFailedToConfigureMailboxException(Strings.ErrorUnableToConfigureMailbox(<>f__AnonymousType.Folder.ToString(), this.MailboxPrincipal.MailboxInfo.DisplayName));
				}
				num += num2;
			}
			this.report.FoldersPrivilegedCount = num;
			this.mailboxSession.Mailbox[MailboxSchema.GroupMailboxPermissionsVersion] = GroupMailboxPermissionHandler.GroupMailboxPermissionVersion;
			this.mailboxSession.Mailbox.Save();
			this.mailboxSession.Mailbox.Load();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007A9C File Offset: 0x00005C9C
		private void SendWelcomeMessage()
		{
			try
			{
				GroupWarmingMessageComposer groupWarmingMessageComposer = new GroupWarmingMessageComposer(this.group, this.executingUser);
				using (MessageItem messageItem = MessageItem.Create(this.mailboxSession, this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
				{
					groupWarmingMessageComposer.WriteToMessage(messageItem);
					messageItem.Save(SaveMode.NoConflictResolution);
				}
			}
			catch (LocalizedException ex)
			{
				this.TraceAndReportWarning(Strings.WarningUnableToSendWelcomeMessage(ex.Message));
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007B20 File Offset: 0x00005D20
		private void MarkGroupConfigured()
		{
			if (this.group.IsGroupMailboxConfigured)
			{
				return;
			}
			this.group.IsGroupMailboxConfigured = true;
			this.adSession.Save(this.group);
			this.TraceDebug("Set group's IsGroupMailboxConfigured property to true. ExternalDirectoryObjectId={0}", new object[]
			{
				this.group.ExternalDirectoryObjectId
			});
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007B7C File Offset: 0x00005D7C
		private void ExecuteIf(Predicate<GroupMailboxConfigurationAction> condition, GroupMailboxConfigurationAction actionType, Action action)
		{
			if (condition(actionType))
			{
				using (new GroupMailboxConfigurationActionStopwatch(this.report, actionType))
				{
					action();
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007BC4 File Offset: 0x00005DC4
		private void TraceDebug(string message, params object[] args)
		{
			ConfigureGroupMailbox.Tracer.TraceDebug((long)this.GetHashCode(), message, args);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007BD9 File Offset: 0x00005DD9
		private void TraceAndReportWarning(LocalizedString message)
		{
			ConfigureGroupMailbox.Tracer.TraceWarning((long)this.GetHashCode(), message);
			this.report.Warnings.Add(message);
		}

		// Token: 0x0400004E RID: 78
		private const string DefaultTimeZone = "Pacific Standard Time";

		// Token: 0x0400004F RID: 79
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x04000050 RID: 80
		private readonly IRecipientSession adSession;

		// Token: 0x04000051 RID: 81
		private readonly ADUser group;

		// Token: 0x04000052 RID: 82
		private readonly ADUser executingUser;

		// Token: 0x04000053 RID: 83
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000054 RID: 84
		private readonly MailboxStoreTypeProvider mailboxStoreTypeProvider;

		// Token: 0x04000055 RID: 85
		private GroupMailboxConfigurationReport report;
	}
}
