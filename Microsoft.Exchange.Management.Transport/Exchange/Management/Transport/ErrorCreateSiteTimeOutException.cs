using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000187 RID: 391
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCreateSiteTimeOutException : SpCsomCallException
	{
		// Token: 0x06000F93 RID: 3987 RVA: 0x000365E7 File Offset: 0x000347E7
		public ErrorCreateSiteTimeOutException(string url) : base(Strings.ErrorCreateSiteTimeOut(url))
		{
			this.url = url;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000365FC File Offset: 0x000347FC
		public ErrorCreateSiteTimeOutException(string url, Exception innerException) : base(Strings.ErrorCreateSiteTimeOut(url), innerException)
		{
			this.url = url;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00036612 File Offset: 0x00034812
		protected ErrorCreateSiteTimeOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.url = (string)info.GetValue("url", typeof(string));
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003663C File Offset: 0x0003483C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("url", this.url);
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00036657 File Offset: 0x00034857
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04000683 RID: 1667
		private readonly string url;
	}
}
