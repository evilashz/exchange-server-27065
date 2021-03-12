using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B0 RID: 1712
	public class CryptoAlgorithm
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x0003D47D File Offset: 0x0003B67D
		public CryptoAlgorithm(int id, string name)
		{
			this.Id = id;
			this.Name = name;
			this.settings = new Dictionary<string, string>();
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0003D49E File Offset: 0x0003B69E
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x0003D4A5 File Offset: 0x0003B6A5
		public static string PreferredKeyedHashAlgorithm { get; set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0003D4AD File Offset: 0x0003B6AD
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0003D4B4 File Offset: 0x0003B6B4
		public static string PreferredHashAlgorithm { get; set; }

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0003D4BC File Offset: 0x0003B6BC
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x0003D4C3 File Offset: 0x0003B6C3
		public static string PreferredSymmetricAlgorithm { get; set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0003D4CB File Offset: 0x0003B6CB
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x0003D4D3 File Offset: 0x0003B6D3
		public int Id { get; private set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		public string Name { get; private set; }

		// Token: 0x06001FDB RID: 8155 RVA: 0x0003D4ED File Offset: 0x0003B6ED
		public void AddOrUpdateAlgorithmSetting(string settingName, string settingValue)
		{
			this.settings[settingName] = settingValue;
		}

		// Token: 0x04001ED5 RID: 7893
		private Dictionary<string, string> settings;
	}
}
