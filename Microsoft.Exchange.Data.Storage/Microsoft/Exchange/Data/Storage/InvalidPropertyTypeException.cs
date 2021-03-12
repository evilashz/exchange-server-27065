using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200073F RID: 1855
	[Serializable]
	public class InvalidPropertyTypeException : StoragePermanentException
	{
		// Token: 0x0600480C RID: 18444 RVA: 0x00130917 File Offset: 0x0012EB17
		public InvalidPropertyTypeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x00130920 File Offset: 0x0012EB20
		public InvalidPropertyTypeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0013092A File Offset: 0x0012EB2A
		protected InvalidPropertyTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
