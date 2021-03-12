using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000490 RID: 1168
	[Serializable]
	public class InvalidPartnerIdException : InvalidOperationException
	{
		// Token: 0x060034E4 RID: 13540 RVA: 0x000D2027 File Offset: 0x000D0227
		public InvalidPartnerIdException(string message) : base(message)
		{
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000D2030 File Offset: 0x000D0230
		public InvalidPartnerIdException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000D203A File Offset: 0x000D023A
		protected InvalidPartnerIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
