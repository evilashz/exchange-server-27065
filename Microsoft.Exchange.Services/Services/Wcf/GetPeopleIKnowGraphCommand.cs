using System;
using Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000924 RID: 2340
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPeopleIKnowGraphCommand : ServiceCommand<GetPeopleIKnowGraphResponse>
	{
		// Token: 0x060043D2 RID: 17362 RVA: 0x000E6738 File Offset: 0x000E4938
		public GetPeopleIKnowGraphCommand(CallContext context, GetPeopleIKnowGraphRequest request) : base(context)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			this.InitializeTracers();
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000E6768 File Offset: 0x000E4968
		protected override GetPeopleIKnowGraphResponse InternalExecute()
		{
			IPeopleIKnowSerializerFactory serializerFactory = new PeopleIKnowSerializerFactory();
			IPeopleIKnowServiceFactory peopleIKnowServiceFactory = new PeopleIKnowServiceFactory(serializerFactory);
			IPeopleIKnowService peopleIKnowService = peopleIKnowServiceFactory.CreatePeopleIKnowService(this.tracer);
			string serializedPeopleIKnowGraph = peopleIKnowService.GetSerializedPeopleIKnowGraph(base.MailboxIdentityMailboxSession, new XSOFactory());
			return new GetPeopleIKnowGraphResponse
			{
				SerializedPeopleIKnowGraph = serializedPeopleIKnowGraph
			};
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000E67B4 File Offset: 0x000E49B4
		protected override void LogTracesForCurrentRequest()
		{
			IOutgoingWebResponseContext outgoingWebResponseContext = base.CallContext.CreateWebResponseContext();
			WcfServiceCommandBase.TraceLoggerFactory.Create(outgoingWebResponseContext.Headers).LogTraces(this.requestTracer);
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x000E67E8 File Offset: 0x000E49E8
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
				tracer = new InMemoryTracer(ExTraceGlobals.GetPeopleIKnowGraphCallTracer.Category, ExTraceGlobals.GetPeopleIKnowGraphCallTracer.TraceTag);
			}
			this.requestTracer = tracer;
			this.tracer = ExTraceGlobals.GetPeopleIKnowGraphCallTracer.Compose(this.requestTracer);
		}

		// Token: 0x0400278D RID: 10125
		private ITracer tracer = ExTraceGlobals.GetPeopleIKnowGraphCallTracer;

		// Token: 0x0400278E RID: 10126
		private ITracer requestTracer = NullTracer.Instance;
	}
}
