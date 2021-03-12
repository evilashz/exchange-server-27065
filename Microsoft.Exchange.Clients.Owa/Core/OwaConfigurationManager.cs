using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200017C RID: 380
	public sealed class OwaConfigurationManager
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x000598FD File Offset: 0x00057AFD
		private OwaConfigurationManager()
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00059926 File Offset: 0x00057B26
		internal static void ShutDownConfigurationManager()
		{
			if (OwaConfigurationManager.instance != null)
			{
				OwaConfigurationManager.instance.UnregisterConfigurationChangeNotification();
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0005993C File Offset: 0x00057B3C
		public static Configuration Configuration
		{
			get
			{
				if (OwaConfigurationManager.instance == null)
				{
					return null;
				}
				Guid requestVdirObjectGuid = OwaConfigurationManager.GetRequestVdirObjectGuid();
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UseBackendVdirConfiguration.Enabled || !Globals.IsInitialized || requestVdirObjectGuid == Guid.Empty)
				{
					return OwaConfigurationManager.instance.configuration;
				}
				Configuration result;
				lock (OwaConfigurationManager.instance.syncRoot)
				{
					if (OwaConfigurationManager.instance.configurations == null)
					{
						OwaConfigurationManager.instance.configurations = new Dictionary<Guid, Configuration>();
					}
					OwaConfigurationManager.instance.RemoveEntriesIfExpired();
					Configuration configuration;
					if (OwaConfigurationManager.instance.configurations.TryGetValue(requestVdirObjectGuid, out configuration))
					{
						result = configuration;
					}
					else
					{
						try
						{
							configuration = OwaConfigurationManager.LoadConfiguration(requestVdirObjectGuid);
							if (configuration != null)
							{
								OwaConfigurationManager.instance.configurations.Add(requestVdirObjectGuid, configuration);
								if (OwaConfigurationManager.instance.minExpirationTime > configuration.ExpirationTime)
								{
									OwaConfigurationManager.instance.minExpirationTime = configuration.ExpirationTime;
								}
							}
							result = configuration;
						}
						catch (OwaADObjectNotFoundException ex)
						{
							OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_GenericConfigurationUpdateError, "Configuration", ex.Message);
							throw new OwaInvalidRequestException("Configuration", ex);
						}
					}
				}
				return result;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00059A84 File Offset: 0x00057C84
		internal static void CreateAndLoadConfigurationManager()
		{
			OwaConfigurationManager owaConfigurationManager = new OwaConfigurationManager();
			if (Globals.OwaVDirType == OWAVDirType.OWA)
			{
				try
				{
					if (owaConfigurationManager == null)
					{
						throw new OwaInvalidConfigurationException(LocalizedStrings.GetNonEncoded(940832921));
					}
					OwaConfigurationManager.InitializeConfigurationManager();
					OwaConfigurationManager.instance = owaConfigurationManager;
					OwaConfigurationManager.LoadConfiguration();
					if (Globals.ListenAdNotifications)
					{
						OwaConfigurationManager.instance.RegisterConfigurationChangeNotification();
					}
					return;
				}
				catch (Exception ex)
				{
					throw new OwaInvalidConfigurationException(ex.Message, ex);
				}
			}
			OwaConfigurationManager.instance = owaConfigurationManager;
			owaConfigurationManager.configuration = new CalendarVDirConfiguration();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00059B04 File Offset: 0x00057D04
		private static void InitializeConfigurationManager()
		{
			try
			{
				OwaConfigurationManager.session = Utilities.CreateADSystemConfigurationSessionScopedToFirstOrg(false, ConsistencyMode.FullyConsistent);
				Server server = OwaConfigurationManager.session.FindLocalServer();
				OwaConfigurationManager.isPhoneticSupportEnabled = server.IsPhoneticSupportEnabled;
				OwaConfigurationManager.virtualDirectoryName = HttpRuntime.AppDomainAppVirtualPath.Replace("'", "''");
				if (OwaConfigurationManager.virtualDirectoryName[0] == '/')
				{
					OwaConfigurationManager.virtualDirectoryName = OwaConfigurationManager.virtualDirectoryName.Substring(1);
				}
				OwaConfigurationManager.webSiteName = HttpRuntime.AppDomainAppId;
				if (OwaConfigurationManager.webSiteName[0] == '/')
				{
					OwaConfigurationManager.webSiteName = OwaConfigurationManager.webSiteName.Substring(1);
				}
				int num = OwaConfigurationManager.webSiteName.IndexOf('/');
				OwaConfigurationManager.webSiteName = OwaConfigurationManager.webSiteName.Substring(num);
				OwaConfigurationManager.webSiteName = string.Format("IIS://{0}{1}", server.Fqdn, OwaConfigurationManager.webSiteName);
				num = OwaConfigurationManager.webSiteName.LastIndexOf('/');
				OwaConfigurationManager.webSiteName = IisUtility.GetWebSiteName(OwaConfigurationManager.webSiteName.Substring(0, num));
				OwaConfigurationManager.virtualDirectoryDN = new ADObjectId(server.DistinguishedName).GetDescendantId("Protocols", "HTTP", new string[]
				{
					string.Format("{0} ({1})", OwaConfigurationManager.virtualDirectoryName, OwaConfigurationManager.webSiteName)
				});
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ConfigurationManagerTracer.TraceDebug<string>(0L, "Unable to retrieve the AD session or the server object. {0}", ex.Message);
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ADSystemConfigurationSession, string.Empty, new object[]
				{
					ex.Message
				});
				throw;
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00059C8C File Offset: 0x00057E8C
		private static void ConfigurationUpdate(ADNotificationEventArgs args)
		{
			ExTraceGlobals.ConfigurationManagerTracer.TraceDebug(0L, "OwaConfigurationManager.ConfigurationUpdate");
			OwaConfigurationManager.LoadConfiguration();
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00059CA4 File Offset: 0x00057EA4
		private static Guid GetRequestVdirObjectGuid()
		{
			Guid result;
			try
			{
				string text = HttpContext.Current.Request.Headers["X-vDirObjectId"];
				if (string.IsNullOrEmpty(text))
				{
					result = Guid.Empty;
				}
				else
				{
					result = new Guid(text);
				}
			}
			catch (Exception ex)
			{
				OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_GenericConfigurationUpdateError, "GetRequestVdirObjectGuid", ex.Message);
				throw new OwaInvalidRequestException("GetRequestVdirObjectGuid", ex);
			}
			return result;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00059D1C File Offset: 0x00057F1C
		private void LogConfigurationException(ExEventLog.EventTuple eventLogConstant, string traceMessage, string exceptionMessage)
		{
			ExTraceGlobals.ConfigurationManagerTracer.TraceDebug<string>(0L, traceMessage, exceptionMessage);
			OwaDiagnostics.LogEvent(eventLogConstant, string.Empty, new object[]
			{
				OwaConfigurationManager.virtualDirectoryName,
				OwaConfigurationManager.webSiteName,
				exceptionMessage
			});
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00059D5F File Offset: 0x00057F5F
		private bool GotSettings()
		{
			return OwaConfigurationManager.instance.configuration != null;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00059D70 File Offset: 0x00057F70
		internal static void LoadConfiguration()
		{
			bool flag = true;
			Configuration value = null;
			try
			{
				value = new Configuration(OwaConfigurationManager.session, OwaConfigurationManager.virtualDirectoryName, OwaConfigurationManager.webSiteName, OwaConfigurationManager.virtualDirectoryDN, OwaConfigurationManager.isPhoneticSupportEnabled);
			}
			catch (Exception ex)
			{
				string traceMessage = "Unable to read the OWA settings from the AD. {0}";
				if (ex is OwaInvalidConfigurationException)
				{
					OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_FailedToUpdateConfigurationSettings, traceMessage, ex.Message);
				}
				else if (ex is ADTransientException)
				{
					OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_ReadSettingsFromAD, traceMessage, ex.Message);
				}
				else
				{
					OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_GenericConfigurationUpdateError, traceMessage, ex.Message);
				}
				if (!OwaConfigurationManager.instance.GotSettings())
				{
					throw;
				}
				flag = false;
			}
			if (flag)
			{
				Interlocked.Exchange<Configuration>(ref OwaConfigurationManager.instance.configuration, value);
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00059E38 File Offset: 0x00058038
		internal static Configuration LoadConfiguration(Guid vDirADObjectGuid)
		{
			Configuration result;
			try
			{
				ADObjectId vDirADObjectId = new ADObjectId(vDirADObjectGuid);
				Configuration configuration = new Configuration(OwaConfigurationManager.session, "X-vDirObjectId", vDirADObjectGuid.ToString(), vDirADObjectId, OwaConfigurationManager.isPhoneticSupportEnabled);
				result = configuration;
			}
			catch (Exception ex)
			{
				string traceMessage = "Unable to read the OWA settings from the AD. {0}";
				if (ex is OwaInvalidConfigurationException)
				{
					OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_FailedToUpdateConfigurationSettings, traceMessage, ex.Message);
				}
				else if (ex is ADTransientException)
				{
					OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_ReadSettingsFromAD, traceMessage, ex.Message);
				}
				else
				{
					OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_GenericConfigurationUpdateError, traceMessage, ex.Message);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00059EE8 File Offset: 0x000580E8
		private void RegisterConfigurationChangeNotification()
		{
			ADNotificationCallback callback = new ADNotificationCallback(OwaConfigurationManager.ConfigurationUpdate);
			try
			{
				this.owaCookie = ADNotificationAdapter.RegisterChangeNotification<ADOwaVirtualDirectory>(OwaConfigurationManager.virtualDirectoryDN ?? OwaConfigurationManager.session.GetOrgContainerId(), callback, null);
				if (this.configuration.IsPublicFoldersEnabledOnThisVdir && !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.PublicFolderTreePerTenanant.Enabled)
				{
					this.publicFolderTreeCookie = ADNotificationAdapter.RegisterChangeNotification<PublicFolderTree>(OwaConfigurationManager.session.GetOrgContainerId(), callback, null);
					this.publicFolderDatabaseCookie = ADNotificationAdapter.RegisterChangeNotification<PublicFolderDatabase>(OwaConfigurationManager.session.GetOrgContainerId(), callback, null);
				}
			}
			catch (Exception ex)
			{
				OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_ADNotificationsRegistration, "Unable to register a change notification listener for AD Owa Virtual Directory. {0}", ex.Message);
				throw;
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00059FAC File Offset: 0x000581AC
		private void UnregisterConfigurationChangeNotification()
		{
			if (this.owaCookie == null)
			{
				if (this.publicFolderTreeCookie == null)
				{
					return;
				}
			}
			try
			{
				Utilities.CreateADSystemConfigurationSessionScopedToFirstOrg(true, ConsistencyMode.IgnoreInvalid);
				if (this.owaCookie != null)
				{
					ADNotificationAdapter.UnregisterChangeNotification(this.owaCookie);
				}
				if (this.publicFolderTreeCookie != null)
				{
					ADNotificationAdapter.UnregisterChangeNotification(this.publicFolderTreeCookie);
				}
				if (this.publicFolderDatabaseCookie != null)
				{
					ADNotificationAdapter.UnregisterChangeNotification(this.publicFolderDatabaseCookie);
				}
			}
			catch (Exception ex)
			{
				OwaConfigurationManager.instance.LogConfigurationException(ClientsEventLogConstants.Tuple_UnregisterADNotifications, "Unable to unregister from change notifications. {0}", ex.Message);
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0005A03C File Offset: 0x0005823C
		private void RemoveEntriesIfExpired()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (OwaConfigurationManager.instance.minExpirationTime < utcNow)
			{
				OwaConfigurationManager.instance.minExpirationTime = DateTime.MaxValue;
				foreach (KeyValuePair<Guid, Configuration> keyValuePair in OwaConfigurationManager.instance.configurations)
				{
					if (keyValuePair.Value.ExpirationTime < utcNow)
					{
						OwaConfigurationManager.instance.expiredConfigurations.Add(keyValuePair.Key);
					}
					else if (keyValuePair.Value.ExpirationTime < OwaConfigurationManager.instance.minExpirationTime)
					{
						OwaConfigurationManager.instance.minExpirationTime = keyValuePair.Value.ExpirationTime;
					}
				}
				foreach (Guid key in OwaConfigurationManager.instance.expiredConfigurations)
				{
					OwaConfigurationManager.instance.configurations.Remove(key);
				}
				OwaConfigurationManager.instance.expiredConfigurations.Clear();
			}
		}

		// Token: 0x04000935 RID: 2357
		private const string VDirObjectIdHeaderName = "X-vDirObjectId";

		// Token: 0x04000936 RID: 2358
		private static ITopologyConfigurationSession session;

		// Token: 0x04000937 RID: 2359
		private static ADObjectId virtualDirectoryDN;

		// Token: 0x04000938 RID: 2360
		private static string virtualDirectoryName;

		// Token: 0x04000939 RID: 2361
		private static string webSiteName;

		// Token: 0x0400093A RID: 2362
		private static bool isPhoneticSupportEnabled;

		// Token: 0x0400093B RID: 2363
		private static OwaConfigurationManager instance;

		// Token: 0x0400093C RID: 2364
		private List<Guid> expiredConfigurations = new List<Guid>();

		// Token: 0x0400093D RID: 2365
		private DateTime minExpirationTime = DateTime.MinValue;

		// Token: 0x0400093E RID: 2366
		private Configuration configuration;

		// Token: 0x0400093F RID: 2367
		private Dictionary<Guid, Configuration> configurations;

		// Token: 0x04000940 RID: 2368
		private object syncRoot = new object();

		// Token: 0x04000941 RID: 2369
		private ADNotificationRequestCookie owaCookie;

		// Token: 0x04000942 RID: 2370
		private ADNotificationRequestCookie publicFolderTreeCookie;

		// Token: 0x04000943 RID: 2371
		private ADNotificationRequestCookie publicFolderDatabaseCookie;
	}
}
