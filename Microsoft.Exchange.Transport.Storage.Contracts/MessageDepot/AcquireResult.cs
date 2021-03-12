using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000002 RID: 2
	internal class AcquireResult
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AcquireResult(IMessageDepotItemWrapper itemWrapper, AcquireToken token)
		{
			if (itemWrapper == null)
			{
				throw new ArgumentNullException("itemWrapper");
			}
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this.itemWrapper = itemWrapper;
			this.token = token;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002108 File Offset: 0x00000308
		public IMessageDepotItemWrapper ItemWrapper
		{
			get
			{
				return this.itemWrapper;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002110 File Offset: 0x00000310
		public AcquireToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly IMessageDepotItemWrapper itemWrapper;

		// Token: 0x04000002 RID: 2
		private readonly AcquireToken token;
	}
}
