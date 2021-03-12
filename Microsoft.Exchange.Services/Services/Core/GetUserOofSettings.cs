using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200033C RID: 828
	internal sealed class GetUserOofSettings : AvailabilityCommandBase<GetUserOofSettingsRequest, GetUserOofSettingsResponse>
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x0007B9A4 File Offset: 0x00079BA4
		public GetUserOofSettings(CallContext callContext, GetUserOofSettingsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0007B9B0 File Offset: 0x00079BB0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			if (base.Result.Value == null)
			{
				return new GetUserOofSettingsResponse
				{
					ResponseMessage = new ResponseMessage(base.Result.Code, base.Result.Error),
					EmitAllowExternalOof = false
				};
			}
			return base.Result.Value;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0007BA08 File Offset: 0x00079C08
		internal override ServiceResult<GetUserOofSettingsResponse> Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			PerformanceContext ldapInitialPerformanceContext = new PerformanceContext(PerformanceContext.Current);
			PerformanceContext rpcInitialPerformanceContext;
			NativeMethods.GetTLSPerformanceContext(out rpcInitialPerformanceContext);
			this.EmailAddress = base.Request.Mailbox;
			ServiceResult<GetUserOofSettingsResponse> userOofSettingsExecute;
			try
			{
				userOofSettingsExecute = this.GetUserOofSettingsExecute();
			}
			finally
			{
				base.LogLatency(ldapInitialPerformanceContext, rpcInitialPerformanceContext);
				stopwatch.Stop();
				base.CallContext.ProtocolLog.AppendGenericInfo("TimeInGetUserOOFSettings", stopwatch.ElapsedMilliseconds);
			}
			return userOofSettingsExecute;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0007BA88 File Offset: 0x00079C88
		private ServiceResult<GetUserOofSettingsResponse> GetUserOofSettingsExecute()
		{
			LocalizedException ex = null;
			GetUserOofSettingsResponse getUserOofSettingsResponse = new GetUserOofSettingsResponse();
			if (this.EmailAddress == null || this.EmailAddress.Address == null)
			{
				throw FaultExceptionUtilities.CreateAvailabilityFault(new InvalidParameterException(CoreResources.descInvalidRequest), FaultParty.Sender);
			}
			try
			{
				MailboxSession mailboxSession = base.GetMailboxSession(this.EmailAddress.Address);
				if (mailboxSession == null)
				{
					ex = new InvalidParameterException(CoreResources.descInvalidOofRequestPublicFolder);
					ex.ErrorCode = 240;
				}
				else
				{
					getUserOofSettingsResponse.OofSettings = UserOofSettings.GetUserOofSettings(mailboxSession);
					getUserOofSettingsResponse.AllowExternalOof = UserOofSettings.GetUserPolicy(mailboxSession.MailboxOwner).ToString();
				}
			}
			catch (InvalidScheduledOofDuration invalidScheduledOofDuration)
			{
				ex = invalidScheduledOofDuration;
				ex.ErrorCode = 227;
			}
			catch (InvalidUserOofSettings invalidUserOofSettings)
			{
				ex = invalidUserOofSettings;
				ex.ErrorCode = 240;
			}
			getUserOofSettingsResponse.ResponseMessage = ResponseMessageBuilder.ResponseMessageFromExchangeException(ex);
			return new ServiceResult<GetUserOofSettingsResponse>(getUserOofSettingsResponse);
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0007BB6C File Offset: 0x00079D6C
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0007BB74 File Offset: 0x00079D74
		private EmailAddress EmailAddress { get; set; }
	}
}
