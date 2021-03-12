using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LookUpStringIdFailureException : LocalizedException
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x000091E0 File Offset: 0x000073E0
		public LookUpStringIdFailureException() : base(MigrationMonitorStrings.ErrorLookUpStringId)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000091ED File Offset: 0x000073ED
		public LookUpStringIdFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorLookUpStringId, innerException)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000091FB File Offset: 0x000073FB
		protected LookUpStringIdFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009205 File Offset: 0x00007405
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
