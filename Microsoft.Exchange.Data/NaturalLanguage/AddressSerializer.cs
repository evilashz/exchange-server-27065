using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddressSerializer : BaseSerializer<Address>
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0000DF22 File Offset: 0x0000C122
		protected override XmlSerializer GetSerializer()
		{
			return AddressSerializer.serializer;
		}

		// Token: 0x04000184 RID: 388
		private static XmlSerializer serializer = new XmlSerializer(typeof(Address[]), new XmlRootAttribute("Addresses"));
	}
}
