using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A9 RID: 937
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TPRProviderNotListeningException : TransientException
	{
		// Token: 0x0600279E RID: 10142 RVA: 0x000B6478 File Offset: 0x000B4678
		public TPRProviderNotListeningException() : base(ReplayStrings.TPRProviderNotListening)
		{
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000B6485 File Offset: 0x000B4685
		public TPRProviderNotListeningException(Exception innerException) : base(ReplayStrings.TPRProviderNotListening, innerException)
		{
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000B6493 File Offset: 0x000B4693
		protected TPRProviderNotListeningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000B649D File Offset: 0x000B469D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
