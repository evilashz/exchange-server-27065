using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000737 RID: 1847
	[Serializable]
	public class InvalidEventWatermarkException : StoragePermanentException
	{
		// Token: 0x060047F3 RID: 18419 RVA: 0x001307CE File Offset: 0x0012E9CE
		public InvalidEventWatermarkException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x001307D7 File Offset: 0x0012E9D7
		protected InvalidEventWatermarkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
