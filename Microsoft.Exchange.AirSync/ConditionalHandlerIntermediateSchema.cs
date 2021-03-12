using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007A RID: 122
	internal class ConditionalHandlerIntermediateSchema : ObjectSchema
	{
		// Token: 0x040004A5 RID: 1189
		public static readonly SimpleProviderPropertyDefinition HandlerStartTime = ConditionalHandlerSchema.BuildValueTypePropDef<ExDateTime>("_HandlerStartTime");

		// Token: 0x040004A6 RID: 1190
		public static readonly SimpleProviderPropertyDefinition ProxyStartTime = ConditionalHandlerSchema.BuildValueTypePropDef<ExDateTime>("_ProxyStartTime");

		// Token: 0x040004A7 RID: 1191
		public static readonly SimpleProviderPropertyDefinition PostWlmStartTime = ConditionalHandlerSchema.BuildValueTypePropDef<ExDateTime>("_PostWlmStartTime");
	}
}
