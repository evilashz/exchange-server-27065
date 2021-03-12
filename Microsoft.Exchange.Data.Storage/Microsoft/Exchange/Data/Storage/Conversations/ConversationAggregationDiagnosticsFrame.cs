using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000889 RID: 2185
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationAggregationDiagnosticsFrame : IConversationAggregationDiagnosticsFrame
	{
		// Token: 0x06005202 RID: 20994 RVA: 0x00157286 File Offset: 0x00155486
		public ConversationAggregationDiagnosticsFrame(IMailboxSession session)
		{
			this.session = session;
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00157298 File Offset: 0x00155498
		public ConversationAggregationResult TrackAggregation(string operationName, AggregationDelegate aggregation)
		{
			ConversationAggregationResult result;
			using (this.CreateDiagnosticsFrame("ConversationAggregation", operationName))
			{
				try
				{
					result = aggregation(this.Logger);
				}
				catch (Exception value)
				{
					this.Logger.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.Error>
					{
						{
							ConversationAggregationLogSchema.Error.Context,
							base.GetType().Name
						},
						{
							ConversationAggregationLogSchema.Error.Exception,
							value
						}
					});
					throw;
				}
			}
			return result;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x00157318 File Offset: 0x00155518
		private IDiagnosticsFrame CreateDiagnosticsFrame(string operationContext, string operationName)
		{
			return new DiagnosticsFrame(operationContext, operationName, ConversationAggregationDiagnosticsFrame.Tracer, this.Logger, this.CreatePerformanceTracker());
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00157332 File Offset: 0x00155532
		private IConversationAggregationLogger CreateLogger()
		{
			return new ConversationAggregationLogger(this.session.MailboxGuid, this.session.OrganizationId);
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x0015734F File Offset: 0x0015554F
		private IMailboxPerformanceTracker CreatePerformanceTracker()
		{
			return new ConversationAggregationPerformanceTracker(this.session);
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x0015735C File Offset: 0x0015555C
		private IConversationAggregationLogger Logger
		{
			get
			{
				if (this.logger == null)
				{
					this.logger = this.CreateLogger();
				}
				return this.logger;
			}
		}

		// Token: 0x04002C94 RID: 11412
		private const string OperationContext = "ConversationAggregation";

		// Token: 0x04002C95 RID: 11413
		private static readonly Trace Tracer = ExTraceGlobals.ConversationTracer;

		// Token: 0x04002C96 RID: 11414
		private readonly IMailboxSession session;

		// Token: 0x04002C97 RID: 11415
		private IConversationAggregationLogger logger;
	}
}
