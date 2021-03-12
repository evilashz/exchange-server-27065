using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017B RID: 379
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncStateSizeException : MigrationPermanentException
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x0006FD2A File Offset: 0x0006DF2A
		public SyncStateSizeException() : base(Strings.SyncStateSizeError)
		{
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0006FD37 File Offset: 0x0006DF37
		public SyncStateSizeException(Exception innerException) : base(Strings.SyncStateSizeError, innerException)
		{
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0006FD45 File Offset: 0x0006DF45
		protected SyncStateSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0006FD4F File Offset: 0x0006DF4F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
