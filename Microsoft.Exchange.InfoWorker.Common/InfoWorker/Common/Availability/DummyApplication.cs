using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000079 RID: 121
	internal sealed class DummyApplication : Application
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000E4C6 File Offset: 0x0000C6C6
		public static FreeBusyQuery[] ConvertBaseToFreeBusyQuery(BaseQuery[] baseQueries)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000E4CD File Offset: 0x0000C6CD
		public override ThreadCounter Worker
		{
			get
			{
				return DummyApplication.WorkerThreadCounter;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		public override ThreadCounter IOCompletion
		{
			get
			{
				return DummyApplication.IOCompletionThreadCounter;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000E4DB File Offset: 0x0000C6DB
		public override int MinimumRequiredVersion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public override LocalizedString Name
		{
			get
			{
				return Strings.DummyApplicationName;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E4E5 File Offset: 0x0000C6E5
		private DummyApplication() : base(false)
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000E4EE File Offset: 0x0000C6EE
		public override IService CreateService(WebServiceUri webServiceUri, TargetServerVersion targetVersion, RequestType requestType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000E4F5 File Offset: 0x0000C6F5
		public override IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxArray, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public override void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E503 File Offset: 0x0000C703
		public override string GetParameterDataString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E50A File Offset: 0x0000C70A
		public override LocalQuery CreateLocalQuery(ClientContext clientContext, DateTime requestCompletionDeadline)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E511 File Offset: 0x0000C711
		public override BaseQueryResult CreateQueryResult(LocalizedException exception)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E518 File Offset: 0x0000C718
		public override BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E51F File Offset: 0x0000C71F
		public override BaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E526 File Offset: 0x0000C726
		public override BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E52D File Offset: 0x0000C72D
		public override BaseQuery CreateFromGroup(RecipientData recipientData, BaseQuery[] groupMembers, bool groupCapped)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000E534 File Offset: 0x0000C734
		public override Offer OfferForExternalSharing
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E53B File Offset: 0x0000C73B
		public override bool EnabledInRelationship(OrganizationRelationship organizationRelationship)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E542 File Offset: 0x0000C742
		public override AvailabilityException CreateExceptionForUnsupportedVersion(RecipientData recipient, int serverVersion)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001E0 RID: 480
		private static readonly ThreadCounter WorkerThreadCounter = new ThreadCounter();

		// Token: 0x040001E1 RID: 481
		private static readonly ThreadCounter IOCompletionThreadCounter = new ThreadCounter();

		// Token: 0x040001E2 RID: 482
		public static readonly DummyApplication Instance = new DummyApplication();
	}
}
