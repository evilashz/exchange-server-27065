using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A65 RID: 2661
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ABInvalidCredentialException : ABOperationException
	{
		// Token: 0x06007EF1 RID: 32497 RVA: 0x001A3C58 File Offset: 0x001A1E58
		public ABInvalidCredentialException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x001A3C61 File Offset: 0x001A1E61
		public ABInvalidCredentialException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x001A3C6B File Offset: 0x001A1E6B
		protected ABInvalidCredentialException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x001A3C75 File Offset: 0x001A1E75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
