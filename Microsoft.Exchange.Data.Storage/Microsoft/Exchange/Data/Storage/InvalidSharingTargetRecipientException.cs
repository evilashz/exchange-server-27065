using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F3 RID: 243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSharingTargetRecipientException : StoragePermanentException
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x000692E1 File Offset: 0x000674E1
		public InvalidSharingTargetRecipientException() : base(ServerStrings.InvalidSharingTargetRecipientException)
		{
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000692EE File Offset: 0x000674EE
		public InvalidSharingTargetRecipientException(Exception innerException) : base(ServerStrings.InvalidSharingTargetRecipientException, innerException)
		{
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000692FC File Offset: 0x000674FC
		protected InvalidSharingTargetRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00069306 File Offset: 0x00067506
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
