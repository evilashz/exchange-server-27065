using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E8 RID: 1768
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class SharepointDocumentLibraryItemId : SharepointItemId
	{
		// Token: 0x06004641 RID: 17985 RVA: 0x0012B208 File Offset: 0x00129408
		internal SharepointDocumentLibraryItemId(string id, string listName, Uri siteUri, CultureInfo cultureInfo, UriFlags uriFlags, ICollection<string> itemHierarchy) : base(id, listName, siteUri, cultureInfo, uriFlags)
		{
			if (itemHierarchy == null)
			{
				throw new ArgumentNullException("itemHierarchy");
			}
			if (itemHierarchy.Count < 1)
			{
				throw new ArgumentException("itemHierarchy");
			}
			this.itemHierarchy = new List<string>(itemHierarchy.Count);
			foreach (string text in itemHierarchy)
			{
				if (text.Length > 0 && (text[text.Length - 1] == '/' || text[text.Length - 1] == '\\'))
				{
					this.itemHierarchy.Add(text.Substring(0, text.Length - 1));
				}
				else
				{
					this.itemHierarchy.Add(text);
				}
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x0012B2E4 File Offset: 0x001294E4
		internal string ParentDirectoryStructure
		{
			get
			{
				if (this.itemHierarchy.Count == 1)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.itemHierarchy.Count - 1; i++)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('/');
					}
					stringBuilder.Append(this.itemHierarchy[i]);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x0012B34A File Offset: 0x0012954A
		internal ReadOnlyCollection<string> ItemHierarchy
		{
			get
			{
				return new ReadOnlyCollection<string>(this.itemHierarchy);
			}
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x0012B358 File Offset: 0x00129558
		protected override StringBuilder ToStringHelper()
		{
			StringBuilder stringBuilder = base.ToStringHelper();
			stringBuilder.Append("&ItemHierarchy=");
			for (int i = 0; i < this.itemHierarchy.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(Uri.EscapeDataString("/"));
				}
				stringBuilder.Append(Uri.EscapeDataString(this.itemHierarchy[i]));
			}
			return stringBuilder;
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x0012B3BC File Offset: 0x001295BC
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SharepointDocumentLibraryItemId sharepointDocumentLibraryItemId = (SharepointDocumentLibraryItemId)obj;
				return sharepointDocumentLibraryItemId.ItemId == base.ItemId;
			}
			return false;
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x0012B3EC File Offset: 0x001295EC
		public override int GetHashCode()
		{
			return base.GetHashCode() + this.itemHierarchy.GetHashCode();
		}

		// Token: 0x0400265E RID: 9822
		private readonly List<string> itemHierarchy;
	}
}
