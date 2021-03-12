using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A8B RID: 2699
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADInvalidCredentialException : ADOperationException
	{
		// Token: 0x06007F89 RID: 32649 RVA: 0x001A423A File Offset: 0x001A243A
		public ADInvalidCredentialException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x001A4243 File Offset: 0x001A2443
		public ADInvalidCredentialException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x001A424D File Offset: 0x001A244D
		protected ADInvalidCredentialException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x001A4257 File Offset: 0x001A2457
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
