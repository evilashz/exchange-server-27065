using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000179 RID: 377
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionErrorException : MigrationPermanentException
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x0006FCCC File Offset: 0x0006DECC
		public ConnectionErrorException() : base(Strings.ConnectionErrorStatus)
		{
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0006FCD9 File Offset: 0x0006DED9
		public ConnectionErrorException(Exception innerException) : base(Strings.ConnectionErrorStatus, innerException)
		{
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0006FCE7 File Offset: 0x0006DEE7
		protected ConnectionErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0006FCF1 File Offset: 0x0006DEF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
