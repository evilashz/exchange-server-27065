using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200005C RID: 92
	public abstract class TableFunctionOperator : SimpleQueryOperator
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00013690 File Offset: 0x00011890
		protected TableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : this(connectionProvider, new TableFunctionOperator.TableFunctionOperatorDefinition(culture, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation))
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000136BD File Offset: 0x000118BD
		protected TableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000136C7 File Offset: 0x000118C7
		internal IList<KeyRange> KeyRanges
		{
			get
			{
				return this.OperatorDefinition.KeyRanges;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000136D4 File Offset: 0x000118D4
		public object[] Parameters
		{
			get
			{
				return this.OperatorDefinition.Parameters;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000136E1 File Offset: 0x000118E1
		private new TableFunctionOperator.TableFunctionOperatorDefinition OperatorDefinition
		{
			get
			{
				return (TableFunctionOperator.TableFunctionOperatorDefinition)base.OperatorDefinitionBase;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000136EE File Offset: 0x000118EE
		public virtual uint RowsReturned
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000136F1 File Offset: 0x000118F1
		public TableFunction TableFunction
		{
			get
			{
				return (TableFunction)base.Table;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000136FE File Offset: 0x000118FE
		public override void EnumerateDescendants(Action<DataAccessOperator> operatorAction)
		{
			operatorAction(this);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013707 File Offset: 0x00011907
		public override void RemoveChildren()
		{
		}

		// Token: 0x0200005D RID: 93
		public class TableFunctionOperatorDefinition : SimpleQueryOperator.SimpleQueryOperatorDefinition
		{
			// Token: 0x060003F3 RID: 1011 RVA: 0x0001370C File Offset: 0x0001190C
			public TableFunctionOperatorDefinition(CultureInfo culture, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : base(culture, tableFunction, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, frequentOperation)
			{
				this.parameters = parameters;
				this.keyRanges = KeyRange.Normalize(keyRanges, base.Table.PrimaryKeyIndex.SortOrder, (culture == null) ? null : culture.CompareInfo, backwards);
				this.backwards = backwards;
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00013767 File Offset: 0x00011967
			internal override string OperatorName
			{
				get
				{
					return "TBLFUNC";
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001376E File Offset: 0x0001196E
			public IList<KeyRange> KeyRanges
			{
				get
				{
					return this.keyRanges;
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00013776 File Offset: 0x00011976
			public object[] Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001377E File Offset: 0x0001197E
			public override SortOrder SortOrder
			{
				get
				{
					return base.Table.PrimaryKeyIndex.SortOrder;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013790 File Offset: 0x00011990
			public override bool Backwards
			{
				get
				{
					return this.backwards;
				}
			}

			// Token: 0x060003F9 RID: 1017 RVA: 0x00013798 File Offset: 0x00011998
			public override SimpleQueryOperator CreateOperator(IConnectionProvider connectionProvider)
			{
				return Factory.CreateTableFunctionOperator(connectionProvider, this);
			}

			// Token: 0x060003FA RID: 1018 RVA: 0x000137A1 File Offset: 0x000119A1
			public override void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction)
			{
				operatorDefinitionAction(this);
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x000137AC File Offset: 0x000119AC
			internal override void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
			{
				sb.Append("select function");
				bool multiLine = (formatOptions & StringFormatOptions.MultiLine) == StringFormatOptions.MultiLine;
				if (base.Table != null)
				{
					sb.Append(" ");
					sb.Append(base.Table.Name);
				}
				else
				{
					sb.Append(" <null>");
				}
				sb.Append("(");
				if (this.Parameters != null)
				{
					for (int i = 0; i < this.Parameters.Length; i++)
					{
						if (i != 0)
						{
							sb.Append(", ");
						}
						sb.Append("[");
						if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.SkipParametersData && this.Parameters[i] is IEnumerable)
						{
							sb.Append("X");
						}
						else if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || !(this.Parameters[i] is byte[]) || ((byte[])this.Parameters[i]).Length <= 32)
						{
							sb.AppendAsString(this.Parameters[i]);
						}
						else
						{
							sb.Append("<long_blob>");
						}
						sb.Append("]");
					}
				}
				sb.Append(")");
				if (base.ColumnsToFetch != null)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  fetch:[");
					DataAccessOperator.DataAccessOperatorDefinition.AppendColumnsSummaryToStringBuilder(sb, base.ColumnsToFetch, null, formatOptions);
					sb.Append("]");
				}
				if (this.KeyRanges.Count != 1 || !this.KeyRanges[0].IsAllRows)
				{
					base.Indent(sb, multiLine, nestingLevel, false);
					sb.Append("  KeyRanges:[");
					if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.KeyRanges.Count <= 4)
					{
						for (int j = 0; j < this.KeyRanges.Count; j++)
						{
							if (j != 0)
							{
								sb.Append(", ");
							}
							this.KeyRanges[j].AppendToStringBuilder(sb, formatOptions);
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

			// Token: 0x060003FC RID: 1020 RVA: 0x00013B07 File Offset: 0x00011D07
			internal override void CalculateHashValueForStatisticPurposes(out int simple, out int detail)
			{
				detail = (38760 ^ base.Table.GetHashCode() ^ this.keyRanges.Count);
				simple = (38760 ^ base.Table.TableClass.GetHashCode());
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x00013B48 File Offset: 0x00011D48
			internal override bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other)
			{
				TableFunctionOperator.TableFunctionOperatorDefinition tableFunctionOperatorDefinition = other as TableFunctionOperator.TableFunctionOperatorDefinition;
				return tableFunctionOperatorDefinition != null && base.Table.Equals(tableFunctionOperatorDefinition.Table) && this.keyRanges.Count == tableFunctionOperatorDefinition.keyRanges.Count;
			}

			// Token: 0x04000129 RID: 297
			private readonly IList<KeyRange> keyRanges;

			// Token: 0x0400012A RID: 298
			private readonly bool backwards;

			// Token: 0x0400012B RID: 299
			private readonly object[] parameters;
		}
	}
}
