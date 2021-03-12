using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005D3 RID: 1491
	[Cmdlet("Test", "MAPIConnectivity", SupportsShouldProcess = true, DefaultParameterSetName = "Server")]
	public sealed class TestMapiConnectivity : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000D4842 File Offset: 0x000D2A42
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x000D4859 File Offset: 0x000D2A59
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Server", ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000D486C File Offset: 0x000D2A6C
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000D4892 File Offset: 0x000D2A92
		[Parameter(Mandatory = false, ParameterSetName = "Server")]
		public SwitchParameter IncludePassive
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludePassive"] ?? false);
			}
			set
			{
				base.Fields["IncludePassive"] = value;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000D48AA File Offset: 0x000D2AAA
		// (set) Token: 0x06003492 RID: 13458 RVA: 0x000D48C1 File Offset: 0x000D2AC1
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Database", ValueFromPipeline = true)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000D48D4 File Offset: 0x000D2AD4
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000D48EB File Offset: 0x000D2AEB
		[Parameter(Mandatory = false, ParameterSetName = "Database")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter CopyOnServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["CopyOnServer"];
			}
			set
			{
				base.Fields["CopyOnServer"] = value;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000D48FE File Offset: 0x000D2AFE
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000D4924 File Offset: 0x000D2B24
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000D493C File Offset: 0x000D2B3C
		// (set) Token: 0x06003498 RID: 13464 RVA: 0x000D4962 File Offset: 0x000D2B62
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter EnableSoftDeletedRecipientLogon
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnableSoftDeletedRecipientLogon"] ?? false);
			}
			set
			{
				base.Fields["EnableSoftDeletedRecipientLogon"] = value;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000D497A File Offset: 0x000D2B7A
		// (set) Token: 0x0600349A RID: 13466 RVA: 0x000D499C File Offset: 0x000D2B9C
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int PerConnectionTimeout
		{
			get
			{
				return (int)(base.Fields["PerConnectionTimeout"] ?? 60);
			}
			set
			{
				base.Fields["PerConnectionTimeout"] = value;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000D49B4 File Offset: 0x000D2BB4
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x000D49D6 File Offset: 0x000D2BD6
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int AllConnectionsTimeout
		{
			get
			{
				return (int)(base.Fields["AllConnectionsTimeout"] ?? 90);
			}
			set
			{
				base.Fields["AllConnectionsTimeout"] = value;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000D49EE File Offset: 0x000D2BEE
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x000D4A10 File Offset: 0x000D2C10
		[Parameter(Mandatory = false)]
		[ValidateRange(1, 2147483647)]
		public int ActiveDirectoryTimeout
		{
			get
			{
				return (int)(base.Fields["ActiveDirectoryTimeout"] ?? 15);
			}
			set
			{
				base.Fields["ActiveDirectoryTimeout"] = value;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000D4A28 File Offset: 0x000D2C28
		// (set) Token: 0x060034A0 RID: 13472 RVA: 0x000D4A49 File Offset: 0x000D2C49
		[Parameter(Mandatory = false)]
		public bool MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000D4A64 File Offset: 0x000D2C64
		private void EventForNonSuccessTransactionResultList(List<MapiTransactionOutcome> transactions, int emptyListEventId, EventTypeEnumeration emptyListEventType, string emptyListEventMsg, int nonEmptyListEventId, EventTypeEnumeration nonEmptyListEventType, string nonEmptyListEventMsgPrefix)
		{
			if (transactions.Count == 0)
			{
				this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring MAPIConnectivity", emptyListEventId, emptyListEventType, emptyListEventMsg));
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(nonEmptyListEventMsgPrefix);
			foreach (MapiTransactionOutcome mapiTransactionOutcome in transactions)
			{
				stringBuilder.Append(Strings.MapiTransactionFailedSummary(this.wasTargetMailboxSpecified ? mapiTransactionOutcome.LongTargetString() : Strings.SystemMailboxTarget(mapiTransactionOutcome.ShortTargetString()), mapiTransactionOutcome.Error));
			}
			this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring MAPIConnectivity", nonEmptyListEventId, nonEmptyListEventType, stringBuilder.ToString()));
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000D4B38 File Offset: 0x000D2D38
		private void WriteErrorAndMonitoringEvent(Exception exception, ErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, errorCategory, target);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000D4B64 File Offset: 0x000D2D64
		private DatabaseLocationInfo GetServerForDatabase(ActiveManager activeManager, Guid dbGuid)
		{
			DatabaseLocationInfo result = null;
			try
			{
				result = activeManager.GetServerForDatabase(dbGuid);
			}
			catch (DatabaseNotFoundException)
			{
				base.WriteWarning(string.Format("Caught DatabaseNotFoundException when trying to get the server for database {0} from activemanager; will use the server info from AD.", dbGuid));
			}
			catch (ObjectNotFoundException)
			{
				base.WriteWarning(string.Format("Caught ObjectNotFoundException when trying to get the server for database {0} from activemanager; will use the server info from AD.", dbGuid));
			}
			catch (StoragePermanentException)
			{
				base.WriteWarning(string.Format("Caught StoragePermanentException when trying to get the server for database {0} from activemanager; will use the server info from AD.", dbGuid));
			}
			catch (StorageTransientException)
			{
				base.WriteWarning(string.Format("Caught StorageTransientException when trying to get the server for database {0} from activemanager; will use the server info from AD.", dbGuid));
			}
			return result;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000D4C18 File Offset: 0x000D2E18
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000D4C20 File Offset: 0x000D2E20
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Database" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageTestMAPIConnectivityDatabase(this.Database.ToString());
				}
				if ("Server" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageTestMAPIConnectivityServer(this.Server.ToString());
				}
				return Strings.ConfirmationMessageTestMAPIConnectivityIdentity(this.Identity.ToString());
			}
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000D4C83 File Offset: 0x000D2E83
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000D4C98 File Offset: 0x000D2E98
		protected override void InternalValidate()
		{
			bool flag = false;
			try
			{
				ADRecipient adrecipient = null;
				List<MailboxDatabase> list = new List<MailboxDatabase>();
				bool flag2 = false;
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				base.TenantGlobalCatalogSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)this.ActiveDirectoryTimeout));
				this.ConfigurationSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)this.ActiveDirectoryTimeout));
				this.transactionTargets = new List<MapiTransaction>();
				if (this.Identity != null)
				{
					if (this.EnableSoftDeletedRecipientLogon)
					{
						IDirectorySession directorySession = base.DataSession as IDirectorySession;
						directorySession.SessionSettings.IncludeSoftDeletedObjects = true;
					}
					ADUser aduser = (ADUser)RecipientTaskHelper.ResolveDataObject<ADUser>(base.DataSession, base.TenantGlobalCatalogSession, base.ServerSettings, this.Identity, null, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
					if (this.Archive)
					{
						if (aduser.ArchiveDatabase == null)
						{
							base.WriteError(new MdbAdminTaskException(Strings.ErrorArchiveNotEnabled(aduser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
						}
						else
						{
							this.Database = new DatabaseIdParameter(aduser.ArchiveDatabase);
						}
					}
					else
					{
						this.Database = new DatabaseIdParameter(aduser.Database);
					}
					flag = true;
					adrecipient = aduser;
				}
				if (this.Database != null)
				{
					MailboxDatabase mailboxDatabase = null;
					MailboxDatabase mailboxDatabase2 = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())));
					if (mailboxDatabase2.Recovery)
					{
						string name = mailboxDatabase2.Name;
						RecoveryMailboxDatabaseNotMonitoredException exception = new RecoveryMailboxDatabaseNotMonitoredException(name);
						this.WriteErrorAndMonitoringEvent(exception, ErrorCategory.InvalidOperation, null, 1006, "MSExchange Monitoring MAPIConnectivity");
						return;
					}
					mailboxDatabase = mailboxDatabase2;
					if (!flag)
					{
						try
						{
							MapiTaskHelper.VerifyDatabaseAndItsOwningServerInScope(base.SessionSettings, mailboxDatabase, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
							flag = true;
						}
						catch (InvalidOperationException exception2)
						{
							this.WriteErrorAndMonitoringEvent(exception2, ErrorCategory.InvalidOperation, null, 1005, "MSExchange Monitoring MAPIConnectivity");
							return;
						}
					}
					list.Add(mailboxDatabase);
					if (this.CopyOnServer != null)
					{
						this.Server = this.CopyOnServer;
					}
					else
					{
						DatabaseLocationInfo serverForDatabase = this.GetServerForDatabase(activeManagerInstance, mailboxDatabase.Guid);
						if (serverForDatabase != null)
						{
							this.Server = ServerIdParameter.Parse(serverForDatabase.ServerFqdn);
						}
						else
						{
							this.Server = ServerIdParameter.Parse(mailboxDatabase.Server.DistinguishedName);
						}
					}
				}
				if (this.Server == null)
				{
					string machineName = Environment.MachineName;
					this.Server = ServerIdParameter.Parse(machineName);
				}
				this.targetServer = (Server)base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				LocalizedException ex = null;
				if (!this.targetServer.IsExchange2007OrLater)
				{
					ex = new OperationOnOldServerException(this.targetServer.Name);
				}
				else if (!this.targetServer.IsMailboxServer)
				{
					ex = new OperationOnlyOnMailboxServerException(this.targetServer.Name);
				}
				if (ex != null)
				{
					this.WriteErrorAndMonitoringEvent(ex, ErrorCategory.InvalidArgument, null, 1005, "MSExchange Monitoring MAPIConnectivity");
				}
				else
				{
					if (!flag)
					{
						try
						{
							MapiTaskHelper.VerifyIsWithinConfigWriteScope(base.SessionSettings, this.targetServer, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
						}
						catch (InvalidOperationException exception3)
						{
							this.WriteErrorAndMonitoringEvent(exception3, ErrorCategory.InvalidOperation, null, 1005, "MSExchange Monitoring MAPIConnectivity");
							return;
						}
					}
					if (list.Count == 0)
					{
						MailboxDatabase[] mailboxDatabases = this.targetServer.GetMailboxDatabases();
						if (mailboxDatabases.Length > 0)
						{
							flag2 = true;
						}
						foreach (MailboxDatabase mailboxDatabase3 in mailboxDatabases)
						{
							if (!mailboxDatabase3.AutoDagExcludeFromMonitoring)
							{
								if (this.IncludePassive)
								{
									list.Add(mailboxDatabase3);
								}
								else
								{
									DatabaseLocationInfo serverForDatabase = this.GetServerForDatabase(activeManagerInstance, mailboxDatabase3.Guid);
									if ((serverForDatabase != null && serverForDatabase.ServerGuid == this.targetServer.Guid) || (serverForDatabase == null && mailboxDatabase3.Server.ObjectGuid == this.targetServer.Guid))
									{
										list.Add(mailboxDatabase3);
									}
								}
							}
						}
					}
					if (adrecipient != null)
					{
						this.wasTargetMailboxSpecified = true;
						this.transactionTargets.Add(new MapiTransaction(this.targetServer, list[0], adrecipient, this.Archive, list[0].Server.ObjectGuid == this.targetServer.Guid));
					}
					else
					{
						foreach (MailboxDatabase mailboxDatabase4 in list)
						{
							if (!mailboxDatabase4.Recovery)
							{
								GeneralMailboxIdParameter id = GeneralMailboxIdParameter.Parse(string.Format(CultureInfo.InvariantCulture, "SystemMailbox{{{0}}}", new object[]
								{
									mailboxDatabase4.Guid.ToString()
								}));
								IEnumerable<ADSystemMailbox> dataObjects = base.GetDataObjects<ADSystemMailbox>(id, base.RootOrgGlobalCatalogSession, null);
								using (IEnumerator<ADSystemMailbox> enumerator2 = dataObjects.GetEnumerator())
								{
									adrecipient = (enumerator2.MoveNext() ? enumerator2.Current : null);
									this.transactionTargets.Add(new MapiTransaction(this.targetServer, mailboxDatabase4, adrecipient, false, mailboxDatabase4.Server.ObjectGuid == this.targetServer.Guid));
								}
							}
						}
					}
					this.transactionTargets.Sort();
					if (this.transactionTargets.Count < 1)
					{
						if (flag2)
						{
							this.WriteWarning(Strings.MapiTransactionServerWithoutMdbs(this.targetServer.Name));
							this.onlyPassives = true;
						}
						else
						{
							this.WriteErrorAndMonitoringEvent(new NoMdbForOperationException(this.targetServer.Name), ErrorCategory.ReadError, null, 1010, "MSExchange Monitoring MAPIConnectivity");
						}
					}
				}
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000D530C File Offset: 0x000D350C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				List<MapiTransactionOutcome> list = new List<MapiTransactionOutcome>();
				List<MapiTransactionOutcome> list2 = new List<MapiTransactionOutcome>();
				List<MapiTransactionOutcome> list3 = new List<MapiTransactionOutcome>();
				List<MapiTransactionOutcome> list4 = new List<MapiTransactionOutcome>();
				int num = this.PerConnectionTimeout * 1000;
				if (this.transactionTargets.Count > 0 && TestMapiConnectivity.needExtraTransaction)
				{
					this.transactionTargets[0].TimedExecute((num < 10000) ? 10000 : num);
					TestMapiConnectivity.needExtraTransaction = false;
				}
				Stopwatch stopwatch = Stopwatch.StartNew();
				foreach (MapiTransaction mapiTransaction in this.transactionTargets)
				{
					MapiTransactionOutcome mapiTransactionOutcome;
					if (stopwatch.Elapsed.TotalSeconds < (double)this.AllConnectionsTimeout)
					{
						mapiTransactionOutcome = mapiTransaction.TimedExecute(num);
					}
					else
					{
						mapiTransactionOutcome = new MapiTransactionOutcome(mapiTransaction.TargetServer, mapiTransaction.Database, mapiTransaction.ADRecipient);
						mapiTransactionOutcome.Update(MapiTransactionResultEnum.Failure, TimeSpan.Zero, Strings.MapiTranscationErrorMsgNoTimeLeft(this.AllConnectionsTimeout), null, null, mapiTransaction.Database.Server.ObjectGuid == mapiTransaction.TargetServer.Guid);
					}
					base.WriteObject(mapiTransactionOutcome);
					string performanceInstance = this.wasTargetMailboxSpecified ? mapiTransactionOutcome.LongTargetString() : mapiTransactionOutcome.ShortTargetString();
					double performanceValue;
					if (mapiTransactionOutcome.Result.Value == MapiTransactionResultEnum.Success)
					{
						performanceValue = mapiTransactionOutcome.Latency.TotalMilliseconds;
					}
					else if (mapiTransactionOutcome.Result.Value == MapiTransactionResultEnum.MdbMoved)
					{
						list3.Add(mapiTransactionOutcome);
						performanceValue = 0.0;
					}
					else if (mapiTransactionOutcome.Result.Value == MapiTransactionResultEnum.StoreNotRunning)
					{
						list2.Add(mapiTransactionOutcome);
						performanceValue = -1.0;
					}
					else
					{
						if (this.onlyPassives)
						{
							list4.Add(mapiTransactionOutcome);
						}
						else
						{
							list.Add(mapiTransactionOutcome);
						}
						performanceValue = -1.0;
					}
					this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter("MSExchange Monitoring MAPIConnectivity", "Logon Latency", performanceInstance, performanceValue));
				}
				this.EventForNonSuccessTransactionResultList(list4, 1011, EventTypeEnumeration.Information, Strings.AllMapiTransactionsSucceeded, 1009, EventTypeEnumeration.Error, Strings.MapiTransactionFailedAgainstServerPrefix);
				this.EventForNonSuccessTransactionResultList(list2, 1008, EventTypeEnumeration.Information, Strings.AllMapiTransactionsSucceeded, 1004, EventTypeEnumeration.Error, Strings.SomeMapiTransactionsFailedPrefix);
				this.EventForNonSuccessTransactionResultList(list, 1000, EventTypeEnumeration.Information, Strings.AllMapiTransactionsSucceeded, 1001, EventTypeEnumeration.Error, Strings.SomeMapiTransactionsFailedPrefix);
				this.EventForNonSuccessTransactionResultList(list3, 1002, EventTypeEnumeration.Information, Strings.NoMdbWasMovedWhileRunning, 1003, EventTypeEnumeration.Warning, Strings.SomeMdbsWereMovedWhileRunningPrefix);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000D5618 File Offset: 0x000D3818
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.monitoringData = new MonitoringData();
		}

		// Token: 0x04002445 RID: 9285
		private const string CmdletNoun = "MAPIConnectivity";

		// Token: 0x04002446 RID: 9286
		private const string CmdletMonitoringEventSource = "MSExchange Monitoring MAPIConnectivity";

		// Token: 0x04002447 RID: 9287
		private const int DefaultPerConnectionTimeoutInSeconds = 60;

		// Token: 0x04002448 RID: 9288
		private const int DefaultAllConnectionsTimeoutInSeconds = 90;

		// Token: 0x04002449 RID: 9289
		private const int DefaultADOperationsTimeoutInSeconds = 15;

		// Token: 0x0400244A RID: 9290
		private const string MonitoringPerformanceObject = "MSExchange Monitoring MAPIConnectivity";

		// Token: 0x0400244B RID: 9291
		private const string MonitoringLatencyPerfCounter = "Logon Latency";

		// Token: 0x0400244C RID: 9292
		private const double LatencyPerformanceInCaseOfError = -1.0;

		// Token: 0x0400244D RID: 9293
		private const double LatencyPerformanceInCaseOfMdbMoved = 0.0;

		// Token: 0x0400244E RID: 9294
		private const int MinimalTimeoutMSecForExtraTransaction = 10000;

		// Token: 0x0400244F RID: 9295
		private MonitoringData monitoringData;

		// Token: 0x04002450 RID: 9296
		private List<MapiTransaction> transactionTargets;

		// Token: 0x04002451 RID: 9297
		private Server targetServer;

		// Token: 0x04002452 RID: 9298
		private bool wasTargetMailboxSpecified;

		// Token: 0x04002453 RID: 9299
		private bool onlyPassives;

		// Token: 0x04002454 RID: 9300
		private static bool needExtraTransaction = true;

		// Token: 0x020005D4 RID: 1492
		private static class EventId
		{
			// Token: 0x04002455 RID: 9301
			internal const int AllMapiTransactionsSucceeded = 1000;

			// Token: 0x04002456 RID: 9302
			internal const int SomeMapiTransactionsFailed = 1001;

			// Token: 0x04002457 RID: 9303
			internal const int NoMbdWasMovedWhileRunning = 1002;

			// Token: 0x04002458 RID: 9304
			internal const int SomeMdbsWereMovedWhileRunning = 1003;

			// Token: 0x04002459 RID: 9305
			internal const int MapiTransactionsFailedStoreNotRunning = 1004;

			// Token: 0x0400245A RID: 9306
			public const int OperationOnInvalidServer = 1005;

			// Token: 0x0400245B RID: 9307
			internal const int RecoveryMailboxDatabaseNotMonitored = 1006;

			// Token: 0x0400245C RID: 9308
			internal const int NodeDoesNotOwnExchangeVirtualServer = 1007;

			// Token: 0x0400245D RID: 9309
			internal const int MapiTransactionsSucceededStoreRunning = 1008;

			// Token: 0x0400245E RID: 9310
			internal const int MapiTransactionsFailedAgainstServer = 1009;

			// Token: 0x0400245F RID: 9311
			internal const int DomainControllerNotAccessible = 1010;

			// Token: 0x04002460 RID: 9312
			internal const int MapiTransactionsSucceededAgainstServer = 1011;
		}
	}
}
