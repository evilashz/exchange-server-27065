using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000398 RID: 920
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PostModernGroupItemType : BaseRequestType
	{
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0002A3D6 File Offset: 0x000285D6
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0002A3DE File Offset: 0x000285DE
		public EmailAddressType ModernGroupEmailAddress
		{
			get
			{
				return this.modernGroupEmailAddressField;
			}
			set
			{
				this.modernGroupEmailAddressField = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0002A3E7 File Offset: 0x000285E7
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0002A3EF File Offset: 0x000285EF
		public NonEmptyArrayOfAllItemsType Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04001339 RID: 4921
		private EmailAddressType modernGroupEmailAddressField;

		// Token: 0x0400133A RID: 4922
		private NonEmptyArrayOfAllItemsType itemsField;
	}
}
