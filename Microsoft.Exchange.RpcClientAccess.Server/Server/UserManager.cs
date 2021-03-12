using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000048 RID: 72
	internal sealed class UserManager : BaseObject
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000D444 File Offset: 0x0000B644
		internal UserManager() : this(new DatabaseLocationProvider(), new DatabaseInfoCache(DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 82, ".ctor", "f:\\15.00.1497\\sources\\dev\\mapimt\\src\\Server\\UserManager.cs"), ServiceConfiguration.Schema.ADUserDataCacheTimeout.DefaultValue))
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D47C File Offset: 0x0000B67C
		internal UserManager(IDatabaseLocationProvider databaseLocationProvider, LazyLookupTimeoutCache<Guid, DatabaseInfo> databaseInfoCache)
		{
			this.databaseLocationProvider = databaseLocationProvider;
			this.databaseInfoCache = databaseInfoCache;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D4AF File Offset: 0x0000B6AF
		private IDatabaseLocationProvider DatabaseLocationProvider
		{
			get
			{
				return this.databaseLocationProvider;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public ExchangePrincipal FindExchangePrincipal(string legacyDN, string userDomain, out MiniRecipient miniRecipient)
		{
			ExchangePrincipal result = null;
			StorageMiniRecipient storageMiniRecipient = null;
			try
			{
				if (!string.IsNullOrEmpty(userDomain))
				{
					result = ExchangePrincipal.FromLegacyDNByMiniRecipient(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(userDomain), legacyDN, RemotingOptions.AllowCrossSite, UserManager.miniRecipientProperties, out storageMiniRecipient);
				}
				else
				{
					result = ExchangePrincipal.FromLegacyDNByMiniRecipient(ADSessionSettings.FromRootOrgScopeSet(), legacyDN, RemotingOptions.AllowCrossSite, UserManager.miniRecipientProperties, out storageMiniRecipient);
				}
			}
			finally
			{
				miniRecipient = storageMiniRecipient;
			}
			return result;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D514 File Offset: 0x0000B714
		public ExchangePrincipal FindExchangePrincipal(string legacyDN, string userDomain)
		{
			MiniRecipient miniRecipient;
			return this.FindExchangePrincipal(legacyDN, userDomain, out miniRecipient);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D52C File Offset: 0x0000B72C
		public int GetActiveUserCount()
		{
			List<UserManager.User> list = null;
			int num = 0;
			try
			{
				this.userListLock.EnterReadLock();
				foreach (UserManager.User user in this.userList.Values)
				{
					if (user.LastAccessTimestamp == this.lastRetrievalTimestamp)
					{
						num++;
					}
					if (user.CanRemove)
					{
						if (list == null)
						{
							list = new List<UserManager.User>();
						}
						list.Add(user);
					}
				}
				this.lastRetrievalTimestamp++;
			}
			finally
			{
				try
				{
					this.userListLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (list != null && list.Count > 0)
			{
				try
				{
					this.userListLock.EnterWriteLock();
					foreach (UserManager.User user2 in list)
					{
						if (user2.CanRemove)
						{
							this.userList.Remove(user2);
						}
					}
				}
				finally
				{
					try
					{
						this.userListLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			return num;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D680 File Offset: 0x0000B880
		public IUser Get(SecurityIdentifier authenticatedUserSid, string actAsLegacyDN, string userDomain)
		{
			UserManager.User user = new UserManager.User(this, authenticatedUserSid, actAsLegacyDN, userDomain);
			bool flag = false;
			UserManager.User user2;
			if (!this.TryGetExistingUser(user, out user2, out flag))
			{
				try
				{
					this.userListLock.EnterUpgradeableReadLock();
					if (!this.TryGetExistingUser(user, out user2, out flag))
					{
						try
						{
							this.userListLock.EnterWriteLock();
							this.userList.Add(user, user);
							user.AddReference();
							flag = true;
						}
						finally
						{
							try
							{
								this.userListLock.ExitWriteLock();
							}
							catch (SynchronizationLockException)
							{
							}
						}
					}
				}
				finally
				{
					try
					{
						this.userListLock.ExitUpgradeableReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			if (user2 != null)
			{
				user = user2;
			}
			bool flag2 = false;
			IUser result;
			try
			{
				if (flag)
				{
					RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.UserCount.Increment();
				}
				user.UpdatePrincipalCacheWrapped(true);
				flag2 = true;
				result = user;
			}
			finally
			{
				if (!flag2)
				{
					user.Release();
				}
			}
			return result;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D77C File Offset: 0x0000B97C
		internal bool CheckAccess(ClientSecurityContext clientSecurityContext, MiniRecipient accessingMiniRecipient)
		{
			bool result;
			try
			{
				if (accessingMiniRecipient.ExchangeSecurityDescriptor != null && accessingMiniRecipient.Database != null)
				{
					RawSecurityDescriptor rawSecurityDescriptor = this.GetDatabaseInfo(accessingMiniRecipient.Database.ObjectGuid).InheritMailboxSecurityDescriptor(accessingMiniRecipient.ExchangeSecurityDescriptor);
					int grantedAccess = clientSecurityContext.GetGrantedAccess(rawSecurityDescriptor, accessingMiniRecipient.Sid, AccessMask.CreateChild);
					if ((long)grantedAccess == 1L)
					{
						ExTraceGlobals.AccessControlTracer.TraceInformation<string, ClientSecurityContext>(0, Activity.TraceId, "Granted: mailbox security descriptor for mailbox {0} grants owner access to {1}", accessingMiniRecipient.LegacyExchangeDN, clientSecurityContext);
						result = true;
					}
					else
					{
						if (ExTraceGlobals.AccessControlTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.AccessControlTracer.TraceError<string, ClientSecurityContext, int>(0, Activity.TraceId, "Denied: mailbox security descriptor for mailbox {0} denies owner access to {1}. Maximum allowed access: {2:X8}", accessingMiniRecipient.LegacyExchangeDN, clientSecurityContext, clientSecurityContext.GetGrantedAccess(rawSecurityDescriptor, accessingMiniRecipient.Sid, AccessMask.MaximumAllowed));
						}
						result = false;
					}
				}
				else
				{
					ExTraceGlobals.AccessControlTracer.TraceError<RecipientType, string>(0, Activity.TraceId, "Denied: mailbox security descriptor for {0} {1} is not set", accessingMiniRecipient.RecipientType, accessingMiniRecipient.LegacyExchangeDN);
					result = false;
				}
			}
			catch (Win32Exception arg)
			{
				ExTraceGlobals.AccessControlTracer.TraceError<ClientSecurityContext, Win32Exception>(0, Activity.TraceId, "Denied: access check failed on ClientSecurityContext {0} with {1}.", clientSecurityContext, arg);
				result = false;
			}
			return result;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D888 File Offset: 0x0000BA88
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UserManager>(this);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D890 File Offset: 0x0000BA90
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.databaseInfoCache);
			base.InternalDispose();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		private DatabaseInfo GetDatabaseInfo(Guid databaseGuid)
		{
			return this.databaseInfoCache.Get(databaseGuid);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D8B1 File Offset: 0x0000BAB1
		private void OnUserReferenceReleased(int referencesLeft)
		{
			if (referencesLeft <= 0)
			{
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.UserCount.Decrement();
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		private bool TryGetExistingUser(UserManager.User user, out UserManager.User existingUser, out bool isUserActivated)
		{
			existingUser = null;
			isUserActivated = false;
			bool result;
			try
			{
				this.userListLock.EnterReadLock();
				if (this.userList.TryGetValue(user, out existingUser))
				{
					int num = existingUser.AddReference();
					isUserActivated = (num == 1);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				try
				{
					this.userListLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x04000147 RID: 327
		private static readonly PropertyDefinition[] miniRecipientProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.MAPIBlockOutlookVersions,
			ADRecipientSchema.MAPIBlockOutlookRpcHttp,
			ADRecipientSchema.MAPIEnabled,
			ADUserSchema.UserAccountControl
		}.Union(ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>().AllProperties).ToArray<PropertyDefinition>();

		// Token: 0x04000148 RID: 328
		private readonly Dictionary<UserManager.User, UserManager.User> userList = new Dictionary<UserManager.User, UserManager.User>();

		// Token: 0x04000149 RID: 329
		private readonly ReaderWriterLockSlim userListLock = new ReaderWriterLockSlim();

		// Token: 0x0400014A RID: 330
		private readonly LazyLookupTimeoutCache<Guid, DatabaseInfo> databaseInfoCache;

		// Token: 0x0400014B RID: 331
		private readonly IDatabaseLocationProvider databaseLocationProvider;

		// Token: 0x0400014C RID: 332
		private int lastRetrievalTimestamp = 1;

		// Token: 0x02000049 RID: 73
		internal sealed class User : IUser, WatsonHelper.IProvideWatsonReportData, IEquatable<UserManager.User>
		{
			// Token: 0x06000277 RID: 631 RVA: 0x0000D98C File Offset: 0x0000BB8C
			internal User(UserManager userManager, SecurityIdentifier authenticatedUserSid, string authenticatedUserLegacyDN, string domain)
			{
				this.userManager = userManager;
				this.AuthenticatedUserSid = authenticatedUserSid;
				this.userDn = authenticatedUserLegacyDN;
				this.domain = domain;
				if (this.domain == null)
				{
					this.domain = string.Empty;
				}
				this.exchangeOrganizationInfo = ExOrgInfoFlags.UseAutoDiscoverForPublicFolderConfiguration;
				this.blockedOutlookVersions = new MapiVersionRanges(null);
				this.mapiBlockOutlookRpcHttp = false;
				this.mapiEnabled = true;
				this.mapiCachedModeRequired = false;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000278 RID: 632 RVA: 0x0000DA23 File Offset: 0x0000BC23
			public string LegacyDistinguishedName
			{
				get
				{
					return this.userDn;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000279 RID: 633 RVA: 0x0000DA2B File Offset: 0x0000BC2B
			public string Domain
			{
				get
				{
					return this.domain;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x0600027A RID: 634 RVA: 0x0000DA33 File Offset: 0x0000BC33
			public string DisplayName
			{
				get
				{
					if (this.accessingPrincipal == null)
					{
						return string.Empty;
					}
					return this.accessingPrincipal.MailboxInfo.DisplayName;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600027B RID: 635 RVA: 0x0000DA53 File Offset: 0x0000BC53
			public OrganizationId OrganizationId
			{
				get
				{
					if (this.accessingPrincipal != null)
					{
						return this.accessingPrincipal.MailboxInfo.OrganizationId;
					}
					if (this.miniRecipient != null)
					{
						return this.miniRecipient.OrganizationId;
					}
					return null;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600027C RID: 636 RVA: 0x0000DA83 File Offset: 0x0000BC83
			public SecurityIdentifier MasterAccountSid
			{
				get
				{
					if (this.IsConnectAsValidDisabledUser())
					{
						return this.miniRecipient.MasterAccountSid;
					}
					return null;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600027D RID: 637 RVA: 0x0000DA9A File Offset: 0x0000BC9A
			public SecurityIdentifier ConnectAsSid
			{
				get
				{
					if (this.accessingPrincipal != null)
					{
						return this.accessingPrincipal.Sid;
					}
					if (this.miniRecipient != null)
					{
						return this.miniRecipient.Sid;
					}
					return null;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600027E RID: 638 RVA: 0x0000DAC5 File Offset: 0x0000BCC5
			public ExOrgInfoFlags ExchangeOrganizationInfo
			{
				get
				{
					return this.exchangeOrganizationInfo;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000DACD File Offset: 0x0000BCCD
			public MapiVersionRanges MapiBlockOutlookVersions
			{
				get
				{
					return this.blockedOutlookVersions;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000280 RID: 640 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
			public bool MapiBlockOutlookRpcHttp
			{
				get
				{
					return this.mapiBlockOutlookRpcHttp;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000281 RID: 641 RVA: 0x0000DADD File Offset: 0x0000BCDD
			public bool MapiEnabled
			{
				get
				{
					return this.mapiEnabled;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000282 RID: 642 RVA: 0x0000DAE5 File Offset: 0x0000BCE5
			public bool MapiCachedModeRequired
			{
				get
				{
					return this.mapiCachedModeRequired;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000283 RID: 643 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
			public MiniRecipient MiniRecipient
			{
				get
				{
					if (this.miniRecipient == null)
					{
						try
						{
							this.UpdatePrincipalCacheIfNeeded();
						}
						catch (UserHasNoMailboxException)
						{
						}
					}
					return this.miniRecipient;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000284 RID: 644 RVA: 0x0000DB28 File Offset: 0x0000BD28
			public bool IsFederatedSystemAttendant
			{
				get
				{
					return LegacyDnHelper.IsFederatedSystemAttendant(this.userDn);
				}
			}

			// Token: 0x06000285 RID: 645 RVA: 0x0000DB50 File Offset: 0x0000BD50
			private bool CheckSecurityContext(IExchangePrincipal mailboxExchangePrincipal, ClientSecurityContext securityContext)
			{
				ArgumentValidator.ThrowIfNull("securityContext", securityContext);
				bool flag = (mailboxExchangePrincipal.Sid != null && mailboxExchangePrincipal.Sid.Equals(securityContext.UserSid)) || (mailboxExchangePrincipal.MasterAccountSid != null && mailboxExchangePrincipal.MasterAccountSid.Equals(securityContext.UserSid));
				return flag || (mailboxExchangePrincipal.SidHistory != null && mailboxExchangePrincipal.SidHistory.Any((SecurityIdentifier sid) => sid.Equals(securityContext.UserSid)));
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000286 RID: 646 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
			// (set) Token: 0x06000287 RID: 647 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
			internal int LastAccessTimestamp { get; private set; }

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000288 RID: 648 RVA: 0x0000DC02 File Offset: 0x0000BE02
			internal bool CanRemove
			{
				get
				{
					return this.referenceCount == 0 && !this.HasImportantInformation;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000289 RID: 649 RVA: 0x0000DC18 File Offset: 0x0000BE18
			private ExDateTime BackoffConnectUntil
			{
				get
				{
					ExDateTime result;
					lock (this.budgetLock)
					{
						result = this.backoffConnectUntil;
					}
					return result;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600028A RID: 650 RVA: 0x0000DC5C File Offset: 0x0000BE5C
			// (set) Token: 0x0600028B RID: 651 RVA: 0x0000DC64 File Offset: 0x0000BE64
			private SecurityIdentifier AuthenticatedUserSid { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x0600028C RID: 652 RVA: 0x0000DC6D File Offset: 0x0000BE6D
			private bool HasImportantInformation
			{
				get
				{
					return this.BackoffConnectUntil > ExDateTime.UtcNow;
				}
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000DC7F File Offset: 0x0000BE7F
			public override bool Equals(object obj)
			{
				return this.Equals(obj as UserManager.User);
			}

			// Token: 0x0600028E RID: 654 RVA: 0x0000DC8D File Offset: 0x0000BE8D
			public override int GetHashCode()
			{
				return this.AuthenticatedUserSid.GetHashCode() ^ this.userDn.GetHashCode() ^ this.domain.GetHashCode();
			}

			// Token: 0x0600028F RID: 655 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
			public bool Equals(UserManager.User other)
			{
				return other != null && this.AuthenticatedUserSid.Equals(other.AuthenticatedUserSid) && LegacyDN.StringComparer.Equals(this.userDn, other.userDn) && string.Equals(this.domain, other.domain, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000290 RID: 656 RVA: 0x0000DD04 File Offset: 0x0000BF04
			public void CheckCanConnect()
			{
				if (ExDateTime.UtcNow >= this.BackoffConnectUntil)
				{
					this.attemptsLeftBeforeBackoffTakesEffect = 10;
					this.isRepeatingBackoff = false;
					return;
				}
				if (this.attemptsLeftBeforeBackoffTakesEffect <= 0)
				{
					ExTraceGlobals.ClientThrottledTracer.TraceWarning<string>(Activity.TraceId, "Connect for user '{0}' has been backed off", this.LegacyDistinguishedName);
					ClientBackoffException ex = new ClientBackoffException("Client connect is backed off", this.backoffConnectReason);
					ex.IsRepeatingBackoff = this.isRepeatingBackoff;
					this.isRepeatingBackoff = true;
					throw ex;
				}
				ExTraceGlobals.ClientThrottledTracer.TraceDebug<string, int>(Activity.TraceId, "Connect for user '{0}' is going to be backed off after {1} more attempts.", this.LegacyDistinguishedName, this.attemptsLeftBeforeBackoffTakesEffect);
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000DD9C File Offset: 0x0000BF9C
			public void BackoffConnect(Exception reason)
			{
				this.BackoffConnect(UserManager.User.defaultBackoffPeriod, reason);
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000DDAC File Offset: 0x0000BFAC
			public void InvalidatePrincipalCache()
			{
				lock (this.accessingPrincipalUpdateLock)
				{
					this.accessingPrincipalExpirationTime = ExDateTime.MinValue;
				}
				ExTraceGlobals.ConnectRpcTracer.TraceDebug<string>(0, (long)this.GetHashCode(), "Invalidated cached ExchangePrincipal for '{0}'", this.userDn);
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000DE10 File Offset: 0x0000C010
			public void RegisterActivity()
			{
				this.LastAccessTimestamp = this.userManager.lastRetrievalTimestamp;
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000DE23 File Offset: 0x0000C023
			public void Release()
			{
				this.userManager.OnUserReferenceReleased(this.RemoveReference());
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000DE38 File Offset: 0x0000C038
			public void UpdatePrincipalCacheWrapped(bool ignoreCrossForestMailboxErrors)
			{
				try
				{
					this.UpdatePrincipalCacheIfNeeded();
				}
				catch (UserHasNoMailboxException)
				{
					if (!ignoreCrossForestMailboxErrors)
					{
						throw;
					}
					ExTraceGlobals.ConnectRpcTracer.TraceInformation<string>(0, Activity.TraceId, "Failed to retrieve ExchangePrincipal for '{0}': user has no mailbox in this forest", this.LegacyDistinguishedName);
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new UnknownUserException(string.Format("Unable to map userDn '{0}' to exchangePrincipal", this.LegacyDistinguishedName), innerException);
				}
				catch (StoragePermanentException innerException2)
				{
					throw new LoginFailureException("Unable to access AD", innerException2);
				}
				catch (StorageTransientException innerException3)
				{
					throw new LoginFailureException("Unable to access AD", innerException3);
				}
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000DED8 File Offset: 0x0000C0D8
			private void UpdatePrincipalCacheIfNeeded()
			{
				if (LegacyDnHelper.IsFederatedSystemAttendant(this.userDn))
				{
					return;
				}
				lock (this.accessingPrincipalUpdateLock)
				{
					if (this.accessingPrincipalExpirationTime < ExDateTime.UtcNow)
					{
						this.accessingPrincipalExpirationTime = ExDateTime.UtcNow + Configuration.ServiceConfiguration.ADUserDataCacheTimeout;
						this.accessingPrincipalHasNoMailbox = false;
						MiniRecipient miniRecipient = null;
						try
						{
							this.accessingPrincipal = this.userManager.FindExchangePrincipal(this.userDn, this.domain, out miniRecipient);
							this.miniRecipient = miniRecipient;
						}
						catch (UserHasNoMailboxException)
						{
							this.miniRecipient = miniRecipient;
							this.accessingPrincipalHasNoMailbox = true;
							throw;
						}
						try
						{
							this.blockedOutlookVersions = new MapiVersionRanges((string)this.miniRecipient[ADRecipientSchema.MAPIBlockOutlookVersions]);
						}
						catch (FormatException innerException)
						{
							throw new ClientVersionException(string.Format("Version specification should have 3 parts, MAPIBlockOutlookVersions:{0}", this.miniRecipient[ADRecipientSchema.MAPIBlockOutlookVersions]), innerException);
						}
						catch (ArgumentOutOfRangeException innerException2)
						{
							throw new ClientVersionException(string.Format("Version number part should be between 0 and 65535, MAPIBlockOutlookVersions:{0}", this.miniRecipient[ADRecipientSchema.MAPIBlockOutlookVersions]), innerException2);
						}
						this.mapiBlockOutlookRpcHttp = (bool)this.miniRecipient[ADRecipientSchema.MAPIBlockOutlookRpcHttp];
						this.mapiEnabled = (bool)this.miniRecipient[ADRecipientSchema.MAPIEnabled];
						this.mapiCachedModeRequired = (bool)this.miniRecipient[ADRecipientSchema.MAPIBlockOutlookNonCachedMode];
						this.exchangeOrganizationInfo = this.GetExOrgInfoFlags();
						if (ExTraceGlobals.ConnectRpcTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.ConnectRpcTracer.TraceInformation<string, string>(0, Activity.TraceId, "Updated cached ExchangePrincipal for '{0}'. Server = '{1}', Mdb = '{2}'", this.userDn, (this.accessingPrincipal.MailboxInfo.Location != MailboxDatabaseLocation.Unknown) ? this.accessingPrincipal.MailboxInfo.Location.ToString() : "Database location info not available");
						}
					}
					else if (this.accessingPrincipal == null)
					{
						if (this.accessingPrincipalHasNoMailbox)
						{
							throw new UserHasNoMailboxException();
						}
						throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
					}
				}
			}

			// Token: 0x06000297 RID: 663 RVA: 0x0000E128 File Offset: 0x0000C328
			public ExchangePrincipal GetExchangePrincipal(string legacyDN)
			{
				if (LegacyDN.StringComparer.Equals(this.LegacyDistinguishedName, legacyDN))
				{
					this.UpdatePrincipalCacheWrapped(false);
					return this.accessingPrincipal;
				}
				return this.userManager.FindExchangePrincipal(legacyDN, this.domain);
			}

			// Token: 0x06000298 RID: 664 RVA: 0x0000E160 File Offset: 0x0000C360
			public void CorrelateIdentityWithLegacyDN(ClientSecurityContext clientSecurityContext)
			{
				Exception ex;
				if (this.accessingPrincipal == null)
				{
					if (this.miniRecipient.MasterAccountSid != null && !this.miniRecipient.MasterAccountSid.Equals(UserManager.User.useObjectSid))
					{
						SecurityIdentifier masterAccountSid = this.miniRecipient.MasterAccountSid;
						if (masterAccountSid.Equals(clientSecurityContext.UserSid))
						{
							return;
						}
						foreach (IdentityReference identityReference in clientSecurityContext.GetGroups())
						{
							if (identityReference.Equals(masterAccountSid))
							{
								return;
							}
						}
						ex = new LoginPermException(string.Format("MasterAccountSid of the {0} \"{1}\" {2} doesn't match SID {3} that a client has authenticated with.", new object[]
						{
							this.miniRecipient.RecipientType,
							this.userDn,
							this.miniRecipient.MasterAccountSid,
							clientSecurityContext
						}));
						this.BackoffConnect(ex);
						throw ex;
					}
					else if ((this.miniRecipient.RecipientType != RecipientType.User && this.miniRecipient.RecipientType != RecipientType.MailUser) || !this.miniRecipient.Sid.Equals(clientSecurityContext.UserSid))
					{
						goto IL_13C;
					}
					return;
				}
				if (this.CheckSecurityContext(this.accessingPrincipal, clientSecurityContext))
				{
					return;
				}
				if (this.userManager.CheckAccess(clientSecurityContext, this.miniRecipient))
				{
					return;
				}
				IL_13C:
				ex = new LoginPermException(string.Format("'{0}' can't act as owner of a {1} object '{2}' with SID {3} and MasterAccountSid {4}", new object[]
				{
					clientSecurityContext,
					this.miniRecipient.RecipientType,
					this.userDn,
					this.miniRecipient.Sid,
					this.miniRecipient.MasterAccountSid
				}));
				this.BackoffConnect(ex);
				throw ex;
			}

			// Token: 0x06000299 RID: 665 RVA: 0x0000E31C File Offset: 0x0000C51C
			string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
			{
				return string.Empty;
			}

			// Token: 0x0600029A RID: 666 RVA: 0x0000E323 File Offset: 0x0000C523
			public int AddReference()
			{
				return Interlocked.Increment(ref this.referenceCount);
			}

			// Token: 0x0600029B RID: 667 RVA: 0x0000E330 File Offset: 0x0000C530
			private void BackoffConnect(TimeSpan duration, Exception reason)
			{
				if ((this.miniRecipient.RecipientTypeDetails & RecipientTypeDetails.MonitoringMailbox) == RecipientTypeDetails.MonitoringMailbox)
				{
					return;
				}
				ExDateTime exDateTime = ExDateTime.UtcNow.Add(duration);
				int num = 0;
				lock (this.budgetLock)
				{
					if (this.backoffConnectUntil < exDateTime)
					{
						this.backoffConnectUntil = exDateTime;
					}
					num = Interlocked.Decrement(ref this.attemptsLeftBeforeBackoffTakesEffect);
					if (num < 0)
					{
						this.attemptsLeftBeforeBackoffTakesEffect = 0;
					}
					exDateTime = this.backoffConnectUntil;
					this.backoffConnectReason = reason;
				}
				ExTraceGlobals.ClientThrottledTracer.TraceWarning<string, ExDateTime, int>(Activity.TraceId, "Throttling connect requests for '{0}' until {1} after {2} more attempts", this.userDn, exDateTime, num);
			}

			// Token: 0x0600029C RID: 668 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
			private int RemoveReference()
			{
				return Interlocked.Decrement(ref this.referenceCount);
			}

			// Token: 0x0600029D RID: 669 RVA: 0x0000E410 File Offset: 0x0000C610
			private ExOrgInfoFlags GetExOrgInfoFlags()
			{
				Guid arg;
				ExOrgInfoFlags exOrgInfoFlags;
				if (PublicFolderSession.TryGetHierarchyMailboxGuidForUser(this.accessingPrincipal.MailboxInfo.OrganizationId, this.accessingPrincipal.MailboxInfo.MailboxGuid, this.accessingPrincipal.DefaultPublicFolderMailbox, out arg))
				{
					exOrgInfoFlags = (ExOrgInfoFlags.PublicFoldersEnabled | ExOrgInfoFlags.UseAutoDiscoverForPublicFolderConfiguration);
					ExTraceGlobals.ConnectRpcTracer.TraceInformation<Guid, ExOrgInfoFlags>(0, Activity.TraceId, "We found a local public folder mailbox {0}. ExOrgInfoFlags returned:{1}", arg, exOrgInfoFlags);
				}
				else if (this.accessingPrincipal.MailboxInfo.Location.HomePublicFolderDatabaseGuid != Guid.Empty && this.userManager.DatabaseLocationProvider.GetLocationInfo(this.accessingPrincipal.MailboxInfo.Location.HomePublicFolderDatabaseGuid, false, true) != null)
				{
					PublicFoldersDeployment publicFoldersDeploymentType = PublicFolderSession.GetPublicFoldersDeploymentType(this.accessingPrincipal.MailboxInfo.OrganizationId);
					if (publicFoldersDeploymentType == PublicFoldersDeployment.Remote)
					{
						exOrgInfoFlags = (ExOrgInfoFlags.PublicFoldersEnabled | ExOrgInfoFlags.UseAutoDiscoverForPublicFolderConfiguration);
					}
					else
					{
						exOrgInfoFlags = ExOrgInfoFlags.PublicFoldersEnabled;
					}
					ExTraceGlobals.ConnectRpcTracer.TraceInformation<Guid, PublicFoldersDeployment, ExOrgInfoFlags>(0, Activity.TraceId, "Coexistence scenario. HomePublicFolderDatabaseGuid: {0}. PublicFoldersDeploymentType: {1}. ExOrgInfoFlags returned:{2}", this.accessingPrincipal.MailboxInfo.Location.HomePublicFolderDatabaseGuid, publicFoldersDeploymentType, exOrgInfoFlags);
				}
				else
				{
					exOrgInfoFlags = ExOrgInfoFlags.UseAutoDiscoverForPublicFolderConfiguration;
					ExTraceGlobals.ConnectRpcTracer.TraceInformation<ExOrgInfoFlags>(0, Activity.TraceId, "No public folders are provisioned. ExOrgInfoFlags returned:{0}", exOrgInfoFlags);
				}
				return exOrgInfoFlags;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000E524 File Offset: 0x0000C724
			private bool IsConnectAsValidDisabledUser()
			{
				return this.miniRecipient != null && (this.miniRecipient.RecipientType == RecipientType.User || this.miniRecipient.RecipientType == RecipientType.MailUser || this.miniRecipient.RecipientType == RecipientType.UserMailbox) && ((UserAccountControlFlags)this.miniRecipient[ADUserSchema.UserAccountControl] & UserAccountControlFlags.AccountDisabled) != UserAccountControlFlags.None;
			}

			// Token: 0x0400014D RID: 333
			private const int DefaultAttemptsBeforeBackoff = 10;

			// Token: 0x0400014E RID: 334
			private static readonly SecurityIdentifier useObjectSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);

			// Token: 0x0400014F RID: 335
			private static readonly TimeSpan defaultBackoffPeriod = TimeSpan.FromSeconds(10.0);

			// Token: 0x04000150 RID: 336
			private readonly UserManager userManager;

			// Token: 0x04000151 RID: 337
			private readonly object budgetLock = new object();

			// Token: 0x04000152 RID: 338
			private readonly object accessingPrincipalUpdateLock = new object();

			// Token: 0x04000153 RID: 339
			private readonly string userDn;

			// Token: 0x04000154 RID: 340
			private readonly string domain;

			// Token: 0x04000155 RID: 341
			private int referenceCount;

			// Token: 0x04000156 RID: 342
			private ExDateTime backoffConnectUntil = ExDateTime.MinValue;

			// Token: 0x04000157 RID: 343
			private Exception backoffConnectReason;

			// Token: 0x04000158 RID: 344
			private int attemptsLeftBeforeBackoffTakesEffect;

			// Token: 0x04000159 RID: 345
			private bool isRepeatingBackoff;

			// Token: 0x0400015A RID: 346
			private ExchangePrincipal accessingPrincipal;

			// Token: 0x0400015B RID: 347
			private MiniRecipient miniRecipient;

			// Token: 0x0400015C RID: 348
			private bool accessingPrincipalHasNoMailbox;

			// Token: 0x0400015D RID: 349
			private ExDateTime accessingPrincipalExpirationTime = ExDateTime.MinValue;

			// Token: 0x0400015E RID: 350
			private MapiVersionRanges blockedOutlookVersions;

			// Token: 0x0400015F RID: 351
			private bool mapiBlockOutlookRpcHttp;

			// Token: 0x04000160 RID: 352
			private bool mapiEnabled;

			// Token: 0x04000161 RID: 353
			private bool mapiCachedModeRequired;

			// Token: 0x04000162 RID: 354
			private ExOrgInfoFlags exchangeOrganizationInfo;
		}
	}
}
