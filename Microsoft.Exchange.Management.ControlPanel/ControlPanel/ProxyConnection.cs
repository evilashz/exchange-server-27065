using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Monitoring;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200039B RID: 923
	internal class ProxyConnection : ExchangeControlPanelApplication
	{
		// Token: 0x060030FF RID: 12543 RVA: 0x000959B0 File Offset: 0x00093BB0
		public ProxyConnection(Uri serviceUrl) : this(serviceUrl.AbsolutePath, new ProxyWebSession(serviceUrl)
		{
			TrustAnySSLCertificate = Registry.AllowInternalUntrustedCerts
		})
		{
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000959DC File Offset: 0x00093BDC
		private ProxyConnection(string virtualDirectory, ProxyWebSession proxyWebSession) : base(virtualDirectory, proxyWebSession)
		{
			this.ProxyWebSession = proxyWebSession;
			proxyWebSession.RequestException += this.ProxyWebSession_RequestException;
			Action<ProxyConnection> newProxyConnection = ProxyConnection.NewProxyConnection;
			if (newProxyConnection != null)
			{
				newProxyConnection(this);
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x00095A23 File Offset: 0x00093C23
		// (set) Token: 0x06003102 RID: 12546 RVA: 0x00095A2B File Offset: 0x00093C2B
		public ProxyWebSession ProxyWebSession { get; private set; }

		// Token: 0x06003103 RID: 12547 RVA: 0x00095A34 File Offset: 0x00093C34
		private void ProxyWebSession_RequestException(object sender, WebExceptionEventArgs e)
		{
			if (e.Exception.GetTroubleshootingID() == WebExceptionTroubleshootingID.ServiceUnavailable)
			{
				this.isAlive = false;
			}
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x00095AC8 File Offset: 0x00093CC8
		public void Ping(Action<ProxyConnection> onStatusAvailable)
		{
			base.Ping(delegate(ExchangeControlPanelApplication.PingResponse pingResponse)
			{
				this.isAlive = pingResponse.IsAlive;
				this.isCompatible = this.CheckVersionCompatibility(pingResponse.ApplicationVersion);
				onStatusAvailable(this);
			}, delegate(Exception exception)
			{
				this.isAlive = false;
				onStatusAvailable(this);
			});
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x00095B07 File Offset: 0x00093D07
		public bool IsAlive
		{
			get
			{
				return this.isAlive;
			}
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x00095B11 File Offset: 0x00093D11
		public bool IsCompatible
		{
			get
			{
				return this.isCompatible;
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x00095B1C File Offset: 0x00093D1C
		private bool CheckVersionCompatibility(Version internalApplicationVersion)
		{
			bool result;
			using (StringWriter stringWriter = new StringWriter())
			{
				bool flag = ProxyConnection.ValidProxyVersions.IsCompatible(internalApplicationVersion, stringWriter);
				if (!flag)
				{
					string periodicKey = base.BaseUri.Host + internalApplicationVersion.ToString();
					EcpEventLogConstants.Tuple_ProxyErrorCASCompatibility.LogPeriodicEvent(periodicKey, new object[]
					{
						EcpEventLogExtensions.GetUserNameToLog(),
						Environment.MachineName,
						ThemeResource.ApplicationVersion,
						base.BaseUri.Host,
						internalApplicationVersion,
						stringWriter.GetStringBuilder()
					});
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x00095BC4 File Offset: 0x00093DC4
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x00095BE1 File Offset: 0x00093DE1
		internal static ProxyVersions ValidProxyVersions
		{
			get
			{
				if (ProxyConnection.validProxyVersions == null)
				{
					ProxyConnection.validProxyVersions = new ProxyVersions(HttpRuntime.AppDomainAppPath);
				}
				return ProxyConnection.validProxyVersions;
			}
			set
			{
				ProxyConnection.validProxyVersions = value;
			}
		}

		// Token: 0x040023B5 RID: 9141
		private volatile bool isAlive;

		// Token: 0x040023B6 RID: 9142
		private volatile bool isCompatible = true;

		// Token: 0x040023B7 RID: 9143
		internal static Action<ProxyConnection> NewProxyConnection;

		// Token: 0x040023B8 RID: 9144
		private static ProxyVersions validProxyVersions;
	}
}
