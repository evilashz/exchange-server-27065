using System;
using System.IO;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200002F RID: 47
	internal interface ISerializer<TModelItem> where TModelItem : InferenceBaseModelItem
	{
		// Token: 0x060000B8 RID: 184
		void Serialize(Stream stream, TModelItem modelItem);

		// Token: 0x060000B9 RID: 185
		TModelItem Deserialize(Stream stream);
	}
}
