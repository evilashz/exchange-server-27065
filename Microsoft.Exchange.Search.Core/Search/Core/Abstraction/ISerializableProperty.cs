using System;
using System.IO;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200003D RID: 61
	internal interface ISerializableProperty
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000128 RID: 296
		SerializablePropertyType Type { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000129 RID: 297
		SerializablePropertyId Id { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012A RID: 298
		object Value { get; }

		// Token: 0x0600012B RID: 299
		void Serialize(BinaryWriter writer);

		// Token: 0x0600012C RID: 300
		void Deserialize(BinaryReader reader);
	}
}
