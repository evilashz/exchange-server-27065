using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200004F RID: 79
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3MirroredAccountNotPossibleException : LocalizedException
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000045F4 File Offset: 0x000027F4
		public Pop3MirroredAccountNotPossibleException() : base(CXStrings.Pop3MirroredAccountNotPossibleMsg)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004601 File Offset: 0x00002801
		public Pop3MirroredAccountNotPossibleException(Exception innerException) : base(CXStrings.Pop3MirroredAccountNotPossibleMsg, innerException)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000460F File Offset: 0x0000280F
		protected Pop3MirroredAccountNotPossibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004619 File Offset: 0x00002819
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
