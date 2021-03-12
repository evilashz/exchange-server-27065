using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class KeywordSerializer : BaseSerializer<Keyword>
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000DF80 File Offset: 0x0000C180
		protected override XmlSerializer GetSerializer()
		{
			return KeywordSerializer.serializer;
		}

		// Token: 0x04000186 RID: 390
		private static XmlSerializer serializer = new XmlSerializer(typeof(Keyword[]), new XmlRootAttribute("Keywords"));
	}
}
