using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000006 RID: 6
	internal class InMemoryObjectSchema : ObjectSchema
	{
		// Token: 0x04000022 RID: 34
		public static readonly SimplePropertyDefinition Identity = new SimplePropertyDefinition("Identity", typeof(ObjectId), null);

		// Token: 0x04000023 RID: 35
		public static readonly SimplePropertyDefinition ObjectState = new SimplePropertyDefinition("ObjectState", typeof(ObjectState), Microsoft.Exchange.Data.ObjectState.New);

		// Token: 0x04000024 RID: 36
		public static readonly SimplePropertyDefinition ExchangeVersion = new SimplePropertyDefinition("ExchangeVersion", typeof(ExchangeObjectVersion), ExchangeObjectVersion.Exchange2010);
	}
}
