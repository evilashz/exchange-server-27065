using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001002 RID: 4098
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotModifyPhotoBecauseMailboxIsInTransitException : LocalizedException
	{
		// Token: 0x0600AEC6 RID: 44742 RVA: 0x00293628 File Offset: 0x00291828
		public CannotModifyPhotoBecauseMailboxIsInTransitException() : base(Strings.CannotModifyPhotoBecauseMailboxIsInTransit)
		{
		}

		// Token: 0x0600AEC7 RID: 44743 RVA: 0x00293635 File Offset: 0x00291835
		public CannotModifyPhotoBecauseMailboxIsInTransitException(Exception innerException) : base(Strings.CannotModifyPhotoBecauseMailboxIsInTransit, innerException)
		{
		}

		// Token: 0x0600AEC8 RID: 44744 RVA: 0x00293643 File Offset: 0x00291843
		protected CannotModifyPhotoBecauseMailboxIsInTransitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEC9 RID: 44745 RVA: 0x0029364D File Offset: 0x0029184D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
