using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.MessageSecurity.MessageClassifications
{
	// Token: 0x0200000F RID: 15
	internal static class ClassificationUtils
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000031EC File Offset: 0x000013EC
		public static List<string> ExtractClassifications(HeaderList headers)
		{
			Header[] array = headers.FindAll("X-MS-Exchange-Organization-Classification");
			List<string> list = new List<string>(array.Length);
			foreach (Header header in array)
			{
				string text = ClassificationUtils.ExtractId(header.Value);
				if (text.Length > 0)
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003248 File Offset: 0x00001448
		public static void PromoteStoreClassifications(HeaderList headers)
		{
			foreach (Header header in headers.FindAll("x-microsoft-classID"))
			{
				string value = ClassificationUtils.ExtractId(header.Value);
				if (!string.IsNullOrEmpty(value))
				{
					headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Classification", value));
				}
			}
			ClassificationUtils.DropStoreLabels(headers);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000329F File Offset: 0x0000149F
		public static void DropStoreLabels(HeaderList headers)
		{
			headers.RemoveAll("x-microsoft-classified");
			headers.RemoveAll("x-microsoft-classification");
			headers.RemoveAll("x-microsoft-classDesc");
			headers.RemoveAll("x-microsoft-classID");
			headers.RemoveAll("X-microsoft-classKeep");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000032D8 File Offset: 0x000014D8
		public static void PromoteIfUnclassified(HeaderList headers, string classificationGuidText)
		{
			string text = ClassificationUtils.ExtractId(classificationGuidText);
			if (text.Length == 0)
			{
				return;
			}
			if (headers.FindFirst("X-MS-Exchange-Organization-Classification") != null)
			{
				return;
			}
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Classification", text));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003318 File Offset: 0x00001518
		private static string ExtractId(string classificationGuidText)
		{
			int num = classificationGuidText.IndexOf(';');
			string text = (num < 0) ? classificationGuidText : classificationGuidText.Substring(0, num);
			return text.Trim(ClassificationUtils.LabelTrimCharacters);
		}

		// Token: 0x04000045 RID: 69
		private static readonly char[] LabelTrimCharacters = new char[]
		{
			'{',
			'}',
			' '
		};
	}
}
