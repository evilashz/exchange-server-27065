using System;
using System.Globalization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x0200001F RID: 31
	public struct LeaseToken
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000566E File Offset: 0x0000386E
		public LeaseToken(string path, DateTime expiry, LeaseTokenType type, DateTime lastSync, DateTime alertTime, int version)
		{
			this = new LeaseToken(string.Empty, path, expiry, type, lastSync, alertTime, version);
			this.stringForm = this.ComputeString();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005690 File Offset: 0x00003890
		private LeaseToken(string stringForm, string path, DateTime expiry, LeaseTokenType type, DateTime lastSync, DateTime alertTime, int version)
		{
			this.stringForm = stringForm;
			this.path = path;
			this.expiry = expiry;
			this.type = type;
			this.lastSync = lastSync;
			this.alertTime = alertTime;
			this.version = version;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000056C7 File Offset: 0x000038C7
		public string StringForm
		{
			get
			{
				return this.stringForm;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000056CF File Offset: 0x000038CF
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000056D7 File Offset: 0x000038D7
		public DateTime Expiry
		{
			get
			{
				return this.expiry;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000056DF File Offset: 0x000038DF
		public LeaseTokenType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000056E7 File Offset: 0x000038E7
		public DateTime LastSync
		{
			get
			{
				return this.lastSync;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000056EF File Offset: 0x000038EF
		public DateTime AlertTime
		{
			get
			{
				return this.alertTime;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000056F7 File Offset: 0x000038F7
		public bool NotHeld
		{
			get
			{
				return string.IsNullOrEmpty(this.path);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005704 File Offset: 0x00003904
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000570C File Offset: 0x0000390C
		public static LeaseToken Parse(string leaseString)
		{
			string[] array = leaseString.Split(new char[]
			{
				';'
			});
			string text = string.Empty;
			DateTime minValue = DateTime.MinValue;
			DateTime minValue2 = DateTime.MinValue;
			DateTime minValue3 = DateTime.MinValue;
			LeaseTokenType leaseTokenType = LeaseTokenType.Lock;
			int num = 0;
			if (string.IsNullOrEmpty(leaseString))
			{
				return LeaseToken.Empty;
			}
			if (array.Length < 3)
			{
				return LeaseToken.Empty;
			}
			text = array[0];
			if (!DateTime.TryParse(array[1], CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue))
			{
				return LeaseToken.Empty;
			}
			if (!LeaseToken.TryStringToLeaseTokenType(array[2], out leaseTokenType))
			{
				return LeaseToken.Empty;
			}
			if (array.Length == 4 && !int.TryParse(array[3], NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				return LeaseToken.Empty;
			}
			if (array.Length == 5)
			{
				if (!DateTime.TryParse(array[3], CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue2))
				{
					return LeaseToken.Empty;
				}
				if (!DateTime.TryParse(array[4], CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue3))
				{
					return LeaseToken.Empty;
				}
			}
			if (array.Length >= 6)
			{
				if (!DateTime.TryParse(array[3], CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue2))
				{
					return LeaseToken.Empty;
				}
				if (!DateTime.TryParse(array[4], CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue3))
				{
					return LeaseToken.Empty;
				}
				if (!int.TryParse(array[5], NumberStyles.None, CultureInfo.InvariantCulture, out num))
				{
					return LeaseToken.Empty;
				}
			}
			return new LeaseToken(leaseString, text, minValue, leaseTokenType, minValue2, minValue3, num);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005848 File Offset: 0x00003A48
		private static string LeaseTokenTypeToString(LeaseTokenType type)
		{
			switch (type)
			{
			case LeaseTokenType.Lock:
				return "L";
			case LeaseTokenType.Option:
				return "O";
			case LeaseTokenType.None:
				return "N";
			default:
				throw new InvalidOperationException("LeaseTokenTypeToString");
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005887 File Offset: 0x00003A87
		private static bool TryStringToLeaseTokenType(string s, out LeaseTokenType type)
		{
			if (s.StartsWith("L"))
			{
				type = LeaseTokenType.Lock;
				return true;
			}
			if (s.StartsWith("O"))
			{
				type = LeaseTokenType.Option;
				return true;
			}
			if (s.StartsWith("N"))
			{
				type = LeaseTokenType.None;
				return true;
			}
			type = LeaseTokenType.None;
			return false;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000058C4 File Offset: 0x00003AC4
		private string ComputeString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0};{1};{2};{3};{4};{5}", new object[]
			{
				this.path,
				this.expiry.ToString(CultureInfo.InvariantCulture),
				LeaseToken.LeaseTokenTypeToString(this.type),
				this.lastSync.ToString(CultureInfo.InvariantCulture),
				this.alertTime.ToString(CultureInfo.InvariantCulture),
				this.version.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x04000086 RID: 134
		public static readonly LeaseToken Empty = new LeaseToken(string.Empty, DateTime.MinValue, LeaseTokenType.None, DateTime.MinValue, DateTime.MinValue, 0);

		// Token: 0x04000087 RID: 135
		private readonly string stringForm;

		// Token: 0x04000088 RID: 136
		private readonly string path;

		// Token: 0x04000089 RID: 137
		private readonly DateTime expiry;

		// Token: 0x0400008A RID: 138
		private readonly DateTime lastSync;

		// Token: 0x0400008B RID: 139
		private readonly DateTime alertTime;

		// Token: 0x0400008C RID: 140
		private readonly LeaseTokenType type;

		// Token: 0x0400008D RID: 141
		private readonly int version;
	}
}
