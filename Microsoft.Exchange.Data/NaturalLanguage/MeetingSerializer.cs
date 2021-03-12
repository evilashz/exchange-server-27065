using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MeetingSerializer : BaseSerializer<Meeting>
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000DF51 File Offset: 0x0000C151
		protected override XmlSerializer GetSerializer()
		{
			return MeetingSerializer.serializer;
		}

		// Token: 0x04000185 RID: 389
		private static XmlSerializer serializer = new XmlSerializer(typeof(Meeting[]), new XmlRootAttribute("Meetings"));
	}
}
