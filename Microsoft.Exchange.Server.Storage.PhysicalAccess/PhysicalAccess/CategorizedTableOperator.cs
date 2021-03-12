using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000069 RID: 105
	public abstract class CategorizedTableOperator : SimpleQueryOperator
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00014D4C File Offset: 0x00012F4C
		protected CategorizedTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation) : this(connectionProvider, new CategorizedTableOperator.CategorizedTableOperatorDefinition(culture, table, categorizedTableParams, collapseState, columnsToFetch, additionalHeaderRenameDictionary, additionalLeafRenameDictionary, (restriction is SearchCriteriaTrue) ? null : restriction, skipTo, maxRows, keyRange, backwards, frequentOperation))
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014D89 File Offset: 0x00012F89
		protected CategorizedTableOperator(IConnectionProvider connectionProvider, CategorizedTableOperator.CategorizedTableOperatorDefinition definition) : base(connectionProvider, definition)
		{
			if (ExTraceGlobals.CategorizedTableOperatorTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.CategorizedTableOperatorTracer.TraceDebug<CategorizedTableOperator, KeyRange>(0L, "{0}  keyRange:[{1}]", this, definition.KeyRange);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00014DB8 File Offset: 0x00012FB8
		internal Reader HeaderReader
		{
			get
			{
				if (this.headerReader == null)
				{
					return null;
				}
				return this.headerReader.InternalReader;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00014DCF File Offset: 0x00012FCF
		internal Reader LeafReader
		{
			get
			{
				if (this.leafReader == null)
				{
					return null;
				}
				return this.leafReader.InternalReader;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00014DE6 File Offset: 0x00012FE6
		internal Table HeaderTable
		{
			get
			{
				return this.OperatorDefinition.HeaderTable;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00014DF3 File Offset: 0x00012FF3
		internal Table LeafTable
		{
			get
			{
				return this.OperatorDefinition.LeafTable;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00014E00 File Offset: 0x00013000
		internal int CategoryCount
		{
			get
			{
				return this.OperatorDefinition.CategoryCount;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00014E0D File Offset: 0x0001300D
		internal bool BaseMessageViewInReverseOrder
		{
			get
			{
				return this.OperatorDefinition.BaseMessageViewInReverseOrder;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00014E1A File Offset: 0x0001301A
		internal CategorizedTableCollapseState CollapseState
		{
			get
			{
				return this.OperatorDefinition.CollapseState;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00014E27 File Offset: 0x00013027
		internal IList<Column> HeaderColumnsToFetch
		{
			get
			{
				return this.OperatorDefinition.HeaderColumnsToFetch;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00014E34 File Offset: 0x00013034
		internal IList<Column> LeafColumnsToFetch
		{
			get
			{
				return this.OperatorDefinition.LeafColumnsToFetch;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00014E41 File Offset: 0x00013041
		internal IList<Column> LeafColumnsToFetchWithoutJoin
		{
			get
			{
				return this.OperatorDefinition.LeafColumnsToFetchWithoutJoin;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00014E4E File Offset: 0x0001304E
		internal bool AtLeastOneExternalColumn
		{
			get
			{
				return this.OperatorDefinition.AtLeastOneExternalColumn;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00014E5B File Offset: 0x0001305B
		internal IReadOnlyDictionary<Column, Column> HeaderRenameDictionary
		{
			get
			{
				return this.OperatorDefinition.HeaderRenameDictionary;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00014E68 File Offset: 0x00013068
		internal IReadOnlyDictionary<Column, Column> LeafIndexRenameDictionary
		{
			get
			{
				return this.OperatorDefinition.LeafIndexRenameDictionary;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00014E75 File Offset: 0x00013075
		internal IReadOnlyDictionary<Column, Column> LeafMessageRenameDictionary
		{
			get
			{
				return this.OperatorDefinition.LeafMessageRenameDictionary;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00014E82 File Offset: 0x00013082
		internal SortOrder HeaderLogicalSortOrder
		{
			get
			{
				return this.OperatorDefinition.HeaderLogicalSortOrder;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00014E8F File Offset: 0x0001308F
		internal SortOrder LeafLogicalSortOrder
		{
			get
			{
				return this.OperatorDefinition.LeafLogicalSortOrder;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00014E9C File Offset: 0x0001309C
		internal IList<object> HeaderKeyPrefix
		{
			get
			{
				return this.OperatorDefinition.HeaderKeyPrefix;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00014EA9 File Offset: 0x000130A9
		internal IList<object> LeafKeyPrefix
		{
			get
			{
				return this.OperatorDefinition.LeafKeyPrefix;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00014EB6 File Offset: 0x000130B6
		internal KeyRange HeaderKeyRange
		{
			get
			{
				return this.OperatorDefinition.HeaderKeyRange;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00014EC3 File Offset: 0x000130C3
		internal KeyRange LeafKeyRange
		{
			get
			{
				return this.OperatorDefinition.LeafKeyRange;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00014ED0 File Offset: 0x000130D0
		internal bool IsInclusiveStartKey
		{
			get
			{
				return this.OperatorDefinition.IsInclusiveStartKey;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00014EDD File Offset: 0x000130DD
		internal IList<StorePropTag> HeaderOnlyPropTags
		{
			get
			{
				return this.OperatorDefinition.HeaderOnlyPropTags;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00014EEA File Offset: 0x000130EA
		internal Column DepthColumn
		{
			get
			{
				return this.OperatorDefinition.DepthColumn;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00014EF7 File Offset: 0x000130F7
		internal Column CategIdColumn
		{
			get
			{
				return this.OperatorDefinition.CategIdColumn;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00014F04 File Offset: 0x00013104
		internal Column RowTypeColumn
		{
			get
			{
				return this.OperatorDefinition.RowTypeColumn;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00014F11 File Offset: 0x00013111
		public new CategorizedTableOperator.CategorizedTableOperatorDefinition OperatorDefinition
		{
			get
			{
				return (CategorizedTableOperator.CategorizedTableOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014F20 File Offset: 0x00013120
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			Reader result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = new CategorizedTableOperator.CategorizedTableReader(base.Connection, this, disposeQueryOperator);
			}
			return result;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014F74 File Offset: 0x00013174
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014F80 File Offset: 0x00013180
		protected bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			if (this.leafReader != null)
			{
				this.leafReader.CloseReader();
				this.leafReader = null;
			}
			if (this.headerReader != null)
			{
				this.headerReader.CloseReader();
				this.headerReader = null;
			}
			this.rowsReturned = 0;
			StartStopKey startKey = this.LeafKeyRange.StartKey;
			bool flag = startKey.Count > this.LeafKeyPrefix.Count;
			bool flag2 = base.Backwards && !flag;
			TableOperator tableOperator = Factory.CreateTableOperator(base.Culture, base.Connection, this.HeaderTable, this.HeaderTable.PrimaryKeyIndex, this.HeaderColumnsToFetch, null, this.HeaderRenameDictionary, 0, 0, this.HeaderKeyRange, base.Backwards, base.FrequentOperation);
			this.headerReader = new CategorizedTableOperator.HeaderOrLeafReader(tableOperator.ExecuteReader(true));
			bool flag3 = this.HeaderReader.Read();
			if (!flag3)
			{
				base.TraceMove("MoveFirst", false);
			}
			else if (!this.IsHeaderVisible())
			{
				flag3 = this.MoveNext("MoveFirst", base.SkipTo, true, ref rowsSkipped);
			}
			else if ((flag && this.CheckHeaderRowMatchesLeafStartKey()) || (flag2 && this.CheckStartOnLastLeafRowOfHeader()))
			{
				if (!this.IsLowestHeaderLevel())
				{
					throw new StoreException((LID)37663U, ErrorCodeValue.InvalidBookmark, "Leaf bookmark was provided, but header bookmark does not correspond to a header row at the lowest category header level.");
				}
				this.GetLeafReaderIfNecessary(flag);
				if (this.LeafReader != null)
				{
					if (!this.LeafReader.Read())
					{
						Reader reader = this.LeafReader;
						flag3 = this.MoveNext("MoveFirst", base.SkipTo, true, ref rowsSkipped);
					}
					else
					{
						if (!this.CheckMatchingHeaderAndLeaf())
						{
							throw new StoreException((LID)62239U, ErrorCodeValue.InvalidBookmark, "Header and Leaf row bookmarks do not match.");
						}
						if (base.Criteria != null)
						{
							bool flag4 = base.Criteria.Evaluate(this.leafReader, base.CompareInfo);
							bool? flag5 = new bool?(true);
							if (!flag4 || flag5 == null)
							{
								return this.MoveNext("MoveFirst", base.SkipTo, false, ref rowsSkipped);
							}
						}
						if (base.SkipTo > 0)
						{
							rowsSkipped++;
							flag3 = this.MoveNext("MoveFirst", base.SkipTo - 1, false, ref rowsSkipped);
						}
						else
						{
							this.rowsReturned++;
							base.TraceMove("Leaf MoveFirst", true);
						}
					}
				}
				else
				{
					flag3 = this.MoveNext("MoveFirst", base.SkipTo, true, ref rowsSkipped);
				}
			}
			else
			{
				bool isInclusiveStartKey = this.IsInclusiveStartKey;
				StartStopKey startKey2 = this.HeaderKeyRange.StartKey;
				if (isInclusiveStartKey != startKey2.Inclusive && this.CheckHeaderRowMatchesHeaderStartKey())
				{
					flag3 = this.MoveNext("MoveFirst", base.SkipTo, false, ref rowsSkipped);
				}
				else
				{
					if (base.Criteria != null)
					{
						bool flag6 = base.Criteria.Evaluate(this.headerReader, base.CompareInfo);
						bool? flag7 = new bool?(true);
						if (!flag6 || flag7 == null)
						{
							return this.MoveNext("MoveFirst", base.SkipTo, base.Backwards, ref rowsSkipped);
						}
					}
					if (base.SkipTo > 0)
					{
						rowsSkipped++;
						flag3 = this.MoveNext("MoveFirst", base.SkipTo - 1, base.Backwards, ref rowsSkipped);
					}
					else
					{
						this.rowsReturned++;
						base.TraceMove("Header MoveFirst", true);
					}
				}
			}
			return flag3;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000152C8 File Offset: 0x000134C8
		private bool MoveNext()
		{
			bool forceNextHeader = base.Backwards && this.LeafReader == null;
			int num = 0;
			return this.MoveNext("MoveNext", 0, forceNextHeader, ref num);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000152FC File Offset: 0x000134FC
		protected bool MoveNext(string operation, int numberLeftToSkip, bool forceNextHeader, ref int rowsSkipped)
		{
			if (base.MaxRows > 0 && this.rowsReturned >= base.MaxRows)
			{
				base.TraceMove(operation + " reached MaxRows", false);
				return false;
			}
			Column column = (numberLeftToSkip > 1 && (base.Criteria == null || base.Criteria is SearchCriteriaTrue)) ? this.GetContentCountColumn() : null;
			bool flag = false;
			IL_28C:
			while (!flag)
			{
				if (!forceNextHeader)
				{
					bool flag2 = false;
					if (column != null && numberLeftToSkip > 1 && this.LeafReader == null && this.IsLeafVisible())
					{
						int num = this.HeaderReader.GetInt32(column);
						if (base.Backwards)
						{
							num++;
						}
						if (num <= numberLeftToSkip)
						{
							numberLeftToSkip -= num;
							rowsSkipped += num;
							flag2 = true;
						}
					}
					if (!flag2)
					{
						this.GetLeafReaderIfNecessary(false);
					}
				}
				if (this.LeafReader != null)
				{
					if (this.IsHeaderExpanded() && !forceNextHeader)
					{
						while (this.LeafReader.Read())
						{
							if (base.Criteria != null)
							{
								bool flag3 = base.Criteria.Evaluate(this.leafReader, base.CompareInfo);
								bool? flag4 = new bool?(true);
								if (!flag3 || flag4 == null)
								{
									continue;
								}
							}
							if (numberLeftToSkip <= 0)
							{
								this.rowsReturned++;
								base.TraceMove("Leaf " + operation, true);
								return true;
							}
							rowsSkipped++;
							numberLeftToSkip--;
						}
					}
					this.leafReader.CloseReader();
					this.leafReader = null;
					if (base.Backwards)
					{
						if (base.Criteria != null)
						{
							bool flag5 = base.Criteria.Evaluate(this.headerReader, base.CompareInfo);
							bool? flag6 = new bool?(true);
							if (!flag5 || flag6 == null)
							{
								goto IL_1DF;
							}
						}
						if (numberLeftToSkip <= 0)
						{
							this.rowsReturned++;
							base.TraceMove("Header " + operation, true);
							return true;
						}
						rowsSkipped++;
						numberLeftToSkip--;
					}
				}
				IL_1DF:
				forceNextHeader = false;
				while (this.HeaderReader.Read())
				{
					if (this.IsHeaderVisible())
					{
						if (base.Backwards && this.IsLeafVisible())
						{
							goto IL_28C;
						}
						if (base.Criteria != null)
						{
							bool flag7 = base.Criteria.Evaluate(this.headerReader, base.CompareInfo);
							bool? flag8 = new bool?(true);
							if (!flag7 || flag8 == null)
							{
								if (this.IsLeafVisible())
								{
									goto IL_28C;
								}
								continue;
							}
						}
						if (numberLeftToSkip > 0)
						{
							rowsSkipped++;
							numberLeftToSkip--;
							goto IL_28C;
						}
						this.rowsReturned++;
						base.TraceMove("Header " + operation, true);
						return true;
					}
				}
				flag = true;
			}
			base.TraceMove(operation, false);
			return false;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000155A4 File Offset: 0x000137A4
		private void GetLeafReaderIfNecessary(bool useLeafBookmark)
		{
			if (this.LeafReader == null && this.IsLeafVisible())
			{
				IList<object> list = new List<object>(this.LeafKeyPrefix);
				int count = this.LeafKeyPrefix.Count;
				for (int i = 0; i < this.CategoryCount; i++)
				{
					Column column = this.LeafLogicalSortOrder.Columns[i];
					object value = this.HeaderReader.GetValue(column);
					list.Add(value);
				}
				StartStopKey startStopKey = new StartStopKey(true, list);
				StartStopKey startKey = useLeafBookmark ? this.LeafKeyRange.StartKey : startStopKey;
				this.TraceLeafReader(startKey, useLeafBookmark);
				TableOperator tableOperator = Factory.CreateTableOperator(base.Culture, base.Connection, this.LeafTable, this.LeafTable.PrimaryKeyIndex, this.LeafColumnsToFetchWithoutJoin, null, this.LeafIndexRenameDictionary, 0, 0, new KeyRange(startKey, startStopKey), base.Backwards ^ this.BaseMessageViewInReverseOrder, base.FrequentOperation);
				SimpleQueryOperator simpleQueryOperator;
				if (this.AtLeastOneExternalColumn)
				{
					using (tableOperator)
					{
						simpleQueryOperator = Factory.CreateJoinOperator(base.Culture, base.Connection, base.Table, this.LeafColumnsToFetch, null, this.LeafMessageRenameDictionary, 0, 0, base.Table.PrimaryKeyIndex.Columns, tableOperator, base.FrequentOperation);
						goto IL_13C;
					}
				}
				simpleQueryOperator = tableOperator;
				try
				{
					IL_13C:
					this.leafReader = new CategorizedTableOperator.HeaderOrLeafReader(simpleQueryOperator.ExecuteReader(true));
					simpleQueryOperator = null;
				}
				finally
				{
					if (simpleQueryOperator != null)
					{
						simpleQueryOperator.Dispose();
						simpleQueryOperator = null;
					}
				}
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015730 File Offset: 0x00013930
		private void TraceLeafReader(StartStopKey startKey, bool useLeafBookmark)
		{
			if (ExTraceGlobals.CategorizedTableOperatorTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("cn:[");
				stringBuilder.Append(base.Connection.GetHashCode());
				stringBuilder.Append("] ");
				stringBuilder.Append("GetLeafReader");
				stringBuilder.Append(" op:[");
				stringBuilder.Append(base.OperatorName);
				stringBuilder.Append(" ");
				stringBuilder.Append(this.GetHashCode());
				stringBuilder.Append("]");
				stringBuilder.Append(" useLeafBookmark:[");
				stringBuilder.Append(useLeafBookmark);
				stringBuilder.Append("]");
				stringBuilder.Append(" startKey(" + startKey.Count + "):[");
				startKey.AppendToStringBuilder(stringBuilder, StringFormatOptions.None);
				stringBuilder.Append("]");
				stringBuilder.Append(" atLeastOneExternalColumn:[");
				stringBuilder.Append(this.AtLeastOneExternalColumn);
				stringBuilder.Append("]");
				ExTraceGlobals.CategorizedTableOperatorTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015858 File Offset: 0x00013A58
		private bool CheckStartOnLastLeafRowOfHeader()
		{
			bool result = false;
			if (this.IsLeafVisible())
			{
				StartStopKey startKey = this.HeaderKeyRange.StartKey;
				if (startKey.Count <= this.HeaderKeyPrefix.Count)
				{
					result = true;
				}
				else
				{
					StartStopKey startKey2 = this.HeaderKeyRange.StartKey;
					if (!startKey2.Inclusive)
					{
						result = true;
					}
					else if (!this.CheckHeaderRowMatchesHeaderStartKey())
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000158BC File Offset: 0x00013ABC
		private bool CheckHeaderRowMatchesHeaderStartKey()
		{
			int num = this.HeaderKeyPrefix.Count;
			for (;;)
			{
				int num2 = num;
				StartStopKey startKey = this.HeaderKeyRange.StartKey;
				if (num2 >= startKey.Count)
				{
					return true;
				}
				StartStopKey startKey2 = this.HeaderKeyRange.StartKey;
				object x = startKey2.Values[num];
				Column column = this.HeaderLogicalSortOrder.Columns[num - this.HeaderKeyPrefix.Count];
				object value = this.HeaderReader.GetValue(column);
				if (!ValueHelper.ValuesEqual(x, value, base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
				{
					break;
				}
				num++;
			}
			return false;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015950 File Offset: 0x00013B50
		private bool CheckHeaderRowMatchesLeafStartKey()
		{
			for (int i = 0; i < this.CategoryCount; i++)
			{
				Column column = this.LeafLogicalSortOrder.Columns[i];
				object value = this.HeaderReader.GetValue(column);
				StartStopKey startKey = this.LeafKeyRange.StartKey;
				object y = startKey.Values[this.LeafKeyPrefix.Count + i];
				if (!ValueHelper.ValuesEqual(value, y, base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000159CC File Offset: 0x00013BCC
		private bool CheckMatchingHeaderAndLeaf()
		{
			for (int i = 0; i < this.CategoryCount; i++)
			{
				Column column = this.LeafLogicalSortOrder.Columns[i];
				object value = this.HeaderReader.GetValue(column);
				object obj = this.LeafReader.GetValue(column);
				Column column2 = this.HeaderRenameDictionary[column];
				bool flag;
				obj = ValueHelper.TruncateValueIfNecessary(obj, column2.Type, column2.MaxLength, out flag);
				if (!ValueHelper.ValuesEqual(value, obj, base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00015A54 File Offset: 0x00013C54
		private bool IsHeaderExpanded()
		{
			int @int = this.HeaderReader.GetInt32(this.DepthColumn);
			long int2 = this.HeaderReader.GetInt64(this.CategIdColumn);
			return this.CollapseState.IsHeaderExpanded(@int, int2);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00015A94 File Offset: 0x00013C94
		private bool IsHeaderVisible()
		{
			int @int = this.HeaderReader.GetInt32(this.DepthColumn);
			long int2 = this.HeaderReader.GetInt64(this.CategIdColumn);
			return this.CollapseState.IsHeaderVisible(@int, int2);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00015AD4 File Offset: 0x00013CD4
		protected bool IsLowestHeaderLevel()
		{
			int @int = this.HeaderReader.GetInt32(this.DepthColumn);
			return @int == this.CategoryCount - 1;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015AFE File Offset: 0x00013CFE
		protected bool IsLeafVisible()
		{
			return this.IsLowestHeaderLevel() && this.IsHeaderExpanded();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015B10 File Offset: 0x00013D10
		protected Column GetContentCountColumn()
		{
			foreach (Column column in this.HeaderColumnsToFetch)
			{
				if (column is ExtendedPropertyColumn && ((ExtendedPropertyColumn)column).StorePropTag.PropTag == 906100739U)
				{
					return column;
				}
			}
			return null;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015B80 File Offset: 0x00013D80
		protected Reader CurrentReader(Column columnToRetrieve)
		{
			return (this.leafReader == null || this.MustRetrieveFromHeader(columnToRetrieve)) ? this.headerReader.InternalReader : this.leafReader.InternalReader;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015BB8 File Offset: 0x00013DB8
		private bool MustRetrieveFromHeader(Column columnToRetrieve)
		{
			if (this.HeaderOnlyPropTags == null)
			{
				return false;
			}
			ExtendedPropertyColumn extendedPropertyColumn = columnToRetrieve as ExtendedPropertyColumn;
			return extendedPropertyColumn != null && this.HeaderOnlyPropTags.Contains(extendedPropertyColumn.StorePropTag);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015BF2 File Offset: 0x00013DF2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CategorizedTableOperator>(this);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015BFA File Offset: 0x00013DFA
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.leafReader != null)
				{
					this.leafReader.CloseReader();
					this.leafReader = null;
				}
				if (this.headerReader != null)
				{
					this.headerReader.CloseReader();
					this.headerReader = null;
				}
			}
		}

		// Token: 0x0400015C RID: 348
		private CategorizedTableOperator.HeaderOrLeafReader headerReader;

		// Token: 0x0400015D RID: 349
		private CategorizedTableOperator.HeaderOrLeafReader leafReader;

		// Token: 0x0400015E RID: 350
		private int rowsReturned;

		// Token: 0x0200006A RID: 106
		public class CategorizedTableOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x0600048A RID: 1162 RVA: 0x00015C34 File Offset: 0x00013E34
			public CategorizedTableOperatorDefinition(CultureInfo culture, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation) : base(culture, table, columnsToFetch, restriction, null, skipTo, maxRows, frequentOperation)
			{
				this.categorizedTableParams = categorizedTableParams;
				this.collapseState = collapseState;
				this.isInclusiveStartKey = keyRange.StartKey.Inclusive;
				this.backwards = backwards;
				this.keyRange = keyRange;
				this.ConfigureHeaderAndLeafColumnsToFetchAndRenameDictionaries(table, columnsToFetch, categorizedTableParams, collapseState, restriction, additionalHeaderRenameDictionary, additionalLeafRenameDictionary);
				this.ComputeHeaderAndLeafKeyRanges(keyRange, categorizedTableParams, backwards);
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600048B RID: 1163 RVA: 0x00015CA2 File Offset: 0x00013EA2
			internal override string OperatorName
			{
				get
				{
					return "CATEGORIZED TABLE";
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600048C RID: 1164 RVA: 0x00015CA9 File Offset: 0x00013EA9
			public override bool Backwards
			{
				get
				{
					return this.backwards;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600048D RID: 1165 RVA: 0x00015CB1 File Offset: 0x00013EB1
			public CategorizedTableParams TableParams
			{
				get
				{
					return this.categorizedTableParams;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600048E RID: 1166 RVA: 0x00015CB9 File Offset: 0x00013EB9
			public KeyRange KeyRange
			{
				get
				{
					return this.keyRange;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x0600048F RID: 1167 RVA: 0x00015CC1 File Offset: 0x00013EC1
			internal Table HeaderTable
			{
				get
				{
					return this.categorizedTableParams.HeaderTable;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000490 RID: 1168 RVA: 0x00015CCE File Offset: 0x00013ECE
			internal Table LeafTable
			{
				get
				{
					return this.categorizedTableParams.LeafTable;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000491 RID: 1169 RVA: 0x00015CDB File Offset: 0x00013EDB
			internal int CategoryCount
			{
				get
				{
					return this.categorizedTableParams.CategoryCount;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000492 RID: 1170 RVA: 0x00015CE8 File Offset: 0x00013EE8
			internal bool BaseMessageViewInReverseOrder
			{
				get
				{
					return this.categorizedTableParams.BaseMessageViewInReverseOrder;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x00015CF5 File Offset: 0x00013EF5
			public CategorizedTableCollapseState CollapseState
			{
				get
				{
					return this.collapseState;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000494 RID: 1172 RVA: 0x00015CFD File Offset: 0x00013EFD
			internal IList<Column> HeaderColumnsToFetch
			{
				get
				{
					return this.headerColumnsToFetch;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x00015D05 File Offset: 0x00013F05
			internal IList<Column> LeafColumnsToFetch
			{
				get
				{
					return this.leafColumnsToFetch;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000496 RID: 1174 RVA: 0x00015D0D File Offset: 0x00013F0D
			internal IList<Column> LeafColumnsToFetchWithoutJoin
			{
				get
				{
					return this.leafColumnsToFetchWithoutJoin;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000497 RID: 1175 RVA: 0x00015D15 File Offset: 0x00013F15
			internal bool AtLeastOneExternalColumn
			{
				get
				{
					return this.atLeastOneExternalColumn;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000498 RID: 1176 RVA: 0x00015D1D File Offset: 0x00013F1D
			internal IReadOnlyDictionary<Column, Column> HeaderRenameDictionary
			{
				get
				{
					return this.headerRenameDictionary;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000499 RID: 1177 RVA: 0x00015D25 File Offset: 0x00013F25
			internal IReadOnlyDictionary<Column, Column> LeafIndexRenameDictionary
			{
				get
				{
					return this.leafIndexRenameDictionary;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x0600049A RID: 1178 RVA: 0x00015D2D File Offset: 0x00013F2D
			internal IReadOnlyDictionary<Column, Column> LeafMessageRenameDictionary
			{
				get
				{
					return this.leafMessageRenameDictionary;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600049B RID: 1179 RVA: 0x00015D35 File Offset: 0x00013F35
			internal KeyRange HeaderKeyRange
			{
				get
				{
					return this.headerKeyRange;
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x00015D3D File Offset: 0x00013F3D
			internal KeyRange LeafKeyRange
			{
				get
				{
					return this.leafKeyRange;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x0600049D RID: 1181 RVA: 0x00015D45 File Offset: 0x00013F45
			internal bool IsInclusiveStartKey
			{
				get
				{
					return this.isInclusiveStartKey;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x00015D4D File Offset: 0x00013F4D
			public override SortOrder SortOrder
			{
				get
				{
					return this.HeaderTable.PrimaryKeyIndex.SortOrder;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600049F RID: 1183 RVA: 0x00015D5F File Offset: 0x00013F5F
			internal SortOrder HeaderLogicalSortOrder
			{
				get
				{
					return this.categorizedTableParams.HeaderLogicalSortOrder;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00015D6C File Offset: 0x00013F6C
			internal SortOrder LeafLogicalSortOrder
			{
				get
				{
					return this.categorizedTableParams.LeafLogicalSortOrder;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00015D79 File Offset: 0x00013F79
			internal IList<object> HeaderKeyPrefix
			{
				get
				{
					return this.categorizedTableParams.HeaderKeyPrefix;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00015D86 File Offset: 0x00013F86
			internal IList<object> LeafKeyPrefix
			{
				get
				{
					return this.categorizedTableParams.LeafKeyPrefix;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00015D93 File Offset: 0x00013F93
			internal IList<StorePropTag> HeaderOnlyPropTags
			{
				get
				{
					return this.categorizedTableParams.HeaderOnlyPropTags;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00015DA0 File Offset: 0x00013FA0
			internal Column DepthColumn
			{
				get
				{
					return this.categorizedTableParams.DepthColumn;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00015DBC File Offset: 0x00013FBC
			internal Column CategIdColumn
			{
				get
				{
					return this.categorizedTableParams.CategIdColumn;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00015DD8 File Offset: 0x00013FD8
			internal Column RowTypeColumn
			{
				get
				{
					return this.categorizedTableParams.RowTypeColumn;
				}
			}

			// Token: 0x060004A7 RID: 1191 RVA: 0x00015DF2 File Offset: 0x00013FF2
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateCategorizedTableOperator(connectionProvider, this);
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x00015DFB File Offset: 0x00013FFB
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x00015E44 File Offset: 0x00014044
			private void ConfigureHeaderAndLeafColumnsToFetchAndRenameDictionaries(Table table, IList<Column> columnsToFetch, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary)
			{
				List<Column> list3 = new List<Column>(table.PrimaryKeyIndex.Columns);
				int num = (columnsToFetch != null) ? columnsToFetch.Count : 0;
				this.headerColumnsToFetch = new List<Column>(num);
				list3.Capacity = table.PrimaryKeyIndex.ColumnCount + num;
				this.leafColumnsToFetch = list3;
				this.headerRenameDictionary = new Dictionary<Column, Column>(categorizedTableParams.HeaderRenameDictionary.Count + additionalHeaderRenameDictionary.Count + 1 + columnsToFetch.Count + categorizedTableParams.LeafLogicalSortOrder.Columns.Count);
				foreach (KeyValuePair<Column, Column> keyValuePair in categorizedTableParams.HeaderRenameDictionary)
				{
					this.headerRenameDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<Column, Column> keyValuePair2 in additionalHeaderRenameDictionary)
				{
					this.headerRenameDictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
				this.headerRenameDictionary.Add(this.RowTypeColumn, Factory.CreateFunctionColumn("HeaderRowType", typeof(int), 4, 0, this.HeaderTable, (object[] columnValues) => collapseState.IsHeaderExpanded((int)columnValues[0], (long)columnValues[1]) ? 3 : 4, "ComputeHeaderRowType", new Column[]
				{
					this.DepthColumn,
					this.CategIdColumn
				}));
				this.leafMessageRenameDictionary = new Dictionary<Column, Column>(additionalLeafRenameDictionary.Count + 3);
				foreach (KeyValuePair<Column, Column> keyValuePair3 in additionalLeafRenameDictionary)
				{
					this.leafMessageRenameDictionary.Add(keyValuePair3.Key, keyValuePair3.Value);
				}
				this.leafMessageRenameDictionary.Add(this.CategIdColumn, Factory.CreateConstantColumn(null, this.CategIdColumn));
				this.leafMessageRenameDictionary.Add(this.DepthColumn, Factory.CreateConstantColumn(this.CategoryCount, this.DepthColumn));
				this.leafMessageRenameDictionary.Add(this.RowTypeColumn, Factory.CreateConstantColumn(1, this.RowTypeColumn));
				this.leafIndexRenameDictionary = new Dictionary<Column, Column>(categorizedTableParams.LeafRenameDictionary.Count + this.leafMessageRenameDictionary.Count);
				foreach (KeyValuePair<Column, Column> keyValuePair4 in categorizedTableParams.LeafRenameDictionary)
				{
					this.leafIndexRenameDictionary.Add(keyValuePair4.Key, keyValuePair4.Value);
				}
				foreach (KeyValuePair<Column, Column> keyValuePair5 in this.leafMessageRenameDictionary)
				{
					this.leafIndexRenameDictionary.Add(keyValuePair5.Key, keyValuePair5.Value);
				}
				foreach (Column column in columnsToFetch)
				{
					this.ValidateColumnToFetch(column);
				}
				if (restriction != null)
				{
					List<Column> list2 = new List<Column>();
					restriction.EnumerateColumns(delegate(Column c, object list)
					{
						((List<Column>)list).Add(c);
					}, list2, true);
					foreach (Column column2 in list2)
					{
						this.ValidateColumnToFetch(column2);
					}
				}
				for (int i = 0; i < categorizedTableParams.LeafLogicalSortOrder.Columns.Count; i++)
				{
					Column column3 = categorizedTableParams.LeafLogicalSortOrder.Columns[i];
					if (!this.leafColumnsToFetch.Contains(column3))
					{
						this.leafColumnsToFetch.Add(column3);
					}
					if (i >= categorizedTableParams.CategoryCount && !this.headerRenameDictionary.ContainsKey(column3))
					{
						this.headerRenameDictionary[column3] = Factory.CreateConstantColumn(null, column3);
					}
				}
				foreach (Column column4 in categorizedTableParams.HeaderLogicalSortOrder.Columns)
				{
					if (!this.headerColumnsToFetch.Contains(column4))
					{
						this.headerColumnsToFetch.Add(column4);
					}
					if (!this.leafColumnsToFetch.Contains(column4))
					{
						ExtendedPropertyColumn extendedPropertyColumn = column4 as ExtendedPropertyColumn;
						if (extendedPropertyColumn == null || categorizedTableParams.HeaderOnlyPropTags == null || !categorizedTableParams.HeaderOnlyPropTags.Contains(extendedPropertyColumn.StorePropTag))
						{
							this.leafColumnsToFetch.Add(column4);
						}
					}
				}
				if (!this.headerColumnsToFetch.Contains(categorizedTableParams.CategIdColumn))
				{
					this.headerColumnsToFetch.Add(categorizedTableParams.CategIdColumn);
				}
				if (!this.leafColumnsToFetch.Contains(categorizedTableParams.CategIdColumn))
				{
					this.leafColumnsToFetch.Add(categorizedTableParams.CategIdColumn);
				}
				if (this.atLeastOneExternalColumn)
				{
					this.leafColumnsToFetchWithoutJoin = new List<Column>();
					for (int j = 0; j < table.PrimaryKeyIndex.Columns.Count; j++)
					{
						Column item = this.leafColumnsToFetch[j];
						this.leafColumnsToFetchWithoutJoin.Add(item);
					}
					for (int k = table.PrimaryKeyIndex.Columns.Count; k < this.leafColumnsToFetch.Count; k++)
					{
						Column column5 = this.leafColumnsToFetch[k];
						Column column6;
						if (this.leafIndexRenameDictionary.TryGetValue(column5, out column6) && column6.MaxLength >= column5.MaxLength)
						{
							this.leafColumnsToFetchWithoutJoin.Add(column5);
						}
					}
					return;
				}
				this.leafColumnsToFetchWithoutJoin = this.leafColumnsToFetch;
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x00016468 File Offset: 0x00014668
			private void ValidateColumnToFetch(Column column)
			{
				if (!this.headerColumnsToFetch.Contains(column))
				{
					this.headerColumnsToFetch.Add(column);
				}
				bool flag = false;
				if (this.categorizedTableParams.HeaderOnlyPropTags != null)
				{
					ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
					if (extendedPropertyColumn != null && this.categorizedTableParams.HeaderOnlyPropTags.Contains(extendedPropertyColumn.StorePropTag))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					if (!this.leafColumnsToFetch.Contains(column))
					{
						this.leafColumnsToFetch.Add(column);
					}
					Column actualColumn = column.ActualColumn;
					ConversionColumn conversionColumn = actualColumn as ConversionColumn;
					if (conversionColumn != null)
					{
						column = conversionColumn.ArgumentColumn;
					}
					if (!this.headerRenameDictionary.ContainsKey(column) && !(column is ConstantColumn))
					{
						this.headerRenameDictionary[column] = Factory.CreateConstantColumn(null, column);
						Column column2;
						if (!this.atLeastOneExternalColumn && (!this.leafIndexRenameDictionary.TryGetValue(column, out column2) || column2.MaxLength < column.MaxLength))
						{
							this.atLeastOneExternalColumn = true;
						}
					}
				}
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x00016560 File Offset: 0x00014760
			private void ComputeHeaderAndLeafKeyRanges(KeyRange keyRange, CategorizedTableParams categorizedTableParams, bool backwards)
			{
				Index primaryKeyIndex = categorizedTableParams.HeaderTable.PrimaryKeyIndex;
				Index primaryKeyIndex2 = categorizedTableParams.LeafTable.PrimaryKeyIndex;
				if (categorizedTableParams.HeaderKeyPrefix.Count >= primaryKeyIndex.ColumnCount || categorizedTableParams.LeafKeyPrefix.Count >= primaryKeyIndex2.ColumnCount)
				{
					throw new StoreException((LID)40208U, ErrorCodeValue.InvalidBookmark, "Key prefix must be a subset of the full primary key.");
				}
				int num = primaryKeyIndex.ColumnCount - categorizedTableParams.HeaderKeyPrefix.Count;
				int num2 = primaryKeyIndex2.ColumnCount - categorizedTableParams.LeafKeyPrefix.Count;
				List<object> list = new List<object>(categorizedTableParams.HeaderKeyPrefix);
				List<object> list2 = new List<object>(categorizedTableParams.HeaderKeyPrefix);
				List<object> list3 = new List<object>(categorizedTableParams.LeafKeyPrefix);
				List<object> values = new List<object>(categorizedTableParams.LeafKeyPrefix);
				bool inclusive = keyRange.StartKey.Inclusive;
				if (!keyRange.StartKey.IsEmpty)
				{
					if (keyRange.StartKey.Count != num + num2)
					{
						throw new StoreException((LID)49872U, ErrorCodeValue.InvalidBookmark, "The start key must be the concatenation of a header table key and a leaf table key.");
					}
					for (int i = 0; i < num; i++)
					{
						object obj = keyRange.StartKey.Values[i];
						Column column = primaryKeyIndex.Columns[categorizedTableParams.HeaderKeyPrefix.Count + i];
						bool flag;
						obj = ValueHelper.TruncateValueIfNecessary(obj, column.Type, column.MaxLength, out flag);
						list.Add(obj);
					}
					int index = categorizedTableParams.HeaderKeyPrefix.Count + num - 1;
					int? num3 = list[index] as int?;
					if (num3 == null || num3 < 0 || num3 > categorizedTableParams.CategoryCount)
					{
						throw new StoreException((LID)48400U, ErrorCodeValue.InvalidBookmark, "Invalid key value for Depth column.");
					}
					if (num3 == categorizedTableParams.CategoryCount)
					{
						inclusive = true;
						for (int j = num; j < keyRange.StartKey.Count; j++)
						{
							object obj2 = keyRange.StartKey.Values[j];
							Column column2 = primaryKeyIndex2.Columns[categorizedTableParams.LeafKeyPrefix.Count + j - num];
							bool flag2;
							obj2 = ValueHelper.TruncateValueIfNecessary(obj2, column2.Type, column2.MaxLength, out flag2);
							list3.Add(obj2);
						}
						list[index] = num3 - 1;
					}
					else if (num3 == categorizedTableParams.CategoryCount - 1 && !backwards)
					{
						inclusive = true;
					}
				}
				if (!keyRange.StopKey.IsEmpty)
				{
					if (keyRange.StopKey.Count != num + num2)
					{
						throw new StoreException((LID)64784U, ErrorCodeValue.InvalidBookmark, "The stop key must be the concatenation of a header table key and a leaf table key.");
					}
					for (int k = 0; k < num; k++)
					{
						object obj3 = keyRange.StopKey.Values[k];
						Column column3 = primaryKeyIndex.Columns[categorizedTableParams.HeaderKeyPrefix.Count + k];
						bool flag3;
						obj3 = ValueHelper.TruncateValueIfNecessary(obj3, column3.Type, column3.MaxLength, out flag3);
						list2.Add(obj3);
					}
				}
				this.headerKeyRange = new KeyRange(new StartStopKey(inclusive, list), new StartStopKey(keyRange.StopKey.Inclusive, list2));
				this.leafKeyRange = new KeyRange(new StartStopKey(keyRange.StartKey.Inclusive, list3), new StartStopKey(true, values));
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x00016938 File Offset: 0x00014B38
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select");
				bool flag = (formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails;
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				sb.Append(" from Header ");
				sb.Append(this.HeaderTable.Name);
				sb.Append(" and Leaf ");
				sb.Append(this.LeafTable.Name);
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  categoryCount:[");
				sb.Append(this.CategoryCount);
				sb.Append("]");
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  levelsInitiallyExpanded:[");
				sb.Append(this.CollapseState.LevelsInitiallyExpanded);
				sb.Append("]");
				if (base.ColumnsToFetch != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  fetch:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, base.ColumnsToFetch, null, formatOptions);
					sb.Append("]");
				}
				if (base.Criteria != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  where:[");
					base.Criteria.AppendToString(sb, formatOptions);
					sb.Append("]");
				}
				if (base.SkipTo != 0)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  skipTo:[");
					sb.Append(base.SkipTo);
					sb.Append("]");
				}
				if (base.MaxRows != 0)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  maxRows:[");
					sb.Append(base.MaxRows);
					sb.Append("]");
				}
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  headerLogicalSortOrder(" + this.HeaderLogicalSortOrder.Count + "):[");
				this.HeaderLogicalSortOrder.AppendToStringBuilder(sb, formatOptions);
				sb.Append("]");
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  leafLogicalSortOrder(" + this.LeafLogicalSortOrder.Count + "):[");
				this.LeafLogicalSortOrder.AppendToStringBuilder(sb, formatOptions);
				sb.Append("]");
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  headerKeyPrefix(" + this.HeaderKeyPrefix.Count + "):[");
				StartStopKey.AppendKeyValuesToStringBuilder(sb, formatOptions, this.HeaderKeyPrefix);
				sb.Append("]");
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  leafKeyPrefix(" + this.LeafKeyPrefix.Count + "):[");
				StartStopKey.AppendKeyValuesToStringBuilder(sb, formatOptions, this.LeafKeyPrefix);
				sb.Append("]");
				if (!this.HeaderKeyRange.IsAllRows)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  headerKeyRange:[");
					this.HeaderKeyRange.AppendToStringBuilder(sb, formatOptions);
					sb.Append("]");
				}
				if (!this.LeafKeyRange.IsAllRows)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  leafKeyRange:[");
					this.LeafKeyRange.AppendToStringBuilder(sb, formatOptions);
					sb.Append("]");
				}
				base.Indent(sb, multiLine, nestingLevel, false);
				sb.Append("  atLeastOneExternalColumn:[");
				sb.Append(this.AtLeastOneExternalColumn);
				sb.Append("]");
				if (flag || this.Backwards)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  backwards:[");
					sb.Append(this.Backwards);
					sb.Append("]");
				}
				if (flag)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x0400015F RID: 351
			private readonly CategorizedTableParams categorizedTableParams;

			// Token: 0x04000160 RID: 352
			private readonly CategorizedTableCollapseState collapseState;

			// Token: 0x04000161 RID: 353
			private readonly KeyRange keyRange;

			// Token: 0x04000162 RID: 354
			private readonly bool isInclusiveStartKey;

			// Token: 0x04000163 RID: 355
			private readonly bool backwards;

			// Token: 0x04000164 RID: 356
			private IList<Column> headerColumnsToFetch;

			// Token: 0x04000165 RID: 357
			private IList<Column> leafColumnsToFetch;

			// Token: 0x04000166 RID: 358
			private IList<Column> leafColumnsToFetchWithoutJoin;

			// Token: 0x04000167 RID: 359
			private bool atLeastOneExternalColumn;

			// Token: 0x04000168 RID: 360
			private Dictionary<Column, Column> headerRenameDictionary;

			// Token: 0x04000169 RID: 361
			private Dictionary<Column, Column> leafIndexRenameDictionary;

			// Token: 0x0400016A RID: 362
			private Dictionary<Column, Column> leafMessageRenameDictionary;

			// Token: 0x0400016B RID: 363
			private KeyRange headerKeyRange;

			// Token: 0x0400016C RID: 364
			private KeyRange leafKeyRange;
		}

		// Token: 0x0200006C RID: 108
		private class CategorizedTableReader : Reader
		{
			// Token: 0x060004D2 RID: 1234 RVA: 0x00016ECF File Offset: 0x000150CF
			public CategorizedTableReader(Connection connection, CategorizedTableOperator categorizedTableOperator, bool disposeOperator) : base(connection, categorizedTableOperator, disposeOperator)
			{
				this.isReaderOpen = true;
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016EE1 File Offset: 0x000150E1
			private CategorizedTableOperator CategorizedTableOperator
			{
				get
				{
					return (CategorizedTableOperator)base.SimpleQueryOperator;
				}
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x00016EEE File Offset: 0x000150EE
			private Reader CurrentReader(Column columnToRetrieve)
			{
				return this.CategorizedTableOperator.CurrentReader(columnToRetrieve);
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00016EFC File Offset: 0x000150FC
			public override bool IsClosed
			{
				get
				{
					return !this.isReaderOpen;
				}
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00016F08 File Offset: 0x00015108
			public override bool Read(out int rowsSkipped)
			{
				rowsSkipped = 0;
				bool result;
				if (this.movedToFirst)
				{
					result = this.CategorizedTableOperator.MoveNext();
				}
				else
				{
					result = this.CategorizedTableOperator.MoveFirst(out rowsSkipped);
					this.movedToFirst = true;
				}
				return result;
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x00016F45 File Offset: 0x00015145
			public override bool? GetNullableBoolean(Column column)
			{
				return this.CurrentReader(column).GetNullableBoolean(column);
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00016F54 File Offset: 0x00015154
			public override bool GetBoolean(Column column)
			{
				return this.CurrentReader(column).GetBoolean(column);
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00016F63 File Offset: 0x00015163
			public override long? GetNullableInt64(Column column)
			{
				return this.CurrentReader(column).GetNullableInt64(column);
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x00016F72 File Offset: 0x00015172
			public override long GetInt64(Column column)
			{
				return this.CurrentReader(column).GetInt64(column);
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x00016F81 File Offset: 0x00015181
			public override int? GetNullableInt32(Column column)
			{
				return this.CurrentReader(column).GetNullableInt32(column);
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x00016F90 File Offset: 0x00015190
			public override int GetInt32(Column column)
			{
				return this.CurrentReader(column).GetInt32(column);
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00016F9F File Offset: 0x0001519F
			public override short? GetNullableInt16(Column column)
			{
				return this.CurrentReader(column).GetNullableInt16(column);
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00016FAE File Offset: 0x000151AE
			public override short GetInt16(Column column)
			{
				return this.CurrentReader(column).GetInt16(column);
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00016FBD File Offset: 0x000151BD
			public override Guid? GetNullableGuid(Column column)
			{
				return this.CurrentReader(column).GetNullableGuid(column);
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x00016FCC File Offset: 0x000151CC
			public override Guid GetGuid(Column column)
			{
				return this.CurrentReader(column).GetGuid(column);
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00016FDB File Offset: 0x000151DB
			public override DateTime? GetNullableDateTime(Column column)
			{
				return this.CurrentReader(column).GetNullableDateTime(column);
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x00016FEA File Offset: 0x000151EA
			public override DateTime GetDateTime(Column column)
			{
				return this.CurrentReader(column).GetDateTime(column);
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00016FF9 File Offset: 0x000151F9
			public override byte[] GetBinary(Column column)
			{
				return this.CurrentReader(column).GetBinary(column);
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00017008 File Offset: 0x00015208
			public override string GetString(Column column)
			{
				return this.CurrentReader(column).GetString(column);
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00017017 File Offset: 0x00015217
			public override object GetValue(Column column)
			{
				return this.CurrentReader(column).GetValue(column);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x00017026 File Offset: 0x00015226
			public override long GetChars(Column column, long dataIndex, char[] outBuffer, int bufferIndex, int length)
			{
				return this.CurrentReader(column).GetChars(column, dataIndex, outBuffer, bufferIndex, length);
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x0001703B File Offset: 0x0001523B
			public override long GetBytes(Column column, long dataIndex, byte[] outBuffer, int bufferIndex, int length)
			{
				return this.CurrentReader(column).GetBytes(column, dataIndex, outBuffer, bufferIndex, length);
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00017050 File Offset: 0x00015250
			public override void Close()
			{
				this.isReaderOpen = false;
				base.Close();
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0001705F File Offset: 0x0001525F
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<CategorizedTableOperator.CategorizedTableReader>(this);
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x00017067 File Offset: 0x00015267
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose)
				{
					this.Close();
					if (base.DisposeQueryOperator && base.SimpleQueryOperator != null)
					{
						base.SimpleQueryOperator.Dispose();
					}
				}
			}

			// Token: 0x04000171 RID: 369
			private bool movedToFirst;

			// Token: 0x04000172 RID: 370
			private bool isReaderOpen;
		}

		// Token: 0x0200006D RID: 109
		private class HeaderOrLeafReader : ITWIR
		{
			// Token: 0x060004EB RID: 1259 RVA: 0x0001708D File Offset: 0x0001528D
			public HeaderOrLeafReader(Reader reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001709C File Offset: 0x0001529C
			public Reader InternalReader
			{
				get
				{
					return this.reader;
				}
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x000170A4 File Offset: 0x000152A4
			public void CloseReader()
			{
				this.reader.Dispose();
				this.reader = null;
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x000170B8 File Offset: 0x000152B8
			int ITWIR.GetColumnSize(Column column)
			{
				return ((IColumn)column).GetSize(this);
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x000170CE File Offset: 0x000152CE
			object ITWIR.GetColumnValue(Column column)
			{
				return this.reader.GetValue(column);
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x000170DC File Offset: 0x000152DC
			int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
			{
				object value = this.reader.GetValue(column);
				return SizeOfColumn.GetColumnSize(column, value).GetValueOrDefault();
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x00017107 File Offset: 0x00015307
			object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
			{
				return this.reader.GetValue(column);
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x00017118 File Offset: 0x00015318
			int ITWIR.GetPropertyColumnSize(PropertyColumn column)
			{
				object value = this.reader.GetValue(column);
				return SizeOfColumn.GetColumnSize(column, value).GetValueOrDefault();
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x00017143 File Offset: 0x00015343
			object ITWIR.GetPropertyColumnValue(PropertyColumn column)
			{
				return this.reader.GetValue(column);
			}

			// Token: 0x04000173 RID: 371
			private Reader reader;
		}
	}
}
