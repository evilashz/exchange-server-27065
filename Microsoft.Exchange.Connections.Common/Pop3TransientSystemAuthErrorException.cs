using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000054 RID: 84
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3TransientSystemAuthErrorException : LocalizedException
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000477F File Offset: 0x0000297F
		public Pop3TransientSystemAuthErrorException() : base(CXStrings.Pop3TransientSystemAuthErrorMsg)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000478C File Offset: 0x0000298C
		public Pop3TransientSystemAuthErrorException(Exception innerException) : base(CXStrings.Pop3TransientSystemAuthErrorMsg, innerException)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000479A File Offset: 0x0000299A
		protected Pop3TransientSystemAuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000047A4 File Offset: 0x000029A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
