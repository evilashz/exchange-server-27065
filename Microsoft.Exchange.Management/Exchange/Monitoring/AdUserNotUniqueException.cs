using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001036 RID: 4150
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdUserNotUniqueException : LocalizedException
	{
		// Token: 0x0600AFC6 RID: 44998 RVA: 0x00294DCD File Offset: 0x00292FCD
		public AdUserNotUniqueException(string user) : base(Strings.ErrorAdUserNotUnique(user))
		{
			this.user = user;
		}

		// Token: 0x0600AFC7 RID: 44999 RVA: 0x00294DE2 File Offset: 0x00292FE2
		public AdUserNotUniqueException(string user, Exception innerException) : base(Strings.ErrorAdUserNotUnique(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x0600AFC8 RID: 45000 RVA: 0x00294DF8 File Offset: 0x00292FF8
		protected AdUserNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x0600AFC9 RID: 45001 RVA: 0x00294E22 File Offset: 0x00293022
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x1700380B RID: 14347
		// (get) Token: 0x0600AFCA RID: 45002 RVA: 0x00294E3D File Offset: 0x0029303D
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04006171 RID: 24945
		private readonly string user;
	}
}
