using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000142 RID: 322
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SplitPlanFoldersInvalidException : Exception
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x00051F0F File Offset: 0x0005010F
		public SplitPlanFoldersInvalidException() : base(Strings.SplitPlanFoldersInvalidError)
		{
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00051F21 File Offset: 0x00050121
		public SplitPlanFoldersInvalidException(Exception innerException) : base(Strings.SplitPlanFoldersInvalidError, innerException)
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00051F34 File Offset: 0x00050134
		protected SplitPlanFoldersInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00051F3E File Offset: 0x0005013E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
