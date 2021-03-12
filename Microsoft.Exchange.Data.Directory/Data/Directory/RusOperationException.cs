using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A74 RID: 2676
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RusOperationException : ADOperationException
	{
		// Token: 0x06007F2D RID: 32557 RVA: 0x001A3EA1 File Offset: 0x001A20A1
		public RusOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x001A3EAA File Offset: 0x001A20AA
		public RusOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x001A3EB4 File Offset: 0x001A20B4
		protected RusOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x001A3EBE File Offset: 0x001A20BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
