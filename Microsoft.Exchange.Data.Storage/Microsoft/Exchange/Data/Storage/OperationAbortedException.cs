using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200011A RID: 282
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OperationAbortedException : LocalizedException
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x0006A34E File Offset: 0x0006854E
		public OperationAbortedException() : base(ServerStrings.OperationAborted)
		{
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006A35B File Offset: 0x0006855B
		public OperationAbortedException(Exception innerException) : base(ServerStrings.OperationAborted, innerException)
		{
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0006A369 File Offset: 0x00068569
		protected OperationAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0006A373 File Offset: 0x00068573
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
