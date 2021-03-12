using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D1D RID: 3357
	[Cmdlet("Get", "UMCallDataRecord")]
	public class GetUMCallDataRecord : UMReportsTaskBase<MailboxIdParameter>
	{
		// Token: 0x170027F7 RID: 10231
		// (get) Token: 0x060080DF RID: 32991 RVA: 0x0020F66E File Offset: 0x0020D86E
		// (set) Token: 0x060080E0 RID: 32992 RVA: 0x0020F676 File Offset: 0x0020D876
		private new MailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x060080E1 RID: 32993 RVA: 0x0020F67F File Offset: 0x0020D87F
		// (set) Token: 0x060080E2 RID: 32994 RVA: 0x0020F696 File Offset: 0x0020D896
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x0020F6A9 File Offset: 0x0020D8A9
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.userMailbox = this.ValidateMailboxAndSetOrg(this.Mailbox);
		}

		// Token: 0x060080E4 RID: 32996 RVA: 0x0020F6C4 File Offset: 0x0020D8C4
		private ADUser ValidateMailboxAndSetOrg(MailboxIdParameter mbParam)
		{
			IRecipientSession session = this.CreateSessionToResolveRecipientObjects(false);
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mbParam, session, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(mbParam.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotUnique(mbParam.ToString())));
			OrganizationId organizationId = aduser.OrganizationId;
			ADUser result = aduser;
			if (organizationId != null)
			{
				base.CurrentOrganizationId = organizationId;
			}
			return result;
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x0020F728 File Offset: 0x0020D928
		private IRecipientSession CreateSessionToResolveRecipientObjects(bool scopeToExcecutingUser)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, scopeToExcecutingUser);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 123, "CreateSessionToResolveRecipientObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\GetUMCallDataRecord.cs");
		}

		// Token: 0x060080E6 RID: 32998 RVA: 0x0020F778 File Offset: 0x0020D978
		protected override void ProcessMailbox()
		{
			try
			{
				using (IUMCallDataRecordStorage umcallDataRecordsAcessor = InterServerMailboxAccessor.GetUMCallDataRecordsAcessor(this.DataObject))
				{
					CDRData[] umcallDataRecordsForUser = umcallDataRecordsAcessor.GetUMCallDataRecordsForUser(this.userMailbox.LegacyExchangeDN);
					if (umcallDataRecordsForUser != null)
					{
						this.WriteAsConfigObjects(umcallDataRecordsForUser);
					}
				}
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerTransient, null);
			}
			catch (StoragePermanentException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.ServerTransient, null);
			}
			catch (ContentIndexingNotEnabledException exception3)
			{
				base.WriteError(exception3, ExchangeErrorCategory.ServerTransient, null);
			}
			catch (CDROperationException exception4)
			{
				base.WriteError(exception4, ErrorCategory.ReadError, null);
			}
			catch (EWSUMMailboxAccessException exception5)
			{
				base.WriteError(exception5, ErrorCategory.ReadError, null);
			}
			catch (UnableToFindUMReportDataException)
			{
			}
		}

		// Token: 0x060080E7 RID: 32999 RVA: 0x0020F868 File Offset: 0x0020DA68
		private void WriteAsConfigObjects(CDRData[] cdrs)
		{
			foreach (CDRData cdrdata in cdrs)
			{
				UMCallDataRecord umcallDataRecord = new UMCallDataRecord(this.DataObject.Identity);
				umcallDataRecord.Date = cdrdata.CallStartTime;
				umcallDataRecord.Duration = TimeSpan.FromSeconds((double)cdrdata.CallDuration);
				umcallDataRecord.DialPlan = cdrdata.DialPlanName;
				umcallDataRecord.CallType = cdrdata.CallType;
				umcallDataRecord.CallingNumber = cdrdata.CallerPhoneNumber;
				if (!string.IsNullOrEmpty(cdrdata.DialedString))
				{
					umcallDataRecord.CalledNumber = cdrdata.DialedString;
				}
				else
				{
					umcallDataRecord.CalledNumber = cdrdata.CalledPhoneNumber;
				}
				umcallDataRecord.Gateway = cdrdata.IPGatewayName;
				umcallDataRecord.UserMailboxName = this.userMailbox.DisplayName;
				umcallDataRecord.AudioCodec = cdrdata.AudioQualityMetrics.AudioCodec;
				umcallDataRecord.NMOS = Utils.GetNullableAudioQualityMetric(cdrdata.AudioQualityMetrics.NMOS);
				umcallDataRecord.NMOSDegradation = Utils.GetNullableAudioQualityMetric(cdrdata.AudioQualityMetrics.NMOSDegradation);
				umcallDataRecord.PercentPacketLoss = Utils.GetNullableAudioQualityMetric(cdrdata.AudioQualityMetrics.PacketLoss);
				umcallDataRecord.Jitter = Utils.GetNullableAudioQualityMetric(cdrdata.AudioQualityMetrics.Jitter);
				umcallDataRecord.RoundTripMilliseconds = Utils.GetNullableAudioQualityMetric(cdrdata.AudioQualityMetrics.RoundTrip);
				umcallDataRecord.BurstLossDurationMilliseconds = Utils.GetNullableAudioQualityMetric(cdrdata.AudioQualityMetrics.BurstDuration);
				this.userMailbox.ResetChangeTracking();
				base.WriteObject(umcallDataRecord);
			}
		}

		// Token: 0x04003F11 RID: 16145
		private ADUser userMailbox;
	}
}
