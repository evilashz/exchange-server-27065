using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LookUpServerIdFailureException : LocalizedException
	{
		// Token: 0x060001DC RID: 476 RVA: 0x000091B1 File Offset: 0x000073B1
		public LookUpServerIdFailureException() : base(MigrationMonitorStrings.ErrorLookUpServerId)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000091BE File Offset: 0x000073BE
		public LookUpServerIdFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorLookUpServerId, innerException)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000091CC File Offset: 0x000073CC
		protected LookUpServerIdFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000091D6 File Offset: 0x000073D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
