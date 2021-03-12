using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200003B RID: 59
	internal class FailedToInstantiateLogFileInfoException : MessageTracingException
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000BC6B File Offset: 0x00009E6B
		public FailedToInstantiateLogFileInfoException(string fileName) : base(fileName)
		{
		}
	}
}
