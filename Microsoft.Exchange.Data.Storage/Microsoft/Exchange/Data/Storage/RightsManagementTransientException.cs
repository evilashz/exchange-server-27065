using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000774 RID: 1908
	[Serializable]
	public class RightsManagementTransientException : StorageTransientException
	{
		// Token: 0x060048B8 RID: 18616 RVA: 0x001316D5 File Offset: 0x0012F8D5
		public RightsManagementTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x001316DE File Offset: 0x0012F8DE
		public RightsManagementTransientException(LocalizedString message, LocalizedException innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001316E8 File Offset: 0x0012F8E8
		protected RightsManagementTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
