using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A4 RID: 420
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPGatewayNotFoundException : LocalizedException
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x00034FB8 File Offset: 0x000331B8
		public IPGatewayNotFoundException() : base(Strings.IPGatewayNotFoundException)
		{
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00034FC5 File Offset: 0x000331C5
		public IPGatewayNotFoundException(Exception innerException) : base(Strings.IPGatewayNotFoundException, innerException)
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00034FD3 File Offset: 0x000331D3
		protected IPGatewayNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00034FDD File Offset: 0x000331DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
