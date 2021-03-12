using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200001A RID: 26
	internal class Breadcrumbs
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00007A94 File Offset: 0x00005C94
		internal static ITempFile GenerateDump()
		{
			ITempFile tempFile = TempFileFactory.CreateTempFile();
			using (FileStream fileStream = new FileStream(tempFile.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					int num = Breadcrumbs.sequenceNumber + 1 & 2047;
					streamWriter.WriteLine();
					streamWriter.WriteLine("--- Dumping breadcrumbs --- ");
					streamWriter.WriteLine("{0}, {1} \r\n", Breadcrumbs.initialTickCount, Breadcrumbs.initialDateTime);
					for (int i = 0; i < 2048; i++)
					{
						string value = Breadcrumbs.breadcrumbs[num];
						if (!string.IsNullOrEmpty(value))
						{
							streamWriter.WriteLine(value);
						}
						num = (num + 1 & 2047);
					}
					streamWriter.WriteLine();
				}
			}
			return tempFile;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007B70 File Offset: 0x00005D70
		internal static void AddBreadcrumb(string crumb)
		{
			if (!string.IsNullOrEmpty(crumb))
			{
				int num = Interlocked.Increment(ref Breadcrumbs.sequenceNumber);
				int num2 = num & 2047;
				if (crumb.Length > 1024)
				{
					crumb = crumb.Substring(0, 1024);
				}
				crumb = string.Format(CultureInfo.InvariantCulture, "{0}, TID:{1} {2}", new object[]
				{
					Environment.TickCount & int.MaxValue,
					Thread.CurrentThread.ManagedThreadId,
					crumb
				});
				Breadcrumbs.breadcrumbs[num2] = crumb;
			}
		}

		// Token: 0x0400008D RID: 141
		private const int NumCrumbs = 2048;

		// Token: 0x0400008E RID: 142
		private const int IndexMask = 2047;

		// Token: 0x0400008F RID: 143
		private static int sequenceNumber = 0;

		// Token: 0x04000090 RID: 144
		private static string[] breadcrumbs = new string[2048];

		// Token: 0x04000091 RID: 145
		private static int initialTickCount = Environment.TickCount & int.MaxValue;

		// Token: 0x04000092 RID: 146
		private static ExDateTime initialDateTime = ExDateTime.UtcNow;
	}
}
