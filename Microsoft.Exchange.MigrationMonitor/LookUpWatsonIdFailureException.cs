using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LookUpWatsonIdFailureException : LocalizedException
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00009562 File Offset: 0x00007762
		public LookUpWatsonIdFailureException() : base(MigrationMonitorStrings.ErrorLookUpWatsonId)
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000956F File Offset: 0x0000776F
		public LookUpWatsonIdFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorLookUpWatsonId, innerException)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000957D File Offset: 0x0000777D
		protected LookUpWatsonIdFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009587 File Offset: 0x00007787
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
