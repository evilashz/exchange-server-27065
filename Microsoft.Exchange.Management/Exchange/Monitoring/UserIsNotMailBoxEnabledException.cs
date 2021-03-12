using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001037 RID: 4151
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserIsNotMailBoxEnabledException : LocalizedException
	{
		// Token: 0x0600AFCB RID: 45003 RVA: 0x00294E45 File Offset: 0x00293045
		public UserIsNotMailBoxEnabledException(string user) : base(Strings.ErrorUserIsNotMailBoxEnabled(user))
		{
			this.user = user;
		}

		// Token: 0x0600AFCC RID: 45004 RVA: 0x00294E5A File Offset: 0x0029305A
		public UserIsNotMailBoxEnabledException(string user, Exception innerException) : base(Strings.ErrorUserIsNotMailBoxEnabled(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x0600AFCD RID: 45005 RVA: 0x00294E70 File Offset: 0x00293070
		protected UserIsNotMailBoxEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x0600AFCE RID: 45006 RVA: 0x00294E9A File Offset: 0x0029309A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x1700380C RID: 14348
		// (get) Token: 0x0600AFCF RID: 45007 RVA: 0x00294EB5 File Offset: 0x002930B5
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04006172 RID: 24946
		private readonly string user;
	}
}
