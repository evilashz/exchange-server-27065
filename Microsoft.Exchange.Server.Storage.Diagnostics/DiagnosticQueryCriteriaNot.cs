using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200000E RID: 14
	public class DiagnosticQueryCriteriaNot : DiagnosticQueryCriteria, IEquatable<DiagnosticQueryCriteriaNot>
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000049FF File Offset: 0x00002BFF
		private DiagnosticQueryCriteriaNot(DiagnosticQueryCriteria nestedCriterion)
		{
			this.nestedCriterion = nestedCriterion;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004A0E File Offset: 0x00002C0E
		public DiagnosticQueryCriteria NestedCriterion
		{
			get
			{
				return this.nestedCriterion;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004A16 File Offset: 0x00002C16
		public static DiagnosticQueryCriteriaNot Create(DiagnosticQueryCriteria nestedCriterion)
		{
			return new DiagnosticQueryCriteriaNot(nestedCriterion);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004A1E File Offset: 0x00002C1E
		public override string ToString()
		{
			return string.Format("not ({0})", this.NestedCriterion.ToString());
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A38 File Offset: 0x00002C38
		public override int GetHashCode()
		{
			if (base.HashCode == null)
			{
				base.HashCode = new int?(this.nestedCriterion.GetHashCode());
			}
			return base.HashCode.Value;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A7C File Offset: 0x00002C7C
		public override bool Equals(object obj)
		{
			DiagnosticQueryCriteriaNot diagnosticQueryCriteriaNot = obj as DiagnosticQueryCriteriaNot;
			return diagnosticQueryCriteriaNot != null && this.Equals(diagnosticQueryCriteriaNot);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004A9C File Offset: 0x00002C9C
		public bool Equals(DiagnosticQueryCriteriaNot not)
		{
			return not.NestedCriterion.Equals(this.NestedCriterion);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public override DiagnosticQueryCriteria Reduce()
		{
			DiagnosticQueryCriteria diagnosticQueryCriteria = this.nestedCriterion.Reduce();
			DiagnosticQueryCriteriaNot diagnosticQueryCriteriaNot = diagnosticQueryCriteria as DiagnosticQueryCriteriaNot;
			if (diagnosticQueryCriteriaNot != null)
			{
				return diagnosticQueryCriteriaNot.nestedCriterion;
			}
			return new DiagnosticQueryCriteriaNot(diagnosticQueryCriteria);
		}

		// Token: 0x04000084 RID: 132
		private readonly DiagnosticQueryCriteria nestedCriterion;
	}
}
