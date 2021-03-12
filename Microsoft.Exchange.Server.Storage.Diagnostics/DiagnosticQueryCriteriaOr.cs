using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200000F RID: 15
	public class DiagnosticQueryCriteriaOr : DiagnosticQueryCriteria, IEquatable<DiagnosticQueryCriteriaOr>
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private DiagnosticQueryCriteriaOr(DiagnosticQueryCriteria[] nestedCriteria)
		{
			this.nestedCriteria = nestedCriteria;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004AEF File Offset: 0x00002CEF
		public DiagnosticQueryCriteria[] NestedCriteria
		{
			get
			{
				return this.nestedCriteria;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004AF7 File Offset: 0x00002CF7
		public static DiagnosticQueryCriteriaOr Create(params DiagnosticQueryCriteria[] nestedCriteria)
		{
			return new DiagnosticQueryCriteriaOr(nestedCriteria);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004B00 File Offset: 0x00002D00
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

		// Token: 0x0600008A RID: 138 RVA: 0x00004B64 File Offset: 0x00002D64
		public override string ToString()
		{
			if (this.description == null)
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.nestedCriteria)
				{
					stringBuilder.AppendFormat("{0}{1}", (stringBuilder.Length > 0) ? ", " : string.Empty, diagnosticQueryCriteria.ToString());
					this.description = string.Format("or ({0})", stringBuilder.ToString());
				}
			}
			return this.description;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public override bool Equals(object obj)
		{
			DiagnosticQueryCriteriaOr diagnosticQueryCriteriaOr = obj as DiagnosticQueryCriteriaOr;
			return diagnosticQueryCriteriaOr != null && this.Equals(diagnosticQueryCriteriaOr);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004C00 File Offset: 0x00002E00
		public bool Equals(DiagnosticQueryCriteriaOr or)
		{
			if (or.NestedCriteria.Length == this.NestedCriteria.Length)
			{
				foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.NestedCriteria)
				{
					bool flag = false;
					foreach (DiagnosticQueryCriteria obj in or.NestedCriteria)
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

		// Token: 0x0600008D RID: 141 RVA: 0x00004C7C File Offset: 0x00002E7C
		public override DiagnosticQueryCriteria Reduce()
		{
			List<DiagnosticQueryCriteria> list = new List<DiagnosticQueryCriteria>(this.nestedCriteria.Length);
			foreach (DiagnosticQueryCriteria diagnosticQueryCriteria in this.nestedCriteria)
			{
				DiagnosticQueryCriteria diagnosticQueryCriteria2 = diagnosticQueryCriteria.Reduce();
				DiagnosticQueryCriteriaOr diagnosticQueryCriteriaOr = diagnosticQueryCriteria2 as DiagnosticQueryCriteriaOr;
				if (diagnosticQueryCriteriaOr != null)
				{
					list.AddRange(diagnosticQueryCriteriaOr.nestedCriteria);
				}
				else
				{
					list.Add(diagnosticQueryCriteria2);
				}
			}
			return new DiagnosticQueryCriteriaOr(list.ToArray());
		}

		// Token: 0x04000085 RID: 133
		private readonly DiagnosticQueryCriteria[] nestedCriteria;

		// Token: 0x04000086 RID: 134
		private string description;
	}
}
