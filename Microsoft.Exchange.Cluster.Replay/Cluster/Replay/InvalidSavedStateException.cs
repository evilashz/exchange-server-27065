using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000399 RID: 921
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSavedStateException : TransientException
	{
		// Token: 0x06002747 RID: 10055 RVA: 0x000B5A62 File Offset: 0x000B3C62
		public InvalidSavedStateException() : base(ReplayStrings.InvalidSavedStateException)
		{
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000B5A6F File Offset: 0x000B3C6F
		public InvalidSavedStateException(Exception innerException) : base(ReplayStrings.InvalidSavedStateException, innerException)
		{
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000B5A7D File Offset: 0x000B3C7D
		protected InvalidSavedStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000B5A87 File Offset: 0x000B3C87
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
