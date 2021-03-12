using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000046 RID: 70
	public interface IStoreQueryTargetBase<T>
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000499 RID: 1177
		string Name { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600049A RID: 1178
		Type[] ParameterTypes { get; }
	}
}
