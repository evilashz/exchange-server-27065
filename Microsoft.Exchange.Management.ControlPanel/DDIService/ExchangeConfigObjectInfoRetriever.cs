using System;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000134 RID: 308
	public class ExchangeConfigObjectInfoRetriever : IDataObjectInfoRetriever
	{
		// Token: 0x060020EB RID: 8427 RVA: 0x00063F28 File Offset: 0x00062128
		public virtual void Retrieve(Type dataObjectType, string propertyName, out Type objectType, out PropertyDefinition propertyDefinition)
		{
			PropertyInfo propertyEx = dataObjectType.GetPropertyEx(propertyName);
			if (propertyEx != null)
			{
				objectType = propertyEx.PropertyType;
			}
			else
			{
				objectType = null;
			}
			propertyDefinition = PropertyConstraintProvider.GetPropertyDefinition(dataObjectType, propertyName);
		}
	}
}
