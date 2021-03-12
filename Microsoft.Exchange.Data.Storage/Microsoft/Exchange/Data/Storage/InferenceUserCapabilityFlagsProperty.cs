using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C6A RID: 3178
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class InferenceUserCapabilityFlagsProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FD3 RID: 28627 RVA: 0x001E1680 File Offset: 0x001DF880
		public InferenceUserCapabilityFlagsProperty(InferenceUserCapabilityFlags flag) : base(flag.ToString(), typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.InferenceUserCapabilityFlags, PropertyDependencyType.AllRead)
		})
		{
			this.propertyFlag = (int)flag;
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x001E16CC File Offset: 0x001DF8CC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			int valueOrDefault = propertyBag.GetValueOrDefault<int>(InternalSchema.InferenceUserCapabilityFlags, 0);
			return (valueOrDefault & this.propertyFlag) == this.propertyFlag;
		}

		// Token: 0x06006FD5 RID: 28629 RVA: 0x001E1700 File Offset: 0x001DF900
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object newValue)
		{
			int num = propertyBag.GetValueOrDefault<int>(InternalSchema.InferenceUserCapabilityFlags, 0);
			if ((bool)newValue)
			{
				num |= this.propertyFlag;
			}
			else
			{
				num &= ~this.propertyFlag;
			}
			propertyBag.SetOrDeleteProperty(InternalSchema.InferenceUserCapabilityFlags, (num == 0) ? null : num);
		}

		// Token: 0x040043CD RID: 17357
		private readonly int propertyFlag;
	}
}
