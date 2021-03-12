using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B3 RID: 691
	internal sealed class ConvertId : MultiStepServiceCommand<ConvertIdRequest, AlternateIdBase>
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x0005B139 File Offset: 0x00059339
		public ConvertId(CallContext callContext, ConvertIdRequest request) : base(callContext, request)
		{
			this.destinationFormat = request.DestinationFormat;
			this.sourceIds = request.SourceIds;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0005B15C File Offset: 0x0005935C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			ConvertIdResponse convertIdResponse = new ConvertIdResponse();
			convertIdResponse.AddResponses(base.Results);
			return convertIdResponse;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0005B17C File Offset: 0x0005937C
		internal override ServiceResult<AlternateIdBase> Execute()
		{
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2647010621U, ref num);
			if (num != 0)
			{
				Thread.Sleep(num);
			}
			AlternateIdBase alternateIdBase = this.sourceIds[base.CurrentStep];
			AlternateIdBase value = alternateIdBase.ConvertId(this.destinationFormat);
			return new ServiceResult<AlternateIdBase>(value);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0005B1C6 File Offset: 0x000593C6
		internal override int StepCount
		{
			get
			{
				return this.sourceIds.Length;
			}
		}

		// Token: 0x04000D09 RID: 3337
		private IdFormat destinationFormat;

		// Token: 0x04000D0A RID: 3338
		private AlternateIdBase[] sourceIds;
	}
}
