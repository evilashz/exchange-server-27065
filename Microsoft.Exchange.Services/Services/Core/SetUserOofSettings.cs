using System;
using System.Diagnostics;
using System.ServiceModel;
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
	// Token: 0x0200037A RID: 890
	internal sealed class SetUserOofSettings : AvailabilityCommandBase<SetUserOofSettingsRequest, SetUserOofSettingsResponse>
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x00089BFC File Offset: 0x00087DFC
		public SetUserOofSettings(CallContext callContext, SetUserOofSettingsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00089C08 File Offset: 0x00087E08
		internal override IExchangeWebMethodResponse GetResponse()
		{
			if (base.Result.Value == null)
			{
				return new SetUserOofSettingsResponse
				{
					ResponseMessage = new ResponseMessage(base.Result.Code, base.Result.Error)
				};
			}
			return base.Result.Value;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00089C58 File Offset: 0x00087E58
		internal override ServiceResult<SetUserOofSettingsResponse> Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			PerformanceContext ldapInitialPerformanceContext = new PerformanceContext(PerformanceContext.Current);
			PerformanceContext rpcInitialPerformanceContext;
			NativeMethods.GetTLSPerformanceContext(out rpcInitialPerformanceContext);
			this.EmailAddress = base.Request.Mailbox;
			this.UserOofSettings = base.Request.UserOofSettings;
			ServiceResult<SetUserOofSettingsResponse> result;
			try
			{
				result = this.SetUserOofSettingsExecute();
			}
			finally
			{
				stopwatch.Stop();
				base.LogLatency(ldapInitialPerformanceContext, rpcInitialPerformanceContext);
				base.CallContext.ProtocolLog.AppendGenericInfo("TimeInSetUserOOFSettings", stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00089CEC File Offset: 0x00087EEC
		private ServiceResult<SetUserOofSettingsResponse> SetUserOofSettingsExecute()
		{
			SetUserOofSettingsResponse setUserOofSettingsResponse = new SetUserOofSettingsResponse();
			FaultException ex = null;
			LocalizedException ex2 = null;
			if (this.EmailAddress == null)
			{
				ex = FaultExceptionUtilities.CreateAvailabilityFault(new InvalidParameterException(CoreResources.descInvalidRequest), FaultParty.Sender);
			}
			else
			{
				try
				{
					MailboxSession mailboxSession = base.GetMailboxSession(this.EmailAddress.Address);
					if (mailboxSession == null)
					{
						ex2 = new InvalidParameterException(CoreResources.descInvalidOofRequestPublicFolder);
						ex2.ErrorCode = 240;
					}
					else
					{
						this.UserOofSettings.Save(mailboxSession);
					}
				}
				catch (InvalidScheduledOofDuration invalidScheduledOofDuration)
				{
					ex2 = invalidScheduledOofDuration;
					ex2.ErrorCode = 227;
				}
				catch (InvalidUserOofSettings invalidUserOofSettings)
				{
					ex2 = invalidUserOofSettings;
					ex2.ErrorCode = 240;
				}
				setUserOofSettingsResponse.ResponseMessage = ResponseMessageBuilder.ResponseMessageFromExchangeException(ex2);
			}
			if (ex != null)
			{
				throw ex;
			}
			return new ServiceResult<SetUserOofSettingsResponse>(setUserOofSettingsResponse);
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00089DB0 File Offset: 0x00087FB0
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x00089DB8 File Offset: 0x00087FB8
		private EmailAddress EmailAddress { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00089DC1 File Offset: 0x00087FC1
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x00089DC9 File Offset: 0x00087FC9
		private UserOofSettings UserOofSettings { get; set; }
	}
}
