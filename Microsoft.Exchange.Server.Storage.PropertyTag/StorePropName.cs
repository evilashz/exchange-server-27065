using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000009 RID: 9
	public class StorePropName : IComparable<StorePropName>, IEquatable<StorePropName>
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003210 File Offset: 0x00001410
		public StorePropName(Guid guid, string name)
		{
			this.guid = guid;
			this.name = name;
			this.dispId = uint.MaxValue;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000322D File Offset: 0x0000142D
		public StorePropName(Guid guid, uint dispId)
		{
			this.guid = guid;
			this.name = null;
			this.dispId = dispId;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000324A File Offset: 0x0000144A
		public static StorePropName Invalid
		{
			get
			{
				return StorePropName.invalid;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003251 File Offset: 0x00001451
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003259 File Offset: 0x00001459
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003261 File Offset: 0x00001461
		public uint DispId
		{
			get
			{
				return this.dispId;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003269 File Offset: 0x00001469
		public static bool operator ==(StorePropName name1, StorePropName name2)
		{
			return name1.Equals(name2);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003272 File Offset: 0x00001472
		public static bool operator !=(StorePropName name1, StorePropName name2)
		{
			return !name1.Equals(name2);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003280 File Offset: 0x00001480
		public static bool IsValidName(Guid guid, string name)
		{
			if (guid.Equals(StorePropName.InternetHeadersNamespaceGuid))
			{
				if (name == null)
				{
					return false;
				}
				foreach (char c in name)
				{
					if (c < '!' || c > '~' || c == ':')
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000032D4 File Offset: 0x000014D4
		public bool Equals(StorePropName other)
		{
			return this.guid == other.guid && (object.ReferenceEquals(this.name, other.name) || string.Equals(this.name, other.name)) && this.dispId == other.dispId;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000332A File Offset: 0x0000152A
		public override bool Equals(object other)
		{
			return other is StorePropName && this.Equals((StorePropName)other);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003344 File Offset: 0x00001544
		public int CompareTo(StorePropName other)
		{
			int num = this.guid.CompareTo(other.guid);
			if (num != 0)
			{
				return num;
			}
			if (!object.ReferenceEquals(this.name, other.name))
			{
				if (this.name == null)
				{
					return -1;
				}
				return this.name.CompareTo(other.name);
			}
			else
			{
				if (this.dispId > other.dispId)
				{
					return 1;
				}
				if (this.dispId < other.dispId)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000033BC File Offset: 0x000015BC
		public override int GetHashCode()
		{
			return this.guid.GetHashCode() + (int)this.dispId + ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000033FC File Offset: 0x000015FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003420 File Offset: 0x00001620
		public void AppendToString(StringBuilder sb)
		{
			sb.Append(this.guid.ToString("D"));
			sb.Append(":");
			if (this.name != null)
			{
				sb.Append(this.name);
				return;
			}
			sb.Append("N:0x");
			sb.Append(this.dispId.ToString("X8"));
		}

		// Token: 0x0400005C RID: 92
		public static readonly Guid UnnamedPropertyNamespaceGuid = new Guid("00020328-0000-0000-C000-000000000046");

		// Token: 0x0400005D RID: 93
		public static readonly Guid InternetHeadersNamespaceGuid = new Guid("00020386-0000-0000-C000-000000000046");

		// Token: 0x0400005E RID: 94
		private static readonly StorePropName invalid = new StorePropName(Guid.Empty, null);

		// Token: 0x0400005F RID: 95
		private readonly Guid guid;

		// Token: 0x04000060 RID: 96
		private readonly string name;

		// Token: 0x04000061 RID: 97
		private readonly uint dispId;
	}
}
