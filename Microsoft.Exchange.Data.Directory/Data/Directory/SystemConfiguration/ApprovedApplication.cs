using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004FA RID: 1274
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[ImmutableObject(true)]
	[Serializable]
	public class ApprovedApplication : IComparable, IComparable<ApprovedApplication>
	{
		// Token: 0x06003879 RID: 14457 RVA: 0x000DAA20 File Offset: 0x000D8C20
		protected ApprovedApplication(string appHash, string appName, string cabName, bool isFromFile)
		{
			if (appHash == null)
			{
				throw new ArgumentNullException("appHash");
			}
			if (appName == null)
			{
				throw new ArgumentNullException("appName");
			}
			this.appHash = appHash;
			this.appName = appName;
			this.cabName = cabName;
			this.isFromFile = isFromFile;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000DAA6C File Offset: 0x000D8C6C
		protected ApprovedApplication(string cabFile)
		{
			if (cabFile == null)
			{
				throw new ArgumentNullException("cabFile");
			}
			this.cabName = cabFile;
			this.isCab = true;
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x000DAA90 File Offset: 0x000D8C90
		public string AppString
		{
			get
			{
				return this.ApprovedApplicationString;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000DAA98 File Offset: 0x000D8C98
		public string AppHash
		{
			get
			{
				return this.appHash;
			}
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000DAAA0 File Offset: 0x000D8CA0
		public string AppName
		{
			get
			{
				return this.appName;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x000DAAA8 File Offset: 0x000D8CA8
		public string CabName
		{
			get
			{
				return this.cabName;
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000DAAB0 File Offset: 0x000D8CB0
		internal bool IsCab
		{
			get
			{
				return this.isCab;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000DAAB8 File Offset: 0x000D8CB8
		internal bool IsFromFile
		{
			get
			{
				return this.isFromFile;
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000DAAC0 File Offset: 0x000D8CC0
		public static ApprovedApplication Parse(string appValue)
		{
			if (appValue == null)
			{
				throw new ArgumentNullException("appValue");
			}
			string text = null;
			bool flag = false;
			string text2;
			string text3;
			if (appValue.Length > 40 && appValue[40] == ':')
			{
				string[] array = appValue.Split(new char[]
				{
					':'
				});
				if (array == null || array.Length < 2)
				{
					throw new ArgumentException("appValue");
				}
				text2 = array[0];
				text3 = array[1];
				text = ((array.Length > 2) ? array[2] : null);
			}
			else
			{
				FileInfo fileInfo = new FileInfo(appValue);
				if (!fileInfo.Exists)
				{
					throw new FileNotFoundException(appValue);
				}
				if (StringComparer.OrdinalIgnoreCase.Compare(fileInfo.Extension, ".cab") == 0)
				{
					return new ApprovedApplication(appValue);
				}
				text3 = fileInfo.Name;
				text2 = ApprovedApplication.GetSHA1Hash(appValue, false);
				flag = true;
			}
			return new ApprovedApplication(text2, text3, text, flag);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000DAB98 File Offset: 0x000D8D98
		public static MultiValuedProperty<ApprovedApplication> ParseCab(string cabFile)
		{
			if (cabFile == null)
			{
				throw new ArgumentNullException("cabFile");
			}
			string text = null;
			MultiValuedProperty<ApprovedApplication> result;
			try
			{
				FileInfo fileInfo = new FileInfo(cabFile);
				text = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("D"));
				Directory.CreateDirectory(text);
				ApprovedApplication.OpenCabinetFile(fileInfo.FullName, text);
				result = ApprovedApplication.BuildApprovedApplicationList(text, fileInfo.Name);
			}
			finally
			{
				if (text != null)
				{
					Directory.Delete(text, true);
				}
			}
			return result;
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000DAC18 File Offset: 0x000D8E18
		public static bool operator ==(ApprovedApplication a, ApprovedApplication b)
		{
			return a == b || (a != null && b != null && StringComparer.OrdinalIgnoreCase.Equals(a.AppHash, b.AppHash) && StringComparer.OrdinalIgnoreCase.Equals(a.AppName, b.AppName));
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000DAC58 File Offset: 0x000D8E58
		public static bool operator !=(ApprovedApplication a, ApprovedApplication b)
		{
			return !(a == b);
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000DAC64 File Offset: 0x000D8E64
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.AppHash) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(this.AppName);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000DAC87 File Offset: 0x000D8E87
		public override bool Equals(object obj)
		{
			return this == obj as ApprovedApplication;
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000DAC98 File Offset: 0x000D8E98
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			ApprovedApplication approvedApplication = other as ApprovedApplication;
			if (approvedApplication == null)
			{
				throw new ArgumentException(DirectoryStrings.ExInvalidTypeArgumentException("other", other.GetType(), typeof(ApprovedApplication)), "other");
			}
			return this.CompareTo(approvedApplication);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000DACEC File Offset: 0x000D8EEC
		public int CompareTo(ApprovedApplication other)
		{
			if (null == other)
			{
				return 1;
			}
			int num = StringComparer.OrdinalIgnoreCase.Compare(this.AppName, other.AppName);
			if (num != 0)
			{
				return num;
			}
			return StringComparer.OrdinalIgnoreCase.Compare(this.AppHash, other.AppHash);
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000DAD36 File Offset: 0x000D8F36
		public bool Equals(ApprovedApplication other)
		{
			return this == other;
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x000DAD40 File Offset: 0x000D8F40
		internal string ApprovedApplicationString
		{
			get
			{
				if (this.appString == null)
				{
					if (string.IsNullOrEmpty(this.CabName))
					{
						this.appString = string.Format("{0}{1}{2}", this.AppHash, ':', this.AppName);
					}
					else
					{
						this.appString = string.Format("{0}{1}{2}{1}{3}", new object[]
						{
							this.AppHash,
							':',
							this.AppName,
							this.CabName
						});
					}
				}
				return this.appString;
			}
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000DADCC File Offset: 0x000D8FCC
		internal static void SHA1TransformBlock(HashAlgorithm sha1, Stream stream, long readSize)
		{
			byte[] array = new byte[4096];
			int num;
			do
			{
				num = stream.Read(array, 0, (int)((readSize > (long)array.Length) ? ((long)array.Length) : readSize));
				if (num > 0)
				{
					sha1.TransformBlock(array, 0, num, array, 0);
				}
				readSize -= (long)num;
			}
			while (num > 0);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000DAE18 File Offset: 0x000D9018
		internal static string GetSHA1Hash(string fileName, bool hashPEOnly)
		{
			byte[] array = null;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (SHA1Cng sha1Cng = new SHA1Cng())
				{
					List<long[]> list = null;
					if (ApprovedApplication.IsPEFile(fileName, out list))
					{
						for (int i = 0; i < list.Count; i++)
						{
							long num = list[i][0];
							long offset = list[i][1];
							ApprovedApplication.SHA1TransformBlock(sha1Cng, fileStream, num - fileStream.Position);
							fileStream.Seek(offset, SeekOrigin.Current);
						}
						if (fileStream.Position < fileStream.Length)
						{
							ApprovedApplication.SHA1TransformBlock(sha1Cng, fileStream, fileStream.Length - fileStream.Position);
						}
						sha1Cng.TransformFinalBlock(new byte[0], 0, 0);
						array = sha1Cng.Hash;
					}
					else if (!hashPEOnly)
					{
						array = sha1Cng.ComputeHash(fileStream);
					}
				}
			}
			StringBuilder stringBuilder = null;
			if (array != null)
			{
				stringBuilder = new StringBuilder(2 * array.Length);
				for (int j = 0; j < array.Length; j++)
				{
					stringBuilder.AppendFormat("{0:x2}", array[j]);
				}
			}
			if (stringBuilder == null)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000DAF50 File Offset: 0x000D9150
		internal static void OpenCabinetFile(string cabName, string outputPath)
		{
			Process process = Process.Start(new ProcessStartInfo("extrac32.exe", string.Format("/Y /E /L \"{0}\" \"{1}\"", outputPath, cabName))
			{
				CreateNoWindow = true
			});
			process.WaitForExit();
			process.Close();
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000DAF90 File Offset: 0x000D9190
		internal static bool IsPEFile(string fileName, out List<long[]> skipInterval)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			skipInterval = new List<long[]>();
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (fileStream.Length <= 60L)
					{
						return false;
					}
					fileStream.Seek(60L, SeekOrigin.Begin);
					int num = (int)binaryReader.ReadByte();
					if (fileStream.Length < (long)(num + 4))
					{
						return false;
					}
					fileStream.Seek((long)num, SeekOrigin.Begin);
					if (binaryReader.ReadByte() != 80 || binaryReader.ReadByte() != 69 || binaryReader.ReadByte() != 0 || binaryReader.ReadByte() != 0)
					{
						return false;
					}
					if (fileStream.Length < (long)(num + 4 + 20 + 2))
					{
						return false;
					}
					fileStream.Seek(20L, SeekOrigin.Current);
					int num2 = (int)((ushort)binaryReader.ReadInt16());
					int num3 = (num2 == 523) ? 16 : 0;
					if (fileStream.Length < (long)(num + 4 + 20 + 96 + num3))
					{
						return false;
					}
					long[] item = new long[]
					{
						(long)(num + 4 + 20 + 64),
						4L
					};
					skipInterval.Add(item);
					fileStream.Seek((long)(90 + num3), SeekOrigin.Current);
					long num4 = (long)((ulong)binaryReader.ReadInt32());
					if (num4 < 5L)
					{
						return true;
					}
					if (fileStream.Length < (long)(num + 4 + 20 + 96 + num3) + 8L * num4)
					{
						return false;
					}
					fileStream.Seek(32L, SeekOrigin.Current);
					long num5 = (long)((ulong)binaryReader.ReadInt32());
					long num6 = (long)((ulong)binaryReader.ReadInt32());
					if (fileStream.Length < num5 + num6)
					{
						return false;
					}
					item = new long[]
					{
						(long)(num + 4 + 20 + 96 + 32 + num3),
						8L
					};
					skipInterval.Add(item);
					if (num5 > 0L && num6 > 0L)
					{
						item = new long[]
						{
							num5,
							num6
						};
						skipInterval.Add(item);
					}
				}
			}
			return true;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000DB1CC File Offset: 0x000D93CC
		private static MultiValuedProperty<ApprovedApplication> BuildApprovedApplicationList(string path, string cabName)
		{
			MultiValuedProperty<ApprovedApplication> multiValuedProperty = new MultiValuedProperty<ApprovedApplication>();
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			FileInfo[] files = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
			foreach (FileInfo fileInfo in files)
			{
				fileInfo.IsReadOnly = false;
				string sha1Hash = ApprovedApplication.GetSHA1Hash(fileInfo.FullName, true);
				if (sha1Hash != null)
				{
					multiValuedProperty.Add(new ApprovedApplication(sha1Hash, fileInfo.Name, cabName, true));
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000DB23E File Offset: 0x000D943E
		public override string ToString()
		{
			return this.ApprovedApplicationString;
		}

		// Token: 0x04002695 RID: 9877
		internal const char SeparatorCharacter = ':';

		// Token: 0x04002696 RID: 9878
		private string appHash;

		// Token: 0x04002697 RID: 9879
		private string appName;

		// Token: 0x04002698 RID: 9880
		private string cabName;

		// Token: 0x04002699 RID: 9881
		private bool isCab;

		// Token: 0x0400269A RID: 9882
		private bool isFromFile;

		// Token: 0x0400269B RID: 9883
		private string appString;
	}
}
