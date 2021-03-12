using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E6 RID: 1766
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class SharepointListId : SharepointSiteId
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x0012B0C3 File Offset: 0x001292C3
		internal SharepointListId(string listName, Uri siteUri, CultureInfo cultureInfo, UriFlags uriFlags) : base(siteUri, uriFlags)
		{
			this.cultureInfo = cultureInfo;
			this.listName = listName;
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x0012B0DC File Offset: 0x001292DC
		public override bool Equals(object obj)
		{
			return base.Equals(obj) && ((SharepointListId)obj).listName == this.listName;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x0012B0FF File Offset: 0x001292FF
		public override int GetHashCode()
		{
			return base.GetHashCode() + this.listName.GetHashCode();
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x0012B113 File Offset: 0x00129313
		internal string ListName
		{
			get
			{
				return this.listName;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x0012B11B File Offset: 0x0012931B
		// (set) Token: 0x06004638 RID: 17976 RVA: 0x0012B123 File Offset: 0x00129323
		internal KeyValuePair<string, XmlNode>? Cache
		{
			get
			{
				return this.cache;
			}
			set
			{
				this.cache = value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x0012B12C File Offset: 0x0012932C
		// (set) Token: 0x0600463A RID: 17978 RVA: 0x0012B134 File Offset: 0x00129334
		internal CultureInfo CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
			set
			{
				this.cultureInfo = value;
			}
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x0012B140 File Offset: 0x00129340
		protected override StringBuilder ToStringHelper()
		{
			StringBuilder stringBuilder = base.ToStringHelper();
			stringBuilder.AppendFormat("&ListName={0}", Uri.EscapeDataString(this.ListName));
			return stringBuilder;
		}

		// Token: 0x0400265A RID: 9818
		private readonly string listName;

		// Token: 0x0400265B RID: 9819
		[NonSerialized]
		private KeyValuePair<string, XmlNode>? cache;

		// Token: 0x0400265C RID: 9820
		private CultureInfo cultureInfo;
	}
}
