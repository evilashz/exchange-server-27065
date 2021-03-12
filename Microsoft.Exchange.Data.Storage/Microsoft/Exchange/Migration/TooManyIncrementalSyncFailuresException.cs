using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000182 RID: 386
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TooManyIncrementalSyncFailuresException : MigrationPermanentException
	{
		// Token: 0x060016E3 RID: 5859 RVA: 0x0006FEBC File Offset: 0x0006E0BC
		public TooManyIncrementalSyncFailuresException() : base(Strings.CommunicationErrorStatus)
		{
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0006FEC9 File Offset: 0x0006E0C9
		public TooManyIncrementalSyncFailuresException(Exception innerException) : base(Strings.CommunicationErrorStatus, innerException)
		{
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0006FED7 File Offset: 0x0006E0D7
		protected TooManyIncrementalSyncFailuresException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0006FEE1 File Offset: 0x0006E0E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
