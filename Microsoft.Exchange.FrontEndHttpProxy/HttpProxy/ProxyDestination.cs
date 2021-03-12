using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D8 RID: 216
	internal class ProxyDestination
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		internal ProxyDestination(int version, int portToUse, string[] allDestinations, string[] destinationsInService)
		{
			if (allDestinations == null)
			{
				throw new ArgumentNullException("allDestinations can't be null!");
			}
			if (destinationsInService == null)
			{
				throw new ArgumentNullException("destinationsInServices can't be null!");
			}
			if (allDestinations.Length == 0)
			{
				throw new ArgumentException("allDestinations must have at least one server!");
			}
			this.version = version;
			this.port = portToUse;
			this.serverFqdnList = allDestinations;
			this.inServiceServerFqdnList = destinationsInService;
			this.isFixedDestination = false;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002E308 File Offset: 0x0002C508
		internal ProxyDestination(int version, int portToUse, string fqdn)
		{
			this.version = version;
			this.port = portToUse;
			this.serverFqdnList = new string[]
			{
				fqdn
			};
			this.inServiceServerFqdnList = null;
			this.isFixedDestination = true;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0002E349 File Offset: 0x0002C549
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0002E351 File Offset: 0x0002C551
		internal int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0002E359 File Offset: 0x0002C559
		internal bool IsDynamicTarget
		{
			get
			{
				return !this.isFixedDestination && this.version < Server.E15MinVersion && this.version >= Server.E2007MinVersion;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002E384 File Offset: 0x0002C584
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Is fixed = {0}; Port = {1}; Servers = (", this.isFixedDestination, this.Port);
			for (int i = 0; i < this.serverFqdnList.Length - 1; i++)
			{
				stringBuilder.AppendFormat("{0},", this.serverFqdnList[i]);
			}
			if (this.serverFqdnList.Length > 0)
			{
				stringBuilder.AppendFormat("{0}); Servers in service = (", this.serverFqdnList[this.serverFqdnList.Length - 1]);
			}
			else
			{
				stringBuilder.Append("); Servers in service = (");
			}
			if (this.inServiceServerFqdnList == null || this.inServiceServerFqdnList.Length == 0)
			{
				stringBuilder.Append(")");
			}
			else if (this.inServiceServerFqdnList != null)
			{
				for (int j = 0; j < this.inServiceServerFqdnList.Length - 1; j++)
				{
					stringBuilder.AppendFormat("{0},", this.inServiceServerFqdnList[j]);
				}
				stringBuilder.AppendFormat("{0})", this.inServiceServerFqdnList[this.inServiceServerFqdnList.Length - 1]);
			}
			else
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002E49C File Offset: 0x0002C69C
		internal string GetHostName(int key)
		{
			if (this.isFixedDestination)
			{
				return this.serverFqdnList[0];
			}
			string text = null;
			checked
			{
				if (this.serverFqdnList.Length > 0)
				{
					text = this.serverFqdnList[(int)((IntPtr)(unchecked((ulong)key % (ulong)((long)this.serverFqdnList.Length))))];
					if (!this.inServiceServerFqdnList.Contains(text))
					{
						text = null;
						if (this.inServiceServerFqdnList.Length > 0)
						{
							text = this.inServiceServerFqdnList[(int)((IntPtr)(unchecked((ulong)key % (ulong)((long)this.inServiceServerFqdnList.Length))))];
						}
					}
				}
				return text;
			}
		}

		// Token: 0x040004C4 RID: 1220
		private readonly bool isFixedDestination;

		// Token: 0x040004C5 RID: 1221
		private readonly int port;

		// Token: 0x040004C6 RID: 1222
		private readonly int version;

		// Token: 0x040004C7 RID: 1223
		private string[] serverFqdnList;

		// Token: 0x040004C8 RID: 1224
		private string[] inServiceServerFqdnList;
	}
}
