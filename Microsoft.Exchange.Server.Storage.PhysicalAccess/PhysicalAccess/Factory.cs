using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccessJet;
using Microsoft.Exchange.Server.Storage.PhysicalAccessSql;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Isam.Esent.Interop.Windows7;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000021 RID: 33
	public static class Factory
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000DA87 File Offset: 0x0000BC87
		public static int GetOptimalStreamChunkSize()
		{
			return Factory.Instance.GetOptimalStreamChunkSize();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000DA93 File Offset: 0x0000BC93
		public static void GetDatabaseThreadStats(out JET_THREADSTATS stats)
		{
			Factory.Instance.GetDatabaseThreadStats(out stats);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		public static Connection CreateConnection(IDatabaseExecutionContext outerExecutionContext, Database database, string identification)
		{
			return Factory.Instance.CreateConnection(outerExecutionContext, database, identification);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000DAAF File Offset: 0x0000BCAF
		public static void PrepareForCrashDump()
		{
			Factory.Instance.PrepareForCrashDump();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000DABB File Offset: 0x0000BCBB
		public static DatabaseType GetDatabaseType()
		{
			return Factory.Instance.GetDatabaseType();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		public static Database CreateDatabase(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions)
		{
			return Factory.Instance.CreateDatabase(dbGuid, displayName, logPath, filePath, fileName, databaseFlags, databaseOptions);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		public static Table CreateTable(string name, TableClass tableClass, CultureInfo culture, bool trackDirtyObjects, TableAccessHints tableAccessHints, bool readOnly, Visibility visibility, bool schemaExtension, SpecialColumns specialCols, Index[] indexes, PhysicalColumn[] computedColumns, PhysicalColumn[] columns)
		{
			return Factory.Instance.CreateTable(name, tableClass, culture, trackDirtyObjects, tableAccessHints, readOnly, visibility, schemaExtension, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		public static void DeleteTable(IConnectionProvider connectionProvider, string tableName)
		{
			if (ExTraceGlobals.DbInteractionSummaryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					stringBuilder.Append("cn:[");
					stringBuilder.Append(connectionProvider.GetConnection().GetHashCode());
					stringBuilder.Append("] ");
				}
				stringBuilder.Append("Delete table ");
				stringBuilder.Append(tableName);
				ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			Factory.Instance.DeleteTable(connectionProvider, tableName);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000DB97 File Offset: 0x0000BD97
		public static TableFunction CreateTableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns)
		{
			return Factory.Instance.CreateTableFunction(name, getTableContents, getColumnFromRow, visibility, parameterTypes, indexes, columns);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000DBAD File Offset: 0x0000BDAD
		public static DataRow CreateDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] initialValues)
		{
			return Factory.Instance.CreateDataRow(culture, connectionProvider, table, writeThrough, initialValues);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000DBBF File Offset: 0x0000BDBF
		public static DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] primaryKeyValues)
		{
			return Factory.OpenDataRow(culture, connectionProvider, table, writeThrough, true, primaryKeyValues);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000DBCD File Offset: 0x0000BDCD
		public static DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader)
		{
			return Factory.Instance.OpenDataRow(culture, connectionProvider, table, writeThrough, reader);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public static DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, bool load, params ColumnValue[] primaryKeyValues)
		{
			DataRow dataRow = Factory.Instance.OpenDataRow(culture, connectionProvider, table, writeThrough, primaryKeyValues);
			if (load)
			{
				dataRow = dataRow.VerifyAndLoad(connectionProvider);
			}
			return dataRow;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		public static ConstantColumn CreateConstantColumn(object value)
		{
			PropertyType propertyType = PropertyTypeHelper.PropTypeFromClrType(value.GetType());
			Type type = PropertyTypeHelper.ClrTypeFromPropType(propertyType);
			int size = PropertyTypeHelper.SizeFromPropType(propertyType);
			int maxLength = PropertyTypeHelper.MaxLengthFromPropType(propertyType);
			return Factory.CreateConstantColumn(null, type, Visibility.Public, size, maxLength, value);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000DC45 File Offset: 0x0000BE45
		public static ConstantColumn CreateConstantColumn(object value, Column column)
		{
			return Factory.CreateConstantColumn(column.Name, column.Type, column.Visibility, column.Size, column.MaxLength, value);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000DC6B File Offset: 0x0000BE6B
		public static ConstantColumn CreateConstantColumn(string name, Type type, int size, int maxLength, object value)
		{
			return Factory.Instance.CreateConstantColumn(name, type, Visibility.Public, size, maxLength, value);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000DC7E File Offset: 0x0000BE7E
		public static ConstantColumn CreateConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value)
		{
			return Factory.Instance.CreateConstantColumn(name, type, visibility, size, maxLength, value);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public static ConversionColumn CreateConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn)
		{
			return Factory.Instance.CreateConversionColumn(name, type, size, maxLength, table, conversionFunction, functionName, argumentColumn);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		public static FunctionColumn CreateFunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, params Column[] argumentColumns)
		{
			return Factory.Instance.CreateFunctionColumn(name, type, size, maxLength, table, function, functionName, argumentColumns);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000DCDB File Offset: 0x0000BEDB
		public static MappedPropertyColumn CreateMappedPropertyColumn(Column column, StorePropTag propTag)
		{
			return Factory.Instance.CreateMappedPropertyColumn(column.ActualColumn, propTag);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000DCEE File Offset: 0x0000BEEE
		public static MappedPropertyColumn CreateNestedMappedPropertyColumn(Column column, StorePropTag propTag)
		{
			return Factory.Instance.CreateMappedPropertyColumn(column, propTag);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		public static PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, int maxInlineLength)
		{
			return Factory.CreatePhysicalColumn(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, visibility, maxLength, size, null, -1, maxInlineLength);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000DD24 File Offset: 0x0000BF24
		public static PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, int maxInlineLength)
		{
			return Factory.CreatePhysicalColumn(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, schemaExtension, visibility, maxLength, size, null, -1, maxInlineLength);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public static PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength)
		{
			return Factory.Instance.CreatePhysicalColumn(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, visibility, maxLength, size, table, index, maxInlineLength);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public static PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength)
		{
			return Factory.Instance.CreatePhysicalColumn(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, schemaExtension, visibility, maxLength, size, table, index, maxInlineLength);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public static PropertyColumn CreatePropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator)
		{
			return Factory.Instance.CreatePropertyColumn(name, type, size, maxLength, table, propTag, rowPropBagCreator, null);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		public static PropertyColumn CreatePropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, Column[] dependOn)
		{
			return Factory.Instance.CreatePropertyColumn(name, type, size, maxLength, table, propTag, rowPropBagCreator, dependOn);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000DDF3 File Offset: 0x0000BFF3
		public static SizeOfColumn CreateSizeOfColumn(Column termColumn)
		{
			return Factory.CreateSizeOfColumn(null, termColumn, false);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000DDFD File Offset: 0x0000BFFD
		public static SizeOfColumn CreateSizeOfColumn(string name, Column termColumn)
		{
			return Factory.CreateSizeOfColumn(name, termColumn, false);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000DE07 File Offset: 0x0000C007
		public static SizeOfColumn CreateSizeOfColumn(string name, Column termColumn, bool compressedSize)
		{
			return Factory.Instance.CreateSizeOfColumn(name, termColumn, compressedSize);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000DE16 File Offset: 0x0000C016
		public static IEnumerable<VirtualColumnDefinition> GetSupportedVirtualColumns()
		{
			return Factory.Instance.GetSupportedVirtualColumns();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000DE22 File Offset: 0x0000C022
		public static ApplyOperator CreateApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition)
		{
			return Factory.Instance.CreateApplyOperator(connectionProvider, definition);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000DE30 File Offset: 0x0000C030
		public static ApplyOperator CreateApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation)
		{
			return Factory.Instance.CreateApplyOperator(culture, connectionProvider, tableFunction, tableFunctionParameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, outerQuery, frequentOperation);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000DE59 File Offset: 0x0000C059
		public static CategorizedTableOperator CreateCategorizedTableOperator(IConnectionProvider connectionProvider, CategorizedTableOperator.CategorizedTableOperatorDefinition definition)
		{
			return Factory.Instance.CreateCategorizedTableOperator(connectionProvider, definition);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000DE68 File Offset: 0x0000C068
		public static CategorizedTableOperator CreateCategorizedTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateCategorizedTableOperator(culture, connectionProvider, table, categorizedTableParams, collapseState, columnsToFetch, additionalHeaderRenameDictionary, additionalLeafRenameDictionary, restriction, skipTo, maxRows, keyRange, backwards, frequentOperation);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000DE97 File Offset: 0x0000C097
		public static CountOperator CreateCountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition)
		{
			return Factory.Instance.CreateCountOperator(connectionProvider, definition);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000DEA5 File Offset: 0x0000C0A5
		public static CountOperator CreateCountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation)
		{
			return Factory.Instance.CreateCountOperator(culture, connectionProvider, queryOperator, frequentOperation);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000DEB5 File Offset: 0x0000C0B5
		public static DeleteOperator CreateDeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation)
		{
			return Factory.Instance.CreateDeleteOperator(culture, connectionProvider, tableOperator, frequentOperation);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000DEC5 File Offset: 0x0000C0C5
		public static IndexAndOperator CreateIndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition)
		{
			return Factory.Instance.CreateIndexAndOperator(connectionProvider, definition);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000DED3 File Offset: 0x0000C0D3
		public static IndexAndOperator CreateIndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation)
		{
			return Factory.Instance.CreateIndexAndOperator(culture, connectionProvider, columnsToFetch, queryOperators, frequentOperation);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000DEE5 File Offset: 0x0000C0E5
		public static IndexNotOperator CreateIndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition)
		{
			return Factory.Instance.CreateIndexNotOperator(connectionProvider, definition);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000DEF3 File Offset: 0x0000C0F3
		public static IndexNotOperator CreateIndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation)
		{
			return Factory.Instance.CreateIndexNotOperator(culture, connectionProvider, columnsToFetch, queryOperator, notOperator, frequentOperation);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000DF07 File Offset: 0x0000C107
		public static IndexOrOperator CreateIndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition)
		{
			return Factory.Instance.CreateIndexOrOperator(connectionProvider, definition);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000DF15 File Offset: 0x0000C115
		public static IndexOrOperator CreateIndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation)
		{
			return Factory.Instance.CreateIndexOrOperator(culture, connectionProvider, columnsToFetch, queryOperators, frequentOperation);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000DF28 File Offset: 0x0000C128
		public static InsertOperator CreateInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Column columnToFetch, bool frequentOperation)
		{
			return Factory.Instance.CreateInsertOperator(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, valuesToInsert, null, null, columnToFetch, false, false, frequentOperation);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000DF50 File Offset: 0x0000C150
		public static InsertOperator CreateInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Column columnToFetch, bool unversioned, bool ignoreDuplicateKey, bool frequentOperation)
		{
			return Factory.Instance.CreateInsertOperator(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, valuesToInsert, null, null, columnToFetch, unversioned, ignoreDuplicateKey, frequentOperation);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public static InsertOperator CreateInsertFromSelectOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, Action<object[]> actionOnInsert, Column[] argumentColumns, bool unversioned, bool ignoreDuplicateKey, bool frequentOperation)
		{
			return Factory.Instance.CreateInsertOperator(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, null, actionOnInsert, argumentColumns, null, unversioned, ignoreDuplicateKey, frequentOperation);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		public static JoinOperator CreateJoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition)
		{
			return Factory.Instance.CreateJoinOperator(connectionProvider, definition);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public static JoinOperator CreateJoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation)
		{
			return Factory.Instance.CreateJoinOperator(culture, connectionProvider, table, columnsToFetch, null, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery, frequentOperation);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public static JoinOperator CreateJoinOperator(CultureInfo culture, Connection connection, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation)
		{
			return Factory.Instance.CreateJoinOperator(culture, connection, table, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery, frequentOperation);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000E00B File Offset: 0x0000C20B
		public static DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition)
		{
			return Factory.Instance.CreateDistinctOperator(connectionProvider, definition);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000E019 File Offset: 0x0000C219
		public static DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation)
		{
			return Factory.Instance.CreateDistinctOperator(connectionProvider, skipTo, maxRows, outerQuery, frequentOperation);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000E02B File Offset: 0x0000C22B
		public static OrdinalPositionOperator CreateOrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition)
		{
			return Factory.Instance.CreateOrdinalPositionOperator(connectionProvider, definition);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000E039 File Offset: 0x0000C239
		public static OrdinalPositionOperator CreateOrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation)
		{
			return Factory.Instance.CreateOrdinalPositionOperator(culture, connectionProvider, queryOperator, keySortOrder, key, frequentOperation);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000E04D File Offset: 0x0000C24D
		public static PreReadOperator CreatePreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, List<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation)
		{
			return Factory.Instance.CreatePreReadOperator(culture, connectionProvider, table, index, keyRanges, longValueColumns, frequentOperation);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000E063 File Offset: 0x0000C263
		public static SortOperator CreateSortOperator(IConnectionProvider connectionProvider, SortOperator.SortOperatorDefinition definition)
		{
			return Factory.Instance.CreateSortOperator(connectionProvider, definition);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000E074 File Offset: 0x0000C274
		public static SortOperator CreateSortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, KeyRange keyRange, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateSortOperator(culture, connectionProvider, queryOperator, skipTo, maxRows, sortOrder, new KeyRange[]
			{
				keyRange
			}, backwards, frequentOperation);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public static SortOperator CreateSortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateSortOperator(culture, connectionProvider, queryOperator, skipTo, maxRows, sortOrder, keyRanges, backwards, frequentOperation);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000E0D5 File Offset: 0x0000C2D5
		public static TableFunctionOperator CreateTableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition)
		{
			return Factory.Instance.CreateTableFunctionOperator(connectionProvider, definition);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public static TableFunctionOperator CreateTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateTableFunctionOperator(culture, connectionProvider, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, new KeyRange[]
			{
				keyRange
			}, backwards, frequentOperation);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000E124 File Offset: 0x0000C324
		public static TableFunctionOperator CreateTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateTableFunctionOperator(culture, connectionProvider, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000E14F File Offset: 0x0000C34F
		public static TableOperator CreateTableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition)
		{
			return Factory.Instance.CreateTableOperator(connectionProvider, definition);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000E160 File Offset: 0x0000C360
		public static TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateTableOperator(culture, connectionProvider, table, index, columnsToFetch, null, restriction, renameDictionary, skipTo, maxRows, new KeyRange[]
			{
				keyRange
			}, backwards, true, frequentOperation);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public static TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateTableOperator(culture, connectionProvider, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, new KeyRange[]
			{
				keyRange
			}, backwards, true, frequentOperation);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public static TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
		{
			return Factory.Instance.CreateTableOperator(culture, connectionProvider, table, index, columnsToFetch, null, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, true, frequentOperation);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000E218 File Offset: 0x0000C418
		public static TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool opportunedPreread, bool frequentOperation)
		{
			return Factory.Instance.CreateTableOperator(culture, connectionProvider, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, new KeyRange[]
			{
				keyRange
			}, backwards, opportunedPreread, frequentOperation);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000E25C File Offset: 0x0000C45C
		public static TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation)
		{
			return Factory.Instance.CreateTableOperator(culture, connectionProvider, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, opportunedPreread, frequentOperation);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000E28C File Offset: 0x0000C48C
		public static TableOperator CreateTableOperator(CultureInfo culture, Connection connection, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation)
		{
			return Factory.Instance.CreateTableOperator(culture, connection, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, opportunedPreread, frequentOperation);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000E2BB File Offset: 0x0000C4BB
		public static UpdateOperator CreateUpdateOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation)
		{
			return Factory.Instance.CreateUpdateOperator(culture, connectionProvider, tableOperator, columnsToUpdate, valuesToUpdate, frequentOperation);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000E2CF File Offset: 0x0000C4CF
		public static SearchCriteriaTrue CreateSearchCriteriaTrue()
		{
			return Factory.Instance.CreateSearchCriteriaTrue();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000E2DB File Offset: 0x0000C4DB
		public static SearchCriteriaFalse CreateSearchCriteriaFalse()
		{
			return Factory.Instance.CreateSearchCriteriaFalse();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000E2E7 File Offset: 0x0000C4E7
		public static SearchCriteriaAnd CreateSearchCriteriaAnd(params SearchCriteria[] nestedCriteria)
		{
			return Factory.Instance.CreateSearchCriteriaAnd(nestedCriteria);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public static SearchCriteriaOr CreateSearchCriteriaOr(params SearchCriteria[] nestedCriteria)
		{
			return Factory.Instance.CreateSearchCriteriaOr(nestedCriteria);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000E301 File Offset: 0x0000C501
		public static SearchCriteriaNot CreateSearchCriteriaNot(SearchCriteria criteria)
		{
			return Factory.Instance.CreateSearchCriteriaNot(criteria);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000E30E File Offset: 0x0000C50E
		public static SearchCriteriaNear CreateSearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria)
		{
			return Factory.Instance.CreateSearchCriteriaNear(distance, ordered, criteria);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000E31D File Offset: 0x0000C51D
		public static SearchCriteriaCompare CreateSearchCriteriaCompare(Column lhs, SearchCriteriaCompare.SearchRelOp op, Column rhs)
		{
			return Factory.Instance.CreateSearchCriteriaCompare(lhs, op, rhs);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000E32C File Offset: 0x0000C52C
		public static SearchCriteriaBitMask CreateSearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op)
		{
			return Factory.Instance.CreateSearchCriteriaBitMask(lhs, rhs, op);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000E33B File Offset: 0x0000C53B
		public static SearchCriteriaText CreateSearchCriteriaText(Column lhs, SearchCriteriaText.SearchTextFullness fullnessFlags, SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, Column rhs)
		{
			return Factory.Instance.CreateSearchCriteriaText(lhs, fullnessFlags, fuzzynessFlags, rhs);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000E34B File Offset: 0x0000C54B
		internal static void Initialize(DatabaseType databaseTypeToUse, Factory.JetHADatabaseCreator haCreator)
		{
			if (databaseTypeToUse == DatabaseType.Jet)
			{
				Factory.concreteFactory = new Factory.JetFactory(haCreator);
				return;
			}
			if (databaseTypeToUse == DatabaseType.Sql)
			{
				Factory.concreteFactory = Factory.GetSqlFactory();
				return;
			}
			throw new InvalidOperationException("Attempting to initialize the Factory class with an unknown DatabaseType.");
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000E375 File Offset: 0x0000C575
		private static Factory.IConcreteFactory GetSqlFactory()
		{
			return new Factory.SqlFactory();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000E37C File Offset: 0x0000C57C
		private static Factory.IConcreteFactory Instance
		{
			get
			{
				return Factory.concreteFactory;
			}
		}

		// Token: 0x040000B3 RID: 179
		private static Factory.IConcreteFactory concreteFactory = new Factory.JetFactory(null);

		// Token: 0x02000022 RID: 34
		internal interface IConcreteFactory
		{
			// Token: 0x06000187 RID: 391
			int GetOptimalStreamChunkSize();

			// Token: 0x06000188 RID: 392
			void GetDatabaseThreadStats(out JET_THREADSTATS stats);

			// Token: 0x06000189 RID: 393
			Connection CreateConnection(IDatabaseExecutionContext outerExecutionContext, Database database, string identification);

			// Token: 0x0600018A RID: 394
			void PrepareForCrashDump();

			// Token: 0x0600018B RID: 395
			DatabaseType GetDatabaseType();

			// Token: 0x0600018C RID: 396
			Database CreateDatabase(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions);

			// Token: 0x0600018D RID: 397
			Table CreateTable(string name, TableClass tableClass, CultureInfo culture, bool trackDirtyObjects, TableAccessHints tableAccessHints, bool readOnly, Visibility visibility, bool schemaExtension, SpecialColumns specialCols, Index[] indexes, PhysicalColumn[] computedColumns, PhysicalColumn[] columns);

			// Token: 0x0600018E RID: 398
			void DeleteTable(IConnectionProvider connectionProvider, string tableName);

			// Token: 0x0600018F RID: 399
			TableFunction CreateTableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns);

			// Token: 0x06000190 RID: 400
			DataRow CreateDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] initialValues);

			// Token: 0x06000191 RID: 401
			DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] primaryKeyValues);

			// Token: 0x06000192 RID: 402
			DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader);

			// Token: 0x06000193 RID: 403
			ConstantColumn CreateConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value);

			// Token: 0x06000194 RID: 404
			ConversionColumn CreateConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn);

			// Token: 0x06000195 RID: 405
			FunctionColumn CreateFunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, Column[] argumentColumns);

			// Token: 0x06000196 RID: 406
			MappedPropertyColumn CreateMappedPropertyColumn(Column actualColumn, StorePropTag propTag);

			// Token: 0x06000197 RID: 407
			PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength);

			// Token: 0x06000198 RID: 408
			PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool sxhemaExtension, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength);

			// Token: 0x06000199 RID: 409
			PropertyColumn CreatePropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, Column[] dependOn);

			// Token: 0x0600019A RID: 410
			SizeOfColumn CreateSizeOfColumn(string name, Column termColumn, bool compressedSize);

			// Token: 0x0600019B RID: 411
			IEnumerable<VirtualColumnDefinition> GetSupportedVirtualColumns();

			// Token: 0x0600019C RID: 412
			CountOperator CreateCountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition);

			// Token: 0x0600019D RID: 413
			CountOperator CreateCountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation);

			// Token: 0x0600019E RID: 414
			DeleteOperator CreateDeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation);

			// Token: 0x0600019F RID: 415
			InsertOperator CreateInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Action<object[]> actionOnInsert, Column[] argumentColumns, Column columnToFetch, bool unversioned, bool ignoreDuplicateKey, bool frequentOperation);

			// Token: 0x060001A0 RID: 416
			JoinOperator CreateJoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition);

			// Token: 0x060001A1 RID: 417
			JoinOperator CreateJoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation);

			// Token: 0x060001A2 RID: 418
			DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition);

			// Token: 0x060001A3 RID: 419
			DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation);

			// Token: 0x060001A4 RID: 420
			OrdinalPositionOperator CreateOrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition);

			// Token: 0x060001A5 RID: 421
			OrdinalPositionOperator CreateOrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation);

			// Token: 0x060001A6 RID: 422
			SortOperator CreateSortOperator(IConnectionProvider connectionProvider, SortOperator.SortOperatorDefinition definition);

			// Token: 0x060001A7 RID: 423
			SortOperator CreateSortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation);

			// Token: 0x060001A8 RID: 424
			TableFunctionOperator CreateTableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition);

			// Token: 0x060001A9 RID: 425
			TableFunctionOperator CreateTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation);

			// Token: 0x060001AA RID: 426
			TableOperator CreateTableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition);

			// Token: 0x060001AB RID: 427
			TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation);

			// Token: 0x060001AC RID: 428
			CategorizedTableOperator CreateCategorizedTableOperator(IConnectionProvider connectionProvider, CategorizedTableOperator.CategorizedTableOperatorDefinition definition);

			// Token: 0x060001AD RID: 429
			CategorizedTableOperator CreateCategorizedTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation);

			// Token: 0x060001AE RID: 430
			PreReadOperator CreatePreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation);

			// Token: 0x060001AF RID: 431
			UpdateOperator CreateUpdateOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation);

			// Token: 0x060001B0 RID: 432
			ApplyOperator CreateApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition);

			// Token: 0x060001B1 RID: 433
			ApplyOperator CreateApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation);

			// Token: 0x060001B2 RID: 434
			IndexAndOperator CreateIndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition);

			// Token: 0x060001B3 RID: 435
			IndexAndOperator CreateIndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation);

			// Token: 0x060001B4 RID: 436
			IndexOrOperator CreateIndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition);

			// Token: 0x060001B5 RID: 437
			IndexOrOperator CreateIndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation);

			// Token: 0x060001B6 RID: 438
			IndexNotOperator CreateIndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition);

			// Token: 0x060001B7 RID: 439
			IndexNotOperator CreateIndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation);

			// Token: 0x060001B8 RID: 440
			SearchCriteriaTrue CreateSearchCriteriaTrue();

			// Token: 0x060001B9 RID: 441
			SearchCriteriaFalse CreateSearchCriteriaFalse();

			// Token: 0x060001BA RID: 442
			SearchCriteriaAnd CreateSearchCriteriaAnd(params SearchCriteria[] nestedCriteria);

			// Token: 0x060001BB RID: 443
			SearchCriteriaOr CreateSearchCriteriaOr(params SearchCriteria[] nestedCriteria);

			// Token: 0x060001BC RID: 444
			SearchCriteriaNot CreateSearchCriteriaNot(SearchCriteria criteria);

			// Token: 0x060001BD RID: 445
			SearchCriteriaNear CreateSearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria);

			// Token: 0x060001BE RID: 446
			SearchCriteriaCompare CreateSearchCriteriaCompare(Column lhs, SearchCriteriaCompare.SearchRelOp op, Column rhs);

			// Token: 0x060001BF RID: 447
			SearchCriteriaBitMask CreateSearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op);

			// Token: 0x060001C0 RID: 448
			SearchCriteriaText CreateSearchCriteriaText(Column lhs, SearchCriteriaText.SearchTextFullness fullnessFlags, SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, Column rhs);
		}

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x060001C2 RID: 450
		public delegate Database JetHADatabaseCreator(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions);

		// Token: 0x02000024 RID: 36
		private class JetFactory : Factory.IConcreteFactory
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x0000E390 File Offset: 0x0000C590
			public JetFactory(Factory.JetHADatabaseCreator haCreator)
			{
				this.haDatabaseCreator = haCreator;
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x0000E39F File Offset: 0x0000C59F
			public int GetOptimalStreamChunkSize()
			{
				return 65200;
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
			public void PrepareForCrashDump()
			{
				Windows7Api.JetConfigureProcessForCrashDump(CrashDumpGrbit.Minimum | CrashDumpGrbit.CacheIncludeCorruptedPages);
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x0000E3AF File Offset: 0x0000C5AF
			public DatabaseType GetDatabaseType()
			{
				return DatabaseType.Jet;
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0000E3B2 File Offset: 0x0000C5B2
			public void GetDatabaseThreadStats(out JET_THREADSTATS stats)
			{
				VistaApi.JetGetThreadStats(out stats);
			}

			// Token: 0x060001CA RID: 458 RVA: 0x0000E3BA File Offset: 0x0000C5BA
			public Connection CreateConnection(IDatabaseExecutionContext outerExecutionContext, Database database, string identification)
			{
				return new JetConnection(outerExecutionContext, database as JetDatabase, identification);
			}

			// Token: 0x060001CB RID: 459 RVA: 0x0000E3C9 File Offset: 0x0000C5C9
			public Database CreateDatabase(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions)
			{
				if (this.haDatabaseCreator != null)
				{
					return this.haDatabaseCreator(dbGuid, displayName, logPath, filePath, fileName, databaseFlags, databaseOptions);
				}
				return new JetDatabase(dbGuid, displayName, logPath, filePath, fileName, databaseFlags, databaseOptions);
			}

			// Token: 0x060001CC RID: 460 RVA: 0x0000E3FC File Offset: 0x0000C5FC
			public Table CreateTable(string name, TableClass tableClass, CultureInfo culture, bool trackDirtyObjects, TableAccessHints tableAccessHints, bool readOnly, Visibility visibility, bool schemaExtension, SpecialColumns specialCols, Index[] indexes, PhysicalColumn[] computedColumns, PhysicalColumn[] columns)
			{
				return new JetTable(name, tableClass, culture, trackDirtyObjects, tableAccessHints, readOnly, visibility, schemaExtension, specialCols, indexes, computedColumns, columns);
			}

			// Token: 0x060001CD RID: 461 RVA: 0x0000E423 File Offset: 0x0000C623
			public void DeleteTable(IConnectionProvider connectionProvider, string tableName)
			{
				JetTable.DeleteJetTable(connectionProvider, tableName);
			}

			// Token: 0x060001CE RID: 462 RVA: 0x0000E42C File Offset: 0x0000C62C
			public TableFunction CreateTableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns)
			{
				return new JetTableFunction(name, getTableContents, getColumnFromRow, visibility, parameterTypes, indexes, columns);
			}

			// Token: 0x060001CF RID: 463 RVA: 0x0000E43E File Offset: 0x0000C63E
			public DataRow CreateDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] initialValues)
			{
				return new JetDataRow(DataRow.Create, culture, connectionProvider, table, writeThrough, initialValues);
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x0000E451 File Offset: 0x0000C651
			public DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] primaryKeyValues)
			{
				return new JetDataRow(DataRow.Open, culture, connectionProvider, table, writeThrough, primaryKeyValues);
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x0000E464 File Offset: 0x0000C664
			public DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader)
			{
				return new JetDataRow(DataRow.Open, culture, connectionProvider, table, writeThrough, reader);
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x0000E477 File Offset: 0x0000C677
			public ConstantColumn CreateConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value)
			{
				return new JetConstantColumn(name, type, visibility, size, maxLength, value);
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x0000E487 File Offset: 0x0000C687
			public ConversionColumn CreateConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn)
			{
				return new JetConversionColumn(name, type, size, maxLength, table, conversionFunction, functionName, argumentColumn);
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x0000E49B File Offset: 0x0000C69B
			public FunctionColumn CreateFunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, Column[] argumentColumns)
			{
				return new JetFunctionColumn(name, type, size, maxLength, table, function, functionName, argumentColumns);
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x0000E4AF File Offset: 0x0000C6AF
			public MappedPropertyColumn CreateMappedPropertyColumn(Column actualColumn, StorePropTag propTag)
			{
				return new JetMappedPropertyColumn(actualColumn, propTag);
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
			public PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength)
			{
				return new JetPhysicalColumn(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, visibility, maxLength, size, table, index, maxInlineLength);
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
			public PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength)
			{
				return new JetPhysicalColumn(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, schemaExtension, visibility, maxLength, size, table, index, maxInlineLength);
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x0000E50F File Offset: 0x0000C70F
			public PropertyColumn CreatePropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, Column[] dependOn)
			{
				return new JetPropertyColumn(name, type, size, maxLength, table, propTag, rowPropBagCreator, dependOn);
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x0000E523 File Offset: 0x0000C723
			public SizeOfColumn CreateSizeOfColumn(string name, Column termColumn, bool compressedSize)
			{
				return new JetSizeOfColumn(name, termColumn, compressedSize);
			}

			// Token: 0x060001DA RID: 474 RVA: 0x0000E52D File Offset: 0x0000C72D
			public IEnumerable<VirtualColumnDefinition> GetSupportedVirtualColumns()
			{
				return JetTable.SupportedVirtualColumns.Values;
			}

			// Token: 0x060001DB RID: 475 RVA: 0x0000E539 File Offset: 0x0000C739
			public CountOperator CreateCountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition)
			{
				return new JetCountOperator(connectionProvider, definition);
			}

			// Token: 0x060001DC RID: 476 RVA: 0x0000E542 File Offset: 0x0000C742
			public CountOperator CreateCountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation)
			{
				return new JetCountOperator(culture, connectionProvider, queryOperator, frequentOperation);
			}

			// Token: 0x060001DD RID: 477 RVA: 0x0000E54E File Offset: 0x0000C74E
			public DeleteOperator CreateDeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation)
			{
				return new JetDeleteOperator(culture, connectionProvider, tableOperator, frequentOperation);
			}

			// Token: 0x060001DE RID: 478 RVA: 0x0000E55C File Offset: 0x0000C75C
			public InsertOperator CreateInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Action<object[]> actionOnInsert, Column[] argumentColumns, Column columnToFetch, bool unversioned, bool ignoreDuplicateKey, bool frequentOperation)
			{
				return new JetInsertOperator(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, valuesToInsert, actionOnInsert, argumentColumns, columnToFetch, unversioned, ignoreDuplicateKey, frequentOperation);
			}

			// Token: 0x060001DF RID: 479 RVA: 0x0000E583 File Offset: 0x0000C783
			public JoinOperator CreateJoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition)
			{
				return new JetJoinOperator(connectionProvider, definition);
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x0000E58C File Offset: 0x0000C78C
			public JoinOperator CreateJoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation)
			{
				return new JetJoinOperator(culture, connectionProvider, table, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery, frequentOperation);
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x0000E5B3 File Offset: 0x0000C7B3
			public DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition)
			{
				return new JetDistinctOperator(connectionProvider, definition);
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x0000E5BC File Offset: 0x0000C7BC
			public DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation)
			{
				return new JetDistinctOperator(connectionProvider, skipTo, maxRows, outerQuery, frequentOperation);
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x0000E5CA File Offset: 0x0000C7CA
			public OrdinalPositionOperator CreateOrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition)
			{
				return new JetOrdinalPositionOperator(connectionProvider, definition);
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x0000E5D3 File Offset: 0x0000C7D3
			public OrdinalPositionOperator CreateOrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation)
			{
				return new JetOrdinalPositionOperator(culture, connectionProvider, queryOperator, keySortOrder, key, frequentOperation);
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x0000E5E3 File Offset: 0x0000C7E3
			public SortOperator CreateSortOperator(IConnectionProvider connectionProvider, SortOperator.SortOperatorDefinition definition)
			{
				return new JetSortOperator(connectionProvider, definition);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x0000E5EC File Offset: 0x0000C7EC
			public SortOperator CreateSortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
			{
				return new JetSortOperator(culture, connectionProvider, queryOperator, skipTo, maxRows, sortOrder, keyRanges, backwards, frequentOperation);
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x0000E60D File Offset: 0x0000C80D
			public TableFunctionOperator CreateTableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition)
			{
				return new JetTableFunctionOperator(connectionProvider, definition);
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x0000E618 File Offset: 0x0000C818
			public TableFunctionOperator CreateTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
			{
				return new JetTableFunctionOperator(culture, connectionProvider, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation);
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x0000E63F File Offset: 0x0000C83F
			public TableOperator CreateTableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition)
			{
				return new JetTableOperator(connectionProvider, definition);
			}

			// Token: 0x060001EA RID: 490 RVA: 0x0000E648 File Offset: 0x0000C848
			public TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation)
			{
				return new JetTableOperator(culture, connectionProvider, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, opportunedPreread, frequentOperation);
			}

			// Token: 0x060001EB RID: 491 RVA: 0x0000E673 File Offset: 0x0000C873
			public CategorizedTableOperator CreateCategorizedTableOperator(IConnectionProvider connectionProvider, CategorizedTableOperator.CategorizedTableOperatorDefinition definition)
			{
				return new JetCategorizedTableOperator(connectionProvider, definition);
			}

			// Token: 0x060001EC RID: 492 RVA: 0x0000E67C File Offset: 0x0000C87C
			public CategorizedTableOperator CreateCategorizedTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation)
			{
				return new JetCategorizedTableOperator(culture, connectionProvider, table, categorizedTableParams, collapseState, columnsToFetch, additionalHeaderRenameDictionary, additionalLeafRenameDictionary, restriction, skipTo, maxRows, keyRange, backwards, frequentOperation);
			}

			// Token: 0x060001ED RID: 493 RVA: 0x0000E6A7 File Offset: 0x0000C8A7
			public PreReadOperator CreatePreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation)
			{
				return new JetPreReadOperator(culture, connectionProvider, table, index, keyRanges, longValueColumns, frequentOperation);
			}

			// Token: 0x060001EE RID: 494 RVA: 0x0000E6B9 File Offset: 0x0000C8B9
			public UpdateOperator CreateUpdateOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation)
			{
				return new JetUpdateOperator(culture, connectionProvider, tableOperator, columnsToUpdate, valuesToUpdate, frequentOperation);
			}

			// Token: 0x060001EF RID: 495 RVA: 0x0000E6C9 File Offset: 0x0000C8C9
			public ApplyOperator CreateApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition)
			{
				return new JetApplyOperator(connectionProvider, definition);
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
			public ApplyOperator CreateApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation)
			{
				return new JetApplyOperator(culture, connectionProvider, tableFunction, tableFunctionParameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, outerQuery, frequentOperation);
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x0000E6F9 File Offset: 0x0000C8F9
			public IndexAndOperator CreateIndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition)
			{
				return new JetIndexAndOperator(connectionProvider, definition);
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x0000E702 File Offset: 0x0000C902
			public IndexAndOperator CreateIndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation)
			{
				return new JetIndexAndOperator(culture, connectionProvider, columnsToFetch, queryOperators, frequentOperation);
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x0000E710 File Offset: 0x0000C910
			public IndexOrOperator CreateIndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition)
			{
				return new JetIndexOrOperator(connectionProvider, definition);
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x0000E719 File Offset: 0x0000C919
			public IndexOrOperator CreateIndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation)
			{
				return new JetIndexOrOperator(culture, connectionProvider, columnsToFetch, queryOperators, frequentOperation);
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x0000E727 File Offset: 0x0000C927
			public IndexNotOperator CreateIndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition)
			{
				return new JetIndexNotOperator(connectionProvider, definition);
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x0000E730 File Offset: 0x0000C930
			public IndexNotOperator CreateIndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation)
			{
				return new JetIndexNotOperator(culture, connectionProvider, columnsToFetch, queryOperator, notOperator, frequentOperation);
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x0000E740 File Offset: 0x0000C940
			public SearchCriteriaTrue CreateSearchCriteriaTrue()
			{
				return JetSearchCriteriaTrue.Instance;
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x0000E747 File Offset: 0x0000C947
			public SearchCriteriaFalse CreateSearchCriteriaFalse()
			{
				return JetSearchCriteriaFalse.Instance;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x0000E74E File Offset: 0x0000C94E
			public SearchCriteriaAnd CreateSearchCriteriaAnd(params SearchCriteria[] nestedCriteria)
			{
				return new JetSearchCriteriaAnd(nestedCriteria);
			}

			// Token: 0x060001FA RID: 506 RVA: 0x0000E756 File Offset: 0x0000C956
			public SearchCriteriaOr CreateSearchCriteriaOr(params SearchCriteria[] nestedCriteria)
			{
				return new JetSearchCriteriaOr(nestedCriteria);
			}

			// Token: 0x060001FB RID: 507 RVA: 0x0000E75E File Offset: 0x0000C95E
			public SearchCriteriaNot CreateSearchCriteriaNot(SearchCriteria criteria)
			{
				return new JetSearchCriteriaNot(criteria);
			}

			// Token: 0x060001FC RID: 508 RVA: 0x0000E766 File Offset: 0x0000C966
			public SearchCriteriaNear CreateSearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria)
			{
				return new JetSearchCriteriaNear(distance, ordered, criteria);
			}

			// Token: 0x060001FD RID: 509 RVA: 0x0000E770 File Offset: 0x0000C970
			public SearchCriteriaCompare CreateSearchCriteriaCompare(Column lhs, SearchCriteriaCompare.SearchRelOp op, Column rhs)
			{
				return new JetSearchCriteriaCompare(lhs, op, rhs);
			}

			// Token: 0x060001FE RID: 510 RVA: 0x0000E77A File Offset: 0x0000C97A
			public SearchCriteriaBitMask CreateSearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op)
			{
				return new JetSearchCriteriaBitMask(lhs, rhs, op);
			}

			// Token: 0x060001FF RID: 511 RVA: 0x0000E784 File Offset: 0x0000C984
			public SearchCriteriaText CreateSearchCriteriaText(Column lhs, SearchCriteriaText.SearchTextFullness fullnessFlags, SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, Column rhs)
			{
				return new JetSearchCriteriaText(lhs, fullnessFlags, fuzzynessFlags, rhs);
			}

			// Token: 0x040000B4 RID: 180
			private Factory.JetHADatabaseCreator haDatabaseCreator;
		}

		// Token: 0x02000025 RID: 37
		private class SqlFactory : Factory.IConcreteFactory
		{
			// Token: 0x06000200 RID: 512 RVA: 0x0000E790 File Offset: 0x0000C990
			public int GetOptimalStreamChunkSize()
			{
				return 64320;
			}

			// Token: 0x06000201 RID: 513 RVA: 0x0000E797 File Offset: 0x0000C997
			public void GetDatabaseThreadStats(out JET_THREADSTATS stats)
			{
				stats = default(JET_THREADSTATS);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
			public Connection CreateConnection(IDatabaseExecutionContext outerExecutionContext, Database database, string identification)
			{
				return new SqlConnection(outerExecutionContext, database as SqlDatabase, identification);
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000E7AF File Offset: 0x0000C9AF
			public void PrepareForCrashDump()
			{
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000E7B1 File Offset: 0x0000C9B1
			public DatabaseType GetDatabaseType()
			{
				return DatabaseType.Sql;
			}

			// Token: 0x06000205 RID: 517 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
			public Database CreateDatabase(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions)
			{
				return new SqlDatabase(displayName, logPath, filePath, fileName, databaseFlags, databaseOptions);
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
			public Table CreateTable(string name, TableClass tableClass, CultureInfo culture, bool trackDirtyObjects, TableAccessHints tableAccessHints, bool readOnly, Visibility visibility, bool schemaExtension, SpecialColumns specialCols, Index[] indexes, PhysicalColumn[] computedColumns, PhysicalColumn[] columns)
			{
				return new SqlTable(name, tableClass, culture, trackDirtyObjects, tableAccessHints, readOnly, visibility, schemaExtension, specialCols, indexes, computedColumns, columns);
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000E7EF File Offset: 0x0000C9EF
			public void DeleteTable(IConnectionProvider connectionProvider, string tableName)
			{
				SqlTable.DeleteSqlTable(connectionProvider, tableName);
			}

			// Token: 0x06000208 RID: 520 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
			public TableFunction CreateTableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns)
			{
				return new SqlTableFunction(name, getTableContents, getColumnFromRow, visibility, parameterTypes, indexes, columns);
			}

			// Token: 0x06000209 RID: 521 RVA: 0x0000E80A File Offset: 0x0000CA0A
			public DataRow CreateDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] initialValues)
			{
				return new SqlDataRow(DataRow.Create, culture, connectionProvider, table, writeThrough, initialValues);
			}

			// Token: 0x0600020A RID: 522 RVA: 0x0000E81D File Offset: 0x0000CA1D
			public DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, params ColumnValue[] primaryKeyValues)
			{
				return new SqlDataRow(DataRow.Open, culture, connectionProvider, table, writeThrough, primaryKeyValues);
			}

			// Token: 0x0600020B RID: 523 RVA: 0x0000E830 File Offset: 0x0000CA30
			public DataRow OpenDataRow(CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader)
			{
				return new SqlDataRow(DataRow.Open, culture, connectionProvider, table, writeThrough, reader);
			}

			// Token: 0x0600020C RID: 524 RVA: 0x0000E843 File Offset: 0x0000CA43
			public ConstantColumn CreateConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value)
			{
				return new SqlConstantColumn(name, type, visibility, size, maxLength, value);
			}

			// Token: 0x0600020D RID: 525 RVA: 0x0000E853 File Offset: 0x0000CA53
			public ConversionColumn CreateConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn)
			{
				return new SqlConversionColumn(name, type, size, maxLength, table, conversionFunction, functionName, argumentColumn);
			}

			// Token: 0x0600020E RID: 526 RVA: 0x0000E867 File Offset: 0x0000CA67
			public FunctionColumn CreateFunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, Column[] argumentColumns)
			{
				return new SqlFunctionColumn(name, type, size, maxLength, table, function, functionName, argumentColumns);
			}

			// Token: 0x0600020F RID: 527 RVA: 0x0000E87B File Offset: 0x0000CA7B
			public MappedPropertyColumn CreateMappedPropertyColumn(Column actualColumn, StorePropTag propTag)
			{
				return new SqlMappedPropertyColumn(actualColumn, propTag);
			}

			// Token: 0x06000210 RID: 528 RVA: 0x0000E884 File Offset: 0x0000CA84
			public PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength)
			{
				return new SqlPhysicalColumn(name, type, nullable, identity, streamSupport, notFetchedByDefault, visibility, maxLength, size, table, index, maxInlineLength);
			}

			// Token: 0x06000211 RID: 529 RVA: 0x0000E8AC File Offset: 0x0000CAAC
			public PhysicalColumn CreatePhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength)
			{
				return new SqlPhysicalColumn(name, type, nullable, identity, streamSupport, notFetchedByDefault, schemaExtension, visibility, maxLength, size, table, index, maxInlineLength);
			}

			// Token: 0x06000212 RID: 530 RVA: 0x0000E8D6 File Offset: 0x0000CAD6
			public PropertyColumn CreatePropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, Column[] dependOn)
			{
				return new SqlPropertyColumn(name, type, size, maxLength, table, propTag, dependOn);
			}

			// Token: 0x06000213 RID: 531 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
			public SizeOfColumn CreateSizeOfColumn(string name, Column termColumn, bool compressedSize)
			{
				return new SqlSizeOfColumn(name, termColumn, compressedSize);
			}

			// Token: 0x06000214 RID: 532 RVA: 0x0000E8F2 File Offset: 0x0000CAF2
			public IEnumerable<VirtualColumnDefinition> GetSupportedVirtualColumns()
			{
				return new VirtualColumnDefinition[0];
			}

			// Token: 0x06000215 RID: 533 RVA: 0x0000E8FA File Offset: 0x0000CAFA
			public CountOperator CreateCountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition)
			{
				return new SqlCountOperator(connectionProvider, definition);
			}

			// Token: 0x06000216 RID: 534 RVA: 0x0000E903 File Offset: 0x0000CB03
			public CountOperator CreateCountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation)
			{
				return new SqlCountOperator(culture, connectionProvider, queryOperator, frequentOperation);
			}

			// Token: 0x06000217 RID: 535 RVA: 0x0000E90F File Offset: 0x0000CB0F
			public DeleteOperator CreateDeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation)
			{
				return new SqlDeleteOperator(culture, connectionProvider, tableOperator, frequentOperation);
			}

			// Token: 0x06000218 RID: 536 RVA: 0x0000E91B File Offset: 0x0000CB1B
			public InsertOperator CreateInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Action<object[]> actionOnInsert, Column[] argumentColumns, Column columnToFetch, bool unversioned, bool ignoreDuplicateKey, bool frequentOperation)
			{
				return new SqlInsertOperator(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, valuesToInsert, columnToFetch, frequentOperation);
			}

			// Token: 0x06000219 RID: 537 RVA: 0x0000E92F File Offset: 0x0000CB2F
			public JoinOperator CreateJoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition)
			{
				return new SqlJoinOperator(connectionProvider, definition);
			}

			// Token: 0x0600021A RID: 538 RVA: 0x0000E938 File Offset: 0x0000CB38
			public JoinOperator CreateJoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation)
			{
				return new SqlJoinOperator(culture, connectionProvider, table, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery, frequentOperation);
			}

			// Token: 0x0600021B RID: 539 RVA: 0x0000E95F File Offset: 0x0000CB5F
			public DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition)
			{
				return new SqlDistinctOperator(connectionProvider, definition);
			}

			// Token: 0x0600021C RID: 540 RVA: 0x0000E968 File Offset: 0x0000CB68
			public DistinctOperator CreateDistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation)
			{
				return new SqlDistinctOperator(connectionProvider, skipTo, maxRows, outerQuery, frequentOperation);
			}

			// Token: 0x0600021D RID: 541 RVA: 0x0000E976 File Offset: 0x0000CB76
			public OrdinalPositionOperator CreateOrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition)
			{
				return new SqlOrdinalPositionOperator(connectionProvider, definition);
			}

			// Token: 0x0600021E RID: 542 RVA: 0x0000E97F File Offset: 0x0000CB7F
			public OrdinalPositionOperator CreateOrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation)
			{
				return new SqlOrdinalPositionOperator(culture, connectionProvider, queryOperator, keySortOrder, key, frequentOperation);
			}

			// Token: 0x0600021F RID: 543 RVA: 0x0000E98F File Offset: 0x0000CB8F
			public SortOperator CreateSortOperator(IConnectionProvider connectionProvider, SortOperator.SortOperatorDefinition definition)
			{
				return new SqlSortOperator(connectionProvider, definition);
			}

			// Token: 0x06000220 RID: 544 RVA: 0x0000E998 File Offset: 0x0000CB98
			public SortOperator CreateSortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
			{
				return new SqlSortOperator(culture, connectionProvider, queryOperator, skipTo, maxRows, sortOrder, keyRanges, backwards, frequentOperation);
			}

			// Token: 0x06000221 RID: 545 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
			public TableFunctionOperator CreateTableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition)
			{
				return new SqlTableFunctionOperator(connectionProvider, definition);
			}

			// Token: 0x06000222 RID: 546 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
			public TableFunctionOperator CreateTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation)
			{
				return new SqlTableFunctionOperator(culture, connectionProvider, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation);
			}

			// Token: 0x06000223 RID: 547 RVA: 0x0000E9EB File Offset: 0x0000CBEB
			public TableOperator CreateTableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition)
			{
				return new SqlTableOperator(connectionProvider, definition);
			}

			// Token: 0x06000224 RID: 548 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
			public TableOperator CreateTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation)
			{
				return new SqlTableOperator(culture, connectionProvider, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation);
			}

			// Token: 0x06000225 RID: 549 RVA: 0x0000EA1D File Offset: 0x0000CC1D
			public CategorizedTableOperator CreateCategorizedTableOperator(IConnectionProvider connectionProvider, CategorizedTableOperator.CategorizedTableOperatorDefinition definition)
			{
				return new SqlCategorizedTableOperator(connectionProvider, definition);
			}

			// Token: 0x06000226 RID: 550 RVA: 0x0000EA28 File Offset: 0x0000CC28
			public CategorizedTableOperator CreateCategorizedTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation)
			{
				return new SqlCategorizedTableOperator(culture, connectionProvider, table, categorizedTableParams, collapseState, columnsToFetch, additionalHeaderRenameDictionary, additionalLeafRenameDictionary, restriction, skipTo, maxRows, keyRange, backwards, frequentOperation);
			}

			// Token: 0x06000227 RID: 551 RVA: 0x0000EA53 File Offset: 0x0000CC53
			public PreReadOperator CreatePreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation)
			{
				return new SqlPreReadOperator(culture, connectionProvider, table, index, keyRanges, longValueColumns, frequentOperation);
			}

			// Token: 0x06000228 RID: 552 RVA: 0x0000EA65 File Offset: 0x0000CC65
			public UpdateOperator CreateUpdateOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation)
			{
				return new SqlUpdateOperator(culture, connectionProvider, tableOperator, columnsToUpdate, valuesToUpdate, frequentOperation);
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000EA75 File Offset: 0x0000CC75
			public ApplyOperator CreateApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition)
			{
				return new SqlApplyOperator(connectionProvider, definition);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000EA80 File Offset: 0x0000CC80
			public ApplyOperator CreateApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation)
			{
				return new SqlApplyOperator(culture, connectionProvider, tableFunction, tableFunctionParameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, outerQuery, frequentOperation);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000EAA5 File Offset: 0x0000CCA5
			public IndexAndOperator CreateIndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition)
			{
				return new SqlIndexAndOperator(connectionProvider, definition);
			}

			// Token: 0x0600022C RID: 556 RVA: 0x0000EAAE File Offset: 0x0000CCAE
			public IndexAndOperator CreateIndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation)
			{
				return new SqlIndexAndOperator(culture, connectionProvider, columnsToFetch, queryOperators, frequentOperation);
			}

			// Token: 0x0600022D RID: 557 RVA: 0x0000EABC File Offset: 0x0000CCBC
			public IndexOrOperator CreateIndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition)
			{
				return new SqlIndexOrOperator(connectionProvider, definition);
			}

			// Token: 0x0600022E RID: 558 RVA: 0x0000EAC5 File Offset: 0x0000CCC5
			public IndexOrOperator CreateIndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation)
			{
				return new SqlIndexOrOperator(culture, connectionProvider, columnsToFetch, queryOperators, frequentOperation);
			}

			// Token: 0x0600022F RID: 559 RVA: 0x0000EAD3 File Offset: 0x0000CCD3
			public IndexNotOperator CreateIndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition)
			{
				return new SqlIndexNotOperator(connectionProvider, definition);
			}

			// Token: 0x06000230 RID: 560 RVA: 0x0000EADC File Offset: 0x0000CCDC
			public IndexNotOperator CreateIndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation)
			{
				return new SqlIndexNotOperator(culture, connectionProvider, columnsToFetch, queryOperator, notOperator, frequentOperation);
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000EAEC File Offset: 0x0000CCEC
			public SearchCriteriaTrue CreateSearchCriteriaTrue()
			{
				return SqlSearchCriteriaTrue.Instance;
			}

			// Token: 0x06000232 RID: 562 RVA: 0x0000EAF3 File Offset: 0x0000CCF3
			public SearchCriteriaFalse CreateSearchCriteriaFalse()
			{
				return SqlSearchCriteriaFalse.Instance;
			}

			// Token: 0x06000233 RID: 563 RVA: 0x0000EAFA File Offset: 0x0000CCFA
			public SearchCriteriaAnd CreateSearchCriteriaAnd(params SearchCriteria[] nestedCriteria)
			{
				return new SqlSearchCriteriaAnd(nestedCriteria);
			}

			// Token: 0x06000234 RID: 564 RVA: 0x0000EB02 File Offset: 0x0000CD02
			public SearchCriteriaOr CreateSearchCriteriaOr(params SearchCriteria[] nestedCriteria)
			{
				return new SqlSearchCriteriaOr(nestedCriteria);
			}

			// Token: 0x06000235 RID: 565 RVA: 0x0000EB0A File Offset: 0x0000CD0A
			public SearchCriteriaNot CreateSearchCriteriaNot(SearchCriteria criteria)
			{
				return new SqlSearchCriteriaNot(criteria);
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000EB12 File Offset: 0x0000CD12
			public SearchCriteriaNear CreateSearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria)
			{
				return new SqlSearchCriteriaNear(distance, ordered, criteria);
			}

			// Token: 0x06000237 RID: 567 RVA: 0x0000EB1C File Offset: 0x0000CD1C
			public SearchCriteriaCompare CreateSearchCriteriaCompare(Column lhs, SearchCriteriaCompare.SearchRelOp op, Column rhs)
			{
				return new SqlSearchCriteriaCompare(lhs, op, rhs);
			}

			// Token: 0x06000238 RID: 568 RVA: 0x0000EB26 File Offset: 0x0000CD26
			public SearchCriteriaBitMask CreateSearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op)
			{
				return new SqlSearchCriteriaBitMask(lhs, rhs, op);
			}

			// Token: 0x06000239 RID: 569 RVA: 0x0000EB30 File Offset: 0x0000CD30
			public SearchCriteriaText CreateSearchCriteriaText(Column lhs, SearchCriteriaText.SearchTextFullness fullnessFlags, SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, Column rhs)
			{
				return new SqlSearchCriteriaText(lhs, fullnessFlags, fuzzynessFlags, rhs);
			}
		}
	}
}
