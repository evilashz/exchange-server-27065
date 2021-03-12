using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000E0 RID: 224
	internal static class SqlClientFactory
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0002F41C File Offset: 0x0002D61C
		internal static ISqlClientFactory Instance
		{
			get
			{
				return SqlClientFactory.hookableFactory.Value;
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002F428 File Offset: 0x0002D628
		public static ISqlCommand CreateSqlCommand()
		{
			return SqlClientFactory.Instance.CreateSqlCommand();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002F434 File Offset: 0x0002D634
		public static ISqlCommand CreateSqlCommand(SqlCommand command)
		{
			return SqlClientFactory.Instance.CreateSqlCommand(command);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002F441 File Offset: 0x0002D641
		public static ISqlCommand CreateSqlCommand(string commandText, ISqlConnection connection, ISqlTransaction transaction)
		{
			return SqlClientFactory.Instance.CreateSqlCommand(commandText, connection, transaction);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002F450 File Offset: 0x0002D650
		public static ISqlConnection CreateSqlConnection(SqlConnection connection)
		{
			return SqlClientFactory.Instance.CreateSqlConnection(connection);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002F45D File Offset: 0x0002D65D
		public static ISqlConnection CreateSqlConnection(string connectionString)
		{
			return SqlClientFactory.Instance.CreateSqlConnection(connectionString);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002F46A File Offset: 0x0002D66A
		public static ISqlDataReader CreateSqlDataReader(SqlDataReader reader)
		{
			return SqlClientFactory.Instance.CreateSqlDataReader(reader);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002F477 File Offset: 0x0002D677
		public static ISqlTransaction CreateSqlTransaction(SqlTransaction transaction)
		{
			return SqlClientFactory.Instance.CreateSqlTransaction(transaction);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002F484 File Offset: 0x0002D684
		internal static IDisposable SetTestHook(ISqlClientFactory factory)
		{
			return SqlClientFactory.hookableFactory.SetTestHook(factory);
		}

		// Token: 0x04000341 RID: 833
		private static Hookable<ISqlClientFactory> hookableFactory = Hookable<ISqlClientFactory>.Create(true, new SqlClientFactory.Factory());

		// Token: 0x020000E1 RID: 225
		private class Factory : ISqlClientFactory
		{
			// Token: 0x06000999 RID: 2457 RVA: 0x0002F4A3 File Offset: 0x0002D6A3
			public ISqlCommand CreateSqlCommand()
			{
				return new SqlClientFactory.SqlCommandWrapper();
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x0002F4AA File Offset: 0x0002D6AA
			public ISqlCommand CreateSqlCommand(SqlCommand command)
			{
				if (command != null)
				{
					return new SqlClientFactory.SqlCommandWrapper(command);
				}
				return null;
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x0002F4B7 File Offset: 0x0002D6B7
			public ISqlCommand CreateSqlCommand(string commandText, ISqlConnection connection, ISqlTransaction transaction)
			{
				return new SqlClientFactory.SqlCommandWrapper(commandText, connection, transaction);
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x0002F4C1 File Offset: 0x0002D6C1
			public ISqlConnection CreateSqlConnection(SqlConnection connection)
			{
				if (connection != null)
				{
					return new SqlClientFactory.SqlConnectionWrapper(connection);
				}
				return null;
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x0002F4CE File Offset: 0x0002D6CE
			public ISqlConnection CreateSqlConnection(string connectionString)
			{
				return new SqlClientFactory.SqlConnectionWrapper(connectionString);
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x0002F4D6 File Offset: 0x0002D6D6
			public ISqlDataReader CreateSqlDataReader(SqlDataReader reader)
			{
				if (reader != null)
				{
					return new SqlClientFactory.SqlDataReaderWrapper(reader);
				}
				return null;
			}

			// Token: 0x0600099F RID: 2463 RVA: 0x0002F4E3 File Offset: 0x0002D6E3
			public ISqlTransaction CreateSqlTransaction(SqlTransaction transaction)
			{
				if (transaction != null)
				{
					return new SqlClientFactory.SqlTransactionWrapper(transaction);
				}
				return null;
			}
		}

		// Token: 0x020000E2 RID: 226
		private class SqlCommandWrapper : DisposableBase, ISqlCommand, IDisposable
		{
			// Token: 0x060009A1 RID: 2465 RVA: 0x0002F4F8 File Offset: 0x0002D6F8
			public SqlCommandWrapper() : this(new SqlCommand())
			{
			}

			// Token: 0x060009A2 RID: 2466 RVA: 0x0002F505 File Offset: 0x0002D705
			public SqlCommandWrapper(SqlCommand command)
			{
				this.command = command;
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0002F514 File Offset: 0x0002D714
			public SqlCommandWrapper(string commandText, ISqlConnection connection, ISqlTransaction transaction)
			{
				try
				{
					SqlConnection connection2 = (connection == null) ? null : connection.WrappedConnection;
					SqlTransaction transaction2 = (transaction == null) ? null : transaction.WrappedTransaction;
					this.command = new SqlCommand(commandText, connection2, transaction2);
				}
				finally
				{
					if (this.command == null)
					{
						base.Dispose();
					}
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0002F570 File Offset: 0x0002D770
			// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0002F57D File Offset: 0x0002D77D
			public string CommandText
			{
				get
				{
					return this.command.CommandText;
				}
				set
				{
					this.command.CommandText = value;
				}
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0002F58B File Offset: 0x0002D78B
			// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0002F598 File Offset: 0x0002D798
			public int CommandTimeout
			{
				get
				{
					return this.command.CommandTimeout;
				}
				set
				{
					this.command.CommandTimeout = value;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002F5A6 File Offset: 0x0002D7A6
			// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0002F5B3 File Offset: 0x0002D7B3
			public CommandType CommandType
			{
				get
				{
					return this.command.CommandType;
				}
				set
				{
					this.command.CommandType = value;
				}
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002F5C4 File Offset: 0x0002D7C4
			// (set) Token: 0x060009AB RID: 2475 RVA: 0x0002F62D File Offset: 0x0002D82D
			public ISqlConnection Connection
			{
				get
				{
					SqlConnection connection = this.command.Connection;
					if (connection == null)
					{
						return null;
					}
					if (this.activeConnection == null)
					{
						this.activeConnection = SqlClientFactory.CreateSqlConnection(connection);
					}
					else if (!object.ReferenceEquals(this.activeConnection.WrappedConnection, connection))
					{
						this.activeConnection.WrappedConnection.Dispose();
						this.activeConnection.WrappedConnection = connection;
					}
					return this.activeConnection;
				}
				set
				{
					this.command.Connection = ((value == null) ? null : value.WrappedConnection);
				}
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002F646 File Offset: 0x0002D846
			public SqlParameterCollection Parameters
			{
				get
				{
					return this.command.Parameters;
				}
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002F654 File Offset: 0x0002D854
			// (set) Token: 0x060009AE RID: 2478 RVA: 0x0002F6BD File Offset: 0x0002D8BD
			public ISqlTransaction Transaction
			{
				get
				{
					SqlTransaction transaction = this.command.Transaction;
					if (transaction == null)
					{
						return null;
					}
					if (this.activeTransaction == null)
					{
						this.activeTransaction = SqlClientFactory.CreateSqlTransaction(transaction);
					}
					else if (!object.ReferenceEquals(this.activeTransaction.WrappedTransaction, transaction))
					{
						this.activeTransaction.WrappedTransaction.Dispose();
						this.activeTransaction.WrappedTransaction = transaction;
					}
					return this.activeTransaction;
				}
				set
				{
					this.command.Transaction = ((value == null) ? null : value.WrappedTransaction);
				}
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x0002F6D6 File Offset: 0x0002D8D6
			public override string ToString()
			{
				return this.command.ToString();
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x0002F6E3 File Offset: 0x0002D8E3
			public override bool Equals(object other)
			{
				return this.command.Equals(other);
			}

			// Token: 0x060009B1 RID: 2481 RVA: 0x0002F6F1 File Offset: 0x0002D8F1
			public override int GetHashCode()
			{
				return this.command.GetHashCode();
			}

			// Token: 0x060009B2 RID: 2482 RVA: 0x0002F6FE File Offset: 0x0002D8FE
			public int ExecuteNonQuery()
			{
				return this.command.ExecuteNonQuery();
			}

			// Token: 0x060009B3 RID: 2483 RVA: 0x0002F70B File Offset: 0x0002D90B
			public ISqlDataReader ExecuteReader()
			{
				return SqlClientFactory.CreateSqlDataReader(this.command.ExecuteReader());
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x0002F71D File Offset: 0x0002D91D
			public ISqlDataReader ExecuteReader(CommandBehavior behavior)
			{
				return SqlClientFactory.CreateSqlDataReader(this.command.ExecuteReader(behavior));
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x0002F730 File Offset: 0x0002D930
			public object ExecuteScalar()
			{
				return this.command.ExecuteScalar();
			}

			// Token: 0x060009B6 RID: 2486 RVA: 0x0002F740 File Offset: 0x0002D940
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose)
				{
					if (this.command != null)
					{
						this.command.Dispose();
						this.command = null;
					}
					if (this.activeConnection != null)
					{
						this.activeConnection.Dispose();
						this.activeConnection = null;
					}
					if (this.activeTransaction != null)
					{
						this.activeTransaction.Dispose();
						this.activeTransaction = null;
					}
				}
			}

			// Token: 0x060009B7 RID: 2487 RVA: 0x0002F79E File Offset: 0x0002D99E
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SqlClientFactory.SqlCommandWrapper>(this);
			}

			// Token: 0x04000342 RID: 834
			private SqlCommand command;

			// Token: 0x04000343 RID: 835
			private ISqlConnection activeConnection;

			// Token: 0x04000344 RID: 836
			private ISqlTransaction activeTransaction;
		}

		// Token: 0x020000E3 RID: 227
		private class SqlConnectionWrapper : DisposableBase, ISqlConnection, IDisposable
		{
			// Token: 0x060009B8 RID: 2488 RVA: 0x0002F7A6 File Offset: 0x0002D9A6
			public SqlConnectionWrapper(SqlConnection connection)
			{
				this.connection = connection;
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x0002F7B8 File Offset: 0x0002D9B8
			public SqlConnectionWrapper(string connectionString)
			{
				try
				{
					this.connection = new SqlConnection(connectionString);
				}
				finally
				{
					if (this.connection == null)
					{
						base.Dispose();
					}
				}
			}

			// Token: 0x14000002 RID: 2
			// (add) Token: 0x060009BA RID: 2490 RVA: 0x0002F7F8 File Offset: 0x0002D9F8
			// (remove) Token: 0x060009BB RID: 2491 RVA: 0x0002F806 File Offset: 0x0002DA06
			public event SqlInfoMessageEventHandler InfoMessage
			{
				add
				{
					this.connection.InfoMessage += value;
				}
				remove
				{
					this.connection.InfoMessage -= value;
				}
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x060009BC RID: 2492 RVA: 0x0002F814 File Offset: 0x0002DA14
			public ConnectionState State
			{
				get
				{
					return this.connection.State;
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002F821 File Offset: 0x0002DA21
			// (set) Token: 0x060009BE RID: 2494 RVA: 0x0002F829 File Offset: 0x0002DA29
			public SqlConnection WrappedConnection
			{
				get
				{
					return this.connection;
				}
				set
				{
					this.connection = value;
				}
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x0002F832 File Offset: 0x0002DA32
			public override string ToString()
			{
				return this.connection.ToString();
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x0002F83F File Offset: 0x0002DA3F
			public override bool Equals(object other)
			{
				return this.connection.Equals(other);
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x0002F84D File Offset: 0x0002DA4D
			public override int GetHashCode()
			{
				return this.connection.GetHashCode();
			}

			// Token: 0x060009C2 RID: 2498 RVA: 0x0002F85A File Offset: 0x0002DA5A
			public ISqlTransaction BeginTransaction(IsolationLevel iso)
			{
				return SqlClientFactory.CreateSqlTransaction(this.connection.BeginTransaction(iso));
			}

			// Token: 0x060009C3 RID: 2499 RVA: 0x0002F86D File Offset: 0x0002DA6D
			public void ClearPool()
			{
				SqlConnection.ClearPool(this.connection);
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x0002F87A File Offset: 0x0002DA7A
			public void Close()
			{
				this.connection.Close();
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x0002F887 File Offset: 0x0002DA87
			public ISqlCommand CreateCommand()
			{
				return SqlClientFactory.CreateSqlCommand(this.connection.CreateCommand());
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x0002F899 File Offset: 0x0002DA99
			public void Open()
			{
				this.connection.Open();
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x0002F8A6 File Offset: 0x0002DAA6
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose && this.connection != null)
				{
					this.connection.Dispose();
					this.connection = null;
				}
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x0002F8C5 File Offset: 0x0002DAC5
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SqlClientFactory.SqlConnectionWrapper>(this);
			}

			// Token: 0x04000345 RID: 837
			private SqlConnection connection;
		}

		// Token: 0x020000E4 RID: 228
		private class SqlDataReaderWrapper : DisposableBase, ISqlDataReader, IDisposable
		{
			// Token: 0x060009C9 RID: 2505 RVA: 0x0002F8CD File Offset: 0x0002DACD
			public SqlDataReaderWrapper(SqlDataReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x060009CA RID: 2506 RVA: 0x0002F8DC File Offset: 0x0002DADC
			public int FieldCount
			{
				get
				{
					return this.reader.FieldCount;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002F8E9 File Offset: 0x0002DAE9
			public bool IsClosed
			{
				get
				{
					return this.reader.IsClosed;
				}
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x0002F8F6 File Offset: 0x0002DAF6
			public override string ToString()
			{
				return this.reader.ToString();
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x0002F903 File Offset: 0x0002DB03
			public override bool Equals(object other)
			{
				return this.reader.Equals(other);
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x0002F911 File Offset: 0x0002DB11
			public override int GetHashCode()
			{
				return this.reader.GetHashCode();
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x0002F91E File Offset: 0x0002DB1E
			public void Close()
			{
				this.reader.Close();
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x0002F92B File Offset: 0x0002DB2B
			public bool GetBoolean(int i)
			{
				return this.reader.GetBoolean(i);
			}

			// Token: 0x060009D1 RID: 2513 RVA: 0x0002F939 File Offset: 0x0002DB39
			public long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
			{
				return this.reader.GetBytes(i, dataIndex, buffer, bufferIndex, length);
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x0002F94D File Offset: 0x0002DB4D
			public long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
			{
				return this.reader.GetChars(i, dataIndex, buffer, bufferIndex, length);
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x0002F961 File Offset: 0x0002DB61
			public DateTime GetDateTime(int i)
			{
				return this.reader.GetDateTime(i);
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x0002F96F File Offset: 0x0002DB6F
			public Type GetFieldType(int i)
			{
				return this.reader.GetFieldType(i);
			}

			// Token: 0x060009D5 RID: 2517 RVA: 0x0002F97D File Offset: 0x0002DB7D
			public Guid GetGuid(int i)
			{
				return this.reader.GetGuid(i);
			}

			// Token: 0x060009D6 RID: 2518 RVA: 0x0002F98B File Offset: 0x0002DB8B
			public short GetInt16(int i)
			{
				return this.reader.GetInt16(i);
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x0002F999 File Offset: 0x0002DB99
			public int GetInt32(int i)
			{
				return this.reader.GetInt32(i);
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x0002F9A7 File Offset: 0x0002DBA7
			public long GetInt64(int i)
			{
				return this.reader.GetInt64(i);
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x0002F9B5 File Offset: 0x0002DBB5
			public string GetName(int i)
			{
				return this.reader.GetName(i);
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x0002F9C3 File Offset: 0x0002DBC3
			public int GetOrdinal(string name)
			{
				return this.reader.GetOrdinal(name);
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x0002F9D1 File Offset: 0x0002DBD1
			public SqlBinary GetSqlBinary(int i)
			{
				return this.reader.GetSqlBinary(i);
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x0002F9DF File Offset: 0x0002DBDF
			public string GetString(int i)
			{
				return this.reader.GetString(i);
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x0002F9ED File Offset: 0x0002DBED
			public object GetValue(int i)
			{
				return this.reader.GetValue(i);
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x0002F9FB File Offset: 0x0002DBFB
			public bool IsDBNull(int i)
			{
				return this.reader.IsDBNull(i);
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x0002FA09 File Offset: 0x0002DC09
			public bool NextResult()
			{
				return this.reader.NextResult();
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x0002FA16 File Offset: 0x0002DC16
			public bool Read()
			{
				return this.reader.Read();
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x0002FA23 File Offset: 0x0002DC23
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose)
				{
					this.reader.Dispose();
					this.reader = null;
				}
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x0002FA3A File Offset: 0x0002DC3A
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SqlClientFactory.SqlDataReaderWrapper>(this);
			}

			// Token: 0x04000346 RID: 838
			private SqlDataReader reader;
		}

		// Token: 0x020000E5 RID: 229
		private class SqlTransactionWrapper : DisposableBase, ISqlTransaction, IDisposable
		{
			// Token: 0x060009E3 RID: 2531 RVA: 0x0002FA42 File Offset: 0x0002DC42
			public SqlTransactionWrapper(SqlTransaction transaction)
			{
				this.transaction = transaction;
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002FA51 File Offset: 0x0002DC51
			// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0002FA59 File Offset: 0x0002DC59
			public SqlTransaction WrappedTransaction
			{
				get
				{
					return this.transaction;
				}
				set
				{
					this.transaction = value;
				}
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x0002FA62 File Offset: 0x0002DC62
			public override string ToString()
			{
				return this.transaction.ToString();
			}

			// Token: 0x060009E7 RID: 2535 RVA: 0x0002FA6F File Offset: 0x0002DC6F
			public override bool Equals(object other)
			{
				return this.transaction.Equals(other);
			}

			// Token: 0x060009E8 RID: 2536 RVA: 0x0002FA7D File Offset: 0x0002DC7D
			public override int GetHashCode()
			{
				return this.transaction.GetHashCode();
			}

			// Token: 0x060009E9 RID: 2537 RVA: 0x0002FA8A File Offset: 0x0002DC8A
			public void Commit()
			{
				this.transaction.Commit();
			}

			// Token: 0x060009EA RID: 2538 RVA: 0x0002FA97 File Offset: 0x0002DC97
			public void Rollback()
			{
				this.transaction.Rollback();
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose)
				{
					this.transaction.Dispose();
					this.transaction = null;
				}
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x0002FABB File Offset: 0x0002DCBB
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SqlClientFactory.SqlTransactionWrapper>(this);
			}

			// Token: 0x04000347 RID: 839
			private SqlTransaction transaction;
		}
	}
}
