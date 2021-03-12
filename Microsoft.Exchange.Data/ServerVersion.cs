using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000092 RID: 146
	[ImmutableObject(true)]
	[Serializable]
	public sealed class ServerVersion : IComparable, IComparable<ServerVersion>
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000EAA5 File Offset: 0x0000CCA5
		public ServerVersion(int major, int minor, int build, int revision)
		{
			this.version = new Version(major, minor, build, revision);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000EABD File Offset: 0x0000CCBD
		public ServerVersion(int major, int minor, int build, int revision, string filePatchLevelDescription) : this(major, minor, build, revision)
		{
			this.filePatchLevelDescription = filePatchLevelDescription;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000EAD2 File Offset: 0x0000CCD2
		public ServerVersion(int versionNumber) : this(versionNumber >> 22 & 63, versionNumber >> 16 & 63, versionNumber & 32767, 0, string.Empty)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000EAF5 File Offset: 0x0000CCF5
		public ServerVersion(long versionNumber) : this((int)(versionNumber >> 48 & 65535L), (int)(versionNumber >> 32 & 65535L), (int)(versionNumber >> 16 & 65535L), (int)(versionNumber & 65535L), string.Empty)
		{
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000EB2F File Offset: 0x0000CD2F
		public int Major
		{
			get
			{
				return this.version.Major;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		public int Minor
		{
			get
			{
				return this.version.Minor;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000EB49 File Offset: 0x0000CD49
		public int Build
		{
			get
			{
				return this.version.Build;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000EB56 File Offset: 0x0000CD56
		public int Revision
		{
			get
			{
				return this.version.Revision;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000EB63 File Offset: 0x0000CD63
		public string FilePatchLevelDescription
		{
			get
			{
				return this.filePatchLevelDescription;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000EB6B File Offset: 0x0000CD6B
		internal static ServerVersion InstalledVersion
		{
			get
			{
				if (ServerVersion.installedVersion == null)
				{
					ServerVersion.installedVersion = ExchangeSetupContext.InstalledVersion;
				}
				return ServerVersion.installedVersion;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public static ServerVersion ParseFromSerialNumber(string serialNumber)
		{
			ServerVersion result;
			if (!ServerVersion.TryParseFromSerialNumber(serialNumber, out result))
			{
				throw new FormatException(DataStrings.ErrorSerialNumberFormatError(serialNumber));
			}
			return result;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public static bool TryParseFromSerialNumber(string serialNumber, out ServerVersion serverVersion)
		{
			serverVersion = null;
			if (serialNumber == null)
			{
				return false;
			}
			string text = null;
			string text2 = null;
			Match match = Regex.Match(serialNumber, "build (\\d+(\\.\\d+)*)", RegexOptions.IgnoreCase);
			if (match.Success && match.Groups.Count > 1)
			{
				text = match.Groups[1].Value;
				match = Regex.Match(serialNumber, "\\(build (\\d+(\\.\\d+)*): ([^\\)]+)\\)", RegexOptions.IgnoreCase);
				if (match.Success && match.Groups.Count > 3)
				{
					text2 = match.Groups[3].Value;
				}
			}
			match = Regex.Match(serialNumber, "version (\\d+\\.\\d+)", RegexOptions.IgnoreCase);
			if (match.Success && match.Groups.Count > 1)
			{
				string str = match.Groups[1].Value;
				if (text != null)
				{
					str = str + "." + text;
				}
				Version version = new Version(str);
				int num = version.Build;
				if (version.Major >= 8)
				{
					if (num >= 30000)
					{
						num = version.Build - 30000;
					}
					else if (num >= 10000)
					{
						num = version.Build - 10000;
					}
				}
				try
				{
					serverVersion = new ServerVersion(version.Major, version.Minor, num, version.Revision, text2);
					return true;
				}
				catch (ArgumentException)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000ED18 File Offset: 0x0000CF18
		public string ToString(bool addSerialNumberOffset)
		{
			int num = this.Build + ((addSerialNumberOffset && this.Major >= 8) ? 30000 : 0);
			if (!string.IsNullOrEmpty(this.FilePatchLevelDescription))
			{
				return string.Format("Version {0}.{1} (Build {2}.{3}: {4})", new object[]
				{
					this.Major,
					this.Minor,
					num,
					this.Revision,
					this.FilePatchLevelDescription
				});
			}
			return string.Format("Version {0}.{1} (Build {2}.{3})", new object[]
			{
				this.Major,
				this.Minor,
				num,
				this.Revision
			});
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000EDE2 File Offset: 0x0000CFE2
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000EDEB File Offset: 0x0000CFEB
		public int ToInt()
		{
			return (this.Build & 32767) | (this.Minor & 63) << 16 | (this.Major & 63) << 22 | 1879080960;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public long ToLong()
		{
			ulong num = (ulong)((long)this.Major & 65535L);
			num <<= 48;
			ulong num2 = (ulong)((long)this.Minor & 65535L);
			num2 <<= 32;
			ulong num3 = (ulong)((long)this.Build & 65535L);
			num3 <<= 16;
			ulong num4 = (ulong)((long)this.Revision & 65535L);
			return (long)(num | num2 | num3 | num4);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000EE7B File Offset: 0x0000D07B
		public bool Equals(ServerVersion other)
		{
			return other != null && this.version == other.version && string.Compare(this.FilePatchLevelDescription, other.FilePatchLevelDescription) == 0;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000EEAB File Offset: 0x0000D0AB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ServerVersion);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public override int GetHashCode()
		{
			int num = this.version.GetHashCode();
			if (!string.IsNullOrEmpty(this.FilePatchLevelDescription))
			{
				num ^= this.FilePatchLevelDescription.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		public static bool operator ==(ServerVersion left, ServerVersion right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000EEFA File Offset: 0x0000D0FA
		public static bool operator !=(ServerVersion left, ServerVersion right)
		{
			return !(left == right);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000EF06 File Offset: 0x0000D106
		public static implicit operator Version(ServerVersion sVersion)
		{
			return sVersion.version;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000EF0E File Offset: 0x0000D10E
		public static implicit operator ServerVersion(Version version)
		{
			return new ServerVersion(version.Major, version.Minor, version.Build, version.Revision);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000EF30 File Offset: 0x0000D130
		public static int Compare(ServerVersion a, ServerVersion b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			int num = a.Major - b.Major;
			if (num == 0)
			{
				num = a.Minor - b.Minor;
				if (num == 0)
				{
					num = a.Build - b.Build;
					if (num == 0)
					{
						num = a.Revision - b.Revision;
					}
				}
			}
			return num;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000EFA7 File Offset: 0x0000D1A7
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is ServerVersion))
			{
				throw new ArgumentException(DataStrings.InvalidTypeArgumentException("obj", obj.GetType(), typeof(ServerVersion)));
			}
			return ServerVersion.Compare(this, (ServerVersion)obj);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000EFE7 File Offset: 0x0000D1E7
		int IComparable<ServerVersion>.CompareTo(ServerVersion other)
		{
			if (null == other)
			{
				return 1;
			}
			return ServerVersion.Compare(this, other);
		}

		// Token: 0x0400020F RID: 527
		private const string ShortVersionFormat = "Version {0}.{1} (Build {2}.{3})";

		// Token: 0x04000210 RID: 528
		private const string LongVersionFormat = "Version {0}.{1} (Build {2}.{3}: {4})";

		// Token: 0x04000211 RID: 529
		private Version version;

		// Token: 0x04000212 RID: 530
		private string filePatchLevelDescription;

		// Token: 0x04000213 RID: 531
		private static ServerVersion installedVersion;
	}
}
