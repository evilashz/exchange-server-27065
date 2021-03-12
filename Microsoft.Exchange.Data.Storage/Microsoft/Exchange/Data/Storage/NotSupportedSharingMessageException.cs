using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F1 RID: 241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NotSupportedSharingMessageException : StoragePermanentException
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x000691E5 File Offset: 0x000673E5
		public NotSupportedSharingMessageException() : base(ServerStrings.NotSupportedSharingMessageException)
		{
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000691F2 File Offset: 0x000673F2
		public NotSupportedSharingMessageException(Exception innerException) : base(ServerStrings.NotSupportedSharingMessageException, innerException)
		{
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00069200 File Offset: 0x00067400
		protected NotSupportedSharingMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0006920A File Offset: 0x0006740A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
