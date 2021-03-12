using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000289 RID: 649
	internal class AcceptedDomainTable : AcceptedDomainMap
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x00072652 File Offset: 0x00070852
		public AcceptedDomainTable(List<string> internalDomains, AcceptedDomainEntry defaultDomain, List<AcceptedDomainEntry> entries) : base(entries)
		{
			this.DefaultDomain = defaultDomain;
			this.edgeToBHDomains = new ReadOnlyCollection<string>(internalDomains);
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0007266E File Offset: 0x0007086E
		public ReadOnlyCollection<string> EdgeToBHDomains
		{
			get
			{
				return this.edgeToBHDomains;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00072676 File Offset: 0x00070876
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0007267E File Offset: 0x0007087E
		public AcceptedDomainEntry DefaultDomain { get; private set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00072687 File Offset: 0x00070887
		public string DefaultDomainName
		{
			get
			{
				return AcceptedDomainTable.GetDomainName(this.DefaultDomain);
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00072694 File Offset: 0x00070894
		private static string GetDomainName(AcceptedDomainEntry entry)
		{
			if (entry != null)
			{
				return entry.DomainName.Domain;
			}
			return null;
		}

		// Token: 0x04000D26 RID: 3366
		public const string TransportSettingsContainerName = "Transport Settings";

		// Token: 0x04000D27 RID: 3367
		public const string AcceptedDomainsContainerName = "Accepted Domains";

		// Token: 0x04000D28 RID: 3368
		private static readonly ExEventLog Log = new ExEventLog(ExTraceGlobals.ConfigurationTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000D29 RID: 3369
		private readonly ReadOnlyCollection<string> edgeToBHDomains;

		// Token: 0x0200028E RID: 654
		public class Builder : ConfigurationLoader<AcceptedDomainTable, AcceptedDomainTable.Builder>.SimpleBuilder<AcceptedDomain>
		{
			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00072DAB File Offset: 0x00070FAB
			// (set) Token: 0x06001C0F RID: 7183 RVA: 0x00072DB3 File Offset: 0x00070FB3
			public bool IsBridgehead
			{
				get
				{
					return this.bridgehead;
				}
				set
				{
					this.bridgehead = value;
				}
			}

			// Token: 0x06001C10 RID: 7184 RVA: 0x00072DBC File Offset: 0x00070FBC
			public static int CreateAcceptedDomainEntries(IEnumerable<AcceptedDomain> domains, out List<AcceptedDomainEntry> entries, out AcceptedDomainEntry defaultDomain, out List<string> internalDomains)
			{
				int num = 0;
				entries = new List<AcceptedDomainEntry>();
				defaultDomain = null;
				internalDomains = new List<string>();
				if (domains != null)
				{
					foreach (AcceptedDomain acceptedDomain in domains)
					{
						if (acceptedDomain.DomainName == null)
						{
							string text = string.Format("Accepted domain name is null for the Distinguished Name '{0}'.", acceptedDomain.DistinguishedName ?? "not available");
							ExTraceGlobals.ConfigurationTracer.TraceError(0L, text);
							AcceptedDomainTable.Log.LogEvent(TransportEventLogConstants.Tuple_InvalidAcceptedDomain, null, new object[]
							{
								acceptedDomain.DistinguishedName
							});
							EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "InvalidAcceptedDomain", null, text, ResultSeverityLevel.Warning, false);
						}
						else
						{
							try
							{
								AcceptedDomainEntry acceptedDomainEntry = new AcceptedDomainEntry(acceptedDomain, acceptedDomain.OrganizationId);
								entries.Add(acceptedDomainEntry);
								if (acceptedDomainEntry.IsDefault)
								{
									defaultDomain = acceptedDomainEntry;
								}
								if (acceptedDomainEntry.IsInternal)
								{
									internalDomains.Add(acceptedDomainEntry.DomainName.Domain);
								}
								num += acceptedDomainEntry.EstimatedSize;
							}
							catch (ExchangeDataException ex)
							{
								ExTraceGlobals.ConfigurationTracer.TraceError<SmtpDomainWithSubdomains, ExchangeDataException>(0L, "Entry for {0} is invalid {1}", acceptedDomain.DomainName, ex);
								AcceptedDomainTable.Log.LogEvent(TransportEventLogConstants.Tuple_RejectedAcceptedDomain, acceptedDomain.DomainName.ToString(), new object[]
								{
									acceptedDomain.DomainName,
									ex
								});
							}
						}
					}
				}
				return num;
			}

			// Token: 0x06001C11 RID: 7185 RVA: 0x00072F50 File Offset: 0x00071150
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				base.RootId = session.GetOrgContainerId().GetChildId("Transport Settings").GetChildId("Accepted Domains");
				base.LoadData(session, QueryScope.OneLevel);
			}

			// Token: 0x06001C12 RID: 7186 RVA: 0x00072F7C File Offset: 0x0007117C
			protected override AcceptedDomainTable BuildCache(List<AcceptedDomain> domains)
			{
				List<AcceptedDomainEntry> entries;
				AcceptedDomainEntry acceptedDomainEntry;
				List<string> internalDomains;
				AcceptedDomainTable.Builder.CreateAcceptedDomainEntries(domains, out entries, out acceptedDomainEntry, out internalDomains);
				if (this.IsBridgehead && (acceptedDomainEntry == null || acceptedDomainEntry.DomainName == null || acceptedDomainEntry.DomainName.Equals(SmtpDomainWithSubdomains.StarDomain)))
				{
					AcceptedDomainTable.Log.LogEvent(TransportEventLogConstants.Tuple_DefaultAuthoritativeDomainInvalid, null, new object[0]);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, "There is no default authoritative domain or the domain name is empty.", ResultSeverityLevel.Warning, false);
					return null;
				}
				return new AcceptedDomainTable(internalDomains, acceptedDomainEntry, entries);
			}

			// Token: 0x06001C13 RID: 7187 RVA: 0x00072FF8 File Offset: 0x000711F8
			protected override ADOperationResult TryRegisterChangeNotification<TConfigObject>(Func<ADObjectId> rootIdGetter, out ADNotificationRequestCookie cookie)
			{
				return TransportADNotificationAdapter.TryRegisterNotifications(new Func<ADObjectId>(ConfigurationLoader<AcceptedDomainTable, AcceptedDomainTable.Builder>.Builder.GetFirstOrgContainerId), new ADNotificationCallback(base.Reload), new TransportADNotificationAdapter.TransportADNotificationRegister(TransportADNotificationAdapter.Instance.RegisterForAcceptedDomainNotifications), 3, out cookie);
			}

			// Token: 0x04000D3B RID: 3387
			private bool bridgehead;
		}
	}
}
