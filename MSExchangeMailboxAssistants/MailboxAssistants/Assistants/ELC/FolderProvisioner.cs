using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200005F RID: 95
	internal sealed class FolderProvisioner
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00014F9C File Offset: 0x0001319C
		public FolderProvisioner(DatabaseInfo databaseInfo, ElcAuditLog elcAuditLog, ElcFolderSubAssistant assistant)
		{
			this.databaseInfo = databaseInfo;
			this.elcAuditLog = elcAuditLog;
			this.elcAssistant = assistant;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00014FB9 File Offset: 0x000131B9
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Folder provisioner for " + this.databaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00014FE4 File Offset: 0x000131E4
		public void OnWindowBegin()
		{
			FolderProvisioner.Tracer.TraceDebug<FolderProvisioner>((long)this.GetHashCode(), "{0}: OnWindowBegin called.", this);
			this.elcRootUrl = AdFolderReader.GetElcRootUrl();
			FolderProvisioner.TracerPfd.TracePfd<int, FolderProvisioner>((long)this.GetHashCode(), "PFD IWE {0} {1}: OnWindowBegin called ", 23831, this);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00015024 File Offset: 0x00013224
		public void OnWindowEnd()
		{
			FolderProvisioner.Tracer.TraceDebug<FolderProvisioner>((long)this.GetHashCode(), "{0}: OnWindowEnd called.", this);
			FolderProvisioner.Tracer.TraceDebug<int, FolderProvisioner>((long)this.GetHashCode(), "PFD IWE {0} {1}: OnWindowEnd called.", 23191, this);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00015484 File Offset: 0x00013684
		public void Invoke(ElcUserFolderInformation userInfo)
		{
			FolderProvisioner.<>c__DisplayClass4 CS$<>8__locals1 = new FolderProvisioner.<>c__DisplayClass4();
			CS$<>8__locals1.userInfo = userInfo;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.userInfo.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Folder provisioner invoked for this mailbox.", new object[]
			{
				TraceContext.Get()
			});
			CS$<>8__locals1.elcRootFolder = null;
			try
			{
				FolderProvisioner.<>c__DisplayClass6 CS$<>8__locals2 = new FolderProvisioner.<>c__DisplayClass6();
				CS$<>8__locals2.CS$<>8__locals5 = CS$<>8__locals1;
				FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Provisioning mailbox.", new object[]
				{
					TraceContext.Get()
				});
				CS$<>8__locals2.adFolderCount = ((CS$<>8__locals1.userInfo.UserAdFolders == null) ? 0 : CS$<>8__locals1.userInfo.UserAdFolders.Count);
				CS$<>8__locals2.mailboxFolderCount = ((CS$<>8__locals1.userInfo.MailboxFolders == null) ? 0 : CS$<>8__locals1.userInfo.MailboxFolders.Count);
				CS$<>8__locals2.mailboxSession = CS$<>8__locals1.userInfo.MailboxSession;
				FolderProvisioner.Tracer.TraceDebug<object, int, int>((long)this.GetHashCode(), "{0}: Has '{1}' AD folders and '{2}' mailbox folders.", TraceContext.Get(), CS$<>8__locals2.adFolderCount, CS$<>8__locals2.mailboxFolderCount);
				CS$<>8__locals1.elcRootFolder = this.ProvisionElcRootFolder(CS$<>8__locals1.userInfo, CS$<>8__locals2.adFolderCount > 0);
				ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<Invoke>b__0)), new FilterDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<Invoke>b__1)), new CatchDelegate(null, (UIntPtr)ldftn(<Invoke>b__2)));
				FolderProvisioner.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Provisioning mailbox completed.", 31383, TraceContext.Get());
				if (!this.CheckRootFolderRequired(CS$<>8__locals1.elcRootFolder, CS$<>8__locals1.userInfo) && CS$<>8__locals1.elcRootFolder != null)
				{
					using (Folder folder = Folder.Bind(CS$<>8__locals1.userInfo.MailboxSession, DefaultFolderType.Inbox, new PropertyDefinition[]
					{
						FolderSchema.ElcRootFolderEntryId
					}))
					{
						FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Deleting the ELC root entry Id prop on the Inbox.", new object[]
						{
							TraceContext.Get()
						});
						folder.DeleteProperties(new PropertyDefinition[]
						{
							FolderSchema.ElcRootFolderEntryId
						});
						folder.Save();
					}
					FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Deleting the ELC root props on the ELC root folder.", new object[]
					{
						TraceContext.Get()
					});
					CS$<>8__locals1.elcRootFolder.DeleteProperties(new PropertyDefinition[]
					{
						FolderSchema.AdminFolderFlags
					});
					try
					{
						FolderSaveResult folderSaveResult = CS$<>8__locals1.elcRootFolder.Save();
						if (folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to clear the ELC root folder.", new object[]
							{
								TraceContext.Get()
							});
						}
						else
						{
							FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ELC Root folder is now a normal folder.", new object[]
							{
								TraceContext.Get()
							});
						}
					}
					catch (ObjectNotFoundException)
					{
						FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to find the ELC root folder. The folder may have been deleted.", new object[]
						{
							TraceContext.Get()
						});
					}
					CS$<>8__locals1.userInfo.ElcRootId = null;
					CS$<>8__locals1.userInfo.ElcRootName = null;
				}
			}
			finally
			{
				if (CS$<>8__locals1.elcRootFolder != null)
				{
					CS$<>8__locals1.elcRootFolder.Dispose();
					CS$<>8__locals1.elcRootFolder = null;
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00015848 File Offset: 0x00013A48
		private void SyncElcRootFolder(Folder elcRootFolder, ElcUserFolderInformation userInfo)
		{
			try
			{
				FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Syncing the ELC root folder.", new object[]
				{
					TraceContext.Get()
				});
				FolderProvisioner.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Syncing the ELC root folder.", 17047, TraceContext.Get());
				bool flag = false;
				if (userInfo.ElcRootFolderData == null)
				{
					throw new ELCRootFailureException(Strings.descFailedToFindElcRoot(userInfo.MailboxSmtpAddress));
				}
				string elcRootHomePageUrl = userInfo.ElcRootHomePageUrl;
				string mailboxSmtpAddress = userInfo.MailboxSmtpAddress;
				if (string.IsNullOrEmpty(this.elcRootUrl))
				{
					if (!string.IsNullOrEmpty(elcRootHomePageUrl))
					{
						FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "'{0}': Need to delete the ELC Root Folder url - old url is '{1}'", TraceContext.Get(), elcRootHomePageUrl);
						elcRootFolder.DeleteProperties(new PropertyDefinition[]
						{
							FolderSchema.FolderHomePageUrl
						});
						elcRootFolder.ClassName = "IPF.Note";
						flag = true;
					}
				}
				else if (string.IsNullOrEmpty(elcRootHomePageUrl) || string.Compare(elcRootHomePageUrl, this.elcRootUrl, true) != 0)
				{
					FolderProvisioner.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "'{0}': Need to change the ELC Root Folder url - old url is '{1}', new folder url is '{2}'", TraceContext.Get(), elcRootHomePageUrl, this.elcRootUrl);
					ElcMailboxHelper.SetPropAndTrace(elcRootFolder, FolderSchema.FolderHomePageUrl, this.elcRootUrl);
					elcRootFolder.ClassName = FolderProvisioner.ElcRootFolderClass;
					flag = true;
				}
				if (!string.IsNullOrEmpty(userInfo.ElcRootFolderData.LocalizedName) && string.Compare(userInfo.ElcRootFolderData.LocalizedName, userInfo.ElcRootFolderData.Name, true) != 0)
				{
					FolderProvisioner.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "'{0}': Need to rename the ELC Root Folder - old name is '{1}', new name is '{2}'", TraceContext.Get(), userInfo.ElcRootFolderData.Name, userInfo.ElcRootFolderData.LocalizedName);
					PropertyError propertyError = null;
					try
					{
						propertyError = userInfo.MailboxSession.RenameDefaultFolder(DefaultFolderType.ElcRoot, userInfo.ElcRootFolderData.LocalizedName);
					}
					catch (ObjectExistedException)
					{
						FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ELC Root Folder rename failed because the folder name is in conflict.", new object[]
						{
							TraceContext.Get()
						});
						propertyError = new PropertyError(StoreObjectSchema.DisplayName, PropertyErrorCode.FolderNameConflict);
					}
					if (propertyError != null)
					{
						FolderProvisioner.Tracer.TraceError<object, PropertyError>((long)this.GetHashCode(), "'{0}': Unable to rename the ELC root folder to the localized name. Error is '{2}'", TraceContext.Get(), propertyError);
					}
					flag = true;
				}
				if (flag)
				{
					FolderSaveResult folderSaveResult = elcRootFolder.Save();
					if (folderSaveResult.OperationResult != OperationResult.Succeeded)
					{
						FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "{0}: Unable to set the ELC Root Folder home page. Will attempt this again on the next run.", new object[]
						{
							TraceContext.Get()
						});
					}
					elcRootFolder.Load();
				}
			}
			catch (ObjectNotFoundException innerException)
			{
				FolderProvisioner.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: ELC Root Folder '{1}' was deleted.", TraceContext.Get(), userInfo.ElcRootName);
				throw new ELCRootFailureException(Strings.descFailedToFindElcRoot(userInfo.MailboxSmtpAddress), innerException);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00015B94 File Offset: 0x00013D94
		private void SyncELCFoldersInMailbox(ElcUserFolderInformation userInfo, Folder elcRootFolder)
		{
			string mailboxSmtpAddress = userInfo.MailboxSmtpAddress;
			IExchangePrincipal mailboxOwner = userInfo.MailboxSession.MailboxOwner;
			FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Syncing ELC Folders in Mailbox.", new object[]
			{
				TraceContext.Get()
			});
			FolderProvisioner.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Syncing ELC Folders in Mailbox.", 25239, TraceContext.Get());
			using (List<MailboxFolderData>.Enumerator enumerator = userInfo.MailboxFolders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MailboxFolderData mailboxFolder = enumerator.Current;
					userInfo.SetCurrentFolder(mailboxFolder.Name, mailboxFolder.Id);
					this.elcAssistant.ThrottleStoreCallAndCheckForShutdown(mailboxOwner);
					int num;
					if (mailboxFolder.IsOrganizationalFolder())
					{
						StoreObjectId elcRootId = userInfo.ElcRootId;
						if (elcRootId != null && !mailboxFolder.ParentId.Equals(elcRootId))
						{
							this.ClearElcSettings(userInfo.MailboxSession, mailboxFolder);
							continue;
						}
						num = userInfo.UserAdFolders.FindIndex((AdFolderData adFolder) => mailboxFolder.ElcFolderGuid == adFolder.Folder.Guid);
						if (num == -1)
						{
							num = userInfo.UserAdFolders.FindIndex((AdFolderData adFolder) => string.Compare(mailboxFolder.Name, adFolder.Folder.GetLocalizedFolderName(userInfo.MailboxSession.MailboxOwner.PreferredCultures), true, CultureInfo.InvariantCulture) == 0);
							if (num != -1 && userInfo.UserAdFolders[num].Folder.FolderType != ElcFolderType.ManagedCustomFolder)
							{
								FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Clearing ELC settings on folder '{1}'.", TraceContext.Get(), mailboxFolder.Name);
								this.ClearElcSettings(userInfo.MailboxSession, mailboxFolder);
								continue;
							}
						}
						if (num == -1 || !userInfo.UserAdFolders[num].LinkedToTemplate)
						{
							this.SyncUnlinkedFolder(mailboxFolder, userInfo);
							continue;
						}
					}
					else
					{
						FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Syncing folder '{1}'.", TraceContext.Get(), mailboxFolder.Name);
						ElcFolderType? defaultFolderType = this.GetDefaultFolderType(userInfo.MailboxSession, mailboxFolder.Id.ObjectId);
						if (defaultFolderType != null)
						{
							num = userInfo.UserAdFolders.FindIndex((AdFolderData adFolder) => mailboxFolder.ElcFolderGuid == adFolder.Folder.Guid);
							if (num >= 0 && defaultFolderType != userInfo.UserAdFolders[num].Folder.FolderType)
							{
								FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: The folder type of the default folder in the mailbox and the AD don't match. Clearing the mailbox folder settings.'.", TraceContext.Get(), mailboxFolder.Name);
								num = -1;
							}
						}
						else
						{
							FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Folder type for folder '{1}' does not match supported default folders. Clearing the ELC settings on it.", TraceContext.Get(), mailboxFolder.Name);
							num = -1;
						}
						if (num == -1)
						{
							FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Default folder '{1}' is not part of the ELC mailbox policy for this user. Clearing ELC settings.", TraceContext.Get(), mailboxFolder.Name);
							this.ClearElcSettings(userInfo.MailboxSession, mailboxFolder);
							continue;
						}
					}
					AdFolderData adFolderData = userInfo.UserAdFolders[num];
					this.UpdateELCFolderInMailbox(userInfo.MailboxSession, mailboxFolder, true, adFolderData.Folder);
					userInfo.UserAdFolders[num].Synced = true;
					userInfo.TotalMailboxElcFolders++;
				}
			}
			string nonLocalizedAssistantName = (this.elcAssistant != null) ? this.elcAssistant.ElcAssistantType.ToString() : "<null>";
			foreach (AdFolderData adFolderData2 in userInfo.UserAdFolders)
			{
				userInfo.CurrentFolderName = adFolderData2.Folder.FolderName;
				if (!adFolderData2.Synced)
				{
					FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Need to create an additional ELC folder '{1}'.", TraceContext.Get(), adFolderData2.Folder.GetLocalizedFolderName(userInfo.MailboxSession.MailboxOwner.PreferredCultures));
					try
					{
						string folderFullPath = ProvisionedFolderCreator.CreateOneELCFolderInMailbox(adFolderData2, userInfo.MailboxSession, elcRootFolder);
						userInfo.TotalMailboxElcFolders++;
						if (this.elcAuditLog.FolderLoggingEnabled)
						{
							this.elcAuditLog.Append(new FolderAuditLogData(folderFullPath, userInfo.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), ELCAction.ManagedFolderProvisioning.ToString()), nonLocalizedAssistantName);
						}
					}
					catch (ELCDefaultFolderNotFoundException)
					{
						FolderProvisioner.Tracer.TraceError<object, ElcFolderType>((long)this.GetHashCode(), "{0}: Failed to provision default folder {1} in mailbox - folder not found.", TraceContext.Get(), adFolderData2.Folder.FolderType);
					}
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000161E0 File Offset: 0x000143E0
		private void SyncUnlinkedFolder(MailboxFolderData mailboxFolder, ElcUserFolderInformation userInfo)
		{
			FolderProvisioner.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Syncing Unlinked Folders in Mailbox.", 21143, TraceContext.Get());
			if (mailboxFolder.IsOrganizationalFolder() && (userInfo.ElcRootId == null || !mailboxFolder.ParentId.Equals(userInfo.ElcRootId)))
			{
				this.ClearElcSettings(userInfo.MailboxSession, mailboxFolder);
				return;
			}
			if (!mailboxFolder.IsOrganizationalFolder())
			{
				this.ClearElcSettings(userInfo.MailboxSession, mailboxFolder);
				return;
			}
			ELCFolder elcfolder = userInfo.AllAdFolders.Find((ELCFolder adFolderTemp) => adFolderTemp.Guid == mailboxFolder.ElcFolderGuid);
			if (elcfolder == null)
			{
				FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Looking up Orphaned Org Folder '{1}' by folder name.", TraceContext.Get(), mailboxFolder.Name);
				elcfolder = userInfo.AllAdFolders.Find((ELCFolder adFolderTemp) => adFolderTemp.FolderType == ElcFolderType.ManagedCustomFolder && string.Compare(mailboxFolder.Name, adFolderTemp.GetLocalizedFolderName(userInfo.MailboxSession.MailboxOwner.PreferredCultures), true, CultureInfo.InvariantCulture) == 0);
			}
			if (elcfolder == null)
			{
				this.ClearElcSettings(userInfo.MailboxSession, mailboxFolder);
				return;
			}
			this.UpdateELCFolderInMailbox(userInfo.MailboxSession, mailboxFolder, false, elcfolder);
			AdFolderData adFolderData = new AdFolderData();
			adFolderData.Folder = elcfolder;
			adFolderData.FolderSettings = AdFolderReader.FetchFolderContentSettings(elcfolder);
			adFolderData.LinkedToTemplate = false;
			adFolderData.Synced = true;
			if (userInfo.UserAdFolders == null)
			{
				userInfo.UserAdFolders = new List<AdFolderData>();
			}
			userInfo.UserAdFolders.Add(adFolderData);
			userInfo.TotalMailboxElcFolders++;
			FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "'{0}': Added folder '{1}' to list of AD folders.", TraceContext.Get(), elcfolder.GetLocalizedFolderName(userInfo.MailboxSession.MailboxOwner.PreferredCultures));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000163D4 File Offset: 0x000145D4
		private void UpdateELCFolderInMailbox(MailboxSession session, MailboxFolderData mailboxFolder, bool linkedToTemplate, ELCFolder elcFolder)
		{
			bool flag = false;
			string text = session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			UserConfiguration userConfiguration = null;
			IDictionary dictionary = null;
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			try
			{
				FolderProvisioner.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Updating ELC Folders in Mailbox.", 29335, TraceContext.Get());
				using (Folder folder = Folder.Bind(session, mailboxFolder.Id))
				{
					if (mailboxFolder.Id.ObjectId.Equals(session.GetDefaultFolderId(DefaultFolderType.Root)))
					{
						try
						{
							FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Attempting to retrieve config message for the all-other folder info.", new object[]
							{
								TraceContext.Get()
							});
							userConfiguration = session.UserConfigurationManager.GetFolderConfiguration("ELC", UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, defaultFolderId);
							dictionary = userConfiguration.GetDictionary();
						}
						catch (ObjectNotFoundException)
						{
							FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Config message for the all-other folder info is not present - creating it.", new object[]
							{
								TraceContext.Get()
							});
							ProvisionedFolderCreator.CreateAllOthersConfigMsg(elcFolder, session);
						}
						catch (CorruptDataException)
						{
							FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Config message for the all-other folder info is corrupt - deleting and recreating.", new object[]
							{
								TraceContext.Get()
							});
							session.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
							{
								"ELC"
							});
							ProvisionedFolderCreator.CreateAllOthersConfigMsg(elcFolder, session);
						}
					}
					this.CheckValidUpdate(session, mailboxFolder, elcFolder);
					bool flag2 = elcFolder.FolderType == ElcFolderType.ManagedCustomFolder;
					ulong num = elcFolder.StorageQuota.IsUnlimited ? 0UL : elcFolder.StorageQuota.Value.ToKB();
					ELCFolderFlags elcfolderFlags = flag2 ? ELCFolderFlags.Provisioned : ELCFolderFlags.None;
					if (flag2)
					{
						if (linkedToTemplate)
						{
							elcfolderFlags |= ELCFolderFlags.Protected;
						}
						if (num > 0UL)
						{
							if (ELCFolderFlags.Quota == (mailboxFolder.Flags & ELCFolderFlags.Quota) || ELCFolderFlags.TrackFolderSize == (mailboxFolder.Flags & ELCFolderFlags.TrackFolderSize))
							{
								elcfolderFlags |= ELCFolderFlags.TrackFolderSize;
								elcfolderFlags |= ELCFolderFlags.Quota;
							}
							else
							{
								if (folder.HasSubfolders || folder.ItemCount > 0)
								{
									FolderProvisioner.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: Quota cannot be set on folder '{1}' because it is not empty.", TraceContext.Get(), folder.DisplayName);
									throw new SkipException(Strings.descFolderNotEmpty(folder.DisplayName, text));
								}
								elcfolderFlags |= ELCFolderFlags.TrackFolderSize;
								elcfolderFlags |= ELCFolderFlags.Quota;
							}
						}
						else if ((mailboxFolder.Flags & ELCFolderFlags.Quota) == ELCFolderFlags.Quota)
						{
							elcfolderFlags |= ELCFolderFlags.TrackFolderSize;
						}
					}
					if (elcFolder.MustDisplayCommentEnabled)
					{
						elcfolderFlags |= ELCFolderFlags.MustDisplayComment;
					}
					if (mailboxFolder.Flags != elcfolderFlags)
					{
						ElcMailboxHelper.SetPropAndTrace(folder, FolderSchema.AdminFolderFlags, elcfolderFlags);
						if (userConfiguration != null)
						{
							dictionary["elcFlags"] = (int)elcfolderFlags;
						}
						flag = true;
					}
					string value = elcFolder.Guid.ToString();
					if (mailboxFolder.ElcFolderGuid != elcFolder.Guid)
					{
						ElcMailboxHelper.SetPropAndTrace(folder, FolderSchema.ELCPolicyIds, value);
						if (userConfiguration != null)
						{
							dictionary["policyId"] = value;
						}
						flag = true;
					}
					string localizedFolderName = elcFolder.GetLocalizedFolderName(session.MailboxOwner.PreferredCultures);
					if (flag2)
					{
						if (StringComparer.InvariantCultureIgnoreCase.Compare(mailboxFolder.Name, localizedFolderName) != 0)
						{
							folder.DisplayName = localizedFolderName;
							flag = true;
						}
						if (mailboxFolder.FolderQuota != (int)num)
						{
							if (num > 0UL)
							{
								ElcMailboxHelper.SetPropAndTrace(folder, FolderSchema.FolderQuota, (int)num);
								FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "'{0}': Quota for folder '{1}' changed from '{2}' to '{3}'.", new object[]
								{
									TraceContext.Get(),
									localizedFolderName,
									num,
									mailboxFolder.FolderQuota
								});
								flag = true;
							}
							else if (mailboxFolder.FolderQuota > 0)
							{
								folder.DeleteProperties(new PropertyDefinition[]
								{
									FolderSchema.FolderQuota
								});
								flag = true;
								FolderProvisioner.Tracer.TraceDebug<object, string, int>((long)this.GetHashCode(), "{0}: Removed folder quota for folder '{1}', original value = '{2}'.", TraceContext.Get(), localizedFolderName, mailboxFolder.FolderQuota);
							}
						}
					}
					string localizedFolderComment = elcFolder.GetLocalizedFolderComment(session.MailboxOwner.PreferredCultures);
					if (elcFolder.MustDisplayCommentEnabled && string.IsNullOrEmpty(localizedFolderComment))
					{
						FolderProvisioner.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: AD folder '{1}' is corrupt - the comment does not match the mustDisplayComment field.", TraceContext.Get(), elcFolder.Name);
						throw new SkipException(Strings.descInvalidCommentChange(localizedFolderName));
					}
					if (!string.IsNullOrEmpty(localizedFolderComment))
					{
						ElcMailboxHelper.SetPropAndTrace(folder, FolderSchema.ELCFolderComment, localizedFolderComment);
						if (userConfiguration != null)
						{
							dictionary["elcComment"] = localizedFolderComment;
						}
						flag = true;
					}
					else if (mailboxFolder.Comment != null)
					{
						ElcMailboxHelper.SetPropAndTrace(folder, FolderSchema.ELCFolderComment, Globals.BlankComment);
						if (userConfiguration != null)
						{
							dictionary["elcComment"] = null;
						}
						flag = true;
						FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Deleted folder comment.", new object[]
						{
							TraceContext.Get()
						});
					}
					if (flag)
					{
						ProvisionedFolderCreator.SaveELCFolder(folder, false);
						FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: ELC settings on folder '{1}' have been updated.", TraceContext.Get(), localizedFolderName);
						if (userConfiguration != null)
						{
							userConfiguration.Save();
						}
					}
				}
			}
			catch (ObjectNotFoundException innerException)
			{
				FolderProvisioner.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: Got a NotFound exception - folder '{1}' was deleted from underneath us.", TraceContext.Get(), mailboxFolder.Name);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToUpdateElcFolder, null, new object[]
				{
					mailboxFolder.Name,
					text
				});
				throw new ELCFolderSyncException(text, mailboxFolder.Name, innerException);
			}
			catch (SaveConflictException innerException2)
			{
				throw new ELCFolderSyncException(text, mailboxFolder.Name, innerException2);
			}
			catch (QuotaExceededException innerException3)
			{
				throw new ELCFolderSyncException(text, mailboxFolder.Name, innerException3);
			}
			finally
			{
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
					userConfiguration = null;
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000169F8 File Offset: 0x00014BF8
		private void CheckValidUpdate(MailboxSession session, MailboxFolderData mailboxFolder, ELCFolder adFolder)
		{
			bool flag = adFolder.FolderType == ElcFolderType.ManagedCustomFolder;
			bool flag2 = ELCFolderFlags.Provisioned == (mailboxFolder.Flags & ELCFolderFlags.Provisioned);
			string localizedFolderName = adFolder.GetLocalizedFolderName(session.MailboxOwner.PreferredCultures);
			if (flag2 == flag)
			{
				if (!flag2)
				{
					ElcFolderType? defaultFolderType = this.GetDefaultFolderType(session, mailboxFolder.Id.ObjectId);
					if (defaultFolderType != null && defaultFolderType.Value != adFolder.FolderType)
					{
						FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "{0}: Default Policy folder type changed for folder '{1}', mailbox folderType = {2}, AD folderType = {3}.", new object[]
						{
							TraceContext.Get(),
							mailboxFolder.Name,
							defaultFolderType,
							adFolder.FolderType
						});
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidELCFolderChange, null, new object[]
						{
							adFolder.Name
						});
						throw new SkipException(Strings.descInvalidFolderUpdateDefTypeChange(localizedFolderName, (int)adFolder.FolderType, (int)defaultFolderType.Value, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
					}
				}
				return;
			}
			FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "{0}: Invalid folder update. ELC Folder '{1}' changed from being a '{2}' to a '{3}'", new object[]
			{
				TraceContext.Get(),
				adFolder.Id,
				flag ? "Default folder" : "Organizational",
				flag ? "Organizational" : "Default folder"
			});
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidELCFolderChange, null, new object[]
			{
				localizedFolderName
			});
			if (flag)
			{
				throw new SkipException(Strings.descInvalidFolderUpdateOrgToDef(localizedFolderName, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			throw new SkipException(Strings.descInvalidFolderUpdateDefToOrg(localizedFolderName, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00016BE8 File Offset: 0x00014DE8
		private ElcFolderType? GetDefaultFolderType(MailboxSession session, StoreObjectId folderId)
		{
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Calendar)))
			{
				return new ElcFolderType?(ElcFolderType.Calendar);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Contacts)))
			{
				return new ElcFolderType?(ElcFolderType.Contacts);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.DeletedItems)))
			{
				return new ElcFolderType?(ElcFolderType.DeletedItems);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Drafts)))
			{
				return new ElcFolderType?(ElcFolderType.Drafts);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Inbox)))
			{
				return new ElcFolderType?(ElcFolderType.Inbox);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Journal)))
			{
				return new ElcFolderType?(ElcFolderType.Journal);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.JunkEmail)))
			{
				return new ElcFolderType?(ElcFolderType.JunkEmail);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Notes)))
			{
				return new ElcFolderType?(ElcFolderType.Notes);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Outbox)))
			{
				return new ElcFolderType?(ElcFolderType.Outbox);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.SentItems)))
			{
				return new ElcFolderType?(ElcFolderType.SentItems);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Tasks)))
			{
				return new ElcFolderType?(ElcFolderType.Tasks);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.Root)))
			{
				return new ElcFolderType?(ElcFolderType.All);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.RssSubscription)))
			{
				return new ElcFolderType?(ElcFolderType.RssSubscriptions);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.SyncIssues)))
			{
				return new ElcFolderType?(ElcFolderType.SyncIssues);
			}
			if (folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.CommunicatorHistory)))
			{
				return new ElcFolderType?(ElcFolderType.ConversationHistory);
			}
			return null;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00016D58 File Offset: 0x00014F58
		private void ClearElcSettings(MailboxSession session, MailboxFolderData mailboxFolder)
		{
			try
			{
				FolderProvisioner.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Remove ELC Setting of folder.", 19735, TraceContext.Get());
				FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Clearing ELC settings on folder '{1}'.", TraceContext.Get(), mailboxFolder.Name);
				using (Folder folder = Folder.Bind(session, mailboxFolder.Id))
				{
					StoreObjectId objectId = folder.Id.ObjectId;
					folder.DeleteProperties(new PropertyDefinition[]
					{
						FolderSchema.AdminFolderFlags,
						FolderSchema.ELCPolicyIds,
						FolderSchema.ELCFolderComment,
						FolderSchema.FolderQuota
					});
					ProvisionedFolderCreator.SaveELCFolder(folder, false);
					if (!mailboxFolder.IsOrganizationalFolder() && objectId.Equals(session.GetDefaultFolderId(DefaultFolderType.Root)))
					{
						FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Deleting the all-other folders config message.", new object[]
						{
							TraceContext.Get()
						});
						StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
						session.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
						{
							"ELC"
						});
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Folder was not found in the mailbox - no ELC settings need to be cleared.", new object[]
				{
					TraceContext.Get()
				});
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00016ECC File Offset: 0x000150CC
		private bool CheckRootFolderRequired(Folder elcRootFolder, ElcUserFolderInformation userInfo)
		{
			if (elcRootFolder == null)
			{
				return false;
			}
			if ((userInfo.UserAdFolders == null || userInfo.UserAdFolders.Count == 0) && userInfo.TotalMailboxElcFolders == 0)
			{
				FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Checking to see if we can make the ELC Root Folder '{1}' normal.", TraceContext.Get(), userInfo.ElcRootName);
				bool flag = AdFolderReader.ElcMailboxPoliciesExist(userInfo.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId);
				string str = flag ? "ELC Mailbox Policies exist in the Org." : "ELC Mailbox Policies do not exist in the Org.";
				FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: " + str, new object[]
				{
					TraceContext.Get()
				});
				bool flag2 = false;
				foreach (ELCFolder elcfolder in userInfo.AllAdFolders)
				{
					if (elcfolder.FolderType == ElcFolderType.ManagedCustomFolder)
					{
						flag2 = true;
						break;
					}
				}
				string str2 = flag2 ? "Organizational Folders exist." : "Organizational Folders do not exist.";
				FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: " + str2, new object[]
				{
					TraceContext.Get()
				});
				if ((!flag2 && !flag) || !elcRootFolder.HasSubfolders)
				{
					FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: The ELC root folder '{1}' can be removed.", TraceContext.Get(), userInfo.ElcRootName);
					return false;
				}
			}
			FolderProvisioner.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: The ELC root folder '{1}' cannot be removed.", TraceContext.Get(), userInfo.ElcRootName);
			return true;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00017060 File Offset: 0x00015260
		private void UpgradeElcRootFolder(MailboxSession mailboxSession, Folder elcRootFolder, MailboxFolderData elcRootData)
		{
			FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ELC Root folder needs to be fixed so that XSO recognizes it as a default folder.", new object[]
			{
				TraceContext.Get()
			});
			string text = Strings.descElcRootFolderName;
			StoreObjectId objectId = elcRootFolder.Id.ObjectId;
			if (!elcRootData.Name.StartsWith(text, true, CultureInfo.InvariantCulture))
			{
				FolderProvisioner.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: ELC Root folder name needs to be changed from '{1}' to '{2}'.", TraceContext.Get(), elcRootData.Name, text);
				elcRootFolder.DisplayName = text;
				for (int i = 1; i < 10; i++)
				{
					bool flag = false;
					FolderSaveResult folderSaveResult = elcRootFolder.Save();
					if (folderSaveResult.OperationResult == OperationResult.Succeeded)
					{
						elcRootFolder.Load();
						break;
					}
					for (int j = 0; j < folderSaveResult.PropertyErrors.Length; j++)
					{
						FolderProvisioner.Tracer.TraceDebug<object, PropertyErrorCode, PropertyDefinition>((long)this.GetHashCode(), "{0}: ELC Root Folder save resulted in the error '{1}' for property '{2}'.", TraceContext.Get(), folderSaveResult.PropertyErrors[j].PropertyErrorCode, folderSaveResult.PropertyErrors[j].PropertyDefinition);
						if (folderSaveResult.PropertyErrors[j].PropertyDefinition == StoreObjectSchema.DisplayName && folderSaveResult.PropertyErrors[j].PropertyErrorCode == PropertyErrorCode.FolderNameConflict)
						{
							FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ELC Root Folder save failed because the folder name is in conflict.", new object[]
							{
								TraceContext.Get()
							});
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "{0}: Unable to save the ELC Root Folder - folder name is in conflict.", new object[]
						{
							TraceContext.Get()
						});
						throw new ELCRootFailureException(Strings.descFailedToCreateELCRoot(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()), null);
					}
					elcRootFolder.Load();
					elcRootFolder.DisplayName = Strings.descElcRootFolderName + i;
				}
			}
			using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.Inbox, new PropertyDefinition[]
			{
				FolderSchema.ElcRootFolderEntryId
			}))
			{
				ElcMailboxHelper.SetPropAndTrace(folder, FolderSchema.ElcRootFolderEntryId, objectId.ProviderLevelItemId);
				FolderSaveResult folderSaveResult2 = folder.Save();
				if (folderSaveResult2.OperationResult != OperationResult.Succeeded)
				{
					FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Failed to set the ELC root entryId on the Inbox folder.", new object[]
					{
						TraceContext.Get()
					});
				}
			}
			using (Folder folder2 = Folder.Bind(mailboxSession, DefaultFolderType.Configuration, new PropertyDefinition[]
			{
				FolderSchema.ElcRootFolderEntryId
			}))
			{
				ElcMailboxHelper.SetPropAndTrace(folder2, FolderSchema.ElcRootFolderEntryId, objectId.ProviderLevelItemId);
				FolderSaveResult folderSaveResult3 = folder2.Save();
				if (folderSaveResult3.OperationResult != OperationResult.Succeeded)
				{
					FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Failed to set the ELC root entryId on the Inbox folder.", new object[]
					{
						TraceContext.Get()
					});
				}
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00017354 File Offset: 0x00015554
		private Folder ProvisionElcRootFolder(ElcUserFolderInformation userInfo, bool needsElcRootFolder)
		{
			Folder folder = null;
			bool flag = true;
			StoreObjectId elcRootId = userInfo.ElcRootId;
			string mailboxSmtpAddress = userInfo.MailboxSmtpAddress;
			Folder result;
			try
			{
				if (elcRootId != null)
				{
					folder = Folder.Bind(userInfo.MailboxSession, elcRootId, new PropertyDefinition[]
					{
						FolderSchema.FolderHomePageUrl
					});
					this.SyncElcRootFolder(folder, userInfo);
					flag = false;
				}
				else
				{
					MailboxFolderData elcRootFolderData = userInfo.ElcRootFolderData;
					if (elcRootFolderData != null)
					{
						MailboxSession mailboxSession = userInfo.MailboxSession;
						folder = Folder.Bind(mailboxSession, elcRootFolderData.Id, new PropertyDefinition[]
						{
							FolderSchema.FolderHomePageUrl
						});
						this.UpgradeElcRootFolder(mailboxSession, folder, elcRootFolderData);
						this.SyncElcRootFolder(folder, userInfo);
						flag = false;
					}
					else if (needsElcRootFolder && AdFolderReader.HasOrganizationalFolders(userInfo.UserAdFolders))
					{
						FolderProvisioner.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ELC Root folder needs to be created.", new object[]
						{
							TraceContext.Get()
						});
						try
						{
							folder = ProvisionedFolderCreator.CreateELCRootFolder(userInfo.MailboxSession, this.elcRootUrl);
							if (folder == null)
							{
								throw new ELCRootFailureException(Strings.descFailedToCreateElcRootRetry(userInfo.MailboxSmtpAddress), null);
							}
							flag = false;
						}
						catch (DefaultFolderNameClashException innerException)
						{
							FolderProvisioner.Tracer.TraceError((long)this.GetHashCode(), "'{0}': Unable to create the ELC root folder in the mailbox because there are folders with the same name in the user's mailbox.", new object[]
							{
								TraceContext.Get()
							});
							Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCRootNameClash, null, new object[]
							{
								FolderProvisioner.ELCRootName,
								mailboxSmtpAddress
							});
							throw new SkipException(innerException);
						}
					}
				}
				result = folder;
			}
			finally
			{
				if (flag && folder != null)
				{
					folder.Dispose();
					folder = null;
				}
			}
			return result;
		}

		// Token: 0x040002C6 RID: 710
		private static readonly string ElcRootFolderClass = "IPF.Note.OutlookHomepage";

		// Token: 0x040002C7 RID: 711
		private static readonly string ELCRootName = "Managed Folders";

		// Token: 0x040002C8 RID: 712
		private static readonly Trace Tracer = ExTraceGlobals.FolderProvisionerTracer;

		// Token: 0x040002C9 RID: 713
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040002CA RID: 714
		private string elcRootUrl;

		// Token: 0x040002CB RID: 715
		private DatabaseInfo databaseInfo;

		// Token: 0x040002CC RID: 716
		private ElcAuditLog elcAuditLog;

		// Token: 0x040002CD RID: 717
		private ElcFolderSubAssistant elcAssistant;

		// Token: 0x040002CE RID: 718
		private string toString;
	}
}
