using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000FB RID: 251
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PublishedFolderAccessDeniedException : StoragePermanentException
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x0006958A File Offset: 0x0006778A
		public PublishedFolderAccessDeniedException() : base(ServerStrings.PublishedFolderAccessDeniedException)
		{
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00069597 File Offset: 0x00067797
		public PublishedFolderAccessDeniedException(Exception innerException) : base(ServerStrings.PublishedFolderAccessDeniedException, innerException)
		{
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000695A5 File Offset: 0x000677A5
		protected PublishedFolderAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000695AF File Offset: 0x000677AF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
