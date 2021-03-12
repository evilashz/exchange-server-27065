using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class FailedToProgressException : ExchangeDataException
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x000669D2 File Offset: 0x00064BD2
		public FailedToProgressException(string message) : base(message)
		{
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000669DB File Offset: 0x00064BDB
		protected FailedToProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
