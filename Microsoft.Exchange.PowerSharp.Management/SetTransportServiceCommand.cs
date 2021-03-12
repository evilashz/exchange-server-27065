using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000672 RID: 1650
	public class SetTransportServiceCommand : SyntheticCommandWithPipelineInputNoOutput<TransportServer>
	{
		// Token: 0x0600572F RID: 22319 RVA: 0x00088CA9 File Offset: 0x00086EA9
		private SetTransportServiceCommand() : base("Set-TransportService")
		{
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x00088CB6 File Offset: 0x00086EB6
		public SetTransportServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x00088CC5 File Offset: 0x00086EC5
		public virtual SetTransportServiceCommand SetParameters(SetTransportServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x00088CCF File Offset: 0x00086ECF
		public virtual SetTransportServiceCommand SetParameters(SetTransportServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000673 RID: 1651
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003604 RID: 13828
			// (set) Token: 0x06005733 RID: 22323 RVA: 0x00088CD9 File Offset: 0x00086ED9
			public virtual EnhancedTimeSpan QueueLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxAge"] = value;
				}
			}

			// Token: 0x17003605 RID: 13829
			// (set) Token: 0x06005734 RID: 22324 RVA: 0x00088CF1 File Offset: 0x00086EF1
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003606 RID: 13830
			// (set) Token: 0x06005735 RID: 22325 RVA: 0x00088D09 File Offset: 0x00086F09
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003607 RID: 13831
			// (set) Token: 0x06005736 RID: 22326 RVA: 0x00088D21 File Offset: 0x00086F21
			public virtual LocalLongFullPath QueueLogPath
			{
				set
				{
					base.PowerSharpParameters["QueueLogPath"] = value;
				}
			}

			// Token: 0x17003608 RID: 13832
			// (set) Token: 0x06005737 RID: 22327 RVA: 0x00088D34 File Offset: 0x00086F34
			public virtual EnhancedTimeSpan WlmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxAge"] = value;
				}
			}

			// Token: 0x17003609 RID: 13833
			// (set) Token: 0x06005738 RID: 22328 RVA: 0x00088D4C File Offset: 0x00086F4C
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700360A RID: 13834
			// (set) Token: 0x06005739 RID: 22329 RVA: 0x00088D64 File Offset: 0x00086F64
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700360B RID: 13835
			// (set) Token: 0x0600573A RID: 22330 RVA: 0x00088D7C File Offset: 0x00086F7C
			public virtual LocalLongFullPath WlmLogPath
			{
				set
				{
					base.PowerSharpParameters["WlmLogPath"] = value;
				}
			}

			// Token: 0x1700360C RID: 13836
			// (set) Token: 0x0600573B RID: 22331 RVA: 0x00088D8F File Offset: 0x00086F8F
			public virtual EnhancedTimeSpan AgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxAge"] = value;
				}
			}

			// Token: 0x1700360D RID: 13837
			// (set) Token: 0x0600573C RID: 22332 RVA: 0x00088DA7 File Offset: 0x00086FA7
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700360E RID: 13838
			// (set) Token: 0x0600573D RID: 22333 RVA: 0x00088DBF File Offset: 0x00086FBF
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700360F RID: 13839
			// (set) Token: 0x0600573E RID: 22334 RVA: 0x00088DD7 File Offset: 0x00086FD7
			public virtual LocalLongFullPath AgentLogPath
			{
				set
				{
					base.PowerSharpParameters["AgentLogPath"] = value;
				}
			}

			// Token: 0x17003610 RID: 13840
			// (set) Token: 0x0600573F RID: 22335 RVA: 0x00088DEA File Offset: 0x00086FEA
			public virtual bool AgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003611 RID: 13841
			// (set) Token: 0x06005740 RID: 22336 RVA: 0x00088E02 File Offset: 0x00087002
			public virtual EnhancedTimeSpan FlowControlLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxAge"] = value;
				}
			}

			// Token: 0x17003612 RID: 13842
			// (set) Token: 0x06005741 RID: 22337 RVA: 0x00088E1A File Offset: 0x0008701A
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003613 RID: 13843
			// (set) Token: 0x06005742 RID: 22338 RVA: 0x00088E32 File Offset: 0x00087032
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003614 RID: 13844
			// (set) Token: 0x06005743 RID: 22339 RVA: 0x00088E4A File Offset: 0x0008704A
			public virtual LocalLongFullPath FlowControlLogPath
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogPath"] = value;
				}
			}

			// Token: 0x17003615 RID: 13845
			// (set) Token: 0x06005744 RID: 22340 RVA: 0x00088E5D File Offset: 0x0008705D
			public virtual bool FlowControlLogEnabled
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogEnabled"] = value;
				}
			}

			// Token: 0x17003616 RID: 13846
			// (set) Token: 0x06005745 RID: 22341 RVA: 0x00088E75 File Offset: 0x00087075
			public virtual EnhancedTimeSpan ProcessingSchedulerLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxAge"] = value;
				}
			}

			// Token: 0x17003617 RID: 13847
			// (set) Token: 0x06005746 RID: 22342 RVA: 0x00088E8D File Offset: 0x0008708D
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003618 RID: 13848
			// (set) Token: 0x06005747 RID: 22343 RVA: 0x00088EA5 File Offset: 0x000870A5
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003619 RID: 13849
			// (set) Token: 0x06005748 RID: 22344 RVA: 0x00088EBD File Offset: 0x000870BD
			public virtual LocalLongFullPath ProcessingSchedulerLogPath
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogPath"] = value;
				}
			}

			// Token: 0x1700361A RID: 13850
			// (set) Token: 0x06005749 RID: 22345 RVA: 0x00088ED0 File Offset: 0x000870D0
			public virtual bool ProcessingSchedulerLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogEnabled"] = value;
				}
			}

			// Token: 0x1700361B RID: 13851
			// (set) Token: 0x0600574A RID: 22346 RVA: 0x00088EE8 File Offset: 0x000870E8
			public virtual EnhancedTimeSpan ResourceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxAge"] = value;
				}
			}

			// Token: 0x1700361C RID: 13852
			// (set) Token: 0x0600574B RID: 22347 RVA: 0x00088F00 File Offset: 0x00087100
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700361D RID: 13853
			// (set) Token: 0x0600574C RID: 22348 RVA: 0x00088F18 File Offset: 0x00087118
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700361E RID: 13854
			// (set) Token: 0x0600574D RID: 22349 RVA: 0x00088F30 File Offset: 0x00087130
			public virtual LocalLongFullPath ResourceLogPath
			{
				set
				{
					base.PowerSharpParameters["ResourceLogPath"] = value;
				}
			}

			// Token: 0x1700361F RID: 13855
			// (set) Token: 0x0600574E RID: 22350 RVA: 0x00088F43 File Offset: 0x00087143
			public virtual bool ResourceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ResourceLogEnabled"] = value;
				}
			}

			// Token: 0x17003620 RID: 13856
			// (set) Token: 0x0600574F RID: 22351 RVA: 0x00088F5B File Offset: 0x0008715B
			public virtual EnhancedTimeSpan DnsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003621 RID: 13857
			// (set) Token: 0x06005750 RID: 22352 RVA: 0x00088F73 File Offset: 0x00087173
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003622 RID: 13858
			// (set) Token: 0x06005751 RID: 22353 RVA: 0x00088F8B File Offset: 0x0008718B
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003623 RID: 13859
			// (set) Token: 0x06005752 RID: 22354 RVA: 0x00088FA3 File Offset: 0x000871A3
			public virtual LocalLongFullPath DnsLogPath
			{
				set
				{
					base.PowerSharpParameters["DnsLogPath"] = value;
				}
			}

			// Token: 0x17003624 RID: 13860
			// (set) Token: 0x06005753 RID: 22355 RVA: 0x00088FB6 File Offset: 0x000871B6
			public virtual bool DnsLogEnabled
			{
				set
				{
					base.PowerSharpParameters["DnsLogEnabled"] = value;
				}
			}

			// Token: 0x17003625 RID: 13861
			// (set) Token: 0x06005754 RID: 22356 RVA: 0x00088FCE File Offset: 0x000871CE
			public virtual EnhancedTimeSpan JournalLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxAge"] = value;
				}
			}

			// Token: 0x17003626 RID: 13862
			// (set) Token: 0x06005755 RID: 22357 RVA: 0x00088FE6 File Offset: 0x000871E6
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003627 RID: 13863
			// (set) Token: 0x06005756 RID: 22358 RVA: 0x00088FFE File Offset: 0x000871FE
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003628 RID: 13864
			// (set) Token: 0x06005757 RID: 22359 RVA: 0x00089016 File Offset: 0x00087216
			public virtual LocalLongFullPath JournalLogPath
			{
				set
				{
					base.PowerSharpParameters["JournalLogPath"] = value;
				}
			}

			// Token: 0x17003629 RID: 13865
			// (set) Token: 0x06005758 RID: 22360 RVA: 0x00089029 File Offset: 0x00087229
			public virtual bool JournalLogEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalLogEnabled"] = value;
				}
			}

			// Token: 0x1700362A RID: 13866
			// (set) Token: 0x06005759 RID: 22361 RVA: 0x00089041 File Offset: 0x00087241
			public virtual EnhancedTimeSpan TransportMaintenanceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxAge"] = value;
				}
			}

			// Token: 0x1700362B RID: 13867
			// (set) Token: 0x0600575A RID: 22362 RVA: 0x00089059 File Offset: 0x00087259
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700362C RID: 13868
			// (set) Token: 0x0600575B RID: 22363 RVA: 0x00089071 File Offset: 0x00087271
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700362D RID: 13869
			// (set) Token: 0x0600575C RID: 22364 RVA: 0x00089089 File Offset: 0x00087289
			public virtual LocalLongFullPath TransportMaintenanceLogPath
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogPath"] = value;
				}
			}

			// Token: 0x1700362E RID: 13870
			// (set) Token: 0x0600575D RID: 22365 RVA: 0x0008909C File Offset: 0x0008729C
			public virtual bool TransportMaintenanceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogEnabled"] = value;
				}
			}

			// Token: 0x1700362F RID: 13871
			// (set) Token: 0x0600575E RID: 22366 RVA: 0x000890B4 File Offset: 0x000872B4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003630 RID: 13872
			// (set) Token: 0x0600575F RID: 22367 RVA: 0x000890C7 File Offset: 0x000872C7
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x17003631 RID: 13873
			// (set) Token: 0x06005760 RID: 22368 RVA: 0x000890DF File Offset: 0x000872DF
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x17003632 RID: 13874
			// (set) Token: 0x06005761 RID: 22369 RVA: 0x000890F7 File Offset: 0x000872F7
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x17003633 RID: 13875
			// (set) Token: 0x06005762 RID: 22370 RVA: 0x0008910F File Offset: 0x0008730F
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003634 RID: 13876
			// (set) Token: 0x06005763 RID: 22371 RVA: 0x00089127 File Offset: 0x00087327
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003635 RID: 13877
			// (set) Token: 0x06005764 RID: 22372 RVA: 0x0008913F File Offset: 0x0008733F
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x17003636 RID: 13878
			// (set) Token: 0x06005765 RID: 22373 RVA: 0x00089152 File Offset: 0x00087352
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x17003637 RID: 13879
			// (set) Token: 0x06005766 RID: 22374 RVA: 0x0008916A File Offset: 0x0008736A
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x17003638 RID: 13880
			// (set) Token: 0x06005767 RID: 22375 RVA: 0x00089182 File Offset: 0x00087382
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003639 RID: 13881
			// (set) Token: 0x06005768 RID: 22376 RVA: 0x0008919A File Offset: 0x0008739A
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x1700363A RID: 13882
			// (set) Token: 0x06005769 RID: 22377 RVA: 0x000891B2 File Offset: 0x000873B2
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x1700363B RID: 13883
			// (set) Token: 0x0600576A RID: 22378 RVA: 0x000891C5 File Offset: 0x000873C5
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x1700363C RID: 13884
			// (set) Token: 0x0600576B RID: 22379 RVA: 0x000891D8 File Offset: 0x000873D8
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700363D RID: 13885
			// (set) Token: 0x0600576C RID: 22380 RVA: 0x000891F0 File Offset: 0x000873F0
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x1700363E RID: 13886
			// (set) Token: 0x0600576D RID: 22381 RVA: 0x00089208 File Offset: 0x00087408
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x1700363F RID: 13887
			// (set) Token: 0x0600576E RID: 22382 RVA: 0x00089220 File Offset: 0x00087420
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x17003640 RID: 13888
			// (set) Token: 0x0600576F RID: 22383 RVA: 0x00089233 File Offset: 0x00087433
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x17003641 RID: 13889
			// (set) Token: 0x06005770 RID: 22384 RVA: 0x0008924B File Offset: 0x0008744B
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x17003642 RID: 13890
			// (set) Token: 0x06005771 RID: 22385 RVA: 0x00089263 File Offset: 0x00087463
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x17003643 RID: 13891
			// (set) Token: 0x06005772 RID: 22386 RVA: 0x0008927B File Offset: 0x0008747B
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x17003644 RID: 13892
			// (set) Token: 0x06005773 RID: 22387 RVA: 0x00089293 File Offset: 0x00087493
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x17003645 RID: 13893
			// (set) Token: 0x06005774 RID: 22388 RVA: 0x000892AB File Offset: 0x000874AB
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x17003646 RID: 13894
			// (set) Token: 0x06005775 RID: 22389 RVA: 0x000892C3 File Offset: 0x000874C3
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x17003647 RID: 13895
			// (set) Token: 0x06005776 RID: 22390 RVA: 0x000892DB File Offset: 0x000874DB
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x17003648 RID: 13896
			// (set) Token: 0x06005777 RID: 22391 RVA: 0x000892F3 File Offset: 0x000874F3
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x17003649 RID: 13897
			// (set) Token: 0x06005778 RID: 22392 RVA: 0x0008930B File Offset: 0x0008750B
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700364A RID: 13898
			// (set) Token: 0x06005779 RID: 22393 RVA: 0x00089323 File Offset: 0x00087523
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700364B RID: 13899
			// (set) Token: 0x0600577A RID: 22394 RVA: 0x0008933B File Offset: 0x0008753B
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x1700364C RID: 13900
			// (set) Token: 0x0600577B RID: 22395 RVA: 0x0008934E File Offset: 0x0008754E
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x1700364D RID: 13901
			// (set) Token: 0x0600577C RID: 22396 RVA: 0x00089366 File Offset: 0x00087566
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x1700364E RID: 13902
			// (set) Token: 0x0600577D RID: 22397 RVA: 0x0008937E File Offset: 0x0008757E
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700364F RID: 13903
			// (set) Token: 0x0600577E RID: 22398 RVA: 0x00089396 File Offset: 0x00087596
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003650 RID: 13904
			// (set) Token: 0x0600577F RID: 22399 RVA: 0x000893AE File Offset: 0x000875AE
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x17003651 RID: 13905
			// (set) Token: 0x06005780 RID: 22400 RVA: 0x000893C1 File Offset: 0x000875C1
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003652 RID: 13906
			// (set) Token: 0x06005781 RID: 22401 RVA: 0x000893D9 File Offset: 0x000875D9
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003653 RID: 13907
			// (set) Token: 0x06005782 RID: 22402 RVA: 0x000893F1 File Offset: 0x000875F1
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003654 RID: 13908
			// (set) Token: 0x06005783 RID: 22403 RVA: 0x00089409 File Offset: 0x00087609
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003655 RID: 13909
			// (set) Token: 0x06005784 RID: 22404 RVA: 0x0008941C File Offset: 0x0008761C
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003656 RID: 13910
			// (set) Token: 0x06005785 RID: 22405 RVA: 0x00089434 File Offset: 0x00087634
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003657 RID: 13911
			// (set) Token: 0x06005786 RID: 22406 RVA: 0x0008944C File Offset: 0x0008764C
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003658 RID: 13912
			// (set) Token: 0x06005787 RID: 22407 RVA: 0x00089464 File Offset: 0x00087664
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003659 RID: 13913
			// (set) Token: 0x06005788 RID: 22408 RVA: 0x00089477 File Offset: 0x00087677
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x1700365A RID: 13914
			// (set) Token: 0x06005789 RID: 22409 RVA: 0x0008948F File Offset: 0x0008768F
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x1700365B RID: 13915
			// (set) Token: 0x0600578A RID: 22410 RVA: 0x000894A7 File Offset: 0x000876A7
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700365C RID: 13916
			// (set) Token: 0x0600578B RID: 22411 RVA: 0x000894BF File Offset: 0x000876BF
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x1700365D RID: 13917
			// (set) Token: 0x0600578C RID: 22412 RVA: 0x000894D7 File Offset: 0x000876D7
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x1700365E RID: 13918
			// (set) Token: 0x0600578D RID: 22413 RVA: 0x000894EF File Offset: 0x000876EF
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x1700365F RID: 13919
			// (set) Token: 0x0600578E RID: 22414 RVA: 0x00089507 File Offset: 0x00087707
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x17003660 RID: 13920
			// (set) Token: 0x0600578F RID: 22415 RVA: 0x0008951A File Offset: 0x0008771A
			public virtual bool PipelineTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingEnabled"] = value;
				}
			}

			// Token: 0x17003661 RID: 13921
			// (set) Token: 0x06005790 RID: 22416 RVA: 0x00089532 File Offset: 0x00087732
			public virtual bool ContentConversionTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["ContentConversionTracingEnabled"] = value;
				}
			}

			// Token: 0x17003662 RID: 13922
			// (set) Token: 0x06005791 RID: 22417 RVA: 0x0008954A File Offset: 0x0008774A
			public virtual LocalLongFullPath PipelineTracingPath
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingPath"] = value;
				}
			}

			// Token: 0x17003663 RID: 13923
			// (set) Token: 0x06005792 RID: 22418 RVA: 0x0008955D File Offset: 0x0008775D
			public virtual SmtpAddress? PipelineTracingSenderAddress
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingSenderAddress"] = value;
				}
			}

			// Token: 0x17003664 RID: 13924
			// (set) Token: 0x06005793 RID: 22419 RVA: 0x00089575 File Offset: 0x00087775
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003665 RID: 13925
			// (set) Token: 0x06005794 RID: 22420 RVA: 0x0008958D File Offset: 0x0008778D
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x17003666 RID: 13926
			// (set) Token: 0x06005795 RID: 22421 RVA: 0x000895A5 File Offset: 0x000877A5
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x17003667 RID: 13927
			// (set) Token: 0x06005796 RID: 22422 RVA: 0x000895BD File Offset: 0x000877BD
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003668 RID: 13928
			// (set) Token: 0x06005797 RID: 22423 RVA: 0x000895D5 File Offset: 0x000877D5
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003669 RID: 13929
			// (set) Token: 0x06005798 RID: 22424 RVA: 0x000895ED File Offset: 0x000877ED
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700366A RID: 13930
			// (set) Token: 0x06005799 RID: 22425 RVA: 0x00089605 File Offset: 0x00087805
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700366B RID: 13931
			// (set) Token: 0x0600579A RID: 22426 RVA: 0x00089618 File Offset: 0x00087818
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x1700366C RID: 13932
			// (set) Token: 0x0600579B RID: 22427 RVA: 0x00089630 File Offset: 0x00087830
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x1700366D RID: 13933
			// (set) Token: 0x0600579C RID: 22428 RVA: 0x00089643 File Offset: 0x00087843
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x1700366E RID: 13934
			// (set) Token: 0x0600579D RID: 22429 RVA: 0x00089656 File Offset: 0x00087856
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x1700366F RID: 13935
			// (set) Token: 0x0600579E RID: 22430 RVA: 0x0008966E File Offset: 0x0008786E
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003670 RID: 13936
			// (set) Token: 0x0600579F RID: 22431 RVA: 0x00089686 File Offset: 0x00087886
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x17003671 RID: 13937
			// (set) Token: 0x060057A0 RID: 22432 RVA: 0x00089699 File Offset: 0x00087899
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003672 RID: 13938
			// (set) Token: 0x060057A1 RID: 22433 RVA: 0x000896B1 File Offset: 0x000878B1
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003673 RID: 13939
			// (set) Token: 0x060057A2 RID: 22434 RVA: 0x000896C9 File Offset: 0x000878C9
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003674 RID: 13940
			// (set) Token: 0x060057A3 RID: 22435 RVA: 0x000896E1 File Offset: 0x000878E1
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003675 RID: 13941
			// (set) Token: 0x060057A4 RID: 22436 RVA: 0x000896F4 File Offset: 0x000878F4
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x17003676 RID: 13942
			// (set) Token: 0x060057A5 RID: 22437 RVA: 0x0008970C File Offset: 0x0008790C
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x17003677 RID: 13943
			// (set) Token: 0x060057A6 RID: 22438 RVA: 0x00089724 File Offset: 0x00087924
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x17003678 RID: 13944
			// (set) Token: 0x060057A7 RID: 22439 RVA: 0x0008973C File Offset: 0x0008793C
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x17003679 RID: 13945
			// (set) Token: 0x060057A8 RID: 22440 RVA: 0x00089754 File Offset: 0x00087954
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700367A RID: 13946
			// (set) Token: 0x060057A9 RID: 22441 RVA: 0x0008976C File Offset: 0x0008796C
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x1700367B RID: 13947
			// (set) Token: 0x060057AA RID: 22442 RVA: 0x00089784 File Offset: 0x00087984
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x1700367C RID: 13948
			// (set) Token: 0x060057AB RID: 22443 RVA: 0x0008979C File Offset: 0x0008799C
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x1700367D RID: 13949
			// (set) Token: 0x060057AC RID: 22444 RVA: 0x000897B4 File Offset: 0x000879B4
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x1700367E RID: 13950
			// (set) Token: 0x060057AD RID: 22445 RVA: 0x000897CC File Offset: 0x000879CC
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x1700367F RID: 13951
			// (set) Token: 0x060057AE RID: 22446 RVA: 0x000897DF File Offset: 0x000879DF
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x17003680 RID: 13952
			// (set) Token: 0x060057AF RID: 22447 RVA: 0x000897F7 File Offset: 0x000879F7
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x17003681 RID: 13953
			// (set) Token: 0x060057B0 RID: 22448 RVA: 0x0008980A File Offset: 0x00087A0A
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003682 RID: 13954
			// (set) Token: 0x060057B1 RID: 22449 RVA: 0x00089822 File Offset: 0x00087A22
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003683 RID: 13955
			// (set) Token: 0x060057B2 RID: 22450 RVA: 0x0008983A File Offset: 0x00087A3A
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003684 RID: 13956
			// (set) Token: 0x060057B3 RID: 22451 RVA: 0x00089852 File Offset: 0x00087A52
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003685 RID: 13957
			// (set) Token: 0x060057B4 RID: 22452 RVA: 0x0008986A File Offset: 0x00087A6A
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x17003686 RID: 13958
			// (set) Token: 0x060057B5 RID: 22453 RVA: 0x00089882 File Offset: 0x00087A82
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x17003687 RID: 13959
			// (set) Token: 0x060057B6 RID: 22454 RVA: 0x00089895 File Offset: 0x00087A95
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003688 RID: 13960
			// (set) Token: 0x060057B7 RID: 22455 RVA: 0x000898AD File Offset: 0x00087AAD
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x17003689 RID: 13961
			// (set) Token: 0x060057B8 RID: 22456 RVA: 0x000898C5 File Offset: 0x00087AC5
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700368A RID: 13962
			// (set) Token: 0x060057B9 RID: 22457 RVA: 0x000898DD File Offset: 0x00087ADD
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700368B RID: 13963
			// (set) Token: 0x060057BA RID: 22458 RVA: 0x000898F5 File Offset: 0x00087AF5
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x1700368C RID: 13964
			// (set) Token: 0x060057BB RID: 22459 RVA: 0x0008990D File Offset: 0x00087B0D
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x1700368D RID: 13965
			// (set) Token: 0x060057BC RID: 22460 RVA: 0x00089920 File Offset: 0x00087B20
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x1700368E RID: 13966
			// (set) Token: 0x060057BD RID: 22461 RVA: 0x00089938 File Offset: 0x00087B38
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700368F RID: 13967
			// (set) Token: 0x060057BE RID: 22462 RVA: 0x00089950 File Offset: 0x00087B50
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003690 RID: 13968
			// (set) Token: 0x060057BF RID: 22463 RVA: 0x00089968 File Offset: 0x00087B68
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003691 RID: 13969
			// (set) Token: 0x060057C0 RID: 22464 RVA: 0x00089980 File Offset: 0x00087B80
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x17003692 RID: 13970
			// (set) Token: 0x060057C1 RID: 22465 RVA: 0x00089998 File Offset: 0x00087B98
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x17003693 RID: 13971
			// (set) Token: 0x060057C2 RID: 22466 RVA: 0x000899B0 File Offset: 0x00087BB0
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x17003694 RID: 13972
			// (set) Token: 0x060057C3 RID: 22467 RVA: 0x000899C8 File Offset: 0x00087BC8
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x17003695 RID: 13973
			// (set) Token: 0x060057C4 RID: 22468 RVA: 0x000899E0 File Offset: 0x00087BE0
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x17003696 RID: 13974
			// (set) Token: 0x060057C5 RID: 22469 RVA: 0x000899F8 File Offset: 0x00087BF8
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x17003697 RID: 13975
			// (set) Token: 0x060057C6 RID: 22470 RVA: 0x00089A10 File Offset: 0x00087C10
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x17003698 RID: 13976
			// (set) Token: 0x060057C7 RID: 22471 RVA: 0x00089A28 File Offset: 0x00087C28
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x17003699 RID: 13977
			// (set) Token: 0x060057C8 RID: 22472 RVA: 0x00089A3B File Offset: 0x00087C3B
			public virtual bool UseDowngradedExchangeServerAuth
			{
				set
				{
					base.PowerSharpParameters["UseDowngradedExchangeServerAuth"] = value;
				}
			}

			// Token: 0x1700369A RID: 13978
			// (set) Token: 0x060057C9 RID: 22473 RVA: 0x00089A53 File Offset: 0x00087C53
			public virtual int IntraOrgConnectorSmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorSmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x1700369B RID: 13979
			// (set) Token: 0x060057CA RID: 22474 RVA: 0x00089A6B File Offset: 0x00087C6B
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x1700369C RID: 13980
			// (set) Token: 0x060057CB RID: 22475 RVA: 0x00089A83 File Offset: 0x00087C83
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x1700369D RID: 13981
			// (set) Token: 0x060057CC RID: 22476 RVA: 0x00089A9B File Offset: 0x00087C9B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700369E RID: 13982
			// (set) Token: 0x060057CD RID: 22477 RVA: 0x00089AB3 File Offset: 0x00087CB3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700369F RID: 13983
			// (set) Token: 0x060057CE RID: 22478 RVA: 0x00089ACB File Offset: 0x00087CCB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170036A0 RID: 13984
			// (set) Token: 0x060057CF RID: 22479 RVA: 0x00089AE3 File Offset: 0x00087CE3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170036A1 RID: 13985
			// (set) Token: 0x060057D0 RID: 22480 RVA: 0x00089AFB File Offset: 0x00087CFB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000674 RID: 1652
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170036A2 RID: 13986
			// (set) Token: 0x060057D2 RID: 22482 RVA: 0x00089B1B File Offset: 0x00087D1B
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170036A3 RID: 13987
			// (set) Token: 0x060057D3 RID: 22483 RVA: 0x00089B2E File Offset: 0x00087D2E
			public virtual EnhancedTimeSpan QueueLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxAge"] = value;
				}
			}

			// Token: 0x170036A4 RID: 13988
			// (set) Token: 0x060057D4 RID: 22484 RVA: 0x00089B46 File Offset: 0x00087D46
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036A5 RID: 13989
			// (set) Token: 0x060057D5 RID: 22485 RVA: 0x00089B5E File Offset: 0x00087D5E
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036A6 RID: 13990
			// (set) Token: 0x060057D6 RID: 22486 RVA: 0x00089B76 File Offset: 0x00087D76
			public virtual LocalLongFullPath QueueLogPath
			{
				set
				{
					base.PowerSharpParameters["QueueLogPath"] = value;
				}
			}

			// Token: 0x170036A7 RID: 13991
			// (set) Token: 0x060057D7 RID: 22487 RVA: 0x00089B89 File Offset: 0x00087D89
			public virtual EnhancedTimeSpan WlmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxAge"] = value;
				}
			}

			// Token: 0x170036A8 RID: 13992
			// (set) Token: 0x060057D8 RID: 22488 RVA: 0x00089BA1 File Offset: 0x00087DA1
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036A9 RID: 13993
			// (set) Token: 0x060057D9 RID: 22489 RVA: 0x00089BB9 File Offset: 0x00087DB9
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036AA RID: 13994
			// (set) Token: 0x060057DA RID: 22490 RVA: 0x00089BD1 File Offset: 0x00087DD1
			public virtual LocalLongFullPath WlmLogPath
			{
				set
				{
					base.PowerSharpParameters["WlmLogPath"] = value;
				}
			}

			// Token: 0x170036AB RID: 13995
			// (set) Token: 0x060057DB RID: 22491 RVA: 0x00089BE4 File Offset: 0x00087DE4
			public virtual EnhancedTimeSpan AgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxAge"] = value;
				}
			}

			// Token: 0x170036AC RID: 13996
			// (set) Token: 0x060057DC RID: 22492 RVA: 0x00089BFC File Offset: 0x00087DFC
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036AD RID: 13997
			// (set) Token: 0x060057DD RID: 22493 RVA: 0x00089C14 File Offset: 0x00087E14
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036AE RID: 13998
			// (set) Token: 0x060057DE RID: 22494 RVA: 0x00089C2C File Offset: 0x00087E2C
			public virtual LocalLongFullPath AgentLogPath
			{
				set
				{
					base.PowerSharpParameters["AgentLogPath"] = value;
				}
			}

			// Token: 0x170036AF RID: 13999
			// (set) Token: 0x060057DF RID: 22495 RVA: 0x00089C3F File Offset: 0x00087E3F
			public virtual bool AgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AgentLogEnabled"] = value;
				}
			}

			// Token: 0x170036B0 RID: 14000
			// (set) Token: 0x060057E0 RID: 22496 RVA: 0x00089C57 File Offset: 0x00087E57
			public virtual EnhancedTimeSpan FlowControlLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxAge"] = value;
				}
			}

			// Token: 0x170036B1 RID: 14001
			// (set) Token: 0x060057E1 RID: 22497 RVA: 0x00089C6F File Offset: 0x00087E6F
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036B2 RID: 14002
			// (set) Token: 0x060057E2 RID: 22498 RVA: 0x00089C87 File Offset: 0x00087E87
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036B3 RID: 14003
			// (set) Token: 0x060057E3 RID: 22499 RVA: 0x00089C9F File Offset: 0x00087E9F
			public virtual LocalLongFullPath FlowControlLogPath
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogPath"] = value;
				}
			}

			// Token: 0x170036B4 RID: 14004
			// (set) Token: 0x060057E4 RID: 22500 RVA: 0x00089CB2 File Offset: 0x00087EB2
			public virtual bool FlowControlLogEnabled
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogEnabled"] = value;
				}
			}

			// Token: 0x170036B5 RID: 14005
			// (set) Token: 0x060057E5 RID: 22501 RVA: 0x00089CCA File Offset: 0x00087ECA
			public virtual EnhancedTimeSpan ProcessingSchedulerLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxAge"] = value;
				}
			}

			// Token: 0x170036B6 RID: 14006
			// (set) Token: 0x060057E6 RID: 22502 RVA: 0x00089CE2 File Offset: 0x00087EE2
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036B7 RID: 14007
			// (set) Token: 0x060057E7 RID: 22503 RVA: 0x00089CFA File Offset: 0x00087EFA
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036B8 RID: 14008
			// (set) Token: 0x060057E8 RID: 22504 RVA: 0x00089D12 File Offset: 0x00087F12
			public virtual LocalLongFullPath ProcessingSchedulerLogPath
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogPath"] = value;
				}
			}

			// Token: 0x170036B9 RID: 14009
			// (set) Token: 0x060057E9 RID: 22505 RVA: 0x00089D25 File Offset: 0x00087F25
			public virtual bool ProcessingSchedulerLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogEnabled"] = value;
				}
			}

			// Token: 0x170036BA RID: 14010
			// (set) Token: 0x060057EA RID: 22506 RVA: 0x00089D3D File Offset: 0x00087F3D
			public virtual EnhancedTimeSpan ResourceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxAge"] = value;
				}
			}

			// Token: 0x170036BB RID: 14011
			// (set) Token: 0x060057EB RID: 22507 RVA: 0x00089D55 File Offset: 0x00087F55
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036BC RID: 14012
			// (set) Token: 0x060057EC RID: 22508 RVA: 0x00089D6D File Offset: 0x00087F6D
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036BD RID: 14013
			// (set) Token: 0x060057ED RID: 22509 RVA: 0x00089D85 File Offset: 0x00087F85
			public virtual LocalLongFullPath ResourceLogPath
			{
				set
				{
					base.PowerSharpParameters["ResourceLogPath"] = value;
				}
			}

			// Token: 0x170036BE RID: 14014
			// (set) Token: 0x060057EE RID: 22510 RVA: 0x00089D98 File Offset: 0x00087F98
			public virtual bool ResourceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ResourceLogEnabled"] = value;
				}
			}

			// Token: 0x170036BF RID: 14015
			// (set) Token: 0x060057EF RID: 22511 RVA: 0x00089DB0 File Offset: 0x00087FB0
			public virtual EnhancedTimeSpan DnsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxAge"] = value;
				}
			}

			// Token: 0x170036C0 RID: 14016
			// (set) Token: 0x060057F0 RID: 22512 RVA: 0x00089DC8 File Offset: 0x00087FC8
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036C1 RID: 14017
			// (set) Token: 0x060057F1 RID: 22513 RVA: 0x00089DE0 File Offset: 0x00087FE0
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036C2 RID: 14018
			// (set) Token: 0x060057F2 RID: 22514 RVA: 0x00089DF8 File Offset: 0x00087FF8
			public virtual LocalLongFullPath DnsLogPath
			{
				set
				{
					base.PowerSharpParameters["DnsLogPath"] = value;
				}
			}

			// Token: 0x170036C3 RID: 14019
			// (set) Token: 0x060057F3 RID: 22515 RVA: 0x00089E0B File Offset: 0x0008800B
			public virtual bool DnsLogEnabled
			{
				set
				{
					base.PowerSharpParameters["DnsLogEnabled"] = value;
				}
			}

			// Token: 0x170036C4 RID: 14020
			// (set) Token: 0x060057F4 RID: 22516 RVA: 0x00089E23 File Offset: 0x00088023
			public virtual EnhancedTimeSpan JournalLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxAge"] = value;
				}
			}

			// Token: 0x170036C5 RID: 14021
			// (set) Token: 0x060057F5 RID: 22517 RVA: 0x00089E3B File Offset: 0x0008803B
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036C6 RID: 14022
			// (set) Token: 0x060057F6 RID: 22518 RVA: 0x00089E53 File Offset: 0x00088053
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036C7 RID: 14023
			// (set) Token: 0x060057F7 RID: 22519 RVA: 0x00089E6B File Offset: 0x0008806B
			public virtual LocalLongFullPath JournalLogPath
			{
				set
				{
					base.PowerSharpParameters["JournalLogPath"] = value;
				}
			}

			// Token: 0x170036C8 RID: 14024
			// (set) Token: 0x060057F8 RID: 22520 RVA: 0x00089E7E File Offset: 0x0008807E
			public virtual bool JournalLogEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalLogEnabled"] = value;
				}
			}

			// Token: 0x170036C9 RID: 14025
			// (set) Token: 0x060057F9 RID: 22521 RVA: 0x00089E96 File Offset: 0x00088096
			public virtual EnhancedTimeSpan TransportMaintenanceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxAge"] = value;
				}
			}

			// Token: 0x170036CA RID: 14026
			// (set) Token: 0x060057FA RID: 22522 RVA: 0x00089EAE File Offset: 0x000880AE
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036CB RID: 14027
			// (set) Token: 0x060057FB RID: 22523 RVA: 0x00089EC6 File Offset: 0x000880C6
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036CC RID: 14028
			// (set) Token: 0x060057FC RID: 22524 RVA: 0x00089EDE File Offset: 0x000880DE
			public virtual LocalLongFullPath TransportMaintenanceLogPath
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogPath"] = value;
				}
			}

			// Token: 0x170036CD RID: 14029
			// (set) Token: 0x060057FD RID: 22525 RVA: 0x00089EF1 File Offset: 0x000880F1
			public virtual bool TransportMaintenanceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogEnabled"] = value;
				}
			}

			// Token: 0x170036CE RID: 14030
			// (set) Token: 0x060057FE RID: 22526 RVA: 0x00089F09 File Offset: 0x00088109
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170036CF RID: 14031
			// (set) Token: 0x060057FF RID: 22527 RVA: 0x00089F1C File Offset: 0x0008811C
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x170036D0 RID: 14032
			// (set) Token: 0x06005800 RID: 22528 RVA: 0x00089F34 File Offset: 0x00088134
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x170036D1 RID: 14033
			// (set) Token: 0x06005801 RID: 22529 RVA: 0x00089F4C File Offset: 0x0008814C
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x170036D2 RID: 14034
			// (set) Token: 0x06005802 RID: 22530 RVA: 0x00089F64 File Offset: 0x00088164
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036D3 RID: 14035
			// (set) Token: 0x06005803 RID: 22531 RVA: 0x00089F7C File Offset: 0x0008817C
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036D4 RID: 14036
			// (set) Token: 0x06005804 RID: 22532 RVA: 0x00089F94 File Offset: 0x00088194
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x170036D5 RID: 14037
			// (set) Token: 0x06005805 RID: 22533 RVA: 0x00089FA7 File Offset: 0x000881A7
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x170036D6 RID: 14038
			// (set) Token: 0x06005806 RID: 22534 RVA: 0x00089FBF File Offset: 0x000881BF
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170036D7 RID: 14039
			// (set) Token: 0x06005807 RID: 22535 RVA: 0x00089FD7 File Offset: 0x000881D7
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170036D8 RID: 14040
			// (set) Token: 0x06005808 RID: 22536 RVA: 0x00089FEF File Offset: 0x000881EF
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170036D9 RID: 14041
			// (set) Token: 0x06005809 RID: 22537 RVA: 0x0008A007 File Offset: 0x00088207
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x170036DA RID: 14042
			// (set) Token: 0x0600580A RID: 22538 RVA: 0x0008A01A File Offset: 0x0008821A
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x170036DB RID: 14043
			// (set) Token: 0x0600580B RID: 22539 RVA: 0x0008A02D File Offset: 0x0008822D
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170036DC RID: 14044
			// (set) Token: 0x0600580C RID: 22540 RVA: 0x0008A045 File Offset: 0x00088245
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170036DD RID: 14045
			// (set) Token: 0x0600580D RID: 22541 RVA: 0x0008A05D File Offset: 0x0008825D
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170036DE RID: 14046
			// (set) Token: 0x0600580E RID: 22542 RVA: 0x0008A075 File Offset: 0x00088275
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x170036DF RID: 14047
			// (set) Token: 0x0600580F RID: 22543 RVA: 0x0008A088 File Offset: 0x00088288
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x170036E0 RID: 14048
			// (set) Token: 0x06005810 RID: 22544 RVA: 0x0008A0A0 File Offset: 0x000882A0
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x170036E1 RID: 14049
			// (set) Token: 0x06005811 RID: 22545 RVA: 0x0008A0B8 File Offset: 0x000882B8
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x170036E2 RID: 14050
			// (set) Token: 0x06005812 RID: 22546 RVA: 0x0008A0D0 File Offset: 0x000882D0
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x170036E3 RID: 14051
			// (set) Token: 0x06005813 RID: 22547 RVA: 0x0008A0E8 File Offset: 0x000882E8
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x170036E4 RID: 14052
			// (set) Token: 0x06005814 RID: 22548 RVA: 0x0008A100 File Offset: 0x00088300
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x170036E5 RID: 14053
			// (set) Token: 0x06005815 RID: 22549 RVA: 0x0008A118 File Offset: 0x00088318
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x170036E6 RID: 14054
			// (set) Token: 0x06005816 RID: 22550 RVA: 0x0008A130 File Offset: 0x00088330
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x170036E7 RID: 14055
			// (set) Token: 0x06005817 RID: 22551 RVA: 0x0008A148 File Offset: 0x00088348
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x170036E8 RID: 14056
			// (set) Token: 0x06005818 RID: 22552 RVA: 0x0008A160 File Offset: 0x00088360
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036E9 RID: 14057
			// (set) Token: 0x06005819 RID: 22553 RVA: 0x0008A178 File Offset: 0x00088378
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036EA RID: 14058
			// (set) Token: 0x0600581A RID: 22554 RVA: 0x0008A190 File Offset: 0x00088390
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x170036EB RID: 14059
			// (set) Token: 0x0600581B RID: 22555 RVA: 0x0008A1A3 File Offset: 0x000883A3
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x170036EC RID: 14060
			// (set) Token: 0x0600581C RID: 22556 RVA: 0x0008A1BB File Offset: 0x000883BB
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x170036ED RID: 14061
			// (set) Token: 0x0600581D RID: 22557 RVA: 0x0008A1D3 File Offset: 0x000883D3
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036EE RID: 14062
			// (set) Token: 0x0600581E RID: 22558 RVA: 0x0008A1EB File Offset: 0x000883EB
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036EF RID: 14063
			// (set) Token: 0x0600581F RID: 22559 RVA: 0x0008A203 File Offset: 0x00088403
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x170036F0 RID: 14064
			// (set) Token: 0x06005820 RID: 22560 RVA: 0x0008A216 File Offset: 0x00088416
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x170036F1 RID: 14065
			// (set) Token: 0x06005821 RID: 22561 RVA: 0x0008A22E File Offset: 0x0008842E
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036F2 RID: 14066
			// (set) Token: 0x06005822 RID: 22562 RVA: 0x0008A246 File Offset: 0x00088446
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036F3 RID: 14067
			// (set) Token: 0x06005823 RID: 22563 RVA: 0x0008A25E File Offset: 0x0008845E
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x170036F4 RID: 14068
			// (set) Token: 0x06005824 RID: 22564 RVA: 0x0008A271 File Offset: 0x00088471
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x170036F5 RID: 14069
			// (set) Token: 0x06005825 RID: 22565 RVA: 0x0008A289 File Offset: 0x00088489
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170036F6 RID: 14070
			// (set) Token: 0x06005826 RID: 22566 RVA: 0x0008A2A1 File Offset: 0x000884A1
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170036F7 RID: 14071
			// (set) Token: 0x06005827 RID: 22567 RVA: 0x0008A2B9 File Offset: 0x000884B9
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x170036F8 RID: 14072
			// (set) Token: 0x06005828 RID: 22568 RVA: 0x0008A2CC File Offset: 0x000884CC
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x170036F9 RID: 14073
			// (set) Token: 0x06005829 RID: 22569 RVA: 0x0008A2E4 File Offset: 0x000884E4
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170036FA RID: 14074
			// (set) Token: 0x0600582A RID: 22570 RVA: 0x0008A2FC File Offset: 0x000884FC
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170036FB RID: 14075
			// (set) Token: 0x0600582B RID: 22571 RVA: 0x0008A314 File Offset: 0x00088514
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x170036FC RID: 14076
			// (set) Token: 0x0600582C RID: 22572 RVA: 0x0008A32C File Offset: 0x0008852C
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x170036FD RID: 14077
			// (set) Token: 0x0600582D RID: 22573 RVA: 0x0008A344 File Offset: 0x00088544
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x170036FE RID: 14078
			// (set) Token: 0x0600582E RID: 22574 RVA: 0x0008A35C File Offset: 0x0008855C
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x170036FF RID: 14079
			// (set) Token: 0x0600582F RID: 22575 RVA: 0x0008A36F File Offset: 0x0008856F
			public virtual bool PipelineTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingEnabled"] = value;
				}
			}

			// Token: 0x17003700 RID: 14080
			// (set) Token: 0x06005830 RID: 22576 RVA: 0x0008A387 File Offset: 0x00088587
			public virtual bool ContentConversionTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["ContentConversionTracingEnabled"] = value;
				}
			}

			// Token: 0x17003701 RID: 14081
			// (set) Token: 0x06005831 RID: 22577 RVA: 0x0008A39F File Offset: 0x0008859F
			public virtual LocalLongFullPath PipelineTracingPath
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingPath"] = value;
				}
			}

			// Token: 0x17003702 RID: 14082
			// (set) Token: 0x06005832 RID: 22578 RVA: 0x0008A3B2 File Offset: 0x000885B2
			public virtual SmtpAddress? PipelineTracingSenderAddress
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingSenderAddress"] = value;
				}
			}

			// Token: 0x17003703 RID: 14083
			// (set) Token: 0x06005833 RID: 22579 RVA: 0x0008A3CA File Offset: 0x000885CA
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003704 RID: 14084
			// (set) Token: 0x06005834 RID: 22580 RVA: 0x0008A3E2 File Offset: 0x000885E2
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x17003705 RID: 14085
			// (set) Token: 0x06005835 RID: 22581 RVA: 0x0008A3FA File Offset: 0x000885FA
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x17003706 RID: 14086
			// (set) Token: 0x06005836 RID: 22582 RVA: 0x0008A412 File Offset: 0x00088612
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003707 RID: 14087
			// (set) Token: 0x06005837 RID: 22583 RVA: 0x0008A42A File Offset: 0x0008862A
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003708 RID: 14088
			// (set) Token: 0x06005838 RID: 22584 RVA: 0x0008A442 File Offset: 0x00088642
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003709 RID: 14089
			// (set) Token: 0x06005839 RID: 22585 RVA: 0x0008A45A File Offset: 0x0008865A
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700370A RID: 14090
			// (set) Token: 0x0600583A RID: 22586 RVA: 0x0008A46D File Offset: 0x0008866D
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x1700370B RID: 14091
			// (set) Token: 0x0600583B RID: 22587 RVA: 0x0008A485 File Offset: 0x00088685
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x1700370C RID: 14092
			// (set) Token: 0x0600583C RID: 22588 RVA: 0x0008A498 File Offset: 0x00088698
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x1700370D RID: 14093
			// (set) Token: 0x0600583D RID: 22589 RVA: 0x0008A4AB File Offset: 0x000886AB
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x1700370E RID: 14094
			// (set) Token: 0x0600583E RID: 22590 RVA: 0x0008A4C3 File Offset: 0x000886C3
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700370F RID: 14095
			// (set) Token: 0x0600583F RID: 22591 RVA: 0x0008A4DB File Offset: 0x000886DB
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x17003710 RID: 14096
			// (set) Token: 0x06005840 RID: 22592 RVA: 0x0008A4EE File Offset: 0x000886EE
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003711 RID: 14097
			// (set) Token: 0x06005841 RID: 22593 RVA: 0x0008A506 File Offset: 0x00088706
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003712 RID: 14098
			// (set) Token: 0x06005842 RID: 22594 RVA: 0x0008A51E File Offset: 0x0008871E
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003713 RID: 14099
			// (set) Token: 0x06005843 RID: 22595 RVA: 0x0008A536 File Offset: 0x00088736
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003714 RID: 14100
			// (set) Token: 0x06005844 RID: 22596 RVA: 0x0008A549 File Offset: 0x00088749
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x17003715 RID: 14101
			// (set) Token: 0x06005845 RID: 22597 RVA: 0x0008A561 File Offset: 0x00088761
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x17003716 RID: 14102
			// (set) Token: 0x06005846 RID: 22598 RVA: 0x0008A579 File Offset: 0x00088779
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x17003717 RID: 14103
			// (set) Token: 0x06005847 RID: 22599 RVA: 0x0008A591 File Offset: 0x00088791
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x17003718 RID: 14104
			// (set) Token: 0x06005848 RID: 22600 RVA: 0x0008A5A9 File Offset: 0x000887A9
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x17003719 RID: 14105
			// (set) Token: 0x06005849 RID: 22601 RVA: 0x0008A5C1 File Offset: 0x000887C1
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x1700371A RID: 14106
			// (set) Token: 0x0600584A RID: 22602 RVA: 0x0008A5D9 File Offset: 0x000887D9
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x1700371B RID: 14107
			// (set) Token: 0x0600584B RID: 22603 RVA: 0x0008A5F1 File Offset: 0x000887F1
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x1700371C RID: 14108
			// (set) Token: 0x0600584C RID: 22604 RVA: 0x0008A609 File Offset: 0x00088809
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x1700371D RID: 14109
			// (set) Token: 0x0600584D RID: 22605 RVA: 0x0008A621 File Offset: 0x00088821
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x1700371E RID: 14110
			// (set) Token: 0x0600584E RID: 22606 RVA: 0x0008A634 File Offset: 0x00088834
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x1700371F RID: 14111
			// (set) Token: 0x0600584F RID: 22607 RVA: 0x0008A64C File Offset: 0x0008884C
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x17003720 RID: 14112
			// (set) Token: 0x06005850 RID: 22608 RVA: 0x0008A65F File Offset: 0x0008885F
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003721 RID: 14113
			// (set) Token: 0x06005851 RID: 22609 RVA: 0x0008A677 File Offset: 0x00088877
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003722 RID: 14114
			// (set) Token: 0x06005852 RID: 22610 RVA: 0x0008A68F File Offset: 0x0008888F
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003723 RID: 14115
			// (set) Token: 0x06005853 RID: 22611 RVA: 0x0008A6A7 File Offset: 0x000888A7
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003724 RID: 14116
			// (set) Token: 0x06005854 RID: 22612 RVA: 0x0008A6BF File Offset: 0x000888BF
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x17003725 RID: 14117
			// (set) Token: 0x06005855 RID: 22613 RVA: 0x0008A6D7 File Offset: 0x000888D7
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x17003726 RID: 14118
			// (set) Token: 0x06005856 RID: 22614 RVA: 0x0008A6EA File Offset: 0x000888EA
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003727 RID: 14119
			// (set) Token: 0x06005857 RID: 22615 RVA: 0x0008A702 File Offset: 0x00088902
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x17003728 RID: 14120
			// (set) Token: 0x06005858 RID: 22616 RVA: 0x0008A71A File Offset: 0x0008891A
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003729 RID: 14121
			// (set) Token: 0x06005859 RID: 22617 RVA: 0x0008A732 File Offset: 0x00088932
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700372A RID: 14122
			// (set) Token: 0x0600585A RID: 22618 RVA: 0x0008A74A File Offset: 0x0008894A
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x1700372B RID: 14123
			// (set) Token: 0x0600585B RID: 22619 RVA: 0x0008A762 File Offset: 0x00088962
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x1700372C RID: 14124
			// (set) Token: 0x0600585C RID: 22620 RVA: 0x0008A775 File Offset: 0x00088975
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x1700372D RID: 14125
			// (set) Token: 0x0600585D RID: 22621 RVA: 0x0008A78D File Offset: 0x0008898D
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700372E RID: 14126
			// (set) Token: 0x0600585E RID: 22622 RVA: 0x0008A7A5 File Offset: 0x000889A5
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700372F RID: 14127
			// (set) Token: 0x0600585F RID: 22623 RVA: 0x0008A7BD File Offset: 0x000889BD
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003730 RID: 14128
			// (set) Token: 0x06005860 RID: 22624 RVA: 0x0008A7D5 File Offset: 0x000889D5
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x17003731 RID: 14129
			// (set) Token: 0x06005861 RID: 22625 RVA: 0x0008A7ED File Offset: 0x000889ED
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x17003732 RID: 14130
			// (set) Token: 0x06005862 RID: 22626 RVA: 0x0008A805 File Offset: 0x00088A05
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x17003733 RID: 14131
			// (set) Token: 0x06005863 RID: 22627 RVA: 0x0008A81D File Offset: 0x00088A1D
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x17003734 RID: 14132
			// (set) Token: 0x06005864 RID: 22628 RVA: 0x0008A835 File Offset: 0x00088A35
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x17003735 RID: 14133
			// (set) Token: 0x06005865 RID: 22629 RVA: 0x0008A84D File Offset: 0x00088A4D
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x17003736 RID: 14134
			// (set) Token: 0x06005866 RID: 22630 RVA: 0x0008A865 File Offset: 0x00088A65
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x17003737 RID: 14135
			// (set) Token: 0x06005867 RID: 22631 RVA: 0x0008A87D File Offset: 0x00088A7D
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x17003738 RID: 14136
			// (set) Token: 0x06005868 RID: 22632 RVA: 0x0008A890 File Offset: 0x00088A90
			public virtual bool UseDowngradedExchangeServerAuth
			{
				set
				{
					base.PowerSharpParameters["UseDowngradedExchangeServerAuth"] = value;
				}
			}

			// Token: 0x17003739 RID: 14137
			// (set) Token: 0x06005869 RID: 22633 RVA: 0x0008A8A8 File Offset: 0x00088AA8
			public virtual int IntraOrgConnectorSmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorSmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x1700373A RID: 14138
			// (set) Token: 0x0600586A RID: 22634 RVA: 0x0008A8C0 File Offset: 0x00088AC0
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x1700373B RID: 14139
			// (set) Token: 0x0600586B RID: 22635 RVA: 0x0008A8D8 File Offset: 0x00088AD8
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x1700373C RID: 14140
			// (set) Token: 0x0600586C RID: 22636 RVA: 0x0008A8F0 File Offset: 0x00088AF0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700373D RID: 14141
			// (set) Token: 0x0600586D RID: 22637 RVA: 0x0008A908 File Offset: 0x00088B08
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700373E RID: 14142
			// (set) Token: 0x0600586E RID: 22638 RVA: 0x0008A920 File Offset: 0x00088B20
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700373F RID: 14143
			// (set) Token: 0x0600586F RID: 22639 RVA: 0x0008A938 File Offset: 0x00088B38
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003740 RID: 14144
			// (set) Token: 0x06005870 RID: 22640 RVA: 0x0008A950 File Offset: 0x00088B50
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
