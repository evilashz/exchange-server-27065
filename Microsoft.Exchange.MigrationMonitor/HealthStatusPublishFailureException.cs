using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class HealthStatusPublishFailureException : LocalizedException
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00009182 File Offset: 0x00007382
		public HealthStatusPublishFailureException() : base(MigrationMonitorStrings.ErrorHealthStatusPublishFailureException)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000918F File Offset: 0x0000738F
		public HealthStatusPublishFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorHealthStatusPublishFailureException, innerException)
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000919D File Offset: 0x0000739D
		protected HealthStatusPublishFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000091A7 File Offset: 0x000073A7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
