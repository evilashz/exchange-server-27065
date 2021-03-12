using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E9 RID: 4329
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGetTrustedPublishingDomainFromRmsOnlineException : LocalizedException
	{
		// Token: 0x0600B37A RID: 45946 RVA: 0x0029B47B File Offset: 0x0029967B
		public FailedToGetTrustedPublishingDomainFromRmsOnlineException(Exception e, Exception inner) : base(Strings.RmsOnlineFailedToGetTpd(e, inner))
		{
			this.e = e;
			this.inner = inner;
		}

		// Token: 0x0600B37B RID: 45947 RVA: 0x0029B498 File Offset: 0x00299698
		public FailedToGetTrustedPublishingDomainFromRmsOnlineException(Exception e, Exception inner, Exception innerException) : base(Strings.RmsOnlineFailedToGetTpd(e, inner), innerException)
		{
			this.e = e;
			this.inner = inner;
		}

		// Token: 0x0600B37C RID: 45948 RVA: 0x0029B4B8 File Offset: 0x002996B8
		protected FailedToGetTrustedPublishingDomainFromRmsOnlineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (Exception)info.GetValue("e", typeof(Exception));
			this.inner = (Exception)info.GetValue("inner", typeof(Exception));
		}

		// Token: 0x0600B37D RID: 45949 RVA: 0x0029B50D File Offset: 0x0029970D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
			info.AddValue("inner", this.inner);
		}

		// Token: 0x170038F3 RID: 14579
		// (get) Token: 0x0600B37E RID: 45950 RVA: 0x0029B539 File Offset: 0x00299739
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170038F4 RID: 14580
		// (get) Token: 0x0600B37F RID: 45951 RVA: 0x0029B541 File Offset: 0x00299741
		public Exception Inner
		{
			get
			{
				return this.inner;
			}
		}

		// Token: 0x04006259 RID: 25177
		private readonly Exception e;

		// Token: 0x0400625A RID: 25178
		private readonly Exception inner;
	}
}
