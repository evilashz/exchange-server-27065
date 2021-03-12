using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200072B RID: 1835
	[Serializable]
	public class EventNotFoundException : StoragePermanentException
	{
		// Token: 0x060047CE RID: 18382 RVA: 0x001305A3 File Offset: 0x0012E7A3
		public EventNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x001305AD File Offset: 0x0012E7AD
		public EventNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x001305B6 File Offset: 0x0012E7B6
		protected EventNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
