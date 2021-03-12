using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveSidToADAccountException : LocalizedException
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000046F7 File Offset: 0x000028F7
		public CannotResolveSidToADAccountException(string userId) : base(Strings.CannotResolveSidToADAccountException(userId))
		{
			this.userId = userId;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000470C File Offset: 0x0000290C
		public CannotResolveSidToADAccountException(string userId, Exception innerException) : base(Strings.CannotResolveSidToADAccountException(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004722 File Offset: 0x00002922
		protected CannotResolveSidToADAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000474C File Offset: 0x0000294C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004767 File Offset: 0x00002967
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04000059 RID: 89
		private readonly string userId;
	}
}
