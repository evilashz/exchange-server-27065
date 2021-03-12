using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017D RID: 381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TargetMailboxQuotaWarningException : MigrationPermanentException
	{
		// Token: 0x060016CE RID: 5838 RVA: 0x0006FD88 File Offset: 0x0006DF88
		public TargetMailboxQuotaWarningException() : base(Strings.LabsMailboxQuotaWarningStatus)
		{
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0006FD95 File Offset: 0x0006DF95
		public TargetMailboxQuotaWarningException(Exception innerException) : base(Strings.LabsMailboxQuotaWarningStatus, innerException)
		{
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0006FDA3 File Offset: 0x0006DFA3
		protected TargetMailboxQuotaWarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0006FDAD File Offset: 0x0006DFAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
