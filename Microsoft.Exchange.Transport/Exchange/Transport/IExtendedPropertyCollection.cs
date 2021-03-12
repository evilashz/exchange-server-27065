using System;
using System.IO;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000E6 RID: 230
	internal interface IExtendedPropertyCollection : IReadOnlyExtendedPropertyCollection
	{
		// Token: 0x06000832 RID: 2098
		void Deserialize(Stream stream, int numberOfExtendedPropertiesToFetch, bool doNotAddPropertyIfPresent);

		// Token: 0x06000833 RID: 2099
		bool Remove(string name);

		// Token: 0x06000834 RID: 2100
		void SetValue<T>(string name, T value);

		// Token: 0x06000835 RID: 2101
		void Clear();
	}
}
