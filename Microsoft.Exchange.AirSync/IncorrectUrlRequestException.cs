using System;
using System.Net;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	internal class IncorrectUrlRequestException : AirSyncPermanentException
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003F42E File Offset: 0x0003D62E
		internal IncorrectUrlRequestException(HttpStatusCode httpStatusCode, string headerNameIn, string headerValueIn) : base(httpStatusCode, StatusCode.None, null, false)
		{
			this.headerName = headerNameIn;
			this.headerValue = headerValueIn;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003F448 File Offset: 0x0003D648
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x0003F450 File Offset: 0x0003D650
		internal string HeaderName
		{
			get
			{
				return this.headerName;
			}
			set
			{
				this.headerName = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0003F459 File Offset: 0x0003D659
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0003F461 File Offset: 0x0003D661
		internal string HeaderValue
		{
			get
			{
				return this.headerValue;
			}
			set
			{
				this.headerValue = value;
			}
		}

		// Token: 0x04000742 RID: 1858
		private string headerName;

		// Token: 0x04000743 RID: 1859
		private string headerValue;
	}
}
