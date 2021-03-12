using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class CorruptDataException : StoragePermanentException
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x000673DD File Offset: 0x000655DD
		public CorruptDataException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000673E6 File Offset: 0x000655E6
		public CorruptDataException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000673F0 File Offset: 0x000655F0
		protected CorruptDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
