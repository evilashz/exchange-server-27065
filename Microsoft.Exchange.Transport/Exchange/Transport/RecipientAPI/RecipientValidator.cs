using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x02000533 RID: 1331
	internal class RecipientValidator : IDisposeTrackable, IDisposable
	{
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x001044F1 File Offset: 0x001026F1
		public bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x001044F9 File Offset: 0x001026F9
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x00104501 File Offset: 0x00102701
		protected DateTime LastReload
		{
			get
			{
				return this.lastReload;
			}
			set
			{
				this.lastReload = value;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x0010450A File Offset: 0x0010270A
		protected GuardedTimer Timer
		{
			get
			{
				return this.timer;
			}
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00104514 File Offset: 0x00102714
		public bool RecipientDoesNotExist(RoutingAddress smtpAddress)
		{
			StringHasher stringHasher = new StringHasher();
			ulong hash = stringHasher.GetHash((string)smtpAddress);
			if (!this.initialized)
			{
				return false;
			}
			bool result;
			try
			{
				this.cacheLock.EnterReadLock();
				result = !this.recipientHashes.ContainsKey(hash);
			}
			finally
			{
				this.cacheLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00104578 File Offset: 0x00102778
		public bool Initialize()
		{
			return this.Initialize(false);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x001045E8 File Offset: 0x001027E8
		public bool Initialize(bool suppressDisposeTracker)
		{
			Server localServer = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				if (this.configSession == null)
				{
					this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 231, "Initialize", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\RecipientAPI\\RecipientValidator.cs");
				}
				localServer = this.configSession.FindLocalServer();
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				RecipientValidator.eventLog.LogEvent(TransportEventLogConstants.Tuple_DirectoryUnavailableLoadingValidationCache, null, new object[]
				{
					adoperationResult.Exception.Message
				});
				return false;
			}
			if (localServer.IsEdgeServer)
			{
				this.edgeRole = true;
			}
			else if (!localServer.IsHubTransportServer)
			{
				throw new InvalidOperationException("wrong location");
			}
			this.timer = new GuardedTimer(new TimerCallback(this.LoadRecipients), null, 0, -1);
			this.disposeTracker = this.GetDisposeTracker();
			if (suppressDisposeTracker)
			{
				this.SuppressDisposeTracker();
			}
			return true;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x001046AC File Offset: 0x001028AC
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.cacheLock != null)
				{
					this.cacheLock.Dispose();
					this.cacheLock = null;
				}
				if (this.timer != null)
				{
					this.timer.Dispose(true);
					this.timer = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00104717 File Offset: 0x00102917
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RecipientValidator>(this);
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x0010471F File Offset: 0x0010291F
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x001047D8 File Offset: 0x001029D8
		protected virtual bool TryConnectToDirectory(Cookie cookie)
		{
			if (this.connection == null)
			{
				ADObjectId rootId = null;
				PooledLdapConnection sourcePooledConnection = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					if (this.recipientSession == null)
					{
						this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 339, "TryConnectToDirectory", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\RecipientAPI\\RecipientValidator.cs");
					}
					sourcePooledConnection = this.recipientSession.GetReadConnection(this.edgeRole ? null : cookie.DomainController, ref rootId);
				}, 3);
				if (!adoperationResult.Succeeded)
				{
					RecipientValidator.eventLog.LogEvent(TransportEventLogConstants.Tuple_DirectoryUnavailableLoadingValidationCache, null, new object[]
					{
						adoperationResult.Exception.Message
					});
					cookie.DomainController = null;
					return false;
				}
				this.connection = new Connection(sourcePooledConnection);
			}
			return true;
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x001048EC File Offset: 0x00102AEC
		protected virtual bool TryInitializeCookie(bool shouldReload)
		{
			if (shouldReload)
			{
				this.cookies.Clear();
			}
			if (this.edgeRole)
			{
				if (!this.cookies.ContainsKey("OU=MSExchangeGateway"))
				{
					Cookie cookie = new Cookie("OU=MSExchangeGateway");
					this.cookies.Add(cookie.BaseDN, cookie);
				}
				return true;
			}
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADForest localForest = ADForest.GetLocalForest();
				ADCrossRef[] domainPartitions = localForest.GetDomainPartitions();
				foreach (ADCrossRef adcrossRef in domainPartitions)
				{
					string distinguishedName = adcrossRef.NCName.DistinguishedName;
					if (!this.cookies.ContainsKey(distinguishedName))
					{
						Cookie cookie2 = new Cookie(distinguishedName);
						this.cookies.Add(cookie2.BaseDN, cookie2);
					}
				}
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				RecipientValidator.eventLog.LogEvent(TransportEventLogConstants.Tuple_DirectoryUnavailableLoadingValidationCache, null, new object[]
				{
					adoperationResult.Exception.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x0010498C File Offset: 0x00102B8C
		private void LoadRecipients(object state)
		{
			bool flag = DateTime.UtcNow - this.lastReload > Components.TransportAppConfig.RecipientValidtor.ReloadInterval;
			if (!this.TryInitializeCookie(flag))
			{
				return;
			}
			Dictionary<ulong, int> dictionary = null;
			if (flag)
			{
				dictionary = new Dictionary<ulong, int>();
			}
			bool flag2 = true;
			Dictionary<string, Cookie> dictionary2 = new Dictionary<string, Cookie>();
			DateTime utcNow = DateTime.UtcNow;
			foreach (string key in this.cookies.Keys)
			{
				Cookie cookie = this.cookies[key];
				Cookie cookie2 = null;
				if (!this.TryConnectToDirectory(cookie))
				{
					return;
				}
				if (this.LoadRecipientsOneDomain(cookie, dictionary, out cookie2))
				{
					cookie2.DomainController = this.connection.Fqdn;
					cookie2.LastUpdated = DateTime.UtcNow;
					dictionary2.Add(cookie2.BaseDN, cookie2);
				}
				else
				{
					flag2 = false;
				}
			}
			foreach (Cookie cookie3 in dictionary2.Values)
			{
				this.cookies[cookie3.BaseDN] = cookie3;
			}
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(utcNow);
			if (flag && flag2)
			{
				Interlocked.Exchange<Dictionary<ulong, int>>(ref this.recipientHashes, dictionary);
				this.lastReload = DateTime.UtcNow;
				RecipientValidator.eventLog.LogEvent(TransportEventLogConstants.Tuple_RecipientValidationCacheLoaded, null, new object[]
				{
					timeSpan.ToString(),
					this.recipients.ToString(),
					this.recipientHashes.Count.ToString()
				});
				this.initialized = true;
			}
			if (this.connection != null)
			{
				this.connection.Dispose();
				this.connection = null;
			}
			this.timer.Change((int)Components.TransportAppConfig.RecipientValidtor.RefreshInterval.TotalMilliseconds, -1);
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00104BA4 File Offset: 0x00102DA4
		private bool LoadRecipientsOneDomain(Cookie cookie, Dictionary<ulong, int> newHashes, out Cookie retCookie)
		{
			retCookie = Cookie.Clone(cookie);
			bool flag = cookie.CookieValue == null;
			try
			{
				foreach (ExSearchResultEntry exSearchResultEntry in this.connection.DirSyncScan(retCookie, "(proxyAddresses=*)", SearchScope.Subtree, RecipientValidator.attributes))
				{
					DirectoryAttribute directoryAttribute = null;
					if (!exSearchResultEntry.IsDeleted && exSearchResultEntry.Attributes.TryGetValue("proxyAddresses", out directoryAttribute))
					{
						int i = 0;
						while (i < directoryAttribute.Count)
						{
							ProxyAddress proxyAddress = null;
							try
							{
								proxyAddress = ProxyAddress.Parse(directoryAttribute[i] as string);
							}
							catch (ArgumentException)
							{
								goto IL_11C;
							}
							goto IL_87;
							IL_11C:
							i++;
							continue;
							IL_87:
							ulong hash;
							if (proxyAddress.Prefix == RecipientValidator.smtpHashPrefix)
							{
								if (!ulong.TryParse(proxyAddress.AddressString, NumberStyles.HexNumber, null, out hash))
								{
									goto IL_11C;
								}
							}
							else
							{
								if (!(proxyAddress is SmtpProxyAddress))
								{
									goto IL_11C;
								}
								hash = RecipientValidator.stringHasher.GetHash(proxyAddress.AddressString);
							}
							if (!flag)
							{
								try
								{
									this.cacheLock.EnterWriteLock();
									if (!this.recipientHashes.ContainsKey(hash))
									{
										this.recipientHashes.Add(hash, 0);
									}
								}
								finally
								{
									this.cacheLock.ExitWriteLock();
								}
								goto IL_11C;
							}
							if (!newHashes.ContainsKey(hash))
							{
								newHashes.Add(hash, 0);
								goto IL_11C;
							}
							goto IL_11C;
						}
						this.recipients++;
					}
				}
			}
			catch (ExDirectoryException ex)
			{
				RecipientValidator.eventLog.LogEvent(TransportEventLogConstants.Tuple_DirectoryUnavailableLoadingValidationCache, null, new object[]
				{
					ex.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x04001F9E RID: 8094
		private const string AdamUserContainerNC = "OU=MSExchangeGateway";

		// Token: 0x04001F9F RID: 8095
		private const string RecipientFilter = "(proxyAddresses=*)";

		// Token: 0x04001FA0 RID: 8096
		private static readonly string[] attributes = new string[]
		{
			"proxyAddresses"
		};

		// Token: 0x04001FA1 RID: 8097
		private static readonly StringHasher stringHasher = new StringHasher();

		// Token: 0x04001FA2 RID: 8098
		private static readonly ProxyAddressPrefix smtpHashPrefix = new CustomProxyAddressPrefix("sh");

		// Token: 0x04001FA3 RID: 8099
		private static ExEventLog eventLog = new ExEventLog(new Guid("8cd349b7-795a-47f7-b99e-6f6a7fb399e1"), TransportEventLog.GetEventSource());

		// Token: 0x04001FA4 RID: 8100
		private GuardedTimer timer;

		// Token: 0x04001FA5 RID: 8101
		private Dictionary<ulong, int> recipientHashes = new Dictionary<ulong, int>();

		// Token: 0x04001FA6 RID: 8102
		private IRecipientSession recipientSession;

		// Token: 0x04001FA7 RID: 8103
		private ITopologyConfigurationSession configSession;

		// Token: 0x04001FA8 RID: 8104
		private Dictionary<string, Cookie> cookies = new Dictionary<string, Cookie>();

		// Token: 0x04001FA9 RID: 8105
		private bool initialized;

		// Token: 0x04001FAA RID: 8106
		private int recipients;

		// Token: 0x04001FAB RID: 8107
		private bool edgeRole;

		// Token: 0x04001FAC RID: 8108
		private Connection connection;

		// Token: 0x04001FAD RID: 8109
		private DisposeTracker disposeTracker;

		// Token: 0x04001FAE RID: 8110
		private bool disposed;

		// Token: 0x04001FAF RID: 8111
		private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

		// Token: 0x04001FB0 RID: 8112
		private DateTime lastReload = DateTime.MinValue;
	}
}
