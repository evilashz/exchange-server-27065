using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000FC RID: 252
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoSharingHandlerFoundException : StoragePermanentException
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x000695B9 File Offset: 0x000677B9
		public NoSharingHandlerFoundException(string recipient) : base(ServerStrings.NoSharingHandlerFoundException(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000695CE File Offset: 0x000677CE
		public NoSharingHandlerFoundException(string recipient, Exception innerException) : base(ServerStrings.NoSharingHandlerFoundException(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000695E4 File Offset: 0x000677E4
		protected NoSharingHandlerFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0006960E File Offset: 0x0006780E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x00069629 File Offset: 0x00067829
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04000990 RID: 2448
		private readonly string recipient;
	}
}
