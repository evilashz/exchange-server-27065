using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxAssistants.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.SharingPolicyAssistant
{
	// Token: 0x0200015F RID: 351
	internal sealed class SharingPolicyAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase, IDemandJobNotification
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x00055928 File Offset: 0x00053B28
		public SharingPolicyAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00055933 File Offset: 0x00053B33
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00055935 File Offset: 0x00053B35
		public void OnBeforeDemandJob(Guid mailboxGuid, Guid databaseGuid)
		{
			SharingPolicyCache.PurgeCache();
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0005593C File Offset: 0x00053B3C
		protected override void OnShutdownInternal()
		{
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00055940 File Offset: 0x00053B40
		private bool IsMailboxNeedingPolicyUpdate(MailboxSession mailboxSession, SharingPolicyCache sharingPolicyCache)
		{
			byte[] valueOrDefault = mailboxSession.Mailbox.GetValueOrDefault<byte[]>(MailboxSchema.LastSharingPolicyAppliedId, null);
			if (valueOrDefault == null)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox does not have LastSharingPolicyAppliedId present and is ready to have policy applied.", mailboxSession.MailboxGuid);
				return true;
			}
			if (valueOrDefault.Length != SharingPolicyAssistant.GuidBytesLength)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox does have LastSharingPolicyAppliedId set, but its value is corrupted. Mailbox ready to have policy applied.", mailboxSession.MailboxGuid);
				return true;
			}
			byte[] valueOrDefault2 = mailboxSession.Mailbox.GetValueOrDefault<byte[]>(MailboxSchema.LastSharingPolicyAppliedHash, null);
			if (valueOrDefault2 == null)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox does not have LastSharingPolicyAppliedHash present yet. Mailbox ready to have policy applied.", mailboxSession.MailboxGuid);
				return true;
			}
			if (!ArrayComparer<byte>.Comparer.Equals(sharingPolicyCache.Hash, valueOrDefault2))
			{
				SharingPolicyAssistant.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox has different sharing policy applied than the one in the AD. Mailbox ready to have policy applied.", mailboxSession.MailboxGuid);
				return true;
			}
			SharingPolicyAssistant.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "{0}: mailbox has same sharing policy applied as the one in the AD. No need to apply policy to this mailbox.", mailboxSession.MailboxGuid);
			return false;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00055A2C File Offset: 0x00053C2C
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			ADUser aduser = SharingPolicyAssistant.GetADUser(mailboxSession.MailboxOwner, mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			if (aduser == null)
			{
				return;
			}
			if (ADRecipient.IsSystemMailbox(aduser.RecipientTypeDetails))
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal, RecipientTypeDetails>((long)this.GetHashCode(), "{0}: Skipping the mailbox processing as it is a system mailbox. RecipientTypeDetails {1}.", mailboxSession.MailboxOwner, aduser.RecipientTypeDetails);
				return;
			}
			SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Begin process mailbox", mailboxSession.MailboxOwner);
			SharingPolicyCache sharingPolicyCache = SharingPolicyCache.Get(aduser);
			bool flag = false;
			if (sharingPolicyCache != null)
			{
				if (!this.IsMailboxNeedingPolicyUpdate(mailboxSession, sharingPolicyCache))
				{
					return;
				}
				flag = this.ApplyPolicy(mailboxSession, sharingPolicyCache.Policy);
			}
			if (flag)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Storing applied policy to mailbox table.", mailboxSession.MailboxOwner);
				SharingPolicyAssistant.UpdateMailboxData(mailboxSession.Mailbox, (aduser.SharingPolicy == null && !sharingPolicyCache.BelongsToDehydratedContainer) ? SharingPolicyCache.DynamicDefaultPolicy.ObjectGuid.ToByteArray() : sharingPolicyCache.Policy.Id.ObjectGuid.ToByteArray(), sharingPolicyCache.Hash);
			}
			else
			{
				SharingPolicyAssistant.Tracer.TraceWarning<IExchangePrincipal>(0L, "{0}: Unable to find sharing policy for this mailbox.", mailboxSession.MailboxOwner);
				ExDateTime? exDateTime = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.LastSharingPolicyAppliedTime) as ExDateTime?;
				byte[] array = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.LastSharingPolicyAppliedHash) as byte[];
				if (exDateTime == null || array != null)
				{
					SharingPolicyAssistant.UpdateMailboxData(mailboxSession.Mailbox, null, null);
				}
				else
				{
					TimeSpan retryTimeSpan = ExDateTime.UtcNow.Subtract(exDateTime.Value);
					if (ExDateTime.UtcNow.Subtract(exDateTime.Value) > SharingPolicyAssistant.RetryThresholdLimit)
					{
						SharingPolicyAssistant.SubmitInformationalWatson(mailboxSession.MailboxOwner, retryTimeSpan);
						SharingPolicyAssistant.UpdateMailboxData(mailboxSession.Mailbox, null, null);
					}
				}
			}
			SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: End process mailbox", mailboxSession.MailboxOwner);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00055C1C File Offset: 0x00053E1C
		private static void UpdateMailboxData(Mailbox mailBox, byte[] policyId, byte[] hashValue)
		{
			if (policyId == null)
			{
				mailBox.Delete(MailboxSchema.LastSharingPolicyAppliedId);
			}
			else
			{
				mailBox[MailboxSchema.LastSharingPolicyAppliedId] = policyId;
			}
			if (hashValue == null)
			{
				mailBox.Delete(MailboxSchema.LastSharingPolicyAppliedHash);
			}
			else
			{
				mailBox[MailboxSchema.LastSharingPolicyAppliedHash] = hashValue;
			}
			mailBox[MailboxSchema.LastSharingPolicyAppliedTime] = ExDateTime.UtcNow;
			mailBox.Save();
			mailBox.Load();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00055C84 File Offset: 0x00053E84
		private static ADUser GetADUser(IExchangePrincipal exchangePrincipal, IRecipientSession recipientSession)
		{
			ADRecipient adrecipient = DirectoryHelper.ReadADRecipient(exchangePrincipal.MailboxInfo.MailboxGuid, exchangePrincipal.MailboxInfo.IsArchive, recipientSession);
			if (adrecipient == null)
			{
				SharingPolicyAssistant.Tracer.TraceError<IExchangePrincipal>(0L, "{0}: Unable to find ADRecipient object for this mailbox", exchangePrincipal);
				return null;
			}
			ADUser aduser = adrecipient as ADUser;
			if (aduser == null)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>(0L, "{0}: Ignoring this mailbox because it is not an ADUser", exchangePrincipal);
			}
			return aduser;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00055CE4 File Offset: 0x00053EE4
		private static void SubmitInformationalWatson(IExchangePrincipal exchangePrincipal, TimeSpan retryTimeSpan)
		{
			try
			{
				string message = string.Format("Sharing Policy Assistant could not process mailbox for {0} even after:{1} hours. Threshold:{2} hours", exchangePrincipal.MailboxInfo.PrimarySmtpAddress, retryTimeSpan.TotalHours, SharingPolicyAssistant.RetryThresholdLimit.TotalHours);
				throw new InvalidOperationException(message);
			}
			catch (InvalidOperationException exception)
			{
				ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps | ReportOptions.DeepStackTraceHash | ReportOptions.DoNotFreezeThreads, string.Empty);
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00055DB0 File Offset: 0x00053FB0
		private bool ApplyPolicy(MailboxSession mailboxSession, SharingPolicy policy)
		{
			using (MailboxData mailboxData = new MailboxData(mailboxSession, policy))
			{
				if (mailboxData.MaxAnonymousDetailLevel == 0)
				{
					SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "Anonymous access is disabled. Clearing AD SharingAnonymousIdentities entries for user:{0}", mailboxData.MailboxSession.MailboxOwner);
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						ADUser aduser = mailboxData.RecipientSession.Read(mailboxData.MailboxSession.MailboxOwner.ObjectId) as ADUser;
						aduser.SharingAnonymousIdentities.Clear();
						mailboxData.RecipientSession.Save(aduser);
					}, 3);
					if (!adoperationResult.Succeeded)
					{
						SharingPolicyAssistant.Tracer.TraceError<IExchangePrincipal, Exception>((long)this.GetHashCode(), "Failed to clear AD SharingAnonymousIdentities entries for user:{0},  Error: {1}", mailboxData.MailboxSession.MailboxOwner, adoperationResult.Exception);
						return false;
					}
				}
				using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.Configuration))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, SharingPolicyAssistant.FolderQueryProperties))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(64);
							if (rows == null || rows.Length == 0)
							{
								break;
							}
							foreach (object[] array2 in rows)
							{
								string arg = array2[1] as string;
								StoreId storeId = array2[0] as StoreId;
								if (storeId == null)
								{
									SharingPolicyAssistant.Tracer.TraceError<IExchangePrincipal, object, string>((long)this.GetHashCode(), "{0}: cannot process folder row due unknown id: {1} - {2}", mailboxSession.MailboxOwner, array2[0], arg);
								}
								else
								{
									using (FolderData folderData = new FolderData(mailboxSession, storeId))
									{
										this.ApplyPolicyForExternalSharing(array2, mailboxData, folderData);
										this.ApplyPolicyForAnonymousSharing(array2, mailboxData, folderData);
										if (folderData.IsChanged)
										{
											folderData.Folder.Save();
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00055FE4 File Offset: 0x000541E4
		private void ApplyPolicyForExternalSharing(object[] row, MailboxData mailboxData, FolderData folderData)
		{
			RawSecurityDescriptor rawSecurityDescriptor = row[2] as RawSecurityDescriptor;
			if (rawSecurityDescriptor == null)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal, StoreId>((long)this.GetHashCode(), "{0}: Got null security descriptor property from folder query for folder {1}", mailboxData.MailboxSession.MailboxOwner, folderData.Id);
				this.ApplyPolicyToFolder(mailboxData.SharingPolicy, folderData);
				return;
			}
			SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal, StoreId, RawSecurityDescriptor>((long)this.GetHashCode(), "{0}: Security descriptor retrieved from folder {1}: {2}", mailboxData.MailboxSession.MailboxOwner, folderData.Id, rawSecurityDescriptor);
			if (SharingPolicyAssistant.HasExternalUser(mailboxData.ExternalUserCollection, rawSecurityDescriptor))
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Security descriptor contains external users, need to apply policy to folder", mailboxData.MailboxSession.MailboxOwner);
				this.ApplyPolicyToFolder(mailboxData.SharingPolicy, folderData);
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00056098 File Offset: 0x00054298
		private static bool HasExternalUser(ExternalUserCollection externalUserCollection, RawSecurityDescriptor securityDescriptor)
		{
			if (securityDescriptor.DiscretionaryAcl != null)
			{
				foreach (GenericAce genericAce in securityDescriptor.DiscretionaryAcl)
				{
					if (genericAce.AceType == AceType.AccessAllowed || genericAce.AceType == AceType.AccessDenied)
					{
						CommonAce commonAce = genericAce as CommonAce;
						if (commonAce != null)
						{
							ExternalUser externalUser = externalUserCollection.FindExternalUser(commonAce.SecurityIdentifier);
							if (externalUser != null)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00056108 File Offset: 0x00054308
		private void ApplyPolicyToFolder(SharingPolicy policy, FolderData folderData)
		{
			StoreObjectType objectType = ObjectClass.GetObjectType(folderData.Folder.ClassName);
			List<PermissionSecurityPrincipal> list = new List<PermissionSecurityPrincipal>();
			PermissionSet permissionSet = folderData.Folder.GetPermissionSet();
			foreach (Permission permission in permissionSet)
			{
				if (permission.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal)
				{
					SharingPolicyAction sharingPolicyAction = (SharingPolicyAction)0;
					if (policy != null)
					{
						sharingPolicyAction = (permission.Principal.ExternalUser.IsReachUser ? policy.GetAllowedForAnonymousCalendarSharing() : policy.GetAllowed(permission.Principal.ExternalUser.SmtpAddress.Domain));
					}
					MemberRights memberRights = MemberRights.None;
					if (sharingPolicyAction != (SharingPolicyAction)0)
					{
						memberRights = PolicyAllowedMemberRights.GetAllowed(sharingPolicyAction, objectType);
					}
					if (memberRights == MemberRights.None)
					{
						list.Add(permission.Principal);
					}
					else
					{
						MemberRights memberRights2 = ~memberRights & permission.MemberRights;
						if (memberRights2 != MemberRights.None)
						{
							if (objectType == StoreObjectType.CalendarFolder)
							{
								if ((permission.MemberRights & MemberRights.ReadAny) != MemberRights.None)
								{
									permission.MemberRights |= MemberRights.FreeBusyDetailed;
								}
								if ((permission.MemberRights & MemberRights.FreeBusyDetailed) != MemberRights.None)
								{
									permission.MemberRights |= MemberRights.FreeBusySimple;
								}
							}
							permission.MemberRights = (memberRights & permission.MemberRights);
							folderData.IsChanged = true;
						}
					}
				}
			}
			if (list.Count > 0)
			{
				foreach (PermissionSecurityPrincipal securityPrincipal in list)
				{
					permissionSet.RemoveEntry(securityPrincipal);
				}
				folderData.IsChanged = true;
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000562A4 File Offset: 0x000544A4
		private void ApplyPolicyForAnonymousSharing(object[] row, MailboxData mailboxData, FolderData folderData)
		{
			object obj = row[3];
			if (obj == null || obj is PropertyError)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal, StoreId, object>((long)this.GetHashCode(), "{0}: Got null ExtendedFolderFlags property from folder query for folder {1}: {2}", mailboxData.MailboxSession.MailboxOwner, folderData.Id, obj ?? "<null>");
				return;
			}
			if (((ExtendedFolderFlags)obj & ExtendedFolderFlags.ExchangePublishedCalendar) != (ExtendedFolderFlags)0)
			{
				SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal, object>((long)this.GetHashCode(), "{0}: This is a published folder, need to apply policy to folder. ExtendedFolderFlags = {1}", mailboxData.MailboxSession.MailboxOwner, obj);
				this.ApplyPolicyToFolder(mailboxData.MaxAnonymousDetailLevel, folderData);
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00056330 File Offset: 0x00054530
		private void ApplyPolicyToFolder(int maxAllowedDetailLevel, FolderData folderData)
		{
			SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal, StoreId, int>((long)this.GetHashCode(), "{0}: Apply policy to a published folder {1}. MaxAllowedDetailLevel = {2}", folderData.MailboxSession.MailboxOwner, folderData.Id, maxAllowedDetailLevel);
			UserConfiguration publishingConfiguration = this.GetPublishingConfiguration(folderData.MailboxSession, folderData.Id);
			if (publishingConfiguration == null)
			{
				return;
			}
			using (publishingConfiguration)
			{
				IDictionary dictionary = null;
				Exception ex = null;
				try
				{
					dictionary = publishingConfiguration.GetDictionary();
				}
				catch (CorruptDataException ex2)
				{
					ex = ex2;
				}
				catch (InvalidOperationException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					SharingPolicyAssistant.Tracer.TraceError<IExchangePrincipal, Exception>((long)this.GetHashCode(), "{0}: Data of user configuration is invalid or corrupt. Exception = {1}", folderData.MailboxSession.MailboxOwner, ex);
				}
				else if (maxAllowedDetailLevel == 0)
				{
					if ((bool)dictionary[MailboxCalendarFolderSchema.PublishEnabled.Name])
					{
						SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Anonymous sharing is not allowed at all. Trying to disable.", folderData.MailboxSession.MailboxOwner);
						dictionary[MailboxCalendarFolderSchema.PublishEnabled.Name] = false;
						dictionary[MailboxCalendarFolderSchema.PublishedCalendarUrl.Name] = null;
						dictionary[MailboxCalendarFolderSchema.PublishedICalUrl.Name] = null;
						publishingConfiguration.Save();
						ExtendedFolderFlags? valueAsNullable = folderData.Folder.GetValueAsNullable<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags);
						if (valueAsNullable != null)
						{
							ExtendedFolderFlags extendedFolderFlags = valueAsNullable.Value & (ExtendedFolderFlags)2147483647;
							if (valueAsNullable.Value != extendedFolderFlags)
							{
								folderData.Folder[FolderSchema.ExtendedFolderFlags] = extendedFolderFlags;
								folderData.IsChanged = true;
							}
						}
					}
				}
				else
				{
					object obj = dictionary[MailboxCalendarFolderSchema.DetailLevel.Name];
					if (obj != null && (int)obj > maxAllowedDetailLevel)
					{
						SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Anonymous sharing is not allowed with current detail level. Trying to reduce.", folderData.MailboxSession.MailboxOwner);
						dictionary[MailboxCalendarFolderSchema.DetailLevel.Name] = maxAllowedDetailLevel;
						publishingConfiguration.Save();
					}
				}
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00056550 File Offset: 0x00054750
		private UserConfiguration GetPublishingConfiguration(MailboxSession mailboxSession, StoreId folderId)
		{
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = UserConfigurationHelper.GetPublishingConfiguration(mailboxSession, folderId, false);
				if (userConfiguration == null)
				{
					SharingPolicyAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: User configuration doesn't exist.", mailboxSession.MailboxOwner);
				}
			}
			catch (CorruptDataException arg)
			{
				SharingPolicyAssistant.Tracer.TraceError<IExchangePrincipal, CorruptDataException>((long)this.GetHashCode(), "{0}: User configuration is corrupt. Exception = {1}", mailboxSession.MailboxOwner, arg);
			}
			return userConfiguration;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00056627 File Offset: 0x00054827
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0005662F File Offset: 0x0005482F
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00056637 File Offset: 0x00054837
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x0400092F RID: 2351
		private const int QueryFolderRowsBatch = 64;

		// Token: 0x04000930 RID: 2352
		private static readonly int GuidBytesLength = Guid.Empty.ToByteArray().Length;

		// Token: 0x04000931 RID: 2353
		private static readonly PropertyDefinition[] FolderQueryProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName,
			FolderSchema.SecurityDescriptor,
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x04000932 RID: 2354
		private static readonly TimeSpan RetryThresholdLimit = TimeSpan.FromDays(2.0);

		// Token: 0x04000933 RID: 2355
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x02000160 RID: 352
		private enum FolderQueryPropertiesIndex
		{
			// Token: 0x04000935 RID: 2357
			Id,
			// Token: 0x04000936 RID: 2358
			DisplayName,
			// Token: 0x04000937 RID: 2359
			SecurityDescriptor,
			// Token: 0x04000938 RID: 2360
			ExtendedFolderFlags
		}
	}
}
