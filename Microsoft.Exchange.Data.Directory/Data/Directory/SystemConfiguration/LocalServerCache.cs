using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200064D RID: 1613
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class LocalServerCache
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06004BCF RID: 19407 RVA: 0x00117EBC File Offset: 0x001160BC
		// (remove) Token: 0x06004BD0 RID: 19408 RVA: 0x00117EF0 File Offset: 0x001160F0
		public static event LocalServerCache.LocalServerChangeHandler Change;

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x00117F23 File Offset: 0x00116123
		public static string LocalServerFqdn
		{
			get
			{
				if (LocalServerCache.localServerFqdn == null)
				{
					LocalServerCache.localServerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
				}
				return LocalServerCache.localServerFqdn;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x00117F3C File Offset: 0x0011613C
		public static Server LocalServer
		{
			get
			{
				LocalServerCache.InitializeIfNeeded();
				return LocalServerCache.localServer;
			}
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x00117F48 File Offset: 0x00116148
		internal static void Initialize()
		{
			LocalServerCache.ReadLocalServer();
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00117F50 File Offset: 0x00116150
		private static void InitializeIfNeeded()
		{
			if (!LocalServerCache.IsInitialized())
			{
				lock (LocalServerCache.locker)
				{
					if (!LocalServerCache.IsInitialized())
					{
						LocalServerCache.timeout = DateTime.UtcNow + LocalServerCache.ExpirationTime;
						try
						{
							LocalServerCache.ReadLocalServer();
							LocalServerCache.SubscribeForNotifications();
						}
						catch (LocalizedException arg)
						{
							LocalServerCache.Tracer.TraceError<LocalizedException>(0L, "LocalServerCache: unable to initialize due exception: {0}", arg);
						}
						if (LocalServerCache.notification != null)
						{
							LocalServerCache.timeout = DateTime.MaxValue;
						}
						LocalServerCache.initialized = true;
					}
				}
			}
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00117FF0 File Offset: 0x001161F0
		private static bool IsInitialized()
		{
			return !(DateTime.UtcNow > LocalServerCache.timeout) && LocalServerCache.initialized;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0011800A File Offset: 0x0011620A
		private static void SubscribeForNotifications()
		{
			if (LocalServerCache.notification == null && LocalServerCache.localServer != null)
			{
				LocalServerCache.notification = ADNotificationAdapter.RegisterChangeNotification<Server>(LocalServerCache.localServer.Id, new ADNotificationCallback(LocalServerCache.NotificationHandler));
			}
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0011803C File Offset: 0x0011623C
		private static void NotificationHandler(ADNotificationEventArgs args)
		{
			LocalServerCache.Tracer.TraceDebug(0L, "LocalServerCache: local server object changed");
			try
			{
				LocalServerCache.ReadLocalServer();
			}
			catch (LocalizedException arg)
			{
				LocalServerCache.Tracer.TraceError<LocalizedException>(0L, "LocalServerCache: failed to read local server object from AD due exception: {0}", arg);
				return;
			}
			if (LocalServerCache.Change != null)
			{
				LocalServerCache.Tracer.TraceDebug(0L, "LocalServerCache: notifying subscribers of change.");
				LocalServerCache.Change(LocalServerCache.localServer);
			}
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x001180B0 File Offset: 0x001162B0
		private static void ReadLocalServer()
		{
			LocalServerCache.Tracer.TraceDebug(0L, "LocalServerCache: reading local server object");
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 208, "ReadLocalServer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\LocalServerCache.cs");
			LocalServerCache.localServer = topologyConfigurationSession.ReadLocalServer();
		}

		// Token: 0x04003404 RID: 13316
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x04003405 RID: 13317
		private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04003406 RID: 13318
		private static DateTime timeout;

		// Token: 0x04003407 RID: 13319
		private static bool initialized;

		// Token: 0x04003408 RID: 13320
		private static object locker = new object();

		// Token: 0x04003409 RID: 13321
		private static ADNotificationRequestCookie notification;

		// Token: 0x0400340A RID: 13322
		private static string localServerFqdn;

		// Token: 0x0400340B RID: 13323
		private static Server localServer;

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x06004BDB RID: 19419
		public delegate void LocalServerChangeHandler(Server server);
	}
}
