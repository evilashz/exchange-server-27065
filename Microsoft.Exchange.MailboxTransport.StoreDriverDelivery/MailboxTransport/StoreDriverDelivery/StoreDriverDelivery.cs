using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Shared;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000006 RID: 6
	internal sealed class StoreDriverDelivery : StoreDriverDeliveryDiagnostics, IStoreDriverDelivery, IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003562 File Offset: 0x00001762
		private StoreDriverDelivery()
		{
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(StoreDriverDelivery.FaultInjectionCallback));
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000358B File Offset: 0x0000178B
		public static StoreDriverDelivery Instance
		{
			get
			{
				return StoreDriverDelivery.instance;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003592 File Offset: 0x00001792
		public static IPHostEntry LocalIP
		{
			get
			{
				return StoreDriverDelivery.localIp;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003599 File Offset: 0x00001799
		public static IPAddress LocalIPAddress
		{
			get
			{
				return StoreDriverDelivery.localIp.AddressList[0];
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000035A7 File Offset: 0x000017A7
		public bool Retired
		{
			get
			{
				return this.retired;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000035B4 File Offset: 0x000017B4
		public string CurrentState
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append("Delivery thread count=");
				stringBuilder.AppendLine(StoreDriverDeliveryDiagnostics.DeliveringThreads.ToString());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000035F2 File Offset: 0x000017F2
		internal static string MailboxServerFqdn
		{
			get
			{
				return StoreDriverDelivery.localFqdn;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000035F9 File Offset: 0x000017F9
		internal static string MailboxServerDomain
		{
			get
			{
				return StoreDriverDelivery.localDomain;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003600 File Offset: 0x00001800
		internal static string MailboxServerName
		{
			get
			{
				return StoreDriverDelivery.localHostName;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003607 File Offset: 0x00001807
		public static IStoreDriverDelivery CreateStoreDriver()
		{
			return StoreDriverDelivery.Instance;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000360E File Offset: 0x0000180E
		public static void InitializePerformanceCounterMaintenance()
		{
			StoreDriverDelivery.performanceCounterMaintenanceTimer = new GuardedTimer(new TimerCallback(StoreDriverDelivery.RefreshPerformanceCounters), null, TimeSpan.FromSeconds(60.0));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003635 File Offset: 0x00001835
		public static void ShutdownPerformanceCounterMaintenance()
		{
			if (StoreDriverDelivery.performanceCounterMaintenanceTimer != null)
			{
				StoreDriverDelivery.performanceCounterMaintenanceTimer.Dispose(true);
				StoreDriverDelivery.performanceCounterMaintenanceTimer = null;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000364F File Offset: 0x0000184F
		private static void SetupLatencyTracker(MbxTransportMailItem mbxItem)
		{
			mbxItem.TrackSuccessfulConnectLatency(LatencyComponent.SmtpReceive);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003659 File Offset: 0x00001859
		public void Retire()
		{
			this.retired = true;
			StoreDriverDeliveryDiagnostics.Diag.TraceDebug(0L, "Retire StoreDriverDelivery");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003728 File Offset: 0x00001928
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			lock (this.syncObject)
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					try
					{
						StoreDriverDelivery.localIp = Dns.GetHostEntry("localhost");
						StoreDriverDelivery.localFqdn = StoreDriverDelivery.localIp.HostName;
						StoreDriverDelivery.localDomain = StoreDriverDelivery.GetDomainNameFromFqdn(StoreDriverDelivery.localFqdn);
						StoreDriverDelivery.localHostName = StoreDriverDelivery.GetShortNameFromFqdn(StoreDriverDelivery.localFqdn);
					}
					catch (SocketException ex)
					{
						StoreDriverDeliveryDiagnostics.Diag.TraceError<string>(0L, "Start failed: {0}", ex.ToString());
						StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_StoreDriverGetLocalIPFailure, null, new object[]
						{
							ex
						});
						throw new TransportComponentLoadFailedException(ex.Message, ex);
					}
					ProcessAccessManager.RegisterComponent(this);
					StoreDriverDeliveryDiagnostics.Diag.TraceDebug(0L, "Start delivery");
				}, 1);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000377C File Offset: 0x0000197C
		public void Stop()
		{
			StoreDriverDeliveryDiagnostics.Diag.TraceDebug(0L, "Stop StoreDriverDelivery");
			if (!this.retired)
			{
				this.Retire();
			}
			lock (this.syncObject)
			{
				this.StopDelivery();
				ProcessAccessManager.UnregisterComponent(this);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000037E4 File Offset: 0x000019E4
		public void Pause()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000037E6 File Offset: 0x000019E6
		public void Continue()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037E8 File Offset: 0x000019E8
		public void Load()
		{
			try
			{
				MExEvents.Initialize(Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config"), ProcessTransportRole.MailboxDelivery, LatencyAgentGroup.StoreDriver, "Microsoft.Exchange.Data.Transport.StoreDriverDelivery.StoreDriverDeliveryAgent");
				ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.Transport);
				StoreDriverDeliveryDiagnostics.Initialize();
				StoreDriverDelivery.InitializePerformanceCounterMaintenance();
			}
			catch (ExchangeConfigurationException ex)
			{
				StoreDriverDeliveryDiagnostics.Diag.TraceError((long)this.GetHashCode(), "StoreDriver.Load threw ExchangeConfigurationException: shutting down service.");
				StoreDriverDelivery.eventLogger.LogEvent(TransportEventLogConstants.Tuple_CannotStartAgents, null, new object[]
				{
					ex.LocalizedString,
					ex
				});
				this.Stop();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003880 File Offset: 0x00001A80
		public void Unload()
		{
			MExEvents.Shutdown();
			StoreDriverDelivery.ShutdownPerformanceCounterMaintenance();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000388C File Offset: 0x00001A8C
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003890 File Offset: 0x00001A90
		public SmtpResponse DoLocalDelivery(TransportMailItem item)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			ulong num3 = 0UL;
			MbxTransportMailItem mbxTransportMailItem = null;
			string sourceContext = string.Empty;
			if (StoreDriverDelivery.SilentlyDropProbeMessages(item))
			{
				return SmtpResponse.ProbeMessageDropped;
			}
			try
			{
				item.CacheTransportSettings();
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2735091005U);
				num3 = SessionId.GetNextSessionId();
				mbxTransportMailItem = new MbxTransportMailItem(item);
				mbxTransportMailItem.SessionStartTime = DateTime.UtcNow;
				sourceContext = StoreDriverDelivery.GenerateSessionSourceContext(num3, mbxTransportMailItem.SessionStartTime);
				StoreDriverDelivery.SetupLatencyTracker(mbxTransportMailItem);
				StoreDriverDelivery.ResolveItemRecipients(item);
				if (!this.ResolveMdbParameters(mbxTransportMailItem, num3))
				{
					return mbxTransportMailItem.Response;
				}
				StoreDriverDelivery.ScopeADRecipientCache(item);
				ConnectionLog.MapiDeliveryConnectionStart(num3, mbxTransportMailItem.DatabaseName, string.Format("Delivery;MailboxServer={0};Database={1}", StoreDriverDelivery.MailboxServerFqdn, mbxTransportMailItem.DatabaseName));
				if (this.retired)
				{
					ConnectionLog.MapiDeliveryConnectionRetired(num3, mbxTransportMailItem.DatabaseName);
					return SmtpResponseGenerator.StoreDriverRetireResponse;
				}
				MSExchangeStoreDriver.MessageDeliveryAttempts.Increment();
				MSExchangeStoreDriver.CurrentDeliveryThreads.Increment();
				StoreDriverDeliveryDiagnostics.IncrementDeliveringThreads();
				StoreDriverDeliveryDiagnostics.Diag.TracePfd<int, string>(0L, "PFD ESD {0} Start local delivery to {1}", 21403, mbxTransportMailItem.DatabaseName);
				ConnectionLog.MapiDeliveryConnectionStartingDelivery(num3, mbxTransportMailItem.DatabaseName);
				int num4 = this.DeliverMailItem(mbxTransportMailItem, num3);
				if (0 < num4)
				{
					num += (ulong)mbxTransportMailItem.MimeSize;
					num2 += (ulong)((long)num4);
				}
				ConnectionLog.MapiDeliveryConnectionStop(num3, mbxTransportMailItem.DatabaseName, 1UL, num, num2);
			}
			catch (ADTransientException exception)
			{
				if (mbxTransportMailItem == null)
				{
					mbxTransportMailItem = new MbxTransportMailItem(item);
				}
				RetryException exception2 = new RetryException(new MessageStatus(MessageAction.Retry, AckReason.RecipientMailboxLocationInfoNotAvailable, exception));
				StoreDriverDelivery.AckMailItemOnRetryException(AckStatus.Retry, mbxTransportMailItem, exception2, sourceContext, num3, num, num2);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(48992U);
			}
			catch (RetryException exception3)
			{
				StoreDriverDelivery.AckMailItemOnRetryException(AckStatus.Retry, mbxTransportMailItem, exception3, sourceContext, num3, num, num2);
			}
			finally
			{
				StoreDriverDeliveryDiagnostics.DecrementDeliveringThreads();
				MSExchangeStoreDriver.CurrentDeliveryThreads.Decrement();
				StoreDriverDeliveryDiagnostics.Diag.TracePfd<int, string>(0L, "PFD ESD {0} Stop local delivery to {1}", 17563, mbxTransportMailItem.DatabaseName);
				StoreDriverDeliveryDiagnostics.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
			}
			return mbxTransportMailItem.Response;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003AB8 File Offset: 0x00001CB8
		internal static string GenerateSessionSourceContext(ulong sessionId, DateTime sessionStartTime)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:X16};{1:yyyy-MM-ddTHH\\:mm\\:ss.fffZ}", new object[]
			{
				sessionId,
				sessionStartTime
			});
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003AF0 File Offset: 0x00001CF0
		internal static string GetShortNameFromFqdn(string fqdn)
		{
			int num = fqdn.IndexOf('.');
			if (num != -1)
			{
				return fqdn.Substring(0, num);
			}
			return fqdn;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B14 File Offset: 0x00001D14
		internal static string GetDomainNameFromFqdn(string fqdn)
		{
			int num = fqdn.IndexOf('.');
			if (num != -1)
			{
				return fqdn.Substring(num + 1);
			}
			return string.Empty;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B40 File Offset: 0x00001D40
		private static void LogRetryExceptionInConnectionLog(RetryException exception, MbxTransportMailItem mbxItem, string sourceContext, ulong sessionId, ulong bytesDelivered, ulong recipientCount)
		{
			StringBuilder stringBuilder = new StringBuilder(400);
			stringBuilder.Append("Lost connection - scheduling retry. ");
			if (exception.MessageStatus.Exception != null)
			{
				string exceptionTypeString = StorageExceptionHandler.GetExceptionTypeString(exception.MessageStatus.Exception);
				string exceptionDiagnosticInfo = StorageExceptionHandler.GetExceptionDiagnosticInfo(exception.MessageStatus.Exception);
				if (string.IsNullOrEmpty(exceptionDiagnosticInfo))
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "SmtpResponse: {0}: {1}: {2}.", new object[]
					{
						exception.MessageStatus.Response,
						exception.StoreDriverContext,
						exceptionTypeString
					});
				}
				else
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "SmtpResponse: {0}: {1}: {2}: {3}", new object[]
					{
						exception.MessageStatus.Response,
						exception.StoreDriverContext,
						exceptionTypeString,
						exceptionDiagnosticInfo
					});
				}
			}
			ConnectionLog.MapiDeliveryConnectionLost(sessionId, mbxItem.DatabaseName, stringBuilder.ToString(), 1UL, bytesDelivered, recipientCount);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003C34 File Offset: 0x00001E34
		private static Exception FaultInjectionCallback(string exceptionType)
		{
			LocalizedString localizedString = new LocalizedString("Fault injection.");
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.MailboxRules.InvalidRuleException", StringComparison.OrdinalIgnoreCase))
			{
				return new InvalidRuleException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.StoragePermanentException", StringComparison.OrdinalIgnoreCase))
			{
				return new StoragePermanentException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.StorageTransientException", StringComparison.OrdinalIgnoreCase))
			{
				return new StorageTransientException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionSessionLimit", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException = MapiExceptionHelper.SessionLimitException(localizedString);
				return new TooManyObjectsOpenedException(localizedString, innerException);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionRpcServerTooBusy", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException2 = MapiExceptionHelper.RpcServerTooBusyException(localizedString);
				return new StorageTransientException(localizedString, innerException2);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionNotEnoughMemory", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException3 = MapiExceptionHelper.NotEnoughMemoryException(localizedString);
				return new StorageTransientException(localizedString, innerException3);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionMaxThreadsPerMdbExceeded", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException4 = MapiExceptionHelper.MaxThreadsPerMdbExceededException(localizedString);
				return new StorageTransientException(localizedString, innerException4);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionMaxThreadsPerSCTExceeded", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException5 = MapiExceptionHelper.MaxThreadsPerSCTExceededException(localizedString);
				return new StorageTransientException(localizedString, innerException5);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionUnconfigured", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException6 = MapiExceptionHelper.UnconfiguredException(localizedString);
				return new MailboxUnavailableException(localizedString, innerException6);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionUnknownUser", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException7 = MapiExceptionHelper.UnknownUserException(localizedString);
				return new StoragePermanentException(localizedString, innerException7);
			}
			if (exceptionType.Equals("CannotGetSiteInfoException", StringComparison.OrdinalIgnoreCase))
			{
				return new StoragePermanentException(localizedString, new CannotGetSiteInfoException(localizedString));
			}
			if (exceptionType.Equals("System.ArgumentException", StringComparison.OrdinalIgnoreCase))
			{
				return new ArgumentException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Exchange.Data.Directory.ADTransientException", StringComparison.OrdinalIgnoreCase))
			{
				return new ADTransientException(localizedString);
			}
			if (exceptionType.Equals("StoreDriverAgentTransientException", StringComparison.OrdinalIgnoreCase))
			{
				return new StoreDriverAgentTransientException(Strings.StoreDriverAgentTransientExceptionEmail);
			}
			if (exceptionType.Equals("UnexpectedMessageCountException", StringComparison.OrdinalIgnoreCase))
			{
				return new UnexpectedMessageCountException("Fault injection.");
			}
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.MailboxInSiteFailoverException", StringComparison.OrdinalIgnoreCase))
			{
				return new MailboxInSiteFailoverException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionDuplicateDelivery", StringComparison.OrdinalIgnoreCase))
			{
				return new StoragePermanentException(localizedString, new MapiExceptionDuplicateDelivery(localizedString, 0, 0, null, null));
			}
			if (exceptionType.Equals("Microsoft.Exchange.MailboxTransport.StoreDriverCommon.SmtpResponseException", StringComparison.OrdinalIgnoreCase))
			{
				return new SmtpResponseException(AckReason.ApprovalUpdateSuccess, "TestSource");
			}
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.MailboxInfoStaleException", StringComparison.OrdinalIgnoreCase))
			{
				return new MailboxInfoStaleException(localizedString, new DatabaseNotFoundException(localizedString));
			}
			if (exceptionType.StartsWith("Microsoft.Mapi.MapiException", StringComparison.OrdinalIgnoreCase))
			{
				return FaultInjectionHelper.CreateXsoExceptionFromMapiException(exceptionType, localizedString);
			}
			return new ApplicationException(localizedString);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003EB4 File Offset: 0x000020B4
		private static void ResolveItemRecipients(TransportMailItem item)
		{
			try
			{
				List<ProxyAddress> list = new List<ProxyAddress>(item.Recipients.Count);
				foreach (MailRecipient mailRecipient in item.Recipients)
				{
					list.Add(new SmtpProxyAddress((string)mailRecipient.Email, false));
				}
				item.ADRecipientCache.FindAndCacheRecipients(list);
			}
			catch (ADTransientException exception)
			{
				throw new RetryException(new MessageStatus(MessageAction.Retry, AckReason.NotResolvedRecipient, exception));
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003F50 File Offset: 0x00002150
		private static void AckMailItemOnRetryException(AckStatus status, MbxTransportMailItem mailItem, RetryException exception, string sourceContext, ulong sessionId, ulong bytesDelivered, ulong recipientCount)
		{
			StoreDriverDeliveryDiagnostics.Diag.TraceDebug<RetryException, AckStatus>(0L, "StoreDriverDelivery encountered an exception {0}. Message Action: {1}.", exception, status);
			if (exception.MessageStatus.Action == MessageAction.RetryQueue)
			{
				mailItem.MessageLevelAction = MessageAction.RetryQueue;
			}
			mailItem.AckMailItem(status, exception.MessageStatus.Response, new AckDetails(StoreDriverDelivery.localHostName), exception.MessageStatus.RetryInterval, sourceContext);
			StoreDriverDelivery.LogRetryExceptionInConnectionLog(exception, mailItem, sourceContext, sessionId, bytesDelivered, recipientCount);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003FBB File Offset: 0x000021BB
		private static void RefreshPerformanceCounters(object state)
		{
			StoreDriverDeliveryPerfCounters.RefreshPerformanceCounters();
			StoreDriverDatabasePerfCounters.RefreshPerformanceCounters();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003FC8 File Offset: 0x000021C8
		private static bool SilentlyDropProbeMessages(TransportMailItem transportMailItem)
		{
			Header header = transportMailItem.RootPart.Headers.FindFirst("X-Exchange-Probe-Drop-Message");
			if (header != null && header.Value == "MailboxTransportDelivery-SDD-250")
			{
				StoreDriverDeliveryDiagnostics.Diag.TraceDebug<string, string, string>(0L, "Email '{0}' will be dropped because header '{1}' with value '{2}' was detected", transportMailItem.InternetMessageId ?? "not available", "X-Exchange-Probe-Drop-Message", "MailboxTransportDelivery-SDD-250");
				Queue<AckStatusAndResponse> queue = new Queue<AckStatusAndResponse>();
				queue.Enqueue(new AckStatusAndResponse(AckStatus.SuccessNoDsn, SmtpResponse.ProbeMessageDropped));
				transportMailItem.Ack(AckStatus.SuccessNoDsn, SmtpResponse.ProbeMessageDropped, transportMailItem.Recipients, queue);
				return true;
			}
			return false;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004058 File Offset: 0x00002258
		private static void ScopeADRecipientCache(TransportMailItem item)
		{
			Guid gal = StoreDriverDelivery.GetGal(item.RootPart.Headers);
			if (gal == Guid.Empty)
			{
				return;
			}
			ADObjectId scopingAddressListId = new ADObjectId(gal);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(item.OrganizationId, scopingAddressListId);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 921, "ScopeADRecipientCache", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\MailboxTransportDelivery\\StoreDriver\\StoreDriverDelivery.cs");
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = new ADRecipientCache<TransportMiniRecipient>(tenantOrRootOrgRecipientSession, TransportMiniRecipientSchema.Properties, 1, item.OrganizationId);
			Guid addressBookPolicy = StoreDriverDelivery.GetAddressBookPolicy(item.RootPart.Headers);
			foreach (ProxyAddress proxyAddress in item.ADRecipientCache.Keys)
			{
				Result<TransportMiniRecipient> result;
				if (item.ADRecipientCache.TryGetValue(proxyAddress, out result) && result.Data != null && result.Data.AddressBookPolicy != null && result.Data.AddressBookPolicy.ObjectGuid == addressBookPolicy)
				{
					adrecipientCache.AddCacheEntry(proxyAddress, result);
				}
			}
			item.ADRecipientCache = adrecipientCache;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004174 File Offset: 0x00002374
		private static Guid GetGal(HeaderList headers)
		{
			ArgumentValidator.ThrowIfNull("headers", headers);
			Header header = headers.FindFirst("X-MS-Exchange-Forest-GAL-Scope");
			if (header != null)
			{
				return StoreDriverDelivery.GetHeaderValueAsGuid(header);
			}
			return Guid.Empty;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000041A8 File Offset: 0x000023A8
		private static Guid GetAddressBookPolicy(HeaderList headers)
		{
			ArgumentValidator.ThrowIfNull("headers", headers);
			Header header = headers.FindFirst("X-MS-Exchange-ABP-GUID");
			if (header != null)
			{
				return StoreDriverDelivery.GetHeaderValueAsGuid(header);
			}
			return Guid.Empty;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000041DC File Offset: 0x000023DC
		private static string GetHeaderValue(Header header)
		{
			string text;
			if (header.TryGetValue(out text))
			{
				return text.Trim();
			}
			return string.Empty;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004200 File Offset: 0x00002400
		private static Guid GetHeaderValueAsGuid(Header header)
		{
			string headerValue = StoreDriverDelivery.GetHeaderValue(header);
			Guid result;
			if (!string.IsNullOrEmpty(headerValue) && Guid.TryParse(headerValue, out result))
			{
				return result;
			}
			return Guid.Empty;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004230 File Offset: 0x00002430
		private bool ResolveMdbParameters(MbxTransportMailItem mbxItem, ulong sessionId)
		{
			bool flag = false;
			ProxyAddress proxyAddress = ProxyAddress.Parse("SMTP:" + mbxItem.Recipients[0].ToString());
			Result<TransportMiniRecipient> result;
			if (mbxItem.ADRecipientCache.TryGetValue(proxyAddress, out result) && result.Data != null)
			{
				if (result.Data.Database != null)
				{
					mbxItem.DatabaseGuid = result.Data.Database.ObjectGuid;
					mbxItem.DatabaseName = result.Data.Database.Name;
					flag = true;
				}
				else if (result.Data.RecipientTypeDetails == RecipientTypeDetails.PublicFolder)
				{
					ADObjectId adobjectId = null;
					if (mbxItem.Recipients[0].ExtendedProperties.TryGetValue<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.Database", out adobjectId) && adobjectId != null)
					{
						mbxItem.DatabaseGuid = adobjectId.ObjectGuid;
						mbxItem.DatabaseName = adobjectId.Name;
						flag = true;
					}
				}
			}
			if (flag)
			{
				Guid databaseGuid = mbxItem.DatabaseGuid;
				if (mbxItem.DatabaseGuid == Guid.Empty)
				{
					flag = false;
					StoreDriverDeliveryDiagnostics.Diag.TraceWarning<long>(0L, "MDB parameters were found but were null or empty for item {0}.", mbxItem.RecordId);
				}
			}
			if (!flag)
			{
				mbxItem.AckMailItem(AckStatus.Fail, AckReason.MissingMdbProperties, null, null, StoreDriverDelivery.GenerateSessionSourceContext(sessionId, mbxItem.SessionStartTime));
				StoreDriverDeliveryDiagnostics.Diag.TraceWarning<long>(0L, "Failed to get MDB parameters for item {0}.", mbxItem.RecordId);
			}
			return flag;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004384 File Offset: 0x00002584
		private void StopDelivery()
		{
			StoreDriverDeliveryDiagnostics.Diag.TraceDebug(0L, "Stop Delivery");
			while (StoreDriverDeliveryDiagnostics.DeliveringThreads > 0)
			{
				Thread.Sleep(500);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000043AC File Offset: 0x000025AC
		private int DeliverMailItem(MbxTransportMailItem mailItem, ulong sessionId)
		{
			int result = 0;
			TraceHelper.MessageProbeActivityId = mailItem.SystemProbeId;
			TraceHelper.StoreDriverDeliveryTracer.TracePass<string>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Processing started for MessageId {0}.", mailItem.InternetMessageId ?? "NULL");
			TimeSpan? retryInterval;
			using (MailItemDeliver mailItemDeliver = new MailItemDeliver(mailItem, sessionId))
			{
				try
				{
					StoreDriverDeliveryDiagnostics.HangDetector[sessionId] = mailItemDeliver;
					mailItemDeliver.DeliverToRecipients();
				}
				finally
				{
					StoreDriverDeliveryDiagnostics.HangDetector.Remove(sessionId);
				}
				result = mailItemDeliver.DeliveredRecipients;
				retryInterval = mailItemDeliver.RetryInterval;
			}
			AckDetails ackDetails = new AckDetails(StoreDriverDelivery.localHostName);
			ackDetails.AddEventData("MailboxDatabaseName", mailItem.DatabaseName);
			mailItem.AckMailItem(AckStatus.Success, SmtpResponse.NoopOk, ackDetails, retryInterval, StoreDriverDelivery.GenerateSessionSourceContext(sessionId, mailItem.SessionStartTime));
			TraceHelper.StoreDriverDeliveryTracer.TracePass<string>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Processing complete for MessageID {0}.", mailItem.InternetMessageId ?? "NULL");
			return result;
		}

		// Token: 0x04000024 RID: 36
		public const string MailboxDatabaseNamePropertyName = "MailboxDatabaseName";

		// Token: 0x04000025 RID: 37
		public const string ExceptionAgentName = "ExceptionAgentName";

		// Token: 0x04000026 RID: 38
		private static string localFqdn;

		// Token: 0x04000027 RID: 39
		private static string localDomain;

		// Token: 0x04000028 RID: 40
		private static string localHostName;

		// Token: 0x04000029 RID: 41
		private static IPHostEntry localIp;

		// Token: 0x0400002A RID: 42
		private static ExEventLog eventLogger = new ExEventLog(new Guid("{D81003EF-1A7B-4AF0-BA18-236DB5A83114}"), "MSExchange Store Driver Delivery");

		// Token: 0x0400002B RID: 43
		private static StoreDriverDelivery instance = new StoreDriverDelivery();

		// Token: 0x0400002C RID: 44
		private static GuardedTimer performanceCounterMaintenanceTimer;

		// Token: 0x0400002D RID: 45
		private volatile bool retired;

		// Token: 0x0400002E RID: 46
		private object syncObject = new object();
	}
}
