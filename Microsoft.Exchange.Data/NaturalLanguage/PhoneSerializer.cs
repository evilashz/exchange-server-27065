using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000079 RID: 121
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PhoneSerializer : BaseSerializer<Phone>
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000DFAF File Offset: 0x0000C1AF
		protected override XmlSerializer GetSerializer()
		{
			return PhoneSerializer.serializer;
		}

		// Token: 0x04000187 RID: 391
		private static XmlSerializer serializer = new XmlSerializer(typeof(Phone[]), new XmlRootAttribute("Phones"));
	}
}
