using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips
{
	// Token: 0x02000508 RID: 1288
	internal static class FileDeleter
	{
		// Token: 0x06001FCF RID: 8143 RVA: 0x000C242C File Offset: 0x000C062C
		public static void Delete(string directoryPath, IFileDeletionPolicy policy)
		{
			if (!string.IsNullOrEmpty(directoryPath) && policy != null)
			{
				IEnumerable<string> enumerable = from o in Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
				let filePath = Path.Combine(directoryPath, o)
				where policy.ShouldDelete(filePath)
				select o;
				foreach (string path in enumerable)
				{
					try
					{
						File.Delete(path);
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x04001742 RID: 5954
		private const string SearchPattern = "*.*";
	}
}
