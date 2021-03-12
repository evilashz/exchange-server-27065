using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationCancelledByUserRequestException : MigrationPermanentException
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x0006FAF1 File Offset: 0x0006DCF1
		public MigrationCancelledByUserRequestException() : base(Strings.MigrationCancelledByUserRequest)
		{
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0006FAFE File Offset: 0x0006DCFE
		public MigrationCancelledByUserRequestException(Exception innerException) : base(Strings.MigrationCancelledByUserRequest, innerException)
		{
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0006FB0C File Offset: 0x0006DD0C
		protected MigrationCancelledByUserRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0006FB16 File Offset: 0x0006DD16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
