using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000712 RID: 1810
	[Serializable]
	public class AccountDisabledException : StoragePermanentException
	{
		// Token: 0x06004786 RID: 18310 RVA: 0x0013019A File Offset: 0x0012E39A
		public AccountDisabledException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x001301A3 File Offset: 0x0012E3A3
		public AccountDisabledException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x001301AD File Offset: 0x0012E3AD
		protected AccountDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
