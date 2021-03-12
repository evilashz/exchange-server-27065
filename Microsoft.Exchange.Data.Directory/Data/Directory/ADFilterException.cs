using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A71 RID: 2673
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADFilterException : ADOperationException
	{
		// Token: 0x06007F21 RID: 32545 RVA: 0x001A3E2C File Offset: 0x001A202C
		public ADFilterException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x001A3E35 File Offset: 0x001A2035
		public ADFilterException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F23 RID: 32547 RVA: 0x001A3E3F File Offset: 0x001A203F
		protected ADFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F24 RID: 32548 RVA: 0x001A3E49 File Offset: 0x001A2049
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
