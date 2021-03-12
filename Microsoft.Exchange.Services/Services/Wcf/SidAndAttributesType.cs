using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC9 RID: 3529
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class SidAndAttributesType
	{
		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060059DC RID: 23004 RVA: 0x00118765 File Offset: 0x00116965
		// (set) Token: 0x060059DD RID: 23005 RVA: 0x0011876D File Offset: 0x0011696D
		[XmlElement(Order = 0)]
		public string SecurityIdentifier
		{
			get
			{
				return this.securityIdentifierField;
			}
			set
			{
				this.securityIdentifierField = value;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060059DE RID: 23006 RVA: 0x00118776 File Offset: 0x00116976
		// (set) Token: 0x060059DF RID: 23007 RVA: 0x0011877E File Offset: 0x0011697E
		[XmlAttribute]
		public uint Attributes
		{
			get
			{
				return this.attributesField;
			}
			set
			{
				this.attributesField = value;
			}
		}

		// Token: 0x040031B8 RID: 12728
		private string securityIdentifierField;

		// Token: 0x040031B9 RID: 12729
		private uint attributesField;
	}
}
