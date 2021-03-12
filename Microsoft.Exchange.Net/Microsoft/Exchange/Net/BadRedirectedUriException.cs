using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D8 RID: 216
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BadRedirectedUriException : LocalizedException
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00014184 File Offset: 0x00012384
		public BadRedirectedUriException(string uri) : base(HttpStrings.BadRedirectedUriException(uri))
		{
			this.uri = uri;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00014199 File Offset: 0x00012399
		public BadRedirectedUriException(string uri, Exception innerException) : base(HttpStrings.BadRedirectedUriException(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000141AF File Offset: 0x000123AF
		protected BadRedirectedUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (string)info.GetValue("uri", typeof(string));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000141D9 File Offset: 0x000123D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000141F4 File Offset: 0x000123F4
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04000471 RID: 1137
		private readonly string uri;
	}
}
