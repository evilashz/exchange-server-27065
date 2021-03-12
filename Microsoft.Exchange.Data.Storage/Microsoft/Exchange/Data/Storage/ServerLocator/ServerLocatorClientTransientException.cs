using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.ServerLocator
{
	// Token: 0x02000D41 RID: 3393
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerLocatorClientTransientException : TransientException
	{
		// Token: 0x060075A1 RID: 30113 RVA: 0x00208B59 File Offset: 0x00206D59
		public ServerLocatorClientTransientException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x060075A2 RID: 30114 RVA: 0x00208B62 File Offset: 0x00206D62
		public ServerLocatorClientTransientException(LocalizedString localizedString, Exception inner) : base(localizedString, inner)
		{
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x00208B6C File Offset: 0x00206D6C
		protected ServerLocatorClientTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
