using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000FF RID: 255
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotChangePermissionsOnFolderException : StoragePermanentException
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x0006968F File Offset: 0x0006788F
		public CannotChangePermissionsOnFolderException() : base(ServerStrings.CannotChangePermissionsOnFolder)
		{
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0006969C File Offset: 0x0006789C
		public CannotChangePermissionsOnFolderException(Exception innerException) : base(ServerStrings.CannotChangePermissionsOnFolder, innerException)
		{
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000696AA File Offset: 0x000678AA
		protected CannotChangePermissionsOnFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000696B4 File Offset: 0x000678B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
