using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidEncryptedSharedFolderDataException : StoragePermanentException
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0006913E File Offset: 0x0006733E
		public InvalidEncryptedSharedFolderDataException() : base(ServerStrings.InvalidEncryptedSharedFolderDataException)
		{
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0006914B File Offset: 0x0006734B
		public InvalidEncryptedSharedFolderDataException(Exception innerException) : base(ServerStrings.InvalidEncryptedSharedFolderDataException, innerException)
		{
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00069159 File Offset: 0x00067359
		protected InvalidEncryptedSharedFolderDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00069163 File Offset: 0x00067363
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
