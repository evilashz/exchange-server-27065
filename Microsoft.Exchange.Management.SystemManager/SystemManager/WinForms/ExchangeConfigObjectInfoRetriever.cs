using System;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A3 RID: 163
	public class ExchangeConfigObjectInfoRetriever : IDataObjectInfoRetriever
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00014170 File Offset: 0x00012370
		public virtual void Retrieve(Type dataObjectType, string propertyName, out Type objectType)
		{
			PropertyInfo property = dataObjectType.GetProperty(propertyName);
			if (property != null)
			{
				objectType = property.PropertyType;
				return;
			}
			objectType = null;
		}
	}
}
