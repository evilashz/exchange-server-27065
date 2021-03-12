using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	internal class ShadowSubmissionInfo : SubmissionInfo
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000043CD File Offset: 0x000025CD
		public ShadowSubmissionInfo(string serverDN, string serverFqdn, IPAddress networkAddress, Guid mdbGuid, bool isShadowSupported, DateTime originalCreateTime, string mailboxHopLatency, string internetMessageId, string sender, long contentHash) : base(serverDN, serverFqdn, networkAddress, mdbGuid, isShadowSupported, originalCreateTime, mailboxHopLatency)
		{
			this.internetMessageId = internetMessageId;
			this.sender = sender;
			base.ContentHash = contentHash;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000043F8 File Offset: 0x000025F8
		public string InternetMessageId
		{
			get
			{
				return this.internetMessageId;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004400 File Offset: 0x00002600
		public string Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004408 File Offset: 0x00002608
		public override bool IsShadowSubmission
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000440B File Offset: 0x0000260B
		public override SubmissionItem CreateSubmissionItem(MailItemSubmitter context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004414 File Offset: 0x00002614
		public override OrganizationId GetOrganizationId()
		{
			OrganizationId result;
			ADOperationResult adoperationResult = MultiTenantTransport.TryGetOrganizationId(new RoutingAddress(this.sender), out result);
			bool succeeded = adoperationResult.Succeeded;
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000443C File Offset: 0x0000263C
		public override SenderGuidTraceFilter GetTraceFilter()
		{
			return default(SenderGuidTraceFilter);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004454 File Offset: 0x00002654
		public override string GetPoisonId()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				this.sender,
				this.internetMessageId
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000448C File Offset: 0x0000268C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Sender {0}, InternetMessageId {1}", new object[]
			{
				this.sender,
				this.internetMessageId
			});
		}

		// Token: 0x04000069 RID: 105
		private readonly string internetMessageId;

		// Token: 0x0400006A RID: 106
		private readonly string sender;
	}
}
