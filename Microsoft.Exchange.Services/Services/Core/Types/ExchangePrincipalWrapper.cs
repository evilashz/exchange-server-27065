using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000769 RID: 1897
	internal sealed class ExchangePrincipalWrapper
	{
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000C8679 File Offset: 0x000C6879
		// (set) Token: 0x060038A2 RID: 14498 RVA: 0x000C8681 File Offset: 0x000C6881
		public ExchangePrincipal ExchangePrincipal { get; private set; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x000C868A File Offset: 0x000C688A
		// (set) Token: 0x060038A4 RID: 14500 RVA: 0x000C8692 File Offset: 0x000C6892
		public DateTime CreatedOn { get; private set; }

		// Token: 0x060038A5 RID: 14501 RVA: 0x000C869B File Offset: 0x000C689B
		internal ExchangePrincipalWrapper(ExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			this.ExchangePrincipal = exchangePrincipal;
			this.CreatedOn = DateTime.UtcNow;
		}
	}
}
