using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C4 RID: 1732
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ProxyConnectionException : ConnectionException
	{
		// Token: 0x0600457E RID: 17790 RVA: 0x00127BE3 File Offset: 0x00125DE3
		internal ProxyConnectionException(ObjectId objectId, string message, Exception innerException) : base(objectId, message, innerException)
		{
		}
	}
}
