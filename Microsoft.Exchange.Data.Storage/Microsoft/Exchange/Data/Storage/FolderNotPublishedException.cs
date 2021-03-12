using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000FA RID: 250
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FolderNotPublishedException : StoragePermanentException
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x0006955B File Offset: 0x0006775B
		public FolderNotPublishedException() : base(ServerStrings.FolderNotPublishedException)
		{
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00069568 File Offset: 0x00067768
		public FolderNotPublishedException(Exception innerException) : base(ServerStrings.FolderNotPublishedException, innerException)
		{
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00069576 File Offset: 0x00067776
		protected FolderNotPublishedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00069580 File Offset: 0x00067780
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
