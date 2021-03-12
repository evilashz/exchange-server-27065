using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010CE RID: 4302
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsUrlIsInvalidException : LocalizedException
	{
		// Token: 0x0600B2FA RID: 45818 RVA: 0x0029A971 File Offset: 0x00298B71
		public RmsUrlIsInvalidException(Uri uri) : base(Strings.RmsUrlIsInvalidException(uri))
		{
			this.uri = uri;
		}

		// Token: 0x0600B2FB RID: 45819 RVA: 0x0029A986 File Offset: 0x00298B86
		public RmsUrlIsInvalidException(Uri uri, Exception innerException) : base(Strings.RmsUrlIsInvalidException(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x0600B2FC RID: 45820 RVA: 0x0029A99C File Offset: 0x00298B9C
		protected RmsUrlIsInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (Uri)info.GetValue("uri", typeof(Uri));
		}

		// Token: 0x0600B2FD RID: 45821 RVA: 0x0029A9C6 File Offset: 0x00298BC6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
		}

		// Token: 0x170038DF RID: 14559
		// (get) Token: 0x0600B2FE RID: 45822 RVA: 0x0029A9E1 File Offset: 0x00298BE1
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04006245 RID: 25157
		private readonly Uri uri;
	}
}
