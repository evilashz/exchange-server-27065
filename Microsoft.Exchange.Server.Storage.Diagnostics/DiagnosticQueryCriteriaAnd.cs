using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200000C RID: 12
	public class DiagnosticQueryCriteriaAnd : DiagnosticQueryCriteria, IEquatable<DiagnosticQueryCriteriaAnd>
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004632 File Offset: 0x00002832
		private DiagnosticQueryCriteriaAnd(DiagnosticQueryCriteria[] nestedCriteria)
		{
			this.nestedCriteria = nestedCriteria;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004641 File Offset: 0x00002841
		public DiagnosticQueryCriteria[] NestedCriteria
		{
			get
			{
				return this.nestedCriteria;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004649 File Offset: 0x00002849
		public static DiagnosticQueryCriteriaAnd Create(params DiagnosticQueryCriteria[] nestedCriteria)
		{
			return new DiagnosticQueryCriteriaAnd(nestedCriteria);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004654 File Offset: 0x00002854
		public override string ToString()
		{
			if (this.description == null)
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.nestedCriteria)
				{
					stringBuilder.AppendFormat("{0}{1}", (stringBuilder.Length > 0) ? ", " : string.Empty, diagnosticQueryCriteria.ToString());
				}
				this.description = string.Format("and ({0})", stringBuilder.ToString());
			}
			return this.description;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000046D0 File Offset: 0x000028D0
		public override int GetHashCode()
		{
			if (base.HashCode == null)
			{
				int num = 0;
				foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.nestedCriteria)
				{
					num ^= diagnosticQueryCriteria.GetHashCode();
				}
				base.HashCode = new int?(num);
			}
			return base.HashCode.Value;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004734 File Offset: 0x00002934
		public override bool Equals(object obj)
		{
			DiagnosticQueryCriteriaAnd diagnosticQueryCriteriaAnd = obj as DiagnosticQueryCriteriaAnd;
			return diagnosticQueryCriteriaAnd != null && this.Equals(diagnosticQueryCriteriaAnd);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004754 File Offset: 0x00002954
		public bool Equals(DiagnosticQueryCriteriaAnd and)
		{
			if (and.NestedCriteria.Length == this.NestedCriteria.Length)
			{
				foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.NestedCriteria)
				{
					bool flag = false;
					foreach (DiagnosticQueryCriteria obj in and.NestedCriteria)
					{
						if (diagnosticQueryCriteria.Equals(obj))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000047D0 File Offset: 0x000029D0
		public override DiagnosticQueryCriteria Reduce()
		{
			List<DiagnosticQueryCriteria> list = new List<DiagnosticQueryCriteria>(this.nestedCriteria.Length);
			foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.nestedCriteria)
			{
				DiagnosticQueryCriteria diagnosticQueryCriteria2 = diagnosticQueryCriteria.Reduce();
				DiagnosticQueryCriteriaAnd diagnosticQueryCriteriaAnd = diagnosticQueryCriteria2 as DiagnosticQueryCriteriaAnd;
				if (diagnosticQueryCriteriaAnd != null)
				{
					list.AddRange(diagnosticQueryCriteriaAnd.nestedCriteria);
				}
				else
				{
					list.Add(diagnosticQueryCriteria2);
				}
			}
			return new DiagnosticQueryCriteriaAnd(list.ToArray());
		}

		// Token: 0x0400007F RID: 127
		private readonly DiagnosticQueryCriteria[] nestedCriteria;

		// Token: 0x04000080 RID: 128
		private string description;
	}
}
