using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000EE RID: 238
	[ConfigurationCollection(typeof(DiagnosticsComponent), AddItemName = "DiagnosticsComponent", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class DiagnosticsComponents : ConfigurationElementCollection
	{
		// Token: 0x17000289 RID: 649
		public DiagnosticsComponent this[int index]
		{
			get
			{
				return base.BaseGet(index) as DiagnosticsComponent;
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00026923 File Offset: 0x00024B23
		protected override ConfigurationElement CreateNewElement()
		{
			return new DiagnosticsComponent();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002692A File Offset: 0x00024B2A
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as DiagnosticsComponent).Name;
		}
	}
}
