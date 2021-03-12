using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D2 RID: 2002
	internal abstract class MsoFullSyncCookieManager : FullSyncCookieManager
	{
		// Token: 0x06006368 RID: 25448 RVA: 0x00158CFC File Offset: 0x00156EFC
		protected MsoFullSyncCookieManager(Guid contextId) : base(contextId)
		{
			this.configSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromExternalDirectoryOrganizationId(contextId), 55, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\CookieManager\\MsoFullSyncCookieManager.cs");
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x00158D38 File Offset: 0x00156F38
		public override byte[] ReadCookie()
		{
			MsoTenantCookieContainer msoTenantCookieContainer = this.configSession.GetMsoTenantCookieContainer(base.ContextId);
			if (msoTenantCookieContainer == null)
			{
				return null;
			}
			MultiValuedProperty<byte[]> multiValuedProperty = this.RetrievePersistedPageTokens(msoTenantCookieContainer);
			if (multiValuedProperty.Count == 0)
			{
				return null;
			}
			base.LastCookie = MsoFullSyncCookie.FromStorageCookie(multiValuedProperty[0]);
			return base.LastCookie.RawCookie;
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x00158D8C File Offset: 0x00156F8C
		public override DateTime? GetMostRecentCookieTimestamp()
		{
			if (base.LastCookie == null)
			{
				this.ReadCookie();
			}
			if (base.LastCookie != null)
			{
				return new DateTime?(base.LastCookie.Timestamp);
			}
			return null;
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x00158DCC File Offset: 0x00156FCC
		public override void WriteCookie(byte[] cookie, DateTime timestamp)
		{
			if (cookie == null || cookie.Length == 0)
			{
				throw new ArgumentException("cookie is empty");
			}
			MsoTenantCookieContainer msoTenantCookieContainer = this.configSession.GetMsoTenantCookieContainer(base.ContextId);
			if (msoTenantCookieContainer != null)
			{
				int cookieVersion = (base.LastCookie != null) ? base.LastCookie.Version : 1;
				MsoFullSyncCookie msoFullSyncCookie = new MsoFullSyncCookie(cookie, cookieVersion);
				msoFullSyncCookie.Timestamp = timestamp;
				if (base.LastCookie != null)
				{
					msoFullSyncCookie.SyncType = base.LastCookie.SyncType;
					msoFullSyncCookie.SyncRequestor = base.LastCookie.SyncRequestor;
					msoFullSyncCookie.WhenSyncRequested = base.LastCookie.WhenSyncRequested;
					msoFullSyncCookie.WhenSyncStarted = ((base.LastCookie.WhenSyncStarted != DateTime.MinValue) ? base.LastCookie.WhenSyncStarted : timestamp);
				}
				byte[] item = msoFullSyncCookie.ToStorageCookie();
				MultiValuedProperty<byte[]> multiValuedProperty = this.RetrievePersistedPageTokens(msoTenantCookieContainer);
				multiValuedProperty.Clear();
				multiValuedProperty.Add(item);
				this.configSession.Save(msoTenantCookieContainer);
				this.LogPersistPageTokenEvent();
			}
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x00158EC4 File Offset: 0x001570C4
		public void WriteInitialSyncCookie(TenantSyncType type, string requestor)
		{
			string s = (type == TenantSyncType.Full) ? "start" : "start-partial-sync";
			base.LastCookie = new MsoFullSyncCookie(Encoding.UTF8.GetBytes(s), 3);
			base.LastCookie.SyncType = type;
			base.LastCookie.SyncRequestor = requestor;
			base.LastCookie.WhenSyncRequested = DateTime.UtcNow;
			this.WriteCookie(base.LastCookie.RawCookie, DateTime.MinValue);
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x00158F37 File Offset: 0x00157137
		public static bool IsInitialFullSyncCookie(byte[] cookie)
		{
			return MsoFullSyncCookieManager.CompareCookie(cookie, "start");
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x00158F44 File Offset: 0x00157144
		public static bool IsInitialPartialSyncCookie(byte[] cookie)
		{
			return MsoFullSyncCookieManager.CompareCookie(cookie, "start-partial-sync");
		}

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x0600636F RID: 25455 RVA: 0x00158F51 File Offset: 0x00157151
		public override string DomainController
		{
			get
			{
				if (this.configSession != null)
				{
					return this.configSession.DomainController;
				}
				return string.Empty;
			}
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x00158F6C File Offset: 0x0015716C
		private static bool CompareCookie(byte[] cookie, string syncCookieString)
		{
			if (cookie == null || cookie.Length == 0)
			{
				throw new ArgumentException("Cookie is empty.");
			}
			if (cookie.Length != syncCookieString.Length)
			{
				return false;
			}
			string @string = Encoding.UTF8.GetString(cookie);
			return string.Equals(@string, syncCookieString, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x00158FB0 File Offset: 0x001571B0
		public override void ClearCookie()
		{
			MsoTenantCookieContainer msoTenantCookieContainer = this.configSession.GetMsoTenantCookieContainer(base.ContextId);
			if (msoTenantCookieContainer != null)
			{
				MultiValuedProperty<byte[]> multiValuedProperty = this.RetrievePersistedPageTokens(msoTenantCookieContainer);
				multiValuedProperty.Clear();
				if (multiValuedProperty.Changed)
				{
					this.configSession.Save(msoTenantCookieContainer);
				}
				this.LogClearPageTokenEvent();
			}
		}

		// Token: 0x06006372 RID: 25458
		protected abstract MultiValuedProperty<byte[]> RetrievePersistedPageTokens(MsoTenantCookieContainer container);

		// Token: 0x06006373 RID: 25459
		protected abstract void LogPersistPageTokenEvent();

		// Token: 0x06006374 RID: 25460
		protected abstract void LogClearPageTokenEvent();

		// Token: 0x04004248 RID: 16968
		private const string StartFullSyncCookie = "start";

		// Token: 0x04004249 RID: 16969
		private const string StartPartialSyncCookie = "start-partial-sync";

		// Token: 0x0400424A RID: 16970
		private readonly ITenantConfigurationSession configSession;
	}
}
