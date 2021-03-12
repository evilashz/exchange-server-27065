using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200024A RID: 586
	public class ServerVersion
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x00079033 File Offset: 0x00077233
		private ServerVersion(int major, int minor, int build, int dot)
		{
			this.major = major;
			this.minor = minor;
			this.build = build;
			this.dot = dot;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00079058 File Offset: 0x00077258
		public static ServerVersion CreateFromVersionNumber(int versionNumber)
		{
			int num = versionNumber & 32767;
			int num2 = versionNumber >> 16 & 63;
			int num3 = versionNumber >> 22 & 63;
			int num4 = 0;
			return new ServerVersion(num3, num2, num, num4);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00079088 File Offset: 0x00077288
		public static bool IsE14SP1OrGreater(int versionNumber)
		{
			ServerVersion serverVersion = ServerVersion.CreateFromVersionNumber(versionNumber);
			return serverVersion.Major >= 14 && serverVersion.Minor >= 1;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000790B4 File Offset: 0x000772B4
		public static ServerVersion CreateFromVersionString(string versionString)
		{
			if (versionString == null)
			{
				throw new ArgumentNullException("versionString");
			}
			return ServerVersion.TryValidateVersionString(versionString);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000790CC File Offset: 0x000772CC
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
						num = a.Dot - b.Dot;
					}
				}
			}
			return num;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00079137 File Offset: 0x00077337
		public static bool IsEqualMajorVersion(int a, int b)
		{
			return ((a ^ b) & 264241152) == 0;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00079148 File Offset: 0x00077348
		private static ServerVersion TryValidateVersionString(string versionString)
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
			int nextVersionPart = ServerVersion.GetNextVersionPart(versionString, 0, 2, out num, out flag);
			if (nextVersionPart == -1 || flag)
			{
				return null;
			}
			nextVersionPart = ServerVersion.GetNextVersionPart(versionString, nextVersionPart, 2, out num2, out flag);
			if (nextVersionPart == -1 || flag)
			{
				return null;
			}
			nextVersionPart = ServerVersion.GetNextVersionPart(versionString, nextVersionPart, 4, out num3, out flag);
			if (nextVersionPart == -1)
			{
				return null;
			}
			if (!flag)
			{
				nextVersionPart = ServerVersion.GetNextVersionPart(versionString, nextVersionPart, 4, out num4, out flag);
				if (nextVersionPart == -1 || !flag)
				{
					return null;
				}
			}
			return new ServerVersion(num, num2, num3, num4);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000791DC File Offset: 0x000773DC
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

		// Token: 0x060013AF RID: 5039 RVA: 0x0007925C File Offset: 0x0007745C
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000792E6 File Offset: 0x000774E6
		public int Major
		{
			get
			{
				return this.major;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000792EE File Offset: 0x000774EE
		public int Minor
		{
			get
			{
				return this.minor;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000792F6 File Offset: 0x000774F6
		public int Build
		{
			get
			{
				return this.build;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000792FE File Offset: 0x000774FE
		public int Dot
		{
			get
			{
				return this.dot;
			}
		}

		// Token: 0x04000D8F RID: 3471
		private int major;

		// Token: 0x04000D90 RID: 3472
		private int minor;

		// Token: 0x04000D91 RID: 3473
		private int build;

		// Token: 0x04000D92 RID: 3474
		private int dot;

		// Token: 0x04000D93 RID: 3475
		private string versionString;

		// Token: 0x0200024B RID: 587
		public class ServerVersionComparer : IEqualityComparer<ServerVersion>
		{
			// Token: 0x060013B4 RID: 5044 RVA: 0x00079306 File Offset: 0x00077506
			public bool Equals(ServerVersion a, ServerVersion b)
			{
				return this.GetHashCode(a) == this.GetHashCode(b);
			}

			// Token: 0x060013B5 RID: 5045 RVA: 0x00079318 File Offset: 0x00077518
			public int GetHashCode(ServerVersion a)
			{
				if (a == null)
				{
					throw new ArgumentNullException("a");
				}
				return (a.major << 26 & -67108864) | (a.minor << 20 & 66060288) | (a.build << 5 & 1048544) | (a.dot & 31);
			}
		}
	}
}
