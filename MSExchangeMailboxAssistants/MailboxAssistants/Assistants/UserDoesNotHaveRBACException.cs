using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200013A RID: 314
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserDoesNotHaveRBACException : LocalizedException
	{
		// Token: 0x06000CF8 RID: 3320 RVA: 0x00051AF8 File Offset: 0x0004FCF8
		public UserDoesNotHaveRBACException(string username) : base(Strings.ErrorUserMissingOrWithoutRBAC(username))
		{
			this.username = username;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00051B0D File Offset: 0x0004FD0D
		public UserDoesNotHaveRBACException(string username, Exception innerException) : base(Strings.ErrorUserMissingOrWithoutRBAC(username), innerException)
		{
			this.username = username;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00051B23 File Offset: 0x0004FD23
		protected UserDoesNotHaveRBACException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.username = (string)info.GetValue("username", typeof(string));
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00051B4D File Offset: 0x0004FD4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("username", this.username);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00051B68 File Offset: 0x0004FD68
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x04000836 RID: 2102
		private readonly string username;
	}
}
