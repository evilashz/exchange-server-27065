using System;
using System.Net.NetworkInformation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.XropService;

namespace Microsoft.Mapi
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExRpcConnectionInfo
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000457C File Offset: 0x0000277C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004584 File Offset: 0x00002784
		public ExRpcConnectionCreateFlag CreateFlags { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000458D File Offset: 0x0000278D
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004595 File Offset: 0x00002795
		public ConnectFlag ConnectFlags { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000459E File Offset: 0x0000279E
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000045A6 File Offset: 0x000027A6
		public string ServerDn { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000045AF File Offset: 0x000027AF
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000045B7 File Offset: 0x000027B7
		public Guid MdbGuid { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000045C0 File Offset: 0x000027C0
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000045C8 File Offset: 0x000027C8
		public string UserDn { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000045D1 File Offset: 0x000027D1
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000045D9 File Offset: 0x000027D9
		public string UserName { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000045E2 File Offset: 0x000027E2
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000045EA File Offset: 0x000027EA
		public string Domain { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000045F3 File Offset: 0x000027F3
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000045FB File Offset: 0x000027FB
		public string Password { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004604 File Offset: 0x00002804
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000460C File Offset: 0x0000280C
		public string HttpProxyServerName { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004615 File Offset: 0x00002815
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000461D File Offset: 0x0000281D
		public int ConnectionModulation { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004626 File Offset: 0x00002826
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000462E File Offset: 0x0000282E
		public int LocaleIdForReturnedString { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004637 File Offset: 0x00002837
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000463F File Offset: 0x0000283F
		public int LocaleIdForSort { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004648 File Offset: 0x00002848
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004650 File Offset: 0x00002850
		public int CodePageId { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004659 File Offset: 0x00002859
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004661 File Offset: 0x00002861
		public int ReconnectIntervalInMins { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000466A File Offset: 0x0000286A
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004672 File Offset: 0x00002872
		public int RpcBufferSize { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000467B File Offset: 0x0000287B
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004683 File Offset: 0x00002883
		public int AuxBufferSize { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000468C File Offset: 0x0000288C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004694 File Offset: 0x00002894
		public byte[] ClientSessionInfo { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000469D File Offset: 0x0000289D
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000046A5 File Offset: 0x000028A5
		public MapiApplicationId ApplicationId { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000046AE File Offset: 0x000028AE
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000046B6 File Offset: 0x000028B6
		public TimeSpan ConnectionTimeout { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000046BF File Offset: 0x000028BF
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000046C7 File Offset: 0x000028C7
		public TimeSpan CallTimeout { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000046D0 File Offset: 0x000028D0
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000046D8 File Offset: 0x000028D8
		public Client XropClient { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000046E1 File Offset: 0x000028E1
		public bool IsCrossServer
		{
			get
			{
				if (this.isCrossServer == null)
				{
					this.isCrossServer = new bool?(this.IsCrossServerCall());
				}
				return this.isCrossServer.Value;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000470C File Offset: 0x0000290C
		public ExRpcConnectionInfo(ExRpcConnectionCreateFlag createFlags, ConnectFlag connectFlags, string serverDn, Guid mdbGuid, string userDn, string userName, string domain, string password, string httpProxyServerName, int connectionModulation, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, Client xropClient, byte[] clientSessionInfo, string applicationId, TimeSpan connectionTimeout, TimeSpan callTimeout)
		{
			this.CreateFlags = createFlags;
			this.ConnectFlags = connectFlags;
			this.ServerDn = serverDn;
			this.MdbGuid = mdbGuid;
			this.UserDn = userDn;
			this.UserName = userName;
			this.Domain = domain;
			this.Password = password;
			this.HttpProxyServerName = httpProxyServerName;
			this.ConnectionModulation = connectionModulation;
			this.LocaleIdForReturnedString = lcidString;
			this.LocaleIdForSort = lcidSort;
			this.CodePageId = cpid;
			this.ReconnectIntervalInMins = cReconnectIntervalInMins;
			this.RpcBufferSize = cbRpcBufferSize;
			this.AuxBufferSize = cbRpcBufferSize;
			this.XropClient = xropClient;
			this.ClientSessionInfo = clientSessionInfo;
			this.ApplicationId = MapiApplicationId.FromClientInfoString(applicationId);
			this.ConnectionTimeout = connectionTimeout;
			this.CallTimeout = callTimeout;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000047D8 File Offset: 0x000029D8
		public override string ToString()
		{
			return string.Format("serverDn={0}, domain={1}, userDn={2}, userName={3}, mdbGuid={4}, applicationId={5}", new object[]
			{
				this.ServerDn,
				this.Domain,
				this.UserDn,
				this.UserName,
				this.MdbGuid,
				this.ApplicationId.ClientInfo
			});
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004838 File Offset: 0x00002A38
		internal string GetDestinationServerName()
		{
			if (!string.IsNullOrEmpty(this.destinationServerName))
			{
				return this.destinationServerName;
			}
			if (string.IsNullOrEmpty(this.ServerDn))
			{
				throw new InvalidOperationException("serverDn cannot be null/empty");
			}
			int num = this.ServerDn.IndexOf("/cn=Configuration/cn=Servers/cn=", StringComparison.OrdinalIgnoreCase);
			if (num >= 0)
			{
				this.destinationServerName = this.ServerDn.Substring(num + "/cn=Configuration/cn=Servers/cn=".Length);
			}
			else
			{
				this.destinationServerName = this.ServerDn;
			}
			return this.destinationServerName;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000048B8 File Offset: 0x00002AB8
		internal static string GetLocalServerFQDN()
		{
			if (string.IsNullOrEmpty(ExRpcConnectionInfo.localServerFQDN))
			{
				string text = MapiStore.GetLocalServerFqdn();
				if (string.IsNullOrEmpty(text))
				{
					text = Environment.MachineName;
					string localServerDomainName = ExRpcConnectionInfo.LocalServerDomainName;
					if (!text.Contains(localServerDomainName))
					{
						text = text + "." + localServerDomainName;
					}
				}
				ExRpcConnectionInfo.localServerFQDN = text;
			}
			return ExRpcConnectionInfo.localServerFQDN;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000490C File Offset: 0x00002B0C
		private bool IsCrossServerCall()
		{
			if ((this.ConnectFlags & ConnectFlag.LocalRpcOnly) == ConnectFlag.LocalRpcOnly || this.XropClient != null)
			{
				return false;
			}
			string text = this.GetDestinationServerName();
			string value = ExRpcConnectionInfo.GetLocalServerFQDN();
			string machineName = Environment.MachineName;
			return !text.Equals("localhost", StringComparison.OrdinalIgnoreCase) && !text.Equals(value, StringComparison.OrdinalIgnoreCase) && !text.Equals(machineName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040000AD RID: 173
		private bool? isCrossServer = null;

		// Token: 0x040000AE RID: 174
		private string destinationServerName;

		// Token: 0x040000AF RID: 175
		private static string localServerFQDN;

		// Token: 0x040000B0 RID: 176
		private static readonly string LocalServerDomainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
	}
}
