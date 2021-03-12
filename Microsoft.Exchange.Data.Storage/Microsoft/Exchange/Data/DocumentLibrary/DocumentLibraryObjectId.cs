using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x0200069C RID: 1692
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class DocumentLibraryObjectId : ObjectId
	{
		// Token: 0x06004515 RID: 17685 RVA: 0x00125EA2 File Offset: 0x001240A2
		protected internal DocumentLibraryObjectId(UriFlags uriFlags)
		{
			this.uriFlags = uriFlags;
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x00125EB4 File Offset: 0x001240B4
		public static DocumentLibraryObjectId Parse(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				throw new ArgumentException("str");
			}
			Uri uri = null;
			try
			{
				uri = new Uri(str);
			}
			catch (UriFormatException innerException)
			{
				throw new CorruptDataException(str, Strings.ExCorruptData, innerException);
			}
			if (uri.IsUnc)
			{
				int num = str.IndexOf(UncObjectId.QueryPart);
				if (num < 0 || str[num - 1] != '?')
				{
					throw new CorruptDataException(str, Strings.ExCorruptData);
				}
				UriFlags uriFlags;
				try
				{
					uriFlags = (UriFlags)Enum.Parse(typeof(UriFlags), str.Substring(num + UncObjectId.QueryPart.Length));
				}
				catch (ArgumentException innerException2)
				{
					throw new CorruptDataException(str, Strings.ExCorruptData, innerException2);
				}
				Uri uri2 = new Uri(str.Substring(0, num - 1));
				if (!Utils.IsValidUncUri(uri2))
				{
					throw new CorruptDataException(str, Strings.ExCorruptData);
				}
				return new UncObjectId(uri2, uriFlags);
			}
			else
			{
				if (uri.IsFile)
				{
					throw new ArgumentException("str");
				}
				return SharepointSiteId.Parse(str);
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x00125FCC File Offset: 0x001241CC
		public static DocumentLibraryObjectId Deserialize(byte[] byteArray)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			if (byteArray.Length == 0)
			{
				throw new ArgumentException("byteArray");
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			return DocumentLibraryObjectId.Parse(utf8Encoding.GetString(byteArray));
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x0012600C File Offset: 0x0012420C
		public static DocumentLibraryObjectId Deserialize(string base64Id)
		{
			if (base64Id == null)
			{
				throw new ArgumentNullException("base64Id");
			}
			byte[] byteArray = Convert.FromBase64String(base64Id);
			return DocumentLibraryObjectId.Deserialize(byteArray);
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x00126036 File Offset: 0x00124236
		public UriFlags UriFlags
		{
			get
			{
				return this.uriFlags;
			}
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0012603E File Offset: 0x0012423E
		public string ToBase64String()
		{
			return Convert.ToBase64String(this.GetBytes());
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x0012604C File Offset: 0x0012424C
		public override byte[] GetBytes()
		{
			string s = this.ToString();
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			return utf8Encoding.GetBytes(s);
		}

		// Token: 0x04002595 RID: 9621
		private UriFlags uriFlags;
	}
}
