using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public class CharsetNotInstalledException : InvalidCharsetException
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x000668A9 File Offset: 0x00064AA9
		public CharsetNotInstalledException(int codePage) : base(codePage, GlobalizationStrings.NotInstalledCodePage(codePage))
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000668B8 File Offset: 0x00064AB8
		public CharsetNotInstalledException(string charsetName) : base(charsetName, GlobalizationStrings.NotInstalledCharset((charsetName == null) ? "<null>" : charsetName))
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000668D1 File Offset: 0x00064AD1
		internal CharsetNotInstalledException(string charsetName, int codePage) : base(charsetName, codePage, GlobalizationStrings.NotInstalledCharsetCodePage(codePage, (charsetName == null) ? "<null>" : charsetName))
		{
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000668EC File Offset: 0x00064AEC
		public CharsetNotInstalledException(int codePage, string message) : base(codePage, message)
		{
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000668F6 File Offset: 0x00064AF6
		public CharsetNotInstalledException(string charsetName, string message) : base(charsetName, message)
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00066900 File Offset: 0x00064B00
		public CharsetNotInstalledException(int codePage, string message, Exception innerException) : base(codePage, message, innerException)
		{
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0006690B File Offset: 0x00064B0B
		public CharsetNotInstalledException(string charsetName, string message, Exception innerException) : base(charsetName, message, innerException)
		{
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00066916 File Offset: 0x00064B16
		protected CharsetNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
