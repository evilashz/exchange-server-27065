using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000007 RID: 7
	internal class DelegatedSecurityToken
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003F74 File Offset: 0x00002174
		public DelegatedSecurityToken(string displayName, string partnerOrgId, string[] groupsId)
		{
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (string.IsNullOrEmpty(partnerOrgId))
			{
				throw new ArgumentNullException("partnerOrgId");
			}
			this.displayName = displayName;
			this.partnerOrgId = partnerOrgId;
			this.groupIds = groupsId;
			this.UTCCreationTime = DateTime.UtcNow;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003FC8 File Offset: 0x000021C8
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003FD0 File Offset: 0x000021D0
		public string[] PartnerGroupIds
		{
			get
			{
				return this.groupIds;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003FD8 File Offset: 0x000021D8
		public string PartnerOrgDirectoryId
		{
			get
			{
				return this.partnerOrgId;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003FE8 File Offset: 0x000021E8
		public DateTime UTCCreationTime
		{
			get
			{
				return this.utcCreationTime;
			}
			private set
			{
				this.utcCreationTime = value;
				this.utcExpirationTime = this.utcCreationTime.Add(DelegatedSecurityToken.TokenLifetime);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004007 File Offset: 0x00002207
		public DateTime UTCExpirationTime
		{
			get
			{
				return this.utcExpirationTime;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000400F File Offset: 0x0000220F
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000402D File Offset: 0x0000222D
		internal static TimeSpan TokenLifetime
		{
			get
			{
				if (DelegatedSecurityToken.tokenLifetime != null)
				{
					return DelegatedSecurityToken.tokenLifetime.Value;
				}
				return DelegatedSecurityToken.DefaultMaximumTokenLifetime;
			}
			set
			{
				if (value < DelegatedSecurityToken.MaximumTokenLifetime)
				{
					DelegatedSecurityToken.tokenLifetime = new TimeSpan?(value);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004048 File Offset: 0x00002248
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.toStringRep))
			{
				string text = this.utcCreationTime.ToString(DateTimeFormatInfo.InvariantInfo);
				string text2 = Uri.EscapeDataString(this.displayName);
				StringBuilder stringBuilder = new StringBuilder(text.Length + this.partnerOrgId.Length + text2.Length + this.groupIds.Length * 32 + this.groupIds.Length + 3);
				stringBuilder.Append(text);
				stringBuilder.Append('&');
				stringBuilder.Append(this.partnerOrgId);
				stringBuilder.Append('&');
				stringBuilder.Append(text2);
				stringBuilder.Append('&');
				int num = 0;
				while (this.groupIds != null && num < this.groupIds.Length)
				{
					stringBuilder.Append(this.groupIds[num]);
					if (num + 1 < this.groupIds.Length)
					{
						stringBuilder.Append(',');
					}
					num++;
				}
				this.toStringRep = stringBuilder.ToString();
			}
			return this.toStringRep;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004148 File Offset: 0x00002348
		public bool IsExpired()
		{
			return DateTime.UtcNow > this.UTCExpirationTime;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000415C File Offset: 0x0000235C
		internal static DelegatedSecurityToken Parse(string securityToken)
		{
			string[] array = securityToken.Split(new char[]
			{
				'&'
			});
			if (array.Length < 4)
			{
				throw new ArgumentException("securityToken");
			}
			DateTime utccreationTime = DateTime.Parse(array[0], DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
			string text = array[1];
			string text2 = Uri.UnescapeDataString(array[2]);
			string[] groupsId = array[3].Split(new char[]
			{
				','
			});
			return new DelegatedSecurityToken(text2, text, groupsId)
			{
				UTCCreationTime = utccreationTime
			};
		}

		// Token: 0x04000024 RID: 36
		internal const char SecurityTokenSeparator = '&';

		// Token: 0x04000025 RID: 37
		internal const char GroupTokenSeparator = ',';

		// Token: 0x04000026 RID: 38
		internal static readonly TimeSpan DefaultMaximumTokenLifetime = new TimeSpan(6, 0, 0);

		// Token: 0x04000027 RID: 39
		internal static readonly TimeSpan MaximumTokenLifetime = new TimeSpan(7, 0, 0, 0);

		// Token: 0x04000028 RID: 40
		private static TimeSpan? tokenLifetime;

		// Token: 0x04000029 RID: 41
		private string displayName;

		// Token: 0x0400002A RID: 42
		private string partnerOrgId;

		// Token: 0x0400002B RID: 43
		private string[] groupIds;

		// Token: 0x0400002C RID: 44
		private DateTime utcCreationTime;

		// Token: 0x0400002D RID: 45
		private DateTime utcExpirationTime;

		// Token: 0x0400002E RID: 46
		private volatile string toStringRep;
	}
}
