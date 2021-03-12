using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000080 RID: 128
	public class AddressSet : ExtractionSet<Address>
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000E1D7 File Offset: 0x0000C3D7
		public AddressSet() : base(new AddressSerializer())
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public static implicit operator AddressSet(Address[] addresses)
		{
			return new AddressSet
			{
				Extractions = addresses
			};
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000E1FF File Offset: 0x0000C3FF
		public static XmlSerializer Serializer
		{
			get
			{
				return AddressSet.serializer;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E206 File Offset: 0x0000C406
		public override XmlSerializer GetSerializer()
		{
			return AddressSet.serializer;
		}

		// Token: 0x04000190 RID: 400
		private static XmlSerializer serializer = new XmlSerializer(typeof(AddressSet));
	}
}
