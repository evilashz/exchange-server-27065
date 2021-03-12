using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000044 RID: 68
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3NonCompliantServerException : LocalizedException
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00005EFF File Offset: 0x000040FF
		public Pop3NonCompliantServerException() : base(Strings.Pop3NonCompliantServerException)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005F0C File Offset: 0x0000410C
		public Pop3NonCompliantServerException(Exception innerException) : base(Strings.Pop3NonCompliantServerException, innerException)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005F1A File Offset: 0x0000411A
		protected Pop3NonCompliantServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005F24 File Offset: 0x00004124
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
