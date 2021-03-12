using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006AE RID: 1710
	[DataContract(Name = "AlternateId", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "AlternateIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AlternateId : AlternateIdBase
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x000BE1D1 File Offset: 0x000BC3D1
		public AlternateId()
		{
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000BE1D9 File Offset: 0x000BC3D9
		internal AlternateId(string id, string primarySmtpAddress, IdFormat format) : base(format)
		{
			this.id = id;
			this.mailbox = primarySmtpAddress;
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000BE1F0 File Offset: 0x000BC3F0
		internal AlternateId(string id, string primarySmtpAddress, IdFormat format, bool isArchive) : base(format)
		{
			this.id = id;
			this.mailbox = primarySmtpAddress;
			this.isArchive = isArchive;
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000BE20F File Offset: 0x000BC40F
		// (set) Token: 0x060034C7 RID: 13511 RVA: 0x000BE217 File Offset: 0x000BC417
		[DataMember(IsRequired = true, Order = 1)]
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000BE220 File Offset: 0x000BC420
		// (set) Token: 0x060034C9 RID: 13513 RVA: 0x000BE228 File Offset: 0x000BC428
		[DataMember(IsRequired = true, Order = 2)]
		[XmlAttribute]
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000BE231 File Offset: 0x000BC431
		[XmlIgnore]
		public bool IsArchiveSpecified
		{
			get
			{
				return this.isArchive;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000BE239 File Offset: 0x000BC439
		// (set) Token: 0x060034CC RID: 13516 RVA: 0x000BE241 File Offset: 0x000BC441
		[XmlAttribute]
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public bool IsArchive
		{
			get
			{
				return this.isArchive;
			}
			set
			{
				this.isArchive = value;
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000BE24A File Offset: 0x000BC44A
		internal override CanonicalConvertedId Parse()
		{
			return AlternateIdBase.GetIdConverter(base.Format).Parse(this);
		}

		// Token: 0x04001DB6 RID: 7606
		private string id;

		// Token: 0x04001DB7 RID: 7607
		private string mailbox;

		// Token: 0x04001DB8 RID: 7608
		private bool isArchive;
	}
}
