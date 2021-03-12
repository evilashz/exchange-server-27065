using System;
using System.ServiceModel;
using System.ServiceProcess;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.MSARPSService
{
	// Token: 0x02000002 RID: 2
	public sealed class Service : ExServiceBase, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public Service()
		{
			ExWatson.Register();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E0 File Offset: 0x000002E0
		public static void Main(string[] args)
		{
			using (Service service = new Service())
			{
				int num = Privileges.RemoveAllExcept(Service.requiredPrivileges);
				if (num != 0)
				{
					Environment.Exit(num);
				}
				bool flag = false;
				foreach (string text in args)
				{
					string a = text.ToLower();
					if (!(a == "-console"))
					{
						Console.WriteLine("invalid options\n");
						return;
					}
					flag = true;
				}
				if (flag)
				{
					Service.runningAsService = false;
					ExServiceBase.RunAsConsole(service);
				}
				else
				{
					Service.runningAsService = true;
					ServiceBase.Run(service);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002188 File Offset: 0x00000388
		protected override void OnStartInternal(string[] args)
		{
			try
			{
				this.msaTokenServiceHost = new ServiceHost(typeof(MSATokenValidationService), new Uri[0]);
				this.msaTokenServiceHost.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(this.HandleUnknownMessageReceived);
				this.msaTokenServiceHost.Faulted += this.HandleServiceHostFault;
				this.msaTokenServiceHost.Open(TimeSpan.FromSeconds(60.0));
			}
			catch (Exception ex)
			{
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "OnStartInternal", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002230 File Offset: 0x00000430
		protected override void OnStopInternal()
		{
			try
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug((long)this.GetHashCode(), "MSARPSServie.OnStopInternal - Stopping MSARPSServie");
				if (Service.runningAsService)
				{
					base.RequestAdditionalTime((int)(((this.msaTokenServiceHost != null) ? this.msaTokenServiceHost.CloseTimeout : TimeSpan.Zero) + TimeSpan.FromMinutes(2.0)).TotalMilliseconds);
				}
				this.ServiceCleanUp(true);
			}
			catch (System.TimeoutException ex)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.OnStopInternal() - MSARPSServie stopped with exception: {0}", ex);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "OnStopInternal", new object[]
				{
					ex
				});
			}
			catch (System.ServiceProcess.TimeoutException ex2)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.OnStopInternal() - MSARPSServie stopped with exception: {0}", ex2);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "OnStopInternal", new object[]
				{
					ex2
				});
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002334 File Offset: 0x00000534
		private void AbortServiceGracefully()
		{
			this.ServiceCleanUp(false);
			Environment.Exit(1);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002344 File Offset: 0x00000544
		private void ServiceCleanUp(bool throwOnUnExpectedError)
		{
			try
			{
				if (this.msaTokenServiceHost != null)
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug((long)this.GetHashCode(), "MSARPSServie.ServiceCleanUp - Stopping Wcf Host");
					WcfUtils.DisposeWcfClientGracefully(this.msaTokenServiceHost, false);
					this.msaTokenServiceHost = null;
				}
			}
			catch (System.TimeoutException ex)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.ServiceCleanUp() - MSARPSServie stopped with exception: {0}", ex);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "ServiceCleanUp", new object[]
				{
					ex
				});
			}
			catch (System.ServiceProcess.TimeoutException ex2)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.ServiceCleanUp() - MSARPSServie stopped with exception: {0}", ex2);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "ServiceCleanUp", new object[]
				{
					ex2
				});
			}
			catch (CommunicationException ex3)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.ServiceCleanUp() - MSARPSServie stopped with exception: {0}", ex3);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "ServiceCleanUp", new object[]
				{
					ex3
				});
			}
			catch (AggregateException ex4)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.ServiceCleanUp() - MSARPSServie stopped with exception: {0}", ex4);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "ServiceCleanUp", new object[]
				{
					ex4
				});
			}
			catch (Exception ex5)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>((long)this.GetHashCode(), "MSARPSServie.ServiceCleanUp() - MSARPSServie stopped with exception: {0}", ex5);
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "ServiceCleanUp", new object[]
				{
					ex5
				});
				if (throwOnUnExpectedError)
				{
					throw;
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002510 File Offset: 0x00000710
		private void HandleUnknownMessageReceived(object sender, EventArgs e)
		{
			ExTraceGlobals.AuthenticationTracer.TraceError(0L, "Unknown Message Received {0}", new object[]
			{
				sender ?? string.Empty
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002544 File Offset: 0x00000744
		private void HandleServiceHostFault(object sender, EventArgs e)
		{
			try
			{
				ExTraceGlobals.AuthenticationTracer.TraceError((long)this.GetHashCode(), "Service Endpoint Faulted. Details {0}", new object[]
				{
					sender ?? string.Empty
				});
				this.msaTokenServiceHost.Faulted -= this.HandleServiceHostFault;
				this.msaTokenServiceHost.UnknownMessageReceived -= new EventHandler<UnknownMessageReceivedEventArgs>(this.HandleUnknownMessageReceived);
				WcfUtils.DisposeWcfClientGracefully(this.msaTokenServiceHost, false);
				this.msaTokenServiceHost = new ServiceHost(typeof(MSATokenValidationService), new Uri[0]);
				if (Service.tryRecoverFromFailureCount < 3)
				{
					this.msaTokenServiceHost.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(this.HandleUnknownMessageReceived);
					this.msaTokenServiceHost.Faulted += this.HandleServiceHostFault;
					Service.tryRecoverFromFailureCount++;
				}
				this.msaTokenServiceHost.Open(TimeSpan.FromSeconds(120.0));
			}
			catch (Exception ex)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string>((long)this.GetHashCode(), "Error trying to create WCF Endpoint. Details {0}", ex.ToString());
				Service.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_MSARPSServiceUnhandledException, "HandleServiceHostFault", new object[]
				{
					e
				});
				this.AbortServiceGracefully();
			}
			finally
			{
				ServiceHost serviceHost = this.msaTokenServiceHost;
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly string[] requiredPrivileges = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeCreateGlobalPrivilege"
		};

		// Token: 0x04000002 RID: 2
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.AuthenticationTracer.Category, "MSExchange LiveIdBasicAuthentication");

		// Token: 0x04000003 RID: 3
		private static int tryRecoverFromFailureCount;

		// Token: 0x04000004 RID: 4
		private static bool runningAsService;

		// Token: 0x04000005 RID: 5
		private ServiceHost msaTokenServiceHost;
	}
}
