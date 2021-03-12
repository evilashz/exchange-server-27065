using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000143 RID: 323
	internal interface IExtendedPropertyStore<T> where T : PropertyBase
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C7F RID: 3199
		int ExtendedPropertiesCount { get; }

		// Token: 0x06000C80 RID: 3200
		bool TryGetExtendedProperty(string nameSpace, string name, out T extendedProperty);

		// Token: 0x06000C81 RID: 3201
		T GetExtendedProperty(string nameSpace, string name);

		// Token: 0x06000C82 RID: 3202
		IEnumerable<T> GetExtendedPropertiesEnumerable();

		// Token: 0x06000C83 RID: 3203
		void AddExtendedProperty(T extendedProperty);
	}
}
