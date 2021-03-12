using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000FD RID: 253
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoExternalOwaAvailableException : StoragePermanentException
	{
		// Token: 0x0600137E RID: 4990 RVA: 0x00069631 File Offset: 0x00067831
		public NoExternalOwaAvailableException() : base(ServerStrings.NoExternalOwaAvailableException)
		{
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006963E File Offset: 0x0006783E
		public NoExternalOwaAvailableException(Exception innerException) : base(ServerStrings.NoExternalOwaAvailableException, innerException)
		{
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0006964C File Offset: 0x0006784C
		protected NoExternalOwaAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00069656 File Offset: 0x00067856
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
