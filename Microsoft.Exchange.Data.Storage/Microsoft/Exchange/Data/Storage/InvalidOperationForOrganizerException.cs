using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200073B RID: 1851
	[Serializable]
	public class InvalidOperationForOrganizerException : StoragePermanentException
	{
		// Token: 0x060047FE RID: 18430 RVA: 0x00130838 File Offset: 0x0012EA38
		public InvalidOperationForOrganizerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00130841 File Offset: 0x0012EA41
		public InvalidOperationForOrganizerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x0013084B File Offset: 0x0012EA4B
		protected InvalidOperationForOrganizerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
