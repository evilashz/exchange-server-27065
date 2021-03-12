using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A0 RID: 416
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMServerNotFoundException : LocalizedException
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x00034EBB File Offset: 0x000330BB
		public UMServerNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00034EC4 File Offset: 0x000330C4
		public UMServerNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00034ECE File Offset: 0x000330CE
		protected UMServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00034ED8 File Offset: 0x000330D8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
