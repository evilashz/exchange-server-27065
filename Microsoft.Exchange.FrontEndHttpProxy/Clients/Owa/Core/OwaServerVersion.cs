using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200006A RID: 106
	public class OwaServerVersion
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00013D18 File Offset: 0x00011F18
		private OwaServerVersion(int major, int minor, int build, int dot)
		{
			this.major = major;
			this.minor = minor;
			this.build = build;
			this.dot = dot;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00013D3D File Offset: 0x00011F3D
		public int Major
		{
			get
			{
				return this.major;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00013D45 File Offset: 0x00011F45
		public int Minor
		{
			get
			{
				return this.minor;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00013D4D File Offset: 0x00011F4D
		public int Build
		{
			get
			{
				return this.build;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00013D55 File Offset: 0x00011F55
		public int Dot
		{
			get
			{
				return this.dot;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00013D60 File Offset: 0x00011F60
		public static OwaServerVersion CreateFromVersionNumber(int versionNumber)
		{
			int num = versionNumber & 32767;
			int num2 = versionNumber >> 16 & 63;
			int num3 = versionNumber >> 22 & 63;
			int num4 = 0;
			return new OwaServerVersion(num3, num2, num, num4);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00013D90 File Offset: 0x00011F90
		public static bool IsE14SP1OrGreater(int versionNumber)
		{
			OwaServerVersion owaServerVersion = OwaServerVersion.CreateFromVersionNumber(versionNumber);
			return owaServerVersion.Major >= 14 && owaServerVersion.Minor >= 1;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00013DBC File Offset: 0x00011FBC
		public static OwaServerVersion CreateFromVersionString(string versionString)
		{
			if (versionString == null)
			{
				throw new ArgumentNullException("versionString");
			}
			return OwaServerVersion.TryValidateVersionString(versionString);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00013DD4 File Offset: 0x00011FD4
		public static int Compare(OwaServerVersion a, OwaServerVersion b)
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
						num = a.Dot - b.Dot;
					}
				}
			}
			return num;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00013E3F File Offset: 0x0001203F
		public static bool IsEqualMajorVersion(int a, int b)
		{
			return ((a ^ b) & 264241152) == 0;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00013E50 File Offset: 0x00012050
		public override string ToString()
		{
			if (this.versionString == null)
			{
				string arg = string.Concat(new object[]
				{
					this.major,
					".",
					this.minor,
					".",
					this.build
				});
				if (this.dot != -1)
				{
					arg = arg + "." + this.dot;
				}
				this.versionString = arg;
			}
			return this.versionString;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00013EDC File Offset: 0x000120DC
		private static OwaServerVersion TryValidateVersionString(string versionString)
		{
			if (string.IsNullOrEmpty(versionString))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			bool flag = false;
			int nextVersionPart = OwaServerVersion.GetNextVersionPart(versionString, 0, 2, out num, out flag);
			if (nextVersionPart == -1 || flag)
			{
				return null;
			}
			nextVersionPart = OwaServerVersion.GetNextVersionPart(versionString, nextVersionPart, 2, out num2, out flag);
			if (nextVersionPart == -1 || flag)
			{
				return null;
			}
			nextVersionPart = OwaServerVersion.GetNextVersionPart(versionString, nextVersionPart, 4, out num3, out flag);
			if (nextVersionPart == -1)
			{
				return null;
			}
			if (!flag)
			{
				nextVersionPart = OwaServerVersion.GetNextVersionPart(versionString, nextVersionPart, 4, out num4, out flag);
				if (nextVersionPart == -1 || !flag)
				{
					return null;
				}
			}
			return new OwaServerVersion(num, num2, num3, num4);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00013F70 File Offset: 0x00012170
		private static int GetNextVersionPart(string versionString, int start, int maximumLength, out int part, out bool foundEnd)
		{
			bool flag = false;
			int num = start;
			part = 0;
			foundEnd = false;
			StringBuilder stringBuilder = new StringBuilder(maximumLength);
			for (;;)
			{
				if (num == versionString.Length)
				{
					if (stringBuilder.Length == 0)
					{
						break;
					}
					flag = true;
					foundEnd = true;
				}
				else
				{
					char c = versionString[num];
					if (c == '.')
					{
						flag = true;
					}
					else
					{
						if (!char.IsDigit(c))
						{
							return -1;
						}
						if (maximumLength == stringBuilder.Length)
						{
							return -1;
						}
						stringBuilder.Append(c);
					}
				}
				num++;
				if (flag)
				{
					goto Block_6;
				}
			}
			return -1;
			Block_6:
			part = int.Parse(stringBuilder.ToString());
			return num;
		}

		// Token: 0x04000237 RID: 567
		private readonly int major;

		// Token: 0x04000238 RID: 568
		private readonly int minor;

		// Token: 0x04000239 RID: 569
		private readonly int build;

		// Token: 0x0400023A RID: 570
		private readonly int dot;

		// Token: 0x0400023B RID: 571
		private string versionString;

		// Token: 0x0200006B RID: 107
		public class ServerVersionComparer : IEqualityComparer<OwaServerVersion>
		{
			// Token: 0x06000351 RID: 849 RVA: 0x00013FED File Offset: 0x000121ED
			public bool Equals(OwaServerVersion a, OwaServerVersion b)
			{
				return this.GetHashCode(a) == this.GetHashCode(b);
			}

			// Token: 0x06000352 RID: 850 RVA: 0x00014000 File Offset: 0x00012200
			public int GetHashCode(OwaServerVersion owaServerVersion)
			{
				if (owaServerVersion == null)
				{
					throw new ArgumentNullException("owaServerVersion");
				}
				return (owaServerVersion.major << 26 & -67108864) | (owaServerVersion.minor << 20 & 66060288) | (owaServerVersion.build << 5 & 1048544) | (owaServerVersion.dot & 31);
			}
		}
	}
}
