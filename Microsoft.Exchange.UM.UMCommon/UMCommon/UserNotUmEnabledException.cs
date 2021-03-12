using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000199 RID: 409
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserNotUmEnabledException : LocalizedException
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x00034CE0 File Offset: 0x00032EE0
		public UserNotUmEnabledException(string user) : base(Strings.ExceptionUserNotUmEnabled(user))
		{
			this.user = user;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00034CF5 File Offset: 0x00032EF5
		public UserNotUmEnabledException(string user, Exception innerException) : base(Strings.ExceptionUserNotUmEnabled(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00034D0B File Offset: 0x00032F0B
		protected UserNotUmEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00034D35 File Offset: 0x00032F35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00034D50 File Offset: 0x00032F50
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x0400077F RID: 1919
		private readonly string user;
	}
}
