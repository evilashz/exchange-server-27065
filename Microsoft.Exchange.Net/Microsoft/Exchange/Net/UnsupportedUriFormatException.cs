using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D7 RID: 215
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedUriFormatException : LocalizedException
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0001410C File Offset: 0x0001230C
		public UnsupportedUriFormatException(string uri) : base(HttpStrings.UnsupportedUriFormatException(uri))
		{
			this.uri = uri;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00014121 File Offset: 0x00012321
		public UnsupportedUriFormatException(string uri, Exception innerException) : base(HttpStrings.UnsupportedUriFormatException(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00014137 File Offset: 0x00012337
		protected UnsupportedUriFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (string)info.GetValue("uri", typeof(string));
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00014161 File Offset: 0x00012361
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001417C File Offset: 0x0001237C
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04000470 RID: 1136
		private readonly string uri;
	}
}
