using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000143 RID: 323
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedMoveStateException : Exception
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x00051F48 File Offset: 0x00050148
		public UnexpectedMoveStateException(string moveState) : base(Strings.UnexpectedMoveStateError(moveState))
		{
			this.moveState = moveState;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00051F62 File Offset: 0x00050162
		public UnexpectedMoveStateException(string moveState, Exception innerException) : base(Strings.UnexpectedMoveStateError(moveState), innerException)
		{
			this.moveState = moveState;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00051F7D File Offset: 0x0005017D
		protected UnexpectedMoveStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.moveState = (string)info.GetValue("moveState", typeof(string));
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00051FA7 File Offset: 0x000501A7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("moveState", this.moveState);
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00051FC2 File Offset: 0x000501C2
		public string MoveState
		{
			get
			{
				return this.moveState;
			}
		}

		// Token: 0x0400083E RID: 2110
		private readonly string moveState;
	}
}
