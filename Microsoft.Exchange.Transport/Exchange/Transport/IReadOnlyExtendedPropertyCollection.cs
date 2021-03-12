using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000E5 RID: 229
	internal interface IReadOnlyExtendedPropertyCollection
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600082C RID: 2092
		int Count { get; }

		// Token: 0x0600082D RID: 2093
		bool Contains(string name);

		// Token: 0x0600082E RID: 2094
		T GetValue<T>(string name, T defaultValue);

		// Token: 0x0600082F RID: 2095
		void Serialize(Stream stream);

		// Token: 0x06000830 RID: 2096
		bool TryGetListValue<ItemT>(string name, out ReadOnlyCollection<ItemT> value);

		// Token: 0x06000831 RID: 2097
		bool TryGetValue<T>(string name, out T value);
	}
}
