using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A7E RID: 2686
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveMSAUserNetIDException : DataSourceOperationException
	{
		// Token: 0x06007F55 RID: 32597 RVA: 0x001A4027 File Offset: 0x001A2227
		public CannotResolveMSAUserNetIDException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x001A4030 File Offset: 0x001A2230
		public CannotResolveMSAUserNetIDException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x001A403A File Offset: 0x001A223A
		protected CannotResolveMSAUserNetIDException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x001A4044 File Offset: 0x001A2244
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
