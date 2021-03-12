using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200001A RID: 26
	internal static class ValidationHelper
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003A0B File Offset: 0x00001C0B
		public static void ThrowIfDirectoryNotExist(string directoryName, string name)
		{
			ValidationHelper.ThrowIfNullOrEmpty(directoryName, name);
			if (!Directory.Exists(directoryName))
			{
				throw new ArgumentException(Strings.NotExist(directoryName));
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A2D File Offset: 0x00001C2D
		public static void ThrowIfFileNotExist(string fileName, string name)
		{
			ValidationHelper.ThrowIfNullOrEmpty(fileName, name);
			if (!File.Exists(fileName))
			{
				throw new ArgumentException(Strings.NotExist(fileName));
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A4F File Offset: 0x00001C4F
		public static void ThrowIfNullOrEmpty(string value, string name)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(Strings.IsNullOrEmpty(name ?? "Unknown"));
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A73 File Offset: 0x00001C73
		public static void ThrowIfNullOrEmpty<T>(IEnumerable<T> value, string name)
		{
			if (value == null || !value.Any<T>())
			{
				throw new ArgumentNullException(Strings.IsNullOrEmpty(name ?? "Unknown"));
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A9A File Offset: 0x00001C9A
		public static void ThrowIfNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(Strings.IsNullOrEmpty(name ?? "Unknown"));
			}
		}
	}
}
