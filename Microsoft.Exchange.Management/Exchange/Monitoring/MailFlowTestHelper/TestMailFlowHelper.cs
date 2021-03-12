using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Monitoring.MailFlowTestHelper
{
	// Token: 0x020005F7 RID: 1527
	internal class TestMailFlowHelper
	{
		// Token: 0x0600365D RID: 13917 RVA: 0x000E00F5 File Offset: 0x000DE2F5
		internal TestMailFlowHelper(TestMailFlow taskInstance) : this()
		{
			this.taskInstance = taskInstance;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000E0104 File Offset: 0x000DE304
		internal TestMailFlowHelper()
		{
			this.monitoringData = new MonitoringData();
			this.monitoringDataSource = "MSExchange Monitoring Mailflow ";
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000E0122 File Offset: 0x000DE322
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x000E012A File Offset: 0x000DE32A
		protected ADSystemMailbox SourceSystemMailbox
		{
			get
			{
				return this.sourceSystemMailbox;
			}
			set
			{
				this.sourceSystemMailbox = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x000E0133 File Offset: 0x000DE333
		// (set) Token: 0x06003662 RID: 13922 RVA: 0x000E013B File Offset: 0x000DE33B
		protected Server SourceMailboxServer
		{
			get
			{
				return this.sourceMailboxServer;
			}
			set
			{
				this.sourceMailboxServer = value;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000E0144 File Offset: 0x000DE344
		// (set) Token: 0x06003664 RID: 13924 RVA: 0x000E014C File Offset: 0x000DE34C
		protected MailboxDatabase SourceSystemMailboxMdb
		{
			get
			{
				return this.sourceSystemMailboxMdb;
			}
			set
			{
				this.sourceSystemMailboxMdb = value;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000E0155 File Offset: 0x000DE355
		// (set) Token: 0x06003666 RID: 13926 RVA: 0x000E015D File Offset: 0x000DE35D
		protected TestMailFlow Task
		{
			get
			{
				return this.task;
			}
			set
			{
				this.task = value;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000E0166 File Offset: 0x000DE366
		// (set) Token: 0x06003668 RID: 13928 RVA: 0x000E016E File Offset: 0x000DE36E
		protected bool IsRemoteTest
		{
			get
			{
				return this.isRemoteTest;
			}
			set
			{
				this.isRemoteTest = value;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000E0177 File Offset: 0x000DE377
		// (set) Token: 0x0600366A RID: 13930 RVA: 0x000E017F File Offset: 0x000DE37F
		protected string MonitoringDataSource
		{
			get
			{
				return this.monitoringDataSource;
			}
			set
			{
				this.monitoringDataSource = value;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000E0188 File Offset: 0x000DE388
		// (set) Token: 0x0600366C RID: 13932 RVA: 0x000E0190 File Offset: 0x000DE390
		protected MonitoringData MonitoringData
		{
			get
			{
				return this.monitoringData;
			}
			set
			{
				this.monitoringData = value;
			}
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000E019C File Offset: 0x000DE39C
		internal virtual void InternalValidate()
		{
			TimeSpan value = TimeSpan.FromSeconds((double)this.Task.ActiveDirectoryTimeout);
			this.Task.TenantGlobalCatalogSession.ServerTimeout = new TimeSpan?(value);
			this.Task.ConfigurationSession.ServerTimeout = new TimeSpan?(value);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000E01E7 File Offset: 0x000DE3E7
		internal virtual void InternalProcessRecord()
		{
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000E01E9 File Offset: 0x000DE3E9
		internal virtual void InternalStateReset()
		{
			this.monitoringData = new MonitoringData();
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000E01F6 File Offset: 0x000DE3F6
		internal void SetTask(TestMailFlow task)
		{
			this.Task = task;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000E01FF File Offset: 0x000DE3FF
		internal void OutputMonitoringData()
		{
			this.Task.WriteObject(this.monitoringData);
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000E0214 File Offset: 0x000DE414
		internal void DiagnoseAndReportMapiException(LocalizedException ex)
		{
			MapiTransaction mapiTransaction = new MapiTransaction(this.SourceMailboxServer, this.SourceSystemMailboxMdb, this.SourceSystemMailbox, false, true);
			bool flag;
			bool flag2;
			string value = mapiTransaction.DiagnoseMapiOperationException(ex, out flag, out flag2);
			RecipientTaskException ex2 = new RecipientTaskException(new LocalizedString(value));
			if (flag)
			{
				this.AddErrorMonitoringEvent(1009, ex2.Message);
			}
			else
			{
				this.AddErrorMonitoringEvent(1007, ex2.Message);
			}
			this.Task.WriteError(ex2, ErrorCategory.InvalidData, null);
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000E028C File Offset: 0x000DE48C
		protected static MapiMessage GetDeliveryReceipt(MapiFolder folder, string subject, bool dsnOnly)
		{
			string value = DsnHumanReadableWriter.GetLocalizedSubjectPrefix(DsnFlags.Relay).ToString();
			MapiTable contentsTable;
			MapiTable mapiTable = contentsTable = folder.GetContentsTable();
			try
			{
				mapiTable.SetColumns(new PropTag[]
				{
					PropTag.EntryId,
					PropTag.Subject,
					PropTag.MessageClass
				});
				PropValue[][] array = mapiTable.QueryRows(1000, QueryRowsFlags.None);
				for (int i = 0; i <= array.GetUpperBound(0); i++)
				{
					if (!dsnOnly || ObjectClass.IsDsnPositive(array[i][2].Value.ToString()))
					{
						string text = array[i][1].Value.ToString();
						if ((!text.StartsWith(value, StringComparison.OrdinalIgnoreCase) || subject.StartsWith(value, StringComparison.OrdinalIgnoreCase)) && text.EndsWith(subject, StringComparison.OrdinalIgnoreCase))
						{
							byte[] bytes = array[i][0].GetBytes();
							return (MapiMessage)folder.OpenEntry(bytes);
						}
					}
				}
			}
			finally
			{
				if (contentsTable != null)
				{
					((IDisposable)contentsTable).Dispose();
				}
			}
			return null;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000E039C File Offset: 0x000DE59C
		protected static void CreateAndSubmitMessage(MapiFolder folder, string sourceMailboxName, string targetMailAddress, string subject, bool deleteAfterSubmit)
		{
			using (MapiMessage mapiMessage = folder.CreateMessage())
			{
				PropValue[] props = new PropValue[]
				{
					new PropValue(PropTag.DeleteAfterSubmit, deleteAfterSubmit),
					new PropValue(PropTag.Subject, subject),
					new PropValue(PropTag.ReceivedByEmailAddress, targetMailAddress),
					new PropValue(PropTag.Body, "This is a Test-Mailflow probe message.")
				};
				mapiMessage.SetProps(props);
				mapiMessage.ModifyRecipients(ModifyRecipientsFlags.AddRecipients, new AdrEntry[]
				{
					new AdrEntry(new PropValue[]
					{
						new PropValue(PropTag.EmailAddress, targetMailAddress),
						new PropValue(PropTag.OriginatorDeliveryReportRequested, deleteAfterSubmit),
						new PropValue(PropTag.AddrType, "SMTP"),
						new PropValue(PropTag.RecipientType, RecipientType.To),
						new PropValue(PropTag.DisplayName, sourceMailboxName)
					})
				});
				mapiMessage.SaveChanges();
				mapiMessage.SubmitMessage(SubmitMessageFlags.ForceSubmit);
			}
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000E0504 File Offset: 0x000DE704
		protected static bool IsValidPropData(PropValue[][] array, int dimOneIndex, int dimTwoSize)
		{
			for (int i = 0; i < dimTwoSize; i++)
			{
				if (array[dimOneIndex][i].IsError() || array[dimOneIndex][i].Value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000E0540 File Offset: 0x000DE740
		protected void WriteVerbose(LocalizedString text)
		{
			if (this.taskInstance != null)
			{
				this.taskInstance.WriteVerbose(text);
			}
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000E0556 File Offset: 0x000DE756
		protected void SetMonitoringDataSourceType(string source)
		{
			if (!string.IsNullOrEmpty(source))
			{
				this.monitoringDataSource = "MSExchange Monitoring Mailflow " + source;
			}
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000E0571 File Offset: 0x000DE771
		protected void AddMonitoringEvent(int id, EventTypeEnumeration type, string message)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(this.monitoringDataSource, id, type, message));
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000E0591 File Offset: 0x000DE791
		protected void AddSuccessMonitoringEvent(int id, string message)
		{
			this.AddMonitoringEvent(id, EventTypeEnumeration.Success, message);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000E059C File Offset: 0x000DE79C
		protected void AddErrorMonitoringEvent(int id, string message)
		{
			this.AddMonitoringEvent(id, EventTypeEnumeration.Error, message);
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000E05A7 File Offset: 0x000DE7A7
		protected void AddWarningMonitoringEvent(int id, string message)
		{
			this.AddMonitoringEvent(id, EventTypeEnumeration.Warning, message);
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000E05B2 File Offset: 0x000DE7B2
		protected void AddInformationMonitoringEvent(int id, string message)
		{
			this.AddMonitoringEvent(id, EventTypeEnumeration.Information, message);
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000E05BD File Offset: 0x000DE7BD
		protected void WriteErrorAndMonitoringEvent(Exception ex, ErrorCategory category, int eventId)
		{
			this.AddErrorMonitoringEvent(eventId, ex.Message);
			this.Task.WriteError(ex, category, null);
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000E05DA File Offset: 0x000DE7DA
		protected void AddPerfCounter(string counter, string instance, double value)
		{
			this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(this.monitoringDataSource, counter, instance, value));
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000E05FC File Offset: 0x000DE7FC
		protected ADSystemMailbox GetServerSystemMailbox(Server server)
		{
			ADSystemMailbox result = null;
			try
			{
				this.SourceSystemMailboxMdb = this.GetServerMdb(this.SourceMailboxServer);
				result = this.GetSystemMailboxFromMdb(this.SourceSystemMailboxMdb);
			}
			catch (MailboxServerNotHostingMdbException)
			{
				this.AddErrorMonitoringEvent(1005, Strings.TestMailflowServerWithoutMdbs(server.Name));
			}
			this.SourceSystemMailbox = result;
			return result;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000E0664 File Offset: 0x000DE864
		protected ADSystemMailbox GetSystemMailboxFromMdb(MailboxDatabase mdb)
		{
			ADSystemMailbox adsystemMailbox = null;
			GeneralMailboxIdParameter id = GeneralMailboxIdParameter.Parse(string.Format(CultureInfo.InvariantCulture, "SystemMailbox{{{0}}}", new object[]
			{
				mdb.Guid.ToString()
			}));
			IEnumerable<ADSystemMailbox> adDataObjects = this.Task.GetAdDataObjects<ADSystemMailbox>(id, this.Task.TenantGlobalCatalogSession);
			using (IEnumerator<ADSystemMailbox> enumerator = adDataObjects.GetEnumerator())
			{
				adsystemMailbox = (enumerator.MoveNext() ? enumerator.Current : null);
			}
			if (adsystemMailbox == null)
			{
				this.WriteErrorAndMonitoringEvent(new RecipientTaskException(Strings.TestMailflowNoSystemMailbox), ErrorCategory.InvalidOperation, 1005);
			}
			return adsystemMailbox;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000E0714 File Offset: 0x000DE914
		protected MailboxDatabase GetServerMdb(Server server)
		{
			MailboxDatabase[] mailboxDatabases = server.GetMailboxDatabases();
			if (mailboxDatabases.Length < 1)
			{
				this.Task.WriteError(new NoMdbForOperationException(server.Name), ErrorCategory.PermissionDenied, null);
			}
			for (int i = 0; i < mailboxDatabases.Length; i++)
			{
				if (mailboxDatabases[i].Server.ObjectGuid == server.Guid && (this.IsMdbMounted(mailboxDatabases[i], null) || i >= mailboxDatabases.Length - 1))
				{
					return mailboxDatabases[i];
				}
			}
			throw new MailboxServerNotHostingMdbException(server.Name);
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000E0794 File Offset: 0x000DE994
		protected bool IsMdbMounted(Database mdb, string fqdn)
		{
			string server = string.IsNullOrEmpty(fqdn) ? mdb.Server.Name : fqdn;
			this.WriteVerbose(Strings.VerboseConnectionAdminRpcInterface(server));
			bool result;
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", server, null, null, null))
			{
				MdbStatus[] array = exRpcAdmin.ListMdbStatus(new Guid[]
				{
					mdb.Guid
				});
				result = ((array[0].Status & MdbStatusFlags.Online) != MdbStatusFlags.Offline);
			}
			return result;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000E0824 File Offset: 0x000DEA24
		protected void OutputResult(string result, EnhancedTimeSpan latency, bool remoteTest)
		{
			this.Task.WriteObject(new TestMailflowOutput(result, latency, remoteTest));
		}

		// Token: 0x04002523 RID: 9507
		protected const string MonitoringLatencyPerfCounter = "Mailflow Latency";

		// Token: 0x04002524 RID: 9508
		private const string MonitoringSourceBase = "MSExchange Monitoring Mailflow ";

		// Token: 0x04002525 RID: 9509
		private const string MonitoringProbeNumberPerfCounter = "Probe Number";

		// Token: 0x04002526 RID: 9510
		private MonitoringData monitoringData;

		// Token: 0x04002527 RID: 9511
		private string monitoringDataSource;

		// Token: 0x04002528 RID: 9512
		private ADSystemMailbox sourceSystemMailbox;

		// Token: 0x04002529 RID: 9513
		private Server sourceMailboxServer;

		// Token: 0x0400252A RID: 9514
		private MailboxDatabase sourceSystemMailboxMdb;

		// Token: 0x0400252B RID: 9515
		private TestMailFlow task;

		// Token: 0x0400252C RID: 9516
		private bool isRemoteTest;

		// Token: 0x0400252D RID: 9517
		private TestMailFlow taskInstance;

		// Token: 0x020005F8 RID: 1528
		public static class EventId
		{
			// Token: 0x0400252E RID: 9518
			public const int TestMailflowSuccess = 1000;

			// Token: 0x0400252F RID: 9519
			public const int TestMailflowFailure = 1001;

			// Token: 0x04002530 RID: 9520
			public const int TestMailflowError = 1002;

			// Token: 0x04002531 RID: 9521
			public const int TestMailflowWarning = 1003;

			// Token: 0x04002532 RID: 9522
			public const int NodeDoesNotOwnExchangeVirtualServer = 1004;

			// Token: 0x04002533 RID: 9523
			public const int NoSystemMailboxAvailable = 1005;

			// Token: 0x04002534 RID: 9524
			public const int OperationOnInvalidServer = 1006;

			// Token: 0x04002535 RID: 9525
			public const int MapiError = 1007;

			// Token: 0x04002536 RID: 9526
			public const int InvalidEmailAddressFormat = 1008;

			// Token: 0x04002537 RID: 9527
			public const int MdbMovedWhilePerformingTest = 1009;

			// Token: 0x04002538 RID: 9528
			public const int TargetMdbInRecovery = 1010;

			// Token: 0x04002539 RID: 9529
			public const int MdbServerNotFound = 1011;

			// Token: 0x0400253A RID: 9530
			public const int CrossPremiseProbeResponseMatch = 2000;

			// Token: 0x0400253B RID: 9531
			public const int CrossPremiseProbesPending = 2001;

			// Token: 0x0400253C RID: 9532
			public const int CrossPremiseProbeNdred = 2002;

			// Token: 0x0400253D RID: 9533
			public const int CrossPremiseNoEgressTargets = 2003;

			// Token: 0x0400253E RID: 9534
			public const int CrossPremiseServerNotSelected = 2004;
		}
	}
}
