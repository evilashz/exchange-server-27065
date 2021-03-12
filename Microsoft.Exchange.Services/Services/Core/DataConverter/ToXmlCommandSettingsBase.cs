using System;
using System.Xml;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000FA RID: 250
	internal abstract class ToXmlCommandSettingsBase : CommandSettings
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00022B55 File Offset: 0x00020D55
		public ToXmlCommandSettingsBase()
		{
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00022B5D File Offset: 0x00020D5D
		public ToXmlCommandSettingsBase(PropertyPath propertyPath)
		{
			this.propertyPath = propertyPath;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00022B6C File Offset: 0x00020D6C
		public PropertyPath PropertyPath
		{
			get
			{
				return this.propertyPath;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00022B74 File Offset: 0x00020D74
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00022B7C File Offset: 0x00020D7C
		public XmlElement ServiceItem
		{
			get
			{
				return this.serviceItem;
			}
			set
			{
				this.serviceItem = value;
			}
		}

		// Token: 0x040006DC RID: 1756
		private PropertyPath propertyPath;

		// Token: 0x040006DD RID: 1757
		private XmlElement serviceItem;
	}
}
