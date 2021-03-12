using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200005D RID: 93
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RemoteServerIsBackedOffException : TransientException
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00006B2B File Offset: 0x00004D2B
		public RemoteServerIsBackedOffException() : base(Strings.RemoteServerIsBackedOffException)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00006B38 File Offset: 0x00004D38
		public RemoteServerIsBackedOffException(Exception innerException) : base(Strings.RemoteServerIsBackedOffException, innerException)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006B46 File Offset: 0x00004D46
		protected RemoteServerIsBackedOffException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006B50 File Offset: 0x00004D50
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
