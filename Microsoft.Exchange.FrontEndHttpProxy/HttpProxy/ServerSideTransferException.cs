using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	internal class ServerSideTransferException : HttpException
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000BE50 File Offset: 0x0000A050
		public ServerSideTransferException(string redirectUrl, LegacyRedirectTypeOptions redirectType)
		{
			this.RedirectUrl = redirectUrl;
			this.RedirectType = redirectType;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BE66 File Offset: 0x0000A066
		public ServerSideTransferException(Exception innerException) : base(innerException.Message, innerException)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000BE75 File Offset: 0x0000A075
		public ServerSideTransferException(string redirectUrl, string message) : base(message)
		{
			this.RedirectUrl = redirectUrl;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BE85 File Offset: 0x0000A085
		public ServerSideTransferException(string redirectUrl, string message, Exception innerException) : base(message, innerException)
		{
			this.RedirectUrl = redirectUrl;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BE96 File Offset: 0x0000A096
		protected ServerSideTransferException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.RedirectUrl = (string)info.GetValue("redirectUrl", typeof(string));
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000BECB File Offset: 0x0000A0CB
		public string RedirectUrl { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public LegacyRedirectTypeOptions RedirectType { get; private set; }

		// Token: 0x06000229 RID: 553 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("redirectUrl", this.RedirectUrl);
			}
		}
	}
}
