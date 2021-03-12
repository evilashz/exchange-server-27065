using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Include
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x000194DE File Offset: 0x000176DE
		internal Include()
		{
			this.anyAttrField = new List<XmlAttribute>();
			this.anyField = new List<XmlElement>();
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000194FC File Offset: 0x000176FC
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00019504 File Offset: 0x00017704
		[XmlAnyElement(Order = 0)]
		internal List<XmlElement> Any
		{
			get
			{
				return this.anyField;
			}
			set
			{
				this.anyField = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001950D File Offset: 0x0001770D
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00019515 File Offset: 0x00017715
		internal string href
		{
			get
			{
				return this.hrefField;
			}
			set
			{
				this.hrefField = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001951E File Offset: 0x0001771E
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00019526 File Offset: 0x00017726
		[XmlAnyAttribute]
		internal List<XmlAttribute> AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040002FE RID: 766
		private List<XmlElement> anyField;

		// Token: 0x040002FF RID: 767
		private string hrefField;

		// Token: 0x04000300 RID: 768
		private List<XmlAttribute> anyAttrField;
	}
}
