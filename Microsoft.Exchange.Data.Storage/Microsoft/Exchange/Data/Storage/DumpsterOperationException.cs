using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200063C RID: 1596
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DumpsterOperationException : StoragePermanentException
	{
		// Token: 0x060041C2 RID: 16834 RVA: 0x00117851 File Offset: 0x00115A51
		public DumpsterOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x0011785A File Offset: 0x00115A5A
		public DumpsterOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x00117864 File Offset: 0x00115A64
		private DumpsterOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
