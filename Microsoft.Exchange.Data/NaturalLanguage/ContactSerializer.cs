using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactSerializer : BaseSerializer<Contact>
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000E03C File Offset: 0x0000C23C
		protected override XmlSerializer GetSerializer()
		{
			return ContactSerializer.serializer;
		}

		// Token: 0x0400018A RID: 394
		private static XmlSerializer serializer = new XmlSerializer(typeof(Contact[]), new XmlRootAttribute("Contacts"));
	}
}
