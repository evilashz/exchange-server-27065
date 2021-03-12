using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006AF RID: 1711
	[XmlType(TypeName = "AlternatePublicFolderIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AlternatePublicFolderId : AlternateIdBase
	{
		// Token: 0x060034CE RID: 13518 RVA: 0x000BE25D File Offset: 0x000BC45D
		public AlternatePublicFolderId()
		{
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000BE265 File Offset: 0x000BC465
		internal AlternatePublicFolderId(string folderId, IdFormat format) : base(format)
		{
			this.folderId = folderId;
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000BE275 File Offset: 0x000BC475
		// (set) Token: 0x060034D1 RID: 13521 RVA: 0x000BE27D File Offset: 0x000BC47D
		[XmlAttribute]
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000BE286 File Offset: 0x000BC486
		internal override CanonicalConvertedId Parse()
		{
			return AlternateIdBase.GetIdConverter(base.Format).Parse(this);
		}

		// Token: 0x04001DB9 RID: 7609
		private string folderId;
	}
}
