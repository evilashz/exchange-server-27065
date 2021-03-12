using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000790 RID: 1936
	[Serializable]
	public class VirusDetectedException : VirusException
	{
		// Token: 0x06004906 RID: 18694 RVA: 0x00131B3E File Offset: 0x0012FD3E
		public VirusDetectedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x00131B47 File Offset: 0x0012FD47
		public VirusDetectedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x00131B51 File Offset: 0x0012FD51
		protected VirusDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
