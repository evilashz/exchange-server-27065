using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200115B RID: 4443
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CookieAndParameterServiceInstanceIdMismatchException : LocalizedException
	{
		// Token: 0x0600B5B2 RID: 46514 RVA: 0x0029EAB2 File Offset: 0x0029CCB2
		public CookieAndParameterServiceInstanceIdMismatchException(string cookieServiceInstanceId, string paramterServiceInstanceId) : base(Strings.CookieAndParameterServiceInstanceIdMismatch(cookieServiceInstanceId, paramterServiceInstanceId))
		{
			this.cookieServiceInstanceId = cookieServiceInstanceId;
			this.paramterServiceInstanceId = paramterServiceInstanceId;
		}

		// Token: 0x0600B5B3 RID: 46515 RVA: 0x0029EACF File Offset: 0x0029CCCF
		public CookieAndParameterServiceInstanceIdMismatchException(string cookieServiceInstanceId, string paramterServiceInstanceId, Exception innerException) : base(Strings.CookieAndParameterServiceInstanceIdMismatch(cookieServiceInstanceId, paramterServiceInstanceId), innerException)
		{
			this.cookieServiceInstanceId = cookieServiceInstanceId;
			this.paramterServiceInstanceId = paramterServiceInstanceId;
		}

		// Token: 0x0600B5B4 RID: 46516 RVA: 0x0029EAF0 File Offset: 0x0029CCF0
		protected CookieAndParameterServiceInstanceIdMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cookieServiceInstanceId = (string)info.GetValue("cookieServiceInstanceId", typeof(string));
			this.paramterServiceInstanceId = (string)info.GetValue("paramterServiceInstanceId", typeof(string));
		}

		// Token: 0x0600B5B5 RID: 46517 RVA: 0x0029EB45 File Offset: 0x0029CD45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cookieServiceInstanceId", this.cookieServiceInstanceId);
			info.AddValue("paramterServiceInstanceId", this.paramterServiceInstanceId);
		}

		// Token: 0x17003963 RID: 14691
		// (get) Token: 0x0600B5B6 RID: 46518 RVA: 0x0029EB71 File Offset: 0x0029CD71
		public string CookieServiceInstanceId
		{
			get
			{
				return this.cookieServiceInstanceId;
			}
		}

		// Token: 0x17003964 RID: 14692
		// (get) Token: 0x0600B5B7 RID: 46519 RVA: 0x0029EB79 File Offset: 0x0029CD79
		public string ParamterServiceInstanceId
		{
			get
			{
				return this.paramterServiceInstanceId;
			}
		}

		// Token: 0x040062C9 RID: 25289
		private readonly string cookieServiceInstanceId;

		// Token: 0x040062CA RID: 25290
		private readonly string paramterServiceInstanceId;
	}
}
