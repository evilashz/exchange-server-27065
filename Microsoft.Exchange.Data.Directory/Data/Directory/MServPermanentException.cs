using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B14 RID: 2836
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MServPermanentException : DataSourceOperationException
	{
		// Token: 0x06008222 RID: 33314 RVA: 0x001A7D88 File Offset: 0x001A5F88
		public MServPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x001A7D91 File Offset: 0x001A5F91
		public MServPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x001A7D9B File Offset: 0x001A5F9B
		protected MServPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008225 RID: 33317 RVA: 0x001A7DA5 File Offset: 0x001A5FA5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
