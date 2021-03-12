using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000141 RID: 321
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PublicFolderMoveSuspendedException : Exception
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x00051ED6 File Offset: 0x000500D6
		public PublicFolderMoveSuspendedException() : base(Strings.PublicFolderMoveSuspendedError)
		{
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00051EE8 File Offset: 0x000500E8
		public PublicFolderMoveSuspendedException(Exception innerException) : base(Strings.PublicFolderMoveSuspendedError, innerException)
		{
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00051EFB File Offset: 0x000500FB
		protected PublicFolderMoveSuspendedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00051F05 File Offset: 0x00050105
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
