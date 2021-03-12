using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000FD RID: 253
	internal class PropTagsDataContext : DataContext
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x000127BC File Offset: 0x000109BC
		public PropTagsDataContext(PropTag[] ptags)
		{
			this.ptags = ptags;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000127CB File Offset: 0x000109CB
		public override string ToString()
		{
			return string.Format("PropTags: {0}", CommonUtils.ConcatEntries<PropTag>(this.ptags, new Func<PropTag, string>(TraceUtils.DumpPropTag)));
		}

		// Token: 0x04000563 RID: 1379
		private PropTag[] ptags;
	}
}
