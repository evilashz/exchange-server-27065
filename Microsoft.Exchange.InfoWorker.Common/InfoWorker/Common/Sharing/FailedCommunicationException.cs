using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	public sealed class FailedCommunicationException : SharingSynchronizationException
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x00050D78 File Offset: 0x0004EF78
		public FailedCommunicationException() : base(Strings.FailedCommunicationException)
		{
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00050D85 File Offset: 0x0004EF85
		public FailedCommunicationException(Exception innerException) : base(Strings.FailedCommunicationException, innerException)
		{
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00050D93 File Offset: 0x0004EF93
		internal FailedCommunicationException(Exception innerException, DelegationTokenRequest delegationTokenRequest) : base(Strings.FailedCommunicationException, innerException)
		{
			this.Data.Add("Delegation Token Request", delegationTokenRequest);
		}

		// Token: 0x04000B9A RID: 2970
		private const string DelegationTokenRequestAdditionalData = "Delegation Token Request";
	}
}
