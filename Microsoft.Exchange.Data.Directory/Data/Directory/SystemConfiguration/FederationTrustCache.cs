using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000651 RID: 1617
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FederationTrustCache
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06004BE2 RID: 19426 RVA: 0x001181CC File Offset: 0x001163CC
		// (remove) Token: 0x06004BE3 RID: 19427 RVA: 0x00118200 File Offset: 0x00116400
		public static event FederationTrustCache.FederationTrustChangeHandler Change;

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00118233 File Offset: 0x00116433
		public static IEnumerable<FederationTrust> GetFederationTrusts()
		{
			FederationTrustCache.InitializeIfNeeded();
			return new List<FederationTrust>(FederationTrustCache.dictionaryByADObjectId.Values);
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0011824C File Offset: 0x0011644C
		public static FederationTrust GetFederationTrust(ADObjectId id)
		{
			FederationTrustCache.InitializeIfNeeded();
			FederationTrust result;
			if (FederationTrustCache.dictionaryByADObjectId.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x00118270 File Offset: 0x00116470
		public static FederationTrust GetFederationTrust(string name)
		{
			FederationTrustCache.InitializeIfNeeded();
			FederationTrust result;
			if (FederationTrustCache.dictionaryByCommonName.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00118294 File Offset: 0x00116494
		internal static void Initialize()
		{
			FederationTrustCache.LoadFederationTrust();
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0011829C File Offset: 0x0011649C
		private static void InitializeIfNeeded()
		{
			if (!FederationTrustCache.IsInitialized())
			{
				lock (FederationTrustCache.locker)
				{
					if (!FederationTrustCache.IsInitialized())
					{
						FederationTrustCache.timeout = DateTime.UtcNow + FederationTrustCache.ExpirationTime;
						try
						{
							FederationTrustCache.LoadFederationTrust();
							FederationTrustCache.SubscribeForNotifications();
						}
						catch (LocalizedException arg)
						{
							FederationTrustCache.Tracer.TraceError<LocalizedException>(0L, "FederationTrustCache: Unable to initialize due exception: {0}", arg);
						}
						if (FederationTrustCache.notification != null)
						{
							FederationTrustCache.timeout = DateTime.MaxValue;
						}
						FederationTrustCache.initialized = true;
					}
				}
			}
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0011833C File Offset: 0x0011653C
		private static bool IsInitialized()
		{
			return !(DateTime.UtcNow > FederationTrustCache.timeout) && FederationTrustCache.initialized;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00118358 File Offset: 0x00116558
		private static void LoadFederationTrust()
		{
			FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: Searching for federation trust configuration in AD");
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 206, "LoadFederationTrust", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\FederationTrustCache.cs");
			FederationTrust[] array = tenantOrTopologyConfigurationSession.Find<FederationTrust>(null, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: no federation trust has been setup");
				return;
			}
			FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: loading {0} FederationTrust objects.");
			Dictionary<ADObjectId, FederationTrust> dictionary = new Dictionary<ADObjectId, FederationTrust>(1);
			Dictionary<string, FederationTrust> dictionary2 = new Dictionary<string, FederationTrust>(1);
			foreach (FederationTrust federationTrust in array)
			{
				dictionary.Add(federationTrust.Id, federationTrust);
				dictionary2.Add(federationTrust.Name, federationTrust);
			}
			FederationTrustCache.dictionaryByADObjectId = dictionary;
			FederationTrustCache.dictionaryByCommonName = dictionary2;
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x00118424 File Offset: 0x00116624
		private static void SubscribeForNotifications()
		{
			if (FederationTrustCache.notification != null)
			{
				return;
			}
			FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: getting federation trust container in AD.");
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 260, "SubscribeForNotifications", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\FederationTrustCache.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Federation Trusts");
			FederationTrustCache.Tracer.TraceDebug<ADObjectId>(0L, "FederationTrustCache: found federation trust configuration object in AD. {0}", childId);
			FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: registering for configuration changes in AD");
			FederationTrustCache.notification = ADNotificationAdapter.RegisterChangeNotification<FederationTrust>(childId, new ADNotificationCallback(FederationTrustCache.NotificationHandler));
			FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: successfully registered for federation trust configuration changes in AD");
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x001184C8 File Offset: 0x001166C8
		private static void NotificationHandler(ADNotificationEventArgs args)
		{
			FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: changes detected in configuration in AD.");
			try
			{
				FederationTrustCache.LoadFederationTrust();
			}
			catch (LocalizedException arg)
			{
				FederationTrustCache.Tracer.TraceError<LocalizedException>(0L, "FederationTrustCache: failed to read federation trust from AD due exception: {0}", arg);
				return;
			}
			if (FederationTrustCache.Change != null)
			{
				FederationTrustCache.Tracer.TraceDebug(0L, "FederationTrustCache: notifying subscribers of change.");
				FederationTrustCache.Change();
			}
		}

		// Token: 0x04003413 RID: 13331
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x04003414 RID: 13332
		private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04003415 RID: 13333
		private static DateTime timeout;

		// Token: 0x04003416 RID: 13334
		private static bool initialized;

		// Token: 0x04003417 RID: 13335
		private static object locker = new object();

		// Token: 0x04003418 RID: 13336
		private static ADNotificationRequestCookie notification;

		// Token: 0x04003419 RID: 13337
		private static Dictionary<ADObjectId, FederationTrust> dictionaryByADObjectId = new Dictionary<ADObjectId, FederationTrust>();

		// Token: 0x0400341A RID: 13338
		private static Dictionary<string, FederationTrust> dictionaryByCommonName = new Dictionary<string, FederationTrust>();

		// Token: 0x02000652 RID: 1618
		// (Invoke) Token: 0x06004BEF RID: 19439
		public delegate void FederationTrustChangeHandler();
	}
}
