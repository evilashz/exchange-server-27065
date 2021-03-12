using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EBC RID: 3772
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ValidationContext
	{
		// Token: 0x06008264 RID: 33380 RVA: 0x00239788 File Offset: 0x00237988
		internal ValidationContext(StoreSession session)
		{
			this.session = session;
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x06008265 RID: 33381 RVA: 0x00239797 File Offset: 0x00237997
		internal StoreSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x0400577B RID: 22395
		private readonly StoreSession session;
	}
}
