using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	internal class OAuthIdentityDeserializationException : SecurityException
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x00034F66 File Offset: 0x00033166
		public OAuthIdentityDeserializationException(string message) : base(message)
		{
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00034F6F File Offset: 0x0003316F
		public OAuthIdentityDeserializationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00034F79 File Offset: 0x00033179
		protected OAuthIdentityDeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
