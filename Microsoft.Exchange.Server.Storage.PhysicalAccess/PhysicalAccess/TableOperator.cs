using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000058 RID: 88
	public abstract class TableOperator : SimpleQueryOperator
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00013120 File Offset: 0x00011320
		protected TableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation) : this(connectionProvider, new TableOperator.TableOperatorDefinition(culture, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, opportunedPreread, frequentOperation))
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00013151 File Offset: 0x00011351
		protected TableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00013166 File Offset: 0x00011366
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0001316E File Offset: 0x0001136E
		public int PrereadChunkSize
		{
			get
			{
				return this.prereadChunkSize;
			}
			set
			{
				this.prereadChunkSize = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00013177 File Offset: 0x00011377
		internal IList<KeyRange> KeyRanges
		{
			get
			{
				return this.OperatorDefinition.KeyRanges;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00013184 File Offset: 0x00011384
		internal Index Index
		{
			get
			{
				return this.OperatorDefinition.Index;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00013191 File Offset: 0x00011391
		public IList<Column> LongValueColumnsToPreread
		{
			get
			{
				return this.OperatorDefinition.LongValueColumnsToPreread;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001319E File Offset: 0x0001139E
		public bool OpportunedPreread
		{
			get
			{
				return this.OperatorDefinition.OpportunedPreread;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000131AB File Offset: 0x000113AB
		public new TableOperator.TableOperatorDefinition OperatorDefinition
		{
			get
			{
				return (TableOperator.TableOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000131B8 File Offset: 0x000113B8
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000131C1 File Offset: 0x000113C1
		public override void RemoveChildren()
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000131C4 File Offset: 0x000113C4
		public override bool OperatorUsesTablePartition(Table table, IList<object> partitionKeyPrefix)
		{
			if (base.Table != table || this.KeyRanges.Count == 0)
			{
				return false;
			}
			int numberOfPartioningColumns = base.Table.SpecialCols.NumberOfPartioningColumns;
			int num = numberOfPartioningColumns;
			StartStopKey startKey = this.KeyRanges[0].StartKey;
			return num == StartStopKey.CommonKeyPrefix(startKey.Values, partitionKeyPrefix, CultureHelper.DefaultCultureInfo.CompareInfo);
		}

		// Token: 0x04000122 RID: 290
		protected const int DefaultPrereadChunkSize = 150;

		// Token: 0x04000123 RID: 291
		private int prereadChunkSize = 150;

		// Token: 0x02000059 RID: 89
		public class TableOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x060003DA RID: 986 RVA: 0x0001322C File Offset: 0x0001142C
			public TableOperatorDefinition(CultureInfo culture, Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation) : base(culture, table, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, frequentOperation)
			{
				this.index = index;
				this.keyRanges = keyRanges;
				this.backwards = backwards;
				this.opportunedPreread = opportunedPreread;
				this.keyRanges = KeyRange.Normalize(this.keyRanges, index.SortOrder, base.CompareInfo, backwards);
				this.longValueColumnsToPreread = longValueColumnsToPreread;
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060003DB RID: 987 RVA: 0x00013293 File Offset: 0x00011493
			internal override string OperatorName
			{
				get
				{
					return "TABLE";
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0001329A File Offset: 0x0001149A
			public IList<KeyRange> KeyRanges
			{
				get
				{
					return this.keyRanges;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x060003DD RID: 989 RVA: 0x000132A2 File Offset: 0x000114A2
			public Index Index
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060003DE RID: 990 RVA: 0x000132AA File Offset: 0x000114AA
			public bool OpportunedPreread
			{
				get
				{
					return this.opportunedPreread;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x060003DF RID: 991 RVA: 0x000132B2 File Offset: 0x000114B2
			public override bool Backwards
			{
				get
				{
					return this.backwards;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x000132BA File Offset: 0x000114BA
			public override SortOrder SortOrder
			{
				get
				{
					return this.Index.SortOrder;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x000132C7 File Offset: 0x000114C7
			public IList<Column> LongValueColumnsToPreread
			{
				get
				{
					return this.longValueColumnsToPreread;
				}
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x000132CF File Offset: 0x000114CF
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateTableOperator(connectionProvider, this);
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x000132D8 File Offset: 0x000114D8
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x000132E4 File Offset: 0x000114E4
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (base.Table != null)
				{
					sb.Append(" from ");
					sb.Append(base.Table.Name);
				}
				if (base.ColumnsToFetch != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  fetch:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, base.ColumnsToFetch, null, formatOptions);
					sb.Append("]");
				}
				if (this.LongValueColumnsToPreread != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  lvpreread:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, this.LongValueColumnsToPreread, null, formatOptions);
					sb.Append("]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.Index != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  index:[");
					sb.Append(this.Index.Name);
					sb.Append("]");
				}
				if (this.KeyRanges.Count != 1 || !this.KeyRanges[0].IsAllRows)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  KeyRanges:[");
					if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.KeyRanges.Count <= 4)
					{
						for (int i = 0; i < this.KeyRanges.Count; i++)
						{
							if (i != 0)
							{
								sb.Append(" ,");
							}
							this.KeyRanges[i].AppendToStringBuilder(sb, formatOptions);
						}
					}
					else
					{
						if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None)
						{
							sb.Append(this.KeyRanges.Count);
						}
						else
						{
							sb.Append("multiple");
						}
						sb.Append(" ranges");
					}
					sb.Append("]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.Backwards)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  backwards:[");
					sb.Append(this.Backwards);
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
					if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None || base.MaxRows == 1)
					{
						sb.Append(base.MaxRows);
					}
					else
					{
						sb.Append("X");
					}
					sb.Append("]");
				}
				if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  frequentOp:[");
					sb.Append(base.FrequentOperation);
					sb.Append("]");
				}
			}

			// Token: 0x060003E5 RID: 997 RVA: 0x000135E0 File Offset: 0x000117E0
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				int num;
				int num2;
				base.CalculateHashValueForStatisticPurposes(out num, out num2);
				detail = (42856 ^ base.Table.GetHashCode() ^ this.keyRanges.Count ^ num2);
				simple = (42856 ^ base.Table.TableClass.GetHashCode() ^ num);
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x00013638 File Offset: 0x00011838
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				TableOperator.TableOperatorDefinition tableOperatorDefinition = other as TableOperator.TableOperatorDefinition;
				return tableOperatorDefinition != null && base.Table.Equals(tableOperatorDefinition.Table) && this.keyRanges.Count == tableOperatorDefinition.keyRanges.Count && base.IsEqualsForStatisticPurposes(other);
			}

			// Token: 0x04000124 RID: 292
			private readonly IList<KeyRange> keyRanges;

			// Token: 0x04000125 RID: 293
			private readonly Index index;

			// Token: 0x04000126 RID: 294
			private readonly bool backwards;

			// Token: 0x04000127 RID: 295
			private readonly bool opportunedPreread;

			// Token: 0x04000128 RID: 296
			private IList<Column> longValueColumnsToPreread;
		}
	}
}
