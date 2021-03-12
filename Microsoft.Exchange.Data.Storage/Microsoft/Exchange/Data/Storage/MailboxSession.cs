using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.ThrottlingService.Client;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.XropService;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Mapi.Security;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSession : StoreSession, IMailboxSession, IStoreSession, IDisposable
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00009075 File Offset: 0x00007275
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000907C File Offset: 0x0000727C
		public static HashSet<DefaultFolderType> DefaultFoldersToForceInit { get; set; }

		// Token: 0x06000141 RID: 321 RVA: 0x00009084 File Offset: 0x00007284
		private static MailboxSession InternalCreateMailboxSession(LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegatedUser, CultureInfo cultureInfo, string clientInfoString, IBudget budget, Action<MailboxSession> initializeMailboxSession, MailboxSession.InitializeMailboxSessionFailure initializeMailboxSessionFailure)
		{
			return MailboxSession.InternalCreateMailboxSession(logonType, owner, delegatedUser, cultureInfo, clientInfoString, budget, initializeMailboxSession, initializeMailboxSessionFailure, null);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000090A4 File Offset: 0x000072A4
		private static MailboxSession InternalCreateMailboxSession(LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegatedUser, CultureInfo cultureInfo, string clientInfoString, IBudget budget, Action<MailboxSession> initializeMailboxSession, MailboxSession.InitializeMailboxSessionFailure initializeMailboxSessionFailure, MailboxSessionSharableDataManager sharedDataManager)
		{
			if (logonType != LogonType.Admin && logonType != LogonType.DelegatedAdmin && logonType != LogonType.Transport && logonType != LogonType.SystemService && logonType != LogonType.Owner && logonType != LogonType.BestAccess && (owner.MailboxInfo.IsArchive || owner.MailboxInfo.IsAggregated))
			{
				throw new InvalidOperationException("Archive and aggregated mailbox logon not valid for " + logonType.ToString());
			}
			if ((logonType != LogonType.Admin || clientInfoString == null || !MailboxSession.AllowedClientsForPublicFolderMailbox.IsMatch(clientInfoString)) && owner.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
			{
				throw new AccessDeniedException(ServerStrings.OperationNotSupportedOnPublicFolderMailbox);
			}
			if (logonType != LogonType.Admin && !MailboxSession.IsAdminAuditSession(logonType, owner, clientInfoString))
			{
				MailboxSession.InternalValidateServerVersion(owner);
			}
			MailboxSession.InternalValidateDatacenterAccess(logonType, owner, delegatedUser);
			bool flag = false;
			MailboxSession mailboxSession = null;
			sharedDataManager = (sharedDataManager ?? new MailboxSessionSharableDataManager());
			try
			{
				mailboxSession = new MailboxSession(cultureInfo, clientInfoString, budget, sharedDataManager);
				if (owner.MailboxInfo.IsArchive || owner.MailboxInfo.MailboxType == MailboxLocationType.AuxArchive)
				{
					mailboxSession.Capabilities = SessionCapabilities.ArchiveSessionCapabilities;
				}
				else if (owner.MailboxInfo.IsAggregated)
				{
					mailboxSession.Capabilities = SessionCapabilities.MirrorSessionCapabilities;
				}
				initializeMailboxSession(mailboxSession);
				int? num = PropertyBag.CheckNullablePropertyValue<int>(InternalSchema.LocaleId, mailboxSession.Mailbox.TryGetProperty(InternalSchema.LocaleId));
				if (num != null)
				{
					int lcidFromCulture = LocaleMap.GetLcidFromCulture(mailboxSession.InternalPreferedCulture);
					if (num.Value != lcidFromCulture)
					{
						throw new ConnectionFailedPermanentException(ServerStrings.CultureMismatchAfterConnect(lcidFromCulture.ToString(), num.Value.ToString()));
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					initializeMailboxSessionFailure();
					if (mailboxSession != null)
					{
						mailboxSession.Dispose();
						mailboxSession = null;
					}
				}
			}
			return mailboxSession;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000923C File Offset: 0x0000743C
		public MailboxSession.MailboxItemCountsAndSizes ReadMailboxCountsAndSizes()
		{
			base.Mailbox.Load(MailboxSession.mailboxItemCountsAndSizesProperties);
			MailboxSession.MailboxItemCountsAndSizes result;
			result.ItemCount = (base.Mailbox.TryGetProperty(InternalSchema.ItemCount) as int?);
			result.TotalItemSize = (base.Mailbox.TryGetProperty(MailboxSchema.QuotaUsedExtended) as long?);
			result.DeletedItemCount = (base.Mailbox.TryGetProperty(InternalSchema.DeletedMsgCount) as int?);
			result.TotalDeletedItemSize = (base.Mailbox.TryGetProperty(MailboxSchema.DumpsterQuotaUsedExtended) as long?);
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000092E0 File Offset: 0x000074E0
		public void MarkAsEhaMailbox()
		{
			base.Mailbox.Load(new PropertyDefinition[]
			{
				StoreObjectSchema.RetentionFlags
			});
			base.Mailbox[StoreObjectSchema.RetentionFlags] = RetentionAndArchiveFlags.EHAMigration;
			base.Mailbox.Save();
			base.Mailbox.Load();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009338 File Offset: 0x00007538
		private static void CheckSystemFolderBypass(LogonType logonType, IList<DefaultFolderType> folders)
		{
			if (logonType != LogonType.Admin && logonType != LogonType.SystemService && !Util.Contains(folders, DefaultFolderType.System))
			{
				throw new AccessDeniedException(ServerStrings.ExSystemFolderAccessDenied);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009358 File Offset: 0x00007558
		private static MailboxSession CreateMailboxSession(LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegateUser, object identity, OpenMailboxSessionFlags flags, CultureInfo cultureInfo, string clientInfoString, PropertyDefinition[] mailboxProperties, IList<DefaultFolderType> foldersToInit, GenericIdentity auxiliaryIdentity, IBudget budget, MailboxSessionSharableDataManager sharedDataManager)
		{
			return MailboxSession.CreateMailboxSession(logonType, owner, delegateUser, identity, flags, cultureInfo, clientInfoString, mailboxProperties, foldersToInit, auxiliaryIdentity, budget, false, sharedDataManager, UnifiedGroupMemberType.Unknown);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000946C File Offset: 0x0000766C
		private static MailboxSession CreateMailboxSession(LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegateUser, object identity, OpenMailboxSessionFlags flags, CultureInfo cultureInfo, string clientInfoString, PropertyDefinition[] mailboxProperties, IList<DefaultFolderType> foldersToInit, GenericIdentity auxiliaryIdentity, IBudget budget, bool unifiedSession, MailboxSessionSharableDataManager sharedDataManager, UnifiedGroupMemberType memberType)
		{
			MailboxSession.CheckSystemFolderBypass(logonType, foldersToInit);
			return MailboxSession.InternalCreateMailboxSession(logonType, owner, delegateUser, cultureInfo, clientInfoString, budget, delegate(MailboxSession mailboxSession)
			{
				mailboxSession.mailboxProperties = mailboxProperties;
				mailboxSession.foldersToInit = foldersToInit;
				mailboxSession.unifiedGroupMemberType = memberType;
				try
				{
					mailboxSession.Initialize(null, logonType, owner, delegateUser, identity, flags, auxiliaryIdentity, unifiedSession);
				}
				catch (StorageTransientException e)
				{
					MailboxSession.TriggerSiteMailboxSyncIfNeeded(e, owner, clientInfoString);
					throw;
				}
			}, delegate()
			{
				ExTraceGlobals.SessionTracer.TraceError(0L, "MailboxSession::CreateMailboxSession. Operation failed. mailboxOwner = {0}, delegateUser = {1}, flag = {2}, cultureInfo = {3}, clientInfoString = {4}.", new object[]
				{
					owner,
					delegateUser,
					(int)flags,
					cultureInfo,
					clientInfoString
				});
			}, sharedDataManager);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000095F4 File Offset: 0x000077F4
		private static MailboxSession CreateAlternateMailboxSession(MailboxSession linkedSession, LogonType logonType, IExchangePrincipal owner, object identity, OpenMailboxSessionFlags flags, IList<DefaultFolderType> foldersToInit)
		{
			MailboxSession.CheckSystemFolderBypass(logonType, foldersToInit);
			DelegateLogonUser delegatedUser = new DelegateLogonUser(linkedSession.UserLegacyDN);
			if (owner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox && owner.MailboxInfo.MailboxGuid != linkedSession.MailboxOwner.MailboxInfo.MailboxGuid)
			{
				throw new AccessDeniedException(ServerStrings.AttemptingSessionCreationAgainstWrongGroupMailbox(owner.MailboxInfo.PrimarySmtpAddress.ToString(), linkedSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			return MailboxSession.InternalCreateMailboxSession(logonType, owner, delegatedUser, linkedSession.InternalCulture, linkedSession.ClientInfoString, null, delegate(MailboxSession mailboxSession)
			{
				mailboxSession.mailboxProperties = null;
				mailboxSession.foldersToInit = foldersToInit;
				mailboxSession.unifiedGroupMemberType = linkedSession.unifiedGroupMemberType;
				try
				{
					mailboxSession.Initialize(linkedSession.Mailbox.MapiStore, logonType, owner, new DelegateLogonUser(linkedSession.UserLegacyDN), identity, flags, null, false);
				}
				catch (StorageTransientException e)
				{
					MailboxSession.TriggerSiteMailboxSyncIfNeeded(e, owner, linkedSession.ClientInfoString);
					throw;
				}
			}, delegate()
			{
				ExTraceGlobals.SessionTracer.TraceError<IExchangePrincipal, int>(0L, "MailboxSession::CreateAlternateMailboxSession. Operation failed. mailboxOwner = {0}, flag = {1}.", owner, (int)flags);
			});
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009724 File Offset: 0x00007924
		private static void InternalBuildOpenMailboxSessionFlags(MailboxSession.InitializationFlags initFlags, LogonType logonType, IList<DefaultFolderType> foldersToInit, out OpenMailboxSessionFlags openFlags)
		{
			EnumValidator.ThrowIfInvalid<LogonType>(logonType, "logonType");
			EnumValidator.ThrowIfInvalid<MailboxSession.InitializationFlags>(initFlags, "initFlags");
			if ((initFlags & MailboxSession.InitializationFlags.DefaultFolders) == MailboxSession.InitializationFlags.DefaultFolders && (foldersToInit == null || foldersToInit.Count == 0))
			{
				throw new ArgumentException("Must have foldersToInit if initFlags includes InitializationFlags.DefaultFolders", "foldersToInit");
			}
			if ((initFlags & MailboxSession.InitializationFlags.DefaultFolders) != MailboxSession.InitializationFlags.DefaultFolders && foldersToInit != null && foldersToInit.Count > 0)
			{
				throw new ArgumentException("initFlags must include InitializationFlags.DefaultFolders if foldersToInit is specified", "foldersToInit");
			}
			if ((initFlags & (MailboxSession.InitializationFlags.RequestLocalRpc | MailboxSession.InitializationFlags.OverrideHomeMdb | MailboxSession.InitializationFlags.DisconnectedMailbox | MailboxSession.InitializationFlags.XForestMove | MailboxSession.InitializationFlags.MoveUser)) != MailboxSession.InitializationFlags.None && logonType != LogonType.Admin && logonType != LogonType.SystemService && logonType != LogonType.DelegatedAdmin)
			{
				throw new ArgumentException("Flags not valid for non-Admin logon", "initFlags");
			}
			if ((initFlags & (MailboxSession.InitializationFlags.QuotaMessageDelivery | MailboxSession.InitializationFlags.NormalMessageDelivery | MailboxSession.InitializationFlags.SpecialMessageDelivery)) != MailboxSession.InitializationFlags.None && logonType != LogonType.Transport)
			{
				throw new ArgumentException("Flags not valid for non-Transport logon", "initFlags");
			}
			openFlags = OpenMailboxSessionFlags.None;
			if ((initFlags & MailboxSession.InitializationFlags.DefaultFolders) == MailboxSession.InitializationFlags.DefaultFolders)
			{
				openFlags |= OpenMailboxSessionFlags.InitDefaultFolders;
			}
			if ((initFlags & MailboxSession.InitializationFlags.UserConfigurationManager) == MailboxSession.InitializationFlags.UserConfigurationManager)
			{
				openFlags |= OpenMailboxSessionFlags.InitUserConfigurationManager;
			}
			if ((initFlags & MailboxSession.InitializationFlags.CopyOnWrite) == MailboxSession.InitializationFlags.CopyOnWrite)
			{
				openFlags |= OpenMailboxSessionFlags.InitCopyOnWrite;
				if ((openFlags & OpenMailboxSessionFlags.UseRecoveryDatabase) != OpenMailboxSessionFlags.None)
				{
					throw new ArgumentException("No CopyOnWrite allowed for Recovery DB logons", "initFlags");
				}
			}
			if ((initFlags & MailboxSession.InitializationFlags.DeadSessionChecking) == MailboxSession.InitializationFlags.DeadSessionChecking)
			{
				openFlags |= OpenMailboxSessionFlags.InitDeadSessionChecking;
			}
			if ((initFlags & MailboxSession.InitializationFlags.CheckPrivateItemsAccess) == MailboxSession.InitializationFlags.CheckPrivateItemsAccess)
			{
				openFlags |= OpenMailboxSessionFlags.InitCheckPrivateItemsAccess;
			}
			if ((initFlags & MailboxSession.InitializationFlags.SuppressFolderIdPrefetch) == MailboxSession.InitializationFlags.SuppressFolderIdPrefetch)
			{
				openFlags |= OpenMailboxSessionFlags.SuppressFolderIdPrefetch;
			}
			if ((initFlags & MailboxSession.InitializationFlags.UseNamedProperties) == MailboxSession.InitializationFlags.UseNamedProperties)
			{
				openFlags |= OpenMailboxSessionFlags.UseNamedProperties;
			}
			if ((initFlags & MailboxSession.InitializationFlags.DeferDefaultFolderIdInitialization) == MailboxSession.InitializationFlags.DeferDefaultFolderIdInitialization)
			{
				openFlags |= OpenMailboxSessionFlags.DeferDefaultFolderIdInitialization;
			}
			if ((initFlags & MailboxSession.InitializationFlags.UseRecoveryDatabase) == MailboxSession.InitializationFlags.UseRecoveryDatabase)
			{
				openFlags |= OpenMailboxSessionFlags.UseRecoveryDatabase;
				if ((openFlags & OpenMailboxSessionFlags.InitCopyOnWrite) != OpenMailboxSessionFlags.None)
				{
					throw new ArgumentException("No CopyOnWrite allowed for Recovery DB logons", "initFlags");
				}
			}
			if ((initFlags & MailboxSession.InitializationFlags.NonInteractiveSession) == MailboxSession.InitializationFlags.NonInteractiveSession)
			{
				openFlags |= OpenMailboxSessionFlags.NonInteractiveSession;
			}
			if ((initFlags & MailboxSession.InitializationFlags.IgnoreForcedFolderInit) == MailboxSession.InitializationFlags.IgnoreForcedFolderInit)
			{
				openFlags |= OpenMailboxSessionFlags.IgnoreForcedFolderInit;
			}
			if ((initFlags & MailboxSession.InitializationFlags.ReadOnly) == MailboxSession.InitializationFlags.ReadOnly)
			{
				openFlags |= OpenMailboxSessionFlags.ReadOnly;
			}
			switch (logonType)
			{
			case LogonType.Admin:
			case LogonType.DelegatedAdmin:
				openFlags |= OpenMailboxSessionFlags.RequestAdminAccess;
				if ((initFlags & MailboxSession.InitializationFlags.RequestLocalRpc) == MailboxSession.InitializationFlags.RequestLocalRpc)
				{
					openFlags |= OpenMailboxSessionFlags.RequestLocalRpcConnection;
				}
				if ((initFlags & MailboxSession.InitializationFlags.OverrideHomeMdb) == MailboxSession.InitializationFlags.OverrideHomeMdb)
				{
					openFlags |= OpenMailboxSessionFlags.OverrideHomeMdb;
				}
				if ((initFlags & MailboxSession.InitializationFlags.AllowAdminLocalization) == MailboxSession.InitializationFlags.AllowAdminLocalization)
				{
					openFlags |= OpenMailboxSessionFlags.AllowAdminLocalization;
					return;
				}
				break;
			case LogonType.Delegated:
			case LogonType.BestAccess:
				break;
			case LogonType.Transport:
				openFlags |= OpenMailboxSessionFlags.RequestTransportAccess;
				if ((initFlags & MailboxSession.InitializationFlags.QuotaMessageDelivery) == MailboxSession.InitializationFlags.QuotaMessageDelivery)
				{
					openFlags |= OpenMailboxSessionFlags.OpenForQuotaMessageDelivery;
				}
				if ((initFlags & MailboxSession.InitializationFlags.NormalMessageDelivery) == MailboxSession.InitializationFlags.NormalMessageDelivery)
				{
					openFlags |= OpenMailboxSessionFlags.OpenForNormalMessageDelivery;
				}
				if ((initFlags & MailboxSession.InitializationFlags.SpecialMessageDelivery) == MailboxSession.InitializationFlags.SpecialMessageDelivery)
				{
					openFlags |= OpenMailboxSessionFlags.OpenForSpecialMessageDelivery;
					return;
				}
				break;
			case LogonType.SystemService:
				openFlags |= OpenMailboxSessionFlags.RequestAdminAccess;
				if (initFlags.HasFlag(MailboxSession.InitializationFlags.RequestLocalRpc))
				{
					openFlags |= OpenMailboxSessionFlags.RequestLocalRpcConnection;
				}
				if (initFlags.HasFlag(MailboxSession.InitializationFlags.OverrideHomeMdb))
				{
					openFlags |= OpenMailboxSessionFlags.OverrideHomeMdb;
				}
				if (initFlags.HasFlag(MailboxSession.InitializationFlags.DisconnectedMailbox))
				{
					openFlags |= OpenMailboxSessionFlags.DisconnectedMailbox;
				}
				if (initFlags.HasFlag(MailboxSession.InitializationFlags.XForestMove))
				{
					openFlags |= OpenMailboxSessionFlags.XForestMove;
				}
				if (initFlags.HasFlag(MailboxSession.InitializationFlags.MoveUser))
				{
					openFlags |= OpenMailboxSessionFlags.MoveUser;
				}
				if (initFlags.HasFlag(MailboxSession.InitializationFlags.OlcSync))
				{
					openFlags |= OpenMailboxSessionFlags.OlcSync;
				}
				if ((initFlags & MailboxSession.InitializationFlags.AllowAdminLocalization) == MailboxSession.InitializationFlags.AllowAdminLocalization)
				{
					openFlags |= OpenMailboxSessionFlags.AllowAdminLocalization;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009A88 File Offset: 0x00007C88
		public static MailboxSession ConfigurableOpen(IExchangePrincipal mailbox, MailboxAccessInfo accessInfo, CultureInfo cultureInfo, string clientInfoString, LogonType logonType, PropertyDefinition[] mailboxProperties, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit)
		{
			return MailboxSession.ConfigurableOpen(mailbox, accessInfo, cultureInfo, clientInfoString, logonType, mailboxProperties, initFlags, foldersToInit, null);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009AA8 File Offset: 0x00007CA8
		private static MailboxSession ConfigurableOpenAlternate(MailboxSession linkedSession, IExchangePrincipal mailbox, object identity, LogonType logonType, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit)
		{
			Util.ThrowOnNullArgument(linkedSession, "linkedSession");
			Util.ThrowOnNullArgument(mailbox, "mailbox");
			OpenMailboxSessionFlags flags;
			MailboxSession.InternalBuildOpenMailboxSessionFlags(initFlags, logonType, foldersToInit, out flags);
			return MailboxSession.CreateAlternateMailboxSession(linkedSession, logonType, mailbox, identity, flags, foldersToInit);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009AE3 File Offset: 0x00007CE3
		private static bool IsMemberLogonToGroupMailbox(IExchangePrincipal mailboxOwner, LogonType logonType)
		{
			return mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox && logonType != LogonType.Admin && logonType != LogonType.SystemService && logonType != LogonType.Transport && logonType != LogonType.Owner;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009B1C File Offset: 0x00007D1C
		protected MailboxSession(CultureInfo cultureInfo, string clientInfoString, IBudget budget, MailboxSessionSharableDataManager sharedDataManager) : base(cultureInfo, clientInfoString, budget)
		{
			this.activitySessionHook = Hookable<IActivitySession>.Create(true, () => this.activitySession, delegate(IActivitySession value)
			{
				this.activitySession = value;
			});
			this.sharedDataManager = sharedDataManager;
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.delegateSessionManager = new DelegateSessionManager(this);
				disposeGuard.Success();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public static MailboxSession Open(IExchangePrincipal mailboxOwner, AuthzContextHandle authenticatedUserHandle, CultureInfo cultureInfo, string clientInfoString)
		{
			if (authenticatedUserHandle == null)
			{
				throw new ArgumentNullException("authenticatedUserHandle");
			}
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(authenticatedUserHandle);
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Owner, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009BEC File Offset: 0x00007DEC
		public DelegateSessionHandle GetDelegateSessionHandle(IExchangePrincipal principal)
		{
			DelegateSessionHandle result;
			using (base.CheckDisposed("GetDelegateSessionHandle"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveDelegateUsers, "CanHaveDelegateUsers");
				if (this.logonType != LogonType.Owner && principal.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox)
				{
					throw new InvalidOperationException("This session itself is a delegated session.");
				}
				if (principal == null)
				{
					throw new ArgumentNullException("principal");
				}
				DelegateSessionEntry entry = this.InternalGetDelegateSessionEntry(principal, OpenBy.Consumer);
				result = new DelegateSessionHandle(entry);
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009C80 File Offset: 0x00007E80
		internal DelegateSessionEntry InternalGetDelegateSessionEntry(IExchangePrincipal principal, OpenBy openBy)
		{
			DelegateSessionEntry delegateSessionEntry;
			using (base.CreateSessionGuard("InternalGetDelegateSessionEntry"))
			{
				delegateSessionEntry = this.delegateSessionManager.GetDelegateSessionEntry(principal, openBy);
			}
			return delegateSessionEntry;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009CC8 File Offset: 0x00007EC8
		public static MailboxSession Open(IExchangePrincipal mailboxOwner, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(clientSecurityContext);
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Owner, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009D00 File Offset: 0x00007F00
		public MailboxSession OpenAlternate(IExchangePrincipal mailboxOwner)
		{
			MailboxSession result;
			using (base.CheckDisposed("OpenAlternate"))
			{
				if (mailboxOwner == null)
				{
					throw new ArgumentNullException("mailboxOwner");
				}
				MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
				result = MailboxSession.ConfigurableOpenAlternate(this, mailboxOwner, this.identity, LogonType.BestAccess, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009D68 File Offset: 0x00007F68
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, string accessingUserDn, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			return MailboxSession.OpenWithBestAccess(mailboxOwner, accessingUserDn, clientSecurityContext, cultureInfo, clientInfoString, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009D76 File Offset: 0x00007F76
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, string accessingUserLegacyDn, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb)
		{
			return MailboxSession.OpenAsAdmin(mailboxOwner, accessingUserLegacyDn, clientSecurityContext, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, false);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009D88 File Offset: 0x00007F88
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, string accessingUserLegacyDn, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb, bool useRecoveryDatabase)
		{
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(accessingUserLegacyDn, clientSecurityContext);
			return MailboxSession.OpenAsAdmin(mailboxOwner, accessInfo, LogonType.DelegatedAdmin, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, useRecoveryDatabase, false, false);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009DB8 File Offset: 0x00007FB8
		internal static MailboxSession InternalOpenDelegateAccess(MailboxSession delegateMailboxSession, IExchangePrincipal principal)
		{
			if (delegateMailboxSession == null)
			{
				throw new ArgumentNullException("delegateMailboxSession");
			}
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			delegateMailboxSession.CheckMasterSessionForCalendarDelegate();
			MailboxAccessInfo accessInfo = null;
			string clientInfoString = delegateMailboxSession.clientInfoString;
			string accessingUserDn;
			if (principal.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				accessingUserDn = delegateMailboxSession.UserLegacyDN;
			}
			else
			{
				accessingUserDn = delegateMailboxSession.MailboxOwnerLegacyDN;
			}
			if (delegateMailboxSession.Identity is WindowsIdentity)
			{
				accessInfo = new MailboxAccessInfo(accessingUserDn, new WindowsPrincipal((WindowsIdentity)delegateMailboxSession.Identity));
			}
			else if (delegateMailboxSession.Identity is ClientIdentityInfo)
			{
				if (principal.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox && principal.MailboxInfo.MailboxGuid != delegateMailboxSession.MailboxOwner.MailboxInfo.MailboxGuid)
				{
					throw new AccessDeniedException(ServerStrings.AttemptingSessionCreationAgainstWrongGroupMailbox(principal.MailboxInfo.PrimarySmtpAddress.ToString(), delegateMailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
				}
				accessInfo = new MailboxAccessInfo(accessingUserDn, (ClientIdentityInfo)delegateMailboxSession.Identity);
			}
			LogonType logonType = (delegateMailboxSession.LogonType == LogonType.Admin) ? LogonType.Admin : LogonType.BestAccess;
			MailboxSession mailboxSession = MailboxSession.ConfigurableOpen(principal, accessInfo, delegateMailboxSession.InternalPreferedCulture, clientInfoString, logonType, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders, null, false, null, delegateMailboxSession.unifiedGroupMemberType);
			mailboxSession.ExTimeZone = delegateMailboxSession.ExTimeZone;
			mailboxSession.MasterMailboxSession = delegateMailboxSession;
			mailboxSession.SetClientIPEndpoints(delegateMailboxSession.ClientIPAddress, delegateMailboxSession.ServerIPAddress);
			return mailboxSession;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009F32 File Offset: 0x00008132
		private void CheckMasterSessionForCalendarDelegate()
		{
			if (!base.IsConnected)
			{
				throw new InvalidOperationException(string.Format("The master mailbox session is not connected.", new object[0]));
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009F52 File Offset: 0x00008152
		private static void CheckNoRemoteExchangePrincipal(IExchangePrincipal ep)
		{
			if (ep.MailboxInfo.IsRemote)
			{
				throw new NotSupportedException("This operation is not supported for remote connections.");
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009F6C File Offset: 0x0000816C
		public void SetReceiveFolder(string messageClass, StoreObjectId folderId)
		{
			using (this.CheckObjectState("SetReceiveFolder"))
			{
				if (messageClass == null)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "MailboxSession::SetReceiveFolder. SetReceiveFolder cannot be called with messageClass being null.");
					throw new ArgumentNullException("messageClass");
				}
				if (folderId == null || folderId.ObjectType != StoreObjectType.Folder)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "MailboxSession::SetReceiveFolder. SetReceiveFolder called with an invaild folder id parameter.");
					throw new ArgumentException("folderId");
				}
				byte[] providerLevelItemId = folderId.ProviderLevelItemId;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					base.Mailbox.MapiStore.SetReceiveFolder(messageClass, providerLevelItemId);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReceiveFolder, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::SetReceiveFolder.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReceiveFolder, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::SetReceiveFolder.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A150 File Offset: 0x00008350
		public void ClearReceiveFolder(string messageClass)
		{
			using (this.CheckObjectState("ClearReceiveFolder"))
			{
				if (string.IsNullOrEmpty(messageClass))
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "MailboxSession::ClearReceiveFolder. ClearReceiveFolder cannot be called with messageClass being null or empty.");
					if (messageClass == null)
					{
						throw new ArgumentNullException("messageClass");
					}
					throw new ArgumentException("messageClass");
				}
				else
				{
					bool flag = false;
					try
					{
						if (this != null)
						{
							this.BeginMapiCall();
							this.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						base.Mailbox.MapiStore.SetReceiveFolder(messageClass, null);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReceiveFolder, ex, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession::SetReceiveFolder.", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReceiveFolder, ex2, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession::SetReceiveFolder.", new object[0]),
							ex2
						});
					}
					finally
					{
						try
						{
							if (this != null)
							{
								this.EndMapiCall();
								if (flag)
								{
									this.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A310 File Offset: 0x00008510
		public StoreObjectId GetReceiveFolder(string messageClass)
		{
			StoreObjectId receiveFolderId;
			using (base.CreateSessionGuard("GetReceiveFolder"))
			{
				string text;
				receiveFolderId = this.GetReceiveFolderId(messageClass, out text);
			}
			return receiveFolderId;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A354 File Offset: 0x00008554
		public ReceiveFolderInfo[] GetReceiveFolderInfo()
		{
			ReceiveFolderInfo[] result;
			using (this.CheckObjectState("GetReceiveFolderInfo"))
			{
				PropValue[][] array = null;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					array = base.Mailbox.MapiStore.GetReceiveFolderInfo();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetReceiveFolderInfo, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetReceiveFolderInfo", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetReceiveFolderInfo, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetReceiveFolderInfo", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				ReceiveFolderInfo[] array2 = new ReceiveFolderInfo[array.Length];
				for (long num = 0L; num < (long)array.Length; num += 1L)
				{
					checked
					{
						PropValue[] array3 = array[(int)((IntPtr)num)];
						array2[(int)((IntPtr)num)] = new ReceiveFolderInfo(array3[0].GetBytes(), array3[1].GetString(), new ExDateTime(this.ExTimeZone, array3[2].GetDateTime()));
					}
				}
				result = array2;
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A550 File Offset: 0x00008750
		public StoreObjectId GetReceiveFolderId(string messageClass, out string explicitMessageClass)
		{
			StoreObjectId result;
			using (this.CheckObjectState("GetReceiveFolderId"))
			{
				byte[] array = null;
				string text = null;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					array = base.Mailbox.MapiStore.GetReceiveFolderEntryId(messageClass, out text);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetReceiveFolder, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetReceiveFolderId.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetReceiveFolder, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetReceiveFolderId.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				explicitMessageClass = text;
				if (array != null && array.Length > 0)
				{
					result = StoreObjectId.FromProviderSpecificId(array, StoreObjectType.Folder);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A700 File Offset: 0x00008900
		public StoreObjectId GetTransportQueueFolderId()
		{
			StoreObjectId result;
			using (this.CheckObjectState("GetTransportQueueFolderId"))
			{
				byte[] array = null;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					array = base.Mailbox.MapiStore.GetTransportQueueFolderId();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetTransportQueueFolderId, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetTransportQueueFolderId.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetTransportQueueFolderId, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetTransportQueueFolderId.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				if (array != null && array.Length > 0)
				{
					result = StoreObjectId.FromProviderSpecificId(array, StoreObjectType.Folder);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A868 File Offset: 0x00008A68
		public void UpdateDeferredActionMessages(byte[] serverSvrEId, byte[] clientSvrEId)
		{
			ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxSession::UpdateDeferredActionMessages. Operation started. mailbox = {0}.", this.mailboxOwner.LegacyDn);
			using (this.CheckObjectState("UpdateDeferredActionMessages"))
			{
				ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.OriginalMessageSvrEId, serverSvrEId);
				try
				{
					using (Folder folder = Folder.Bind(this, DefaultFolderType.DeferredActionFolder))
					{
						List<StoreObjectId> list = new List<StoreObjectId>();
						using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, MailboxSession.DeferredActionMessagesDefinitions))
						{
							byte[] array = (clientSvrEId != null) ? IdConverter.CreateEntryIdFromForeignServerId(clientSvrEId) : null;
							while (queryResult.SeekToCondition(SeekReference.OriginCurrent, seekFilter))
							{
								object[][] rows = queryResult.GetRows(1);
								if (rows.Length != 1)
								{
									break;
								}
								StoreObjectId objectId = ((VersionedId)rows[0][0]).ObjectId;
								if (array != null && array.Length != 0)
								{
									using (Item item = Item.Bind(this, objectId))
									{
										item.OpenAsReadWrite();
										item[ItemSchema.OriginalMessageEntryId] = array;
										item[ItemSchema.DeferredActionMessageBackPatched] = true;
										item.Save(SaveMode.NoConflictResolutionForceSave);
										continue;
									}
								}
								list.Add(objectId);
							}
							if (list.Count > 0)
							{
								folder.DeleteObjects(DeleteItemFlags.HardDelete, list.ToArray());
							}
						}
					}
				}
				finally
				{
					StoreObjectId storeObjectId = base.IdConverter.CreateMessageIdFromSvrEId(serverSvrEId);
					using (Folder folder2 = Folder.Bind(this, DefaultFolderType.AllItems))
					{
						folder2.DeleteObjects(DeleteItemFlags.SoftDelete, new StoreId[]
						{
							storeObjectId
						});
					}
				}
			}
			ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxSession::UpdateDeferredActionMessages. Operation succeeded. mailbox = {0}.", this.mailboxOwner.LegacyDn);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public DefaultFolderType IsDefaultFolderType(StoreId folderId)
		{
			DefaultFolderType result;
			using (this.CheckObjectState("IsDefaultFolderType"))
			{
				ExTraceGlobals.SessionTracer.Information<StoreId>((long)this.GetHashCode(), "MailboxSession::IsDefaultFolderType. FolderId = {0}.", folderId);
				if (folderId == null)
				{
					throw new ArgumentNullException("folderId");
				}
				if (this.isDefaultFolderManagerBeingInitialized)
				{
					ExTraceGlobals.SessionTracer.TraceError<string, StoreId>((long)this.GetHashCode(), "MailboxSession::IsDefaultFolderType. The method is called when the default folders are not initialized completely yet. Mailbox = {0}, folderId = {1}.", this.MailboxOwnerLegacyDN, folderId);
					result = DefaultFolderType.None;
				}
				else
				{
					if (this.defaultFolderManager == null)
					{
						throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
					}
					result = this.defaultFolderManager.IsDefaultFolderType(folderId);
				}
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000AB50 File Offset: 0x00008D50
		public StoreObjectId CreateDefaultFolder(DefaultFolderType defaultFolderType)
		{
			StoreObjectId result;
			using (this.CheckObjectState("CreateDefaultFolder"))
			{
				base.CheckCapabilities(base.Capabilities.CanCreateDefaultFolders, "CanCreateDefaultFolders");
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				result = this.defaultFolderManager.CreateDefaultFolder(defaultFolderType);
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public override bool TryFixDefaultFolderId(DefaultFolderType defaultFolderType, out StoreObjectId id)
		{
			bool result;
			using (this.CheckObjectState("TryFixDefaultFolderId"))
			{
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
				if (!base.Capabilities.CanCreateDefaultFolders)
				{
					id = null;
					result = false;
				}
				else if (this.defaultFolderManager == null)
				{
					id = null;
					result = false;
				}
				else
				{
					result = this.defaultFolderManager.TryFixDefaultFolderId(defaultFolderType, out id);
				}
			}
			return result;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AC48 File Offset: 0x00008E48
		public StoreObjectId RefreshDefaultFolder(DefaultFolderType defaultFolderType)
		{
			StoreObjectId result;
			using (this.CheckObjectState("RefreshDefaultFolder"))
			{
				base.CheckCapabilities(base.Capabilities.CanCreateDefaultFolders, "CanCreateDefaultFolders");
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				result = this.defaultFolderManager.RefreshDefaultFolder(defaultFolderType);
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		public void DeleteDefaultFolder(DefaultFolderType defaultFolderType, DeleteItemFlags deleteItemFlags)
		{
			using (this.CheckObjectState("DeleteDefaultFolder"))
			{
				base.CheckCapabilities(base.Capabilities.CanCreateDefaultFolders, "CanCreateDefaultFolders");
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				this.CheckSystemFolderAccess(this.defaultFolderManager.GetDefaultFolderId(defaultFolderType));
				this.defaultFolderManager.DeleteDefaultFolder(defaultFolderType, deleteItemFlags);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public StoreObjectId CreateSystemFolder()
		{
			StoreObjectId result;
			using (this.CheckObjectState("CreateSystemFolder"))
			{
				if (!this.UseSystemFolder)
				{
					throw new AccessDeniedException(ServerStrings.ExSystemFolderAccessDenied);
				}
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				if (base.LogonType != LogonType.SystemService)
				{
					throw new AccessDeniedException(ServerStrings.ExSystemFolderAccessDenied);
				}
				result = this.defaultFolderManager.CreateDefaultSystemFolder();
			}
			return result;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		public StoreObjectId GetSystemFolderId()
		{
			StoreObjectId defaultFolderId;
			using (this.CheckObjectState("GetSystemFolderId"))
			{
				if (!this.UseSystemFolder)
				{
					throw new AccessDeniedException(ServerStrings.ExSystemFolderAccessDenied);
				}
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				if (base.LogonType != LogonType.SystemService && base.LogonType != LogonType.Transport)
				{
					throw new AccessDeniedException(ServerStrings.ExSystemFolderAccessDenied);
				}
				defaultFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.System);
			}
			return defaultFolderId;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000AE74 File Offset: 0x00009074
		internal bool UseSystemFolder
		{
			get
			{
				bool result;
				using (this.CheckObjectState("UseSystemFolder"))
				{
					result = Util.Contains(this.foldersToInit, DefaultFolderType.System);
				}
				return result;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000AEBC File Offset: 0x000090BC
		public StoreObjectId GetAdminAuditLogsFolderId()
		{
			StoreObjectId defaultFolderId;
			using (this.CheckObjectState("GetAdminAuditLogsFolderId"))
			{
				if (!this.UseAdminAuditLogsFolder)
				{
					throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsFolderAccessDenied);
				}
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				if (!this.bypassAuditsFolderAccessChecking && base.LogonType != LogonType.SystemService)
				{
					throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsFolderAccessDenied);
				}
				defaultFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.AdminAuditLogs);
			}
			return defaultFolderId;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000AF4C File Offset: 0x0000914C
		private static bool IsAdminAuditSession(LogonType logonType, IExchangePrincipal owner, string clientInfoString)
		{
			return owner != null && !string.IsNullOrEmpty(owner.MailboxInfo.PrimarySmtpAddress.ToString()) && !string.IsNullOrEmpty(clientInfoString) && logonType == LogonType.SystemService && owner.MailboxInfo.PrimarySmtpAddress.ToString().Contains("SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}") && clientInfoString.Contains("AdminLog");
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000AFC0 File Offset: 0x000091C0
		internal bool UseAdminAuditLogsFolder
		{
			get
			{
				bool result;
				using (this.CheckObjectState("UseAdminAuditLogsFolder"))
				{
					result = Util.Contains(this.foldersToInit, DefaultFolderType.AdminAuditLogs);
				}
				return result;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B008 File Offset: 0x00009208
		public StoreObjectId GetAuditsFolderId()
		{
			StoreObjectId defaultFolderId;
			using (this.CheckObjectState("GetAuditsFolderId"))
			{
				if (!this.UseAuditsFolder)
				{
					throw new AccessDeniedException(ServerStrings.ExAuditsFolderAccessDenied);
				}
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				if (!this.bypassAuditsFolderAccessChecking && base.LogonType != LogonType.SystemService)
				{
					throw new AccessDeniedException(ServerStrings.ExAuditsFolderAccessDenied);
				}
				defaultFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.Audits);
			}
			return defaultFolderId;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000B098 File Offset: 0x00009298
		internal bool UseAuditsFolder
		{
			get
			{
				bool result;
				using (this.CheckObjectState("UseAuditsFolder"))
				{
					result = Util.Contains(this.foldersToInit, DefaultFolderType.Audits);
				}
				return result;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B0E0 File Offset: 0x000092E0
		public override StoreObjectId GetDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			StoreObjectId defaultFolderId;
			using (this.CheckObjectState("GetDefaultFolderId"))
			{
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
				if (defaultFolderType == DefaultFolderType.System)
				{
					throw new InvalidOperationException(ServerStrings.ExCannotAccessSystemFolderId);
				}
				if (defaultFolderType == DefaultFolderType.AdminAuditLogs)
				{
					throw new InvalidOperationException(ServerStrings.ExCannotAccessAdminAuditLogsFolderId);
				}
				if (defaultFolderType == DefaultFolderType.Audits)
				{
					throw new InvalidOperationException(ServerStrings.ExCannotAccessAuditsFolderId);
				}
				if (this.defaultFolderManager == null)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				defaultFolderId = this.defaultFolderManager.GetDefaultFolderId(defaultFolderType);
			}
			return defaultFolderId;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B18C File Offset: 0x0000938C
		public VersionedId GetLocalFreeBusyMessageId(StoreObjectId freeBusyFolderId)
		{
			VersionedId result;
			using (this.CheckObjectState("GetLocalFreeBusyMessageId"))
			{
				if (freeBusyFolderId != null)
				{
					using (Folder folder = Folder.Bind(this, freeBusyFolderId))
					{
						using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, MailboxSession.ItemSchemaIdStoreDefinition))
						{
							if (queryResult.SeekToCondition(SeekReference.OriginBeginning, MailboxSession.FreeBusyQueryFilter))
							{
								object[][] rows = queryResult.GetRows(1);
								if (rows != null && rows.Length > 0)
								{
									return PropertyBag.CheckPropertyValue<VersionedId>(ItemSchema.Id, rows[0][0]);
								}
							}
						}
					}
				}
				ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::GetLocalFreeBusyMessageId. No FreeBusyMessage was found from the FreeBusy folder. Mailbox = {0}.", this.InternalMailboxOwner);
				result = null;
			}
			return result;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B268 File Offset: 0x00009468
		internal DefaultFolder InternalGetDefaultFolder(DefaultFolderType defaultFolderType)
		{
			DefaultFolder defaultFolder;
			using (base.CreateSessionGuard("InternalGetDefaultFolder"))
			{
				defaultFolder = this.defaultFolderManager.GetDefaultFolder(defaultFolderType);
			}
			return defaultFolder;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public OperationResult LocalizeDefaultFolders(out Exception[] problems)
		{
			OperationResult result;
			using (this.CheckObjectState("LocalizeDefaultFolders"))
			{
				if (this.defaultFolderManager == null || base.LogonType == LogonType.Delegated || (base.StoreFlag & OpenStoreFlag.NoLocalization) == OpenStoreFlag.NoLocalization)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				result = this.defaultFolderManager.Localize(out problems);
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B330 File Offset: 0x00009530
		public bool VerifyDefaultFolderLocalization()
		{
			bool result;
			using (this.CheckObjectState("VerifyDefaultFolderLocalization"))
			{
				if (this.defaultFolderManager == null || (base.StoreFlag & OpenStoreFlag.NoLocalization) == OpenStoreFlag.NoLocalization)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				result = this.defaultFolderManager.VerifyLocalization();
			}
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public PropertyError RenameDefaultFolder(DefaultFolderType folderType, string newName)
		{
			PropertyError result;
			using (this.CheckObjectState("RenameDefaultFolder"))
			{
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(folderType, DefaultFolderType.ElcRoot);
				if (newName == null)
				{
					throw new ArgumentNullException("newName");
				}
				if (this.defaultFolderManager == null || base.LogonType != LogonType.Admin)
				{
					throw new InvalidOperationException(ServerStrings.ExDefaultFoldersNotInitialized);
				}
				if (folderType == DefaultFolderType.System && base.LogonType != LogonType.SystemService && base.LogonType != LogonType.Transport)
				{
					throw new InvalidOperationException(ServerStrings.ExSystemFolderAccessDenied);
				}
				if (folderType == DefaultFolderType.AdminAuditLogs && base.LogonType != LogonType.SystemService)
				{
					throw new InvalidOperationException(ServerStrings.ExAdminAuditLogsFolderAccessDenied);
				}
				if (folderType == DefaultFolderType.Audits && base.LogonType != LogonType.SystemService)
				{
					throw new InvalidOperationException(ServerStrings.ExAuditsFolderAccessDenied);
				}
				DefaultFolder defaultFolder = this.InternalGetDefaultFolder(folderType);
				PropertyError propertyError;
				if (!defaultFolder.Rename(newName, out propertyError))
				{
					result = propertyError;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000B494 File Offset: 0x00009694
		public DefaultFolderType[] DefaultFolders
		{
			get
			{
				DefaultFolderType[] defaultFolderInitializationOrder;
				using (this.CheckObjectState("DefaultFolders::get"))
				{
					defaultFolderInitializationOrder = DefaultFolderManager.defaultFolderInitializationOrder;
				}
				return defaultFolderInitializationOrder;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000B4D4 File Offset: 0x000096D4
		IUserConfigurationManager IMailboxSession.UserConfigurationManager
		{
			get
			{
				return this.UserConfigurationManager;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B4DC File Offset: 0x000096DC
		public UserConfigurationManager UserConfigurationManager
		{
			get
			{
				UserConfigurationManager result;
				using (this.CheckObjectState("UserConfigurationManager::get"))
				{
					base.CheckCapabilities(base.Capabilities.CanHaveUserConfigurationManager, "CanHaveUserConfigurationManager");
					result = this.userConfigurationManager;
				}
				return result;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000B534 File Offset: 0x00009734
		public MapiStore __ContainedMapiStore
		{
			get
			{
				MapiStore mapiStore;
				using (this.CheckObjectState("__ContainedMapiStore::get"))
				{
					mapiStore = base.Mailbox.MapiStore;
				}
				return mapiStore;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B57C File Offset: 0x0000977C
		public override string GccResourceIdentifier
		{
			get
			{
				string result;
				using (base.CheckDisposed("GccResourceIdentifier::get"))
				{
					result = this.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
				}
				return result;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000B5D8 File Offset: 0x000097D8
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000B61C File Offset: 0x0000981C
		public override ExTimeZone ExTimeZone
		{
			get
			{
				ExTimeZone result;
				using (base.CheckDisposed("ExTimeZone::get"))
				{
					result = this.exTimeZone;
				}
				return result;
			}
			set
			{
				using (base.CheckDisposed("ExTimeZone::set"))
				{
					if (value == null)
					{
						throw new ArgumentNullException("ExTimeZone");
					}
					this.exTimeZone = value;
					this.delegateSessionManager.SetTimeZone(this.exTimeZone);
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B67C File Offset: 0x0000987C
		public ExternalUserCollection GetExternalUsers()
		{
			ExternalUserCollection result;
			using (this.CheckObjectState("GetExternalUsers"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveExternalUsers, "CanHaveExternalUsers");
				result = new ExternalUserCollection(this);
			}
			return result;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public IADOrgPerson DelegateUser
		{
			get
			{
				IADOrgPerson result;
				using (base.CheckDisposed("DelegateUser::get"))
				{
					base.CheckCapabilities(base.Capabilities.CanHaveDelegateUsers, "CanHaveDelegateUsers");
					result = this.delegateUser;
				}
				return result;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000B72C File Offset: 0x0000992C
		public string MailboxOwnerLegacyDN
		{
			get
			{
				string mailboxLegacyDn;
				using (base.CheckDisposed("MailboxOwnerLegacyDN::get"))
				{
					mailboxLegacyDn = this.mailboxOwner.MailboxInfo.GetMailboxLegacyDn(this.mailboxOwner.LegacyDn);
				}
				return mailboxLegacyDn;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B784 File Offset: 0x00009984
		public override IExchangePrincipal MailboxOwner
		{
			get
			{
				IExchangePrincipal result;
				using (base.CheckDisposed("MailboxOwner::get"))
				{
					if (this.mailboxOwner == null)
					{
						throw new InvalidOperationException(ServerStrings.ExNoMailboxOwner);
					}
					result = this.mailboxOwner;
				}
				return result;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public override CultureInfo PreferedCulture
		{
			get
			{
				CultureInfo internalPreferedCulture;
				using (base.CheckDisposed("PreferedCulture::get"))
				{
					base.CheckCapabilities(base.Capabilities.CanHaveCulture, "CanHaveCulture");
					internalPreferedCulture = this.InternalPreferedCulture;
				}
				return internalPreferedCulture;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000B838 File Offset: 0x00009A38
		internal override CultureInfo InternalPreferedCulture
		{
			get
			{
				CultureInfo result;
				using (base.CheckDisposed("InternalPreferedCulture::get"))
				{
					if (this.preferedCultureInfoCache == null)
					{
						CultureInfo[] cultures = this.InternalGetMailboxCultures();
						CultureInfo preferedCulture = Util.CultureSelector.GetPreferedCulture(cultures);
						this.preferedCultureInfoCache = MapiCultureInfo.AdjustFromClientRequest(base.InternalPreferedCulture, preferedCulture);
						ExTraceGlobals.StorageTracer.TraceError<CultureInfo>((long)this.GetHashCode(), "Picked PreferedCulture: {0}.", this.preferedCultureInfoCache);
					}
					result = this.preferedCultureInfoCache;
				}
				return result;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public override string DisplayAddress
		{
			get
			{
				return this.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		public bool CanActAsOwner
		{
			get
			{
				bool result;
				using (base.CheckDisposed("CanActAsOwner::get"))
				{
					bool? flag = base.Mailbox.TryGetProperty(InternalSchema.CanActAsOwner) as bool?;
					if (flag == null)
					{
						result = false;
					}
					else
					{
						result = flag.Value;
					}
				}
				return result;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000B95C File Offset: 0x00009B5C
		public bool CanSendAs
		{
			get
			{
				bool result;
				using (base.CheckDisposed("CanSendAs::get"))
				{
					bool? flag = base.Mailbox.TryGetProperty(InternalSchema.CanSendAs) as bool?;
					if (flag == null)
					{
						result = false;
					}
					else
					{
						result = flag.Value;
					}
				}
				return result;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public bool IsMailboxLocalized
		{
			get
			{
				bool result;
				using (base.CheckDisposed("IsMailboxLocalized::get"))
				{
					if (this.mailboxProperties != null && !this.mailboxProperties.Contains(InternalSchema.IsMailboxLocalized))
					{
						result = true;
					}
					else
					{
						bool? flag = base.Mailbox.TryGetProperty(InternalSchema.IsMailboxLocalized) as bool?;
						if (flag == null)
						{
							result = false;
						}
						else
						{
							result = flag.Value;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BA50 File Offset: 0x00009C50
		public bool ConnectWithStatus()
		{
			bool result;
			using (base.CheckDisposed("ConnectWithStatus"))
			{
				bool flag = false;
				if (base.IsConnected)
				{
					ExTraceGlobals.SessionTracer.TraceError<string, Type, int>((long)this.GetHashCode(), "MailboxSession::{0}. Object type = {1}, hashcode = {2} trying to call Connect when already connected.", "Connect", base.GetType(), this.GetHashCode());
					throw new ConnectionFailedPermanentException(ServerStrings.ExAlreadyConnected);
				}
				ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "MailboxSession::Connect.");
				bool flag2 = false;
				try
				{
					if (base.IsDead)
					{
						bool flag3 = base.StopDeadSessionChecking();
						this.ForceOpen(null);
						if (flag3)
						{
							base.StartDeadSessionChecking();
						}
						flag = true;
					}
					base.IsConnected = true;
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "MailboxSession::Connect. Operation failed. mailbox = {0}.", this.mailboxOwner.LegacyDn);
					}
					else
					{
						ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxSession::Connect. Operation succeeded. mailbox = {0}.", this.mailboxOwner.LegacyDn);
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BB64 File Offset: 0x00009D64
		public override void Connect()
		{
			using (base.CreateSessionGuard("Connect"))
			{
				this.ConnectWithStatus();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		protected override MapiStore ForceOpen(MapiStore linkedStore)
		{
			return this.ForceOpen(linkedStore, false);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		protected MapiStore ForceOpen(MapiStore linkedStore, bool unifiedSession)
		{
			MapiStore result;
			using (base.CreateSessionGuard("ForceOpen"))
			{
				bool flag = false;
				MapiStore mapiStore = null;
				ClientSecurityContext clientSecurityContext = null;
				try
				{
					if (this.ShouldThrowWrongServerException(this.mailboxOwner))
					{
						MapiExceptionMailboxInTransit innerException = new MapiExceptionMailboxInTransit("Detected site violation", 0, 1292, null, null);
						throw new WrongServerException(ServerStrings.PrincipalFromDifferentSite, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.mailboxOwner.MailboxInfo.Location.ServerVersion, innerException);
					}
					if (MailboxSession.IsMemberLogonToGroupMailbox(this.mailboxOwner, base.LogonType))
					{
						if (base.UnifiedGroupMemberType == UnifiedGroupMemberType.Unknown)
						{
							if (ExTraceGlobals.GroupMailboxSessionTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.GroupMailboxSessionTracer.TraceError<StackTrace>((long)this.GetHashCode(), "GroupMailboxAccess: Not a valid way to create session for the group mailbox: {0}", new StackTrace(true));
							}
							throw new AccessDeniedException(ServerStrings.InvalidMechanismToAccessGroupMailbox);
						}
						base.StoreFlag |= OpenStoreFlag.ShowAllFIDCs;
					}
					byte[] tenantHint = StoreSession.GetTenantHint(this.mailboxOwner);
					bool flag2 = false;
					if (this.mailboxOwner.MailboxInfo.IsArchive && this.mailboxOwner.MailboxInfo.IsRemote)
					{
						flag2 = true;
						if ((this.mailboxOwner.MailboxInfo.ArchiveStatus & ArchiveStatusFlags.Active) != ArchiveStatusFlags.Active)
						{
							throw new MailboxOfflineException(ServerStrings.RemoteArchiveOffline);
						}
					}
					flag2 = (flag2 || (!this.mailboxOwner.MailboxInfo.IsArchive && this.mailboxOwner.MailboxInfo.IsRemote));
					if (flag2)
					{
						mapiStore = this.InternalGetRemoteConnection();
					}
					else
					{
						this.connectFlag |= ConnectFlag.UseRpcContextPool;
						if (StoreSession.UseRPCContextPoolResiliency)
						{
							this.connectFlag |= ConnectFlag.UseResiliency;
						}
						WindowsIdentity windowsIdentity = this.identity as WindowsIdentity;
						if (windowsIdentity == null)
						{
							ClientIdentityInfo clientIdentityInfo = this.identity as ClientIdentityInfo;
							if (clientIdentityInfo != null)
							{
								if (this.mailboxOwner.MailboxInfo.IsArchive)
								{
									base.StoreFlag |= OpenStoreFlag.MailboxGuid;
									if (linkedStore == null)
									{
										bool flag3 = false;
										try
										{
											try
											{
												if (this != null)
												{
													this.BeginMapiCall();
													this.BeginServerHealthCall();
													flag3 = true;
												}
												if (StorageGlobals.MapiTestHookBeforeCall != null)
												{
													StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
												}
												mapiStore = MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, clientIdentityInfo, base.ClientInfoString, tenantHint, unifiedSession);
											}
											catch (MapiPermanentException ex)
											{
												throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex, this, this, "{0}. MapiException = {1}.", new object[]
												{
													string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
													{
														this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
														this.userLegacyDn,
														this.mailboxOwner.LegacyDn,
														this.mailboxOwner.MailboxInfo.MailboxGuid
													}),
													ex
												});
											}
											catch (MapiRetryableException ex2)
											{
												throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex2, this, this, "{0}. MapiException = {1}.", new object[]
												{
													string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
													{
														this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
														this.userLegacyDn,
														this.mailboxOwner.LegacyDn,
														this.mailboxOwner.MailboxInfo.MailboxGuid
													}),
													ex2
												});
											}
											goto IL_1790;
										}
										finally
										{
											try
											{
												if (this != null)
												{
													this.EndMapiCall();
													if (flag3)
													{
														this.EndServerHealthCall();
													}
												}
											}
											finally
											{
												if (StorageGlobals.MapiTestHookAfterCall != null)
												{
													StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
												}
											}
										}
									}
									bool flag4 = false;
									try
									{
										try
										{
											if (this != null)
											{
												this.BeginMapiCall();
												this.BeginServerHealthCall();
												flag4 = true;
											}
											if (StorageGlobals.MapiTestHookBeforeCall != null)
											{
												StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
											}
											mapiStore = linkedStore.OpenAlternateMailbox(this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, tenantHint);
										}
										catch (MapiPermanentException ex3)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex3, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
												{
													this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
													this.userLegacyDn,
													this.mailboxOwner.LegacyDn,
													this.mailboxOwner.MailboxInfo.MailboxGuid
												}),
												ex3
											});
										}
										catch (MapiRetryableException ex4)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex4, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
												{
													this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
													this.userLegacyDn,
													this.mailboxOwner.LegacyDn,
													this.mailboxOwner.MailboxInfo.MailboxGuid
												}),
												ex4
											});
										}
										goto IL_1790;
									}
									finally
									{
										try
										{
											if (this != null)
											{
												this.EndMapiCall();
												if (flag4)
												{
													this.EndServerHealthCall();
												}
											}
										}
										finally
										{
											if (StorageGlobals.MapiTestHookAfterCall != null)
											{
												StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
											}
										}
									}
								}
								if (this.mailboxOwner.MailboxInfo.IsAggregated)
								{
									mapiStore = this.InternalGetAggregatedMailboxConnection(linkedStore, clientIdentityInfo, tenantHint);
									goto IL_1790;
								}
								if (this.mailboxOwner.MailboxInfo.MailboxGuid != Guid.Empty && !this.mailboxOwner.MailboxInfo.MailboxDatabase.IsNullOrEmpty())
								{
									base.StoreFlag |= OpenStoreFlag.MailboxGuid;
									if (this.mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
									{
										clientSecurityContext = ClientSecurityContext.DuplicateAuthZContextHandle(clientIdentityInfo.hAuthZ);
										GroupMailboxAuthorizationHandler.AddGroupMailboxAccessSid(clientSecurityContext, this.mailboxOwner.MailboxInfo.MailboxGuid, base.UnifiedGroupMemberType);
										clientIdentityInfo = new ClientIdentityInfo(clientSecurityContext.ClientContextHandle.DangerousGetHandle(), clientIdentityInfo.sidUser, clientIdentityInfo.sidPrimaryGroup);
									}
									ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Use user's MailboxGuid. MailboxGuid = {0}.", this.InternalMailboxOwner);
									if (linkedStore == null)
									{
										bool flag5 = false;
										try
										{
											try
											{
												if (this != null)
												{
													this.BeginMapiCall();
													this.BeginServerHealthCall();
													flag5 = true;
												}
												if (StorageGlobals.MapiTestHookBeforeCall != null)
												{
													StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
												}
												mapiStore = MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, clientIdentityInfo, base.ClientInfoString, tenantHint, unifiedSession);
											}
											catch (MapiPermanentException ex5)
											{
												throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex5, this, this, "{0}. MapiException = {1}.", new object[]
												{
													string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
													ex5
												});
											}
											catch (MapiRetryableException ex6)
											{
												throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex6, this, this, "{0}. MapiException = {1}.", new object[]
												{
													string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
													ex6
												});
											}
											goto IL_1790;
										}
										finally
										{
											try
											{
												if (this != null)
												{
													this.EndMapiCall();
													if (flag5)
													{
														this.EndServerHealthCall();
													}
												}
											}
											finally
											{
												if (StorageGlobals.MapiTestHookAfterCall != null)
												{
													StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
												}
											}
										}
									}
									bool flag6 = false;
									try
									{
										try
										{
											if (this != null)
											{
												this.BeginMapiCall();
												this.BeginServerHealthCall();
												flag6 = true;
											}
											if (StorageGlobals.MapiTestHookBeforeCall != null)
											{
												StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
											}
											mapiStore = linkedStore.OpenAlternateMailbox(this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, tenantHint);
										}
										catch (MapiPermanentException ex7)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex7, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex7
											});
										}
										catch (MapiRetryableException ex8)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex8, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex8
											});
										}
										goto IL_1790;
									}
									finally
									{
										try
										{
											if (this != null)
											{
												this.EndMapiCall();
												if (flag6)
												{
													this.EndServerHealthCall();
												}
											}
										}
										finally
										{
											if (StorageGlobals.MapiTestHookAfterCall != null)
											{
												StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
											}
										}
									}
								}
								base.StoreFlag &= ~OpenStoreFlag.MailboxGuid;
								if (linkedStore == null)
								{
									bool flag7 = false;
									try
									{
										try
										{
											if (this != null)
											{
												this.BeginMapiCall();
												this.BeginServerHealthCall();
												flag7 = true;
											}
											if (StorageGlobals.MapiTestHookBeforeCall != null)
											{
												StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
											}
											mapiStore = MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn, null, null, null, this.connectFlag, base.StoreFlag, clientIdentityInfo, this.InternalPreferedCulture, base.ClientInfoString, tenantHint, unifiedSession);
										}
										catch (MapiPermanentException ex9)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex9, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex9
											});
										}
										catch (MapiRetryableException ex10)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex10, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex10
											});
										}
										goto IL_1790;
									}
									finally
									{
										try
										{
											if (this != null)
											{
												this.EndMapiCall();
												if (flag7)
												{
													this.EndServerHealthCall();
												}
											}
										}
										finally
										{
											if (StorageGlobals.MapiTestHookAfterCall != null)
											{
												StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
											}
										}
									}
								}
								bool flag8 = false;
								try
								{
									try
									{
										if (this != null)
										{
											this.BeginMapiCall();
											this.BeginServerHealthCall();
											flag8 = true;
										}
										if (StorageGlobals.MapiTestHookBeforeCall != null)
										{
											StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
										}
										mapiStore = linkedStore.OpenAlternateMailbox(this.mailboxOwner.LegacyDn, base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, tenantHint);
									}
									catch (MapiPermanentException ex11)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex11, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
											ex11
										});
									}
									catch (MapiRetryableException ex12)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex12, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
											ex12
										});
									}
									goto IL_1790;
								}
								finally
								{
									try
									{
										if (this != null)
										{
											this.EndMapiCall();
											if (flag8)
											{
												this.EndServerHealthCall();
											}
										}
									}
									finally
									{
										if (StorageGlobals.MapiTestHookAfterCall != null)
										{
											StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
										}
									}
								}
							}
							throw new NotSupportedException(string.Format("The type of the identity  {0} is not supported.", this.identity.GetType()));
						}
						if (this.mailboxOwner.MailboxInfo.IsArchive)
						{
							base.StoreFlag |= OpenStoreFlag.MailboxGuid;
							ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Use user's MailboxGuid. MailboxGuid = {0}.", this.InternalMailboxOwner);
							if (linkedStore == null)
							{
								bool flag9 = false;
								try
								{
									try
									{
										if (this != null)
										{
											this.BeginMapiCall();
											this.BeginServerHealthCall();
											flag9 = true;
										}
										if (StorageGlobals.MapiTestHookBeforeCall != null)
										{
											StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
										}
										mapiStore = MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, windowsIdentity, base.ClientInfoString, tenantHint, unifiedSession);
									}
									catch (MapiPermanentException ex13)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex13, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
											{
												this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
												this.userLegacyDn,
												this.mailboxOwner.LegacyDn,
												this.mailboxOwner.MailboxInfo.MailboxGuid
											}),
											ex13
										});
									}
									catch (MapiRetryableException ex14)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex14, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
											{
												this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
												this.userLegacyDn,
												this.mailboxOwner.LegacyDn,
												this.mailboxOwner.MailboxInfo.MailboxGuid
											}),
											ex14
										});
									}
									goto IL_1790;
								}
								finally
								{
									try
									{
										if (this != null)
										{
											this.EndMapiCall();
											if (flag9)
											{
												this.EndServerHealthCall();
											}
										}
									}
									finally
									{
										if (StorageGlobals.MapiTestHookAfterCall != null)
										{
											StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
										}
									}
								}
							}
							bool flag10 = false;
							try
							{
								try
								{
									if (this != null)
									{
										this.BeginMapiCall();
										this.BeginServerHealthCall();
										flag10 = true;
									}
									if (StorageGlobals.MapiTestHookBeforeCall != null)
									{
										StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
									}
									mapiStore = linkedStore.OpenAlternateMailbox(this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, tenantHint);
								}
								catch (MapiPermanentException ex15)
								{
									throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex15, this, this, "{0}. MapiException = {1}.", new object[]
									{
										string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
										{
											this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
											this.userLegacyDn,
											this.mailboxOwner.LegacyDn,
											this.mailboxOwner.MailboxInfo.MailboxGuid
										}),
										ex15
									});
								}
								catch (MapiRetryableException ex16)
								{
									throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex16, this, this, "{0}. MapiException = {1}.", new object[]
									{
										string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, archive Guid = {3}.", new object[]
										{
											this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
											this.userLegacyDn,
											this.mailboxOwner.LegacyDn,
											this.mailboxOwner.MailboxInfo.MailboxGuid
										}),
										ex16
									});
								}
								goto IL_1790;
							}
							finally
							{
								try
								{
									if (this != null)
									{
										this.EndMapiCall();
										if (flag10)
										{
											this.EndServerHealthCall();
										}
									}
								}
								finally
								{
									if (StorageGlobals.MapiTestHookAfterCall != null)
									{
										StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
									}
								}
							}
						}
						if (!this.mailboxOwner.MailboxInfo.IsAggregated)
						{
							if (this.mailboxOwner.MailboxInfo.MailboxGuid != Guid.Empty && !this.mailboxOwner.MailboxInfo.MailboxDatabase.IsNullOrEmpty())
							{
								base.StoreFlag |= OpenStoreFlag.MailboxGuid;
								ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Use user's MailboxGuid. MailboxGuid = {0}.", this.InternalMailboxOwner);
								if (linkedStore == null)
								{
									bool flag11 = false;
									try
									{
										try
										{
											if (this != null)
											{
												this.BeginMapiCall();
												this.BeginServerHealthCall();
												flag11 = true;
											}
											if (StorageGlobals.MapiTestHookBeforeCall != null)
											{
												StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
											}
											mapiStore = MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, windowsIdentity, base.ClientInfoString, tenantHint, unifiedSession);
										}
										catch (MapiPermanentException ex17)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex17, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex17
											});
										}
										catch (MapiRetryableException ex18)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex18, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex18
											});
										}
										goto IL_1790;
									}
									finally
									{
										try
										{
											if (this != null)
											{
												this.EndMapiCall();
												if (flag11)
												{
													this.EndServerHealthCall();
												}
											}
										}
										finally
										{
											if (StorageGlobals.MapiTestHookAfterCall != null)
											{
												StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
											}
										}
									}
								}
								bool flag12 = false;
								try
								{
									try
									{
										if (this != null)
										{
											this.BeginMapiCall();
											this.BeginServerHealthCall();
											flag12 = true;
										}
										if (StorageGlobals.MapiTestHookBeforeCall != null)
										{
											StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
										}
										mapiStore = linkedStore.OpenAlternateMailbox(this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, tenantHint);
									}
									catch (MapiPermanentException ex19)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex19, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
											ex19
										});
									}
									catch (MapiRetryableException ex20)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex20, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
											ex20
										});
									}
									goto IL_1790;
								}
								finally
								{
									try
									{
										if (this != null)
										{
											this.EndMapiCall();
											if (flag12)
											{
												this.EndServerHealthCall();
											}
										}
									}
									finally
									{
										if (StorageGlobals.MapiTestHookAfterCall != null)
										{
											StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
										}
									}
								}
							}
							if (!string.IsNullOrEmpty(this.mailboxOwner.LegacyDn))
							{
								base.StoreFlag &= ~OpenStoreFlag.MailboxGuid;
								ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Use User's LegacyDistinguishedName. LegacyDistinguishedName = {0}.", this.InternalMailboxOwner);
								if (linkedStore == null)
								{
									bool flag13 = false;
									try
									{
										try
										{
											if (this != null)
											{
												this.BeginMapiCall();
												this.BeginServerHealthCall();
												flag13 = true;
											}
											if (StorageGlobals.MapiTestHookBeforeCall != null)
											{
												StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
											}
											mapiStore = MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn, null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, windowsIdentity, base.ClientInfoString, tenantHint, unifiedSession);
										}
										catch (MapiPermanentException ex21)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex21, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex21
											});
										}
										catch (MapiRetryableException ex22)
										{
											throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex22, this, this, "{0}. MapiException = {1}.", new object[]
											{
												string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
												ex22
											});
										}
										goto IL_1790;
									}
									finally
									{
										try
										{
											if (this != null)
											{
												this.EndMapiCall();
												if (flag13)
												{
													this.EndServerHealthCall();
												}
											}
										}
										finally
										{
											if (StorageGlobals.MapiTestHookAfterCall != null)
											{
												StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
											}
										}
									}
								}
								bool flag14 = false;
								try
								{
									try
									{
										if (this != null)
										{
											this.BeginMapiCall();
											this.BeginServerHealthCall();
											flag14 = true;
										}
										if (StorageGlobals.MapiTestHookBeforeCall != null)
										{
											StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
										}
										mapiStore = linkedStore.OpenAlternateMailbox(this.mailboxOwner.LegacyDn, base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, tenantHint);
									}
									catch (MapiPermanentException ex23)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex23, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
											ex23
										});
									}
									catch (MapiRetryableException ex24)
									{
										throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex24, this, this, "{0}. MapiException = {1}.", new object[]
										{
											string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}.", this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.LegacyDn),
											ex24
										});
									}
									goto IL_1790;
								}
								finally
								{
									try
									{
										if (this != null)
										{
											this.EndMapiCall();
											if (flag14)
											{
												this.EndServerHealthCall();
											}
										}
									}
									finally
									{
										if (StorageGlobals.MapiTestHookAfterCall != null)
										{
											StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
										}
									}
								}
							}
							ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "MailboxSession::ForceOpen. The user has neither MailboxGuid nor LegacyDistinguishedName. Owner = {0}.", this.mailboxOwner.MailboxInfo.DisplayName);
							throw new NotSupportedException("Must have legacyDN or Guids");
						}
						mapiStore = this.InternalGetAggregatedMailboxConnection(linkedStore, windowsIdentity, tenantHint);
					}
					IL_1790:
					base.IsDead = false;
					MapiStore mapiStore2 = mapiStore;
					PropertyDefinition[] array = this.mailboxProperties;
					base.SetMailboxStoreObject(MailboxStoreObject.Bind(this, mapiStore2, (array != null) ? ((ICollection<PropertyDefinition>)array) : MailboxSchema.Instance.AllProperties, this.useNamedProperties, this.mailboxProperties != null));
					flag = true;
				}
				finally
				{
					if (clientSecurityContext != null)
					{
						clientSecurityContext.Dispose();
					}
					if (!flag)
					{
						ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Operation failed. mailbox = {0}.", this.mailboxOwner.LegacyDn);
						this.isConnected = false;
						base.SetMailboxStoreObject(null);
						if (mapiStore != null)
						{
							mapiStore.Dispose();
							mapiStore = null;
						}
					}
					else
					{
						ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Operation succeeded. mailbox = {0}.", this.mailboxOwner.LegacyDn);
					}
				}
				result = mapiStore;
			}
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public override void Disconnect()
		{
			using (base.CheckDisposed("Disconnect"))
			{
				ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "MailboxSession::Disconnect.");
				if (!base.IsConnected)
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "MailboxSession::Disconnect. The mailbox has not been connected yet. Mailbox = {0}.", this.MailboxOwnerLegacyDN);
					throw new ConnectionFailedPermanentException(ServerStrings.ExNotConnected);
				}
				ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxSession::Disconnect. Disconnect succeeded. mailbox = {0}.", this.MailboxOwnerLegacyDN);
				base.IsConnected = false;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public MasterCategoryList GetMasterCategoryList()
		{
			return this.GetMasterCategoryList(false);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000D978 File Offset: 0x0000BB78
		public MasterCategoryList GetMasterCategoryList(bool forceReload)
		{
			MasterCategoryList result;
			using (base.CheckDisposed("GetMasterCategoryList"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveMasterCategoryList, "CanHaveMasterCategoryList");
				MasterCategoryList masterCategoryList = this.InternalGetMasterCategoryList();
				masterCategoryList.Load(forceReload);
				result = masterCategoryList;
			}
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public void DeleteMasterCategoryList()
		{
			using (base.CheckDisposed("DeleteMasterCategoryList"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveMasterCategoryList, "CanHaveMasterCategoryList");
				MasterCategoryList.Delete(this);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public void SaveMasterCategoryList()
		{
			using (base.CheckDisposed("SaveMasterCategoryList"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveMasterCategoryList, "CanHaveMasterCategoryList");
				if (this.masterCategoryList != null && this.masterCategoryList.IsLoaded)
				{
					this.masterCategoryList.Save();
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		public AggregateOperationResult UnsafeCopyItemsAndSetProperties(StoreId destinationFolderId, StoreId[] ids, PropertyDefinition[] propertyDefinitions, object[] values)
		{
			AggregateOperationResult result;
			using (this.CheckObjectState("UnsafeCopyItemsAndSetProperties"))
			{
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "MailboxSession::UnsafeCopyItemsAndSetProperties. HashCode = {0}", this.GetHashCode());
				List<GroupOperationResult> list = new List<GroupOperationResult>();
				Dictionary<StoreObjectId, List<StoreId>> dictionary = new Dictionary<StoreObjectId, List<StoreId>>();
				base.GroupNonOccurrenceByFolder(ids, dictionary, list);
				base.ExecuteOperationOnObjects(dictionary, list, delegate(Folder sourceFolder, StoreId[] sourceObjectIds)
				{
					GroupOperationResult groupOperationResult = sourceFolder.UnsafeCopyItemsAndSetProperties(destinationFolderId, sourceObjectIds, propertyDefinitions, values);
					return new AggregateOperationResult(groupOperationResult.OperationResult, new GroupOperationResult[]
					{
						groupOperationResult
					});
				});
				result = Folder.CreateAggregateOperationResult(list);
			}
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public AggregateOperationResult UnsafeMoveItemsAndSetProperties(StoreId destinationFolderId, StoreId[] ids, PropertyDefinition[] propertyDefinitions, object[] values)
		{
			AggregateOperationResult result;
			using (this.CheckObjectState("UnsafeMovItemsAndSetProperties"))
			{
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "MailboxSession::UnsafeMoveItemsAndSetProperties. HashCode = {0}", this.GetHashCode());
				List<GroupOperationResult> list = new List<GroupOperationResult>();
				Dictionary<StoreObjectId, List<StoreId>> dictionary = new Dictionary<StoreObjectId, List<StoreId>>();
				base.GroupNonOccurrenceByFolder(ids, dictionary, list);
				base.ExecuteOperationOnObjects(dictionary, list, delegate(Folder sourceFolder, StoreId[] sourceObjectIds)
				{
					GroupOperationResult groupOperationResult = sourceFolder.UnsafeMoveItemsAndSetProperties(destinationFolderId, sourceObjectIds, propertyDefinitions, values);
					return new AggregateOperationResult(groupOperationResult.OperationResult, new GroupOperationResult[]
					{
						groupOperationResult
					});
				});
				result = Folder.CreateAggregateOperationResult(list);
			}
			return result;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public CultureInfo[] GetMailboxCultures()
		{
			CultureInfo[] result;
			using (base.CreateSessionGuard("GetMailboxCultures"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveCulture, "CanHaveCulture");
				result = this.InternalGetMailboxCultures();
			}
			return result;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		internal CultureInfo[] InternalGetMailboxCultures()
		{
			CultureInfo[] result;
			using (base.CheckDisposed("InternalGetMailboxCultures"))
			{
				List<CultureInfo> list = new List<CultureInfo>();
				if (base.InternalCulture != null)
				{
					list.Add(base.InternalCulture);
				}
				if (this.InternalMailboxOwner != null)
				{
					foreach (CultureInfo item in this.MailboxOwner.PreferredCultures)
					{
						list.Add(item);
					}
					ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::GetMailboxCultures. Get culture from AD for {0}.", this.InternalMailboxOwner);
				}
				list.Add(CultureInfo.CurrentCulture);
				list.Add(MailboxSession.productDefaultCulture);
				result = (from culture in list.Distinct<CultureInfo>()
				where !culture.Equals(CultureInfo.InvariantCulture)
				select culture).ToArray<CultureInfo>();
			}
			return result;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public bool IsGroupMailbox()
		{
			object obj = base.Mailbox.TryGetProperty(MailboxSchema.MailboxTypeDetail);
			return obj is int && StoreSession.IsGroupMailbox((int)obj);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000DE28 File Offset: 0x0000C028
		public bool IsMailboxOof()
		{
			bool result;
			using (this.CheckObjectState("IsMailboxOof"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveOof, "CanHaveOof");
				base.Mailbox.ForceReload(new PropertyDefinition[]
				{
					MailboxSchema.MailboxOofState
				});
				bool? flag = base.Mailbox.TryGetProperty(MailboxSchema.MailboxOofState) as bool?;
				if (flag == null)
				{
					result = false;
				}
				else
				{
					result = flag.Value;
				}
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public Guid GetPerUserGuid(Guid replGuid, byte[] globCount)
		{
			Guid perUserGuid;
			using (this.CheckObjectState("GetPerUserGuid"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					perUserGuid = base.Mailbox.MapiStore.GetPerUserGuid(replGuid, globCount);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetPerUserGuid, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetPerUserGuid.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetPerUserGuid, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetPerUserGuid.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return perUserGuid;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000E014 File Offset: 0x0000C214
		public byte[][] GetPerUserLongTermIds(Guid guid)
		{
			byte[][] perUserLongTermIds;
			using (this.CheckObjectState("GetPerUserLongTermIds"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					perUserLongTermIds = base.Mailbox.MapiStore.GetPerUserLongTermIds(guid);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetPerUserLongTermIds, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetPerUserLongTermIds.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetPerUserLongTermIds, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetPerUserLongTermIds.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return perUserLongTermIds;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000E160 File Offset: 0x0000C360
		public bool GetAllPerUserLongTermIds(byte[] lastLtid, out PerUserData[] perUserDatas)
		{
			bool allPerUserLongTermIds;
			using (this.CheckObjectState("GetAllPerUserLongTermIds"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					allPerUserLongTermIds = base.Mailbox.MapiStore.GetAllPerUserLongTermIds(lastLtid, out perUserDatas);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetAllPerUserLongTermIds, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetAllPerUserLongTermIds.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetAllPerUserLongTermIds, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetAllPerUserLongTermIds.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return allPerUserLongTermIds;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public void PrereadMessages(params StoreId[] storeIds)
		{
			if (storeIds != null)
			{
				byte[][] entryIds = StoreId.StoreIdsToEntryIds(storeIds);
				using (base.CheckDisposed("PrereadMessages"))
				{
					bool flag = false;
					try
					{
						if (this != null)
						{
							this.BeginMapiCall();
							this.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						base.Mailbox.MapiStore.PrereadMessages(entryIds);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSubmitMessage, ex, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession.PrereadMessages", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSubmitMessage, ex2, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession.PrereadMessages", new object[0]),
							ex2
						});
					}
					finally
					{
						try
						{
							if (this != null)
							{
								this.EndMapiCall();
								if (flag)
								{
									this.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000E40C File Offset: 0x0000C60C
		public COWSession CowSession
		{
			get
			{
				COWSession result;
				using (base.CheckDisposed("CowSession::get"))
				{
					result = this.copyOnWriteNotification;
				}
				return result;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000E450 File Offset: 0x0000C650
		public COWSettings COWSettings
		{
			get
			{
				COWSettings result;
				using (base.CheckDisposed("COWSettings::get"))
				{
					result = ((this.copyOnWriteNotification == null) ? null : this.copyOnWriteNotification.Settings);
				}
				return result;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		public ulong? DumpsterSize
		{
			get
			{
				ulong? result;
				using (base.CheckDisposed("DumpsterSize::get"))
				{
					base.Mailbox.ForceReload(new PropertyDefinition[]
					{
						MailboxSchema.DumpsterQuotaUsedExtended
					});
					object obj = base.Mailbox.TryGetProperty(MailboxSchema.DumpsterQuotaUsedExtended);
					if (obj is PropertyError)
					{
						ExTraceGlobals.SessionTracer.TraceError<MailboxSession, PropertyError>((long)this.GetHashCode(), "{0}: We could not get size of this mailbox due to PropertyError {1}. Skipping it.", this, (PropertyError)obj);
						result = null;
					}
					else
					{
						ulong value = (ulong)((long)obj);
						result = new ulong?(value);
					}
				}
				return result;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000E550 File Offset: 0x0000C750
		public bool? IsDumpsterOverQuota(Unlimited<ByteQuantifiedSize> dumpsterQuota)
		{
			bool? result;
			using (base.CheckDisposed("IsDumpsterOverQuota"))
			{
				if (dumpsterQuota.IsUnlimited)
				{
					result = new bool?(false);
				}
				else
				{
					ulong? dumpsterSize = this.DumpsterSize;
					if (dumpsterSize != null)
					{
						result = new bool?(dumpsterSize.Value > dumpsterQuota.Value.ToBytes());
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		protected override ObjectAccessGuard CheckObjectState(string methodName)
		{
			ObjectAccessGuard objectAccessGuard = base.CheckObjectState(methodName);
			bool flag = false;
			ObjectAccessGuard result;
			try
			{
				if (!base.IsConnected)
				{
					ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxSession::{0}. The mailbox session is not connected yet.", methodName);
					throw new InvalidOperationException(ServerStrings.ExStoreSessionDisconnected);
				}
				flag = true;
				result = objectAccessGuard;
			}
			finally
			{
				if (!flag)
				{
					objectAccessGuard.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000E640 File Offset: 0x0000C840
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.IsInternallyOpenedDelegateAccess && !this.CanDispose)
				{
					throw new InvalidOperationException("Consumer should not have disposed this session as it's opened for calendar delegate access and managed by XSO internally.");
				}
				this.InternalDisposeServerObjects();
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.Dispose();
					this.copyOnWriteNotification = null;
				}
				Util.DisposeIfPresent(this.delegateSessionManager);
				this.delegateSessionManager = null;
				this.mailboxOwner = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public override DisposeTracker GetDisposeTracker()
		{
			DisposeTracker result;
			using (base.CreateSessionGuard("GetDisposeTracker"))
			{
				result = DisposeTracker.Get<MailboxSession>(this);
			}
			return result;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000E734 File Offset: 0x0000C934
		internal bool CanDispose
		{
			get
			{
				bool result;
				using (base.CreateSessionGuard("CanDispose::get"))
				{
					result = this.canDispose;
				}
				return result;
			}
			set
			{
				using (base.CreateSessionGuard("CanDispose::set"))
				{
					if (this.IsInternallyOpenedDelegateAccess)
					{
						this.canDispose = value;
					}
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000E77C File Offset: 0x0000C97C
		internal MailboxSessionSharableDataManager SharedDataManager
		{
			get
			{
				MailboxSessionSharableDataManager result;
				using (base.CreateSessionGuard("SharedDataManager::get"))
				{
					result = this.sharedDataManager;
				}
				return result;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		public override bool IsRemote
		{
			get
			{
				bool isRemote;
				using (base.CheckDisposed("IsRemote::get"))
				{
					isRemote = this.MailboxOwner.MailboxInfo.IsRemote;
				}
				return isRemote;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000E80C File Offset: 0x0000CA0C
		public override Guid MdbGuid
		{
			get
			{
				Guid databaseGuid;
				using (base.CheckDisposed("MdbGuid::get"))
				{
					databaseGuid = this.MailboxOwner.MailboxInfo.GetDatabaseGuid();
				}
				return databaseGuid;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000E858 File Offset: 0x0000CA58
		public override Guid MailboxGuid
		{
			get
			{
				Guid mailboxGuid;
				using (base.CheckDisposed("MailboxGuid::get"))
				{
					mailboxGuid = this.MailboxOwner.MailboxInfo.MailboxGuid;
				}
				return mailboxGuid;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		public override string ServerFullyQualifiedDomainName
		{
			get
			{
				string serverFqdn;
				using (base.CheckDisposed("ServerFullyQualifiedDomainName::get"))
				{
					serverFqdn = this.MailboxOwner.MailboxInfo.Location.ServerFqdn;
				}
				return serverFqdn;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public override OrganizationId OrganizationId
		{
			get
			{
				OrganizationId organizationId;
				using (base.CheckDisposed("OrganizationId::get"))
				{
					organizationId = this.MailboxOwner.MailboxInfo.OrganizationId;
				}
				return organizationId;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000E940 File Offset: 0x0000CB40
		internal bool IsInternallyOpenedDelegateAccess
		{
			get
			{
				bool result;
				using (base.CreateSessionGuard("IsInternallyOpenedDelegateAccess::get"))
				{
					if (this.masterMailboxSession != null)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000E988 File Offset: 0x0000CB88
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000E9CC File Offset: 0x0000CBCC
		public MailboxSession MasterMailboxSession
		{
			get
			{
				MailboxSession result;
				using (base.CheckDisposed("MasterMailboxSession::get"))
				{
					result = this.masterMailboxSession;
				}
				return result;
			}
			set
			{
				using (base.CheckDisposed("MasterMailboxSession::set"))
				{
					this.masterMailboxSession = value;
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		internal void CheckPrivateItemsAccessPermission(string delegateUserLegacyDn)
		{
			using (base.CreateSessionGuard("CheckPrivateItemsAccessPermission"))
			{
				this.filterPrivateItems = true;
				if (this.IsGroupMailbox())
				{
					this.filterPrivateItems = false;
				}
				ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(new Participant(null, delegateUserLegacyDn, "EX"), ParticipantEntryIdConsumer.SupportsADParticipantEntryId);
				try
				{
					byte[] delegateBytes = participantEntryId.ToByteArray();
					StoreObjectId defaultFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.FreeBusyData);
					if (defaultFolderId != null)
					{
						this.CheckFilterPrivateItemsFromFreeBusy(defaultFolderId, delegateBytes);
					}
				}
				catch (StoragePermanentException arg)
				{
					ExTraceGlobals.SessionTracer.TraceError<string, string, StoragePermanentException>(0L, "MailboxSession::CheckPrivateItemsAccessPermission. Hit unknown exception and we ignore. Mailbox = {0}, DelegateUser = {1}, Exception = {2}.", this.mailboxOwner.MailboxInfo.DisplayName, delegateUserLegacyDn, arg);
				}
				catch (StorageTransientException arg2)
				{
					ExTraceGlobals.SessionTracer.TraceError<string, string, StorageTransientException>(0L, "MailboxSession::CheckPrivateItemsAccessPermission. Hit unknown exception and we ignore. Mailbox = {0}, DelegateUser = {1}, Exception = {2}.", this.mailboxOwner.MailboxInfo.DisplayName, delegateUserLegacyDn, arg2);
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		private void CheckFilterPrivateItemsFromFreeBusy(StoreObjectId freeBusyFolderId, byte[] delegateBytes)
		{
			VersionedId localFreeBusyMessageId = this.GetLocalFreeBusyMessageId(freeBusyFolderId);
			if (localFreeBusyMessageId != null)
			{
				using (Item item = Item.Bind(this, localFreeBusyMessageId, MailboxSession.DelegateDefinitions))
				{
					byte[][] valueOrDefault = item.GetValueOrDefault<byte[][]>(InternalSchema.DelegateEntryIds2);
					int[] valueOrDefault2 = item.GetValueOrDefault<int[]>(InternalSchema.DelegateFlags);
					if (valueOrDefault == null || valueOrDefault2 == null)
					{
						return;
					}
					if (valueOrDefault.Length != valueOrDefault2.Length)
					{
						return;
					}
					for (int i = 0; i < valueOrDefault.Length; i++)
					{
						byte[] y = valueOrDefault[i];
						if (ArrayComparer<byte>.Comparer.Equals(delegateBytes, y))
						{
							this.filterPrivateItems = (valueOrDefault2[i] != 1);
							break;
						}
					}
					return;
				}
			}
			ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::CheckFilterPrivateItemsFromFreeBusy. No FreeBusyMessage was found from the FreeBusy folder. Mailbox = {0}.", this.InternalMailboxOwner);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public string DisplayName
		{
			get
			{
				string displayName;
				using (this.CheckObjectState("DisplayName::get"))
				{
					displayName = this.MailboxOwner.MailboxInfo.DisplayName;
				}
				return displayName;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000EC04 File Offset: 0x0000CE04
		public IExchangePrincipal InternalMailboxOwner
		{
			get
			{
				IExchangePrincipal result;
				using (base.CheckDisposed("InternalMailboxOwner::get"))
				{
					result = this.mailboxOwner;
				}
				return result;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000EC48 File Offset: 0x0000CE48
		private bool AreIdsEquivalent(StoreObjectId systemFolderId, StoreObjectId idToCompare)
		{
			if (systemFolderId != null && systemFolderId.ProviderLevelItemId.Length > 0 && idToCompare != null && idToCompare.ProviderLevelItemId.Length > 0)
			{
				if (!idToCompare.IsFolderId)
				{
					idToCompare = IdConverter.GetParentIdFromMessageId(idToCompare);
				}
				return systemFolderId.Equals(idToCompare);
			}
			return false;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public override IActivitySession ActivitySession
		{
			get
			{
				IActivitySession value;
				using (base.CheckDisposed("ActivitySession::get"))
				{
					value = this.activitySessionHook.Value;
				}
				return value;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		internal override void CheckSystemFolderAccess(StoreObjectId id)
		{
			using (base.CreateSessionGuard("CheckSystemFolderAccess"))
			{
				if (!this.isDefaultFolderManagerBeingInitialized && base.LogonType != LogonType.SystemService)
				{
					if (this.UseSystemFolder && base.LogonType != LogonType.Transport)
					{
						StoreObjectId defaultFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.System);
						if (this.AreIdsEquivalent(defaultFolderId, id))
						{
							throw new AccessDeniedException(ServerStrings.ExSystemFolderAccessDenied);
						}
					}
					if (this.UseAdminAuditLogsFolder && !this.bypassAuditsFolderAccessChecking)
					{
						StoreObjectId adminAuditLogsFolderId = null;
						this.BypassAuditsFolderAccessChecking(delegate
						{
							adminAuditLogsFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.AdminAuditLogs);
						});
						if (this.AreIdsEquivalent(adminAuditLogsFolderId, id))
						{
							throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsFolderAccessDenied);
						}
					}
					if (this.UseAuditsFolder && !this.bypassAuditsFolderAccessChecking)
					{
						StoreObjectId auditsFolderId = null;
						this.BypassAuditsFolderAccessChecking(delegate
						{
							auditsFolderId = this.defaultFolderManager.GetDefaultFolderId(DefaultFolderType.Audits);
						});
						if (this.AreIdsEquivalent(auditsFolderId, id))
						{
							throw new AccessDeniedException(ServerStrings.ExAuditsFolderAccessDenied);
						}
					}
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000EE30 File Offset: 0x0000D030
		public void EnablePrivateItemsFilter()
		{
			using (base.CheckDisposed("EnablePrivateItemsFilter"))
			{
				this.disableFilterPrivateItems = false;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000EE70 File Offset: 0x0000D070
		public void DisablePrivateItemsFilter()
		{
			using (base.CheckDisposed("DisablePrivateItemsFilter"))
			{
				this.disableFilterPrivateItems = true;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public bool PrivateItemsFilterDisabled
		{
			get
			{
				bool result;
				using (base.CheckDisposed("PrivateItemsFilterDisabled::get"))
				{
					result = this.disableFilterPrivateItems;
				}
				return result;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		public bool FilterPrivateItems
		{
			get
			{
				bool result;
				using (base.CheckDisposed("FilterPrivateItems::get"))
				{
					result = (this.ShouldFilterPrivateItems && !this.disableFilterPrivateItems);
				}
				return result;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000EF44 File Offset: 0x0000D144
		public override ContactFolders ContactFolders
		{
			get
			{
				ContactFolders result;
				using (base.CheckDisposed("ContactFolders::get"))
				{
					if (this.contactFolders == null)
					{
						this.contactFolders = ContactFolders.Load(XSOFactory.Default, this);
					}
					result = this.contactFolders;
				}
				return result;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public bool ShouldFilterPrivateItems
		{
			get
			{
				bool result;
				using (base.CheckDisposed("ShouldFilterPrivateItems::get"))
				{
					result = this.filterPrivateItems;
				}
				return result;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		internal MasterCategoryList InternalGetMasterCategoryList()
		{
			MasterCategoryList result;
			using (base.CreateSessionGuard("InternalGetMasterCategoryList"))
			{
				if (this.masterCategoryList == null)
				{
					this.masterCategoryList = new MasterCategoryList(this);
				}
				result = this.masterCategoryList;
			}
			return result;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000F03C File Offset: 0x0000D23C
		internal bool InternalIsConfigurationFolder(StoreObjectId id)
		{
			bool result;
			using (base.CreateSessionGuard("InternalIsConfigurationFolder"))
			{
				if (this.isDefaultFolderManagerBeingInitialized)
				{
					result = id.Equals(StoreObjectId.FromProviderSpecificId(base.Mailbox.MapiStore.GetNonIpmSubtreeFolderEntryId(), StoreObjectType.Folder));
				}
				else
				{
					result = (this.IsDefaultFolderType(id) == DefaultFolderType.Configuration);
				}
			}
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		internal override bool OnBeforeItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, CallbackContext callbackContext)
		{
			bool result;
			using (base.CreateSessionGuard("OnBeforeItemChange"))
			{
				base.OnBeforeItemChange(operation, session, itemId, item, callbackContext);
				if (operation == ItemChangeOperation.Submit)
				{
					this.CheckIfItemNeedsRecipientsGroupExpansion(item);
				}
				if (item != null)
				{
					this.CheckForImplicitMarkAsNotClutter(operation, item.PropertyBag);
				}
				if (this.ActivitySession != null)
				{
					this.ActivitySession.CaptureActivityBeforeItemChange(operation, itemId, item);
				}
				if (this.copyOnWriteNotification != null)
				{
					result = this.copyOnWriteNotification.OnBeforeItemChange(operation, session, itemId, item, callbackContext);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000F148 File Offset: 0x0000D348
		internal override void OnAfterItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result, CallbackContext callbackContext)
		{
			using (base.CreateSessionGuard("OnAfterItemChange"))
			{
				base.OnAfterItemChange(operation, session, itemId, item, result, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.OnAfterItemChange(operation, session, itemId, item, result, callbackContext);
				}
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		internal override bool OnBeforeItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, CallbackContext callbackContext)
		{
			bool result;
			using (base.CreateSessionGuard("OnBeforeItemSave"))
			{
				base.OnBeforeItemSave(operation, session, itemId, item, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					result = this.copyOnWriteNotification.OnBeforeItemSave(operation, session, itemId, item, callbackContext);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000F214 File Offset: 0x0000D414
		internal override void OnAfterItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result, CallbackContext callbackContext)
		{
			using (base.CreateSessionGuard("OnAfterItemSave"))
			{
				base.OnAfterItemSave(operation, session, itemId, item, result, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.OnAfterItemSave(operation, session, itemId, item, result, callbackContext);
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000F278 File Offset: 0x0000D478
		internal override bool OnBeforeFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, CallbackContext callbackContext)
		{
			bool result;
			using (base.CreateSessionGuard("OnBeforeFolderChange"))
			{
				base.OnBeforeFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					result = this.copyOnWriteNotification.OnBeforeFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, callbackContext);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		internal override void OnAfterFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, GroupOperationResult result, CallbackContext callbackContext)
		{
			using (base.CreateSessionGuard("OnAfterFolderChange"))
			{
				base.OnAfterFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, result, callbackContext);
				if (result != null && result.OperationResult != OperationResult.Failed && (destinationSession == null || sourceSession == destinationSession))
				{
					if (this.ActivitySession != null)
					{
						this.ActivitySession.CaptureActivityAfterFolderChange(operation, flags, result.ObjectIds, result.ResultObjectIds, sourceFolderId, destinationFolderId);
					}
					this.CaptureMarkAsClutterOrNotClutter(operation, flags, result, sourceFolderId, destinationFolderId);
				}
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.OnAfterFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, result, callbackContext);
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		internal override void OnBeforeFolderBind(StoreObjectId folderId, CallbackContext callbackContext)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			using (base.CreateSessionGuard("OnBeforeFolderBind"))
			{
				base.OnBeforeFolderBind(folderId, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.OnBeforeFolderBind(this, folderId, callbackContext);
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000F40C File Offset: 0x0000D60C
		internal override void OnAfterFolderBind(StoreObjectId folderId, CoreFolder folder, bool success, CallbackContext callbackContext)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			using (base.CreateSessionGuard("OnAfterFolderBind"))
			{
				base.OnAfterFolderBind(folderId, folder, success, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.OnAfterFolderBind(this, folderId, folder, success, callbackContext);
				}
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000F474 File Offset: 0x0000D674
		internal override GroupOperationResult GetCallbackResults()
		{
			GroupOperationResult result;
			using (base.CreateSessionGuard("GetCallbackResults"))
			{
				if (this.copyOnWriteNotification != null)
				{
					result = this.copyOnWriteNotification.GetCallbackResults();
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		internal void BypassAuditsFolderAccessChecking(Action action)
		{
			Util.ThrowOnNullArgument(action, "action");
			using (base.CreateSessionGuard("BypassAuditsFolderAccessChecking"))
			{
				bool flag = !this.bypassAuditsFolderAccessChecking;
				this.bypassAuditsFolderAccessChecking = true;
				try
				{
					action();
				}
				finally
				{
					if (flag)
					{
						this.bypassAuditsFolderAccessChecking = false;
					}
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000F53C File Offset: 0x0000D73C
		internal void BypassAuditing(Action action)
		{
			Util.ThrowOnNullArgument(action, "action");
			using (base.CreateSessionGuard("BypassAuditing"))
			{
				bool flag = !this.bypassAuditing;
				this.bypassAuditing = true;
				try
				{
					action();
				}
				finally
				{
					if (flag)
					{
						this.bypassAuditing = false;
					}
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		internal void DoNothingIfBypassAuditing(Action action)
		{
			Util.ThrowOnNullArgument(action, "action");
			using (base.CreateSessionGuard("DoNothingIfBypassAuditing"))
			{
				if (!this.bypassAuditing)
				{
					action();
				}
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000F604 File Offset: 0x0000D804
		internal void CheckForImplicitMarkAsNotClutter(ItemChangeOperation operation, ICorePropertyBag propertyBag)
		{
			using (base.CreateSessionGuard("CheckForImplicitMarkAsNotClutter"))
			{
				ArgumentValidator.ThrowIfNull("propertyBag", propertyBag);
				if (operation == ItemChangeOperation.Update)
				{
					bool valueOrDefault = propertyBag.GetValueOrDefault<bool>(ItemSchema.IsClutter, false);
					StoreObjectId valueOrDefault2 = propertyBag.GetValueOrDefault<StoreObjectId>(StoreObjectSchema.ParentItemId, null);
					bool flag = valueOrDefault2 != null && valueOrDefault2.Equals(this.GetDefaultFolderId(DefaultFolderType.Clutter));
					if (valueOrDefault || flag)
					{
						bool flag2 = propertyBag.IsPropertyDirty(InternalSchema.FlagStatus) && propertyBag.GetValueOrDefault<FlagStatus>(InternalSchema.FlagStatus, FlagStatus.NotFlagged) == FlagStatus.Flagged;
						bool flag3 = false;
						if (propertyBag.IsPropertyDirty(InternalSchema.LastVerbExecuted))
						{
							LastAction valueOrDefault3 = propertyBag.GetValueOrDefault<LastAction>(InternalSchema.LastVerbExecuted, LastAction.Open);
							if (valueOrDefault3 == LastAction.ReplyToSender || valueOrDefault3 == LastAction.ReplyToAll || valueOrDefault3 == LastAction.Forward || (valueOrDefault3 >= LastAction.VotingOptionMin && valueOrDefault3 <= LastAction.VotingOptionMax))
							{
								flag3 = true;
							}
						}
						if (flag2 || flag3)
						{
							propertyBag[InternalSchema.InferenceProcessingNeeded] = true;
							InferenceProcessingActions inferenceProcessingActions = (InferenceProcessingActions)propertyBag.GetValueOrDefault<long>(InternalSchema.InferenceProcessingActions, 0L);
							inferenceProcessingActions |= InferenceProcessingActions.ProcessImplicitMarkAsNotClutter;
							propertyBag[InternalSchema.InferenceProcessingActions] = (long)inferenceProcessingActions;
						}
					}
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000F728 File Offset: 0x0000D928
		private void InternalInitializeDefaultFolders(IList<DefaultFolderType> foldersToInit, OpenMailboxSessionFlags openFlags)
		{
			if (ExTraceGlobals.SessionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string, OpenMailboxSessionFlags>((long)this.GetHashCode(), "MailboxSession::InternalInitializeDefaultFolders. Initializing default folders {0} with flags {1}", string.Join<DefaultFolderType>(", ", foldersToInit.ToArray<DefaultFolderType>()), openFlags);
			}
			this.isDefaultFolderManagerBeingInitialized = true;
			this.defaultFolderManager = DefaultFolderManager.Create(this, openFlags, foldersToInit);
			this.isDefaultFolderManagerBeingInitialized = false;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000F788 File Offset: 0x0000D988
		private void InternalLocalizeInitialDefaultFolders(OpenMailboxSessionFlags openFlags)
		{
			CultureInfo mailboxLocale = this.MailboxOwner.PreferredCultures.DefaultIfEmpty(this.InternalPreferedCulture).First<CultureInfo>();
			DefaultFolderManager defaultFolderManager = DefaultFolderManager.Create(this, openFlags, MailboxSession.DefaultFoldersToLocalizeOnFirstLogon);
			Exception[] array;
			defaultFolderManager.Localize(out array);
			if (array != null && array.Length > 0)
			{
				ExTraceGlobals.SessionTracer.TraceError<string, Exception>(0L, "MailboxSession::InternalLocalizeInitialDefaultFolders. Failed to localize default folders. Mailbox = {0}, exception = {1}.", this.MailboxOwnerLegacyDN, array[0]);
			}
			this.SetMailboxLocale(mailboxLocale);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000F7F1 File Offset: 0x0000D9F1
		public void SetMailboxLocale()
		{
			this.SetMailboxLocale(this.InternalPreferedCulture);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000F800 File Offset: 0x0000DA00
		public void SetMailboxLocale(CultureInfo cultureInfo)
		{
			base.Mailbox[InternalSchema.IsMailboxLocalized] = true;
			base.Mailbox[InternalSchema.LocaleId] = 0;
			base.Mailbox[InternalSchema.LocaleId] = LocaleMap.GetLcidFromCulture(cultureInfo);
			base.Mailbox.Save();
			base.Mailbox.Load();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000F86C File Offset: 0x0000DA6C
		private void CheckIfItemNeedsRecipientsGroupExpansion(CoreItem item)
		{
			if (item != null && item.Recipients != null)
			{
				COWSettings cowsettings = this.COWSettings;
				if (cowsettings == null)
				{
					cowsettings = new COWSettings(this);
				}
				if (cowsettings.LegalHoldEnabled())
				{
					bool flag = false;
					foreach (CoreRecipient coreRecipient in item.Recipients)
					{
						object obj = coreRecipient.Participant.TryGetProperty(ParticipantSchema.IsDistributionList);
						if (!PropertyError.IsPropertyError(obj) && (bool)obj)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						item.PropertyBag[InternalSchema.NeedGroupExpansion] = true;
					}
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000F924 File Offset: 0x0000DB24
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000F968 File Offset: 0x0000DB68
		public bool IsUnified
		{
			get
			{
				bool result;
				using (base.CheckDisposed("IsUnified::get"))
				{
					result = this.isUnified;
				}
				return result;
			}
			set
			{
				using (base.CheckDisposed("IsUnified::set"))
				{
					this.isUnified = value;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public bool IsAuditConfigFromUCCPolicyEnabled
		{
			get
			{
				bool value;
				using (base.CheckDisposed("IsAuditConfigFromUCCPolicyEnabled::get"))
				{
					if (this.isAuditConfigFromUCCPolicyEnabled == null)
					{
						VariantConfigurationSnapshot configuration = this.MailboxOwner.GetConfiguration();
						this.isAuditConfigFromUCCPolicyEnabled = new bool?(configuration != null && configuration.Ipaed.AuditConfigFromUCCPolicy.Enabled);
					}
					value = this.isAuditConfigFromUCCPolicyEnabled.Value;
				}
				return value;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		private static bool IsOwnerLogon(LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegatedUser)
		{
			return logonType == LogonType.Owner || (logonType == LogonType.BestAccess && (delegatedUser == null || string.IsNullOrEmpty(delegatedUser.LegacyDn) || string.Equals(delegatedUser.LegacyDn, owner.LegacyDn, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public bool TryToSyncSiteMailboxNow()
		{
			using (this.CheckObjectState("TryToSyncSiteMailboxNow"))
			{
				if (this.siteMailboxSynchronizerReference != null)
				{
					return this.siteMailboxSynchronizerReference.TryToSyncNow();
				}
			}
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		private static void TriggerSiteMailboxSyncIfNeeded(StorageTransientException e, IExchangePrincipal mailbox, string clientInfoString)
		{
			MapiExceptionLogonFailed mapiExceptionLogonFailed = e.InnerException as MapiExceptionLogonFailed;
			if (mailbox.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox && mapiExceptionLogonFailed != null && !string.IsNullOrEmpty(clientInfoString) && clientInfoString.StartsWith("Client=OWA", StringComparison.OrdinalIgnoreCase))
			{
				using (SiteMailboxSynchronizerReference siteMailboxSynchronizer = SiteMailboxSynchronizerManager.Instance.GetSiteMailboxSynchronizer(mailbox, Utils.GetSyncClientString("Failed_OWA_Logon")))
				{
					siteMailboxSynchronizer.TryToSyncNow();
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000FB30 File Offset: 0x0000DD30
		private static MailboxSession ConfigurableOpen(IExchangePrincipal mailbox, MailboxAccessInfo accessInfo, CultureInfo cultureInfo, string clientInfoString, LogonType logonType, PropertyDefinition[] mailboxProperties, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit, IBudget budget)
		{
			return MailboxSession.ConfigurableOpen(mailbox, accessInfo, cultureInfo, clientInfoString, logonType, mailboxProperties, initFlags, foldersToInit, budget, false, null, UnifiedGroupMemberType.Unknown);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000FB54 File Offset: 0x0000DD54
		private static MailboxSession ConfigurableOpen(IExchangePrincipal mailbox, MailboxAccessInfo accessInfo, CultureInfo cultureInfo, string clientInfoString, LogonType logonType, PropertyDefinition[] mailboxProperties, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit, IBudget budget, MailboxSessionSharableDataManager sharedDataManager)
		{
			return MailboxSession.ConfigurableOpen(mailbox, accessInfo, cultureInfo, clientInfoString, logonType, mailboxProperties, initFlags, foldersToInit, budget, false, sharedDataManager, UnifiedGroupMemberType.Unknown);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000FB78 File Offset: 0x0000DD78
		private static MailboxSession ConfigurableOpen(IExchangePrincipal mailbox, MailboxAccessInfo accessInfo, CultureInfo cultureInfo, string clientInfoString, LogonType logonType, PropertyDefinition[] mailboxProperties, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit, IBudget budget, bool unifiedSession, MailboxSessionSharableDataManager sharedDataManager, UnifiedGroupMemberType memberType)
		{
			Util.ThrowOnNullArgument(mailbox, "mailbox");
			Util.ThrowOnNullArgument(accessInfo, "accessInfo");
			if (mailboxProperties != null && mailboxProperties.Length == 0)
			{
				throw new ArgumentException("mailboxProperties must be null or a non-zero length PropertyDefinition[]", "mailboxProperties");
			}
			DelegateLogonUser delegateLogonUser;
			if (accessInfo.AccessingUserAdEntry != null)
			{
				delegateLogonUser = new DelegateLogonUser(accessInfo.AccessingUserAdEntry);
			}
			else if (accessInfo.AccessingUserDn != null)
			{
				delegateLogonUser = new DelegateLogonUser(accessInfo.AccessingUserDn);
			}
			else
			{
				delegateLogonUser = new DelegateLogonUser(null);
			}
			OpenMailboxSessionFlags flags;
			MailboxSession.InternalBuildOpenMailboxSessionFlags(initFlags, logonType, foldersToInit, out flags);
			object obj = null;
			if (accessInfo.AuthenticatedUserPrincipal != null)
			{
				obj = accessInfo.AuthenticatedUserPrincipal.Identity;
			}
			else if (accessInfo.AuthenticatedUserContext != null)
			{
				if (accessInfo.AuthenticatedUserContext.UserSid.IsWellKnown(WellKnownSidType.LocalSystemSid))
				{
					obj = new ClientIdentityInfo(accessInfo.AuthenticatedUserContext.ClientContextHandle.DangerousGetHandle(), accessInfo.AuthenticatedUserContext.UserSid, new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null));
				}
				else
				{
					obj = StoreSession.FromAuthZContext(mailbox.MailboxInfo.OrganizationId.ToADSessionSettings(), accessInfo.AuthenticatedUserContext.ClientContextHandle);
				}
			}
			else if (accessInfo.AuthenticatedUserHandle != null)
			{
				obj = StoreSession.FromAuthZContext(mailbox.MailboxInfo.OrganizationId.ToADSessionSettings(), accessInfo.AuthenticatedUserHandle);
			}
			else if (accessInfo.ClientIdentityInfo != null)
			{
				obj = accessInfo.ClientIdentityInfo;
			}
			if (obj == null)
			{
				throw new ObjectNotFoundException(ServerStrings.UserCannotBeFoundFromContext(Marshal.GetLastWin32Error()));
			}
			return MailboxSession.CreateMailboxSession(logonType, mailbox, delegateLogonUser, obj, flags, cultureInfo, clientInfoString, mailboxProperties, foldersToInit, accessInfo.AuxiliaryIdentity, budget, unifiedSession, sharedDataManager, memberType);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000FCE1 File Offset: 0x0000DEE1
		public static MailboxSession Open(IExchangePrincipal mailboxOwner, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString)
		{
			return MailboxSession.Open(mailboxOwner, authenticatedUser, cultureInfo, clientInfoString, true);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public static MailboxSession Open(IExchangePrincipal mailboxOwner, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString, bool wantCachedConnection)
		{
			if (authenticatedUser == null)
			{
				throw new ArgumentNullException("authenticatedUser");
			}
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(authenticatedUser);
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Owner, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, string accessingUserDn, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (authenticatedUser == null)
			{
				throw new ArgumentNullException("authenticatedUser");
			}
			MailboxAccessInfo accessInfo;
			if (string.IsNullOrEmpty(accessingUserDn))
			{
				accessInfo = new MailboxAccessInfo(authenticatedUser);
			}
			else
			{
				accessInfo = new MailboxAccessInfo(accessingUserDn, authenticatedUser);
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.BestAccess, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000FD80 File Offset: 0x0000DF80
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, string accessingUserDn, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, GenericIdentity auxiliaryIdentity)
		{
			return MailboxSession.OpenWithBestAccess(mailboxOwner, accessingUserDn, clientSecurityContext, cultureInfo, clientInfoString, auxiliaryIdentity, false);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000FD90 File Offset: 0x0000DF90
		private static MailboxAccessInfo GetMailboxAccessInfo(string accessingUserDn, ClientSecurityContext clientSecurityContext, GenericIdentity auxiliaryIdentity)
		{
			if (string.IsNullOrEmpty(accessingUserDn))
			{
				return new MailboxAccessInfo(clientSecurityContext, auxiliaryIdentity);
			}
			return new MailboxAccessInfo(accessingUserDn, clientSecurityContext, auxiliaryIdentity);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, string accessingUserDn, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, GenericIdentity auxiliaryIdentity, bool unifiedSession)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			UnifiedGroupMemberType memberType = UnifiedGroupMemberType.Unknown;
			if (mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				AccessingUserInfo accessUserInfo = new AccessingUserInfo(accessingUserDn, null, mailboxOwner.MailboxInfo.OrganizationId, null);
				memberType = MailboxSession.GetUserMembershipType(mailboxOwner, accessUserInfo, clientSecurityContext, clientInfoString);
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, MailboxSession.GetMailboxAccessInfo(accessingUserDn, clientSecurityContext, auxiliaryIdentity), cultureInfo, clientInfoString, LogonType.BestAccess, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders, null, unifiedSession, null, memberType);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, IADOrgPerson accessingUser, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString)
		{
			if (accessingUser == null)
			{
				throw new ArgumentNullException("accessingUser");
			}
			MailboxSession mailboxSession = MailboxSession.OpenWithBestAccess(mailboxOwner, accessingUser.LegacyExchangeDN, authenticatedUser, cultureInfo, clientInfoString);
			if (mailboxSession.LogonType == LogonType.Delegated)
			{
				mailboxSession.delegateUser = accessingUser;
			}
			return mailboxSession;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000FE69 File Offset: 0x0000E069
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, IADOrgPerson accessingUser, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			return MailboxSession.OpenWithBestAccess(mailboxOwner, accessingUser, clientSecurityContext, cultureInfo, clientInfoString, false);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000FE77 File Offset: 0x0000E077
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, IADOrgPerson accessingUser, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, bool unifiedSession)
		{
			return MailboxSession.OpenWithBestAccess(mailboxOwner, accessingUser, clientSecurityContext, cultureInfo, clientInfoString, null, unifiedSession);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000FE87 File Offset: 0x0000E087
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, IADOrgPerson accessingUser, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, GenericIdentity auxiliaryIdentity)
		{
			return MailboxSession.OpenWithBestAccess(mailboxOwner, accessingUser, clientSecurityContext, cultureInfo, clientInfoString, auxiliaryIdentity, false);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000FE98 File Offset: 0x0000E098
		public static MailboxSession OpenWithBestAccess(IExchangePrincipal mailboxOwner, IADOrgPerson accessingUser, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, GenericIdentity auxiliaryIdentity, bool unifiedSession)
		{
			if (accessingUser == null)
			{
				throw new ArgumentNullException("accessingUser");
			}
			UnifiedGroupMemberType memberType = UnifiedGroupMemberType.Unknown;
			if (mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				AccessingUserInfo accessUserInfo = new AccessingUserInfo(accessingUser.LegacyExchangeDN, accessingUser.ExternalDirectoryObjectId, mailboxOwner.MailboxInfo.OrganizationId, accessingUser.Id);
				memberType = MailboxSession.GetUserMembershipType(mailboxOwner, accessUserInfo, clientSecurityContext, clientInfoString);
			}
			MailboxSession mailboxSession = MailboxSession.ConfigurableOpen(mailboxOwner, MailboxSession.GetMailboxAccessInfo(accessingUser.LegacyExchangeDN, clientSecurityContext, auxiliaryIdentity), cultureInfo, clientInfoString, LogonType.BestAccess, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders, null, unifiedSession, null, memberType);
			if (mailboxSession.LogonType == LogonType.Delegated)
			{
				mailboxSession.delegateUser = accessingUser;
			}
			return mailboxSession;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000FF30 File Offset: 0x0000E130
		public static MailboxSession OpenAsDelegate(IExchangePrincipal mailboxOwner, IADOrgPerson delegateUser, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (delegateUser == null)
			{
				throw new ArgumentNullException("delegateUser");
			}
			if (authenticatedUser == null)
			{
				throw new ArgumentNullException("authenticatedUser");
			}
			if (string.IsNullOrEmpty(delegateUser.LegacyExchangeDN))
			{
				ExTraceGlobals.SessionTracer.TraceError<string, string, string>(0L, "MailboxSession::OpenAsDelegate. delegateUser's LegacyDn is Null or Empty. mailboxOwner = {0}, delegateUser.DisplayName = {1}, delegateUser.LegacyExchangeDN = {2}.", mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName, delegateUser.LegacyExchangeDN);
				throw new AccessDeniedException(ServerStrings.ExMailboxAccessDenied(mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName));
			}
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(delegateUser, authenticatedUser);
			MailboxSession mailboxSession = MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Delegated, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
			if (mailboxSession.CanActAsOwner)
			{
				ExTraceGlobals.SessionTracer.TraceError<string>(0L, "MailboxSession::OpenAsDelegate. The user cannot act as a delegate. mailboxOwner.DisplayName = {0}.", mailboxOwner.MailboxInfo.DisplayName);
				mailboxSession.Dispose();
				throw new MailboxMustBeAccessedAsOwnerException(ServerStrings.ExMailboxMustBeAccessedAsOwner(mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName));
			}
			return mailboxSession;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00010024 File Offset: 0x0000E224
		public static MailboxSession OpenAsDelegate(IExchangePrincipal mailboxOwner, IADOrgPerson delegateUser, AuthzContextHandle authenticatedUserHandle, CultureInfo cultureInfo, string clientInfoString)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (delegateUser == null)
			{
				throw new ArgumentNullException("delegateUser");
			}
			if (authenticatedUserHandle == null || authenticatedUserHandle.IsInvalid)
			{
				throw new ArgumentNullException("authenticatedUserHandle");
			}
			if (string.IsNullOrEmpty(delegateUser.LegacyExchangeDN))
			{
				ExTraceGlobals.SessionTracer.TraceError<string, string, string>(0L, "MailboxSession::OpenAsDelegate. Delegated user's legacyDn is Empty or Null. mailboxOwner = {0}, delegateUser.DisplayName = {1}, delegateUser.legacyDn = {2}.", mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName, delegateUser.LegacyExchangeDN);
				throw new AccessDeniedException(ServerStrings.ExMailboxAccessDenied(mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName));
			}
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(delegateUser, authenticatedUserHandle);
			MailboxSession mailboxSession = MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Delegated, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
			if (mailboxSession.CanActAsOwner)
			{
				ExTraceGlobals.SessionTracer.TraceError<string, string>(0L, "MailboxSession::OpenAsDelegate. Delegated user cannot act as the owner. mailboxOwner = {0}, delegateUser.DisplayName = {1}.", mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName);
				mailboxSession.Dispose();
				throw new MailboxMustBeAccessedAsOwnerException(ServerStrings.ExMailboxMustBeAccessedAsOwner(mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName));
			}
			return mailboxSession;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00010128 File Offset: 0x0000E328
		public static MailboxSession OpenAsDelegate(IExchangePrincipal mailboxOwner, IADOrgPerson delegateUser, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (delegateUser == null)
			{
				throw new ArgumentNullException("delegateUser");
			}
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			if (string.IsNullOrEmpty(delegateUser.LegacyExchangeDN))
			{
				ExTraceGlobals.SessionTracer.TraceError<string, string, string>(0L, "MailboxSession::OpenAsDelegate. Delegated user's legacyDn is Empty or Null. mailboxOwner = {0}, delegateUser.DisplayName = {1}, delegateUser.legacyDn = {2}.", mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName, delegateUser.LegacyExchangeDN);
				throw new AccessDeniedException(ServerStrings.ExMailboxAccessDenied(mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName));
			}
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(delegateUser, clientSecurityContext);
			MailboxSession mailboxSession = MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Delegated, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders);
			if (mailboxSession.CanActAsOwner)
			{
				mailboxSession.Dispose();
				throw new MailboxMustBeAccessedAsOwnerException(ServerStrings.ExMailboxMustBeAccessedAsOwner(mailboxOwner.MailboxInfo.DisplayName, delegateUser.DisplayName));
			}
			return mailboxSession;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00010200 File Offset: 0x0000E400
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString)
		{
			return MailboxSession.OpenAsAdmin(mailboxOwner, cultureInfo, clientInfoString, null);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001020C File Offset: 0x0000E40C
		public static MailboxSession OpenAsAdminWithBudget(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, IBudget budget)
		{
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()), null);
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Admin, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders, budget);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00010240 File Offset: 0x0000E440
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, GenericIdentity auxiliaryIdentity)
		{
			return MailboxSession.OpenAsAdmin(mailboxOwner, cultureInfo, clientInfoString, false, false, auxiliaryIdentity, false);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00010250 File Offset: 0x0000E450
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, GenericIdentity auxiliaryIdentity, bool nonInteractiveSession)
		{
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()), auxiliaryIdentity);
			MailboxSession.InitializationFlags initializationFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties;
			if (nonInteractiveSession)
			{
				initializationFlags |= MailboxSession.InitializationFlags.NonInteractiveSession;
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.Admin, null, initializationFlags, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00010291 File Offset: 0x0000E491
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc)
		{
			return MailboxSession.OpenAsAdmin(mailboxOwner, cultureInfo, clientInfoString, useLocalRpc, false);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0001029D File Offset: 0x0000E49D
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb)
		{
			return MailboxSession.OpenAsAdmin(mailboxOwner, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, false);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000102AC File Offset: 0x0000E4AC
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb, bool readOnly)
		{
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
			return MailboxSession.OpenAsAdmin(mailboxOwner, accessInfo, LogonType.Admin, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, false, false, readOnly);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000102DC File Offset: 0x0000E4DC
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb, GenericIdentity auxiliaryIdentity, bool allowAdminLocalization = false)
		{
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()), auxiliaryIdentity);
			return MailboxSession.OpenAsAdmin(mailboxOwner, accessInfo, LogonType.Admin, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, false, allowAdminLocalization, false);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0001030C File Offset: 0x0000E50C
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, string accessingUserLegacyDn, WindowsPrincipal accessingWindowsPrincipal, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb)
		{
			return MailboxSession.OpenAsAdmin(mailboxOwner, accessingUserLegacyDn, accessingWindowsPrincipal, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, false);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00010320 File Offset: 0x0000E520
		public static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, string accessingUserLegacyDn, WindowsPrincipal accessingWindowsPrincipal, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb, bool useRecoveryDatabase)
		{
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(accessingUserLegacyDn, accessingWindowsPrincipal);
			return MailboxSession.OpenAsAdmin(mailboxOwner, accessInfo, LogonType.DelegatedAdmin, cultureInfo, clientInfoString, useLocalRpc, ignoreHomeMdb, useRecoveryDatabase, false, false);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010350 File Offset: 0x0000E550
		private static MailboxSession OpenAsAdmin(IExchangePrincipal mailboxOwner, MailboxAccessInfo accessInfo, LogonType logonType, CultureInfo cultureInfo, string clientInfoString, bool useLocalRpc, bool ignoreHomeMdb, bool recoveryDatabase, bool allowAdminLocalization = false, bool readOnly = false)
		{
			MailboxSession.InitializationFlags initializationFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties;
			if (useLocalRpc)
			{
				initializationFlags |= MailboxSession.InitializationFlags.RequestLocalRpc;
			}
			if (ignoreHomeMdb)
			{
				initializationFlags |= MailboxSession.InitializationFlags.OverrideHomeMdb;
			}
			if (recoveryDatabase)
			{
				initializationFlags |= MailboxSession.InitializationFlags.UseRecoveryDatabase;
				initializationFlags &= ~MailboxSession.InitializationFlags.CopyOnWrite;
			}
			if (allowAdminLocalization)
			{
				initializationFlags |= MailboxSession.InitializationFlags.AllowAdminLocalization;
			}
			if (readOnly)
			{
				initializationFlags |= MailboxSession.InitializationFlags.ReadOnly;
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, logonType, null, initializationFlags, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000103B0 File Offset: 0x0000E5B0
		public static MailboxSession OpenAsSystemService(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString)
		{
			return MailboxSession.OpenAsSystemService(mailboxOwner, cultureInfo, clientInfoString, false);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000103BC File Offset: 0x0000E5BC
		public static MailboxSession OpenAsSystemService(IExchangePrincipal mailboxOwner, CultureInfo cultureInfo, string clientInfoString, bool readOnly)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
			MailboxSession.InitializationFlags initializationFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties;
			if (readOnly)
			{
				initializationFlags |= MailboxSession.InitializationFlags.ReadOnly;
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, cultureInfo, clientInfoString, LogonType.SystemService, null, initializationFlags, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00010409 File Offset: 0x0000E609
		public static MailboxSession OpenAsMrs(IExchangePrincipal mailboxOwner, MailboxSession.InitializationFlags extraInitializationFlags, string clientInfoString)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent())), CultureInfo.InvariantCulture, clientInfoString, LogonType.SystemService, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties | extraInitializationFlags, MailboxSession.AllDefaultFolders);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00010444 File Offset: 0x0000E644
		public static MailboxSession OpenAsTransport(IExchangePrincipal mailboxOwner, string clientInfoString)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (clientInfoString == null)
			{
				throw new ArgumentNullException("clientInfoString");
			}
			if (clientInfoString.Length == 0)
			{
				throw new ArgumentException("clientInfoString has zero length", "clientInfoString");
			}
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, CultureInfo.InvariantCulture, clientInfoString, LogonType.Transport, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AlwaysInitDefaultFolders);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000104B4 File Offset: 0x0000E6B4
		public static MailboxSession OpenAsTransport(IExchangePrincipal mailboxOwner, OpenTransportSessionFlags flags)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			EnumValidator.ThrowIfInvalid<OpenTransportSessionFlags>(flags, "flags");
			MailboxSession.CheckNoRemoteExchangePrincipal(mailboxOwner);
			MailboxSession.InitializationFlags initializationFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties;
			switch (flags)
			{
			case OpenTransportSessionFlags.OpenForQuotaMessageDelivery:
				initializationFlags |= MailboxSession.InitializationFlags.QuotaMessageDelivery;
				break;
			case OpenTransportSessionFlags.OpenForNormalMessageDelivery:
				initializationFlags |= MailboxSession.InitializationFlags.NormalMessageDelivery;
				break;
			case OpenTransportSessionFlags.OpenForSpecialMessageDelivery:
				initializationFlags |= MailboxSession.InitializationFlags.SpecialMessageDelivery;
				break;
			}
			MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, CultureInfo.InvariantCulture, "Client=Hub Transport", LogonType.Transport, null, initializationFlags, MailboxSession.AlwaysInitDefaultFolders);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010544 File Offset: 0x0000E744
		public MailboxSession CloneWithBestAccess(IExchangePrincipal mailboxOwner, IADOrgPerson accessingUser, ClientSecurityContext clientSecurityContext, string clientInfoString, GenericIdentity auxiliaryIdentity, bool unifiedSession)
		{
			ArgumentValidator.ThrowIfNull("mailboxOwner", mailboxOwner);
			ArgumentValidator.ThrowIfNull("accessingUser", accessingUser);
			UnifiedGroupMemberType memberType = UnifiedGroupMemberType.Unknown;
			if (mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				AccessingUserInfo accessUserInfo = new AccessingUserInfo(accessingUser.LegacyExchangeDN, accessingUser.ExternalDirectoryObjectId, mailboxOwner.MailboxInfo.OrganizationId, accessingUser.Id);
				memberType = MailboxSession.GetUserMembershipType(mailboxOwner, accessUserInfo, clientSecurityContext, clientInfoString);
			}
			return this.CloneWithBestAccess(mailboxOwner, accessingUser.LegacyExchangeDN, clientSecurityContext, clientInfoString, auxiliaryIdentity, unifiedSession, memberType);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public MailboxSession CloneWithBestAccess(IExchangePrincipal mailboxOwner, string accessingUserDn, ClientSecurityContext clientSecurityContext, string clientInfoString, GenericIdentity auxiliaryIdentity, bool unifiedSession, UnifiedGroupMemberType memberType)
		{
			if (mailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxOwner");
			}
			if (!object.Equals(this.mailboxOwner.ObjectId, mailboxOwner.ObjectId))
			{
				throw new ArgumentException("mailboxOwner not same");
			}
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			MailboxAccessInfo accessInfo;
			if (string.IsNullOrEmpty(accessingUserDn))
			{
				accessInfo = new MailboxAccessInfo(clientSecurityContext, auxiliaryIdentity);
			}
			else
			{
				accessInfo = new MailboxAccessInfo(accessingUserDn, clientSecurityContext, auxiliaryIdentity);
			}
			return MailboxSession.ConfigurableOpen(mailboxOwner, accessInfo, this.SessionCultureInfo, clientInfoString, LogonType.BestAccess, null, MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties, MailboxSession.AllDefaultFolders, null, unifiedSession, this.sharedDataManager, memberType);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001069A File Offset: 0x0000E89A
		public void ReconstructExchangePrincipal()
		{
			DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
			{
				ADSessionSettings adSettings;
				if (base.PersistableTenantPartitionHint != null)
				{
					adSettings = ADSessionSettings.FromTenantPartitionHint(TenantPartitionHint.FromPersistablePartitionHint(base.PersistableTenantPartitionHint));
				}
				else
				{
					adSettings = ADSessionSettings.FromRootOrgScopeSet();
				}
				Guid mailboxGuid = this.MailboxGuid;
				this.mailboxOwner = ExchangePrincipal.FromMailboxGuid(adSettings, this.MailboxGuid, null);
			}, "ReconstructExchangePrincipal");
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000106B2 File Offset: 0x0000E8B2
		private bool TryGetServiceUserLegacyDn(out string serviceUserLegacyDn)
		{
			serviceUserLegacyDn = null;
			if (!this.MailboxOwner.MailboxInfo.IsRemote)
			{
				serviceUserLegacyDn = Server.GetSystemAttendantLegacyDN(LegacyDN.Parse(this.MailboxOwner.MailboxInfo.Location.ServerLegacyDn)).ToString();
				return true;
			}
			return false;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public Rules InboxRules
		{
			get
			{
				Rules result;
				using (this.CheckObjectState("InboxRules::get"))
				{
					base.CheckCapabilities(base.Capabilities.CanHaveRules, "CanHaveRules");
					if (this.inboxRules == null)
					{
						using (DisposeGuard disposeGuard = default(DisposeGuard))
						{
							Folder folder = Folder.Bind(this, DefaultFolderType.Inbox);
							disposeGuard.Add<Folder>(folder);
							this.inboxRules = new Rules(folder);
							disposeGuard.Success();
							goto IL_8C;
						}
					}
					if (this.inboxRules.ServerRules == null)
					{
						Rules rules = this.inboxRules;
						this.inboxRules = new Rules(rules.Folder);
					}
					IL_8C:
					result = this.inboxRules;
				}
				return result;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public Stream GetUnsearchableItems()
		{
			if (!this.IsRemote)
			{
				return new UnsearchableItemsStream(this);
			}
			return base.Mailbox.OpenPropertyStream(InternalSchema.UnsearchableItemsStream, PropertyOpenMode.ReadOnly);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public Rules AllInboxRules
		{
			get
			{
				Rules result;
				using (this.CheckObjectState("AllInboxRules::get"))
				{
					base.CheckCapabilities(base.Capabilities.CanHaveRules, "CanHaveRules");
					if (this.allInboxRules == null)
					{
						using (DisposeGuard disposeGuard = default(DisposeGuard))
						{
							Folder folder = Folder.Bind(this, DefaultFolderType.Inbox);
							disposeGuard.Add<Folder>(folder);
							this.allInboxRules = new Rules(folder, true);
							disposeGuard.Success();
						}
					}
					result = this.allInboxRules;
				}
				return result;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00010890 File Offset: 0x0000EA90
		public JunkEmailRule JunkEmailRule
		{
			get
			{
				return this.GetJunkEmailRule(false);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00010899 File Offset: 0x0000EA99
		public JunkEmailRule FilteredJunkEmailRule
		{
			get
			{
				return this.GetJunkEmailRule(true);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000108A4 File Offset: 0x0000EAA4
		private JunkEmailRule GetJunkEmailRule(bool filterJunkEmailRule)
		{
			JunkEmailRule result;
			using (this.CheckObjectState("JunkEmailRule::get"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveJunkEmailRule, "CanHaveJunkEmailRule");
				result = JunkEmailRule.Create(this, filterJunkEmailRule);
			}
			return result;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000108FC File Offset: 0x0000EAFC
		public JunkEmailRule.JunkEmailStatus GetJunkEmailRuleStatus()
		{
			JunkEmailRule.JunkEmailStatus result;
			using (this.CheckObjectState("GetJunkEmailRuleStatus"))
			{
				base.CheckCapabilities(base.Capabilities.CanHaveJunkEmailRule, "CanHaveJunkEmailRule");
				if (base.Mailbox == null || base.Mailbox.MapiStore == null)
				{
					string str = null;
					if (base.Mailbox == null)
					{
						str = ",Mailbox = null";
					}
					else if (base.Mailbox.MapiStore == null)
					{
						str = ",MapiStore = null";
					}
					throw new ConnectionFailedPermanentException(new LocalizedString(ServerStrings.ExStoreSessionDisconnected + str));
				}
				JunkEmailRule.JunkEmailStatus junkEmailStatus = JunkEmailRule.JunkEmailStatus.None;
				MapiFolder mapiFolder = null;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mapiFolder = base.Mailbox.MapiStore.GetInboxFolder();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				using (mapiFolder)
				{
					MapiTable mapiTable = null;
					bool flag2 = false;
					try
					{
						if (this != null)
						{
							this.BeginMapiCall();
							this.BeginServerHealthCall();
							flag2 = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						mapiTable = mapiFolder.GetAssociatedContentsTable();
					}
					catch (MapiPermanentException ex3)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex3, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
							ex3
						});
					}
					catch (MapiRetryableException ex4)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex4, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
							ex4
						});
					}
					finally
					{
						try
						{
							if (this != null)
							{
								this.EndMapiCall();
								if (flag2)
								{
									this.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
					using (mapiTable)
					{
						int num = 26080;
						PropTag propTag = PropTagHelper.PropTagFromIdAndType(num + 12, PropType.String);
						PropTag propTag2 = PropTagHelper.PropTagFromIdAndType(num + 9, PropType.Int);
						PropTag[] columns = new PropTag[]
						{
							propTag,
							propTag2
						};
						bool flag3 = false;
						try
						{
							if (this != null)
							{
								this.BeginMapiCall();
								this.BeginServerHealthCall();
								flag3 = true;
							}
							if (StorageGlobals.MapiTestHookBeforeCall != null)
							{
								StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
							}
							mapiTable.Restrict(new Restriction.ContentRestriction(PropTag.MessageClass, "IPM.ExtendedRule.Message", ContentFlags.IgnoreCase));
							mapiTable.SetColumns(columns);
							mapiTable.SeekRow(BookMark.Beginning, 0);
						}
						catch (MapiPermanentException ex5)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex5, this, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
								ex5
							});
						}
						catch (MapiRetryableException ex6)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex6, this, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
								ex6
							});
						}
						finally
						{
							try
							{
								if (this != null)
								{
									this.EndMapiCall();
									if (flag3)
									{
										this.EndServerHealthCall();
									}
								}
							}
							finally
							{
								if (StorageGlobals.MapiTestHookAfterCall != null)
								{
									StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
								}
							}
						}
						for (;;)
						{
							PropValue[][] array = null;
							bool flag4 = false;
							try
							{
								if (this != null)
								{
									this.BeginMapiCall();
									this.BeginServerHealthCall();
									flag4 = true;
								}
								if (StorageGlobals.MapiTestHookBeforeCall != null)
								{
									StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
								}
								array = mapiTable.QueryRows(10);
							}
							catch (MapiPermanentException ex7)
							{
								throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex7, this, this, "{0}. MapiException = {1}.", new object[]
								{
									string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
									ex7
								});
							}
							catch (MapiRetryableException ex8)
							{
								throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetContentsTable, ex8, this, this, "{0}. MapiException = {1}.", new object[]
								{
									string.Format("MailboxSession::GetJunkEmailRuleStatus.", new object[0]),
									ex8
								});
							}
							finally
							{
								try
								{
									if (this != null)
									{
										this.EndMapiCall();
										if (flag4)
										{
											this.EndServerHealthCall();
										}
									}
								}
								finally
								{
									if (StorageGlobals.MapiTestHookAfterCall != null)
									{
										StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
									}
								}
							}
							if (array.Length == 0)
							{
								goto IL_50A;
							}
							foreach (PropValue[] array3 in array)
							{
								if (array3[0].GetString() == "Junk E-mail Rule")
								{
									goto Block_39;
								}
							}
						}
						Block_39:
						junkEmailStatus |= JunkEmailRule.JunkEmailStatus.IsPresent;
						PropValue[] array3;
						RuleStateFlags @int = (RuleStateFlags)array3[1].GetInt(0);
						if ((@int & RuleStateFlags.Enabled) == RuleStateFlags.Enabled)
						{
							junkEmailStatus |= JunkEmailRule.JunkEmailStatus.IsEnabled;
						}
						if ((@int & RuleStateFlags.Error) == RuleStateFlags.Error)
						{
							junkEmailStatus |= JunkEmailRule.JunkEmailStatus.IsError;
						}
						if ((@int & RuleStateFlags.RuleParseError) == RuleStateFlags.RuleParseError)
						{
							junkEmailStatus |= JunkEmailRule.JunkEmailStatus.IsError;
						}
						return junkEmailStatus;
						IL_50A:;
					}
				}
				result = junkEmailStatus;
			}
			return result;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00011010 File Offset: 0x0000F210
		public override bool CheckSubmissionQuota(int recipientCount)
		{
			bool result;
			using (base.CheckDisposed("CheckSubmissionQuota"))
			{
				if (this.IsRemote)
				{
					throw new NotSupportedException("CheckSubmissionQuota not supported for remote sessions.");
				}
				if (recipientCount == 0)
				{
					result = true;
				}
				else if (this.mailboxOwner.MailboxInfo.Configuration.ThrottlingPolicy.IsNullOrEmpty() && base.LogonType == LogonType.Transport)
				{
					result = true;
				}
				else if (this.MailboxOwner.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox)
				{
					result = true;
				}
				else
				{
					result = MailboxSession.checkSubmissionQuotaDelegate(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.mailboxOwner.MailboxInfo.Location.ServerVersion, this.mailboxOwner.MailboxInfo.MailboxGuid, recipientCount, this.mailboxOwner.MailboxInfo.Configuration.ThrottlingPolicy, this.mailboxOwner.MailboxInfo.OrganizationId, base.ClientInfoString);
				}
			}
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00011120 File Offset: 0x0000F320
		public RoutingTypeOptionsData GetOptionsData(string routingType)
		{
			RoutingTypeOptionsData optionsData;
			using (this.CheckObjectState("GetOptionsData"))
			{
				optionsData = OptionsDataBuilder.Instance.GetOptionsData(this, routingType);
			}
			return optionsData;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00011168 File Offset: 0x0000F368
		public PolicyTagList GetPolicyTagList(RetentionActionType type)
		{
			PolicyTagList result;
			using (base.CheckDisposed("GetPolicyTagList"))
			{
				if (type != (RetentionActionType)0)
				{
					EnumValidator.ThrowIfInvalid<RetentionActionType>(type, "RetentionActionType");
				}
				PolicyTagList policyTagList = null;
				if (this.policyTagListDictionary == null)
				{
					this.policyTagListDictionary = new Dictionary<RetentionActionType, PolicyTagList>();
				}
				else
				{
					this.policyTagListDictionary.TryGetValue(type, out policyTagList);
				}
				if (!this.policyTagListDictionary.ContainsKey(type))
				{
					policyTagList = PolicyTagList.GetPolicyTagListFromMailboxSession(type, this);
					this.policyTagListDictionary[type] = policyTagList;
				}
				result = policyTagList;
			}
			return result;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000111FC File Offset: 0x0000F3FC
		public override ADSessionSettings GetADSessionSettings()
		{
			ADSessionSettings result;
			using (base.CheckDisposed("GetADSessionSettings"))
			{
				ADSessionSettings adsessionSettings;
				if (this.InternalMailboxOwner != null)
				{
					adsessionSettings = this.MailboxOwner.MailboxInfo.OrganizationId.ToADSessionSettings();
				}
				else
				{
					adsessionSettings = OrganizationId.ForestWideOrgId.ToADSessionSettings();
				}
				adsessionSettings.AccountingObject = base.AccountingObject;
				result = adsessionSettings;
			}
			return result;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00011844 File Offset: 0x0000FA44
		public void AuditMailboxAccess(IAuditEvent auditEvent, bool isAsynchronous)
		{
			MailboxSession.<>c__DisplayClass3a CS$<>8__locals1 = new MailboxSession.<>c__DisplayClass3a();
			CS$<>8__locals1.auditEvent = auditEvent;
			CS$<>8__locals1.isAsynchronous = isAsynchronous;
			CS$<>8__locals1.<>4__this = this;
			Util.ThrowOnNullArgument(CS$<>8__locals1.auditEvent, "auditEvent");
			using (MailboxAuditOpticsLogData logData = new MailboxAuditOpticsLogData())
			{
				logData.RecordId = CS$<>8__locals1.auditEvent.RecordId;
				logData.Tenant = CS$<>8__locals1.auditEvent.OrganizationId;
				logData.Mailbox = CS$<>8__locals1.auditEvent.MailboxGuid.ToString();
				logData.Operation = CS$<>8__locals1.auditEvent.OperationName;
				logData.LogonType = CS$<>8__locals1.auditEvent.LogonTypeName;
				logData.OperationSucceeded = CS$<>8__locals1.auditEvent.OperationSucceeded;
				logData.ExternalAccess = CS$<>8__locals1.auditEvent.ExternalAccess;
				logData.Asynchronous = CS$<>8__locals1.isAsynchronous;
				using (base.CreateSessionGuard("AuditMailboxAccess"))
				{
					if (this.defaultFolderManager != null)
					{
						this.BypassAuditsFolderAccessChecking(delegate
						{
							CS$<>8__locals1.<>4__this.DoNothingIfBypassAuditing(delegate
							{
								CS$<>8__locals1.<>4__this.BypassAuditing(delegate
								{
									Stopwatch stopwatch = Stopwatch.StartNew();
									int num = CS$<>8__locals1.isAsynchronous ? 1 : 3;
									bool flag = false;
									IAuditLogRecord logRecord = CS$<>8__locals1.auditEvent.GetLogRecord();
									Exception ex = null;
									int i = 0;
									IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
									if (currentActivityScope != null)
									{
										logData.ActionContext = string.Format("{0}.{1}.{2}.{3}", new object[]
										{
											currentActivityScope.Component,
											currentActivityScope.Feature,
											currentActivityScope.Action,
											currentActivityScope.ClientInfo
										});
										logData.ClientRequestId = currentActivityScope.ClientRequestId;
									}
									logData.ActionContext = string.Format("{0}.{1}", logData.ActionContext, (CS$<>8__locals1.<>4__this.RemoteClientSessionInfo != null) ? CS$<>8__locals1.<>4__this.RemoteClientSessionInfo.ClientInfoString : CS$<>8__locals1.<>4__this.ClientInfoString);
									try
									{
										CS$<>8__locals1.<>4__this.copyOnWriteNotification.CheckAndCreateAuditsFolder(CS$<>8__locals1.<>4__this);
										while (i < num)
										{
											ex = null;
											try
											{
												logData.RecordSize = CS$<>8__locals1.<>4__this.GetAuditLog(logRecord.CreationTime).WriteAuditRecord(logRecord);
												stopwatch.Stop();
												COWSession.PerfCounters.TotalAuditSave.Increment();
												COWSession.PerfCounters.TotalAuditSaveTime.IncrementBy(stopwatch.ElapsedMilliseconds);
												logData.LoggingTime = stopwatch.ElapsedMilliseconds;
												flag = true;
												if (ExTraceGlobals.SessionTracer.IsTraceEnabled(TraceType.DebugTrace))
												{
													ExTraceGlobals.SessionTracer.TraceDebug<string, string>((long)CS$<>8__locals1.<>4__this.GetHashCode(), "[MailboxSession::AuditMailboxAccess] audit log has been committed successfully to mailbox {0}: {1}", CS$<>8__locals1.<>4__this.mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), AuditLogParseSerialize.GetAsString(logRecord));
												}
												logData.AuditSucceeded = true;
												break;
											}
											catch (IOException ex2)
											{
												ex = ex2;
												if (CS$<>8__locals1.isAsynchronous)
												{
													throw;
												}
											}
											catch (TransientException ex3)
											{
												ex = ex3;
												if (CS$<>8__locals1.isAsynchronous)
												{
													throw;
												}
											}
											catch (LocalizedException ex4)
											{
												ex = ex4;
												if (CS$<>8__locals1.isAsynchronous)
												{
													throw;
												}
												break;
											}
											catch (Exception ex5)
											{
												ex = ex5;
												throw;
											}
											finally
											{
												if (ex != null)
												{
													ExTraceGlobals.SessionTracer.TraceError<string, Guid, Exception>((long)CS$<>8__locals1.<>4__this.GetHashCode(), "Error occurred while saving audit information for mailbox '{0}' {1}. Exception details:/r/n{2}", CS$<>8__locals1.<>4__this.mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), CS$<>8__locals1.<>4__this.MailboxGuid, ex);
												}
											}
											i++;
										}
									}
									catch (TransientException ex6)
									{
										ExTraceGlobals.SessionTracer.TraceError<string, Guid, TransientException>((long)CS$<>8__locals1.<>4__this.GetHashCode(), "Error occurred while CheckAndCreateAuditsFolder for mailbox '{0}' {1}. Exception details:/r/n{2}", CS$<>8__locals1.<>4__this.mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), CS$<>8__locals1.<>4__this.MailboxGuid, ex6);
										ex = ex6;
										if (CS$<>8__locals1.isAsynchronous)
										{
											throw;
										}
									}
									finally
									{
										if (!flag)
										{
											string text = CS$<>8__locals1.<>4__this.mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() ?? string.Empty;
											string text2 = string.Empty;
											string text3 = AuditLogParseSerialize.GetAsString(logRecord);
											if (ex != null)
											{
												text2 = ex.ToString();
											}
											ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3292933437U, ref text3);
											if (text.Length > 30000)
											{
												text = text.Substring(0, 30000);
												text2 = string.Empty;
												text3 = string.Empty;
											}
											else if (text.Length + text2.Length > 30000)
											{
												text2 = text2.Substring(0, 30000 - text.Length);
												text3 = string.Empty;
											}
											else if (text.Length + text2.Length + text3.Length > 30000)
											{
												text3 = text3.Substring(0, 30000 - text.Length - text2.Length);
											}
											ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorSavingMailboxAudit, CS$<>8__locals1.<>4__this.mailboxOwner.ObjectId.ToString(), new object[]
											{
												text,
												CS$<>8__locals1.<>4__this.MailboxGuid,
												i,
												text2,
												text3
											});
											if (stopwatch.IsRunning)
											{
												stopwatch.Stop();
											}
											logData.AuditSucceeded = false;
											logData.LoggingError = ex;
											logData.LoggingTime = stopwatch.ElapsedMilliseconds;
										}
									}
								});
							});
						});
					}
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000119DC File Offset: 0x0000FBDC
		private IAuditLog GetAuditLog(DateTime timestamp)
		{
			if (AuditFeatureManager.IsPartitionedMailboxLogEnabled(this.mailboxOwner))
			{
				if (this.auditLog == null || this.auditLog.EstimatedLogEndTime < timestamp)
				{
					AuditLogCollection auditLogCollection = new AuditLogCollection(this, this.GetAuditsFolderId(), ExTraceGlobals.SessionTracer, (IAuditLogRecord record, MessageItem message) => AuditLogParseSerialize.SerializeMailboxAuditRecord(record, message));
					if (!auditLogCollection.FindLog(timestamp, true, out this.auditLog))
					{
						this.auditLog = new AuditLog(this, this.GetAuditsFolderId(), DateTime.MinValue, DateTime.MaxValue, 0, (IAuditLogRecord record, MessageItem message) => AuditLogParseSerialize.SerializeMailboxAuditRecord(record, message));
					}
				}
			}
			else if (this.auditLog == null)
			{
				this.auditLog = new AuditLog(this, this.GetAuditsFolderId(), DateTime.MinValue, DateTime.MaxValue, 0, (IAuditLogRecord record, MessageItem message) => AuditLogParseSerialize.SerializeMailboxAuditRecord(record, message));
			}
			return this.auditLog;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011ADA File Offset: 0x0000FCDA
		private static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (SslConfiguration.AllowExternalUntrustedCerts)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string, SslPolicyErrors>((long)sender.GetHashCode(), "MailboxSession::CertificateErrorHandler. Allowed SSL certificate {0} with error {1}", certificate.Subject, sslPolicyErrors);
				return true;
			}
			return false;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011B08 File Offset: 0x0000FD08
		private bool IgnoreActiveManagerSiteBoundary()
		{
			return base.LogonType == LogonType.Transport;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00011B13 File Offset: 0x0000FD13
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00011B1A File Offset: 0x0000FD1A
		internal static MailboxSession.CheckSubmissionQuotaDelegate CheckSubmissionQuotaImpl
		{
			get
			{
				return MailboxSession.checkSubmissionQuotaDelegate;
			}
			set
			{
				MailboxSession.checkSubmissionQuotaDelegate = value;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011B24 File Offset: 0x0000FD24
		private static void RegisterCallback()
		{
			if (MailboxSession.registeredCertificateErrorHandler)
			{
				return;
			}
			lock (MailboxSession.lockRegisterCertificateErrorHandler)
			{
				if (!MailboxSession.registeredCertificateErrorHandler)
				{
					CertificateValidationManager.RegisterCallback("XRopXsoClient", new RemoteCertificateValidationCallback(MailboxSession.CertificateErrorHandler));
					MailboxSession.registeredCertificateErrorHandler = true;
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00011B8C File Offset: 0x0000FD8C
		private void Initialize(MapiStore linkedStore, LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegateUser, object identity, OpenMailboxSessionFlags flags, GenericIdentity auxiliaryIdentity)
		{
			this.Initialize(linkedStore, logonType, owner, delegateUser, identity, flags, auxiliaryIdentity, false);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00011BAC File Offset: 0x0000FDAC
		private void Initialize(MapiStore linkedStore, LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegateUser, object identity, OpenMailboxSessionFlags flags, GenericIdentity auxiliaryIdentity, bool unifiedSession)
		{
			ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "MailboxSession::Initialize.");
			if (owner == null)
			{
				ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "MailboxSession::Initialize. The in parameter {0} should not be null.", "owner");
				throw new ArgumentNullException("owner");
			}
			if (identity == null)
			{
				ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "MailboxSession::Initialize. The in parameter {0} should not be null.", "identity");
				throw new ArgumentNullException("identity");
			}
			EnumValidator<OpenMailboxSessionFlags>.ThrowIfInvalid(flags, "flags");
			this.delegateUser = delegateUser.ADOrgPerson;
			this.mailboxOwner = owner;
			base.LogonType = logonType;
			Guid mailboxGuid = this.MailboxOwner.MailboxInfo.MailboxGuid;
			this.IsUnified = unifiedSession;
			this.identity = identity;
			this.auxiliaryIdentity = auxiliaryIdentity;
			this.useNamedProperties = flags.HasFlag(OpenMailboxSessionFlags.UseNamedProperties);
			this.nonInteractiveSession = flags.HasFlag(OpenMailboxSessionFlags.NonInteractiveSession);
			base.IsMoveUser = flags.HasFlag(OpenMailboxSessionFlags.MoveUser);
			base.IsXForestMove = flags.HasFlag(OpenMailboxSessionFlags.XForestMove);
			base.IsOlcMoveDestination = flags.HasFlag(OpenMailboxSessionFlags.OlcSync);
			base.StoreFlag |= OpenStoreFlag.NoMail;
			if (flags.HasFlag(OpenMailboxSessionFlags.OverrideHomeMdb))
			{
				base.StoreFlag |= OpenStoreFlag.OverrideHomeMdb;
			}
			if (flags.HasFlag(OpenMailboxSessionFlags.DisconnectedMailbox))
			{
				base.StoreFlag |= OpenStoreFlag.DisconnectedMailbox;
			}
			if (flags.HasFlag(OpenMailboxSessionFlags.XForestMove))
			{
				base.StoreFlag |= OpenStoreFlag.XForestMove;
			}
			if (flags.HasFlag(OpenMailboxSessionFlags.ReadOnly))
			{
				base.Capabilities = base.Capabilities.CloneAndExtendCapabilities(SessionCapabilitiesFlags.ReadOnly);
			}
			if (!this.mailboxOwner.MailboxInfo.MailboxDatabase.IsNullOrEmpty() && this.mailboxOwner.MailboxInfo.MailboxGuid != Guid.Empty && !this.mailboxOwner.MailboxInfo.IsRemote)
			{
				base.StoreFlag |= OpenStoreFlag.MailboxGuid;
			}
			if (flags.HasFlag(OpenMailboxSessionFlags.UseRecoveryDatabase))
			{
				if (!base.StoreFlag.HasFlag(OpenStoreFlag.MailboxGuid))
				{
					throw new ArgumentException("must be logging in with GUIDs, not legDN", "owner");
				}
				base.StoreFlag |= (OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.TakeOwnership | OpenStoreFlag.OverrideHomeMdb | OpenStoreFlag.FailIfNoMailbox | OpenStoreFlag.NoLocalization | OpenStoreFlag.RestoreDatabase);
			}
			if (flags.HasFlag(OpenMailboxSessionFlags.RequestLocalRpcConnection))
			{
				MailboxSession.CheckNoRemoteExchangePrincipal(this.MailboxOwner);
				this.connectFlag |= ConnectFlag.LocalRpcOnly;
			}
			if (this.MailboxOwner.MailboxInfo.Location.ServerVersion < Server.E15MinVersion)
			{
				this.connectFlag |= ConnectFlag.IsPreExchange15;
			}
			if (this.MailboxOwner.RecipientTypeDetails == RecipientTypeDetails.MonitoringMailbox)
			{
				this.connectFlag |= ConnectFlag.MonitoringMailbox;
			}
			if (flags.HasFlag(OpenMailboxSessionFlags.RequestExchangeRpcServer) || StoreSession.TestRequestExchangeRpcServer)
			{
				this.connectFlag |= ConnectFlag.ConnectToExchangeRpcServerOnly;
			}
			else if (!this.MailboxOwner.MailboxInfo.IsRemote && this.MailboxOwner.MailboxInfo.Location.IsLegacyServer())
			{
				this.connectFlag |= ConnectFlag.AllowLegacyStore;
			}
			if (base.LogonType == LogonType.BestAccess)
			{
				if (string.IsNullOrEmpty(delegateUser.LegacyDn))
				{
					this.userLegacyDn = this.MailboxOwner.LegacyDn;
				}
				else
				{
					this.userLegacyDn = delegateUser.LegacyDn;
				}
				this.connectFlag |= ConnectFlag.UseDelegatedAuthPrivilege;
				base.StoreFlag |= OpenStoreFlag.TakeOwnership;
			}
			else if (base.LogonType == LogonType.Owner)
			{
				this.userLegacyDn = this.MailboxOwner.LegacyDn;
				this.connectFlag |= ConnectFlag.UseDelegatedAuthPrivilege;
				base.StoreFlag |= OpenStoreFlag.TakeOwnership;
			}
			else if (base.LogonType == LogonType.Delegated)
			{
				this.userLegacyDn = delegateUser.LegacyDn;
				this.connectFlag |= ConnectFlag.UseDelegatedAuthPrivilege;
				base.StoreFlag |= OpenStoreFlag.TakeOwnership;
			}
			else if (base.LogonType == LogonType.Admin || base.LogonType == LogonType.SystemService)
			{
				this.connectFlag |= ConnectFlag.UseAdminPrivilege;
				base.StoreFlag |= (OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.TakeOwnership);
				if ((flags & OpenMailboxSessionFlags.AllowAdminLocalization) != OpenMailboxSessionFlags.AllowAdminLocalization)
				{
					base.StoreFlag |= OpenStoreFlag.NoLocalization;
				}
				this.TryGetServiceUserLegacyDn(out this.userLegacyDn);
			}
			else if (base.LogonType == LogonType.DelegatedAdmin)
			{
				this.connectFlag |= ConnectFlag.UseDelegatedAuthPrivilege;
				base.StoreFlag |= (OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.TakeOwnership);
				if ((flags & OpenMailboxSessionFlags.AllowAdminLocalization) != OpenMailboxSessionFlags.AllowAdminLocalization)
				{
					base.StoreFlag |= OpenStoreFlag.NoLocalization;
				}
				this.userLegacyDn = delegateUser.LegacyDn;
			}
			else if (base.LogonType == LogonType.Transport)
			{
				this.connectFlag |= ConnectFlag.UseTransportPrivilege;
				base.StoreFlag |= OpenStoreFlag.NoLocalization;
				if ((flags & OpenMailboxSessionFlags.OpenForQuotaMessageDelivery) == OpenMailboxSessionFlags.OpenForQuotaMessageDelivery)
				{
					base.StoreFlag |= OpenStoreFlag.DeliverQuotaMessage;
				}
				if ((flags & OpenMailboxSessionFlags.OpenForNormalMessageDelivery) == OpenMailboxSessionFlags.OpenForNormalMessageDelivery)
				{
					base.StoreFlag |= OpenStoreFlag.DeliverNormalMessage;
				}
				if ((flags & OpenMailboxSessionFlags.OpenForSpecialMessageDelivery) == OpenMailboxSessionFlags.OpenForSpecialMessageDelivery)
				{
					base.StoreFlag |= OpenStoreFlag.DeliverSpecialMessage;
				}
				this.TryGetServiceUserLegacyDn(out this.userLegacyDn);
			}
			ExTraceGlobals.SessionTracer.Information<string, string>((long)this.GetHashCode(), "MailboxSession::Initialize. server = {0}, user = {1}.", this.mailboxOwner.MailboxInfo.IsRemote ? string.Empty : this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn);
			MapiStore mapiStore = null;
			bool flag = false;
			try
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "MailboxSession::Initialize. server = {0}, user = {1}, connectFlag = {2}, storeFlag = {3}, culture = {4}, clientInfoString = {5}, flags = {6}.", new object[]
				{
					this.mailboxOwner.MailboxInfo.IsRemote ? string.Empty : this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
					this.userLegacyDn,
					this.connectFlag,
					base.StoreFlag,
					this.InternalPreferedCulture,
					base.ClientInfoString,
					(int)flags
				});
				this.connectFlag |= ConnectFlag.LowMemoryFootprint;
				mapiStore = this.ForceOpen(linkedStore, unifiedSession);
				base.IsConnected = true;
				if (base.LogonType == LogonType.BestAccess)
				{
					if (this.CanActAsOwner)
					{
						base.LogonType = LogonType.Owner;
					}
					else
					{
						if (owner.MailboxInfo.IsArchive)
						{
							throw new AccessDeniedException(ServerStrings.ExMailboxAccessDenied(this.mailboxOwner.MailboxInfo.DisplayName, "Archive mailbox:" + this.mailboxOwner.MailboxInfo.MailboxGuid.ToString()));
						}
						if (owner.MailboxInfo.IsAggregated)
						{
							throw new AccessDeniedException(ServerStrings.ExMailboxAccessDenied(this.mailboxOwner.MailboxInfo.DisplayName, "Aggregated mailbox:" + this.mailboxOwner.MailboxInfo.MailboxGuid));
						}
						base.LogonType = LogonType.Delegated;
						base.StoreFlag &= ~OpenStoreFlag.TakeOwnership;
					}
				}
				if (base.LogonType != LogonType.Admin || base.ClientInfoString == null || !MailboxSession.AllowedClientsForPublicFolderMailbox.IsMatch(base.ClientInfoString))
				{
					object obj = base.Mailbox.TryGetProperty(MailboxSchema.MailboxType);
					if (obj is int && StoreSession.IsPublicFolderMailbox((int)obj))
					{
						throw new AccessDeniedException(ServerStrings.OperationNotSupportedOnPublicFolderMailbox);
					}
				}
				if ((flags & OpenMailboxSessionFlags.InitDefaultFolders) == OpenMailboxSessionFlags.InitDefaultFolders && Util.Contains(this.foldersToInit, DefaultFolderType.Reminders) && (flags & OpenMailboxSessionFlags.InitUserConfigurationManager) != OpenMailboxSessionFlags.InitUserConfigurationManager)
				{
					throw new InvalidOperationException("Must have UserConfigurationManager to init Reminders folder");
				}
				if ((flags & OpenMailboxSessionFlags.InitUserConfigurationManager) == OpenMailboxSessionFlags.InitUserConfigurationManager)
				{
					this.userConfigurationManager = new UserConfigurationManager(this);
				}
				if ((flags & OpenMailboxSessionFlags.InitDefaultFolders) == OpenMailboxSessionFlags.InitDefaultFolders)
				{
					this.InternalInitializeDefaultFolders(this.foldersToInit, flags);
					if ((base.StoreFlag & OpenStoreFlag.NoLocalization) != OpenStoreFlag.NoLocalization && !Util.IsSpecialLcid(this.InternalPreferedCulture.LCID) && base.LogonType != LogonType.Delegated && !this.IsMailboxLocalized)
					{
						this.InternalLocalizeInitialDefaultFolders(flags);
					}
				}
				if ((flags & OpenMailboxSessionFlags.InitCopyOnWrite) == OpenMailboxSessionFlags.InitCopyOnWrite)
				{
					if ((flags & OpenMailboxSessionFlags.UseRecoveryDatabase) != OpenMailboxSessionFlags.None)
					{
						throw new ArgumentException("No CopyOnWrite allowed for Recovery DB logons", "flags");
					}
					if (!owner.MailboxInfo.IsRemote)
					{
						this.copyOnWriteNotification = COWSession.Create(this);
					}
				}
				if ((flags & OpenMailboxSessionFlags.InitCheckPrivateItemsAccess) == OpenMailboxSessionFlags.InitCheckPrivateItemsAccess && base.LogonType == LogonType.Delegated)
				{
					this.CheckPrivateItemsAccessPermission(delegateUser.LegacyDn);
				}
				this.activitySession = Microsoft.Exchange.Data.Storage.ActivitySession.Create(this);
				if (Utils.IsTeamMailbox(this))
				{
					try
					{
						TeamMailboxNotificationHelper.SendWelcomeMessageIfNeeded(this);
					}
					catch (StorageTransientException ex)
					{
						ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), string.Format("MailboxSession::Initialize. Failed to send welcome message for site mailbox because of {0} with detail: {1}", ex.GetType(), ex.Message));
					}
					catch (StoragePermanentException ex2)
					{
						ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), string.Format("MailboxSession::Initialize. Failed to send welcome message for site mailbox because of {0} with detail: {1}", ex2.GetType(), ex2.Message));
					}
					if (!string.IsNullOrEmpty(base.ClientInfoString) && (base.ClientInfoString.StartsWith("Client=OWA", StringComparison.OrdinalIgnoreCase) || base.ClientInfoString.StartsWith("Client=MSExchangeRPC", StringComparison.OrdinalIgnoreCase)))
					{
						string client;
						if (base.ClientInfoString.StartsWith("Client=OWA", StringComparison.OrdinalIgnoreCase))
						{
							client = "Client=OWA";
						}
						else
						{
							client = "Client=MSExchangeRPC";
						}
						this.siteMailboxSynchronizerReference = SiteMailboxSynchronizerManager.Instance.GetSiteMailboxSynchronizer(this.mailboxOwner, client);
					}
				}
				try
				{
					ExtendedRuleConditionConstraint.InitExtendedRuleSizeLimitIfNeeded(this);
				}
				catch (StoragePermanentException ex3)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), string.Format("MailboxSession::Initialize. Failed to initialize extended rule size limit because of {0} with detail: {1}", ex3.GetType(), ex3.Message));
				}
				catch (StorageTransientException ex4)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), string.Format("MailboxSession::Initialize. Failed to initialize extended rule size limit because of {0} with detail: {1}", ex4.GetType(), ex4.Message));
				}
				OrganizationContentConversionProperties organizationContentConversionProperties;
				if (StoreSession.directoryAccessor.TryGetOrganizationContentConversionProperties(this.OrganizationId, out organizationContentConversionProperties))
				{
					base.PreferredInternetCodePageForShiftJis = organizationContentConversionProperties.PreferredInternetCodePageForShiftJis;
					base.RequiredCoverage = organizationContentConversionProperties.RequiredCharsetCoverage;
				}
				if ((flags & OpenMailboxSessionFlags.InitDeadSessionChecking) == OpenMailboxSessionFlags.InitDeadSessionChecking)
				{
					base.StartDeadSessionChecking();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (this.copyOnWriteNotification != null)
					{
						this.copyOnWriteNotification.Dispose();
					}
					this.copyOnWriteNotification = null;
					base.IsConnected = false;
					base.SetMailboxStoreObject(null);
					if (mapiStore != null)
					{
						mapiStore.Dispose();
						mapiStore = null;
					}
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00012718 File Offset: 0x00010918
		internal static MailboxSession CreateDummyInstance()
		{
			IGenericADUser adUser = new GenericADUser
			{
				MailboxDatabase = new ADObjectId(Guid.NewGuid()),
				LegacyDn = " ",
				OrganizationId = null,
				DisplayName = " ",
				PrimarySmtpAddress = new SmtpAddress("foo@contoso.com"),
				MailboxGuid = Guid.NewGuid(),
				GrantSendOnBehalfTo = new ADMultiValuedProperty<ADObjectId>(),
				Languages = Array<CultureInfo>.Empty,
				RecipientType = RecipientType.UserMailbox,
				RecipientTypeDetails = RecipientTypeDetails.None,
				ObjectId = new ADObjectId(Guid.NewGuid()),
				MasterAccountSid = null,
				AggregatedMailboxGuids = Array<Guid>.Empty
			};
			return new MailboxSession
			{
				mailboxOwner = new UserPrincipalBuilder(adUser).Build(),
				identity = WindowsIdentity.GetCurrent()
			};
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000127DF File Offset: 0x000109DF
		private MailboxSession()
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000127F4 File Offset: 0x000109F4
		private static void InternalValidateServerVersion(IExchangePrincipal owner)
		{
			if (owner.MailboxInfo.Location.ServerVersion != 0)
			{
				ServerVersion serverVersion = new ServerVersion(owner.MailboxInfo.Location.ServerVersion);
				if (serverVersion.Major < StoreSession.CurrentServerMajorVersion)
				{
					throw new NotSupportedWithServerVersionException(owner.MailboxInfo.PrimarySmtpAddress.ToString(), serverVersion.Major, StoreSession.CurrentServerMajorVersion);
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00012860 File Offset: 0x00010A60
		private static void InternalValidateDatacenterAccess(LogonType logonType, IExchangePrincipal owner, DelegateLogonUser delegatedUser)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.DataStorage.CheckLicense.Enabled)
			{
				if ((logonType == LogonType.Owner || logonType == LogonType.BestAccess || logonType == LogonType.Admin || logonType == LogonType.Delegated || logonType == LogonType.DelegatedAdmin) && StoreSession.directoryAccessor.IsTenantAccessBlocked(owner.MailboxInfo.OrganizationId))
				{
					throw new TenantAccessBlockedException(ServerStrings.ExTenantAccessBlocked(owner.MailboxInfo.OrganizationId.ToString()));
				}
				if (MailboxSession.IsOwnerLogon(logonType, owner, delegatedUser) && !MailboxSession.AllowMailboxLogon(owner))
				{
					throw new InvalidLicenseException(ServerStrings.ExInvalidLicense(owner.MailboxInfo.DisplayName));
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000128F4 File Offset: 0x00010AF4
		private static bool AllowMailboxLogon(IExchangePrincipal exchangePrincipal)
		{
			bool flag = StoreSession.directoryAccessor.IsLicensingEnforcedInOrg(exchangePrincipal.MailboxInfo.OrganizationId);
			return exchangePrincipal.RecipientTypeDetails != RecipientTypeDetails.UserMailbox || !flag || (!StoreSession.directoryAccessor.IsTenantAccessBlocked(exchangePrincipal.MailboxInfo.OrganizationId) && CapabilityHelper.AllowMailboxLogon(exchangePrincipal.MailboxInfo.Configuration.SkuCapability, exchangePrincipal.MailboxInfo.Configuration.SkuAssigned, exchangePrincipal.MailboxInfo.WhenMailboxCreated));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00012970 File Offset: 0x00010B70
		private static UnifiedGroupMemberType GetUserMembershipType(IExchangePrincipal groupMailbox, AccessingUserInfo accessUserInfo, ClientSecurityContext clientSecurityContext, string clientInfoString)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("clientInfoString", clientInfoString);
			GroupMailboxAuthorizationHandler groupMailboxAuthorizationHandler = new GroupMailboxAuthorizationHandler(groupMailbox, accessUserInfo, clientInfoString, clientSecurityContext, groupMailbox.GetConfiguration());
			UnifiedGroupMemberType membershipType = groupMailboxAuthorizationHandler.GetMembershipType();
			if (membershipType == UnifiedGroupMemberType.None && groupMailbox.ModernGroupType == ModernGroupObjectType.Private)
			{
				throw new AccessDeniedException(ServerStrings.NotAuthorizedtoAccessGroupMailbox(accessUserInfo.LegacyExchangeDN, groupMailbox.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			return membershipType;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000129D8 File Offset: 0x00010BD8
		private MapiStore InternalGetRemoteConnection()
		{
			bool flag = false;
			MapiStore mapiStore = null;
			Client client = null;
			try
			{
				ConnectFlag connectFlag = (this.connectFlag & ~ConnectFlag.UseDelegatedAuthPrivilege) | ConnectFlag.ConnectToExchangeRpcServerOnly;
				if (base.LogonType == LogonType.SystemService)
				{
					connectFlag |= ConnectFlag.RemoteSystemService;
				}
				Uri remoteEndpoint = base.RemoteMailboxProperties.GetRemoteEndpoint(this.mailboxOwner.MailboxInfo.RemoteIdentity);
				if (this.mailboxOwner.MailboxInfo.RemoteIdentity == null || remoteEndpoint == null)
				{
					throw new MailboxOfflineException(ServerStrings.CannotAccessRemoteMailbox);
				}
				string text = "SMTP:" + this.mailboxOwner.MailboxInfo.RemoteIdentity.ToString();
				ExternalAuthentication current = ExternalAuthentication.GetCurrent();
				if (!current.Enabled)
				{
					string text2 = this.mailboxOwner.MailboxInfo.RemoteIdentity.Value.ToString();
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ExternalAuthDisabledMailboxSession, text2, new object[]
					{
						text2
					});
					ExTraceGlobals.XtcTracer.TraceError<string>(0L, "External authentification is disabled, remote mailbox/archive access for user {0} will be disabled.", text2);
					throw new MailboxOfflineException(ServerStrings.CannotAccessRemoteMailbox);
				}
				MailboxSession.RegisterCallback();
				string text3 = string.Format("{0}/{1}", remoteEndpoint, CertificateValidationManager.GenerateComponentIdQueryString("XRopXsoClient"));
				Uri internetWebProxy = null;
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && null != localServer.InternetWebProxy)
				{
					ExTraceGlobals.SessionTracer.TraceDebug<Uri>((long)this.GetHashCode(), "Using configured InternetWebProxy {0}", localServer.InternetWebProxy);
					internetWebProxy = localServer.InternetWebProxy;
				}
				FederatedClientCredentials federatedCredentialsForDelegation = base.RemoteMailboxProperties.GetFederatedCredentialsForDelegation(current);
				client = new Client(federatedCredentialsForDelegation, new Uri(text3), internetWebProxy, this.mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), !this.nonInteractiveSession);
				bool flag2 = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag2 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mapiStore = MapiStore.OpenRemoteMailbox(text3, (base.LogonType == LogonType.Admin || base.LogonType == LogonType.SystemService) ? null : text, text, connectFlag, base.StoreFlag, this.InternalPreferedCulture, client, text, base.ClientInfoString, ClientSessionInfo.WrapInfoForRemoteServer(this));
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.mailboxOwner.MailboxInfo.RemoteIdentity.ToString()), ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::ForceOpen. Endpoint = {0}, Identity = {1}, User = {2}.", remoteEndpoint, this.mailboxOwner.MailboxInfo.RemoteIdentity, this.userLegacyDn),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.mailboxOwner.MailboxInfo.RemoteIdentity.ToString()), ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSession::ForceOpen. Endpoint = {0}, Identity = {1}, User = {2}.", remoteEndpoint, this.mailboxOwner.MailboxInfo.RemoteIdentity, this.userLegacyDn),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag2)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (mapiStore != null)
					{
						mapiStore.Dispose();
						mapiStore = null;
					}
					if (client != null)
					{
						client.Dispose();
						client = null;
					}
				}
			}
			return mapiStore;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00012E74 File Offset: 0x00011074
		private MapiStore InternalGetAggregatedMailboxConnection(MapiStore linkedStore, ClientIdentityInfo clientIdentity, byte[] partitionHint)
		{
			return this.InternalGetAggregatedMailboxConnection(linkedStore, () => MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), null, null, null, this.connectFlag, this.StoreFlag, this.InternalPreferedCulture, clientIdentity, this.ClientInfoString, partitionHint), partitionHint);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00012F5C File Offset: 0x0001115C
		private MapiStore InternalGetAggregatedMailboxConnection(MapiStore linkedStore, WindowsIdentity windowsIdentity, byte[] partitionHint)
		{
			return this.InternalGetAggregatedMailboxConnection(linkedStore, () => MapiStore.OpenMailbox(this.mailboxOwner.MailboxInfo.Location.ServerFqdn, this.userLegacyDn, this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), null, null, null, this.connectFlag, this.StoreFlag, this.InternalPreferedCulture, windowsIdentity, this.ClientInfoString, partitionHint), partitionHint);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00012FA0 File Offset: 0x000111A0
		private MapiStore InternalGetAggregatedMailboxConnection(MapiStore linkedStore, Func<MapiStore> openMailbox, byte[] partitionHint)
		{
			MapiStore result = null;
			base.StoreFlag |= OpenStoreFlag.MailboxGuid;
			ExTraceGlobals.SessionTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "MailboxSession::ForceOpen. Use user's MailboxGuid. MailboxGuid = {0}.", this.InternalMailboxOwner);
			if (linkedStore == null)
			{
				bool flag = false;
				try
				{
					try
					{
						if (this != null)
						{
							this.BeginMapiCall();
							this.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						result = openMailbox();
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, alt mbx Guid = {3}.", new object[]
							{
								this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
								this.userLegacyDn,
								this.mailboxOwner.LegacyDn,
								this.mailboxOwner.MailboxInfo.MailboxGuid
							}),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex2, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, alt mbx Guid = {3}.", new object[]
							{
								this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
								this.userLegacyDn,
								this.mailboxOwner.LegacyDn,
								this.mailboxOwner.MailboxInfo.MailboxGuid
							}),
							ex2
						});
					}
					return result;
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			bool flag2 = false;
			try
			{
				if (this != null)
				{
					this.BeginMapiCall();
					this.BeginServerHealthCall();
					flag2 = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = linkedStore.OpenAlternateMailbox(this.mailboxOwner.MailboxInfo.MailboxGuid, this.mailboxOwner.MailboxInfo.GetDatabaseGuid(), base.StoreFlag, this.InternalPreferedCulture, base.ClientInfoString, partitionHint);
			}
			catch (MapiPermanentException ex3)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex3, this, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, alt mbx Guid = {3}.", new object[]
					{
						this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
						this.userLegacyDn,
						this.mailboxOwner.LegacyDn,
						this.mailboxOwner.MailboxInfo.MailboxGuid
					}),
					ex3
				});
			}
			catch (MapiRetryableException ex4)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex4, this, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MailboxSession::ForceOpen. server = {0}, user = {1}, owner = {2}, alt mbx Guid = {3}.", new object[]
					{
						this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
						this.userLegacyDn,
						this.mailboxOwner.LegacyDn,
						this.mailboxOwner.MailboxInfo.MailboxGuid
					}),
					ex4
				});
			}
			finally
			{
				try
				{
					if (this != null)
					{
						this.EndMapiCall();
						if (flag2)
						{
							this.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000133F8 File Offset: 0x000115F8
		private bool ShouldThrowWrongServerException(IExchangePrincipal mailboxOwner)
		{
			return !this.IgnoreActiveManagerSiteBoundary() && !mailboxOwner.IsCrossSiteAccessAllowed && mailboxOwner.MailboxInfo.Location != null && !mailboxOwner.MailboxInfo.Location.IsLocal();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001342C File Offset: 0x0001162C
		private void InternalDisposeServerObjects()
		{
			if (this.siteMailboxSynchronizerReference != null)
			{
				this.siteMailboxSynchronizerReference.Dispose();
				this.siteMailboxSynchronizerReference = null;
			}
			if (this.inboxRules != null)
			{
				this.inboxRules.Folder.Dispose();
				this.inboxRules = null;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00013468 File Offset: 0x00011668
		private void CaptureMarkAsClutterOrNotClutter(FolderChangeOperation operation, FolderChangeOperationFlags flags, GroupOperationResult result, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId)
		{
			UserMoveActionHandler userMoveActionHandler = null;
			if (UserMoveActionHandler.TryCreate(this, operation, flags, result, sourceFolderId, destinationFolderId, out userMoveActionHandler))
			{
				userMoveActionHandler.HandleUserMoves();
			}
		}

		// Token: 0x0400003D RID: 61
		private const string FreeBusyMessage = "LocalFreebusy";

		// Token: 0x0400003E RID: 62
		private const int RecipientCacheSize = 1024;

		// Token: 0x0400003F RID: 63
		private const MailboxSession.InitializationFlags OwnerFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties;

		// Token: 0x04000040 RID: 64
		private const MailboxSession.InitializationFlags TransportFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties;

		// Token: 0x04000041 RID: 65
		private const MailboxSession.InitializationFlags AdminFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.UseNamedProperties;

		// Token: 0x04000042 RID: 66
		private const MailboxSession.InitializationFlags SystemServiceFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties;

		// Token: 0x04000043 RID: 67
		private const MailboxSession.InitializationFlags MrsFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties;

		// Token: 0x04000044 RID: 68
		private const MailboxSession.InitializationFlags DelegateFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.CopyOnWrite | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.CheckPrivateItemsAccess | MailboxSession.InitializationFlags.UseNamedProperties;

		// Token: 0x04000045 RID: 69
		private const int ErrorSavingMailboxAuditLengthThreshold = 30000;

		// Token: 0x04000046 RID: 70
		private const string RemoteMailboxComponentId = "XRopXsoClient";

		// Token: 0x04000047 RID: 71
		private const string GroupMailboxOperation = "MungeUserToken";

		// Token: 0x04000048 RID: 72
		private static readonly Regex AllowedClientsForPublicFolderMailbox = new Regex(string.Format("\\A({0}|{1});Action=(SetMailboxFolderPermissionBase|Get-MailboxFolderPermission|Test-MapiConnectivity)", "Client=Management", "Client=Monitoring"), RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);

		// Token: 0x04000049 RID: 73
		private static DefaultFolderType[] allDefaultFolders = (DefaultFolderType[])Enum.GetValues(typeof(DefaultFolderType));

		// Token: 0x0400004A RID: 74
		private static readonly PropertyDefinition[] IdDefinition = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x0400004B RID: 75
		private static readonly PropertyDefinition[] DelegateDefinitions = new PropertyDefinition[]
		{
			InternalSchema.DelegateEntryIds2,
			InternalSchema.DelegateFlags
		};

		// Token: 0x0400004C RID: 76
		private static readonly PropertyDefinition[] DeferredActionMessagesDefinitions = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.OriginalMessageSvrEId
		};

		// Token: 0x0400004D RID: 77
		private static readonly QueryFilter FreeBusyQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, "LocalFreebusy");

		// Token: 0x0400004E RID: 78
		private static readonly StorePropertyDefinition[] ItemSchemaIdStoreDefinition = new StorePropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x0400004F RID: 79
		public static ReadOnlyCollection<DefaultFolderType> AllDefaultFolders = new ReadOnlyCollection<DefaultFolderType>(MailboxSession.allDefaultFolders);

		// Token: 0x04000050 RID: 80
		private static CultureInfo productDefaultCulture = new CultureInfo("en-US");

		// Token: 0x04000051 RID: 81
		private IADOrgPerson delegateUser;

		// Token: 0x04000052 RID: 82
		private bool canDispose;

		// Token: 0x04000053 RID: 83
		private MailboxSession masterMailboxSession;

		// Token: 0x04000054 RID: 84
		private DelegateSessionManager delegateSessionManager;

		// Token: 0x04000055 RID: 85
		private IExchangePrincipal mailboxOwner;

		// Token: 0x04000056 RID: 86
		private ExTimeZone exTimeZone = ExTimeZone.UtcTimeZone;

		// Token: 0x04000057 RID: 87
		private MasterCategoryList masterCategoryList;

		// Token: 0x04000058 RID: 88
		private PropertyDefinition[] mailboxProperties;

		// Token: 0x04000059 RID: 89
		private static PropertyDefinition[] mailboxItemCountsAndSizesProperties = new PropertyDefinition[]
		{
			InternalSchema.ItemCount,
			MailboxSchema.QuotaUsedExtended,
			InternalSchema.DeletedMsgCount,
			MailboxSchema.DumpsterQuotaUsedExtended
		};

		// Token: 0x0400005A RID: 90
		private IList<DefaultFolderType> foldersToInit;

		// Token: 0x0400005B RID: 91
		private bool useNamedProperties;

		// Token: 0x0400005C RID: 92
		private bool filterPrivateItems;

		// Token: 0x0400005D RID: 93
		private bool disableFilterPrivateItems;

		// Token: 0x0400005E RID: 94
		private UserConfigurationManager userConfigurationManager;

		// Token: 0x0400005F RID: 95
		private DefaultFolderManager defaultFolderManager;

		// Token: 0x04000060 RID: 96
		private bool isDefaultFolderManagerBeingInitialized;

		// Token: 0x04000061 RID: 97
		private CultureInfo preferedCultureInfoCache;

		// Token: 0x04000062 RID: 98
		private COWSession copyOnWriteNotification;

		// Token: 0x04000063 RID: 99
		private bool bypassAuditsFolderAccessChecking;

		// Token: 0x04000064 RID: 100
		private bool bypassAuditing;

		// Token: 0x04000065 RID: 101
		private static readonly object lockRegisterCertificateErrorHandler = new object();

		// Token: 0x04000066 RID: 102
		private IActivitySession activitySession;

		// Token: 0x04000067 RID: 103
		private ContactFolders contactFolders;

		// Token: 0x04000068 RID: 104
		private MailboxSessionSharableDataManager sharedDataManager;

		// Token: 0x04000069 RID: 105
		internal Hookable<IActivitySession> activitySessionHook;

		// Token: 0x0400006A RID: 106
		public static ReadOnlyCollection<DefaultFolderType> FreeDefaultFolders = new ReadOnlyCollection<DefaultFolderType>(new DefaultFolderType[]
		{
			DefaultFolderType.Configuration,
			DefaultFolderType.Root,
			DefaultFolderType.Inbox,
			DefaultFolderType.LegacySpoolerQueue
		});

		// Token: 0x0400006B RID: 107
		public static ReadOnlyCollection<DefaultFolderType> AlwaysInitDefaultFolders = new ReadOnlyCollection<DefaultFolderType>(new DefaultFolderType[]
		{
			DefaultFolderType.Root,
			DefaultFolderType.Configuration,
			DefaultFolderType.Inbox,
			DefaultFolderType.Outbox,
			DefaultFolderType.SentItems,
			DefaultFolderType.DeletedItems,
			DefaultFolderType.CommonViews,
			DefaultFolderType.SearchFolders,
			DefaultFolderType.DeferredActionFolder,
			DefaultFolderType.LegacySchedule,
			DefaultFolderType.LegacyShortcuts,
			DefaultFolderType.LegacyViews,
			DefaultFolderType.System,
			DefaultFolderType.AdminAuditLogs,
			DefaultFolderType.Audits,
			DefaultFolderType.Clutter
		});

		// Token: 0x0400006C RID: 108
		private static ReadOnlyCollection<DefaultFolderType> DefaultFoldersToLocalizeOnFirstLogon = new ReadOnlyCollection<DefaultFolderType>(new DefaultFolderType[]
		{
			DefaultFolderType.Root,
			DefaultFolderType.Inbox,
			DefaultFolderType.Outbox,
			DefaultFolderType.SentItems,
			DefaultFolderType.DeletedItems,
			DefaultFolderType.Calendar,
			DefaultFolderType.Contacts,
			DefaultFolderType.Drafts,
			DefaultFolderType.Journal,
			DefaultFolderType.Notes,
			DefaultFolderType.Tasks
		});

		// Token: 0x0400006D RID: 109
		private bool isUnified;

		// Token: 0x0400006E RID: 110
		private bool? isAuditConfigFromUCCPolicyEnabled;

		// Token: 0x0400006F RID: 111
		private static MailboxSession.CheckSubmissionQuotaDelegate checkSubmissionQuotaDelegate = (string mailboxServer, int mailboxServerVersion, Guid mailboxGuid, int recipientCount, ADObjectId throttlingPolicyId, OrganizationId organizationId, string clientInfo) => MailboxThrottle.Instance.ObtainUserSubmissionTokens(mailboxServer, mailboxServerVersion, mailboxGuid, recipientCount, throttlingPolicyId, organizationId, clientInfo);

		// Token: 0x04000070 RID: 112
		private static bool registeredCertificateErrorHandler = false;

		// Token: 0x04000071 RID: 113
		private Rules inboxRules;

		// Token: 0x04000072 RID: 114
		private Rules allInboxRules;

		// Token: 0x04000073 RID: 115
		private Dictionary<RetentionActionType, PolicyTagList> policyTagListDictionary;

		// Token: 0x04000074 RID: 116
		private bool nonInteractiveSession;

		// Token: 0x04000075 RID: 117
		private SiteMailboxSynchronizerReference siteMailboxSynchronizerReference;

		// Token: 0x04000076 RID: 118
		private IAuditLog auditLog;

		// Token: 0x0200000D RID: 13
		[Flags]
		public enum InitializationFlags
		{
			// Token: 0x0400007E RID: 126
			None = 0,
			// Token: 0x0400007F RID: 127
			DefaultFolders = 1,
			// Token: 0x04000080 RID: 128
			UserConfigurationManager = 2,
			// Token: 0x04000081 RID: 129
			CopyOnWrite = 4,
			// Token: 0x04000082 RID: 130
			DeadSessionChecking = 8,
			// Token: 0x04000083 RID: 131
			CheckPrivateItemsAccess = 16,
			// Token: 0x04000084 RID: 132
			RequestLocalRpc = 32,
			// Token: 0x04000085 RID: 133
			OverrideHomeMdb = 64,
			// Token: 0x04000086 RID: 134
			QuotaMessageDelivery = 128,
			// Token: 0x04000087 RID: 135
			NormalMessageDelivery = 256,
			// Token: 0x04000088 RID: 136
			SpecialMessageDelivery = 512,
			// Token: 0x04000089 RID: 137
			SuppressFolderIdPrefetch = 1024,
			// Token: 0x0400008A RID: 138
			UseNamedProperties = 2048,
			// Token: 0x0400008B RID: 139
			DeferDefaultFolderIdInitialization = 4096,
			// Token: 0x0400008C RID: 140
			UseRecoveryDatabase = 8192,
			// Token: 0x0400008D RID: 141
			NonInteractiveSession = 16384,
			// Token: 0x0400008E RID: 142
			DisconnectedMailbox = 32768,
			// Token: 0x0400008F RID: 143
			XForestMove = 65536,
			// Token: 0x04000090 RID: 144
			MoveUser = 131072,
			// Token: 0x04000091 RID: 145
			IgnoreForcedFolderInit = 262144,
			// Token: 0x04000092 RID: 146
			AllowAdminLocalization = 524288,
			// Token: 0x04000093 RID: 147
			ReadOnly = 1048576,
			// Token: 0x04000094 RID: 148
			OlcSync = 2097152
		}

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x06000220 RID: 544
		private delegate void InitializeMailboxSessionFailure();

		// Token: 0x0200000F RID: 15
		public struct MailboxItemCountsAndSizes
		{
			// Token: 0x04000095 RID: 149
			public int? ItemCount;

			// Token: 0x04000096 RID: 150
			public long? TotalItemSize;

			// Token: 0x04000097 RID: 151
			public int? DeletedItemCount;

			// Token: 0x04000098 RID: 152
			public long? TotalDeletedItemSize;
		}

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x06000224 RID: 548
		internal delegate bool CheckSubmissionQuotaDelegate(string mailboxServer, int mailboxServerVersion, Guid mailboxGuid, int recipientCount, ADObjectId throttlingPolicyId, OrganizationId organizationId, string clientInfo);
	}
}
