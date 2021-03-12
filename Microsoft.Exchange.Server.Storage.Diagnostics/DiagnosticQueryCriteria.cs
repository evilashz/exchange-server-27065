using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200000B RID: 11
	public abstract class DiagnosticQueryCriteria
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004619 File Offset: 0x00002819
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00004621 File Offset: 0x00002821
		protected int? HashCode
		{
			get
			{
				return this.hashCode;
			}
			set
			{
				this.hashCode = value;
			}
		}

		// Token: 0x0600006A RID: 106
		public abstract DiagnosticQueryCriteria Reduce();

		// Token: 0x0400007E RID: 126
		private int? hashCode;
	}
}
