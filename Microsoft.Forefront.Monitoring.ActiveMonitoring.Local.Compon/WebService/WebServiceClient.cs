using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x0200028E RID: 654
	internal class WebServiceClient
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x00043188 File Offset: 0x00041388
		public WebServiceClient(WebServiceConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentException("config");
			}
			this.configuration = config;
			string text = this.Configuration.ProxyAssembly;
			if (!File.Exists(text))
			{
				string fileName = Path.GetFileName(this.Configuration.ProxyAssembly);
				text = Path.Combine(".", fileName);
				if (!File.Exists(text))
				{
					text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
				}
			}
			try
			{
				this.assembly = Assembly.LoadFrom(text);
			}
			catch
			{
				Type type = Type.GetType(this.Configuration.ProxyClassName, false);
				if (!(type != null))
				{
					throw;
				}
				this.assembly = type.Assembly;
			}
			ServicePointManager.EnableDnsRoundRobin = true;
			ServicePointManager.DnsRefreshTimeout = 100000;
			ServicePointManager.MaxServicePointIdleTime = 90000;
			if (this.Configuration.ProxyGenerated)
			{
				this.proxy = this.CreateProxy();
				return;
			}
			this.proxy = this.CreateNonGeneratedProxy();
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0004328C File Offset: 0x0004148C
		internal object Proxy
		{
			get
			{
				return this.proxy;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00043294 File Offset: 0x00041494
		internal WebServiceConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0004329C File Offset: 0x0004149C
		internal Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x000432A4 File Offset: 0x000414A4
		internal Binding Binding
		{
			get
			{
				Binding binding;
				if (this.Configuration.BindingType.Equals("WSHttpBinding", StringComparison.OrdinalIgnoreCase))
				{
					binding = this.WSHttpBinding;
				}
				else if (this.Configuration.BindingType.Equals("NetTcpBinding", StringComparison.OrdinalIgnoreCase))
				{
					binding = this.NetTcpBinding;
				}
				else if (this.Configuration.BindingType.Equals("BasicHttpBinding", StringComparison.OrdinalIgnoreCase))
				{
					binding = this.BasicHttpBinding;
				}
				else if (this.Configuration.BindingType.Equals("WSDualHttpBinding", StringComparison.OrdinalIgnoreCase))
				{
					binding = this.WSDualHttpBinding;
				}
				else
				{
					if (!this.Configuration.BindingType.Equals("NetNamedPipeBinding", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(string.Format("Binding {0} not supported.", this.Configuration.BindingType));
					}
					binding = this.NetNamedPipeBinding;
				}
				if (this.Configuration.SendTimeout != TimeSpan.MaxValue)
				{
					binding.SendTimeout = this.Configuration.SendTimeout;
				}
				if (this.Configuration.ReceiveTimeout != TimeSpan.MaxValue)
				{
					binding.ReceiveTimeout = this.Configuration.ReceiveTimeout;
				}
				if (this.Configuration.OpenTimeout != TimeSpan.MaxValue)
				{
					binding.OpenTimeout = this.Configuration.OpenTimeout;
				}
				if (this.Configuration.CloseTimeout != TimeSpan.MaxValue)
				{
					binding.CloseTimeout = this.Configuration.CloseTimeout;
				}
				return binding;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00043418 File Offset: 0x00041618
		internal WSHttpBinding WSHttpBinding
		{
			get
			{
				WSHttpBinding wshttpBinding = new WSHttpBinding();
				if (!string.IsNullOrWhiteSpace(this.Configuration.AllowCookies))
				{
					wshttpBinding.AllowCookies = Utils.GetBoolean(this.Configuration.AllowCookies, "AllowCookies");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BypassProxyOnLocal))
				{
					wshttpBinding.BypassProxyOnLocal = Utils.GetBoolean(this.Configuration.BypassProxyOnLocal, "BypassProxyOnLocal");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.HostNameComparisonMode))
				{
					wshttpBinding.HostNameComparisonMode = Utils.GetEnumValue<HostNameComparisonMode>(this.Configuration.HostNameComparisonMode, "HostNameComparisonMode");
				}
				if (this.Configuration.MaxBufferPoolSize > 0)
				{
					wshttpBinding.MaxBufferPoolSize = (long)this.Configuration.MaxBufferPoolSize;
				}
				if (this.Configuration.MaxReceivedMessageSize > 0L)
				{
					wshttpBinding.MaxReceivedMessageSize = this.Configuration.MaxReceivedMessageSize;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageEncoding))
				{
					wshttpBinding.MessageEncoding = Utils.GetEnumValue<WSMessageEncoding>(this.Configuration.MessageEncoding, "MessageEncoding");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingName))
				{
					wshttpBinding.Name = this.Configuration.BindingName;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingNamespace))
				{
					wshttpBinding.Namespace = this.Configuration.BindingNamespace;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TextEncoding))
				{
					wshttpBinding.TextEncoding = this.TextEncoding;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransactionFlow))
				{
					wshttpBinding.TransactionFlow = Utils.GetBoolean(this.Configuration.TransactionFlow, "TransactionFlow");
				}
				this.SetReaderQuotas(wshttpBinding.ReaderQuotas);
				this.SetReliableSession(wshttpBinding.ReliableSession);
				if (!string.IsNullOrWhiteSpace(this.Configuration.SecurityMode))
				{
					wshttpBinding.Security.Mode = Utils.GetEnumValue<SecurityMode>(this.Configuration.SecurityMode, "Mode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransportCredentialType))
				{
					wshttpBinding.Security.Transport.ClientCredentialType = Utils.GetEnumValue<HttpClientCredentialType>(this.Configuration.TransportCredentialType, "ClientCredentialType");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.Realm))
				{
					wshttpBinding.Security.Transport.Realm = this.Configuration.Realm;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.AlgorithmSuite))
				{
					wshttpBinding.Security.Message.AlgorithmSuite = Utils.GetProperty<SecurityAlgorithmSuite>(this.Configuration.AlgorithmSuite, "AlgorithmSuite");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageCredentialType))
				{
					wshttpBinding.Security.Message.ClientCredentialType = Utils.GetEnumValue<MessageCredentialType>(this.Configuration.MessageCredentialType, "ClientCredentialType");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.EstablishSecurityContext))
				{
					wshttpBinding.Security.Message.EstablishSecurityContext = Utils.GetBoolean(this.Configuration.EstablishSecurityContext, "EstablishSecurityContext");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.NegotiateServiceCredential))
				{
					wshttpBinding.Security.Message.NegotiateServiceCredential = Utils.GetBoolean(this.Configuration.NegotiateServiceCredential, "NegotiateServiceCredential");
				}
				wshttpBinding.UseDefaultWebProxy = this.SetWebProxy();
				return wshttpBinding;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00043748 File Offset: 0x00041948
		internal NetTcpBinding NetTcpBinding
		{
			get
			{
				NetTcpBinding netTcpBinding = new NetTcpBinding();
				if (!string.IsNullOrWhiteSpace(this.Configuration.HostNameComparisonMode))
				{
					netTcpBinding.HostNameComparisonMode = Utils.GetEnumValue<HostNameComparisonMode>(this.Configuration.HostNameComparisonMode, "HostNameComparisonMode");
				}
				if (this.Configuration.ListenBacklog > 0)
				{
					netTcpBinding.ListenBacklog = this.Configuration.ListenBacklog;
				}
				if (this.Configuration.MaxBufferPoolSize > 0)
				{
					netTcpBinding.MaxBufferPoolSize = (long)this.Configuration.MaxBufferPoolSize;
				}
				if (this.Configuration.MaxBufferSize > 0)
				{
					netTcpBinding.MaxBufferSize = this.Configuration.MaxBufferSize;
				}
				if (this.Configuration.MaxConnections > 0)
				{
					netTcpBinding.MaxConnections = this.Configuration.MaxConnections;
				}
				if (this.Configuration.MaxReceivedMessageSize > 0L)
				{
					netTcpBinding.MaxReceivedMessageSize = this.Configuration.MaxReceivedMessageSize;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingName))
				{
					netTcpBinding.Name = this.Configuration.BindingName;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingNamespace))
				{
					netTcpBinding.Namespace = this.Configuration.BindingNamespace;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransactionFlow))
				{
					netTcpBinding.TransactionFlow = Utils.GetBoolean(this.Configuration.TransactionFlow, "TransactionFlow");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransferMode))
				{
					netTcpBinding.TransferMode = Utils.GetEnumValue<TransferMode>(this.Configuration.TransferMode, "TransferMode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransactionProtocol))
				{
					netTcpBinding.TransactionProtocol = Utils.GetProperty<TransactionProtocol>(this.Configuration.TransactionProtocol, "TransactionProtocol");
				}
				this.SetReaderQuotas(netTcpBinding.ReaderQuotas);
				this.SetReliableSession(netTcpBinding.ReliableSession);
				if (!string.IsNullOrWhiteSpace(this.Configuration.SecurityMode))
				{
					netTcpBinding.Security.Mode = Utils.GetEnumValue<SecurityMode>(this.Configuration.SecurityMode, "Mode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransportCredentialType))
				{
					netTcpBinding.Security.Transport.ClientCredentialType = Utils.GetEnumValue<TcpClientCredentialType>(this.Configuration.TransportCredentialType, "ClientCredentialType");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.ProtectionLevel))
				{
					netTcpBinding.Security.Transport.ProtectionLevel = Utils.GetEnumValue<ProtectionLevel>(this.Configuration.ProtectionLevel, "ProtectionLevel");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.AlgorithmSuite))
				{
					netTcpBinding.Security.Message.AlgorithmSuite = Utils.GetProperty<SecurityAlgorithmSuite>(this.Configuration.AlgorithmSuite, "AlgorithmSuite");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageCredentialType))
				{
					netTcpBinding.Security.Message.ClientCredentialType = Utils.GetEnumValue<MessageCredentialType>(this.Configuration.MessageCredentialType, "ClientCredentialType");
				}
				return netTcpBinding;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00043A1C File Offset: 0x00041C1C
		internal BasicHttpBinding BasicHttpBinding
		{
			get
			{
				BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
				if (!string.IsNullOrWhiteSpace(this.Configuration.AllowCookies))
				{
					basicHttpBinding.AllowCookies = Utils.GetBoolean(this.Configuration.AllowCookies, "AllowCookies");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BypassProxyOnLocal))
				{
					basicHttpBinding.BypassProxyOnLocal = Utils.GetBoolean(this.Configuration.BypassProxyOnLocal, "BypassProxyOnLocal");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.HostNameComparisonMode))
				{
					basicHttpBinding.HostNameComparisonMode = Utils.GetEnumValue<HostNameComparisonMode>(this.Configuration.HostNameComparisonMode, "HostNameComparisonMode");
				}
				if (this.Configuration.MaxBufferPoolSize > 0)
				{
					basicHttpBinding.MaxBufferPoolSize = (long)this.Configuration.MaxBufferPoolSize;
				}
				if (this.Configuration.MaxBufferSize > 0)
				{
					basicHttpBinding.MaxBufferSize = this.Configuration.MaxBufferSize;
				}
				if (this.Configuration.MaxReceivedMessageSize > 0L)
				{
					basicHttpBinding.MaxReceivedMessageSize = this.Configuration.MaxReceivedMessageSize;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageEncoding))
				{
					basicHttpBinding.MessageEncoding = Utils.GetEnumValue<WSMessageEncoding>(this.Configuration.MessageEncoding, "MessageEncoding");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingName))
				{
					basicHttpBinding.Name = this.Configuration.BindingName;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingNamespace))
				{
					basicHttpBinding.Namespace = this.Configuration.BindingName;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TextEncoding))
				{
					basicHttpBinding.TextEncoding = this.TextEncoding;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransferMode))
				{
					basicHttpBinding.TransferMode = Utils.GetEnumValue<TransferMode>(this.Configuration.TransferMode, "TransferMode");
				}
				this.SetReaderQuotas(basicHttpBinding.ReaderQuotas);
				if (!string.IsNullOrWhiteSpace(this.Configuration.SecurityMode))
				{
					basicHttpBinding.Security.Mode = Utils.GetEnumValue<BasicHttpSecurityMode>(this.Configuration.SecurityMode, "Mode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransportCredentialType))
				{
					basicHttpBinding.Security.Transport.ClientCredentialType = Utils.GetEnumValue<HttpClientCredentialType>(this.Configuration.TransportCredentialType, "ClientCredentialType");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.Realm))
				{
					basicHttpBinding.Security.Transport.Realm = this.Configuration.Realm;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.AlgorithmSuite))
				{
					basicHttpBinding.Security.Message.AlgorithmSuite = Utils.GetProperty<SecurityAlgorithmSuite>(this.Configuration.AlgorithmSuite, "AlgorithmSuite");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageCredentialType))
				{
					basicHttpBinding.Security.Message.ClientCredentialType = Utils.GetEnumValue<BasicHttpMessageCredentialType>(this.Configuration.MessageCredentialType, "ClientCredentialType");
				}
				basicHttpBinding.UseDefaultWebProxy = this.SetWebProxy();
				return basicHttpBinding;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00043CF0 File Offset: 0x00041EF0
		internal WSDualHttpBinding WSDualHttpBinding
		{
			get
			{
				WSDualHttpBinding wsdualHttpBinding = new WSDualHttpBinding();
				if (!string.IsNullOrWhiteSpace(this.Configuration.BypassProxyOnLocal))
				{
					wsdualHttpBinding.BypassProxyOnLocal = Utils.GetBoolean(this.Configuration.BypassProxyOnLocal, "BypassProxyOnLocal");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.ClientBaseAddress))
				{
					wsdualHttpBinding.ClientBaseAddress = new Uri(this.Configuration.ClientBaseAddress);
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.HostNameComparisonMode))
				{
					wsdualHttpBinding.HostNameComparisonMode = Utils.GetEnumValue<HostNameComparisonMode>(this.Configuration.HostNameComparisonMode, "HostNameComparisonMode");
				}
				if (this.Configuration.MaxBufferPoolSize > 0)
				{
					wsdualHttpBinding.MaxBufferPoolSize = (long)this.Configuration.MaxBufferPoolSize;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageEncoding))
				{
					wsdualHttpBinding.MessageEncoding = Utils.GetEnumValue<WSMessageEncoding>(this.Configuration.MessageEncoding, "MessageEncoding");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingName))
				{
					wsdualHttpBinding.Name = this.Configuration.BindingName;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingNamespace))
				{
					wsdualHttpBinding.Namespace = this.Configuration.BindingNamespace;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TextEncoding))
				{
					wsdualHttpBinding.TextEncoding = this.TextEncoding;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransactionFlow))
				{
					wsdualHttpBinding.TransactionFlow = Utils.GetBoolean(this.Configuration.TransactionFlow, "TransactionFlow");
				}
				this.SetReaderQuotas(wsdualHttpBinding.ReaderQuotas);
				this.SetReliableSession(wsdualHttpBinding.ReliableSession);
				if (!string.IsNullOrWhiteSpace(this.Configuration.SecurityMode))
				{
					wsdualHttpBinding.Security.Mode = Utils.GetEnumValue<WSDualHttpSecurityMode>(this.Configuration.SecurityMode, "Mode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.MessageCredentialType))
				{
					wsdualHttpBinding.Security.Message.ClientCredentialType = Utils.GetEnumValue<MessageCredentialType>(this.Configuration.MessageCredentialType, "ClientCredentialType");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.AlgorithmSuite))
				{
					wsdualHttpBinding.Security.Message.AlgorithmSuite = Utils.GetProperty<SecurityAlgorithmSuite>(this.Configuration.AlgorithmSuite, "AlgorithmSuite");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.NegotiateServiceCredential))
				{
					wsdualHttpBinding.Security.Message.NegotiateServiceCredential = Utils.GetBoolean(this.Configuration.NegotiateServiceCredential, "NegotiateServiceCredential");
				}
				wsdualHttpBinding.UseDefaultWebProxy = this.SetWebProxy();
				return wsdualHttpBinding;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00043F60 File Offset: 0x00042160
		internal NetNamedPipeBinding NetNamedPipeBinding
		{
			get
			{
				NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding();
				if (!string.IsNullOrWhiteSpace(this.Configuration.HostNameComparisonMode))
				{
					netNamedPipeBinding.HostNameComparisonMode = Utils.GetEnumValue<HostNameComparisonMode>(this.Configuration.HostNameComparisonMode, "HostNameComparisonMode");
				}
				if (this.Configuration.MaxBufferPoolSize > 0)
				{
					netNamedPipeBinding.MaxBufferPoolSize = (long)this.Configuration.MaxBufferPoolSize;
				}
				if (this.Configuration.MaxBufferSize > 0)
				{
					netNamedPipeBinding.MaxBufferSize = this.Configuration.MaxBufferSize;
				}
				if (this.Configuration.MaxConnections > 0)
				{
					netNamedPipeBinding.MaxConnections = this.Configuration.MaxConnections;
				}
				if (this.Configuration.MaxReceivedMessageSize > 0L)
				{
					netNamedPipeBinding.MaxReceivedMessageSize = this.Configuration.MaxReceivedMessageSize;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingName))
				{
					netNamedPipeBinding.Name = this.Configuration.BindingName;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.BindingNamespace))
				{
					netNamedPipeBinding.Namespace = this.Configuration.BindingNamespace;
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransactionFlow))
				{
					netNamedPipeBinding.TransactionFlow = Utils.GetBoolean(this.Configuration.TransactionFlow, "TransactionFlow");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransferMode))
				{
					netNamedPipeBinding.TransferMode = Utils.GetEnumValue<TransferMode>(this.Configuration.TransferMode, "TransferMode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.TransactionProtocol))
				{
					netNamedPipeBinding.TransactionProtocol = Utils.GetProperty<TransactionProtocol>(this.Configuration.TransactionProtocol, "TransactionProtocol");
				}
				this.SetReaderQuotas(netNamedPipeBinding.ReaderQuotas);
				if (!string.IsNullOrWhiteSpace(this.Configuration.SecurityMode))
				{
					netNamedPipeBinding.Security.Mode = Utils.GetEnumValue<NetNamedPipeSecurityMode>(this.Configuration.SecurityMode, "Mode");
				}
				if (!string.IsNullOrWhiteSpace(this.Configuration.ProtectionLevel))
				{
					netNamedPipeBinding.Security.Transport.ProtectionLevel = Utils.GetEnumValue<ProtectionLevel>(this.Configuration.ProtectionLevel, "ProtectionLevel");
				}
				return netNamedPipeBinding;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00044164 File Offset: 0x00042364
		internal EndpointAddress EndpointAddress
		{
			get
			{
				string text = this.Configuration.Uri;
				if (this.Configuration.FwLinkEnabled)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.Configuration.FwLinkUri);
					using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
					{
						if (httpWebResponse.StatusCode == HttpStatusCode.OK)
						{
							text = httpWebResponse.ResponseUri.AbsoluteUri;
						}
					}
				}
				if (this.Configuration.UseCertificateAuthentication)
				{
					return new EndpointAddress(new Uri(text), EndpointIdentity.CreateDnsIdentity(this.Configuration.FindValue), new AddressHeader[0]);
				}
				return new EndpointAddress(text);
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00044218 File Offset: 0x00042418
		internal Encoding TextEncoding
		{
			get
			{
				string item = this.Configuration.TextEncoding.ToLower();
				List<string> list = new List<string>
				{
					"utf-8",
					"Utf8TextEncoding".ToLower()
				};
				if (list.Contains(item))
				{
					return Encoding.UTF8;
				}
				list = new List<string>
				{
					"utf-16",
					"Utf16TextEncoding".ToLower()
				};
				if (list.Contains(item))
				{
					return Encoding.Unicode;
				}
				list = new List<string>
				{
					"unicodeFFFE",
					"utf-16BE".ToLower(),
					"UnicodeFffeTextEncoding".ToLower()
				};
				if (list.Contains(item))
				{
					return Encoding.BigEndianUnicode;
				}
				throw new ArgumentException(string.Format("Work definition error - attribute 'TextEncoding' has invalid value '{0}'.", this.Configuration.TextEncoding));
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x000442FB File Offset: 0x000424FB
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x00044303 File Offset: 0x00042503
		private bool Aborted { get; set; }

		// Token: 0x0600157D RID: 5501 RVA: 0x0004430C File Offset: 0x0004250C
		public MethodInfo GetValidatorMethod(Type paramType)
		{
			if (string.IsNullOrWhiteSpace(this.Configuration.ProxyValidatorClassName))
			{
				throw new InvalidOperationException("Work definition error - ValidatorClassName is missing in Proxy node");
			}
			if (string.IsNullOrWhiteSpace(this.Configuration.ProxyValidatorMethodName))
			{
				throw new InvalidOperationException("Work definition error - ValidatorMethodName is missing in Proxy node");
			}
			Type type = this.Assembly.GetType(this.Configuration.ProxyValidatorClassName, true, true);
			MethodInfo method = type.GetMethod(this.Configuration.ProxyValidatorMethodName, new Type[]
			{
				paramType,
				paramType,
				typeof(string).MakeByRefType()
			});
			if (method == null)
			{
				throw new InvalidOperationException(string.Format("Work definition error - ValidatorMethodName={0}.{1} which accepts parameters of type={2} doesnot exist in Assembly={3}", new object[]
				{
					this.Configuration.ProxyValidatorClassName,
					this.Configuration.ProxyValidatorMethodName,
					paramType,
					this.Configuration.ProxyAssembly
				}));
			}
			return method;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000443F0 File Offset: 0x000425F0
		public MethodInfo GetDiagnosticsInfoMethod()
		{
			if (string.IsNullOrWhiteSpace(this.Configuration.ProxyValidatorClassName))
			{
				throw new InvalidOperationException("Work definition error - ValidatorClassName is missing in Proxy node");
			}
			if (string.IsNullOrWhiteSpace(this.Configuration.ProxyDiagnosticsInfoMethodName))
			{
				throw new InvalidOperationException("Work definition error - DiagnosticsInfoMethodName is missing in Proxy node");
			}
			Type type = this.Assembly.GetType(this.Configuration.ProxyValidatorClassName, true, true);
			MethodInfo method = type.GetMethod(this.Configuration.ProxyDiagnosticsInfoMethodName);
			if (method == null)
			{
				throw new InvalidOperationException(string.Format("Work definition error - DiagnosticsInfoMethodName={0}.{1} doesnot exist in Assembly={3}", this.Configuration.ProxyValidatorClassName, this.Configuration.ProxyDiagnosticsInfoMethodName, this.Configuration.ProxyAssembly));
			}
			return method;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000444A0 File Offset: 0x000426A0
		internal void Abort()
		{
			if (this.Proxy != null)
			{
				MethodInfo method = this.Proxy.GetType().GetMethod("Abort");
				if (method != null)
				{
					method.Invoke(this.Proxy, null);
					this.Aborted = true;
				}
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000444EC File Offset: 0x000426EC
		internal void Close()
		{
			if (this.Proxy != null && !this.Aborted)
			{
				MethodInfo method = this.Proxy.GetType().GetMethod("Close");
				if (method != null)
				{
					try
					{
						method.Invoke(this.Proxy, null);
					}
					catch (CommunicationException)
					{
						this.Abort();
						throw;
					}
					catch (TimeoutException)
					{
						this.Abort();
						throw;
					}
				}
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00044568 File Offset: 0x00042768
		private static X509Certificate2 FindFirstCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, string findValue)
		{
			X509Store x509Store = new X509Store(storeName, storeLocation);
			X509Certificate2 result;
			try
			{
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(findType, findValue, true);
				if (x509Certificate2Collection.Count == 0)
				{
					throw new Exception(string.Format("Certificate not found. StoreName {0}; StoreLocation {1}; FindType {2}; FindValue {3}", new object[]
					{
						storeName,
						storeLocation,
						findType,
						findValue
					}));
				}
				result = x509Certificate2Collection[0];
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000445F4 File Offset: 0x000427F4
		private void CheckConstructor()
		{
			string proxyClassName = this.Configuration.ProxyClassName;
			Type type = this.Assembly.GetType(proxyClassName, true, true);
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.HasThis, new Type[]
			{
				typeof(Binding),
				typeof(EndpointAddress)
			}, null);
			if (constructor == null)
			{
				throw new Exception(string.Format("The constructor of {0} that takes Binding and EndpointAddress parameters is not available.", proxyClassName));
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00044668 File Offset: 0x00042868
		private object CreateProxy()
		{
			this.CheckConstructor();
			object[] args = new object[]
			{
				this.Binding,
				this.EndpointAddress
			};
			string proxyClassName = this.Configuration.ProxyClassName;
			object obj = this.Assembly.CreateInstance(proxyClassName, false, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public, null, args, null, null);
			if (obj == null)
			{
				throw new Exception(string.Format("Failure creating an instance of {0}.", proxyClassName));
			}
			PropertyInfo property = obj.GetType().GetProperty("ClientCredentials");
			if (property == null)
			{
				throw new Exception(string.Format("The type {0} does not contain the property 'ClientCredentials'.", proxyClassName));
			}
			ClientCredentials clientCredentials = (ClientCredentials)property.GetValue(obj, null);
			if (clientCredentials == null)
			{
				throw new Exception(string.Format("The value of property 'ClientCredentials' of type {0} is null.", proxyClassName));
			}
			if (this.Configuration.UseCertificateAuthentication)
			{
				X509CertificateInitiatorClientCredential clientCertificate = clientCredentials.ClientCertificate;
				clientCertificate.Certificate = WebServiceClient.FindFirstCertificate(this.configuration.StoreLocation, this.configuration.StoreName, this.Configuration.X509FindType, this.Configuration.FindValue);
				if (!string.IsNullOrWhiteSpace(this.Configuration.ServiceCertificateValidationMode))
				{
					X509CertificateRecipientClientCredential serviceCertificate = clientCredentials.ServiceCertificate;
					serviceCertificate.Authentication.CertificateValidationMode = Utils.GetEnumValue<X509CertificateValidationMode>(this.Configuration.ServiceCertificateValidationMode, "ServiceCertificateValidationMode");
				}
			}
			else if (this.Configuration.UseUserNameAuthentication)
			{
				UserNamePasswordClientCredential userName = clientCredentials.UserName;
				userName.UserName = this.Configuration.Username;
				userName.Password = this.Configuration.Password;
			}
			return obj;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000447E0 File Offset: 0x000429E0
		private object CreateNonGeneratedProxy()
		{
			string proxyClassName = this.Configuration.ProxyClassName;
			object[] array = null;
			if (this.Configuration.ProxyInstanceMethod.Parameters.Count != 0)
			{
				List<object> list = new List<object>();
				foreach (Parameter parameter in this.Configuration.ProxyInstanceMethod.Parameters)
				{
					object item = null;
					if (!parameter.IsNull)
					{
						if (string.IsNullOrWhiteSpace(parameter.Type))
						{
							item = parameter.Value.InnerText;
						}
						else
						{
							Type type;
							try
							{
								type = Type.GetType(parameter.Type, true, true);
							}
							catch
							{
								type = this.Assembly.GetType(parameter.Type, true, true);
							}
							item = Utils.DeserializeFromXml(parameter.Value.InnerXml, type);
						}
					}
					list.Add(item);
				}
				array = list.ToArray();
			}
			object obj;
			if (!this.Configuration.ProxyInstanceMethodIsStatic)
			{
				obj = this.Assembly.CreateInstance(proxyClassName, false, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public, null, array, null, null);
			}
			else
			{
				string name = this.Configuration.ProxyInstanceMethod.Name;
				Type type2 = this.Assembly.GetType(proxyClassName, true, true);
				MethodInfo method = type2.GetMethod(name, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);
				if (method == null)
				{
					throw new ArgumentException(string.Format("ProxyConstructor '{0}' not found in proxy class type '{1}'.", name, proxyClassName));
				}
				obj = method.Invoke(null, array);
			}
			if (obj == null)
			{
				throw new Exception(string.Format("Failure creating an instance of {0}.", proxyClassName));
			}
			return obj;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00044978 File Offset: 0x00042B78
		private void SetReaderQuotas(XmlDictionaryReaderQuotas readerQuotas)
		{
			if (this.Configuration.MaxDepth > 0)
			{
				readerQuotas.MaxDepth = this.Configuration.MaxDepth;
			}
			if (this.Configuration.MaxNameTableCharCount > 0)
			{
				readerQuotas.MaxNameTableCharCount = this.Configuration.MaxNameTableCharCount;
			}
			if (this.Configuration.MaxArrayLength > 0)
			{
				readerQuotas.MaxArrayLength = this.Configuration.MaxArrayLength;
			}
			if (this.Configuration.MaxBytesPerRead > 0)
			{
				readerQuotas.MaxBytesPerRead = this.Configuration.MaxBytesPerRead;
			}
			if (this.Configuration.MaxStringContentLength > 0)
			{
				readerQuotas.MaxStringContentLength = this.Configuration.MaxStringContentLength;
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00044A20 File Offset: 0x00042C20
		private void SetReliableSession(OptionalReliableSession reliableSession)
		{
			if (!string.IsNullOrWhiteSpace(this.Configuration.Ordered))
			{
				reliableSession.Ordered = Utils.GetBoolean(this.Configuration.Ordered, "Ordered");
			}
			if (this.Configuration.InactivityTimeout != TimeSpan.MaxValue)
			{
				reliableSession.InactivityTimeout = this.Configuration.InactivityTimeout;
			}
			if (this.Configuration.ReliableSessionEnabled != null)
			{
				reliableSession.Enabled = (this.Configuration.ReliableSessionEnabled ?? false);
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00044ABC File Offset: 0x00042CBC
		private void SetReliableSession(ReliableSession reliableSession)
		{
			if (!string.IsNullOrWhiteSpace(this.Configuration.Ordered))
			{
				reliableSession.Ordered = Utils.GetBoolean(this.Configuration.Ordered, "Ordered");
			}
			if (this.Configuration.InactivityTimeout != TimeSpan.MaxValue)
			{
				reliableSession.InactivityTimeout = this.Configuration.InactivityTimeout;
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00044B20 File Offset: 0x00042D20
		private bool SetWebProxy()
		{
			if (this.Configuration.WebProxyEnabled && !this.Configuration.UseDefaultWebProxy)
			{
				Uri uri = new Uri(this.Configuration.WebProxyServerUri);
				WebProxy webProxy = new WebProxy(uri.Host, this.Configuration.WebProxyPort);
				if (this.Configuration.WebProxyCredentialsRequired)
				{
					if (string.IsNullOrEmpty(this.Configuration.WebProxyDomain))
					{
						webProxy.Credentials = new NetworkCredential(this.Configuration.WebProxyUsername, this.Configuration.WebProxyPassword);
					}
					else
					{
						webProxy.Credentials = new NetworkCredential(this.Configuration.WebProxyUsername, this.Configuration.WebProxyPassword, this.Configuration.WebProxyDomain);
					}
				}
				WebRequest.DefaultWebProxy = webProxy;
			}
			return this.Configuration.UseDefaultWebProxy;
		}

		// Token: 0x04000A71 RID: 2673
		private object proxy;

		// Token: 0x04000A72 RID: 2674
		private WebServiceConfiguration configuration;

		// Token: 0x04000A73 RID: 2675
		private Assembly assembly;
	}
}
