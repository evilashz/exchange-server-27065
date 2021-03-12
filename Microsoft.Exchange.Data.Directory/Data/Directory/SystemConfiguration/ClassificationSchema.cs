using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B3 RID: 1203
	internal sealed class ClassificationSchema : ADConfigurationObjectSchema
	{
		// Token: 0x060036E4 RID: 14052 RVA: 0x000D6F70 File Offset: 0x000D5170
		private static void PermissionMenuVisibleSetter(object o, IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ClassificationSchema.ClassificationFlags];
			propertyBag[ClassificationSchema.ClassificationFlags] = (((bool)o) ? (num | 1) : (num & -2));
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000D6FB0 File Offset: 0x000D51B0
		private static object PermissionMenuVisibleGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ClassificationSchema.ClassificationFlags];
			return (num & 1) == 1;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000D6FDC File Offset: 0x000D51DC
		private static void RetainClassificationEnabledSetter(object o, IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ClassificationSchema.ClassificationFlags];
			propertyBag[ClassificationSchema.ClassificationFlags] = (((bool)o) ? (num | 2) : (num & -3));
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000D701C File Offset: 0x000D521C
		private static object RetainClassificationEnabledGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ClassificationSchema.ClassificationFlags];
			return (num & 2) == 2;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000D7048 File Offset: 0x000D5248
		private static object LocaleGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId == null)
			{
				return null;
			}
			string name = adobjectId.Parent.Name;
			if (!(name == "Default"))
			{
				return name;
			}
			return null;
		}

		// Token: 0x0400251F RID: 9503
		private const int FlagDisplayable = 1;

		// Token: 0x04002520 RID: 9504
		private const int FlagRetainClassification = 2;

		// Token: 0x04002521 RID: 9505
		public static readonly ADPropertyDefinition ClassificationID = new ADPropertyDefinition("classificationID", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchMessageClassificationID", ADPropertyDefinitionFlags.WriteOnce | ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002522 RID: 9506
		public static readonly ADPropertyDefinition Version = new ADPropertyDefinition("version", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageClassificationVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002523 RID: 9507
		public static readonly ADPropertyDefinition ClassificationFlags = new ADPropertyDefinition("flags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageClassificationFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002524 RID: 9508
		public static readonly ADPropertyDefinition Locale = new ADPropertyDefinition("locale", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMessageClassificationLocale", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ClassificationSchema.LocaleGetter), null, null, null);

		// Token: 0x04002525 RID: 9509
		public static readonly ADPropertyDefinition DisplayName = new ADPropertyDefinition("displayName", ExchangeObjectVersion.Exchange2007, typeof(string), "displayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002526 RID: 9510
		public static readonly ADPropertyDefinition SenderDescription = new ADPropertyDefinition("senderDescription", ExchangeObjectVersion.Exchange2007, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002527 RID: 9511
		public static readonly ADPropertyDefinition RecipientDescription = new ADPropertyDefinition("recipientDescription", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMessageClassificationBanner", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002528 RID: 9512
		public static readonly ADPropertyDefinition DisplayPrecedence = new ADPropertyDefinition("displayPrecedence", ExchangeObjectVersion.Exchange2007, typeof(ClassificationDisplayPrecedenceLevel), "msExchMessageClassificationDisplayPrecedence", ADPropertyDefinitionFlags.None, ClassificationDisplayPrecedenceLevel.Medium, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ClassificationDisplayPrecedenceLevel))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002529 RID: 9513
		public static readonly ADPropertyDefinition PermissionMenuVisible = new ADPropertyDefinition("permissionMenuVisible", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ClassificationSchema.ClassificationFlags
		}, null, new GetterDelegate(ClassificationSchema.PermissionMenuVisibleGetter), new SetterDelegate(ClassificationSchema.PermissionMenuVisibleSetter), null, null);

		// Token: 0x0400252A RID: 9514
		public static readonly ADPropertyDefinition RetainClassificationEnabled = new ADPropertyDefinition("retainClassification", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ClassificationSchema.ClassificationFlags
		}, null, new GetterDelegate(ClassificationSchema.RetainClassificationEnabledGetter), new SetterDelegate(ClassificationSchema.RetainClassificationEnabledSetter), null, null);
	}
}
