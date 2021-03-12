using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	public class NetID : IEquatable<NetID>
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x00026416 File Offset: 0x00024616
		public NetID(long netID)
		{
			this.netID = netID;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00026425 File Offset: 0x00024625
		public NetID(byte[] netID) : this(NetID.BytesToString(netID))
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00026434 File Offset: 0x00024634
		public NetID(string netID)
		{
			if (string.IsNullOrEmpty(netID))
			{
				throw new ArgumentNullException("netID");
			}
			if (netID.Length != 16 || netID.Trim().Length != 16)
			{
				throw new FormatException(DataStrings.ErrorIncorrectLiveIdFormat(netID));
			}
			try
			{
				this.netID = long.Parse(netID, NumberStyles.HexNumber);
			}
			catch (FormatException)
			{
				throw new FormatException(DataStrings.ErrorIncorrectLiveIdFormat(netID));
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000264BC File Offset: 0x000246BC
		public static NetID Parse(string netID)
		{
			return new NetID(netID);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000264C4 File Offset: 0x000246C4
		public static bool TryParse(string netID, out NetID outNetID)
		{
			outNetID = null;
			long num;
			if (netID != null && netID.Length == 16 && netID.Trim().Length == 16 && long.TryParse(netID, NumberStyles.HexNumber, null, out num))
			{
				outNetID = new NetID(num);
				return true;
			}
			return false;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002650B File Offset: 0x0002470B
		public bool Equals(NetID other)
		{
			return other != null && this.netID == other.netID;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00026520 File Offset: 0x00024720
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NetID);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002652E File Offset: 0x0002472E
		public static bool operator ==(NetID left, NetID right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002653F File Offset: 0x0002473F
		public static bool operator !=(NetID left, NetID right)
		{
			return !(left == right);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002654B File Offset: 0x0002474B
		public override int GetHashCode()
		{
			return this.netID.GetHashCode();
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00026558 File Offset: 0x00024758
		public override string ToString()
		{
			return string.Format("{0:X16}", this.netID);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00026570 File Offset: 0x00024770
		internal byte[] ToByteArray()
		{
			byte[] bytes = BitConverter.GetBytes(this.netID);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00026597 File Offset: 0x00024797
		internal ulong ToUInt64()
		{
			return (ulong)this.netID;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000265A0 File Offset: 0x000247A0
		private static string BytesToString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.Append(string.Format("{0:X2}", b));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400076D RID: 1901
		internal const string PrefixMSWLID = "MS-WLID:";

		// Token: 0x0400076E RID: 1902
		internal const string Prefix = "KERBEROS:";

		// Token: 0x0400076F RID: 1903
		internal const string PrefixExWLID = "ExWLID:";

		// Token: 0x04000770 RID: 1904
		internal const string ExWLIDFormat = "ExWLID:{0}-{1}";

		// Token: 0x04000771 RID: 1905
		internal const string Suffix = "@live.com";

		// Token: 0x04000772 RID: 1906
		internal const string PrefixConsumerWLID = "CS-WLID:";

		// Token: 0x04000773 RID: 1907
		internal const string PrefixOriginalNetID = "EXORIGNETID:";

		// Token: 0x04000774 RID: 1908
		private long netID;
	}
}
