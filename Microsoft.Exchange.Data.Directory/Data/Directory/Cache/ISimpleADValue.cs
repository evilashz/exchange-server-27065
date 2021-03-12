using System;
using System.IO;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000AC RID: 172
	internal interface ISimpleADValue<T>
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000952 RID: 2386
		string Name { get; }

		// Token: 0x06000953 RID: 2387
		void Read(BinaryReader reader);

		// Token: 0x06000954 RID: 2388
		void Write(BinaryWriter writer);

		// Token: 0x06000955 RID: 2389
		bool Equals(T right);
	}
}
