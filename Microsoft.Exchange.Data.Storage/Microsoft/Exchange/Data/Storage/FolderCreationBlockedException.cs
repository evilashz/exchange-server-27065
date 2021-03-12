using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000EE RID: 238
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FolderCreationBlockedException : StoragePermanentException
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x0006910F File Offset: 0x0006730F
		public FolderCreationBlockedException() : base(ServerStrings.ErrorFolderCreationIsBlocked)
		{
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0006911C File Offset: 0x0006731C
		public FolderCreationBlockedException(Exception innerException) : base(ServerStrings.ErrorFolderCreationIsBlocked, innerException)
		{
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0006912A File Offset: 0x0006732A
		protected FolderCreationBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00069134 File Offset: 0x00067334
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
