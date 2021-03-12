using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002D2 RID: 722
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotHaveLocalAccountException : LocalizedException
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x0005D459 File Offset: 0x0005B659
		public CannotHaveLocalAccountException(string user) : base(Strings.CannotHaveLocalAccountException(user))
		{
			this.user = user;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005D46E File Offset: 0x0005B66E
		public CannotHaveLocalAccountException(string user, Exception innerException) : base(Strings.CannotHaveLocalAccountException(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005D484 File Offset: 0x0005B684
		protected CannotHaveLocalAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005D4AE File Offset: 0x0005B6AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0005D4C9 File Offset: 0x0005B6C9
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x040009A1 RID: 2465
		private readonly string user;
	}
}
