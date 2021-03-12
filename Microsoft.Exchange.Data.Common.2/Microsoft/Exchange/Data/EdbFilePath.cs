using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public sealed class EdbFilePath : LocalLongFullPath
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003EC2 File Offset: 0x000020C2
		private bool IsTemporaryEdbFile
		{
			get
			{
				return 0 == string.Compare(Path.GetFileName(base.PathName), "tmp.edb", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003EE0 File Offset: 0x000020E0
		public bool IsPathInRootDirectory
		{
			get
			{
				if (!base.IsValid)
				{
					throw new NotSupportedException("IsPathInRootDirectory");
				}
				if (this.isPathInRootDirectory == null)
				{
					string directoryName = Path.GetDirectoryName(base.PathName);
					if (string.IsNullOrEmpty(directoryName) || string.IsNullOrEmpty(Path.GetDirectoryName(directoryName)))
					{
						this.isPathInRootDirectory = new bool?(true);
					}
					else
					{
						this.isPathInRootDirectory = new bool?(false);
					}
				}
				return this.isPathInRootDirectory.Value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003F53 File Offset: 0x00002153
		public new static EdbFilePath Parse(string pathName)
		{
			return (EdbFilePath)LongPath.ParseInternal(pathName, new EdbFilePath());
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F65 File Offset: 0x00002165
		public static bool TryParse(string path, out EdbFilePath resultObject)
		{
			resultObject = (EdbFilePath)LongPath.TryParseInternal(path, new EdbFilePath());
			return null != resultObject;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003F84 File Offset: 0x00002184
		protected override bool ParseCore(string path, bool nothrow)
		{
			if (base.ParseCore(path, nothrow))
			{
				if (this.IsTemporaryEdbFile)
				{
					base.IsValid = false;
					if (!nothrow)
					{
						throw new ArgumentException(DataStrings.ErrorEdbFileCannotBeTmp(path), "path");
					}
				}
				else
				{
					string fileName = Path.GetFileName(base.PathName);
					try
					{
						LocalLongFullPath.ValidateFilePath(Path.Combine(Path.Combine(EdbFilePath.DefaultEdbFilePath, "LocalCopies"), fileName) + "0000");
					}
					catch (FormatException innerException)
					{
						base.IsValid = false;
						if (!nothrow)
						{
							throw new ArgumentException(DataStrings.ErrorEdbFileNameTooLong(fileName), innerException);
						}
					}
				}
			}
			if (!base.IsValid && !nothrow)
			{
				throw new ArgumentException(DataStrings.ErrorEdbFilePathCannotConvert(path));
			}
			return base.IsValid;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004038 File Offset: 0x00002238
		public new static EdbFilePath ParseFromPathNameAndFileName(string pathName, string fileName)
		{
			return EdbFilePath.Parse(Path.Combine(pathName, fileName));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004046 File Offset: 0x00002246
		public void ValidateEdbFileExtension()
		{
			LocalLongFullPath.ValidatePathWithSpecifiedExtension(this, ".edb");
		}

		// Token: 0x04000033 RID: 51
		private const string EdbFileExtensionString = ".edb";

		// Token: 0x04000034 RID: 52
		public const string TemporaryDatabaseFileName = "tmp.edb";

		// Token: 0x04000035 RID: 53
		public const string DefaultLocalCopyDirectoryName = "LocalCopies";

		// Token: 0x04000036 RID: 54
		public const string MaximumRetrySuffix = "0000";

		// Token: 0x04000037 RID: 55
		private bool? isPathInRootDirectory;

		// Token: 0x04000038 RID: 56
		public static readonly string DefaultEdbFilePath = Path.GetFullPath("X:\\Program Files\\");
	}
}
