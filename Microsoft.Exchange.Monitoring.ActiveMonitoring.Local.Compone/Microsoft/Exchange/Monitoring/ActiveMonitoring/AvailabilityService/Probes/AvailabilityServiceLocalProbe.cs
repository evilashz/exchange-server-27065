using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Calendar.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Probes
{
	// Token: 0x02000019 RID: 25
	public class AvailabilityServiceLocalProbe : AvailabilityServiceProbe
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000094C7 File Offset: 0x000076C7
		protected override string ComponentId
		{
			get
			{
				return "AvailabilityService_LAM_Probe";
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000094CE File Offset: 0x000076CE
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Initialize(ExTraceGlobals.AvailabilityServiceTracer);
			ExchangeServerRoleEndpoint exchangeServerRoleEndpoint = LocalEndpointManager.Instance.ExchangeServerRoleEndpoint;
			if (!ExEnvironment.IsTest)
			{
				this.DoWorkOffBoxRequest(cancellationToken);
				return;
			}
			base.DoWorkInternal(cancellationToken);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000094FC File Offset: 0x000076FC
		protected override void UpdateProbeResultAttributes()
		{
			base.UpdateProbeResultAttributes();
			if (base.IsProbeFailed)
			{
				this.UpdateTargetBEServerName();
				base.Result.StateAttribute14 = this.targetBEServer;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009524 File Offset: 0x00007724
		protected override void ThrowProbeError()
		{
			if (!base.IsProbeFailed)
			{
				return;
			}
			Server exchangeServerByName = DirectoryAccessor.Instance.GetExchangeServerByName(this.targetBEServer);
			if (DirectoryAccessor.Instance.IsMonitoringOffline(exchangeServerByName))
			{
				this.probeErrorCode = "Server In Maintenance";
				base.Result.FailureCategory = (int)AvailabilityServiceProbeUtil.KnownErrors[this.probeErrorCode];
				base.Result.StateAttribute1 = ((AvailabilityServiceProbeUtil.FailingComponent)base.Result.FailureCategory).ToString();
				base.Result.StateAttribute2 = this.probeErrorCode;
				return;
			}
			throw new AvailabilityServiceValidationException(base.ProbeErrorMessage);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000095C0 File Offset: 0x000077C0
		private void DoWorkOffBoxRequest(CancellationToken cancellationToken)
		{
			MailboxDatabaseInfo activeDatabaseCopy = this.GetActiveDatabaseCopy();
			if (activeDatabaseCopy == null)
			{
				base.LogTrace("At least one active database will be required to make off-box requests. Skip the run.");
				this.probeErrorCode = "No Active Copy Database On Mailbox";
				base.Result.FailureCategory = (int)AvailabilityServiceProbeUtil.KnownErrors[this.probeErrorCode];
				base.Result.StateAttribute1 = ((AvailabilityServiceProbeUtil.FailingComponent)base.Result.FailureCategory).ToString();
				base.Result.StateAttribute2 = this.probeErrorCode;
				return;
			}
			base.LogTrace("Override monitoring account for requester with the one from GetActiveDatabaseCopy().");
			base.Definition.Account = activeDatabaseCopy.MonitoringAccount + "@" + activeDatabaseCopy.MonitoringAccountDomain;
			base.Definition.AccountPassword = activeDatabaseCopy.MonitoringAccountPassword;
			base.Definition.AccountDisplayName = activeDatabaseCopy.MonitoringAccount;
			base.Result.StateAttribute23 = "onenote:///\\\\exstore\\files\\userfiles\\gisellid\\Supportability%20for%20Calendar\\Availability%20Service.one#Battle%20Card%20E15%20Availability%20Service%20Probe&section-id={9C4DDCB7-B82B-4D3D-AD32-000FBD7878F4}&page-id={205CE2D5-D729-41E0-B7EA-DB95A3B5B4F8}&end";
			base.DoWorkInternal(cancellationToken);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000096A0 File Offset: 0x000078A0
		private MailboxDatabaseInfo GetActiveDatabaseCopy()
		{
			ICollection<MailboxDatabaseInfo> mailboxDatabaseInfoCollectionForBackend = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			if (mailboxDatabaseInfoCollectionForBackend != null && mailboxDatabaseInfoCollectionForBackend.Count > 0)
			{
				int num = 0;
				int num2 = AvailabilityServiceLocalProbe.randomNumberGenerator.Next(mailboxDatabaseInfoCollectionForBackend.Count);
				MailboxDatabaseInfo mailboxDatabaseInfo;
				for (;;)
				{
					mailboxDatabaseInfo = mailboxDatabaseInfoCollectionForBackend.ElementAt(num2);
					Guid mailboxDatabaseGuid = mailboxDatabaseInfo.MailboxDatabaseGuid;
					if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid))
					{
						break;
					}
					if (++num2 == mailboxDatabaseInfoCollectionForBackend.Count)
					{
						num2 = 0;
					}
					if (++num >= mailboxDatabaseInfoCollectionForBackend.Count)
					{
						goto IL_6E;
					}
				}
				return mailboxDatabaseInfo;
			}
			IL_6E:
			return null;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000971C File Offset: 0x0000791C
		private void UpdateTargetBEServerName()
		{
			if (ExEnvironment.IsTest && !string.IsNullOrEmpty(base.Result.StateAttribute13))
			{
				this.targetBEServer = base.Result.StateAttribute13;
				return;
			}
			if (base.Definition.Attributes.ContainsKey("DatabaseGuid"))
			{
				string text = base.Definition.Attributes["DatabaseGuid"];
				if (!string.IsNullOrEmpty(text))
				{
					this.targetBEServer = DirectoryAccessor.Instance.GetDatabaseActiveHost(new Guid(text));
				}
			}
		}

		// Token: 0x040000B7 RID: 183
		private string targetBEServer = string.Empty;

		// Token: 0x040000B8 RID: 184
		private static Random randomNumberGenerator = new Random();
	}
}
