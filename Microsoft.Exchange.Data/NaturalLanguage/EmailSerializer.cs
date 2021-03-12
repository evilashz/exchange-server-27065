using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EmailSerializer : BaseSerializer<Email>
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0000DFDE File Offset: 0x0000C1DE
		protected override XmlSerializer GetSerializer()
		{
			return EmailSerializer.serializer;
		}

		// Token: 0x04000188 RID: 392
		private static XmlSerializer serializer = new XmlSerializer(typeof(Email[]), new XmlRootAttribute("Emails"));
	}
}
