using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000010 RID: 16
	internal class RemoteDomainServerDiscovery : WorkItem<RemoteDomainServerDiscovery.RemoteDomainServerDiscoveryResult>
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000526C File Offset: 0x0000346C
		internal RemoteDomainServerDiscovery(ADObjectId domainId)
		{
			if (domainId == null)
			{
				throw new ArgumentNullException("domainId");
			}
			this.domainFqdn = domainId.ToCanonicalName();
			base.Data = new RemoteDomainServerDiscovery.RemoteDomainServerDiscoveryResult(domainId);
			this.id = string.Format("{0}-{1}-{2}-{3}", new object[]
			{
				base.GetType().Name,
				this.domainFqdn,
				DateTime.UtcNow,
				this.GetHashCode()
			});
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000052EF File Offset: 0x000034EF
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000052F7 File Offset: 0x000034F7
		public override TimeSpan TimeoutInterval
		{
			get
			{
				return ConfigurationData.Instance.RemoteDomainSingleServerDiscoveryTimeout;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005303 File Offset: 0x00003503
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000530B File Offset: 0x0000350B
		private protected CancellationToken CancellationToken { protected get; private set; }

		// Token: 0x06000081 RID: 129 RVA: 0x00005330 File Offset: 0x00003530
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ExTraceGlobals.DiscoveryTracer.TraceFunction<string>((long)this.GetHashCode(), "{0} - Entering Discover", this.domainFqdn);
			this.CancellationToken = cancellationToken;
			StringCollection stringCollection = null;
			try
			{
				stringCollection = NativeHelpers.FindAllDomainControllers(this.domainFqdn, null);
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.DiscoveryTracer.TraceError<string, string>((long)this.GetHashCode(), "{0} - Error trying to get DCs. Error {1}", this.domainFqdn, ex.ToString());
				Task.Factory.StartNew<Task>(() => DnsTroubleshooter.DiagnoseDnsProblemForDomain(this.domainFqdn), cancellationToken, TaskCreationOptions.AttachedToParent, TaskScheduler.Current).Unwrap();
				throw;
			}
			if (ExTraceGlobals.DiscoveryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in stringCollection)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(",");
				}
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - A DC will be selected from the following pool of DCs {1}", this.domainFqdn, stringBuilder.ToString());
			}
			Queue<string> queue = new Queue<string>(stringCollection.Count);
			foreach (string text in stringCollection)
			{
				if (ConfigurationData.Instance.ExcludedDC != null && ConfigurationData.Instance.ExcludedDC.Length > 0 && ConfigurationData.Instance.ExcludedDC.Contains(text, StringComparer.OrdinalIgnoreCase))
				{
					ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Excluding DC {1}", this.domainFqdn, text);
				}
				else
				{
					queue.Enqueue(text);
				}
			}
			if (queue.Count > 0)
			{
				Task.Factory.Iterate(this.FindSuitableDomainController(queue), this.CancellationToken, TaskCreationOptions.AttachedToParent, Task.Factory.GetTargetScheduler());
				ExTraceGlobals.DiscoveryTracer.TraceFunction<string>((long)this.GetHashCode(), "{0} - Exiting Discover", this.domainFqdn);
				return;
			}
			ExTraceGlobals.DiscoveryTracer.TraceError<string>((long)this.GetHashCode(), "{0} - No DCs found", this.domainFqdn);
			Task.Factory.StartNew<Task>(() => DnsTroubleshooter.DiagnoseDnsProblemForDomain(this.domainFqdn), cancellationToken, TaskCreationOptions.AttachedToParent, TaskScheduler.Current).Unwrap();
			this.LogFailureToGetDCFromDomain();
			throw new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableDCInDomain(this.domainFqdn, LocalizedString.Empty));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000594C File Offset: 0x00003B4C
		private IEnumerable<Task> FindSuitableDomainController(Queue<string> domainControllersPool)
		{
			LocalizedString suitabilityErrors = LocalizedString.Empty;
			foreach (string dc in domainControllersPool)
			{
				SuitabilityVerifier.SuitabilityCheckContext context = new SuitabilityVerifier.SuitabilityCheckContext(dc, false, ConfigurationData.Instance.AllowPreW2KSP3DC, ConfigurationData.Instance.IsPDCCheckEnabled, ConfigurationData.Instance.DisableNetLogonCheck);
				Task task = SuitabilityVerifier.PerformServerSuitabilities(context, this.CancellationToken, false);
				yield return task;
				if (task.IsFaulted)
				{
					AggregateException exception = task.Exception;
					if (exception != null)
					{
						Exception baseException = exception.GetBaseException();
						ExTraceGlobals.DiscoveryTracer.TraceError<string, string, string>((long)this.GetHashCode(), "{0} - Suitability check for DC {1}. Error {2}", this.domainFqdn, context.ServerFqdn, baseException.Message);
						suitabilityErrors = DirectoryStrings.AppendLocalizedStrings(suitabilityErrors, baseException.Message);
					}
				}
				if (context.SuitabilityResult.IsSuitable(ADServerRole.DomainController))
				{
					ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - DC suitable. {2}", this.domainFqdn, context.ServerFqdn);
					suitabilityErrors = LocalizedString.Empty;
					base.Data.ServerInfo = new ServerInfo(context.ServerFqdn, NativeHelpers.CanonicalNameFromDistinguishedName(context.SuitabilityResult.RootNC), 389)
					{
						ConfigNC = context.SuitabilityResult.ConfigNC,
						WritableNC = context.SuitabilityResult.WritableNC,
						SchemaNC = context.SuitabilityResult.SchemaNC,
						RootDomainNC = context.SuitabilityResult.RootNC
					};
					yield break;
				}
			}
			this.LogFailureToGetDCFromDomain();
			throw new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableDCInDomain(this.domainFqdn, suitabilityErrors));
			yield break;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005970 File Offset: 0x00003B70
		private void LogFailureToGetDCFromDomain()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN_FAILED, "FindDcInRemoteForest" + this.domainFqdn, new object[]
			{
				this.domainFqdn
			});
		}

		// Token: 0x04000038 RID: 56
		private readonly string domainFqdn;

		// Token: 0x04000039 RID: 57
		private readonly string id;

		// Token: 0x02000011 RID: 17
		internal class RemoteDomainServerDiscoveryResult
		{
			// Token: 0x06000086 RID: 134 RVA: 0x000059A9 File Offset: 0x00003BA9
			internal RemoteDomainServerDiscoveryResult(ADObjectId domainId)
			{
				this.DomainId = domainId;
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000087 RID: 135 RVA: 0x000059B8 File Offset: 0x00003BB8
			// (set) Token: 0x06000088 RID: 136 RVA: 0x000059C0 File Offset: 0x00003BC0
			public ADObjectId DomainId { get; private set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000089 RID: 137 RVA: 0x000059C9 File Offset: 0x00003BC9
			// (set) Token: 0x0600008A RID: 138 RVA: 0x000059D1 File Offset: 0x00003BD1
			public ServerInfo ServerInfo { get; set; }

			// Token: 0x0600008B RID: 139 RVA: 0x000059DA File Offset: 0x00003BDA
			public override string ToString()
			{
				return string.Format("DomainId: {0}  ServerInfo: {1}", this.DomainId.ToString(), (this.ServerInfo == null) ? "<NULL>" : this.ServerInfo.ToString());
			}
		}
	}
}
