using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A2 RID: 418
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RpcUMServerNotFoundException : UMServerNotFoundException
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00034F5A File Offset: 0x0003315A
		public RpcUMServerNotFoundException() : base(Strings.RpcUMServerNotFoundException)
		{
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00034F67 File Offset: 0x00033167
		public RpcUMServerNotFoundException(Exception innerException) : base(Strings.RpcUMServerNotFoundException, innerException)
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00034F75 File Offset: 0x00033175
		protected RpcUMServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00034F7F File Offset: 0x0003317F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
