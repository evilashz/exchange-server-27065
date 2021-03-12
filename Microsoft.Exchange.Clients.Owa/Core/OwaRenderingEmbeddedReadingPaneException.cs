using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001D3 RID: 467
	[Serializable]
	public class OwaRenderingEmbeddedReadingPaneException : OwaTransientException
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0005E9DD File Offset: 0x0005CBDD
		public OwaRenderingEmbeddedReadingPaneException(Exception innerException) : base(string.Empty, innerException)
		{
		}
	}
}
