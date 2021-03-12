using System;
using System.IO;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B28 RID: 2856
	internal sealed class SerializedAccessTokenBody : RequestBody
	{
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000A08F8 File Offset: 0x0009EAF8
		public SerializedAccessTokenBody(SerializedAccessToken accessToken)
		{
			if (accessToken == null)
			{
				throw new ArgumentNullException("accessToken");
			}
			this.accessToken = accessToken;
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000A0915 File Offset: 0x0009EB15
		public sealed override void Write(Stream writeStream)
		{
			this.accessToken.Serialize(writeStream);
		}

		// Token: 0x040035A7 RID: 13735
		private SerializedAccessToken accessToken;
	}
}
