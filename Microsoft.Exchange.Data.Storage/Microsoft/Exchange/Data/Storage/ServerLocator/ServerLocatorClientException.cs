using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.ServerLocator
{
	// Token: 0x02000D40 RID: 3392
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerLocatorClientException : LocalizedException
	{
		// Token: 0x0600759E RID: 30110 RVA: 0x00208B3C File Offset: 0x00206D3C
		public ServerLocatorClientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600759F RID: 30111 RVA: 0x00208B45 File Offset: 0x00206D45
		public ServerLocatorClientException(LocalizedString message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x00208B4F File Offset: 0x00206D4F
		protected ServerLocatorClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
