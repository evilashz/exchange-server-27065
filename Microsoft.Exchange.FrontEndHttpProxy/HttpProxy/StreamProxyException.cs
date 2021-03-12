using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	internal class StreamProxyException : Exception
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000BF03 File Offset: 0x0000A103
		public StreamProxyException(Exception innerException) : base(innerException.Message, innerException)
		{
		}
	}
}
