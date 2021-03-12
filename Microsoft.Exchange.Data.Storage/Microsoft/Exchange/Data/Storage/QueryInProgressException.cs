using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000769 RID: 1897
	[Serializable]
	public class QueryInProgressException : StorageTransientException
	{
		// Token: 0x06004897 RID: 18583 RVA: 0x001313EF File Offset: 0x0012F5EF
		public QueryInProgressException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x001313F9 File Offset: 0x0012F5F9
		protected QueryInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
