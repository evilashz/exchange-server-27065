using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000133 RID: 307
	public interface IDataObjectInfoRetriever
	{
		// Token: 0x060020EA RID: 8426
		void Retrieve(Type dataObjectType, string propertyName, out Type type, out PropertyDefinition propertyDefinition);
	}
}
