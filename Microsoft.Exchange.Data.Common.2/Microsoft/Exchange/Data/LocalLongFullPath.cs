using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class LocalLongFullPath : LongPath
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003A9F File Offset: 0x00001C9F
		protected LocalLongFullPath()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public new static LocalLongFullPath Parse(string path)
		{
			return (LocalLongFullPath)LongPath.ParseInternal(path, new LocalLongFullPath());
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003AB9 File Offset: 0x00001CB9
		public static bool TryParse(string path, out LocalLongFullPath resultObject)
		{
			resultObject = (LocalLongFullPath)LongPath.TryParseInternal(path, new LocalLongFullPath());
			return null != resultObject;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public static LocalLongFullPath ParseFromPathNameAndFileName(string pathName, string fileName)
		{
			LocalLongFullPath localLongFullPath = LocalLongFullPath.Parse(Path.Combine(pathName, fileName));
			try
			{
				localLongFullPath.ValidateFilePathLength();
			}
			catch (FormatException ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			return localLongFullPath;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B1C File Offset: 0x00001D1C
		public static string ConvertInvalidCharactersInPathName(string fileName)
		{
			return LocalLongFullPath.ConvertInvalidCharactersInternal(fileName, Path.GetInvalidPathChars());
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B29 File Offset: 0x00001D29
		public static string ConvertInvalidCharactersInFileName(string fileName)
		{
			return LocalLongFullPath.ConvertInvalidCharactersInternal(fileName, Path.GetInvalidFileNameChars());
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003B38 File Offset: 0x00001D38
		protected static string ConvertInvalidCharactersInternal(string fileName, char[] invalidChars)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("fileName");
			}
			fileName = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(fileName));
			Array.Sort<char>(invalidChars);
			StringBuilder stringBuilder = new StringBuilder(fileName.Length + 1);
			foreach (char c in fileName)
			{
				if (Array.BinarySearch<char>(invalidChars, c) < 0 && c != '~')
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('_');
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				' ',
				'.'
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003BE4 File Offset: 0x00001DE4
		protected static void ValidatePathWithSpecifiedExtension(LocalLongFullPath path, string specifiedExtension)
		{
			if (string.IsNullOrEmpty(specifiedExtension))
			{
				specifiedExtension = string.Empty;
			}
			else if (specifiedExtension[0] != '.' || specifiedExtension.Length == 1 || -1 != specifiedExtension.IndexOfAny(("." + new string(Path.GetInvalidFileNameChars())).ToCharArray(), 1))
			{
				throw new FormatException(DataStrings.ErrorInvalidExtension(specifiedExtension));
			}
			if (specifiedExtension != null && string.Compare(Path.GetExtension(path.PathName), specifiedExtension, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new FormatException(DataStrings.ErrorFilePathMismatchExpectedExtension(path.PathName, specifiedExtension));
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003C6E File Offset: 0x00001E6E
		public void ValidateDirectoryPathLength()
		{
			LocalLongFullPath.ValidateDirectoryPath(base.PathName);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C7C File Offset: 0x00001E7C
		protected static void ValidateDirectoryPath(string input)
		{
			try
			{
				string text = Path.GetFullPath(input);
				if (Path.DirectorySeparatorChar == text[text.Length - 1] || Path.AltDirectorySeparatorChar == text[text.Length - 1])
				{
					text = Path.GetDirectoryName(text);
				}
				if (!string.IsNullOrEmpty(text) && 248 <= text.Length)
				{
					throw new PathTooLongException(DataStrings.ErrorLocalLongFullPathTooLong(input));
				}
			}
			catch (IOException ex)
			{
				throw new FormatException(ex.Message, ex);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D04 File Offset: 0x00001F04
		protected static void ValidateFilePath(string input)
		{
			try
			{
				string text = Path.GetFullPath(input);
				if (Path.DirectorySeparatorChar == text[text.Length - 1] || Path.AltDirectorySeparatorChar == text[text.Length - 1])
				{
					throw new FormatException(DataStrings.ErrorInvalidFullyQualifiedFileName(input));
				}
				if (Path.IsPathRooted(text) && 259 <= text.Length)
				{
					throw new PathTooLongException(DataStrings.ErrorLocalLongFullPathTooLong(input));
				}
				text = Path.GetDirectoryName(text);
				if (!string.IsNullOrEmpty(text) && 248 <= text.Length)
				{
					throw new PathTooLongException(DataStrings.ErrorLocalLongFullPathTooLong(input));
				}
			}
			catch (IOException ex)
			{
				throw new FormatException(ex.Message, ex);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DB8 File Offset: 0x00001FB8
		protected override bool ParseCore(string path, bool nothrow)
		{
			if (base.ParseCore(path, nothrow))
			{
				if (!base.IsLocalFull)
				{
					base.IsValid = false;
				}
				else
				{
					try
					{
						Path.GetFullPath(base.PathName);
					}
					catch (IOException ex)
					{
						base.IsValid = false;
						if (!nothrow)
						{
							throw new ArgumentException(ex.Message, ex);
						}
					}
				}
			}
			if (!base.IsValid && !nothrow)
			{
				throw new ArgumentException(DataStrings.ErrorLocalLongFullPathCannotConvert(path), "path");
			}
			return base.IsValid;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003E3C File Offset: 0x0000203C
		public void ValidateFilePathLength()
		{
			LocalLongFullPath.ValidateFilePath(base.PathName);
		}

		// Token: 0x04000031 RID: 49
		private const int MaxDirectoryPath = 248;

		// Token: 0x04000032 RID: 50
		private const int MaxPath = 260;
	}
}
