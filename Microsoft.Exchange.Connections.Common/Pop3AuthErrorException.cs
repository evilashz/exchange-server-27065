using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000049 RID: 73
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3AuthErrorException : LocalizedException
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000439D File Offset: 0x0000259D
		public Pop3AuthErrorException() : base(CXStrings.Pop3AuthErrorMsg)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000043AA File Offset: 0x000025AA
		public Pop3AuthErrorException(Exception innerException) : base(CXStrings.Pop3AuthErrorMsg, innerException)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000043B8 File Offset: 0x000025B8
		protected Pop3AuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000043C2 File Offset: 0x000025C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
