using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200088A RID: 2186
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversationAggregationLogConfiguration : LogConfigurationBase
	{
		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06005209 RID: 21001 RVA: 0x00157384 File Offset: 0x00155584
		public static ConversationAggregationLogConfiguration Default
		{
			get
			{
				if (ConversationAggregationLogConfiguration.defaultInstance == null)
				{
					ConversationAggregationLogConfiguration.defaultInstance = new ConversationAggregationLogConfiguration();
				}
				return ConversationAggregationLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x0015739C File Offset: 0x0015559C
		protected override string Component
		{
			get
			{
				return "ConversationAggregationLog";
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x001573A3 File Offset: 0x001555A3
		protected override string Type
		{
			get
			{
				return "Conversation Aggregation Log";
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x0600520C RID: 21004 RVA: 0x001573AA File Offset: 0x001555AA
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ConversationAggregationTracer;
			}
		}

		// Token: 0x04002C98 RID: 11416
		private static ConversationAggregationLogConfiguration defaultInstance;
	}
}
