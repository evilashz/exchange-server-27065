using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C1A RID: 3098
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class AutoResponseFlagProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E5E RID: 28254 RVA: 0x001DA9C4 File Offset: 0x001D8BC4
		internal AutoResponseFlagProperty(string displayName, MessageFlags flag, AutoResponseSuppress suppressFlag) : base(displayName, typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.Flags, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.AutoResponseSuppressInternal, PropertyDependencyType.NeedForRead)
		})
		{
			this.suppressMask = suppressFlag;
			this.nativeFlag = flag;
		}

		// Token: 0x06006E5F RID: 28255 RVA: 0x001DAA1C File Offset: 0x001D8C1C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			bool flagValue = AutoResponseFlagProperty.GetFlagValue(propertyBag, InternalSchema.Flags, (int)this.nativeFlag);
			bool flagValue2 = AutoResponseFlagProperty.GetFlagValue(propertyBag, InternalSchema.AutoResponseSuppressInternal, (int)this.suppressMask);
			return flagValue && !flagValue2;
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x001DAA5C File Offset: 0x001D8C5C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06006E61 RID: 28257 RVA: 0x001DAA64 File Offset: 0x001D8C64
		private static bool GetFlagValue(PropertyBag.BasicPropertyStore propertyBag, NativeStorePropertyDefinition prop, int flag)
		{
			int num;
			return Util.TryConvertTo<int>(propertyBag.GetValue(prop), out num) && (num & flag) == flag;
		}

		// Token: 0x0400409A RID: 16538
		private readonly AutoResponseSuppress suppressMask;

		// Token: 0x0400409B RID: 16539
		private readonly MessageFlags nativeFlag;
	}
}
