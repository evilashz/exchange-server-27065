using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000050 RID: 80
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3NonCompliantServerException : LocalizedException
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00004623 File Offset: 0x00002823
		public Pop3NonCompliantServerException() : base(CXStrings.Pop3NonCompliantServerMsg)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004630 File Offset: 0x00002830
		public Pop3NonCompliantServerException(Exception innerException) : base(CXStrings.Pop3NonCompliantServerMsg, innerException)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000463E File Offset: 0x0000283E
		protected Pop3NonCompliantServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004648 File Offset: 0x00002848
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
