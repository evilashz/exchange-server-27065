using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000013 RID: 19
	internal static class ADUserCache
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00009EBC File Offset: 0x000080BC
		internal static void Start()
		{
			TimeSpan adcacheRefreshInterval = GlobalSettings.ADCacheRefreshInterval;
			ADUserCache.activeDirectoryUpdatingThread = new Timer(new TimerCallback(ADUserCache.UpdateADCache), null, adcacheRefreshInterval, adcacheRefreshInterval);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009EE8 File Offset: 0x000080E8
		internal static void Stop()
		{
			if (ADUserCache.activeDirectoryUpdatingThread != null)
			{
				ADUserCache.activeDirectoryUpdatingThread.Dispose();
				ADUserCache.activeDirectoryUpdatingThread = null;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009F3C File Offset: 0x0000813C
		internal static ActiveSyncMiniRecipient TryGetADUser(IAirSyncUser user, ProtocolLogger logger)
		{
			ADUserCache.<>c__DisplayClass1 CS$<>8__locals1 = new ADUserCache.<>c__DisplayClass1();
			AirSyncDiagnostics.Assert(user != null, "IAirSyncUser cannot be null in GetADUser", new object[0]);
			CS$<>8__locals1.sid2 = user.ClientSecurityContextWrapper.UserSid;
			ActiveSyncMiniRecipient activeSyncMiniRecipient = null;
			if (!GlobalSettings.DisableCaching && ADUserCache.TryGetADUser(CS$<>8__locals1.sid2.Value, out activeSyncMiniRecipient))
			{
				return activeSyncMiniRecipient;
			}
			string value = user.Name;
			if (GlobalSettings.IsWindowsLiveIDEnabled)
			{
				value = user.WindowsLiveId;
			}
			if (string.IsNullOrEmpty(value))
			{
				logger.SetValue(ProtocolLoggerData.Error, "BadAuthUsername");
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidCombinationOfIDs, null, false);
			}
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "ADUserCache.TryGetADUser:: user IdentityType {0}", user.Identity.GetType().FullName);
			ADSessionSettings sessionSettings = ADUserCache.GetSessionSettings(GlobalSettings.IsMultiTenancyEnabled ? (user.Identity as LiveIDIdentity).UserOrganizationId : OrganizationId.ForestWideOrgId, logger);
			IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, 0, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 124, "TryGetADUser", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\ADUserCache.cs");
			ActiveSyncMiniRecipient recipient = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				recipient = recipientSession.FindMiniRecipientBySid<ActiveSyncMiniRecipient>(CS$<>8__locals1.sid2, ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>().AllProperties);
			});
			logger.SetValue(ProtocolLoggerData.DomainController, recipientSession.LastUsedDc);
			if (recipient == null)
			{
				AirSyncDiagnostics.TraceDebug<SecurityIdentifier>(ExTraceGlobals.RequestsTracer, null, "ADUserCache.TryGetADUserFromSid can't find ActiveSyncMiniRecipient for sid {0}", CS$<>8__locals1.sid2);
				return null;
			}
			activeSyncMiniRecipient = ((recipient.RecipientType == RecipientType.UserMailbox || recipient.RecipientType == RecipientType.MailUser) ? recipient : null);
			if (activeSyncMiniRecipient == null)
			{
				AirSyncDiagnostics.TraceDebug<RecipientType>(ExTraceGlobals.RequestsTracer, null, "ADUserCache.TryGetADUserFromSid recipient type is {0} when expected UserMailbox/MailUser", recipient.RecipientType);
				return null;
			}
			if (!GlobalSettings.DisableCaching)
			{
				ADUserCache.AddADUserToCache(CS$<>8__locals1.sid2.Value, activeSyncMiniRecipient);
			}
			return activeSyncMiniRecipient;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A11C File Offset: 0x0000831C
		internal static ADSessionSettings GetSessionSettings(OrganizationId orgId, ProtocolLogger logger)
		{
			AirSyncDiagnostics.Assert(orgId != null);
			if (GlobalSettings.IsMultiTenancyEnabled)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "organizationID :{0}", orgId.ToString());
				ADSessionSettings result = null;
				ADNotificationAdapter.RunADOperation(delegate()
				{
					result = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId);
				});
				return result;
			}
			return ADSessionSettings.FromRootOrgScopeSet();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A19C File Offset: 0x0000839C
		private static void UpdateADCache(object unused)
		{
			ADUserCache.activeDirectoryCacheLock.AcquireWriterLock(-1);
			try
			{
				ADUserCache.cache0 = ADUserCache.cache1;
				ADUserCache.cache1 = ADUserCache.cache2;
				ADUserCache.cache2 = new Dictionary<string, ActiveSyncMiniRecipient>(100);
			}
			finally
			{
				ADUserCache.activeDirectoryCacheLock.ReleaseWriterLock();
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private static bool TryGetADUser(string key, out ActiveSyncMiniRecipient activeDirectoryUser)
		{
			ADUserCache.activeDirectoryCacheLock.AcquireReaderLock(-1);
			bool result;
			try
			{
				if (ADUserCache.cache2.TryGetValue(key, out activeDirectoryUser))
				{
					result = true;
				}
				else if (ADUserCache.cache1.TryGetValue(key, out activeDirectoryUser))
				{
					result = true;
				}
				else
				{
					result = ADUserCache.cache0.TryGetValue(key, out activeDirectoryUser);
				}
			}
			finally
			{
				ADUserCache.activeDirectoryCacheLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A25C File Offset: 0x0000845C
		private static void AddADUserToCache(string key, ActiveSyncMiniRecipient activeDirectoryUser)
		{
			ADUserCache.activeDirectoryCacheLock.AcquireWriterLock(-1);
			try
			{
				if (!ADUserCache.cache2.ContainsKey(key))
				{
					ADUserCache.cache2.Add(key, activeDirectoryUser);
				}
			}
			finally
			{
				ADUserCache.activeDirectoryCacheLock.ReleaseWriterLock();
			}
		}

		// Token: 0x040001CB RID: 459
		private static Dictionary<string, ActiveSyncMiniRecipient> cache0 = new Dictionary<string, ActiveSyncMiniRecipient>(100);

		// Token: 0x040001CC RID: 460
		private static Dictionary<string, ActiveSyncMiniRecipient> cache1 = new Dictionary<string, ActiveSyncMiniRecipient>(100);

		// Token: 0x040001CD RID: 461
		private static Dictionary<string, ActiveSyncMiniRecipient> cache2 = new Dictionary<string, ActiveSyncMiniRecipient>(100);

		// Token: 0x040001CE RID: 462
		private static FastReaderWriterLock activeDirectoryCacheLock = new FastReaderWriterLock();

		// Token: 0x040001CF RID: 463
		private static Timer activeDirectoryUpdatingThread;
	}
}
