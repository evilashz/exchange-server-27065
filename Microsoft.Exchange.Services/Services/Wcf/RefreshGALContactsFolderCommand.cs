using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Entities.People.EntitySets.ContactCommands;
using Microsoft.Exchange.Entities.People.EntitySets.ResponseTypes;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000931 RID: 2353
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RefreshGALContactsFolderCommand : SingleStepServiceCommand<RefreshGALContactsFolderRequest, RefreshGALFolderResponseEntity>
	{
		// Token: 0x0600444A RID: 17482 RVA: 0x000E9D64 File Offset: 0x000E7F64
		public RefreshGALContactsFolderCommand(CallContext callContext, RefreshGALContactsFolderRequest request) : base(callContext, request)
		{
			this.InitializeTracers();
			this.perfLogger = new GALContactsRefreshRequestPerformanceLogger(base.CallContext.ProtocolLog, this.requestTracer);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x000E9DBC File Offset: 0x000E7FBC
		internal override ServiceResult<RefreshGALFolderResponseEntity> Execute()
		{
			RefreshGalFolder refreshGalFolder = new RefreshGalFolder(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), CallContext.Current.ADRecipientSessionContext.GetADRecipientSession(), this.tracer, this.perfLogger, new XSOFactory());
			return new ServiceResult<RefreshGALFolderResponseEntity>(refreshGalFolder.Execute(null));
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x000E9E0B File Offset: 0x000E800B
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RefreshGALContactsFolderResponse(base.Result);
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x000E9E18 File Offset: 0x000E8018
		protected override void LogTracesForCurrentRequest()
		{
			IOutgoingWebResponseContext outgoingWebResponseContext = base.CallContext.CreateWebResponseContext();
			ServiceCommandBase.TraceLoggerFactory.Create(outgoingWebResponseContext.Headers).LogTraces(this.requestTracer);
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000E9E4C File Offset: 0x000E804C
		private void InitializeTracers()
		{
			ITracer tracer;
			if (!base.IsRequestTracingEnabled)
			{
				ITracer instance = NullTracer.Instance;
				tracer = instance;
			}
			else
			{
				tracer = new InMemoryTracer(ExTraceGlobals.RefreshGALContactsFolderTracer.Category, ExTraceGlobals.RefreshGALContactsFolderTracer.TraceTag);
			}
			this.requestTracer = tracer;
			this.tracer = ExTraceGlobals.RefreshGALContactsFolderTracer.Compose(this.requestTracer);
		}

		// Token: 0x040027C6 RID: 10182
		private ITracer tracer = ExTraceGlobals.RefreshGALContactsFolderTracer;

		// Token: 0x040027C7 RID: 10183
		private ITracer requestTracer = NullTracer.Instance;

		// Token: 0x040027C8 RID: 10184
		private readonly IPerformanceDataLogger perfLogger = NullPerformanceDataLogger.Instance;
	}
}
