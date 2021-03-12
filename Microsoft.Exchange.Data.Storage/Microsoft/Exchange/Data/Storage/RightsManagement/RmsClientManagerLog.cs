using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B4B RID: 2891
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RmsClientManagerLog
	{
		// Token: 0x060068CD RID: 26829 RVA: 0x001C0208 File Offset: 0x001BE408
		public static void Start()
		{
			Server localServer = null;
			RmsClientManagerLog.rmsLogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Rms Client Manager Log", RmsClientManagerLog.Fields);
			RmsClientManagerLog.rmsLog = new Log(RmsClientManagerUtils.GetUniqueFileNameForProcess(RmsClientManagerLog.LogSuffix, true), new LogHeaderFormatter(RmsClientManagerLog.rmsLogSchema), "RmsClientManagerLog");
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 254, "Start", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\rightsmanagement\\RmsClientManagerLog.cs");
				localServer = topologyConfigurationSession.FindLocalServer();
				RmsClientManagerLog.notificationCookie = ADNotificationAdapter.RegisterChangeNotification<Server>(localServer.Id, new ADNotificationCallback(RmsClientManagerLog.HandleConfigurationChange));
			});
			if (!adoperationResult.Succeeded)
			{
				RmsClientManagerLog.Tracer.TraceError<Exception>(0L, "Failed to get the local server.  Unable to load the log configuration. Error {0}", adoperationResult.Exception);
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, adoperationResult.Exception);
			}
			RmsClientManagerLog.Configure(localServer);
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x001C02C0 File Offset: 0x001BE4C0
		public static void Stop()
		{
			if (RmsClientManagerLog.notificationCookie != null)
			{
				try
				{
					ADNotificationAdapter.UnregisterChangeNotification(RmsClientManagerLog.notificationCookie);
					if (RmsClientManagerLog.rmsLog != null)
					{
						RmsClientManagerLog.rmsLog.Close();
					}
				}
				catch (ObjectDisposedException)
				{
				}
				RmsClientManagerLog.notificationCookie = null;
			}
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x001C030C File Offset: 0x001BE50C
		public static void Configure(Server serverConfig)
		{
			if (serverConfig == null)
			{
				return;
			}
			RmsClientManagerLog.rmsLogEnabled = serverConfig.IrmLogEnabled;
			if (RmsClientManagerLog.rmsLogEnabled)
			{
				if (serverConfig.IrmLogPath == null || string.IsNullOrEmpty(serverConfig.IrmLogPath.PathName))
				{
					RmsClientManagerLog.rmsLogEnabled = false;
					RmsClientManagerLog.Tracer.TraceError(0L, "Rms Client Manager Log is enabled, but the log path is empty.");
					return;
				}
				if (RmsClientManagerLog.rmsLog != null)
				{
					RmsClientManagerLog.rmsLog.Configure(serverConfig.IrmLogPath.PathName, serverConfig.IrmLogMaxAge, (long)(serverConfig.IrmLogMaxDirectorySize.IsUnlimited ? 0UL : serverConfig.IrmLogMaxDirectorySize.Value.ToBytes()), (long)serverConfig.IrmLogMaxFileSize.ToBytes());
				}
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x001C03C9 File Offset: 0x001BE5C9
		public static void LogUriEvent(RmsClientManagerLog.RmsClientManagerFeature clientManagerFeature, RmsClientManagerLog.RmsClientManagerEvent clientManagerEvent, RmsClientManagerContext context, Uri serverUrl)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(clientManagerFeature, clientManagerEvent, serverUrl, context.OrgId, context.TransactionId, context.ContextStringForm);
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x001C03FC File Offset: 0x001BE5FC
		public static void LogAcquireRmsTemplateResult(RmsClientManagerContext context, RmsTemplate template)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.Template, RmsClientManagerLog.RmsClientManagerEvent.Success, context.OrgId, context.TransactionId, template.Id.ToString(), context.ContextStringForm);
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x001C044C File Offset: 0x001BE64C
		public static void LogAcquireRmsTemplateResult(RmsClientManagerContext context, Uri serverUrl, LinkedList<RmsTemplate> templates)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (RmsTemplate rmsTemplate in templates)
			{
				if (!flag)
				{
					stringBuilder.AppendFormat("; {0}", rmsTemplate.Id.ToString());
				}
				else
				{
					stringBuilder.Append(rmsTemplate.Id.ToString());
					flag = false;
				}
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.Template, RmsClientManagerLog.RmsClientManagerEvent.Success, serverUrl, context.OrgId, context.TransactionId, stringBuilder.ToString(), context.ContextStringForm);
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x001C0518 File Offset: 0x001BE718
		public static void LogAcquireRmsTemplateCacheResult(RmsClientManagerContext context, Guid key)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.Template, RmsClientManagerLog.RmsClientManagerEvent.Success, RmsClientManagerLog.DummyUriForTemplateCache, context.OrgId, context.TransactionId, key.ToString(), context.ContextStringForm);
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x001C0568 File Offset: 0x001BE768
		public static void LogAcquireServerInfoResult(RmsClientManagerContext context, ExternalRMSServerInfo serverInfo)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (serverInfo == null)
			{
				RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.ServerInfo, RmsClientManagerLog.RmsClientManagerEvent.Success, context.OrgId, context.TransactionId, RmsClientManagerLog.ServerInfoNotFound, context.ContextStringForm);
				return;
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.ServerInfo, RmsClientManagerLog.RmsClientManagerEvent.Success, context.OrgId, context.TransactionId, string.Format(CultureInfo.InvariantCulture, "KeyUri: {0}; CertificationWSPipeline: {1}; CertificationWSTargetUri: {2}; ServerLicensingWSPipeline: {3}; ServerLicensingWSTargetUri: {4}; ExpiryTime: {5}", new object[]
			{
				(serverInfo.KeyUri == null) ? RmsClientManagerLog.ServerInfoUriNull : serverInfo.KeyUri.ToString(),
				(serverInfo.CertificationWSPipeline == null) ? RmsClientManagerLog.ServerInfoUriNull : serverInfo.CertificationWSPipeline.ToString(),
				(serverInfo.CertificationWSTargetUri == null) ? RmsClientManagerLog.ServerInfoUriNull : serverInfo.CertificationWSTargetUri.ToString(),
				(serverInfo.ServerLicensingWSPipeline == null) ? RmsClientManagerLog.ServerInfoUriNull : serverInfo.ServerLicensingWSPipeline.ToString(),
				(serverInfo.ServerLicensingWSTargetUri == null) ? RmsClientManagerLog.ServerInfoUriNull : serverInfo.ServerLicensingWSTargetUri.ToString(),
				serverInfo.ExpiryTime.Ticks.ToString()
			}), context.ContextStringForm);
		}

		// Token: 0x060068D5 RID: 26837 RVA: 0x001C06A5 File Offset: 0x001BE8A5
		public static void LogAcquireUseLicense(RmsClientManagerContext context, Uri serverUrl, string user)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.UseLicense, RmsClientManagerLog.RmsClientManagerEvent.Acquire, serverUrl, context.OrgId, context.TransactionId, user, context.ContextStringForm);
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x001C06D8 File Offset: 0x001BE8D8
		public static void LogAcquirePrelicense(RmsClientManagerContext context, Uri serverUrl, string[] identities)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			foreach (string data in identities)
			{
				RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.Prelicense, RmsClientManagerLog.RmsClientManagerEvent.Acquire, serverUrl, context.OrgId, context.TransactionId, data, context.ContextStringForm);
			}
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x001C072C File Offset: 0x001BE92C
		public static void LogAcquirePrelicenseResult(RmsClientManagerContext context, LicenseResponse[] responses)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (responses == null || responses.Length == 0)
			{
				RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.Prelicense, RmsClientManagerLog.RmsClientManagerEvent.Success, context.TransactionId, RmsClientManagerLog.PrelicenseNoResult, context.ContextStringForm);
				return;
			}
			foreach (LicenseResponse licenseResponse in responses)
			{
				if (licenseResponse.Exception != null)
				{
					RmsClientManagerLog.LogException(RmsClientManagerLog.RmsClientManagerFeature.Prelicense, context, licenseResponse.Exception);
				}
				else
				{
					RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.Prelicense, RmsClientManagerLog.RmsClientManagerEvent.Success, context.TransactionId, licenseResponse.UsageRights.ToString(), context.ContextStringForm);
				}
			}
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x001C07C2 File Offset: 0x001BE9C2
		public static void LogAcquireUseLicenseResult(RmsClientManagerContext context, string useLicense)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.UseLicense, RmsClientManagerLog.RmsClientManagerEvent.Success, context.TransactionId, string.IsNullOrEmpty(useLicense) ? RmsClientManagerLog.UseLicenseEmpty : RmsClientManagerLog.UseLicenseExists, context.ContextStringForm);
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x001C0801 File Offset: 0x001BEA01
		public static void LogAcquireRac(RmsClientManagerContext context, Uri serverUrl)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.RacClc, RmsClientManagerLog.RmsClientManagerEvent.Acquire, serverUrl, context.OrgId, context.TransactionId, RmsClientManagerLog.AcquireServerRac, context.ContextStringForm);
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x001C0838 File Offset: 0x001BEA38
		public static void LogAcquireClc(RmsClientManagerContext context, Uri serverUrl)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.RacClc, RmsClientManagerLog.RmsClientManagerEvent.Acquire, serverUrl, context.OrgId, context.TransactionId, RmsClientManagerLog.AcquireServerClc);
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x001C0869 File Offset: 0x001BEA69
		public static void LogAcquireRacClc(RmsClientManagerContext context)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.RacClc, RmsClientManagerLog.RmsClientManagerEvent.Acquire, context.OrgId, context.TransactionId, RmsClientManagerLog.AcquireServerRacClc, context.ContextStringForm);
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x001C08A0 File Offset: 0x001BEAA0
		public static void LogAcquireRacClcResult(RmsClientManagerContext context, TenantLicensePair tenantLicensePair)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.RacClc, RmsClientManagerLog.RmsClientManagerEvent.Success, context.OrgId, context.TransactionId, string.Format(CultureInfo.InvariantCulture, "CLC: {0}; RAC: {1}; Version: {2}", new object[]
			{
				(tenantLicensePair.BoundLicenseClc == null) ? RmsClientManagerLog.LicenseEmpty : RmsClientManagerLog.LicenseExists,
				(tenantLicensePair.Rac == null) ? RmsClientManagerLog.LicenseEmpty : RmsClientManagerLog.LicenseExists,
				tenantLicensePair.Version
			}).ToString(NumberFormatInfo.InvariantInfo), context.ContextStringForm);
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x001C093C File Offset: 0x001BEB3C
		public static void LogAcquireRacClcCacheResult(RmsClientManagerContext context, Uri serviceLocation, Uri publishingLocation, byte version)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.RacClc, RmsClientManagerLog.RmsClientManagerEvent.Success, RmsClientManagerLog.DummyUriForRacClcCache, context.OrgId, context.TransactionId, string.Format(CultureInfo.InvariantCulture, "Service Location: {0}; Publishing Location: {1}; Version: {2}", new object[]
			{
				serviceLocation.ToString(),
				publishingLocation.ToString(),
				version.ToString(NumberFormatInfo.InvariantInfo)
			}), context.ContextStringForm);
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x001C09B5 File Offset: 0x001BEBB5
		public static void LogVerifySignatureResult(RmsClientManagerContext context, string userIdentity)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.SignatureVerification, RmsClientManagerLog.RmsClientManagerEvent.Success, context.OrgId, context.TransactionId, userIdentity, context.ContextStringForm);
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x001C09E8 File Offset: 0x001BEBE8
		public static void LogDrmInitialization(int machineCertIndex)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			RmsClientManagerContext rmsClientManagerContext = new RmsClientManagerContext(OrganizationId.ForestWideOrgId, null);
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.DrmInitialization, RmsClientManagerLog.RmsClientManagerEvent.Success, default(Guid), string.Format(CultureInfo.InvariantCulture, "Selected machine certificate index: {0}", new object[]
			{
				machineCertIndex.ToString(NumberFormatInfo.InvariantInfo)
			}), rmsClientManagerContext.ContextStringForm);
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x001C0A48 File Offset: 0x001BEC48
		public static void LogDrmInitialization(Uri rmsServerUri)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (rmsServerUri == null)
			{
				throw new ArgumentNullException("rmsServerUri");
			}
			RmsClientManagerContext rmsClientManagerContext = new RmsClientManagerContext(OrganizationId.ForestWideOrgId, null);
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.DrmInitialization, RmsClientManagerLog.RmsClientManagerEvent.Success, default(Guid), string.Format(CultureInfo.InvariantCulture, "RMS Server queried for active crypto mode: {0}", new object[]
			{
				rmsServerUri.ToString()
			}), rmsClientManagerContext.ContextStringForm);
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x001C0AB4 File Offset: 0x001BECB4
		public static void LogDrmInitialization(DRMClientVersionInfo msdrmVersionInfo)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (msdrmVersionInfo == null)
			{
				throw new ArgumentNullException("msdrmVersionInfo");
			}
			RmsClientManagerContext rmsClientManagerContext = new RmsClientManagerContext(OrganizationId.ForestWideOrgId, null);
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.DrmInitialization, RmsClientManagerLog.RmsClientManagerEvent.Success, default(Guid), string.Format(CultureInfo.InvariantCulture, "MSDRM.DLL version: {0}.{1}.{2}.{3}", new object[]
			{
				msdrmVersionInfo.V1.ToString(),
				msdrmVersionInfo.V2.ToString(),
				msdrmVersionInfo.V3.ToString(),
				msdrmVersionInfo.V4.ToString()
			}), rmsClientManagerContext.ContextStringForm);
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x001C0B48 File Offset: 0x001BED48
		public static void LogUrlMalFormatException(UriFormatException ex, string key, string originalUri)
		{
			StringBuilder stringBuilder = null;
			if (ex != null)
			{
				stringBuilder = new StringBuilder(ex.Message.Length + ex.GetType().Name.Length + 3);
				stringBuilder.AppendFormat("{0} [{1}]", ex.Message, ex.GetType().Name);
				if (ex.InnerException != null)
				{
					stringBuilder.AppendFormat("; Inner Exception: {0} [{1}]", ex.InnerException.Message, ex.InnerException.GetType().Name);
				}
			}
			string data = string.Format("RMSO connector Url is a malformat url, regkey value: {0}, originalUri: {1}", key, originalUri);
			RmsClientManagerLog.LogEvent(RmsClientManagerLog.RmsClientManagerFeature.ServerInfo, RmsClientManagerLog.RmsClientManagerEvent.Exception, default(Guid), data, stringBuilder.ToString());
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x001C0BF0 File Offset: 0x001BEDF0
		public static void LogException(RmsClientManagerLog.RmsClientManagerFeature clientManagerFeature, RmsClientManagerContext context, Exception ex)
		{
			if (!RmsClientManagerLog.rmsLogEnabled)
			{
				return;
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			StringBuilder stringBuilder = null;
			if (ex != null)
			{
				stringBuilder = new StringBuilder(ex.Message.Length + ex.GetType().Name.Length + 3);
				stringBuilder.AppendFormat("{0} [{1}]", ex.Message, ex.GetType().Name);
				if (ex.InnerException != null)
				{
					stringBuilder.AppendFormat("; Inner Exception: {0} [{1}]", ex.InnerException.Message, ex.InnerException.GetType().Name);
				}
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(RmsClientManagerLog.rmsLogSchema);
			logRowFormatter[1] = clientManagerFeature;
			logRowFormatter[2] = RmsClientManagerLog.RmsClientManagerEvent.Exception;
			Guid transactionId = context.TransactionId;
			logRowFormatter[7] = context.TransactionId.ToString();
			logRowFormatter[6] = context.ContextStringForm;
			if (stringBuilder != null)
			{
				logRowFormatter[5] = stringBuilder.ToString();
			}
			RmsClientManagerLog.Append(logRowFormatter);
		}

		// Token: 0x060068E4 RID: 26852 RVA: 0x001C0CF4 File Offset: 0x001BEEF4
		private static void LogEvent(RmsClientManagerLog.RmsClientManagerFeature clientManagerFeature, RmsClientManagerLog.RmsClientManagerEvent clientManagerEvent, Uri serverUrl, OrganizationId orgId, Guid transaction, string data, string context)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(RmsClientManagerLog.rmsLogSchema);
			logRowFormatter[1] = clientManagerFeature;
			logRowFormatter[2] = clientManagerEvent;
			if (serverUrl != null)
			{
				logRowFormatter[4] = serverUrl.ToString();
			}
			if (orgId != null)
			{
				logRowFormatter[3] = orgId.ToString();
			}
			logRowFormatter[7] = transaction.ToString();
			logRowFormatter[6] = context;
			logRowFormatter[5] = data;
			RmsClientManagerLog.Append(logRowFormatter);
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x001C0D80 File Offset: 0x001BEF80
		private static void LogEvent(RmsClientManagerLog.RmsClientManagerFeature clientManagerFeature, RmsClientManagerLog.RmsClientManagerEvent clientManagerEvent, OrganizationId orgId, Guid transaction, string data, string context)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(RmsClientManagerLog.rmsLogSchema);
			logRowFormatter[1] = clientManagerFeature;
			logRowFormatter[2] = clientManagerEvent;
			if (orgId != null)
			{
				logRowFormatter[3] = orgId.ToString();
			}
			logRowFormatter[7] = transaction.ToString();
			logRowFormatter[6] = context;
			logRowFormatter[5] = data;
			RmsClientManagerLog.Append(logRowFormatter);
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x001C0DF4 File Offset: 0x001BEFF4
		private static void LogEvent(RmsClientManagerLog.RmsClientManagerFeature clientManagerFeature, RmsClientManagerLog.RmsClientManagerEvent clientManagerEvent, Guid transaction, string data, string context)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(RmsClientManagerLog.rmsLogSchema);
			logRowFormatter[1] = clientManagerFeature;
			logRowFormatter[2] = clientManagerEvent;
			logRowFormatter[7] = transaction.ToString();
			logRowFormatter[6] = context;
			logRowFormatter[5] = data;
			RmsClientManagerLog.Append(logRowFormatter);
		}

		// Token: 0x060068E7 RID: 26855 RVA: 0x001C0E54 File Offset: 0x001BF054
		private static void LogEvent(RmsClientManagerLog.RmsClientManagerFeature clientManagerFeature, RmsClientManagerLog.RmsClientManagerEvent clientManagerEvent, Uri serverUrl, OrganizationId orgId, Guid transaction, string context)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(RmsClientManagerLog.rmsLogSchema);
			logRowFormatter[1] = clientManagerFeature;
			logRowFormatter[2] = clientManagerEvent;
			if (serverUrl != null)
			{
				logRowFormatter[4] = serverUrl.ToString();
			}
			if (orgId != null)
			{
				logRowFormatter[3] = orgId.ToString();
			}
			logRowFormatter[7] = transaction.ToString();
			logRowFormatter[6] = context;
			RmsClientManagerLog.Append(logRowFormatter);
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x001C0F1C File Offset: 0x001BF11C
		private static void HandleConfigurationChange(ADNotificationEventArgs args)
		{
			try
			{
				if (Interlocked.Increment(ref RmsClientManagerLog.notificationHandlerCount) == 1)
				{
					Server localServer = null;
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1191, "HandleConfigurationChange", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\rightsmanagement\\RmsClientManagerLog.cs");
						localServer = topologyConfigurationSession.FindLocalServer();
					});
					if (!adoperationResult.Succeeded)
					{
						RmsClientManagerLog.Tracer.TraceError<Exception>(0L, "Failed to get the local server.  Unable to reload the log configuration. Error {0}", adoperationResult.Exception);
					}
					else
					{
						RmsClientManagerLog.Configure(localServer);
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref RmsClientManagerLog.notificationHandlerCount);
			}
		}

		// Token: 0x060068E9 RID: 26857 RVA: 0x001C0FA0 File Offset: 0x001BF1A0
		private static void Append(LogRowFormatter row)
		{
			RmsClientManagerLog.rmsLog.Append(row, 0);
		}

		// Token: 0x04003B7A RID: 15226
		private const string LogType = "Rms Client Manager Log";

		// Token: 0x04003B7B RID: 15227
		private const string LogComponent = "RmsClientManagerLog";

		// Token: 0x04003B7C RID: 15228
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04003B7D RID: 15229
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"feature",
			"event-type",
			"tenant-id",
			"server-url",
			"data",
			"context",
			"transaction-id"
		};

		// Token: 0x04003B7E RID: 15230
		private static int notificationHandlerCount;

		// Token: 0x04003B7F RID: 15231
		private static LogSchema rmsLogSchema;

		// Token: 0x04003B80 RID: 15232
		private static Log rmsLog;

		// Token: 0x04003B81 RID: 15233
		private static bool rmsLogEnabled;

		// Token: 0x04003B82 RID: 15234
		private static ADNotificationRequestCookie notificationCookie;

		// Token: 0x04003B83 RID: 15235
		internal static readonly string LogSuffix = "IRMLOG";

		// Token: 0x04003B84 RID: 15236
		internal static readonly Uri DummyUriForTemplateCache = new Uri("cache:template");

		// Token: 0x04003B85 RID: 15237
		internal static readonly Uri DummyUriForRacClcCache = new Uri("cache:racClc");

		// Token: 0x04003B86 RID: 15238
		internal static readonly string PrelicenseNoResult = "No response";

		// Token: 0x04003B87 RID: 15239
		internal static readonly string UseLicenseEmpty = "UseLicense: Empty";

		// Token: 0x04003B88 RID: 15240
		internal static readonly string UseLicenseExists = "UseLicense: Exists";

		// Token: 0x04003B89 RID: 15241
		internal static readonly string AcquireServerRac = "Server RAC";

		// Token: 0x04003B8A RID: 15242
		internal static readonly string AcquireServerClc = "Server CLC";

		// Token: 0x04003B8B RID: 15243
		internal static readonly string AcquireServerRacClc = "Server RAC/CLC";

		// Token: 0x04003B8C RID: 15244
		internal static readonly string LicenseExists = "Exists";

		// Token: 0x04003B8D RID: 15245
		internal static readonly string LicenseEmpty = "Empty";

		// Token: 0x04003B8E RID: 15246
		internal static readonly string ServerInfoNotFound = "Not Found";

		// Token: 0x04003B8F RID: 15247
		internal static readonly string ServerInfoUriNull = "None";

		// Token: 0x02000B4C RID: 2892
		private enum Field
		{
			// Token: 0x04003B91 RID: 15249
			Time,
			// Token: 0x04003B92 RID: 15250
			Feature,
			// Token: 0x04003B93 RID: 15251
			EventType,
			// Token: 0x04003B94 RID: 15252
			TenantId,
			// Token: 0x04003B95 RID: 15253
			ServerUrl,
			// Token: 0x04003B96 RID: 15254
			Data,
			// Token: 0x04003B97 RID: 15255
			Context,
			// Token: 0x04003B98 RID: 15256
			TransactionId
		}

		// Token: 0x02000B4D RID: 2893
		internal enum RmsClientManagerFeature
		{
			// Token: 0x04003B9A RID: 15258
			Prelicense,
			// Token: 0x04003B9B RID: 15259
			UseLicense,
			// Token: 0x04003B9C RID: 15260
			RacClc,
			// Token: 0x04003B9D RID: 15261
			SignatureVerification,
			// Token: 0x04003B9E RID: 15262
			Template,
			// Token: 0x04003B9F RID: 15263
			ServerInfo,
			// Token: 0x04003BA0 RID: 15264
			DrmInitialization
		}

		// Token: 0x02000B4E RID: 2894
		internal enum RmsClientManagerEvent
		{
			// Token: 0x04003BA2 RID: 15266
			Acquire,
			// Token: 0x04003BA3 RID: 15267
			Success,
			// Token: 0x04003BA4 RID: 15268
			Queued,
			// Token: 0x04003BA5 RID: 15269
			Exception
		}
	}
}
