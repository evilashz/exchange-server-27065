using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008BD RID: 2237
	[XmlType(TypeName = "UserMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UserMailbox
	{
		// Token: 0x06003F65 RID: 16229 RVA: 0x000DB5B9 File Offset: 0x000D97B9
		public UserMailbox()
		{
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000DB5C1 File Offset: 0x000D97C1
		internal UserMailbox(string id, bool isArchive)
		{
			this.id = id;
			this.isArchive = isArchive;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000DB5D8 File Offset: 0x000D97D8
		public override bool Equals(object obj)
		{
			UserMailbox userMailbox = obj as UserMailbox;
			return userMailbox != null && string.Compare(this.id, userMailbox.Id, StringComparison.OrdinalIgnoreCase) == 0 && this.isArchive == userMailbox.IsArchive;
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000DB614 File Offset: 0x000D9814
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000DB61C File Offset: 0x000D981C
		// (set) Token: 0x06003F6A RID: 16234 RVA: 0x000DB624 File Offset: 0x000D9824
		[XmlAttribute("Id")]
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

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000DB62D File Offset: 0x000D982D
		// (set) Token: 0x06003F6C RID: 16236 RVA: 0x000DB635 File Offset: 0x000D9835
		[XmlAttribute("IsArchive")]
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

		// Token: 0x0400244C RID: 9292
		private string id;

		// Token: 0x0400244D RID: 9293
		private bool isArchive;
	}
}
