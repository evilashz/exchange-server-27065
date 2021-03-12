using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200020F RID: 527
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoThumbprinter
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x0004F4F7 File Offset: 0x0004D6F7
		private PhotoThumbprinter()
		{
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004F500 File Offset: 0x0004D700
		public int Compute(Stream photo)
		{
			if (photo == null)
			{
				throw new ArgumentNullException("photo");
			}
			if (photo.Position != 0L)
			{
				throw new ArgumentException("Position within stream MUST be at the beginning.", "photo");
			}
			int result;
			using (MessageDigestForNonCryptographicPurposes messageDigestForNonCryptographicPurposes = new MessageDigestForNonCryptographicPurposes())
			{
				result = BitConverter.ToInt32(messageDigestForNonCryptographicPurposes.ComputeHash(photo), 0);
			}
			return result;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004F568 File Offset: 0x0004D768
		public string FormatAsETag(int? thumbprint)
		{
			if (thumbprint == null)
			{
				return string.Empty;
			}
			return "\"" + thumbprint.Value.ToString("X8") + "\"";
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004F5A7 File Offset: 0x0004D7A7
		public bool ThumbprintMatchesETag(int thumbprint, string etag)
		{
			return this.FormatAsETag(new int?(thumbprint)).Equals(etag, StringComparison.Ordinal);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004F5BC File Offset: 0x0004D7BC
		public int GenerateThumbprintForNegativeCache()
		{
			return Guid.NewGuid().GetHashCode();
		}

		// Token: 0x04000AB1 RID: 2737
		internal static readonly PhotoThumbprinter Default = new PhotoThumbprinter();
	}
}
