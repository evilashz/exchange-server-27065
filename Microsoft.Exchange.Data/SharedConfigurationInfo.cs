using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public sealed class SharedConfigurationInfo
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0002C364 File Offset: 0x0002A564
		public SharedConfigurationInfo(ServerVersion currentVersion, string programId, string offerId)
		{
			if (currentVersion == null)
			{
				throw new ArgumentNullException("currentVersion");
			}
			if (string.IsNullOrEmpty(programId))
			{
				throw new ArgumentNullException("programId");
			}
			if (programId.Contains("-"))
			{
				throw new ArgumentException("programId cannot contain -");
			}
			if (string.IsNullOrEmpty(offerId))
			{
				throw new ArgumentNullException("offerId");
			}
			if (offerId.Contains("-"))
			{
				throw new ArgumentException("offerId cannot contain -");
			}
			this.currentVersion = currentVersion;
			this.programId = programId;
			this.offerId = offerId;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0002C3F6 File Offset: 0x0002A5F6
		public ServerVersion CurrentVersion
		{
			get
			{
				return this.currentVersion;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0002C3FE File Offset: 0x0002A5FE
		public string ProgramId
		{
			get
			{
				return this.programId;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0002C406 File Offset: 0x0002A606
		public string OfferId
		{
			get
			{
				return this.offerId;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0002C40E File Offset: 0x0002A60E
		public static SharedConfigurationInfo FromInstalledVersion(string programId, string offerId)
		{
			return new SharedConfigurationInfo(ServerVersion.InstalledVersion, programId, offerId);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002C41C File Offset: 0x0002A61C
		public bool Equals(SharedConfigurationInfo value)
		{
			return object.ReferenceEquals(this, value) || (value != null && this.currentVersion.Equals(value.currentVersion) && this.programId.Equals(value.programId, StringComparison.OrdinalIgnoreCase) && this.offerId.Equals(value.offerId, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002C478 File Offset: 0x0002A678
		public override bool Equals(object comparand)
		{
			SharedConfigurationInfo sharedConfigurationInfo = comparand as SharedConfigurationInfo;
			return sharedConfigurationInfo != null && this.Equals(sharedConfigurationInfo);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002C498 File Offset: 0x0002A698
		public static bool operator ==(SharedConfigurationInfo left, SharedConfigurationInfo right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002C4A9 File Offset: 0x0002A6A9
		public static bool operator !=(SharedConfigurationInfo left, SharedConfigurationInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002C4B5 File Offset: 0x0002A6B5
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002C4C2 File Offset: 0x0002A6C2
		public string GetPrefix()
		{
			return this.GetPrefix(true);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		private string GetPrefix(bool useWildcard)
		{
			return string.Join("_", new string[]
			{
				this.currentVersion.Major.ToString(),
				this.currentVersion.Minor.ToString(),
				this.currentVersion.Build.ToString(),
				useWildcard ? "*" : this.currentVersion.Revision.ToString()
			});
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002C550 File Offset: 0x0002A750
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.cachedToStringValue))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.GetPrefix(false));
				stringBuilder.Append("-");
				stringBuilder.Append(this.programId);
				stringBuilder.Append("-");
				stringBuilder.Append(this.offerId);
				if (!string.IsNullOrEmpty(this.extension))
				{
					stringBuilder.Append("-");
					stringBuilder.Append(this.extension);
				}
				this.cachedToStringValue = stringBuilder.ToString();
			}
			return this.cachedToStringValue;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002C5E8 File Offset: 0x0002A7E8
		public static SharedConfigurationInfo Parse(string strValue)
		{
			SharedConfigurationInfo result;
			if (!SharedConfigurationInfo.TryParse(strValue, out result))
			{
				throw new ArgumentException("strValue: " + strValue);
			}
			return result;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002C614 File Offset: 0x0002A814
		public static bool TryParse(string strValue, out SharedConfigurationInfo sharedConfigInfo)
		{
			sharedConfigInfo = null;
			if (string.IsNullOrEmpty(strValue))
			{
				return false;
			}
			string[] array = strValue.Split(new string[]
			{
				"-"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array == null || array.Length < 3)
			{
				return false;
			}
			int i = 0;
			string[] array2 = array[i].Split(new string[]
			{
				"_"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array2 == null || array2.Length < 4)
			{
				return false;
			}
			ServerVersion serverVersion = new ServerVersion(int.Parse(array2[0]), int.Parse(array2[1]), int.Parse(array2[2]), int.Parse(array2[3]));
			i++;
			if (string.IsNullOrEmpty(array[i]) || string.IsNullOrEmpty(array[i + 1]))
			{
				return false;
			}
			string text = array[i];
			string text2 = array[i + 1];
			i += 2;
			sharedConfigInfo = new SharedConfigurationInfo(serverVersion, text, text2);
			string text3 = null;
			while (i < array.Length)
			{
				text3 = string.Join("-", new string[]
				{
					text3,
					array[i]
				});
				i++;
			}
			sharedConfigInfo.extension = text3;
			return true;
		}

		// Token: 0x04000872 RID: 2162
		private const int MinimumNumberOfComponents = 3;

		// Token: 0x04000873 RID: 2163
		public const string InnerSeparator = "_";

		// Token: 0x04000874 RID: 2164
		public const string OuterSeparator = "-";

		// Token: 0x04000875 RID: 2165
		public const string CurrentTypeHeader = "C";

		// Token: 0x04000876 RID: 2166
		private ServerVersion currentVersion;

		// Token: 0x04000877 RID: 2167
		private string programId;

		// Token: 0x04000878 RID: 2168
		private string offerId;

		// Token: 0x04000879 RID: 2169
		private string extension;

		// Token: 0x0400087A RID: 2170
		private string cachedToStringValue;
	}
}
