using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A8C RID: 2700
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADInvalidServiceCredentialException : ADTransientException
	{
		// Token: 0x06007F8D RID: 32653 RVA: 0x001A4261 File Offset: 0x001A2461
		public ADInvalidServiceCredentialException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x001A426A File Offset: 0x001A246A
		public ADInvalidServiceCredentialException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x001A4274 File Offset: 0x001A2474
		protected ADInvalidServiceCredentialException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x001A427E File Offset: 0x001A247E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
