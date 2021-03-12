using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Timers;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000025 RID: 37
	public class SqlOutputStream : OutputStream, IMultipleOutputStream
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00008324 File Offset: 0x00006524
		public SqlOutputStream(OutputStream serviceOutputStream, bool uploadSql, string name, string analyzerLogDirectoryMask, SqlOutputStream.Configuration configuration) : base(name)
		{
			if (serviceOutputStream == null)
			{
				throw new ArgumentNullException("serviceOutputStream");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			Logger.LogInformationMessage("SqlOutputStream: Initializing SqlOutputStream: name='{0}'; analyzerLogDirectoryMask='{1}'", new object[]
			{
				name,
				analyzerLogDirectoryMask
			});
			Logger.LogInformationMessage("SqlOutputStream: Configuration: maxBufferRows='{0}'; maxBufferTime='{1}'; performanceDataUploadTimeoutSeconds='{2}'; logRetryEventInterval='{3}'", new object[]
			{
				configuration.MaxBufferRows,
				configuration.MaxBufferTime,
				configuration.PerformanceDataUploadTimeoutSeconds,
				configuration.LogRetryEventInterval
			});
			this.serviceOutputStream = serviceOutputStream;
			this.fileChunkOutputStream = (serviceOutputStream as FileChunkOutputStream);
			this.logRetryEventInterval = configuration.LogRetryEventInterval;
			this.random = new Random();
			this.ResetCounters();
			this.lastReloadConnectionString = DateTime.MinValue;
			this.avgUploadLatencyForLastHundredBatches = new SampleAverage(100);
			this.avgConnectionOpenLatencyForLastHundredBatches = new SampleAverage(100);
			this.avgUploadSizeForLastHundredBatches = new SampleAverage(100);
			this.batchesUploadedForTheLastHourTimeStamp = new Queue<DateTime>();
			this.flusherSleepWaitHandle = new AutoResetEvent(false);
			this.flushingFinishWaitHandle = new AutoResetEvent(false);
			this.connectionStringManagerWaitHandle = new AutoResetEvent(false);
			this.getPartitionWaitHandle = new AutoResetEvent(false);
			this.maxBufferRows = configuration.MaxBufferRows;
			this.maxBufferTime = configuration.MaxBufferTime;
			this.analyzerLogDirectoryMask = analyzerLogDirectoryMask;
			this.flushingLock = new object();
			this.flusherStopped = false;
			this.sqlConnectionLogger = (uploadSql ? new SqlConnectionLogger(base.Name) : new SqlConnectionLogger.Null(base.Name));
			this.perfLogCommand = SqlOutputStream.BuildSqlCommand("[dbo].[uploadMachinePerfData]", SqlOutputStream.PerfLogUploadDefinitions, configuration.PerformanceDataUploadTimeoutSeconds);
			MachineInformationSource.MachineInformation current = MachineInformationSource.MachineInformation.GetCurrent();
			this.currentMachineName = current.MachineName;
			this.currentSiteName = current.SiteName;
			this.environmentMachineName = Environment.MachineName;
			this.connectionStringManager = new SqlOutputStream.ConnectionStringManager(this);
			this.managedConnections = new Dictionary<string, ManagedConnection>();
			this.connectionsToRemove = new List<string>();
			this.flusherSleepWaitHandle.Set();
			this.flushingThread = new Thread(new ThreadStart(this.Flusher));
			this.flushingThread.Name = "Flusher thread in SqlOutputStream";
			this.flushingThread.Start();
			this.machineStream = new SqlOutputStream.MachineSqlOutputStream(this);
			this.tieredStreams = new SqlOutputStream.TieredSqlOutputStream[5];
			for (int i = 0; i < this.tieredStreams.Length; i++)
			{
				this.tieredStreams[i] = new SqlOutputStream.TieredSqlOutputStream(this, i);
			}
			this.performanceCounterLock = new object();
			this.performanceCounterTimer = new System.Timers.Timer(100.0);
			this.performanceCounterTimer.Elapsed += this.UpdateCountersEvent;
			this.performanceCounterTimer.SynchronizingObject = null;
			this.performanceCounterTimer.Start();
			Logger.LogInformationMessage("SqlOutputStream: Initializing SqlOutputStream successful.", new object[0]);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000085EF File Offset: 0x000067EF
		internal int SuccessfulUploadsInLastHundredBatches
		{
			get
			{
				return this.successfulUploadsInLastHundredBatches;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000085F8 File Offset: 0x000067F8
		internal int BufferRowCount
		{
			get
			{
				int num = 0;
				foreach (ManagedConnection managedConnection in this.managedConnections.Values)
				{
					foreach (DataTable dataTable in managedConnection.Buffers.Values)
					{
						num += dataTable.Rows.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000869C File Offset: 0x0000689C
		private SqlOutputStream.ConnectionStringManager EDSConnectionStringManager
		{
			get
			{
				SqlOutputStream.ConnectionStringManager result;
				lock (this.flushingLock)
				{
					if (DateTime.UtcNow - this.lastReloadConnectionString > TimeSpan.FromDays(1.0) && !this.flusherStopped)
					{
						this.ReloadConnectionString();
						while (!this.connectionStringManager.ValidateConnectionString() && !this.flusherStopped)
						{
							this.connectionStringManagerWaitHandle.WaitOne(SqlOutputStream.ReloadConnectionStringInterval);
							this.ReloadConnectionString();
						}
						this.lastReloadConnectionString = DateTime.UtcNow;
					}
					result = this.connectionStringManager;
				}
				return result;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008750 File Offset: 0x00006950
		public static SqlCommand BuildSqlCommand(string name, object[,] definitions, int commandTimeout = 30)
		{
			SqlCommand sqlCommand = new SqlCommand(name);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandTimeout = commandTimeout;
			int length = definitions.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				SqlParameter sqlParameter = new SqlParameter((string)definitions[i, 0], (SqlDbType)definitions[i, 3]);
				sqlParameter.Direction = (ParameterDirection)definitions[i, 4];
				sqlCommand.Parameters.Add(sqlParameter);
			}
			return sqlCommand;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000087C7 File Offset: 0x000069C7
		public static SqlConnectionStringBuilder BuildSqlConnectionStringBuilder(string connectionString)
		{
			return new SqlConnectionStringBuilder(connectionString);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000087D0 File Offset: 0x000069D0
		public OutputStream OpenOutputStream(string analyzerName, string outputFormatName, string streamName)
		{
			if (!analyzerName.Contains("Tier"))
			{
				if (streamName != null)
				{
					if (streamName == "MachineInformation")
					{
						return this.machineStream;
					}
					if (streamName == "Tier2")
					{
						return this.tieredStreams[2];
					}
					if (streamName == "Tier3")
					{
						return this.tieredStreams[3];
					}
					if (streamName == "Tier4")
					{
						return this.tieredStreams[4];
					}
				}
				return this.tieredStreams[1];
			}
			if (this.fileChunkOutputStream != null)
			{
				return this.fileChunkOutputStream.OpenOutputStream(analyzerName, outputFormatName, streamName);
			}
			return this.serviceOutputStream;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008870 File Offset: 0x00006A70
		internal static string[] FindMatchingConnectionStringForSchema(IList<string> connectionStrings, string site)
		{
			if (string.IsNullOrEmpty(site) || site.Equals("unknown", StringComparison.OrdinalIgnoreCase))
			{
				site = string.Empty;
			}
			string[] array = new string[5];
			array[0] = string.Empty;
			foreach (string text in connectionStrings)
			{
				bool flag = false;
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
				try
				{
					dbConnectionStringBuilder.ConnectionString = text;
				}
				catch (ArgumentException ex)
				{
					Logger.LogErrorMessage("SqlOutputStream: Connection string is invalid. Failed to convert to DbConnectionStringBuilder. Connection string: '{0}'.  Exception '{1}'", new object[]
					{
						text,
						ex
					});
					continue;
				}
				if (dbConnectionStringBuilder.ContainsKey("GUID"))
				{
					dbConnectionStringBuilder.Remove("GUID");
				}
				object obj;
				if (dbConnectionStringBuilder.TryGetValue("EdsSqlSchemaVersion", out obj))
				{
					if (obj.ToString().StartsWith("3.0"))
					{
						dbConnectionStringBuilder.Remove("EdsSqlSchemaVersion");
						int num = 0;
						object obj2;
						if (dbConnectionStringBuilder.TryGetValue("Tier", out obj2))
						{
							if (!int.TryParse(obj2.ToString(), out num) || num <= 0 || num > 4)
							{
								Logger.LogErrorMessage("SqlOutputStream: Connection string is invalid. Tier must be between 1 and {1}: Connection string: '{0}'", new object[]
								{
									dbConnectionStringBuilder.ConnectionString,
									4
								});
								continue;
							}
							dbConnectionStringBuilder.Remove("Tier");
						}
						object obj3;
						if (dbConnectionStringBuilder.TryGetValue("Site", out obj3))
						{
							flag = obj3.ToString().Equals(site, StringComparison.OrdinalIgnoreCase);
							if (!flag)
							{
								continue;
							}
							dbConnectionStringBuilder.Remove("Site");
						}
						SqlConnectionStringBuilder sqlConnectionStringBuilder = null;
						try
						{
							sqlConnectionStringBuilder = SqlOutputStream.BuildSqlConnectionStringBuilder(dbConnectionStringBuilder.ConnectionString);
							sqlConnectionStringBuilder.PersistSecurityInfo = true;
						}
						catch (ArgumentException ex2)
						{
							Logger.LogErrorMessage("SqlOutputStream: Connection string is invalid. Failed to convert to SqlConnectionStringBuilder. Connection string: '{0}'.  Exception '{1}'", new object[]
							{
								dbConnectionStringBuilder.ConnectionString,
								ex2
							});
							continue;
						}
						Logger.LogInformationMessage("SqlOutputStream: Found a matching connectionString: '{0}'", new object[]
						{
							SqlConnectionLogger.SafeConnectionString(sqlConnectionStringBuilder.ConnectionString)
						});
						SecureString secureString;
						if (ManageEdsConnectionStrings.DkmDecryptString(sqlConnectionStringBuilder.Password, out secureString))
						{
							try
							{
								IntPtr ptr = Marshal.SecureStringToBSTR(secureString);
								sqlConnectionStringBuilder.Password = Marshal.PtrToStringUni(ptr);
								goto IL_24B;
							}
							finally
							{
								secureString.Dispose();
								secureString = null;
							}
							goto IL_219;
							IL_24B:
							if (string.IsNullOrEmpty(array[num]) || flag)
							{
								array[num] = sqlConnectionStringBuilder.ConnectionString;
								continue;
							}
							continue;
						}
						IL_219:
						Logger.LogErrorMessage("SqlOutputStream: Unable to decrypt the password.", new object[0]);
						Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamDecryptFailed, new object[]
						{
							sqlConnectionStringBuilder.ConnectionString
						});
					}
				}
				else
				{
					Logger.LogErrorMessage("SqlOutputStream: Connection string is invalid. Missing 'EdqSqlSchemaVersion'. Connection string: '{0}'", new object[]
					{
						dbConnectionStringBuilder.ConnectionString
					});
				}
			}
			if (!string.IsNullOrEmpty(array[0]))
			{
				return array;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("SqlOutputStream: Cannot find connection string that matches site '{0}' with version '{1}' in the list of connection strings. Count: '{2}' Value(s): ", site, "3.0", connectionStrings.Count);
			foreach (string value in connectionStrings)
			{
				stringBuilder.Append("'");
				stringBuilder.Append(value);
				stringBuilder.Append("',");
			}
			string text2 = stringBuilder.ToString();
			Logger.LogErrorMessage("{0}", new object[]
			{
				text2
			});
			Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamConnectionStringFromAdNotFound, new object[]
			{
				text2
			});
			return array;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008C3C File Offset: 0x00006E3C
		internal void BatchPerflogDataTable(SqlOutputStream.PerformanceDataTable perfLogData)
		{
			foreach (object obj in perfLogData.Rows)
			{
				DataRow row = (DataRow)obj;
				this.BatchPerfLogData(perfLogData.Tier, perfLogData.MachineName, null, row);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008CA4 File Offset: 0x00006EA4
		internal void BatchPerfLogData(int tier, string csvLine)
		{
			string machineName;
			List<string> csvColumns;
			if (this.CsvLineToColumns(csvLine, out machineName, out csvColumns))
			{
				this.BatchPerfLogData(tier, machineName, csvColumns, null);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00008CC8 File Offset: 0x00006EC8
		internal void BatchPerfLogData(int tier, string machineName, List<string> csvColumns, DataRow row)
		{
			DateTime dateTime;
			if (row == null)
			{
				if (!DateTime.TryParse(csvColumns[0], CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dateTime))
				{
					string text = string.Format("SqlOutputStream: This row has an invalid SampleTimeStart value: '{0}'. This line will not be added to the batch.", csvColumns[0]);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text
					});
					return;
				}
			}
			else
			{
				dateTime = (DateTime)row[0];
			}
			if (string.Equals(machineName, this.environmentMachineName, StringComparison.OrdinalIgnoreCase))
			{
				machineName = this.currentMachineName;
			}
			while (!this.flusherStopped)
			{
				string connectionString = this.GetConnectionString(machineName, tier, dateTime);
				if (string.IsNullOrEmpty(connectionString))
				{
					Logger.LogErrorMessage("SqlOutputStream: connectionString is null or empty, received a row after flusher already stopped. We will not process this line with time: '{0}'", new object[]
					{
						dateTime.ToString()
					});
					return;
				}
				ManagedConnection managedConnection;
				SqlOutputStream.PerformanceDataTable buffer;
				lock (this.flushingLock)
				{
					string text2 = SqlConnectionLogger.SafeConnectionString(connectionString);
					if (!this.managedConnections.TryGetValue(text2, out managedConnection))
					{
						Logger.LogInformationMessage("SqlOutputStream: BatchPerfLogData: Loaded new connection string '{0}' for timestamp '{1}'", new object[]
						{
							text2,
							dateTime
						});
						SqlConnection connection = this.sqlConnectionLogger.BuildSqlConnection(connectionString);
						managedConnection = new ManagedConnection(connection, tier);
						buffer = managedConnection.GetBuffer(machineName);
						this.managedConnections.Add(text2, managedConnection);
						this.flusherSleepWaitHandle.Set();
					}
					buffer = managedConnection.GetBuffer(machineName);
				}
				if (!managedConnection.FlusherExecuted)
				{
					Logger.LogInformationMessage("SqlOutputStream: Sleeping until the flusher successfully flushes.", new object[0]);
					this.flushingFinishWaitHandle.WaitOne(this.logRetryEventInterval);
				}
				if (managedConnection.FlusherExecuted)
				{
					bool flag2 = false;
					if (!this.flusherStopped)
					{
						lock (this.flushingLock)
						{
							if (row == null)
							{
								if (!this.AddCsvColsToTable(csvColumns, buffer))
								{
									return;
								}
							}
							else
							{
								buffer.ImportRow(row);
							}
							int bufferRowCount = this.BufferRowCount;
							if (bufferRowCount >= this.maxBufferRows)
							{
								Logger.LogInformationMessage("SqlOutputStream: Max batched rows reached: '{0}'. Notifying the flusher thread.", new object[]
								{
									buffer.Rows.Count
								});
								this.flusherSleepWaitHandle.Set();
								flag2 = true;
							}
						}
						if (flag2)
						{
							Thread.Sleep(0);
							return;
						}
					}
					else
					{
						Logger.LogErrorMessage("SqlOutputStream: Received a row after flusher already stopped. We will not process this line sample time start: '{0}'", new object[]
						{
							dateTime.ToString()
						});
					}
					return;
				}
			}
			Logger.LogErrorMessage("SqlOutputStream: Received a row after flusher already stopped. We will not process this line with time: '{0}'", new object[]
			{
				dateTime.ToString()
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00008F64 File Offset: 0x00007164
		internal void ProcessMachineStateData(string csvLine)
		{
			this.machineStream.ProcessMachineStateData(csvLine);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00008F74 File Offset: 0x00007174
		internal void Flusher()
		{
			try
			{
				while (!this.flusherStopped)
				{
					this.flusherSleepWaitHandle.WaitOne(this.maxBufferTime);
					try
					{
						lock (this.flushingLock)
						{
							foreach (ManagedConnection managedConnection in this.managedConnections.Values)
							{
								SqlConnection connection = managedConnection.Connection;
								foreach (KeyValuePair<string, SqlOutputStream.PerformanceDataTable> keyValuePair in managedConnection.Buffers)
								{
									SqlOutputStream.PerformanceDataTable value = keyValuePair.Value;
									if (value.Rows.Count > 0 || !managedConnection.FlusherExecuted)
									{
										if (!managedConnection.FlusherExecuted)
										{
											Logger.LogInformationMessage("SqlOutputStream: Execute flusher on startup. Connection String '{0}' Row count: '{1}'", new object[]
											{
												SqlConnectionLogger.SafeConnectionString(connection.ConnectionString),
												value.Rows.Count
											});
										}
										if (this.FlushPerfLog(value, connection))
										{
											managedConnection.FlusherExecuted = true;
										}
										value.Rows.Clear();
									}
								}
							}
							foreach (string key in this.connectionsToRemove)
							{
								ManagedConnection managedConnection2;
								if (this.managedConnections.TryGetValue(key, out managedConnection2))
								{
									int num = 0;
									foreach (SqlOutputStream.PerformanceDataTable performanceDataTable in managedConnection2.Buffers.Values)
									{
										num += performanceDataTable.Rows.Count;
									}
									if (num == 0)
									{
										managedConnection2.Dispose();
										this.managedConnections.Remove(key);
									}
								}
							}
							this.connectionsToRemove.Clear();
						}
					}
					finally
					{
						this.flushingFinishWaitHandle.Set();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogErrorMessage("SqlOutputStream: Unhandled Exception: {0}", new object[]
				{
					ex
				});
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamUnhandledException, new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009264 File Offset: 0x00007464
		internal bool FlushPerfLog(SqlOutputStream.PerformanceDataTable perfLogData, SqlConnection connection)
		{
			Logger.LogInformationMessage("SqlOutputStream: Flushing PerfLog data. Row count: '{0}'", new object[]
			{
				perfLogData.Rows.Count
			});
			this.perfLogCommand.Parameters[0].Value = perfLogData;
			this.perfLogCommand.Parameters[1].Value = perfLogData.MachineName;
			this.perfLogCommand.Connection = connection;
			if (!this.FlushWithRetryLogic(this.perfLogCommand))
			{
				StringBuilder stringBuilder = new StringBuilder(perfLogData.Rows.Count * 256);
				foreach (object obj in perfLogData.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					for (int i = 0; i < perfLogData.Columns.Count; i++)
					{
						stringBuilder.Append(dataRow[i]);
						stringBuilder.Append(',');
					}
					stringBuilder.Append('\t');
				}
				Logger.LogErrorMessage("SqlOutputStream: This perflog data will be ignored. machineName: '{0}' perfLogDataTable: '{1}'", new object[]
				{
					perfLogData.MachineName,
					stringBuilder.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000093AC File Offset: 0x000075AC
		internal void FlushMachineState(object[] values)
		{
			this.machineStream.FlushMachineState(values);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000093BC File Offset: 0x000075BC
		internal bool FlushWithRetryLogic(SqlCommand command)
		{
			string text = string.Empty;
			DateTime utcNow = DateTime.UtcNow;
			int num = 0;
			SqlConnection connection = command.Connection;
			for (;;)
			{
				this.numberOfTriesSinceServiceStarted++;
				if (this.numberOfTriesSinceServiceStarted <= 0)
				{
					this.numberOfTriesSinceServiceStarted = int.MaxValue;
				}
				if (this.successfulUploadsInLastHundredBatches >= 100)
				{
					this.successfulUploadsInLastHundredBatches--;
				}
				try
				{
					string text2 = this.OpenConnection(connection);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
					}
					else
					{
						try
						{
							DateTime utcNow2 = DateTime.UtcNow;
							this.sqlConnectionLogger.ExecuteNonQuery(command);
							DateTime utcNow3 = DateTime.UtcNow;
							this.batchesUploadedForTheLastHourTimeStamp.Enqueue(utcNow3);
							this.avgUploadLatencyForLastHundredBatches.AddNewSample((utcNow3 - utcNow2).TotalMilliseconds);
							long bytesSent = this.sqlConnectionLogger.GetBytesSent(connection);
							this.avgUploadSizeForLastHundredBatches.AddNewSample((double)bytesSent);
							this.successfulUploadsInLastHundredBatches++;
							this.numberOfBatchesUploadedSinceServiceStarted++;
							Logger.LogInformationMessage("SqlOutputStream: We successfully executed '{0}'", new object[]
							{
								command.CommandText
							});
							return true;
						}
						catch (SqlException exception)
						{
							if (!this.sqlConnectionLogger.HandleException(exception, connection, ref text))
							{
								return false;
							}
						}
					}
				}
				finally
				{
					this.CloseConnectionAndUpdateCounters(num, connection);
					num++;
				}
				string error = string.Format("'{0}' Number of tries: '{1}' Last Exception: '{2}' Command '{3}'", new object[]
				{
					this.logRetryEventInterval,
					num,
					text,
					command.CommandText
				});
				if (this.LogRetry(ref utcNow, error))
				{
					break;
				}
				this.DoExponentialBackoff(num, this.flusherSleepWaitHandle);
				if (this.flusherStopped)
				{
					goto Block_7;
				}
			}
			Logger.LogErrorMessage("SqlOutputStream: Resetting the connection strings due to too many retries and rebuffering the data.", new object[0]);
			this.ReloadConnectionString();
			if (command == this.perfLogCommand)
			{
				SqlOutputStream.PerformanceDataTable performanceDataTable = (SqlOutputStream.PerformanceDataTable)command.Parameters[0].Value;
				using (SqlOutputStream.PerformanceDataTable performanceDataTable2 = (SqlOutputStream.PerformanceDataTable)performanceDataTable.Copy())
				{
					performanceDataTable.Rows.Clear();
					this.BatchPerflogDataTable(performanceDataTable2);
				}
				this.connectionsToRemove.Add(SqlConnectionLogger.SafeConnectionString(command.Connection.ConnectionString));
			}
			return false;
			Block_7:
			if (this.flusherStopped)
			{
				Logger.LogInformationMessage("SqlOutputStream: Flusher stop requested. The flusher thread is exiting.", new object[0]);
			}
			return !this.flusherStopped;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009644 File Offset: 0x00007844
		internal string OpenConnection(SqlConnection connection)
		{
			Logger.LogInformationMessage("SqlOutputStream: Opening connection '{0}'", new object[]
			{
				SqlConnectionLogger.SafeConnectionString(connection.ConnectionString)
			});
			string text = string.Empty;
			try
			{
				DateTime utcNow = DateTime.UtcNow;
				this.sqlConnectionLogger.OpenConnection(connection, true);
				this.avgConnectionOpenLatencyForLastHundredBatches.AddNewSample((DateTime.UtcNow - utcNow).TotalMilliseconds);
			}
			catch (Exception ex)
			{
				if (ex is InvalidOperationException)
				{
					text = string.Format("SqlOutputStream: connection.Open() failed. Performing retry logic. '{0}'", ex);
				}
				else
				{
					if (!(ex is SqlException))
					{
						Logger.LogErrorMessage("{0}", new object[]
						{
							ex.ToString()
						});
						throw;
					}
					text = string.Format("SqlOutputStream: connection.Open() failed. Performing retry logic. '{0}'", this.sqlConnectionLogger.CreateSqlExceptionErrorMessage(ex as SqlException, connection, text));
				}
				Logger.LogErrorMessage("{0}", new object[]
				{
					text
				});
			}
			return text;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009738 File Offset: 0x00007938
		internal void CloseConnectionAndUpdateCounters(int tryCountBeforeSuccess, SqlConnection connection)
		{
			try
			{
				this.UpdateCounters(tryCountBeforeSuccess);
			}
			finally
			{
				this.sqlConnectionLogger.CloseConnection(connection);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000976C File Offset: 0x0000796C
		internal void DoExponentialBackoff(int tryCount, AutoResetEvent sleepWaitHandle)
		{
			int num = (int)Math.Pow(2.0, (double)tryCount);
			if (num <= 0)
			{
				num = int.MaxValue;
			}
			num = this.random.Next(0, num);
			double num2 = this.avgConnectionOpenLatencyForLastHundredBatches.AverageValue + this.avgUploadLatencyForLastHundredBatches.AverageValue;
			if (num2 == 0.0)
			{
				num2 = 100.0;
			}
			double value = Math.Min((double)num * num2, this.maxBufferTime.TotalMilliseconds);
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(value);
			Logger.LogInformationMessage("SqlOutputStream: Number of retries: '{0}'. Will sleep before trying again in '{1}'.", new object[]
			{
				tryCount,
				timeSpan
			});
			if (!this.flusherStopped)
			{
				sleepWaitHandle.WaitOne(timeSpan);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000982C File Offset: 0x00007A2C
		internal void StopFlusher()
		{
			if (!this.flusherStopped)
			{
				Logger.LogInformationMessage("SqlOutputStream: Terminating the flusher thread.", new object[0]);
				this.flusherStopped = true;
				Logger.LogInformationMessage("SqlOutputStream: Wake up the flusher thread.", new object[0]);
				if (this.getPartitionWaitHandle != null)
				{
					this.getPartitionWaitHandle.Set();
				}
				if (this.connectionStringManagerWaitHandle != null)
				{
					this.connectionStringManagerWaitHandle.Set();
				}
				if (this.flusherSleepWaitHandle != null)
				{
					this.flusherSleepWaitHandle.Set();
					Thread.Sleep(0);
					if (this.flushingThread != null)
					{
						this.flushingThread.Join();
						Logger.LogInformationMessage("SqlOutputStream: The flusher thread is dead.", new object[0]);
					}
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000098D4 File Offset: 0x00007AD4
		protected override void InternalWriteHeaderLine(string format, params object[] args)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000098D6 File Offset: 0x00007AD6
		protected override void InternalWriteLine(string format, params object[] args)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000098D8 File Offset: 0x00007AD8
		protected override void InternalDispose(bool disposing)
		{
			if (!this.disposed)
			{
				Logger.LogInformationMessage("SqlOutputStream: Disposing SqlOutputStream.", new object[0]);
				this.disposed = true;
				this.StopFlusher();
				if (disposing)
				{
					if (this.sqlConnectionLogger != null)
					{
						this.sqlConnectionLogger.Dispose();
					}
					if (this.perfLogCommand != null)
					{
						this.perfLogCommand.Dispose();
					}
					if (this.machineStream != null)
					{
						this.machineStream.Dispose();
					}
					if (this.managedConnections != null)
					{
						foreach (ManagedConnection managedConnection in this.managedConnections.Values)
						{
							managedConnection.Dispose();
						}
						this.managedConnections.Clear();
					}
					if (this.performanceCounterTimer != null)
					{
						this.performanceCounterTimer.Enabled = false;
						this.performanceCounterTimer.Dispose();
					}
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000099CC File Offset: 0x00007BCC
		private static void UpdateAverageCounters(LinkedList<int> lastHundredBatches, ref double average, double newValue)
		{
			if (lastHundredBatches.Count < 100)
			{
				average = ((lastHundredBatches.Count == 0) ? newValue : ((average * (double)lastHundredBatches.Count + newValue) / (double)(lastHundredBatches.Count + 1)));
			}
			else
			{
				average += (newValue - (double)lastHundredBatches.First.Value) / 100.0;
				lastHundredBatches.RemoveFirst();
			}
			lastHundredBatches.AddLast((int)newValue);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009A34 File Offset: 0x00007C34
		private bool CsvLineToColumns(string csvLine, out string machineName, out List<string> csvCols)
		{
			machineName = null;
			if (!StringUtils.TryGetColumns(csvLine, ',', ref csvCols))
			{
				string text = string.Format("SqlOutputStream: This row '{0}' failed TryGetColumns. This line will not be added to the batch.", csvLine);
				Logger.LogErrorMessage("{0}", new object[]
				{
					text
				});
				return false;
			}
			int num = SqlOutputStream.PerfLogColumnDefinitions.GetLength(0) + 1;
			if (csvCols.Count != num)
			{
				string text2 = string.Format("SqlOutputStream: This row '{0}' has an invalid column count: '{1}' != '{2}'. This line will not be added to the batch.", csvLine, csvCols.Count, num);
				Logger.LogErrorMessage("{0}", new object[]
				{
					text2
				});
				return false;
			}
			machineName = csvCols[1];
			if (machineName == null)
			{
				Logger.LogErrorMessage("SqlOutputStream: Machine name is is null in row: '{0}'", new object[]
				{
					csvLine
				});
				return false;
			}
			return true;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00009AF0 File Offset: 0x00007CF0
		private bool AddCsvColsToTable(List<string> csvCols, DataTable table)
		{
			DataRow dataRow = table.NewRow();
			for (int i = 0; i < SqlOutputStream.PerfLogColumnDefinitions.GetLength(0); i++)
			{
				string columnName = (string)SqlOutputStream.PerfLogColumnDefinitions[i, 0];
				Func<string, object> func = (Func<string, object>)SqlOutputStream.PerfLogColumnDefinitions[i, 2];
				int index = (i == 0) ? 0 : (i + 1);
				object obj = func(csvCols[index]);
				if (obj == null)
				{
					Logger.LogErrorMessage("SqlOutputStream: This row '{0}' has an invalid value: '{1}'. This line will not be added to the batch.", new object[]
					{
						csvCols.ToString(),
						csvCols[index]
					});
					return false;
				}
				dataRow[columnName] = obj;
			}
			table.Rows.Add(dataRow);
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00009BA4 File Offset: 0x00007DA4
		private string GetConnectionString(string machineName, int tier, DateTime sampleTimeStart)
		{
			int num = 1;
			DateTime utcNow = DateTime.UtcNow;
			string text = string.Empty;
			while (string.IsNullOrEmpty(text) && !this.flusherStopped)
			{
				text = this.EDSConnectionStringManager.GetPerfLogConnectionString(machineName, tier, sampleTimeStart);
				if (this.flusherStopped)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(text))
				{
					break;
				}
				string error = string.Format("'{0}' Number of tries: '{1}' GetPerfLogConnectionString for sampleTimeStart '{2}' failed, check service log for details", this.logRetryEventInterval, num, sampleTimeStart);
				this.LogRetry(ref utcNow, error);
				this.DoExponentialBackoff(num++, this.getPartitionWaitHandle);
			}
			return text;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009C38 File Offset: 0x00007E38
		private void ResetCounters()
		{
			EdsPerformanceCounters.BytesLeftToBeProcessedOnDisk.RawValue = 0L;
			EdsPerformanceCounters.NumberOfBatchesUploadedForTheLastHour.RawValue = 0L;
			EdsPerformanceCounters.NumberOfBatchesUploadedSinceServiceStarted.RawValue = 0L;
			EdsPerformanceCounters.SuccessfulUploadsInLastHundredBatches.RawValue = 0L;
			EdsPerformanceCounters.FailedUploadsInLastHundredBatches.RawValue = 0L;
			EdsPerformanceCounters.RetryCountSinceLastSuccessfulUpload.RawValue = 0L;
			EdsPerformanceCounters.AvgConnectionOpenLatencyForLastHundredBatches.RawValue = 0L;
			EdsPerformanceCounters.ConnectionOpenLatencyForLastBatch.RawValue = 0L;
			EdsPerformanceCounters.AvgUploadLatencyForLastHundredBatches.RawValue = 0L;
			EdsPerformanceCounters.UploadLatencyForLastBatch.RawValue = 0L;
			EdsPerformanceCounters.AvgUploadSizeForLastHundredBatches.RawValue = 0L;
			EdsPerformanceCounters.UploadSizeForLastBatch.RawValue = 0L;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009CD5 File Offset: 0x00007ED5
		private void UpdateCountersEvent(object source, ElapsedEventArgs arg)
		{
			this.UpdateCounters(-1);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00009CE0 File Offset: 0x00007EE0
		private void UpdateCounters(int retryCountSinceLastSuccessfulUpload)
		{
			lock (this.performanceCounterLock)
			{
				this.InternalUpdateCounters(retryCountSinceLastSuccessfulUpload);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009D24 File Offset: 0x00007F24
		private void InternalUpdateCounters(int retryCountSinceLastSuccessfulUpload)
		{
			Logger.LogInformationMessage("SqlOutputStream: Updating performance counters.", new object[0]);
			string directoryName = Path.GetDirectoryName(this.analyzerLogDirectoryMask);
			string fileName = Path.GetFileName(this.analyzerLogDirectoryMask);
			if (Directory.Exists(directoryName))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
				FileInfo[] files = directoryInfo.GetFiles(fileName + "*");
				long num = 0L;
				foreach (FileInfo fileInfo in files)
				{
					num += fileInfo.Length;
				}
				EdsPerformanceCounters.BytesLeftToBeProcessedOnDisk.RawValue = num;
			}
			DateTime t = DateTime.UtcNow.Subtract(TimeSpan.FromHours(1.0));
			while (this.batchesUploadedForTheLastHourTimeStamp.Count > 0 && this.batchesUploadedForTheLastHourTimeStamp.Peek() < t)
			{
				this.batchesUploadedForTheLastHourTimeStamp.Dequeue();
			}
			EdsPerformanceCounters.NumberOfBatchesUploadedForTheLastHour.RawValue = (long)this.batchesUploadedForTheLastHourTimeStamp.Count;
			EdsPerformanceCounters.NumberOfBatchesUploadedSinceServiceStarted.RawValue = (long)this.numberOfBatchesUploadedSinceServiceStarted;
			EdsPerformanceCounters.SuccessfulUploadsInLastHundredBatches.RawValue = (long)this.successfulUploadsInLastHundredBatches;
			EdsPerformanceCounters.FailedUploadsInLastHundredBatches.RawValue = (long)(Math.Min(this.numberOfTriesSinceServiceStarted, 100) - this.successfulUploadsInLastHundredBatches);
			if (retryCountSinceLastSuccessfulUpload >= 0)
			{
				EdsPerformanceCounters.RetryCountSinceLastSuccessfulUpload.RawValue = (long)retryCountSinceLastSuccessfulUpload;
			}
			EdsPerformanceCounters.AvgConnectionOpenLatencyForLastHundredBatches.RawValue = (long)this.avgConnectionOpenLatencyForLastHundredBatches.AverageValue;
			if (this.avgConnectionOpenLatencyForLastHundredBatches.LastValue != null)
			{
				EdsPerformanceCounters.ConnectionOpenLatencyForLastBatch.RawValue = (long)this.avgConnectionOpenLatencyForLastHundredBatches.LastValue.Value;
			}
			EdsPerformanceCounters.AvgUploadLatencyForLastHundredBatches.RawValue = (long)this.avgUploadLatencyForLastHundredBatches.AverageValue;
			if (this.avgUploadLatencyForLastHundredBatches.LastValue != null)
			{
				EdsPerformanceCounters.UploadLatencyForLastBatch.RawValue = (long)this.avgUploadLatencyForLastHundredBatches.LastValue.Value;
			}
			EdsPerformanceCounters.AvgUploadSizeForLastHundredBatches.RawValue = (long)this.avgUploadSizeForLastHundredBatches.AverageValue;
			if (this.avgUploadSizeForLastHundredBatches.LastValue != null)
			{
				EdsPerformanceCounters.UploadSizeForLastBatch.RawValue = (long)this.avgUploadSizeForLastHundredBatches.LastValue.Value;
			}
			this.performanceCounterTimer.Interval = 600000.0;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009F60 File Offset: 0x00008160
		private void ReloadConnectionString()
		{
			if (!this.flusherStopped)
			{
				Logger.LogInformationMessage("SqlOutputStream: Reloading connection string.", new object[0]);
				try
				{
					this.connectionStringManager.ReloadStrings(this.currentSiteName);
				}
				catch (Exception arg)
				{
					string text = string.Format("SqlOutputStream: Exception throw when trying to get the connectionString list: '{0}'", arg);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text
					});
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamUnhandledException, new object[]
					{
						text
					});
					throw;
				}
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009FE8 File Offset: 0x000081E8
		private bool LogRetry(ref DateTime firstTryTime, string error)
		{
			Logger.LogErrorMessage("SqlOutputStream: {0}", new object[]
			{
				error
			});
			if (DateTime.UtcNow - firstTryTime > this.logRetryEventInterval)
			{
				firstTryTime = DateTime.UtcNow;
				double configDouble = Microsoft.ExLogAnalyzer.Configuration.GetConfigDouble("SqlOutputStream_TriggerThreshold", 0.0, 1.0, 0.3);
				if (this.random.NextDouble() < configDouble)
				{
					TriggerHandler.Trigger("SqlOutputStreamConsecutiveRetriesForASpecifiedTime", new object[]
					{
						error
					});
				}
				else
				{
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamConsecutiveRetriesForASpecifiedTime, new object[]
					{
						error
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A09C File Offset: 0x0000829C
		// Note: this type is marked as 'beforefieldinit'.
		static SqlOutputStream()
		{
			object[,] array = new object[9, 3];
			array[0, 0] = "SampleTimeStart";
			array[0, 1] = typeof(DateTime);
			array[0, 2] = ParseUtils.DateTimeParser;
			array[1, 0] = "MeasureObject";
			array[1, 1] = typeof(string);
			array[1, 2] = ParseUtils.StringParser;
			array[2, 0] = "MeasureName";
			array[2, 1] = typeof(string);
			array[2, 2] = ParseUtils.StringParser;
			array[3, 0] = "InstanceName";
			array[3, 1] = typeof(string);
			array[3, 2] = ParseUtils.StringParser;
			array[4, 0] = "SampleCount";
			array[4, 1] = typeof(int);
			array[4, 2] = ParseUtils.IntParser;
			array[5, 0] = "Mean";
			array[5, 1] = typeof(float);
			array[5, 2] = ParseUtils.FloatParser;
			array[6, 0] = "Max";
			array[6, 1] = typeof(float);
			array[6, 2] = ParseUtils.FloatParser;
			array[7, 0] = "Min";
			array[7, 1] = typeof(float);
			array[7, 2] = ParseUtils.FloatParser;
			array[8, 0] = "SD";
			array[8, 1] = typeof(float);
			array[8, 2] = ParseUtils.FloatParser;
			SqlOutputStream.PerfLogColumnDefinitions = array;
			object[,] array2 = new object[2, 5];
			array2[0, 0] = "PerfDataTable";
			array2[0, 1] = typeof(DataTable);
			array2[0, 3] = SqlDbType.Structured;
			array2[0, 4] = ParameterDirection.Input;
			array2[1, 0] = "MachineName";
			array2[1, 1] = typeof(string);
			array2[1, 3] = SqlDbType.VarChar;
			array2[1, 4] = ParameterDirection.Input;
			SqlOutputStream.PerfLogUploadDefinitions = array2;
		}

		// Token: 0x04000083 RID: 131
		public const int TierCount = 4;

		// Token: 0x04000084 RID: 132
		public const string EdsSqlSchemaVersion = "3.0";

		// Token: 0x04000085 RID: 133
		private const int PerfCounterBatches = 100;

		// Token: 0x04000086 RID: 134
		private static readonly TimeSpan ReloadConnectionStringInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000087 RID: 135
		private static readonly TimeSpan PartitionRetryDelay = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000088 RID: 136
		private static readonly object[,] PerfLogColumnDefinitions;

		// Token: 0x04000089 RID: 137
		private static readonly object[,] PerfLogUploadDefinitions;

		// Token: 0x0400008A RID: 138
		private readonly int maxBufferRows;

		// Token: 0x0400008B RID: 139
		private readonly TimeSpan maxBufferTime;

		// Token: 0x0400008C RID: 140
		private readonly string analyzerLogDirectoryMask;

		// Token: 0x0400008D RID: 141
		private readonly TimeSpan logRetryEventInterval;

		// Token: 0x0400008E RID: 142
		private readonly Random random;

		// Token: 0x0400008F RID: 143
		private readonly Thread flushingThread;

		// Token: 0x04000090 RID: 144
		private readonly object flushingLock;

		// Token: 0x04000091 RID: 145
		private readonly AutoResetEvent flusherSleepWaitHandle;

		// Token: 0x04000092 RID: 146
		private readonly AutoResetEvent flushingFinishWaitHandle;

		// Token: 0x04000093 RID: 147
		private readonly AutoResetEvent connectionStringManagerWaitHandle;

		// Token: 0x04000094 RID: 148
		private readonly AutoResetEvent getPartitionWaitHandle;

		// Token: 0x04000095 RID: 149
		private readonly Dictionary<string, ManagedConnection> managedConnections;

		// Token: 0x04000096 RID: 150
		private readonly List<string> connectionsToRemove;

		// Token: 0x04000097 RID: 151
		private readonly SqlCommand perfLogCommand;

		// Token: 0x04000098 RID: 152
		private readonly string environmentMachineName;

		// Token: 0x04000099 RID: 153
		private readonly string currentMachineName;

		// Token: 0x0400009A RID: 154
		private readonly string currentSiteName;

		// Token: 0x0400009B RID: 155
		private readonly OutputStream serviceOutputStream;

		// Token: 0x0400009C RID: 156
		private readonly FileChunkOutputStream fileChunkOutputStream;

		// Token: 0x0400009D RID: 157
		private readonly SqlOutputStream.MachineSqlOutputStream machineStream;

		// Token: 0x0400009E RID: 158
		private readonly SqlOutputStream.TieredSqlOutputStream[] tieredStreams;

		// Token: 0x0400009F RID: 159
		private readonly SqlOutputStream.ConnectionStringManager connectionStringManager;

		// Token: 0x040000A0 RID: 160
		private readonly SqlConnectionLogger sqlConnectionLogger;

		// Token: 0x040000A1 RID: 161
		private readonly System.Timers.Timer performanceCounterTimer;

		// Token: 0x040000A2 RID: 162
		private readonly object performanceCounterLock;

		// Token: 0x040000A3 RID: 163
		private volatile bool flusherStopped;

		// Token: 0x040000A4 RID: 164
		private volatile bool disposed;

		// Token: 0x040000A5 RID: 165
		private int successfulUploadsInLastHundredBatches;

		// Token: 0x040000A6 RID: 166
		private SampleAverage avgUploadLatencyForLastHundredBatches;

		// Token: 0x040000A7 RID: 167
		private SampleAverage avgConnectionOpenLatencyForLastHundredBatches;

		// Token: 0x040000A8 RID: 168
		private SampleAverage avgUploadSizeForLastHundredBatches;

		// Token: 0x040000A9 RID: 169
		private int numberOfTriesSinceServiceStarted;

		// Token: 0x040000AA RID: 170
		private Queue<DateTime> batchesUploadedForTheLastHourTimeStamp;

		// Token: 0x040000AB RID: 171
		private int numberOfBatchesUploadedSinceServiceStarted;

		// Token: 0x040000AC RID: 172
		private DateTime lastReloadConnectionString;

		// Token: 0x02000026 RID: 38
		private enum ColumnDefinitionIndex
		{
			// Token: 0x040000AE RID: 174
			ColumnName,
			// Token: 0x040000AF RID: 175
			ColumnType,
			// Token: 0x040000B0 RID: 176
			ColumnParser,
			// Token: 0x040000B1 RID: 177
			ColumnSqlType,
			// Token: 0x040000B2 RID: 178
			ColumnSqlParamDirection
		}

		// Token: 0x02000027 RID: 39
		public class Configuration
		{
			// Token: 0x060000DB RID: 219 RVA: 0x0000A2F0 File Offset: 0x000084F0
			public Configuration(int maxBufferRows, TimeSpan maxBufferTime, TimeSpan logRetryEventInterval, int performanceDataUploadTimeoutSeconds)
			{
				this.maxBufferRows = maxBufferRows;
				this.maxBufferTime = maxBufferTime;
				this.logRetryEventInterval = logRetryEventInterval;
				this.performanceDataUploadTimeoutSeconds = performanceDataUploadTimeoutSeconds;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000DC RID: 220 RVA: 0x0000A315 File Offset: 0x00008515
			public int MaxBufferRows
			{
				get
				{
					return this.maxBufferRows;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000DD RID: 221 RVA: 0x0000A31D File Offset: 0x0000851D
			public TimeSpan MaxBufferTime
			{
				get
				{
					return this.maxBufferTime;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000DE RID: 222 RVA: 0x0000A325 File Offset: 0x00008525
			public TimeSpan LogRetryEventInterval
			{
				get
				{
					return this.logRetryEventInterval;
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000DF RID: 223 RVA: 0x0000A32D File Offset: 0x0000852D
			public int PerformanceDataUploadTimeoutSeconds
			{
				get
				{
					return this.performanceDataUploadTimeoutSeconds;
				}
			}

			// Token: 0x040000B3 RID: 179
			private readonly int maxBufferRows;

			// Token: 0x040000B4 RID: 180
			private readonly TimeSpan maxBufferTime;

			// Token: 0x040000B5 RID: 181
			private readonly TimeSpan logRetryEventInterval;

			// Token: 0x040000B6 RID: 182
			private readonly int performanceDataUploadTimeoutSeconds;
		}

		// Token: 0x02000028 RID: 40
		public class PerformanceDataTable : DataTable
		{
			// Token: 0x060000E0 RID: 224 RVA: 0x0000A338 File Offset: 0x00008538
			public PerformanceDataTable(int tier, string machineName)
			{
				this.Tier = tier;
				this.MachineName = machineName;
				for (int i = 0; i < SqlOutputStream.PerfLogColumnDefinitions.GetLength(0); i++)
				{
					base.Columns.Add(new DataColumn((string)SqlOutputStream.PerfLogColumnDefinitions[i, 0], (Type)SqlOutputStream.PerfLogColumnDefinitions[i, 1]));
				}
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x0000A3A4 File Offset: 0x000085A4
			public override DataTable Clone()
			{
				return new SqlOutputStream.PerformanceDataTable(this.Tier, this.MachineName);
			}

			// Token: 0x040000B7 RID: 183
			public readonly int Tier;

			// Token: 0x040000B8 RID: 184
			public readonly string MachineName;
		}

		// Token: 0x02000029 RID: 41
		private class MachineSqlOutputStream : OutputStream
		{
			// Token: 0x060000E2 RID: 226 RVA: 0x0000A3C4 File Offset: 0x000085C4
			public MachineSqlOutputStream(SqlOutputStream sqlOutputStream) : base("MachineSqlOutputStream")
			{
				this.sqlOutputStream = sqlOutputStream;
				this.sqlConnectionLogger = sqlOutputStream.sqlConnectionLogger.Clone(base.Name);
				this.machineStateCommand = SqlOutputStream.BuildSqlCommand("[EDSPart].[sp_uploadMachineStateData]", SqlOutputStream.MachineSqlOutputStream.MachineStateColumnDefinitions, 30);
				this.machineStateCommand.Connection = this.sqlConnectionLogger.BuildSqlConnection(null);
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x0000A428 File Offset: 0x00008628
			internal void FlushMachineState(object[] values)
			{
				Logger.LogInformationMessage("MachineSqlOutputStream: Flushing MachineState data.", new object[0]);
				for (int i = 0; i < values.Length; i++)
				{
					this.machineStateCommand.Parameters[i].Value = values[i];
				}
				string[] machineConnectionString = this.sqlOutputStream.EDSConnectionStringManager.GetMachineConnectionString();
				foreach (string text in machineConnectionString)
				{
					if (!string.IsNullOrEmpty(text))
					{
						this.UploadMachineState(text, values);
					}
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x0000A4A8 File Offset: 0x000086A8
			internal void UploadMachineState(string connectionString, object[] values)
			{
				if (this.sqlOutputStream.flusherStopped)
				{
					Logger.LogErrorMessage("MachineSqlOutputStream: Received a row after flusher already stopped. We will not process the line", new object[0]);
					return;
				}
				if (string.IsNullOrEmpty(connectionString))
				{
					string text = string.Format("MachineSqlOutputStream: Null or empty connection string returned from connection string manager", new object[0]);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text
					});
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ConnectionStringManagerPartitionInvalid, new object[]
					{
						text
					});
					throw new ArgumentException(text);
				}
				if (!string.Equals(this.machineStateCommand.Connection.ConnectionString, connectionString))
				{
					this.machineStateCommand.Connection.ConnectionString = connectionString;
					Logger.LogInformationMessage("MachineSqlOutputStream: FlushMachineState: Loaded new connection string: '{0}'", new object[]
					{
						SqlConnectionLogger.SafeConnectionString(connectionString)
					});
				}
				if (!this.sqlOutputStream.FlushWithRetryLogic(this.machineStateCommand))
				{
					StringBuilder stringBuilder = new StringBuilder(5000);
					foreach (object obj in values)
					{
						stringBuilder.Append(obj.ToString());
						stringBuilder.Append(',');
					}
					Logger.LogErrorMessage("MachineSqlOutputStream: This machineState data will be ignored. values: '{0}'", new object[]
					{
						stringBuilder.ToString()
					});
				}
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x0000A5DC File Offset: 0x000087DC
			internal void ProcessMachineStateData(string csvLine)
			{
				Logger.LogInformationMessage("MachineSqlOutputStream: Processing a line of MachineState data '{0}'", new object[]
				{
					csvLine
				});
				List<string> list;
				if (!StringUtils.TryGetColumns(csvLine, ',', ref list))
				{
					string text = string.Format("MachineSqlOutputStream: This row '{0}' failed TryGetColumns. This line will be not be processed.", csvLine);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text
					});
					return;
				}
				int length = SqlOutputStream.MachineSqlOutputStream.MachineStateColumnDefinitions.GetLength(0);
				if (list.Count != length)
				{
					string text2 = string.Format("MachineSqlOutputStream: This row '{0}' has an invalid column count: '{1}' != '{2}'. This line will be not be processed.", csvLine, list.Count, length);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text2
					});
					return;
				}
				object[] array = new object[SqlOutputStream.MachineSqlOutputStream.MachineStateColumnDefinitions.GetLength(0)];
				for (int i = 0; i < array.Length; i++)
				{
					Func<string, object> func = (Func<string, object>)SqlOutputStream.MachineSqlOutputStream.MachineStateColumnDefinitions[i, 2];
					array[i] = func(list[i]);
					if (array[i] == null)
					{
						Logger.LogErrorMessage("MachineSqlOutputStream: This row '{0}' has an invalid value: '{1}'. This line will be not be processed.", new object[]
						{
							csvLine,
							list[i]
						});
						return;
					}
				}
				if (!this.sqlOutputStream.flusherStopped)
				{
					lock (this.sqlOutputStream.flushingLock)
					{
						this.FlushMachineState(array);
						return;
					}
				}
				Logger.LogErrorMessage("SqlOutputStream: Received a row after flusher already stopped. We will not process this line: '{0}'", new object[]
				{
					csvLine
				});
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x0000A760 File Offset: 0x00008960
			protected override void InternalWriteHeaderLine(string format, params object[] args)
			{
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000A764 File Offset: 0x00008964
			protected override void InternalWriteLine(string format, params object[] args)
			{
				try
				{
					this.ProcessMachineStateData(string.Format(format, args));
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("SqlOutputStream: Unhandled Exception: {0}", new object[]
					{
						ex
					});
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamUnhandledException, new object[]
					{
						ex
					});
					throw;
				}
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x0000A7C0 File Offset: 0x000089C0
			protected override void InternalDispose(bool disposing)
			{
				Logger.LogInformationMessage("MachineSqlOutputStream: Disposing MachineSqlOutputStream.", new object[0]);
				if (disposing)
				{
					if (this.machineStateCommand != null)
					{
						this.machineStateCommand.Dispose();
					}
					if (this.sqlConnectionLogger != null)
					{
						this.sqlConnectionLogger.Dispose();
					}
				}
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x0000A7FC File Offset: 0x000089FC
			// Note: this type is marked as 'beforefieldinit'.
			static MachineSqlOutputStream()
			{
				object[,] array = new object[10, 5];
				array[0, 0] = "Timestamp";
				array[0, 1] = typeof(DateTime);
				array[0, 2] = ParseUtils.DateTimeParser;
				array[0, 3] = SqlDbType.SmallDateTime;
				array[0, 4] = ParameterDirection.Input;
				array[1, 0] = "MachineName";
				array[1, 1] = typeof(string);
				array[1, 2] = ParseUtils.StringParser;
				array[1, 3] = SqlDbType.VarChar;
				array[1, 4] = ParameterDirection.Input;
				array[2, 0] = "Role";
				array[2, 1] = typeof(string);
				array[2, 2] = ParseUtils.StringParser;
				array[2, 3] = SqlDbType.VarChar;
				array[2, 4] = ParameterDirection.Input;
				array[3, 0] = "Forest";
				array[3, 1] = typeof(string);
				array[3, 2] = ParseUtils.StringParser;
				array[3, 3] = SqlDbType.VarChar;
				array[3, 4] = ParameterDirection.Input;
				array[4, 0] = "Site";
				array[4, 1] = typeof(string);
				array[4, 2] = ParseUtils.StringParser;
				array[4, 3] = SqlDbType.VarChar;
				array[4, 4] = ParameterDirection.Input;
				array[5, 0] = "ServiceState";
				array[5, 1] = typeof(short);
				array[5, 2] = ParseUtils.ShortParser;
				array[5, 3] = SqlDbType.SmallInt;
				array[5, 4] = ParameterDirection.Input;
				array[6, 0] = "VersionMajor";
				array[6, 1] = typeof(int);
				array[6, 2] = ParseUtils.IntParser;
				array[6, 3] = SqlDbType.Int;
				array[6, 4] = ParameterDirection.Input;
				array[7, 0] = "VersionMinor";
				array[7, 1] = typeof(int);
				array[7, 2] = ParseUtils.IntParser;
				array[7, 3] = SqlDbType.Int;
				array[7, 4] = ParameterDirection.Input;
				array[8, 0] = "BuildMajor";
				array[8, 1] = typeof(int);
				array[8, 2] = ParseUtils.IntParser;
				array[8, 3] = SqlDbType.Int;
				array[8, 4] = ParameterDirection.Input;
				array[9, 0] = "BuildMinor";
				array[9, 1] = typeof(int);
				array[9, 2] = ParseUtils.IntParser;
				array[9, 3] = SqlDbType.Int;
				array[9, 4] = ParameterDirection.Input;
				SqlOutputStream.MachineSqlOutputStream.MachineStateColumnDefinitions = array;
			}

			// Token: 0x040000B9 RID: 185
			private static readonly object[,] MachineStateColumnDefinitions;

			// Token: 0x040000BA RID: 186
			private readonly SqlOutputStream sqlOutputStream;

			// Token: 0x040000BB RID: 187
			private readonly SqlCommand machineStateCommand;

			// Token: 0x040000BC RID: 188
			private readonly SqlConnectionLogger sqlConnectionLogger;
		}

		// Token: 0x0200002A RID: 42
		private class TieredSqlOutputStream : OutputStream
		{
			// Token: 0x060000EA RID: 234 RVA: 0x0000AAF3 File Offset: 0x00008CF3
			public TieredSqlOutputStream(SqlOutputStream sqlOutputStream, int tier) : base("TieredSqlOutputStream" + tier.ToString())
			{
				this.sqlOutputStream = sqlOutputStream;
				this.tier = tier;
			}

			// Token: 0x060000EB RID: 235 RVA: 0x0000AB1A File Offset: 0x00008D1A
			protected override void InternalWriteHeaderLine(string format, params object[] args)
			{
			}

			// Token: 0x060000EC RID: 236 RVA: 0x0000AB1C File Offset: 0x00008D1C
			protected override void InternalWriteLine(string format, params object[] args)
			{
				try
				{
					this.sqlOutputStream.BatchPerfLogData(this.tier, string.Format(format, args));
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("TieredSqlOutputStream: Unhandled Exception: {0}", new object[]
					{
						ex
					});
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_SqlOutputStreamUnhandledException, new object[]
					{
						ex
					});
					throw;
				}
			}

			// Token: 0x060000ED RID: 237 RVA: 0x0000AB84 File Offset: 0x00008D84
			protected override void InternalDispose(bool disposing)
			{
			}

			// Token: 0x040000BD RID: 189
			private readonly SqlOutputStream sqlOutputStream;

			// Token: 0x040000BE RID: 190
			private readonly int tier;
		}

		// Token: 0x0200002B RID: 43
		private class ConnectionStringManager : IDisposable
		{
			// Token: 0x060000EE RID: 238 RVA: 0x0000AB88 File Offset: 0x00008D88
			public ConnectionStringManager(SqlOutputStream sqlOutputStream)
			{
				this.logRetryEventInterval = sqlOutputStream.logRetryEventInterval;
				this.partitionRecords = new Dictionary<string, SqlOutputStream.PartitionRecord[]>(StringComparer.OrdinalIgnoreCase);
				this.sqlConnectionLogger = sqlOutputStream.sqlConnectionLogger.Clone("ConnectionStringManager");
				this.timeLastErrorReportedGetPartition = DateTime.MinValue;
				this.timeLastErrorReportedValidateConnectionString = DateTime.MinValue;
				this.connectionStrings = new string[5];
				this.partitionCommand = SqlOutputStream.BuildSqlCommand("[EDSPart].[sp_getPartition]", SqlOutputStream.ConnectionStringManager.PartitionInfoColumnDefinitions, 30);
			}

			// Token: 0x060000EF RID: 239 RVA: 0x0000AC11 File Offset: 0x00008E11
			public string GetConnectionString(int tier)
			{
				if (tier < 0 || tier > 4)
				{
					return string.Empty;
				}
				return this.connectionStrings[tier] ?? this.connectionStrings[0];
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x0000AC35 File Offset: 0x00008E35
			public string[] GetMachineConnectionString()
			{
				return this.connectionStrings;
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x0000AC3D File Offset: 0x00008E3D
			public void Dispose()
			{
				this.InternalDispose(true);
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000AC48 File Offset: 0x00008E48
			public string GetPerfLogConnectionString(string machineName, int tier, DateTime timestamp)
			{
				string connectionString;
				lock (this.getPartitionLock)
				{
					SqlOutputStream.PartitionRecord[] array;
					if (!this.partitionRecords.TryGetValue(machineName, out array))
					{
						array = new SqlOutputStream.PartitionRecord[5];
						this.partitionRecords.Add(machineName, array);
					}
					SqlOutputStream.PartitionRecord partitionRecord = array[tier];
					if (partitionRecord != null && partitionRecord.ConnectionString != null && timestamp >= partitionRecord.ValidPeriodStart && timestamp < partitionRecord.ValidPeriodEnd)
					{
						connectionString = partitionRecord.ConnectionString;
					}
					else
					{
						partitionRecord = this.GetPartitionRecord(machineName, tier, timestamp);
						array[tier] = partitionRecord;
						connectionString = partitionRecord.ConnectionString;
					}
				}
				return connectionString;
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x0000ACF4 File Offset: 0x00008EF4
			public void ReloadStrings(string currentSiteName)
			{
				IList<string> edsConnectionStrings = ManageEdsConnectionStrings.GetEdsConnectionStrings();
				this.connectionStrings = SqlOutputStream.FindMatchingConnectionStringForSchema(edsConnectionStrings, currentSiteName);
				for (int i = 0; i < this.connectionStrings.Length; i++)
				{
					string text = this.connectionStrings[i];
					if (!string.IsNullOrEmpty(text))
					{
						Logger.LogInformationMessage("SqlOutputStream: Loaded connection string '{0}' for site='{1}', tier={2}", new object[]
						{
							SqlConnectionLogger.SafeConnectionString(text),
							currentSiteName,
							i
						});
					}
				}
				lock (this.getPartitionLock)
				{
					this.partitionRecords.Clear();
				}
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x0000ADA0 File Offset: 0x00008FA0
			public bool ValidateConnectionString()
			{
				bool flag = this.ValidateConnectionString(this.connectionStrings[0]);
				for (int i = 1; i < this.connectionStrings.Length; i++)
				{
					string text = this.connectionStrings[i];
					if (!string.IsNullOrEmpty(text))
					{
						flag &= this.ValidateConnectionString(text);
					}
				}
				return flag;
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x0000ADEC File Offset: 0x00008FEC
			public bool ValidateConnectionString(string connectionString)
			{
				if (string.IsNullOrEmpty(connectionString))
				{
					this.LogConnectionError("Connection string used to access the partitioning DB is null or empty");
					return false;
				}
				try
				{
					using (SqlConnection sqlConnection = this.sqlConnectionLogger.BuildSqlConnection(connectionString))
					{
						this.sqlConnectionLogger.OpenConnection(sqlConnection, false);
					}
				}
				catch (Exception ex)
				{
					string error = string.Format("ConnectionStringManager: Validation of connection string failed: Exception thrown when getting partition from DB using connection string '{0}' Exception '{1}'", SqlConnectionLogger.SafeConnectionString(connectionString), ex);
					this.LogConnectionError(error);
					if (ex is SqlException || ex is InvalidOperationException)
					{
						return false;
					}
					throw;
				}
				Logger.LogInformationMessage("SqlOutputStream: Retrieved and validated Partitioning DB connection string '{0}'", new object[]
				{
					SqlConnectionLogger.SafeConnectionString(connectionString)
				});
				return true;
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x0000AEA4 File Offset: 0x000090A4
			private void LogConnectionError(string error)
			{
				Logger.LogErrorMessage("{0}", new object[]
				{
					error
				});
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow - this.timeLastErrorReportedValidateConnectionString > this.logRetryEventInterval)
				{
					this.timeLastErrorReportedValidateConnectionString = utcNow;
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ConnectionStringManagerUnableToConnect, new object[]
					{
						error
					});
				}
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x0000AF04 File Offset: 0x00009104
			private void InternalDispose(bool disposing)
			{
				Logger.LogInformationMessage("ConnectionStringManager: Disposing ConnectionStringManager.", new object[0]);
				if (disposing)
				{
					if (this.partitionCommand != null)
					{
						this.partitionCommand.Dispose();
					}
					if (this.sqlConnectionLogger != null)
					{
						this.sqlConnectionLogger.Dispose();
					}
				}
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x0000AF40 File Offset: 0x00009140
			private SqlOutputStream.PartitionRecord GetPartitionRecord(string machineName, int tier, DateTime timestamp)
			{
				string text = string.Empty;
				DateTime minValue = DateTime.MinValue;
				string text2 = string.Empty;
				this.partitionCommand.Parameters[0].Value = timestamp;
				this.partitionCommand.Parameters[1].Value = machineName;
				this.partitionCommand.Parameters[2].Value = tier;
				this.partitionCommand.Parameters[3].Value = string.Empty;
				this.partitionCommand.Parameters[3].Size = 1024;
				this.partitionCommand.Parameters[4].Value = default(DateTime);
				string connectionString = this.GetConnectionString(tier);
				if (string.IsNullOrEmpty(connectionString))
				{
					string error = string.Format("ConnectionStringManager: There is no connection string associated with the partition command for the period starting '{0}', returning empty record", timestamp);
					this.LogPartitionError(error);
					return SqlOutputStream.PartitionRecord.Empty;
				}
				using (SqlConnection sqlConnection = this.sqlConnectionLogger.BuildSqlConnection(connectionString))
				{
					try
					{
						this.partitionCommand.Connection = sqlConnection;
						this.sqlConnectionLogger.OpenConnection(this.partitionCommand.Connection, false);
						this.sqlConnectionLogger.ExecuteNonQuery(this.partitionCommand);
						text = this.partitionCommand.Parameters[3].Value.ToString();
						text2 = this.partitionCommand.Parameters[4].Value.ToString();
						this.sqlConnectionLogger.CloseConnection(sqlConnection);
					}
					catch (Exception ex)
					{
						if (ex is SqlException || ex is InvalidOperationException)
						{
							string error2 = string.Format("ConnectionStringManager: Exception thrown attempting to get connection string for timestamp '{0}' using '{1}' to access the partitioning DB{2}{3}", new object[]
							{
								timestamp,
								SqlConnectionLogger.SafeConnectionString(connectionString),
								Environment.NewLine,
								ex
							});
							this.LogPartitionError(error2);
							return SqlOutputStream.PartitionRecord.Empty;
						}
						throw;
					}
					finally
					{
						this.partitionCommand.Connection = null;
					}
				}
				if (DateTime.TryParse(text2, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out minValue) && !string.IsNullOrEmpty(text) && timestamp < minValue)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = SqlOutputStream.BuildSqlConnectionStringBuilder(text);
					SqlConnectionStringBuilder sqlConnectionStringBuilder2 = SqlOutputStream.BuildSqlConnectionStringBuilder(connectionString);
					sqlConnectionStringBuilder.UserID = sqlConnectionStringBuilder2.UserID;
					sqlConnectionStringBuilder.Password = sqlConnectionStringBuilder2.Password;
					text = sqlConnectionStringBuilder.ConnectionString;
					return new SqlOutputStream.PartitionRecord(timestamp, minValue, text);
				}
				string error3 = string.Format("ConnectionStringManager: Returned values Connection String '{0}', End '{1}' not valid for timestamp '{2}'", (text == null) ? string.Empty : text, (minValue == DateTime.MinValue) ? text2 : minValue.ToString(), timestamp);
				this.LogPartitionError(error3);
				return SqlOutputStream.PartitionRecord.Empty;
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x0000B214 File Offset: 0x00009414
			private void LogPartitionError(string error)
			{
				Logger.LogErrorMessage("{0}", new object[]
				{
					error
				});
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow - this.timeLastErrorReportedGetPartition > this.logRetryEventInterval)
				{
					this.timeLastErrorReportedGetPartition = utcNow;
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ConnectionStringManagerPartitionInvalid, new object[]
					{
						error
					});
				}
			}

			// Token: 0x060000FA RID: 250 RVA: 0x0000B274 File Offset: 0x00009474
			// Note: this type is marked as 'beforefieldinit'.
			static ConnectionStringManager()
			{
				object[,] array = new object[5, 5];
				array[0, 0] = "DataStartPeriod";
				array[0, 1] = typeof(DateTime);
				array[0, 2] = ParseUtils.DateTimeParser;
				array[0, 3] = SqlDbType.SmallDateTime;
				array[0, 4] = ParameterDirection.Input;
				array[1, 0] = "MachineName";
				array[1, 1] = typeof(string);
				array[1, 2] = ParseUtils.StringParser;
				array[1, 3] = SqlDbType.VarChar;
				array[1, 4] = ParameterDirection.Input;
				array[2, 0] = "Tier";
				array[2, 1] = typeof(short);
				array[2, 2] = ParseUtils.ShortParser;
				array[2, 3] = SqlDbType.SmallInt;
				array[2, 4] = ParameterDirection.Input;
				array[3, 0] = "ConnectionString";
				array[3, 1] = typeof(string);
				array[3, 2] = ParseUtils.IntParser;
				array[3, 3] = SqlDbType.VarChar;
				array[3, 4] = ParameterDirection.Output;
				array[4, 0] = "ConnectionStringExpiry";
				array[4, 1] = typeof(DateTime);
				array[4, 2] = ParseUtils.DateTimeParser;
				array[4, 3] = SqlDbType.SmallDateTime;
				array[4, 4] = ParameterDirection.Output;
				SqlOutputStream.ConnectionStringManager.PartitionInfoColumnDefinitions = array;
			}

			// Token: 0x040000BF RID: 191
			private static readonly object[,] PartitionInfoColumnDefinitions;

			// Token: 0x040000C0 RID: 192
			private readonly SqlCommand partitionCommand;

			// Token: 0x040000C1 RID: 193
			private readonly object getPartitionLock = new object();

			// Token: 0x040000C2 RID: 194
			private readonly TimeSpan logRetryEventInterval;

			// Token: 0x040000C3 RID: 195
			private readonly SqlConnectionLogger sqlConnectionLogger;

			// Token: 0x040000C4 RID: 196
			private DateTime timeLastErrorReportedGetPartition;

			// Token: 0x040000C5 RID: 197
			private DateTime timeLastErrorReportedValidateConnectionString;

			// Token: 0x040000C6 RID: 198
			private Dictionary<string, SqlOutputStream.PartitionRecord[]> partitionRecords;

			// Token: 0x040000C7 RID: 199
			private string[] connectionStrings;

			// Token: 0x0200002C RID: 44
			private enum PartitionCommandIndex
			{
				// Token: 0x040000C9 RID: 201
				DataStartPeriod,
				// Token: 0x040000CA RID: 202
				MachineName,
				// Token: 0x040000CB RID: 203
				Tier,
				// Token: 0x040000CC RID: 204
				ConnectionString,
				// Token: 0x040000CD RID: 205
				ConnectionStringExpiry
			}
		}

		// Token: 0x0200002D RID: 45
		private class PartitionRecord
		{
			// Token: 0x060000FB RID: 251 RVA: 0x0000B3FC File Offset: 0x000095FC
			public PartitionRecord(DateTime validPeriodStart, DateTime validPeriodEnd, string connectionString)
			{
				this.validPeriodStart = validPeriodStart;
				this.validPeriodEnd = validPeriodEnd;
				this.connectionString = connectionString;
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000FC RID: 252 RVA: 0x0000B419 File Offset: 0x00009619
			public DateTime ValidPeriodStart
			{
				get
				{
					return this.validPeriodStart;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000FD RID: 253 RVA: 0x0000B421 File Offset: 0x00009621
			public DateTime ValidPeriodEnd
			{
				get
				{
					return this.validPeriodEnd;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000FE RID: 254 RVA: 0x0000B429 File Offset: 0x00009629
			public string ConnectionString
			{
				get
				{
					return this.connectionString;
				}
			}

			// Token: 0x040000CE RID: 206
			public static readonly SqlOutputStream.PartitionRecord Empty = new SqlOutputStream.PartitionRecord(DateTime.MaxValue, DateTime.MinValue, string.Empty);

			// Token: 0x040000CF RID: 207
			private readonly DateTime validPeriodStart;

			// Token: 0x040000D0 RID: 208
			private readonly DateTime validPeriodEnd;

			// Token: 0x040000D1 RID: 209
			private readonly string connectionString;
		}
	}
}
