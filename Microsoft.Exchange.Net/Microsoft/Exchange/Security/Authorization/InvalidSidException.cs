using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal class InvalidSidException : LocalizedException
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00005F38 File Offset: 0x00004138
		public InvalidSidException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005F42 File Offset: 0x00004142
		public InvalidSidException(string invalidSid) : base(new LocalizedString(NetException.InvalidSid((invalidSid == null) ? string.Empty : invalidSid)))
		{
			this.invalidSid = invalidSid;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005F6B File Offset: 0x0000416B
		public InvalidSidException(string invalidSid, Exception innerException) : base(new LocalizedString(NetException.InvalidSid((invalidSid == null) ? string.Empty : invalidSid)), innerException)
		{
			this.invalidSid = invalidSid;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005F95 File Offset: 0x00004195
		public string InvalidSid
		{
			get
			{
				return this.invalidSid;
			}
		}

		// Token: 0x040000AF RID: 175
		private string invalidSid;
	}
}
