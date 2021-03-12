using System;
using System.Xml;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200010B RID: 267
	internal interface IPAAParser
	{
		// Token: 0x060008DF RID: 2271
		void SerializeTo(PersonalAutoAttendant paa, XmlWriter writer);

		// Token: 0x060008E0 RID: 2272
		PersonalAutoAttendant DeserializeFrom(XmlNode node);
	}
}
