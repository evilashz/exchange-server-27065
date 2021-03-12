using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000003 RID: 3
	internal static class DirectTrust
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020EB File Offset: 0x000002EB
		public static void Load()
		{
			DirectTrust.reloadTimer = new Timer(new TimerCallback(DirectTrust.UpdateDirectTrustCache), null, -1, -1);
			DirectTrust.UpdateDirectTrustCache(null);
			DirectTrust.RegisterDirectTrustMonitoring();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002111 File Offset: 0x00000311
		public static void Unload()
		{
			if (DirectTrust.serverRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(DirectTrust.serverRequestCookie);
			}
			if (DirectTrust.reloadTimer != null)
			{
				DirectTrust.reloadTimer.Dispose();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002174 File Offset: 0x00000374
		private static void RegisterDirectTrustMonitoring()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId childId = DirectTrust.adSession.GetOrgContainerId().GetChildId("Administrative Groups");
				DirectTrust.serverRequestCookie = ADNotificationAdapter.RegisterChangeNotification<Server>(childId, new ADNotificationCallback(DirectTrust.ServerNotificationDispatch), null);
			}, 3);
			if (adoperationResult.Exception != null)
			{
				throw adoperationResult.Exception;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
		private static void ServerNotificationDispatch(ADNotificationEventArgs args)
		{
			try
			{
				if (Interlocked.Increment(ref DirectTrust.notificationHandlerCount) == 1)
				{
					try
					{
						lock (DirectTrust.syncObj)
						{
							if (DirectTrust.notificationProcessingScheduled)
							{
								ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Skip because notification processing already scheduled");
							}
							else
							{
								DirectTrust.reloadTimer.Change((int)DirectTrust.notificationDelay.TotalMilliseconds, -1);
								DirectTrust.notificationProcessingScheduled = true;
								ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Delayed notification processing scheduled");
							}
						}
					}
					catch (ObjectDisposedException)
					{
						DirectTrust.notificationProcessingScheduled = false;
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref DirectTrust.notificationHandlerCount);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002278 File Offset: 0x00000478
		internal static SecurityIdentifier MapCertToSecurityIdentifier(X509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return DirectTrust.MapCertToSecurityIdentifier(new X509Certificate2Wrapper(certificate));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002290 File Offset: 0x00000490
		internal static SecurityIdentifier MapCertToSecurityIdentifier(IX509Certificate2 certificate)
		{
			byte[] publicKey = certificate.GetPublicKey();
			MiniServer miniServer;
			if (!DirectTrust.directTrustCache.TryGetValue(publicKey, out miniServer))
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Can't find certificate {0} in direct trust cache", certificate.Subject);
				return new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);
			}
			if ((bool)miniServer[ServerSchema.IsHubTransportServer])
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Certificate {0} maps to Bridgehead {1}", certificate.Subject, miniServer.Name);
				return WellKnownSids.HubTransportServers;
			}
			if ((bool)miniServer[ServerSchema.IsEdgeServer])
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Certificate {0} maps to Gateway {1}", certificate.Subject, miniServer.Name);
				return WellKnownSids.EdgeTransportServers;
			}
			ExTraceGlobals.GeneralTracer.TraceError<string, string>(0L, "Certificate {0} maps to unknown server {1}. Default to anonymous", certificate.Subject, miniServer.Name);
			return new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002424 File Offset: 0x00000624
		private static void UpdateDirectTrustCache(object state)
		{
			lock (DirectTrust.syncObj)
			{
				if (DirectTrust.isReloadingTopology)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Skip because another thread is already doing reloading the topology");
					return;
				}
				DirectTrust.isReloadingTopology = true;
			}
			try
			{
				Dictionary<byte[], MiniServer> newCache = new Dictionary<byte[], MiniServer>(ArrayComparer<byte>.Comparer);
				bool certificateValid = true;
				bool flag2 = ADNotificationAdapter.TryReadConfigurationPaged<MiniServer>(() => DirectTrust.adSession.FindAllServersWithVersionNumber(Server.E2007MinVersion, DirectTrust.hubOrEdgeRoleFilter, DirectTrust.serverProperties), delegate(MiniServer server)
				{
					byte[] array = (byte[])server[ServerSchema.InternalTransportCertificate];
					if (array != null)
					{
						try
						{
							X509Certificate2 x509Certificate = new X509Certificate2(array);
							byte[] publicKey = x509Certificate.GetPublicKey();
							if (!newCache.ContainsKey(publicKey))
							{
								ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Associate certificate {0} with server {1}, and add it to direct trust cache", x509Certificate.Subject, server.Name);
								newCache.Add(publicKey, server);
							}
							return;
						}
						catch (CryptographicException)
						{
							certificateValid = false;
							return;
						}
					}
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Skip server {0} because its InternalTransportCertificate is null", server.Name);
				});
				if (flag2 && certificateValid)
				{
					Dictionary<byte[], MiniServer> dictionary = Interlocked.Exchange<Dictionary<byte[], MiniServer>>(ref DirectTrust.directTrustCache, newCache);
					int num = (dictionary != null) ? (newCache.Count - dictionary.Count) : newCache.Count;
					if (num > 0)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug<int>(0L, "There are {0} Exchange servers added to the cache", num);
					}
					else
					{
						ExTraceGlobals.GeneralTracer.TraceDebug<int>(0L, "There are {0} Exchange servers removed from the cache", -num);
					}
				}
				else
				{
					ExTraceGlobals.GeneralTracer.TraceError(0L, "An AD error is preventing us from loading all Edge and Hub servers");
				}
			}
			finally
			{
				lock (DirectTrust.syncObj)
				{
					DirectTrust.reloadTimer.Change((int)DirectTrust.updateInterval.TotalMilliseconds, -1);
					DirectTrust.notificationProcessingScheduled = false;
					DirectTrust.isReloadingTopology = false;
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Delayed notification processing reset");
				}
			}
		}

		// Token: 0x04000002 RID: 2
		private static readonly PropertyDefinition[] serverProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ServerSchema.InternalTransportCertificate,
			ServerSchema.CurrentServerRole
		};

		// Token: 0x04000003 RID: 3
		private static readonly OrFilter hubOrEdgeRoleFilter = new OrFilter(new QueryFilter[]
		{
			new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
			new BitMaskAndFilter(ServerSchema.CurrentServerRole, 64UL)
		});

		// Token: 0x04000004 RID: 4
		private static readonly object syncObj = new object();

		// Token: 0x04000005 RID: 5
		private static readonly TimeSpan notificationDelay = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000006 RID: 6
		private static readonly TimeSpan updateInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000007 RID: 7
		private static Timer reloadTimer;

		// Token: 0x04000008 RID: 8
		private static int notificationHandlerCount;

		// Token: 0x04000009 RID: 9
		private static bool notificationProcessingScheduled;

		// Token: 0x0400000A RID: 10
		private static bool isReloadingTopology;

		// Token: 0x0400000B RID: 11
		private static readonly ITopologyConfigurationSession adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 84, "adSession", "f:\\15.00.1497\\sources\\dev\\MessageSecurity\\src\\Common\\Core\\DirectTrust.cs");

		// Token: 0x0400000C RID: 12
		private static Dictionary<byte[], MiniServer> directTrustCache = new Dictionary<byte[], MiniServer>(ArrayComparer<byte>.Comparer);

		// Token: 0x0400000D RID: 13
		private static ADNotificationRequestCookie serverRequestCookie;
	}
}
