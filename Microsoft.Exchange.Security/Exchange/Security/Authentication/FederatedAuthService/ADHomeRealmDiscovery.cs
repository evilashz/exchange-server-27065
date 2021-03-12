using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000052 RID: 82
	internal class ADHomeRealmDiscovery : IHomeRealmDiscovery
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000DD36 File Offset: 0x0000BF36
		private static LiveIdBasicAuthenticationCountersInstance counters
		{
			get
			{
				return AuthServiceHelper.PerformanceCounters;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000DD3D File Offset: 0x0000BF3D
		public ADHomeRealmDiscovery(LiveIdInstanceType instance)
		{
			this.instance = instance;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public LiveIdInstanceType Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public string RealmDiscoveryUri
		{
			get
			{
				return "Offline HomeRealmDiscovery";
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000DD5B File Offset: 0x0000BF5B
		public string StsTag
		{
			get
			{
				return "OfflineHRD";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000DD62 File Offset: 0x0000BF62
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000DD6A File Offset: 0x0000BF6A
		public string ErrorString { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000DD73 File Offset: 0x0000BF73
		public string LiveServer
		{
			get
			{
				return "Offline HomeRealmDiscovery";
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000DD7A File Offset: 0x0000BF7A
		public long Latency
		{
			get
			{
				if (this.stopwatch != null)
				{
					return this.stopwatch.ElapsedMilliseconds;
				}
				return -1L;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000DD92 File Offset: 0x0000BF92
		public long SSLConnectionLatency
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DD98 File Offset: 0x0000BF98
		public IAsyncResult StartRequestChain(object userDomain, AsyncCallback callback, object state)
		{
			this.stopwatch = Stopwatch.StartNew();
			this.authService = (state as AuthService);
			this.userDomain = (string)userDomain;
			if (this.authService == null)
			{
				this.ErrorString = "Internal Service Error: authService is null ";
				throw new ArgumentNullException("authService");
			}
			if (string.IsNullOrEmpty(this.userDomain))
			{
				this.ErrorString = "Internal Service Erro: userDomain is null or empty";
				throw new ArgumentException("userDomain");
			}
			LazyAsyncResult ar = new LazyAsyncResult(null, state, null);
			return callback.BeginInvoke(ar, null, null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public IAsyncResult ProcessRequest(IAsyncResult asyncResult, AsyncCallback callback, object state)
		{
			LazyAsyncResult ar = new LazyAsyncResult(null, state, null);
			return callback.BeginInvoke(ar, null, null);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public DomainConfig ProcessResponse(IAsyncResult asyncResult)
		{
			DomainConfig domainConfig = null;
			DomainConfig result;
			try
			{
				if (this.authService == null)
				{
					this.ErrorString = "Internal Service Error: authService is null ";
					throw new ArgumentNullException("authService");
				}
				if (string.IsNullOrEmpty(this.userDomain))
				{
					this.ErrorString = "Internal Service Erro: userDomain is null or empty";
					throw new ArgumentException("userDomain");
				}
				ITenantConfigurationSession session = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(this.userDomain), 144, "ProcessResponse", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\FederatedAuthService\\ADHomeRealmDiscovery.cs");
				string errorString;
				domainConfig = OfflineOrgIdAuth.GetHRDEntryFromAD(session, null, this.userDomain, out errorString);
				this.ErrorString = errorString;
				if (domainConfig == null)
				{
					throw new ADHrdException(this.ErrorString);
				}
				result = domainConfig;
			}
			catch (CannotResolveTenantNameException ex)
			{
				throw new ADHrdException(ex.Message);
			}
			finally
			{
				this.stopwatch.Stop();
				if (domainConfig == null)
				{
					ADHomeRealmDiscovery.counters.NumberOfFailedADHrdRequests.Increment();
				}
			}
			return result;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000DF28 File Offset: 0x0000C128
		public void Abort()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000DF2A File Offset: 0x0000C12A
		public string GetLatency()
		{
			return string.Format("<{0}-{1}-{2}ms>", "Offline HomeRealmDiscovery", this.instance.ToString(), this.Latency);
		}

		// Token: 0x040001FB RID: 507
		private string userDomain;

		// Token: 0x040001FC RID: 508
		private AuthService authService;

		// Token: 0x040001FD RID: 509
		private LiveIdInstanceType instance;

		// Token: 0x040001FE RID: 510
		private Stopwatch stopwatch;
	}
}
