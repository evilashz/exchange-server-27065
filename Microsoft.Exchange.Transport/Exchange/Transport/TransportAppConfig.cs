using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000298 RID: 664
	internal sealed class TransportAppConfig : ITransportAppConfig
	{
		// Token: 0x06001C5A RID: 7258 RVA: 0x00073193 File Offset: 0x00071393
		private TransportAppConfig()
		{
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0007319B File Offset: 0x0007139B
		public TransportAppConfig.ResourceManagerConfig ResourceManager
		{
			get
			{
				return this.resourceManagerConfig;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x000731A3 File Offset: 0x000713A3
		public TransportAppConfig.JetDatabaseConfig JetDatabase
		{
			get
			{
				return this.jetDatabaseConfig;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x000731AB File Offset: 0x000713AB
		public TransportAppConfig.DumpsterConfig Dumpster
		{
			get
			{
				return this.dumpsterConfig;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x000731B3 File Offset: 0x000713B3
		public TransportAppConfig.ShadowRedundancyConfig ShadowRedundancy
		{
			get
			{
				return this.shadowRedundancyConfig;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x000731BB File Offset: 0x000713BB
		public TransportAppConfig.RemoteDeliveryConfig RemoteDelivery
		{
			get
			{
				return this.remoteDeliveryConfig;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x000731C3 File Offset: 0x000713C3
		public TransportAppConfig.MapiSubmissionConfig MapiSubmission
		{
			get
			{
				return this.mapiSubmissionConfig;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x000731CB File Offset: 0x000713CB
		public TransportAppConfig.ResolverConfig Resolver
		{
			get
			{
				return this.resolverConfig;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x000731D3 File Offset: 0x000713D3
		public TransportAppConfig.RoutingConfig Routing
		{
			get
			{
				return this.routingConfig;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x000731DB File Offset: 0x000713DB
		public TransportAppConfig.ContentConversionConfig ContentConversion
		{
			get
			{
				return this.contentConversionConfig;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x000731E3 File Offset: 0x000713E3
		public TransportAppConfig.IPFilteringDatabaseConfig IPFilteringDatabase
		{
			get
			{
				return this.ipFilteringDatabaseConfig;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x000731EB File Offset: 0x000713EB
		public TransportAppConfig.IMessageResubmissionConfig MessageResubmission
		{
			get
			{
				return this.messageResubmissionConfig;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x000731F3 File Offset: 0x000713F3
		public TransportAppConfig.QueueDatabaseConfig QueueDatabase
		{
			get
			{
				return this.queueDatabaseConfig;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x000731FB File Offset: 0x000713FB
		public TransportAppConfig.WorkerProcessConfig WorkerProcess
		{
			get
			{
				return this.workerProcessConfig;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00073203 File Offset: 0x00071403
		public TransportAppConfig.LatencyTrackerConfig LatencyTracker
		{
			get
			{
				return this.latencyTrackerConfig;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0007320B File Offset: 0x0007140B
		public TransportAppConfig.RecipientValidatorConfig RecipientValidtor
		{
			get
			{
				return this.recipientValidatorConfig;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00073213 File Offset: 0x00071413
		public TransportAppConfig.PerTenantCacheConfig PerTenantCache
		{
			get
			{
				return this.perTenantCacheConfig;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0007321B File Offset: 0x0007141B
		public TransportAppConfig.MessageThrottlingConfiguration MessageThrottlingConfig
		{
			get
			{
				return this.messageThrottlingConfig;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00073223 File Offset: 0x00071423
		public TransportAppConfig.SMTPOutConnectionCacheConfig ConnectionCacheConfig
		{
			get
			{
				return this.connectionCacheConfig;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0007322B File Offset: 0x0007142B
		public TransportAppConfig.IsMemberOfResolverConfiguration TransportIsMemberOfResolverConfig
		{
			get
			{
				return this.transportIsMemberOfResolverConfig;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00073233 File Offset: 0x00071433
		public TransportAppConfig.IsMemberOfResolverConfiguration MailboxRulesIsMemberOfResolverConfig
		{
			get
			{
				return this.mailboxRulesIsMemberOfResolverConfig;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0007323B File Offset: 0x0007143B
		public TransportAppConfig.SmtpAvailabilityConfig SmtpAvailabilityConfiguration
		{
			get
			{
				return this.smtpAvailabilityConfig;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00073243 File Offset: 0x00071443
		public TransportAppConfig.SmtpDataConfig SmtpDataConfiguration
		{
			get
			{
				return this.smtpDataConfig;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0007324B File Offset: 0x0007144B
		public TransportAppConfig.SmtpMailCommandConfig SmtpMailCommandConfiguration
		{
			get
			{
				return this.smtpMailCommandConfig;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00073253 File Offset: 0x00071453
		public TransportAppConfig.MessageContextBlobConfig MessageContextBlobConfiguration
		{
			get
			{
				return this.messageContextBlobConfig;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0007325B File Offset: 0x0007145B
		public TransportAppConfig.SmtpReceiveConfig SmtpReceiveConfiguration
		{
			get
			{
				return this.smtpReceiveConfig;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00073263 File Offset: 0x00071463
		public TransportAppConfig.SmtpSendConfig SmtpSendConfiguration
		{
			get
			{
				return this.smtpSendConfig;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0007326B File Offset: 0x0007146B
		public TransportAppConfig.SmtpProxyConfig SmtpProxyConfiguration
		{
			get
			{
				return this.smtpProxyConfig;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00073273 File Offset: 0x00071473
		public TransportAppConfig.SmtpInboundProxyConfig SmtpInboundProxyConfiguration
		{
			get
			{
				return this.smtpInboundProxyConfig;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0007327B File Offset: 0x0007147B
		public TransportAppConfig.SmtpOutboundProxyConfig SmtpOutboundProxyConfiguration
		{
			get
			{
				return this.smtpOutboundProxyConfig;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00073283 File Offset: 0x00071483
		public TransportAppConfig.DeliveryQueuePrioritizationConfig DeliveryQueuePrioritizationConfiguration
		{
			get
			{
				return this.deliveryQueuePrioritizationConfig;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0007328B File Offset: 0x0007148B
		public TransportAppConfig.QueueConfig QueueConfiguration
		{
			get
			{
				return this.queueConfig;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00073293 File Offset: 0x00071493
		public TransportAppConfig.DeliveryFailureConfig DeliveryFailureConfiguration
		{
			get
			{
				return this.deliveryFailureConfig;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0007329B File Offset: 0x0007149B
		public TransportAppConfig.SecureMailConfig SecureMail
		{
			get
			{
				return this.secureMailConfig;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000732A3 File Offset: 0x000714A3
		public TransportAppConfig.LoggingConfig Logging
		{
			get
			{
				return this.loggingConfig;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000732AB File Offset: 0x000714AB
		public TransportAppConfig.FlowControlLogConfig FlowControlLog
		{
			get
			{
				return this.flowControlLogConfig;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x000732B3 File Offset: 0x000714B3
		public TransportAppConfig.ConditionalThrottlingConfig ThrottlingConfig
		{
			get
			{
				return this.throttlingConfig;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000732BB File Offset: 0x000714BB
		public TransportAppConfig.TransportRulesConfig TransportRuleConfig
		{
			get
			{
				return this.transportRulesConfig;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000732C3 File Offset: 0x000714C3
		public TransportAppConfig.PoisonMessageConfig PoisonMessage
		{
			get
			{
				return this.poisonMessageConfig;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x000732CB File Offset: 0x000714CB
		public TransportAppConfig.SmtpMessageThrottlingAgentConfig SmtpMessageThrottlingConfig
		{
			get
			{
				return this.smtpMessageThrottlingAgentConfig;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000732D3 File Offset: 0x000714D3
		public TransportAppConfig.StateManagementConfig StateManagement
		{
			get
			{
				return this.stateManagementConfig;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000732DB File Offset: 0x000714DB
		public TransportAppConfig.BootLoaderConfig BootLoader
		{
			get
			{
				return this.bootLoaderConfig;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000732E3 File Offset: 0x000714E3
		public TransportAppConfig.ProcessingQuotaConfig ProcessingQuota
		{
			get
			{
				return this.processingQuotaConfig;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000732EB File Offset: 0x000714EB
		public TransportAppConfig.ADPollingConfig ADPolling
		{
			get
			{
				return this.adPollingConfig;
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000732F4 File Offset: 0x000714F4
		public static TransportAppConfig Load()
		{
			try
			{
				if (TransportAppConfig.instance == null)
				{
					lock (TransportAppConfig.initializationLock)
					{
						if (TransportAppConfig.instance == null)
						{
							TransportAppConfig transportAppConfig = new TransportAppConfig();
							transportAppConfig.resourceManagerConfig = TransportAppConfig.ResourceManagerConfig.Load();
							transportAppConfig.jetDatabaseConfig = TransportAppConfig.JetDatabaseConfig.Load();
							transportAppConfig.dumpsterConfig = TransportAppConfig.DumpsterConfig.Load();
							transportAppConfig.messageResubmissionConfig = TransportAppConfig.MessageResubmissionConfig.Load();
							transportAppConfig.shadowRedundancyConfig = TransportAppConfig.ShadowRedundancyConfig.Load();
							transportAppConfig.remoteDeliveryConfig = TransportAppConfig.RemoteDeliveryConfig.Load();
							transportAppConfig.mapiSubmissionConfig = TransportAppConfig.MapiSubmissionConfig.Load();
							transportAppConfig.resolverConfig = TransportAppConfig.ResolverConfig.Load();
							transportAppConfig.routingConfig = TransportAppConfig.RoutingConfig.Load();
							transportAppConfig.contentConversionConfig = TransportAppConfig.ContentConversionConfig.Load();
							transportAppConfig.ipFilteringDatabaseConfig = TransportAppConfig.IPFilteringDatabaseConfig.Load();
							transportAppConfig.queueDatabaseConfig = TransportAppConfig.QueueDatabaseConfig.Load();
							transportAppConfig.workerProcessConfig = TransportAppConfig.WorkerProcessConfig.Load();
							transportAppConfig.latencyTrackerConfig = TransportAppConfig.LatencyTrackerConfig.Load();
							transportAppConfig.recipientValidatorConfig = TransportAppConfig.RecipientValidatorConfig.Load();
							transportAppConfig.perTenantCacheConfig = TransportAppConfig.PerTenantCacheConfig.Load();
							transportAppConfig.messageThrottlingConfig = TransportAppConfig.MessageThrottlingConfiguration.Load();
							transportAppConfig.connectionCacheConfig = TransportAppConfig.SMTPOutConnectionCacheConfig.Load();
							transportAppConfig.transportIsMemberOfResolverConfig = TransportAppConfig.IsMemberOfResolverConfiguration.Load("Transport");
							transportAppConfig.mailboxRulesIsMemberOfResolverConfig = TransportAppConfig.IsMemberOfResolverConfiguration.Load("MailboxRules");
							transportAppConfig.smtpAvailabilityConfig = TransportAppConfig.SmtpAvailabilityConfig.Load();
							transportAppConfig.smtpDataConfig = TransportAppConfig.SmtpDataConfig.Load();
							transportAppConfig.smtpMailCommandConfig = TransportAppConfig.SmtpMailCommandConfig.Load();
							transportAppConfig.messageContextBlobConfig = TransportAppConfig.MessageContextBlobConfig.Load();
							transportAppConfig.smtpReceiveConfig = TransportAppConfig.SmtpReceiveConfig.Load();
							transportAppConfig.smtpSendConfig = TransportAppConfig.SmtpSendConfig.Load();
							transportAppConfig.smtpProxyConfig = TransportAppConfig.SmtpProxyConfig.Load();
							transportAppConfig.smtpInboundProxyConfig = TransportAppConfig.SmtpInboundProxyConfig.Load();
							transportAppConfig.smtpOutboundProxyConfig = TransportAppConfig.SmtpOutboundProxyConfig.Load();
							transportAppConfig.deliveryQueuePrioritizationConfig = TransportAppConfig.DeliveryQueuePrioritizationConfig.Load();
							transportAppConfig.queueConfig = TransportAppConfig.QueueConfig.Load();
							transportAppConfig.deliveryFailureConfig = TransportAppConfig.DeliveryFailureConfig.Load();
							transportAppConfig.secureMailConfig = TransportAppConfig.SecureMailConfig.Load();
							transportAppConfig.loggingConfig = TransportAppConfig.LoggingConfig.Load();
							transportAppConfig.flowControlLogConfig = TransportAppConfig.FlowControlLogConfig.Load();
							transportAppConfig.transportRulesConfig = TransportAppConfig.TransportRulesConfig.Load();
							transportAppConfig.throttlingConfig = TransportAppConfig.ConditionalThrottlingConfig.Load();
							transportAppConfig.poisonMessageConfig = TransportAppConfig.PoisonMessageConfig.Load();
							transportAppConfig.smtpMessageThrottlingAgentConfig = TransportAppConfig.SmtpMessageThrottlingAgentConfig.Load();
							transportAppConfig.stateManagementConfig = TransportAppConfig.StateManagementConfig.Load();
							transportAppConfig.bootLoaderConfig = TransportAppConfig.BootLoaderConfig.Load();
							transportAppConfig.processingQuotaConfig = TransportAppConfig.ProcessingQuotaConfig.Load();
							transportAppConfig.adPollingConfig = TransportAppConfig.ADPollingConfig.Load();
							Thread.MemoryBarrier();
							TransportAppConfig.instance = transportAppConfig;
						}
					}
				}
			}
			catch (ConfigurationErrorsException ex)
			{
				Process currentProcess = Process.GetCurrentProcess();
				TransportAppConfig.Log.LogEvent(TransportEventLogConstants.Tuple_AppConfigLoadFailed, null, new object[]
				{
					currentProcess.ProcessName,
					currentProcess.Id,
					ex.ToString()
				});
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, ex.ToString(), ResultSeverityLevel.Warning, false);
				throw;
			}
			return TransportAppConfig.instance;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000735F0 File Offset: 0x000717F0
		public static XElement GetDiagnosticInfoForType(object config)
		{
			if (config == null)
			{
				return null;
			}
			Type type = config.GetType();
			return new XElement(type.Name, (from property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
			select new XElement(property.Name, property.GetValue(config, null))).ToArray<XElement>());
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00073654 File Offset: 0x00071854
		public static T GetConfigValue<T>(string label, T min, T max, T defaultValue, TransportAppConfig.TryParse<T> tryParse) where T : IComparable<T>
		{
			string value = ConfigurationManager.AppSettings[label];
			T result;
			TransportAppConfig.TryParseConfigValue<T>(label, value, min, max, defaultValue, tryParse, out result);
			return result;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00073680 File Offset: 0x00071880
		public static T GetConfigValue<T>(string label, T defaultValue, TransportAppConfig.TryParse<T> tryParse)
		{
			string value = ConfigurationManager.AppSettings[label];
			T result;
			TransportAppConfig.TryParseConfigValue<T>(value, defaultValue, tryParse, out result);
			return result;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000736A8 File Offset: 0x000718A8
		public static List<T> GetConfigList<T>(string label, char separator, TransportAppConfig.TryParse<T> tryParse)
		{
			string configValuesString = ConfigurationManager.AppSettings[label];
			return TransportAppConfig.GetConfigListFromValue<T>(configValuesString, separator, tryParse);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000736CC File Offset: 0x000718CC
		public static List<T> GetConfigListFromValue<T>(string configValuesString, char separator, TransportAppConfig.TryParse<T> tryParse)
		{
			List<T> list = new List<T>();
			if (!string.IsNullOrEmpty(configValuesString))
			{
				string[] array = configValuesString.Split(new char[]
				{
					separator
				});
				foreach (string value in array)
				{
					T item;
					if (TransportAppConfig.TryParseConfigValue<T>(value, default(T), tryParse, out item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00073736 File Offset: 0x00071936
		public static ByteQuantifiedSize GetConfigByteQuantifiedSize(string label, ByteQuantifiedSize min, ByteQuantifiedSize max, ByteQuantifiedSize defaultValue)
		{
			return TransportAppConfig.GetConfigValue<ByteQuantifiedSize>(label, min, max, defaultValue, new TransportAppConfig.TryParse<ByteQuantifiedSize>(ByteQuantifiedSize.TryParse));
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007374D File Offset: 0x0007194D
		public static int GetConfigInt(string label, int min, int max, int defaultValue)
		{
			return TransportAppConfig.GetConfigValue<int>(label, min, max, defaultValue, new TransportAppConfig.TryParse<int>(int.TryParse));
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00073764 File Offset: 0x00071964
		public static List<int> GetConfigIntList(string label, int min, int max, int defaultValue, char separator)
		{
			List<int> configList = TransportAppConfig.GetConfigList<int>(label, separator, new TransportAppConfig.TryParse<int>(int.TryParse));
			for (int i = 0; i < configList.Count; i++)
			{
				if (configList[i] < min || configList[i] > max)
				{
					configList[i] = defaultValue;
				}
			}
			return configList;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000737B4 File Offset: 0x000719B4
		public static double GetConfigDouble(string label, double min, double max, double defaultValue)
		{
			return TransportAppConfig.GetConfigValue<double>(label, min, max, defaultValue, new TransportAppConfig.TryParse<double>(double.TryParse));
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000737CB File Offset: 0x000719CB
		public static TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			return TransportAppConfig.GetConfigValue<TimeSpan>(label, min, max, defaultValue, new TransportAppConfig.TryParse<TimeSpan>(TimeSpan.TryParse));
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000737E2 File Offset: 0x000719E2
		public static bool GetConfigBool(string label, bool defaultValue)
		{
			return TransportAppConfig.GetConfigValue<bool>(label, defaultValue, new TransportAppConfig.TryParse<bool>(bool.TryParse));
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00073830 File Offset: 0x00071A30
		public static bool? GetConfigNullableBool(string label)
		{
			return TransportAppConfig.GetConfigValue<bool?>(label, null, delegate(string s, out bool? parsed)
			{
				bool value = false;
				if (!string.IsNullOrEmpty(s) && bool.TryParse(s, out value))
				{
					parsed = new bool?(value);
					return true;
				}
				parsed = null;
				return false;
			});
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000738C8 File Offset: 0x00071AC8
		public static TimeSpan? GetConfigNullableTimeSpan(string label, TimeSpan min, TimeSpan max)
		{
			return TransportAppConfig.GetConfigValue<TimeSpan?>(label, null, delegate(string s, out TimeSpan? parsed)
			{
				TimeSpan timeSpan;
				if (!string.IsNullOrEmpty(s) && TimeSpan.TryParse(s, out timeSpan) && timeSpan <= max && timeSpan >= min)
				{
					parsed = new TimeSpan?(timeSpan);
					return true;
				}
				parsed = null;
				return false;
			});
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00073904 File Offset: 0x00071B04
		public static T GetConfigEnum<T>(string label, T defaultValue) where T : struct
		{
			return TransportAppConfig.GetConfigEnum<T>(label, defaultValue, EnumParseOptions.IgnoreCase);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00073928 File Offset: 0x00071B28
		public static T GetConfigEnum<T>(string label, T defaultValue, EnumParseOptions options) where T : struct
		{
			return TransportAppConfig.GetConfigValue<T>(label, defaultValue, delegate(string s, out T parsed)
			{
				return EnumValidator.TryParse<T>(s, options, out parsed);
			});
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00073955 File Offset: 0x00071B55
		internal static string GetConfigString(string label, string defaultValue)
		{
			return ConfigurationManager.AppSettings[label] ?? defaultValue;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00073968 File Offset: 0x00071B68
		private static List<T> GetConfigList<T>(TransportAppConfig.ConfigurationList configurationList, TransportAppConfig.TryParse<T> tryParse)
		{
			List<T> list = new List<T>(configurationList.Count);
			for (int i = 0; i < configurationList.Count; i++)
			{
				T item;
				if (tryParse(configurationList[i].Value, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000739B0 File Offset: 0x00071BB0
		public XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("TransportAppConfig");
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
			{
				XElement xelement2 = new XElement(propertyInfo.Name);
				xelement.Add(xelement2);
				object value = propertyInfo.GetValue(this, null);
				if (value != null)
				{
					foreach (PropertyInfo propertyInfo2 in value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
					{
						xelement2.SetAttributeValue(propertyInfo2.Name, propertyInfo2.GetValue(value, null));
					}
				}
			}
			return xelement;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00073A64 File Offset: 0x00071C64
		private static bool TryParseConfigValue<T>(string label, string value, T min, T max, T defaultValue, TransportAppConfig.TryParse<T> tryParse, out T configValue) where T : IComparable<T>
		{
			if (min != null && max != null && min.CompareTo(max) > 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Minimum must be smaller than or equal to Maximum (Config='{0}', Min='{1}', Max='{2}', Default='{3}').", new object[]
				{
					label,
					min,
					max,
					defaultValue
				}));
			}
			if (TransportAppConfig.TryParseConfigValue<T>(value, defaultValue, tryParse, out configValue) && (min == null || configValue.CompareTo(min) >= 0) && (max == null || configValue.CompareTo(max) <= 0))
			{
				return true;
			}
			configValue = defaultValue;
			return false;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00073B1B File Offset: 0x00071D1B
		private static bool TryParseConfigValue<T>(string value, T defaultValue, TransportAppConfig.TryParse<T> tryParse, out T configValue)
		{
			if (!string.IsNullOrEmpty(value) && tryParse(value, out configValue))
			{
				return true;
			}
			configValue = defaultValue;
			return false;
		}

		// Token: 0x04000D46 RID: 3398
		private static readonly ExEventLog Log = new ExEventLog(ExTraceGlobals.ConfigurationTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000D47 RID: 3399
		private static object initializationLock = new object();

		// Token: 0x04000D48 RID: 3400
		private static TransportAppConfig instance;

		// Token: 0x04000D49 RID: 3401
		private TransportAppConfig.ResourceManagerConfig resourceManagerConfig;

		// Token: 0x04000D4A RID: 3402
		private TransportAppConfig.JetDatabaseConfig jetDatabaseConfig;

		// Token: 0x04000D4B RID: 3403
		private TransportAppConfig.DumpsterConfig dumpsterConfig;

		// Token: 0x04000D4C RID: 3404
		private TransportAppConfig.ShadowRedundancyConfig shadowRedundancyConfig;

		// Token: 0x04000D4D RID: 3405
		private TransportAppConfig.RemoteDeliveryConfig remoteDeliveryConfig;

		// Token: 0x04000D4E RID: 3406
		private TransportAppConfig.MapiSubmissionConfig mapiSubmissionConfig;

		// Token: 0x04000D4F RID: 3407
		private TransportAppConfig.ResolverConfig resolverConfig;

		// Token: 0x04000D50 RID: 3408
		private TransportAppConfig.RoutingConfig routingConfig;

		// Token: 0x04000D51 RID: 3409
		private TransportAppConfig.ContentConversionConfig contentConversionConfig;

		// Token: 0x04000D52 RID: 3410
		private TransportAppConfig.IPFilteringDatabaseConfig ipFilteringDatabaseConfig;

		// Token: 0x04000D53 RID: 3411
		private TransportAppConfig.IMessageResubmissionConfig messageResubmissionConfig;

		// Token: 0x04000D54 RID: 3412
		private TransportAppConfig.QueueDatabaseConfig queueDatabaseConfig;

		// Token: 0x04000D55 RID: 3413
		private TransportAppConfig.WorkerProcessConfig workerProcessConfig;

		// Token: 0x04000D56 RID: 3414
		private TransportAppConfig.LatencyTrackerConfig latencyTrackerConfig;

		// Token: 0x04000D57 RID: 3415
		private TransportAppConfig.RecipientValidatorConfig recipientValidatorConfig;

		// Token: 0x04000D58 RID: 3416
		private TransportAppConfig.PerTenantCacheConfig perTenantCacheConfig;

		// Token: 0x04000D59 RID: 3417
		private TransportAppConfig.MessageThrottlingConfiguration messageThrottlingConfig;

		// Token: 0x04000D5A RID: 3418
		private TransportAppConfig.SMTPOutConnectionCacheConfig connectionCacheConfig;

		// Token: 0x04000D5B RID: 3419
		private TransportAppConfig.IsMemberOfResolverConfiguration transportIsMemberOfResolverConfig;

		// Token: 0x04000D5C RID: 3420
		private TransportAppConfig.IsMemberOfResolverConfiguration mailboxRulesIsMemberOfResolverConfig;

		// Token: 0x04000D5D RID: 3421
		private TransportAppConfig.SmtpAvailabilityConfig smtpAvailabilityConfig;

		// Token: 0x04000D5E RID: 3422
		private TransportAppConfig.SmtpDataConfig smtpDataConfig;

		// Token: 0x04000D5F RID: 3423
		private TransportAppConfig.SmtpMailCommandConfig smtpMailCommandConfig;

		// Token: 0x04000D60 RID: 3424
		private TransportAppConfig.MessageContextBlobConfig messageContextBlobConfig;

		// Token: 0x04000D61 RID: 3425
		private TransportAppConfig.SmtpReceiveConfig smtpReceiveConfig;

		// Token: 0x04000D62 RID: 3426
		private TransportAppConfig.SmtpSendConfig smtpSendConfig;

		// Token: 0x04000D63 RID: 3427
		private TransportAppConfig.SmtpProxyConfig smtpProxyConfig;

		// Token: 0x04000D64 RID: 3428
		private TransportAppConfig.SmtpInboundProxyConfig smtpInboundProxyConfig;

		// Token: 0x04000D65 RID: 3429
		private TransportAppConfig.SmtpOutboundProxyConfig smtpOutboundProxyConfig;

		// Token: 0x04000D66 RID: 3430
		private TransportAppConfig.DeliveryFailureConfig deliveryFailureConfig;

		// Token: 0x04000D67 RID: 3431
		private TransportAppConfig.DeliveryQueuePrioritizationConfig deliveryQueuePrioritizationConfig;

		// Token: 0x04000D68 RID: 3432
		private TransportAppConfig.SecureMailConfig secureMailConfig;

		// Token: 0x04000D69 RID: 3433
		private TransportAppConfig.QueueConfig queueConfig;

		// Token: 0x04000D6A RID: 3434
		private TransportAppConfig.LoggingConfig loggingConfig;

		// Token: 0x04000D6B RID: 3435
		private TransportAppConfig.FlowControlLogConfig flowControlLogConfig;

		// Token: 0x04000D6C RID: 3436
		private TransportAppConfig.ConditionalThrottlingConfig throttlingConfig;

		// Token: 0x04000D6D RID: 3437
		private TransportAppConfig.TransportRulesConfig transportRulesConfig;

		// Token: 0x04000D6E RID: 3438
		private TransportAppConfig.PoisonMessageConfig poisonMessageConfig;

		// Token: 0x04000D6F RID: 3439
		private TransportAppConfig.SmtpMessageThrottlingAgentConfig smtpMessageThrottlingAgentConfig;

		// Token: 0x04000D70 RID: 3440
		private TransportAppConfig.StateManagementConfig stateManagementConfig;

		// Token: 0x04000D71 RID: 3441
		private TransportAppConfig.BootLoaderConfig bootLoaderConfig;

		// Token: 0x04000D72 RID: 3442
		private TransportAppConfig.ProcessingQuotaConfig processingQuotaConfig;

		// Token: 0x04000D73 RID: 3443
		private TransportAppConfig.ADPollingConfig adPollingConfig;

		// Token: 0x02000299 RID: 665
		// (Invoke) Token: 0x06001C9E RID: 7326
		public delegate bool TryParse<T>(string config, out T parsedConfig);

		// Token: 0x0200029A RID: 666
		public interface IMessageResubmissionConfig
		{
			// Token: 0x170007BE RID: 1982
			// (get) Token: 0x06001CA1 RID: 7329
			bool MessageResubmissionEnabled { get; }

			// Token: 0x170007BF RID: 1983
			// (get) Token: 0x06001CA2 RID: 7330
			TimeSpan ResubmissionInterval { get; }

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x06001CA3 RID: 7331
			TimeSpan ResubmissionInitialDelay { get; }

			// Token: 0x170007C1 RID: 1985
			// (get) Token: 0x06001CA4 RID: 7332
			int ResubmissionPageSize { get; }

			// Token: 0x170007C2 RID: 1986
			// (get) Token: 0x06001CA5 RID: 7333
			int MaxOutstandingResubmissionMessages { get; }

			// Token: 0x170007C3 RID: 1987
			// (get) Token: 0x06001CA6 RID: 7334
			int MaxResubmissionRequests { get; }

			// Token: 0x170007C4 RID: 1988
			// (get) Token: 0x06001CA7 RID: 7335
			int MaxRecentResubmissionRequests { get; }

			// Token: 0x170007C5 RID: 1989
			// (get) Token: 0x06001CA8 RID: 7336
			TimeSpan RecentResubmitRequestPeriod { get; }

			// Token: 0x170007C6 RID: 1990
			// (get) Token: 0x06001CA9 RID: 7337
			TimeSpan ResubmitRequestExpiryPeriod { get; }

			// Token: 0x170007C7 RID: 1991
			// (get) Token: 0x06001CAA RID: 7338
			TimeSpan TestResubmitRequestExpiryPeriod { get; }
		}

		// Token: 0x0200029B RID: 667
		public class ConfigurationListItem : ConfigurationElement
		{
			// Token: 0x170007C8 RID: 1992
			// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00073B5E File Offset: 0x00071D5E
			// (set) Token: 0x06001CAC RID: 7340 RVA: 0x00073B70 File Offset: 0x00071D70
			[ConfigurationProperty("value", IsKey = true, IsRequired = true)]
			public string Value
			{
				get
				{
					return (string)base["value"];
				}
				internal set
				{
					base["value"] = value;
				}
			}
		}

		// Token: 0x0200029C RID: 668
		public class ConfigurationList : ConfigurationElementCollection
		{
			// Token: 0x06001CAE RID: 7342 RVA: 0x00073B86 File Offset: 0x00071D86
			public ConfigurationList()
			{
			}

			// Token: 0x06001CAF RID: 7343 RVA: 0x00073B90 File Offset: 0x00071D90
			internal ConfigurationList(List<string> elements)
			{
				foreach (string value in elements)
				{
					this.BaseAdd(new TransportAppConfig.ConfigurationListItem
					{
						Value = value
					});
				}
			}

			// Token: 0x170007C9 RID: 1993
			public TransportAppConfig.ConfigurationListItem this[int index]
			{
				get
				{
					return (TransportAppConfig.ConfigurationListItem)base.BaseGet(index);
				}
			}

			// Token: 0x06001CB1 RID: 7345 RVA: 0x00073C02 File Offset: 0x00071E02
			protected override ConfigurationElement CreateNewElement()
			{
				return new TransportAppConfig.ConfigurationListItem();
			}

			// Token: 0x06001CB2 RID: 7346 RVA: 0x00073C09 File Offset: 0x00071E09
			protected override object GetElementKey(ConfigurationElement element)
			{
				return ((TransportAppConfig.ConfigurationListItem)element).Value;
			}
		}

		// Token: 0x0200029D RID: 669
		public class ConfigurationListsSection : ConfigurationSection
		{
			// Token: 0x06001CB3 RID: 7347 RVA: 0x00073C16 File Offset: 0x00071E16
			internal ConfigurationListsSection()
			{
			}

			// Token: 0x170007CA RID: 1994
			// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x00073C1E File Offset: 0x00071E1E
			// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x00073C30 File Offset: 0x00071E30
			[ConfigurationProperty("downgradedResponses")]
			public TransportAppConfig.ConfigurationList DowngradedResponses
			{
				get
				{
					return (TransportAppConfig.ConfigurationList)base["downgradedResponses"];
				}
				internal set
				{
					base["downgradedResponses"] = value;
				}
			}

			// Token: 0x170007CB RID: 1995
			// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00073C3E File Offset: 0x00071E3E
			// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x00073C50 File Offset: 0x00071E50
			[ConfigurationProperty("transportRulesScanVelocities")]
			public TransportAppConfig.ConfigurationList TransportRulesScanVelocities
			{
				get
				{
					return (TransportAppConfig.ConfigurationList)base["transportRulesScanVelocities"];
				}
				internal set
				{
					base["transportRulesScanVelocities"] = value;
				}
			}

			// Token: 0x170007CC RID: 1996
			// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00073C5E File Offset: 0x00071E5E
			// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x00073C70 File Offset: 0x00071E70
			[ConfigurationProperty("upgradedResponses")]
			public TransportAppConfig.ConfigurationList UpgradedResponses
			{
				get
				{
					return (TransportAppConfig.ConfigurationList)base["upgradedResponses"];
				}
				internal set
				{
					base["upgradedResponses"] = value;
				}
			}
		}

		// Token: 0x0200029E RID: 670
		public sealed class ResourceManagerConfig
		{
			// Token: 0x06001CBA RID: 7354 RVA: 0x00073C7E File Offset: 0x00071E7E
			private ResourceManagerConfig()
			{
			}

			// Token: 0x170007CD RID: 1997
			// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00073C86 File Offset: 0x00071E86
			public bool EnableResourceMonitoring
			{
				get
				{
					return this.enableResourceMonitoring;
				}
			}

			// Token: 0x170007CE RID: 1998
			// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00073C8E File Offset: 0x00071E8E
			public TimeSpan ResourceMonitoringInterval
			{
				get
				{
					return this.resourceMonitoringInterval;
				}
			}

			// Token: 0x170007CF RID: 1999
			// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00073C96 File Offset: 0x00071E96
			public int PercentagePrivateBytesHighThreshold
			{
				get
				{
					return this.percentagePrivateBytesHighThreshold;
				}
			}

			// Token: 0x170007D0 RID: 2000
			// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00073C9E File Offset: 0x00071E9E
			public int PercentagePrivateBytesMediumThreshold
			{
				get
				{
					return this.percentagePrivateBytesMediumThreshold;
				}
			}

			// Token: 0x170007D1 RID: 2001
			// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00073CA6 File Offset: 0x00071EA6
			public int PercentagePrivateBytesNormalThreshold
			{
				get
				{
					return this.percentagePrivateBytesNormalThreshold;
				}
			}

			// Token: 0x170007D2 RID: 2002
			// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00073CAE File Offset: 0x00071EAE
			public int PercentageDatabaseDiskSpaceHighThreshold
			{
				get
				{
					return this.percentageDatabaseDiskSpaceHighThreshold;
				}
			}

			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00073CB6 File Offset: 0x00071EB6
			public int PercentageDatabaseDiskSpaceMediumThreshold
			{
				get
				{
					return this.percentageDatabaseDiskSpaceMediumThreshold;
				}
			}

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00073CBE File Offset: 0x00071EBE
			public int PercentageDatabaseDiskSpaceNormalThreshold
			{
				get
				{
					return this.percentageDatabaseDiskSpaceNormalThreshold;
				}
			}

			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x00073CC6 File Offset: 0x00071EC6
			public int PercentageDatabaseLoggingDiskSpaceHighThreshold
			{
				get
				{
					return this.percentageLoggingDiskSpaceHighThreshold;
				}
			}

			// Token: 0x170007D6 RID: 2006
			// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00073CCE File Offset: 0x00071ECE
			public int PercentageDatabaseLoggingDiskSpaceMediumThreshold
			{
				get
				{
					return this.percentageLoggingDiskSpaceMediumThreshold;
				}
			}

			// Token: 0x170007D7 RID: 2007
			// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00073CD6 File Offset: 0x00071ED6
			public int PercentageDatabaseLoggingDiskSpaceNormalThreshold
			{
				get
				{
					return this.percentageLoggingDiskSpaceNormalThreshold;
				}
			}

			// Token: 0x170007D8 RID: 2008
			// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00073CDE File Offset: 0x00071EDE
			public ByteQuantifiedSize TempDiskSpaceRequired
			{
				get
				{
					return this.tempDiskSpaceRequired;
				}
			}

			// Token: 0x170007D9 RID: 2009
			// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00073CE6 File Offset: 0x00071EE6
			public int VersionBucketsHighThreshold
			{
				get
				{
					return this.versionBucketsHighThreshold;
				}
			}

			// Token: 0x170007DA RID: 2010
			// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00073CEE File Offset: 0x00071EEE
			public int VersionBucketsMediumThreshold
			{
				get
				{
					return this.versionBucketsMediumThreshold;
				}
			}

			// Token: 0x170007DB RID: 2011
			// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00073CF6 File Offset: 0x00071EF6
			public int VersionBucketsNormalThreshold
			{
				get
				{
					return this.versionBucketsNormalThreshold;
				}
			}

			// Token: 0x170007DC RID: 2012
			// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00073CFE File Offset: 0x00071EFE
			public int VersionBucketsHistoryDepth
			{
				get
				{
					return this.versionBucketsHistoryDepth;
				}
			}

			// Token: 0x170007DD RID: 2013
			// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00073D06 File Offset: 0x00071F06
			public int PrivateBytesHistoryDepth
			{
				get
				{
					return this.privateBytesHistoryDepth;
				}
			}

			// Token: 0x170007DE RID: 2014
			// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00073D0E File Offset: 0x00071F0E
			public int PercentagePhysicalMemoryUsedLimit
			{
				get
				{
					return this.percentagePhysicalMemoryUsedLimit;
				}
			}

			// Token: 0x170007DF RID: 2015
			// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00073D16 File Offset: 0x00071F16
			public bool DehydrateMessagesUnderMemoryPressure
			{
				get
				{
					return this.dehydrateMessagesUnderMemoryPressure;
				}
			}

			// Token: 0x170007E0 RID: 2016
			// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00073D1E File Offset: 0x00071F1E
			public int SubmissionQueueNormalThreshold
			{
				get
				{
					return this.submissionQueueNormalThreshold;
				}
			}

			// Token: 0x170007E1 RID: 2017
			// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00073D26 File Offset: 0x00071F26
			public int SubmissionQueueMediumThreshold
			{
				get
				{
					return this.submissionQueueMediumThreshold;
				}
			}

			// Token: 0x170007E2 RID: 2018
			// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00073D2E File Offset: 0x00071F2E
			public int SubmissionQueueHighThreshold
			{
				get
				{
					return this.submissionQueueHighThreshold;
				}
			}

			// Token: 0x170007E3 RID: 2019
			// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00073D36 File Offset: 0x00071F36
			public int SubmissionQueueHistoryDepth
			{
				get
				{
					return this.submissionQueueHistoryDepth;
				}
			}

			// Token: 0x170007E4 RID: 2020
			// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00073D3E File Offset: 0x00071F3E
			public TimeSpan BaseThrottlingDelayInterval
			{
				get
				{
					return this.baseThrottlingDelayInterval;
				}
			}

			// Token: 0x170007E5 RID: 2021
			// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00073D46 File Offset: 0x00071F46
			public TimeSpan MaxThrottlingDelayInterval
			{
				get
				{
					return this.maxThrottlingDelayInterval;
				}
			}

			// Token: 0x170007E6 RID: 2022
			// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00073D4E File Offset: 0x00071F4E
			public TimeSpan StepThrottlingDelayInterval
			{
				get
				{
					return this.stepThrottlingDelayInterval;
				}
			}

			// Token: 0x170007E7 RID: 2023
			// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00073D56 File Offset: 0x00071F56
			public TimeSpan StartThrottlingDelayInterval
			{
				get
				{
					return this.startThrottlingDelayInterval;
				}
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x00073D60 File Offset: 0x00071F60
			public static TransportAppConfig.ResourceManagerConfig Load()
			{
				TransportAppConfig.ResourceManagerConfig resourceManagerConfig = new TransportAppConfig.ResourceManagerConfig();
				resourceManagerConfig.enableResourceMonitoring = TransportAppConfig.GetConfigBool("EnableResourceMonitoring", true);
				if (resourceManagerConfig.enableResourceMonitoring)
				{
					resourceManagerConfig.resourceMonitoringInterval = TransportAppConfig.GetConfigTimeSpan("ResourceMonitoringInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(2.0));
					resourceManagerConfig.percentageDatabaseDiskSpaceHighThreshold = TransportAppConfig.GetConfigInt("PercentageDatabaseDiskSpaceUsedHighThreshold", 10, 100, 100);
					if (resourceManagerConfig.percentageDatabaseDiskSpaceHighThreshold != 0)
					{
						resourceManagerConfig.percentageDatabaseDiskSpaceMediumThreshold = TransportAppConfig.GetConfigInt("PercentageDatabaseDiskSpaceUsedMediumThreshold", 5, resourceManagerConfig.percentageDatabaseDiskSpaceHighThreshold - 1, resourceManagerConfig.percentageDatabaseDiskSpaceHighThreshold - 2);
						resourceManagerConfig.percentageDatabaseDiskSpaceNormalThreshold = TransportAppConfig.GetConfigInt("PercentageDatabaseDiskSpaceUsedNormalThreshold", 3, resourceManagerConfig.percentageDatabaseDiskSpaceMediumThreshold - 1, resourceManagerConfig.percentageDatabaseDiskSpaceMediumThreshold - 2);
					}
					resourceManagerConfig.percentageLoggingDiskSpaceHighThreshold = TransportAppConfig.GetConfigInt("PercentageDatabaseLoggingDiskSpaceUsedHighThreshold", 10, 100, 100);
					if (resourceManagerConfig.percentageLoggingDiskSpaceHighThreshold != 0)
					{
						resourceManagerConfig.percentageLoggingDiskSpaceMediumThreshold = TransportAppConfig.GetConfigInt("PercentageDatabaseLoggingDiskSpaceUsedMediumThreshold", 5, resourceManagerConfig.percentageLoggingDiskSpaceHighThreshold - 1, resourceManagerConfig.percentageLoggingDiskSpaceHighThreshold - 2);
						resourceManagerConfig.percentageLoggingDiskSpaceNormalThreshold = TransportAppConfig.GetConfigInt("PercentageDatabaseLoggingDiskSpaceUsedNormalThreshold", 3, resourceManagerConfig.percentageLoggingDiskSpaceMediumThreshold - 1, resourceManagerConfig.percentageLoggingDiskSpaceMediumThreshold - 2);
					}
					resourceManagerConfig.tempDiskSpaceRequired = TransportAppConfig.GetConfigByteQuantifiedSize("TempDiskSpaceRequired", ByteQuantifiedSize.FromKB(100UL), ByteQuantifiedSize.FromBytes(ulong.MaxValue), ByteQuantifiedSize.FromMB(200UL));
					resourceManagerConfig.percentagePrivateBytesHighThreshold = TransportAppConfig.GetConfigInt("PercentagePrivateBytesUsedHighThreshold", 10, 100, 0);
					if (resourceManagerConfig.percentagePrivateBytesHighThreshold != 0)
					{
						resourceManagerConfig.percentagePrivateBytesMediumThreshold = TransportAppConfig.GetConfigInt("PercentagePrivateBytesUsedMediumThreshold", 8, resourceManagerConfig.percentagePrivateBytesHighThreshold - 1, resourceManagerConfig.percentagePrivateBytesHighThreshold - 2);
						resourceManagerConfig.percentagePrivateBytesNormalThreshold = TransportAppConfig.GetConfigInt("PercentagePrivateBytesUsedNormalThreshold", 6, resourceManagerConfig.percentagePrivateBytesMediumThreshold - 1, resourceManagerConfig.percentagePrivateBytesMediumThreshold - 2);
					}
					resourceManagerConfig.versionBucketsHighThreshold = TransportAppConfig.GetConfigInt("VersionBucketsHighThreshold", 1, 8000, 2500);
					resourceManagerConfig.versionBucketsMediumThreshold = TransportAppConfig.GetConfigInt("VersionBucketsMediumThreshold", 1, resourceManagerConfig.versionBucketsHighThreshold, 2000);
					resourceManagerConfig.versionBucketsNormalThreshold = TransportAppConfig.GetConfigInt("VersionBucketsNormalThreshold", 1, resourceManagerConfig.versionBucketsMediumThreshold, 1750);
					resourceManagerConfig.percentagePhysicalMemoryUsedLimit = TransportAppConfig.GetConfigInt("PercentagePhysicalMemoryUsedLimit", 20, 100, 94);
					resourceManagerConfig.dehydrateMessagesUnderMemoryPressure = TransportAppConfig.GetConfigBool("DehydrateMessagesUnderMemoryPressure", true);
					resourceManagerConfig.versionBucketsHistoryDepth = TransportAppConfig.GetConfigInt("VersionBucketsHistoryDepth", 1, 1000, 10);
					resourceManagerConfig.privateBytesHistoryDepth = TransportAppConfig.GetConfigInt("PrivateBytesHistoryDepth", 1, 1000, 30);
					resourceManagerConfig.submissionQueueHighThreshold = TransportAppConfig.GetConfigInt("SubmissionQueueHighThreshold", 1, 50000, 15000);
					resourceManagerConfig.submissionQueueMediumThreshold = TransportAppConfig.GetConfigInt("SubmissionQueueMediumThreshold", 1, resourceManagerConfig.submissionQueueHighThreshold, 10000);
					resourceManagerConfig.submissionQueueNormalThreshold = TransportAppConfig.GetConfigInt("SubmissionQueueNormalThreshold", 1, resourceManagerConfig.submissionQueueMediumThreshold, 2000);
					resourceManagerConfig.submissionQueueHistoryDepth = TransportAppConfig.GetConfigInt("SubmissionQueueHistoryDepth", 1, 1000, 300);
					resourceManagerConfig.maxThrottlingDelayInterval = TransportAppConfig.GetConfigTimeSpan("SmtpMaxThrottlingDelayInterval", TimeSpan.Zero, TimeSpan.FromMinutes(10.0), TimeSpan.FromSeconds(55.0));
					resourceManagerConfig.baseThrottlingDelayInterval = TransportAppConfig.GetConfigTimeSpan("SmtpBaseThrottlingDelayInterval", TimeSpan.Zero, resourceManagerConfig.maxThrottlingDelayInterval, TimeSpan.Zero);
					resourceManagerConfig.stepThrottlingDelayInterval = TransportAppConfig.GetConfigTimeSpan("SmtpStepThrottlingDelayInterval", TimeSpan.Zero, resourceManagerConfig.maxThrottlingDelayInterval, TimeSpan.FromSeconds(1.0));
					resourceManagerConfig.startThrottlingDelayInterval = TransportAppConfig.GetConfigTimeSpan("SmtpStartThrottlingDelayInterval", TimeSpan.Zero, resourceManagerConfig.maxThrottlingDelayInterval, TimeSpan.FromSeconds(1.0));
				}
				return resourceManagerConfig;
			}

			// Token: 0x04000D75 RID: 3445
			private const int MaxHistoryDepth = 1000;

			// Token: 0x04000D76 RID: 3446
			private bool enableResourceMonitoring;

			// Token: 0x04000D77 RID: 3447
			private TimeSpan resourceMonitoringInterval;

			// Token: 0x04000D78 RID: 3448
			private int percentagePrivateBytesHighThreshold;

			// Token: 0x04000D79 RID: 3449
			private int percentagePrivateBytesMediumThreshold;

			// Token: 0x04000D7A RID: 3450
			private int percentagePrivateBytesNormalThreshold;

			// Token: 0x04000D7B RID: 3451
			private int percentageDatabaseDiskSpaceHighThreshold;

			// Token: 0x04000D7C RID: 3452
			private int percentageDatabaseDiskSpaceMediumThreshold;

			// Token: 0x04000D7D RID: 3453
			private int percentageDatabaseDiskSpaceNormalThreshold;

			// Token: 0x04000D7E RID: 3454
			private int percentageLoggingDiskSpaceHighThreshold;

			// Token: 0x04000D7F RID: 3455
			private int percentageLoggingDiskSpaceMediumThreshold;

			// Token: 0x04000D80 RID: 3456
			private int percentageLoggingDiskSpaceNormalThreshold;

			// Token: 0x04000D81 RID: 3457
			private ByteQuantifiedSize tempDiskSpaceRequired;

			// Token: 0x04000D82 RID: 3458
			private int versionBucketsHighThreshold;

			// Token: 0x04000D83 RID: 3459
			private int versionBucketsMediumThreshold;

			// Token: 0x04000D84 RID: 3460
			private int versionBucketsNormalThreshold;

			// Token: 0x04000D85 RID: 3461
			private int versionBucketsHistoryDepth;

			// Token: 0x04000D86 RID: 3462
			private int privateBytesHistoryDepth;

			// Token: 0x04000D87 RID: 3463
			private int percentagePhysicalMemoryUsedLimit;

			// Token: 0x04000D88 RID: 3464
			private bool dehydrateMessagesUnderMemoryPressure;

			// Token: 0x04000D89 RID: 3465
			private int submissionQueueHighThreshold;

			// Token: 0x04000D8A RID: 3466
			private int submissionQueueMediumThreshold;

			// Token: 0x04000D8B RID: 3467
			private int submissionQueueNormalThreshold;

			// Token: 0x04000D8C RID: 3468
			private int submissionQueueHistoryDepth;

			// Token: 0x04000D8D RID: 3469
			private TimeSpan baseThrottlingDelayInterval;

			// Token: 0x04000D8E RID: 3470
			private TimeSpan maxThrottlingDelayInterval;

			// Token: 0x04000D8F RID: 3471
			private TimeSpan stepThrottlingDelayInterval;

			// Token: 0x04000D90 RID: 3472
			private TimeSpan startThrottlingDelayInterval;
		}

		// Token: 0x0200029F RID: 671
		public class DumpsterConfig
		{
			// Token: 0x06001CD7 RID: 7383 RVA: 0x000740CE File Offset: 0x000722CE
			private DumpsterConfig()
			{
			}

			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x000740D6 File Offset: 0x000722D6
			public bool AllowDuplicateDelivery
			{
				get
				{
					return this.allowDuplicateDelivery;
				}
			}

			// Token: 0x06001CD9 RID: 7385 RVA: 0x000740E0 File Offset: 0x000722E0
			public static TransportAppConfig.DumpsterConfig Load()
			{
				TransportAppConfig.DumpsterConfig dumpsterConfig = new TransportAppConfig.DumpsterConfig();
				string value = ConfigurationManager.AppSettings["DumpsterAllowDuplicateDelivery"];
				if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out dumpsterConfig.allowDuplicateDelivery))
				{
					dumpsterConfig.allowDuplicateDelivery = false;
				}
				return dumpsterConfig;
			}

			// Token: 0x04000D91 RID: 3473
			private const string AllowDuplicateDeliveryString = "DumpsterAllowDuplicateDelivery";

			// Token: 0x04000D92 RID: 3474
			private bool allowDuplicateDelivery;
		}

		// Token: 0x020002A0 RID: 672
		public sealed class ShadowRedundancyConfig
		{
			// Token: 0x06001CDA RID: 7386 RVA: 0x00074121 File Offset: 0x00072321
			private ShadowRedundancyConfig()
			{
			}

			// Token: 0x170007E9 RID: 2025
			// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00074129 File Offset: 0x00072329
			public bool ShadowRedundancyLocalDisabled
			{
				get
				{
					return this.shadowRedundancyLocalDisabled;
				}
			}

			// Token: 0x170007EA RID: 2026
			// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00074131 File Offset: 0x00072331
			public bool DelayedAckSkippingEnabled
			{
				get
				{
					return this.delayedAckSkippingEnabled;
				}
			}

			// Token: 0x170007EB RID: 2027
			// (get) Token: 0x06001CDD RID: 7389 RVA: 0x00074139 File Offset: 0x00072339
			public int DelayedAckSkippingQueueLength
			{
				get
				{
					return this.delayedAckSkippingQueueLength;
				}
			}

			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x06001CDE RID: 7390 RVA: 0x00074141 File Offset: 0x00072341
			public bool ShadowRedundancyPromotionEnabled
			{
				get
				{
					return this.shadowRedundancyPromotionEnabled;
				}
			}

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00074149 File Offset: 0x00072349
			public ShadowRedundancyCompatibilityVersion CompatibilityVersion
			{
				get
				{
					return this.compatibilityVersion;
				}
			}

			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00074151 File Offset: 0x00072351
			public List<RoutingHost> ShadowHubList
			{
				get
				{
					return this.shadowHubList;
				}
			}

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00074159 File Offset: 0x00072359
			public int MaxDiscardIdsPerSmtpCommand
			{
				get
				{
					return this.maxDiscardIdsPerSmtpCommand;
				}
			}

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00074161 File Offset: 0x00072361
			public TimeSpan MaxPendingHeartbeatInterval
			{
				get
				{
					return this.maxPendingHeartbeatInterval;
				}
			}

			// Token: 0x06001CE3 RID: 7395 RVA: 0x0007416C File Offset: 0x0007236C
			public static TransportAppConfig.ShadowRedundancyConfig Load()
			{
				return new TransportAppConfig.ShadowRedundancyConfig
				{
					shadowRedundancyLocalDisabled = TransportAppConfig.GetConfigBool("ShadowRedundancyLocalDisabled", false),
					delayedAckSkippingEnabled = TransportAppConfig.GetConfigBool("DelayedAckSkippingEnabled", true),
					delayedAckSkippingQueueLength = TransportAppConfig.GetConfigInt("DelayedAckSkippingQueueLength", 1, 250000, 100),
					shadowRedundancyPromotionEnabled = TransportAppConfig.GetConfigBool("ShadowRedundancyPromotionEnabled", false),
					compatibilityVersion = TransportAppConfig.GetConfigEnum<ShadowRedundancyCompatibilityVersion>("ShadowRedundancyCompatibilityVersion", ShadowRedundancyCompatibilityVersion.E15),
					shadowHubList = TransportAppConfig.GetConfigList<RoutingHost>("ShadowRedundancyShadowHubList", ',', new TransportAppConfig.TryParse<RoutingHost>(RoutingHost.TryParse)),
					maxDiscardIdsPerSmtpCommand = TransportAppConfig.GetConfigInt("ShadowRedundancyMaxDiscardIdsPerSmtpCommand", 50, 1000, 50),
					maxPendingHeartbeatInterval = TransportAppConfig.GetConfigTimeSpan("ShadowRedundancyMaxPendingHeartbeatInterval", TransportAppConfig.ShadowRedundancyConfig.MaxPendingHeartbeatIntervalMinValue, TransportAppConfig.ShadowRedundancyConfig.MaxPendingHeartbeatIntervalMaxValue, TransportAppConfig.ShadowRedundancyConfig.DefaultMaxPendingHeartbeatInterval)
				};
			}

			// Token: 0x04000D93 RID: 3475
			private const string ShadowRedundancyLocalDisabledLabel = "ShadowRedundancyLocalDisabled";

			// Token: 0x04000D94 RID: 3476
			private const bool ShadowRedundancyLocalDisabledDefault = false;

			// Token: 0x04000D95 RID: 3477
			private const string DelayedAckSkippingEnabledLabel = "DelayedAckSkippingEnabled";

			// Token: 0x04000D96 RID: 3478
			private const bool DelayedAckSkippingEnabledDefault = true;

			// Token: 0x04000D97 RID: 3479
			private const string DelayedAckSkippingQueueLengthLabel = "DelayedAckSkippingQueueLength";

			// Token: 0x04000D98 RID: 3480
			private const int DelayedAckSkippingQueueLengthMin = 1;

			// Token: 0x04000D99 RID: 3481
			private const int DelayedAckSkippingQueueLengthMax = 250000;

			// Token: 0x04000D9A RID: 3482
			private const int DelayedAckSkippingQueueLengthDefault = 100;

			// Token: 0x04000D9B RID: 3483
			private const string ShadowRedundancyPromotionEnabledLabel = "ShadowRedundancyPromotionEnabled";

			// Token: 0x04000D9C RID: 3484
			private const bool ShadowRedundancyPromotionEnabledDefault = false;

			// Token: 0x04000D9D RID: 3485
			private const string ShadowRedundancyCompatibilityVersionLabel = "ShadowRedundancyCompatibilityVersion";

			// Token: 0x04000D9E RID: 3486
			private const string ShadowRedundancyShadowHubListLabel = "ShadowRedundancyShadowHubList";

			// Token: 0x04000D9F RID: 3487
			private const string MaxDiscardIdsPerSmtpCommandLabel = "ShadowRedundancyMaxDiscardIdsPerSmtpCommand";

			// Token: 0x04000DA0 RID: 3488
			private const int MaxDiscardIdsPerSmtpCommandMin = 50;

			// Token: 0x04000DA1 RID: 3489
			private const int MaxDiscardIdsPerSmtpCommandMax = 1000;

			// Token: 0x04000DA2 RID: 3490
			private const int MaxDiscardIdsPerSmtpCommandDefault = 50;

			// Token: 0x04000DA3 RID: 3491
			private const char ShadowRedundancyShadowHubListSeparator = ',';

			// Token: 0x04000DA4 RID: 3492
			private const ShadowRedundancyCompatibilityVersion DefaultCompatibilityVersion = ShadowRedundancyCompatibilityVersion.E15;

			// Token: 0x04000DA5 RID: 3493
			private const string ShadowRedundancyMaxPendingHeartbeatIntervalLabel = "ShadowRedundancyMaxPendingHeartbeatInterval";

			// Token: 0x04000DA6 RID: 3494
			private static readonly TimeSpan DefaultMaxPendingHeartbeatInterval = TimeSpan.FromMinutes(30.0);

			// Token: 0x04000DA7 RID: 3495
			private static readonly TimeSpan MaxPendingHeartbeatIntervalMinValue = TimeSpan.FromMinutes(1.0);

			// Token: 0x04000DA8 RID: 3496
			private static readonly TimeSpan MaxPendingHeartbeatIntervalMaxValue = TimeSpan.MaxValue;

			// Token: 0x04000DA9 RID: 3497
			private bool shadowRedundancyLocalDisabled;

			// Token: 0x04000DAA RID: 3498
			private bool delayedAckSkippingEnabled;

			// Token: 0x04000DAB RID: 3499
			private int delayedAckSkippingQueueLength;

			// Token: 0x04000DAC RID: 3500
			private bool shadowRedundancyPromotionEnabled;

			// Token: 0x04000DAD RID: 3501
			private ShadowRedundancyCompatibilityVersion compatibilityVersion;

			// Token: 0x04000DAE RID: 3502
			private List<RoutingHost> shadowHubList;

			// Token: 0x04000DAF RID: 3503
			private int maxDiscardIdsPerSmtpCommand;

			// Token: 0x04000DB0 RID: 3504
			private TimeSpan maxPendingHeartbeatInterval;
		}

		// Token: 0x020002A1 RID: 673
		public sealed class JetDatabaseConfig
		{
			// Token: 0x06001CE5 RID: 7397 RVA: 0x00074264 File Offset: 0x00072464
			private JetDatabaseConfig()
			{
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00074282 File Offset: 0x00072482
			public ByteQuantifiedSize CheckpointDepthMax
			{
				get
				{
					return this.checkpointDepthMax;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0007428A File Offset: 0x0007248A
			public ByteQuantifiedSize MaxCacheSize
			{
				get
				{
					return this.maxCacheSize;
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00074292 File Offset: 0x00072492
			public ByteQuantifiedSize MinCacheSize
			{
				get
				{
					return this.minCacheSize;
				}
			}

			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0007429A File Offset: 0x0007249A
			public uint StartFlushThreshold
			{
				get
				{
					return this.startFlushThresholdPercent;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x06001CEA RID: 7402 RVA: 0x000742A2 File Offset: 0x000724A2
			public uint StopFlushThreshold
			{
				get
				{
					return this.stopFlushThresholdPercent;
				}
			}

			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000742AA File Offset: 0x000724AA
			public ByteQuantifiedSize BufferedStreamSize
			{
				get
				{
					return this.bufferedStreamSize;
				}
			}

			// Token: 0x170007F7 RID: 2039
			// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000742B2 File Offset: 0x000724B2
			public uint AutomaticShrinkDatabaseFreeSpaceThreshold
			{
				get
				{
					return this.automaticShrinkDatabaseFreeSpaceThreshold;
				}
			}

			// Token: 0x06001CED RID: 7405 RVA: 0x000742BC File Offset: 0x000724BC
			public static TransportAppConfig.JetDatabaseConfig Load()
			{
				TransportAppConfig.JetDatabaseConfig jetDatabaseConfig = new TransportAppConfig.JetDatabaseConfig();
				jetDatabaseConfig.checkpointDepthMax = TransportAppConfig.GetConfigByteQuantifiedSize("DatabaseCheckPointDepthMax", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(384UL));
				jetDatabaseConfig.maxCacheSize = TransportAppConfig.GetConfigByteQuantifiedSize("DatabaseMaxCacheSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(512UL));
				jetDatabaseConfig.minCacheSize = TransportAppConfig.GetConfigByteQuantifiedSize("DatabaseMinCacheSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(32UL));
				if (jetDatabaseConfig.minCacheSize.ToBytes() > jetDatabaseConfig.maxCacheSize.ToBytes())
				{
					jetDatabaseConfig.minCacheSize = jetDatabaseConfig.maxCacheSize / 2;
				}
				string text = ConfigurationManager.AppSettings["DatabaseCacheFlushStart"];
				uint num;
				if (!string.IsNullOrEmpty(text) && uint.TryParse(text, out num) && num <= 100U)
				{
					jetDatabaseConfig.startFlushThresholdPercent = num;
				}
				string text2 = ConfigurationManager.AppSettings["DatabaseCacheFlushStop"];
				if (!string.IsNullOrEmpty(text2) && uint.TryParse(text2, out num) && num <= 100U)
				{
					jetDatabaseConfig.stopFlushThresholdPercent = num;
				}
				jetDatabaseConfig.bufferedStreamSize = TransportAppConfig.GetConfigByteQuantifiedSize("BufferedStreamSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromKB(128UL), ByteQuantifiedSize.FromKB(32UL));
				string text3 = ConfigurationManager.AppSettings["AutomaticShrinkDatabaseFreeSpaceThreshold"];
				if (!string.IsNullOrEmpty(text3) && uint.TryParse(text3, out num) && num >= 0U && num <= 100U)
				{
					jetDatabaseConfig.automaticShrinkDatabaseFreeSpaceThreshold = num;
				}
				return jetDatabaseConfig;
			}

			// Token: 0x04000DB1 RID: 3505
			public static readonly ByteQuantifiedSize PageSize = ByteQuantifiedSize.FromKB(32UL);

			// Token: 0x04000DB2 RID: 3506
			private ByteQuantifiedSize checkpointDepthMax;

			// Token: 0x04000DB3 RID: 3507
			private ByteQuantifiedSize maxCacheSize;

			// Token: 0x04000DB4 RID: 3508
			private ByteQuantifiedSize minCacheSize;

			// Token: 0x04000DB5 RID: 3509
			private uint startFlushThresholdPercent = 3U;

			// Token: 0x04000DB6 RID: 3510
			private uint stopFlushThresholdPercent = 5U;

			// Token: 0x04000DB7 RID: 3511
			private ByteQuantifiedSize bufferedStreamSize;

			// Token: 0x04000DB8 RID: 3512
			private uint automaticShrinkDatabaseFreeSpaceThreshold = 20U;
		}

		// Token: 0x020002A2 RID: 674
		public class RemoteDeliveryConfig
		{
			// Token: 0x06001CEF RID: 7407 RVA: 0x0007443C File Offset: 0x0007263C
			private RemoteDeliveryConfig()
			{
				int num = Enum.GetNames(typeof(DeliveryPriority)).Length;
				this.maxPerDomainPriorityConnections = new int[num];
				this.maxPerDomainPriorityConnections[0] = 3;
				this.maxPerDomainPriorityConnections[1] = 15;
				this.maxPerDomainPriorityConnections[2] = 2;
				this.maxPerDomainPriorityConnections[3] = 2;
				this.messageExpirationTimeout = new TimeSpan[num];
				this.messageExpirationTimeout[0] = TransportAppConfig.RemoteDeliveryConfig.DefaultHighPriorityMessageExpirationTimeout;
				this.messageExpirationTimeout[1] = TransportAppConfig.RemoteDeliveryConfig.DefaultNormalPriorityMessageExpirationTimeout;
				this.messageExpirationTimeout[2] = TransportAppConfig.RemoteDeliveryConfig.DefaultLowPriorityMessageExpirationTimeout;
				this.messageExpirationTimeout[3] = TransportAppConfig.RemoteDeliveryConfig.DefaultLowPriorityMessageExpirationTimeout;
				this.delayNotificationTimeout = new TimeSpan[num];
				this.delayNotificationTimeout[0] = TransportAppConfig.RemoteDeliveryConfig.DefaultHighPriorityDelayNotificationTimeout;
				this.delayNotificationTimeout[1] = TransportAppConfig.RemoteDeliveryConfig.DefaultNormalPriorityDelayNotificationTimeout;
				this.delayNotificationTimeout[2] = TransportAppConfig.RemoteDeliveryConfig.DefaultLowPriorityDelayNotificationTimeout;
				this.delayNotificationTimeout[3] = TransportAppConfig.RemoteDeliveryConfig.DefaultLowPriorityDelayNotificationTimeout;
			}

			// Token: 0x06001CF0 RID: 7408 RVA: 0x000746D4 File Offset: 0x000728D4
			internal static TransportAppConfig.RemoteDeliveryConfig CreateForUnitTestsOnly(List<int> internalMessageRetryIntervalRangeList, List<int> externalMessageRetryIntervalRangeList)
			{
				TransportAppConfig.RemoteDeliveryConfig remoteDeliveryConfig = new TransportAppConfig.RemoteDeliveryConfig();
				TransportAppConfig.RemoteDeliveryConfig.ValidateMessageRetryIntervalRangeList(internalMessageRetryIntervalRangeList);
				TransportAppConfig.RemoteDeliveryConfig.ValidateMessageRetryIntervalRangeList(externalMessageRetryIntervalRangeList);
				remoteDeliveryConfig.internalMessageRetryIntervalRangeList = internalMessageRetryIntervalRangeList;
				remoteDeliveryConfig.externalMessageRetryIntervalRangeList = externalMessageRetryIntervalRangeList;
				return remoteDeliveryConfig;
			}

			// Token: 0x170007F8 RID: 2040
			// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00074702 File Offset: 0x00072902
			public int MessageThresholdToUpdateHealthCounters
			{
				get
				{
					return this.messageThresholdToUpdateHealthCounters;
				}
			}

			// Token: 0x170007F9 RID: 2041
			// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0007470A File Offset: 0x0007290A
			public TimeSpan RefreshIntervalToUpdateHealth
			{
				get
				{
					return this.refreshIntervalToUpdateHealth;
				}
			}

			// Token: 0x170007FA RID: 2042
			// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00074712 File Offset: 0x00072912
			public TimeSpan MaxIdleTimeBeforeResubmit
			{
				get
				{
					return this.maxIdleTimeBeforeResubmission;
				}
			}

			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x0007471A File Offset: 0x0007291A
			public TimeSpan MailboxDeliveryQueueRetryInterval
			{
				get
				{
					return this.mailboxDeliveryQueueRetryInterval;
				}
			}

			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x00074722 File Offset: 0x00072922
			public TimeSpan MailboxDeliveryFastQueueRetryInterval
			{
				get
				{
					return this.mailboxDeliveryFastQueueRetryInterval;
				}
			}

			// Token: 0x170007FD RID: 2045
			// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x0007472A File Offset: 0x0007292A
			public TimeSpan MailboxServerThreadLimitQueueRetryInterval
			{
				get
				{
					return this.mailboxServerThreadLimitQueueRetryInterval;
				}
			}

			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x00074732 File Offset: 0x00072932
			public TimeSpan MailboxDatabaseThreadLimitQueueRetryInterval
			{
				get
				{
					return this.mailboxDatabaseThreadLimitQueueRetryInterval;
				}
			}

			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x0007473A File Offset: 0x0007293A
			public int MaxMailboxDeliveryPerMdbConnections
			{
				get
				{
					return this.maxMailboxDeliveryPerMdbConnections;
				}
			}

			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00074742 File Offset: 0x00072942
			public int MaxMailboxDeliveryPerMdbConnectionsHighHealthPercent
			{
				get
				{
					return this.maxMailboxDeliveryPerMdbConnectionsHighHealthPercent;
				}
			}

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0007474A File Offset: 0x0007294A
			public int MaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent
			{
				get
				{
					return this.maxMailboxDeliveryPerMdbConnectionsMediumHealthPercent;
				}
			}

			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00074752 File Offset: 0x00072952
			public int MaxMailboxDeliveryPerMdbConnectionsLowHealthPercent
			{
				get
				{
					return this.maxMailboxDeliveryPerMdbConnectionsLowHealthPercent;
				}
			}

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0007475A File Offset: 0x0007295A
			public int MaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent
			{
				get
				{
					return this.maxMailboxDeliveryPerMdbConnectionsLowestHealthPercent;
				}
			}

			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00074762 File Offset: 0x00072962
			public bool DynamicMailboxDatabaseThrottlingEnabled
			{
				get
				{
					return this.dynamicMailboxDatabaseThrottlingEnabled;
				}
			}

			// Token: 0x17000805 RID: 2053
			// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0007476A File Offset: 0x0007296A
			public ByteQuantifiedSize MaxMailboxDeliveryConcurrentMessageSizeLimit
			{
				get
				{
					return this.maxMailboxDeliveryConcurrentMessageSizeLimit;
				}
			}

			// Token: 0x17000806 RID: 2054
			// (get) Token: 0x06001CFF RID: 7423 RVA: 0x00074772 File Offset: 0x00072972
			public int MaxStoreDriverDeliveryExceptionCallstackHistoryPerBucket
			{
				get
				{
					return this.maxStoreDriverDeliveryExceptionCallstackHistoryPerBucket;
				}
			}

			// Token: 0x17000807 RID: 2055
			// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0007477A File Offset: 0x0007297A
			public int MaxStoreDriverDeliveryExceptionOccurrenceHistoryPerException
			{
				get
				{
					return this.maxStoreDriverDeliveryExceptionOccurrenceHistoryPerException;
				}
			}

			// Token: 0x17000808 RID: 2056
			// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00074782 File Offset: 0x00072982
			public string StoreDriverExceptionCallstackToTrap
			{
				get
				{
					return this.storeDriverExceptionCallstackToTrap;
				}
			}

			// Token: 0x17000809 RID: 2057
			// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0007478A File Offset: 0x0007298A
			public int MdbHealthMediumToHighThreshold
			{
				get
				{
					return this.mdbHealthMediumToHighThreshold;
				}
			}

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06001D03 RID: 7427 RVA: 0x00074792 File Offset: 0x00072992
			public int MdbHealthLowToMediumThreshold
			{
				get
				{
					return this.mdbHealthLowToMediumThreshold;
				}
			}

			// Token: 0x1700080B RID: 2059
			// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0007479A File Offset: 0x0007299A
			public bool MailboxDeliveryThrottlingEnabled
			{
				get
				{
					return this.mailboxDeliveryThrottlingEnabled;
				}
			}

			// Token: 0x1700080C RID: 2060
			// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000747A2 File Offset: 0x000729A2
			public int MailboxDeliveryThrottlingLogBufferSize
			{
				get
				{
					return this.mailboxDeliveryThrottlingLogBufferSize;
				}
			}

			// Token: 0x1700080D RID: 2061
			// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000747AA File Offset: 0x000729AA
			public TimeSpan MailboxDeliveryThrottlingLogFlushInterval
			{
				get
				{
					return this.mailboxDeliveryThrottlingLogFlushInterval;
				}
			}

			// Token: 0x1700080E RID: 2062
			// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000747B2 File Offset: 0x000729B2
			public TimeSpan MailboxDeliveryThrottlingLogAsyncLogInterval
			{
				get
				{
					return this.mailboxDeliveryThrottlingLogAsyncLogInterval;
				}
			}

			// Token: 0x1700080F RID: 2063
			// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000747BA File Offset: 0x000729BA
			public TimeSpan MailboxDeliveryThrottlingLogSummaryLoggingInterval
			{
				get
				{
					return this.mailboxDeliveryThrottlingLogSummaryLoggingInterval;
				}
			}

			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000747C2 File Offset: 0x000729C2
			public bool MailboxTransportTableBasedExceptionHandlerEnabled
			{
				get
				{
					return this.mailboxTransportTableBasedExceptionHandlerEnabled;
				}
			}

			// Token: 0x17000811 RID: 2065
			// (get) Token: 0x06001D0A RID: 7434 RVA: 0x000747CA File Offset: 0x000729CA
			public TimeSpan QuarantinedMailboxRetryInterval
			{
				get
				{
					return this.quarantinedMailboxRetryInterval;
				}
			}

			// Token: 0x17000812 RID: 2066
			// (get) Token: 0x06001D0B RID: 7435 RVA: 0x000747D2 File Offset: 0x000729D2
			public TimeSpan QueueGlitchRetryInterval
			{
				get
				{
					return this.queueGlitchRetryInterval;
				}
			}

			// Token: 0x17000813 RID: 2067
			// (get) Token: 0x06001D0C RID: 7436 RVA: 0x000747DA File Offset: 0x000729DA
			public int QueueGlitchRetryCount
			{
				get
				{
					return this.queueGlitchRetryCount;
				}
			}

			// Token: 0x17000814 RID: 2068
			// (get) Token: 0x06001D0D RID: 7437 RVA: 0x000747E2 File Offset: 0x000729E2
			public int MaxQueryResultCount
			{
				get
				{
					return this.maxQueryResultCount;
				}
			}

			// Token: 0x17000815 RID: 2069
			// (get) Token: 0x06001D0E RID: 7438 RVA: 0x000747EA File Offset: 0x000729EA
			public DnsFaultTolerance DnsFaultTolerance
			{
				get
				{
					return this.dnsFaultTolerance;
				}
			}

			// Token: 0x17000816 RID: 2070
			// (get) Token: 0x06001D0F RID: 7439 RVA: 0x000747F2 File Offset: 0x000729F2
			public bool LoadBalancingForServerFailoverEnabled
			{
				get
				{
					return this.loadBalancingForServerFailoverEnabled;
				}
			}

			// Token: 0x17000817 RID: 2071
			// (get) Token: 0x06001D10 RID: 7440 RVA: 0x000747FA File Offset: 0x000729FA
			public bool PriorityQueuingEnabled
			{
				get
				{
					return this.priorityQueuingEnabled;
				}
			}

			// Token: 0x17000818 RID: 2072
			// (get) Token: 0x06001D11 RID: 7441 RVA: 0x00074802 File Offset: 0x00072A02
			public bool LocalDeliveryPriorityQueuingEnabled
			{
				get
				{
					return this.localDeliveryPriorityQueuingEnabled;
				}
			}

			// Token: 0x17000819 RID: 2073
			// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0007480A File Offset: 0x00072A0A
			public bool RemoteDeliveryPriorityQueuingEnabled
			{
				get
				{
					return this.remoteDeliveryPriorityQueuingEnabled;
				}
			}

			// Token: 0x1700081A RID: 2074
			// (get) Token: 0x06001D13 RID: 7443 RVA: 0x00074812 File Offset: 0x00072A12
			public ByteQuantifiedSize MaxHighPriorityMessageSize
			{
				get
				{
					return this.maxHighPriorityMessageSize;
				}
			}

			// Token: 0x1700081B RID: 2075
			// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0007481A File Offset: 0x00072A1A
			public int MailboxServerThreadLimit
			{
				get
				{
					return this.mailboxServerThreadLimit;
				}
			}

			// Token: 0x1700081C RID: 2076
			// (get) Token: 0x06001D15 RID: 7445 RVA: 0x00074822 File Offset: 0x00072A22
			public int RecipientThreadLimit
			{
				get
				{
					return this.recipientThreadLimit;
				}
			}

			// Token: 0x1700081D RID: 2077
			// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0007482A File Offset: 0x00072A2A
			public int DeliverySourceThreadLimit
			{
				get
				{
					return this.deliverySourceThreadLimitPerCore * Environment.ProcessorCount;
				}
			}

			// Token: 0x1700081E RID: 2078
			// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00074838 File Offset: 0x00072A38
			public int MailboxDeliveryMaxMessagesPerConnection
			{
				get
				{
					return this.mailboxDeliveryMaxMessagesPerConnection;
				}
			}

			// Token: 0x1700081F RID: 2079
			// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00074840 File Offset: 0x00072A40
			public int[] DeliveryPriorityQuotas
			{
				get
				{
					return this.deliveryPriorityQuotas;
				}
			}

			// Token: 0x17000820 RID: 2080
			// (get) Token: 0x06001D19 RID: 7449 RVA: 0x00074848 File Offset: 0x00072A48
			public TimeSpan StoreDriverRecipientDeliveryHangThreshold
			{
				get
				{
					return this.storeDriverRecipientDeliveryHangThreshold;
				}
			}

			// Token: 0x17000821 RID: 2081
			// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00074850 File Offset: 0x00072A50
			public TimeSpan StoreDriverDeliveryHangDetectionInterval
			{
				get
				{
					return this.storeDriverDeliveryHangDetectionInterval;
				}
			}

			// Token: 0x17000822 RID: 2082
			// (get) Token: 0x06001D1B RID: 7451 RVA: 0x00074858 File Offset: 0x00072A58
			public bool ExcludeDnsServersFromLoopbackAdapters
			{
				get
				{
					return this.excludeDnsServersFromLoopbackAdapters;
				}
			}

			// Token: 0x17000823 RID: 2083
			// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00074860 File Offset: 0x00072A60
			public bool ExcludeIPv6SiteLocalDnsAddresses
			{
				get
				{
					return this.excludeIPv6SiteLocalDnsAddresses;
				}
			}

			// Token: 0x17000824 RID: 2084
			// (get) Token: 0x06001D1D RID: 7453 RVA: 0x00074868 File Offset: 0x00072A68
			public TimeSpan DnsRequestTimeout
			{
				get
				{
					return this.dnsRequestTimeout;
				}
			}

			// Token: 0x17000825 RID: 2085
			// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00074870 File Offset: 0x00072A70
			public TimeSpan DnsQueryRetryInterval
			{
				get
				{
					return this.dnsQueryRetryInterval;
				}
			}

			// Token: 0x17000826 RID: 2086
			// (get) Token: 0x06001D1F RID: 7455 RVA: 0x00074878 File Offset: 0x00072A78
			public TimeSpan ConfigUpdateResubmitDeferInterval
			{
				get
				{
					return this.configUpdateResubmitDeferInterval;
				}
			}

			// Token: 0x17000827 RID: 2087
			// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00074880 File Offset: 0x00072A80
			public bool DnsIpv6Enabled
			{
				get
				{
					return this.dnsIpv6Enabled;
				}
			}

			// Token: 0x17000828 RID: 2088
			// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00074888 File Offset: 0x00072A88
			public int MailboxQueueMessageCountThresholdForConcurrentConnections
			{
				get
				{
					return this.mailboxQueueMessageCountThresholdForConcurrentConnections;
				}
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00074890 File Offset: 0x00072A90
			public int SmtpConnectorQueueMessageCountThresholdForConcurrentConnections
			{
				get
				{
					return this.smtpConnectorQueueMessageCountThresholdForConcurrentConnections;
				}
			}

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00074898 File Offset: 0x00072A98
			public int IntraorgSmtpQueueMessageCountThresholdForConcurrentConnections
			{
				get
				{
					return this.intraorgSmtpQueueMessageCountThresholdForConcurrentConnections;
				}
			}

			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x06001D24 RID: 7460 RVA: 0x000748A0 File Offset: 0x00072AA0
			public int OtherQueueMessageCountThresholdForConcurrentConnections
			{
				get
				{
					return this.otherQueueMessageCountThresholdForConcurrentConnections;
				}
			}

			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x06001D25 RID: 7461 RVA: 0x000748A8 File Offset: 0x00072AA8
			public int DeprioritizeOnRecipientThreadLimitExceededCount
			{
				get
				{
					return this.deprioritizeOnRecipientThreadLimitExceededCount;
				}
			}

			// Token: 0x1700082D RID: 2093
			// (get) Token: 0x06001D26 RID: 7462 RVA: 0x000748B0 File Offset: 0x00072AB0
			public bool MessageRetryIntervalProgressiveBackoffEnabled
			{
				get
				{
					return this.messageRetryIntervalProgressiveBackoffEnabled;
				}
			}

			// Token: 0x1700082E RID: 2094
			// (get) Token: 0x06001D27 RID: 7463 RVA: 0x000748B8 File Offset: 0x00072AB8
			public TimeSpan ResubmitDueToOutboundConnectorChangeInterval
			{
				get
				{
					return this.resubmitDueToOutboundConnectorChangeInterval;
				}
			}

			// Token: 0x1700082F RID: 2095
			// (get) Token: 0x06001D28 RID: 7464 RVA: 0x000748C0 File Offset: 0x00072AC0
			public TimeSpan OutboundConnectorLookbackBufferInterval
			{
				get
				{
					return this.outboundConnectorLookbackBufferInterval;
				}
			}

			// Token: 0x06001D29 RID: 7465 RVA: 0x000748C8 File Offset: 0x00072AC8
			public static TransportAppConfig.RemoteDeliveryConfig Load()
			{
				TransportAppConfig.RemoteDeliveryConfig remoteDeliveryConfig = new TransportAppConfig.RemoteDeliveryConfig();
				remoteDeliveryConfig.maxIdleTimeBeforeResubmission = TransportAppConfig.GetConfigTimeSpan("MaxIdleTimeBeforeResubmit", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxIdleTimeBeforeResubmit);
				remoteDeliveryConfig.mailboxDeliveryQueueRetryInterval = TransportAppConfig.GetConfigTimeSpan("MailboxDeliveryQueueRetryInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDeliveryQueueRetryInterval);
				remoteDeliveryConfig.mailboxDeliveryFastQueueRetryInterval = TransportAppConfig.GetConfigTimeSpan("MailboxDeliveryFastQueueRetryInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDeliveryFastQueueRetryInterval);
				remoteDeliveryConfig.mailboxServerThreadLimitQueueRetryInterval = TransportAppConfig.GetConfigTimeSpan("MailboxServerThreadLimitQueueRetryInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxServerThreadLimitQueueRetryInterval);
				remoteDeliveryConfig.mailboxDatabaseThreadLimitQueueRetryInterval = TransportAppConfig.GetConfigTimeSpan("MailboxDatabaseThreadLimitQueueRetryInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDatabaseThreadLimitQueueRetryInterval);
				remoteDeliveryConfig.maxMailboxDeliveryPerMdbConnections = TransportAppConfig.GetConfigInt("MaxMailboxDeliveryPerMdbConnections", 1, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnections);
				remoteDeliveryConfig.maxMailboxDeliveryPerMdbConnectionsHighHealthPercent = TransportAppConfig.GetConfigInt("MaxMailboxDeliveryPerMdbConnectionsHighHealthPercent", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsHighHealthPercent);
				remoteDeliveryConfig.maxMailboxDeliveryPerMdbConnectionsMediumHealthPercent = TransportAppConfig.GetConfigInt("MaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent);
				remoteDeliveryConfig.maxMailboxDeliveryPerMdbConnectionsLowHealthPercent = TransportAppConfig.GetConfigInt("MaxMailboxDeliveryPerMdbConnectionsLowHealthPercent", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsLowHealthPercent);
				remoteDeliveryConfig.maxMailboxDeliveryPerMdbConnectionsLowestHealthPercent = TransportAppConfig.GetConfigInt("MaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent);
				remoteDeliveryConfig.dynamicMailboxDatabaseThrottlingEnabled = TransportAppConfig.GetConfigBool("DynamicMailboxDatabaseThrottlingEnabled", TransportAppConfig.RemoteDeliveryConfig.DefaultDynamicMailboxDatabaseThrottlingEnabled);
				remoteDeliveryConfig.maxMailboxDeliveryConcurrentMessageSizeLimit = TransportAppConfig.GetConfigByteQuantifiedSize("MaxMailboxDeliveryConcurrentMessageSizeLimit", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryConcurrentMessageSizeLimit);
				remoteDeliveryConfig.mdbHealthMediumToHighThreshold = TransportAppConfig.GetConfigInt("MdbHealthMediumToHighThreshold", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMdbHealthMediumToHighThreshold);
				remoteDeliveryConfig.mdbHealthLowToMediumThreshold = TransportAppConfig.GetConfigInt("MdbHealthLowToMediumThreshold", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMdbHealthLowToMediumThreshold);
				remoteDeliveryConfig.mailboxDeliveryThrottlingEnabled = TransportAppConfig.GetConfigBool("MailboxDeliveryThrottlingEnabled", TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDeliveryThrottlingEnabled);
				remoteDeliveryConfig.mailboxDeliveryThrottlingLogBufferSize = TransportAppConfig.GetConfigInt("MailboxDeliveryThrottlingLogBufferSize", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogBufferSize);
				remoteDeliveryConfig.mailboxDeliveryThrottlingLogFlushInterval = TransportAppConfig.GetConfigTimeSpan("MailboxDeliveryThrottlingLogFlushInterval", TimeSpan.MinValue, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogFlushInterval);
				remoteDeliveryConfig.mailboxDeliveryThrottlingLogAsyncLogInterval = TransportAppConfig.GetConfigTimeSpan("MailboxDeliveryThrottlingLogAsyncLogInterval", TimeSpan.MinValue, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogAsyncLogInterval);
				remoteDeliveryConfig.mailboxDeliveryThrottlingLogSummaryLoggingInterval = TransportAppConfig.GetConfigTimeSpan("MailboxDeliveryThrottlingLogSummaryLoggingInterval", TimeSpan.Zero, TimeSpan.FromHours(1.0), TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogSummaryLoggingInterval);
				remoteDeliveryConfig.mailboxTransportTableBasedExceptionHandlerEnabled = TransportAppConfig.GetConfigBool("MailboxTransportTableBasedExceptionHandlerEnabled", TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxTransportTableBasedExceptionHandlerEnabled);
				remoteDeliveryConfig.quarantinedMailboxRetryInterval = TransportAppConfig.GetConfigTimeSpan("QuarantinedMailboxRetryInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultQuarantinedMailboxRetryInterval);
				remoteDeliveryConfig.queueGlitchRetryInterval = TransportAppConfig.GetConfigTimeSpan("QueueGlitchRetryInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultQueueGlithRetryInterval);
				remoteDeliveryConfig.queueGlitchRetryCount = TransportAppConfig.GetConfigInt("QueueGlitchRetryCount", 0, 15, 4);
				remoteDeliveryConfig.maxQueryResultCount = TransportAppConfig.GetConfigInt("MaxQueueViewerQueryResultCount", 1, int.MaxValue, 50000);
				remoteDeliveryConfig.dnsFaultTolerance = TransportAppConfig.GetConfigEnum<DnsFaultTolerance>("DnsFaultTolerance", DnsFaultTolerance.Lenient);
				remoteDeliveryConfig.loadBalancingForServerFailoverEnabled = TransportAppConfig.GetConfigBool("LoadBalancingForServerFailoverEnabled", true);
				remoteDeliveryConfig.priorityQueuingEnabled = TransportAppConfig.GetConfigBool("PriorityQueuingEnabled", false);
				remoteDeliveryConfig.localDeliveryPriorityQueuingEnabled = TransportAppConfig.GetConfigBool("LocalDeliveryPriorityQueuingEnabled", true);
				remoteDeliveryConfig.remoteDeliveryPriorityQueuingEnabled = TransportAppConfig.GetConfigBool("RemoteDeliveryPriorityQueuingEnabled", true);
				remoteDeliveryConfig.maxPerDomainPriorityConnections[0] = TransportAppConfig.GetConfigInt("MaxPerDomainHighPriorityConnections", 1, int.MaxValue, 3);
				remoteDeliveryConfig.maxPerDomainPriorityConnections[1] = TransportAppConfig.GetConfigInt("MaxPerDomainNormalPriorityConnections", 1, int.MaxValue, 15);
				remoteDeliveryConfig.maxPerDomainPriorityConnections[2] = TransportAppConfig.GetConfigInt("MaxPerDomainLowPriorityConnections", 1, int.MaxValue, 2);
				remoteDeliveryConfig.messageExpirationTimeout[0] = TransportAppConfig.GetConfigTimeSpan("HighPriorityMessageExpirationTimeout", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultHighPriorityMessageExpirationTimeout);
				remoteDeliveryConfig.messageExpirationTimeout[1] = TransportAppConfig.GetConfigTimeSpan("NormalPriorityMessageExpirationTimeout", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultNormalPriorityMessageExpirationTimeout);
				remoteDeliveryConfig.messageExpirationTimeout[2] = TransportAppConfig.GetConfigTimeSpan("LowPriorityMessageExpirationTimeout", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultLowPriorityMessageExpirationTimeout);
				remoteDeliveryConfig.delayNotificationTimeout[0] = TransportAppConfig.GetConfigTimeSpan("HighPriorityDelayNotificationTimeout", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultHighPriorityDelayNotificationTimeout);
				remoteDeliveryConfig.delayNotificationTimeout[1] = TransportAppConfig.GetConfigTimeSpan("NormalPriorityDelayNotificationTimeout", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultNormalPriorityDelayNotificationTimeout);
				remoteDeliveryConfig.delayNotificationTimeout[2] = TransportAppConfig.GetConfigTimeSpan("LowPriorityDelayNotificationTimeout", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultLowPriorityDelayNotificationTimeout);
				remoteDeliveryConfig.maxHighPriorityMessageSize = TransportAppConfig.GetConfigByteQuantifiedSize("MaxHighPriorityMessageSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), TransportAppConfig.RemoteDeliveryConfig.DefaultMaxHighPriorityMessageSize);
				remoteDeliveryConfig.mailboxServerThreadLimit = TransportAppConfig.GetConfigInt("MailboxServerThreadLimit", 1, TransportAppConfig.WorkerProcessConfig.MaxWorkerThreadsMaximum, 120);
				remoteDeliveryConfig.recipientThreadLimit = TransportAppConfig.GetConfigInt("RecipientThreadLimit", 1, remoteDeliveryConfig.maxMailboxDeliveryPerMdbConnections, 3);
				remoteDeliveryConfig.deliverySourceThreadLimitPerCore = TransportAppConfig.GetConfigInt("DeliverySourceThreadLimitPerCore", 1, int.MaxValue, 5);
				remoteDeliveryConfig.mailboxDeliveryMaxMessagesPerConnection = TransportAppConfig.GetConfigInt("MailboxDeliveryMaxMessagesPerConnection", 1, int.MaxValue, 20);
				remoteDeliveryConfig.deliveryPriorityQuotas[0] = TransportAppConfig.GetConfigInt("HighPriorityDeliveryQueueQuota", 1, int.MaxValue, 40);
				remoteDeliveryConfig.deliveryPriorityQuotas[1] = TransportAppConfig.GetConfigInt("NormalPriorityDeliveryQueueQuota", 1, int.MaxValue, 25);
				remoteDeliveryConfig.deliveryPriorityQuotas[2] = TransportAppConfig.GetConfigInt("LowPriorityDeliveryQueueQuota", 1, int.MaxValue, 4);
				remoteDeliveryConfig.deliveryPriorityQuotas[3] = TransportAppConfig.GetConfigInt("NonePriorityDeliveryQueueQuota", 1, int.MaxValue, 1);
				remoteDeliveryConfig.maxStoreDriverDeliveryExceptionCallstackHistoryPerBucket = TransportAppConfig.GetConfigInt("MaxStoreDriverDeliveryExceptionCallstackHistoryPerBucket", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxStoreDriverDeliveryExceptionCallstackHistoryPerBucket);
				remoteDeliveryConfig.maxStoreDriverDeliveryExceptionOccurrenceHistoryPerException = TransportAppConfig.GetConfigInt("MaxStoreDriverDeliveryExceptionOccurrenceHistoryPerException", 0, int.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultMaxStoreDriverDeliveryExceptionOccurrenceHistoryPerException);
				remoteDeliveryConfig.storeDriverExceptionCallstackToTrap = TransportAppConfig.GetConfigString("StoreDriverExceptionCallstackToTrap", TransportAppConfig.RemoteDeliveryConfig.DefaultStoreDriverExceptionCallstackToTrap);
				remoteDeliveryConfig.storeDriverRecipientDeliveryHangThreshold = TransportAppConfig.GetConfigTimeSpan("StoreDriverRecipientDeliveryHangThreshold", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultStoreDriverRecipientDeliveryHangThreshold);
				remoteDeliveryConfig.storeDriverDeliveryHangDetectionInterval = TransportAppConfig.GetConfigTimeSpan("StoreDriverDeliveryHangDetectionInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultStoreDriverDeliveryHangDetectionInterval);
				remoteDeliveryConfig.excludeDnsServersFromLoopbackAdapters = TransportAppConfig.GetConfigBool("ExcludeDnsServersFromLoopbackAdapters", true);
				remoteDeliveryConfig.excludeIPv6SiteLocalDnsAddresses = TransportAppConfig.GetConfigBool("ExcludeIPv6SiteLocalDnsAddresses", true);
				remoteDeliveryConfig.dnsRequestTimeout = TransportAppConfig.GetConfigTimeSpan("DnsRequestTimeout", TransportAppConfig.RemoteDeliveryConfig.DefaultDnsRequestTimeout, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultDnsRequestTimeout);
				remoteDeliveryConfig.dnsQueryRetryInterval = TransportAppConfig.GetConfigTimeSpan("DnsQueryRetryInterval", TransportAppConfig.RemoteDeliveryConfig.DefaultDnsQueryRetryInterval, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultDnsQueryRetryInterval);
				remoteDeliveryConfig.configUpdateResubmitDeferInterval = TransportAppConfig.GetConfigTimeSpan("ConfigUpdateResubmitDeferInterval", TimeSpan.Zero, TimeSpan.FromHours(12.0), TimeSpan.FromMinutes(1.0));
				remoteDeliveryConfig.dnsIpv6Enabled = TransportAppConfig.GetConfigBool("DnsIpv6Enabled", true);
				remoteDeliveryConfig.mailboxQueueMessageCountThresholdForConcurrentConnections = TransportAppConfig.GetConfigInt("MailboxQueueMessageCountThresholdForConcurrentConnections", 1, int.MaxValue, 20);
				remoteDeliveryConfig.smtpConnectorQueueMessageCountThresholdForConcurrentConnections = TransportAppConfig.GetConfigInt("SmtpConnectorQueueMessageCountThresholdForConcurrentConnections", 1, int.MaxValue, 20);
				remoteDeliveryConfig.intraorgSmtpQueueMessageCountThresholdForConcurrentConnections = TransportAppConfig.GetConfigInt("IntraorgSmtpQueueMessageCountThresholdForConcurrentConnections", 1, int.MaxValue, 20);
				remoteDeliveryConfig.otherQueueMessageCountThresholdForConcurrentConnections = TransportAppConfig.GetConfigInt("OtherQueueMessageCountThresholdForConcurrentConnections", 1, int.MaxValue, 20);
				remoteDeliveryConfig.refreshIntervalToUpdateHealth = TransportAppConfig.GetConfigTimeSpan("RemoteDeliveryHealthCounterUpdateInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultRefreshIntervalForUpdatingHealth);
				remoteDeliveryConfig.resubmitDueToOutboundConnectorChangeInterval = TransportAppConfig.GetConfigTimeSpan("ResubmitDueToOutboundConnectorChangeInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultResubmitDueToOutboundConnectorChangeInterval);
				remoteDeliveryConfig.outboundConnectorLookbackBufferInterval = TransportAppConfig.GetConfigTimeSpan("OutboundConnectorLookbackBufferInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.RemoteDeliveryConfig.DefaultOutboundConnectorLookbackBufferInterval);
				remoteDeliveryConfig.messageThresholdToUpdateHealthCounters = TransportAppConfig.GetConfigInt("MinimumQueuedMessageCountForMonitoring", 0, int.MaxValue, 100);
				remoteDeliveryConfig.deprioritizeOnRecipientThreadLimitExceededCount = TransportAppConfig.GetConfigInt("DeprioritizeOnRecipientThreadLimitExceededCount", 1, 1000, 3);
				remoteDeliveryConfig.messageRetryIntervalProgressiveBackoffEnabled = TransportAppConfig.GetConfigBool("MessageRetryIntervalProgressiveBackoffEnabled", false);
				remoteDeliveryConfig.internalMessageRetryIntervalRangeList = TransportAppConfig.GetConfigIntList("InternalMessageRetryIntervalRangeList", 1, 43200, 60, ',');
				remoteDeliveryConfig.externalMessageRetryIntervalRangeList = TransportAppConfig.GetConfigIntList("ExternalMessageRetryIntervalRangeList", 1, 43200, 60, ',');
				if (remoteDeliveryConfig.internalMessageRetryIntervalRangeList.Count == 0)
				{
					remoteDeliveryConfig.internalMessageRetryIntervalRangeList = TransportAppConfig.RemoteDeliveryConfig.DefaultInternalMessageRetryIntervalRangeList;
				}
				if (remoteDeliveryConfig.externalMessageRetryIntervalRangeList.Count == 0)
				{
					remoteDeliveryConfig.externalMessageRetryIntervalRangeList = TransportAppConfig.RemoteDeliveryConfig.DefaultExternalMessageRetryIntervalRangeList;
				}
				TransportAppConfig.RemoteDeliveryConfig.ValidateMessageRetryIntervalRangeList(remoteDeliveryConfig.internalMessageRetryIntervalRangeList);
				TransportAppConfig.RemoteDeliveryConfig.ValidateMessageRetryIntervalRangeList(remoteDeliveryConfig.externalMessageRetryIntervalRangeList);
				return remoteDeliveryConfig;
			}

			// Token: 0x06001D2A RID: 7466 RVA: 0x000750BD File Offset: 0x000732BD
			public int MaxPerDomainPriorityConnections(DeliveryPriority priority)
			{
				return this.maxPerDomainPriorityConnections[(int)priority];
			}

			// Token: 0x06001D2B RID: 7467 RVA: 0x000750C7 File Offset: 0x000732C7
			public TimeSpan MessageExpirationTimeout(DeliveryPriority priority)
			{
				return this.messageExpirationTimeout[(int)priority];
			}

			// Token: 0x06001D2C RID: 7468 RVA: 0x000750DA File Offset: 0x000732DA
			public TimeSpan DelayNotificationTimeout(DeliveryPriority priority)
			{
				return this.delayNotificationTimeout[(int)priority];
			}

			// Token: 0x06001D2D RID: 7469 RVA: 0x000750ED File Offset: 0x000732ED
			public TimeSpan GetMessageRetryInterval(int numConsecutiveRetries, bool isInternalDelivery)
			{
				return TransportAppConfig.RemoteDeliveryConfig.GetMessageRetryInterval(numConsecutiveRetries, isInternalDelivery ? this.internalMessageRetryIntervalRangeList : this.externalMessageRetryIntervalRangeList, this.random);
			}

			// Token: 0x06001D2E RID: 7470 RVA: 0x0007510C File Offset: 0x0007330C
			private static TimeSpan GetMessageRetryInterval(int numConsecutiveRetries, List<int> messageRetryIntervalRangeList, Random random)
			{
				if (numConsecutiveRetries < 1)
				{
					throw new ArgumentOutOfRangeException(string.Format("numConsecutiveRetries [{0}] should have a positive value", numConsecutiveRetries));
				}
				int num = Math.Min(messageRetryIntervalRangeList.Count - 1, 2 * numConsecutiveRetries - 1);
				int num2 = random.Next(messageRetryIntervalRangeList[num - 1], messageRetryIntervalRangeList[num]);
				return TimeSpan.FromSeconds((double)num2);
			}

			// Token: 0x06001D2F RID: 7471 RVA: 0x00075164 File Offset: 0x00073364
			private static void ValidateMessageRetryIntervalRangeList(List<int> messageRetryIntervalRangeList)
			{
				if (messageRetryIntervalRangeList.Count < 2)
				{
					throw new ConfigurationErrorsException("retry interval range list should have at least 2 entries");
				}
				if (messageRetryIntervalRangeList.Count % 2 == 1)
				{
					throw new ConfigurationErrorsException("retry interval range list should have an even number of entries");
				}
				for (int i = 1; i < messageRetryIntervalRangeList.Count; i++)
				{
					if (messageRetryIntervalRangeList[i] <= messageRetryIntervalRangeList[i - 1])
					{
						throw new ConfigurationErrorsException("retry interval range list should have entries increasing in value");
					}
				}
			}

			// Token: 0x04000DB9 RID: 3513
			private const bool DefaultDnsIpv6Enabled = true;

			// Token: 0x04000DBA RID: 3514
			private const int DefaultQueueGlitchRetryCount = 4;

			// Token: 0x04000DBB RID: 3515
			private const int DefaultMaxQueryResultCount = 50000;

			// Token: 0x04000DBC RID: 3516
			private const DnsFaultTolerance DefaultDnsFaultTolerance = DnsFaultTolerance.Lenient;

			// Token: 0x04000DBD RID: 3517
			private const bool DefaultLoadBalancingForServerFailoverEnabled = true;

			// Token: 0x04000DBE RID: 3518
			private const int DefaultMaxPerDomainHighPriorityConnections = 3;

			// Token: 0x04000DBF RID: 3519
			private const int DefaultMaxPerDomainNormalPriorityConnections = 15;

			// Token: 0x04000DC0 RID: 3520
			private const int DefaultMaxPerDomainLowPriorityConnections = 2;

			// Token: 0x04000DC1 RID: 3521
			private const int DefaultMailboxServerThreadLimit = 120;

			// Token: 0x04000DC2 RID: 3522
			private const int DefaultRecipientThreadLimit = 3;

			// Token: 0x04000DC3 RID: 3523
			private const int DefaultDeliverySourceThreadLimitPerCore = 5;

			// Token: 0x04000DC4 RID: 3524
			private const int DefaultMailboxDeliveryMaxMessagesPerConnection = 20;

			// Token: 0x04000DC5 RID: 3525
			private const int DefaultNonePriorityDeliveryQueueQuota = 1;

			// Token: 0x04000DC6 RID: 3526
			private const int DefaultLowPriorityDeliveryQueueQuota = 4;

			// Token: 0x04000DC7 RID: 3527
			private const int DefaultNormalPriorityDeliveryQueueQuota = 25;

			// Token: 0x04000DC8 RID: 3528
			private const int DefaultHighPriorityDeliveryQueueQuota = 40;

			// Token: 0x04000DC9 RID: 3529
			private const int DefaultMessageThresoldForUpdatingOutboundIPPoolCounters = 100;

			// Token: 0x04000DCA RID: 3530
			private static readonly TimeSpan DefaultRefreshIntervalForUpdatingHealth = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000DCB RID: 3531
			private static readonly TimeSpan DefaultMaxIdleTimeBeforeResubmit = TimeSpan.FromHours(12.0);

			// Token: 0x04000DCC RID: 3532
			private static readonly TimeSpan DefaultMailboxDeliveryQueueRetryInterval = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000DCD RID: 3533
			private static readonly TimeSpan DefaultMailboxDeliveryFastQueueRetryInterval = TimeSpan.FromSeconds(2.0);

			// Token: 0x04000DCE RID: 3534
			private static readonly TimeSpan DefaultMailboxServerThreadLimitQueueRetryInterval = TimeSpan.FromSeconds(5.0);

			// Token: 0x04000DCF RID: 3535
			private static readonly TimeSpan DefaultMailboxDatabaseThreadLimitQueueRetryInterval = TimeSpan.FromSeconds(5.0);

			// Token: 0x04000DD0 RID: 3536
			private static readonly int DefaultMaxMailboxDeliveryPerMdbConnections = 8;

			// Token: 0x04000DD1 RID: 3537
			private static readonly int DefaultMaxMailboxDeliveryPerMdbConnectionsHighHealthPercent = 75;

			// Token: 0x04000DD2 RID: 3538
			private static readonly int DefaultMaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent = 50;

			// Token: 0x04000DD3 RID: 3539
			private static readonly int DefaultMaxMailboxDeliveryPerMdbConnectionsLowHealthPercent = 25;

			// Token: 0x04000DD4 RID: 3540
			private static readonly int DefaultMaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent = 1;

			// Token: 0x04000DD5 RID: 3541
			private static readonly bool DefaultDynamicMailboxDatabaseThrottlingEnabled = true;

			// Token: 0x04000DD6 RID: 3542
			private static readonly int DefaultThrottlingLogBufferSize = 65536;

			// Token: 0x04000DD7 RID: 3543
			private static readonly TimeSpan DefaultThrottlingLogFlushInterval = TimeSpan.FromSeconds(60.0);

			// Token: 0x04000DD8 RID: 3544
			private static readonly TimeSpan DefaultThrottlingLogAsyncLogInterval = TimeSpan.FromSeconds(15.0);

			// Token: 0x04000DD9 RID: 3545
			private static readonly TimeSpan DefaultThrottlingLogSummaryLoggingInterval = TimeSpan.FromMinutes(15.0);

			// Token: 0x04000DDA RID: 3546
			private static readonly ByteQuantifiedSize DefaultMaxMailboxDeliveryConcurrentMessageSizeLimit = ByteQuantifiedSize.FromMB(512UL);

			// Token: 0x04000DDB RID: 3547
			private static readonly int DefaultMaxStoreDriverDeliveryExceptionCallstackHistoryPerBucket = 20;

			// Token: 0x04000DDC RID: 3548
			private static readonly int DefaultMaxStoreDriverDeliveryExceptionOccurrenceHistoryPerException = 10;

			// Token: 0x04000DDD RID: 3549
			private static readonly string DefaultStoreDriverExceptionCallstackToTrap = null;

			// Token: 0x04000DDE RID: 3550
			private static readonly int DefaultMdbHealthMediumToHighThreshold = 67;

			// Token: 0x04000DDF RID: 3551
			private static readonly int DefaultMdbHealthLowToMediumThreshold = 34;

			// Token: 0x04000DE0 RID: 3552
			private static readonly bool DefaultMailboxDeliveryThrottlingEnabled = true;

			// Token: 0x04000DE1 RID: 3553
			private static readonly bool DefaultMailboxTransportTableBasedExceptionHandlerEnabled = true;

			// Token: 0x04000DE2 RID: 3554
			private static readonly TimeSpan DefaultQuarantinedMailboxRetryInterval = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000DE3 RID: 3555
			private static readonly TimeSpan DefaultQueueGlithRetryInterval = TimeSpan.FromMinutes(1.0);

			// Token: 0x04000DE4 RID: 3556
			private static readonly TimeSpan DefaultHighPriorityMessageExpirationTimeout = TimeSpan.FromHours(8.0);

			// Token: 0x04000DE5 RID: 3557
			private static readonly TimeSpan DefaultNormalPriorityMessageExpirationTimeout = TimeSpan.FromDays(2.0);

			// Token: 0x04000DE6 RID: 3558
			private static readonly TimeSpan DefaultLowPriorityMessageExpirationTimeout = TimeSpan.FromDays(2.0);

			// Token: 0x04000DE7 RID: 3559
			private static readonly TimeSpan DefaultHighPriorityDelayNotificationTimeout = TimeSpan.FromMinutes(30.0);

			// Token: 0x04000DE8 RID: 3560
			private static readonly TimeSpan DefaultNormalPriorityDelayNotificationTimeout = TimeSpan.FromHours(4.0);

			// Token: 0x04000DE9 RID: 3561
			private static readonly TimeSpan DefaultLowPriorityDelayNotificationTimeout = TimeSpan.FromHours(8.0);

			// Token: 0x04000DEA RID: 3562
			private static readonly ByteQuantifiedSize DefaultMaxHighPriorityMessageSize = ByteQuantifiedSize.FromKB(250UL);

			// Token: 0x04000DEB RID: 3563
			private static readonly TimeSpan DefaultStoreDriverRecipientDeliveryHangThreshold = TimeSpan.FromMinutes(20.0);

			// Token: 0x04000DEC RID: 3564
			private static readonly TimeSpan DefaultStoreDriverDeliveryHangDetectionInterval = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000DED RID: 3565
			private static readonly TimeSpan DefaultDnsRequestTimeout = TimeSpan.FromMinutes(1.0);

			// Token: 0x04000DEE RID: 3566
			private static readonly TimeSpan DefaultDnsQueryRetryInterval = TimeSpan.FromSeconds(5.0);

			// Token: 0x04000DEF RID: 3567
			private static readonly TimeSpan DefaultResubmitDueToOutboundConnectorChangeInterval = TimeSpan.FromMinutes(15.0);

			// Token: 0x04000DF0 RID: 3568
			private static readonly TimeSpan DefaultOutboundConnectorLookbackBufferInterval = TimeSpan.FromMinutes(2.0);

			// Token: 0x04000DF1 RID: 3569
			private static readonly List<int> DefaultInternalMessageRetryIntervalRangeList = new List<int>
			{
				5,
				15,
				25,
				35,
				295,
				305,
				800,
				1000
			};

			// Token: 0x04000DF2 RID: 3570
			private static readonly List<int> DefaultExternalMessageRetryIntervalRangeList = new List<int>
			{
				840,
				960,
				1020,
				1140,
				3540,
				3660
			};

			// Token: 0x04000DF3 RID: 3571
			private int[] deliveryPriorityQuotas = new int[4];

			// Token: 0x04000DF4 RID: 3572
			private TimeSpan maxIdleTimeBeforeResubmission = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxIdleTimeBeforeResubmit;

			// Token: 0x04000DF5 RID: 3573
			private TimeSpan mailboxDeliveryQueueRetryInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDeliveryQueueRetryInterval;

			// Token: 0x04000DF6 RID: 3574
			private TimeSpan mailboxDeliveryFastQueueRetryInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDeliveryFastQueueRetryInterval;

			// Token: 0x04000DF7 RID: 3575
			private TimeSpan mailboxServerThreadLimitQueueRetryInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxServerThreadLimitQueueRetryInterval;

			// Token: 0x04000DF8 RID: 3576
			private TimeSpan mailboxDatabaseThreadLimitQueueRetryInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDatabaseThreadLimitQueueRetryInterval;

			// Token: 0x04000DF9 RID: 3577
			private int maxMailboxDeliveryPerMdbConnections = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnections;

			// Token: 0x04000DFA RID: 3578
			private int maxMailboxDeliveryPerMdbConnectionsHighHealthPercent = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsHighHealthPercent;

			// Token: 0x04000DFB RID: 3579
			private int maxMailboxDeliveryPerMdbConnectionsMediumHealthPercent = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent;

			// Token: 0x04000DFC RID: 3580
			private int maxMailboxDeliveryPerMdbConnectionsLowHealthPercent = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsLowHealthPercent;

			// Token: 0x04000DFD RID: 3581
			private int maxMailboxDeliveryPerMdbConnectionsLowestHealthPercent = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent;

			// Token: 0x04000DFE RID: 3582
			private bool dynamicMailboxDatabaseThrottlingEnabled = TransportAppConfig.RemoteDeliveryConfig.DefaultDynamicMailboxDatabaseThrottlingEnabled;

			// Token: 0x04000DFF RID: 3583
			private int mailboxDeliveryThrottlingLogBufferSize = TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogBufferSize;

			// Token: 0x04000E00 RID: 3584
			private TimeSpan mailboxDeliveryThrottlingLogFlushInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogFlushInterval;

			// Token: 0x04000E01 RID: 3585
			private TimeSpan mailboxDeliveryThrottlingLogAsyncLogInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogAsyncLogInterval;

			// Token: 0x04000E02 RID: 3586
			private TimeSpan mailboxDeliveryThrottlingLogSummaryLoggingInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultThrottlingLogSummaryLoggingInterval;

			// Token: 0x04000E03 RID: 3587
			private ByteQuantifiedSize maxMailboxDeliveryConcurrentMessageSizeLimit = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxMailboxDeliveryConcurrentMessageSizeLimit;

			// Token: 0x04000E04 RID: 3588
			private int maxStoreDriverDeliveryExceptionCallstackHistoryPerBucket = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxStoreDriverDeliveryExceptionCallstackHistoryPerBucket;

			// Token: 0x04000E05 RID: 3589
			private int maxStoreDriverDeliveryExceptionOccurrenceHistoryPerException = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxStoreDriverDeliveryExceptionOccurrenceHistoryPerException;

			// Token: 0x04000E06 RID: 3590
			private string storeDriverExceptionCallstackToTrap = TransportAppConfig.RemoteDeliveryConfig.DefaultStoreDriverExceptionCallstackToTrap;

			// Token: 0x04000E07 RID: 3591
			private int mdbHealthMediumToHighThreshold = TransportAppConfig.RemoteDeliveryConfig.DefaultMdbHealthMediumToHighThreshold;

			// Token: 0x04000E08 RID: 3592
			private int mdbHealthLowToMediumThreshold = TransportAppConfig.RemoteDeliveryConfig.DefaultMdbHealthLowToMediumThreshold;

			// Token: 0x04000E09 RID: 3593
			private bool mailboxDeliveryThrottlingEnabled = TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxDeliveryThrottlingEnabled;

			// Token: 0x04000E0A RID: 3594
			private bool mailboxTransportTableBasedExceptionHandlerEnabled = TransportAppConfig.RemoteDeliveryConfig.DefaultMailboxTransportTableBasedExceptionHandlerEnabled;

			// Token: 0x04000E0B RID: 3595
			private TimeSpan quarantinedMailboxRetryInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultQuarantinedMailboxRetryInterval;

			// Token: 0x04000E0C RID: 3596
			private TimeSpan queueGlitchRetryInterval = TransportAppConfig.RemoteDeliveryConfig.DefaultQueueGlithRetryInterval;

			// Token: 0x04000E0D RID: 3597
			private int queueGlitchRetryCount = 4;

			// Token: 0x04000E0E RID: 3598
			private int maxQueryResultCount = 50000;

			// Token: 0x04000E0F RID: 3599
			private DnsFaultTolerance dnsFaultTolerance;

			// Token: 0x04000E10 RID: 3600
			private int messageThresholdToUpdateHealthCounters = 100;

			// Token: 0x04000E11 RID: 3601
			private TimeSpan refreshIntervalToUpdateHealth = TransportAppConfig.RemoteDeliveryConfig.DefaultRefreshIntervalForUpdatingHealth;

			// Token: 0x04000E12 RID: 3602
			private bool loadBalancingForServerFailoverEnabled;

			// Token: 0x04000E13 RID: 3603
			private bool priorityQueuingEnabled;

			// Token: 0x04000E14 RID: 3604
			private bool localDeliveryPriorityQueuingEnabled;

			// Token: 0x04000E15 RID: 3605
			private bool remoteDeliveryPriorityQueuingEnabled;

			// Token: 0x04000E16 RID: 3606
			private int[] maxPerDomainPriorityConnections;

			// Token: 0x04000E17 RID: 3607
			private TimeSpan[] messageExpirationTimeout;

			// Token: 0x04000E18 RID: 3608
			private TimeSpan[] delayNotificationTimeout;

			// Token: 0x04000E19 RID: 3609
			private ByteQuantifiedSize maxHighPriorityMessageSize = TransportAppConfig.RemoteDeliveryConfig.DefaultMaxHighPriorityMessageSize;

			// Token: 0x04000E1A RID: 3610
			private int mailboxServerThreadLimit = 120;

			// Token: 0x04000E1B RID: 3611
			private int recipientThreadLimit = 3;

			// Token: 0x04000E1C RID: 3612
			private int deliverySourceThreadLimitPerCore = 5;

			// Token: 0x04000E1D RID: 3613
			private int mailboxDeliveryMaxMessagesPerConnection = 20;

			// Token: 0x04000E1E RID: 3614
			private TimeSpan storeDriverRecipientDeliveryHangThreshold;

			// Token: 0x04000E1F RID: 3615
			private TimeSpan storeDriverDeliveryHangDetectionInterval;

			// Token: 0x04000E20 RID: 3616
			private bool excludeDnsServersFromLoopbackAdapters;

			// Token: 0x04000E21 RID: 3617
			private bool excludeIPv6SiteLocalDnsAddresses;

			// Token: 0x04000E22 RID: 3618
			private TimeSpan dnsRequestTimeout;

			// Token: 0x04000E23 RID: 3619
			private TimeSpan dnsQueryRetryInterval;

			// Token: 0x04000E24 RID: 3620
			private TimeSpan configUpdateResubmitDeferInterval;

			// Token: 0x04000E25 RID: 3621
			private bool dnsIpv6Enabled;

			// Token: 0x04000E26 RID: 3622
			private int mailboxQueueMessageCountThresholdForConcurrentConnections;

			// Token: 0x04000E27 RID: 3623
			private int smtpConnectorQueueMessageCountThresholdForConcurrentConnections;

			// Token: 0x04000E28 RID: 3624
			private int intraorgSmtpQueueMessageCountThresholdForConcurrentConnections;

			// Token: 0x04000E29 RID: 3625
			private int otherQueueMessageCountThresholdForConcurrentConnections;

			// Token: 0x04000E2A RID: 3626
			private int deprioritizeOnRecipientThreadLimitExceededCount;

			// Token: 0x04000E2B RID: 3627
			private bool messageRetryIntervalProgressiveBackoffEnabled;

			// Token: 0x04000E2C RID: 3628
			private List<int> internalMessageRetryIntervalRangeList;

			// Token: 0x04000E2D RID: 3629
			private List<int> externalMessageRetryIntervalRangeList;

			// Token: 0x04000E2E RID: 3630
			private TimeSpan resubmitDueToOutboundConnectorChangeInterval;

			// Token: 0x04000E2F RID: 3631
			private TimeSpan outboundConnectorLookbackBufferInterval;

			// Token: 0x04000E30 RID: 3632
			private Random random = new Random();
		}

		// Token: 0x020002A3 RID: 675
		public class MapiSubmissionConfig
		{
			// Token: 0x06001D31 RID: 7473 RVA: 0x000754B2 File Offset: 0x000736B2
			private MapiSubmissionConfig()
			{
			}

			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x06001D32 RID: 7474 RVA: 0x000754CA File Offset: 0x000736CA
			public int MaxConcurrentSubmissionsPerMailboxServer
			{
				get
				{
					return this.maxConcurrentSubmissionsPerMailboxServer;
				}
			}

			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x06001D33 RID: 7475 RVA: 0x000754D2 File Offset: 0x000736D2
			public int MaxConcurrentSubmissionsPerMailboxDatabase
			{
				get
				{
					return this.maxConcurrentSubmissionsPerMailboxDatabase;
				}
			}

			// Token: 0x17000832 RID: 2098
			// (get) Token: 0x06001D34 RID: 7476 RVA: 0x000754DA File Offset: 0x000736DA
			public int MaxStoreDriverSubmissionExceptionOccurrenceHistoryPerException
			{
				get
				{
					return this.maxStoreDriverSubmissionExceptionOccurrenceHistoryPerException;
				}
			}

			// Token: 0x17000833 RID: 2099
			// (get) Token: 0x06001D35 RID: 7477 RVA: 0x000754E2 File Offset: 0x000736E2
			public int MaxStoreDriverSubmissionExceptionCallstackHistoryPerBucket
			{
				get
				{
					return this.maxStoreDriverSubmissionExceptionCallstackHistoryPerBucket;
				}
			}

			// Token: 0x06001D36 RID: 7478 RVA: 0x000754EC File Offset: 0x000736EC
			public static TransportAppConfig.MapiSubmissionConfig Load()
			{
				return new TransportAppConfig.MapiSubmissionConfig
				{
					maxConcurrentSubmissionsPerMailboxServer = TransportAppConfig.GetConfigInt("MaxConcurrentSubmissionsPerMailboxServer", 0, int.MaxValue, 12),
					maxConcurrentSubmissionsPerMailboxDatabase = TransportAppConfig.GetConfigInt("MaxConcurrentSubmissionsPerMailboxDatabase", 0, int.MaxValue, 4),
					maxStoreDriverSubmissionExceptionOccurrenceHistoryPerException = TransportAppConfig.GetConfigInt("MaxStoreDriverSubmissionExceptionOccurrenceHistoryPerException", 0, int.MaxValue, 10),
					maxStoreDriverSubmissionExceptionCallstackHistoryPerBucket = TransportAppConfig.GetConfigInt("MaxStoreDriverSubmissionExceptionCallstackHistoryPerBucket", 0, int.MaxValue, 10)
				};
			}

			// Token: 0x04000E31 RID: 3633
			private const int DefaultMaxConcurrentSubmissionsPerMailboxServer = 12;

			// Token: 0x04000E32 RID: 3634
			private const int DefaultMaxConcurrentSubmissionsPerMailboxDatabase = 4;

			// Token: 0x04000E33 RID: 3635
			private const int DefaultMaxStoreDriverSubmissionExceptionOccurrenceHistoryPerException = 10;

			// Token: 0x04000E34 RID: 3636
			private const int DefaultMaxStoreDriverSubmissionExceptionCallstackHistoryPerBucket = 20;

			// Token: 0x04000E35 RID: 3637
			private int maxConcurrentSubmissionsPerMailboxServer;

			// Token: 0x04000E36 RID: 3638
			private int maxConcurrentSubmissionsPerMailboxDatabase;

			// Token: 0x04000E37 RID: 3639
			private int maxStoreDriverSubmissionExceptionOccurrenceHistoryPerException = 10;

			// Token: 0x04000E38 RID: 3640
			private int maxStoreDriverSubmissionExceptionCallstackHistoryPerBucket = 20;
		}

		// Token: 0x020002A4 RID: 676
		public class ResolverConfig
		{
			// Token: 0x06001D37 RID: 7479 RVA: 0x0007555F File Offset: 0x0007375F
			private ResolverConfig()
			{
			}

			// Token: 0x17000834 RID: 2100
			// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0007559B File Offset: 0x0007379B
			public bool IsResolverEnabled
			{
				get
				{
					return this.isResolverEnabled;
				}
			}

			// Token: 0x17000835 RID: 2101
			// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000755A3 File Offset: 0x000737A3
			public int ExpansionSizeLimit
			{
				get
				{
					return this.expansionSizeLimit;
				}
			}

			// Token: 0x17000836 RID: 2102
			// (get) Token: 0x06001D3A RID: 7482 RVA: 0x000755AB File Offset: 0x000737AB
			public ResolverLogLevel ResolverLogLevel
			{
				get
				{
					return this.resolverLogLevel;
				}
			}

			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000755B3 File Offset: 0x000737B3
			public int BatchLookupRecipientCount
			{
				get
				{
					return this.batchLookupRecipientCount;
				}
			}

			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x06001D3C RID: 7484 RVA: 0x000755BB File Offset: 0x000737BB
			public double ResolverRetryInterval
			{
				get
				{
					return this.resolverRetryInterval;
				}
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06001D3D RID: 7485 RVA: 0x000755C3 File Offset: 0x000737C3
			public double DeliverMoveMailboxRetryInterval
			{
				get
				{
					return this.deliverMoveMailboxRetryInterval;
				}
			}

			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06001D3E RID: 7486 RVA: 0x000755CB File Offset: 0x000737CB
			public int MaxExecutingJobs
			{
				get
				{
					return this.maxExecutingJobs;
				}
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000755D3 File Offset: 0x000737D3
			public int MaxJobThreads
			{
				get
				{
					return this.maxJobThreads;
				}
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000755DB File Offset: 0x000737DB
			public int MaxJobsPerThread
			{
				get
				{
					return this.maxJobsPerThread;
				}
			}

			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x06001D41 RID: 7489 RVA: 0x000755E3 File Offset: 0x000737E3
			public TimeSpan JobHealthTimeThreshold
			{
				get
				{
					return this.jobHealthTimeThreshold;
				}
			}

			// Token: 0x1700083E RID: 2110
			// (get) Token: 0x06001D42 RID: 7490 RVA: 0x000755EB File Offset: 0x000737EB
			public TimeSpan JobHealthUpdateInterval
			{
				get
				{
					return this.jobHealthUpdateInterval;
				}
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x06001D43 RID: 7491 RVA: 0x000755F3 File Offset: 0x000737F3
			public bool EnableForwardingProhibitedFeature
			{
				get
				{
					return this.enableForwardingProhibitedFeature;
				}
			}

			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000755FB File Offset: 0x000737FB
			public int MaxResolveRecipientCacheSize
			{
				get
				{
					return this.maxResolveRecipientCacheSize;
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00075603 File Offset: 0x00073803
			public int MaxResolverMemberOfGroupCacheSize
			{
				get
				{
					return this.maxResolverMemberOfGroupCacheSize;
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0007560B File Offset: 0x0007380B
			public TimeSpan AcceptedDomainReloadInterval
			{
				get
				{
					return this.acceptedDomainReloadInterval;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00075613 File Offset: 0x00073813
			public int AcceptedDomainReloadLoggingThreshold
			{
				get
				{
					return this.acceptedDomainReloadLoggingThreshold;
				}
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0007561B File Offset: 0x0007381B
			public bool NDRForAmbiguousRecipients
			{
				get
				{
					return this.ndrForAmbiguousRecipients;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00075623 File Offset: 0x00073823
			public TimeSpan DeferralTimeForAmbiguousRecipients
			{
				get
				{
					return this.deferralTimeForAmbiguousRecipients;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0007562B File Offset: 0x0007382B
			public bool LargeDGLimitEnforcementEnabled
			{
				get
				{
					return this.largeDGLimitEnforcementEnabled;
				}
			}

			// Token: 0x17000847 RID: 2119
			// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00075633 File Offset: 0x00073833
			public ByteQuantifiedSize LargeDGMaxMessageSize
			{
				get
				{
					return this.largeDGMaxMessageSize;
				}
			}

			// Token: 0x17000848 RID: 2120
			// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0007563B File Offset: 0x0007383B
			public int LargeDGGroupCount
			{
				get
				{
					return this.largeDGGroupCount;
				}
			}

			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00075643 File Offset: 0x00073843
			public int LargeDGGroupCountForUnRestrictedDG
			{
				get
				{
					return this.largeDGGroupCountForUnRestrictedDG;
				}
			}

			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0007564B File Offset: 0x0007384B
			public bool ForceNdrForDlRestrictionError
			{
				get
				{
					return this.forceNdrForDlRestrictionError;
				}
			}

			// Token: 0x06001D4F RID: 7503 RVA: 0x00075654 File Offset: 0x00073854
			public static TransportAppConfig.ResolverConfig Load()
			{
				TransportAppConfig.ResolverConfig resolverConfig = new TransportAppConfig.ResolverConfig();
				resolverConfig.resolverRetryInterval = TransportAppConfig.GetConfigDouble("ResolverRetryInterval", 0.0, double.MaxValue, 30.0);
				resolverConfig.batchLookupRecipientCount = TransportAppConfig.GetConfigInt("BatchLookupRecipientCount", 0, 50, 20);
				resolverConfig.deliverMoveMailboxRetryInterval = TransportAppConfig.GetConfigDouble("DeliverMoveMailboxRetryInterval", 0.0, double.MaxValue, 2.0);
				resolverConfig.resolverLogLevel = TransportAppConfig.ResolverConfig.GetResolverLogLevel();
				resolverConfig.expansionSizeLimit = TransportAppConfig.GetConfigInt("ExpansionSizeLimit", 1, int.MaxValue, 1000);
				resolverConfig.enableForwardingProhibitedFeature = TransportAppConfig.GetConfigBool("EnableForwardingProhibitedFeature", false);
				resolverConfig.isResolverEnabled = TransportAppConfig.GetConfigBool("isResolverEnabled", false);
				int processorCount = Environment.ProcessorCount;
				resolverConfig.maxExecutingJobs = TransportAppConfig.GetConfigInt("MaxExecutingJobs", 1, 50, 6) * processorCount;
				resolverConfig.maxJobThreads = TransportAppConfig.GetConfigInt("MaxJobThreads", 1, 25, 3) * processorCount;
				resolverConfig.maxJobsPerThread = TransportAppConfig.GetConfigInt("MaxJobsPerThread", 1, int.MaxValue, 1);
				resolverConfig.jobHealthTimeThreshold = TransportAppConfig.GetConfigTimeSpan("JobHealthTimeThreshold", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(15.0));
				resolverConfig.jobHealthUpdateInterval = TransportAppConfig.GetConfigTimeSpan("JobHealthUpdateInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromSeconds(30.0));
				resolverConfig.maxResolveRecipientCacheSize = TransportAppConfig.GetConfigInt("MaxResolveRecipientCacheSize", 0, int.MaxValue, 100000);
				resolverConfig.maxResolverMemberOfGroupCacheSize = TransportAppConfig.GetConfigInt("MaxResolverMemberOfGroupCacheSize", 0, int.MaxValue, 100000);
				resolverConfig.acceptedDomainReloadInterval = TransportAppConfig.GetConfigTimeSpan("AcceptedDomainForcedReloadInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(2.0));
				resolverConfig.acceptedDomainReloadLoggingThreshold = TransportAppConfig.GetConfigInt("AcceptedDomainForcedReloadLoggingThreshold", 0, int.MaxValue, 0);
				resolverConfig.ndrForAmbiguousRecipients = TransportAppConfig.GetConfigBool("NDRForAmbiguousRecipients", false);
				resolverConfig.deferralTimeForAmbiguousRecipients = TransportAppConfig.GetConfigTimeSpan("DeferralTimeForAmbiguousRecipients", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(6.0), TimeSpan.FromMinutes(30.0));
				resolverConfig.largeDGLimitEnforcementEnabled = TransportAppConfig.GetConfigBool("LargeDGLimitEnforcementEnabled", false);
				resolverConfig.forceNdrForDlRestrictionError = TransportAppConfig.GetConfigBool("ForceNdrForDlRestrictionError", true);
				if (resolverConfig.largeDGLimitEnforcementEnabled)
				{
					resolverConfig.largeDGMaxMessageSize = TransportAppConfig.GetConfigByteQuantifiedSize("LargeDGMaxMessageSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(2UL));
					resolverConfig.largeDGGroupCount = TransportAppConfig.GetConfigInt("LargeDGGroupCount", 0, int.MaxValue, 5000);
					resolverConfig.largeDGGroupCountForUnRestrictedDG = TransportAppConfig.GetConfigInt("LargeDGGroupCountForUnRestrictedDG", 0, int.MaxValue, 5000);
				}
				return resolverConfig;
			}

			// Token: 0x06001D50 RID: 7504 RVA: 0x00075914 File Offset: 0x00073B14
			private static ResolverLogLevel GetResolverLogLevel()
			{
				string value = ConfigurationManager.AppSettings["ResolverLogLevel"];
				if (string.IsNullOrEmpty(value))
				{
					return ResolverLogLevel.Disabled;
				}
				ResolverLogLevel result;
				try
				{
					result = (ResolverLogLevel)Enum.Parse(typeof(ResolverLogLevel), value, true);
				}
				catch (ArgumentException)
				{
					return ResolverLogLevel.Disabled;
				}
				return result;
			}

			// Token: 0x04000E39 RID: 3641
			private const double DefaultRetryInterval = 30.0;

			// Token: 0x04000E3A RID: 3642
			private const double DefaultMoveMailboxRetryInterval = 2.0;

			// Token: 0x04000E3B RID: 3643
			private const int DefaultMaxResolveRecipientCacheSize = 100000;

			// Token: 0x04000E3C RID: 3644
			private const int DefaultMaxResolverMemberOfGroupCacheSize = 100000;

			// Token: 0x04000E3D RID: 3645
			private bool isResolverEnabled;

			// Token: 0x04000E3E RID: 3646
			private int expansionSizeLimit;

			// Token: 0x04000E3F RID: 3647
			private ResolverLogLevel resolverLogLevel;

			// Token: 0x04000E40 RID: 3648
			private int batchLookupRecipientCount;

			// Token: 0x04000E41 RID: 3649
			private double resolverRetryInterval = 30.0;

			// Token: 0x04000E42 RID: 3650
			private double deliverMoveMailboxRetryInterval = 30.0;

			// Token: 0x04000E43 RID: 3651
			private int maxExecutingJobs;

			// Token: 0x04000E44 RID: 3652
			private int maxJobThreads;

			// Token: 0x04000E45 RID: 3653
			private int maxJobsPerThread;

			// Token: 0x04000E46 RID: 3654
			private TimeSpan jobHealthTimeThreshold;

			// Token: 0x04000E47 RID: 3655
			private TimeSpan jobHealthUpdateInterval;

			// Token: 0x04000E48 RID: 3656
			private bool enableForwardingProhibitedFeature;

			// Token: 0x04000E49 RID: 3657
			private int maxResolveRecipientCacheSize = 100000;

			// Token: 0x04000E4A RID: 3658
			private int maxResolverMemberOfGroupCacheSize = 100000;

			// Token: 0x04000E4B RID: 3659
			private TimeSpan acceptedDomainReloadInterval;

			// Token: 0x04000E4C RID: 3660
			private int acceptedDomainReloadLoggingThreshold;

			// Token: 0x04000E4D RID: 3661
			private bool ndrForAmbiguousRecipients;

			// Token: 0x04000E4E RID: 3662
			private TimeSpan deferralTimeForAmbiguousRecipients;

			// Token: 0x04000E4F RID: 3663
			private ByteQuantifiedSize largeDGMaxMessageSize;

			// Token: 0x04000E50 RID: 3664
			private int largeDGGroupCount;

			// Token: 0x04000E51 RID: 3665
			private int largeDGGroupCountForUnRestrictedDG;

			// Token: 0x04000E52 RID: 3666
			private bool largeDGLimitEnforcementEnabled;

			// Token: 0x04000E53 RID: 3667
			private bool forceNdrForDlRestrictionError;
		}

		// Token: 0x020002A5 RID: 677
		public class RoutingConfig
		{
			// Token: 0x06001D51 RID: 7505 RVA: 0x0007596C File Offset: 0x00073B6C
			protected RoutingConfig()
			{
				this.configReloadInterval = TransportAppConfig.RoutingConfig.GetConfigReloadInterval();
				this.databaseFullReloadInterval = TransportAppConfig.RoutingConfig.GetDatabaseFullReloadInterval();
				this.deferredReloadInterval = TransportAppConfig.RoutingConfig.GetDeferredReloadInterval();
				this.maxDeferredNotifications = TransportAppConfig.RoutingConfig.GetMaxDeferredNotifications();
				this.minConfigReloadInterval = TransportAppConfig.GetConfigTimeSpan("MinRoutingConfigReloadInterval", TimeSpan.Zero, TimeSpan.FromHours(12.0), TimeSpan.FromMinutes(5.0));
				this.pfReplicaAgeThreshold = TransportAppConfig.RoutingConfig.GetPfReplicaAgeThreshold();
				this.disableExchangeServerAuth = TransportAppConfig.RoutingConfig.GetDisableExchangeServerAuth();
				this.localLoopMessageDeferralIntervals = TransportAppConfig.RoutingConfig.GetLocalLoopMessageDeferralIntervals();
				this.localLoopSubdomainDepth = TransportAppConfig.RoutingConfig.GetLocalLoopSubdomainDepth();
				this.localLoopDetectionEnabled = TransportAppConfig.RoutingConfig.GetLocalLoopDetectionEnabled();
				this.loopDetectionNumberOfTransits = TransportAppConfig.RoutingConfig.GetLoopDetectionNumberOfTransits();
				this.localLoopDetectionSubDomainLeftToRightOffsetForPerfCounter = TransportAppConfig.RoutingConfig.GetLocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter();
				this.maxAllowedCategorizerResubmits = TransportAppConfig.GetConfigInt("MaxAllowedCategorizerResubmits", 2, int.MaxValue, 1000);
				this.destinationRoutingToRemoteSitesEnabled = TransportAppConfig.GetConfigBool("DestinationRoutingToRemoteSitesEnabled", true);
				this.dagRoutingEnabled = TransportAppConfig.GetConfigBool("DagRoutingEnabled", true);
				this.smtpDeliveryToMailboxEnabled = TransportAppConfig.GetConfigBool("SmtpDeliveryToMailboxEnabled", true);
				this.proxyRoutingAllowedTargetVersions = TransportAppConfig.GetConfigList<int>("ProxyRoutingAllowedTargetVersions", ',', new TransportAppConfig.TryParse<int>(int.TryParse));
				this.proxyRoutingMaxTotalHubCount = TransportAppConfig.GetConfigInt("ProxyRoutingMaxTotalHubCount", 1, int.MaxValue, 10);
				this.proxyRoutingMaxRemoteSiteHubCount = TransportAppConfig.GetConfigInt("ProxyRoutingMaxRemoteSiteHubCount", 0, int.MaxValue, 4);
				this.proxyRoutingServerSelectStrategy = TransportAppConfig.GetConfigEnum<RoutedServerSelectStrategy>("ProxyRoutingServerSelectStrategy", RoutedServerSelectStrategy.FavorCloserProximity);
				this.outboundFrontendServers = TransportAppConfig.GetConfigList<RoutingHost>("OutboundFrontendServers", ',', new TransportAppConfig.TryParse<RoutingHost>(RoutingHost.TryParse));
				this.externalOutboundFrontendProxyEnabled = TransportAppConfig.GetConfigBool("ExternalOutboundFrontendProxyEnabled", false);
				this.outboundProxyRoutingXVersionEnabled = TransportAppConfig.GetConfigBool("OutboundProxyRoutingXVersionEnabled", true);
				this.routingToNonActiveServersEnabled = TransportAppConfig.GetConfigBool("RoutingToNonActiveServersEnabled", false);
				this.randomLoadBalancingOffsetEnabled = TransportAppConfig.GetConfigBool("RandomLoadBalancingOffsetEnabled", true);
				this.maxDeferCountForRecipientHasNoMdb = TransportAppConfig.GetConfigInt("MaxDeferCountForRecipientHasNoMdb", 0, 1000, 2);
				this.routingTopologyCacheEnabled = TransportAppConfig.GetConfigBool("RoutingTopologyCacheEnabled", true);
				this.deferralTimeForNoMdb = TransportAppConfig.RoutingConfig.GetDeferralTimeForNoMdb();
				this.dagSelectorEnabled = TransportAppConfig.GetConfigBool("DagSelectorEnabled", false);
				this.dagSelectorIncrementMessageThresholdFactor = TransportAppConfig.GetConfigDouble("DagSelectorIncrementMessageThresholdFactor", 0.0, 1.0, 0.25);
				this.dagSelectorMessageThresholdPerServer = TransportAppConfig.GetConfigInt("DagSelectorMessageThresholdPerServer", 1, int.MaxValue, 125);
				this.dagSelectorActiveServersForDagToBeRoutable = TransportAppConfig.GetConfigInt("DagSelectorActiveServersForDagToBeRoutable", 1, 16, 2);
				this.dagSelectorMinimumSitesForDagToBeRoutable = TransportAppConfig.GetConfigInt("DagSelectorMinimumSitesForDagToBeRoutable", 1, 4, 2);
				this.tenantDagQuotaMessageThreshold = TransportAppConfig.GetConfigInt("TenantDagQuotaMessageThreshold", 1, int.MaxValue, 50);
				this.tenantDagQuotaDagsPerTenant = TransportAppConfig.GetConfigInt("TenantDagQuotaDagsPerTenant", 1, int.MaxValue, 3);
				this.tenantDagQuotaPastWeight = TransportAppConfig.GetConfigDouble("TenantDagQuotaPastWeight", 0.0, 1.0, 0.5);
				this.checkDagSelectorHeader = TransportAppConfig.GetConfigBool("CheckDagSelectorHeaderEnabled", false);
				this.logDagSelectorDiagnosticInfo = TransportAppConfig.GetConfigBool("DagSelectorLogDiagnosticInfo", true);
			}

			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00075C7E File Offset: 0x00073E7E
			public bool DagSelectorEnabled
			{
				get
				{
					return this.dagSelectorEnabled && this.dagRoutingEnabled;
				}
			}

			// Token: 0x1700084C RID: 2124
			// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00075C90 File Offset: 0x00073E90
			public int DagSelectorMessageThresholdPerServer
			{
				get
				{
					return this.dagSelectorMessageThresholdPerServer;
				}
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00075C98 File Offset: 0x00073E98
			public int DagSelectorActiveServersForDagToBeRoutable
			{
				get
				{
					return this.dagSelectorActiveServersForDagToBeRoutable;
				}
			}

			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00075CA0 File Offset: 0x00073EA0
			public int DagSelectorMinimumSitesForDagToBeRoutable
			{
				get
				{
					return this.dagSelectorMinimumSitesForDagToBeRoutable;
				}
			}

			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00075CA8 File Offset: 0x00073EA8
			public double DagSelectorIncrementMessageThresholdFactor
			{
				get
				{
					return this.dagSelectorIncrementMessageThresholdFactor;
				}
			}

			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00075CB0 File Offset: 0x00073EB0
			public int TenantDagQuotaMessagesPerDag
			{
				get
				{
					return this.tenantDagQuotaMessageThreshold;
				}
			}

			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00075CB8 File Offset: 0x00073EB8
			public int TenantDagQuotaDagsPerTenant
			{
				get
				{
					return this.tenantDagQuotaDagsPerTenant;
				}
			}

			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00075CC0 File Offset: 0x00073EC0
			public double TenantDagQuotaPastWeight
			{
				get
				{
					return this.tenantDagQuotaPastWeight;
				}
			}

			// Token: 0x17000853 RID: 2131
			// (get) Token: 0x06001D5A RID: 7514 RVA: 0x00075CC8 File Offset: 0x00073EC8
			public bool CheckDagSelectorHeader
			{
				get
				{
					return this.checkDagSelectorHeader;
				}
			}

			// Token: 0x17000854 RID: 2132
			// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00075CD0 File Offset: 0x00073ED0
			public bool DagSelectorLogDiagnosticInfo
			{
				get
				{
					return this.logDagSelectorDiagnosticInfo;
				}
			}

			// Token: 0x17000855 RID: 2133
			// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00075CD8 File Offset: 0x00073ED8
			public TimeSpan ConfigReloadInterval
			{
				get
				{
					return this.configReloadInterval;
				}
			}

			// Token: 0x17000856 RID: 2134
			// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00075CE0 File Offset: 0x00073EE0
			public TimeSpan DatabaseFullReloadInterval
			{
				get
				{
					return this.databaseFullReloadInterval;
				}
			}

			// Token: 0x17000857 RID: 2135
			// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00075CE8 File Offset: 0x00073EE8
			public TimeSpan DeferredReloadInterval
			{
				get
				{
					return this.deferredReloadInterval;
				}
			}

			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00075CF0 File Offset: 0x00073EF0
			public TimeSpan MinConfigReloadInterval
			{
				get
				{
					return this.minConfigReloadInterval;
				}
			}

			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00075CF8 File Offset: 0x00073EF8
			public int MaxDeferredNotifications
			{
				get
				{
					return this.maxDeferredNotifications;
				}
			}

			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00075D00 File Offset: 0x00073F00
			public int LoopDetectionNumberOfTransits
			{
				get
				{
					return this.loopDetectionNumberOfTransits;
				}
			}

			// Token: 0x1700085B RID: 2139
			// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00075D08 File Offset: 0x00073F08
			public int LocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter
			{
				get
				{
					return this.localLoopDetectionSubDomainLeftToRightOffsetForPerfCounter;
				}
			}

			// Token: 0x1700085C RID: 2140
			// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00075D10 File Offset: 0x00073F10
			public TimeSpan PfReplicaAgeThreshold
			{
				get
				{
					return this.pfReplicaAgeThreshold;
				}
			}

			// Token: 0x1700085D RID: 2141
			// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00075D18 File Offset: 0x00073F18
			public int[] LocalLoopMessageDeferralIntervals
			{
				get
				{
					return this.localLoopMessageDeferralIntervals;
				}
			}

			// Token: 0x1700085E RID: 2142
			// (get) Token: 0x06001D65 RID: 7525 RVA: 0x00075D20 File Offset: 0x00073F20
			public int LocalLoopSubdomainDepth
			{
				get
				{
					return this.localLoopSubdomainDepth;
				}
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00075D28 File Offset: 0x00073F28
			public bool LocalLoopDetectionEnabled
			{
				get
				{
					return this.localLoopDetectionEnabled;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x06001D67 RID: 7527 RVA: 0x00075D30 File Offset: 0x00073F30
			public int MaxAllowedCategorizerResubmits
			{
				get
				{
					return this.maxAllowedCategorizerResubmits;
				}
			}

			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x06001D68 RID: 7528 RVA: 0x00075D38 File Offset: 0x00073F38
			public bool DisableExchangeServerAuth
			{
				get
				{
					return this.disableExchangeServerAuth;
				}
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x06001D69 RID: 7529 RVA: 0x00075D40 File Offset: 0x00073F40
			public int MaxDeferCountForRecipientHasNoMdb
			{
				get
				{
					return this.maxDeferCountForRecipientHasNoMdb;
				}
			}

			// Token: 0x17000863 RID: 2147
			// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00075D48 File Offset: 0x00073F48
			public TimeSpan DeferralTimeForNoMdb
			{
				get
				{
					return this.deferralTimeForNoMdb;
				}
			}

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x06001D6B RID: 7531 RVA: 0x00075D50 File Offset: 0x00073F50
			public bool DestinationRoutingToRemoteSitesEnabled
			{
				get
				{
					return this.destinationRoutingToRemoteSitesEnabled;
				}
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x06001D6C RID: 7532 RVA: 0x00075D58 File Offset: 0x00073F58
			// (set) Token: 0x06001D6D RID: 7533 RVA: 0x00075D60 File Offset: 0x00073F60
			public bool DagRoutingEnabled
			{
				get
				{
					return this.dagRoutingEnabled;
				}
				protected set
				{
					this.dagRoutingEnabled = value;
				}
			}

			// Token: 0x17000866 RID: 2150
			// (get) Token: 0x06001D6E RID: 7534 RVA: 0x00075D69 File Offset: 0x00073F69
			public bool SmtpDeliveryToMailboxEnabled
			{
				get
				{
					return this.smtpDeliveryToMailboxEnabled;
				}
			}

			// Token: 0x17000867 RID: 2151
			// (get) Token: 0x06001D6F RID: 7535 RVA: 0x00075D71 File Offset: 0x00073F71
			// (set) Token: 0x06001D70 RID: 7536 RVA: 0x00075D79 File Offset: 0x00073F79
			public IList<int> ProxyRoutingAllowedTargetVersions
			{
				get
				{
					return this.proxyRoutingAllowedTargetVersions;
				}
				protected set
				{
					this.proxyRoutingAllowedTargetVersions = value;
				}
			}

			// Token: 0x17000868 RID: 2152
			// (get) Token: 0x06001D71 RID: 7537 RVA: 0x00075D82 File Offset: 0x00073F82
			// (set) Token: 0x06001D72 RID: 7538 RVA: 0x00075D8A File Offset: 0x00073F8A
			public int ProxyRoutingMaxTotalHubCount
			{
				get
				{
					return this.proxyRoutingMaxTotalHubCount;
				}
				protected set
				{
					this.proxyRoutingMaxTotalHubCount = value;
				}
			}

			// Token: 0x17000869 RID: 2153
			// (get) Token: 0x06001D73 RID: 7539 RVA: 0x00075D93 File Offset: 0x00073F93
			// (set) Token: 0x06001D74 RID: 7540 RVA: 0x00075D9B File Offset: 0x00073F9B
			public int ProxyRoutingMaxRemoteSiteHubCount
			{
				get
				{
					return this.proxyRoutingMaxRemoteSiteHubCount;
				}
				protected set
				{
					this.proxyRoutingMaxRemoteSiteHubCount = value;
				}
			}

			// Token: 0x1700086A RID: 2154
			// (get) Token: 0x06001D75 RID: 7541 RVA: 0x00075DA4 File Offset: 0x00073FA4
			// (set) Token: 0x06001D76 RID: 7542 RVA: 0x00075DAC File Offset: 0x00073FAC
			public RoutedServerSelectStrategy ProxyRoutingServerSelectStrategy
			{
				get
				{
					return this.proxyRoutingServerSelectStrategy;
				}
				protected set
				{
					this.proxyRoutingServerSelectStrategy = value;
				}
			}

			// Token: 0x1700086B RID: 2155
			// (get) Token: 0x06001D77 RID: 7543 RVA: 0x00075DB5 File Offset: 0x00073FB5
			// (set) Token: 0x06001D78 RID: 7544 RVA: 0x00075DBD File Offset: 0x00073FBD
			public IList<RoutingHost> OutboundFrontendServers
			{
				get
				{
					return this.outboundFrontendServers;
				}
				protected set
				{
					this.outboundFrontendServers = value;
				}
			}

			// Token: 0x1700086C RID: 2156
			// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00075DC6 File Offset: 0x00073FC6
			// (set) Token: 0x06001D7A RID: 7546 RVA: 0x00075DCE File Offset: 0x00073FCE
			public bool ExternalOutboundFrontendProxyEnabled
			{
				get
				{
					return this.externalOutboundFrontendProxyEnabled;
				}
				protected set
				{
					this.externalOutboundFrontendProxyEnabled = value;
				}
			}

			// Token: 0x1700086D RID: 2157
			// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00075DD7 File Offset: 0x00073FD7
			// (set) Token: 0x06001D7C RID: 7548 RVA: 0x00075DDF File Offset: 0x00073FDF
			public bool OutboundProxyRoutingXVersionEnabled
			{
				get
				{
					return this.outboundProxyRoutingXVersionEnabled;
				}
				protected set
				{
					this.outboundProxyRoutingXVersionEnabled = value;
				}
			}

			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00075DE8 File Offset: 0x00073FE8
			// (set) Token: 0x06001D7E RID: 7550 RVA: 0x00075DF0 File Offset: 0x00073FF0
			public bool RoutingToNonActiveServersEnabled
			{
				get
				{
					return this.routingToNonActiveServersEnabled;
				}
				protected set
				{
					this.routingToNonActiveServersEnabled = value;
				}
			}

			// Token: 0x1700086F RID: 2159
			// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00075DF9 File Offset: 0x00073FF9
			// (set) Token: 0x06001D80 RID: 7552 RVA: 0x00075E01 File Offset: 0x00074001
			public bool RandomLoadBalancingOffsetEnabled
			{
				get
				{
					return this.randomLoadBalancingOffsetEnabled;
				}
				protected set
				{
					this.randomLoadBalancingOffsetEnabled = value;
				}
			}

			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x06001D81 RID: 7553 RVA: 0x00075E0A File Offset: 0x0007400A
			// (set) Token: 0x06001D82 RID: 7554 RVA: 0x00075E12 File Offset: 0x00074012
			public bool RoutingTopologyCacheEnabled
			{
				get
				{
					return this.routingTopologyCacheEnabled;
				}
				internal set
				{
					this.routingTopologyCacheEnabled = value;
				}
			}

			// Token: 0x06001D83 RID: 7555 RVA: 0x00075E1B File Offset: 0x0007401B
			public static TransportAppConfig.RoutingConfig Load()
			{
				return new TransportAppConfig.RoutingConfig();
			}

			// Token: 0x06001D84 RID: 7556 RVA: 0x00075E22 File Offset: 0x00074022
			private static TimeSpan GetConfigReloadInterval()
			{
				return TransportAppConfig.GetConfigTimeSpan("RoutingConfigReloadInterval", TimeSpan.FromSeconds(30.0), TimeSpan.MaxValue, TimeSpan.FromHours(12.0));
			}

			// Token: 0x06001D85 RID: 7557 RVA: 0x00075E4F File Offset: 0x0007404F
			private static TimeSpan GetDatabaseFullReloadInterval()
			{
				return TransportAppConfig.GetConfigTimeSpan("RoutingConfigDatabaseFullReloadInterval", TimeSpan.FromSeconds(0.0), TimeSpan.MaxValue, TimeSpan.FromHours(4.0));
			}

			// Token: 0x06001D86 RID: 7558 RVA: 0x00075E7C File Offset: 0x0007407C
			private static TimeSpan GetDeferredReloadInterval()
			{
				int configInt = TransportAppConfig.GetConfigInt("DeferredReloadTimeoutSeconds", 0, int.MaxValue, 5);
				if (configInt != 2147483647)
				{
					return TimeSpan.FromSeconds((double)configInt);
				}
				return TimeSpan.MaxValue;
			}

			// Token: 0x06001D87 RID: 7559 RVA: 0x00075EB0 File Offset: 0x000740B0
			private static int GetMaxDeferredNotifications()
			{
				string text = ConfigurationManager.AppSettings["MaxDeferredNotifications"];
				int num;
				if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num <= 0)
				{
					return 20;
				}
				return num;
			}

			// Token: 0x06001D88 RID: 7560 RVA: 0x00075EE7 File Offset: 0x000740E7
			private static int GetLoopDetectionNumberOfTransits()
			{
				return TransportAppConfig.GetConfigInt("LoopDetectionNumberOfTransits", 1, int.MaxValue, 4);
			}

			// Token: 0x06001D89 RID: 7561 RVA: 0x00075EFA File Offset: 0x000740FA
			private static int GetLocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter()
			{
				return TransportAppConfig.GetConfigInt("LocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter", 0, int.MaxValue, 0);
			}

			// Token: 0x06001D8A RID: 7562 RVA: 0x00075F10 File Offset: 0x00074110
			private static bool GetDisableExchangeServerAuth()
			{
				string value = ConfigurationManager.AppSettings["DisableExchangeServerAuth"];
				bool result = false;
				if (!string.IsNullOrEmpty(value))
				{
					bool.TryParse(value, out result);
				}
				return result;
			}

			// Token: 0x06001D8B RID: 7563 RVA: 0x00075F41 File Offset: 0x00074141
			private static TimeSpan GetPfReplicaAgeThreshold()
			{
				return TransportAppConfig.GetConfigTimeSpan("PFReplicaAgeThreshold", TimeSpan.FromSeconds(0.0), TimeSpan.MaxValue, TransportAppConfig.RoutingConfig.DefaultPfReplicaAgeThreshold);
			}

			// Token: 0x06001D8C RID: 7564 RVA: 0x00075F68 File Offset: 0x00074168
			private static int[] GetLocalLoopMessageDeferralIntervals()
			{
				int[] array = TransportAppConfig.GetConfigIntList("LocalLoopMessageDeferralIntervals", 0, int.MaxValue, 0, ',').ToArray();
				if (array == null || array.Length == 0)
				{
					array = TransportAppConfig.RoutingConfig.DefaultLocalLoopMessageDeferralIntervals;
				}
				return array;
			}

			// Token: 0x06001D8D RID: 7565 RVA: 0x00075F9D File Offset: 0x0007419D
			private static int GetLocalLoopSubdomainDepth()
			{
				return TransportAppConfig.GetConfigInt("LocalLoopSubdomainDepth", 0, 10, 0);
			}

			// Token: 0x06001D8E RID: 7566 RVA: 0x00075FAD File Offset: 0x000741AD
			private static bool GetLocalLoopDetectionEnabled()
			{
				return TransportAppConfig.GetConfigBool("LocalLoopDetectionEnabled", true);
			}

			// Token: 0x06001D8F RID: 7567 RVA: 0x00075FBA File Offset: 0x000741BA
			private static TimeSpan GetDeferralTimeForNoMdb()
			{
				return TransportAppConfig.GetConfigTimeSpan("DeferralTimeForNoMdb", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(6.0), TimeSpan.FromMinutes(15.0));
			}

			// Token: 0x04000E54 RID: 3668
			public const string OutboundFrontendServersLabel = "OutboundFrontendServers";

			// Token: 0x04000E55 RID: 3669
			public const char OutboundFrontendServersDelimiter = ',';

			// Token: 0x04000E56 RID: 3670
			private const int DefaultMaxDeferredNotifications = 20;

			// Token: 0x04000E57 RID: 3671
			private const int DefaultLocalLoopSubdomainDepth = 0;

			// Token: 0x04000E58 RID: 3672
			private const int DefaultLoopDetectionNumberOfTransits = 4;

			// Token: 0x04000E59 RID: 3673
			private const int DefaultLocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter = 0;

			// Token: 0x04000E5A RID: 3674
			private const int MaxLocalLoopSubdomainDepth = 10;

			// Token: 0x04000E5B RID: 3675
			private const bool DefaultLocalLoopDetectionEnabled = true;

			// Token: 0x04000E5C RID: 3676
			private static readonly TimeSpan DefaultPfReplicaAgeThreshold = TimeSpan.FromHours(48.0);

			// Token: 0x04000E5D RID: 3677
			private static readonly int[] DefaultLocalLoopMessageDeferralIntervals = new int[]
			{
				0,
				0,
				30,
				30,
				900,
				900,
				900
			};

			// Token: 0x04000E5E RID: 3678
			private readonly TimeSpan configReloadInterval;

			// Token: 0x04000E5F RID: 3679
			private readonly TimeSpan databaseFullReloadInterval;

			// Token: 0x04000E60 RID: 3680
			private readonly TimeSpan deferredReloadInterval;

			// Token: 0x04000E61 RID: 3681
			private readonly int maxDeferredNotifications;

			// Token: 0x04000E62 RID: 3682
			private readonly int loopDetectionNumberOfTransits;

			// Token: 0x04000E63 RID: 3683
			private readonly int localLoopDetectionSubDomainLeftToRightOffsetForPerfCounter;

			// Token: 0x04000E64 RID: 3684
			private readonly TimeSpan minConfigReloadInterval;

			// Token: 0x04000E65 RID: 3685
			private readonly TimeSpan pfReplicaAgeThreshold = TransportAppConfig.RoutingConfig.DefaultPfReplicaAgeThreshold;

			// Token: 0x04000E66 RID: 3686
			private readonly int[] localLoopMessageDeferralIntervals = TransportAppConfig.RoutingConfig.DefaultLocalLoopMessageDeferralIntervals;

			// Token: 0x04000E67 RID: 3687
			private readonly int localLoopSubdomainDepth;

			// Token: 0x04000E68 RID: 3688
			private readonly bool localLoopDetectionEnabled = true;

			// Token: 0x04000E69 RID: 3689
			private readonly int maxAllowedCategorizerResubmits;

			// Token: 0x04000E6A RID: 3690
			private readonly bool disableExchangeServerAuth;

			// Token: 0x04000E6B RID: 3691
			private readonly int maxDeferCountForRecipientHasNoMdb;

			// Token: 0x04000E6C RID: 3692
			private readonly TimeSpan deferralTimeForNoMdb;

			// Token: 0x04000E6D RID: 3693
			private readonly bool destinationRoutingToRemoteSitesEnabled;

			// Token: 0x04000E6E RID: 3694
			private readonly bool smtpDeliveryToMailboxEnabled;

			// Token: 0x04000E6F RID: 3695
			private bool dagRoutingEnabled;

			// Token: 0x04000E70 RID: 3696
			private IList<int> proxyRoutingAllowedTargetVersions;

			// Token: 0x04000E71 RID: 3697
			private int proxyRoutingMaxTotalHubCount;

			// Token: 0x04000E72 RID: 3698
			private int proxyRoutingMaxRemoteSiteHubCount;

			// Token: 0x04000E73 RID: 3699
			private RoutedServerSelectStrategy proxyRoutingServerSelectStrategy;

			// Token: 0x04000E74 RID: 3700
			private IList<RoutingHost> outboundFrontendServers;

			// Token: 0x04000E75 RID: 3701
			private bool externalOutboundFrontendProxyEnabled;

			// Token: 0x04000E76 RID: 3702
			private bool outboundProxyRoutingXVersionEnabled;

			// Token: 0x04000E77 RID: 3703
			private bool routingToNonActiveServersEnabled;

			// Token: 0x04000E78 RID: 3704
			private bool randomLoadBalancingOffsetEnabled;

			// Token: 0x04000E79 RID: 3705
			private readonly bool dagSelectorEnabled;

			// Token: 0x04000E7A RID: 3706
			private readonly int dagSelectorMessageThresholdPerServer;

			// Token: 0x04000E7B RID: 3707
			private readonly double dagSelectorIncrementMessageThresholdFactor;

			// Token: 0x04000E7C RID: 3708
			private readonly int dagSelectorActiveServersForDagToBeRoutable;

			// Token: 0x04000E7D RID: 3709
			private readonly int dagSelectorMinimumSitesForDagToBeRoutable;

			// Token: 0x04000E7E RID: 3710
			private readonly int tenantDagQuotaMessageThreshold;

			// Token: 0x04000E7F RID: 3711
			private readonly int tenantDagQuotaDagsPerTenant;

			// Token: 0x04000E80 RID: 3712
			private readonly double tenantDagQuotaPastWeight;

			// Token: 0x04000E81 RID: 3713
			private readonly bool checkDagSelectorHeader;

			// Token: 0x04000E82 RID: 3714
			private readonly bool logDagSelectorDiagnosticInfo;

			// Token: 0x04000E83 RID: 3715
			private bool routingTopologyCacheEnabled;
		}

		// Token: 0x020002A6 RID: 678
		public sealed class ContentConversionConfig
		{
			// Token: 0x06001D91 RID: 7569 RVA: 0x00076037 File Offset: 0x00074237
			private ContentConversionConfig()
			{
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x06001D92 RID: 7570 RVA: 0x00076052 File Offset: 0x00074252
			public ByteEncoderTypeFor7BitCharsets ByteEncoderTypeFor7BitCharsets
			{
				get
				{
					return this.byteEncoderTypeFor7BitCharsets;
				}
			}

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0007605A File Offset: 0x0007425A
			public bool TreatInlineDispositionAsAttachment
			{
				get
				{
					return this.treatInlineDispositionAsAttachment;
				}
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00076062 File Offset: 0x00074262
			public bool QuoteDisplayNameBeforeRfc2047Encoding
			{
				get
				{
					return this.quoteDisplayNameBeforeRfc2047Encoding;
				}
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0007606A File Offset: 0x0007426A
			public int PreferredInternetCodePageForShiftJis
			{
				get
				{
					return this.preferredInternetCodePageForShiftJis;
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06001D96 RID: 7574 RVA: 0x00076072 File Offset: 0x00074272
			public int RequiredCharsetCoverage
			{
				get
				{
					return this.requiredCharsetCoverage;
				}
			}

			// Token: 0x06001D97 RID: 7575 RVA: 0x0007607C File Offset: 0x0007427C
			public static TransportAppConfig.ContentConversionConfig Load()
			{
				TransportAppConfig.ContentConversionConfig contentConversionConfig = new TransportAppConfig.ContentConversionConfig();
				contentConversionConfig.byteEncoderTypeFor7BitCharsets = TransportAppConfig.ContentConversionConfig.GetByteEncoderTypeFor7BitCharsets();
				string text = ConfigurationManager.AppSettings["PreferredInternetCodePageForShiftJis"];
				int num;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num))
				{
					contentConversionConfig.preferredInternetCodePageForShiftJis = num;
				}
				text = ConfigurationManager.AppSettings["RequiredCharsetCoverage"];
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && num >= 0 && num <= 100)
				{
					contentConversionConfig.requiredCharsetCoverage = num;
				}
				contentConversionConfig.treatInlineDispositionAsAttachment = TransportAppConfig.ContentConversionConfig.GetTreatInlineDispositionAsAttachment();
				contentConversionConfig.quoteDisplayNameBeforeRfc2047Encoding = TransportAppConfig.ContentConversionConfig.GetQuoteDisplayNameBeforeRfc2047Encoding();
				return contentConversionConfig;
			}

			// Token: 0x06001D98 RID: 7576 RVA: 0x0007610C File Offset: 0x0007430C
			private static ByteEncoderTypeFor7BitCharsets GetByteEncoderTypeFor7BitCharsets()
			{
				return TransportAppConfig.GetConfigEnum<ByteEncoderTypeFor7BitCharsets>("ByteEncoderTypeFor7BitCharsets", ByteEncoderTypeFor7BitCharsets.UseQP, EnumParseOptions.AllowNumericConstants | EnumParseOptions.IgnoreCase);
			}

			// Token: 0x06001D99 RID: 7577 RVA: 0x0007611A File Offset: 0x0007431A
			private static bool GetTreatInlineDispositionAsAttachment()
			{
				return TransportAppConfig.GetConfigBool("TreatInlineDispositionAsAttachment", false);
			}

			// Token: 0x06001D9A RID: 7578 RVA: 0x00076127 File Offset: 0x00074327
			private static bool GetQuoteDisplayNameBeforeRfc2047Encoding()
			{
				return TransportAppConfig.GetConfigBool("QuoteDisplayNameBeforeRfc2047Encoding", false);
			}

			// Token: 0x04000E84 RID: 3716
			private const ByteEncoderTypeFor7BitCharsets DefaultByteEncoderTypeFor7BitCharsets = ByteEncoderTypeFor7BitCharsets.UseQP;

			// Token: 0x04000E85 RID: 3717
			private const bool DefaultTreatInlineDispositionAsAttachment = false;

			// Token: 0x04000E86 RID: 3718
			private const bool DefaultQuoteDisplayNameBeforeRfc2047Encoding = false;

			// Token: 0x04000E87 RID: 3719
			private ByteEncoderTypeFor7BitCharsets byteEncoderTypeFor7BitCharsets;

			// Token: 0x04000E88 RID: 3720
			private int preferredInternetCodePageForShiftJis = 50222;

			// Token: 0x04000E89 RID: 3721
			private bool quoteDisplayNameBeforeRfc2047Encoding;

			// Token: 0x04000E8A RID: 3722
			private int requiredCharsetCoverage = 100;

			// Token: 0x04000E8B RID: 3723
			private bool treatInlineDispositionAsAttachment;
		}

		// Token: 0x020002A7 RID: 679
		public sealed class IPFilteringDatabaseConfig
		{
			// Token: 0x06001D9B RID: 7579 RVA: 0x00076134 File Offset: 0x00074334
			private IPFilteringDatabaseConfig()
			{
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0007613C File Offset: 0x0007433C
			public string DatabasePath
			{
				get
				{
					return this.databasePath;
				}
			}

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x06001D9D RID: 7581 RVA: 0x00076144 File Offset: 0x00074344
			public string LogFilePath
			{
				get
				{
					return this.logFilePath;
				}
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0007614C File Offset: 0x0007434C
			public uint LogFileSize
			{
				get
				{
					return (uint)this.logFileSize.ToBytes();
				}
			}

			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0007615A File Offset: 0x0007435A
			public uint LogBufferSize
			{
				get
				{
					return (uint)this.logBufferSize.ToBytes();
				}
			}

			// Token: 0x06001DA0 RID: 7584 RVA: 0x00076168 File Offset: 0x00074368
			public static TransportAppConfig.IPFilteringDatabaseConfig Load()
			{
				TransportAppConfig.IPFilteringDatabaseConfig ipfilteringDatabaseConfig = new TransportAppConfig.IPFilteringDatabaseConfig();
				string value = ConfigurationManager.AppSettings["IPFilterDatabasePath"];
				if (!string.IsNullOrEmpty(value))
				{
					ipfilteringDatabaseConfig.databasePath = value;
				}
				string value2 = ConfigurationManager.AppSettings["IPFilterDatabaseLoggingPath"];
				if (!string.IsNullOrEmpty(value2))
				{
					ipfilteringDatabaseConfig.logFilePath = value2;
				}
				ipfilteringDatabaseConfig.logFileSize = TransportAppConfig.GetConfigByteQuantifiedSize("IPFilterDatabaseLoggingFileSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromKB(512UL));
				ipfilteringDatabaseConfig.logBufferSize = TransportAppConfig.GetConfigByteQuantifiedSize("IPFilterDatabaseLoggingBufferSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromKB(5UL));
				return ipfilteringDatabaseConfig;
			}

			// Token: 0x04000E8C RID: 3724
			private string databasePath;

			// Token: 0x04000E8D RID: 3725
			private string logFilePath;

			// Token: 0x04000E8E RID: 3726
			private ByteQuantifiedSize logFileSize;

			// Token: 0x04000E8F RID: 3727
			private ByteQuantifiedSize logBufferSize;
		}

		// Token: 0x020002A8 RID: 680
		public sealed class MessageResubmissionConfig : TransportAppConfig.IMessageResubmissionConfig
		{
			// Token: 0x06001DA1 RID: 7585 RVA: 0x00076208 File Offset: 0x00074408
			private MessageResubmissionConfig()
			{
			}

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x00076210 File Offset: 0x00074410
			// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x00076218 File Offset: 0x00074418
			public bool MessageResubmissionEnabled { get; private set; }

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x00076221 File Offset: 0x00074421
			// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x00076229 File Offset: 0x00074429
			public TimeSpan ResubmissionInterval { get; private set; }

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x00076232 File Offset: 0x00074432
			// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x0007623A File Offset: 0x0007443A
			public TimeSpan ResubmissionInitialDelay { get; private set; }

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00076243 File Offset: 0x00074443
			// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x0007624B File Offset: 0x0007444B
			public int ResubmissionPageSize { get; private set; }

			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00076254 File Offset: 0x00074454
			// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0007625C File Offset: 0x0007445C
			public int MaxOutstandingResubmissionMessages { get; private set; }

			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00076265 File Offset: 0x00074465
			// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0007626D File Offset: 0x0007446D
			public int MaxResubmissionRequests { get; private set; }

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00076276 File Offset: 0x00074476
			// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0007627E File Offset: 0x0007447E
			public TimeSpan ResubmitRequestExpiryPeriod { get; private set; }

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00076287 File Offset: 0x00074487
			// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0007628F File Offset: 0x0007448F
			public TimeSpan TestResubmitRequestExpiryPeriod { get; private set; }

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00076298 File Offset: 0x00074498
			// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x000762A0 File Offset: 0x000744A0
			public int MaxRecentResubmissionRequests { get; private set; }

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x000762A9 File Offset: 0x000744A9
			// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x000762B1 File Offset: 0x000744B1
			public TimeSpan RecentResubmitRequestPeriod { get; private set; }

			// Token: 0x06001DB6 RID: 7606 RVA: 0x000762BC File Offset: 0x000744BC
			public static TransportAppConfig.IMessageResubmissionConfig Load()
			{
				TransportAppConfig.MessageResubmissionConfig messageResubmissionConfig = new TransportAppConfig.MessageResubmissionConfig();
				messageResubmissionConfig.MessageResubmissionEnabled = TransportAppConfig.GetConfigBool("MessageResubmissionEnabled", true);
				messageResubmissionConfig.ResubmissionInterval = TransportAppConfig.GetConfigTimeSpan("MessageResubmissionInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(2.0));
				messageResubmissionConfig.ResubmissionInitialDelay = TransportAppConfig.GetConfigTimeSpan("MessageResubmissionInitialDelay", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromSeconds(30.0));
				messageResubmissionConfig.MaxOutstandingResubmissionMessages = TransportAppConfig.GetConfigInt("MessageResubmissionMaxOutstandingMessages", 1, int.MaxValue, 90);
				messageResubmissionConfig.ResubmissionPageSize = TransportAppConfig.GetConfigInt("MessageResubmissionPageSize", 1, messageResubmissionConfig.MaxOutstandingResubmissionMessages, 30);
				messageResubmissionConfig.MaxResubmissionRequests = TransportAppConfig.GetConfigInt("MaxResubmissionRequests", 0, int.MaxValue, 100);
				messageResubmissionConfig.MaxRecentResubmissionRequests = TransportAppConfig.GetConfigInt("MaxRecentResubmissionRequests", 0, int.MaxValue, messageResubmissionConfig.MaxResubmissionRequests);
				messageResubmissionConfig.RecentResubmitRequestPeriod = TransportAppConfig.GetConfigTimeSpan("RecentResubmitRequestPeriod", TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue, TimeSpan.FromHours(1.0));
				messageResubmissionConfig.ResubmitRequestExpiryPeriod = TransportAppConfig.GetConfigTimeSpan("ResubmitRequestExpiryPeriod", TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue, TimeSpan.FromDays(3.0));
				messageResubmissionConfig.TestResubmitRequestExpiryPeriod = TransportAppConfig.GetConfigTimeSpan("TestResubmitRequestExpiryPeriod", TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue, TimeSpan.FromHours(3.0));
				return messageResubmissionConfig;
			}
		}

		// Token: 0x020002A9 RID: 681
		public interface IProcessingQuotaConfig
		{
			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x06001DB7 RID: 7607
			bool EnforceProcessingQuota { get; }

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x06001DB8 RID: 7608
			bool TestProcessingQuota { get; }

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x06001DB9 RID: 7609
			TimeSpan UpdateThrottlingDataInterval { get; }

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x06001DBA RID: 7610
			int HighWatermarkCpuPercentage { get; }

			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x06001DBB RID: 7611
			int LowWatermarkCpuPercentage { get; }

			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x06001DBC RID: 7612
			string ThrottleDataFilePath { get; }
		}

		// Token: 0x020002AA RID: 682
		public class ProcessingQuotaConfig : TransportAppConfig.IProcessingQuotaConfig
		{
			// Token: 0x06001DBD RID: 7613 RVA: 0x0007643C File Offset: 0x0007463C
			public static TransportAppConfig.ProcessingQuotaConfig Load()
			{
				return new TransportAppConfig.ProcessingQuotaConfig
				{
					EnforceProcessingQuota = (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.EnforceProcessingQuota.Enabled && TransportAppConfig.GetConfigBool("EnforceProcessingQuota", false)),
					TestProcessingQuota = (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TestProcessingQuota.Enabled && TransportAppConfig.GetConfigBool("TestProcessingQuota", false)),
					UpdateThrottlingDataInterval = TransportAppConfig.GetConfigTimeSpan("ProcessingQuotaDataUpdateInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(10.0)),
					HighWatermarkCpuPercentage = TransportAppConfig.GetConfigInt("HighWatermarkProcessingQuotaTriggerPercentage", 0, 100, 60),
					LowWatermarkCpuPercentage = TransportAppConfig.GetConfigInt("LowWatermarkProcessingQuotaTriggerPercentage", 0, 100, 40),
					ThrottleDataFilePath = TransportAppConfig.GetConfigString("ProcessingQuotaDataFilePath", string.Empty)
				};
			}

			// Token: 0x1700088A RID: 2186
			// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0007651A File Offset: 0x0007471A
			// (set) Token: 0x06001DBF RID: 7615 RVA: 0x00076522 File Offset: 0x00074722
			public bool EnforceProcessingQuota { get; private set; }

			// Token: 0x1700088B RID: 2187
			// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0007652B File Offset: 0x0007472B
			// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x00076533 File Offset: 0x00074733
			public bool TestProcessingQuota { get; private set; }

			// Token: 0x1700088C RID: 2188
			// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0007653C File Offset: 0x0007473C
			// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x00076544 File Offset: 0x00074744
			public TimeSpan UpdateThrottlingDataInterval { get; private set; }

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0007654D File Offset: 0x0007474D
			// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x00076555 File Offset: 0x00074755
			public int HighWatermarkCpuPercentage { get; private set; }

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0007655E File Offset: 0x0007475E
			// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x00076566 File Offset: 0x00074766
			public int LowWatermarkCpuPercentage { get; private set; }

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0007656F File Offset: 0x0007476F
			// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x00076577 File Offset: 0x00074777
			public string ThrottleDataFilePath { get; private set; }
		}

		// Token: 0x020002AB RID: 683
		public class QueueDatabaseConfig : IMessagingDatabaseConfig
		{
			// Token: 0x06001DCB RID: 7627 RVA: 0x00076588 File Offset: 0x00074788
			private QueueDatabaseConfig()
			{
			}

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x06001DCC RID: 7628 RVA: 0x00076590 File Offset: 0x00074790
			public string DatabasePath
			{
				get
				{
					return this.databasePath;
				}
			}

			// Token: 0x17000891 RID: 2193
			// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00076598 File Offset: 0x00074798
			public string LogFilePath
			{
				get
				{
					return this.logFilePath;
				}
			}

			// Token: 0x17000892 RID: 2194
			// (get) Token: 0x06001DCE RID: 7630 RVA: 0x000765A0 File Offset: 0x000747A0
			public uint LogFileSize
			{
				get
				{
					return (uint)this.logFileSize.ToBytes();
				}
			}

			// Token: 0x17000893 RID: 2195
			// (get) Token: 0x06001DCF RID: 7631 RVA: 0x000765AE File Offset: 0x000747AE
			public uint LogBufferSize
			{
				get
				{
					return (uint)this.logBufferSize.ToBytes();
				}
			}

			// Token: 0x17000894 RID: 2196
			// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x000765BC File Offset: 0x000747BC
			public uint ExtensionSize
			{
				get
				{
					return (uint)this.extensionSize.ToBytes();
				}
			}

			// Token: 0x17000895 RID: 2197
			// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x000765CA File Offset: 0x000747CA
			public uint MaxBackgroundCleanupTasks
			{
				get
				{
					return this.maxBackgroundCleanupTasks;
				}
			}

			// Token: 0x17000896 RID: 2198
			// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x000765D2 File Offset: 0x000747D2
			public int MaxConnections
			{
				get
				{
					return this.maxConnections;
				}
			}

			// Token: 0x17000897 RID: 2199
			// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x000765DA File Offset: 0x000747DA
			public DatabaseRecoveryAction DatabaseRecoveryAction
			{
				get
				{
					return this.databaseRecoveryAction;
				}
			}

			// Token: 0x17000898 RID: 2200
			// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x000765E2 File Offset: 0x000747E2
			public TimeSpan MessagingGenerationCleanupAge
			{
				get
				{
					if (this.safetyNetHoldTime == null)
					{
						return this.transportConfiguration.TransportSettings.TransportSettings.SafetyNetHoldTime;
					}
					return this.safetyNetHoldTime.Value;
				}
			}

			// Token: 0x17000899 RID: 2201
			// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x00076618 File Offset: 0x00074818
			public TimeSpan MessagingGenerationExpirationAge
			{
				get
				{
					if (this.transportConfiguration == null || this.transportConfiguration.LocalServer == null)
					{
						return TimeSpan.MaxValue;
					}
					return this.MessagingGenerationCleanupAge + this.transportConfiguration.LocalServer.TransportServer.MessageExpirationTimeout;
				}
			}

			// Token: 0x1700089A RID: 2202
			// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00076665 File Offset: 0x00074865
			public TimeSpan MessagingGenerationLength
			{
				get
				{
					return this.messagingGenerationLength;
				}
			}

			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0007666D File Offset: 0x0007486D
			public TimeSpan DefaultAsyncCommitTimeout
			{
				get
				{
					return this.defaultAsyncCommitTimeout;
				}
			}

			// Token: 0x1700089C RID: 2204
			// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x00076675 File Offset: 0x00074875
			public byte MaxMessageLoadTimePercentage
			{
				get
				{
					return this.maxMessageLoadTimePercentage;
				}
			}

			// Token: 0x1700089D RID: 2205
			// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0007667D File Offset: 0x0007487D
			public TimeSpan StatisticsUpdateInterval
			{
				get
				{
					return this.statisticsUpdateInterval;
				}
			}

			// Token: 0x1700089E RID: 2206
			// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00076685 File Offset: 0x00074885
			public bool CloneInOriginalGeneration
			{
				get
				{
					return this.cloneInOriginalGeneration;
				}
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x00076690 File Offset: 0x00074890
			private static string GetQueueDBPath(string appConfigKey, string defaultValue)
			{
				string text = Path.Combine("D:\\Queue", "mail.que");
				if (!string.IsNullOrEmpty(text) && File.Exists(text))
				{
					return "D:\\Queue";
				}
				string path = ConfigurationContext.Setup.IsUnpacked ? ConfigurationContext.Setup.InstallPath : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string configString = TransportAppConfig.GetConfigString(appConfigKey, defaultValue);
				if (!Path.IsPathRooted(configString))
				{
					return Path.Combine(path, configString);
				}
				return configString;
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x000766FC File Offset: 0x000748FC
			public static TransportAppConfig.QueueDatabaseConfig Load()
			{
				TransportAppConfig.QueueDatabaseConfig queueDatabaseConfig = new TransportAppConfig.QueueDatabaseConfig();
				queueDatabaseConfig.databasePath = TransportAppConfig.QueueDatabaseConfig.GetQueueDBPath("QueueDatabasePath", "TransportRoles\\data\\Queue\\");
				queueDatabaseConfig.logFilePath = TransportAppConfig.QueueDatabaseConfig.GetQueueDBPath("QueueDatabaseLoggingPath", queueDatabaseConfig.databasePath);
				queueDatabaseConfig.maxConnections = TransportAppConfig.GetConfigInt("QueueDatabaseMaxConnections", 1, int.MaxValue, 4);
				queueDatabaseConfig.logFileSize = TransportAppConfig.GetConfigByteQuantifiedSize("QueueDatabaseLoggingFileSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(5UL));
				queueDatabaseConfig.logBufferSize = TransportAppConfig.GetConfigByteQuantifiedSize("QueueDatabaseLoggingBufferSize", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(5UL));
				queueDatabaseConfig.extensionSize = TransportAppConfig.GetConfigByteQuantifiedSize("QueueDatabaseExtensionSize", ByteQuantifiedSize.FromMB(1UL), ByteQuantifiedSize.FromBytes((ulong)-1), ByteQuantifiedSize.FromMB(256UL));
				queueDatabaseConfig.maxBackgroundCleanupTasks = (uint)TransportAppConfig.GetConfigInt("QueueDatabaseMaxBackgroundCleanupTasks", 0, int.MaxValue, 32);
				queueDatabaseConfig.databaseRecoveryAction = TransportAppConfig.GetConfigEnum<DatabaseRecoveryAction>("QueueDatabaseRecoveryAction", DatabaseRecoveryAction.Move);
				queueDatabaseConfig.messagingGenerationLength = TransportAppConfig.GetConfigTimeSpan("QueueDatabaseGenerationLength", TimeSpan.FromSeconds(10.0), TimeSpan.FromDays(10.0), TimeSpan.FromHours(1.0));
				queueDatabaseConfig.safetyNetHoldTime = TransportAppConfig.GetConfigNullableTimeSpan("SafetyNetHoldTimeInterval", TimeSpan.FromSeconds(15.0), TimeSpan.FromSeconds(2147483647.0));
				queueDatabaseConfig.defaultAsyncCommitTimeout = TransportAppConfig.GetConfigTimeSpan("QueueDatabaseAsyncCommitTimeout", TimeSpan.FromMilliseconds(25.0), TimeSpan.FromSeconds(60.0), TimeSpan.FromMilliseconds(100.0));
				queueDatabaseConfig.maxMessageLoadTimePercentage = (byte)TransportAppConfig.GetConfigInt("MaxMessageLoadTimePercentage", 1, 100, 75);
				queueDatabaseConfig.recentGenerationDepth = TransportAppConfig.GetConfigInt("RecentGenerationDepth", 0, 24, 1);
				queueDatabaseConfig.statisticsUpdateInterval = TransportAppConfig.GetConfigTimeSpan("StatisticsUpdateInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(20.0), TimeSpan.FromMinutes(5.0));
				queueDatabaseConfig.cloneInOriginalGeneration = TransportAppConfig.GetConfigBool("CloneInOriginalGeneration", true);
				return queueDatabaseConfig;
			}

			// Token: 0x06001DDD RID: 7645 RVA: 0x00076903 File Offset: 0x00074B03
			public void SetLoadTimeDependencies(ITransportConfiguration transportConfiguration)
			{
				this.transportConfiguration = transportConfiguration;
			}

			// Token: 0x1700089F RID: 2207
			// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0007690C File Offset: 0x00074B0C
			public int RecentGenerationDepth
			{
				get
				{
					return this.recentGenerationDepth;
				}
			}

			// Token: 0x04000EA0 RID: 3744
			private const string DefaultDatabaseFileName = "mail.que";

			// Token: 0x04000EA1 RID: 3745
			private const string DefaultDatacenterDatabasePath = "D:\\Queue";

			// Token: 0x04000EA2 RID: 3746
			private const DatabaseRecoveryAction DefaultDatabaseRecoveryAction = DatabaseRecoveryAction.Move;

			// Token: 0x04000EA3 RID: 3747
			private string databasePath;

			// Token: 0x04000EA4 RID: 3748
			private string logFilePath;

			// Token: 0x04000EA5 RID: 3749
			private ByteQuantifiedSize logFileSize;

			// Token: 0x04000EA6 RID: 3750
			private ByteQuantifiedSize logBufferSize;

			// Token: 0x04000EA7 RID: 3751
			private uint maxBackgroundCleanupTasks;

			// Token: 0x04000EA8 RID: 3752
			private int maxConnections;

			// Token: 0x04000EA9 RID: 3753
			private DatabaseRecoveryAction databaseRecoveryAction;

			// Token: 0x04000EAA RID: 3754
			private TimeSpan? safetyNetHoldTime;

			// Token: 0x04000EAB RID: 3755
			private TimeSpan messagingGenerationLength;

			// Token: 0x04000EAC RID: 3756
			private TimeSpan defaultAsyncCommitTimeout;

			// Token: 0x04000EAD RID: 3757
			private byte maxMessageLoadTimePercentage;

			// Token: 0x04000EAE RID: 3758
			private ITransportConfiguration transportConfiguration;

			// Token: 0x04000EAF RID: 3759
			private ByteQuantifiedSize extensionSize;

			// Token: 0x04000EB0 RID: 3760
			private int recentGenerationDepth;

			// Token: 0x04000EB1 RID: 3761
			private TimeSpan statisticsUpdateInterval;

			// Token: 0x04000EB2 RID: 3762
			private bool cloneInOriginalGeneration;
		}

		// Token: 0x020002AC RID: 684
		public class WorkerProcessConfig
		{
			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00076914 File Offset: 0x00074B14
			public int MinIOThreads
			{
				get
				{
					return this.minIOThreads;
				}
			}

			// Token: 0x170008A1 RID: 2209
			// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0007691C File Offset: 0x00074B1C
			public int MinWorkerThreads
			{
				get
				{
					return this.minWorkerThreads;
				}
			}

			// Token: 0x170008A2 RID: 2210
			// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00076924 File Offset: 0x00074B24
			public int MaxIOThreads
			{
				get
				{
					return this.maxIOThreads;
				}
			}

			// Token: 0x170008A3 RID: 2211
			// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0007692C File Offset: 0x00074B2C
			public int MaxWorkerThreads
			{
				get
				{
					return this.maxWorkerThreads;
				}
			}

			// Token: 0x170008A4 RID: 2212
			// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00076934 File Offset: 0x00074B34
			public bool HandleLeakedException
			{
				get
				{
					return this.handleLeakedException;
				}
			}

			// Token: 0x170008A5 RID: 2213
			// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0007693C File Offset: 0x00074B3C
			public string TemporaryStoragePath
			{
				get
				{
					return this.temporaryStoragePath;
				}
			}

			// Token: 0x170008A6 RID: 2214
			// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00076944 File Offset: 0x00074B44
			public bool CrashOnStopTimeout
			{
				get
				{
					return this.crashOnStopTimeout;
				}
			}

			// Token: 0x170008A7 RID: 2215
			// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0007694C File Offset: 0x00074B4C
			// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x00076954 File Offset: 0x00074B54
			public TimeSpan BackgroundProcessingThreadHangDetectionToleranceInterval { get; private set; }

			// Token: 0x170008A8 RID: 2216
			// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0007695D File Offset: 0x00074B5D
			public int FreeMemoryRequiredToStartInMbytes
			{
				get
				{
					return this.freeMemoryRequiredToStartInMBytes;
				}
			}

			// Token: 0x06001DE9 RID: 7657 RVA: 0x00076968 File Offset: 0x00074B68
			public static TransportAppConfig.WorkerProcessConfig Load()
			{
				TransportAppConfig.WorkerProcessConfig workerProcessConfig = new TransportAppConfig.WorkerProcessConfig();
				TransportAppConfig.WorkerProcessConfig.GetThreadLimits(Environment.ProcessorCount, out workerProcessConfig.minIOThreads, out workerProcessConfig.maxIOThreads, out workerProcessConfig.minWorkerThreads, out workerProcessConfig.maxWorkerThreads);
				workerProcessConfig.handleLeakedException = TransportAppConfig.GetConfigBool("HandleLeakedException", true);
				workerProcessConfig.crashOnStopTimeout = TransportAppConfig.GetConfigBool("CrashOnStopTimeout", false);
				workerProcessConfig.temporaryStoragePath = TransportAppConfig.GetConfigString("TemporaryStoragePath", Path.GetTempPath());
				workerProcessConfig.BackgroundProcessingThreadHangDetectionToleranceInterval = TransportAppConfig.GetConfigTimeSpan("BackgroundProcessingThreadHangDetectionToleranceInterval", TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(15.0));
				workerProcessConfig.freeMemoryRequiredToStartInMBytes = TransportAppConfig.GetConfigInt("FreeMemoryRequiredToStartServiceInMbytes", 0, int.MaxValue, 0);
				return workerProcessConfig;
			}

			// Token: 0x06001DEA RID: 7658 RVA: 0x00076A20 File Offset: 0x00074C20
			private static void GetThreadLimits(int numProcs, out int minIoThreads, out int maxIoThreads, out int minWorkerThreads, out int maxWorkerThreads)
			{
				minIoThreads = TransportAppConfig.GetConfigInt("MinIOThreads", numProcs, numProcs * 100, Math.Min(60 + numProcs * 10, 120));
				minWorkerThreads = TransportAppConfig.GetConfigInt("MinWorkerThreads", numProcs, numProcs * 200, Math.Min(120 + numProcs * 20, 400));
				maxIoThreads = TransportAppConfig.GetConfigInt("MaxIOThreads", numProcs, numProcs * 100, numProcs * 25);
				maxWorkerThreads = TransportAppConfig.GetConfigInt("MaxWorkerThreads", numProcs, TransportAppConfig.WorkerProcessConfig.MaxWorkerThreadsMaximum, numProcs * 65);
				if (maxIoThreads < minIoThreads)
				{
					maxIoThreads = minIoThreads;
				}
				if (maxWorkerThreads < minWorkerThreads)
				{
					maxWorkerThreads = minWorkerThreads;
				}
			}

			// Token: 0x04000EB3 RID: 3763
			private const string MinIOThreadsKey = "MinIOThreads";

			// Token: 0x04000EB4 RID: 3764
			private const string MinWorkerThreadsKey = "MinWorkerThreads";

			// Token: 0x04000EB5 RID: 3765
			private const string MaxIOThreadsKey = "MaxIOThreads";

			// Token: 0x04000EB6 RID: 3766
			private const string MaxWorkerThreadsKey = "MaxWorkerThreads";

			// Token: 0x04000EB7 RID: 3767
			private const string HandleLeakedExceptionKey = "HandleLeakedException";

			// Token: 0x04000EB8 RID: 3768
			private const string TemporaryStoragePathKey = "TemporaryStoragePath";

			// Token: 0x04000EB9 RID: 3769
			private const string CrashOnStopTimeoutKey = "CrashOnStopTimeout";

			// Token: 0x04000EBA RID: 3770
			private const string BackgroundProcessingThreadHangDetectionToleranceIntervalKey = "BackgroundProcessingThreadHangDetectionToleranceInterval";

			// Token: 0x04000EBB RID: 3771
			private const string FreeMemoryRequiredToStartInMbytesKey = "FreeMemoryRequiredToStartServiceInMbytes";

			// Token: 0x04000EBC RID: 3772
			internal static readonly int MaxWorkerThreadsMaximum = Environment.ProcessorCount * 1000;

			// Token: 0x04000EBD RID: 3773
			private int minIOThreads;

			// Token: 0x04000EBE RID: 3774
			private int minWorkerThreads;

			// Token: 0x04000EBF RID: 3775
			private int maxIOThreads;

			// Token: 0x04000EC0 RID: 3776
			private int maxWorkerThreads;

			// Token: 0x04000EC1 RID: 3777
			private bool handleLeakedException = true;

			// Token: 0x04000EC2 RID: 3778
			private string temporaryStoragePath;

			// Token: 0x04000EC3 RID: 3779
			private bool crashOnStopTimeout;

			// Token: 0x04000EC4 RID: 3780
			private int freeMemoryRequiredToStartInMBytes;
		}

		// Token: 0x020002AD RID: 685
		public class LatencyTrackerConfig
		{
			// Token: 0x06001DED RID: 7661 RVA: 0x00076AD4 File Offset: 0x00074CD4
			private LatencyTrackerConfig()
			{
			}

			// Token: 0x170008A9 RID: 2217
			// (get) Token: 0x06001DEE RID: 7662 RVA: 0x00076ADC File Offset: 0x00074CDC
			public TimeSpan ServerLatencyThreshold
			{
				get
				{
					return this.serverLatencyThreshold;
				}
			}

			// Token: 0x170008AA RID: 2218
			// (get) Token: 0x06001DEF RID: 7663 RVA: 0x00076AE4 File Offset: 0x00074CE4
			public TimeSpan PercentileLatencyExpiryInterval
			{
				get
				{
					return this.percentileLatencyExpiryInterval;
				}
			}

			// Token: 0x170008AB RID: 2219
			// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00076AEC File Offset: 0x00074CEC
			public ushort PercentileLatencyInfinitySeconds
			{
				get
				{
					return this.percentileLatencyInfinitySeconds;
				}
			}

			// Token: 0x170008AC RID: 2220
			// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00076AF4 File Offset: 0x00074CF4
			// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x00076AFC File Offset: 0x00074CFC
			public bool TrustExternalPickupReceivedHeaders
			{
				get
				{
					return this.trustExternalPickupReceivedHeaders;
				}
				internal set
				{
					this.trustExternalPickupReceivedHeaders = value;
				}
			}

			// Token: 0x170008AD RID: 2221
			// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x00076B05 File Offset: 0x00074D05
			public bool MessageLatencyEnabled
			{
				get
				{
					return this.messageLatencyEnabled;
				}
			}

			// Token: 0x170008AE RID: 2222
			// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00076B0D File Offset: 0x00074D0D
			public bool TreeLatencyTrackingEnabled
			{
				get
				{
					return this.treeLatencyTrackingEnabled;
				}
			}

			// Token: 0x170008AF RID: 2223
			// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x00076B15 File Offset: 0x00074D15
			public TimeSpan ComponentThresholdInterval
			{
				get
				{
					return this.componentThresholdInterval;
				}
			}

			// Token: 0x170008B0 RID: 2224
			// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00076B1D File Offset: 0x00074D1D
			public TimeSpan MinInterestingUnknownInterval
			{
				get
				{
					return this.minInterestingUnknownInterval;
				}
			}

			// Token: 0x06001DF7 RID: 7671 RVA: 0x00076B28 File Offset: 0x00074D28
			public static TransportAppConfig.LatencyTrackerConfig Load()
			{
				return new TransportAppConfig.LatencyTrackerConfig
				{
					serverLatencyThreshold = TransportAppConfig.GetConfigTimeSpan("MinTotalServerLatencyToLog", TimeSpan.Zero, TransportAppConfig.LatencyTrackerConfig.MaxLatency, TransportAppConfig.LatencyTrackerConfig.DefaultServerLatencyThreshold),
					componentThresholdInterval = TransportAppConfig.GetConfigTimeSpan("ComponentThresholdInterval", TimeSpan.Zero, TransportAppConfig.LatencyTrackerConfig.MaxLatency, TransportAppConfig.LatencyTrackerConfig.DefaultComponentThresholdInterval),
					minInterestingUnknownInterval = TransportAppConfig.GetConfigTimeSpan("MinInterestingUnknownInterval", TimeSpan.Zero, TransportAppConfig.LatencyTrackerConfig.MaxLatency, TransportAppConfig.LatencyTrackerConfig.DefaultMinInterestingUnknownInterval),
					percentileLatencyExpiryInterval = TransportAppConfig.GetConfigTimeSpan("PercentileLatencyExpiryInterval", TransportAppConfig.LatencyTrackerConfig.MinPercentileLatencyExpiryInterval, TransportAppConfig.LatencyTrackerConfig.MaxPercentileLatencyExpiryInterval, TransportAppConfig.LatencyTrackerConfig.DefaultPercentileLatencyExpiryInterval),
					percentileLatencyInfinitySeconds = TransportAppConfig.LatencyTrackerConfig.GetPercentileLatencyInfinitySeconds(),
					trustExternalPickupReceivedHeaders = TransportAppConfig.GetConfigBool("TrustExternalPickupReceivedHeaders", false),
					messageLatencyEnabled = TransportAppConfig.GetConfigBool("MessageLatencyEnabled", true),
					treeLatencyTrackingEnabled = TransportAppConfig.GetConfigBool("TreeLatencyTrackingEnabled", false)
				};
			}

			// Token: 0x06001DF8 RID: 7672 RVA: 0x00076BF8 File Offset: 0x00074DF8
			private static ushort GetPercentileLatencyInfinitySeconds()
			{
				return (ushort)(TransportAppConfig.GetConfigTimeSpan("PercentileLatencyInfinityInterval", TransportAppConfig.LatencyTrackerConfig.MinPercentileLatencyInfinityInterval, TransportAppConfig.LatencyTrackerConfig.MaxPercentileLatencyInfinityInterval, TransportAppConfig.LatencyTrackerConfig.DefaultPercentileLatencyInfinityInterval).Ticks / 10000000L);
			}

			// Token: 0x04000EC6 RID: 3782
			public const bool DefaultTrustExternalPickupReceivedHeaders = false;

			// Token: 0x04000EC7 RID: 3783
			public const string TrustExternalPickupReceivedHeadersLabel = "TrustExternalPickupReceivedHeaders";

			// Token: 0x04000EC8 RID: 3784
			public const string ServerLatencyThresholdLabel = "MinTotalServerLatencyToLog";

			// Token: 0x04000EC9 RID: 3785
			public const string PercentileLatencyExpiryIntervalLabel = "PercentileLatencyExpiryInterval";

			// Token: 0x04000ECA RID: 3786
			public const string PercentileLatencyInfinityIntervalLabel = "PercentileLatencyInfinityInterval";

			// Token: 0x04000ECB RID: 3787
			public const string MessageLatencyEnabledLabel = "MessageLatencyEnabled";

			// Token: 0x04000ECC RID: 3788
			public const string TreeLatencyTrackingEnabledLabel = "TreeLatencyTrackingEnabled";

			// Token: 0x04000ECD RID: 3789
			public const string ComponentThresholdIntervalLabel = "ComponentThresholdInterval";

			// Token: 0x04000ECE RID: 3790
			public const string MinInterestingUnknownIntervalLabel = "MinInterestingUnknownInterval";

			// Token: 0x04000ECF RID: 3791
			public const bool DefaultMessageLatencyEnabled = true;

			// Token: 0x04000ED0 RID: 3792
			public const bool DefaultTreeLatencyTrackingEnabled = false;

			// Token: 0x04000ED1 RID: 3793
			public static readonly TimeSpan MaxLatency = TimeSpan.FromSeconds(43200.0);

			// Token: 0x04000ED2 RID: 3794
			public static readonly TimeSpan DefaultServerLatencyThreshold = TimeSpan.Zero;

			// Token: 0x04000ED3 RID: 3795
			public static readonly TimeSpan DefaultPercentileLatencyExpiryInterval = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000ED4 RID: 3796
			public static readonly TimeSpan MinPercentileLatencyExpiryInterval = TimeSpan.FromSeconds(15.0);

			// Token: 0x04000ED5 RID: 3797
			public static readonly TimeSpan MaxPercentileLatencyExpiryInterval = TimeSpan.FromHours(1.0);

			// Token: 0x04000ED6 RID: 3798
			public static readonly TimeSpan DefaultPercentileLatencyInfinityInterval = TimeSpan.FromMinutes(15.0);

			// Token: 0x04000ED7 RID: 3799
			public static readonly TimeSpan MinPercentileLatencyInfinityInterval = TimeSpan.FromSeconds(2.0);

			// Token: 0x04000ED8 RID: 3800
			public static readonly TimeSpan MaxPercentileLatencyInfinityInterval = TimeSpan.FromHours(1.0);

			// Token: 0x04000ED9 RID: 3801
			public static readonly TimeSpan DefaultComponentThresholdInterval = TimeSpan.FromMilliseconds(1.0);

			// Token: 0x04000EDA RID: 3802
			public static readonly TimeSpan DefaultMinInterestingUnknownInterval = TimeSpan.FromMilliseconds(1.0);

			// Token: 0x04000EDB RID: 3803
			private TimeSpan serverLatencyThreshold;

			// Token: 0x04000EDC RID: 3804
			private TimeSpan percentileLatencyExpiryInterval;

			// Token: 0x04000EDD RID: 3805
			private ushort percentileLatencyInfinitySeconds;

			// Token: 0x04000EDE RID: 3806
			private bool trustExternalPickupReceivedHeaders;

			// Token: 0x04000EDF RID: 3807
			private bool messageLatencyEnabled;

			// Token: 0x04000EE0 RID: 3808
			private bool treeLatencyTrackingEnabled;

			// Token: 0x04000EE1 RID: 3809
			private TimeSpan componentThresholdInterval;

			// Token: 0x04000EE2 RID: 3810
			private TimeSpan minInterestingUnknownInterval;
		}

		// Token: 0x020002AE RID: 686
		public class RecipientValidatorConfig
		{
			// Token: 0x06001DFA RID: 7674 RVA: 0x00076CF2 File Offset: 0x00074EF2
			private RecipientValidatorConfig()
			{
			}

			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00076CFA File Offset: 0x00074EFA
			public TimeSpan RefreshInterval
			{
				get
				{
					return this.refreshInterval;
				}
			}

			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x06001DFC RID: 7676 RVA: 0x00076D02 File Offset: 0x00074F02
			public TimeSpan ReloadInterval
			{
				get
				{
					return this.reloadInterval;
				}
			}

			// Token: 0x06001DFD RID: 7677 RVA: 0x00076D0C File Offset: 0x00074F0C
			public static TransportAppConfig.RecipientValidatorConfig Load()
			{
				return new TransportAppConfig.RecipientValidatorConfig
				{
					refreshInterval = TransportAppConfig.GetConfigTimeSpan("RefreshInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.RecipientValidatorConfig.DefaultRefreshInterval),
					reloadInterval = TransportAppConfig.GetConfigTimeSpan("ReloadInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.RecipientValidatorConfig.DefaultReloadInterval)
				};
			}

			// Token: 0x04000EE3 RID: 3811
			private static readonly TimeSpan DefaultRefreshInterval = new TimeSpan(0, 5, 0);

			// Token: 0x04000EE4 RID: 3812
			private static readonly TimeSpan DefaultReloadInterval = new TimeSpan(4, 0, 0);

			// Token: 0x04000EE5 RID: 3813
			private TimeSpan refreshInterval;

			// Token: 0x04000EE6 RID: 3814
			private TimeSpan reloadInterval;
		}

		// Token: 0x020002AF RID: 687
		public sealed class PerTenantCacheConfig
		{
			// Token: 0x06001DFF RID: 7679 RVA: 0x00076D8C File Offset: 0x00074F8C
			private PerTenantCacheConfig()
			{
			}

			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x06001E00 RID: 7680 RVA: 0x00076D94 File Offset: 0x00074F94
			public ByteQuantifiedSize TransportSettingsCacheMaxSize
			{
				get
				{
					return this.transportSettingsCacheMaxSize;
				}
			}

			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x06001E01 RID: 7681 RVA: 0x00076D9C File Offset: 0x00074F9C
			public TimeSpan TransportSettingsCacheExpiryInterval
			{
				get
				{
					return this.transportSettingsCacheExpiryInterval;
				}
			}

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x06001E02 RID: 7682 RVA: 0x00076DA4 File Offset: 0x00074FA4
			public TimeSpan TransportSettingsCacheCleanupInterval
			{
				get
				{
					return this.transportSettingsCacheCleanupInterval;
				}
			}

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00076DAC File Offset: 0x00074FAC
			public ByteQuantifiedSize PerimeterSettingsCacheMaxSize
			{
				get
				{
					return this.perimeterSettingsCacheMaxSize;
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x06001E04 RID: 7684 RVA: 0x00076DB4 File Offset: 0x00074FB4
			public TimeSpan PerimeterSettingsCacheExpiryInterval
			{
				get
				{
					return this.perimeterSettingsCacheExpiryInterval;
				}
			}

			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00076DBC File Offset: 0x00074FBC
			public TimeSpan PerimeterSettingsCacheCleanupInterval
			{
				get
				{
					return this.perimeterSettingsCacheCleanupInterval;
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x06001E06 RID: 7686 RVA: 0x00076DC4 File Offset: 0x00074FC4
			public ByteQuantifiedSize OrganizationMailboxDatabaseCacheMaxSize
			{
				get
				{
					return this.organizationMailboxDatabaseCacheMaxSize;
				}
			}

			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00076DCC File Offset: 0x00074FCC
			public TimeSpan OrganizationMailboxDatabaseCacheExpiryInterval
			{
				get
				{
					return this.organizationMailboxDatabaseCacheExpiryInterval;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06001E08 RID: 7688 RVA: 0x00076DD4 File Offset: 0x00074FD4
			public TimeSpan OrganizationMailboxDatabaseCacheCleanupInterval
			{
				get
				{
					return this.organizationMailboxDatabaseCacheCleanupInterval;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00076DDC File Offset: 0x00074FDC
			public ByteQuantifiedSize JournalingRulesCacheMaxSize
			{
				get
				{
					return this.journalingRulesCacheMaxSize;
				}
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00076DE4 File Offset: 0x00074FE4
			public ByteQuantifiedSize ReconciliationCacheConfigMaxSize
			{
				get
				{
					return this.reconciliationCacheConfigMaxSize;
				}
			}

			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00076DEC File Offset: 0x00074FEC
			public TimeSpan JournalingCacheExpiryInterval
			{
				get
				{
					return this.journalingCacheExpiryInterval;
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06001E0C RID: 7692 RVA: 0x00076DF4 File Offset: 0x00074FF4
			public TimeSpan JournalingCacheCleanupInterval
			{
				get
				{
					return this.journalingCacheCleanupInterval;
				}
			}

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00076DFC File Offset: 0x00074FFC
			public ByteQuantifiedSize MicrosoftExchangeRecipientCacheMaxSize
			{
				get
				{
					return this.microsoftExchangeRecipientCacheMaxSize;
				}
			}

			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06001E0E RID: 7694 RVA: 0x00076E04 File Offset: 0x00075004
			public TimeSpan MicrosoftExchangeRecipientCacheExpiryInterval
			{
				get
				{
					return this.microsoftExchangeRecipientCacheExpiryInterval;
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00076E0C File Offset: 0x0007500C
			public TimeSpan MicrosoftExchangeRecipientCacheCleanupInterval
			{
				get
				{
					return this.microsoftExchangeRecipientCacheCleanupInterval;
				}
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00076E14 File Offset: 0x00075014
			public ByteQuantifiedSize RemoteDomainsCacheMaxSize
			{
				get
				{
					return this.remoteDomainsCacheMaxSize;
				}
			}

			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00076E1C File Offset: 0x0007501C
			public TimeSpan RemoteDomainsCacheExpiryInterval
			{
				get
				{
					return this.remoteDomainsCacheExpiryInterval;
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00076E24 File Offset: 0x00075024
			public TimeSpan RemoteDomainsCacheCleanupInterval
			{
				get
				{
					return this.remoteDomainsCacheCleanupInterval;
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00076E2C File Offset: 0x0007502C
			public ByteQuantifiedSize AcceptedDomainsCacheMaxSize
			{
				get
				{
					return this.acceptedDomainsCacheMaxSize;
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00076E34 File Offset: 0x00075034
			public TimeSpan AcceptedDomainsCacheExpiryInterval
			{
				get
				{
					return this.acceptedDomainsCacheExpiryInterval;
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00076E3C File Offset: 0x0007503C
			public TimeSpan AcceptedDomainsCacheCleanupInterval
			{
				get
				{
					return this.acceptedDomainsCacheCleanupInterval;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00076E44 File Offset: 0x00075044
			public ByteQuantifiedSize TransportRulesCacheMaxSize
			{
				get
				{
					return this.transportRulesCacheMaxSize;
				}
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00076E4C File Offset: 0x0007504C
			public TimeSpan TransportRulesCacheExpiryInterval
			{
				get
				{
					return this.transportRulesCacheExpiryInterval;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00076E54 File Offset: 0x00075054
			public TimeSpan TransportRulesCacheCleanupInterval
			{
				get
				{
					return this.transportRulesCacheCleanupInterval;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00076E5C File Offset: 0x0007505C
			public ByteQuantifiedSize OutboundConnectorsCacheSize
			{
				get
				{
					return this.outboundConnectorsCacheMaxSize;
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x06001E1A RID: 7706 RVA: 0x00076E64 File Offset: 0x00075064
			public TimeSpan OutboundConnectorsCacheExpirationInterval
			{
				get
				{
					return this.outboundConnectorsCacheExpiryInterval;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00076E6C File Offset: 0x0007506C
			public TimeSpan OutboundConnectorsCacheCleanupInterval
			{
				get
				{
					return this.outboundConnectorsCacheCleanupInterval;
				}
			}

			// Token: 0x06001E1C RID: 7708 RVA: 0x00076E74 File Offset: 0x00075074
			public static TransportAppConfig.PerTenantCacheConfig Load()
			{
				return new TransportAppConfig.PerTenantCacheConfig
				{
					transportSettingsCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("TransportSettingsCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					perimeterSettingsCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("PerimeterSettingsCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					organizationMailboxDatabaseCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("OrganizationMailboxDatabaseCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					journalingRulesCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("JournalingRulesCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					reconciliationCacheConfigMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("ReconciliationConfigCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					microsoftExchangeRecipientCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("MicrosoftExchangeRecipientCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					remoteDomainsCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("RemoteDomainsCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					acceptedDomainsCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("AcceptedDomainsCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					transportRulesCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("TransportRulesCacheMaxSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.TransportRulesCacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.TransportRulesCacheSizeMaxValue),
					outboundConnectorsCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("OutboundConnectorsCacheSize", TransportAppConfig.PerTenantCacheConfig.CacheSizeMinValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeMaxValue, TransportAppConfig.PerTenantCacheConfig.CacheSizeDefaultValue),
					transportSettingsCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("TransportSettingsCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					perimeterSettingsCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("PerimeterSettingsCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					organizationMailboxDatabaseCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("OrganizationMailboxDatabaseCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					journalingCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("JournalingRulesCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					microsoftExchangeRecipientCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("MicrosoftExchangeRecipientCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					remoteDomainsCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("RemoteDomainsCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					acceptedDomainsCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("AcceptedDomainsCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					transportRulesCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("TransportRulesCacheExpiryInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					outboundConnectorsCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("OutboundConnectorsCacheExpirationInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheExpiryIntervalDefault),
					transportSettingsCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("TransportSettingsCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					perimeterSettingsCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("PerimeterSettingsCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					organizationMailboxDatabaseCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("OrganizationMailboxDatabaseCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					journalingCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("JournalingRulesCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					microsoftExchangeRecipientCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("MicrosoftExchangeRecipientCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					remoteDomainsCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("RemoteDomainsCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					acceptedDomainsCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("AcceptedDomainsCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					transportRulesCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("TransportRulesCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault),
					outboundConnectorsCacheCleanupInterval = TransportAppConfig.GetConfigTimeSpan("OutboundConnectorsCacheCleanupInterval", TransportAppConfig.PerTenantCacheConfig.CacheIntervalMin, TransportAppConfig.PerTenantCacheConfig.CacheIntervalMax, TransportAppConfig.PerTenantCacheConfig.CacheCleanupIntervalDefault)
				};
			}

			// Token: 0x04000EE7 RID: 3815
			private const string TransportSettingsCacheMaxSizeString = "TransportSettingsCacheMaxSize";

			// Token: 0x04000EE8 RID: 3816
			private const string PerimeterSettingsCacheMaxSizeString = "PerimeterSettingsCacheMaxSize";

			// Token: 0x04000EE9 RID: 3817
			private const string OrganizationMailboxDatabaseCacheMaxSizeString = "OrganizationMailboxDatabaseCacheMaxSize";

			// Token: 0x04000EEA RID: 3818
			private const string JournalingRulesCacheMaxSizeString = "JournalingRulesCacheMaxSize";

			// Token: 0x04000EEB RID: 3819
			private const string ReconciliationConfigCacheMaxSizeString = "ReconciliationConfigCacheMaxSize";

			// Token: 0x04000EEC RID: 3820
			private const string MicrosoftExchangeRecipientCacheMaxSizeString = "MicrosoftExchangeRecipientCacheMaxSize";

			// Token: 0x04000EED RID: 3821
			private const string RemoteDomainsCacheMaxSizeString = "RemoteDomainsCacheMaxSize";

			// Token: 0x04000EEE RID: 3822
			private const string AcceptedDomainsCacheMaxSizeString = "AcceptedDomainsCacheMaxSize";

			// Token: 0x04000EEF RID: 3823
			private const string TransportRulesCacheMaxSizeString = "TransportRulesCacheMaxSize";

			// Token: 0x04000EF0 RID: 3824
			private const string TransportSettingsCacheExpiryIntervalString = "TransportSettingsCacheExpiryInterval";

			// Token: 0x04000EF1 RID: 3825
			private const string PerimeterSettingsCacheExpiryIntervalString = "PerimeterSettingsCacheExpiryInterval";

			// Token: 0x04000EF2 RID: 3826
			private const string OrganizationMailboxDatabaseCacheExpiryIntervalString = "OrganizationMailboxDatabaseCacheExpiryInterval";

			// Token: 0x04000EF3 RID: 3827
			private const string JournalingRulesCacheExpiryIntervalString = "JournalingRulesCacheExpiryInterval";

			// Token: 0x04000EF4 RID: 3828
			private const string MicrosoftExchangeRecipientCacheExpiryIntervalString = "MicrosoftExchangeRecipientCacheExpiryInterval";

			// Token: 0x04000EF5 RID: 3829
			private const string RemoteDomainsCacheExpiryIntervalString = "RemoteDomainsCacheExpiryInterval";

			// Token: 0x04000EF6 RID: 3830
			private const string AcceptedDomainsCacheExpiryIntervalString = "AcceptedDomainsCacheExpiryInterval";

			// Token: 0x04000EF7 RID: 3831
			private const string TransportRulesCacheExpiryIntervalString = "TransportRulesCacheExpiryInterval";

			// Token: 0x04000EF8 RID: 3832
			private const string TransportSettingsCacheCleanupIntervalString = "TransportSettingsCacheCleanupInterval";

			// Token: 0x04000EF9 RID: 3833
			private const string PerimeterSettingsCacheCleanupIntervalString = "PerimeterSettingsCacheCleanupInterval";

			// Token: 0x04000EFA RID: 3834
			private const string OrganizationMailboxDatabaseCacheCleanupIntervalString = "OrganizationMailboxDatabaseCacheCleanupInterval";

			// Token: 0x04000EFB RID: 3835
			private const string JournalingRulesCacheCleanupIntervalString = "JournalingRulesCacheCleanupInterval";

			// Token: 0x04000EFC RID: 3836
			private const string MicrosoftExchangeRecipientCacheCleanupIntervalString = "MicrosoftExchangeRecipientCacheCleanupInterval";

			// Token: 0x04000EFD RID: 3837
			private const string RemoteDomainsCacheCleanupIntervalString = "RemoteDomainsCacheCleanupInterval";

			// Token: 0x04000EFE RID: 3838
			private const string AcceptedDomainsCacheCleanupIntervalString = "AcceptedDomainsCacheCleanupInterval";

			// Token: 0x04000EFF RID: 3839
			private const string TransportRulesCacheCleanupIntervalString = "TransportRulesCacheCleanupInterval";

			// Token: 0x04000F00 RID: 3840
			public const string OutboundConnectorsCacheSizeString = "OutboundConnectorsCacheSize";

			// Token: 0x04000F01 RID: 3841
			public const string OutboundConnectorsCacheExpirationIntervalString = "OutboundConnectorsCacheExpirationInterval";

			// Token: 0x04000F02 RID: 3842
			public const string OutboundConnectorsCacheCleanupIntervalString = "OutboundConnectorsCacheCleanupInterval";

			// Token: 0x04000F03 RID: 3843
			private static readonly ByteQuantifiedSize CacheSizeMinValue = ByteQuantifiedSize.FromMB(0UL);

			// Token: 0x04000F04 RID: 3844
			private static readonly ByteQuantifiedSize CacheSizeMaxValue = ByteQuantifiedSize.FromMB(100UL);

			// Token: 0x04000F05 RID: 3845
			private static readonly ByteQuantifiedSize TransportRulesCacheSizeMaxValue = ByteQuantifiedSize.FromMB(1000UL);

			// Token: 0x04000F06 RID: 3846
			private static readonly ByteQuantifiedSize CacheSizeDefaultValue = ByteQuantifiedSize.FromMB(20UL);

			// Token: 0x04000F07 RID: 3847
			private static readonly TimeSpan CacheIntervalMin = TimeSpan.MinValue;

			// Token: 0x04000F08 RID: 3848
			private static readonly TimeSpan CacheIntervalMax = TimeSpan.FromDays(1.0);

			// Token: 0x04000F09 RID: 3849
			private static readonly TimeSpan CacheExpiryIntervalDefault = TimeSpan.FromMinutes(15.0);

			// Token: 0x04000F0A RID: 3850
			private static readonly TimeSpan CacheCleanupIntervalDefault = TimeSpan.FromHours(2.0);

			// Token: 0x04000F0B RID: 3851
			private ByteQuantifiedSize perimeterSettingsCacheMaxSize;

			// Token: 0x04000F0C RID: 3852
			private TimeSpan perimeterSettingsCacheExpiryInterval;

			// Token: 0x04000F0D RID: 3853
			private TimeSpan perimeterSettingsCacheCleanupInterval;

			// Token: 0x04000F0E RID: 3854
			private ByteQuantifiedSize organizationMailboxDatabaseCacheMaxSize;

			// Token: 0x04000F0F RID: 3855
			private TimeSpan organizationMailboxDatabaseCacheExpiryInterval;

			// Token: 0x04000F10 RID: 3856
			private TimeSpan organizationMailboxDatabaseCacheCleanupInterval;

			// Token: 0x04000F11 RID: 3857
			private ByteQuantifiedSize transportSettingsCacheMaxSize;

			// Token: 0x04000F12 RID: 3858
			private TimeSpan transportSettingsCacheExpiryInterval;

			// Token: 0x04000F13 RID: 3859
			private TimeSpan transportSettingsCacheCleanupInterval;

			// Token: 0x04000F14 RID: 3860
			private ByteQuantifiedSize journalingRulesCacheMaxSize;

			// Token: 0x04000F15 RID: 3861
			private ByteQuantifiedSize reconciliationCacheConfigMaxSize;

			// Token: 0x04000F16 RID: 3862
			private TimeSpan journalingCacheExpiryInterval;

			// Token: 0x04000F17 RID: 3863
			private TimeSpan journalingCacheCleanupInterval;

			// Token: 0x04000F18 RID: 3864
			private ByteQuantifiedSize microsoftExchangeRecipientCacheMaxSize;

			// Token: 0x04000F19 RID: 3865
			private TimeSpan microsoftExchangeRecipientCacheExpiryInterval;

			// Token: 0x04000F1A RID: 3866
			private TimeSpan microsoftExchangeRecipientCacheCleanupInterval;

			// Token: 0x04000F1B RID: 3867
			private ByteQuantifiedSize remoteDomainsCacheMaxSize;

			// Token: 0x04000F1C RID: 3868
			private TimeSpan remoteDomainsCacheExpiryInterval;

			// Token: 0x04000F1D RID: 3869
			private TimeSpan remoteDomainsCacheCleanupInterval;

			// Token: 0x04000F1E RID: 3870
			private ByteQuantifiedSize acceptedDomainsCacheMaxSize;

			// Token: 0x04000F1F RID: 3871
			private TimeSpan acceptedDomainsCacheExpiryInterval;

			// Token: 0x04000F20 RID: 3872
			private TimeSpan acceptedDomainsCacheCleanupInterval;

			// Token: 0x04000F21 RID: 3873
			private ByteQuantifiedSize transportRulesCacheMaxSize;

			// Token: 0x04000F22 RID: 3874
			private TimeSpan transportRulesCacheExpiryInterval;

			// Token: 0x04000F23 RID: 3875
			private TimeSpan transportRulesCacheCleanupInterval;

			// Token: 0x04000F24 RID: 3876
			private ByteQuantifiedSize outboundConnectorsCacheMaxSize;

			// Token: 0x04000F25 RID: 3877
			private TimeSpan outboundConnectorsCacheExpiryInterval;

			// Token: 0x04000F26 RID: 3878
			private TimeSpan outboundConnectorsCacheCleanupInterval;
		}

		// Token: 0x020002B0 RID: 688
		public sealed class MessageThrottlingConfiguration
		{
			// Token: 0x06001E1E RID: 7710 RVA: 0x00077272 File Offset: 0x00075472
			private MessageThrottlingConfiguration()
			{
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0007727A File Offset: 0x0007547A
			public bool Enabled
			{
				get
				{
					return this.enabled;
				}
			}

			// Token: 0x06001E20 RID: 7712 RVA: 0x00077284 File Offset: 0x00075484
			public static TransportAppConfig.MessageThrottlingConfiguration Load()
			{
				return new TransportAppConfig.MessageThrottlingConfiguration
				{
					enabled = TransportAppConfig.GetConfigBool("MessageThrottlingEnabled", true)
				};
			}

			// Token: 0x04000F27 RID: 3879
			private const bool MessageThrottlingEnabledDefault = true;

			// Token: 0x04000F28 RID: 3880
			private const string EnabledPropertyString = "MessageThrottlingEnabled";

			// Token: 0x04000F29 RID: 3881
			private bool enabled;
		}

		// Token: 0x020002B1 RID: 689
		public sealed class SMTPOutConnectionCacheConfig
		{
			// Token: 0x06001E21 RID: 7713 RVA: 0x000772A9 File Offset: 0x000754A9
			private SMTPOutConnectionCacheConfig()
			{
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06001E22 RID: 7714 RVA: 0x000772B1 File Offset: 0x000754B1
			public bool EnableConnectionCache
			{
				get
				{
					return this.enableConnectionCache;
				}
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06001E23 RID: 7715 RVA: 0x000772B9 File Offset: 0x000754B9
			public bool EnableShadowConnectionCache
			{
				get
				{
					return this.enableShadowConnectionCache;
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000772C1 File Offset: 0x000754C1
			public int ConnectionCacheMaxNumberOfEntriesForOutboundProxy
			{
				get
				{
					return this.connectionCacheMaxNumberOfEntriesForOutboundProxy;
				}
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x06001E25 RID: 7717 RVA: 0x000772C9 File Offset: 0x000754C9
			public int ConnectionCacheMaxNumberOfEntriesForNonOutboundProxy
			{
				get
				{
					return this.connectionCacheMaxNumberOfEntriesForNonOutboundProxy;
				}
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x06001E26 RID: 7718 RVA: 0x000772D1 File Offset: 0x000754D1
			public TimeSpan ConnectionTimeoutForOutboundProxy
			{
				get
				{
					return this.connectionTimeoutForOutboundProxy;
				}
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x06001E27 RID: 7719 RVA: 0x000772D9 File Offset: 0x000754D9
			public TimeSpan ConnectionTimeoutForNonOutboundProxy
			{
				get
				{
					return this.connectionTimeoutForNonOutboundProxy;
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000772E1 File Offset: 0x000754E1
			public TimeSpan ConnectionInactivityTimeout
			{
				get
				{
					return this.connectionInactivityTimeoutForOutboundProxy;
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000772E9 File Offset: 0x000754E9
			public TimeSpan ConnectionInactivityTimeoutForNonOutboundProxy
			{
				get
				{
					return this.connectionInactivityTimeoutForNonOutboundProxy;
				}
			}

			// Token: 0x06001E2A RID: 7722 RVA: 0x000772F4 File Offset: 0x000754F4
			public static TransportAppConfig.SMTPOutConnectionCacheConfig Load()
			{
				return new TransportAppConfig.SMTPOutConnectionCacheConfig
				{
					enableConnectionCache = TransportAppConfig.GetConfigBool("EnableConnectionCache", TransportAppConfig.SMTPOutConnectionCacheConfig.EnableConnectionCacheDefaultValue),
					enableShadowConnectionCache = TransportAppConfig.GetConfigBool("EnableShadowConnectionCache", false),
					connectionCacheMaxNumberOfEntriesForOutboundProxy = TransportAppConfig.GetConfigInt("ConnectionCacheMaxNumberOfEntriesForOutboundProxy", TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheNumberOfEntryMinValue, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheNumberOfEntryMaxValue, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheNumberOfEntryDefaultValue),
					connectionCacheMaxNumberOfEntriesForNonOutboundProxy = TransportAppConfig.GetConfigInt("ConnectionCacheMaxNumberOfEntriesForNonOutboundProxy", TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheNumberOfEntryMinValue, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheNumberOfEntryMaxValue, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheNumberOfEntryDefaultValue),
					connectionTimeoutForOutboundProxy = TransportAppConfig.GetConfigTimeSpan("ConnectionTimeoutForOutboundProxy", TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMin, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMax, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheConnectionTimeOutDefault),
					connectionTimeoutForNonOutboundProxy = TransportAppConfig.GetConfigTimeSpan("ConnectionTimeoutForNonOutboundProxy", TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMin, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMax, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheConnectionTimeOutDefault),
					connectionInactivityTimeoutForOutboundProxy = TransportAppConfig.GetConfigTimeSpan("ConnectionInactivityTimeoutForOutboundProxy", TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMin, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMax, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheConnectionInactivityTimeOutDefault),
					connectionInactivityTimeoutForNonOutboundProxy = TransportAppConfig.GetConfigTimeSpan("ConnectionInactivityTimeoutForNonOutboundProxy", TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMin, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionTimeOutMax, TransportAppConfig.SMTPOutConnectionCacheConfig.ConnectionCacheConnectionInactivityTimeOutDefault)
				};
			}

			// Token: 0x04000F2A RID: 3882
			private const string EnableConnectionCacheString = "EnableConnectionCache";

			// Token: 0x04000F2B RID: 3883
			private const string ConnectionCacheMaxNumberOfEntriesForOutboundProxyString = "ConnectionCacheMaxNumberOfEntriesForOutboundProxy";

			// Token: 0x04000F2C RID: 3884
			private const string ConnectionCacheMaxNumberOfEntriesForNonOutboundProxyString = "ConnectionCacheMaxNumberOfEntriesForNonOutboundProxy";

			// Token: 0x04000F2D RID: 3885
			private const string ConnectionCacheConnectionTimeOutForOutboundProxyString = "ConnectionTimeoutForOutboundProxy";

			// Token: 0x04000F2E RID: 3886
			private const string ConnectionCacheConnectionTimeOutForNonOutboundProxyString = "ConnectionTimeoutForNonOutboundProxy";

			// Token: 0x04000F2F RID: 3887
			private const string ConnectionCacheConnectionInactivityTimeOutForOutboundProxyString = "ConnectionInactivityTimeoutForOutboundProxy";

			// Token: 0x04000F30 RID: 3888
			private const string ConnectionCacheConnectionInactivityTimeOutForNonOutboundProxyString = "ConnectionInactivityTimeoutForNonOutboundProxy";

			// Token: 0x04000F31 RID: 3889
			private static readonly bool EnableConnectionCacheDefaultValue = false;

			// Token: 0x04000F32 RID: 3890
			private static readonly int ConnectionCacheNumberOfEntryMinValue = 0;

			// Token: 0x04000F33 RID: 3891
			private static readonly int ConnectionCacheNumberOfEntryMaxValue = 100;

			// Token: 0x04000F34 RID: 3892
			private static readonly int ConnectionCacheNumberOfEntryDefaultValue = 50;

			// Token: 0x04000F35 RID: 3893
			private static readonly TimeSpan ConnectionTimeOutMin = TimeSpan.FromSeconds(5.0);

			// Token: 0x04000F36 RID: 3894
			private static readonly TimeSpan ConnectionTimeOutMax = TimeSpan.FromMinutes(60.0);

			// Token: 0x04000F37 RID: 3895
			private static readonly TimeSpan ConnectionCacheConnectionTimeOutDefault = TimeSpan.FromMinutes(10.0);

			// Token: 0x04000F38 RID: 3896
			private static readonly TimeSpan ConnectionCacheConnectionInactivityTimeOutDefault = TimeSpan.FromMinutes(5.0);

			// Token: 0x04000F39 RID: 3897
			private bool enableConnectionCache;

			// Token: 0x04000F3A RID: 3898
			private bool enableShadowConnectionCache;

			// Token: 0x04000F3B RID: 3899
			private int connectionCacheMaxNumberOfEntriesForOutboundProxy;

			// Token: 0x04000F3C RID: 3900
			private int connectionCacheMaxNumberOfEntriesForNonOutboundProxy;

			// Token: 0x04000F3D RID: 3901
			private TimeSpan connectionTimeoutForOutboundProxy;

			// Token: 0x04000F3E RID: 3902
			private TimeSpan connectionTimeoutForNonOutboundProxy;

			// Token: 0x04000F3F RID: 3903
			private TimeSpan connectionInactivityTimeoutForOutboundProxy;

			// Token: 0x04000F40 RID: 3904
			private TimeSpan connectionInactivityTimeoutForNonOutboundProxy;
		}

		// Token: 0x020002B2 RID: 690
		public sealed class IsMemberOfResolverConfiguration
		{
			// Token: 0x06001E2C RID: 7724 RVA: 0x0007745B File Offset: 0x0007565B
			public IsMemberOfResolverConfiguration(bool enabled, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration resolvedGroupsCacheConfiguration, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration expandedGroupsCacheConfiguration, bool disableDynamicGroups)
			{
				this.enabled = enabled;
				this.resolvedGroupsCacheConfiguration = resolvedGroupsCacheConfiguration;
				this.expandedGroupsCacheConfiguration = expandedGroupsCacheConfiguration;
				this.disableDynamicGroups = disableDynamicGroups;
			}

			// Token: 0x06001E2D RID: 7725 RVA: 0x00077480 File Offset: 0x00075680
			private IsMemberOfResolverConfiguration()
			{
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x06001E2E RID: 7726 RVA: 0x00077488 File Offset: 0x00075688
			public bool Enabled
			{
				get
				{
					return this.enabled;
				}
			}

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00077490 File Offset: 0x00075690
			public TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration ResolvedGroupsCacheConfiguration
			{
				get
				{
					return this.resolvedGroupsCacheConfiguration;
				}
			}

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x06001E30 RID: 7728 RVA: 0x00077498 File Offset: 0x00075698
			public TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration ExpandedGroupsCacheConfiguration
			{
				get
				{
					return this.expandedGroupsCacheConfiguration;
				}
			}

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000774A0 File Offset: 0x000756A0
			public bool DisableDynamicGroups
			{
				get
				{
					return this.disableDynamicGroups;
				}
			}

			// Token: 0x06001E32 RID: 7730 RVA: 0x000774A8 File Offset: 0x000756A8
			public static TransportAppConfig.IsMemberOfResolverConfiguration Load(string componentName)
			{
				TransportAppConfig.IsMemberOfResolverConfiguration isMemberOfResolverConfiguration = new TransportAppConfig.IsMemberOfResolverConfiguration();
				string label = string.Format("{0}_IsMemberOfResolver_Enabled", componentName);
				isMemberOfResolverConfiguration.enabled = TransportAppConfig.GetConfigBool(label, true);
				isMemberOfResolverConfiguration.resolvedGroupsCacheConfiguration = TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.Load(componentName, "ResolvedGroupsCache", TransportAppConfig.IsMemberOfResolverConfiguration.ResolvedGroupsMaxCacheSize, TransportAppConfig.IsMemberOfResolverConfiguration.ResolvedGroupsDefaultCacheSize);
				isMemberOfResolverConfiguration.expandedGroupsCacheConfiguration = TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.Load(componentName, "ExpandedGroupsCache", TransportAppConfig.IsMemberOfResolverConfiguration.ExpandedGroupsMaxCacheSize, TransportAppConfig.IsMemberOfResolverConfiguration.ExpandedGroupsDefaultCacheSize);
				string label2 = string.Format("{0}_IsMemberOfResolver_DisableDynamicGroups", componentName);
				isMemberOfResolverConfiguration.disableDynamicGroups = TransportAppConfig.GetConfigBool(label2, false);
				return isMemberOfResolverConfiguration;
			}

			// Token: 0x04000F41 RID: 3905
			private const string ResolvedGroupsCacheName = "ResolvedGroupsCache";

			// Token: 0x04000F42 RID: 3906
			private const string ExpandedGroupsCacheName = "ExpandedGroupsCache";

			// Token: 0x04000F43 RID: 3907
			private const string EnabledTemplate = "{0}_IsMemberOfResolver_Enabled";

			// Token: 0x04000F44 RID: 3908
			private const string DisableDynamicGroupsTemplate = "{0}_IsMemberOfResolver_DisableDynamicGroups";

			// Token: 0x04000F45 RID: 3909
			private static readonly ByteQuantifiedSize ResolvedGroupsMaxCacheSize = ByteQuantifiedSize.FromGB(1UL);

			// Token: 0x04000F46 RID: 3910
			private static readonly ByteQuantifiedSize ResolvedGroupsDefaultCacheSize = ByteQuantifiedSize.FromMB(32UL);

			// Token: 0x04000F47 RID: 3911
			private static readonly ByteQuantifiedSize ExpandedGroupsMaxCacheSize = ByteQuantifiedSize.FromGB(64UL);

			// Token: 0x04000F48 RID: 3912
			private static readonly ByteQuantifiedSize ExpandedGroupsDefaultCacheSize = ByteQuantifiedSize.FromMB(512UL);

			// Token: 0x04000F49 RID: 3913
			private bool enabled;

			// Token: 0x04000F4A RID: 3914
			private TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration resolvedGroupsCacheConfiguration;

			// Token: 0x04000F4B RID: 3915
			private TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration expandedGroupsCacheConfiguration;

			// Token: 0x04000F4C RID: 3916
			private bool disableDynamicGroups;

			// Token: 0x020002B3 RID: 691
			public sealed class CacheConfiguration
			{
				// Token: 0x06001E34 RID: 7732 RVA: 0x0007755C File Offset: 0x0007575C
				public CacheConfiguration(long maxSizeInBytes, TimeSpan expirationInterval, TimeSpan cleanupInterval, TimeSpan purgeInterval, TimeSpan refreshInterval)
				{
					TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.ThrowIfInvalidSize(maxSizeInBytes, "maxSizeInBytes");
					TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.ThrowIfInvalidTimeSpan(expirationInterval, "expirationInterval");
					TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.ThrowIfInvalidTimeSpan(cleanupInterval, "cleanupInterval");
					TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.ThrowIfInvalidTimeSpan(refreshInterval, "refreshInterval");
					this.maxSize = ByteQuantifiedSize.FromBytes((ulong)maxSizeInBytes);
					this.expirationInterval = expirationInterval;
					this.cleanupInterval = cleanupInterval;
					this.purgeInterval = purgeInterval;
					this.refreshInterval = refreshInterval;
				}

				// Token: 0x06001E35 RID: 7733 RVA: 0x000775C6 File Offset: 0x000757C6
				private CacheConfiguration()
				{
				}

				// Token: 0x170008DC RID: 2268
				// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000775CE File Offset: 0x000757CE
				public ByteQuantifiedSize MaxSize
				{
					get
					{
						return this.maxSize;
					}
				}

				// Token: 0x170008DD RID: 2269
				// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000775D6 File Offset: 0x000757D6
				public TimeSpan ExpirationInterval
				{
					get
					{
						return this.expirationInterval;
					}
				}

				// Token: 0x170008DE RID: 2270
				// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000775DE File Offset: 0x000757DE
				public TimeSpan CleanupInterval
				{
					get
					{
						return this.cleanupInterval;
					}
				}

				// Token: 0x170008DF RID: 2271
				// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000775E6 File Offset: 0x000757E6
				public TimeSpan PurgeInterval
				{
					get
					{
						return this.purgeInterval;
					}
				}

				// Token: 0x170008E0 RID: 2272
				// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000775EE File Offset: 0x000757EE
				public TimeSpan RefreshInterval
				{
					get
					{
						return this.refreshInterval;
					}
				}

				// Token: 0x06001E3B RID: 7739 RVA: 0x000775F8 File Offset: 0x000757F8
				public static TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration Load(string componentName, string cacheName, ByteQuantifiedSize maxCacheSize, ByteQuantifiedSize defaultCacheSize)
				{
					TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration cacheConfiguration = new TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration();
					string label = string.Format("{0}_IsMemberOfResolver_{1}_MaxSize", componentName, cacheName);
					cacheConfiguration.maxSize = TransportAppConfig.GetConfigByteQuantifiedSize(label, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MinCacheSize, maxCacheSize, defaultCacheSize);
					string label2 = string.Format("{0}_IsMemberOfResolver_{1}_ExpirationInterval", componentName, cacheName);
					cacheConfiguration.expirationInterval = TransportAppConfig.GetConfigTimeSpan(label2, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MinCacheExpirationInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MaxCacheExpirationInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.DefaultCacheExpirationInterval);
					string label3 = string.Format("{0}_IsMemberOfResolver_{1}_CleanupInterval", componentName, cacheName);
					cacheConfiguration.cleanupInterval = TransportAppConfig.GetConfigTimeSpan(label3, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MinCacheCleanupInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MaxCacheCleanupInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.DefaultCacheCleanupInterval);
					string label4 = string.Format("{0}_IsMemberOfResolver_{1}_PurgeInterval", componentName, cacheName);
					cacheConfiguration.purgeInterval = TransportAppConfig.GetConfigTimeSpan(label4, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MinCachePurgeInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MaxCachePurgeInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.DefaultCachePurgeInterval);
					string label5 = string.Format("{0}_IsMemberOfResolver_{1}_RefreshInterval", componentName, cacheName);
					cacheConfiguration.refreshInterval = TransportAppConfig.GetConfigTimeSpan(label5, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MinCacheRefreshInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.MaxCacheRefreshInterval, TransportAppConfig.IsMemberOfResolverConfiguration.CacheConfiguration.DefaultCacheRefreshInterval);
					return cacheConfiguration;
				}

				// Token: 0x06001E3C RID: 7740 RVA: 0x000776D0 File Offset: 0x000758D0
				private static void ThrowIfInvalidSize(long size, string name)
				{
					if (size < 0L)
					{
						throw new ArgumentOutOfRangeException(name, size, name + " must be greater than or equal to 0");
					}
				}

				// Token: 0x06001E3D RID: 7741 RVA: 0x000776EF File Offset: 0x000758EF
				private static void ThrowIfInvalidTimeSpan(TimeSpan timeSpan, string name)
				{
					if (timeSpan.TotalSeconds < 0.0)
					{
						throw new ArgumentOutOfRangeException(name, timeSpan, name + " must be greater than or equal to 0");
					}
				}

				// Token: 0x04000F4D RID: 3917
				private const string ExpirationIntervalTemplate = "{0}_IsMemberOfResolver_{1}_ExpirationInterval";

				// Token: 0x04000F4E RID: 3918
				private const string CleanupIntervalTemplate = "{0}_IsMemberOfResolver_{1}_CleanupInterval";

				// Token: 0x04000F4F RID: 3919
				private const string PurgeIntervalTemplate = "{0}_IsMemberOfResolver_{1}_PurgeInterval";

				// Token: 0x04000F50 RID: 3920
				private const string RefreshIntervalTemplate = "{0}_IsMemberOfResolver_{1}_RefreshInterval";

				// Token: 0x04000F51 RID: 3921
				private const string MaxSizeTemplate = "{0}_IsMemberOfResolver_{1}_MaxSize";

				// Token: 0x04000F52 RID: 3922
				private static readonly ByteQuantifiedSize MinCacheSize = ByteQuantifiedSize.FromMB(0UL);

				// Token: 0x04000F53 RID: 3923
				private static readonly TimeSpan MinCacheExpirationInterval = TimeSpan.FromSeconds(5.0);

				// Token: 0x04000F54 RID: 3924
				private static readonly TimeSpan MaxCacheExpirationInterval = TimeSpan.FromDays(1.0);

				// Token: 0x04000F55 RID: 3925
				private static readonly TimeSpan DefaultCacheExpirationInterval = TimeSpan.FromHours(3.0);

				// Token: 0x04000F56 RID: 3926
				private static readonly TimeSpan MinCacheCleanupInterval = TimeSpan.FromSeconds(5.0);

				// Token: 0x04000F57 RID: 3927
				private static readonly TimeSpan MaxCacheCleanupInterval = TimeSpan.FromHours(4.0);

				// Token: 0x04000F58 RID: 3928
				private static readonly TimeSpan DefaultCacheCleanupInterval = TimeSpan.FromHours(1.0);

				// Token: 0x04000F59 RID: 3929
				private static readonly TimeSpan MinCachePurgeInterval = TimeSpan.FromSeconds(5.0);

				// Token: 0x04000F5A RID: 3930
				private static readonly TimeSpan MaxCachePurgeInterval = TimeSpan.FromHours(4.0);

				// Token: 0x04000F5B RID: 3931
				private static readonly TimeSpan DefaultCachePurgeInterval = TimeSpan.FromMinutes(5.0);

				// Token: 0x04000F5C RID: 3932
				private static readonly TimeSpan MinCacheRefreshInterval = TimeSpan.FromSeconds(5.0);

				// Token: 0x04000F5D RID: 3933
				private static readonly TimeSpan MaxCacheRefreshInterval = TimeSpan.FromHours(4.0);

				// Token: 0x04000F5E RID: 3934
				private static readonly TimeSpan DefaultCacheRefreshInterval = TimeSpan.FromMinutes(10.0);

				// Token: 0x04000F5F RID: 3935
				private ByteQuantifiedSize maxSize;

				// Token: 0x04000F60 RID: 3936
				private TimeSpan expirationInterval;

				// Token: 0x04000F61 RID: 3937
				private TimeSpan cleanupInterval;

				// Token: 0x04000F62 RID: 3938
				private TimeSpan purgeInterval;

				// Token: 0x04000F63 RID: 3939
				private TimeSpan refreshInterval;
			}
		}

		// Token: 0x020002B4 RID: 692
		public sealed class SmtpAvailabilityConfig
		{
			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00077819 File Offset: 0x00075A19
			public int SmtpAvailabilityMinConnectionsToMonitor
			{
				get
				{
					return this.smtpAvailabilityMinConnectionsToMonitor;
				}
			}

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00077821 File Offset: 0x00075A21
			public TimeSpan KerberosTicketCacheFlushMinInterval
			{
				get
				{
					return this.kerberosTicketCacheFlushMinInterval;
				}
			}

			// Token: 0x06001E41 RID: 7745 RVA: 0x0007782C File Offset: 0x00075A2C
			public static TransportAppConfig.SmtpAvailabilityConfig Load()
			{
				return new TransportAppConfig.SmtpAvailabilityConfig
				{
					smtpAvailabilityMinConnectionsToMonitor = TransportAppConfig.GetConfigInt("SmtpAvailabilityMinConnectionsToMonitor", 0, int.MaxValue, 20),
					kerberosTicketCacheFlushMinInterval = TransportAppConfig.GetConfigTimeSpan("KerberosTicketCacheFlushMinInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0))
				};
			}

			// Token: 0x04000F64 RID: 3940
			private const int DefaultSmtpAvailabilityMinConnectionsToMonitor = 20;

			// Token: 0x04000F65 RID: 3941
			private int smtpAvailabilityMinConnectionsToMonitor;

			// Token: 0x04000F66 RID: 3942
			private TimeSpan kerberosTicketCacheFlushMinInterval;
		}

		// Token: 0x020002B5 RID: 693
		public interface ISmtpMailCommandConfig
		{
			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06001E43 RID: 7747
			bool TransferAdditionalTenantDataThroughXAttr { get; }

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06001E44 RID: 7748
			bool UseAdditionalTenantDataFromXAttr { get; }
		}

		// Token: 0x020002B6 RID: 694
		public sealed class SmtpMailCommandConfig : TransportAppConfig.ISmtpMailCommandConfig
		{
			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06001E45 RID: 7749 RVA: 0x00077888 File Offset: 0x00075A88
			// (set) Token: 0x06001E46 RID: 7750 RVA: 0x00077890 File Offset: 0x00075A90
			public bool TransferAdditionalTenantDataThroughXAttr { get; private set; }

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00077899 File Offset: 0x00075A99
			// (set) Token: 0x06001E48 RID: 7752 RVA: 0x000778A1 File Offset: 0x00075AA1
			public bool UseAdditionalTenantDataFromXAttr { get; private set; }

			// Token: 0x06001E49 RID: 7753 RVA: 0x000778AC File Offset: 0x00075AAC
			public static TransportAppConfig.SmtpMailCommandConfig Load()
			{
				return new TransportAppConfig.SmtpMailCommandConfig
				{
					TransferAdditionalTenantDataThroughXAttr = TransportAppConfig.GetConfigBool("TransferAdditionalTenantDataThroughXATTR", false),
					UseAdditionalTenantDataFromXAttr = TransportAppConfig.GetConfigBool("UseAdditionalTenantDataFromXATTR", false)
				};
			}
		}

		// Token: 0x020002B7 RID: 695
		public sealed class SmtpDataConfig
		{
			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x06001E4B RID: 7755 RVA: 0x000778EA File Offset: 0x00075AEA
			public string SmtpDataResponse
			{
				get
				{
					return this.smtpDataResponse;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000778F2 File Offset: 0x00075AF2
			public bool InboundApplyMissingEncoding
			{
				get
				{
					return this.inboundApplyMissingEncoding;
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06001E4D RID: 7757 RVA: 0x000778FA File Offset: 0x00075AFA
			public bool OutboundRejectBareLinefeeds
			{
				get
				{
					return this.outboundRejectBareLinefeeds;
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00077902 File Offset: 0x00075B02
			public bool AcceptAndFixSmtpAddressWithInvalidLocalPart
			{
				get
				{
					return this.acceptAndFixSmtpAddressWithInvalidLocalPart;
				}
			}

			// Token: 0x06001E4F RID: 7759 RVA: 0x0007790C File Offset: 0x00075B0C
			public static TransportAppConfig.SmtpDataConfig Load()
			{
				return new TransportAppConfig.SmtpDataConfig
				{
					smtpDataResponse = TransportAppConfig.GetConfigString("SmtpCustomEndOfDataResponseString", "Queued mail for delivery"),
					inboundApplyMissingEncoding = TransportAppConfig.GetConfigBool("SmtpDataCommandInboundApplyMissingEncoding", true),
					outboundRejectBareLinefeeds = TransportAppConfig.GetConfigBool("SmtpDataCommandOutboundRejectBareLinefeeds", true),
					acceptAndFixSmtpAddressWithInvalidLocalPart = TransportAppConfig.GetConfigBool("AcceptAndFixSmtpAddressWithInvalidLocalPart", false)
				};
			}

			// Token: 0x04000F69 RID: 3945
			private const string DefaultSmtpDataResponse = "Queued mail for delivery";

			// Token: 0x04000F6A RID: 3946
			private string smtpDataResponse;

			// Token: 0x04000F6B RID: 3947
			private bool inboundApplyMissingEncoding;

			// Token: 0x04000F6C RID: 3948
			private bool outboundRejectBareLinefeeds;

			// Token: 0x04000F6D RID: 3949
			private bool acceptAndFixSmtpAddressWithInvalidLocalPart;
		}

		// Token: 0x020002B8 RID: 696
		public sealed class MessageContextBlobConfig
		{
			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00077970 File Offset: 0x00075B70
			public bool AdvertiseExtendedProperties
			{
				get
				{
					if (this.advertiseExtendedProperties == null)
					{
						return Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery || Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub;
					}
					return this.advertiseExtendedProperties.Value;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000779A7 File Offset: 0x00075BA7
			public bool AdvertiseADRecipientCache
			{
				get
				{
					if (this.advertiseADRecipientCache == null)
					{
						return Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub || Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery;
					}
					return this.advertiseADRecipientCache.Value;
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000779DE File Offset: 0x00075BDE
			public bool AdvertiseFastIndex
			{
				get
				{
					return this.advertiseFastIndex;
				}
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000779E6 File Offset: 0x00075BE6
			public bool TransferADRecipientCache
			{
				get
				{
					if (this.transferADRecipientCache == null)
					{
						return Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub || Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission;
					}
					return this.transferADRecipientCache.Value;
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06001E55 RID: 7765 RVA: 0x00077A20 File Offset: 0x00075C20
			public bool TransferExtendedProperties
			{
				get
				{
					bool result = Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub || Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery || Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission;
					if (this.transferExtendedProperties == null)
					{
						return result;
					}
					return this.transferExtendedProperties.Value;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06001E56 RID: 7766 RVA: 0x00077A76 File Offset: 0x00075C76
			public bool TransferFastIndex
			{
				get
				{
					return this.transferFastIndex;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06001E57 RID: 7767 RVA: 0x00077A7E File Offset: 0x00075C7E
			public ByteQuantifiedSize ExtendedPropertiesMaxBlobSize
			{
				get
				{
					return this.extendedPropertiesMaxBlobSize;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06001E58 RID: 7768 RVA: 0x00077A86 File Offset: 0x00075C86
			public ByteQuantifiedSize AdrcCacheMaxBlobSize
			{
				get
				{
					return this.adrcCacheMaxBlobSize;
				}
			}

			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x06001E59 RID: 7769 RVA: 0x00077A8E File Offset: 0x00075C8E
			public ByteQuantifiedSize FastIndexMaxBlobSize
			{
				get
				{
					return this.fastIndexMaxBlobSize;
				}
			}

			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x06001E5A RID: 7770 RVA: 0x00077A96 File Offset: 0x00075C96
			public bool WatsonReportOnFailureEnabled
			{
				get
				{
					return this.watsonReportOnFailureEnabled;
				}
			}

			// Token: 0x06001E5B RID: 7771 RVA: 0x00077AA0 File Offset: 0x00075CA0
			public static TransportAppConfig.MessageContextBlobConfig Load()
			{
				return new TransportAppConfig.MessageContextBlobConfig
				{
					advertiseADRecipientCache = TransportAppConfig.GetConfigNullableBool("SMTPReceiveAdvertiseADRecipientCache"),
					advertiseExtendedProperties = TransportAppConfig.GetConfigNullableBool("SMTPReceiveAdvertiseExtendedProperties"),
					advertiseFastIndex = TransportAppConfig.GetConfigBool("SMTPReceiveAdvertiseFastIndex", false),
					transferADRecipientCache = TransportAppConfig.GetConfigNullableBool("SMTPSendTransferADRecipientCache"),
					transferExtendedProperties = TransportAppConfig.GetConfigNullableBool("SMTPSendTransferExtendedProperties"),
					transferFastIndex = TransportAppConfig.GetConfigBool("SMTPSendTransferFastIndex", false),
					watsonReportOnFailureEnabled = TransportAppConfig.GetConfigBool("WatsonReportOnFailureEnabled", true),
					extendedPropertiesMaxBlobSize = TransportAppConfig.GetConfigByteQuantifiedSize("ExtendedPropertiesMaxSmtpBlobSize", ByteQuantifiedSize.MinValue, ByteQuantifiedSize.FromMB(500UL), ByteQuantifiedSize.FromMB(200UL)),
					adrcCacheMaxBlobSize = TransportAppConfig.GetConfigByteQuantifiedSize("AdrcCacheMaxBlobSize", ByteQuantifiedSize.MinValue, ByteQuantifiedSize.FromMB(500UL), ByteQuantifiedSize.FromMB(200UL)),
					fastIndexMaxBlobSize = TransportAppConfig.GetConfigByteQuantifiedSize("FastIndexMaxBlobSize", ByteQuantifiedSize.MinValue, ByteQuantifiedSize.FromMB(500UL), ByteQuantifiedSize.FromMB(200UL))
				};
			}

			// Token: 0x04000F6E RID: 3950
			private bool? advertiseExtendedProperties;

			// Token: 0x04000F6F RID: 3951
			private bool? advertiseADRecipientCache;

			// Token: 0x04000F70 RID: 3952
			private bool advertiseFastIndex;

			// Token: 0x04000F71 RID: 3953
			private bool? transferADRecipientCache;

			// Token: 0x04000F72 RID: 3954
			private bool? transferExtendedProperties;

			// Token: 0x04000F73 RID: 3955
			private bool transferFastIndex;

			// Token: 0x04000F74 RID: 3956
			private ByteQuantifiedSize extendedPropertiesMaxBlobSize;

			// Token: 0x04000F75 RID: 3957
			private ByteQuantifiedSize adrcCacheMaxBlobSize;

			// Token: 0x04000F76 RID: 3958
			private ByteQuantifiedSize fastIndexMaxBlobSize;

			// Token: 0x04000F77 RID: 3959
			private bool watsonReportOnFailureEnabled;
		}

		// Token: 0x020002B9 RID: 697
		public sealed class SmtpReceiveConfig
		{
			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00077BB0 File Offset: 0x00075DB0
			public bool TarpitMuaSubmission
			{
				get
				{
					return this.tarpitMuaSubmission;
				}
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x06001E5E RID: 7774 RVA: 0x00077BB8 File Offset: 0x00075DB8
			public bool SMTPAcceptAnyRecipient
			{
				get
				{
					return this.smtpAcceptAnyRecipient;
				}
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00077BC0 File Offset: 0x00075DC0
			public bool MailboxDeliveryAcceptAnonymousUsers
			{
				get
				{
					return this.mailboxDeliveryAcceptAnonymousUsers;
				}
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x06001E60 RID: 7776 RVA: 0x00077BC8 File Offset: 0x00075DC8
			public int MaxProxyHopCount
			{
				get
				{
					return this.maxProxyHopCount;
				}
			}

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00077BD0 File Offset: 0x00075DD0
			public bool RejectUnscopedMessages
			{
				get
				{
					return this.rejectUnscopedMessages;
				}
			}

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x06001E62 RID: 7778 RVA: 0x00077BD8 File Offset: 0x00075DD8
			public bool BlockedSessionLoggingEnabled
			{
				get
				{
					return this.blockedSessionLoggingEnabled;
				}
			}

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x06001E63 RID: 7779 RVA: 0x00077BE0 File Offset: 0x00075DE0
			public bool OneLevelWildcardMatchForCertSelection
			{
				get
				{
					return this.oneLevelWildcardMatchForCertSelection;
				}
			}

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x06001E64 RID: 7780 RVA: 0x00077BE8 File Offset: 0x00075DE8
			public bool WaitForSmtpSessionsAtShutdown
			{
				get
				{
					return this.waitForSmtpSessionsAtShutdown;
				}
			}

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00077BF0 File Offset: 0x00075DF0
			public bool GrantExchangeServerPermissions
			{
				get
				{
					return this.grantExchangeServerPermissions;
				}
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x06001E66 RID: 7782 RVA: 0x00077BF8 File Offset: 0x00075DF8
			public bool ExclusiveAddressUse
			{
				get
				{
					return this.exclusiveAddressUse;
				}
			}

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00077C00 File Offset: 0x00075E00
			public bool Ipv6ReceiveConnectionThrottlingEnabled
			{
				get
				{
					return this.ipv6ReceiveConnectionThrottlingEnabled;
				}
			}

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06001E68 RID: 7784 RVA: 0x00077C08 File Offset: 0x00075E08
			public bool ReceiveTlsThrottlingEnabled
			{
				get
				{
					return this.receiveTlsThrottlingEnabled;
				}
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06001E69 RID: 7785 RVA: 0x00077C10 File Offset: 0x00075E10
			public bool DisableHandleInheritance
			{
				get
				{
					return this.disableHandleInheritance;
				}
			}

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00077C18 File Offset: 0x00075E18
			public int NetworkConnectionReceiveBufferSize
			{
				get
				{
					return this.networkConnectionReceiveBufferSize;
				}
			}

			// Token: 0x06001E6B RID: 7787 RVA: 0x00077C20 File Offset: 0x00075E20
			public static TransportAppConfig.SmtpReceiveConfig Load()
			{
				return new TransportAppConfig.SmtpReceiveConfig
				{
					tarpitMuaSubmission = TransportAppConfig.GetConfigBool("TarpitMuaSubmission", false),
					smtpAcceptAnyRecipient = (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SmtpAcceptAnyRecipient.Enabled && TransportAppConfig.GetConfigBool("SMTPAcceptAnyRecipientOverride", false)),
					mailboxDeliveryAcceptAnonymousUsers = TransportAppConfig.GetConfigBool("MailboxDeliveryAcceptAnonymousUsers", false),
					maxProxyHopCount = TransportAppConfig.GetConfigInt("MaxProxyHopCount", 1, 10, 3),
					rejectUnscopedMessages = TransportAppConfig.GetConfigBool("RejectUnscopedMessages", true),
					blockedSessionLoggingEnabled = TransportAppConfig.GetConfigBool("BlockedSessionLoggingEnabled", false),
					oneLevelWildcardMatchForCertSelection = TransportAppConfig.GetConfigBool("SmtpReceiveOneLevelWildcardMatchForCertSelection", false),
					waitForSmtpSessionsAtShutdown = TransportAppConfig.GetConfigBool("WaitForSmtpSessionsAtShutdown", false),
					grantExchangeServerPermissions = TransportAppConfig.GetConfigBool("SmtpReceiveGrantExchangeServerPermissions", false),
					exclusiveAddressUse = TransportAppConfig.GetConfigBool("SmtpReceiveExclusiveAddressUse", true),
					ipv6ReceiveConnectionThrottlingEnabled = TransportAppConfig.GetConfigBool("Ipv6ReceiveConnectionThrottlingEnabled", false),
					receiveTlsThrottlingEnabled = TransportAppConfig.GetConfigBool("ReceiveTlsThrottlingEnabled", false),
					disableHandleInheritance = TransportAppConfig.GetConfigBool("SmtpReceiveDisableHandleInheritance", true),
					networkConnectionReceiveBufferSize = TransportAppConfig.GetConfigInt("NetworkConnectionReceiveBufferSize", 4096, int.MaxValue, 4096)
				};
			}

			// Token: 0x04000F78 RID: 3960
			private bool tarpitMuaSubmission;

			// Token: 0x04000F79 RID: 3961
			private bool smtpAcceptAnyRecipient;

			// Token: 0x04000F7A RID: 3962
			private bool mailboxDeliveryAcceptAnonymousUsers;

			// Token: 0x04000F7B RID: 3963
			private int maxProxyHopCount;

			// Token: 0x04000F7C RID: 3964
			private bool rejectUnscopedMessages;

			// Token: 0x04000F7D RID: 3965
			private bool blockedSessionLoggingEnabled;

			// Token: 0x04000F7E RID: 3966
			private bool oneLevelWildcardMatchForCertSelection;

			// Token: 0x04000F7F RID: 3967
			private bool waitForSmtpSessionsAtShutdown;

			// Token: 0x04000F80 RID: 3968
			private bool grantExchangeServerPermissions;

			// Token: 0x04000F81 RID: 3969
			private bool exclusiveAddressUse;

			// Token: 0x04000F82 RID: 3970
			private bool ipv6ReceiveConnectionThrottlingEnabled;

			// Token: 0x04000F83 RID: 3971
			private bool receiveTlsThrottlingEnabled;

			// Token: 0x04000F84 RID: 3972
			private bool disableHandleInheritance;

			// Token: 0x04000F85 RID: 3973
			private int networkConnectionReceiveBufferSize;
		}

		// Token: 0x020002BA RID: 698
		public sealed class SmtpSendConfig
		{
			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00077D57 File Offset: 0x00075F57
			public List<SmtpResponse> DowngradedResponses
			{
				get
				{
					return this.downgradedResponses;
				}
			}

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00077D5F File Offset: 0x00075F5F
			public List<SmtpResponse> UpgradedResponses
			{
				get
				{
					return this.upgradedResponses;
				}
			}

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00077D67 File Offset: 0x00075F67
			public bool RetryMessageOnRcptTransientError
			{
				get
				{
					return this.retryMessageOnRcptTransientError;
				}
			}

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00077D6F File Offset: 0x00075F6F
			public TimeSpan SuspiciousDisconnectRetryInterval
			{
				get
				{
					return this.suspiciousDisconnectRetryInterval;
				}
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x06001E71 RID: 7793 RVA: 0x00077D77 File Offset: 0x00075F77
			public int PoisonForRemoteThreshold
			{
				get
				{
					return this.poisonForRemoteThreshold;
				}
			}

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00077D7F File Offset: 0x00075F7F
			public bool OneLevelWildcardMatchForCertSelection
			{
				get
				{
					return this.oneLevelWildcardMatchForCertSelection;
				}
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00077D87 File Offset: 0x00075F87
			public bool SendFewerMessagesToSlowerServerEnabled
			{
				get
				{
					return this.sendFewerMessagesToSlowerServerEnabled;
				}
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00077D8F File Offset: 0x00075F8F
			public bool CacheOnlyUrlRetrievalForRemoteCertChain
			{
				get
				{
					return this.cacheOnlyUrlRetrievalForRemoteCertChain;
				}
			}

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00077D97 File Offset: 0x00075F97
			public bool TreatTransientErrorsAsPermanentErrors
			{
				get
				{
					return this.treatTransientErrorsAsPermanentErrors;
				}
			}

			// Token: 0x06001E76 RID: 7798 RVA: 0x00077DA0 File Offset: 0x00075FA0
			public static TransportAppConfig.SmtpSendConfig Load()
			{
				TransportAppConfig.SmtpSendConfig smtpSendConfig = new TransportAppConfig.SmtpSendConfig();
				TransportAppConfig.ConfigurationListsSection configurationListsSection = ConfigurationManager.GetSection("customLists") as TransportAppConfig.ConfigurationListsSection;
				if (configurationListsSection != null)
				{
					smtpSendConfig.downgradedResponses = TransportAppConfig.GetConfigList<SmtpResponse>(configurationListsSection.DowngradedResponses, new TransportAppConfig.TryParse<SmtpResponse>(SmtpResponse.TryParse));
					smtpSendConfig.upgradedResponses = TransportAppConfig.GetConfigList<SmtpResponse>(configurationListsSection.UpgradedResponses, new TransportAppConfig.TryParse<SmtpResponse>(SmtpResponse.TryParse));
				}
				smtpSendConfig.retryMessageOnRcptTransientError = TransportAppConfig.GetConfigBool("SMTPSendRetryMessageOnRcptTransientError", true);
				smtpSendConfig.poisonForRemoteThreshold = TransportAppConfig.GetConfigInt("SMTPSendPoisonForRemoteThreshold", 0, 100, 1000);
				smtpSendConfig.oneLevelWildcardMatchForCertSelection = TransportAppConfig.GetConfigBool("SmtpSendOneLevelWildcardMatchForCertSelection", false);
				smtpSendConfig.suspiciousDisconnectRetryInterval = TransportAppConfig.GetConfigTimeSpan("SMTPSendSuspiciousDisconnectRetryInterval", TimeSpan.Zero, TimeSpan.FromHours(12.0), TimeSpan.FromMinutes(10.0));
				smtpSendConfig.sendFewerMessagesToSlowerServerEnabled = TransportAppConfig.GetConfigBool("SendFewerMessagesToSlowerServer", true);
				smtpSendConfig.cacheOnlyUrlRetrievalForRemoteCertChain = TransportAppConfig.GetConfigBool("SmtpSendCacheOnlyUrlRetrievalForRemoteCertChain", true);
				smtpSendConfig.treatTransientErrorsAsPermanentErrors = TransportAppConfig.GetConfigBool("SmtpSendTreatTransientErrorsAsPermanentErrors", true);
				return smtpSendConfig;
			}

			// Token: 0x04000F86 RID: 3974
			private List<SmtpResponse> downgradedResponses = new List<SmtpResponse>();

			// Token: 0x04000F87 RID: 3975
			private List<SmtpResponse> upgradedResponses = new List<SmtpResponse>();

			// Token: 0x04000F88 RID: 3976
			private bool retryMessageOnRcptTransientError;

			// Token: 0x04000F89 RID: 3977
			private TimeSpan suspiciousDisconnectRetryInterval;

			// Token: 0x04000F8A RID: 3978
			private int poisonForRemoteThreshold;

			// Token: 0x04000F8B RID: 3979
			private bool oneLevelWildcardMatchForCertSelection;

			// Token: 0x04000F8C RID: 3980
			private bool sendFewerMessagesToSlowerServerEnabled;

			// Token: 0x04000F8D RID: 3981
			private bool cacheOnlyUrlRetrievalForRemoteCertChain;

			// Token: 0x04000F8E RID: 3982
			private bool treatTransientErrorsAsPermanentErrors;
		}

		// Token: 0x020002BB RID: 699
		public sealed class SmtpProxyConfig
		{
			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00077EBD File Offset: 0x000760BD
			public int MaxProxySetupAttempts
			{
				get
				{
					return this.maxProxySetupAttempts;
				}
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00077EC5 File Offset: 0x000760C5
			public bool ValidateProxyTargetCertificate
			{
				get
				{
					return this.validateProxyTargetCertificate;
				}
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00077ECD File Offset: 0x000760CD
			public bool RequireXProxyExtension
			{
				get
				{
					return this.requireXProxyExtension;
				}
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00077ED5 File Offset: 0x000760D5
			public SmtpDomainWithSubdomains ProxyCertificateFqdn
			{
				get
				{
					return this.proxyCertificateFqdn;
				}
			}

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00077EDD File Offset: 0x000760DD
			public int ProxyPort
			{
				get
				{
					return this.proxyPort;
				}
			}

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00077EE5 File Offset: 0x000760E5
			public bool SimulateUserNotInAdAuthError
			{
				get
				{
					return this.simulateUserNotInAdAuthError;
				}
			}

			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00077EED File Offset: 0x000760ED
			public string PodRedirectTemplate
			{
				get
				{
					return this.podRedirectTemplate;
				}
			}

			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00077EF5 File Offset: 0x000760F5
			public int PodSiteStartRange
			{
				get
				{
					return this.podSiteStartRange;
				}
			}

			// Token: 0x17000914 RID: 2324
			// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00077EFD File Offset: 0x000760FD
			public int PodSiteEndRange
			{
				get
				{
					return this.podSiteEndRange;
				}
			}

			// Token: 0x17000915 RID: 2325
			// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00077F05 File Offset: 0x00076105
			public bool ReplayAuthLogin
			{
				get
				{
					return this.replayAuthLogin;
				}
			}

			// Token: 0x17000916 RID: 2326
			// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00077F0D File Offset: 0x0007610D
			public bool PreferMailboxMountedServer
			{
				get
				{
					return this.preferMailboxMountedServer;
				}
			}

			// Token: 0x06001E83 RID: 7811 RVA: 0x00077F18 File Offset: 0x00076118
			public static TransportAppConfig.SmtpProxyConfig Load()
			{
				return new TransportAppConfig.SmtpProxyConfig
				{
					maxProxySetupAttempts = TransportAppConfig.GetConfigInt("MaxProxySetupAttempts", 1, 100, 6),
					proxyPort = TransportAppConfig.GetConfigInt("ProxyPort", 0, 65535, 465),
					simulateUserNotInAdAuthError = TransportAppConfig.GetConfigBool("SimulateUserNotInAdAuthError", false),
					validateProxyTargetCertificate = TransportAppConfig.GetConfigBool("ValidateProxyTargetCertificate", false),
					requireXProxyExtension = TransportAppConfig.GetConfigBool("RequireXProxyExtension", true),
					proxyCertificateFqdn = TransportAppConfig.GetConfigValue<SmtpDomainWithSubdomains>("ProxyCertificateFqdn", new SmtpDomainWithSubdomains("smtp.outlook.com"), new TransportAppConfig.TryParse<SmtpDomainWithSubdomains>(SmtpDomainWithSubdomains.TryParse)),
					podRedirectTemplate = TransportAppConfig.GetConfigString("PodRedirectTemplate", "pod{0}.proxy.outlook.com"),
					podSiteStartRange = TransportAppConfig.GetConfigInt("PodSiteStartRange", 0, int.MaxValue, 50000),
					podSiteEndRange = TransportAppConfig.GetConfigInt("PodSiteEndRange", 0, int.MaxValue, 59999),
					replayAuthLogin = TransportAppConfig.GetConfigBool("ClientProxyReplayAuthLogin", false),
					preferMailboxMountedServer = TransportAppConfig.GetConfigBool("ClientProxyPreferMailboxMountedServer", true)
				};
			}

			// Token: 0x04000F8F RID: 3983
			private int maxProxySetupAttempts;

			// Token: 0x04000F90 RID: 3984
			private bool validateProxyTargetCertificate;

			// Token: 0x04000F91 RID: 3985
			private bool requireXProxyExtension;

			// Token: 0x04000F92 RID: 3986
			private SmtpDomainWithSubdomains proxyCertificateFqdn;

			// Token: 0x04000F93 RID: 3987
			private int proxyPort;

			// Token: 0x04000F94 RID: 3988
			private bool simulateUserNotInAdAuthError;

			// Token: 0x04000F95 RID: 3989
			private string podRedirectTemplate;

			// Token: 0x04000F96 RID: 3990
			private int podSiteStartRange;

			// Token: 0x04000F97 RID: 3991
			private int podSiteEndRange;

			// Token: 0x04000F98 RID: 3992
			private bool replayAuthLogin;

			// Token: 0x04000F99 RID: 3993
			private bool preferMailboxMountedServer;
		}

		// Token: 0x020002BC RID: 700
		public interface ISmtpInboundProxyConfig
		{
			// Token: 0x17000917 RID: 2327
			// (get) Token: 0x06001E85 RID: 7813
			bool InboundProxyDestinationTrackingEnabled { get; }

			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x06001E86 RID: 7814
			bool InboundProxyAccountForestTrackingEnabled { get; }

			// Token: 0x17000919 RID: 2329
			// (get) Token: 0x06001E87 RID: 7815
			bool RejectBasedOnInboundProxyDestinationTrackingEnabled { get; }

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x06001E88 RID: 7816
			bool RejectBasedOnInboundProxyAccountForestTrackingEnabled { get; }

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x06001E89 RID: 7817
			bool TrackInboundProxyDestinationsInRcpt { get; }

			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x06001E8A RID: 7818
			int PerDestinationConnectionPercentageThreshold { get; }

			// Token: 0x1700091D RID: 2333
			// (get) Token: 0x06001E8B RID: 7819
			int PerAccountForestConnectionPercentageThreshold { get; }

			// Token: 0x1700091E RID: 2334
			// (get) Token: 0x06001E8C RID: 7820
			TimeSpan InboundProxyDestinationTrackerLogInterval { get; }

			// Token: 0x06001E8D RID: 7821
			bool TryGetDestinationConnectionThreshold(string destination, out int threshold);

			// Token: 0x06001E8E RID: 7822
			bool TryGetAccountForestConnectionThreshold(string destination, out int threshold);
		}

		// Token: 0x020002BD RID: 701
		public sealed class SmtpInboundProxyConfig : TransportAppConfig.ISmtpInboundProxyConfig
		{
			// Token: 0x1700091F RID: 2335
			// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00078029 File Offset: 0x00076229
			public bool RequireXProxyFromExtension
			{
				get
				{
					return this.requireXProxyFromExtension;
				}
			}

			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x06001E90 RID: 7824 RVA: 0x00078031 File Offset: 0x00076231
			public bool RequireTls
			{
				get
				{
					return this.requireTls;
				}
			}

			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00078039 File Offset: 0x00076239
			public bool UseExternalDnsServers
			{
				get
				{
					return this.useExternalDnsServers;
				}
			}

			// Token: 0x17000922 RID: 2338
			// (get) Token: 0x06001E92 RID: 7826 RVA: 0x00078041 File Offset: 0x00076241
			public bool IsInternalDestination
			{
				get
				{
					return this.isInternalDestination;
				}
			}

			// Token: 0x17000923 RID: 2339
			// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00078049 File Offset: 0x00076249
			public List<IPAddress> ProxyDestinations
			{
				get
				{
					return this.proxyDestinations;
				}
			}

			// Token: 0x17000924 RID: 2340
			// (get) Token: 0x06001E94 RID: 7828 RVA: 0x00078051 File Offset: 0x00076251
			public bool TreatProxyDestinationAsExternal
			{
				get
				{
					return this.treatProxyDestinationAsExternal;
				}
			}

			// Token: 0x17000925 RID: 2341
			// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00078059 File Offset: 0x00076259
			public SmtpDomainWithSubdomains TlsDomain
			{
				get
				{
					return this.tlsDomain;
				}
			}

			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x06001E96 RID: 7830 RVA: 0x00078061 File Offset: 0x00076261
			public TlsAuthLevel TlsAuthLevel
			{
				get
				{
					return this.tlsAuthLevel;
				}
			}

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x06001E97 RID: 7831 RVA: 0x00078069 File Offset: 0x00076269
			public string ExternalCertificateSubject
			{
				get
				{
					return this.externalCertificateSubject;
				}
			}

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00078071 File Offset: 0x00076271
			public ByteQuantifiedSize AccumulatedMessageSizeThreshold
			{
				get
				{
					return this.accumulatedMessageSizeThreshold;
				}
			}

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00078079 File Offset: 0x00076279
			public bool PreserveTargetResponse
			{
				get
				{
					return this.preserveTargetResponse;
				}
			}

			// Token: 0x1700092A RID: 2346
			// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00078081 File Offset: 0x00076281
			public bool SendNewXProxyFromArguments
			{
				get
				{
					return this.sendNewXProxyFromArguments;
				}
			}

			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00078089 File Offset: 0x00076289
			public int PerHostConnectionAttempts
			{
				get
				{
					return this.perHostConnectionAttempts;
				}
			}

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x06001E9C RID: 7836 RVA: 0x00078091 File Offset: 0x00076291
			public int PerDestinationConnectionPercentageThreshold
			{
				get
				{
					return this.perDestinationConnectionPercentageThreshold;
				}
			}

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00078099 File Offset: 0x00076299
			public int PerAccountForestConnectionPercentageThreshold
			{
				get
				{
					return this.perAccountForestConnectionPercentageThreshold;
				}
			}

			// Token: 0x1700092E RID: 2350
			// (get) Token: 0x06001E9E RID: 7838 RVA: 0x000780A1 File Offset: 0x000762A1
			public bool InboundProxyDestinationTrackingEnabled
			{
				get
				{
					return this.inboundProxyDestinationTrackingEnabled;
				}
			}

			// Token: 0x1700092F RID: 2351
			// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000780A9 File Offset: 0x000762A9
			public bool InboundProxyAccountForestTrackingEnabled
			{
				get
				{
					return this.inboundProxyAccountForestTrackingEnabled;
				}
			}

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x000780B1 File Offset: 0x000762B1
			public bool RejectBasedOnInboundProxyDestinationTrackingEnabled
			{
				get
				{
					return this.rejectBasedOnInboundProxyDestinationTrackingEnabled;
				}
			}

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000780B9 File Offset: 0x000762B9
			public bool RejectBasedOnInboundProxyAccountForestTrackingEnabled
			{
				get
				{
					return this.rejectBasedOnInboundProxyAccountForestTrackingEnabled;
				}
			}

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x000780C1 File Offset: 0x000762C1
			public bool TrackInboundProxyDestinationsInRcpt
			{
				get
				{
					return this.trackInboundProxyDestinationsInRcpt;
				}
			}

			// Token: 0x17000933 RID: 2355
			// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000780C9 File Offset: 0x000762C9
			public TimeSpan InboundProxyDestinationTrackerLogInterval
			{
				get
				{
					return this.inboundProxyDestinationTrackerLogInterval;
				}
			}

			// Token: 0x06001EA4 RID: 7844 RVA: 0x000780D1 File Offset: 0x000762D1
			private bool TryGetDestinationConnectionThreshold(Dictionary<string, int> dictionary, string destination, out int threshold)
			{
				if (!dictionary.TryGetValue(destination, out threshold))
				{
					threshold = 0;
					return false;
				}
				return true;
			}

			// Token: 0x06001EA5 RID: 7845 RVA: 0x000780E3 File Offset: 0x000762E3
			public bool TryGetDestinationConnectionThreshold(string destination, out int threshold)
			{
				return this.TryGetDestinationConnectionThreshold(this.perDestinationConnectionThresholdDictionary, destination, out threshold);
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x000780F3 File Offset: 0x000762F3
			public bool TryGetAccountForestConnectionThreshold(string destination, out int threshold)
			{
				return this.TryGetDestinationConnectionThreshold(this.perAccountForestConnectionThresholdDictionary, destination, out threshold);
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x00078104 File Offset: 0x00076304
			public static TransportAppConfig.SmtpInboundProxyConfig Load()
			{
				TransportAppConfig.SmtpInboundProxyConfig smtpInboundProxyConfig = new TransportAppConfig.SmtpInboundProxyConfig();
				smtpInboundProxyConfig.requireXProxyFromExtension = TransportAppConfig.GetConfigBool("InboundProxyRequireXProxyFromExtension", true);
				smtpInboundProxyConfig.requireTls = TransportAppConfig.GetConfigBool("InboundProxyRequireTls", true);
				smtpInboundProxyConfig.useExternalDnsServers = TransportAppConfig.GetConfigBool("InboundProxyUseExternalDnsServers", false);
				smtpInboundProxyConfig.isInternalDestination = TransportAppConfig.GetConfigBool("InboundProxyIsInternalDestination", false);
				smtpInboundProxyConfig.proxyDestinations = TransportAppConfig.GetConfigList<IPAddress>("InboundProxyDestinations", ',', new TransportAppConfig.TryParse<IPAddress>(IPAddress.TryParse));
				smtpInboundProxyConfig.treatProxyDestinationAsExternal = TransportAppConfig.GetConfigBool("InboundProxyTreatProxyDestinationAsExternal", false);
				smtpInboundProxyConfig.tlsAuthLevel = TransportAppConfig.GetConfigEnum<TlsAuthLevel>("InboundProxyTlsAuthLevel", TransportAppConfig.SmtpInboundProxyConfig.DefaultTlsAuthLevel);
				smtpInboundProxyConfig.tlsDomain = TransportAppConfig.GetConfigValue<SmtpDomainWithSubdomains>("InboundProxyTlsDomain", (smtpInboundProxyConfig.tlsAuthLevel == TlsAuthLevel.DomainValidation) ? new SmtpDomainWithSubdomains("mail.messaging.microsoft.com") : null, new TransportAppConfig.TryParse<SmtpDomainWithSubdomains>(SmtpDomainWithSubdomains.TryParse));
				smtpInboundProxyConfig.externalCertificateSubject = TransportAppConfig.GetConfigString("InboundProxyExternalCertificateSubject", null);
				smtpInboundProxyConfig.accumulatedMessageSizeThreshold = TransportAppConfig.GetConfigByteQuantifiedSize("InboundProxyAccumulatedMessageSizeThreshold", ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL), TransportAppConfig.SmtpInboundProxyConfig.DefaultAccumulatedMessageSizeThreshold);
				smtpInboundProxyConfig.preserveTargetResponse = TransportAppConfig.GetConfigBool("InboundProxyPreserveTargetResponse", false);
				smtpInboundProxyConfig.sendNewXProxyFromArguments = TransportAppConfig.GetConfigBool("InboundProxySendNewXProxyFromArguments", true);
				smtpInboundProxyConfig.perHostConnectionAttempts = TransportAppConfig.GetConfigInt("InboundProxyPerHostConnectionAttempts", 1, 10, 1);
				smtpInboundProxyConfig.inboundProxyDestinationTrackingEnabled = TransportAppConfig.GetConfigBool("InboundProxyDestinationTrackerEnabled", false);
				smtpInboundProxyConfig.inboundProxyAccountForestTrackingEnabled = TransportAppConfig.GetConfigBool("InboundProxyAccountForestTrackerEnabled", false);
				smtpInboundProxyConfig.rejectBasedOnInboundProxyDestinationTrackingEnabled = TransportAppConfig.GetConfigBool("RejectBasedOnInboundProxyDestinationTrackerEnabled", false);
				smtpInboundProxyConfig.rejectBasedOnInboundProxyAccountForestTrackingEnabled = TransportAppConfig.GetConfigBool("RejectBasedOnInboundProxyAccountForestTrackerEnabled", false);
				smtpInboundProxyConfig.trackInboundProxyDestinationsInRcpt = TransportAppConfig.GetConfigBool("TrackInboundProxyDestinationsInRcpt", false);
				smtpInboundProxyConfig.perDestinationConnectionPercentageThreshold = TransportAppConfig.GetConfigInt("InboundProxyPerDestinationConnectionPercentage", 0, 100, 20);
				smtpInboundProxyConfig.perAccountForestConnectionPercentageThreshold = TransportAppConfig.GetConfigInt("InboundProxyPerAccountForestConnectionPercentage", 0, 100, 10);
				smtpInboundProxyConfig.inboundProxyDestinationTrackerLogInterval = TransportAppConfig.GetConfigTimeSpan("InboundProxyDestinationTrackerLogInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0));
				smtpInboundProxyConfig.perDestinationConnectionThresholdDictionary = TransportAppConfig.SmtpInboundProxyConfig.GetPerDestinationConnectionThresholdDictionary(TransportAppConfig.GetConfigString("InboundProxyPerDestinationConnectionThresholds", null));
				smtpInboundProxyConfig.perAccountForestConnectionThresholdDictionary = TransportAppConfig.SmtpInboundProxyConfig.GetPerDestinationConnectionThresholdDictionary(TransportAppConfig.GetConfigString("InboundProxyPerAccountForestConnectionThresholds", null));
				return smtpInboundProxyConfig;
			}

			// Token: 0x06001EA8 RID: 7848 RVA: 0x00078320 File Offset: 0x00076520
			private static Dictionary<string, int> GetPerDestinationConnectionThresholdDictionary(string configString)
			{
				if (string.IsNullOrEmpty(configString))
				{
					return new Dictionary<string, int>();
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				string[] array = configString.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'='
					}, StringSplitOptions.RemoveEmptyEntries);
					int value;
					if (array3.Length == 2 && int.TryParse(array3[1], out value))
					{
						dictionary.Add(array3[0], value);
					}
				}
				return dictionary;
			}

			// Token: 0x04000F9A RID: 3994
			private static readonly ByteQuantifiedSize DefaultAccumulatedMessageSizeThreshold = ByteQuantifiedSize.FromMB(1UL);

			// Token: 0x04000F9B RID: 3995
			private static readonly TlsAuthLevel DefaultTlsAuthLevel = TlsAuthLevel.EncryptionOnly;

			// Token: 0x04000F9C RID: 3996
			private bool requireXProxyFromExtension;

			// Token: 0x04000F9D RID: 3997
			private bool requireTls;

			// Token: 0x04000F9E RID: 3998
			private bool useExternalDnsServers;

			// Token: 0x04000F9F RID: 3999
			private bool isInternalDestination;

			// Token: 0x04000FA0 RID: 4000
			private List<IPAddress> proxyDestinations;

			// Token: 0x04000FA1 RID: 4001
			private bool treatProxyDestinationAsExternal;

			// Token: 0x04000FA2 RID: 4002
			private SmtpDomainWithSubdomains tlsDomain;

			// Token: 0x04000FA3 RID: 4003
			private TlsAuthLevel tlsAuthLevel;

			// Token: 0x04000FA4 RID: 4004
			private string externalCertificateSubject;

			// Token: 0x04000FA5 RID: 4005
			private ByteQuantifiedSize accumulatedMessageSizeThreshold;

			// Token: 0x04000FA6 RID: 4006
			private bool preserveTargetResponse;

			// Token: 0x04000FA7 RID: 4007
			private bool sendNewXProxyFromArguments;

			// Token: 0x04000FA8 RID: 4008
			private int perHostConnectionAttempts;

			// Token: 0x04000FA9 RID: 4009
			private bool inboundProxyDestinationTrackingEnabled;

			// Token: 0x04000FAA RID: 4010
			private bool inboundProxyAccountForestTrackingEnabled;

			// Token: 0x04000FAB RID: 4011
			private bool rejectBasedOnInboundProxyDestinationTrackingEnabled;

			// Token: 0x04000FAC RID: 4012
			private bool trackInboundProxyDestinationsInRcpt;

			// Token: 0x04000FAD RID: 4013
			private int perDestinationConnectionPercentageThreshold;

			// Token: 0x04000FAE RID: 4014
			private TimeSpan inboundProxyDestinationTrackerLogInterval;

			// Token: 0x04000FAF RID: 4015
			private Dictionary<string, int> perDestinationConnectionThresholdDictionary;

			// Token: 0x04000FB0 RID: 4016
			private Dictionary<string, int> perAccountForestConnectionThresholdDictionary;

			// Token: 0x04000FB1 RID: 4017
			private bool rejectBasedOnInboundProxyAccountForestTrackingEnabled;

			// Token: 0x04000FB2 RID: 4018
			private int perAccountForestConnectionPercentageThreshold;
		}

		// Token: 0x020002BE RID: 702
		public sealed class SmtpOutboundProxyConfig
		{
			// Token: 0x17000934 RID: 2356
			// (get) Token: 0x06001EAB RID: 7851 RVA: 0x000783C7 File Offset: 0x000765C7
			public bool RequireTls
			{
				get
				{
					return this.requireTls;
				}
			}

			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x06001EAC RID: 7852 RVA: 0x000783CF File Offset: 0x000765CF
			public bool UseExternalDnsServers
			{
				get
				{
					return this.useExternalDnsServers;
				}
			}

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000783D7 File Offset: 0x000765D7
			public bool TreatProxyHopAsExternal
			{
				get
				{
					return this.treatProxyHopAsExternal;
				}
			}

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000783DF File Offset: 0x000765DF
			public SmtpDomainWithSubdomains TlsDomain
			{
				get
				{
					return this.tlsDomain;
				}
			}

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000783E7 File Offset: 0x000765E7
			public TlsAuthLevel TlsAuthLevel
			{
				get
				{
					return this.tlsAuthLevel;
				}
			}

			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x000783EF File Offset: 0x000765EF
			public string ExternalCertificateSubject
			{
				get
				{
					return this.externalCertificateSubject;
				}
			}

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000783F7 File Offset: 0x000765F7
			public int BulkRiskPoolPort
			{
				get
				{
					return this.bulkRiskPoolPort;
				}
			}

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x000783FF File Offset: 0x000765FF
			public int HighRiskPoolPort
			{
				get
				{
					return this.highRiskPoolPort;
				}
			}

			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x00078407 File Offset: 0x00076607
			public int LowRiskPoolPort
			{
				get
				{
					return this.lowRiskPoolPort;
				}
			}

			// Token: 0x1700093D RID: 2365
			// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0007840F File Offset: 0x0007660F
			public Fqdn SendConnectorFqdn
			{
				get
				{
					return this.sendConnectorFqdn;
				}
			}

			// Token: 0x1700093E RID: 2366
			// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00078417 File Offset: 0x00076617
			public string ResourceForestMatchingDomains
			{
				get
				{
					return this.resourceForestMatchingDomains;
				}
			}

			// Token: 0x06001EB6 RID: 7862 RVA: 0x00078420 File Offset: 0x00076620
			public static TransportAppConfig.SmtpOutboundProxyConfig Load()
			{
				TransportAppConfig.SmtpOutboundProxyConfig smtpOutboundProxyConfig = new TransportAppConfig.SmtpOutboundProxyConfig();
				smtpOutboundProxyConfig.requireTls = TransportAppConfig.GetConfigBool("OutboundProxyRequireTls", true);
				smtpOutboundProxyConfig.useExternalDnsServers = TransportAppConfig.GetConfigBool("OutboundProxyUseExternalDnsServers", false);
				smtpOutboundProxyConfig.treatProxyHopAsExternal = TransportAppConfig.GetConfigBool("OutboundProxyTreatProxyHopAsExternal", false);
				smtpOutboundProxyConfig.tlsAuthLevel = TransportAppConfig.GetConfigEnum<TlsAuthLevel>("OutboundProxyTlsAuthLevel", TransportAppConfig.SmtpOutboundProxyConfig.DefaultTlsAuthLevel);
				smtpOutboundProxyConfig.tlsDomain = TransportAppConfig.GetConfigValue<SmtpDomainWithSubdomains>("OutboundProxyTlsDomain", (smtpOutboundProxyConfig.tlsAuthLevel == TlsAuthLevel.DomainValidation) ? new SmtpDomainWithSubdomains("mail.messaging.microsoft.com") : null, new TransportAppConfig.TryParse<SmtpDomainWithSubdomains>(SmtpDomainWithSubdomains.TryParse));
				smtpOutboundProxyConfig.externalCertificateSubject = TransportAppConfig.GetConfigString("OutboundProxyExternalCertificateSubject", null);
				smtpOutboundProxyConfig.bulkRiskPoolPort = TransportAppConfig.GetConfigInt("BulkRiskPoolPort", 0, 65535, 1028);
				smtpOutboundProxyConfig.highRiskPoolPort = TransportAppConfig.GetConfigInt("HighRiskPoolPort", 0, 65535, 1031);
				smtpOutboundProxyConfig.lowRiskPoolPort = TransportAppConfig.GetConfigInt("LowRiskPoolPort", 0, 65535, 1701);
				smtpOutboundProxyConfig.sendConnectorFqdn = TransportAppConfig.GetConfigValue<Fqdn>("OutboundProxySendConnectorFqdn", null, new TransportAppConfig.TryParse<Fqdn>(Fqdn.TryParse));
				smtpOutboundProxyConfig.resourceForestMatchingDomains = TransportAppConfig.GetConfigString("ResourceForestMatchingDomains", TransportAppConfig.SmtpOutboundProxyConfig.DefaultResourceForestMatchingDomains);
				return smtpOutboundProxyConfig;
			}

			// Token: 0x04000FB3 RID: 4019
			private static readonly TlsAuthLevel DefaultTlsAuthLevel = TlsAuthLevel.EncryptionOnly;

			// Token: 0x04000FB4 RID: 4020
			private static readonly string DefaultResourceForestMatchingDomains = "prod.outlook.com|prod.exchangelabs.com|protection.gbl|sdf.exchangelabs.com|ffo.gbl|prod.partner.outlook.cn|protectioncn.gbl|mgd.msft.net";

			// Token: 0x04000FB5 RID: 4021
			private bool requireTls;

			// Token: 0x04000FB6 RID: 4022
			private bool useExternalDnsServers;

			// Token: 0x04000FB7 RID: 4023
			private SmtpDomainWithSubdomains tlsDomain;

			// Token: 0x04000FB8 RID: 4024
			private TlsAuthLevel tlsAuthLevel;

			// Token: 0x04000FB9 RID: 4025
			private bool treatProxyHopAsExternal;

			// Token: 0x04000FBA RID: 4026
			private string externalCertificateSubject;

			// Token: 0x04000FBB RID: 4027
			private int bulkRiskPoolPort;

			// Token: 0x04000FBC RID: 4028
			private int highRiskPoolPort;

			// Token: 0x04000FBD RID: 4029
			private int lowRiskPoolPort;

			// Token: 0x04000FBE RID: 4030
			private Fqdn sendConnectorFqdn;

			// Token: 0x04000FBF RID: 4031
			private string resourceForestMatchingDomains;
		}

		// Token: 0x020002BF RID: 703
		public sealed class DeliveryQueuePrioritizationConfig
		{
			// Token: 0x1700093F RID: 2367
			// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x0007855C File Offset: 0x0007675C
			public ByteQuantifiedSize AccumulatedMessageSizeThreshold
			{
				get
				{
					return this.accumulatedMessageSizeThreshold;
				}
			}

			// Token: 0x17000940 RID: 2368
			// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00078564 File Offset: 0x00076764
			public ByteQuantifiedSize AnonymousAccumulatedMessageSizeThreshold
			{
				get
				{
					return this.anonymousAccumulatedMessageSizeThreshold;
				}
			}

			// Token: 0x17000941 RID: 2369
			// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0007856C File Offset: 0x0007676C
			public int AccumulatedRecipientCostLevel1Threshold
			{
				get
				{
					return this.accumulatedRecipientCostLevel1Threshold;
				}
			}

			// Token: 0x17000942 RID: 2370
			// (get) Token: 0x06001EBC RID: 7868 RVA: 0x00078574 File Offset: 0x00076774
			public int AccumulatedRecipientCostLevel2Threshold
			{
				get
				{
					return this.accumulatedRecipientCostLevel2Threshold;
				}
			}

			// Token: 0x17000943 RID: 2371
			// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0007857C File Offset: 0x0007677C
			public int AnonymousAccumulatedRecipientCostLevel1Threshold
			{
				get
				{
					return this.anonymousAccumulatedRecipientCostLevel1Threshold;
				}
			}

			// Token: 0x17000944 RID: 2372
			// (get) Token: 0x06001EBE RID: 7870 RVA: 0x00078584 File Offset: 0x00076784
			public int AnonymousAccumulatedRecipientCostLevel2Threshold
			{
				get
				{
					return this.anonymousAccumulatedRecipientCostLevel2Threshold;
				}
			}

			// Token: 0x17000945 RID: 2373
			// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0007858C File Offset: 0x0007678C
			public bool PrioritizationEnabled
			{
				get
				{
					return this.prioritizationEnabled;
				}
			}

			// Token: 0x17000946 RID: 2374
			// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00078594 File Offset: 0x00076794
			public bool PriorityHeaderPromotionEnabled
			{
				get
				{
					return this.priorityHeaderPromotionEnabled;
				}
			}

			// Token: 0x17000947 RID: 2375
			// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0007859C File Offset: 0x0007679C
			// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x000785A4 File Offset: 0x000767A4
			public long AccumulatedRecipientCountLevel1Threshold { get; private set; }

			// Token: 0x17000948 RID: 2376
			// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x000785AD File Offset: 0x000767AD
			// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x000785B5 File Offset: 0x000767B5
			public long AccumulatedRecipientCountLevel2Threshold { get; private set; }

			// Token: 0x06001EC5 RID: 7877 RVA: 0x000785C0 File Offset: 0x000767C0
			public static TransportAppConfig.DeliveryQueuePrioritizationConfig Load()
			{
				return new TransportAppConfig.DeliveryQueuePrioritizationConfig
				{
					accumulatedMessageSizeThreshold = TransportAppConfig.GetConfigByteQuantifiedSize("DeliveryQueuePrioritizationAccumulatedMessageSizeThreshold", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL), TransportAppConfig.DeliveryQueuePrioritizationConfig.DefaultAccumulatedMessageSizeThreshold),
					anonymousAccumulatedMessageSizeThreshold = TransportAppConfig.GetConfigByteQuantifiedSize("DeliveryQueuePrioritizationAnonymousAccumulatedMessageSizeThreshold", ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL), TransportAppConfig.DeliveryQueuePrioritizationConfig.DefaultAnonymousAccumulatedMessageSizeThreshold),
					accumulatedRecipientCostLevel1Threshold = TransportAppConfig.GetConfigInt("DeliveryQueuePrioritizationAccumulatedRecipientCostLevel1Threshold", 0, int.MaxValue, 500),
					accumulatedRecipientCostLevel2Threshold = TransportAppConfig.GetConfigInt("DeliveryQueuePrioritizationAccumulatedRecipientCostLevel2Threshold", 0, int.MaxValue, 2000),
					anonymousAccumulatedRecipientCostLevel1Threshold = TransportAppConfig.GetConfigInt("DeliveryQueuePrioritizationAnonymousAccumulatedRecipientCostLevel1Threshold", 0, int.MaxValue, 50),
					anonymousAccumulatedRecipientCostLevel2Threshold = TransportAppConfig.GetConfigInt("DeliveryQueuePrioritizationAnonymousAccumulatedRecipientCostLevel2Threshold", 0, int.MaxValue, 200),
					prioritizationEnabled = TransportAppConfig.GetConfigBool("DeliveryQueuePrioritizationEnabled", true),
					priorityHeaderPromotionEnabled = TransportAppConfig.GetConfigBool("PriorityHeaderPromotionEnabled", true),
					AccumulatedRecipientCountLevel1Threshold = TransportAppConfig.GetConfigValue<long>("DeliveryQueuePrioritizationAccumulatedRecipientCountLevel1Threshold", 1L, long.MaxValue, 10L, new TransportAppConfig.TryParse<long>(long.TryParse)),
					AccumulatedRecipientCountLevel2Threshold = TransportAppConfig.GetConfigValue<long>("DeliveryQueuePrioritizationAccumulatedRecipientCountLevel2Threshold", 1L, long.MaxValue, 40L, new TransportAppConfig.TryParse<long>(long.TryParse))
				};
			}

			// Token: 0x04000FC0 RID: 4032
			private const int DefaultAccumulatedRecipientCostLevel1Threshold = 500;

			// Token: 0x04000FC1 RID: 4033
			private const int DefaultAccumulatedRecipientCostLevel2Threshold = 2000;

			// Token: 0x04000FC2 RID: 4034
			private const int DefaultAnonymousAccumulatedRecipientCostLevel1Threshold = 50;

			// Token: 0x04000FC3 RID: 4035
			private const int DefaultAnonymousAccumulatedRecipientCostLevel2Threshold = 200;

			// Token: 0x04000FC4 RID: 4036
			private const bool DefaultPrioritizationEnabled = true;

			// Token: 0x04000FC5 RID: 4037
			private const long DefaultAccumulatedRecipientCountLevel1Threshold = 10L;

			// Token: 0x04000FC6 RID: 4038
			private const long DefaultAccumulatedRecipientCountLevel2Threshold = 40L;

			// Token: 0x04000FC7 RID: 4039
			private static readonly ByteQuantifiedSize DefaultAccumulatedMessageSizeThreshold = ByteQuantifiedSize.FromMB(1UL);

			// Token: 0x04000FC8 RID: 4040
			private static readonly ByteQuantifiedSize DefaultAnonymousAccumulatedMessageSizeThreshold = ByteQuantifiedSize.FromMB(1UL);

			// Token: 0x04000FC9 RID: 4041
			private ByteQuantifiedSize accumulatedMessageSizeThreshold;

			// Token: 0x04000FCA RID: 4042
			private ByteQuantifiedSize anonymousAccumulatedMessageSizeThreshold;

			// Token: 0x04000FCB RID: 4043
			private int accumulatedRecipientCostLevel1Threshold;

			// Token: 0x04000FCC RID: 4044
			private int accumulatedRecipientCostLevel2Threshold;

			// Token: 0x04000FCD RID: 4045
			private int anonymousAccumulatedRecipientCostLevel1Threshold;

			// Token: 0x04000FCE RID: 4046
			private int anonymousAccumulatedRecipientCostLevel2Threshold;

			// Token: 0x04000FCF RID: 4047
			private bool prioritizationEnabled;

			// Token: 0x04000FD0 RID: 4048
			private bool priorityHeaderPromotionEnabled;
		}

		// Token: 0x020002C0 RID: 704
		public interface ILegacyQueueConfig
		{
			// Token: 0x17000949 RID: 2377
			// (get) Token: 0x06001EC8 RID: 7880
			TimeSpan MinLargeQueueDeferEventInterval { get; }

			// Token: 0x1700094A RID: 2378
			// (get) Token: 0x06001EC9 RID: 7881
			TimeSpan MinQueueRetryOrSuspendDeferEventInterval { get; }

			// Token: 0x1700094B RID: 2379
			// (get) Token: 0x06001ECA RID: 7882
			TimeSpan MessageDeferEventCheckInterval { get; }

			// Token: 0x1700094C RID: 2380
			// (get) Token: 0x06001ECB RID: 7883
			TimeSpan QueueLoggingInterval { get; }

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x06001ECC RID: 7884
			int QueueLoggingThreshold { get; }

			// Token: 0x1700094E RID: 2382
			// (get) Token: 0x06001ECD RID: 7885
			bool QueueLoggingEnabled { get; }

			// Token: 0x1700094F RID: 2383
			// (get) Token: 0x06001ECE RID: 7886
			bool QueuedRecipientsByAgeTrackingEnabled { get; }

			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x06001ECF RID: 7887
			TimeSpan MaxUpdateQueueBlockedInterval { get; }

			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x06001ED0 RID: 7888
			TimeSpan RecentPerfCounterTrackingInterval { get; }

			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x06001ED1 RID: 7889
			TimeSpan RecentPerfCounterTrackingBucketSize { get; }

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06001ED2 RID: 7890
			bool AsynchronousRetryQueue { get; }
		}

		// Token: 0x020002C1 RID: 705
		public sealed class QueueConfig : TransportAppConfig.ILegacyQueueConfig
		{
			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00078729 File Offset: 0x00076929
			public TimeSpan MinLargeQueueDeferEventInterval
			{
				get
				{
					return this.minLargeQueueDeferEventInterval;
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x00078731 File Offset: 0x00076931
			public TimeSpan MinQueueRetryOrSuspendDeferEventInterval
			{
				get
				{
					return this.minQueueRetryOrSuspendDeferEventInterval;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x00078739 File Offset: 0x00076939
			public TimeSpan MessageDeferEventCheckInterval
			{
				get
				{
					return this.messageDeferEventCheckInterval;
				}
			}

			// Token: 0x17000957 RID: 2391
			// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x00078741 File Offset: 0x00076941
			public TimeSpan QueueLoggingInterval
			{
				get
				{
					return this.queueLoggingInterval;
				}
			}

			// Token: 0x17000958 RID: 2392
			// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00078749 File Offset: 0x00076949
			public int QueueLoggingThreshold
			{
				get
				{
					return this.queueLoggingThreshold;
				}
			}

			// Token: 0x17000959 RID: 2393
			// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00078751 File Offset: 0x00076951
			public bool QueueLoggingEnabled
			{
				get
				{
					return this.queueLoggingEnabled;
				}
			}

			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00078759 File Offset: 0x00076959
			public bool QueuedRecipientsByAgeTrackingEnabled
			{
				get
				{
					return this.queuedRecipientsByAgeTrackingEnabled;
				}
			}

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00078761 File Offset: 0x00076961
			public TimeSpan MaxUpdateQueueBlockedInterval
			{
				get
				{
					return this.maxQueueUpdateBlockedInterval;
				}
			}

			// Token: 0x1700095C RID: 2396
			// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00078769 File Offset: 0x00076969
			public TimeSpan RecentPerfCounterTrackingInterval
			{
				get
				{
					return this.recentPerfCounterTrackingInterval;
				}
			}

			// Token: 0x1700095D RID: 2397
			// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00078771 File Offset: 0x00076971
			public TimeSpan RecentPerfCounterTrackingBucketSize
			{
				get
				{
					return this.recentPerfCounterTrackingBucketSize;
				}
			}

			// Token: 0x1700095E RID: 2398
			// (get) Token: 0x06001EDD RID: 7901 RVA: 0x00078779 File Offset: 0x00076979
			public bool AsynchronousRetryQueue
			{
				get
				{
					return this.asynchronousRetryQueue;
				}
			}

			// Token: 0x1700095F RID: 2399
			// (get) Token: 0x06001EDE RID: 7902 RVA: 0x00078781 File Offset: 0x00076981
			public TimeSpan QueueResubmitRetryTimeout
			{
				get
				{
					return this.queueResubmitRetryTimeout;
				}
			}

			// Token: 0x17000960 RID: 2400
			// (get) Token: 0x06001EDF RID: 7903 RVA: 0x00078789 File Offset: 0x00076989
			public TimeSpan QueueResubmitRetryInterval
			{
				get
				{
					return this.queueResubmitRetryInterval;
				}
			}

			// Token: 0x17000961 RID: 2401
			// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x00078791 File Offset: 0x00076991
			public TimeSpan SynchronousRetryQueueTimeout
			{
				get
				{
					return this.synchronousRetryQueueTimeout;
				}
			}

			// Token: 0x06001EE1 RID: 7905 RVA: 0x0007879C File Offset: 0x0007699C
			public static TransportAppConfig.QueueConfig Load()
			{
				TransportAppConfig.QueueConfig queueConfig = new TransportAppConfig.QueueConfig();
				queueConfig.minLargeQueueDeferEventInterval = TransportAppConfig.GetConfigTimeSpan("MinLargeQueueDeferEventInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TimeSpan.Zero);
				queueConfig.minQueueRetryOrSuspendDeferEventInterval = TransportAppConfig.GetConfigTimeSpan("MinQueueRetryOrSuspendDeferEventInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TimeSpan.Zero);
				queueConfig.messageDeferEventCheckInterval = TransportAppConfig.GetConfigTimeSpan("MessageDeferEventCheckInterval", TimeSpan.FromSeconds(5.0), TimeSpan.FromHours(1.0), TimeSpan.FromSeconds(120.0));
				queueConfig.queueLoggingInterval = TransportAppConfig.GetConfigTimeSpan("QueueLoggingInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(1.0));
				queueConfig.queueLoggingThreshold = TransportAppConfig.GetConfigInt("QueueLoggingThreshold", 0, int.MaxValue, 10);
				queueConfig.maxQueueUpdateBlockedInterval = TransportAppConfig.GetConfigTimeSpan("MaxQueueUpdateBlockedInterval", TimeSpan.FromMinutes(5.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(15.0));
				queueConfig.queueLoggingEnabled = TransportAppConfig.GetConfigBool("QueueLogginEnabled", true);
				queueConfig.queuedRecipientsByAgeTrackingEnabled = TransportAppConfig.GetConfigBool("QueuedRecipientsByAgeTrackingEnabled", true);
				queueConfig.recentPerfCounterTrackingInterval = TransportAppConfig.GetConfigTimeSpan("RecentPerfCounterTrackingInterval", TimeSpan.Zero, TimeSpan.FromHours(6.0), TimeSpan.FromMinutes(5.0));
				queueConfig.recentPerfCounterTrackingBucketSize = TransportAppConfig.GetConfigTimeSpan("RecentPerfCounterTrackingBucketSize", TimeSpan.Zero, queueConfig.recentPerfCounterTrackingInterval, TimeSpan.FromSeconds(15.0));
				queueConfig.asynchronousRetryQueue = TransportAppConfig.GetConfigBool("AsynchronousRetryQueue", false);
				queueConfig.queueResubmitRetryTimeout = TransportAppConfig.GetConfigTimeSpan("QueueResubmitRetryTimeout", TimeSpan.Zero, TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(1.0));
				queueConfig.queueResubmitRetryInterval = TransportAppConfig.GetConfigTimeSpan("QueueResubmitRetryInterval", TimeSpan.FromMilliseconds(100.0), TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(5.0));
				queueConfig.synchronousRetryQueueTimeout = TransportAppConfig.GetConfigTimeSpan("SynchronousRetryQueueTimeout", TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(10.0));
				return queueConfig;
			}

			// Token: 0x04000FD3 RID: 4051
			private const string MinLargeQueueDeferEventIntervalString = "MinLargeQueueDeferEventInterval";

			// Token: 0x04000FD4 RID: 4052
			private const string MinQueueRetryOrSuspendDeferEventIntervalString = "MinQueueRetryOrSuspendDeferEventInterval";

			// Token: 0x04000FD5 RID: 4053
			private const string MessageDeferEventCheckIntervalString = "MessageDeferEventCheckInterval";

			// Token: 0x04000FD6 RID: 4054
			private const string QueueLoggingIntervalString = "QueueLoggingInterval";

			// Token: 0x04000FD7 RID: 4055
			private const string QueueLoggingThresholdString = "QueueLoggingThreshold";

			// Token: 0x04000FD8 RID: 4056
			private const string QueueLogginEnabledString = "QueueLogginEnabled";

			// Token: 0x04000FD9 RID: 4057
			private const string QueuedRecipientsByAgeTrackingEnabledString = "QueuedRecipientsByAgeTrackingEnabled";

			// Token: 0x04000FDA RID: 4058
			private const string MaxQueueUpdateBlockedIntervalString = "MaxQueueUpdateBlockedInterval";

			// Token: 0x04000FDB RID: 4059
			private TimeSpan minLargeQueueDeferEventInterval;

			// Token: 0x04000FDC RID: 4060
			private TimeSpan minQueueRetryOrSuspendDeferEventInterval;

			// Token: 0x04000FDD RID: 4061
			private TimeSpan messageDeferEventCheckInterval;

			// Token: 0x04000FDE RID: 4062
			private TimeSpan queueLoggingInterval;

			// Token: 0x04000FDF RID: 4063
			private bool queueLoggingEnabled;

			// Token: 0x04000FE0 RID: 4064
			private bool queuedRecipientsByAgeTrackingEnabled;

			// Token: 0x04000FE1 RID: 4065
			private int queueLoggingThreshold;

			// Token: 0x04000FE2 RID: 4066
			private TimeSpan maxQueueUpdateBlockedInterval;

			// Token: 0x04000FE3 RID: 4067
			private TimeSpan recentPerfCounterTrackingInterval;

			// Token: 0x04000FE4 RID: 4068
			private TimeSpan recentPerfCounterTrackingBucketSize;

			// Token: 0x04000FE5 RID: 4069
			private bool asynchronousRetryQueue;

			// Token: 0x04000FE6 RID: 4070
			private TimeSpan queueResubmitRetryTimeout;

			// Token: 0x04000FE7 RID: 4071
			private TimeSpan queueResubmitRetryInterval;

			// Token: 0x04000FE8 RID: 4072
			private TimeSpan synchronousRetryQueueTimeout;
		}

		// Token: 0x020002C2 RID: 706
		public sealed class DeliveryFailureConfig
		{
			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000789EE File Offset: 0x00076BEE
			public int DeliveryFailureMinSampleRouting5_4_4
			{
				get
				{
					return this.deliveryFailureMinSampleRouting544;
				}
			}

			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000789F6 File Offset: 0x00076BF6
			public int DeliveryFailureMinSampleResolver5_1_4
			{
				get
				{
					return this.deliveryFailureMinSampleResolver514;
				}
			}

			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000789FE File Offset: 0x00076BFE
			public int DeliveryFailureMinSampleResolver5_2_0
			{
				get
				{
					return this.deliveryFailureMinSampleResolver520;
				}
			}

			// Token: 0x17000965 RID: 2405
			// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00078A06 File Offset: 0x00076C06
			public int DeliveryFailureMinSampleResolver5_2_4
			{
				get
				{
					return this.deliveryFailureMinSampleResolver524;
				}
			}

			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x00078A0E File Offset: 0x00076C0E
			public int DeliveryFailureMinSampleResolver5_4_6
			{
				get
				{
					return this.deliveryFailureMinSampleResolver546;
				}
			}

			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00078A16 File Offset: 0x00076C16
			public int DeliveryFailureMinSampleDeliverySMTP5_6_0
			{
				get
				{
					return this.deliveryFailureMinSampleDeliverySMTP560;
				}
			}

			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x00078A1E File Offset: 0x00076C1E
			public int DeliveryFailureMinSampleStoreDriver5_2_0
			{
				get
				{
					return this.deliveryFailureMinSampleStoreDriver520;
				}
			}

			// Token: 0x17000969 RID: 2409
			// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00078A26 File Offset: 0x00076C26
			public int DeliveryFailureMinSampleStoreDriver5_6_0
			{
				get
				{
					return this.deliveryFailureMinSampleStoreDriver560;
				}
			}

			// Token: 0x1700096A RID: 2410
			// (get) Token: 0x06001EEB RID: 7915 RVA: 0x00078A2E File Offset: 0x00076C2E
			public int DeliveryFailureMinSampleDeliveryAgent
			{
				get
				{
					return this.deliveryFailureMinSampleDeliveryAgent;
				}
			}

			// Token: 0x1700096B RID: 2411
			// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00078A36 File Offset: 0x00076C36
			public int DeliveryFailureMinSampleForeignConnector
			{
				get
				{
					return this.deliveryFailureMinSampleForeignConnector;
				}
			}

			// Token: 0x1700096C RID: 2412
			// (get) Token: 0x06001EED RID: 7917 RVA: 0x00078A3E File Offset: 0x00076C3E
			public string DSNServerConnectorFqdn
			{
				get
				{
					return this.dsnServerConnectorFqdn;
				}
			}

			// Token: 0x06001EEE RID: 7918 RVA: 0x00078A48 File Offset: 0x00076C48
			public static TransportAppConfig.DeliveryFailureConfig Load()
			{
				TransportAppConfig.DeliveryFailureConfig deliveryFailureConfig = new TransportAppConfig.DeliveryFailureConfig();
				deliveryFailureConfig.deliveryFailureMinSampleRouting544 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleRouting5_4_4", 0, int.MaxValue, 200);
				deliveryFailureConfig.deliveryFailureMinSampleResolver514 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleResolver5_1_4", 0, int.MaxValue, 0);
				deliveryFailureConfig.deliveryFailureMinSampleResolver520 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleResolver5_2_0", 0, int.MaxValue, 1000);
				deliveryFailureConfig.deliveryFailureMinSampleResolver524 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleResolver5_2_4", 0, int.MaxValue, 200);
				deliveryFailureConfig.deliveryFailureMinSampleResolver546 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleResolver5_4_6", 0, int.MaxValue, 0);
				deliveryFailureConfig.deliveryFailureMinSampleDeliverySMTP560 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleDeliverySMTP5_6_0", 0, int.MaxValue, 200);
				deliveryFailureConfig.deliveryFailureMinSampleStoreDriver520 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleStoreDriver5_2_0", 0, int.MaxValue, 500);
				deliveryFailureConfig.deliveryFailureMinSampleStoreDriver560 = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleStoreDriver5_6_0", 0, int.MaxValue, 100);
				deliveryFailureConfig.deliveryFailureMinSampleDeliveryAgent = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleDeliveryAgent", 0, int.MaxValue, 1000);
				deliveryFailureConfig.deliveryFailureMinSampleForeignConnector = TransportAppConfig.GetConfigInt("DeliveryFailureMinSampleForeignConnector", 0, int.MaxValue, 1000);
				deliveryFailureConfig.dsnServerConnectorFqdn = TransportAppConfig.GetConfigString("DSNServerConnectorFqdn", null);
				if (!string.IsNullOrEmpty(deliveryFailureConfig.dsnServerConnectorFqdn) && !DatacenterRegistry.IsForefrontForOffice())
				{
					throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "Invalid Configuration: The DSNServerConnectorFqdn AppConfig parameter '{0}' is only valid on a FFO Hub role.", new object[]
					{
						deliveryFailureConfig.dsnServerConnectorFqdn
					}));
				}
				return deliveryFailureConfig;
			}

			// Token: 0x04000FE9 RID: 4073
			private const int DefaultDeliveryFailureMinSampleRouting544 = 200;

			// Token: 0x04000FEA RID: 4074
			private const int DefaultDeliveryFailureMinSampleResolver514 = 0;

			// Token: 0x04000FEB RID: 4075
			private const int DefaultDeliveryFailureMinSampleResolver520 = 1000;

			// Token: 0x04000FEC RID: 4076
			private const int DefaultDeliveryFailureMinSampleResolver524 = 200;

			// Token: 0x04000FED RID: 4077
			private const int DefaultDeliveryFailureMinSampleResolver546 = 0;

			// Token: 0x04000FEE RID: 4078
			private const int DefaultDeliveryFailureMinSampleDeliverySMTP560 = 200;

			// Token: 0x04000FEF RID: 4079
			private const int DefaultDeliveryFailureMinSampleStoreDriver520 = 500;

			// Token: 0x04000FF0 RID: 4080
			private const int DefaultDeliveryFailureMinSampleStoreDriver560 = 100;

			// Token: 0x04000FF1 RID: 4081
			private const int DefaultDeliveryFailureMinSampleDeliveryAgent = 1000;

			// Token: 0x04000FF2 RID: 4082
			private const int DefaultDeliveryFailureMinSampleForeignConnector = 1000;

			// Token: 0x04000FF3 RID: 4083
			private int deliveryFailureMinSampleRouting544;

			// Token: 0x04000FF4 RID: 4084
			private int deliveryFailureMinSampleResolver514;

			// Token: 0x04000FF5 RID: 4085
			private int deliveryFailureMinSampleResolver520;

			// Token: 0x04000FF6 RID: 4086
			private int deliveryFailureMinSampleResolver524;

			// Token: 0x04000FF7 RID: 4087
			private int deliveryFailureMinSampleResolver546;

			// Token: 0x04000FF8 RID: 4088
			private int deliveryFailureMinSampleDeliverySMTP560;

			// Token: 0x04000FF9 RID: 4089
			private int deliveryFailureMinSampleStoreDriver520;

			// Token: 0x04000FFA RID: 4090
			private int deliveryFailureMinSampleStoreDriver560;

			// Token: 0x04000FFB RID: 4091
			private int deliveryFailureMinSampleDeliveryAgent;

			// Token: 0x04000FFC RID: 4092
			private int deliveryFailureMinSampleForeignConnector;

			// Token: 0x04000FFD RID: 4093
			private string dsnServerConnectorFqdn;
		}

		// Token: 0x020002C3 RID: 707
		public sealed class SecureMailConfig
		{
			// Token: 0x1700096D RID: 2413
			// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00078BB2 File Offset: 0x00076DB2
			public bool ClientCertificateChainValidationEnabled
			{
				get
				{
					return this.clientCertificateChainValidationEnabled;
				}
			}

			// Token: 0x1700096E RID: 2414
			// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00078BBA File Offset: 0x00076DBA
			public bool TreatCRLTransientFailuresAsSuccessEnabled
			{
				get
				{
					return this.treatCRLTransientFailuresAsSuccessEnabled;
				}
			}

			// Token: 0x1700096F RID: 2415
			// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00078BC2 File Offset: 0x00076DC2
			public ByteQuantifiedSize CertificateValidationCacheMaxSize
			{
				get
				{
					return this.certificateValidationCacheMaxSize;
				}
			}

			// Token: 0x17000970 RID: 2416
			// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00078BCA File Offset: 0x00076DCA
			public TimeSpan CertificateValidationCacheExpiryInterval
			{
				get
				{
					return this.certificateValidationCacheExpiryInterval;
				}
			}

			// Token: 0x17000971 RID: 2417
			// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00078BD2 File Offset: 0x00076DD2
			public TimeSpan CertificateValidationCacheTransientFailureExpiryInterval
			{
				get
				{
					return this.certificateValidationCacheTransientFailureExpiryInterval;
				}
			}

			// Token: 0x17000972 RID: 2418
			// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00078BDA File Offset: 0x00076DDA
			public int SubjectAlternativeNameLimit
			{
				get
				{
					return this.subjectAlternativeNameLimit;
				}
			}

			// Token: 0x06001EF6 RID: 7926 RVA: 0x00078BE4 File Offset: 0x00076DE4
			public static TransportAppConfig.SecureMailConfig Load()
			{
				return new TransportAppConfig.SecureMailConfig
				{
					clientCertificateChainValidationEnabled = TransportAppConfig.GetConfigBool("ClientCertificateChainValidationEnabled", true),
					treatCRLTransientFailuresAsSuccessEnabled = TransportAppConfig.GetConfigBool("TreatCRLTransientFailuresAsSuccessEnabled", true),
					certificateValidationCacheMaxSize = TransportAppConfig.GetConfigByteQuantifiedSize("CertificateValidationCacheMaxSize", ByteQuantifiedSize.Zero, ByteQuantifiedSize.FromMB(100UL), ByteQuantifiedSize.FromMB(1UL)),
					certificateValidationCacheExpiryInterval = TransportAppConfig.GetConfigTimeSpan("CertificateValidationCacheExpiryInterval", TimeSpan.Zero, TimeSpan.FromHours(24.0), TimeSpan.FromHours(1.0)),
					certificateValidationCacheTransientFailureExpiryInterval = TransportAppConfig.GetConfigTimeSpan("CertificateValidationCacheTransientFailureExpiryInterval", TimeSpan.Zero, TimeSpan.FromHours(24.0), TimeSpan.FromMinutes(5.0)),
					subjectAlternativeNameLimit = TransportAppConfig.GetConfigInt("SubjectAlternativeNameLimit", 0, int.MaxValue, 256)
				};
			}

			// Token: 0x04000FFE RID: 4094
			private const bool DefaultClientCertificateChainValidationEnabled = true;

			// Token: 0x04000FFF RID: 4095
			private const bool DefaultTreatCRLTransientFailuresAsSuccessEnabled = true;

			// Token: 0x04001000 RID: 4096
			private bool clientCertificateChainValidationEnabled;

			// Token: 0x04001001 RID: 4097
			private bool treatCRLTransientFailuresAsSuccessEnabled;

			// Token: 0x04001002 RID: 4098
			private ByteQuantifiedSize certificateValidationCacheMaxSize;

			// Token: 0x04001003 RID: 4099
			private TimeSpan certificateValidationCacheExpiryInterval;

			// Token: 0x04001004 RID: 4100
			private TimeSpan certificateValidationCacheTransientFailureExpiryInterval;

			// Token: 0x04001005 RID: 4101
			private int subjectAlternativeNameLimit;
		}

		// Token: 0x020002C4 RID: 708
		public sealed class LoggingConfig
		{
			// Token: 0x17000973 RID: 2419
			// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00078CC3 File Offset: 0x00076EC3
			public int SmtpSendLogBufferSize
			{
				get
				{
					return this.smtpSendLogBufferSize;
				}
			}

			// Token: 0x17000974 RID: 2420
			// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00078CCB File Offset: 0x00076ECB
			public TimeSpan SmtpSendLogFlushInterval
			{
				get
				{
					return this.smtpSendLogFlushInterval;
				}
			}

			// Token: 0x17000975 RID: 2421
			// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00078CD3 File Offset: 0x00076ED3
			public TimeSpan SmtpSendLogAsyncInterval
			{
				get
				{
					return this.smtpSendLogAsyncInterval;
				}
			}

			// Token: 0x17000976 RID: 2422
			// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00078CDB File Offset: 0x00076EDB
			public int SmtpRecvLogBufferSize
			{
				get
				{
					return this.smtpRecvLogBufferSize;
				}
			}

			// Token: 0x17000977 RID: 2423
			// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00078CE3 File Offset: 0x00076EE3
			public TimeSpan SmtpRecvLogFlushInterval
			{
				get
				{
					return this.smtpRecvLogFlushInterval;
				}
			}

			// Token: 0x17000978 RID: 2424
			// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00078CEB File Offset: 0x00076EEB
			public TimeSpan SmtpRecvLogAsyncInterval
			{
				get
				{
					return this.smtpRecvLogAsyncInterval;
				}
			}

			// Token: 0x17000979 RID: 2425
			// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00078CF3 File Offset: 0x00076EF3
			public int ConnectivityLogBufferSize
			{
				get
				{
					return this.connectivityLogBufferSize;
				}
			}

			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00078CFB File Offset: 0x00076EFB
			public TimeSpan ConnectivityLogFlushInterval
			{
				get
				{
					return this.connectivityLogFlushInterval;
				}
			}

			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00078D03 File Offset: 0x00076F03
			public TimeSpan ConnectivityLogAsyncInterval
			{
				get
				{
					return this.connectivityLogAsyncInterval;
				}
			}

			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00078D0B File Offset: 0x00076F0B
			public int MsgTrkLogBufferSize
			{
				get
				{
					return this.msgTrkLogBufferSize;
				}
			}

			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00078D13 File Offset: 0x00076F13
			public TimeSpan MsgTrkLogFlushInterval
			{
				get
				{
					return this.msgTrkLogFlushInterval;
				}
			}

			// Token: 0x1700097E RID: 2430
			// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00078D1B File Offset: 0x00076F1B
			public int MaxMsgTrkAgenInfoSize
			{
				get
				{
					return this.maxMsgTrkAgentInfoSize;
				}
			}

			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x06001F04 RID: 7940 RVA: 0x00078D23 File Offset: 0x00076F23
			public int TransportWlmLogBufferSize
			{
				get
				{
					return this.transportWlmLogBufferSize;
				}
			}

			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00078D2B File Offset: 0x00076F2B
			public TimeSpan TransportWlmLogFlushInterval
			{
				get
				{
					return this.transportWlmLogFlushInterval;
				}
			}

			// Token: 0x06001F06 RID: 7942 RVA: 0x00078D34 File Offset: 0x00076F34
			public static TransportAppConfig.LoggingConfig Load()
			{
				return new TransportAppConfig.LoggingConfig
				{
					smtpSendLogBufferSize = TransportAppConfig.GetConfigInt("SmtpSendLogBufferSize", 0, 10485760, 524288),
					smtpSendLogFlushInterval = TransportAppConfig.GetConfigTimeSpan("SmtpSendLogFlushInterval", TransportAppConfig.LoggingConfig.MinLogFlushInterval, TransportAppConfig.LoggingConfig.MaxLogFlushInterval, TransportAppConfig.LoggingConfig.DefaultSmtpSendLogFlushInterval),
					smtpSendLogAsyncInterval = TransportAppConfig.GetConfigTimeSpan("SmtpSendLogAsyncInterval", TransportAppConfig.LoggingConfig.MinLogAsyncInterval, TransportAppConfig.LoggingConfig.MaxLogAsyncInterval, TransportAppConfig.LoggingConfig.DefaultSmtpSendLogAsyncInterval),
					smtpRecvLogBufferSize = TransportAppConfig.GetConfigInt("SmtpRecvLogBufferSize", 0, 10485760, 524288),
					smtpRecvLogFlushInterval = TransportAppConfig.GetConfigTimeSpan("SmtpRecvLogFlushInterval", TransportAppConfig.LoggingConfig.MinLogFlushInterval, TransportAppConfig.LoggingConfig.MaxLogFlushInterval, TransportAppConfig.LoggingConfig.DefaultSmtpRecvLogFlushInterval),
					smtpRecvLogAsyncInterval = TransportAppConfig.GetConfigTimeSpan("SmtpRecvLogAsyncInterval", TransportAppConfig.LoggingConfig.MinLogAsyncInterval, TransportAppConfig.LoggingConfig.MaxLogAsyncInterval, TransportAppConfig.LoggingConfig.DefaultSmtpRecvLogAsyncInterval),
					connectivityLogBufferSize = TransportAppConfig.GetConfigInt("ConnectivityLogBufferSize", 0, 10485760, 524288),
					connectivityLogFlushInterval = TransportAppConfig.GetConfigTimeSpan("ConnectivityLogFlushInterval", TransportAppConfig.LoggingConfig.MinLogFlushInterval, TransportAppConfig.LoggingConfig.MaxLogFlushInterval, TransportAppConfig.LoggingConfig.DefaultConnectivityLogFlushInterval),
					connectivityLogAsyncInterval = TransportAppConfig.GetConfigTimeSpan("ConnectivityLogAsyncInterval", TransportAppConfig.LoggingConfig.MinLogAsyncInterval, TransportAppConfig.LoggingConfig.MaxLogAsyncInterval, TransportAppConfig.LoggingConfig.DefaultConnectivityLogAsyncInterval),
					msgTrkLogBufferSize = TransportAppConfig.GetConfigInt("MsgTrkLogBufferSize", 0, 10485760, 0),
					msgTrkLogFlushInterval = TransportAppConfig.GetConfigTimeSpan("MsgTrkLogFlushInterval", TransportAppConfig.LoggingConfig.MinLogFlushInterval, TransportAppConfig.LoggingConfig.MaxLogFlushInterval, TransportAppConfig.LoggingConfig.DefaultMsgTrkLogFlushInterval),
					maxMsgTrkAgentInfoSize = TransportAppConfig.GetConfigInt("MaxMsgTrkAgentInfoSize", 0, int.MaxValue, TransportAppConfig.LoggingConfig.DefaultMaxMsgTrkAgentInfoSize),
					transportWlmLogBufferSize = TransportAppConfig.GetConfigInt("TransportWlmLogBufferSize", 0, 10485760, 65536),
					transportWlmLogFlushInterval = TransportAppConfig.GetConfigTimeSpan("TransportWlmLogFlushInterval", TransportAppConfig.LoggingConfig.MinLogFlushInterval, TransportAppConfig.LoggingConfig.MaxLogFlushInterval, TransportAppConfig.LoggingConfig.DefaultTransportWlmLogFlushInterval)
				};
			}

			// Token: 0x04001006 RID: 4102
			private const string SmtpSendLogBufferSizeKey = "SmtpSendLogBufferSize";

			// Token: 0x04001007 RID: 4103
			private const string SmtpSendLogFlushIntervalKey = "SmtpSendLogFlushInterval";

			// Token: 0x04001008 RID: 4104
			private const string SmtpSendLogAsyncIntervalKey = "SmtpSendLogAsyncInterval";

			// Token: 0x04001009 RID: 4105
			private const string SmtpRecvLogBufferSizeKey = "SmtpRecvLogBufferSize";

			// Token: 0x0400100A RID: 4106
			private const string SmtpRecvLogFlushIntervalKey = "SmtpRecvLogFlushInterval";

			// Token: 0x0400100B RID: 4107
			private const string SmtpRecvLogAsyncIntervalKey = "SmtpRecvLogAsyncInterval";

			// Token: 0x0400100C RID: 4108
			private const string ConnectivityLogBufferSizeKey = "ConnectivityLogBufferSize";

			// Token: 0x0400100D RID: 4109
			private const string ConnectivityLogFlushIntervalKey = "ConnectivityLogFlushInterval";

			// Token: 0x0400100E RID: 4110
			private const string ConnectivityLogAsyncIntervalKey = "ConnectivityLogAsyncInterval";

			// Token: 0x0400100F RID: 4111
			private const string MsgTrkLogBufferSizeKey = "MsgTrkLogBufferSize";

			// Token: 0x04001010 RID: 4112
			private const string MsgTrkLogFlushIntervalKey = "MsgTrkLogFlushInterval";

			// Token: 0x04001011 RID: 4113
			private const string MaxMsgTrkAgentInfoSizeKey = "MaxMsgTrkAgentInfoSize";

			// Token: 0x04001012 RID: 4114
			private const string TransportWlmLogBufferSizeKey = "TransportWlmLogBufferSize";

			// Token: 0x04001013 RID: 4115
			private const string TransportWlmLogFlushIntervalKey = "TransportWlmLogFlushInterval";

			// Token: 0x04001014 RID: 4116
			private const int DefaultSmtpSendLogBufferSize = 524288;

			// Token: 0x04001015 RID: 4117
			private const int DefaultSmtpRecvLogBufferSize = 524288;

			// Token: 0x04001016 RID: 4118
			private const int DefaultConnectivityLogBufferSize = 524288;

			// Token: 0x04001017 RID: 4119
			private const int DefaultMsgTrkLogBufferSize = 0;

			// Token: 0x04001018 RID: 4120
			private const int DefaultTransportWlmLogBufferSize = 65536;

			// Token: 0x04001019 RID: 4121
			private const int MaxLogBufferSize = 10485760;

			// Token: 0x0400101A RID: 4122
			private const int MinLogBufferSize = 0;

			// Token: 0x0400101B RID: 4123
			private static readonly TimeSpan DefaultSmtpSendLogFlushInterval = TimeSpan.FromSeconds(30.0);

			// Token: 0x0400101C RID: 4124
			private static readonly TimeSpan DefaultSmtpSendLogAsyncInterval = TimeSpan.FromSeconds(1.0);

			// Token: 0x0400101D RID: 4125
			private static readonly TimeSpan DefaultSmtpRecvLogFlushInterval = TimeSpan.FromSeconds(30.0);

			// Token: 0x0400101E RID: 4126
			private static readonly TimeSpan DefaultSmtpRecvLogAsyncInterval = TimeSpan.FromSeconds(1.0);

			// Token: 0x0400101F RID: 4127
			private static readonly TimeSpan DefaultConnectivityLogFlushInterval = TimeSpan.FromSeconds(30.0);

			// Token: 0x04001020 RID: 4128
			private static readonly TimeSpan DefaultConnectivityLogAsyncInterval = TimeSpan.FromSeconds(1.0);

			// Token: 0x04001021 RID: 4129
			private static readonly TimeSpan DefaultMsgTrkLogFlushInterval = TimeSpan.MaxValue;

			// Token: 0x04001022 RID: 4130
			private static readonly int DefaultMaxMsgTrkAgentInfoSize = 3072;

			// Token: 0x04001023 RID: 4131
			private static readonly TimeSpan DefaultTransportWlmLogFlushInterval = TimeSpan.FromSeconds(60.0);

			// Token: 0x04001024 RID: 4132
			private static readonly TimeSpan MaxLogFlushInterval = TimeSpan.MaxValue;

			// Token: 0x04001025 RID: 4133
			private static readonly TimeSpan MinLogFlushInterval = TimeSpan.Zero;

			// Token: 0x04001026 RID: 4134
			private static readonly TimeSpan MaxLogAsyncInterval = TimeSpan.FromSeconds(20.0);

			// Token: 0x04001027 RID: 4135
			private static readonly TimeSpan MinLogAsyncInterval = TimeSpan.FromMilliseconds(100.0);

			// Token: 0x04001028 RID: 4136
			private int smtpSendLogBufferSize;

			// Token: 0x04001029 RID: 4137
			private TimeSpan smtpSendLogFlushInterval;

			// Token: 0x0400102A RID: 4138
			private TimeSpan smtpSendLogAsyncInterval;

			// Token: 0x0400102B RID: 4139
			private int smtpRecvLogBufferSize;

			// Token: 0x0400102C RID: 4140
			private TimeSpan smtpRecvLogFlushInterval;

			// Token: 0x0400102D RID: 4141
			private TimeSpan smtpRecvLogAsyncInterval;

			// Token: 0x0400102E RID: 4142
			private int connectivityLogBufferSize;

			// Token: 0x0400102F RID: 4143
			private TimeSpan connectivityLogFlushInterval;

			// Token: 0x04001030 RID: 4144
			private TimeSpan connectivityLogAsyncInterval;

			// Token: 0x04001031 RID: 4145
			private int msgTrkLogBufferSize;

			// Token: 0x04001032 RID: 4146
			private TimeSpan msgTrkLogFlushInterval;

			// Token: 0x04001033 RID: 4147
			private int maxMsgTrkAgentInfoSize;

			// Token: 0x04001034 RID: 4148
			private int transportWlmLogBufferSize;

			// Token: 0x04001035 RID: 4149
			private TimeSpan transportWlmLogFlushInterval;
		}

		// Token: 0x020002C5 RID: 709
		public sealed class FlowControlLogConfig : IFlowControlLogConfig
		{
			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00078FC8 File Offset: 0x000771C8
			public TimeSpan AsyncInterval
			{
				get
				{
					return this.asyncInterval;
				}
			}

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00078FD0 File Offset: 0x000771D0
			public int BufferSize
			{
				get
				{
					return this.bufferSize;
				}
			}

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00078FD8 File Offset: 0x000771D8
			public TimeSpan FlushInterval
			{
				get
				{
					return this.flushInterval;
				}
			}

			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00078FE0 File Offset: 0x000771E0
			public TimeSpan SummaryLoggingInterval
			{
				get
				{
					return this.summaryLoggingInterval;
				}
			}

			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00078FE8 File Offset: 0x000771E8
			public TimeSpan SummaryBucketLength
			{
				get
				{
					return this.summaryBucketLength;
				}
			}

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00078FF0 File Offset: 0x000771F0
			public int MaxSummaryLinesLogged
			{
				get
				{
					return this.maxSummaryLinesLogged;
				}
			}

			// Token: 0x06001F0F RID: 7951 RVA: 0x00078FF8 File Offset: 0x000771F8
			public static TransportAppConfig.FlowControlLogConfig Load()
			{
				return new TransportAppConfig.FlowControlLogConfig
				{
					bufferSize = TransportAppConfig.GetConfigInt("FlowControlLogBufferSize", 0, 10485760, 65536),
					flushInterval = TransportAppConfig.GetConfigTimeSpan("FlowControlLogFlushInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromSeconds(60.0)),
					asyncInterval = TransportAppConfig.GetConfigTimeSpan("FlowControlLogAsyncInterval", TimeSpan.FromMilliseconds(100.0), TimeSpan.FromSeconds(20.0), TimeSpan.FromSeconds(15.0)),
					summaryLoggingInterval = TransportAppConfig.GetConfigTimeSpan("FlowControlLogSummaryInterval", TimeSpan.Zero, TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(15.0)),
					summaryBucketLength = TransportAppConfig.GetConfigTimeSpan("FlowControlLogSummaryBucketLength", TimeSpan.Zero, TimeSpan.FromHours(1.0), TimeSpan.FromSeconds(30.0)),
					maxSummaryLinesLogged = TransportAppConfig.GetConfigInt("FlowControlLogMaxSummaryLinesLogged", 0, int.MaxValue, 100)
				};
			}

			// Token: 0x04001036 RID: 4150
			private TimeSpan asyncInterval;

			// Token: 0x04001037 RID: 4151
			private int bufferSize;

			// Token: 0x04001038 RID: 4152
			private TimeSpan flushInterval;

			// Token: 0x04001039 RID: 4153
			private TimeSpan summaryLoggingInterval;

			// Token: 0x0400103A RID: 4154
			private TimeSpan summaryBucketLength;

			// Token: 0x0400103B RID: 4155
			private int maxSummaryLinesLogged;
		}

		// Token: 0x020002C7 RID: 711
		public sealed class ConditionalThrottlingConfig : IWaitConditionManagerConfig
		{
			// Token: 0x06001F1F RID: 7967 RVA: 0x0007910B File Offset: 0x0007730B
			public ConditionalThrottlingConfig()
			{
			}

			// Token: 0x06001F20 RID: 7968 RVA: 0x00079114 File Offset: 0x00077314
			public ConditionalThrottlingConfig(bool isCategorizer, TransportAppConfig.ConditionalThrottlingConfig clone)
			{
				this.isCategorizer = isCategorizer;
				this.aboveThresholdThrottlingBehaviorEnabled = clone.aboveThresholdThrottlingBehaviorEnabled;
				this.categorizerProcessingTimeThrottlingEnabled = clone.categorizerProcessingTimeThrottlingEnabled;
				this.categorizerTenantThrottlingEnabled = clone.categorizerTenantThrottlingEnabled;
				this.categorizerSenderThrottlingEnabled = clone.categorizerSenderThrottlingEnabled;
				this.categorizerThrottlingHistoryBucketSize = clone.categorizerThrottlingHistoryBucketSize;
				this.categorizerThrottlingHistoryInterval = clone.categorizerThrottlingHistoryInterval;
				this.deliverySenderThrottlingEnabled = clone.deliverySenderThrottlingEnabled;
				this.deliveryTenantThrottlingEnabled = clone.deliveryTenantThrottlingEnabled;
				this.deliveryThrottlingHistoryBucketSize = clone.deliveryThrottlingHistoryBucketSize;
				this.deliveryThrottlingHistoryInterval = clone.deliveryThrottlingHistoryInterval;
				this.throttlingProcessingMinThreshold = clone.throttlingProcessingMinThreshold;
				this.throttlingMemoryMaxThreshold = clone.throttlingMemoryMaxThreshold;
				this.throttlingMemoryMinThreshold = clone.throttlingMemoryMinThreshold;
				this.dehydrationThreshold = clone.dehydrationThreshold;
				this.emptyThrottlingCostRemovalInterval = clone.emptyThrottlingCostRemovalInterval;
				this.maxAllowedCapacityPercentage = clone.maxAllowedCapacityPercentage;
				this.categorizerThrottlingAsyncThreadPercentage = clone.categorizerThrottlingAsyncThreadPercentage;
			}

			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000791FA File Offset: 0x000773FA
			// (set) Token: 0x06001F22 RID: 7970 RVA: 0x00079202 File Offset: 0x00077402
			public bool CategorizerTenantThrottlingEnabled
			{
				get
				{
					return this.categorizerTenantThrottlingEnabled;
				}
				internal set
				{
					this.categorizerTenantThrottlingEnabled = value;
				}
			}

			// Token: 0x17000996 RID: 2454
			// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0007920B File Offset: 0x0007740B
			// (set) Token: 0x06001F24 RID: 7972 RVA: 0x00079213 File Offset: 0x00077413
			public bool CategorizerSenderThrottlingEnabled
			{
				get
				{
					return this.categorizerSenderThrottlingEnabled;
				}
				internal set
				{
					this.categorizerSenderThrottlingEnabled = value;
				}
			}

			// Token: 0x17000997 RID: 2455
			// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0007921C File Offset: 0x0007741C
			// (set) Token: 0x06001F26 RID: 7974 RVA: 0x00079224 File Offset: 0x00077424
			public bool DeliveryTenantThrottlingEnabled
			{
				get
				{
					return this.deliveryTenantThrottlingEnabled;
				}
				internal set
				{
					this.deliveryTenantThrottlingEnabled = value;
				}
			}

			// Token: 0x17000998 RID: 2456
			// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0007922D File Offset: 0x0007742D
			// (set) Token: 0x06001F28 RID: 7976 RVA: 0x00079235 File Offset: 0x00077435
			public bool DeliverySenderThrottlingEnabled
			{
				get
				{
					return this.deliverySenderThrottlingEnabled;
				}
				internal set
				{
					this.deliverySenderThrottlingEnabled = value;
				}
			}

			// Token: 0x17000999 RID: 2457
			// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0007923E File Offset: 0x0007743E
			public bool QuotaOverrideEnabled
			{
				get
				{
					return this.isCategorizer && this.categorizerTenantThrottlingEnabled && Components.TransportAppConfig.ProcessingQuota.EnforceProcessingQuota;
				}
			}

			// Token: 0x1700099A RID: 2458
			// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00079261 File Offset: 0x00077461
			public bool TestQuotaOverrideEnabled
			{
				get
				{
					return this.isCategorizer && this.categorizerTenantThrottlingEnabled && Components.TransportAppConfig.ProcessingQuota.TestProcessingQuota;
				}
			}

			// Token: 0x1700099B RID: 2459
			// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00079284 File Offset: 0x00077484
			// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0007928C File Offset: 0x0007748C
			public bool CategorizerProcessingTimeThrottlingEnabled
			{
				get
				{
					return this.categorizerProcessingTimeThrottlingEnabled;
				}
				internal set
				{
					this.categorizerProcessingTimeThrottlingEnabled = value;
				}
			}

			// Token: 0x1700099C RID: 2460
			// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00079295 File Offset: 0x00077495
			// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0007929D File Offset: 0x0007749D
			public TimeSpan CategorizerThrottlingHistoryInterval
			{
				get
				{
					return this.categorizerThrottlingHistoryInterval;
				}
				internal set
				{
					this.categorizerThrottlingHistoryInterval = value;
				}
			}

			// Token: 0x1700099D RID: 2461
			// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000792A6 File Offset: 0x000774A6
			// (set) Token: 0x06001F30 RID: 7984 RVA: 0x000792AE File Offset: 0x000774AE
			public TimeSpan CategorizerThrottlingHistoryBucketSize
			{
				get
				{
					return this.categorizerThrottlingHistoryBucketSize;
				}
				internal set
				{
					this.categorizerThrottlingHistoryBucketSize = value;
				}
			}

			// Token: 0x1700099E RID: 2462
			// (get) Token: 0x06001F31 RID: 7985 RVA: 0x000792B7 File Offset: 0x000774B7
			// (set) Token: 0x06001F32 RID: 7986 RVA: 0x000792BF File Offset: 0x000774BF
			public TimeSpan DeliveryThrottlingHistoryInterval
			{
				get
				{
					return this.deliveryThrottlingHistoryInterval;
				}
				internal set
				{
					this.deliveryThrottlingHistoryInterval = value;
				}
			}

			// Token: 0x1700099F RID: 2463
			// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000792C8 File Offset: 0x000774C8
			// (set) Token: 0x06001F34 RID: 7988 RVA: 0x000792D0 File Offset: 0x000774D0
			public TimeSpan DeliveryThrottlingHistoryBucketSize
			{
				get
				{
					return this.deliveryThrottlingHistoryBucketSize;
				}
				internal set
				{
					this.deliveryThrottlingHistoryBucketSize = value;
				}
			}

			// Token: 0x170009A0 RID: 2464
			// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000792D9 File Offset: 0x000774D9
			// (set) Token: 0x06001F36 RID: 7990 RVA: 0x000792E1 File Offset: 0x000774E1
			public int CategorizerThrottlingAsyncThreadPercentage
			{
				get
				{
					return this.categorizerThrottlingAsyncThreadPercentage;
				}
				internal set
				{
					this.categorizerThrottlingAsyncThreadPercentage = value;
				}
			}

			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000792EA File Offset: 0x000774EA
			// (set) Token: 0x06001F38 RID: 7992 RVA: 0x000792F2 File Offset: 0x000774F2
			public TimeSpan ThrottlingProcessingMinThreshold
			{
				get
				{
					return this.throttlingProcessingMinThreshold;
				}
				internal set
				{
					this.throttlingProcessingMinThreshold = value;
				}
			}

			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000792FB File Offset: 0x000774FB
			// (set) Token: 0x06001F3A RID: 7994 RVA: 0x00079303 File Offset: 0x00077503
			public ByteQuantifiedSize ThrottlingMemoryMinThreshold
			{
				get
				{
					return this.throttlingMemoryMinThreshold;
				}
				internal set
				{
					this.throttlingMemoryMinThreshold = value;
				}
			}

			// Token: 0x170009A3 RID: 2467
			// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0007930C File Offset: 0x0007750C
			public ByteQuantifiedSize ThrottlingMemoryMaxThreshold
			{
				get
				{
					return this.throttlingMemoryMaxThreshold;
				}
			}

			// Token: 0x170009A4 RID: 2468
			// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00079314 File Offset: 0x00077514
			// (set) Token: 0x06001F3D RID: 7997 RVA: 0x0007931C File Offset: 0x0007751C
			public bool AboveThresholdThrottlingBehaviorEnabled
			{
				get
				{
					return this.aboveThresholdThrottlingBehaviorEnabled;
				}
				internal set
				{
					this.aboveThresholdThrottlingBehaviorEnabled = value;
				}
			}

			// Token: 0x170009A5 RID: 2469
			// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00079325 File Offset: 0x00077525
			// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0007932D File Offset: 0x0007752D
			public int MaxAllowedCapacityPercentage
			{
				get
				{
					return this.maxAllowedCapacityPercentage;
				}
				internal set
				{
					this.maxAllowedCapacityPercentage = value;
				}
			}

			// Token: 0x170009A6 RID: 2470
			// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00079336 File Offset: 0x00077536
			// (set) Token: 0x06001F41 RID: 8001 RVA: 0x0007933E File Offset: 0x0007753E
			public TimeSpan EmptyThrottlingCostRemovalInterval
			{
				get
				{
					return this.emptyThrottlingCostRemovalInterval;
				}
				internal set
				{
					this.emptyThrottlingCostRemovalInterval = value;
				}
			}

			// Token: 0x170009A7 RID: 2471
			// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00079347 File Offset: 0x00077547
			// (set) Token: 0x06001F43 RID: 8003 RVA: 0x0007934F File Offset: 0x0007754F
			public int LockedMessageDehydrationThreshold
			{
				get
				{
					return this.dehydrationThreshold;
				}
				internal set
				{
					this.dehydrationThreshold = value;
				}
			}

			// Token: 0x170009A8 RID: 2472
			// (get) Token: 0x06001F44 RID: 8004 RVA: 0x00079358 File Offset: 0x00077558
			public bool TenantThrottlingEnabled
			{
				get
				{
					if (!this.isCategorizer)
					{
						return this.deliveryTenantThrottlingEnabled;
					}
					return this.categorizerTenantThrottlingEnabled;
				}
			}

			// Token: 0x170009A9 RID: 2473
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0007936F File Offset: 0x0007756F
			public bool SenderThrottlingEnabled
			{
				get
				{
					if (!this.isCategorizer)
					{
						return this.deliverySenderThrottlingEnabled;
					}
					return this.categorizerSenderThrottlingEnabled;
				}
			}

			// Token: 0x170009AA RID: 2474
			// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00079386 File Offset: 0x00077586
			public bool ProcessingTimeThrottlingEnabled
			{
				get
				{
					return this.isCategorizer && this.categorizerProcessingTimeThrottlingEnabled;
				}
			}

			// Token: 0x170009AB RID: 2475
			// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00079398 File Offset: 0x00077598
			public TimeSpan ThrottlingHistoryInterval
			{
				get
				{
					if (!this.isCategorizer)
					{
						return this.deliveryThrottlingHistoryInterval;
					}
					return this.categorizerThrottlingHistoryInterval;
				}
			}

			// Token: 0x170009AC RID: 2476
			// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000793AF File Offset: 0x000775AF
			public TimeSpan ThrottlingHistoryBucketSize
			{
				get
				{
					if (!this.isCategorizer)
					{
						return this.deliveryThrottlingHistoryBucketSize;
					}
					return this.categorizerThrottlingHistoryBucketSize;
				}
			}

			// Token: 0x170009AD RID: 2477
			// (get) Token: 0x06001F49 RID: 8009 RVA: 0x000793C6 File Offset: 0x000775C6
			public TimeSpan LockExpirationInterval
			{
				get
				{
					return this.lockExpirationInterval;
				}
			}

			// Token: 0x170009AE RID: 2478
			// (get) Token: 0x06001F4A RID: 8010 RVA: 0x000793CE File Offset: 0x000775CE
			public TimeSpan LockExpirationCheckPeriod
			{
				get
				{
					return this.lockExpirationCheckPeriod;
				}
			}

			// Token: 0x06001F4B RID: 8011 RVA: 0x000793D8 File Offset: 0x000775D8
			public static TransportAppConfig.ConditionalThrottlingConfig Load()
			{
				TransportAppConfig.ConditionalThrottlingConfig conditionalThrottlingConfig = new TransportAppConfig.ConditionalThrottlingConfig();
				conditionalThrottlingConfig.categorizerTenantThrottlingEnabled = (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TenantThrottling.Enabled && TransportAppConfig.GetConfigBool("CategorizerTenantThrottlingEnabled", VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TenantThrottling.Enabled));
				conditionalThrottlingConfig.categorizerSenderThrottlingEnabled = TransportAppConfig.GetConfigBool("CategorizerSenderThrottlingEnabled", TransportAppConfig.ConditionalThrottlingConfig.DefaultSenderThrottlingEnabled);
				conditionalThrottlingConfig.deliveryTenantThrottlingEnabled = (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TenantThrottling.Enabled && TransportAppConfig.GetConfigBool("DeliveryTenantThrottlingEnabled", VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TenantThrottling.Enabled));
				conditionalThrottlingConfig.deliverySenderThrottlingEnabled = TransportAppConfig.GetConfigBool("DeliverySenderThrottlingEnabled", TransportAppConfig.ConditionalThrottlingConfig.DefaultSenderThrottlingEnabled);
				conditionalThrottlingConfig.categorizerProcessingTimeThrottlingEnabled = TransportAppConfig.GetConfigBool("CategorizerProcessingTimeThrottlingEnabled", TransportAppConfig.ConditionalThrottlingConfig.DefaultCategorizerProcessingTimeThrottlingEnabled);
				conditionalThrottlingConfig.categorizerThrottlingHistoryInterval = TransportAppConfig.GetConfigTimeSpan("CategorizerThrottlingHistoryInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultCategorizerThrottlingHistoryInterval);
				conditionalThrottlingConfig.categorizerThrottlingHistoryBucketSize = TransportAppConfig.GetConfigTimeSpan("CategorizerThrottlingHistoryBucketSize", TimeSpan.FromSeconds(5.0), conditionalThrottlingConfig.categorizerThrottlingHistoryInterval, TransportAppConfig.ConditionalThrottlingConfig.DefaultCategorizerThrottlingHistoryBucketSize);
				conditionalThrottlingConfig.deliveryThrottlingHistoryInterval = TransportAppConfig.GetConfigTimeSpan("DeliveryThrottlingHistoryInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultDeliveryThrottlingHistoryInterval);
				conditionalThrottlingConfig.deliveryThrottlingHistoryBucketSize = TransportAppConfig.GetConfigTimeSpan("DeliveryThrottlingHistoryBucketSize", TimeSpan.FromSeconds(5.0), conditionalThrottlingConfig.deliveryThrottlingHistoryInterval, TransportAppConfig.ConditionalThrottlingConfig.DefaultDeliveryThrottlingHistoryBucketSize);
				conditionalThrottlingConfig.throttlingProcessingMinThreshold = TransportAppConfig.GetConfigTimeSpan("ThrottlingProcessingTimeMinThreshold", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultThrottlingProcessingMinThreshold);
				conditionalThrottlingConfig.throttlingMemoryMinThreshold = TransportAppConfig.GetConfigByteQuantifiedSize("ThrottlingMemoryMinThreshold", ByteQuantifiedSize.Zero, ByteQuantifiedSize.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultThrottlingMemoryMinThreshold);
				conditionalThrottlingConfig.throttlingMemoryMaxThreshold = TransportAppConfig.GetConfigByteQuantifiedSize("ThrottlingMemoryMaxThreshold", ByteQuantifiedSize.Zero, ByteQuantifiedSize.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultThrottlingMemoryMaxThreshold);
				conditionalThrottlingConfig.aboveThresholdThrottlingBehaviorEnabled = TransportAppConfig.GetConfigBool("AboveThresholdThrottlingBehaviorEnabled", TransportAppConfig.ConditionalThrottlingConfig.DefaultAllowAboveThresholdThrottlingBehaviorEnabled);
				conditionalThrottlingConfig.maxAllowedCapacityPercentage = TransportAppConfig.GetConfigInt("MaxAllowedCapacityPercentageThrotlling", 0, 100, TransportAppConfig.ConditionalThrottlingConfig.DefaultMaxAllowedCapacityPercentage);
				conditionalThrottlingConfig.emptyThrottlingCostRemovalInterval = TransportAppConfig.GetConfigTimeSpan("EmptyThrotllingCostRemovalInterval", TimeSpan.FromSeconds(5.0), TimeSpan.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultEmptyThrottlingCostRemovalInterval);
				conditionalThrottlingConfig.dehydrationThreshold = TransportAppConfig.GetConfigInt("ConditionalLockingDehydrationThreshold", 0, int.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultDehydrationThreshold);
				conditionalThrottlingConfig.categorizerThrottlingAsyncThreadPercentage = TransportAppConfig.GetConfigInt("CategorizerThrottlingAsyncThreadPercentage", 0, 100, TransportAppConfig.ConditionalThrottlingConfig.DefaultCategorizerThrottlingAsyncThreadPercentage);
				conditionalThrottlingConfig.lockExpirationInterval = TransportAppConfig.GetConfigTimeSpan("LockExpirationInterval", TimeSpan.Zero, TimeSpan.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultLockExpirationInterval);
				conditionalThrottlingConfig.lockExpirationCheckPeriod = TransportAppConfig.GetConfigTimeSpan("LockExpirationCheckPeriod", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.ConditionalThrottlingConfig.DefaultLockExpirationCheckPeriod);
				return conditionalThrottlingConfig;
			}

			// Token: 0x06001F4C RID: 8012 RVA: 0x00079680 File Offset: 0x00077880
			internal IWaitConditionManagerConfig GetConfig(bool forCategorizer)
			{
				return new TransportAppConfig.ConditionalThrottlingConfig(forCategorizer, this);
			}

			// Token: 0x0400103C RID: 4156
			private const string CategorizerTenantThrottlingEnabledString = "CategorizerTenantThrottlingEnabled";

			// Token: 0x0400103D RID: 4157
			private const string CategorizerSenderThrottlingEnabledString = "CategorizerSenderThrottlingEnabled";

			// Token: 0x0400103E RID: 4158
			private const string DeliveryTenantThrottlingEnabledString = "DeliveryTenantThrottlingEnabled";

			// Token: 0x0400103F RID: 4159
			private const string DeliverySenderThrottlingEnabledString = "DeliverySenderThrottlingEnabled";

			// Token: 0x04001040 RID: 4160
			private const string CategorizerProcessingTimeThrottlingEnabledString = "CategorizerProcessingTimeThrottlingEnabled";

			// Token: 0x04001041 RID: 4161
			private const string CategorizerThrottlingHistoryIntervalString = "CategorizerThrottlingHistoryInterval";

			// Token: 0x04001042 RID: 4162
			private const string CategorizerThrottlingHistoryBucketSizeString = "CategorizerThrottlingHistoryBucketSize";

			// Token: 0x04001043 RID: 4163
			private const string DeliveryThrottlingHistoryIntervalString = "DeliveryThrottlingHistoryInterval";

			// Token: 0x04001044 RID: 4164
			private const string DeliveryThrottlingHistoryBucketSizeString = "DeliveryThrottlingHistoryBucketSize";

			// Token: 0x04001045 RID: 4165
			private const string ThrottlingProcessingTimeMinThresholdString = "ThrottlingProcessingTimeMinThreshold";

			// Token: 0x04001046 RID: 4166
			private const string ThrottlingMemoryMinThresholdString = "ThrottlingMemoryMinThreshold";

			// Token: 0x04001047 RID: 4167
			private const string ThrottlingMemoryMaxThresholdString = "ThrottlingMemoryMaxThreshold";

			// Token: 0x04001048 RID: 4168
			private const string AboveThresholdThrottlingBehaviorEnabledString = "AboveThresholdThrottlingBehaviorEnabled";

			// Token: 0x04001049 RID: 4169
			private const string MaxAllowedCapacityPercentageThrotllingString = "MaxAllowedCapacityPercentageThrotlling";

			// Token: 0x0400104A RID: 4170
			private const string EmptyThrottlingCostRemovalIntervalString = "EmptyThrotllingCostRemovalInterval";

			// Token: 0x0400104B RID: 4171
			private const string DehydrationThresholdString = "ConditionalLockingDehydrationThreshold";

			// Token: 0x0400104C RID: 4172
			private const string CategorizerThrottlingAsyncThreadPercentageString = "CategorizerThrottlingAsyncThreadPercentage";

			// Token: 0x0400104D RID: 4173
			private const string LockExpirationIntervalString = "LockExpirationInterval";

			// Token: 0x0400104E RID: 4174
			private const string LockExpirationCheckPeriodString = "LockExpirationCheckPeriod";

			// Token: 0x0400104F RID: 4175
			private static readonly bool DefaultSenderThrottlingEnabled = false;

			// Token: 0x04001050 RID: 4176
			private static readonly bool DefaultCategorizerProcessingTimeThrottlingEnabled = true;

			// Token: 0x04001051 RID: 4177
			private static readonly TimeSpan DefaultCategorizerThrottlingHistoryInterval = TimeSpan.FromMinutes(2.0);

			// Token: 0x04001052 RID: 4178
			private static readonly TimeSpan DefaultCategorizerThrottlingHistoryBucketSize = TimeSpan.FromSeconds(5.0);

			// Token: 0x04001053 RID: 4179
			private static readonly TimeSpan DefaultDeliveryThrottlingHistoryInterval = TimeSpan.FromMinutes(2.0);

			// Token: 0x04001054 RID: 4180
			private static readonly TimeSpan DefaultDeliveryThrottlingHistoryBucketSize = TimeSpan.FromSeconds(5.0);

			// Token: 0x04001055 RID: 4181
			private static readonly TimeSpan DefaultThrottlingProcessingMinThreshold = TimeSpan.FromSeconds(1.0);

			// Token: 0x04001056 RID: 4182
			private static readonly ByteQuantifiedSize DefaultThrottlingMemoryMinThreshold = ByteQuantifiedSize.FromKB(50UL);

			// Token: 0x04001057 RID: 4183
			private static readonly ByteQuantifiedSize DefaultThrottlingMemoryMaxThreshold = ByteQuantifiedSize.FromMB(500UL);

			// Token: 0x04001058 RID: 4184
			private static readonly bool DefaultAllowAboveThresholdThrottlingBehaviorEnabled = true;

			// Token: 0x04001059 RID: 4185
			private static readonly int DefaultMaxAllowedCapacityPercentage = 85;

			// Token: 0x0400105A RID: 4186
			private static readonly TimeSpan DefaultEmptyThrottlingCostRemovalInterval = TimeSpan.FromSeconds(30.0);

			// Token: 0x0400105B RID: 4187
			private static readonly int DefaultDehydrationThreshold = 2500;

			// Token: 0x0400105C RID: 4188
			private static readonly int DefaultCategorizerThrottlingAsyncThreadPercentage = 50;

			// Token: 0x0400105D RID: 4189
			private static readonly TimeSpan DefaultLockExpirationInterval = TimeSpan.FromMinutes(10.0);

			// Token: 0x0400105E RID: 4190
			private static readonly TimeSpan DefaultLockExpirationCheckPeriod = TimeSpan.FromMinutes(1.0);

			// Token: 0x0400105F RID: 4191
			private readonly bool isCategorizer;

			// Token: 0x04001060 RID: 4192
			private bool categorizerTenantThrottlingEnabled;

			// Token: 0x04001061 RID: 4193
			private bool categorizerSenderThrottlingEnabled;

			// Token: 0x04001062 RID: 4194
			private bool deliveryTenantThrottlingEnabled;

			// Token: 0x04001063 RID: 4195
			private bool deliverySenderThrottlingEnabled;

			// Token: 0x04001064 RID: 4196
			private bool categorizerProcessingTimeThrottlingEnabled;

			// Token: 0x04001065 RID: 4197
			private TimeSpan categorizerThrottlingHistoryInterval;

			// Token: 0x04001066 RID: 4198
			private TimeSpan categorizerThrottlingHistoryBucketSize;

			// Token: 0x04001067 RID: 4199
			private TimeSpan deliveryThrottlingHistoryInterval;

			// Token: 0x04001068 RID: 4200
			private TimeSpan deliveryThrottlingHistoryBucketSize;

			// Token: 0x04001069 RID: 4201
			private TimeSpan throttlingProcessingMinThreshold;

			// Token: 0x0400106A RID: 4202
			private ByteQuantifiedSize throttlingMemoryMinThreshold;

			// Token: 0x0400106B RID: 4203
			private ByteQuantifiedSize throttlingMemoryMaxThreshold;

			// Token: 0x0400106C RID: 4204
			private bool aboveThresholdThrottlingBehaviorEnabled;

			// Token: 0x0400106D RID: 4205
			private int maxAllowedCapacityPercentage;

			// Token: 0x0400106E RID: 4206
			private TimeSpan emptyThrottlingCostRemovalInterval;

			// Token: 0x0400106F RID: 4207
			private int dehydrationThreshold;

			// Token: 0x04001070 RID: 4208
			private int categorizerThrottlingAsyncThreadPercentage;

			// Token: 0x04001071 RID: 4209
			private TimeSpan lockExpirationInterval;

			// Token: 0x04001072 RID: 4210
			private TimeSpan lockExpirationCheckPeriod;
		}

		// Token: 0x020002C8 RID: 712
		public sealed class TransportRulesConfig
		{
			// Token: 0x06001F4E RID: 8014 RVA: 0x00079778 File Offset: 0x00077978
			private TransportRulesConfig()
			{
			}

			// Token: 0x170009AF RID: 2479
			// (get) Token: 0x06001F4F RID: 8015 RVA: 0x00079780 File Offset: 0x00077980
			// (set) Token: 0x06001F50 RID: 8016 RVA: 0x00079788 File Offset: 0x00077988
			public int TransportRuleLoadTimeReportingThresholdInMilliseconds { get; private set; }

			// Token: 0x170009B0 RID: 2480
			// (get) Token: 0x06001F51 RID: 8017 RVA: 0x00079791 File Offset: 0x00077991
			// (set) Token: 0x06001F52 RID: 8018 RVA: 0x00079799 File Offset: 0x00077999
			public int TransportRuleExecutionTimeReportingThresholdInBytesPerSecond { get; private set; }

			// Token: 0x170009B1 RID: 2481
			// (get) Token: 0x06001F53 RID: 8019 RVA: 0x000797A2 File Offset: 0x000779A2
			// (set) Token: 0x06001F54 RID: 8020 RVA: 0x000797AA File Offset: 0x000779AA
			public int TransportRuleLoadTimeAlertingThresholdInMilliseconds { get; private set; }

			// Token: 0x170009B2 RID: 2482
			// (get) Token: 0x06001F55 RID: 8021 RVA: 0x000797B3 File Offset: 0x000779B3
			// (set) Token: 0x06001F56 RID: 8022 RVA: 0x000797BB File Offset: 0x000779BB
			public int TransportRuleExecutionTimeAlertingThresholdInBytesPerSecond { get; private set; }

			// Token: 0x170009B3 RID: 2483
			// (get) Token: 0x06001F57 RID: 8023 RVA: 0x000797C4 File Offset: 0x000779C4
			// (set) Token: 0x06001F58 RID: 8024 RVA: 0x000797CC File Offset: 0x000779CC
			public int TransportRuleMinFipsTimeoutInMilliseconds { get; private set; }

			// Token: 0x170009B4 RID: 2484
			// (get) Token: 0x06001F59 RID: 8025 RVA: 0x000797D5 File Offset: 0x000779D5
			// (set) Token: 0x06001F5A RID: 8026 RVA: 0x000797DD File Offset: 0x000779DD
			public Dictionary<string, uint> ScanVelocities { get; private set; }

			// Token: 0x170009B5 RID: 2485
			// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000797E6 File Offset: 0x000779E6
			// (set) Token: 0x06001F5C RID: 8028 RVA: 0x000797EE File Offset: 0x000779EE
			public int TransportRuleMaxForkCount { get; private set; }

			// Token: 0x06001F5D RID: 8029 RVA: 0x000797F8 File Offset: 0x000779F8
			public static TransportAppConfig.TransportRulesConfig Load()
			{
				TransportAppConfig.TransportRulesConfig transportRulesConfig = new TransportAppConfig.TransportRulesConfig();
				transportRulesConfig.TransportRuleLoadTimeReportingThresholdInMilliseconds = TransportAppConfig.GetConfigInt("TransportRuleLoadTimeReportingThresholdInMilliseconds", 0, int.MaxValue, 1000);
				transportRulesConfig.TransportRuleExecutionTimeReportingThresholdInBytesPerSecond = TransportAppConfig.GetConfigInt("TransportRuleExecutionTimeReportingThresholdInBytesPerSecond", 1, int.MaxValue, 307200);
				transportRulesConfig.TransportRuleLoadTimeAlertingThresholdInMilliseconds = TransportAppConfig.GetConfigInt("TransportRuleLoadTimeAlertingThresholdInMilliseconds", 0, int.MaxValue, 30000);
				transportRulesConfig.TransportRuleExecutionTimeAlertingThresholdInBytesPerSecond = TransportAppConfig.GetConfigInt("TransportRuleExecutionTimeAlertingThresholdInBytesPerSecond", 1, int.MaxValue, 1024);
				transportRulesConfig.TransportRuleMinFipsTimeoutInMilliseconds = TransportAppConfig.GetConfigInt("TransportRuleMinFipsTimeoutInMilliseconds", 0, int.MaxValue, 60000);
				transportRulesConfig.TransportRuleMaxForkCount = TransportAppConfig.GetConfigInt("TransportRuleMaxForkCount", 0, int.MaxValue, 200);
				transportRulesConfig.ScanVelocities = new Dictionary<string, uint>(TransportAppConfig.TransportRulesConfig.defaultScanVelocities);
				TransportAppConfig.ConfigurationListsSection configurationListsSection = ConfigurationManager.GetSection("customLists") as TransportAppConfig.ConfigurationListsSection;
				if (configurationListsSection != null)
				{
					List<KeyValuePair<string, uint>> configList = TransportAppConfig.GetConfigList<KeyValuePair<string, uint>>(configurationListsSection.TransportRulesScanVelocities, new TransportAppConfig.TryParse<KeyValuePair<string, uint>>(TransportAppConfig.TransportRulesConfig.TryParse));
					foreach (KeyValuePair<string, uint> keyValuePair in configList)
					{
						if (keyValuePair.Value != 0U)
						{
							transportRulesConfig.ScanVelocities[keyValuePair.Key] = keyValuePair.Value;
						}
					}
				}
				return transportRulesConfig;
			}

			// Token: 0x06001F5E RID: 8030 RVA: 0x00079948 File Offset: 0x00077B48
			private static bool TryParse(string config, out KeyValuePair<string, uint> parsedConfig)
			{
				parsedConfig = new KeyValuePair<string, uint>(string.Empty, 0U);
				if (string.IsNullOrWhiteSpace(config))
				{
					return false;
				}
				string[] array = config.Split(new char[]
				{
					','
				});
				if (array == null || array.Length != 2)
				{
					return false;
				}
				int value = 0;
				if (!int.TryParse(array[1], out value))
				{
					return false;
				}
				parsedConfig = new KeyValuePair<string, uint>(array[0], (uint)value);
				return true;
			}

			// Token: 0x04001073 RID: 4211
			private static readonly Dictionary<string, uint> defaultScanVelocities = new Dictionary<string, uint>
			{
				{
					".",
					30U
				},
				{
					"doc",
					1292U
				},
				{
					"docx",
					92U
				},
				{
					"xls",
					166U
				},
				{
					"xlsx",
					30U
				},
				{
					"ppt",
					7000U
				},
				{
					"pptx",
					400U
				},
				{
					"htm",
					120U
				},
				{
					"html",
					120U
				},
				{
					"pdf",
					840U
				}
			};
		}

		// Token: 0x020002C9 RID: 713
		public sealed class PoisonMessageConfig
		{
			// Token: 0x170009B6 RID: 2486
			// (get) Token: 0x06001F60 RID: 8032 RVA: 0x00079A5A File Offset: 0x00077C5A
			// (set) Token: 0x06001F61 RID: 8033 RVA: 0x00079A62 File Offset: 0x00077C62
			public TimeSpan CrashDetectionWindow { get; private set; }

			// Token: 0x170009B7 RID: 2487
			// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00079A6B File Offset: 0x00077C6B
			// (set) Token: 0x06001F63 RID: 8035 RVA: 0x00079A73 File Offset: 0x00077C73
			public double AsyncMultiplier { get; private set; }

			// Token: 0x06001F64 RID: 8036 RVA: 0x00079A7C File Offset: 0x00077C7C
			public static TransportAppConfig.PoisonMessageConfig Load()
			{
				return new TransportAppConfig.PoisonMessageConfig
				{
					CrashDetectionWindow = TransportAppConfig.GetConfigTimeSpan("PoisonMessageDetectionWindow", TimeSpan.Zero, TransportAppConfig.PoisonMessageConfig.maxCrashDetectionWindow, TransportAppConfig.PoisonMessageConfig.defaultCrashDetectionWindow),
					AsyncMultiplier = TransportAppConfig.GetConfigDouble("PoisonThresholdAsyncMultiplier", TransportAppConfig.PoisonMessageConfig.minPoisonThresholdAsyncMultiplier, TransportAppConfig.PoisonMessageConfig.maxPoisonThresholdAsyncMultiplier, TransportAppConfig.PoisonMessageConfig.defaultPoisonThresholdAsyncMultiplier)
				};
			}

			// Token: 0x0400107B RID: 4219
			private const string PoisonMessageDetectionWindow = "PoisonMessageDetectionWindow";

			// Token: 0x0400107C RID: 4220
			private const string PoisonThresholdAsyncMultiplier = "PoisonThresholdAsyncMultiplier";

			// Token: 0x0400107D RID: 4221
			private static TimeSpan defaultCrashDetectionWindow = TimeSpan.FromHours(8.0);

			// Token: 0x0400107E RID: 4222
			private static TimeSpan maxCrashDetectionWindow = TimeSpan.FromDays(2.0);

			// Token: 0x0400107F RID: 4223
			private static double minPoisonThresholdAsyncMultiplier = 0.0;

			// Token: 0x04001080 RID: 4224
			private static double maxPoisonThresholdAsyncMultiplier = 1.0;

			// Token: 0x04001081 RID: 4225
			private static double defaultPoisonThresholdAsyncMultiplier = 0.4;
		}

		// Token: 0x020002CA RID: 714
		public sealed class BootLoaderConfig : IBootLoaderConfig
		{
			// Token: 0x170009B8 RID: 2488
			// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00079B35 File Offset: 0x00077D35
			// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00079B3D File Offset: 0x00077D3D
			public bool BootLoaderMessageTrackingEnabled { get; private set; }

			// Token: 0x170009B9 RID: 2489
			// (get) Token: 0x06001F69 RID: 8041 RVA: 0x00079B46 File Offset: 0x00077D46
			// (set) Token: 0x06001F6A RID: 8042 RVA: 0x00079B4E File Offset: 0x00077D4E
			public TimeSpan MessageDropTimeout { get; private set; }

			// Token: 0x170009BA RID: 2490
			// (get) Token: 0x06001F6B RID: 8043 RVA: 0x00079B57 File Offset: 0x00077D57
			// (set) Token: 0x06001F6C RID: 8044 RVA: 0x00079B5F File Offset: 0x00077D5F
			public TimeSpan MessageExpirationGracePeriod { get; private set; }

			// Token: 0x170009BB RID: 2491
			// (get) Token: 0x06001F6D RID: 8045 RVA: 0x00079B68 File Offset: 0x00077D68
			// (set) Token: 0x06001F6E RID: 8046 RVA: 0x00079B70 File Offset: 0x00077D70
			public TimeSpan PoisonMessageRetentionPeriod { get; private set; }

			// Token: 0x170009BC RID: 2492
			// (get) Token: 0x06001F6F RID: 8047 RVA: 0x00079B79 File Offset: 0x00077D79
			public bool PoisonCountPublishingEnabled
			{
				get
				{
					return this.PoisonCountLookbackHours > 0;
				}
			}

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06001F70 RID: 8048 RVA: 0x00079B84 File Offset: 0x00077D84
			// (set) Token: 0x06001F71 RID: 8049 RVA: 0x00079B8C File Offset: 0x00077D8C
			public int PoisonCountLookbackHours { get; private set; }

			// Token: 0x06001F72 RID: 8050 RVA: 0x00079B98 File Offset: 0x00077D98
			public static TransportAppConfig.BootLoaderConfig Load()
			{
				return new TransportAppConfig.BootLoaderConfig
				{
					BootLoaderMessageTrackingEnabled = TransportAppConfig.GetConfigBool("BootLoaderMessageTrackingEnabled", TransportAppConfig.BootLoaderConfig.defaultBootLoaderMessageTrackingEnabled),
					MessageDropTimeout = TransportAppConfig.GetConfigTimeSpan("BootLoaderMessageSilentExpirationPeriod", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.BootLoaderConfig.defaultMessageDropTimeout),
					MessageExpirationGracePeriod = TransportAppConfig.GetConfigTimeSpan("BootLoaderMessageExpirationGracePeriod", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TransportAppConfig.BootLoaderConfig.defaultMessageExpirationGracePeriod),
					PoisonMessageRetentionPeriod = TransportAppConfig.GetConfigTimeSpan("PoisonMessageRetentionPeriod", TimeSpan.FromDays(1.0), TimeSpan.MaxValue, TransportAppConfig.BootLoaderConfig.defaultPoisonMessageRetentionPeriod),
					PoisonCountLookbackHours = TransportAppConfig.GetConfigInt("PoisonMessagePerfCounterLookbackHours", int.MinValue, int.MaxValue, TransportAppConfig.BootLoaderConfig.defaultPoisonCountLookbackHours)
				};
			}

			// Token: 0x04001084 RID: 4228
			private const string BootLoaderMessageTrackingEnabledSetting = "BootLoaderMessageTrackingEnabled";

			// Token: 0x04001085 RID: 4229
			private const string MessageDropTimeoutSetting = "BootLoaderMessageSilentExpirationPeriod";

			// Token: 0x04001086 RID: 4230
			private const string MessageExpirationGracePeriodSetting = "BootLoaderMessageExpirationGracePeriod";

			// Token: 0x04001087 RID: 4231
			private const string PoisonMessageRetentionPeriodSetting = "PoisonMessageRetentionPeriod";

			// Token: 0x04001088 RID: 4232
			private const string PoisonMessageLookbackHoursSetting = "PoisonMessagePerfCounterLookbackHours";

			// Token: 0x04001089 RID: 4233
			private static bool defaultBootLoaderMessageTrackingEnabled = false;

			// Token: 0x0400108A RID: 4234
			private static TimeSpan defaultMessageDropTimeout = TimeSpan.FromDays(7.0);

			// Token: 0x0400108B RID: 4235
			private static TimeSpan defaultMessageExpirationGracePeriod = TimeSpan.FromMinutes(30.0);

			// Token: 0x0400108C RID: 4236
			private static TimeSpan defaultPoisonMessageRetentionPeriod = TimeSpan.FromDays(365.0);

			// Token: 0x0400108D RID: 4237
			private static int defaultPoisonCountLookbackHours = 24;
		}

		// Token: 0x020002CB RID: 715
		internal interface ISmtpMessageThrottlingAgentConfig
		{
			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06001F75 RID: 8053
			bool Enabled { get; }

			// Token: 0x170009BF RID: 2495
			// (get) Token: 0x06001F76 RID: 8054
			bool LoggingModeEnabled { get; }

			// Token: 0x170009C0 RID: 2496
			// (get) Token: 0x06001F77 RID: 8055
			bool SubjectThrottlingEnabled { get; }

			// Token: 0x170009C1 RID: 2497
			// (get) Token: 0x06001F78 RID: 8056
			int InitialSenderRecipientRateTrackingThreshold { get; }

			// Token: 0x170009C2 RID: 2498
			// (get) Token: 0x06001F79 RID: 8057
			TimeSpan InitialRateTrackingInterval { get; }

			// Token: 0x170009C3 RID: 2499
			// (get) Token: 0x06001F7A RID: 8058
			TimeSpan SenderRateTrackingInterval { get; }

			// Token: 0x170009C4 RID: 2500
			// (get) Token: 0x06001F7B RID: 8059
			bool WhitelistEnabled { get; }

			// Token: 0x170009C5 RID: 2501
			// (get) Token: 0x06001F7C RID: 8060
			int EhaSenderToEhaRecipientThreshold { get; }

			// Token: 0x170009C6 RID: 2502
			// (get) Token: 0x06001F7D RID: 8061
			int SenderRecipientThreshold { get; }

			// Token: 0x170009C7 RID: 2503
			// (get) Token: 0x06001F7E RID: 8062
			int LowWatermarkThreshold { get; }

			// Token: 0x170009C8 RID: 2504
			// (get) Token: 0x06001F7F RID: 8063
			TimeSpan ExpirationProcessingInterval { get; }

			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x06001F80 RID: 8064
			int SenderRecipientSubjectThreshold { get; }

			// Token: 0x170009CA RID: 2506
			// (get) Token: 0x06001F81 RID: 8065
			int SubjectTrackingRemoveThreshold { get; }

			// Token: 0x170009CB RID: 2507
			// (get) Token: 0x06001F82 RID: 8066
			TimeSpan SubjectCounterSlidingWindow { get; }

			// Token: 0x170009CC RID: 2508
			// (get) Token: 0x06001F83 RID: 8067
			TimeSpan SubjectCounterSlidingWindowBucketLength { get; }

			// Token: 0x170009CD RID: 2509
			// (get) Token: 0x06001F84 RID: 8068
			int MaxSubjectCountPerSenderRecipient { get; }

			// Token: 0x170009CE RID: 2510
			// (get) Token: 0x06001F85 RID: 8069
			int SenderRecipientPartialSubjectThrottlingThreshold { get; }

			// Token: 0x170009CF RID: 2511
			// (get) Token: 0x06001F86 RID: 8070
			TimeSpan TrackSummaryLoggingInterval { get; }

			// Token: 0x170009D0 RID: 2512
			// (get) Token: 0x06001F87 RID: 8071
			TimeSpan TrackSummaryBucketLength { get; }

			// Token: 0x170009D1 RID: 2513
			// (get) Token: 0x06001F88 RID: 8072
			int MaxSummaryLinesLogged { get; }
		}

		// Token: 0x020002CC RID: 716
		internal class SmtpMessageThrottlingAgentConfig : TransportAppConfig.ISmtpMessageThrottlingAgentConfig
		{
			// Token: 0x06001F89 RID: 8073 RVA: 0x00079CB3 File Offset: 0x00077EB3
			protected SmtpMessageThrottlingAgentConfig()
			{
			}

			// Token: 0x170009D2 RID: 2514
			// (get) Token: 0x06001F8A RID: 8074 RVA: 0x00079CBB File Offset: 0x00077EBB
			// (set) Token: 0x06001F8B RID: 8075 RVA: 0x00079CC3 File Offset: 0x00077EC3
			public bool Enabled { get; private set; }

			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x06001F8C RID: 8076 RVA: 0x00079CCC File Offset: 0x00077ECC
			// (set) Token: 0x06001F8D RID: 8077 RVA: 0x00079CD4 File Offset: 0x00077ED4
			public bool LoggingModeEnabled { get; private set; }

			// Token: 0x170009D4 RID: 2516
			// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00079CDD File Offset: 0x00077EDD
			// (set) Token: 0x06001F8F RID: 8079 RVA: 0x00079CE5 File Offset: 0x00077EE5
			public virtual bool SubjectThrottlingEnabled { get; private set; }

			// Token: 0x170009D5 RID: 2517
			// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00079CEE File Offset: 0x00077EEE
			// (set) Token: 0x06001F91 RID: 8081 RVA: 0x00079CF6 File Offset: 0x00077EF6
			public virtual int InitialSenderRecipientRateTrackingThreshold { get; private set; }

			// Token: 0x170009D6 RID: 2518
			// (get) Token: 0x06001F92 RID: 8082 RVA: 0x00079CFF File Offset: 0x00077EFF
			// (set) Token: 0x06001F93 RID: 8083 RVA: 0x00079D07 File Offset: 0x00077F07
			public TimeSpan InitialRateTrackingInterval { get; private set; }

			// Token: 0x170009D7 RID: 2519
			// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00079D10 File Offset: 0x00077F10
			// (set) Token: 0x06001F95 RID: 8085 RVA: 0x00079D18 File Offset: 0x00077F18
			public bool WhitelistEnabled { get; private set; }

			// Token: 0x170009D8 RID: 2520
			// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00079D21 File Offset: 0x00077F21
			// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00079D29 File Offset: 0x00077F29
			public virtual int EhaSenderToEhaRecipientThreshold { get; private set; }

			// Token: 0x170009D9 RID: 2521
			// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00079D32 File Offset: 0x00077F32
			// (set) Token: 0x06001F99 RID: 8089 RVA: 0x00079D3A File Offset: 0x00077F3A
			public TimeSpan SenderRateTrackingInterval { get; private set; }

			// Token: 0x170009DA RID: 2522
			// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00079D43 File Offset: 0x00077F43
			// (set) Token: 0x06001F9B RID: 8091 RVA: 0x00079D4B File Offset: 0x00077F4B
			public int SenderRecipientThreshold
			{
				get
				{
					return this.senderRecipientThreshold;
				}
				set
				{
					this.senderRecipientThreshold = value;
				}
			}

			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x06001F9C RID: 8092 RVA: 0x00079D54 File Offset: 0x00077F54
			// (set) Token: 0x06001F9D RID: 8093 RVA: 0x00079D5C File Offset: 0x00077F5C
			public int LowWatermarkThreshold { get; private set; }

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x06001F9E RID: 8094 RVA: 0x00079D65 File Offset: 0x00077F65
			// (set) Token: 0x06001F9F RID: 8095 RVA: 0x00079D6D File Offset: 0x00077F6D
			public TimeSpan ExpirationProcessingInterval { get; private set; }

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x00079D76 File Offset: 0x00077F76
			// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x00079D7E File Offset: 0x00077F7E
			public virtual int SenderRecipientSubjectThreshold { get; private set; }

			// Token: 0x170009DE RID: 2526
			// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x00079D87 File Offset: 0x00077F87
			// (set) Token: 0x06001FA3 RID: 8099 RVA: 0x00079D8F File Offset: 0x00077F8F
			public virtual int SubjectTrackingRemoveThreshold { get; private set; }

			// Token: 0x170009DF RID: 2527
			// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x00079D98 File Offset: 0x00077F98
			// (set) Token: 0x06001FA5 RID: 8101 RVA: 0x00079DA0 File Offset: 0x00077FA0
			public virtual TimeSpan SubjectCounterSlidingWindow { get; private set; }

			// Token: 0x170009E0 RID: 2528
			// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x00079DA9 File Offset: 0x00077FA9
			// (set) Token: 0x06001FA7 RID: 8103 RVA: 0x00079DB1 File Offset: 0x00077FB1
			public virtual TimeSpan SubjectCounterSlidingWindowBucketLength { get; private set; }

			// Token: 0x170009E1 RID: 2529
			// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x00079DBA File Offset: 0x00077FBA
			// (set) Token: 0x06001FA9 RID: 8105 RVA: 0x00079DC2 File Offset: 0x00077FC2
			public virtual int MaxSubjectCountPerSenderRecipient { get; private set; }

			// Token: 0x170009E2 RID: 2530
			// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00079DCB File Offset: 0x00077FCB
			// (set) Token: 0x06001FAB RID: 8107 RVA: 0x00079DD3 File Offset: 0x00077FD3
			public virtual int SenderRecipientPartialSubjectThrottlingThreshold { get; private set; }

			// Token: 0x170009E3 RID: 2531
			// (get) Token: 0x06001FAC RID: 8108 RVA: 0x00079DDC File Offset: 0x00077FDC
			public TimeSpan TrackSummaryLoggingInterval
			{
				get
				{
					return Components.TransportAppConfig.flowControlLogConfig.SummaryLoggingInterval;
				}
			}

			// Token: 0x170009E4 RID: 2532
			// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00079DED File Offset: 0x00077FED
			public TimeSpan TrackSummaryBucketLength
			{
				get
				{
					return Components.TransportAppConfig.flowControlLogConfig.SummaryBucketLength;
				}
			}

			// Token: 0x170009E5 RID: 2533
			// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00079DFE File Offset: 0x00077FFE
			public int MaxSummaryLinesLogged
			{
				get
				{
					return Components.TransportAppConfig.flowControlLogConfig.MaxSummaryLinesLogged;
				}
			}

			// Token: 0x06001FAF RID: 8111 RVA: 0x00079E10 File Offset: 0x00078010
			public static TransportAppConfig.SmtpMessageThrottlingAgentConfig Load()
			{
				TransportAppConfig.SmtpMessageThrottlingAgentConfig smtpMessageThrottlingAgentConfig = new TransportAppConfig.SmtpMessageThrottlingAgentConfig();
				smtpMessageThrottlingAgentConfig.Enabled = TransportAppConfig.GetConfigBool("SenderRecipientThrottlingEnabled", true);
				smtpMessageThrottlingAgentConfig.LoggingModeEnabled = TransportAppConfig.GetConfigBool("SenderRecipientThrottlingLoggingModeEnabled", false);
				smtpMessageThrottlingAgentConfig.InitialSenderRecipientRateTrackingThreshold = TransportAppConfig.GetConfigInt("SenderRecipientThrottlingInitialRateTrackingThreshold", 1, int.MaxValue, 200);
				smtpMessageThrottlingAgentConfig.WhitelistEnabled = TransportAppConfig.GetConfigBool("WhitelistThrottlingEnabled", true);
				smtpMessageThrottlingAgentConfig.EhaSenderToEhaRecipientThreshold = TransportAppConfig.GetConfigInt("SmtpThrottlingAgentEhaSenderToEhaRecipientRatePerHour", 0, int.MaxValue, 300000);
				smtpMessageThrottlingAgentConfig.SubjectThrottlingEnabled = TransportAppConfig.GetConfigBool("SenderRecipientSubjectThrottlingEnabled", false);
				smtpMessageThrottlingAgentConfig.SenderRecipientPartialSubjectThrottlingThreshold = TransportAppConfig.GetConfigInt("SmtpThrottlingAgentSenderRecipientPartialSubjectThrottlingThreshold", 0, int.MaxValue, 3000);
				smtpMessageThrottlingAgentConfig.SenderRecipientSubjectThreshold = TransportAppConfig.GetConfigInt("SmtpThrottlingAgentSenderRecipientSubjectThreshold", 0, int.MaxValue, 3000);
				smtpMessageThrottlingAgentConfig.SubjectTrackingRemoveThreshold = TransportAppConfig.GetConfigInt("SmtpThrottlingAgentSubjectTrackingRemoveThreshold", 0, int.MaxValue, 100);
				smtpMessageThrottlingAgentConfig.MaxSubjectCountPerSenderRecipient = TransportAppConfig.GetConfigInt("SmtpThrottlingAgentMaxSubjectCountPerSenderRecipient", 0, 5000, 5);
				smtpMessageThrottlingAgentConfig.SubjectCounterSlidingWindow = TransportAppConfig.GetConfigTimeSpan("SmtpThrottlingAgentSubjectCounterSlidingWindow", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(30.0));
				smtpMessageThrottlingAgentConfig.SubjectCounterSlidingWindowBucketLength = TransportAppConfig.GetConfigTimeSpan("SmtpThrottlingAgentSubjectCounterSlidingWindowBucketLength", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(3.0));
				smtpMessageThrottlingAgentConfig.InitialRateTrackingInterval = TransportAppConfig.GetConfigTimeSpan("SenderRecipientThrottlingInitialRateTrackingInterval", TimeSpan.FromSeconds(10.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(5.0));
				smtpMessageThrottlingAgentConfig.SenderRateTrackingInterval = TransportAppConfig.GetConfigTimeSpan("SenderRecipientThrottlingRateTrackingInterval", TimeSpan.FromSeconds(20.0), TimeSpan.FromMinutes(180.0), TimeSpan.FromMinutes(60.0));
				smtpMessageThrottlingAgentConfig.LowWatermarkThreshold = TransportAppConfig.GetConfigInt("SenderRecipientThrottlingLowWatermarkThreshold", 0, int.MaxValue, smtpMessageThrottlingAgentConfig.InitialSenderRecipientRateTrackingThreshold);
				smtpMessageThrottlingAgentConfig.ExpirationProcessingInterval = TransportAppConfig.GetConfigTimeSpan("SenderRecipientThrottlingExpirationProcessingInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(5.0));
				return smtpMessageThrottlingAgentConfig;
			}

			// Token: 0x04001093 RID: 4243
			private int senderRecipientThreshold;
		}

		// Token: 0x020002CD RID: 717
		public sealed class StateManagementConfig
		{
			// Token: 0x06001FB0 RID: 8112 RVA: 0x0007A02F File Offset: 0x0007822F
			private StateManagementConfig()
			{
			}

			// Token: 0x170009E6 RID: 2534
			// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0007A037 File Offset: 0x00078237
			// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x0007A03F File Offset: 0x0007823F
			public bool StateChangeDetectionEnabled { get; private set; }

			// Token: 0x06001FB3 RID: 8115 RVA: 0x0007A048 File Offset: 0x00078248
			public static TransportAppConfig.StateManagementConfig Load()
			{
				return new TransportAppConfig.StateManagementConfig
				{
					StateChangeDetectionEnabled = TransportAppConfig.GetConfigBool("StateManagementStateChangeDetectionEnabled", true)
				};
			}

			// Token: 0x040010A4 RID: 4260
			private const string StateChangeDetectionEnabledLabel = "StateManagementStateChangeDetectionEnabled";
		}

		// Token: 0x020002CE RID: 718
		public sealed class ADPollingConfig
		{
			// Token: 0x06001FB4 RID: 8116 RVA: 0x0007A06D File Offset: 0x0007826D
			private ADPollingConfig()
			{
			}

			// Token: 0x06001FB5 RID: 8117 RVA: 0x0007A078 File Offset: 0x00078278
			public static TransportAppConfig.ADPollingConfig Load()
			{
				return new TransportAppConfig.ADPollingConfig
				{
					InterceptorRulesReloadInterval = TransportAppConfig.GetConfigTimeSpan("InterceptorRulesReloadInterval", TransportAppConfig.ADPollingConfig.minAllowedReloadInterval, TransportAppConfig.ADPollingConfig.maxAllowedReloadInterval, TimeSpan.FromMinutes(5.0)),
					TransportLogSearchServiceReloadInterval = TransportAppConfig.GetConfigTimeSpan("TransportLogSearchServiceReloadInterval", TransportAppConfig.ADPollingConfig.minAllowedReloadInterval, TransportAppConfig.ADPollingConfig.maxAllowedReloadInterval, TimeSpan.FromHours(6.0)),
					ConfigurationComponentReloadInterval = TransportAppConfig.GetConfigTimeSpan("ConfigurationComponentReloadInterval", TransportAppConfig.ADPollingConfig.minAllowedReloadInterval, TransportAppConfig.ADPollingConfig.maxAllowedReloadInterval, TimeSpan.FromMinutes(5.0)),
					DsnMessageCustomizationReloadInterval = TransportAppConfig.GetConfigTimeSpan("DsnMessageCustomizationReloadInterval", TransportAppConfig.ADPollingConfig.minAllowedReloadInterval, TransportAppConfig.ADPollingConfig.maxAllowedReloadInterval, TimeSpan.FromHours(6.0))
				};
			}

			// Token: 0x170009E7 RID: 2535
			// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0007A12C File Offset: 0x0007832C
			// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x0007A134 File Offset: 0x00078334
			public TimeSpan InterceptorRulesReloadInterval { get; private set; }

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0007A13D File Offset: 0x0007833D
			// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x0007A145 File Offset: 0x00078345
			public TimeSpan TransportLogSearchServiceReloadInterval { get; private set; }

			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0007A14E File Offset: 0x0007834E
			// (set) Token: 0x06001FBB RID: 8123 RVA: 0x0007A156 File Offset: 0x00078356
			public TimeSpan ConfigurationComponentReloadInterval { get; private set; }

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0007A15F File Offset: 0x0007835F
			// (set) Token: 0x06001FBD RID: 8125 RVA: 0x0007A167 File Offset: 0x00078367
			public TimeSpan DsnMessageCustomizationReloadInterval { get; private set; }

			// Token: 0x040010A6 RID: 4262
			private static readonly TimeSpan minAllowedReloadInterval = TimeSpan.FromSeconds(5.0);

			// Token: 0x040010A7 RID: 4263
			private static readonly TimeSpan maxAllowedReloadInterval = TimeSpan.FromDays(1.0);
		}
	}
}
