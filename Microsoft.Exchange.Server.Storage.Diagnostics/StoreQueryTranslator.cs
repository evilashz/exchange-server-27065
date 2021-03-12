using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200005C RID: 92
	internal sealed class StoreQueryTranslator
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00011FF4 File Offset: 0x000101F4
		private StoreQueryTranslator(DiagnosticQueryParser.QueryType queryType, IList<Column> fetchColumns, IList<Column> selectColumns, StoreQueryTranslator.Context fromContext, StoreQueryTranslator.SetCollection setColumns, SearchCriteria whereCriteria, SortOrder orderBy, bool isCountQuery, int maxRows, bool allowRestricted, string userIdentity, int? mailboxNumber, IList<Processor> processors, bool unlimited)
		{
			this.queryType = queryType;
			this.fetchColumns = fetchColumns;
			this.selectColumns = selectColumns;
			this.fromContext = fromContext;
			this.setColumns = setColumns;
			this.whereCriteria = whereCriteria;
			this.orderBy = orderBy;
			this.isCountQuery = isCountQuery;
			this.maxRows = maxRows;
			this.userIdentity = userIdentity;
			this.mailboxNumber = mailboxNumber;
			this.allowRestricted = allowRestricted;
			this.processors = processors;
			this.unlimited = unlimited;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00012074 File Offset: 0x00010274
		public DiagnosticQueryParser.QueryType QueryType
		{
			get
			{
				return this.queryType;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0001207C File Offset: 0x0001027C
		public IList<Column> Fetch
		{
			get
			{
				return this.fetchColumns;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00012084 File Offset: 0x00010284
		public IList<Column> Select
		{
			get
			{
				return this.selectColumns;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0001208C File Offset: 0x0001028C
		public StoreQueryTranslator.Context From
		{
			get
			{
				return this.fromContext;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00012094 File Offset: 0x00010294
		public StoreQueryTranslator.SetCollection Set
		{
			get
			{
				return this.setColumns;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0001209C File Offset: 0x0001029C
		public SearchCriteria Where
		{
			get
			{
				return this.whereCriteria;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000120A4 File Offset: 0x000102A4
		public SortOrder OrderBy
		{
			get
			{
				return this.orderBy;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000120AC File Offset: 0x000102AC
		public bool IsCountQuery
		{
			get
			{
				return this.isCountQuery;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000120B4 File Offset: 0x000102B4
		public int MaxRows
		{
			get
			{
				return this.maxRows;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000120BC File Offset: 0x000102BC
		public string UserIdentity
		{
			get
			{
				return this.userIdentity;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000120C4 File Offset: 0x000102C4
		public int? MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000120CC File Offset: 0x000102CC
		public bool AllowRestricted
		{
			get
			{
				return this.allowRestricted;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000120D4 File Offset: 0x000102D4
		public IList<Processor> Processors
		{
			get
			{
				return this.processors;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000120DC File Offset: 0x000102DC
		public bool Unlimited
		{
			get
			{
				return this.unlimited;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000120E4 File Offset: 0x000102E4
		private static StoreQueryTranslator.IStoreTableFactory TableFactory
		{
			get
			{
				return StoreQueryTranslator.hookableTableFactory.Value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000120F0 File Offset: 0x000102F0
		private static Regex PropertyTagPattern
		{
			get
			{
				return StoreQueryTranslator.propTagPattern;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000120F8 File Offset: 0x000102F8
		public static StoreQueryTranslator Create(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, DiagnosticQueryParser parser, DiagnosableParameters parameters)
		{
			if (string.IsNullOrEmpty(parser.From.Database) || context.Database.MdbName.Equals(parser.From.Database, StringComparison.OrdinalIgnoreCase))
			{
				return StoreQueryTranslator.Create(context, parser.Type, parser.Select, parser.From.Table, parser.Set, parser.Where, parser.OrderBy, parser.IsCountQuery, parser.MaxRows, parameters.AllowRestrictedData, parameters.UserIdentity, parameters.Unlimited);
			}
			throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.DatabaseMismatch());
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00012190 File Offset: 0x00010390
		internal static bool IsJetOnlyTable(string tableName)
		{
			return string.Equals(tableName, "MSysObjects", StringComparison.OrdinalIgnoreCase) || string.Equals(tableName, "MSysObjids", StringComparison.OrdinalIgnoreCase) || string.Equals(tableName, "SpaceUsage", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000121BF File Offset: 0x000103BF
		internal static IDisposable SetTestHook(StoreQueryTranslator.IStoreTableFactory factory)
		{
			return StoreQueryTranslator.hookableTableFactory.SetTestHook(factory);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000121CC File Offset: 0x000103CC
		private static StoreQueryTranslator Create(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, DiagnosticQueryParser.QueryType queryType, IList<DiagnosticQueryParser.Column> selectList, DiagnosticQueryParser.TableInfo fromTable, IDictionary<string, string> setList, DiagnosticQueryCriteria whereCondition, IList<DiagnosticQueryParser.SortColumn> orderByList, bool isCountQuery, int maxRows, bool allowRestricted, string userIdentity, bool unlimited)
		{
			StoreQueryTranslator.Context context2 = StoreQueryTranslator.TableFactory.GetContext(context, fromTable.Name, fromTable.Parameters);
			return StoreQueryTranslator.InternalCreate(queryType, selectList, context2, setList, whereCondition, orderByList, isCountQuery, maxRows, allowRestricted, userIdentity, unlimited);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001220C File Offset: 0x0001040C
		private static StoreQueryTranslator InternalCreate(DiagnosticQueryParser.QueryType queryType, IList<DiagnosticQueryParser.Column> selectList, StoreQueryTranslator.Context context, IDictionary<string, string> setList, DiagnosticQueryCriteria whereCondition, IList<DiagnosticQueryParser.SortColumn> orderByList, bool isCountQuery, int maxRows, bool allowRestricted, string userIdentity, bool unlimited)
		{
			if (context.Table.Visibility == Visibility.Private)
			{
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.PrivateTable(context.Table.Name));
			}
			if (context.Table.Visibility != Visibility.Public && (context.Table.Visibility != Visibility.Redacted || !allowRestricted))
			{
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.RedactedTable(context.Table.Name));
			}
			SearchCriteria searchCriteria = null;
			SortOrder empty = SortOrder.Empty;
			StoreQueryTranslator.SetCollection setCollection = null;
			int? num = null;
			IList<Column> list;
			IList<Column> list2;
			IList<Processor> list3;
			StoreQueryTranslator.GetColumns(queryType, context, selectList, isCountQuery, allowRestricted, out list, out list2, out list3);
			if (whereCondition != null)
			{
				searchCriteria = StoreQueryTranslator.GetCriteria(context, whereCondition, ref num);
			}
			if (orderByList != null)
			{
				empty = StoreQueryTranslator.GetOrderBy(context, orderByList, isCountQuery);
			}
			if (setList != null && setList.Keys.Count > 0)
			{
				setCollection = StoreQueryTranslator.GetSetColumns(context, setList);
				if (queryType == DiagnosticQueryParser.QueryType.Insert)
				{
					StoreQueryTranslator.CheckRequiredColumns(context, setCollection);
				}
			}
			if (context.Table is TableFunction && queryType != DiagnosticQueryParser.QueryType.Select)
			{
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.InvalidQueryContext());
			}
			return new StoreQueryTranslator(queryType, list, list2, context, setCollection, searchCriteria, empty, isCountQuery, maxRows, allowRestricted, userIdentity, num, list3, unlimited);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00012314 File Offset: 0x00010514
		private static void GetColumns(DiagnosticQueryParser.QueryType queryType, StoreQueryTranslator.Context context, IList<DiagnosticQueryParser.Column> selectList, bool isCountQuery, bool allowRestricted, out IList<Column> fetchColumns, out IList<Column> selectColumns, out IList<Processor> allProcessors)
		{
			if (queryType == DiagnosticQueryParser.QueryType.Select && selectList != null && selectList.Count > 0 && !isCountQuery)
			{
				IList<Column> list = new List<Column>(10);
				IList<Column> list2 = new List<Column>(10);
				fetchColumns = new List<Column>(selectList.Count);
				selectColumns = new List<Column>(selectList.Count);
				allProcessors = new List<Processor>(5);
				foreach (DiagnosticQueryParser.Column column in selectList)
				{
					DiagnosticQueryParser.Processor processor = column as DiagnosticQueryParser.Processor;
					if (processor != null)
					{
						IList<Column> list3 = new List<Column>(5);
						IList<Column> list4 = new List<Column>(processor.Arguments.Count);
						foreach (DiagnosticQueryParser.Column column2 in processor.Arguments)
						{
							if (column2.Identifier.Equals(DiagnosticQueryParser.AllColumns))
							{
								ComponentVersion currentSchemaVersionForDiagnostics = context.Database.CurrentSchemaVersionForDiagnostics;
								using (IEnumerator<PhysicalColumn> enumerator3 = context.Table.Columns.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										Column column3 = enumerator3.Current;
										if (currentSchemaVersionForDiagnostics.IsSupported(column3.MinVersion, column3.MaxVersion))
										{
											list4.Add(column3);
										}
									}
									continue;
								}
							}
							Column item = StoreQueryTranslator.FindColumnByName(context, column2.Identifier);
							if (column2.IsSubtraction)
							{
								list3.Add(item);
							}
							else
							{
								list4.Add(item);
							}
						}
						foreach (Column item2 in list3)
						{
							while (list4.Contains(item2))
							{
								list4.Remove(item2);
							}
						}
						Func<IList<Column>, Processor> func;
						if (!ProcessorCollection.TryGetProcessorFactory(column.Identifier, out func))
						{
							throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.ProcessorNotFound(column.Identifier));
						}
						Processor processor2 = func(list4);
						foreach (Processor.ColumnDefinition columnDefinition in processor2.GetGeneratedColumns())
						{
							PropertyType inType = PropertyTypeHelper.PropTypeFromClrType(columnDefinition.Type);
							Column item3 = Factory.CreateConstantColumn(columnDefinition.Name, columnDefinition.Type, columnDefinition.Visibility, PropertyTypeHelper.SizeFromPropType(inType), PropertyTypeHelper.MaxLengthFromPropType(inType), processor2);
							selectColumns.Add(item3);
						}
						foreach (Column item4 in processor2.Arguments.Values)
						{
							list2.Add(item4);
						}
						allProcessors.Add(processor2);
					}
					else
					{
						if (column.Identifier.Equals(DiagnosticQueryParser.AllColumns))
						{
							ComponentVersion currentSchemaVersionForDiagnostics2 = context.Database.CurrentSchemaVersionForDiagnostics;
							using (IEnumerator<PhysicalColumn> enumerator7 = context.Table.Columns.GetEnumerator())
							{
								while (enumerator7.MoveNext())
								{
									Column column4 = enumerator7.Current;
									if (currentSchemaVersionForDiagnostics2.IsSupported(column4.MinVersion, column4.MaxVersion))
									{
										fetchColumns.Add(column4);
										selectColumns.Add(column4);
									}
								}
								continue;
							}
						}
						Column item5 = StoreQueryTranslator.FindColumnByName(context, column.Identifier);
						if (column.IsSubtraction)
						{
							list.Add(item5);
						}
						else
						{
							fetchColumns.Add(item5);
							selectColumns.Add(item5);
						}
					}
				}
				foreach (Column item6 in list)
				{
					while (fetchColumns.Contains(item6))
					{
						fetchColumns.Remove(item6);
					}
					while (selectColumns.Contains(item6))
					{
						selectColumns.Remove(item6);
					}
				}
				foreach (Column item7 in list2)
				{
					fetchColumns.Add(item7);
				}
				StoreQueryTranslator.FixSelectColumns(context, selectColumns);
				if (selectColumns.Count < 1)
				{
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.EmptySelectList());
				}
			}
			else
			{
				fetchColumns = new Column[]
				{
					StoreQueryTranslator.CreateCountColumn(context.Table)
				};
				selectColumns = new Column[]
				{
					StoreQueryTranslator.CreateCountColumn(context.Table)
				};
				allProcessors = Array<Processor>.Empty;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012850 File Offset: 0x00010A50
		private static void FixSelectColumns(StoreQueryTranslator.Context context, IList<Column> columns)
		{
			if (string.Equals(context.Table.Name, "MSysObjects", StringComparison.OrdinalIgnoreCase))
			{
				for (int i = 0; i < columns.Count; i++)
				{
					if (string.Equals(columns[i].Name, "Name", StringComparison.OrdinalIgnoreCase) || string.Equals(columns[i].Name, "TemplateTable", StringComparison.OrdinalIgnoreCase))
					{
						MSysObjectsTable msysObjectsTable = DatabaseSchema.MSysObjectsTable(context.Database);
						columns[i] = Factory.CreateConversionColumn(columns[i].Name, typeof(string), 0, 128, msysObjectsTable.Table, new Func<object, object>(StoreQueryTranslator.ConvertBinaryToAsciiString), "ConvertBinaryToAsciiString", columns[i]);
					}
				}
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00012914 File Offset: 0x00010B14
		private static object ConvertBinaryToAsciiString(object columnValue)
		{
			byte[] bytes = (byte[])columnValue;
			return Encoding.ASCII.GetString(bytes);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012938 File Offset: 0x00010B38
		private static Column CreateCountColumn(Table table)
		{
			Column column;
			using (LockManager.Lock(StoreQueryTranslator.countColumns))
			{
				if (!StoreQueryTranslator.countColumns.TryGetValue(table.Name, out column))
				{
					column = Factory.CreatePhysicalColumn(table.Name, table.Name, typeof(int), false, false, false, false, Visibility.Public, 0, 4, 4);
					StoreQueryTranslator.countColumns.Add(table.Name, column);
				}
			}
			return column;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000129BC File Offset: 0x00010BBC
		private static SearchCriteria GetCriteria(StoreQueryTranslator.Context context, DiagnosticQueryCriteria whereCondition, ref int? mailboxNumber)
		{
			DiagnosticQueryCriteriaCompare diagnosticQueryCriteriaCompare = whereCondition as DiagnosticQueryCriteriaCompare;
			DiagnosticQueryCriteriaAnd diagnosticQueryCriteriaAnd = whereCondition as DiagnosticQueryCriteriaAnd;
			DiagnosticQueryCriteriaOr diagnosticQueryCriteriaOr = whereCondition as DiagnosticQueryCriteriaOr;
			DiagnosticQueryCriteriaNot diagnosticQueryCriteriaNot = whereCondition as DiagnosticQueryCriteriaNot;
			if (diagnosticQueryCriteriaCompare != null)
			{
				Column column = StoreQueryTranslator.FindColumnByName(context, diagnosticQueryCriteriaCompare.ColumnName);
				if (diagnosticQueryCriteriaCompare.Value != null)
				{
					SearchCriteria comparison = StoreQueryTranslator.GetComparison(column, diagnosticQueryCriteriaCompare);
					StoreQueryTranslator.CheckMailboxNumberCriterion(context, comparison, ref mailboxNumber);
					return comparison;
				}
				return StoreQueryTranslator.GetIsNull(column, diagnosticQueryCriteriaCompare.QueryOperator);
			}
			else
			{
				if (diagnosticQueryCriteriaAnd != null)
				{
					SearchCriteria[] criteria = StoreQueryTranslator.GetCriteria(context, diagnosticQueryCriteriaAnd.NestedCriteria, ref mailboxNumber);
					return Factory.CreateSearchCriteriaAnd(criteria);
				}
				if (diagnosticQueryCriteriaOr != null)
				{
					SearchCriteria[] criteria2 = StoreQueryTranslator.GetCriteria(context, diagnosticQueryCriteriaOr.NestedCriteria, ref mailboxNumber);
					return Factory.CreateSearchCriteriaOr(criteria2);
				}
				if (diagnosticQueryCriteriaNot != null)
				{
					SearchCriteria criteria3 = StoreQueryTranslator.GetCriteria(context, diagnosticQueryCriteriaNot.NestedCriterion, ref mailboxNumber);
					return Factory.CreateSearchCriteriaNot(criteria3);
				}
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.UnknownCriterionType());
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00012A7C File Offset: 0x00010C7C
		private static SearchCriteria[] GetCriteria(StoreQueryTranslator.Context context, IList<DiagnosticQueryCriteria> queryCriteria, ref int? mailboxNumber)
		{
			List<SearchCriteria> list = new List<SearchCriteria>(queryCriteria.Count);
			foreach (DiagnosticQueryCriteria whereCondition in queryCriteria)
			{
				list.Add(StoreQueryTranslator.GetCriteria(context, whereCondition, ref mailboxNumber));
			}
			return list.ToArray();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00012AE0 File Offset: 0x00010CE0
		private static SearchCriteria GetComparison(Column column, DiagnosticQueryCriteriaCompare comparison)
		{
			if (column.Type.Equals(typeof(string)))
			{
				return StoreQueryTranslator.GetTextComparison(column, comparison.QueryOperator, comparison.Value);
			}
			if (string.Equals(column.Table.Name, "MSysObjects", StringComparison.OrdinalIgnoreCase) && (string.Equals(column.Name, "Name", StringComparison.OrdinalIgnoreCase) || string.Equals(column.Name, "TemplateTable", StringComparison.OrdinalIgnoreCase)))
			{
				return StoreQueryTranslator.GetAsciiStringComparison(column, comparison.QueryOperator, comparison.Value);
			}
			object typedValue = StoreQueryTranslator.GetTypedValue(column.Type, comparison.Value);
			if (column.Size != 0 && column.ExtendedTypeCode == ExtendedTypeCode.Binary && typedValue != null && ((byte[])typedValue).Length != column.Size)
			{
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.InvalidFixedColumnValue(column.Name, column.Size, ((byte[])typedValue).Length));
			}
			return Factory.CreateSearchCriteriaCompare(column, StoreQueryTranslator.GetOperator(comparison.QueryOperator), Factory.CreateConstantColumn(typedValue));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012BD8 File Offset: 0x00010DD8
		private static SearchCriteria GetTextComparison(Column column, DiagnosticQueryOperator op, string value)
		{
			if (op.Equals(DiagnosticQueryOperator.Like))
			{
				SearchCriteriaText.SearchTextFullness searchFullness = StoreQueryTranslator.GetSearchFullness(ref value);
				return Factory.CreateSearchCriteriaText(column, searchFullness, SearchCriteriaText.SearchTextFuzzyLevel.IgnoreCase, Factory.CreateConstantColumn(value));
			}
			return Factory.CreateSearchCriteriaCompare(column, StoreQueryTranslator.GetOperator(op), Factory.CreateConstantColumn(value));
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012C24 File Offset: 0x00010E24
		private static SearchCriteria GetAsciiStringComparison(Column column, DiagnosticQueryOperator op, string value)
		{
			if (op == DiagnosticQueryOperator.Like)
			{
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.LikeOperatorNotAllowed(column.Table.Name, column.Name));
			}
			byte[] bytes = Encoding.ASCII.GetBytes(value);
			return Factory.CreateSearchCriteriaCompare(column, StoreQueryTranslator.GetOperator(op), Factory.CreateConstantColumn(bytes));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00012C70 File Offset: 0x00010E70
		private static void CheckMailboxNumberCriterion(StoreQueryTranslator.Context context, SearchCriteria criterion, ref int? mailboxNumber)
		{
			SearchCriteriaCompare searchCriteriaCompare = criterion as SearchCriteriaCompare;
			if (searchCriteriaCompare != null && context.Table.IsPartitioned && context.Table.NumberOfPartitioningColumns > 0 && context.Table.Columns.Count > 0 && !context.Table.Columns[0].IsIdentity && !context.Table.Columns[0].IsNullable && searchCriteriaCompare.Lhs.Name.Equals(context.Table.Columns[0].Name) && searchCriteriaCompare.RelOp.Equals(SearchCriteriaCompare.SearchRelOp.Equal))
			{
				ConstantColumn constantColumn = searchCriteriaCompare.Rhs as ConstantColumn;
				if (constantColumn != null && constantColumn.Value is int)
				{
					mailboxNumber = new int?((int)constantColumn.Value);
				}
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00012D6C File Offset: 0x00010F6C
		private static SearchCriteria GetIsNull(Column column, DiagnosticQueryOperator op)
		{
			switch (op)
			{
			case DiagnosticQueryOperator.Equal:
				return Factory.CreateSearchCriteriaCompare(column, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(null, column));
			case DiagnosticQueryOperator.NotEqual:
				return Factory.CreateSearchCriteriaCompare(column, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(null, column));
			default:
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.UnsupportedQueryOperator(op));
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00012DB4 File Offset: 0x00010FB4
		private static SearchCriteriaCompare.SearchRelOp GetOperator(DiagnosticQueryOperator op)
		{
			switch (op)
			{
			case DiagnosticQueryOperator.Equal:
				return SearchCriteriaCompare.SearchRelOp.Equal;
			case DiagnosticQueryOperator.NotEqual:
				return SearchCriteriaCompare.SearchRelOp.NotEqual;
			case DiagnosticQueryOperator.GreaterThan:
				return SearchCriteriaCompare.SearchRelOp.GreaterThan;
			case DiagnosticQueryOperator.GreaterThanOrEqual:
				return SearchCriteriaCompare.SearchRelOp.GreaterThanEqual;
			case DiagnosticQueryOperator.LessThan:
				return SearchCriteriaCompare.SearchRelOp.LessThan;
			case DiagnosticQueryOperator.LessThanOrEqual:
				return SearchCriteriaCompare.SearchRelOp.LessThanEqual;
			default:
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.UnknownQueryOperator(op));
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00012DFC File Offset: 0x00010FFC
		private static SearchCriteriaText.SearchTextFullness GetSearchFullness(ref string value)
		{
			if (!value.EndsWith("%"))
			{
				return SearchCriteriaText.SearchTextFullness.FullString;
			}
			if (value.StartsWith("%"))
			{
				value = value.Substring(1, value.Length - 2);
				return SearchCriteriaText.SearchTextFullness.SubString;
			}
			value = value.Substring(0, value.Length - 1);
			return SearchCriteriaText.SearchTextFullness.Prefix;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00012E50 File Offset: 0x00011050
		private static object GetTypedValue(Type type, string value)
		{
			if (value == null)
			{
				return null;
			}
			try
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Object:
					if (type.Equals(typeof(byte[])))
					{
						return StoreQueryTranslator.ParseByteArray(value);
					}
					if (type.Equals(typeof(byte[][])))
					{
						byte[] buffer = StoreQueryTranslator.ParseByteArray(value);
						int num = 0;
						return SerializedValue.ParseMVBinary(buffer, ref num);
					}
					if (type.Equals(typeof(Guid)))
					{
						return new Guid(value);
					}
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.UnsupportedQueryValueType(type));
				case TypeCode.Boolean:
					return bool.Parse(value);
				case TypeCode.Char:
					return char.Parse(value);
				case TypeCode.SByte:
					return sbyte.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.Byte:
					return byte.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.Int16:
					return short.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.Int32:
					return int.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.Int64:
					return long.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.Single:
					return float.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.Double:
					return double.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.DateTime:
					return DateTime.Parse(value, CultureInfo.InvariantCulture);
				case TypeCode.String:
					return value;
				}
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.UnsupportedQueryValueType(type));
			}
			catch (FormatException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.FailedToTranslateValue(value, type), ex);
			}
			object result;
			return result;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001304C File Offset: 0x0001124C
		private static Column FindColumnByName(StoreQueryTranslator.Context context, string columnName)
		{
			Column column;
			if (StoreQueryTranslator.TryGetPhysicalColumn(context, columnName, out column) || StoreQueryTranslator.TryParsePropertyTag(context, columnName, out column) || StoreQueryTranslator.TryFindPropInfo(context, columnName, out column) || StoreQueryTranslator.TryGetVirtualColumn(context, columnName, out column))
			{
				ComponentVersion currentSchemaVersionForDiagnostics = context.Database.CurrentSchemaVersionForDiagnostics;
				if (column.Type == null)
				{
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.ColumnTypeMissing(columnName));
				}
				if (column.Table == null)
				{
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.ColumnTableMissing(columnName));
				}
				if (currentSchemaVersionForDiagnostics.IsSupported(column.MinVersion, column.MaxVersion))
				{
					return column;
				}
			}
			throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.ColumnNotFound(columnName));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000130E8 File Offset: 0x000112E8
		private static SortOrder GetOrderBy(StoreQueryTranslator.Context context, IList<DiagnosticQueryParser.SortColumn> orderByList, bool isCountQuery)
		{
			if (!isCountQuery)
			{
				SortOrderBuilder sortOrderBuilder = new SortOrderBuilder();
				foreach (DiagnosticQueryParser.SortColumn sortColumn in orderByList)
				{
					Column column = StoreQueryTranslator.FindColumnByName(context, sortColumn.Name);
					if (sortOrderBuilder.Contains(column))
					{
						throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.DuplicateSortColumn(column.Name));
					}
					sortOrderBuilder.Add(column, sortColumn.Ascending);
				}
				return sortOrderBuilder.ToSortOrder();
			}
			throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.InvalidCountOrderBy());
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001317C File Offset: 0x0001137C
		private static StoreQueryTranslator.SetCollection GetSetColumns(StoreQueryTranslator.Context context, IDictionary<string, string> setList)
		{
			return StoreQueryTranslator.SetCollection.Create(context, setList);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00013188 File Offset: 0x00011388
		private static byte[] ParseByteArray(string value)
		{
			if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && value.Length > 2 && value.Length % 2 == 0)
			{
				try
				{
					return HexConverter.HexStringToByteArray(value.Substring(2));
				}
				catch (FormatException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.FailedToTranslateValue(value, typeof(byte[])));
				}
			}
			throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.FailedToTranslateValue(value, typeof(byte[])));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00013210 File Offset: 0x00011410
		private static bool TryGetPhysicalColumn(StoreQueryTranslator.Context context, string name, out Column physicalColumn)
		{
			physicalColumn = null;
			foreach (Column column in context.Table.Columns)
			{
				if (column.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					physicalColumn = column;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00013278 File Offset: 0x00011478
		private static bool TryGetVirtualColumn(StoreQueryTranslator.Context context, string name, out Column column)
		{
			Column col;
			column = (col = context.Table.VirtualColumn(name));
			return col != null;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001329C File Offset: 0x0001149C
		private static bool TryFindPropInfo(StoreQueryTranslator.Context context, string name, out Column column)
		{
			column = null;
			ObjectType objectType;
			StorePropTag propTag;
			return StoreQueryTranslator.TryGetObjectType(context.Table, out objectType) && WellKnownProperties.TryGetPropTagByTagName(name, objectType, out propTag) && StoreQueryTranslator.TryMapPropertyTagToColumn(context, propTag, out column);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000132D0 File Offset: 0x000114D0
		private static bool TryParsePropertyTag(StoreQueryTranslator.Context context, string name, out Column column)
		{
			column = null;
			StorePropTag propTag;
			return StoreQueryTranslator.TryGetStorePropTag(context, name, out propTag) && StoreQueryTranslator.TryMapPropertyTagToColumn(context, propTag, out column);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000132F8 File Offset: 0x000114F8
		private static bool TryMapPropertyTagToColumn(StoreQueryTranslator.Context context, StorePropTag propTag, out Column column)
		{
			column = null;
			ObjectType objectType;
			if (StoreQueryTranslator.TryGetObjectType(context.Table, out objectType))
			{
				try
				{
					column = PropertySchema.MapToColumn(context.Database, objectType, propTag);
					return true;
				}
				catch (InvalidOperationException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				}
				catch (ArgumentOutOfRangeException exception2)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00013368 File Offset: 0x00011568
		private static bool TryGetStorePropTag(StoreQueryTranslator.Context context, string columnName, out StorePropTag propTag)
		{
			propTag = StorePropTag.Invalid;
			ObjectType objectType;
			uint num;
			if (StoreQueryTranslator.PropertyTagPattern.IsMatch(columnName) && StoreQueryTranslator.TryGetObjectType(context.Table, out objectType) && uint.TryParse(columnName.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
			{
				ushort num2 = (ushort)((num & 4294901760U) >> 16);
				if (num2 >= 32768)
				{
					propTag = StorePropTag.CreateWithoutInfo(num, objectType);
					return true;
				}
				propTag = WellKnownProperties.GetPropTag(num, objectType);
				if (propTag.PropType != PropertyType.Invalid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000133F8 File Offset: 0x000115F8
		private static bool TryGetObjectType(Table fromTable, out ObjectType objectType)
		{
			objectType = ObjectType.Invalid;
			if (fromTable.Name.Equals("Mailbox"))
			{
				objectType = ObjectType.Mailbox;
				return true;
			}
			if (fromTable.Name.Equals("Message"))
			{
				objectType = ObjectType.Message;
				return true;
			}
			if (fromTable.Name.Equals("Folder"))
			{
				objectType = ObjectType.Folder;
				return true;
			}
			if (fromTable.Name.Equals("Attachment"))
			{
				objectType = ObjectType.Attachment;
				return true;
			}
			if (fromTable.Name.Equals("Events"))
			{
				objectType = ObjectType.Event;
				return true;
			}
			return false;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013480 File Offset: 0x00011680
		private static void CheckRequiredColumns(StoreQueryTranslator.Context context, StoreQueryTranslator.SetCollection setColumns)
		{
			foreach (Column column in context.Table.Columns)
			{
				if (!column.IsNullable && !setColumns.Columns.Contains(column))
				{
					PhysicalColumn physicalColumn = column as PhysicalColumn;
					if (physicalColumn == null || !physicalColumn.IsIdentity)
					{
						throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.ColumnRequiresValue(column.Name));
					}
				}
			}
		}

		// Token: 0x040001A1 RID: 417
		private const string StringWildcardCharacter = "%";

		// Token: 0x040001A2 RID: 418
		private static Dictionary<string, Column> countColumns = new Dictionary<string, Column>(20);

		// Token: 0x040001A3 RID: 419
		private static Hookable<StoreQueryTranslator.IStoreTableFactory> hookableTableFactory = Hookable<StoreQueryTranslator.IStoreTableFactory>.Create(true, new StoreQueryTranslator.InternalTableFactory());

		// Token: 0x040001A4 RID: 420
		private static Regex propTagPattern = new Regex("[pP][0-9A-Fa-f]{5,8}");

		// Token: 0x040001A5 RID: 421
		private readonly DiagnosticQueryParser.QueryType queryType;

		// Token: 0x040001A6 RID: 422
		private readonly IList<Column> fetchColumns;

		// Token: 0x040001A7 RID: 423
		private readonly IList<Column> selectColumns;

		// Token: 0x040001A8 RID: 424
		private readonly StoreQueryTranslator.Context fromContext;

		// Token: 0x040001A9 RID: 425
		private readonly StoreQueryTranslator.SetCollection setColumns;

		// Token: 0x040001AA RID: 426
		private readonly SearchCriteria whereCriteria;

		// Token: 0x040001AB RID: 427
		private readonly SortOrder orderBy;

		// Token: 0x040001AC RID: 428
		private readonly bool isCountQuery;

		// Token: 0x040001AD RID: 429
		private readonly int maxRows;

		// Token: 0x040001AE RID: 430
		private readonly string userIdentity;

		// Token: 0x040001AF RID: 431
		private readonly int? mailboxNumber;

		// Token: 0x040001B0 RID: 432
		private readonly bool allowRestricted;

		// Token: 0x040001B1 RID: 433
		private readonly IList<Processor> processors;

		// Token: 0x040001B2 RID: 434
		private readonly bool unlimited;

		// Token: 0x0200005D RID: 93
		internal interface IStoreTableFactory
		{
			// Token: 0x06000301 RID: 769
			StoreQueryTranslator.Context GetContext(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, string tableName, string[] tableParameters);
		}

		// Token: 0x0200005E RID: 94
		internal static class TestAccess
		{
			// Token: 0x06000302 RID: 770 RVA: 0x0001353C File Offset: 0x0001173C
			public static StoreQueryTranslator Create(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, DiagnosticQueryParser.QueryType queryType, IList<DiagnosticQueryParser.Column> selectList, DiagnosticQueryParser.Context fromContext, IDictionary<string, string> setList, DiagnosticQueryCriteria whereCondition, IList<DiagnosticQueryParser.SortColumn> orderByList, bool isCountQuery, int maxRows, bool allowRestricted, string userIdentity, bool unlimited)
			{
				return StoreQueryTranslator.Create(context, queryType, selectList, fromContext.Table, setList, whereCondition, orderByList, isCountQuery, maxRows, allowRestricted, userIdentity, unlimited);
			}

			// Token: 0x06000303 RID: 771 RVA: 0x00013568 File Offset: 0x00011768
			public static StoreQueryTranslator Create(DiagnosticQueryParser.QueryType queryType, IList<Column> fetchColumns, IList<Column> selectColumns, StoreQueryTranslator.Context fromContext, IList<Column> setColumns, IList<object> setValues, SearchCriteria whereCriteria, SortOrder orderBy, bool isCountQuery, int maxRows, bool allowRestricted, string userIdentity, IList<Processor> processors, bool unlimited)
			{
				StoreQueryTranslator.SetCollection setColumns2 = (setColumns != null) ? StoreQueryTranslator.SetCollection.TestAccess.Create(setColumns, setValues) : null;
				return new StoreQueryTranslator(queryType, fetchColumns, selectColumns, fromContext, setColumns2, whereCriteria, orderBy, isCountQuery, maxRows, allowRestricted, userIdentity, null, processors, unlimited);
			}

			// Token: 0x06000304 RID: 772 RVA: 0x000135A9 File Offset: 0x000117A9
			public static Column CreateCountColumn(Table table)
			{
				return StoreQueryTranslator.CreateCountColumn(table);
			}

			// Token: 0x06000305 RID: 773 RVA: 0x000135B1 File Offset: 0x000117B1
			public static StoreQueryTranslator.Context GetContext(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, string tableName, string[] tableParameters)
			{
				return StoreQueryTranslator.TableFactory.GetContext(context, tableName, tableParameters);
			}
		}

		// Token: 0x0200005F RID: 95
		internal class Context
		{
			// Token: 0x06000306 RID: 774 RVA: 0x000135C0 File Offset: 0x000117C0
			private Context(StoreDatabase database, Table table)
			{
				this.database = database;
				this.table = table;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x000135D6 File Offset: 0x000117D6
			private Context(StoreDatabase database, TableFunction table, object[] parameters) : this(database, table)
			{
				this.parameters = parameters;
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000308 RID: 776 RVA: 0x000135E7 File Offset: 0x000117E7
			public StoreDatabase Database
			{
				get
				{
					return this.database;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000309 RID: 777 RVA: 0x000135EF File Offset: 0x000117EF
			public Table Table
			{
				get
				{
					return this.table;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600030A RID: 778 RVA: 0x000135F7 File Offset: 0x000117F7
			public object[] Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x0600030B RID: 779 RVA: 0x000135FF File Offset: 0x000117FF
			public static StoreQueryTranslator.Context Create(StoreDatabase database, Table table)
			{
				return new StoreQueryTranslator.Context(database, table);
			}

			// Token: 0x0600030C RID: 780 RVA: 0x00013608 File Offset: 0x00011808
			public static StoreQueryTranslator.Context Create(StoreDatabase database, TableFunction table, object[] parameters)
			{
				return new StoreQueryTranslator.Context(database, table, parameters);
			}

			// Token: 0x0600030D RID: 781 RVA: 0x00013614 File Offset: 0x00011814
			internal static object[] GetTableFunctionParameters(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, TableFunction tableFunction, string[] tableParameters)
			{
				if (tableParameters == null)
				{
					if (tableFunction.Name.Equals("Catalog", StringComparison.OrdinalIgnoreCase))
					{
						return CatalogTableFunction.GetParameters(context);
					}
					if (tableFunction.ParameterTypes.Length > 0)
					{
						throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.IncorrectParameterCount(tableFunction.Name, tableFunction.ParameterTypes.Length));
					}
					return new object[0];
				}
				else
				{
					if (tableFunction.ParameterTypes.Length != tableParameters.Length)
					{
						throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.IncorrectParameterCount(tableFunction.Name, tableFunction.ParameterTypes.Length));
					}
					object[] array = new object[tableFunction.ParameterTypes.Length];
					for (int i = 0; i < tableFunction.ParameterTypes.Length; i++)
					{
						if (string.Equals(tableParameters[i], "null", StringComparison.OrdinalIgnoreCase))
						{
							array[i] = null;
						}
						else
						{
							array[i] = StoreQueryTranslator.GetTypedValue(tableFunction.ParameterTypes[i], tableParameters[i]);
						}
					}
					return array;
				}
			}

			// Token: 0x040001B3 RID: 435
			private readonly StoreDatabase database;

			// Token: 0x040001B4 RID: 436
			private readonly Table table;

			// Token: 0x040001B5 RID: 437
			private readonly object[] parameters;
		}

		// Token: 0x02000060 RID: 96
		internal class SetCollection
		{
			// Token: 0x0600030E RID: 782 RVA: 0x000136DA File Offset: 0x000118DA
			private SetCollection(IList<Column> columns, IList<object> values)
			{
				this.columns = columns;
				this.values = values;
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x0600030F RID: 783 RVA: 0x000136F0 File Offset: 0x000118F0
			public IList<Column> Columns
			{
				get
				{
					return this.columns;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000310 RID: 784 RVA: 0x000136F8 File Offset: 0x000118F8
			public IList<object> Values
			{
				get
				{
					return this.values;
				}
			}

			// Token: 0x06000311 RID: 785 RVA: 0x00013700 File Offset: 0x00011900
			public static StoreQueryTranslator.SetCollection Create(StoreQueryTranslator.Context context, IDictionary<string, string> setList)
			{
				if (context == null)
				{
					throw new ArgumentNullException("context");
				}
				if (setList == null)
				{
					throw new ArgumentNullException("setList");
				}
				IList<Column> list = new List<Column>(setList.Keys.Count);
				IList<object> list2 = new List<object>(setList.Keys.Count);
				foreach (string text in setList.Keys)
				{
					Column column = StoreQueryTranslator.FindColumnByName(context, text);
					object typedValue = StoreQueryTranslator.GetTypedValue(column.Type, setList[text]);
					if (column.Type.IsValueType && !column.IsNullable && typedValue == null)
					{
						throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.ColumnRequiresValue(column.Name));
					}
					list.Add(column);
					list2.Add(typedValue);
				}
				return new StoreQueryTranslator.SetCollection(list, list2);
			}

			// Token: 0x040001B6 RID: 438
			private readonly IList<Column> columns;

			// Token: 0x040001B7 RID: 439
			private readonly IList<object> values;

			// Token: 0x02000061 RID: 97
			internal static class TestAccess
			{
				// Token: 0x06000312 RID: 786 RVA: 0x000137E8 File Offset: 0x000119E8
				public static StoreQueryTranslator.SetCollection Create(IList<Column> columns, IList<object> values)
				{
					return new StoreQueryTranslator.SetCollection(columns, values);
				}
			}
		}

		// Token: 0x02000062 RID: 98
		private class InternalTableFactory : StoreQueryTranslator.IStoreTableFactory
		{
			// Token: 0x06000313 RID: 787 RVA: 0x000137F4 File Offset: 0x000119F4
			public StoreQueryTranslator.Context GetContext(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, string tableName, string[] tableParameters)
			{
				Table table;
				if (!((table = StoreQueryTranslator.InternalTableFactory.FindTableByName(context, tableName)) != null))
				{
					throw new DiagnosticQueryTranslatorException(DiagnosticQueryStrings.TableNotFound(tableName));
				}
				TableFunction tableFunction = table as TableFunction;
				if (tableFunction != null)
				{
					object[] tableFunctionParameters = StoreQueryTranslator.Context.GetTableFunctionParameters(context, tableFunction, tableParameters);
					return StoreQueryTranslator.Context.Create(context.Database, tableFunction, tableFunctionParameters);
				}
				return StoreQueryTranslator.Context.Create(context.Database, table);
			}

			// Token: 0x06000314 RID: 788 RVA: 0x00013854 File Offset: 0x00011A54
			private static Table FindTableByName(Microsoft.Exchange.Server.Storage.StoreCommonServices.Context context, string tableName)
			{
				Table table = null;
				if (context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Jet && StoreQueryTranslator.IsJetOnlyTable(tableName))
				{
					return null;
				}
				TableFunction tableFunction;
				if (StoreQueryTargets.Targets.TryGetValue(tableName, out tableFunction))
				{
					table = tableFunction;
				}
				else if (context.Database.IsOnlineActive || context.Database.IsOnlinePassiveAttachedReadOnly)
				{
					table = context.Database.PhysicalDatabase.GetTableMetadata(tableName);
				}
				else if (context.Database.IsOnlinePassive && tableName.Equals("Catalog", StringComparison.OrdinalIgnoreCase))
				{
					table = StoreQueryTranslator.InternalTableFactory.catalogTableFunctionForOnlinePassiveReplayingLogs.TableFunction;
				}
				if (table != null && context.Database.CurrentSchemaVersionForDiagnostics.IsSupported(table.MinVersion, table.MaxVersion))
				{
					return table;
				}
				return null;
			}

			// Token: 0x040001B8 RID: 440
			private static readonly CatalogTableFunction catalogTableFunctionForOnlinePassiveReplayingLogs = new CatalogTableFunction();
		}
	}
}
