using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200005B RID: 91
	public interface IDataObjectInfoRetriever
	{
		// Token: 0x060003AD RID: 941
		void Retrieve(Type dataObjectType, string propertyName, out Type type);
	}
}
