using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200000C RID: 12
	internal static class MsiHelper
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002D68 File Offset: 0x00000F68
		public static List<string> DetermineApplicableMsps(string msiFilePath, bool sort, params string[] mspFiles)
		{
			if (!MsiHelper.IsMsiFileExtension(msiFilePath))
			{
				throw new MsiException(Strings.WrongFileType("msiFilePath"));
			}
			ValidationHelper.ThrowIfNullOrEmpty<string>(mspFiles, "mspFiles");
			List<MsiNativeMethods.PatchSequenceInfo> list = new List<MsiNativeMethods.PatchSequenceInfo>();
			foreach (string text in mspFiles)
			{
				if (File.Exists(text) && MsiHelper.IsMspFileExtension(text))
				{
					list.Add(new MsiNativeMethods.PatchSequenceInfo
					{
						PatchData = text,
						PatchDataType = MsiNativeMethods.PatchDataType.PatchFile,
						Order = -1,
						Status = 0U
					});
				}
			}
			List<MsiNativeMethods.PatchSequenceInfo> list2 = new List<MsiNativeMethods.PatchSequenceInfo>();
			if (list.Count > 0)
			{
				MsiNativeMethods.PatchSequenceInfo[] array = list.ToArray();
				if (MsiNativeMethods.DetermineApplicablePatches(msiFilePath, array.Length, array) == 0U)
				{
					foreach (MsiNativeMethods.PatchSequenceInfo item in array)
					{
						if (item.Order >= 0)
						{
							list2.Add(item);
						}
					}
				}
			}
			List<string> list3 = new List<string>();
			if (list2.Count > 0)
			{
				if (sort)
				{
					list2.Sort(new Comparison<MsiNativeMethods.PatchSequenceInfo>(MsiNativeMethods.ComparePatchSequence));
				}
				foreach (MsiNativeMethods.PatchSequenceInfo patchSequenceInfo in list2)
				{
					list3.Add(patchSequenceInfo.PatchData);
				}
			}
			return list3;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ED4 File Offset: 0x000010D4
		public static bool IsMspFileExtension(string fileName)
		{
			ValidationHelper.ThrowIfFileNotExist(fileName, "fileName");
			return fileName.ToLower().EndsWith(".msp", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002EF2 File Offset: 0x000010F2
		public static bool IsMsiFileExtension(string fileName)
		{
			ValidationHelper.ThrowIfFileNotExist(fileName, "fileName");
			return fileName.ToLower().EndsWith(".msi", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F10 File Offset: 0x00001110
		public static void ThrowIfNotSuccess(uint errorCode)
		{
			if (errorCode != 0U)
			{
				throw new MsiException(errorCode);
			}
		}

		// Token: 0x04000022 RID: 34
		private const string MsiExtension = ".msi";

		// Token: 0x04000023 RID: 35
		private const string MspExtension = ".msp";
	}
}
