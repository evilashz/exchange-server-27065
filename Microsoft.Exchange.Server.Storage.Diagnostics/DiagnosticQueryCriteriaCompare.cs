using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200000D RID: 13
	public class DiagnosticQueryCriteriaCompare : DiagnosticQueryCriteria, IEquatable<DiagnosticQueryCriteriaCompare>
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000483D File Offset: 0x00002A3D
		private DiagnosticQueryCriteriaCompare(string columnName, DiagnosticQueryOperator queryOperator, string value)
		{
			this.columnName = columnName;
			this.queryOperator = queryOperator;
			this.value = value;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000485A File Offset: 0x00002A5A
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004862 File Offset: 0x00002A62
		public DiagnosticQueryOperator QueryOperator
		{
			get
			{
				return this.queryOperator;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000486A File Offset: 0x00002A6A
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004872 File Offset: 0x00002A72
		public static DiagnosticQueryCriteriaCompare Create(string columnName, DiagnosticQueryOperator queryOperator, string value)
		{
			return new DiagnosticQueryCriteriaCompare(columnName, queryOperator, value);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000487C File Offset: 0x00002A7C
		public override string ToString()
		{
			double num;
			string format = (double.TryParse(this.Value, out num) || this.Value == null) ? "{0} {1} {2}" : "{0} {1} \"{2}\"";
			return string.Format(format, this.ColumnName, this.QueryOperator, this.Value ?? "NULL");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000048D8 File Offset: 0x00002AD8
		public override int GetHashCode()
		{
			if (base.HashCode == null)
			{
				if (this.Value != null)
				{
					base.HashCode = new int?(this.ColumnName.GetHashCode() ^ this.QueryOperator.GetHashCode() ^ this.Value.GetHashCode());
				}
				else
				{
					base.HashCode = new int?(this.ColumnName.GetHashCode() ^ this.QueryOperator.GetHashCode());
				}
			}
			return base.HashCode.Value;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004968 File Offset: 0x00002B68
		public override bool Equals(object obj)
		{
			DiagnosticQueryCriteriaCompare diagnosticQueryCriteriaCompare = obj as DiagnosticQueryCriteriaCompare;
			return diagnosticQueryCriteriaCompare != null && this.Equals(diagnosticQueryCriteriaCompare);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004988 File Offset: 0x00002B88
		public bool Equals(DiagnosticQueryCriteriaCompare compare)
		{
			return compare.ColumnName.Equals(this.ColumnName) && compare.QueryOperator.Equals(this.QueryOperator) && ((compare.Value == null && this.Value == null) || (compare.Value != null && this.Value != null && compare.Value.Equals(this.Value)));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000049FC File Offset: 0x00002BFC
		public override DiagnosticQueryCriteria Reduce()
		{
			return this;
		}

		// Token: 0x04000081 RID: 129
		private readonly string columnName;

		// Token: 0x04000082 RID: 130
		private readonly DiagnosticQueryOperator queryOperator;

		// Token: 0x04000083 RID: 131
		private readonly string value;
	}
}
