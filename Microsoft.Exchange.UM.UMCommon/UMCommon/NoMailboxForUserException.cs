using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200019A RID: 410
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoMailboxForUserException : LocalizedException
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x00034D58 File Offset: 0x00032F58
		public NoMailboxForUserException(string user) : base(Strings.ExceptionNoMailboxForUser(user))
		{
			this.user = user;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00034D6D File Offset: 0x00032F6D
		public NoMailboxForUserException(string user, Exception innerException) : base(Strings.ExceptionNoMailboxForUser(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00034D83 File Offset: 0x00032F83
		protected NoMailboxForUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00034DAD File Offset: 0x00032FAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00034DC8 File Offset: 0x00032FC8
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000780 RID: 1920
		private readonly string user;
	}
}
