using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017E RID: 382
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LeaveOnServerNotSupportedException : MigrationPermanentException
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x0006FDB7 File Offset: 0x0006DFB7
		public LeaveOnServerNotSupportedException() : base(Strings.LeaveOnServerNotSupportedStatus)
		{
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0006FDC4 File Offset: 0x0006DFC4
		public LeaveOnServerNotSupportedException(Exception innerException) : base(Strings.LeaveOnServerNotSupportedStatus, innerException)
		{
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0006FDD2 File Offset: 0x0006DFD2
		protected LeaveOnServerNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0006FDDC File Offset: 0x0006DFDC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
