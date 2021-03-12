using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000052 RID: 82
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3TransientInUseAuthErrorException : LocalizedException
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00004721 File Offset: 0x00002921
		public Pop3TransientInUseAuthErrorException() : base(CXStrings.Pop3TransientInUseAuthErrorMsg)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000472E File Offset: 0x0000292E
		public Pop3TransientInUseAuthErrorException(Exception innerException) : base(CXStrings.Pop3TransientInUseAuthErrorMsg, innerException)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000473C File Offset: 0x0000293C
		protected Pop3TransientInUseAuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004746 File Offset: 0x00002946
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
