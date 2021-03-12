using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x02000193 RID: 403
	internal class SearchIndexRepairAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x0005D3A0 File Offset: 0x0005B5A0
		public SearchIndexRepairAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			DiagnosticsSessionFactory diagnosticsSessionFactory = new DiagnosticsSessionFactory();
			this.diagnosticsSession = diagnosticsSessionFactory.CreateComponentDiagnosticsSession("SearchIndexRepairAssistant", ExTraceGlobals.GeneralTracer, (long)this.GetHashCode());
			this.diagnosticsSession.TraceDebug("SearchIndexRepairAssistant: SearchIndexRepairAssistant", new object[0]);
			this.indexRepairLogger = new IndexRepairLogger();
			this.config = new SearchConfig(databaseInfo.Guid);
			this.errorStatisticsProvider = new ErrorStatisticsProvider(this.diagnosticsSession, databaseInfo.Guid, this.config);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0005D428 File Offset: 0x0005B628
		public void OnWorkCycleCheckpoint()
		{
			this.diagnosticsSession.TraceDebug("SearchIndexRepairAssistant: OnWorkCycleCheckpoint", new object[0]);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0005D440 File Offset: 0x0005B640
		internal void SetLogger(IndexRepairLogger newLogger)
		{
			this.diagnosticsSession.TraceDebug("SearchIndexRepairAssistant: SetLogger", new object[0]);
			this.indexRepairLogger = newLogger;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0005D460 File Offset: 0x0005B660
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			try
			{
				lock (this)
				{
					if (this.shuttingDown)
					{
						throw new ShutdownException();
					}
					this.diagnosticsSession.TraceDebug("SearchIndexRepairAssistant: InvokeInternal", new object[0]);
					ErrorStatistics errorStatistics = this.errorStatisticsProvider.GetErrorStatistics(invokeArgs.MailboxData.MailboxGuid);
					StoreStatistics storeStatistics = this.GetStoreStatistics(base.DatabaseInfo, invokeArgs.MailboxData.MailboxGuid);
					MailboxStatistics mailboxStatistics = new MailboxStatistics(base.DatabaseInfo.DatabaseName, invokeArgs.MailboxData, errorStatistics, storeStatistics);
					this.indexRepairLogger.LogMailboxStatistics(mailboxStatistics);
				}
			}
			catch (OperationFailedException ex)
			{
				this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "AssistantFailure", new object[]
				{
					"Failure talking to FAST {0}",
					ex
				});
				throw new TransientDatabaseException(Strings.IndexRepairQueryFailure, ex, new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
				{
					this.config.IndexRepairAssistantRetryInterval
				}));
			}
			catch (AssistantException ex2)
			{
				this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "AssistantFailure", new object[]
				{
					"Failed to execute assistant {0}",
					ex2
				});
				throw new SkipException(ex2);
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0005D5C8 File Offset: 0x0005B7C8
		protected override void OnShutdownInternal()
		{
			this.diagnosticsSession.TraceDebug("SearchIndexRepairAssistant: OnShutdownInternal", new object[0]);
			lock (this)
			{
				this.shuttingDown = true;
				this.errorStatisticsProvider.Dispose();
			}
			base.OnShutdownInternal();
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0005D6C0 File Offset: 0x0005B8C0
		private StoreStatistics GetStoreStatistics(DatabaseInfo databaseInfo, Guid mailboxGuid)
		{
			StoreStatistics result = null;
			TimeBasedAssistant.TrackAdminRpcCalls(databaseInfo, "Client=TBA", delegate(ExRpcAdmin admin)
			{
				PropValue[][] mailboxTableInfo = admin.GetMailboxTableInfo(databaseInfo.Guid, mailboxGuid, SearchIndexRepairAssistant.StorePropTags);
				if (mailboxTableInfo.Length != 1)
				{
					throw new AssistantException(Strings.IndexRepairUnexpectedRpcResultLength(mailboxTableInfo.Length));
				}
				if (mailboxTableInfo[0].Length != SearchIndexRepairAssistant.StorePropTags.Length)
				{
					throw new AssistantException(Strings.IndexRepairUnexpectedRpcResultRowLength(mailboxTableInfo[0].Length));
				}
				int @int = mailboxTableInfo[0][0].GetInt();
				int int2 = mailboxTableInfo[0][1].GetInt();
				result = new StoreStatistics((long)@int, (long)int2);
			});
			return result;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0005D736 File Offset: 0x0005B936
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0005D73E File Offset: 0x0005B93E
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0005D746 File Offset: 0x0005B946
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000A1B RID: 2587
		private const string ComponentName = "SearchIndexRepairAssistant";

		// Token: 0x04000A1C RID: 2588
		private static readonly PropTag[] StorePropTags = new PropTag[]
		{
			PropTag.ContentCount,
			PropTag.DeletedMsgCount
		};

		// Token: 0x04000A1D RID: 2589
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000A1E RID: 2590
		private readonly ErrorStatisticsProvider errorStatisticsProvider;

		// Token: 0x04000A1F RID: 2591
		private IndexRepairLogger indexRepairLogger;

		// Token: 0x04000A20 RID: 2592
		private SearchConfig config;

		// Token: 0x04000A21 RID: 2593
		private bool shuttingDown;
	}
}
