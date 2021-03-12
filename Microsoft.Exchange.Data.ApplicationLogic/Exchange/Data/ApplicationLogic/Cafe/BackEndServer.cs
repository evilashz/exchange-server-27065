using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class BackEndServer
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0001DC5F File Offset: 0x0001BE5F
		public BackEndServer(string fqdn, int version)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			if (version == 0)
			{
				throw new ArgumentOutOfRangeException("version");
			}
			this.Fqdn = fqdn;
			this.Version = version;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001DC98 File Offset: 0x0001BE98
		public static BackEndServer FromString(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input");
			}
			string[] array = input.Split(new char[]
			{
				'~'
			});
			int version;
			if (array.Length != 2 || !int.TryParse(array[1], out version))
			{
				throw new ArgumentException("Invalid input value", "input");
			}
			return new BackEndServer(array[0], version);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001DCF7 File Offset: 0x0001BEF7
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0001DCFF File Offset: 0x0001BEFF
		public string Fqdn { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001DD08 File Offset: 0x0001BF08
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0001DD10 File Offset: 0x0001BF10
		public int Version { get; private set; }

		// Token: 0x0600079D RID: 1949 RVA: 0x0001DD19 File Offset: 0x0001BF19
		public override string ToString()
		{
			return string.Format("{0}~{1}", this.Fqdn, this.Version);
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001DD36 File Offset: 0x0001BF36
		public bool IsE15OrHigher
		{
			get
			{
				return this.Version >= Server.E15MinVersion;
			}
		}
	}
}
