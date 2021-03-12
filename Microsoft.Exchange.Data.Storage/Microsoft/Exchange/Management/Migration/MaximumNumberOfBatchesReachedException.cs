using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02000166 RID: 358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MaximumNumberOfBatchesReachedException : LocalizedException
	{
		// Token: 0x06001668 RID: 5736 RVA: 0x0006F658 File Offset: 0x0006D858
		public MaximumNumberOfBatchesReachedException() : base(Strings.MaximumNumberOfBatchesReached)
		{
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0006F665 File Offset: 0x0006D865
		public MaximumNumberOfBatchesReachedException(Exception innerException) : base(Strings.MaximumNumberOfBatchesReached, innerException)
		{
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0006F673 File Offset: 0x0006D873
		protected MaximumNumberOfBatchesReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0006F67D File Offset: 0x0006D87D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
