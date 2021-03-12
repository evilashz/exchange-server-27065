using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200000E RID: 14
	internal class PopImapConditionalHandlerSchema : ConditionalHandlerSchema
	{
		// Token: 0x04000067 RID: 103
		public static readonly SimpleProviderPropertyDefinition RequestId = ConditionalHandlerSchema.BuildStringPropDef("RequestId");

		// Token: 0x04000068 RID: 104
		public static readonly SimpleProviderPropertyDefinition Parameters = ConditionalHandlerSchema.BuildStringPropDef("Parameters");

		// Token: 0x04000069 RID: 105
		public static readonly SimpleProviderPropertyDefinition Response = ConditionalHandlerSchema.BuildValueTypePropDef<int>("Response");

		// Token: 0x0400006A RID: 106
		public static readonly SimpleProviderPropertyDefinition ResponseType = ConditionalHandlerSchema.BuildValueTypePropDef<int>("ResponseType");

		// Token: 0x0400006B RID: 107
		public static readonly SimpleProviderPropertyDefinition LightLogContext = ConditionalHandlerSchema.BuildStringPropDef("LightLogContext");

		// Token: 0x0400006C RID: 108
		public static readonly SimpleProviderPropertyDefinition Message = ConditionalHandlerSchema.BuildStringPropDef("Message");

		// Token: 0x0400006D RID: 109
		public static readonly SimpleProviderPropertyDefinition Traces = ConditionalHandlerSchema.BuildStringPropDef("Traces");
	}
}
