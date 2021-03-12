using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000FE RID: 254
	internal class NamedPropsDataContext : DataContext
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x000127EE File Offset: 0x000109EE
		public NamedPropsDataContext(NamedPropData[] npda)
		{
			this.npda = npda;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000127FD File Offset: 0x000109FD
		public override string ToString()
		{
			return string.Format("NamedProps: {0}", CommonUtils.ConcatEntries<NamedPropData>(this.npda, null));
		}

		// Token: 0x04000564 RID: 1380
		private NamedPropData[] npda;
	}
}
