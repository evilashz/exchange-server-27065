using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD7 RID: 3287
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ExchangeApplicationFlagsProperty : SmartPropertyDefinition
	{
		// Token: 0x060071D4 RID: 29140 RVA: 0x001F8328 File Offset: 0x001F6528
		internal ExchangeApplicationFlagsProperty(ExchangeApplicationFlags flag) : base(flag.ToString(), typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ExchangeApplicationFlags, PropertyDependencyType.AllRead)
		})
		{
			this.propertyFlag = (int)flag;
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x001F8374 File Offset: 0x001F6574
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			int valueOrDefault = propertyBag.GetValueOrDefault<int>(InternalSchema.ExchangeApplicationFlags, 0);
			return (valueOrDefault & this.propertyFlag) == this.propertyFlag;
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x001F83A8 File Offset: 0x001F65A8
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object newValue)
		{
			int num = propertyBag.GetValueOrDefault<int>(InternalSchema.ExchangeApplicationFlags, 0);
			if ((bool)newValue)
			{
				num |= this.propertyFlag;
			}
			else
			{
				num &= ~this.propertyFlag;
			}
			propertyBag.SetOrDeleteProperty(InternalSchema.ExchangeApplicationFlags, (num == 0) ? null : num);
		}

		// Token: 0x04004F10 RID: 20240
		private readonly int propertyFlag;
	}
}
