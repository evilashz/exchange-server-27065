using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LookUpTenantIdFailureException : LocalizedException
	{
		// Token: 0x0600020C RID: 524 RVA: 0x000093E5 File Offset: 0x000075E5
		public LookUpTenantIdFailureException() : base(MigrationMonitorStrings.ErrorLookUpTenantId)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000093F2 File Offset: 0x000075F2
		public LookUpTenantIdFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorLookUpTenantId, innerException)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009400 File Offset: 0x00007600
		protected LookUpTenantIdFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000940A File Offset: 0x0000760A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
