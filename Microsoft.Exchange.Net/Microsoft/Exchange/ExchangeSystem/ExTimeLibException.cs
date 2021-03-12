using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class ExTimeLibException : InvalidOperationException
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000EBB1 File Offset: 0x0000CDB1
		public ExTimeLibException(string message) : base(message)
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000EBBA File Offset: 0x0000CDBA
		public ExTimeLibException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
