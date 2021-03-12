using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000046 RID: 70
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3TransientInUseAuthErrorException : LocalizedException
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00005FFD File Offset: 0x000041FD
		public Pop3TransientInUseAuthErrorException() : base(Strings.Pop3TransientInUseAuthErrorException)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000600A File Offset: 0x0000420A
		public Pop3TransientInUseAuthErrorException(Exception innerException) : base(Strings.Pop3TransientInUseAuthErrorException, innerException)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006018 File Offset: 0x00004218
		protected Pop3TransientInUseAuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006022 File Offset: 0x00004222
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
