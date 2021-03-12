using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000101 RID: 257
	internal class SimpleValueDataContext : DataContext
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x000128EA File Offset: 0x00010AEA
		public SimpleValueDataContext(string name, object value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00012900 File Offset: 0x00010B00
		public override string ToString()
		{
			return string.Format("{0}: {1}", this.name, (this.value == null) ? "(null)" : this.value.ToString());
		}

		// Token: 0x04000567 RID: 1383
		private string name;

		// Token: 0x04000568 RID: 1384
		private object value;
	}
}
