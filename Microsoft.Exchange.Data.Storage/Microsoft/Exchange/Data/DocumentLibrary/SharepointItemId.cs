using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E7 RID: 1767
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class SharepointItemId : SharepointListId
	{
		// Token: 0x0600463C RID: 17980 RVA: 0x0012B16C File Offset: 0x0012936C
		internal SharepointItemId(string id, string listName, Uri siteUri, CultureInfo cultureInfo, UriFlags uriFlags) : base(listName, siteUri, cultureInfo, uriFlags)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id");
			}
			this.id = id;
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x0012B194 File Offset: 0x00129394
		internal string ItemId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x0012B19C File Offset: 0x0012939C
		protected override StringBuilder ToStringHelper()
		{
			StringBuilder stringBuilder = base.ToStringHelper();
			stringBuilder.AppendFormat("&ItemId={0}", this.ItemId);
			return stringBuilder;
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x0012B1C4 File Offset: 0x001293C4
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SharepointItemId sharepointItemId = obj as SharepointItemId;
				return sharepointItemId.id == this.id;
			}
			return false;
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x0012B1F4 File Offset: 0x001293F4
		public override int GetHashCode()
		{
			return base.GetHashCode() + this.id.GetHashCode();
		}

		// Token: 0x0400265D RID: 9821
		private readonly string id;
	}
}
