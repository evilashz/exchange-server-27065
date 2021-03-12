using System;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x02000997 RID: 2455
	internal static class NameSpaceExtractor
	{
		// Token: 0x06006299 RID: 25241 RVA: 0x0014FA84 File Offset: 0x0014DC84
		public static string ExtractNameSpace(string FullyQualifiedTypeName)
		{
			int num = FullyQualifiedTypeName.LastIndexOf(NameSpaceExtractor.NameSpaceSeperator);
			if (num == -1)
			{
				return "";
			}
			return FullyQualifiedTypeName.Substring(0, num);
		}

		// Token: 0x04002C1A RID: 11290
		private static char NameSpaceSeperator = '.';
	}
}
