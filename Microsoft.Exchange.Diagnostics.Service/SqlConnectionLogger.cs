using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Exchange.Diagnostics.Service.Common;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000022 RID: 34
	public class SqlConnectionLogger : IDisposable
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public SqlConnectionLogger(string logPrefix)
		{
			this.logPrefix = (string.IsNullOrEmpty(logPrefix) ? string.Empty : logPrefix);
			this.sessionIds = new Dictionary<SqlConnection, string>();
			this.sessionIdsLock = new object();
			this.connectionStateCommand = new SqlCommand("sqlContext");
			this.connectionStateCommand.CommandType = CommandType.StoredProcedure;
			SqlParameter sqlParameter = new SqlParameter("@context", SqlDbType.NVarChar, 36);
			sqlParameter.Direction = ParameterDirection.Output;
			this.connectionStateCommand.Parameters.Add(sqlParameter);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007B7C File Offset: 0x00005D7C
		public static string SafeConnectionString(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				return string.Empty;
			}
			return new SqlConnectionStringBuilder(connectionString)
			{
				Password = string.Empty
			}.ConnectionString;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007BAF File Offset: 0x00005DAF
		public virtual SqlConnectionLogger Clone(string logPrefix)
		{
			return new SqlConnectionLogger(logPrefix);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public string CreateSqlExceptionErrorMessage(SqlException exception, SqlConnection connection, string contextMessage)
		{
			return string.Format("{0}: SqlException for connection string='{1}'; GUID='{2}'; Exception Number='{3}:{4}'; Message='{5}'; Inner Exception='{6}'.", new object[]
			{
				this.logPrefix,
				SqlConnectionLogger.SafeConnectionString(connection.ConnectionString),
				this.SessionId(connection),
				(int)exception.Class,
				exception.Number,
				contextMessage,
				exception
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007C1C File Offset: 0x00005E1C
		public bool HandleException(SqlException exception, SqlConnection connection, ref string lastException)
		{
			int number = exception.Number;
			if (number == 245 || number == 295)
			{
				lastException = this.CreateSqlExceptionErrorMessage(exception, connection, "command.ExecuteNonQuery() failed. Will not retry.");
				Logger.LogErrorMessage("{0}", new object[]
				{
					lastException
				});
				throw new InvalidOperationException(lastException, exception);
			}
			if (number == 2627)
			{
				lastException = this.CreateSqlExceptionErrorMessage(exception, connection, "command.ExecuteNonQuery() failed for '{0}'. Primary Key violation. Will ignore this batch.");
				Logger.LogErrorMessage("{0}", new object[]
				{
					lastException
				});
				return false;
			}
			if (number == 229)
			{
				lastException = this.CreateSqlExceptionErrorMessage(exception, connection, "command.ExecuteNonQuery() failed. The EXECUTE permission was denied. Will reload connectionString and retry.");
				Logger.LogErrorMessage("{0}", new object[]
				{
					lastException
				});
			}
			else
			{
				lastException = this.CreateSqlExceptionErrorMessage(exception, connection, "command.ExecuteNonQuery() failed. Performing retry logic.");
				Logger.LogErrorMessage("{0}", new object[]
				{
					lastException
				});
			}
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007CF9 File Offset: 0x00005EF9
		public void Dispose()
		{
			this.InternalDispose(true);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007D04 File Offset: 0x00005F04
		public virtual void OpenConnection(SqlConnection connection, bool enableStatistics)
		{
			if (enableStatistics)
			{
				connection.StatisticsEnabled = true;
				connection.ResetStatistics();
			}
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
				Logger.LogInformationMessage("{0}: connection.Open() successful for connection='{1}' GUID='{2}'.", new object[]
				{
					this.logPrefix,
					SqlConnectionLogger.SafeConnectionString(connection.ConnectionString),
					this.SessionId(connection)
				});
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007D64 File Offset: 0x00005F64
		public virtual void CloseConnection(SqlConnection connection)
		{
			connection.StatisticsEnabled = false;
			if (connection.State != ConnectionState.Closed)
			{
				try
				{
					string text = this.SessionId(connection);
					connection.Close();
					Logger.LogInformationMessage("{0}: connection.Close() successful for connection GUID='{1}'.", new object[]
					{
						this.logPrefix,
						text
					});
				}
				catch (SqlException exception)
				{
					string text2 = this.CreateSqlExceptionErrorMessage(exception, connection, "connection.Close() failed.");
					Logger.LogErrorMessage("{0}", new object[]
					{
						text2
					});
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007DEC File Offset: 0x00005FEC
		public virtual void ExecuteNonQuery(SqlCommand command)
		{
			command.ExecuteNonQuery();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007DF8 File Offset: 0x00005FF8
		public long GetBytesSent(SqlConnection connection)
		{
			IDictionary dictionary = connection.RetrieveStatistics();
			return (long)dictionary["BytesSent"];
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007E20 File Offset: 0x00006020
		public SqlConnection BuildSqlConnection(string connectionString)
		{
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.StateChange += this.OnConnectionStateChange;
			sqlConnection.InfoMessage += this.OnInfoMessage;
			return sqlConnection;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007E5C File Offset: 0x0000605C
		private void InternalDispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (disposing)
				{
					if (this.connectionStateCommand != null)
					{
						if (this.connectionStateCommand.Connection != null)
						{
							this.connectionStateCommand.Connection.Dispose();
						}
						this.connectionStateCommand.Dispose();
					}
					if (this.sessionIds != null)
					{
						foreach (SqlConnection sqlConnection in this.sessionIds.Keys)
						{
							if (sqlConnection != null)
							{
								sqlConnection.Dispose();
							}
						}
						this.sessionIds.Clear();
					}
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007F18 File Offset: 0x00006118
		private string SessionId(SqlConnection connection)
		{
			string result;
			lock (this.sessionIdsLock)
			{
				if (!this.sessionIds.TryGetValue(connection, out result))
				{
					return string.Empty;
				}
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007F70 File Offset: 0x00006170
		private void OnConnectionStateChange(object sender, StateChangeEventArgs evtArgs)
		{
			SqlConnection sqlConnection = (SqlConnection)sender;
			ConnectionState currentState = evtArgs.CurrentState;
			switch (currentState)
			{
			case ConnectionState.Closed:
				break;
			case ConnectionState.Open:
				goto IL_56;
			default:
				if (currentState != ConnectionState.Broken)
				{
					return;
				}
				break;
			}
			lock (this.sessionIdsLock)
			{
				this.sessionIds.Remove(sqlConnection);
				return;
			}
			IL_56:
			lock (this.sessionIdsLock)
			{
				this.sessionIds.Remove(sqlConnection);
			}
			string text;
			try
			{
				this.connectionStateCommand.Connection = sqlConnection;
				this.ExecuteNonQuery(this.connectionStateCommand);
				text = this.connectionStateCommand.Parameters["@context"].Value.ToString();
			}
			catch (SqlException exception)
			{
				string text2 = this.CreateSqlExceptionErrorMessage(exception, sqlConnection, "Exception thrown when trying to get the session ID.");
				Logger.LogErrorMessage("{0}", new object[]
				{
					text2
				});
				throw;
			}
			lock (this.sessionIdsLock)
			{
				this.sessionIds[sqlConnection] = text;
			}
			Logger.LogInformationMessage("{0}: Connection.Open() successful for connection string '{1}' SQL Azure session ID '{2}'", new object[]
			{
				this.logPrefix,
				SqlConnectionLogger.SafeConnectionString(sqlConnection.ConnectionString),
				text
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000080FC File Offset: 0x000062FC
		private void OnInfoMessage(object sender, SqlInfoMessageEventArgs args)
		{
			SqlConnection sqlConnection = (SqlConnection)sender;
			Logger.LogErrorMessage("{0}: Message returned from connection to DB '{1}' : '{2}'", new object[]
			{
				this.logPrefix,
				sqlConnection.Database,
				args.Message
			});
			foreach (object obj in args.Errors)
			{
				SqlError sqlError = (SqlError)obj;
				Logger.LogErrorMessage("{0}: The '{1}' has received a severity '{2}', state '{3}' error number '{4}' on line '{5}' of procedure '{6}' on server '{7}':\n'{8}'", new object[]
				{
					this.logPrefix,
					sqlError.Source ?? string.Empty,
					sqlError.Class,
					sqlError.State,
					sqlError.Number,
					sqlError.LineNumber,
					sqlError.Procedure ?? string.Empty,
					sqlError.Server ?? string.Empty,
					sqlError.Message ?? string.Empty
				});
			}
		}

		// Token: 0x04000077 RID: 119
		private const string ConnectionStateParameterName = "@context";

		// Token: 0x04000078 RID: 120
		private readonly Dictionary<SqlConnection, string> sessionIds;

		// Token: 0x04000079 RID: 121
		private readonly object sessionIdsLock;

		// Token: 0x0400007A RID: 122
		private readonly SqlCommand connectionStateCommand;

		// Token: 0x0400007B RID: 123
		private readonly string logPrefix;

		// Token: 0x0400007C RID: 124
		private volatile bool disposed;

		// Token: 0x02000023 RID: 35
		private enum SqlExceptionNumber
		{
			// Token: 0x0400007E RID: 126
			ConnectionStringFailed = 18456,
			// Token: 0x0400007F RID: 127
			TypeConversionFailed = 245,
			// Token: 0x04000080 RID: 128
			ParameterConversionFailed = 295,
			// Token: 0x04000081 RID: 129
			PrimaryKeyViolation = 2627,
			// Token: 0x04000082 RID: 130
			ExecutePermissionDenied = 229
		}

		// Token: 0x02000024 RID: 36
		public class Null : SqlConnectionLogger
		{
			// Token: 0x060000B4 RID: 180 RVA: 0x00008234 File Offset: 0x00006434
			public Null(string logPrefix) : base(logPrefix)
			{
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x0000823D File Offset: 0x0000643D
			public override SqlConnectionLogger Clone(string logPrefix)
			{
				return new SqlConnectionLogger.Null(logPrefix);
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00008245 File Offset: 0x00006445
			public override void OpenConnection(SqlConnection connection, bool enableStatistics)
			{
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00008247 File Offset: 0x00006447
			public override void CloseConnection(SqlConnection connection)
			{
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x0000824C File Offset: 0x0000644C
			public override void ExecuteNonQuery(SqlCommand command)
			{
				string commandText;
				if ((commandText = command.CommandText) != null)
				{
					if (commandText == "sqlContext")
					{
						command.Parameters["@context"].Value = Guid.Empty.ToString();
						return;
					}
					if (commandText == "[dbo].[uploadMachinePerfData]" || commandText == "[EDSPart].[sp_uploadMachineStateData]")
					{
						return;
					}
					if (commandText == "[EDSPart].[sp_getPartition]")
					{
						command.Parameters[3].Value = command.Connection.ConnectionString;
						command.Parameters[4].Value = DateTime.UtcNow.AddDays(1.0).ToString();
						return;
					}
				}
				throw new NotImplementedException();
			}
		}
	}
}
