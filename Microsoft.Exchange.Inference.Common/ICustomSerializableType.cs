using System;
using System.IO;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200002E RID: 46
	public interface ICustomSerializableType
	{
		// Token: 0x060000B6 RID: 182
		void Serialize(BinaryWriter writer);

		// Token: 0x060000B7 RID: 183
		void Deserialize(BinaryReader reader);
	}
}
