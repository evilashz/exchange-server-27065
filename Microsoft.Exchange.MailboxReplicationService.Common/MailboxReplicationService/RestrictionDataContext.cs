using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000FA RID: 250
	internal class RestrictionDataContext : DataContext
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x0001271D File Offset: 0x0001091D
		public RestrictionDataContext(RestrictionData restriction)
		{
			this.restriction = restriction;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001272C File Offset: 0x0001092C
		public override string ToString()
		{
			return string.Format("Restriction: {0}", (this.restriction != null) ? this.restriction.ToString() : "(null)");
		}

		// Token: 0x04000560 RID: 1376
		private RestrictionData restriction;
	}
}
