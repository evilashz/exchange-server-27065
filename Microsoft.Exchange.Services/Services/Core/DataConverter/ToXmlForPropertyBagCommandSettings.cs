using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000FC RID: 252
	internal class ToXmlForPropertyBagCommandSettings : ToXmlCommandSettingsBase
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x00022BC9 File Offset: 0x00020DC9
		public ToXmlForPropertyBagCommandSettings()
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00022BD1 File Offset: 0x00020DD1
		public ToXmlForPropertyBagCommandSettings(PropertyPath propertyPath) : base(propertyPath)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00022BDA File Offset: 0x00020DDA
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00022BE2 File Offset: 0x00020DE2
		public IDictionary<PropertyDefinition, object> PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
			set
			{
				this.propertyBag = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00022BEB File Offset: 0x00020DEB
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x00022BF3 File Offset: 0x00020DF3
		public IdAndSession IdAndSession
		{
			get
			{
				return this.idAndSession;
			}
			set
			{
				this.idAndSession = value;
			}
		}

		// Token: 0x040006E1 RID: 1761
		private IDictionary<PropertyDefinition, object> propertyBag;

		// Token: 0x040006E2 RID: 1762
		private IdAndSession idAndSession;
	}
}
