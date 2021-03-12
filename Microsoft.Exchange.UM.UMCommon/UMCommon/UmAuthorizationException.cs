using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001AF RID: 431
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UmAuthorizationException : LocalizedException
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x00035433 File Offset: 0x00033633
		public UmAuthorizationException(string user, string activity) : base(Strings.UmAuthorizationException(user, activity))
		{
			this.user = user;
			this.activity = activity;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00035450 File Offset: 0x00033650
		public UmAuthorizationException(string user, string activity, Exception innerException) : base(Strings.UmAuthorizationException(user, activity), innerException)
		{
			this.user = user;
			this.activity = activity;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00035470 File Offset: 0x00033670
		protected UmAuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.activity = (string)info.GetValue("activity", typeof(string));
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000354C5 File Offset: 0x000336C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("activity", this.activity);
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000354F1 File Offset: 0x000336F1
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000354F9 File Offset: 0x000336F9
		public string Activity
		{
			get
			{
				return this.activity;
			}
		}

		// Token: 0x0400078A RID: 1930
		private readonly string user;

		// Token: 0x0400078B RID: 1931
		private readonly string activity;
	}
}
