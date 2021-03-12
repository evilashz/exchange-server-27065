using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DCA RID: 3530
	[XmlType(TypeName = "SidAndAttributesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SidAndAttributesTypeForAS
	{
		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060059E1 RID: 23009 RVA: 0x0011878F File Offset: 0x0011698F
		// (set) Token: 0x060059E2 RID: 23010 RVA: 0x00118797 File Offset: 0x00116997
		[XmlElement(Order = 0, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
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

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x001187A0 File Offset: 0x001169A0
		// (set) Token: 0x060059E4 RID: 23012 RVA: 0x001187A8 File Offset: 0x001169A8
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

		// Token: 0x040031BA RID: 12730
		private string securityIdentifierField;

		// Token: 0x040031BB RID: 12731
		private uint attributesField;
	}
}
