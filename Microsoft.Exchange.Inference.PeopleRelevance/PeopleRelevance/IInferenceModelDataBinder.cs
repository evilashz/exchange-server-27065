using System;
using Microsoft.Exchange.Inference.Common;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000004 RID: 4
	internal interface IInferenceModelDataBinder<TInferenceModelItem> where TInferenceModelItem : InferenceBaseModelItem
	{
		// Token: 0x06000014 RID: 20
		TInferenceModelItem GetModelData();

		// Token: 0x06000015 RID: 21
		long SaveModelData(TInferenceModelItem data);
	}
}
