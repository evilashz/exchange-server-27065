using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UrlSerializer : BaseSerializer<Url>
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000E00D File Offset: 0x0000C20D
		protected override XmlSerializer GetSerializer()
		{
			return UrlSerializer.serializer;
		}

		// Token: 0x04000189 RID: 393
		private static XmlSerializer serializer = new XmlSerializer(typeof(Url[]), new XmlRootAttribute("Urls"));
	}
}
