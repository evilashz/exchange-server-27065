using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F8 RID: 1272
	[DataContract]
	public class EmailAddressItem
	{
		// Token: 0x06003D7B RID: 15739 RVA: 0x000B89C6 File Offset: 0x000B6BC6
		public EmailAddressItem(ProxyAddressBase address)
		{
			this.address = address;
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000B89D5 File Offset: 0x000B6BD5
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000B89E2 File Offset: 0x000B6BE2
		[DataMember]
		public string Prefix
		{
			get
			{
				return this.address.PrefixString;
			}
			set
			{
			}
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x000B89E4 File Offset: 0x000B6BE4
		// (set) Token: 0x06003D7F RID: 15743 RVA: 0x000B89F1 File Offset: 0x000B6BF1
		[DataMember]
		public string AddressString
		{
			get
			{
				return this.address.ValueString;
			}
			set
			{
			}
		}

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000B89F3 File Offset: 0x000B6BF3
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000B8A15 File Offset: 0x000B6C15
		[DataMember]
		public string Identity
		{
			get
			{
				return string.Format("{0}:{1}", this.address.PrefixString, this.address.ValueString);
			}
			set
			{
			}
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000B8A18 File Offset: 0x000B6C18
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			EmailAddressItem emailAddressItem = obj as EmailAddressItem;
			return emailAddressItem != null && this.Identity.Equals(emailAddressItem.Identity);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000B8A47 File Offset: 0x000B6C47
		public override int GetHashCode()
		{
			return this.Identity.GetHashCode();
		}

		// Token: 0x04002811 RID: 10257
		private ProxyAddressBase address;
	}
}
