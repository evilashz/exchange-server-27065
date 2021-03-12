using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	internal class MissingRequestedDatabaseException : MailboxServerLocatorException
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0002194E File Offset: 0x0001FB4E
		public MissingRequestedDatabaseException(string databaseId) : base(databaseId)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00021957 File Offset: 0x0001FB57
		public MissingRequestedDatabaseException(string databaseId, Exception innerException) : base(databaseId, innerException)
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00021961 File Offset: 0x0001FB61
		protected MissingRequestedDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
