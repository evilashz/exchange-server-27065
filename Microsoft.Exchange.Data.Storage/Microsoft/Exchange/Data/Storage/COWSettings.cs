using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200061D RID: 1565
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class COWSettings
	{
		// Token: 0x06004077 RID: 16503 RVA: 0x0010E57C File Offset: 0x0010C77C
		static COWSettings()
		{
			COWSettings.SetGlobalDefaults();
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x0010E609 File Offset: 0x0010C809
		public COWSettings(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			this.session = session;
			this.currentFolderId = null;
			this.currentFolder = null;
			this.temporaryDisableHold = false;
			this.temporaryDisableCalendarLogging = false;
			this.BuildUpdateStampMetadata();
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x0010E645 File Offset: 0x0010C845
		public static TimeSpan COWCacheLifeTime
		{
			get
			{
				return COWSettings.cowInfoCache.COWCacheLifeTime;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x0010E651 File Offset: 0x0010C851
		public MailboxSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x0010E659 File Offset: 0x0010C859
		// (set) Token: 0x0600407C RID: 16508 RVA: 0x0010E660 File Offset: 0x0010C860
		internal static bool DumpsterEnabledOverwrite
		{
			get
			{
				return COWSettings.dumpsterEnabled;
			}
			set
			{
				COWSettings.dumpsterEnabled = value;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x0010E668 File Offset: 0x0010C868
		// (set) Token: 0x0600407E RID: 16510 RVA: 0x0010E66F File Offset: 0x0010C86F
		internal static bool DumpsterNonIPMOverwrite
		{
			get
			{
				return COWSettings.dumpsterNonIPM;
			}
			set
			{
				COWSettings.dumpsterNonIPM = value;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x0010E677 File Offset: 0x0010C877
		// (set) Token: 0x06004080 RID: 16512 RVA: 0x0010E67E File Offset: 0x0010C87E
		internal static bool CalendarLoggingEnabledOverwrite
		{
			get
			{
				return COWSettings.calendarLoggingEnabled;
			}
			set
			{
				COWSettings.calendarLoggingEnabled = value;
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x0010E686 File Offset: 0x0010C886
		internal static bool IsImapPoisonMessage(bool onBeforeNotification, COWTriggerAction operation, StoreSession session, CoreItem item)
		{
			return onBeforeNotification && operation == COWTriggerAction.Update && session.ClientInfoString.Equals("Client=IMAP4") && item != null && item.PropertyBag.GetValueOrDefault<bool>(InternalSchema.MimeConversionFailed);
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x0010E6B6 File Offset: 0x0010C8B6
		// (set) Token: 0x06004083 RID: 16515 RVA: 0x0010E6C0 File Offset: 0x0010C8C0
		public bool TemporaryDisableHold
		{
			get
			{
				return this.temporaryDisableHold;
			}
			set
			{
				if (this.session.LogonType == LogonType.Admin || this.session.LogonType == LogonType.Transport)
				{
					ExTraceGlobals.SessionTracer.TraceError<bool>((long)this.session.GetHashCode(), "Setting TemporaryDisableHold to {0}", value);
					this.temporaryDisableHold = value;
				}
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x0010E70C File Offset: 0x0010C90C
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x0010E714 File Offset: 0x0010C914
		public bool TemporaryDisableCalendarLogging
		{
			get
			{
				return this.temporaryDisableCalendarLogging;
			}
			set
			{
				if (this.session.LogonType == LogonType.Admin || this.session.LogonType == LogonType.Transport)
				{
					ExTraceGlobals.SessionTracer.TraceError<bool>((long)this.session.GetHashCode(), "Setting TemporaryDisableCalendarLogging to {0}", value);
					this.temporaryDisableCalendarLogging = value;
				}
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x0010E760 File Offset: 0x0010C960
		public Unlimited<ByteQuantifiedSize> DumpsterWarningQuota
		{
			get
			{
				return this.GetMailboxInfo().DumpsterWarningQuota;
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x0010E76D File Offset: 0x0010C96D
		public Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
		{
			get
			{
				return this.GetMailboxInfo().CalendarLoggingQuota;
			}
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0010E77A File Offset: 0x0010C97A
		public static bool IsPermissibleInferenceAction(MailboxSession session, CoreItem item)
		{
			return session != null && item != null && ((session.LogonType == LogonType.Admin || session.LogonType == LogonType.SystemService) && session.ClientInfoString.Contains("InferenceTrainingAssistant")) && !item.IsLegallyDirty;
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0010E7B3 File Offset: 0x0010C9B3
		public static bool IsMrmAction(MailboxSession session)
		{
			return session != null && (session.LogonType == LogonType.Admin || session.LogonType == LogonType.SystemService) && session.ClientInfoString.Contains("ELCAssistant");
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0010E7DE File Offset: 0x0010C9DE
		public static bool IsCalendarRepairAssistantAction(StoreSession session)
		{
			return session != null && (session.LogonType == LogonType.Admin || session.LogonType == LogonType.SystemService) && session.ClientInfoString.Contains("CalendarRepairAssistant");
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0010E814 File Offset: 0x0010CA14
		internal static void SetGlobalDefaults()
		{
			COWSettings.dumpsterEnabled = true;
			COWSettings.dumpsterNonIPM = false;
			COWSettings.calendarLoggingEnabled = true;
			bool flag = true;
			int capacity = 100;
			int expireTimeInMinutes = 1;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\RecoverableItems", RegistryKeyPermissionCheck.ReadSubTree))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("NonIPM");
					if (value != null && value is int && (int)value == 1)
					{
						COWSettings.dumpsterNonIPM = true;
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings using the registry dumpster nonIPM override value!");
					}
					else
					{
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings failed opening the registry dumpster nonIPM override value!");
					}
					value = registryKey.GetValue("AdminMailboxSessionCacheEnabled");
					if (value != null && value is int)
					{
						flag = ((int)value != 0);
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings using the registry admin mailbox session cache enabled!");
					}
					else
					{
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings failed opening the registry admin mailbox session cache enabled override value!");
					}
					value = registryKey.GetValue("AdminMailboxSessionCacheExpireTime");
					if (value != null && value is int)
					{
						expireTimeInMinutes = (int)value;
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings using the registry admin mailbox session cache expire time override value!");
					}
					else
					{
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings failed opening the registry admin mailbox session cache expire time override value!");
					}
					value = registryKey.GetValue("AdminMailboxSessionCacheSize");
					if (value != null && value is int)
					{
						capacity = (int)value;
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings using the registry admin mailbox session cache size override value!");
					}
					else
					{
						ExTraceGlobals.SessionTracer.TraceDebug(0L, "COWSettings failed opening the registry admin mailbox session cache size override value!");
					}
				}
			}
			if (flag)
			{
				COWSettings.adminMailboxSessionCache = new MruDictionaryListCache<Guid, MailboxSession>(capacity, expireTimeInMinutes, (MailboxSession session) => session.MailboxGuid);
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x0010E9CC File Offset: 0x0010CBCC
		internal static void PurgeCache()
		{
			COWSettings.cowInfoCache.PurgeCache();
			if (COWSettings.adminMailboxSessionCache != null)
			{
				COWSettings.adminMailboxSessionCache.Dispose();
				COWSettings.adminMailboxSessionCache = null;
			}
			COWSettings.SetGlobalDefaults();
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x0010E9F4 File Offset: 0x0010CBF4
		internal static MailboxSession GetAdminMailboxSession(MailboxSession session)
		{
			MailboxSession mailboxSession = null;
			if (COWSettings.adminMailboxSessionCache != null && COWSettings.adminMailboxSessionCache.TryGetAndRemoveValue(session.MailboxGuid, out mailboxSession))
			{
				if (!mailboxSession.IsConnected)
				{
					mailboxSession.ConnectWithStatus();
				}
			}
			else
			{
				mailboxSession = MailboxSession.OpenAsAdmin(session.MailboxOwner, session.InternalCulture, session.ClientInfoString + ";COW=Delegate");
			}
			return mailboxSession;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0010EA52 File Offset: 0x0010CC52
		internal static void ReturnAdminMailboxSession(MailboxSession session)
		{
			if (COWSettings.adminMailboxSessionCache != null)
			{
				COWSettings.adminMailboxSessionCache.Add(session.MailboxGuid, session);
				return;
			}
			session.Dispose();
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0010EA74 File Offset: 0x0010CC74
		internal static void AddMetadata(COWSettings settings, ICoreItem item, COWTriggerAction action)
		{
			Dictionary<PropertyDefinition, object> actionMetadata = settings.GetSessionMetadata();
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in actionMetadata)
			{
				if (keyValuePair.Key == InternalSchema.ClientInfoString)
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null && !string.IsNullOrEmpty(currentActivityScope.ClientInfo) && !string.IsNullOrEmpty(currentActivityScope.Component) && string.Equals(currentActivityScope.Component, "ActiveSync"))
					{
						item.PropertyBag[keyValuePair.Key] = currentActivityScope.ClientInfo;
						continue;
					}
				}
				item.PropertyBag[keyValuePair.Key] = keyValuePair.Value;
			}
			actionMetadata = COWSettings.GetActionMetadata(item, action);
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair2 in actionMetadata)
			{
				item.PropertyBag[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0010EB94 File Offset: 0x0010CD94
		internal static Dictionary<PropertyDefinition, object> GetActionMetadata(ICoreItem item, COWTriggerAction action)
		{
			return new Dictionary<PropertyDefinition, object>(2)
			{
				{
					InternalSchema.CalendarLogTriggerAction,
					action.ToString()
				},
				{
					InternalSchema.OriginalLastModifiedTime,
					ExDateTime.Now
				}
			};
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x0010EBD4 File Offset: 0x0010CDD4
		// (set) Token: 0x06004092 RID: 16530 RVA: 0x0010EBDC File Offset: 0x0010CDDC
		internal StoreObjectId CurrentFolderId
		{
			get
			{
				return this.currentFolderId;
			}
			set
			{
				if ((value == null && this.currentFolder != null) || (value != null && this.currentFolder != null && !this.currentFolderId.Equals(value)))
				{
					this.ResetCurrentFolder();
				}
				this.currentFolderId = value;
			}
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x0010EC10 File Offset: 0x0010CE10
		public static bool HoldEnabled(MailboxSession mailboxSession)
		{
			COWSettings.COWMailboxInfo cowmailboxInfo = COWSettings.cowInfoCache.GetCOWMailboxInfo(mailboxSession);
			return cowmailboxInfo.SingleItemRecovery || cowmailboxInfo.LitigationHold || cowmailboxInfo.InPlaceHoldEnabled;
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0010EC44 File Offset: 0x0010CE44
		public bool LegalHoldEnabled()
		{
			return this.LitigationHoldEnabled() || this.InPlaceHoldEnabled();
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0010EC56 File Offset: 0x0010CE56
		public bool HoldEnabled()
		{
			return !this.TemporaryDisableHold && (this.GetMailboxInfo().SingleItemRecovery || this.GetMailboxInfo().LitigationHold || this.GetMailboxInfo().InPlaceHoldEnabled);
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x0010EC89 File Offset: 0x0010CE89
		public bool IsOnlyInPlaceHoldEnabled()
		{
			return !this.LitigationHoldEnabled() && !this.SIREnabled() && this.InPlaceHoldEnabled();
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0010ECA3 File Offset: 0x0010CEA3
		public bool IsCalendarLoggingEnabled()
		{
			return COWSettings.calendarLoggingEnabled && !this.TemporaryDisableCalendarLogging && this.GetMailboxInfo().CalendarLogging;
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0010ECC1 File Offset: 0x0010CEC1
		public bool IsSiteMailboxMessageDedupEnabled()
		{
			return this.GetMailboxInfo().SiteMailboxMessageDedup;
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x0010ECCE File Offset: 0x0010CECE
		public bool IsMrmAction()
		{
			return COWSettings.IsMrmAction(this.session);
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x0010ECDB File Offset: 0x0010CEDB
		public bool IsPermissibleInferenceAction(CoreItem item)
		{
			return COWSettings.IsPermissibleInferenceAction(this.session, item);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0010ECE9 File Offset: 0x0010CEE9
		private bool SIREnabled()
		{
			return !this.TemporaryDisableHold && this.GetMailboxInfo().SingleItemRecovery;
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0010ED00 File Offset: 0x0010CF00
		private bool InPlaceHoldEnabled()
		{
			return !this.TemporaryDisableHold && this.GetMailboxInfo().InPlaceHoldEnabled;
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x0010ED17 File Offset: 0x0010CF17
		private bool LitigationHoldEnabled()
		{
			return !this.TemporaryDisableHold && this.GetMailboxInfo().LitigationHold;
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0010ED2E File Offset: 0x0010CF2E
		public ulong? GetCalendarLoggingFolderSize(MailboxSession session)
		{
			return COWSettings.cowInfoCache.GetCalendarLoggingFolderSize(session);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0010ED3B File Offset: 0x0010CF3B
		internal Dictionary<PropertyDefinition, object> GetSessionMetadata()
		{
			return this.sessionMetadata;
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0010ED60 File Offset: 0x0010CF60
		internal bool IsCurrentFolderExcludedFromAuditing(MailboxSession sessionWithBestAccess)
		{
			if (this.currentFolderId == null)
			{
				return false;
			}
			if (this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Inbox)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Outbox)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Calendar)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Contacts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.SentItems)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.DeletedItems)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Contacts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Drafts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Tasks)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Notes)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Journal)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Root)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsVersions)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsPurges)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.CalendarLogging)))
			{
				return false;
			}
			StoreObjectId auditsFolderId = null;
			sessionWithBestAccess.BypassAuditsFolderAccessChecking(delegate
			{
				auditsFolderId = sessionWithBestAccess.GetAuditsFolderId();
			});
			if (this.currentFolderId.Equals(auditsFolderId))
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)sessionWithBestAccess.GetHashCode(), "COWSettings::IsCurrentFolderExcludedFromAuditing returns true since the current folder is audit folder.");
				return true;
			}
			if (this.currentFolderId.ObjectType == StoreObjectType.SearchFolder || this.currentFolderId.ObjectType == StoreObjectType.OutlookSearchFolder)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<StoreObjectType>((long)this.session.GetHashCode(), "COWSettings::IsCurrentFolderExcludedFromAuditing returns true since the current folder id indicates a search folder of type {0}.", this.currentFolderId.ObjectType);
				return true;
			}
			try
			{
				if (this.GetCurrentFolder(sessionWithBestAccess) == null)
				{
					return false;
				}
			}
			catch (TooManyObjectsOpenedException)
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.session.GetHashCode(), "COWSettings::IsCurrentFolderExcludedFromAuditing returns true since there are too many objects opened in this session.  Auditing will fail anyway.");
				return true;
			}
			if (this.currentFolder.Id.ObjectId.ObjectType == StoreObjectType.SearchFolder || this.currentFolder.Id.ObjectId.ObjectType == StoreObjectType.OutlookSearchFolder)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<StoreObjectType>((long)this.session.GetHashCode(), "COWSettings::IsCurrentFolderExcludedFromAuditing returns true since the current folder is search folder of type {0}.", this.currentFolder.Id.ObjectId.ObjectType);
				return true;
			}
			StoreFolderFlags valueOrDefault = (StoreFolderFlags)this.currentFolder.GetValueOrDefault<int>(FolderSchema.FolderFlags, 1);
			bool flag = (valueOrDefault & StoreFolderFlags.FolderIPM) == StoreFolderFlags.FolderIPM;
			if (!flag)
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.session.GetHashCode(), "COWSettings::IsCurrentFolderExcludedFromAuditing returns true since the current folder is non-IPM.");
			}
			return !flag;
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0010F0D8 File Offset: 0x0010D2D8
		private static string GetVersionString(long versionNumber)
		{
			ushort num = (ushort)(versionNumber >> 48);
			ushort num2 = (ushort)((versionNumber & 281470681743360L) >> 32);
			ushort num3 = (ushort)((versionNumber & (long)((ulong)-65536)) >> 16);
			ushort num4 = (ushort)(versionNumber & 65535L);
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", new object[]
			{
				num,
				num2,
				num3,
				num4
			});
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x0010F154 File Offset: 0x0010D354
		private void BuildUpdateStampMetadata()
		{
			this.sessionMetadata = new Dictionary<PropertyDefinition, object>(11);
			this.sessionMetadata.Add(InternalSchema.ClientProcessName, this.session.ClientProcessName ?? string.Empty);
			this.sessionMetadata.Add(InternalSchema.ClientMachineName, this.session.ClientMachineName ?? string.Empty);
			this.sessionMetadata.Add(InternalSchema.ClientBuildVersion, COWSettings.GetVersionString(this.session.ClientVersion));
			this.sessionMetadata.Add(InternalSchema.MiddleTierProcessName, COWSettings.middleTierProcessName);
			this.sessionMetadata.Add(InternalSchema.MiddleTierServerName, COWSettings.middleTierServerName);
			this.sessionMetadata.Add(InternalSchema.MiddleTierServerBuildVersion, COWSettings.middleTierServerBuildVersion);
			this.sessionMetadata.Add(InternalSchema.ClientInfoString, this.session.ClientInfoString ?? string.Empty);
			this.sessionMetadata.Add(InternalSchema.ResponsibleUserName, this.session.UserLegacyDN);
			MailboxSession mailboxSession = this.session;
			if (mailboxSession != null && mailboxSession.MailboxOwner != null && !mailboxSession.IsRemote)
			{
				this.sessionMetadata.Add(InternalSchema.MailboxServerName, mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn);
				ServerVersion serverVersion = new ServerVersion(mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion);
				this.sessionMetadata.Add(InternalSchema.MailboxServerBuildVersion, serverVersion.ToString());
				this.sessionMetadata.Add(InternalSchema.MailboxDatabaseName, mailboxSession.MailboxOwner.MailboxInfo.Location.DatabaseLegacyDn);
				return;
			}
			this.sessionMetadata.Add(InternalSchema.MailboxServerName, string.Empty);
			this.sessionMetadata.Add(InternalSchema.MailboxServerBuildVersion, string.Empty);
			this.sessionMetadata.Add(InternalSchema.MailboxDatabaseName, string.Empty);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0010F330 File Offset: 0x0010D530
		internal Folder GetCurrentFolder(MailboxSession sessionWithBestAccess)
		{
			if (this.currentFolder == null || this.currentFolder.IsDisposed)
			{
				if (this.currentFolderId != null)
				{
					try
					{
						this.currentFolder = Folder.Bind(sessionWithBestAccess, this.currentFolderId, COWSettings.folderLoadProperties);
						goto IL_61;
					}
					catch (ObjectNotFoundException)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<StoreObjectId>((long)this.session.GetHashCode(), "Folder Id {0} was not found", this.currentFolderId);
						goto IL_61;
					}
				}
				this.currentFolder = null;
			}
			IL_61:
			return this.currentFolder;
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0010F3B4 File Offset: 0x0010D5B4
		internal void ResetCurrentFolder()
		{
			if (this.currentFolder != null)
			{
				this.currentFolder.Dispose();
				this.currentFolder = null;
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0010F3D0 File Offset: 0x0010D5D0
		internal bool IsCurrentFolderEnabled(MailboxSession sessionWithBestAccess)
		{
			return this.InternalIsCurrentFolderEnabled(sessionWithBestAccess, null);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0010F3DA File Offset: 0x0010D5DA
		internal bool IsCurrentFolderItemEnabled(MailboxSession sessionWithBestAccess, ICoreItem item)
		{
			return this.InternalIsCurrentFolderEnabled(sessionWithBestAccess, item);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0010F3E4 File Offset: 0x0010D5E4
		private bool InternalIsCurrentFolderEnabled(MailboxSession sessionWithBestAccess, ICoreItem item)
		{
			if (this.currentFolderId == null)
			{
				return true;
			}
			if (this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Inbox)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Outbox)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Calendar)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Contacts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.SentItems)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.DeletedItems)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Contacts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Drafts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Tasks)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Notes)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Journal)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Root)))
			{
				return true;
			}
			if (this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsVersions)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsPurges)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDiscoveryHolds)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.CalendarLogging)))
			{
				return true;
			}
			if (this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Configuration)))
			{
				return COWSettings.dumpsterNonIPM;
			}
			if (this.currentFolderId.ObjectType == StoreObjectType.SearchFolder || this.currentFolderId.ObjectType == StoreObjectType.OutlookSearchFolder)
			{
				return this.IsParentFolderEnabled(sessionWithBestAccess, item);
			}
			if (this.GetCurrentFolder(sessionWithBestAccess) == null)
			{
				return false;
			}
			if (this.currentFolderId.ObjectType == StoreObjectType.Unknown && (this.currentFolder.Id.ObjectId.ObjectType == StoreObjectType.SearchFolder || this.currentFolder.Id.ObjectId.ObjectType == StoreObjectType.OutlookSearchFolder))
			{
				return this.IsParentFolderEnabled(sessionWithBestAccess, item);
			}
			StoreFolderFlags valueOrDefault = (StoreFolderFlags)this.currentFolder.GetValueOrDefault<int>(FolderSchema.FolderFlags, 1);
			return COWSettings.dumpsterNonIPM || (valueOrDefault & StoreFolderFlags.FolderIPM) == StoreFolderFlags.FolderIPM;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0010F634 File Offset: 0x0010D834
		private bool IsParentFolderEnabled(MailboxSession sessionWithBestAccess, ICoreItem item)
		{
			if (item != null)
			{
				StoreObjectId folderId;
				StoreObjectId storeObjectId;
				this.GetRealParentFolderForItem(item, out folderId, out storeObjectId);
				return this.IsFolderEnabled(sessionWithBestAccess, folderId);
			}
			return false;
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0010F65C File Offset: 0x0010D85C
		private bool IsFolderEnabled(MailboxSession sessionWithBestAccess, StoreObjectId folderId)
		{
			if (this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Inbox)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Outbox)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Calendar)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Contacts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.SentItems)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.DeletedItems)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Contacts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Drafts)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Tasks)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Notes)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Journal)) || this.currentFolderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.Root)))
			{
				return true;
			}
			if (folderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot)) || folderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsVersions)) || folderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions)) || folderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsPurges)) || folderId.Equals(sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.CalendarLogging)))
			{
				return true;
			}
			try
			{
				using (Folder.Bind(sessionWithBestAccess, folderId, COWSettings.folderLoadProperties))
				{
					StoreFolderFlags valueOrDefault = (StoreFolderFlags)this.currentFolder.GetValueOrDefault<int>(FolderSchema.FolderFlags, 1);
					return COWSettings.dumpsterNonIPM || (valueOrDefault & StoreFolderFlags.FolderIPM) == StoreFolderFlags.FolderIPM;
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<StoreObjectId>((long)sessionWithBestAccess.GetHashCode(), "Folder Id {0} was not found", folderId);
			}
			return false;
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0010F838 File Offset: 0x0010DA38
		internal void GetRealParentFolderForItem(ICoreItem item, out StoreObjectId realParentId, out StoreObjectId realItemId)
		{
			realItemId = item.StoreObjectId;
			realParentId = IdConverter.GetParentIdFromMessageId(item.StoreObjectId);
			long valueOrDefault = item.PropertyBag.GetValueOrDefault<long>(ItemSchema.Fid, 0L);
			if (valueOrDefault != 0L)
			{
				long midFromMessageId = this.session.IdConverter.GetMidFromMessageId(item.Id.ObjectId);
				realParentId = this.session.IdConverter.CreateFolderId(valueOrDefault);
				realItemId = this.session.IdConverter.CreateMessageId(valueOrDefault, midFromMessageId);
				ExTraceGlobals.SessionTracer.TraceDebug<string, string, string>((long)this.session.GetHashCode(), "GroupItemsPerParent: Bind id {0}, real id {1}, real parent id {2}", item.Id.ObjectId.ToHexEntryId(), realItemId.ToHexEntryId(), realParentId.ToHexEntryId());
			}
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0010F8EE File Offset: 0x0010DAEE
		internal COWSettings.COWMailboxInfo GetMailboxInfo()
		{
			if (this.mailboxInfo == null)
			{
				this.mailboxInfo = new COWSettings.COWMailboxInfo?(COWSettings.cowInfoCache.GetCOWMailboxInfo(this.session));
			}
			return this.mailboxInfo.Value;
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0010F923 File Offset: 0x0010DB23
		internal void ResetMailboxInfo()
		{
			this.mailboxInfo = null;
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x0010F931 File Offset: 0x0010DB31
		internal void RemoveMailboxInfoCache(Guid mailboxGuid)
		{
			COWSettings.cowInfoCache.RemoveCacheEntryForMailbox(mailboxGuid);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x0010F93E File Offset: 0x0010DB3E
		internal static COWSettings CreateDummyInstance()
		{
			return new COWSettings();
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x0010F945 File Offset: 0x0010DB45
		private COWSettings()
		{
		}

		// Token: 0x040023A0 RID: 9120
		internal const string RegKeyStringDumpster = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\RecoverableItems";

		// Token: 0x040023A1 RID: 9121
		internal const string RegValueStringNonIPM = "NonIPM";

		// Token: 0x040023A2 RID: 9122
		internal const string RegValueStringAdminMailboxSessionCacheEnabled = "AdminMailboxSessionCacheEnabled";

		// Token: 0x040023A3 RID: 9123
		internal const string RegValueStringAdminMailboxSessionCacheExpireTime = "AdminMailboxSessionCacheExpireTime";

		// Token: 0x040023A4 RID: 9124
		internal const string RegValueStringAdminMailboxSessionCacheSize = "AdminMailboxSessionCacheSize";

		// Token: 0x040023A5 RID: 9125
		internal const string MrmClientString = "ELCAssistant";

		// Token: 0x040023A6 RID: 9126
		internal const string HoldErrorComponentNameForCrimsonChannel = "Hold.HoldErrors.Monitor";

		// Token: 0x040023A7 RID: 9127
		internal const string InferenceClientString = "InferenceTrainingAssistant";

		// Token: 0x040023A8 RID: 9128
		internal const string CalendarRepairClientString = "CalendarRepairAssistant";

		// Token: 0x040023A9 RID: 9129
		internal static readonly char StoreIdSeparator = '￾';

		// Token: 0x040023AA RID: 9130
		private static COWSettings.COWInfoCache cowInfoCache = new COWSettings.COWInfoCache();

		// Token: 0x040023AB RID: 9131
		private static MruDictionaryListCache<Guid, MailboxSession> adminMailboxSessionCache;

		// Token: 0x040023AC RID: 9132
		private static string middleTierProcessName = Process.GetCurrentProcess().ProcessName;

		// Token: 0x040023AD RID: 9133
		private static string middleTierServerName = Util.GetMachineName();

		// Token: 0x040023AE RID: 9134
		private static string middleTierServerBuildVersion = StorageGlobals.BuildVersionString;

		// Token: 0x040023AF RID: 9135
		private static ICollection<PropertyDefinition> folderLoadProperties = new PropertyDefinition[]
		{
			FolderSchema.FolderFlags,
			FolderSchema.FolderPathName
		};

		// Token: 0x040023B0 RID: 9136
		private static ICollection<PropertyDefinition> requiredItemsProperties = new PropertyDefinition[]
		{
			InternalSchema.ItemVersion
		};

		// Token: 0x040023B1 RID: 9137
		private static bool dumpsterEnabled = true;

		// Token: 0x040023B2 RID: 9138
		private static bool dumpsterNonIPM = false;

		// Token: 0x040023B3 RID: 9139
		private static bool calendarLoggingEnabled = true;

		// Token: 0x040023B4 RID: 9140
		private MailboxSession session;

		// Token: 0x040023B5 RID: 9141
		private StoreObjectId currentFolderId;

		// Token: 0x040023B6 RID: 9142
		private Folder currentFolder;

		// Token: 0x040023B7 RID: 9143
		private bool temporaryDisableHold;

		// Token: 0x040023B8 RID: 9144
		private bool temporaryDisableCalendarLogging;

		// Token: 0x040023B9 RID: 9145
		private Dictionary<PropertyDefinition, object> sessionMetadata;

		// Token: 0x040023BA RID: 9146
		private COWSettings.COWMailboxInfo? mailboxInfo;

		// Token: 0x0200061E RID: 1566
		internal interface IExpirableCacheEntry
		{
			// Token: 0x17001334 RID: 4916
			// (get) Token: 0x060040B1 RID: 16561
			// (set) Token: 0x060040B2 RID: 16562
			ExDateTime InfoExpireTime { get; set; }
		}

		// Token: 0x0200061F RID: 1567
		internal struct COWMailboxInfo : COWSettings.IExpirableCacheEntry
		{
			// Token: 0x17001335 RID: 4917
			// (get) Token: 0x060040B3 RID: 16563 RVA: 0x0010F94D File Offset: 0x0010DB4D
			// (set) Token: 0x060040B4 RID: 16564 RVA: 0x0010F955 File Offset: 0x0010DB55
			public ExDateTime InfoExpireTime { get; set; }

			// Token: 0x040023BC RID: 9148
			public bool LitigationHold;

			// Token: 0x040023BD RID: 9149
			public bool CalendarLogging;

			// Token: 0x040023BE RID: 9150
			public bool SiteMailboxMessageDedup;

			// Token: 0x040023BF RID: 9151
			public bool SingleItemRecovery;

			// Token: 0x040023C0 RID: 9152
			public Unlimited<ByteQuantifiedSize> DumpsterWarningQuota;

			// Token: 0x040023C1 RID: 9153
			public Unlimited<ByteQuantifiedSize> CalendarLoggingQuota;

			// Token: 0x040023C2 RID: 9154
			public bool InPlaceHoldEnabled;
		}

		// Token: 0x02000620 RID: 1568
		internal struct COWDatabaseInfo : COWSettings.IExpirableCacheEntry
		{
			// Token: 0x17001336 RID: 4918
			// (get) Token: 0x060040B5 RID: 16565 RVA: 0x0010F95E File Offset: 0x0010DB5E
			// (set) Token: 0x060040B6 RID: 16566 RVA: 0x0010F966 File Offset: 0x0010DB66
			public ExDateTime InfoExpireTime { get; set; }

			// Token: 0x040023C4 RID: 9156
			public Unlimited<ByteQuantifiedSize> DumpsterWarningQuota;

			// Token: 0x040023C5 RID: 9157
			public Unlimited<ByteQuantifiedSize> CalendarLoggingQuota;

			// Token: 0x040023C6 RID: 9158
			public Unlimited<ByteQuantifiedSize> DumpsterQuota;
		}

		// Token: 0x02000621 RID: 1569
		internal struct COWTenantInfo : COWSettings.IExpirableCacheEntry
		{
			// Token: 0x17001337 RID: 4919
			// (get) Token: 0x060040B7 RID: 16567 RVA: 0x0010F96F File Offset: 0x0010DB6F
			// (set) Token: 0x060040B8 RID: 16568 RVA: 0x0010F977 File Offset: 0x0010DB77
			public ExDateTime InfoExpireTime { get; set; }

			// Token: 0x040023C8 RID: 9160
			public bool CalendarVersionStoreEnabled;
		}

		// Token: 0x02000622 RID: 1570
		internal struct CalendarLoggingFolderInfo
		{
			// Token: 0x040023CA RID: 9162
			public ulong Size;

			// Token: 0x040023CB RID: 9163
			public uint AccessCount;
		}

		// Token: 0x02000623 RID: 1571
		internal class COWInfoCache
		{
			// Token: 0x060040B9 RID: 16569 RVA: 0x0010F980 File Offset: 0x0010DB80
			public COWInfoCache() : this(TimeSpan.FromHours(1.0))
			{
			}

			// Token: 0x060040BA RID: 16570 RVA: 0x0010F998 File Offset: 0x0010DB98
			public COWInfoCache(TimeSpan cacheLifeTime)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "COWInfoCache constructor with life time {0}", cacheLifeTime);
				this.cowCacheLifeTime = cacheLifeTime;
				this.cowCacheAccessLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
				this.cowMailboxInfoCache = new Dictionary<Guid, COWSettings.COWMailboxInfo>();
				this.cowDatabaseInfoCache = new Dictionary<ADObjectId, COWSettings.COWDatabaseInfo>();
				this.cowTenantInfoCache = new Dictionary<OrganizationId, COWSettings.COWTenantInfo>();
				this.calendarLoggingFolderInfoCache = new Dictionary<Guid, COWSettings.CalendarLoggingFolderInfo>();
				this.ComputeNextCleanupCacheTime();
			}

			// Token: 0x17001338 RID: 4920
			// (get) Token: 0x060040BB RID: 16571 RVA: 0x0010FA4E File Offset: 0x0010DC4E
			public TimeSpan COWCacheLifeTime
			{
				get
				{
					return this.cowCacheLifeTime;
				}
			}

			// Token: 0x060040BC RID: 16572 RVA: 0x0010FA56 File Offset: 0x0010DC56
			public bool LegalHoldEnabled(StoreSession session)
			{
				return this.GetCOWMailboxInfo(session).LitigationHold || this.GetCOWMailboxInfo(session).InPlaceHoldEnabled;
			}

			// Token: 0x060040BD RID: 16573 RVA: 0x0010FA74 File Offset: 0x0010DC74
			public bool CalendarLoggingEnabled(StoreSession session)
			{
				return this.GetCOWMailboxInfo(session).CalendarLogging;
			}

			// Token: 0x060040BE RID: 16574 RVA: 0x0010FA82 File Offset: 0x0010DC82
			public bool SingleItemRecoveryEnabled(StoreSession session)
			{
				return this.GetCOWMailboxInfo(session).SingleItemRecovery;
			}

			// Token: 0x060040BF RID: 16575 RVA: 0x0010FA90 File Offset: 0x0010DC90
			internal void RemoveCacheEntryForMailbox(Guid mailboxGuid)
			{
				this.cowMailboxInfoCache.Remove(mailboxGuid);
			}

			// Token: 0x060040C0 RID: 16576 RVA: 0x0010FA9F File Offset: 0x0010DC9F
			internal Dictionary<Guid, COWSettings.COWMailboxInfo> GetCache()
			{
				return this.cowMailboxInfoCache;
			}

			// Token: 0x060040C1 RID: 16577 RVA: 0x0010FAA8 File Offset: 0x0010DCA8
			private void TimedOperation(bool writer, COWSettings.COWInfoCache.LockedMethod m)
			{
				ExDateTime now = ExDateTime.Now;
				ExDateTime now2 = ExDateTime.Now;
				try
				{
					now2 = ExDateTime.Now;
					bool flag;
					if (writer)
					{
						flag = this.cowCacheAccessLock.TryEnterWriteLock(this.timeoutLimit);
					}
					else
					{
						flag = this.cowCacheAccessLock.TryEnterReadLock(this.timeoutLimit);
					}
					if (!flag)
					{
						this.timeoutEventsTotal++;
						ExTraceGlobals.SessionTracer.TraceError<double, int>(0L, "COWInfoCache lock wait more than {0} seconds, {1} times.", this.timeoutLimit.TotalSeconds, this.timeoutEventsTotal);
						throw new DumpsterOperationException(ServerStrings.COWMailboxInfoCacheTimeout(this.timeoutLimit.TotalSeconds, this.timeoutEventsTotal), null);
					}
					m();
				}
				finally
				{
					try
					{
						if (writer)
						{
							this.cowCacheAccessLock.ExitWriteLock();
						}
						else
						{
							this.cowCacheAccessLock.ExitReadLock();
						}
					}
					catch (SynchronizationLockException)
					{
					}
					if (ExDateTime.Now > now + this.longWaitLimit)
					{
						this.longWaitEventsTotal++;
						ExTraceGlobals.SessionTracer.TraceError(0L, "Long wait on the mailbox information cache. The limit of {0} seconds was hit {1} times, timeout of {2} seconds was hit {3} times. Execution time {4} seconds", new object[]
						{
							this.longWaitLimit.TotalSeconds,
							this.longWaitEventsTotal,
							this.timeoutLimit.TotalSeconds,
							this.timeoutEventsTotal,
							(ExDateTime.Now - now2).TotalSeconds
						});
						StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorCOWCacheWaitTimeout, null, new object[]
						{
							COWSettings.middleTierProcessName,
							this.longWaitLimit.TotalSeconds,
							this.longWaitEventsTotal,
							this.timeoutLimit.TotalSeconds,
							this.timeoutEventsTotal,
							(ExDateTime.Now - now2).TotalSeconds
						});
					}
				}
			}

			// Token: 0x060040C2 RID: 16578 RVA: 0x0010FD28 File Offset: 0x0010DF28
			internal void PurgeCache()
			{
				this.TimedOperation(false, delegate
				{
					this.cowMailboxInfoCache.Clear();
					this.cowDatabaseInfoCache.Clear();
					this.cowTenantInfoCache.Clear();
					this.calendarLoggingFolderInfoCache.Clear();
					this.perfCounters.DumpsterADSettingCacheSize.RawValue = 0L;
				});
			}

			// Token: 0x060040C3 RID: 16579 RVA: 0x0010FDCC File Offset: 0x0010DFCC
			internal COWSettings.COWMailboxInfo GetCOWMailboxInfo(StoreSession session)
			{
				Util.ThrowOnNullArgument(session, "session");
				bool resultFound = false;
				bool cacheCleanupNeeded = false;
				COWSettings.COWMailboxInfo result = default(COWSettings.COWMailboxInfo);
				this.perfCounters.DumpsterADSettingCacheMisses_Base.Increment();
				this.TimedOperation(false, delegate
				{
					resultFound = this.cowMailboxInfoCache.TryGetValue(session.MailboxGuid, out result);
					cacheCleanupNeeded = (this.timeNextCacheCleanup < ExDateTime.Now);
				});
				if (!resultFound || result.InfoExpireTime < ExDateTime.Now)
				{
					ExTraceGlobals.SessionTracer.TraceDebug<string>((long)session.GetHashCode(), "Reading CopyOnWrite info from AD (reason {0}).", resultFound ? "cache outdated" : "missing from cache");
					COWSettings.COWMailboxInfo updateInfo = this.GetCOWMailboxInfoFromAD(session);
					if (!resultFound)
					{
						this.perfCounters.DumpsterADSettingCacheSize.Increment();
					}
					this.perfCounters.DumpsterADSettingCacheMisses.Increment();
					this.TimedOperation(true, delegate
					{
						this.cowMailboxInfoCache[session.MailboxGuid] = updateInfo;
					});
					result = updateInfo;
					resultFound = true;
				}
				if (cacheCleanupNeeded)
				{
					this.CleanupCacheTimeBased();
				}
				return result;
			}

			// Token: 0x060040C4 RID: 16580 RVA: 0x0010FF64 File Offset: 0x0010E164
			internal ulong? GetCalendarLoggingFolderSize(MailboxSession session)
			{
				COWSettings.COWInfoCache.<>c__DisplayClassf CS$<>8__locals1 = new COWSettings.COWInfoCache.<>c__DisplayClassf();
				CS$<>8__locals1.session = session;
				CS$<>8__locals1.<>4__this = this;
				Util.ThrowOnNullArgument(CS$<>8__locals1.session, "session");
				CS$<>8__locals1.resultFound = false;
				CS$<>8__locals1.calendarLoggingFolderInfo = default(COWSettings.CalendarLoggingFolderInfo);
				this.TimedOperation(false, delegate
				{
					CS$<>8__locals1.resultFound = CS$<>8__locals1.<>4__this.calendarLoggingFolderInfoCache.TryGetValue(CS$<>8__locals1.session.MailboxGuid, out CS$<>8__locals1.calendarLoggingFolderInfo);
				});
				if (!CS$<>8__locals1.resultFound || CS$<>8__locals1.calendarLoggingFolderInfo.AccessCount > this.maxCachedFolderSizeAccessCount)
				{
					using (Folder folder = Folder.Bind(CS$<>8__locals1.session, DefaultFolderType.CalendarLogging, new PropertyDefinition[]
					{
						FolderSchema.ExtendedSize
					}))
					{
						object obj = folder.TryGetProperty(FolderSchema.ExtendedSize);
						if (obj is PropertyError)
						{
							ExTraceGlobals.SessionTracer.TraceError<COWSettings.COWInfoCache, PropertyError>((long)this.GetHashCode(), "{0}: We could not get size of the calendar logging folder due to PropertyError {1}. Skipping it.", this, (PropertyError)obj);
							return null;
						}
						CS$<>8__locals1.calendarLoggingFolderInfo = new COWSettings.CalendarLoggingFolderInfo
						{
							Size = (ulong)((long)obj),
							AccessCount = 0U
						};
						goto IL_10B;
					}
				}
				COWSettings.COWInfoCache.<>c__DisplayClassf CS$<>8__locals2 = CS$<>8__locals1;
				CS$<>8__locals2.calendarLoggingFolderInfo.AccessCount = CS$<>8__locals2.calendarLoggingFolderInfo.AccessCount + 1U;
				IL_10B:
				this.TimedOperation(true, delegate
				{
					CS$<>8__locals1.<>4__this.calendarLoggingFolderInfoCache[CS$<>8__locals1.session.MailboxGuid] = CS$<>8__locals1.calendarLoggingFolderInfo;
				});
				return new ulong?(CS$<>8__locals1.calendarLoggingFolderInfo.Size);
			}

			// Token: 0x060040C5 RID: 16581 RVA: 0x001100B4 File Offset: 0x0010E2B4
			private void ComputeNextCleanupCacheTime()
			{
				this.timeNextCacheCleanup = ExDateTime.Now + this.cowCacheLifeTime;
			}

			// Token: 0x060040C6 RID: 16582 RVA: 0x00110314 File Offset: 0x0010E514
			private void CleanupCacheTimeBased()
			{
				ExDateTime cleanupLimit = ExDateTime.Now;
				int initialCacheSize = 0;
				int finalCacheSize = 0;
				Dictionary<Guid, COWSettings.COWMailboxInfo> cleanCowMailboxInfoCache = new Dictionary<Guid, COWSettings.COWMailboxInfo>();
				Dictionary<ADObjectId, COWSettings.COWDatabaseInfo> cleanCowDatabaseInfoCache = new Dictionary<ADObjectId, COWSettings.COWDatabaseInfo>();
				Dictionary<OrganizationId, COWSettings.COWTenantInfo> cleanCowTenantInfoCache = new Dictionary<OrganizationId, COWSettings.COWTenantInfo>();
				Dictionary<Guid, COWSettings.CalendarLoggingFolderInfo> cleanCalendarLoggingFolderInfoCache = new Dictionary<Guid, COWSettings.CalendarLoggingFolderInfo>();
				bool cleanupDone = false;
				this.TimedOperation(false, delegate
				{
					initialCacheSize = this.cowMailboxInfoCache.Count + this.cowDatabaseInfoCache.Count + this.cowTenantInfoCache.Count;
					cleanCowMailboxInfoCache = this.GetNonExpiredCacheEntries<Guid, COWSettings.COWMailboxInfo>(this.cowMailboxInfoCache, cleanupLimit);
					cleanCowDatabaseInfoCache = this.GetNonExpiredCacheEntries<ADObjectId, COWSettings.COWDatabaseInfo>(this.cowDatabaseInfoCache, cleanupLimit);
					cleanCowTenantInfoCache = this.GetNonExpiredCacheEntries<OrganizationId, COWSettings.COWTenantInfo>(this.cowTenantInfoCache, cleanupLimit);
					foreach (KeyValuePair<Guid, COWSettings.CalendarLoggingFolderInfo> keyValuePair in this.calendarLoggingFolderInfoCache)
					{
						if (keyValuePair.Value.AccessCount < this.maxCachedFolderSizeAccessCount)
						{
							cleanCalendarLoggingFolderInfoCache[keyValuePair.Key] = keyValuePair.Value;
						}
					}
				});
				this.TimedOperation(false, delegate
				{
					cleanupDone = (this.timeNextCacheCleanup > ExDateTime.Now);
				});
				if (!cleanupDone)
				{
					this.TimedOperation(true, delegate
					{
						if (this.timeNextCacheCleanup < ExDateTime.Now)
						{
							initialCacheSize = this.cowMailboxInfoCache.Count + this.cowDatabaseInfoCache.Count + this.cowTenantInfoCache.Count;
							this.cowMailboxInfoCache = cleanCowMailboxInfoCache;
							this.cowDatabaseInfoCache = cleanCowDatabaseInfoCache;
							this.cowTenantInfoCache = cleanCowTenantInfoCache;
							this.calendarLoggingFolderInfoCache = cleanCalendarLoggingFolderInfoCache;
							finalCacheSize = cleanCowMailboxInfoCache.Count + cleanCowDatabaseInfoCache.Count + cleanCowTenantInfoCache.Count;
							this.perfCounters.DumpsterADSettingCacheSize.RawValue = (long)finalCacheSize;
							this.ComputeNextCleanupCacheTime();
							cleanupDone = true;
						}
					});
					if (cleanupDone)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<int, ExDateTime, ExDateTime>(0L, "CleanupCacheTimeBased removed {0} entries, start time {1}, end time {2}.", finalCacheSize - initialCacheSize, cleanupLimit, ExDateTime.Now);
					}
				}
				if (!cleanupDone)
				{
					ExTraceGlobals.SessionTracer.TraceWarning(0L, "CleanupCacheTimeBased useless cleanup: other thread did it.");
				}
			}

			// Token: 0x060040C7 RID: 16583 RVA: 0x0011040C File Offset: 0x0010E60C
			private Dictionary<TKey, TValue> GetNonExpiredCacheEntries<TKey, TValue>(Dictionary<TKey, TValue> cache, ExDateTime expirationTime) where TValue : COWSettings.IExpirableCacheEntry
			{
				Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(cache.Count);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in cache)
				{
					TValue value = keyValuePair.Value;
					if (value.InfoExpireTime >= expirationTime)
					{
						dictionary[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				return dictionary;
			}

			// Token: 0x060040C8 RID: 16584 RVA: 0x0011057C File Offset: 0x0010E77C
			private COWSettings.COWDatabaseInfo GetInterestingDatabaseQuotas(ADObjectId databaseId)
			{
				Util.ThrowOnNullArgument(databaseId, "databaseId");
				bool resultFound = false;
				COWSettings.COWDatabaseInfo databaseInfo = default(COWSettings.COWDatabaseInfo);
				this.perfCounters.DumpsterADSettingCacheMisses_Base.Increment();
				this.TimedOperation(false, delegate
				{
					resultFound = this.cowDatabaseInfoCache.TryGetValue(databaseId, out databaseInfo);
				});
				if (!resultFound || databaseInfo.InfoExpireTime < ExDateTime.Now)
				{
					if (!resultFound)
					{
						this.perfCounters.DumpsterADSettingCacheSize.Increment();
					}
					this.perfCounters.DumpsterADSettingCacheMisses.Increment();
					ExDateTime infoExpireTime;
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2086, "GetInterestingDatabaseQuotas", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\CopyOnWrite\\COWSettings.cs");
						MailboxDatabase mailboxDatabase = topologyConfigurationSession.Read<MailboxDatabase>(databaseId);
						if (mailboxDatabase != null)
						{
							infoExpireTime = ExDateTime.Now + this.cowCacheLifeTime;
							databaseInfo = new COWSettings.COWDatabaseInfo
							{
								DumpsterWarningQuota = mailboxDatabase.RecoverableItemsWarningQuota,
								CalendarLoggingQuota = mailboxDatabase.CalendarLoggingQuota,
								DumpsterQuota = mailboxDatabase.RecoverableItemsQuota,
								InfoExpireTime = infoExpireTime
							};
						}
					});
					if (!adoperationResult.Succeeded)
					{
						LocalizedException ex = adoperationResult.Exception as LocalizedException;
						if (ex != null)
						{
							throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "GetInterestingDatabaseQuotas failed due to directory exception {0}.", new object[]
							{
								ex
							});
						}
						throw adoperationResult.Exception;
					}
					else
					{
						this.TimedOperation(true, delegate
						{
							this.cowDatabaseInfoCache[databaseId] = databaseInfo;
						});
					}
				}
				return databaseInfo;
			}

			// Token: 0x060040C9 RID: 16585 RVA: 0x00110700 File Offset: 0x0010E900
			private bool IsCalendarLoggingEnabledOnTenant(OrganizationId orgId)
			{
				Util.ThrowOnNullArgument(orgId, "orgId");
				bool resultFound = false;
				COWSettings.COWTenantInfo tenantInfo = default(COWSettings.COWTenantInfo);
				this.perfCounters.DumpsterADSettingCacheMisses_Base.Increment();
				this.TimedOperation(false, delegate
				{
					resultFound = this.cowTenantInfoCache.TryGetValue(orgId, out tenantInfo);
				});
				if (!resultFound || tenantInfo.InfoExpireTime < ExDateTime.Now)
				{
					if (!resultFound)
					{
						this.perfCounters.DumpsterADSettingCacheSize.Increment();
					}
					this.perfCounters.DumpsterADSettingCacheMisses.Increment();
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSessionSettings.SessionSettingsFactory.Default.GetRootOrgContainerId(orgId.PartitionId), orgId, null, false), 2166, "IsCalendarLoggingEnabledOnTenant", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\CopyOnWrite\\COWSettings.cs");
					ExDateTime infoExpireTime = ExDateTime.Now + this.cowCacheLifeTime;
					tenantInfo = new COWSettings.COWTenantInfo
					{
						CalendarVersionStoreEnabled = tenantConfigurationSession.GetOrgContainer().CalendarVersionStoreEnabled,
						InfoExpireTime = infoExpireTime
					};
					this.TimedOperation(true, delegate
					{
						this.cowTenantInfoCache[orgId] = tenantInfo;
					});
				}
				return tenantInfo.CalendarVersionStoreEnabled;
			}

			// Token: 0x060040CA RID: 16586 RVA: 0x00110940 File Offset: 0x0010EB40
			private COWSettings.COWMailboxInfo GetCOWMailboxInfoFromAD(StoreSession session)
			{
				ADUser user = null;
				bool litigationHold = false;
				bool calendarLogging = false;
				bool siteMailboxMessageDedup = false;
				bool singleItemRecovery = false;
				bool inPlaceHoldEnabled = false;
				bool? tenantCalendarLoggingEnabled = null;
				Unlimited<ByteQuantifiedSize> dumpsterWarningQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				Unlimited<ByteQuantifiedSize> calendarLoggingQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				Unlimited<ByteQuantifiedSize> unlimited = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					IRecipientSession adrecipientSession = session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
					ADRecipient adrecipient;
					if (session.MailboxOwner.MailboxInfo.MailboxGuid == Guid.Empty && session is MailboxSession)
					{
						adrecipient = adrecipientSession.FindByLegacyExchangeDN(((MailboxSession)session).MailboxOwnerLegacyDN);
					}
					else
					{
						adrecipient = adrecipientSession.FindByExchangeGuidIncludingAlternate(session.MailboxOwner.MailboxInfo.MailboxGuid);
					}
					user = (adrecipient as ADUser);
					if (this.IsDatacenter() && adrecipientSession.SessionSettings.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
					{
						tenantCalendarLoggingEnabled = new bool?(this.IsCalendarLoggingEnabledOnTenant(adrecipientSession.SessionSettings.CurrentOrganizationId));
					}
				});
				if (adoperationResult.Succeeded)
				{
					ExDateTime infoExpireTime = ExDateTime.Now + this.cowCacheLifeTime;
					if (user == null)
					{
						ExTraceGlobals.SessionTracer.TraceError((long)session.GetHashCode(), "User not found in AD.");
						litigationHold = false;
						singleItemRecovery = false;
						inPlaceHoldEnabled = false;
					}
					else
					{
						if (user.LitigationHoldEnabled)
						{
							litigationHold = true;
						}
						if (tenantCalendarLoggingEnabled != null && tenantCalendarLoggingEnabled.Value && !user.CalendarVersionStoreDisabled)
						{
							calendarLogging = true;
						}
						else if (!user.CalendarVersionStoreDisabled)
						{
							calendarLogging = true;
						}
						if (user.SiteMailboxMessageDedupEnabled)
						{
							siteMailboxMessageDedup = true;
						}
						if (user.SingleItemRecoveryEnabled)
						{
							singleItemRecovery = true;
						}
						if (user.UseDatabaseQuotaDefaults != null && !user.UseDatabaseQuotaDefaults.Value)
						{
							if (!user.RecoverableItemsQuota.IsUnlimited)
							{
								unlimited = ByteQuantifiedSize.FromBytes((ulong)(user.RecoverableItemsQuota.Value.ToBytes() * 0.2));
							}
							dumpsterWarningQuota = user.RecoverableItemsWarningQuota;
							calendarLoggingQuota = (user.CalendarLoggingQuota.IsUnlimited ? unlimited : user.CalendarLoggingQuota);
						}
						else
						{
							ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
							if (!exchangePrincipal.MailboxInfo.IsRemote)
							{
								COWSettings.COWDatabaseInfo interestingDatabaseQuotas = this.GetInterestingDatabaseQuotas(exchangePrincipal.MailboxInfo.MailboxDatabase);
								if (!interestingDatabaseQuotas.DumpsterQuota.IsUnlimited)
								{
									unlimited = ByteQuantifiedSize.FromBytes((ulong)(interestingDatabaseQuotas.DumpsterQuota.Value.ToBytes() * 0.2));
								}
								dumpsterWarningQuota = interestingDatabaseQuotas.DumpsterWarningQuota;
								calendarLoggingQuota = (interestingDatabaseQuotas.CalendarLoggingQuota.IsUnlimited ? unlimited : interestingDatabaseQuotas.CalendarLoggingQuota);
							}
						}
						if (user.InPlaceHolds != null && user.InPlaceHolds.Count > 0)
						{
							inPlaceHoldEnabled = true;
						}
					}
					this.perfCounters.DumpsterADSettingRefreshRate.Increment();
					return new COWSettings.COWMailboxInfo
					{
						LitigationHold = litigationHold,
						CalendarLogging = calendarLogging,
						SiteMailboxMessageDedup = siteMailboxMessageDedup,
						SingleItemRecovery = singleItemRecovery,
						InfoExpireTime = infoExpireTime,
						InPlaceHoldEnabled = inPlaceHoldEnabled,
						DumpsterWarningQuota = dumpsterWarningQuota,
						CalendarLoggingQuota = calendarLoggingQuota
					};
				}
				LocalizedException ex = adoperationResult.Exception as LocalizedException;
				if (ex != null)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "GetDumpsterSessionInfoFromAD failed due to directory exception {0}.", new object[]
					{
						ex
					});
				}
				throw adoperationResult.Exception;
			}

			// Token: 0x060040CB RID: 16587 RVA: 0x00110C78 File Offset: 0x0010EE78
			private bool IsDatacenter()
			{
				if (this.isDatacenter == null)
				{
					this.isDatacenter = new bool?(VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled);
				}
				return this.isDatacenter.Value;
			}

			// Token: 0x040023CC RID: 9164
			private readonly MiddleTierStoragePerformanceCountersInstance perfCounters = DumpsterFolderHelper.GetPerfCounters();

			// Token: 0x040023CD RID: 9165
			private readonly TimeSpan longWaitLimit = TimeSpan.FromMinutes(2.0);

			// Token: 0x040023CE RID: 9166
			private readonly TimeSpan timeoutLimit = TimeSpan.FromMinutes(10.0);

			// Token: 0x040023CF RID: 9167
			private readonly TimeSpan cowCacheLifeTime;

			// Token: 0x040023D0 RID: 9168
			private readonly uint maxCachedFolderSizeAccessCount = 50U;

			// Token: 0x040023D1 RID: 9169
			private int longWaitEventsTotal;

			// Token: 0x040023D2 RID: 9170
			private int timeoutEventsTotal;

			// Token: 0x040023D3 RID: 9171
			private ReaderWriterLockSlim cowCacheAccessLock;

			// Token: 0x040023D4 RID: 9172
			private ExDateTime timeNextCacheCleanup;

			// Token: 0x040023D5 RID: 9173
			private Dictionary<Guid, COWSettings.COWMailboxInfo> cowMailboxInfoCache;

			// Token: 0x040023D6 RID: 9174
			private Dictionary<ADObjectId, COWSettings.COWDatabaseInfo> cowDatabaseInfoCache;

			// Token: 0x040023D7 RID: 9175
			private Dictionary<OrganizationId, COWSettings.COWTenantInfo> cowTenantInfoCache;

			// Token: 0x040023D8 RID: 9176
			private Dictionary<Guid, COWSettings.CalendarLoggingFolderInfo> calendarLoggingFolderInfoCache;

			// Token: 0x040023D9 RID: 9177
			private bool? isDatacenter = null;

			// Token: 0x02000624 RID: 1572
			// (Invoke) Token: 0x060040CE RID: 16590
			private delegate void LockedMethod();
		}
	}
}
