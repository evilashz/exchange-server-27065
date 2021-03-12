using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000183 RID: 387
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExternallySuspendedException : MigrationPermanentException
	{
		// Token: 0x060016E7 RID: 5863 RVA: 0x0006FEEB File Offset: 0x0006E0EB
		public ExternallySuspendedException() : base(Strings.ExternallySuspendedFailure)
		{
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0006FEF8 File Offset: 0x0006E0F8
		public ExternallySuspendedException(Exception innerException) : base(Strings.ExternallySuspendedFailure, innerException)
		{
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0006FF06 File Offset: 0x0006E106
		protected ExternallySuspendedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0006FF10 File Offset: 0x0006E110
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
