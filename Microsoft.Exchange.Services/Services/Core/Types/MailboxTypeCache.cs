using System;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200010D RID: 269
	internal sealed class MailboxTypeCache : LazyLookupTimeoutCache<string, MailboxHelper.MailboxTypeType?>
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00026052 File Offset: 0x00024252
		private MailboxTypeCache() : base(10, 65536, false, TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(360.0))
		{
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00026080 File Offset: 0x00024280
		internal static MailboxTypeCache Instance
		{
			get
			{
				if (MailboxTypeCache.instance == null)
				{
					lock (MailboxTypeCache.lockObject)
					{
						if (MailboxTypeCache.instance == null)
						{
							MailboxTypeCache.instance = new MailboxTypeCache();
						}
					}
				}
				return MailboxTypeCache.instance;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000260D8 File Offset: 0x000242D8
		internal long CacheSize
		{
			get
			{
				return this.cacheSize;
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000260E0 File Offset: 0x000242E0
		internal bool TryGetValue(string key, out MailboxHelper.MailboxTypeType? mailboxType)
		{
			mailboxType = base.Get(key);
			if (mailboxType == null)
			{
				int num = -1;
				if (RequestDetailsLogger.Current != null)
				{
					if (int.TryParse(RequestDetailsLogger.Current.Get(EwsMetadata.MailboxTypeCacheMissCount), out num))
					{
						RequestDetailsLogger.Current.Set(EwsMetadata.MailboxTypeCacheMissCount, num + 1);
					}
					else
					{
						RequestDetailsLogger.Current.Set(EwsMetadata.MailboxTypeCacheMissCount, 1);
					}
				}
			}
			else
			{
				int num2 = -1;
				if (RequestDetailsLogger.Current != null)
				{
					if (int.TryParse(RequestDetailsLogger.Current.Get(EwsMetadata.MailboxTypeCacheHitCount), out num2))
					{
						RequestDetailsLogger.Current.Set(EwsMetadata.MailboxTypeCacheHitCount, num2 + 1);
					}
					else
					{
						RequestDetailsLogger.Current.Set(EwsMetadata.MailboxTypeCacheHitCount, 1);
					}
				}
			}
			return mailboxType != null;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000261BC File Offset: 0x000243BC
		internal bool TryAdd(string key, MailboxHelper.MailboxTypeType mailboxType)
		{
			bool flag = this.TryPerformAdd(key, new MailboxHelper.MailboxTypeType?(mailboxType));
			if (!flag)
			{
				MailboxHelper.MailboxTypeType? mailboxTypeType = base.Get(key);
				if (mailboxTypeType != null && mailboxType != mailboxTypeType.Value && RequestDetailsLogger.Current != null)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(CallContext.Current.ProtocolLog, "MailboxTypeCacheMismatch", string.Format("Actual:{0} Expected:{1}", mailboxTypeType.Value, mailboxType));
				}
			}
			else
			{
				Interlocked.Increment(ref this.cacheSize);
			}
			return flag;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002623C File Offset: 0x0002443C
		protected override MailboxHelper.MailboxTypeType? CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			shouldAdd = false;
			return null;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00026258 File Offset: 0x00024458
		internal static bool TryGetKey(string emailAddress, string orgUnitName, out string key)
		{
			key = null;
			if (string.IsNullOrEmpty(orgUnitName) || string.IsNullOrEmpty(emailAddress))
			{
				return false;
			}
			string format = "{0}:{1}";
			key = string.Format(format, orgUnitName, emailAddress);
			return true;
		}

		// Token: 0x04000700 RID: 1792
		private static MailboxTypeCache instance;

		// Token: 0x04000701 RID: 1793
		private static object lockObject = new object();

		// Token: 0x04000702 RID: 1794
		private long cacheSize;
	}
}
