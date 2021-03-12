using System;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class ConnectivityRec : XMLSerializableBase
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x00010F40 File Offset: 0x0000F140
		public ConnectivityRec()
		{
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00010F48 File Offset: 0x0000F148
		internal ConnectivityRec(ServerKind serverKind, MailboxInformation mailboxInfo, MailboxServerInformation serverInfo)
		{
			this.ServerKind = serverKind;
			this.ServerName = serverInfo.MailboxServerName;
			this.ServerVersion = new ServerVersion(serverInfo.MailboxServerVersion);
			if (serverInfo.ProxyServerName != null)
			{
				this.ProxyName = serverInfo.ProxyServerName;
				this.ProxyVersion = serverInfo.ProxyServerVersion.ServerVersion;
			}
			if (mailboxInfo != null)
			{
				this.ProviderName = mailboxInfo.ProviderName;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00010FB3 File Offset: 0x0000F1B3
		internal ConnectivityRec(ServerKind serverKind, VersionInformation versionInfo)
		{
			this.ServerKind = serverKind;
			this.ServerName = versionInfo.ComputerName;
			this.ServerVersion = versionInfo.ServerVersion;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00010FDA File Offset: 0x0000F1DA
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00010FE2 File Offset: 0x0000F1E2
		[XmlIgnore]
		public ServerKind ServerKind { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00010FEB File Offset: 0x0000F1EB
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00010FF3 File Offset: 0x0000F1F3
		[XmlElement(ElementName = "ServerKindInt")]
		public int ServerKindInt
		{
			get
			{
				return (int)this.ServerKind;
			}
			set
			{
				this.ServerKind = (ServerKind)value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00010FFC File Offset: 0x0000F1FC
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00011004 File Offset: 0x0000F204
		[XmlElement(ElementName = "ServerName")]
		public string ServerName { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001100D File Offset: 0x0000F20D
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00011015 File Offset: 0x0000F215
		[XmlIgnore]
		public ServerVersion ServerVersion { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001101E File Offset: 0x0000F21E
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00011040 File Offset: 0x0000F240
		[XmlElement(ElementName = "ServerVersionStr")]
		public string ServerVersionStr
		{
			get
			{
				if (!(this.ServerVersion != null))
				{
					return null;
				}
				return this.ServerVersion.ToString();
			}
			set
			{
				this.ServerVersion = ConnectivityRec.ParseServerVersion(value);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001104E File Offset: 0x0000F24E
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00011056 File Offset: 0x0000F256
		[XmlElement(ElementName = "ProxyName")]
		public string ProxyName { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001105F File Offset: 0x0000F25F
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x00011067 File Offset: 0x0000F267
		[XmlElement(ElementName = "ProviderName")]
		public string ProviderName { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00011070 File Offset: 0x0000F270
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00011078 File Offset: 0x0000F278
		[XmlIgnore]
		public ServerVersion ProxyVersion { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00011081 File Offset: 0x0000F281
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x000110A3 File Offset: 0x0000F2A3
		[XmlElement(ElementName = "ProxyVersionStr")]
		public string ProxyVersionStr
		{
			get
			{
				if (!(this.ProxyVersion != null))
				{
					return null;
				}
				return this.ProxyVersion.ToString();
			}
			set
			{
				this.ProxyVersion = ConnectivityRec.ParseServerVersion(value);
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000110B4 File Offset: 0x0000F2B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			switch (this.ServerKind)
			{
			case ServerKind.MRS:
				stringBuilder.Append("MRS ");
				break;
			case ServerKind.Source:
				stringBuilder.Append("S   ");
				break;
			case ServerKind.Target:
				stringBuilder.Append("T   ");
				break;
			case ServerKind.SourceArchive:
				stringBuilder.Append("SA  ");
				break;
			case ServerKind.TargetArchive:
				stringBuilder.Append("TA  ");
				break;
			case ServerKind.Cmdlet:
				stringBuilder.Append("CMD ");
				break;
			}
			stringBuilder.AppendFormat("{0} ({1})", this.ServerName, this.ServerVersion.ToString());
			if (!string.IsNullOrEmpty(this.ProxyName))
			{
				stringBuilder.AppendFormat("; P {0} ({1})", this.ProxyName, this.ProxyVersion.ToString());
			}
			if (!string.IsNullOrEmpty(this.ProviderName))
			{
				stringBuilder.AppendFormat("; {0}", this.ProviderName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000111AC File Offset: 0x0000F3AC
		private static ServerVersion ParseServerVersion(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			ServerVersion result;
			try
			{
				Version version = new Version(value);
				result = version;
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
