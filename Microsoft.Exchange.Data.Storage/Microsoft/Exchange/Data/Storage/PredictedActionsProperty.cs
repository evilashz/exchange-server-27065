using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA6 RID: 3238
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PredictedActionsProperty : SmartPropertyDefinition
	{
		// Token: 0x060070E0 RID: 28896 RVA: 0x001F4E88 File Offset: 0x001F3088
		internal PredictedActionsProperty(string displayName, NativeStorePropertyDefinition predictedActionsProperty, PropertyFlags propertyFlags) : base(displayName, typeof(PredictedActionAndProbability[]), propertyFlags, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(predictedActionsProperty, PropertyDependencyType.AllRead)
		})
		{
			Util.ThrowOnNullArgument(predictedActionsProperty, "predictedActionsProperty");
			this.predictedActionsProperty = predictedActionsProperty;
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x001F4ED0 File Offset: 0x001F30D0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object obj = propertyBag.GetValue(this.predictedActionsProperty);
			if (!(obj is PropertyError))
			{
				short[] array = (short[])obj;
				int num = array.Length;
				PredictedActionAndProbability[] array2 = new PredictedActionAndProbability[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = new PredictedActionAndProbability(array[i]);
				}
				obj = array2;
			}
			return obj;
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x001F4F30 File Offset: 0x001F3130
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("FlagStatusProperty");
			}
			if (base.PropertyFlags != PropertyFlags.ReadOnly)
			{
				PredictedActionAndProbability[] array = (PredictedActionAndProbability[])value;
				short[] array2 = new short[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i].RawActionAndProbability;
				}
				propertyBag.SetValue(this.predictedActionsProperty, array2);
			}
		}

		// Token: 0x04004E8A RID: 20106
		private NativeStorePropertyDefinition predictedActionsProperty;
	}
}
