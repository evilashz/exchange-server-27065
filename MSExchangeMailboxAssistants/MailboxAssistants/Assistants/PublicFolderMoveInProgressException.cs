using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000140 RID: 320
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PublicFolderMoveInProgressException : TransientException
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x00051EA7 File Offset: 0x000500A7
		public PublicFolderMoveInProgressException() : base(Strings.MoveInProgressError)
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00051EB4 File Offset: 0x000500B4
		public PublicFolderMoveInProgressException(Exception innerException) : base(Strings.MoveInProgressError, innerException)
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00051EC2 File Offset: 0x000500C2
		protected PublicFolderMoveInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00051ECC File Offset: 0x000500CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
