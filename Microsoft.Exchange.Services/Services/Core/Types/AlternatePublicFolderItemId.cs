using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B0 RID: 1712
	[XmlType(TypeName = "AlternatePublicFolderItemIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AlternatePublicFolderItemId : AlternatePublicFolderId
	{
		// Token: 0x060034D3 RID: 13523 RVA: 0x000BE299 File Offset: 0x000BC499
		public AlternatePublicFolderItemId()
		{
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000BE2A1 File Offset: 0x000BC4A1
		internal AlternatePublicFolderItemId(string itemId, string folderId, IdFormat format) : base(folderId, format)
		{
			this.itemId = itemId;
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000BE2B2 File Offset: 0x000BC4B2
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x000BE2BA File Offset: 0x000BC4BA
		[XmlAttribute]
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000BE2C3 File Offset: 0x000BC4C3
		internal override CanonicalConvertedId Parse()
		{
			return AlternateIdBase.GetIdConverter(base.Format).Parse(this);
		}

		// Token: 0x04001DBA RID: 7610
		private string itemId;
	}
}
