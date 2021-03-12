using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.TypeConversion
{
	// Token: 0x0200007C RID: 124
	internal static class StorageEntityFactory
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00009028 File Offset: 0x00007228
		public static IItem CreateFromItem(IItem storageItem)
		{
			foreach (StorageEntityFactory.SupportedTypeInfo supportedTypeInfo in StorageEntityFactory.SupportedTypes)
			{
				if (supportedTypeInfo.StorageType.IsInstanceOfType(storageItem))
				{
					return supportedTypeInfo.Translator.ConvertToEntity(storageItem);
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x04000102 RID: 258
		private static readonly List<StorageEntityFactory.SupportedTypeInfo> SupportedTypes = new List<StorageEntityFactory.SupportedTypeInfo>
		{
			new StorageEntityFactory.SupportedTypeInfo(typeof(ICalendarItemBase), "Microsoft.Exchange.Entities.Calendaring", "Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators.EventTranslator", "Instance"),
			new StorageEntityFactory.SupportedTypeInfo(typeof(IItem), ItemTranslator<IItem, Item<ItemSchema>, ItemSchema>.Instance)
		};

		// Token: 0x0200007D RID: 125
		private class SupportedTypeInfo
		{
			// Token: 0x060002B6 RID: 694 RVA: 0x00009098 File Offset: 0x00007298
			public SupportedTypeInfo(Type storageType, IGenericItemTranslator translator)
			{
				this.StorageType = storageType;
				this.Translator = translator;
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x000090AE File Offset: 0x000072AE
			public SupportedTypeInfo(Type storageType, string assemblyName, string typeName, string getterName) : this(storageType, StorageEntityFactory.SupportedTypeInfo.ResolveTranslator(assemblyName, typeName, getterName))
			{
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060002B8 RID: 696 RVA: 0x000090C0 File Offset: 0x000072C0
			// (set) Token: 0x060002B9 RID: 697 RVA: 0x000090C8 File Offset: 0x000072C8
			public IGenericItemTranslator Translator { get; private set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060002BA RID: 698 RVA: 0x000090D1 File Offset: 0x000072D1
			// (set) Token: 0x060002BB RID: 699 RVA: 0x000090D9 File Offset: 0x000072D9
			public Type StorageType { get; private set; }

			// Token: 0x060002BC RID: 700 RVA: 0x000090E4 File Offset: 0x000072E4
			private static IGenericItemTranslator ResolveTranslator(string assemblyName, string typeName, string getterName)
			{
				AssemblyName name = Assembly.GetExecutingAssembly().GetName();
				string assemblyName2 = name.FullName.Replace(name.Name, assemblyName);
				AssemblyName assemblyRef = new AssemblyName(assemblyName2);
				Assembly assembly = Assembly.Load(assemblyRef);
				Type type = assembly.GetType(typeName);
				PropertyInfo property = type.GetProperty(getterName);
				return (IGenericItemTranslator)property.GetValue(null);
			}
		}
	}
}
