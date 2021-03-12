using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A1 RID: 417
	internal class RunGuidEventArgs : HandledEventArgs
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0002BA14 File Offset: 0x00029C14
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002BA1C File Offset: 0x00029C1C
		public RunGuidEventArgs(Guid guid)
		{
			this.guid = guid;
		}

		// Token: 0x04000322 RID: 802
		private Guid guid;
	}
}
