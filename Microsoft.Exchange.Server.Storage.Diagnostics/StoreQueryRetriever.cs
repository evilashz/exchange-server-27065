using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PhysicalAccessJet;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000055 RID: 85
	public class StoreQueryRetriever : DiagnosticQueryRetriever
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00010A4C File Offset: 0x0000EC4C
		private StoreQueryRetriever(DiagnosticQueryResults results) : base(results)
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00010A55 File Offset: 0x0000EC55
		internal static StoreQueryRetriever.IStoreDatabaseFactory DatabaseFactory
		{
			get
			{
				return StoreQueryRetriever.hookableDatabaseFactory.Value;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010A64 File Offset: 0x0000EC64
		public new static DiagnosticQueryRetriever Create(DiagnosticQueryParser parser, DiagnosableParameters parameters)
		{
			DiagnosticQueryRetriever result;
			using (Context context = StoreQueryRetriever.StoreQueryContext.Create())
			{
				StoreDatabase database = StoreQueryRetriever.DatabaseFactory.GetDatabase(context, parser.From.Database);
				using (context.AssociateWithDatabase(database))
				{
					result = StoreQueryRetriever.Create(context, parser, parameters);
				}
			}
			return result;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public static DiagnosticQueryRetriever Create(Context context, DiagnosticQueryParser parser, DiagnosableParameters parameters)
		{
			StoreQueryTranslator translator = StoreQueryTranslator.Create(context, parser, parameters);
			return StoreQueryRetriever.Retrieve(context, translator);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00010AF5 File Offset: 0x0000ECF5
		internal static IDisposable SetTestHook(StoreQueryRetriever.IStoreDatabaseFactory factory)
		{
			return StoreQueryRetriever.hookableDatabaseFactory.SetTestHook(factory);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00010B04 File Offset: 0x0000ED04
		internal static string GetMailboxDisplayName(Context context, int mailboxNumber)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			SearchCriteria restriction = Factory.CreateSearchCriteriaCompare(mailboxTable.MailboxNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailboxNumber));
			try
			{
				context.BeginTransactionIfNeeded();
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, mailboxTable.Table, mailboxTable.Table.PrimaryKeyIndex, new Column[]
				{
					mailboxTable.MailboxGuid
				}, restriction, null, 0, 1, KeyRange.AllRows, false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (reader.Read())
						{
							object value = reader.GetValue(mailboxTable.MailboxGuid);
							if (value != null)
							{
								return value.ToString();
							}
						}
					}
				}
			}
			finally
			{
				try
				{
					context.Commit();
				}
				finally
				{
					context.Abort();
				}
			}
			return null;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00010C08 File Offset: 0x0000EE08
		private static DiagnosticQueryRetriever Retrieve(Context context, StoreQueryTranslator translator)
		{
			switch (translator.QueryType)
			{
			case DiagnosticQueryParser.QueryType.Select:
				return StoreQueryRetriever.QuerySelect(context, translator);
			case DiagnosticQueryParser.QueryType.Update:
				return StoreQueryRetriever.QueryUpdate(context, translator);
			case DiagnosticQueryParser.QueryType.Insert:
				return StoreQueryRetriever.QueryInsert(context, translator);
			case DiagnosticQueryParser.QueryType.Delete:
				return StoreQueryRetriever.QueryDelete(context, translator);
			default:
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.UnsupportedQueryType());
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00010C60 File Offset: 0x0000EE60
		private static DiagnosticQueryRetriever QuerySelect(Context context, StoreQueryTranslator translator)
		{
			if (translator.From.Table is TableFunction)
			{
				return StoreQueryRetriever.QuerySelectTableFunction(context, translator);
			}
			return StoreQueryRetriever.QuerySelectTable(context, translator);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00010D6C File Offset: 0x0000EF6C
		private static DiagnosticQueryRetriever QuerySelectTable(Context context, StoreQueryTranslator translator)
		{
			SearchCriteria criteria = translator.Where ?? Factory.CreateSearchCriteriaTrue();
			DiagnosticQueryRetriever result;
			try
			{
				context.BeginTransactionIfNeeded();
				if (translator.IsCountQuery)
				{
					result = StoreQueryRetriever.GetCount(context, translator, delegate
					{
						QueryPlanner queryPlanner = new QueryPlanner(context, translator.From.Table, null, criteria, null, null, null, null, null, null, null, SortOrder.Empty, Bookmark.BOT, 0, 0, false, false, false, false, false, QueryPlanner.Hints.Empty);
						return queryPlanner.CreateCountPlan();
					});
				}
				else
				{
					SortOrder sortOrder = StoreQueryRetriever.GetSortOrder(translator);
					result = StoreQueryRetriever.GetRows(context, translator, delegate
					{
						QueryPlanner queryPlanner = new QueryPlanner(context, translator.From.Table, null, criteria, null, null, translator.Fetch, null, null, null, null, sortOrder, Bookmark.BOT, 0, translator.MaxRows, false, false, false, false, false, QueryPlanner.Hints.Empty);
						return queryPlanner.CreatePlan();
					});
				}
			}
			finally
			{
				try
				{
					context.Commit();
				}
				finally
				{
					context.Abort();
				}
			}
			return result;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00010F5C File Offset: 0x0000F15C
		private static DiagnosticQueryRetriever QuerySelectTableFunction(Context context, StoreQueryTranslator translator)
		{
			StoreQueryRetriever.<>c__DisplayClassa CS$<>8__locals1 = new StoreQueryRetriever.<>c__DisplayClassa();
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.translator = translator;
			CS$<>8__locals1.tableFunction = (TableFunction)CS$<>8__locals1.translator.From.Table;
			CS$<>8__locals1.criteria = (CS$<>8__locals1.translator.Where ?? Factory.CreateSearchCriteriaTrue());
			if (CS$<>8__locals1.translator.IsCountQuery)
			{
				return StoreQueryRetriever.GetCount(CS$<>8__locals1.context, CS$<>8__locals1.translator, delegate
				{
					QueryPlanner queryPlanner = new QueryPlanner(CS$<>8__locals1.context, CS$<>8__locals1.translator.From.Table, CS$<>8__locals1.translator.From.Parameters, CS$<>8__locals1.criteria, null, null, null, null, null, null, null, SortOrder.Empty, Bookmark.BOT, 0, 0, false, false, false, false, false, QueryPlanner.Hints.Empty);
					return queryPlanner.CreateCountPlan();
				});
			}
			SortOrder sortOrder = StoreQueryRetriever.GetSortOrder(CS$<>8__locals1.translator);
			return StoreQueryRetriever.GetRows(CS$<>8__locals1.context, CS$<>8__locals1.translator, delegate
			{
				QueryPlanner queryPlanner = new QueryPlanner(CS$<>8__locals1.context, CS$<>8__locals1.tableFunction, CS$<>8__locals1.translator.From.Parameters, CS$<>8__locals1.criteria, null, null, CS$<>8__locals1.translator.Fetch, null, null, null, null, sortOrder, Bookmark.BOT, 0, CS$<>8__locals1.translator.MaxRows, false, false, false, false, false, QueryPlanner.Hints.Empty);
				return queryPlanner.CreatePlan();
			});
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011028 File Offset: 0x0000F228
		private static StoreQueryRetriever GetCount(Context context, StoreQueryTranslator translator, Func<DataAccessOperator> getQueryOperator)
		{
			StoreQueryRetriever result;
			try
			{
				using (DataAccessOperator dataAccessOperator = getQueryOperator())
				{
					object obj = dataAccessOperator.ExecuteScalar();
					string name = translator.Select[0].Name;
					int estimatedData = (obj is int) ? ((int)obj) : 4;
					StoreQueryRetriever.WriteQueryTrace(context, translator, 1, estimatedData);
					result = new StoreQueryRetriever(DiagnosticQueryResults.Create(new string[]
					{
						name
					}, new Type[]
					{
						typeof(int)
					}, new uint[]
					{
						(uint)Math.Max(10, name.Length)
					}, new object[][]
					{
						new object[]
						{
							obj
						}
					}, false, false));
				}
			}
			catch (StoreException ex)
			{
				if (translator.From.Table.IsPartitioned)
				{
					throw new DiagnosticQueryRetrieverException(DiagnosticQueryStrings.PartitionedTable(translator.From.Table.Name, from c in translator.From.Table.Columns.Take(translator.From.Table.SpecialCols.NumberOfPartioningColumns)
					select c.Name));
				}
				throw new DiagnosticQueryRetrieverException(ex.Message);
			}
			catch (NonFatalDatabaseException ex2)
			{
				throw new DiagnosticQueryRetrieverException(ex2.Message);
			}
			catch (InvalidSerializedFormatException ex3)
			{
				throw new DiagnosticQueryRetrieverException(ex3.Message);
			}
			catch (TimeoutException ex4)
			{
				throw new DiagnosticQueryRetrieverException(ex4.Message);
			}
			catch (CommunicationException ex5)
			{
				throw new DiagnosticQueryRetrieverException(ex5.Message);
			}
			return result;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000113AC File Offset: 0x0000F5AC
		private static StoreQueryRetriever GetRows(Context context, StoreQueryTranslator translator, Func<SimpleQueryOperator> getQueryOperator)
		{
			StoreQueryRetriever result;
			try
			{
				using (SimpleQueryOperator op = getQueryOperator())
				{
					bool isSingleRowQuery = StoreQueryRetriever.IsSingleRowQuery(translator, op);
					List<string> listOfProperty = StoreQueryRetriever.GetListOfProperty<string>(translator.Select, (Column c) => c.Name);
					List<Type> listOfProperty2 = StoreQueryRetriever.GetListOfProperty<Type>(translator.Select, (Column c) => c.Type);
					List<uint> listOfProperty3 = StoreQueryRetriever.GetListOfProperty<uint>(translator.Select, delegate(Column c)
					{
						if (c.Name != null)
						{
							return (uint)c.Name.Length;
						}
						return 0U;
					});
					List<object[]> list = new List<object[]>();
					bool flag = false;
					bool interrupted = false;
					int num = 0;
					int value = ConfigurationSchema.StoreQueryMaximumResultSize.Value;
					if (op != null)
					{
						StoreQueryRetriever.<>c__DisplayClass1f CS$<>8__locals4 = new StoreQueryRetriever.<>c__DisplayClass1f();
						CS$<>8__locals4.access = (op as IColumnStreamAccess);
						using (Reader reader = op.ExecuteReader(false))
						{
							if (!translator.Unlimited)
							{
								reader.EnableInterrupts(StoreQueryRetriever.ExpensiveQueryInterruptControl.Create());
							}
							while (reader.Read() && !reader.Interrupted && !flag)
							{
								foreach (Processor processor in translator.Processors)
								{
									processor.OnBeginRow();
								}
								List<object> listOfProperty4 = StoreQueryRetriever.GetListOfProperty<object>(translator.Select, delegate(Column col)
								{
									ConstantColumn constantColumn = col as ConstantColumn;
									Processor processor3 = (constantColumn != null) ? (constantColumn.Value as Processor) : null;
									if (processor3 != null)
									{
										object value2 = processor3.GetValue(op, reader, col);
										return StoreQueryRetriever.ApplyVisibility(translator, col, value2);
									}
									object value3 = reader.GetValue(col);
									byte[][] array = value3 as byte[][];
									LargeValue largeValue = value3 as LargeValue;
									PhysicalColumn physicalColumn = col as PhysicalColumn;
									if (array != null)
									{
										if (array.Length > 0)
										{
											int num2 = SerializedValue.SerializeMVBinary(array, null, 0);
											if (num2 > 0)
											{
												byte[] array2 = new byte[num2];
												SerializedValue.SerializeMVBinary(array, array2, 0);
												return array2;
											}
										}
										return new byte[0];
									}
									if (largeValue != null)
									{
										if (isSingleRowQuery && CS$<>8__locals4.access != null && physicalColumn != null)
										{
											int columnSize = CS$<>8__locals4.access.GetColumnSize(physicalColumn);
											byte[] array3 = new byte[columnSize];
											if (CS$<>8__locals4.access.ReadStream(physicalColumn, 0L, array3, 0, array3.Length) == columnSize)
											{
												return StoreQueryRetriever.ApplyVisibility(translator, col, array3);
											}
										}
										return DiagnosticQueryStrings.TruncatedColumnValue(largeValue.ActualLength);
									}
									return StoreQueryRetriever.ApplyVisibility(translator, col, value3);
								});
								StoreQueryRetriever.AdjustWidths(listOfProperty3, listOfProperty4);
								list.Add(listOfProperty4.ToArray());
								num += StoreQueryRetriever.EstimateRowSize(listOfProperty4);
								if (num > value)
								{
									flag = true;
								}
								foreach (Processor processor2 in translator.Processors)
								{
									processor2.OnAfterRow();
								}
							}
							interrupted = reader.Interrupted;
						}
					}
					StoreQueryRetriever.WriteQueryTrace(context, translator, list.Count, num);
					result = new StoreQueryRetriever(DiagnosticQueryResults.Create(listOfProperty, listOfProperty2, listOfProperty3, list, flag, interrupted));
				}
			}
			catch (StoreException ex)
			{
				if (translator.From.Table.IsPartitioned)
				{
					throw new DiagnosticQueryRetrieverException(DiagnosticQueryStrings.PartitionedTable(translator.From.Table.Name, from c in translator.From.Table.Columns.Take(translator.From.Table.SpecialCols.NumberOfPartioningColumns)
					select c.Name));
				}
				throw new DiagnosticQueryRetrieverException(ex.Message);
			}
			catch (NonFatalDatabaseException ex2)
			{
				throw new DiagnosticQueryRetrieverException(ex2.Message);
			}
			catch (InvalidSerializedFormatException ex3)
			{
				throw new DiagnosticQueryRetrieverException(ex3.Message);
			}
			catch (CommunicationException ex4)
			{
				throw new DiagnosticQueryRetrieverException(ex4.Message);
			}
			catch (TimeoutException ex5)
			{
				throw new DiagnosticQueryRetrieverException(ex5.Message);
			}
			return result;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00011848 File Offset: 0x0000FA48
		private static DiagnosticQueryRetriever QueryInsert(Context context, StoreQueryTranslator translator)
		{
			throw new DiagnosticQueryRetrieverException(DiagnosticQueryStrings.UnimplementedKeyword());
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011854 File Offset: 0x0000FA54
		private static DiagnosticQueryRetriever QueryUpdate(Context context, StoreQueryTranslator translator)
		{
			throw new DiagnosticQueryRetrieverException(DiagnosticQueryStrings.UnimplementedKeyword());
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00011860 File Offset: 0x0000FA60
		private static DiagnosticQueryRetriever QueryDelete(Context context, StoreQueryTranslator translator)
		{
			throw new DiagnosticQueryRetrieverException(DiagnosticQueryStrings.UnimplementedKeyword());
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0001186C File Offset: 0x0000FA6C
		private static SortOrder GetSortOrder(StoreQueryTranslator translator)
		{
			SortOrderBuilder sortOrderBuilder = new SortOrderBuilder(translator.OrderBy);
			foreach (Column column in translator.From.Table.PrimaryKeyIndex.Columns)
			{
				if (!sortOrderBuilder.Contains(column))
				{
					sortOrderBuilder.Add(column);
				}
			}
			return sortOrderBuilder.ToSortOrder();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000118E4 File Offset: 0x0000FAE4
		private static List<T> GetListOfProperty<T>(IEnumerable<Column> columns, Func<Column, T> getter)
		{
			List<T> list = new List<T>();
			foreach (Column arg in columns)
			{
				list.Add(getter(arg));
			}
			return list;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001193C File Offset: 0x0000FB3C
		private static void AdjustWidths(List<uint> widths, List<object> values)
		{
			for (int i = 0; i < widths.Count; i++)
			{
				widths[i] = Math.Max(widths[i], StoreQueryRetriever.GetWidthAccordingToType(values[i]));
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001197C File Offset: 0x0000FB7C
		private static uint GetWidthAccordingToType(object value)
		{
			if (value is string)
			{
				return (uint)((string)value).Length;
			}
			if (value is byte[])
			{
				return (uint)Math.Max(((byte[])value).Length * 2 + 2, 0);
			}
			if (value is Guid)
			{
				return 40U;
			}
			if (value is long || value is ulong)
			{
				return 20U;
			}
			if (value is DateTime)
			{
				return (uint)value.ToString().Length;
			}
			return 10U;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000119EC File Offset: 0x0000FBEC
		private static int EstimateRowSize(IList<object> row)
		{
			int num = 0;
			foreach (object obj in row)
			{
				string text = obj as string;
				byte[] array = obj as byte[];
				if (obj == null)
				{
					num += 4;
				}
				else if (text != null)
				{
					num += text.Length;
				}
				else if (array != null)
				{
					num += array.Length * 2 + 2;
				}
				else if (obj is DateTime)
				{
					num += 26;
				}
				else if (obj is Guid)
				{
					num += 36;
				}
				else if (obj is bool)
				{
					num += 5;
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		private static void WriteQueryTrace(Context context, StoreQueryTranslator translator, int numberOfRows, int estimatedData)
		{
			IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.DiagnosticQuery);
			if (logger == null || !logger.IsLoggingEnabled)
			{
				return;
			}
			string text = (translator.MailboxNumber != null) ? StoreQueryRetriever.GetMailboxDisplayName(context, translator.MailboxNumber.Value) : "Unknown Mailbox";
			Guid diagnosticQuery = LoggerManager.TraceGuids.DiagnosticQuery;
			bool useBufferPool = true;
			bool allowBufferSplit = false;
			string strValue = translator.UserIdentity ?? string.Empty;
			string mdbName = translator.From.Database.MdbName;
			string strValue2 = text;
			string strValue3 = translator.QueryType.ToString();
			string strValue4 = translator.MaxRows.ToString();
			string strValue5;
			if (!translator.IsCountQuery)
			{
				strValue5 = string.Join(", ", from col in translator.Select
				select col.Name);
			}
			else
			{
				strValue5 = "count";
			}
			using (TraceBuffer traceBuffer = TraceRecord.Create(diagnosticQuery, useBufferPool, allowBufferSplit, strValue, mdbName, strValue2, strValue3, strValue4, strValue5, translator.From.Table.Name, (translator.Where != null) ? translator.Where.ToString() : string.Empty, (translator.OrderBy.Count > 0) ? translator.OrderBy.ToString() : string.Empty, numberOfRows, estimatedData, context.Diagnostics.RowStatistics.ReadTotal))
			{
				logger.TryWrite(traceBuffer);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011C10 File Offset: 0x0000FE10
		private static bool IsSingleRowQuery(StoreQueryTranslator translator, SimpleQueryOperator qop)
		{
			TableOperator tableOperator = qop as TableOperator;
			if (translator.MaxRows == 1)
			{
				return true;
			}
			if (tableOperator != null && tableOperator.Index.Unique && tableOperator.KeyRanges.Count == 1)
			{
				StartStopKey startKey = tableOperator.KeyRanges[0].StartKey;
				if (startKey.Inclusive)
				{
					StartStopKey stopKey = tableOperator.KeyRanges[0].StopKey;
					if (stopKey.Inclusive && StartStopKey.CommonKeyPrefix(tableOperator.KeyRanges[0].StartKey, tableOperator.KeyRanges[0].StopKey, tableOperator.CompareInfo) == tableOperator.Index.Columns.Count)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011CCC File Offset: 0x0000FECC
		private static object ApplyVisibility(StoreQueryTranslator translator, Column column, object value)
		{
			if (column.Visibility == Visibility.Public)
			{
				return value;
			}
			if (column.Visibility == Visibility.Redacted && translator.AllowRestricted)
			{
				return value;
			}
			if (column.Type.Equals(typeof(string)) || column.Type.Equals(typeof(byte[])))
			{
				string prefix = VisibilityHelper.GetPrefix(column.Visibility);
				int valueOrDefault = SizeOfColumn.GetColumnSize(column, value).GetValueOrDefault();
				return DiagnosticQueryStrings.RestrictedColumnValue(prefix, valueOrDefault);
			}
			return null;
		}

		// Token: 0x04000194 RID: 404
		private static Hookable<StoreQueryRetriever.IStoreDatabaseFactory> hookableDatabaseFactory = Hookable<StoreQueryRetriever.IStoreDatabaseFactory>.Create(true, new StoreQueryRetriever.InternalDatabaseFactory());

		// Token: 0x02000056 RID: 86
		internal interface IStoreDatabaseFactory
		{
			// Token: 0x060002B5 RID: 693
			StoreDatabase GetDatabase(Context context, string databaseName);
		}

		// Token: 0x02000057 RID: 87
		internal class StoreQueryContext : Context
		{
			// Token: 0x060002B6 RID: 694 RVA: 0x00011D5B File Offset: 0x0000FF5B
			private StoreQueryContext() : base(new ExecutionDiagnostics(), Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.ProcessSecurityContext, false, ClientType.System, CultureHelper.DefaultCultureInfo)
			{
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x00011D74 File Offset: 0x0000FF74
			public static StoreQueryRetriever.StoreQueryContext Create()
			{
				return new StoreQueryRetriever.StoreQueryContext();
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x00011D7C File Offset: 0x0000FF7C
			public override Connection GetConnection()
			{
				if (base.Database.IsOnlineActive || base.Database.IsOnlinePassiveAttachedReadOnly)
				{
					return base.GetConnection();
				}
				if (base.Database.IsOnlinePassive)
				{
					if (this.jetInMemoryConnection == null && base.Database != null)
					{
						this.jetInMemoryConnection = new StoreQueryRetriever.JetInMemoryConnection(this, (JetDatabase)base.Database.PhysicalDatabase, string.Empty);
					}
					return this.jetInMemoryConnection;
				}
				throw new DiagnosticQueryException(DiagnosticQueryStrings.DatabaseOffline(base.Database.MdbName));
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00011E04 File Offset: 0x00010004
			protected override IMailboxContext CreateMailboxContext(int mailboxNumber)
			{
				return StoreQueryRetriever.NullMailboxContext.Create(mailboxNumber, string.Empty);
			}

			// Token: 0x0400019B RID: 411
			private StoreQueryRetriever.JetInMemoryConnection jetInMemoryConnection;
		}

		// Token: 0x02000058 RID: 88
		internal class JetInMemoryConnection : JetConnection
		{
			// Token: 0x060002BA RID: 698 RVA: 0x00011E1E File Offset: 0x0001001E
			public JetInMemoryConnection(IDatabaseExecutionContext outerExecutionContext, JetDatabase database, string identification) : base(outerExecutionContext, database, identification, false)
			{
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00011E2A File Offset: 0x0001002A
			protected override void EnsureJetAccessIsAllowed()
			{
				throw new DiagnosticQueryException("FIXME - need a string for this");
			}
		}

		// Token: 0x02000059 RID: 89
		internal class NullMailboxContext : IMailboxContext
		{
			// Token: 0x060002BC RID: 700 RVA: 0x00011E36 File Offset: 0x00010036
			private NullMailboxContext()
			{
				this.mailboxNumber = 0;
				this.displayName = "Unknown Mailbox";
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00011E50 File Offset: 0x00010050
			private NullMailboxContext(int mailboxNumber, string displayName)
			{
				this.mailboxNumber = mailboxNumber;
				this.displayName = displayName;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060002BE RID: 702 RVA: 0x00011E66 File Offset: 0x00010066
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060002BF RID: 703 RVA: 0x00011E6E File Offset: 0x0001006E
			public bool IsUnifiedMailbox
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x00011E71 File Offset: 0x00010071
			public static IMailboxContext Create(int mailboxNumber, string displayName)
			{
				return new StoreQueryRetriever.NullMailboxContext(mailboxNumber, displayName);
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x00011E7A File Offset: 0x0001007A
			public string GetDisplayName(Context context)
			{
				return this.displayName;
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x00011E82 File Offset: 0x00010082
			public bool GetCreatedByMove(Context context)
			{
				return false;
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x00011E85 File Offset: 0x00010085
			public bool GetPreservingMailboxSignature(Context context)
			{
				return false;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x00011E88 File Offset: 0x00010088
			public bool GetMRSPreservingMailboxSignature(Context context)
			{
				return false;
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x00011E8B File Offset: 0x0001008B
			public HashSet<ushort> GetDefaultPromotedMessagePropertyIds(Context context)
			{
				return null;
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x00011E8E File Offset: 0x0001008E
			public DateTime GetCreationTime(Context context)
			{
				return ParseSerialize.MinFileTimeDateTime;
			}

			// Token: 0x0400019C RID: 412
			public static readonly StoreQueryRetriever.NullMailboxContext Empty = new StoreQueryRetriever.NullMailboxContext();

			// Token: 0x0400019D RID: 413
			private readonly int mailboxNumber;

			// Token: 0x0400019E RID: 414
			private readonly string displayName;
		}

		// Token: 0x0200005A RID: 90
		internal class ExpensiveQueryInterruptControl : IInterruptControl
		{
			// Token: 0x060002C8 RID: 712 RVA: 0x00011EA1 File Offset: 0x000100A1
			public ExpensiveQueryInterruptControl()
			{
				this.started = Stopwatch.StartNew();
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x00011EB4 File Offset: 0x000100B4
			public bool WantToInterrupt
			{
				get
				{
					return this.reads >= ConfigurationSchema.StoreQueryLimitRows.Value || this.started.Elapsed >= ConfigurationSchema.StoreQueryLimitTime.Value;
				}
			}

			// Token: 0x060002CA RID: 714 RVA: 0x00011EE4 File Offset: 0x000100E4
			public static StoreQueryRetriever.ExpensiveQueryInterruptControl Create()
			{
				return new StoreQueryRetriever.ExpensiveQueryInterruptControl();
			}

			// Token: 0x060002CB RID: 715 RVA: 0x00011EEB File Offset: 0x000100EB
			public void RegisterRead(bool probe, TableClass tableClass)
			{
				this.reads++;
			}

			// Token: 0x060002CC RID: 716 RVA: 0x00011EFB File Offset: 0x000100FB
			public void RegisterWrite(TableClass tableClass)
			{
			}

			// Token: 0x060002CD RID: 717 RVA: 0x00011EFD File Offset: 0x000100FD
			public void Reset()
			{
				this.reads = 0;
				this.started.Reset();
			}

			// Token: 0x0400019F RID: 415
			private readonly Stopwatch started;

			// Token: 0x040001A0 RID: 416
			private int reads;
		}

		// Token: 0x0200005B RID: 91
		private class InternalDatabaseFactory : StoreQueryRetriever.IStoreDatabaseFactory
		{
			// Token: 0x060002CE RID: 718 RVA: 0x00011F80 File Offset: 0x00010180
			public StoreDatabase GetDatabase(Context context, string databaseName)
			{
				StoreDatabase foundDatabase = null;
				Storage.ForEachDatabase(context, false, delegate(Context storeContext, StoreDatabase database, Func<bool> shouldCallbackContinue)
				{
					if (database.IsOnlineActive || database.IsOnlinePassive)
					{
						if (string.IsNullOrEmpty(databaseName) && foundDatabase == null)
						{
							foundDatabase = database;
							return;
						}
						if (!string.IsNullOrEmpty(databaseName) && database.MdbName.Equals(databaseName, StringComparison.OrdinalIgnoreCase))
						{
							foundDatabase = database;
						}
					}
				});
				if (foundDatabase != null)
				{
					return foundDatabase;
				}
				if (string.IsNullOrEmpty(databaseName))
				{
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.NoAvailableDatabase());
				}
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.DatabaseNotFound(databaseName));
			}
		}
	}
}
