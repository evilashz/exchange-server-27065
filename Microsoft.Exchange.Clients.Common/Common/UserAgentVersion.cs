using System;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000030 RID: 48
	public class UserAgentVersion : IComparable<UserAgentVersion>
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00009B6C File Offset: 0x00007D6C
		public UserAgentVersion(int buildVersion, int majorVersion, int minorVersion)
		{
			this.build = buildVersion;
			this.major = majorVersion;
			this.minor = minorVersion;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009B8C File Offset: 0x00007D8C
		public UserAgentVersion(string version)
		{
			int[] array = new int[3];
			int[] array2 = array;
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			char c = '.';
			char c2 = '_';
			char value;
			if (version.IndexOf(c2) >= 0)
			{
				value = c2;
			}
			else
			{
				value = c;
			}
			for (;;)
			{
				num = version.IndexOf(value, num + 1);
				if (num == -1)
				{
					num = version.Length;
				}
				if (!int.TryParse(version.Substring(num3, num - num3), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out array2[num2]))
				{
					break;
				}
				num2++;
				num3 = num + 1;
				if (num2 >= array2.Length || num >= version.Length)
				{
					goto IL_8B;
				}
			}
			throw new ArgumentException("The version parameter is not a valid User Agent Version");
			IL_8B:
			this.build = array2[0];
			this.major = array2[1];
			this.minor = array2[2];
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009C3F File Offset: 0x00007E3F
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00009C47 File Offset: 0x00007E47
		public int Build
		{
			get
			{
				return this.build;
			}
			set
			{
				this.build = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009C50 File Offset: 0x00007E50
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00009C58 File Offset: 0x00007E58
		public int Major
		{
			get
			{
				return this.major;
			}
			set
			{
				this.major = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009C61 File Offset: 0x00007E61
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00009C69 File Offset: 0x00007E69
		public int Minor
		{
			get
			{
				return this.minor;
			}
			set
			{
				this.minor = value;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009C72 File Offset: 0x00007E72
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.Build, this.Major, this.Minor);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009CA0 File Offset: 0x00007EA0
		public int CompareTo(UserAgentVersion b)
		{
			int num = (this.Minor.ToString().Length > b.Minor.ToString().Length) ? this.Minor.ToString().Length : b.Minor.ToString().Length;
			int num2 = (this.Major.ToString().Length > b.Major.ToString().Length) ? this.Major.ToString().Length : b.Major.ToString().Length;
			int num3 = this.Minor + (int)Math.Pow(10.0, (double)num) * this.Major + (int)Math.Pow(10.0, (double)(num2 + num)) * this.Build;
			num = b.Minor.ToString().Length;
			int num4 = b.Minor + (int)Math.Pow(10.0, (double)num) * b.Major + (int)Math.Pow(10.0, (double)(num2 + num)) * b.Build;
			return num3 - num4;
		}

		// Token: 0x040002C5 RID: 709
		private const string FormatToString = "{0}.{1}.{2}";

		// Token: 0x040002C6 RID: 710
		private int build;

		// Token: 0x040002C7 RID: 711
		private int major;

		// Token: 0x040002C8 RID: 712
		private int minor;
	}
}
