using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Datacenter
{
	// Token: 0x02000002 RID: 2
	internal abstract class DatacenterTargetConnection : TargetConnection
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected DatacenterTargetConnection(int localServerVersion, DatacenterTargetServerConfig config, EnhancedTimeSpan syncInterval, EdgeSyncLogSession logSession, Trace tracer) : base(localServerVersion, config)
		{
			this.diagSession = new EdgeSyncDiag(logSession, tracer);
			this.syncInterval = syncInterval;
			this.leaseManager = new FileLeaseManager(this.LeaseFileName, config.PrimaryLeaseLocation, config.BackupLeaseLocation, this.SyncInterval, logSession, this.Tracer);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002126 File Offset: 0x00000326
		public EdgeSyncDiag DiagSession
		{
			get
			{
				return this.diagSession;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000212E File Offset: 0x0000032E
		public EdgeSyncLogSession LogSession
		{
			get
			{
				return this.diagSession.LogSession;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000213B File Offset: 0x0000033B
		public Trace Tracer
		{
			get
			{
				return this.diagSession.Tracer;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5
		protected abstract IConfigurationSession ConfigSession { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6
		protected abstract string LeaseFileName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002148 File Offset: 0x00000348
		protected virtual EnhancedTimeSpan SyncInterval
		{
			get
			{
				return this.syncInterval;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002150 File Offset: 0x00000350
		public override bool TryReadCookie(out Dictionary<string, Cookie> cookies)
		{
			if (!this.TryReadCookie(this.ConfigSession, out cookies))
			{
				return false;
			}
			this.ignoreCookieDomainController = false;
			this.Tracer.TraceDebug<int, ADObjectId>((long)this.GetHashCode(), "Successfully read {0} cookie(s) from container <{1}>", cookies.Count, this.GetCookieContainerId());
			return true;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public override bool TrySaveCookie(Dictionary<string, Cookie> cookies)
		{
			Container cookieContainer;
			if (!this.TryGetCookieContainer(this.ConfigSession, out cookieContainer))
			{
				return false;
			}
			MultiValuedProperty<byte[]> multiValuedProperty = new MultiValuedProperty<byte[]>();
			DatacenterTargetConnection.SerializeCookies(cookies.Values, multiValuedProperty);
			cookieContainer.EdgeSyncCookies = multiValuedProperty;
			Cookie cookie = null;
			using (Dictionary<string, Cookie>.ValueCollection.Enumerator enumerator = cookies.Values.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Cookie cookie2 = enumerator.Current;
					cookie = cookie2;
				}
			}
			bool flag = false;
			string text = null;
			if (cookie != null && !string.IsNullOrEmpty(cookie.DomainController))
			{
				text = this.ConfigSession.DomainController;
				this.ConfigSession.DomainController = cookie.DomainController;
				flag = true;
			}
			ADOperationResult adoperationResult = null;
			for (int i = 0; i < 2; i++)
			{
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					this.ConfigSession.Save(cookieContainer);
				}, 3);
				if (adoperationResult.Succeeded)
				{
					this.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully saved {0} cookie(s) to container <{1}> on DomainController <{2}>; OriginalDC <{3}>; Details <{4}>", new object[]
					{
						cookies.Count,
						this.GetCookieContainerId(),
						this.ConfigSession.DomainController,
						text,
						Util.GetCookieInformationToLog(cookies)
					});
					break;
				}
				if (i == 0)
				{
					if (!flag)
					{
						this.DiagSession.LogAndTraceException(adoperationResult.Exception, "Failed to save cookies to container <{0}> against ConfigSession's DomainController <{1}>", new object[]
						{
							this.GetCookieContainerId(),
							this.ConfigSession.DomainController
						});
						break;
					}
					this.DiagSession.LogAndTraceException(adoperationResult.Exception, "Failed to save cookies to container <{0}> against Cookie's DomainController <{1}>. Fallback to ConfigSession's DomainController and retry.", new object[]
					{
						this.GetCookieContainerId(),
						this.ConfigSession.DomainController
					});
					this.ConfigSession.DomainController = text;
				}
				else
				{
					this.DiagSession.LogAndTraceException(adoperationResult.Exception, "Failed to save cookies to container <{0}> against ConfigSession's DomainController <{1}> after fallback.", new object[]
					{
						this.GetCookieContainerId(),
						this.ConfigSession.DomainController
					});
				}
			}
			return adoperationResult.Succeeded;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023EC File Offset: 0x000005EC
		public override LeaseToken GetLease()
		{
			return this.leaseManager.GetLease();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023FC File Offset: 0x000005FC
		public override bool CanTakeOverLease(bool force, LeaseToken lease, DateTime now)
		{
			bool flag2;
			bool flag = this.leaseManager.CanTakeOverLease(force, lease, now, EdgeSyncSvc.EdgeSync.Topology.SiteBridgeheadDistinguishedNames, out flag2);
			if (flag && flag2)
			{
				this.ignoreCookieDomainController = true;
			}
			return flag;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002437 File Offset: 0x00000637
		public override void SetLease(LeaseToken leaseToken)
		{
			this.leaseManager.SetLease(leaseToken);
		}

		// Token: 0x0600000D RID: 13
		protected abstract ADObjectId GetCookieContainerId();

		// Token: 0x0600000E RID: 14 RVA: 0x00002448 File Offset: 0x00000648
		protected bool TryReadCookie(IConfigurationSession adSystemConfigurationSession, out Dictionary<string, Cookie> cookies)
		{
			cookies = null;
			Container container;
			if (!this.TryGetCookieContainer(adSystemConfigurationSession, out container))
			{
				return false;
			}
			if (!this.TryCreateCookieDictionary(container.EdgeSyncCookies, out cookies))
			{
				this.DiagSession.LogAndTraceError("Failed to read cookies from container <{0}>", new object[]
				{
					this.GetCookieContainerId()
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000249C File Offset: 0x0000069C
		private static void SerializeCookies(IEnumerable<Cookie> source, ICollection<byte[]> destination)
		{
			foreach (Cookie cookie in source)
			{
				destination.Add(Encoding.ASCII.GetBytes(cookie.Serialize()));
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000251C File Offset: 0x0000071C
		private bool TryGetCookieContainer(IConfigurationSession configurationSession, out Container cookieContainer)
		{
			Container tempContainer = null;
			cookieContainer = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				tempContainer = configurationSession.Read<Container>(this.GetCookieContainerId());
			}, 3);
			if (adoperationResult.Succeeded)
			{
				cookieContainer = tempContainer;
			}
			else
			{
				this.DiagSession.LogAndTraceException(adoperationResult.Exception, "Failed to read cookie container with ID <{0}> from AD", new object[]
				{
					this.GetCookieContainerId() ?? "null"
				});
			}
			return adoperationResult.Succeeded;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025A4 File Offset: 0x000007A4
		private bool TryCreateCookieDictionary(IList<byte[]> source, out Dictionary<string, Cookie> dictionary)
		{
			dictionary = null;
			Dictionary<string, Cookie> dictionary2 = new Dictionary<string, Cookie>(source.Count);
			foreach (byte[] bytes in source)
			{
				Cookie cookie;
				if (!Cookie.TryDeserialize(Encoding.ASCII.GetString(bytes), out cookie))
				{
					this.DiagSession.LogAndTraceError("Failed to add cookie to dictionary: corrupt cookie detected", new object[0]);
					return false;
				}
				if (dictionary2.ContainsKey(cookie.BaseDN))
				{
					this.DiagSession.LogAndTraceError("Duplicate cookie detected; BaseDN: <{0}>; DC: <{1}>", new object[]
					{
						cookie.BaseDN,
						cookie.DomainController ?? "null"
					});
				}
				else
				{
					this.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Cookie loaded; BaseDN: <{0}>; DC: <{1}>", cookie.BaseDN, cookie.DomainController ?? "null");
					if (this.ignoreCookieDomainController)
					{
						cookie.DomainController = null;
					}
					dictionary2.Add(cookie.BaseDN, cookie);
				}
			}
			dictionary = dictionary2;
			return true;
		}

		// Token: 0x04000001 RID: 1
		protected const int ADRetryCount = 3;

		// Token: 0x04000002 RID: 2
		private readonly EnhancedTimeSpan syncInterval;

		// Token: 0x04000003 RID: 3
		private EdgeSyncDiag diagSession;

		// Token: 0x04000004 RID: 4
		private FileLeaseManager leaseManager;

		// Token: 0x04000005 RID: 5
		private bool ignoreCookieDomainController;
	}
}
