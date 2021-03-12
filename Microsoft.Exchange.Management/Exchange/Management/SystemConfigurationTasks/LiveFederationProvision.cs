using System;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009EA RID: 2538
	internal abstract class LiveFederationProvision : FederationProvision
	{
		// Token: 0x06005A92 RID: 23186 RVA: 0x0017B47D File Offset: 0x0017967D
		protected LiveFederationProvision(string certificateThumbprint, string applicationIdentifier, Task task)
		{
			this.certificateThumbprint = certificateThumbprint;
			this.applicationIdentifier = applicationIdentifier;
			this.task = task;
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x0017B49C File Offset: 0x0017969C
		protected void ReserveDomain(string domain, string applicationIdentifier, ManageDelegationClient client, LocalizedString errorProofDomainOwnership, LiveFederationProvision.GetDomainStateDelegate getDomainState)
		{
			LocalizedString localizedString = Strings.SetLiveFedDomainReserveRequest(domain);
			this.WriteVerbose(localizedString);
			this.WriteProgress(localizedString, localizedString, 0);
			try
			{
				client.ReserveDomain(applicationIdentifier, domain, "ExchangeConnector");
			}
			catch (LiveDomainServicesException ex)
			{
				if (ex.DomainError != null && ex.DomainError.Value == DomainError.ProofOfOwnershipNotValid)
				{
					throw new DomainProofOwnershipException(errorProofDomainOwnership, ex);
				}
				throw new UnableToReserveDomainException(domain, applicationIdentifier, ex.Message, ex);
			}
			this.WriteVerbose(Strings.SetLiveFedDomainReserveResponse(domain));
			DomainState domainState = this.WaitWhilePendingState(DomainState.PendingActivation, localizedString, getDomainState);
			this.WriteProgress(localizedString, localizedString, 100);
			if (domainState == DomainState.Unknown)
			{
				this.WriteVerbose(Strings.ErrorCannotGetDomainStatusFromPartnerSTS(domain, applicationIdentifier, string.Empty));
				return;
			}
			if (DomainState.PendingRelease == domainState)
			{
				throw new UnableToReserveDomainException(domain, applicationIdentifier, DomainState.PendingRelease.ToString());
			}
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x0017B57C File Offset: 0x0017977C
		protected void ReleaseDomain(string domain, string applicationIdentifier, bool force, ManageDelegationClient client, LiveFederationProvision.GetDomainStateDelegate getDomainState)
		{
			LocalizedString localizedString = Strings.RemoveLiveFedDomainReserveRequest(domain);
			this.WriteVerbose(localizedString);
			this.WriteProgress(localizedString, localizedString, 0);
			try
			{
				client.ReleaseDomain(this.ApplicationIdentifier, domain);
			}
			catch (LiveDomainServicesException ex)
			{
				if (!force)
				{
					throw new UnableToReleaseDomainException(domain, applicationIdentifier, ex.Message, ex);
				}
				this.WriteVerbose(Strings.ErrorUnableToReleaseDomain(domain, applicationIdentifier, ex.Message));
			}
			this.WriteVerbose(Strings.RemoveLiveFedDomainReserveResponse(domain));
			this.WaitWhilePendingState(DomainState.PendingRelease, localizedString, getDomainState);
			this.WriteProgress(localizedString, localizedString, 100);
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0017B618 File Offset: 0x00179818
		protected void AddUri(string domain, string applicationIdentifier, ManageDelegationClient client, LocalizedString errorProofDomainOwnership)
		{
			this.WriteVerbose(Strings.SetLiveFedUriReserveRequest(domain));
			try
			{
				client.AddUri(this.ApplicationIdentifier, domain);
			}
			catch (LiveDomainServicesException ex)
			{
				if (ex.DomainError != null && ex.DomainError.Value == DomainError.ProofOfOwnershipNotValid)
				{
					throw new DomainProofOwnershipException(errorProofDomainOwnership, ex);
				}
				throw new UnableToReserveUriException(domain, domain, applicationIdentifier, ex.Message, ex);
			}
			this.WriteVerbose(Strings.SetLiveFedUriReserveResponse(domain));
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x0017B6A8 File Offset: 0x001798A8
		protected void RemoveUri(ManageDelegationClient client, string uri, bool force)
		{
			this.WriteVerbose(Strings.RemoveLiveFedUriReserveRequest(uri));
			try
			{
				client.RemoveUri(this.ApplicationIdentifier, uri);
				this.WriteVerbose(Strings.RemoveLiveFedUriReserveResponse(uri));
			}
			catch (LiveDomainServicesException ex)
			{
				if (!force)
				{
					throw new UnableToReleaseUriException(uri, uri, this.ApplicationIdentifier, ex.Message, ex);
				}
				this.WriteVerbose(Strings.ErrorUnableToReleaseUri(uri, uri, this.ApplicationIdentifier, ex.Message));
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x0017B734 File Offset: 0x00179934
		protected string ApplicationIdentifier
		{
			get
			{
				if (string.IsNullOrEmpty(this.applicationIdentifier))
				{
					throw new ArgumentNullException("applicationIdentifier");
				}
				return this.applicationIdentifier;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x06005A98 RID: 23192 RVA: 0x0017B754 File Offset: 0x00179954
		protected string CertificateThumbprint
		{
			get
			{
				if (string.IsNullOrEmpty(this.certificateThumbprint))
				{
					throw new ArgumentNullException("certificateThumbprint");
				}
				return this.certificateThumbprint;
			}
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06005A99 RID: 23193 RVA: 0x0017B774 File Offset: 0x00179974
		protected WriteVerboseDelegate WriteVerbose
		{
			get
			{
				return new WriteVerboseDelegate(this.task.WriteVerbose);
			}
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0017B788 File Offset: 0x00179988
		private DomainState WaitWhilePendingState(DomainState pendingState, LocalizedString pendingMessage, LiveFederationProvision.GetDomainStateDelegate getDomainState)
		{
			int num = 20;
			int num2 = (int)((double)(100 - num) / (LiveFederationProvision.PendingStateWait.TotalSeconds / LiveFederationProvision.PendingStateWaitInterval.TotalSeconds));
			DateTime t = DateTime.UtcNow + LiveFederationProvision.PendingStateWait;
			DomainState domainState = DomainState.Unknown;
			for (;;)
			{
				this.WriteProgress(pendingMessage, pendingMessage, num);
				try
				{
					domainState = getDomainState();
				}
				catch (LiveDomainServicesException)
				{
					domainState = DomainState.Unknown;
				}
				if (domainState != pendingState || DateTime.UtcNow > t)
				{
					break;
				}
				Thread.Sleep(LiveFederationProvision.PendingStateWaitInterval);
				num += num2;
			}
			return domainState;
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x0017B818 File Offset: 0x00179A18
		private void WriteProgress(LocalizedString activity, LocalizedString statusDescription, int percentCompleted)
		{
			ExProgressRecord exProgressRecord = new ExProgressRecord(0, activity, statusDescription);
			exProgressRecord.PercentComplete = percentCompleted;
			this.task.WriteProgress(exProgressRecord);
		}

		// Token: 0x040033D5 RID: 13269
		private static readonly TimeSpan PendingStateWait = TimeSpan.FromSeconds(60.0);

		// Token: 0x040033D6 RID: 13270
		private static readonly TimeSpan PendingStateWaitInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x040033D7 RID: 13271
		private readonly string applicationIdentifier;

		// Token: 0x040033D8 RID: 13272
		private readonly string certificateThumbprint;

		// Token: 0x040033D9 RID: 13273
		private Task task;

		// Token: 0x020009EB RID: 2539
		// (Invoke) Token: 0x06005A9E RID: 23198
		protected delegate DomainState GetDomainStateDelegate();
	}
}
