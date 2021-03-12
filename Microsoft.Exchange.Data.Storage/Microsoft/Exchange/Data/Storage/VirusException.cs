using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200078F RID: 1935
	[Serializable]
	public abstract class VirusException : StoragePermanentException
	{
		// Token: 0x06004903 RID: 18691 RVA: 0x00131B21 File Offset: 0x0012FD21
		public VirusException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x00131B2A File Offset: 0x0012FD2A
		public VirusException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x00131B34 File Offset: 0x0012FD34
		protected VirusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
