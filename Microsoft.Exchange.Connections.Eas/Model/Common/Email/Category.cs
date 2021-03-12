using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.Email
{
	// Token: 0x02000089 RID: 137
	[XmlType(Namespace = "Email", TypeName = "Category")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Category
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00009594 File Offset: 0x00007794
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000959C File Offset: 0x0000779C
		[XmlText]
		public string Name { get; set; }
	}
}
