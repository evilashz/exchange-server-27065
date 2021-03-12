using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class DatabaseInitializationException : ComponentFailedException
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000023DE File Offset: 0x000005DE
		public DatabaseInitializationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023E7 File Offset: 0x000005E7
		internal override void RethrowNewInstance()
		{
			throw new ComponentFailedTransientException(this);
		}
	}
}
