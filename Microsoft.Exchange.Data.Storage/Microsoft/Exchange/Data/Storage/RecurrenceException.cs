using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000743 RID: 1859
	[Serializable]
	public abstract class RecurrenceException : StoragePermanentException
	{
		// Token: 0x06004815 RID: 18453 RVA: 0x0013096E File Offset: 0x0012EB6E
		public RecurrenceException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x00130977 File Offset: 0x0012EB77
		public RecurrenceException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x00130981 File Offset: 0x0012EB81
		protected RecurrenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
