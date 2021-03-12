using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000083 RID: 131
	public class PhoneSet : ExtractionSet<Phone>
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0000E2BB File Offset: 0x0000C4BB
		public PhoneSet() : base(new PhoneSerializer())
		{
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		public static implicit operator PhoneSet(Phone[] phones)
		{
			return new PhoneSet
			{
				Extractions = phones
			};
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000E2E3 File Offset: 0x0000C4E3
		public static XmlSerializer Serializer
		{
			get
			{
				return PhoneSet.serializer;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E2EA File Offset: 0x0000C4EA
		public override XmlSerializer GetSerializer()
		{
			return PhoneSet.serializer;
		}

		// Token: 0x04000193 RID: 403
		private static XmlSerializer serializer = new XmlSerializer(typeof(PhoneSet));
	}
}
