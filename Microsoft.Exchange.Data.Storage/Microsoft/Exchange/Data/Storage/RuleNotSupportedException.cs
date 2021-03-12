using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000776 RID: 1910
	[Serializable]
	public class RuleNotSupportedException : StoragePermanentException
	{
		// Token: 0x060048BD RID: 18621 RVA: 0x00131738 File Offset: 0x0012F938
		public RuleNotSupportedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x00131741 File Offset: 0x0012F941
		public RuleNotSupportedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0013174B File Offset: 0x0012F94B
		protected RuleNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
