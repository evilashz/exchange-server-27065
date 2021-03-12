using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E5 RID: 1765
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SharepointSiteId : DocumentLibraryObjectId
	{
		// Token: 0x0600462B RID: 17963 RVA: 0x0012ADFD File Offset: 0x00128FFD
		internal SharepointSiteId(Uri siteUri, UriFlags uriFlags) : base(uriFlags)
		{
			this.siteUri = siteUri;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x0012AE0D File Offset: 0x0012900D
		internal SharepointSiteId(string siteUri, UriFlags uriFlags) : this(new Uri(siteUri), uriFlags)
		{
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x0012AE1C File Offset: 0x0012901C
		public new static SharepointSiteId Parse(string arg)
		{
			Uri uri = new Uri(arg);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in uri.Query.Substring(1).Split(new char[]
			{
				'&'
			}))
			{
				string[] array2 = text.Split(new char[]
				{
					'='
				});
				if (array2.Length != 2)
				{
					throw new CorruptDataException(text, Strings.ExCorruptData);
				}
				if (dictionary.ContainsKey(array2[0]))
				{
					throw new CorruptDataException(text, Strings.ExCorruptData);
				}
				dictionary.Add(array2[0], array2[1]);
			}
			int num = 0;
			string s = null;
			if (!dictionary.TryGetValue("UriFlags", out s) || !int.TryParse(s, out num))
			{
				throw new CorruptDataException(arg, Strings.ExCorruptData);
			}
			string text2 = null;
			if (dictionary.TryGetValue("ListName", out text2))
			{
				text2 = Uri.UnescapeDataString(text2);
				string id = null;
				if (dictionary.TryGetValue("ItemId", out id))
				{
					string text3 = null;
					if (dictionary.TryGetValue("ItemHierarchy", out text3))
					{
						if (dictionary.Count != 4 || (num != 33 && num != 17))
						{
							throw new CorruptDataException(arg, Strings.ExCorruptData);
						}
						text3 = Uri.UnescapeDataString(text3);
						return new SharepointDocumentLibraryItemId(id, text2, new Uri(uri.GetLeftPart(UriPartial.Path)), null, (UriFlags)num, text3.Split(new char[]
						{
							'/'
						}));
					}
					else
					{
						if (dictionary.Count != 3)
						{
							throw new CorruptDataException(arg, Strings.ExCorruptData);
						}
						return new SharepointItemId(id, text2, new Uri(uri.GetLeftPart(UriPartial.Path)), null, (UriFlags)num);
					}
				}
				else
				{
					if (dictionary.Count != 2 || (num != 5 && num != 9))
					{
						throw new CorruptDataException(arg, Strings.ExCorruptData);
					}
					return new SharepointListId(text2, new Uri(uri.GetLeftPart(UriPartial.Path)), null, (UriFlags)num);
				}
			}
			else
			{
				if (dictionary.Count != 1 || num != 1)
				{
					throw new CorruptDataException(arg, Strings.ExCorruptData);
				}
				return new SharepointSiteId(uri.GetLeftPart(UriPartial.Path), (UriFlags)num);
			}
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x0012B018 File Offset: 0x00129218
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && ((SharepointSiteId)obj).siteUri == this.siteUri;
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x0012B048 File Offset: 0x00129248
		public override int GetHashCode()
		{
			return this.siteUri.GetHashCode();
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x0012B058 File Offset: 0x00129258
		public override string ToString()
		{
			return new UriBuilder(this.SiteUri)
			{
				Query = this.ToStringHelper().ToString()
			}.Uri.ToString();
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x0012B08D File Offset: 0x0012928D
		public Uri SiteUri
		{
			get
			{
				return this.siteUri;
			}
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x0012B098 File Offset: 0x00129298
		protected virtual StringBuilder ToStringHelper()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UriFlags={0}", (int)base.UriFlags);
			return stringBuilder;
		}

		// Token: 0x04002659 RID: 9817
		private readonly Uri siteUri;
	}
}
