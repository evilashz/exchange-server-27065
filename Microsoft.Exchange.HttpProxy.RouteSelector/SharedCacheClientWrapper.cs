using System;
using System.Diagnostics;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x0200000D RID: 13
	public class SharedCacheClientWrapper : ISharedCacheClient, ISharedCache
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000030C8 File Offset: 0x000012C8
		public SharedCacheClientWrapper(SharedCacheClient client)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			this.client = client;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030E8 File Offset: 0x000012E8
		bool ISharedCache.TryGet(string key, out byte[] returnBytes, IRoutingDiagnostics diagnostics)
		{
			bool result;
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				string text;
				bool flag;
				try
				{
					stopwatch.Start();
					flag = this.client.TryGet(key, out returnBytes, out text);
				}
				finally
				{
					stopwatch.Stop();
					diagnostics.AddSharedCacheLatency(TimeSpan.FromMilliseconds((double)stopwatch.ElapsedMilliseconds));
				}
				diagnostics.AddDiagnosticText(text);
				result = flag;
			}
			catch (CacheClientException innerException)
			{
				throw new SharedCacheException("SharedCacheClient.TryGet(byte[]) failed", innerException);
			}
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003164 File Offset: 0x00001364
		bool ISharedCache.TryGet<T>(string key, out T value, IRoutingDiagnostics diagnostics)
		{
			bool result;
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				string text;
				bool flag;
				try
				{
					stopwatch.Start();
					flag = this.client.TryGet<T>(key, out value, out text);
				}
				finally
				{
					stopwatch.Stop();
					diagnostics.AddSharedCacheLatency(TimeSpan.FromMilliseconds((double)stopwatch.ElapsedMilliseconds));
				}
				diagnostics.AddDiagnosticText(text);
				result = flag;
			}
			catch (CacheClientException innerException)
			{
				throw new SharedCacheException("SharedCacheClient.TryGet(T) failed", innerException);
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000031E0 File Offset: 0x000013E0
		string ISharedCache.GetSharedCacheKeyFromRoutingKey(IRoutingKey key)
		{
			string text = string.Empty;
			switch (key.RoutingItemType)
			{
			case RoutingItemType.ArchiveSmtp:
			{
				ArchiveSmtpRoutingKey archiveSmtpRoutingKey = key as ArchiveSmtpRoutingKey;
				text = SharedCacheClientWrapper.MakeCacheKey(SharedCacheClientWrapper.AnchorSource.Smtp, archiveSmtpRoutingKey.SmtpAddress);
				text += "_Archive";
				break;
			}
			case RoutingItemType.DatabaseGuid:
			{
				DatabaseGuidRoutingKey databaseGuidRoutingKey = key as DatabaseGuidRoutingKey;
				text = SharedCacheClientWrapper.MakeCacheKey(SharedCacheClientWrapper.AnchorSource.DatabaseGuid, databaseGuidRoutingKey.DatabaseGuid);
				break;
			}
			case RoutingItemType.MailboxGuid:
			{
				MailboxGuidRoutingKey mailboxGuidRoutingKey = key as MailboxGuidRoutingKey;
				text = SharedCacheClientWrapper.MakeCacheKey(SharedCacheClientWrapper.AnchorSource.MailboxGuid, mailboxGuidRoutingKey.MailboxGuid);
				break;
			}
			case RoutingItemType.Smtp:
			{
				SmtpRoutingKey smtpRoutingKey = key as SmtpRoutingKey;
				text = SharedCacheClientWrapper.MakeCacheKey(SharedCacheClientWrapper.AnchorSource.Smtp, smtpRoutingKey.SmtpAddress);
				break;
			}
			case RoutingItemType.ExternalDirectoryObjectId:
			{
				ExternalDirectoryObjectIdRoutingKey externalDirectoryObjectIdRoutingKey = key as ExternalDirectoryObjectIdRoutingKey;
				text = SharedCacheClientWrapper.MakeCacheKey(SharedCacheClientWrapper.AnchorSource.ExternalDirectoryObjectId, externalDirectoryObjectIdRoutingKey.UserGuid);
				break;
			}
			case RoutingItemType.LiveIdMemberName:
			{
				LiveIdMemberNameRoutingKey liveIdMemberNameRoutingKey = key as LiveIdMemberNameRoutingKey;
				text = SharedCacheClientWrapper.MakeCacheKey(SharedCacheClientWrapper.AnchorSource.LiveIdMemberName, liveIdMemberNameRoutingKey.LiveIdMemberName);
				break;
			}
			}
			return text;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000032E6 File Offset: 0x000014E6
		bool ISharedCacheClient.TryInsert(string key, byte[] dataBytes, DateTime cacheExpiry, out string diagInfo)
		{
			return this.client.TryInsert(key, dataBytes, cacheExpiry, out diagInfo);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000032F8 File Offset: 0x000014F8
		bool ISharedCacheClient.TryInsert(string key, ISharedCacheEntry value, DateTime valueTimeStamp, out string diagInfo)
		{
			return this.client.TryInsert(key, value, valueTimeStamp, out diagInfo);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000330C File Offset: 0x0000150C
		private static string MakeCacheKey(SharedCacheClientWrapper.AnchorSource source, object obj)
		{
			string text = source.ToString() + "~" + obj.ToString();
			return text.Replace(" ", "_");
		}

		// Token: 0x0400001C RID: 28
		private readonly SharedCacheClient client;

		// Token: 0x0200000E RID: 14
		private enum AnchorSource
		{
			// Token: 0x0400001E RID: 30
			Smtp,
			// Token: 0x0400001F RID: 31
			Sid,
			// Token: 0x04000020 RID: 32
			Domain,
			// Token: 0x04000021 RID: 33
			DomainAndVersion,
			// Token: 0x04000022 RID: 34
			OrganizationId,
			// Token: 0x04000023 RID: 35
			MailboxGuid,
			// Token: 0x04000024 RID: 36
			DatabaseName,
			// Token: 0x04000025 RID: 37
			DatabaseGuid,
			// Token: 0x04000026 RID: 38
			UserADRawEntry,
			// Token: 0x04000027 RID: 39
			ServerInfo,
			// Token: 0x04000028 RID: 40
			ServerVersion,
			// Token: 0x04000029 RID: 41
			Url,
			// Token: 0x0400002A RID: 42
			Anonymous,
			// Token: 0x0400002B RID: 43
			GenericAnchorHint,
			// Token: 0x0400002C RID: 44
			Puid,
			// Token: 0x0400002D RID: 45
			ExternalDirectoryObjectId,
			// Token: 0x0400002E RID: 46
			OAuthActAsUser,
			// Token: 0x0400002F RID: 47
			LiveIdMemberName
		}
	}
}
