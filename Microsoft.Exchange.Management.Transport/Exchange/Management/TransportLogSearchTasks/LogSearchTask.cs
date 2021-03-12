using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.LogSearch;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200004A RID: 74
	public abstract class LogSearchTask : Task, IProgressReport
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000A660 File Offset: 0x00008860
		public LogSearchTask(LocalizedString activityName, CsvTable table, string logName)
		{
			this.activityName = activityName;
			this.table = table;
			this.logName = logName;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A6C9 File Offset: 0x000088C9
		protected CsvTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A6D1 File Offset: 0x000088D1
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000A6E8 File Offset: 0x000088E8
		[Parameter(Mandatory = false)]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000A6FB File Offset: 0x000088FB
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000A703 File Offset: 0x00008903
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A70C File Offset: 0x0000890C
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000A714 File Offset: 0x00008914
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return this.resultSize;
			}
			set
			{
				this.resultSize = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A71D File Offset: 0x0000891D
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000A725 File Offset: 0x00008925
		[Parameter(Mandatory = false)]
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A72E File Offset: 0x0000892E
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000A736 File Offset: 0x00008936
		[Parameter(Mandatory = false)]
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A73F File Offset: 0x0000893F
		public void Report(int progress)
		{
			base.WriteProgress(this.activityName, Strings.SearchStatus(this.server.ToString()), progress);
		}

		// Token: 0x060002A4 RID: 676
		protected abstract LogCondition GetCondition();

		// Token: 0x060002A5 RID: 677
		protected abstract void WriteResult(LogSearchCursor cursor);

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A760 File Offset: 0x00008960
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.server == null)
			{
				this.server = new ServerIdParameter();
			}
			if (this.adSession == null)
			{
				this.adSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 197, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\LogSearch\\LogSearchTask.cs");
			}
			IEnumerable<Server> objects = this.server.GetObjects<Server>(null, this.adSession);
			IEnumerator<Server> enumerator = objects.GetEnumerator();
			Server server = null;
			if (enumerator.MoveNext())
			{
				server = enumerator.Current;
				if (enumerator.MoveNext())
				{
					base.ThrowTerminatingError(new LocalizedException(Strings.ServerNameAmbiguous(this.server.ToString())), ErrorCategory.InvalidArgument, this.server);
				}
			}
			else
			{
				base.ThrowTerminatingError(new LocalizedException(Strings.ServerNotFound(this.server.ToString())), ErrorCategory.InvalidArgument, this.server);
			}
			if (!server.IsExchange2007OrLater)
			{
				base.ThrowTerminatingError(new LocalizedException(Strings.PreE12Server(this.server.ToString())), ErrorCategory.InvalidOperation, this.server);
			}
			else if (!server.IsHubTransportServer && !server.IsEdgeServer && !server.IsMailboxServer)
			{
				base.ThrowTerminatingError(new LocalizedException(Strings.NotTransportServer(this.server.ToString())), ErrorCategory.InvalidOperation, this.server);
			}
			else
			{
				if (string.IsNullOrEmpty(server.Fqdn))
				{
					base.ThrowTerminatingError(new LocalizedException(Strings.MissingServerFQDN(this.server.ToString())), ErrorCategory.InvalidOperation, this.server);
				}
				this.serverObject = server;
			}
			if (this.end <= this.start)
			{
				base.ThrowTerminatingError(new LocalizedException(Strings.EmptyTimeRange), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A904 File Offset: 0x00008B04
		protected override void InternalProcessRecord()
		{
			LogQuery logQuery = new LogQuery();
			logQuery.Filter = this.GetCondition();
			logQuery.Beginning = this.start.ToUniversalTime();
			logQuery.End = this.end.ToUniversalTime();
			uint num = 0U;
			try
			{
				using (LogSearchCursor logSearchCursor = new LogSearchCursor(this.table, this.serverObject.Fqdn, this.serverObject.AdminDisplayVersion, this.logName, logQuery, this))
				{
					this.cursor = logSearchCursor;
					while (logSearchCursor.MoveNext())
					{
						if (!this.resultSize.IsUnlimited && num >= this.resultSize.Value)
						{
							this.WriteWarning(Strings.WarningMoreResultsAvailable);
							break;
						}
						num += 1U;
						this.WriteResult(logSearchCursor);
					}
				}
			}
			catch (LogSearchException ex)
			{
				if (!base.Stopping || ex.ErrorCode != LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED)
				{
					this.WriteLogSearchError(ex);
				}
			}
			catch (RpcException e)
			{
				this.WriteRpcError(e);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AA18 File Offset: 0x00008C18
		protected override void InternalStopProcessing()
		{
			this.cursor.Cancel();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AA25 File Offset: 0x00008C25
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AA38 File Offset: 0x00008C38
		private void WriteLogSearchError(LogSearchException e)
		{
			LocalizedException exception;
			ErrorCategory category;
			if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_LOG_UNKNOWN_LOG)
			{
				exception = new LocalizedException(Strings.LogNotAvailable);
				category = ErrorCategory.InvalidOperation;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_UNKNOWN_SESSION_ID || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED)
			{
				exception = new LocalizedException(Strings.SearchTimeout);
				category = ErrorCategory.OperationTimeout;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_SERVER_TOO_BUSY)
			{
				exception = new LocalizedException(Strings.ServerTooBusy);
				category = ErrorCategory.ResourceUnavailable;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_INVALID_OPERAND_CLASS || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_INCOMPATIBLE_QUERY_OPERAND_TYPES)
			{
				exception = new LocalizedException(Strings.OldServerSearchLogic);
				category = ErrorCategory.InvalidOperation;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_UNRECOGNIZED_QUERY_FIELD)
			{
				exception = new LocalizedException(Strings.OldServerSchema);
				category = ErrorCategory.InvalidOperation;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_MISSING_QUERY_CONDITION || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_UNBOUND_QUERY_VARIABLE || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_DUPLICATE_BOUND_VARIABLE_DECLARATION || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_RESPONSE_OVERFLOW || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_BAD_QUERY_XML || e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_MISSING_BOUND_VARIABLE_NAME)
			{
				exception = new LocalizedException(Strings.InternalError);
				category = ErrorCategory.InvalidOperation;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_QUERY_TOO_COMPLEX)
			{
				exception = new LocalizedException(Strings.QueryTooComplex);
				category = ErrorCategory.InvalidOperation;
			}
			else
			{
				Win32Exception ex = new Win32Exception(e.ErrorCode);
				exception = new LocalizedException(Strings.GenericError(ex.Message), ex);
				category = ErrorCategory.InvalidOperation;
			}
			base.WriteError(exception, category, null);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000ABAC File Offset: 0x00008DAC
		private void WriteRpcError(RpcException e)
		{
			LocalizedException exception;
			ErrorCategory category;
			if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_ENDPOINT_NOT_REGISTERED)
			{
				exception = new LocalizedException(Strings.RpcNotRegistered(this.serverObject.Fqdn));
				category = ErrorCategory.ResourceUnavailable;
			}
			else if (e.ErrorCode == LogSearchErrorCode.LOGSEARCH_E_RPC_SERVER_UNAVAILABLE)
			{
				exception = new LocalizedException(Strings.RpcUnavailable(this.serverObject.Fqdn));
				category = ErrorCategory.InvalidOperation;
			}
			else
			{
				Win32Exception ex = new Win32Exception(e.ErrorCode);
				exception = new LocalizedException(Strings.GenericRpcError(ex.Message, this.serverObject.Fqdn), ex);
				category = ErrorCategory.InvalidOperation;
			}
			base.WriteError(exception, category, null);
		}

		// Token: 0x040000EE RID: 238
		internal IConfigurationSession adSession;

		// Token: 0x040000EF RID: 239
		private DateTime end = DateTime.UtcNow.AddDays(1.0);

		// Token: 0x040000F0 RID: 240
		private DateTime start = CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;

		// Token: 0x040000F1 RID: 241
		private LocalizedString activityName;

		// Token: 0x040000F2 RID: 242
		private readonly string logName;

		// Token: 0x040000F3 RID: 243
		private CsvTable table;

		// Token: 0x040000F4 RID: 244
		private ServerIdParameter server;

		// Token: 0x040000F5 RID: 245
		private Server serverObject;

		// Token: 0x040000F6 RID: 246
		private LogSearchCursor cursor;

		// Token: 0x040000F7 RID: 247
		private Unlimited<uint> resultSize = 1000U;
	}
}
