using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000086 RID: 134
	public class ContactSet : ExtractionSet<Contact>
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000E39F File Offset: 0x0000C59F
		public ContactSet() : base(new ContactSerializer())
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public static implicit operator ContactSet(Contact[] contacts)
		{
			return new ContactSet
			{
				Extractions = contacts
			};
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000E3C7 File Offset: 0x0000C5C7
		public static XmlSerializer Serializer
		{
			get
			{
				return ContactSet.serializer;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E3CE File Offset: 0x0000C5CE
		public override XmlSerializer GetSerializer()
		{
			return ContactSet.serializer;
		}

		// Token: 0x04000196 RID: 406
		private static XmlSerializer serializer = new XmlSerializer(typeof(ContactSet));
	}
}
