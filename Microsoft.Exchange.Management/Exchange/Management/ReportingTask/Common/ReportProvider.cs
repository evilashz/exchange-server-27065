using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ReportingWebService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.ReportingTask.Query;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006AB RID: 1707
	internal class ReportProvider<TReportObject> where TReportObject : ReportObject
	{
		// Token: 0x06003C6E RID: 15470 RVA: 0x00101546 File Offset: 0x000FF746
		public ReportProvider(ITaskContext taskContext, IReportContextFactory reportContextFactory)
		{
			this.queryDecorators = new List<QueryDecorator<TReportObject>>();
			this.TaskContext = taskContext;
			this.reportContextFactory = reportContextFactory;
			this.expressionDecorator = new ExpressionDecorator<TReportObject>(this.TaskContext);
			this.AddQueryDecorator(this.expressionDecorator);
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06003C6F RID: 15471 RVA: 0x00101584 File Offset: 0x000FF784
		// (remove) Token: 0x06003C70 RID: 15472 RVA: 0x001015BC File Offset: 0x000FF7BC
		public event Action<TReportObject> ReportReceived;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06003C71 RID: 15473 RVA: 0x001015F4 File Offset: 0x000FF7F4
		// (remove) Token: 0x06003C72 RID: 15474 RVA: 0x0010162C File Offset: 0x000FF82C
		public event Action<string, string> StatementLogged;

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x00101661 File Offset: 0x000FF861
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x0010166E File Offset: 0x000FF86E
		public Expression Expression
		{
			get
			{
				return this.expressionDecorator.Expression;
			}
			set
			{
				this.expressionDecorator.Expression = value;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x0010167C File Offset: 0x000FF87C
		public bool IsExpressionEnforced
		{
			get
			{
				return this.Expression != null;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x0010168A File Offset: 0x000FF88A
		// (set) Token: 0x06003C77 RID: 15479 RVA: 0x00101692 File Offset: 0x000FF892
		private protected ITaskContext TaskContext { protected get; private set; }

		// Token: 0x06003C78 RID: 15480 RVA: 0x0010169C File Offset: 0x000FF89C
		protected IDbConnection OpenConnections(IDbConnection connection, IDbConnection connectionBackup)
		{
			try
			{
				this.LogSqlConnection(connection);
				connection.Open();
				return connection;
			}
			catch (SqlException sqlException)
			{
				if (connectionBackup != null)
				{
					try
					{
						this.HandleSqlConnectionFailOverWarning(connection, sqlException, connectionBackup);
						this.LogSqlConnection(connectionBackup);
						connectionBackup.Open();
						return connectionBackup;
					}
					catch (SqlException sqlException2)
					{
						this.HandleSqlConnectionError(connectionBackup, sqlException2);
						goto IL_4E;
					}
					catch (InvalidOperationException invalidOperationException)
					{
						this.HandleTimeoutError(invalidOperationException);
						throw;
					}
				}
				this.HandleSqlConnectionError(connection, sqlException);
				IL_4E:;
			}
			catch (InvalidOperationException invalidOperationException2)
			{
				this.HandleTimeoutError(invalidOperationException2);
				throw;
			}
			return null;
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x0010173C File Offset: 0x000FF93C
		public void Execute()
		{
			try
			{
				ReportingTaskFaultInjection.FaultInjectionTracer.TraceTest(4140182845U);
				using (IDbConnection dbConnection = this.reportContextFactory.CreateConnection(false))
				{
					using (IDbConnection dbConnection2 = this.reportContextFactory.CreateConnection(true))
					{
						IDbConnection connection = this.OpenConnections(dbConnection, dbConnection2);
						using (IReportContext reportContext = this.reportContextFactory.CreateReportContext(connection))
						{
							IQueryable<TReportObject> reportQuery = this.GetReportQuery(reportContext);
							this.LogSqlStatement(reportContext, reportQuery, 1);
							foreach (TReportObject reportObject in reportQuery)
							{
								this.RaiseReporReceivedtEvent(reportObject);
							}
						}
					}
				}
			}
			catch (SqlException sqlException)
			{
				this.HandleSqlError(sqlException);
			}
			catch (SqlTypeException sqlTypeException)
			{
				this.HandleSqlError(sqlTypeException);
			}
			catch (ArgumentException argumentException)
			{
				this.HandleLinqError(argumentException);
			}
			catch (InvalidOperationException invalidOperationException)
			{
				this.HandleTimeoutError(invalidOperationException);
				throw;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x00101888 File Offset: 0x000FFA88
		public void AddQueryDecorator(QueryDecorator<TReportObject> queryDecorator)
		{
			if (!this.queryDecorators.Contains(queryDecorator))
			{
				this.queryDecorators.Add(queryDecorator);
			}
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x001018A4 File Offset: 0x000FFAA4
		public void Validate(bool isPipeline)
		{
			foreach (QueryDecorator<TReportObject> queryDecorator in this.queryDecorators)
			{
				if (queryDecorator.IsPipeline == isPipeline)
				{
					queryDecorator.Validate();
				}
			}
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x00101900 File Offset: 0x000FFB00
		protected virtual IQueryable<TReportObject> GetReportQuery(IReportContext reportContext)
		{
			IQueryable<TReportObject> reports = reportContext.GetReports<TReportObject>();
			return this.DecorateQuery(reports);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x00101920 File Offset: 0x000FFB20
		private void OpenConnection(IDbConnection connection)
		{
			try
			{
				this.LogSqlConnection(connection);
				connection.Open();
			}
			catch (SqlException sqlException)
			{
				this.HandleSqlConnectionError(connection, sqlException);
			}
			catch (InvalidOperationException invalidOperationException)
			{
				this.HandleTimeoutError(invalidOperationException);
				throw;
			}
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x00101970 File Offset: 0x000FFB70
		private void RaiseReporReceivedtEvent(TReportObject reportObject)
		{
			if (this.ReportReceived != null)
			{
				this.ReportReceived(reportObject);
			}
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x001019AC File Offset: 0x000FFBAC
		protected IQueryable<TReportObject> DecorateQuery(IQueryable<TReportObject> query)
		{
			IOrderedEnumerable<QueryDecorator<TReportObject>> source = from decorator in this.queryDecorators
			where !this.IsExpressionEnforced || decorator.IsEnforced
			orderby decorator.QueryOrder
			select decorator;
			return source.Aggregate(query, (IQueryable<TReportObject> current, QueryDecorator<TReportObject> decorator) => decorator.GetQuery(current));
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x00101A1C File Offset: 0x000FFC1C
		private void LogSqlConnection(IDbConnection connection)
		{
			ExTraceGlobals.ReportingWebServiceTracer.Information<string>(0L, "SQL Connection: {0}", connection.ConnectionString);
			this.TaskContext.WriteVerbose(Strings.InformationSqlConnection(connection.ConnectionString));
			if (this.StatementLogged != null)
			{
				this.StatementLogged("SQLConnection", Strings.InformationSqlConnection(connection.ConnectionString));
			}
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x00101A80 File Offset: 0x000FFC80
		protected void LogSqlStatement(IReportContext reportContext, IQueryable<TReportObject> query, int logTag)
		{
			string text = query.Expression.ToString();
			string sqlCommandText = reportContext.GetSqlCommandText(query);
			this.TaskContext.WriteVerbose(Strings.InformationSqlStatement(sqlCommandText));
			this.TaskContext.WriteVerbose(Strings.InformationQueryExpression(text));
			ExTraceGlobals.ReportingWebServiceTracer.Information<string>(0L, "SQL Query: {0}", sqlCommandText);
			ExTraceGlobals.ReportingWebServiceTracer.Information<string>(0L, "Expression: {0}", text);
			if (this.StatementLogged != null)
			{
				this.StatementLogged("SQLStatement" + logTag.ToString(), Strings.InformationSqlStatement(sqlCommandText));
				this.StatementLogged("QueryExpression" + logTag.ToString(), Strings.InformationQueryExpression(text));
			}
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x00101B40 File Offset: 0x000FFD40
		private void HandleSqlConnectionFailOverWarning(IDbConnection connection, SqlException sqlException, IDbConnection backupConnection)
		{
			try
			{
				this.TraceSqlException(sqlException);
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_DataMartConnectionFailOverToBackupServer, new string[]
				{
					connection.ConnectionString,
					backupConnection.ConnectionString,
					sqlException.Number.ToString(CultureInfo.InvariantCulture),
					sqlException.Message
				});
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x00101BAC File Offset: 0x000FFDAC
		private void HandleSqlConnectionError(IDbConnection connection, SqlException sqlException)
		{
			this.TraceSqlException(sqlException);
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_DataMartConnectionFailed, new string[]
			{
				connection.ConnectionString,
				sqlException.Number.ToString(CultureInfo.InvariantCulture),
				sqlException.Message
			});
			LocalizedException localizedException = SqlErrorHandler.TrasnlateConnectionError(sqlException);
			this.TaskContext.WriteError(localizedException, ExchangeErrorCategory.ServerOperation, null);
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x00101C14 File Offset: 0x000FFE14
		private void HandleLinqError(ArgumentException argumentException)
		{
			ExTraceGlobals.ReportingWebServiceTracer.TraceError<string>(0L, "LinqException. Message: {0}", argumentException.Message);
			LocalizedException localizedException = new InvalidQueryException(0, argumentException);
			this.TaskContext.WriteError(localizedException, ExchangeErrorCategory.ServerOperation, null);
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x00101C54 File Offset: 0x000FFE54
		private void HandleSqlError(SqlException sqlException)
		{
			this.TraceSqlException(sqlException);
			if (SqlErrorHandler.IsObjectNotFoundError(sqlException))
			{
				this.TaskContext.WriteWarning(Strings.WarningReportNotAvailable);
				return;
			}
			LocalizedException localizedException = SqlErrorHandler.TrasnlateError(sqlException);
			this.TaskContext.WriteError(localizedException, ExchangeErrorCategory.ServerOperation, null);
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x00101C9C File Offset: 0x000FFE9C
		private void HandleTimeoutError(InvalidOperationException invalidOperationException)
		{
			if (invalidOperationException.StackTrace.Contains("System.Data.SqlClient"))
			{
				LocalizedException localizedException = new DataMartTimeoutException(invalidOperationException);
				this.TaskContext.WriteError(localizedException, ExchangeErrorCategory.ServerOperation, null);
			}
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x00101CD4 File Offset: 0x000FFED4
		private void HandleSqlError(SqlTypeException sqlTypeException)
		{
			ExTraceGlobals.ReportingWebServiceTracer.TraceError<string>(0L, "SqlTypeException. Message: {0}", sqlTypeException.Message);
			LocalizedException localizedException = SqlErrorHandler.TrasnlateError(sqlTypeException);
			this.TaskContext.WriteError(localizedException, ExchangeErrorCategory.ServerOperation, null);
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x00101D14 File Offset: 0x000FFF14
		private void TraceSqlException(SqlException sqlException)
		{
			if (ExTraceGlobals.ReportingWebServiceTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.ReportingWebServiceTracer.TraceError(0L, "SqlException. Class: {0}, Number: {1}, Procedure: {2}, LineNumber: {3}, Server: {4}, Source: {5}, State: {6}, Message: {7}", new object[]
				{
					sqlException.Class,
					sqlException.Number,
					sqlException.Procedure,
					sqlException.LineNumber,
					sqlException.Server,
					sqlException.Source,
					sqlException.State,
					sqlException.Message
				});
			}
		}

		// Token: 0x04002736 RID: 10038
		protected readonly List<QueryDecorator<TReportObject>> queryDecorators;

		// Token: 0x04002737 RID: 10039
		private readonly IReportContextFactory reportContextFactory;

		// Token: 0x04002738 RID: 10040
		private readonly ExpressionDecorator<TReportObject> expressionDecorator;
	}
}
