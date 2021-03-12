using System;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Clients;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x0200008F RID: 143
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VersionedClientBase<TService> : WcfClientWithFaultHandling<TService, FaultException<LoadBalanceFault>>, IWcfClient where TService : class, IVersionedService
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0000D4BA File Offset: 0x0000B6BA
		protected VersionedClientBase(Binding binding, EndpointAddress remoteAddress, ILogger logger) : base(binding, remoteAddress)
		{
			this.logger = logger;
			LoadBalanceUtils.SetDataContractSerializerBehavior(base.ChannelFactory.Endpoint.Contract);
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0000D4E7 File Offset: 0x0000B6E7
		public static bool UseUpdatedBinding { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		public bool IsValid
		{
			get
			{
				switch (base.State)
				{
				case CommunicationState.Created:
				case CommunicationState.Opening:
				case CommunicationState.Opened:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000D51B File Offset: 0x0000B71B
		protected ILogger Logger
		{
			get
			{
				return this.logger;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000D523 File Offset: 0x0000B723
		public void Disconnect()
		{
			this.Dispose();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000D568 File Offset: 0x0000B768
		public void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			VersionInformation serverVersionInfo = null;
			this.CallService(delegate()
			{
				TService channel = this.Channel;
				channel.ExchangeVersionInformation(clientVersion, out serverVersionInfo);
			});
			serverVersion = serverVersionInfo;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		protected static TClient CreateClient<TClient>(string serverName, ServiceEndpointAddress serviceEndpoint, Func<Binding, EndpointAddress, ILogger, TClient> constructor, ILogger logger) where TClient : VersionedClientBase<TService>
		{
			logger.Log(MigrationEventType.Verbose, "{0}: Attempting TCP connection to {1}/{2}", new object[]
			{
				typeof(TClient).Name,
				serverName,
				serviceEndpoint
			});
			string address = serviceEndpoint.GetAddress(serverName);
			NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.Transport);
			netTcpBinding.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;
			netTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
			netTcpBinding.MaxReceivedMessageSize = 10485760L;
			netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(10.0);
			netTcpBinding.SendTimeout = TimeSpan.FromMinutes(10.0);
			EndpointAddress arg;
			try
			{
				arg = new EndpointAddress(address);
			}
			catch (UriFormatException ex)
			{
				throw new InvalidEndpointAddressException(ex.GetType().Name, address, ex);
			}
			TClient tclient = default(TClient);
			bool flag = false;
			try
			{
				tclient = constructor(netTcpBinding, arg, logger);
				if (VersionedClientBase<TService>.UseUpdatedBinding)
				{
					logger.LogVerbose("Using binging: {0} ({1})", new object[]
					{
						netTcpBinding.Name,
						netTcpBinding.MessageVersion
					});
					LoadBalanceUtils.UpdateAndLogServiceEndpoint(logger, tclient.Endpoint);
				}
				tclient.ExchangeVersionInformation();
				flag = true;
			}
			finally
			{
				if (!flag && tclient != null)
				{
					tclient.Dispose();
				}
			}
			logger.Log(MigrationEventType.Verbose, "{0}: Established connection to {1}, version={2}", new object[]
			{
				typeof(TClient).Name,
				tclient.ServerVersion.ComputerName,
				tclient.ServerVersion.ToString()
			});
			return tclient;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000D778 File Offset: 0x0000B978
		protected void CallService(Action action)
		{
			this.logger.Log(MigrationEventType.Instrumentation, "BEGIN remote service call: {0}", new object[]
			{
				action.Method
			});
			try
			{
				this.CallService(action, base.Endpoint.Address.ToString());
			}
			finally
			{
				this.logger.Log(MigrationEventType.Instrumentation, "End remote service call: {0}", new object[]
				{
					action.Method
				});
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000D810 File Offset: 0x0000BA10
		protected TResult CallService<TResult>(Func<TResult> action)
		{
			TResult result = default(TResult);
			this.logger.Log(MigrationEventType.Instrumentation, "BEGIN remote service call: {0}", new object[]
			{
				action.Method
			});
			try
			{
				this.CallService(delegate
				{
					result = action();
				}, base.Endpoint.Address.ToString());
			}
			finally
			{
				this.logger.Log(MigrationEventType.Instrumentation, "End remote service call: {0}", new object[]
				{
					action.Method
				});
			}
			return result;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000D904 File Offset: 0x0000BB04
		protected void ExchangeVersionInformation()
		{
			VersionInformation serverVersion = null;
			this.CallService(delegate()
			{
				TService channel = this.Channel;
				channel.ExchangeVersionInformation(LoadBalancerVersionInformation.LoadBalancerVersion, out serverVersion);
			});
			base.ServerVersion = serverVersion;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000D944 File Offset: 0x0000BB44
		protected override void HandleFaultException(FaultException<LoadBalanceFault> fault, string context)
		{
			this.logger.LogError(fault, "Error processing service call from server {0}.", new object[]
			{
				context
			});
			fault.Detail.ReconstructAndThrow();
		}

		// Token: 0x040001A9 RID: 425
		private readonly ILogger logger;
	}
}
