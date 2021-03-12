using System;
using System.Net;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000929 RID: 2345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcBindingInfo : ICloneable
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x0007B4A8 File Offset: 0x000796A8
		public RpcBindingInfo()
		{
			this.RpcAuthentication = AuthenticationService.Negotiate;
			this.Timeout = TimeSpan.FromMinutes(5.0);
			this.WebProxyServer = null;
			this.RpcProxyPort = RpcProxyPort.Default;
			this.UseSsl = true;
			this.RpcHttpCookies = new CookieCollection();
			this.RpcHttpHeaders = new WebHeaderCollection();
			this.AllowImpersonation = false;
			this.AllowRpcRetry = true;
			this.IgnoreInvalidRpcProxyServerCertificateSubject = false;
			this.UseUniqueBinding = true;
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x0007B522 File Offset: 0x00079722
		// (set) Token: 0x0600320D RID: 12813 RVA: 0x0007B52A File Offset: 0x0007972A
		public bool AllowRpcRetry { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x0007B533 File Offset: 0x00079733
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x0007B53B File Offset: 0x0007973B
		public string ClientCertificateSubjectName { get; set; }

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x0007B544 File Offset: 0x00079744
		// (set) Token: 0x06003211 RID: 12817 RVA: 0x0007B54C File Offset: 0x0007974C
		public Guid ClientObjectGuid { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x0007B555 File Offset: 0x00079755
		// (set) Token: 0x06003213 RID: 12819 RVA: 0x0007B55D File Offset: 0x0007975D
		public NetworkCredential Credential { get; set; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x0007B566 File Offset: 0x00079766
		// (set) Token: 0x06003215 RID: 12821 RVA: 0x0007B56E File Offset: 0x0007976E
		public string ProtocolSequence
		{
			get
			{
				return this.protocolSequence;
			}
			set
			{
				this.protocolSequence = ((value != null) ? string.Intern(value.ToLowerInvariant()) : null);
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x0007B587 File Offset: 0x00079787
		// (set) Token: 0x06003217 RID: 12823 RVA: 0x0007B58F File Offset: 0x0007978F
		public string RpcServer { get; set; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x0007B598 File Offset: 0x00079798
		// (set) Token: 0x06003219 RID: 12825 RVA: 0x0007B5A0 File Offset: 0x000797A0
		public int? RpcPort { get; set; }

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x0007B5A9 File Offset: 0x000797A9
		// (set) Token: 0x0600321B RID: 12827 RVA: 0x0007B5B1 File Offset: 0x000797B1
		public string ServicePrincipalName { get; set; }

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x0007B5BA File Offset: 0x000797BA
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x0007B5C2 File Offset: 0x000797C2
		public AuthenticationService RpcAuthentication { get; set; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x0007B5CC File Offset: 0x000797CC
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x0007B5FD File Offset: 0x000797FD
		public bool UseRpcEncryption
		{
			get
			{
				bool? flag = this.useRpcEncryption;
				if (flag == null)
				{
					return this.RpcAuthentication != AuthenticationService.None;
				}
				return flag.GetValueOrDefault();
			}
			set
			{
				this.useRpcEncryption = new bool?(value);
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x0007B60B File Offset: 0x0007980B
		// (set) Token: 0x06003221 RID: 12833 RVA: 0x0007B613 File Offset: 0x00079813
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0007B61C File Offset: 0x0007981C
		public Uri Uri
		{
			get
			{
				RpcBindingInfo rpcBindingInfo = this.Clone();
				rpcBindingInfo.DefaultOmittedProperties();
				return rpcBindingInfo.CreateUri();
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x0007B640 File Offset: 0x00079840
		public bool IsRpcServerLocalMachine
		{
			get
			{
				return string.Equals(this.RpcServer, "localhost", StringComparison.OrdinalIgnoreCase) || string.Equals(this.RpcServer, Environment.MachineName, StringComparison.OrdinalIgnoreCase) || string.Equals(this.RpcServer, ComputerInformation.DnsHostName, StringComparison.OrdinalIgnoreCase) || string.Equals(this.RpcServer, ComputerInformation.DnsFullyQualifiedDomainName, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x0007B699 File Offset: 0x00079899
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x0007B6A4 File Offset: 0x000798A4
		public string RpcProxyServer
		{
			get
			{
				return this.rpcProxyServer;
			}
			set
			{
				string text = value;
				if (!string.IsNullOrEmpty(value))
				{
					int num = value.IndexOf(':');
					if (num >= 0)
					{
						text = value.Substring(0, num);
						this.RpcProxyPort = (RpcProxyPort)int.Parse(value.Substring(num + 1));
					}
				}
				this.rpcProxyServer = text;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x0007B6EC File Offset: 0x000798EC
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x0007B6F4 File Offset: 0x000798F4
		public string WebProxyServer { get; set; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x0007B6FD File Offset: 0x000798FD
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x0007B705 File Offset: 0x00079905
		public RpcProxyPort RpcProxyPort { get; set; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x0007B70E File Offset: 0x0007990E
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x0007B716 File Offset: 0x00079916
		public HttpAuthenticationScheme RpcProxyAuthentication { get; set; }

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x0007B71F File Offset: 0x0007991F
		// (set) Token: 0x0600322D RID: 12845 RVA: 0x0007B727 File Offset: 0x00079927
		public bool UseSsl { get; set; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x0007B730 File Offset: 0x00079930
		// (set) Token: 0x0600322F RID: 12847 RVA: 0x0007B738 File Offset: 0x00079938
		public WebHeaderCollection RpcHttpHeaders { get; private set; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x0007B741 File Offset: 0x00079941
		// (set) Token: 0x06003231 RID: 12849 RVA: 0x0007B749 File Offset: 0x00079949
		public CookieCollection RpcHttpCookies { get; private set; }

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x0007B752 File Offset: 0x00079952
		// (set) Token: 0x06003233 RID: 12851 RVA: 0x0007B75A File Offset: 0x0007995A
		public bool AllowImpersonation { get; set; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x0007B763 File Offset: 0x00079963
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x0007B76B File Offset: 0x0007996B
		public bool UseUniqueBinding { get; set; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x0007B774 File Offset: 0x00079974
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x0007B77C File Offset: 0x0007997C
		public bool UseExplicitEndpointLookup { get; set; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x0007B785 File Offset: 0x00079985
		// (set) Token: 0x06003239 RID: 12857 RVA: 0x0007B78D File Offset: 0x0007998D
		public bool IgnoreInvalidRpcProxyServerCertificateSubject { get; set; }

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x0007B796 File Offset: 0x00079996
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x0007B79E File Offset: 0x0007999E
		public string ExpectedRpcProxyServerCertificateSubject { get; set; }

		// Token: 0x0600323C RID: 12860 RVA: 0x0007B7A7 File Offset: 0x000799A7
		public RpcBindingInfo Clone()
		{
			return (RpcBindingInfo)base.MemberwiseClone();
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0007B7B4 File Offset: 0x000799B4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x0007B7BC File Offset: 0x000799BC
		public bool PackHeadersAndCookiesIntoRpcCookie(out string cookieName, out string cookieValue)
		{
			if (this.RpcHttpCookies.Count == 0 && this.RpcHttpHeaders.Count == 0)
			{
				cookieName = null;
				cookieValue = null;
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			cookieName = string.Empty;
			for (int i = 0; i < this.RpcHttpCookies.Count; i++)
			{
				if (i == 0)
				{
					cookieName = this.RpcHttpCookies[0].Name;
					stringBuilder.Append(this.RpcHttpCookies[0].Value);
				}
				else
				{
					stringBuilder.AppendFormat("; {0}={1}", this.RpcHttpCookies[i].Name, this.RpcHttpCookies[i].Value);
				}
			}
			if (this.RpcHttpHeaders.Count > 0)
			{
				stringBuilder.AppendLine();
				string text = this.RpcHttpHeaders.ToString();
				if (text.EndsWith("\r\n\r\n"))
				{
					text = text.Substring(0, text.Length - "\r\n\r\n".Length);
				}
				stringBuilder.Append(text);
			}
			cookieValue = stringBuilder.ToString();
			return true;
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x0007B8C4 File Offset: 0x00079AC4
		public RpcBindingInfo DefaultOmittedProperties()
		{
			if (string.IsNullOrEmpty(this.RpcServer))
			{
				this.RpcServer = ComputerInformation.DnsFullyQualifiedDomainName;
			}
			this.DefaultProtocolSequence();
			this.DefaultRpcHttpSettings();
			this.ForceLRpcSettings();
			this.UseExplicitEndpointLookup &= (this.ClientObjectGuid != Guid.Empty);
			return this;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0007B919 File Offset: 0x00079B19
		public static string BuildKerberosSpn(string serviceClass, string hostName)
		{
			return string.Format("{0}/{1}", serviceClass ?? "host", hostName ?? ComputerInformation.DnsFullyQualifiedDomainName);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x0007B93C File Offset: 0x00079B3C
		public RpcBindingInfo UseProtocolSequenceWithOptionalRpcPortSpecification(string protocolSequence)
		{
			int num;
			if (protocolSequence != null && (num = protocolSequence.IndexOf(':')) != -1)
			{
				this.ProtocolSequence = protocolSequence.Substring(0, num);
				this.RpcPort = new int?(int.Parse(protocolSequence.Substring(num + 1)));
			}
			else
			{
				this.ProtocolSequence = protocolSequence;
			}
			return this;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0007B98A File Offset: 0x00079B8A
		public RpcBindingInfo UseTcp()
		{
			this.ProtocolSequence = "ncacn_ip_tcp";
			return this;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x0007B998 File Offset: 0x00079B98
		public RpcBindingInfo UseRpcProxy(int rpcPort, string rpcProxyServer, RpcProxyPort rpcProxyPort)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("rpcProxyServer", rpcProxyServer);
			this.ProtocolSequence = "ncacn_http";
			this.RpcPort = new int?(rpcPort);
			this.RpcProxyServer = rpcProxyServer;
			this.RpcProxyPort = rpcProxyPort;
			return this;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x0007B9CB File Offset: 0x00079BCB
		public RpcBindingInfo UseKerberosSpn(string serviceClass, string hostName)
		{
			if (string.IsNullOrEmpty(hostName) && !string.IsNullOrEmpty(this.RpcServer) && this.IsRealServerName(this.RpcServer))
			{
				hostName = this.RpcServer;
			}
			this.ServicePrincipalName = RpcBindingInfo.BuildKerberosSpn(serviceClass, hostName);
			return this;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x0007BA08 File Offset: 0x00079C08
		private Uri CreateUri()
		{
			UriBuilder uriBuilder = new UriBuilder();
			uriBuilder.Scheme = this.ProtocolSequence.Substring(this.ProtocolSequence.LastIndexOf('_') + 1);
			uriBuilder.Host = this.RpcServer;
			if (this.RpcPort != null)
			{
				uriBuilder.Port = this.RpcPort.Value;
			}
			string a;
			if ((a = this.ProtocolSequence) != null)
			{
				if (!(a == "ncacn_http"))
				{
					if (a == "ncacn_ip_tcp")
					{
						uriBuilder.Scheme = Uri.UriSchemeNetTcp;
					}
				}
				else
				{
					uriBuilder.Scheme = (this.UseSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
					uriBuilder.Host = this.RpcProxyServer;
					uriBuilder.Port = (int)this.RpcProxyPort;
					uriBuilder.Path = "rpc/rpcproxy.dll";
					uriBuilder.Query = ((this.RpcPort != null) ? string.Format("{0}:{1}", this.RpcServer, this.RpcPort) : this.RpcServer);
				}
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x0007BB21 File Offset: 0x00079D21
		private void DefaultProtocolSequence()
		{
			if (string.IsNullOrEmpty(this.ProtocolSequence))
			{
				if (this.Credential != null)
				{
					this.UseTcp();
					return;
				}
				if (this.IsRpcServerLocalMachine)
				{
					this.UseProtocolSequenceWithOptionalRpcPortSpecification("ncalrpc");
					return;
				}
				this.UseTcp();
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x0007BB5C File Offset: 0x00079D5C
		private void DefaultRpcHttpSettings()
		{
			if (this.ProtocolSequence == "ncacn_http")
			{
				if (string.IsNullOrEmpty(this.RpcProxyServer))
				{
					this.RpcProxyServer = this.RpcServer;
					if (this.IsRpcServerLocalMachine)
					{
						this.RpcServer = Environment.MachineName;
					}
				}
				if (this.RpcPort == null)
				{
					this.RpcPort = new int?(6001);
				}
				if (this.RpcProxyAuthentication == HttpAuthenticationScheme.Undefined)
				{
					this.RpcProxyAuthentication = ((!this.UseSsl || this.RpcProxyPort == RpcProxyPort.Backend) ? HttpAuthenticationScheme.Negotiate : HttpAuthenticationScheme.Basic);
				}
			}
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x0007BBEF File Offset: 0x00079DEF
		private void ForceLRpcSettings()
		{
			if (this.ProtocolSequence == "ncalrpc")
			{
				this.RpcServer = Environment.MachineName;
				this.RpcAuthentication = AuthenticationService.Ntlm;
			}
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x0007BC16 File Offset: 0x00079E16
		private bool IsRealServerName(string serverName)
		{
			return !serverName.Contains("@");
		}

		// Token: 0x04002BBB RID: 11195
		public const int OutlookConsolidatedEndpoint = 6001;

		// Token: 0x04002BBC RID: 11196
		public const string OutlookSessionCookieName = "OutlookSession";

		// Token: 0x04002BBD RID: 11197
		public const string RpcProxyPath = "rpc/rpcproxy.dll";

		// Token: 0x04002BBE RID: 11198
		public const string LocalHost = "localhost";

		// Token: 0x04002BBF RID: 11199
		public const string WebProxyNone = "<none>";

		// Token: 0x04002BC0 RID: 11200
		public const string WebProxyAuto = null;

		// Token: 0x04002BC1 RID: 11201
		private string protocolSequence;

		// Token: 0x04002BC2 RID: 11202
		private bool? useRpcEncryption;

		// Token: 0x04002BC3 RID: 11203
		private string rpcProxyServer;
	}
}
