using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000760 RID: 1888
	[Serializable]
	public class OleConversionServerBusyException : StorageTransientException
	{
		// Token: 0x0600486E RID: 18542 RVA: 0x00130FFC File Offset: 0x0012F1FC
		public OleConversionServerBusyException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x00131005 File Offset: 0x0012F205
		public OleConversionServerBusyException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x0013100F File Offset: 0x0012F20F
		protected OleConversionServerBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
