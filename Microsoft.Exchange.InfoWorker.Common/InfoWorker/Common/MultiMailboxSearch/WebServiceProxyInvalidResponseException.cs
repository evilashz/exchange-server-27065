using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001CE RID: 462
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class WebServiceProxyInvalidResponseException : MultiMailboxSearchException
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x000355D0 File Offset: 0x000337D0
		public WebServiceProxyInvalidResponseException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000355D9 File Offset: 0x000337D9
		public WebServiceProxyInvalidResponseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000355E3 File Offset: 0x000337E3
		protected WebServiceProxyInvalidResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
