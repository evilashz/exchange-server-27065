using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200000A RID: 10
	internal sealed class MsiDatabase : MsiBase
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002B02 File Offset: 0x00000D02
		public MsiDatabase(string fileName)
		{
			this.OpenDatabase(fileName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B14 File Offset: 0x00000D14
		public List<string> ExtractCabs(string toPath)
		{
			ValidationHelper.ThrowIfNullOrEmpty(toPath, "toPath");
			Directory.CreateDirectory(toPath);
			List<string> result;
			using (MsiView msiView = this.OpenView("SELECT `Name`, `Data` FROM `_Streams`"))
			{
				List<string> list = new List<string>();
				for (;;)
				{
					try
					{
						using (MsiRecord msiRecord = msiView.FetchNextRecord())
						{
							string @string = msiRecord.GetString(1U);
							if (!string.IsNullOrEmpty(@string) && !@string.StartsWith("\u0005", StringComparison.InvariantCultureIgnoreCase))
							{
								msiRecord.SaveStream(2U, Path.Combine(toPath, @string));
								list.Add(@string);
							}
						}
					}
					catch (MsiException ex)
					{
						if (ex.ErrorCode == 259U)
						{
							result = list;
							break;
						}
						throw;
					}
				}
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BDC File Offset: 0x00000DDC
		public string QueryProperty(string tableName, string propertyName)
		{
			ValidationHelper.ThrowIfNullOrEmpty(tableName, "tableName");
			ValidationHelper.ThrowIfNullOrEmpty(propertyName, "propertyName");
			string result = string.Empty;
			using (MsiView msiView = this.OpenView(string.Format("SELECT `Value` FROM `{0}` WHERE `Property`='{1}'", tableName, propertyName)))
			{
				using (MsiRecord msiRecord = msiView.FetchNextRecord())
				{
					result = msiRecord.GetString(1U);
				}
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C5C File Offset: 0x00000E5C
		private MsiView OpenView(string query)
		{
			return new MsiView(this, query);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C68 File Offset: 0x00000E68
		private void OpenDatabase(string fileName)
		{
			if (!MsiHelper.IsMsiFileExtension(fileName) && !MsiHelper.IsMspFileExtension(fileName))
			{
				throw new MsiException(Strings.WrongFileType("msiFilePath"));
			}
			SafeMsiHandle handle;
			uint errorCode = MsiNativeMethods.OpenDatabase(fileName, (IntPtr)(MsiHelper.IsMspFileExtension(fileName) ? MsiDatabase.MsiDbOpenPatchFile : MsiDatabase.MsiDbOpenReadonly), out handle);
			MsiHelper.ThrowIfNotSuccess(errorCode);
			base.Handle = handle;
		}

		// Token: 0x0400001C RID: 28
		private const string SkippedFilesIndicator = "\u0005";

		// Token: 0x0400001D RID: 29
		private const string DataQuery = "SELECT `Name`, `Data` FROM `_Streams`";

		// Token: 0x0400001E RID: 30
		private const string PropertyQuery = "SELECT `Value` FROM `{0}` WHERE `Property`='{1}'";

		// Token: 0x0400001F RID: 31
		private static readonly int MsiDbOpenReadonly = 0;

		// Token: 0x04000020 RID: 32
		private static readonly int MsiDbOpenPatchFile = MsiDatabase.MsiDbOpenReadonly + 32;
	}
}
