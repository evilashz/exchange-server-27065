using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200004E RID: 78
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3LeaveOnServerNotPossibleException : LocalizedException
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000045C5 File Offset: 0x000027C5
		public Pop3LeaveOnServerNotPossibleException() : base(CXStrings.Pop3LeaveOnServerNotPossibleMsg)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000045D2 File Offset: 0x000027D2
		public Pop3LeaveOnServerNotPossibleException(Exception innerException) : base(CXStrings.Pop3LeaveOnServerNotPossibleMsg, innerException)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000045E0 File Offset: 0x000027E0
		protected Pop3LeaveOnServerNotPossibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000045EA File Offset: 0x000027EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
