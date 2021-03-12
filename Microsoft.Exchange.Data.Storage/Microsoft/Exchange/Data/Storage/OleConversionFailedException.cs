using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200075F RID: 1887
	[Serializable]
	public class OleConversionFailedException : StoragePermanentException
	{
		// Token: 0x0600486B RID: 18539 RVA: 0x00130FDF File Offset: 0x0012F1DF
		public OleConversionFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x00130FE8 File Offset: 0x0012F1E8
		public OleConversionFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x00130FF2 File Offset: 0x0012F1F2
		protected OleConversionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
