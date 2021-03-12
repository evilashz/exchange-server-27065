using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	public sealed class ADUserNotFoundException : SharingSynchronizationException
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x00050C0E File Offset: 0x0004EE0E
		public ADUserNotFoundException() : base(Strings.ADUserNotFoundException)
		{
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00050C1B File Offset: 0x0004EE1B
		public ADUserNotFoundException(Exception innerException) : base(Strings.ADUserNotFoundException, innerException)
		{
		}
	}
}
