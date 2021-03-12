using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000067 RID: 103
	internal class BuildVersion : ConfigurablePropertyBag
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000BE63 File Offset: 0x0000A063
		public ulong DALBuildVersion
		{
			get
			{
				return (ulong)this[BuildVersion.DALBuildVersionProp];
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000BE75 File Offset: 0x0000A075
		public ulong DBBuildVersion
		{
			get
			{
				return (ulong)this[BuildVersion.DBBuildVersionProp];
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000BE87 File Offset: 0x0000A087
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000BE90 File Offset: 0x0000A090
		public static string GetBuildVersion(ulong version)
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				(version & 18446462598732840960UL) >> 48,
				(version & 281470681743360UL) >> 32,
				(version & (ulong)-65536) >> 16,
				version & 65535UL
			});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000BF18 File Offset: 0x0000A118
		public static ulong GetBuildVersion(string version)
		{
			string[] array = version.Split(new char[]
			{
				'.'
			});
			if (array != null && array.Length == 4)
			{
				if (!array.Any(delegate(string item)
				{
					ushort num5;
					return !ushort.TryParse(item, out num5);
				}))
				{
					ulong num = (ulong)ushort.Parse(array[0]);
					ulong num2 = (ulong)ushort.Parse(array[1]);
					ulong num3 = (ulong)ushort.Parse(array[2]);
					ulong num4 = (ulong)ushort.Parse(array[3]);
					return num << 48 | num2 << 32 | num3 << 16 | num4;
				}
			}
			throw new ArgumentException("Invalid build version format");
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000BFAF File Offset: 0x0000A1AF
		public static Version GetBuildVersionObject(ulong version)
		{
			return Version.Parse(BuildVersion.GetBuildVersion(version));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public static Version GetBuildVersionObject(int major, int minor, int build, int revision)
		{
			Version result = null;
			if (major != -1 && minor != -1)
			{
				if (build != -1 && revision != -1)
				{
					result = new Version(major, minor, build, revision);
				}
				else if (build != -1)
				{
					result = new Version(major, minor, build);
				}
				else
				{
					result = new Version(major, minor);
				}
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C000 File Offset: 0x0000A200
		public static ulong GetBuildVersion(Version version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			ulong num = (ulong)((ushort)version.Major);
			ulong num2 = (ulong)((ushort)version.Minor);
			ulong num3 = (ulong)((ushort)((version.Build != -1) ? version.Build : 0));
			ulong num4 = (ulong)((ushort)((version.Revision != -1) ? version.Revision : 0));
			return num << 48 | num2 << 32 | num3 << 16 | num4;
		}

		// Token: 0x04000279 RID: 633
		public static readonly HygienePropertyDefinition DALBuildVersionProp = new HygienePropertyDefinition("DALBuildVersion", typeof(ulong), 0UL, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400027A RID: 634
		public static readonly HygienePropertyDefinition DBBuildVersionProp = new HygienePropertyDefinition("DBBuildVersion", typeof(ulong), 0UL, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
