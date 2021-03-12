using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000375 RID: 885
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotCreateEntryIdException : MailboxReplicationPermanentException
	{
		// Token: 0x06002704 RID: 9988 RVA: 0x00053FE1 File Offset: 0x000521E1
		public CannotCreateEntryIdException(string input) : base(MrsStrings.CannotCreateEntryId(input))
		{
			this.input = input;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x00053FF6 File Offset: 0x000521F6
		public CannotCreateEntryIdException(string input, Exception innerException) : base(MrsStrings.CannotCreateEntryId(input), innerException)
		{
			this.input = input;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0005400C File Offset: 0x0005220C
		protected CannotCreateEntryIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.input = (string)info.GetValue("input", typeof(string));
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00054036 File Offset: 0x00052236
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("input", this.input);
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x00054051 File Offset: 0x00052251
		public string Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x04001079 RID: 4217
		private readonly string input;
	}
}
