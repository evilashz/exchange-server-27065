using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BC5 RID: 3013
	public class BareLinefeedException : LocalizedException
	{
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x000AD2FC File Offset: 0x000AB4FC
		public long Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000AD304 File Offset: 0x000AB504
		public BareLinefeedException() : this(-1)
		{
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000AD30D File Offset: 0x000AB50D
		public BareLinefeedException(int position) : base(NetException.DataContainsBareLinefeeds)
		{
			this.position = (long)position;
		}

		// Token: 0x04003838 RID: 14392
		private long position;
	}
}
