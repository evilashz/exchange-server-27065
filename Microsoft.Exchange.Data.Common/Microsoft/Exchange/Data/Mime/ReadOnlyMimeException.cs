using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000051 RID: 81
	internal class ReadOnlyMimeException : InvalidOperationException
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00010190 File Offset: 0x0000E390
		public ReadOnlyMimeException(string method) : base(string.Format("{0} was called on a read-only MIME document.", method))
		{
		}
	}
}
