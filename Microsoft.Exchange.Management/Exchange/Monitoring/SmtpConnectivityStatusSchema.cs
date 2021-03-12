using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C7 RID: 1479
	internal class SmtpConnectivityStatusSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040023F1 RID: 9201
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023F2 RID: 9202
		public static readonly SimpleProviderPropertyDefinition ReceiveConnector = new SimpleProviderPropertyDefinition("ReceiveConnector", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023F3 RID: 9203
		public static readonly SimpleProviderPropertyDefinition Binding = new SimpleProviderPropertyDefinition("Binding", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023F4 RID: 9204
		public static readonly SimpleProviderPropertyDefinition EndPoint = new SimpleProviderPropertyDefinition("EndPoint", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023F5 RID: 9205
		public static readonly SimpleProviderPropertyDefinition StatusCode = new SimpleProviderPropertyDefinition("StatusCode", ExchangeObjectVersion.Exchange2007, typeof(SmtpConnectivityStatusCode), PropertyDefinitionFlags.None, SmtpConnectivityStatusCode.NotTested, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023F6 RID: 9206
		public static readonly SimpleProviderPropertyDefinition Details = new SimpleProviderPropertyDefinition("Details", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
