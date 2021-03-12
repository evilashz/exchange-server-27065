using System;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C9 RID: 2505
	internal class EndInstantSearchSession : SingleStepServiceCommand<EndInstantSearchSessionRequest, EndInstantSearchSessionResponse>
	{
		// Token: 0x060046E5 RID: 18149 RVA: 0x000FC3AC File Offset: 0x000FA5AC
		public EndInstantSearchSession(CallContext callContext, EndInstantSearchSessionRequest request) : base(callContext, request)
		{
			ServiceCommandBase.ThrowIfNull(request.DeviceId, "deviceId", "ServiceCommand::EndInstantSearchSession");
			ServiceCommandBase.ThrowIfNull(request.SessionId, "sessionId", "ServiceCommand::EndInstantSearchSession");
			this.instantSearchManager = PerformInstantSearch.GetManagerForCaller(callContext, request.DeviceId);
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x000FC3FD File Offset: 0x000FA5FD
		public EndInstantSearchSession(CallContext callContext, EndInstantSearchSessionRequest request, InstantSearchManager manager) : base(callContext, request)
		{
			ServiceCommandBase.ThrowIfNull(this.instantSearchManager, "instantSearchManager", "ServiceCommand::EndInstantSearchSession");
			ServiceCommandBase.ThrowIfNull(request.SessionId, "sessionId", "ServiceCommand::EndInstantSearchSession");
			this.instantSearchManager = manager;
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000FC438 File Offset: 0x000FA638
		internal override ServiceResult<EndInstantSearchSessionResponse> Execute()
		{
			EndInstantSearchSessionResponse value = this.instantSearchManager.EndSearchSession(base.Request.SessionId);
			if (base.Request.DeviceId != null)
			{
				PerformInstantSearch.RemoveManagerForCaller(base.CallContext, base.Request.DeviceId);
			}
			return new ServiceResult<EndInstantSearchSessionResponse>(value);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x000FC485 File Offset: 0x000FA685
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x040028B9 RID: 10425
		private readonly InstantSearchManager instantSearchManager;
	}
}
