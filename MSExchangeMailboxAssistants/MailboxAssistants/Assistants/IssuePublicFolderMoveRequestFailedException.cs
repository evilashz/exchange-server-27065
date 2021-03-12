using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200013B RID: 315
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IssuePublicFolderMoveRequestFailedException : Exception
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x00051B70 File Offset: 0x0004FD70
		public IssuePublicFolderMoveRequestFailedException() : base(Strings.IssuePublicFolderMoveRequestFailedError)
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00051B82 File Offset: 0x0004FD82
		public IssuePublicFolderMoveRequestFailedException(Exception innerException) : base(Strings.IssuePublicFolderMoveRequestFailedError, innerException)
		{
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00051B95 File Offset: 0x0004FD95
		protected IssuePublicFolderMoveRequestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00051B9F File Offset: 0x0004FD9F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
