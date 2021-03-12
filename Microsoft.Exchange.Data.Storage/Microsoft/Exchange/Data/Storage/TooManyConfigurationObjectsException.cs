using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000783 RID: 1923
	[Serializable]
	public class TooManyConfigurationObjectsException : StoragePermanentException
	{
		// Token: 0x060048E3 RID: 18659 RVA: 0x00131957 File Offset: 0x0012FB57
		public TooManyConfigurationObjectsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x00131960 File Offset: 0x0012FB60
		public TooManyConfigurationObjectsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0013196A File Offset: 0x0012FB6A
		protected TooManyConfigurationObjectsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
