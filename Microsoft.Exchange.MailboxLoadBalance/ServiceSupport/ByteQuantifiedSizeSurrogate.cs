using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000ED RID: 237
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ByteQuantifiedSizeSurrogate
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x000148BC File Offset: 0x00012ABC
		public ByteQuantifiedSizeSurrogate(ByteQuantifiedSize byteQuantifiedSize)
		{
			this.canonical = byteQuantifiedSize.ToString();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000148D7 File Offset: 0x00012AD7
		public ByteQuantifiedSize ToByteQuantifiedSize()
		{
			return ByteQuantifiedSize.Parse(this.canonical);
		}

		// Token: 0x040002CC RID: 716
		[DataMember]
		private readonly string canonical;
	}
}
