using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Cluster.Replay.IO;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000E9 RID: 233
	internal class EseLogEnumerator : DirectoryEnumerator
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0002D17A File Offset: 0x0002B37A
		public EseLogEnumerator(DirectoryInfo path, string logPrefix, string logSuffix) : base(path, false, false)
		{
			this.m_prefix = logPrefix;
			this.m_suffix = logSuffix;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0002D193 File Offset: 0x0002B393
		public long FindLowestGeneration()
		{
			if (RegistryParameters.FilesystemMaintainsOrder)
			{
				return this.FindLowestGenerationFast();
			}
			return this.FindLowestGenerationSlow();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		public long FindLowestGenerationFast()
		{
			long result = 0L;
			string filter = this.m_prefix + EseLogEnumerator.hexFieldPattern + this.m_suffix;
			string pattern = string.Format("^{0}([0-9A-F]{{8}}){1}$", this.m_prefix, this.m_suffix);
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			base.ReturnBaseNames = true;
			foreach (string input in base.EnumerateFiles(filter, null))
			{
				Match match = regex.Match(input);
				if (match.Success)
				{
					Group group = match.Groups[1];
					string s = group.ToString();
					result = (long)ulong.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					break;
				}
			}
			return result;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002D2B4 File Offset: 0x0002B4B4
		public long FindLowestGenerationSlow()
		{
			string filter = string.Empty;
			long? num = null;
			for (int i = 7; i >= 0; i--)
			{
				for (int j = 1; j <= 15; j++)
				{
					filter = this.BuildFilter(i, j);
					num = base.EnumerateFiles(filter, null).Min(delegate(string file)
					{
						long value;
						if (string.IsNullOrEmpty(file) || !EseHelper.GetGenerationNumberFromFilename(file, this.m_prefix, out value))
						{
							return null;
						}
						return new long?(value);
					});
					if (num != null)
					{
						return num.Value;
					}
				}
			}
			return 0L;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002D364 File Offset: 0x0002B564
		public string FindHighestGenerationLogFile()
		{
			string filter = string.Empty;
			string result = null;
			long? num = null;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 15; j > 0; j--)
				{
					filter = this.BuildFilter(i, j);
					num = base.EnumerateFiles(filter, null).Max(delegate(string file)
					{
						long value;
						if (string.IsNullOrEmpty(file) || !EseHelper.GetGenerationNumberFromFilename(file, this.m_prefix, out value))
						{
							return null;
						}
						return new long?(value);
					});
					if (num != null)
					{
						return EseHelper.MakeLogfileName(this.m_prefix, this.m_suffix, num.Value);
					}
				}
			}
			return result;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002D6E8 File Offset: 0x0002B8E8
		public IEnumerable<string> GetLogFiles(bool includeE00)
		{
			string filter = string.Empty;
			for (int digitIndex = 7; digitIndex >= 0; digitIndex--)
			{
				for (int digit = 1; digit <= 15; digit++)
				{
					filter = this.BuildFilter(digitIndex, digit);
					IOrderedEnumerable<string> orderedFiles = from file in base.EnumerateFiles(filter, null)
					where this.IsValidEseLogFileName(file)
					orderby this.GetGenerationFromFileName(file)
					select file;
					foreach (string fileName in orderedFiles)
					{
						yield return fileName;
					}
				}
			}
			if (includeE00)
			{
				filter = this.BuildE00Filter();
				string fileName2;
				if (base.GetNextFile(filter, out fileName2))
				{
					yield return fileName2;
				}
			}
			yield break;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002D70C File Offset: 0x0002B90C
		protected override LocalizedString GetIOExceptionMessage(string directoryName, string apiName, string ioErrorMessage, int win32ErrorCode)
		{
			return ReplayStrings.EseLogEnumeratorIOError(apiName, ioErrorMessage, win32ErrorCode, directoryName);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002D718 File Offset: 0x0002B918
		private string BuildE00Filter()
		{
			return this.m_prefix + this.m_suffix;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002D72B File Offset: 0x0002B92B
		private string BuildFilter(int firstDigit)
		{
			return this.BuildFilter(0, firstDigit);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002D738 File Offset: 0x0002B938
		private string BuildFilter(int digitIndex, int digit)
		{
			return string.Concat(new object[]
			{
				this.m_prefix,
				new string('0', digitIndex),
				digit.ToString("X"),
				'*',
				this.m_suffix
			});
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002D78C File Offset: 0x0002B98C
		private bool IsValidEseLogFileName(string fileName)
		{
			long num;
			return EseHelper.GetGenerationNumberFromFilename(fileName, this.m_prefix, out num);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002D7A8 File Offset: 0x0002B9A8
		private long GetGenerationFromFileName(string fileName)
		{
			long result;
			EseHelper.GetGenerationNumberFromFilename(fileName, this.m_prefix, out result);
			return result;
		}

		// Token: 0x040003E3 RID: 995
		private const int EseFileNameHexDigits = 8;

		// Token: 0x040003E4 RID: 996
		private readonly string m_prefix;

		// Token: 0x040003E5 RID: 997
		private readonly string m_suffix;

		// Token: 0x040003E6 RID: 998
		private static readonly string hexFieldPattern = new string('?', 8);
	}
}
