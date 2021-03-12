using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000725 RID: 1829
	[Serializable]
	public class DatabaseLocationUnavailableException : StoragePermanentException
	{
		// Token: 0x060047BE RID: 18366 RVA: 0x001304C3 File Offset: 0x0012E6C3
		public DatabaseLocationUnavailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x001304CC File Offset: 0x0012E6CC
		public DatabaseLocationUnavailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x001304D6 File Offset: 0x0012E6D6
		protected DatabaseLocationUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
