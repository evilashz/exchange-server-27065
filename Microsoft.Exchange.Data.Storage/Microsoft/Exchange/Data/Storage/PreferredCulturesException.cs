using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000762 RID: 1890
	[Serializable]
	public class PreferredCulturesException : StoragePermanentException
	{
		// Token: 0x06004873 RID: 18547 RVA: 0x0013102D File Offset: 0x0012F22D
		public PreferredCulturesException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x00131036 File Offset: 0x0012F236
		public PreferredCulturesException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x00131040 File Offset: 0x0012F240
		protected PreferredCulturesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
