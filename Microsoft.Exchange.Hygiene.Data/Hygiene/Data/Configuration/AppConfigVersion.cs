using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Exchange.Hygiene.Data.Configuration
{
	// Token: 0x0200000F RID: 15
	internal struct AppConfigVersion : IEquatable<AppConfigVersion>
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002EB6 File Offset: 0x000010B6
		public AppConfigVersion(long version)
		{
			if (version < 0L)
			{
				throw new ArgumentOutOfRangeException("version");
			}
			this.version = (ulong)version;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002ECF File Offset: 0x000010CF
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002EE5 File Offset: 0x000010E5
		public int Major
		{
			get
			{
				return (int)((this.version & 18446462598732840960UL) >> 48);
			}
			set
			{
				if (value < 0 || value > 32767)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.version = ((281474976710655UL & this.version) | (ulong)((ulong)((long)value) << 48));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F1A File Offset: 0x0000111A
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002F30 File Offset: 0x00001130
		public int Minor
		{
			get
			{
				return (int)((this.version & 281470681743360UL) >> 32);
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.version = ((18446462603027808255UL & this.version) | (ulong)((ulong)((long)value) << 32));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F65 File Offset: 0x00001165
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002F78 File Offset: 0x00001178
		public int Build
		{
			get
			{
				return (int)((this.version & (ulong)-65536) >> 16);
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.version = ((18446744069414649855UL & this.version) | (ulong)((ulong)((long)value) << 16));
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002FAD File Offset: 0x000011AD
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002FBD File Offset: 0x000011BD
		public int Revision
		{
			get
			{
				return (int)(this.version & 65535UL);
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.version = ((18446744073709486080UL & this.version) | (ulong)value);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FEC File Offset: 0x000011EC
		public static AppConfigVersion GetCallingAssemblyVersion()
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(callingAssembly, typeof(AssemblyFileVersionAttribute));
			Version version = (assemblyFileVersionAttribute != null) ? Version.Parse(assemblyFileVersionAttribute.Version) : callingAssembly.GetName().Version;
			return new AppConfigVersion
			{
				Major = version.Major,
				Minor = version.Minor,
				Build = version.Build,
				Revision = version.Revision
			};
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003070 File Offset: 0x00001270
		public static bool TryParse(string version, out AppConfigVersion configVersion)
		{
			if (string.IsNullOrWhiteSpace(version))
			{
				configVersion = default(AppConfigVersion);
				return false;
			}
			long num;
			if (long.TryParse(version, out num) && num >= 0L)
			{
				configVersion = new AppConfigVersion(num);
				return true;
			}
			string[] array = version.Split(new char[]
			{
				'.'
			});
			ushort major;
			ushort minor;
			ushort build;
			ushort revision;
			if (array.Length == 4 && ushort.TryParse(array[0], out major) && ushort.TryParse(array[1], out minor) && ushort.TryParse(array[2], out build) && ushort.TryParse(array[3], out revision))
			{
				configVersion = default(AppConfigVersion);
				configVersion.Major = (int)major;
				configVersion.Minor = (int)minor;
				configVersion.Build = (int)build;
				configVersion.Revision = (int)revision;
				return true;
			}
			configVersion = default(AppConfigVersion);
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000312C File Offset: 0x0000132C
		public override string ToString()
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			string text = this.Major.ToString(invariantCulture);
			string text2 = this.Minor.ToString(invariantCulture);
			string text3 = this.Build.ToString(invariantCulture);
			string text4 = this.Revision.ToString(invariantCulture);
			return string.Concat(new string[]
			{
				text,
				".",
				text2,
				".",
				text3,
				".",
				text4
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031C4 File Offset: 0x000013C4
		public string ToString(bool formatAsBuildVersion)
		{
			if (formatAsBuildVersion)
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				string text = this.Major.ToString("D2", invariantCulture);
				string text2 = this.Minor.ToString("D2", invariantCulture);
				string text3 = this.Build.ToString("D4", invariantCulture);
				string text4 = this.Revision.ToString("D3", invariantCulture);
				return string.Concat(new string[]
				{
					text,
					".",
					text2,
					".",
					text3,
					".",
					text4
				});
			}
			return this.ToString();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003282 File Offset: 0x00001482
		public override bool Equals(object obj)
		{
			return obj is AppConfigVersion && this.version == ((AppConfigVersion)obj).version;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000032A1 File Offset: 0x000014A1
		public bool Equals(AppConfigVersion obj)
		{
			return this.version == obj.version;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000032B2 File Offset: 0x000014B2
		public override int GetHashCode()
		{
			return this.version.GetHashCode();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032BF File Offset: 0x000014BF
		public long ToInt64()
		{
			return (long)this.version;
		}

		// Token: 0x04000017 RID: 23
		private ulong version;
	}
}
