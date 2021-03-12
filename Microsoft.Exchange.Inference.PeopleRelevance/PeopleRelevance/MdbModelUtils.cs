using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200000E RID: 14
	internal static class MdbModelUtils
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002FD0 File Offset: 0x000011D0
		internal static TModelItem GetModelItem<TModelItem, TInferenceModelDataBinder>(TInferenceModelDataBinder dataBinder) where TModelItem : InferenceBaseModelItem where TInferenceModelDataBinder : IInferenceModelDataBinder<TModelItem>
		{
			TModelItem tmodelItem = XsoUtil.MapXsoExceptions<TModelItem>(() => dataBinder.GetModelData());
			ExAssert.RetailAssert(tmodelItem != null, "Data binder returned a null Inference model item");
			return tmodelItem;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003038 File Offset: 0x00001238
		internal static void WriteModelItem<TModelItem, TInferenceModelDataBinder>(TInferenceModelDataBinder dataBinder, TModelItem modelItem) where TModelItem : InferenceBaseModelItem where TInferenceModelDataBinder : IInferenceModelDataBinder<TModelItem>
		{
			XsoUtil.MapXsoExceptions(delegate()
			{
				dataBinder.SaveModelData(modelItem);
			});
		}
	}
}
