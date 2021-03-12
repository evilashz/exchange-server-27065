using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000778 RID: 1912
	[Serializable]
	public class RulesTooBigException : StoragePermanentException
	{
		// Token: 0x060048C3 RID: 18627 RVA: 0x00131777 File Offset: 0x0012F977
		public RulesTooBigException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x00131781 File Offset: 0x0012F981
		protected RulesTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
