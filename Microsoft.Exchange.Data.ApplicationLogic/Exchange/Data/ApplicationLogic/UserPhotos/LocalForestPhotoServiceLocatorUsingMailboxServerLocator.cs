using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E6 RID: 486
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LocalForestPhotoServiceLocatorUsingMailboxServerLocator : IPhotoServiceLocator
	{
		// Token: 0x060011D6 RID: 4566 RVA: 0x0004ADE4 File Offset: 0x00048FE4
		public LocalForestPhotoServiceLocatorUsingMailboxServerLocator(IPerformanceDataLogger perfLogger, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.perfLogger = perfLogger;
			this.tracer = upstreamTracer;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004AE10 File Offset: 0x00049010
		public Uri Locate(ADUser target)
		{
			ArgumentValidator.ThrowIfNull("target", target);
			ArgumentValidator.ThrowIfNull("target.Database", target.Database);
			if (this.lastLocatedTarget != null && this.lastLocatedTarget.Equals(target))
			{
				return this.lastLocatedUri;
			}
			Uri result;
			try
			{
				using (MailboxServerLocator mailboxServerLocator = MailboxServerLocator.CreateWithResourceForestFqdn(target.Database.ObjectGuid, new Fqdn(TopologyProvider.LocalForestFqdn)))
				{
					Uri photoServiceUri = this.GetPhotoServiceUri(this.LocateServer(mailboxServerLocator));
					this.lastLocatedUri = photoServiceUri;
					this.lastLocatedTarget = target;
					result = photoServiceUri;
				}
			}
			catch (BackEndLocatorException arg)
			{
				this.tracer.TraceError<BackEndLocatorException>((long)this.GetHashCode(), "SERVICE LOCATOR[MAILBOXSERVERLOCATOR]: failed to locate service because MailboxServerLocator failed.  Exception: {0}", arg);
				throw;
			}
			catch (ServerLocatorClientTransientException arg2)
			{
				this.tracer.TraceError<ServerLocatorClientTransientException>((long)this.GetHashCode(), "SERVICE LOCATOR[MAILBOXSERVERLOCATOR]: hit a transient error in ServerLocator trying to locate photo service.  Exception: {0}", arg2);
				throw;
			}
			catch (ServiceDiscoveryTransientException arg3)
			{
				this.tracer.TraceError<ServiceDiscoveryTransientException>((long)this.GetHashCode(), "SERVICE LOCATOR[MAILBOXSERVERLOCATOR]: hit a transient error in service discovery trying to locate photo service.  Exception: {0}", arg3);
				throw;
			}
			catch (TransientException arg4)
			{
				this.tracer.TraceError<TransientException>((long)this.GetHashCode(), "SERVICE LOCATOR[MAILBOXSERVERLOCATOR]: hit a transient error trying to locate photo service.  Exception: {0}", arg4);
				throw;
			}
			catch (Exception arg5)
			{
				this.tracer.TraceError<Exception>((long)this.GetHashCode(), "SERVICE LOCATOR[MAILBOXSERVERLOCATOR]: failed to locate photo service.  Exception: {0}", arg5);
				throw;
			}
			return result;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004AF7C File Offset: 0x0004917C
		public bool IsServiceOnThisServer(Uri service)
		{
			ArgumentValidator.ThrowIfNull("service", service);
			return LocalServerCache.LocalServerFqdn.Equals(service.Host, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004AF9C File Offset: 0x0004919C
		private BackEndServer LocateServer(MailboxServerLocator locator)
		{
			BackEndServer result;
			using (new StopwatchPerformanceTracker("LocalForestPhotoServiceLocatorLocateServer", this.perfLogger))
			{
				using (new ADPerformanceTracker("LocalForestPhotoServiceLocatorLocateServer", this.perfLogger))
				{
					IAsyncResult asyncResult = locator.BeginGetServer(null, null);
					if (!asyncResult.AsyncWaitHandle.WaitOne(LocalForestPhotoServiceLocatorUsingMailboxServerLocator.LocateServerTimeout))
					{
						this.tracer.TraceError((long)this.GetHashCode(), "SERVICE LOCATOR[MAILBOXSERVERLOCATOR]: timed out waiting for a response from locator.");
						throw new TimeoutException();
					}
					result = locator.EndGetServer(asyncResult);
				}
			}
			return result;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004B04C File Offset: 0x0004924C
		private Uri GetPhotoServiceUri(BackEndServer server)
		{
			Uri backEndWebServicesUrl;
			using (new StopwatchPerformanceTracker("LocalForestPhotoServiceLocatorGetPhotoServiceUri", this.perfLogger))
			{
				using (new ADPerformanceTracker("LocalForestPhotoServiceLocatorGetPhotoServiceUri", this.perfLogger))
				{
					backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(server);
				}
			}
			return backEndWebServicesUrl;
		}

		// Token: 0x0400097C RID: 2428
		private static readonly TimeSpan LocateServerTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400097D RID: 2429
		private readonly IPerformanceDataLogger perfLogger;

		// Token: 0x0400097E RID: 2430
		private readonly ITracer tracer;

		// Token: 0x0400097F RID: 2431
		private ADUser lastLocatedTarget;

		// Token: 0x04000980 RID: 2432
		private Uri lastLocatedUri;
	}
}
