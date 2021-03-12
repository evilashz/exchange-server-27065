using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200004C RID: 76
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3DisabledResponseException : LocalizedException
	{
		// Token: 0x06000174 RID: 372 RVA: 0x000044C8 File Offset: 0x000026C8
		public Pop3DisabledResponseException() : base(CXStrings.Pop3DisabledResponseMsg)
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000044D5 File Offset: 0x000026D5
		public Pop3DisabledResponseException(Exception innerException) : base(CXStrings.Pop3DisabledResponseMsg, innerException)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000044E3 File Offset: 0x000026E3
		protected Pop3DisabledResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000044ED File Offset: 0x000026ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
