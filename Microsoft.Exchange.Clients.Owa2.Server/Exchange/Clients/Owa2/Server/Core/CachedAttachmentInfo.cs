using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200003A RID: 58
	internal class CachedAttachmentInfo
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00005620 File Offset: 0x00003820
		private CachedAttachmentInfo(string mailboxSmtpAddress, string logonSmtpAddress, string logonDisplayName, string logonPuid, SecurityIdentifier logonSid, string cultureName, string ewsAttachmentId, string sessionId)
		{
			this.MailboxSmtpAddress = mailboxSmtpAddress;
			this.LogonSmtpAddress = logonSmtpAddress;
			this.LogonDisplayName = logonDisplayName;
			this.LogonPuid = logonPuid;
			this.LogonSid = logonSid;
			this.CultureName = cultureName;
			this.EwsAttachmentId = ewsAttachmentId;
			this.SessionId = sessionId;
			this.instanceId = Interlocked.Increment(ref CachedAttachmentInfo.nextInstanceId);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005680 File Offset: 0x00003880
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005688 File Offset: 0x00003888
		public string MailboxSmtpAddress { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005691 File Offset: 0x00003891
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005699 File Offset: 0x00003899
		public string LogonSmtpAddress { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000056A2 File Offset: 0x000038A2
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000056AA File Offset: 0x000038AA
		public string LogonDisplayName { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000056B3 File Offset: 0x000038B3
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000056BB File Offset: 0x000038BB
		public string LogonPuid { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000056C4 File Offset: 0x000038C4
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000056CC File Offset: 0x000038CC
		public SecurityIdentifier LogonSid { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000056D5 File Offset: 0x000038D5
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000056DD File Offset: 0x000038DD
		public string CultureName { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000056E6 File Offset: 0x000038E6
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000056EE File Offset: 0x000038EE
		public string EwsAttachmentId { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000056F7 File Offset: 0x000038F7
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000056FF File Offset: 0x000038FF
		public string SessionId { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005708 File Offset: 0x00003908
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005710 File Offset: 0x00003910
		public CobaltStore CobaltStore { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005719 File Offset: 0x00003919
		public int LockCount
		{
			get
			{
				return this.lockCount;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005721 File Offset: 0x00003921
		public int InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000572C File Offset: 0x0000392C
		public static CachedAttachmentInfo GetInstance(string mailboxSmtpAddress, string ewsAttachmentId, string sessionId, SecurityIdentifier logonSid, string cultureName)
		{
			string cacheKey = CachedAttachmentInfo.GetCacheKey(mailboxSmtpAddress, ewsAttachmentId);
			CachedAttachmentInfo cachedAttachmentInfo = CachedAttachmentInfo.GetFromCache(cacheKey);
			if (cachedAttachmentInfo != null)
			{
				OwaApplication.GetRequestDetailsLogger.Set(WacRequestHandlerMetadata.CacheHit, true);
				return cachedAttachmentInfo;
			}
			CachedAttachmentInfo result;
			lock (CachedAttachmentInfo.factorySynchronizer)
			{
				cachedAttachmentInfo = CachedAttachmentInfo.GetFromCache(cacheKey);
				if (cachedAttachmentInfo != null)
				{
					OwaApplication.GetRequestDetailsLogger.Set(WacRequestHandlerMetadata.CacheHit, true);
					result = cachedAttachmentInfo;
				}
				else
				{
					OwaApplication.GetRequestDetailsLogger.Set(WacRequestHandlerMetadata.CacheHit, false);
					string domain = ((SmtpAddress)mailboxSmtpAddress).Domain;
					string logonSmtpAddress;
					string logonDisplayName;
					string logonPuid;
					CachedAttachmentInfo.GetLogonUserInfo(logonSid, domain, out logonSmtpAddress, out logonDisplayName, out logonPuid);
					cachedAttachmentInfo = new CachedAttachmentInfo(mailboxSmtpAddress, logonSmtpAddress, logonDisplayName, logonPuid, logonSid, cultureName, ewsAttachmentId, sessionId);
					cachedAttachmentInfo.InsertIntoCache(cacheKey);
					result = cachedAttachmentInfo;
				}
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000580C File Offset: 0x00003A0C
		public static string GetCacheKey(string primarySmtpAddress, string ewsAttachmentId)
		{
			return primarySmtpAddress + " " + ewsAttachmentId;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000581A File Offset: 0x00003A1A
		public static CachedAttachmentInfo GetFromCache(string key)
		{
			return (CachedAttachmentInfo)HttpRuntime.Cache.Get(key);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000582C File Offset: 0x00003A2C
		public void IncrementLockCount()
		{
			Interlocked.Increment(ref this.lockCount);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000583A File Offset: 0x00003A3A
		public void DecrementLockCount()
		{
			Interlocked.Decrement(ref this.lockCount);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005848 File Offset: 0x00003A48
		public void InsertIntoCache(string key)
		{
			if (WacConfiguration.Instance.AccessTokenCacheTime > TimeSpan.FromSeconds(0.0))
			{
				HttpRuntime.Cache.Insert(key, this, null, Cache.NoAbsoluteExpiration, WacConfiguration.Instance.AccessTokenCacheTime, new CacheItemUpdateCallback(CachedAttachmentInfo.OnCacheEntryExpired));
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000589C File Offset: 0x00003A9C
		private static void OnCacheEntryExpired(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
		{
			expensiveObject = null;
			dependency = null;
			absoluteExpiration = Cache.NoAbsoluteExpiration;
			slidingExpiration = Cache.NoSlidingExpiration;
			CachedAttachmentInfo cachedAttachmentInfo = (CachedAttachmentInfo)HttpRuntime.Cache.Get(key);
			if (cachedAttachmentInfo.CobaltStore == null)
			{
				return;
			}
			WacRequestHandler.OnCacheEntryExpired(cachedAttachmentInfo);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000059A8 File Offset: 0x00003BA8
		private static void GetLogonUserInfo(SecurityIdentifier sid, string smtpDomain, out string smtpAddress, out string displayName, out string puid)
		{
			string smtpAddressTemporary = sid.ToString();
			string displayNameTemporary = sid.ToString();
			string puidTemporary = string.Empty;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpDomain), 339, "GetLogonUserInfo", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\attachment\\CachedAttachmentInfo.cs");
				PropertyDefinition[] properties = new PropertyDefinition[]
				{
					ADRecipientSchema.PrimarySmtpAddress,
					ADRecipientSchema.LegacyExchangeDN,
					ADRecipientSchema.DisplayName,
					ADUserSchema.NetID
				};
				ADRawEntry entry = tenantOrRootOrgRecipientSession.FindADRawEntryBySid(sid, properties);
				try
				{
					CachedAttachmentInfo.SafeGetValue(entry, ADRecipientSchema.PrimarySmtpAddress, ref smtpAddressTemporary);
					CachedAttachmentInfo.SafeGetValue(entry, ADRecipientSchema.DisplayName, ref displayNameTemporary);
					CachedAttachmentInfo.SafeGetValue(entry, ADUserSchema.NetID, ref puidTemporary);
				}
				catch (NotInBagPropertyErrorException)
				{
				}
			});
			smtpAddress = smtpAddressTemporary;
			displayName = displayNameTemporary;
			puid = puidTemporary;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005A20 File Offset: 0x00003C20
		private static void SafeGetValue(ADRawEntry entry, PropertyDefinition property, ref string result)
		{
			object obj = entry[property];
			if (obj == null)
			{
				return;
			}
			result = obj.ToString();
		}

		// Token: 0x04000078 RID: 120
		private static readonly object factorySynchronizer = new object();

		// Token: 0x04000079 RID: 121
		private static int nextInstanceId;

		// Token: 0x0400007A RID: 122
		private readonly int instanceId;

		// Token: 0x0400007B RID: 123
		private int lockCount;
	}
}
