using System;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProtocolAnalysis;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000006 RID: 6
	internal class ConfigurationAccess
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000A RID: 10 RVA: 0x0000212D File Offset: 0x0000032D
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x00002151 File Offset: 0x00000351
		public static event HandleConfigChange HandleConfigChangeEvent
		{
			add
			{
				ConfigurationAccess.OnHandleConfigChangeEvent += value;
				CommonUtils.RegisterConfigurationChangeHandlers("Sender Reputation", new ADOperation(ConfigurationAccess.RegisterConfigurationChangeHandlers), ExTraceGlobals.FactoryTracer, null);
			}
			remove
			{
				ConfigurationAccess.OnHandleConfigChangeEvent -= value;
				ConfigurationAccess.UnregisterConfigurationChangeHandlers();
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x00002194 File Offset: 0x00000394
		public static event HandleConfigChange OnHandleConfigChangeEvent;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021C7 File Offset: 0x000003C7
		public static SenderReputationConfig ConfigSettings
		{
			get
			{
				if (ConfigurationAccess.settings == null)
				{
					ConfigurationAccess.LoadConfiguration(true);
				}
				return ConfigurationAccess.settings;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		private static void LoadConfiguration(bool onStartup)
		{
			SenderReputationConfig senderReputationConfig;
			ADOperationResult adoperationResult;
			if (ADNotificationAdapter.TryReadConfiguration<SenderReputationConfig>(() => DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 172, "LoadConfiguration", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\Sts\\DbAccess\\DataRowAccess.cs").FindSingletonConfigurationObject<SenderReputationConfig>(), out senderReputationConfig, out adoperationResult))
			{
				ConfigurationAccess.settings = senderReputationConfig;
				return;
			}
			CommonUtils.FailedToReadConfiguration("Sender Reputation", onStartup, adoperationResult.Exception, ExTraceGlobals.FactoryTracer, ConfigurationAccess.eventLogger, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000225C File Offset: 0x0000045C
		private static void RegisterConfigurationChangeHandlers()
		{
			if (ConfigurationAccess.notificationRequestCookie != null)
			{
				return;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 202, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\Sts\\DbAccess\\DataRowAccess.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
			ADObjectId childId2 = childId.GetChildId("Message Hygiene");
			ConfigurationAccess.notificationRequestCookie = ADNotificationAdapter.RegisterChangeNotification<SenderReputationConfig>(childId2, new ADNotificationCallback(ConfigurationAccess.Configure));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C7 File Offset: 0x000004C7
		private static void UnregisterConfigurationChangeHandlers()
		{
			if (ConfigurationAccess.notificationRequestCookie == null)
			{
				return;
			}
			ADNotificationAdapter.UnregisterChangeNotification(ConfigurationAccess.notificationRequestCookie);
			ConfigurationAccess.notificationRequestCookie = null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E1 File Offset: 0x000004E1
		public static void Unsubscribe()
		{
			ConfigurationAccess.UnregisterConfigurationChangeHandlers();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E8 File Offset: 0x000004E8
		private static void Configure(ADNotificationEventArgs args)
		{
			ConfigurationAccess.LoadConfiguration(false);
			if (ConfigurationAccess.OnHandleConfigChangeEvent != null)
			{
				ConfigurationAccess.OnHandleConfigChangeEvent(null, null);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002304 File Offset: 0x00000504
		public static void NotifySrlConfigChanged(PropertyBag newFields)
		{
			ConfigChangedEventArgs e = new ConfigChangedEventArgs(newFields);
			if (ConfigurationAccess.OnHandleConfigChangeEvent != null)
			{
				ConfigurationAccess.OnHandleConfigChangeEvent(null, e);
			}
		}

		// Token: 0x04000002 RID: 2
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.FactoryTracer.Category, "MSExchange Antispam");

		// Token: 0x04000003 RID: 3
		private static SenderReputationConfig settings = null;

		// Token: 0x04000005 RID: 5
		private static ADNotificationRequestCookie notificationRequestCookie = null;
	}
}
