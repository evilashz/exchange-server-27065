using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SourceMailboxQuotaWarningException : MigrationPermanentException
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x0006FD59 File Offset: 0x0006DF59
		public SourceMailboxQuotaWarningException() : base(Strings.RemoteMailboxQuotaWarningStatus)
		{
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0006FD66 File Offset: 0x0006DF66
		public SourceMailboxQuotaWarningException(Exception innerException) : base(Strings.RemoteMailboxQuotaWarningStatus, innerException)
		{
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0006FD74 File Offset: 0x0006DF74
		protected SourceMailboxQuotaWarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0006FD7E File Offset: 0x0006DF7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
