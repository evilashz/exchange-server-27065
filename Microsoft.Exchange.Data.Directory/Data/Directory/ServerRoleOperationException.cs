using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A73 RID: 2675
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerRoleOperationException : ADOperationException
	{
		// Token: 0x06007F29 RID: 32553 RVA: 0x001A3E7A File Offset: 0x001A207A
		public ServerRoleOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x001A3E83 File Offset: 0x001A2083
		public ServerRoleOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x001A3E8D File Offset: 0x001A208D
		protected ServerRoleOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x001A3E97 File Offset: 0x001A2097
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
