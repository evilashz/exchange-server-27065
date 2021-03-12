using System;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A5 RID: 677
	internal abstract class BaseProvisionCommand<RequestType, ResponseType> : SingleStepServiceCommand<RequestType, ServiceResultNone> where RequestType : BaseRequest where ResponseType : BaseResponseMessage, new()
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00058C57 File Offset: 0x00056E57
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x00058C5F File Offset: 0x00056E5F
		private protected string Protocol { protected get; private set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00058C68 File Offset: 0x00056E68
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x00058C70 File Offset: 0x00056E70
		private protected string DeviceType { protected get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00058C79 File Offset: 0x00056E79
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x00058C81 File Offset: 0x00056E81
		private protected string DeviceID { protected get; private set; }

		// Token: 0x06001219 RID: 4633 RVA: 0x00058C8C File Offset: 0x00056E8C
		protected BaseProvisionCommand(CallContext callContext, RequestType request, bool hasPal, string deviceType, string deviceId, bool specifyProtocol, string protocol) : base(callContext, request)
		{
			if (hasPal)
			{
				this.Protocol = "MOWA";
			}
			else if (!specifyProtocol)
			{
				this.Protocol = "DOWA";
			}
			else
			{
				this.Protocol = protocol;
			}
			this.DeviceID = deviceId;
			this.DeviceType = deviceType;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00058CDC File Offset: 0x00056EDC
		internal override IExchangeWebMethodResponse GetResponse()
		{
			ResponseType responseType = Activator.CreateInstance<ResponseType>();
			responseType.ProcessServiceResult(base.Result);
			return responseType;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00058D08 File Offset: 0x00056F08
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			if (!this.IsDeviceIDValid())
			{
				return new ServiceResult<ServiceResultNone>(new ServiceError((CoreResources.IDs)3374101509U, ResponseCodeType.ErrorInvalidArgument, 0, ExchangeVersion.Exchange2012));
			}
			if (!this.IsDeviceTypeValid())
			{
				return new ServiceResult<ServiceResultNone>(new ServiceError(CoreResources.IDs.ErrorInvalidProvisionDeviceType, ResponseCodeType.ErrorInvalidArgument, 0, ExchangeVersion.Exchange2012));
			}
			try
			{
				this.InternalExecute();
			}
			catch (AirSyncPermanentException ex)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, ex, "BaseProvisioningCommand_Execute");
				if (ex.AirSyncStatusCode == StatusCode.MaximumDevicesReached)
				{
					return new ServiceResult<ServiceResultNone>(new ServiceError(CoreResources.IDs.ErrorInternalServerError, ResponseCodeType.ErrorMaximumDevicesReached, 0, ExchangeVersion.Exchange2013));
				}
				return new ServiceResult<ServiceResultNone>(new ServiceError(CoreResources.IDs.ErrorInternalServerError, ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2013));
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x0600121C RID: 4636
		protected abstract void InternalExecute();

		// Token: 0x0600121D RID: 4637 RVA: 0x00058DEC File Offset: 0x00056FEC
		private bool IsDeviceIDValid()
		{
			return !string.IsNullOrEmpty(this.DeviceID) && this.DeviceID.Length <= 32 && this.DeviceID.IndexOf('-') < 0;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00058E1C File Offset: 0x0005701C
		private bool IsDeviceTypeValid()
		{
			return !string.IsNullOrEmpty(this.DeviceType) && this.DeviceType.Length <= 32 && this.DeviceType.IndexOf('-') < 0;
		}
	}
}
