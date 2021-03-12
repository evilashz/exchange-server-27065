using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RemoteServerIsSlowException : MigrationPermanentException
	{
		// Token: 0x060016C2 RID: 5826 RVA: 0x0006FCFB File Offset: 0x0006DEFB
		public RemoteServerIsSlowException() : base(Strings.RemoteServerIsSlow)
		{
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0006FD08 File Offset: 0x0006DF08
		public RemoteServerIsSlowException(Exception innerException) : base(Strings.RemoteServerIsSlow, innerException)
		{
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0006FD16 File Offset: 0x0006DF16
		protected RemoteServerIsSlowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0006FD20 File Offset: 0x0006DF20
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
