using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	internal class MailboxServerLocatorException : DatabaseNotFoundException
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x00021931 File Offset: 0x0001FB31
		public MailboxServerLocatorException(string databaseId) : base(databaseId)
		{
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002193A File Offset: 0x0001FB3A
		public MailboxServerLocatorException(string databaseId, Exception innerException) : base(databaseId, innerException)
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00021944 File Offset: 0x0001FB44
		protected MailboxServerLocatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
