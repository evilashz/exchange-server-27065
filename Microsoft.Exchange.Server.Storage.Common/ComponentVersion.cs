using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200001B RID: 27
	public struct ComponentVersion : IComparable<ComponentVersion>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00005679 File Offset: 0x00003879
		public static ComponentVersion Zero
		{
			get
			{
				return ComponentVersion.zero;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00005680 File Offset: 0x00003880
		public static bool TryParse(string value, out ComponentVersion result)
		{
			string[] array = value.Split(new char[]
			{
				'.'
			});
			short major;
			ushort minor;
			if (array.Length == 1)
			{
				int num;
				if (int.TryParse(array[0], out num))
				{
					result = new ComponentVersion(num);
					return true;
				}
			}
			else if (array.Length == 2 && short.TryParse(array[0], out major) && ushort.TryParse(array[1], out minor))
			{
				result = new ComponentVersion(major, minor);
				return true;
			}
			result = ComponentVersion.Zero;
			return false;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000056FD File Offset: 0x000038FD
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00005705 File Offset: 0x00003905
		public short Major
		{
			get
			{
				return (short)(this.value >> 16);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00005711 File Offset: 0x00003911
		public ushort Minor
		{
			get
			{
				return (ushort)(this.value & 65535);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00005720 File Offset: 0x00003920
		public ComponentVersion(int value)
		{
			this = new ComponentVersion((short)(value >> 16), (ushort)(value & 65535));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00005735 File Offset: 0x00003935
		public ComponentVersion(short major, ushort minor)
		{
			this.value = ((int)major << 16 | (int)minor);
			this.versionCache = string.Format("{0}.{1}", major, minor);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000575F File Offset: 0x0000395F
		public bool IsSupported(int minVersion, int maxVersion)
		{
			return minVersion <= this.value && this.value <= maxVersion;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00005778 File Offset: 0x00003978
		public bool IsSupported(ComponentVersion minVersion, ComponentVersion maxVersion)
		{
			return this.IsSupported(minVersion.Value, maxVersion.Value);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00005790 File Offset: 0x00003990
		public int CompareTo(ComponentVersion other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000057B2 File Offset: 0x000039B2
		public override string ToString()
		{
			return this.versionCache;
		}

		// Token: 0x04000304 RID: 772
		private static ComponentVersion zero = new ComponentVersion(0);

		// Token: 0x04000305 RID: 773
		private int value;

		// Token: 0x04000306 RID: 774
		private string versionCache;
	}
}
