using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000060 RID: 96
	public abstract class PreReadOperator : DataAccessOperator
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00013E9E File Offset: 0x0001209E
		protected PreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation) : base(connectionProvider, new PreReadOperator.PreReadOperatorDefinition(culture, table, index, keyRanges, longValueColumns, frequentOperation))
		{
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00013EB6 File Offset: 0x000120B6
		internal IList<KeyRange> KeyRanges
		{
			get
			{
				return this.OperatorDefinition.KeyRanges;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00013EC3 File Offset: 0x000120C3
		internal Table Table
		{
			get
			{
				return this.OperatorDefinition.Table;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00013ED0 File Offset: 0x000120D0
		internal Index Index
		{
			get
			{
				return this.OperatorDefinition.Index;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00013EDD File Offset: 0x000120DD
		internal IList<Column> LongValueColumns
		{
			get
			{
				return this.OperatorDefinition.LongValueColumns;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00013EEA File Offset: 0x000120EA
		public PreReadOperator.PreReadOperatorDefinition OperatorDefinition
		{
			get
			{
				return (PreReadOperator.PreReadOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00013EF7 File Offset: 0x000120F7
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00013F00 File Offset: 0x00012100
		public override void RemoveChildren()
		{
		}

		// Token: 0x02000061 RID: 97
		public class PreReadOperatorDefinition : DataAccessOperator.DataAccessOperatorDefinition
		{
			// Token: 0x06000419 RID: 1049 RVA: 0x00013F02 File Offset: 0x00012102
			public PreReadOperatorDefinition(CultureInfo culture, Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation) : base(culture, frequentOperation)
			{
				this.table = table;
				this.index = index;
				this.keyRanges = keyRanges;
				this.longValueColumns = longValueColumns;
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x0600041A RID: 1050 RVA: 0x00013F2B File Offset: 0x0001212B
			internal override string OperatorName
			{
				get
				{
					return "PREREAD";
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x00013F32 File Offset: 0x00012132
			internal IList<KeyRange> KeyRanges
			{
				get
				{
					return this.keyRanges;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x00013F3A File Offset: 0x0001213A
			internal IList<Column> LongValueColumns
			{
				get
				{
					return this.longValueColumns;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x00013F42 File Offset: 0x00012142
			internal Table Table
			{
				get
				{
					return this.table;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x00013F4A File Offset: 0x0001214A
			internal Index Index
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x00013F52 File Offset: 0x00012152
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x00013F5C File Offset: 0x0001215C
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				sb.Append("PreRead from ");
				sb.Append(this.Table.Name);
				if (this.KeyRanges.Count != 1 || !this.KeyRanges[0].IsAllRows)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  Index:[");
					sb.Append(this.index.Name);
					sb.Append("]");
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
					sb.Append("]  LongValueColumns:[");
					if (this.longValueColumns != null)
					{
						for (int j = 0; j < this.longValueColumns.Count; j++)
						{
							if (j != 0)
							{
								sb.Append(" ,");
							}
							this.longValueColumns[j].AppendToString(sb, formatOptions);
						}
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

			// Token: 0x06000421 RID: 1057 RVA: 0x0001410F File Offset: 0x0001230F
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				detail = (64616 ^ this.Table.GetHashCode() ^ this.Index.GetHashCode());
				simple = (64616 ^ this.Table.TableClass.GetHashCode());
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x00014150 File Offset: 0x00012350
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return true;
				}
				PreReadOperator.PreReadOperatorDefinition preReadOperatorDefinition = other as PreReadOperator.PreReadOperatorDefinition;
				return preReadOperatorDefinition != null && this.Table.Equals(preReadOperatorDefinition.Table) && this.Index.Equals(preReadOperatorDefinition.Index);
			}

			// Token: 0x04000130 RID: 304
			private readonly IList<KeyRange> keyRanges;

			// Token: 0x04000131 RID: 305
			private readonly Table table;

			// Token: 0x04000132 RID: 306
			private readonly Index index;

			// Token: 0x04000133 RID: 307
			private readonly IList<Column> longValueColumns;
		}
	}
}
