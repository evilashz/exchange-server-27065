using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200000B RID: 11
	internal static class LocalizedResources
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000062F4 File Offset: 0x000044F4
		public static string GetFile(string basePath, string fileName)
		{
			string result = null;
			try
			{
				result = LocalizedResources.GetFile(basePath, fileName, CultureInfo.CurrentUICulture);
			}
			catch (FileNotFoundException)
			{
				CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
				if (CultureInfo.CurrentUICulture.Equals(cultureInfo))
				{
					throw;
				}
				result = LocalizedResources.GetFile(basePath, fileName, cultureInfo);
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000634C File Offset: 0x0000454C
		public static string GetFile(string basePath, string fileName, CultureInfo cultureInfo)
		{
			string text = null;
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.CurrentUICulture;
			}
			string str = Path.HasExtension(fileName) ? Path.GetFileNameWithoutExtension(fileName) : fileName;
			string str2 = Path.HasExtension(fileName) ? Path.GetExtension(fileName) : string.Empty;
			while (cultureInfo != null && !cultureInfo.Equals(CultureInfo.InvariantCulture))
			{
				string text2 = Path.Combine(basePath, str + "." + cultureInfo.Name + str2);
				if (File.Exists(text2))
				{
					text = text2;
					break;
				}
				text2 = Path.Combine(Path.Combine(basePath, cultureInfo.Name), fileName);
				if (File.Exists(text2))
				{
					text = text2;
					break;
				}
				cultureInfo = cultureInfo.Parent;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = Path.Combine(basePath, fileName);
				if (!File.Exists(text))
				{
					throw new FileNotFoundException();
				}
			}
			return text;
		}
	}
}
