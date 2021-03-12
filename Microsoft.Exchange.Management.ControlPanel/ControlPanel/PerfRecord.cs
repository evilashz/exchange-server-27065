using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000282 RID: 642
	[DataContract]
	public class PerfRecord
	{
		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x00083C4C File Offset: 0x00081E4C
		private static IPerformanceDataProvider[] PerformanceDataProviders
		{
			get
			{
				return new IPerformanceDataProvider[]
				{
					PerformanceContext.Current,
					RpcDataProvider.Instance,
					TaskPerformanceData.CmdletInvoked,
					TaskPerformanceData.BeginProcessingInvoked,
					TaskPerformanceData.ProcessRecordInvoked,
					TaskPerformanceData.EndProcessingInvoked,
					EcpPerformanceData.CreateRbacSession,
					EcpPerformanceData.ActiveRunspace,
					EcpPerformanceData.CreateRunspace,
					EcpPerformanceData.PowerShellInvoke,
					EcpPerformanceData.WcfSerialization,
					AspPerformanceData.GetStepData(RequestNotification.AuthenticateRequest),
					AspPerformanceData.GetStepData(RequestNotification.AuthorizeRequest),
					AspPerformanceData.GetStepData(RequestNotification.ResolveRequestCache),
					AspPerformanceData.GetStepData(RequestNotification.MapRequestHandler),
					AspPerformanceData.GetStepData(RequestNotification.AcquireRequestState),
					AspPerformanceData.GetStepData(RequestNotification.ExecuteRequestHandler),
					AspPerformanceData.GetStepData(RequestNotification.ReleaseRequestState),
					AspPerformanceData.GetStepData(RequestNotification.UpdateRequestCache),
					AspPerformanceData.GetStepData(RequestNotification.LogRequest),
					EcpPerformanceData.XamlParsed,
					EcpPerformanceData.DDIServiceExecution,
					EcpPerformanceData.DDITypeConversion
				};
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x00083D44 File Offset: 0x00081F44
		public PerfRecord(string requestPath)
		{
			PerfRecord.activeRequestsCounter.Increment();
			this.aspThreadPerfRecord = new TaskPerformanceRecord(requestPath, PerfRecord.aspLatencyDetectionContextFactory, EcpEventLogConstants.Tuple_EcpApplicationRequestStarted, EcpEventLogConstants.Tuple_EcpApplicationRequestEnded, EcpEventLogExtensions.EventLog);
			this.wcfThreadPerfRecord = new TaskPerformanceRecord(requestPath, PerfRecord.wcfLatencyDetectionContextFactory, EcpEventLogConstants.Tuple_EcpWebServiceRequestStarted, EcpEventLogConstants.Tuple_EcpWebServiceRequestCompleted, EcpEventLogExtensions.EventLog);
			this.aspThreadPerfRecord.Start(PerfRecord.PerformanceDataProviders);
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x00083DD2 File Offset: 0x00081FD2
		// (set) Token: 0x060029F3 RID: 10739 RVA: 0x00083DED File Offset: 0x00081FED
		public static PerfRecord Current
		{
			get
			{
				return (PerfRecord)HttpContext.Current.Items[PerfRecord.ecpRequestContextKey];
			}
			set
			{
				HttpContext.Current.Items[PerfRecord.ecpRequestContextKey] = value;
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00083E04 File Offset: 0x00082004
		public void StepStarted(RequestNotification notification)
		{
			this.aspPerformanceData.StepStarted(notification);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00083E12 File Offset: 0x00082012
		public void StepCompleted()
		{
			this.aspPerformanceData.StepCompleted();
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00083E20 File Offset: 0x00082020
		public void EndRequest()
		{
			if (this.aspThreadPerfRecord.IsCollecting)
			{
				this.averageRequestTime.Stop();
				PerfRecord.activeRequestsCounter.Decrement();
				this.ServerRequestTime = Math.Round(this.aspThreadPerfRecord.Stop().TotalMilliseconds, 4);
				EcpEventLogConstants.Tuple_EcpPerformanceRecord.LogEvent(new object[]
				{
					this.aspThreadPerfRecord.TaskName,
					this
				});
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00083E92 File Offset: 0x00082092
		public void WebServiceCallStarted()
		{
			this.wcfThreadPerfRecord.Start(PerfRecord.PerformanceDataProviders);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00083EA4 File Offset: 0x000820A4
		public void WebServiceCallCompleted()
		{
			this.wcfThreadPerfRecord.Stop();
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00083EB4 File Offset: 0x000820B4
		private double GetLatency(PerfRecord.PerfProvider providerIndex)
		{
			return Math.Round((this.aspThreadPerfRecord[(int)providerIndex].Latency + this.wcfThreadPerfRecord[(int)providerIndex].Latency).TotalMilliseconds, 4);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x00083EFC File Offset: 0x000820FC
		private uint GetCount(PerfRecord.PerfProvider providerIndex)
		{
			return this.aspThreadPerfRecord[(int)providerIndex].Count + this.wcfThreadPerfRecord[(int)providerIndex].Count;
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x00083F32 File Offset: 0x00082132
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x00083F3A File Offset: 0x0008213A
		[DataMember]
		public double ServerRequestTime { get; set; }

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x00083F43 File Offset: 0x00082143
		// (set) Token: 0x060029FE RID: 10750 RVA: 0x00083F4D File Offset: 0x0008214D
		[DataMember]
		public double Authentication
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.AuthenticateRequest);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x00083F54 File Offset: 0x00082154
		// (set) Token: 0x06002A00 RID: 10752 RVA: 0x00083F5E File Offset: 0x0008215E
		[DataMember]
		public double Authorization
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.AuthorizeRequest);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x00083F65 File Offset: 0x00082165
		// (set) Token: 0x06002A02 RID: 10754 RVA: 0x00083F6F File Offset: 0x0008216F
		[DataMember]
		public double ResolveCache
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.ResolveRequestCache);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x00083F76 File Offset: 0x00082176
		// (set) Token: 0x06002A04 RID: 10756 RVA: 0x00083F80 File Offset: 0x00082180
		[DataMember]
		public double MapRequest
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.MapRequestHandler);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x00083F87 File Offset: 0x00082187
		// (set) Token: 0x06002A06 RID: 10758 RVA: 0x00083F91 File Offset: 0x00082191
		[DataMember]
		public double AcquireState
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.AcquireRequestState);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x00083F98 File Offset: 0x00082198
		// (set) Token: 0x06002A08 RID: 10760 RVA: 0x00083FA2 File Offset: 0x000821A2
		[DataMember]
		public double ExecuteRequest
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.ExecuteRequestHandler);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x00083FA9 File Offset: 0x000821A9
		// (set) Token: 0x06002A0A RID: 10762 RVA: 0x00083FB3 File Offset: 0x000821B3
		[DataMember]
		public double ReleaseState
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.ReleaseRequestState);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x00083FBA File Offset: 0x000821BA
		// (set) Token: 0x06002A0C RID: 10764 RVA: 0x00083FC4 File Offset: 0x000821C4
		[DataMember]
		public double UpdateCache
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.UpdateRequestCache);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x00083FCB File Offset: 0x000821CB
		// (set) Token: 0x06002A0E RID: 10766 RVA: 0x00083FD5 File Offset: 0x000821D5
		[DataMember]
		public double LogRequest
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.LogRequest);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x00083FDC File Offset: 0x000821DC
		// (set) Token: 0x06002A10 RID: 10768 RVA: 0x00083FE5 File Offset: 0x000821E5
		[DataMember]
		public uint Rpc
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.Rpc);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x00083FEC File Offset: 0x000821EC
		// (set) Token: 0x06002A12 RID: 10770 RVA: 0x00083FF5 File Offset: 0x000821F5
		[DataMember]
		public double RpcLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.Rpc);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x00083FFC File Offset: 0x000821FC
		// (set) Token: 0x06002A14 RID: 10772 RVA: 0x00084005 File Offset: 0x00082205
		[DataMember]
		public uint Ldap
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.Ldap);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x0008400C File Offset: 0x0008220C
		// (set) Token: 0x06002A16 RID: 10774 RVA: 0x00084015 File Offset: 0x00082215
		[DataMember]
		public double LdapLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.Ldap);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x0008401C File Offset: 0x0008221C
		// (set) Token: 0x06002A18 RID: 10776 RVA: 0x00084025 File Offset: 0x00082225
		[DataMember]
		public uint Rbac
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.CreateRbacSession);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x0008402C File Offset: 0x0008222C
		// (set) Token: 0x06002A1A RID: 10778 RVA: 0x00084035 File Offset: 0x00082235
		[DataMember]
		public double RbacLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.CreateRbacSession);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x0008403C File Offset: 0x0008223C
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x00084047 File Offset: 0x00082247
		[DataMember]
		public double CreateRunspace
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.CreateRunspace);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x0008404E File Offset: 0x0008224E
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x00084057 File Offset: 0x00082257
		[DataMember]
		public double CreateRunspaceLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.CreateRunspace);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x0008405E File Offset: 0x0008225E
		// (set) Token: 0x06002A20 RID: 10784 RVA: 0x00084069 File Offset: 0x00082269
		[DataMember]
		public double ActiveRunspace
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.ActiveRunspace);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x00084070 File Offset: 0x00082270
		// (set) Token: 0x06002A22 RID: 10786 RVA: 0x00084079 File Offset: 0x00082279
		[DataMember]
		public double ActiveRunspaceLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.ActiveRunspace);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x00084080 File Offset: 0x00082280
		// (set) Token: 0x06002A24 RID: 10788 RVA: 0x0008408A File Offset: 0x0008228A
		[DataMember]
		public uint PowerShellInvoke
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.PowerShellInvoke);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x00084091 File Offset: 0x00082291
		// (set) Token: 0x06002A26 RID: 10790 RVA: 0x0008409B File Offset: 0x0008229B
		[DataMember]
		public double PowerShellInvokeLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.PowerShellInvoke);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000840A2 File Offset: 0x000822A2
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x000840AB File Offset: 0x000822AB
		[DataMember]
		public uint Cmdlet
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.Cmdlet);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000840B2 File Offset: 0x000822B2
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x000840BB File Offset: 0x000822BB
		[DataMember]
		public double CmdletLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.Cmdlet);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000840C2 File Offset: 0x000822C2
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x000840CD File Offset: 0x000822CD
		[DataMember]
		public double BeginProcessing
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.BeginProcessing);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x000840D4 File Offset: 0x000822D4
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x000840DD File Offset: 0x000822DD
		[DataMember]
		public double BeginProcessingLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.BeginProcessing);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000840E4 File Offset: 0x000822E4
		// (set) Token: 0x06002A30 RID: 10800 RVA: 0x000840ED File Offset: 0x000822ED
		[DataMember]
		public uint ProcessRecord
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.ProcessRecord);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000840F4 File Offset: 0x000822F4
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x000840FD File Offset: 0x000822FD
		[DataMember]
		public double ProcessRecordLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.ProcessRecord);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x00084104 File Offset: 0x00082304
		// (set) Token: 0x06002A34 RID: 10804 RVA: 0x0008410D File Offset: 0x0008230D
		[DataMember]
		public double EndProcessingLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.EndProcessing);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x00084114 File Offset: 0x00082314
		// (set) Token: 0x06002A36 RID: 10806 RVA: 0x0008411E File Offset: 0x0008231E
		[DataMember]
		public uint WcfSerialization
		{
			get
			{
				return this.GetCount(PerfRecord.PerfProvider.WcfSerialization);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x00084125 File Offset: 0x00082325
		// (set) Token: 0x06002A38 RID: 10808 RVA: 0x0008412F File Offset: 0x0008232F
		[DataMember]
		public double WcfSerializationLatency
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.WcfSerialization);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x00084136 File Offset: 0x00082336
		// (set) Token: 0x06002A3A RID: 10810 RVA: 0x00084140 File Offset: 0x00082340
		[DataMember]
		public double XamlParser
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.XamlParser);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x00084147 File Offset: 0x00082347
		// (set) Token: 0x06002A3C RID: 10812 RVA: 0x00084151 File Offset: 0x00082351
		[DataMember]
		public double DDIServiceExecution
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.DDIServiceExecution);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x00084158 File Offset: 0x00082358
		// (set) Token: 0x06002A3E RID: 10814 RVA: 0x00084162 File Offset: 0x00082362
		[DataMember]
		public double DDITypeConversion
		{
			get
			{
				return this.GetLatency(PerfRecord.PerfProvider.DDITypeConversion);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0008416C File Offset: 0x0008236C
		public void AppendToIisLog()
		{
			if (!string.IsNullOrEmpty(HttpContext.Current.GetSessionID()))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("&perfRecord=");
				stringBuilder.Append(this.ToString().Replace("&", "%26"));
				stringBuilder.Append("&sessionId=");
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					HttpContext.Current.Server.UrlEncode(HttpContext.Current.GetSessionID(), stringWriter);
				}
				LocalSession localSession = RbacPrincipal.GetCurrent(false) as LocalSession;
				if (localSession != null)
				{
					stringBuilder.Append("&logonType=");
					stringBuilder.Append(localSession.LogonTypeFlag);
				}
				HttpContext.Current.Response.AppendToLog(stringBuilder.ToString());
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00084244 File Offset: 0x00082444
		public override string ToString()
		{
			if (this.json == null && this.ServerRequestTime != 0.0)
			{
				this.json = this.ToJsonString(null);
			}
			if (this.json != null)
			{
				return this.json;
			}
			return this.ToJsonString(null);
		}

		// Token: 0x04002101 RID: 8449
		private const int Precision = 4;

		// Token: 0x04002102 RID: 8450
		private static LatencyDetectionContextFactory aspLatencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("ECP.AspRequest");

		// Token: 0x04002103 RID: 8451
		private static LatencyDetectionContextFactory wcfLatencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("ECP.WcfRequest");

		// Token: 0x04002104 RID: 8452
		private TaskPerformanceRecord aspThreadPerfRecord;

		// Token: 0x04002105 RID: 8453
		private TaskPerformanceRecord wcfThreadPerfRecord;

		// Token: 0x04002106 RID: 8454
		private AspPerformanceData aspPerformanceData = new AspPerformanceData();

		// Token: 0x04002107 RID: 8455
		private AverageTimePerfCounter averageRequestTime = new AverageTimePerfCounter(EcpPerfCounters.AverageResponseTime, EcpPerfCounters.AverageResponseTimeBase, true);

		// Token: 0x04002108 RID: 8456
		private static PerfCounterGroup activeRequestsCounter = new PerfCounterGroup(EcpPerfCounters.ActiveRequests, EcpPerfCounters.ActiveRequestsPeak, EcpPerfCounters.ActiveRequestsTotal);

		// Token: 0x04002109 RID: 8457
		private static object ecpRequestContextKey = new object();

		// Token: 0x0400210A RID: 8458
		private string json;

		// Token: 0x02000283 RID: 643
		internal enum PerfProvider
		{
			// Token: 0x0400210D RID: 8461
			Ldap,
			// Token: 0x0400210E RID: 8462
			Rpc,
			// Token: 0x0400210F RID: 8463
			Cmdlet,
			// Token: 0x04002110 RID: 8464
			BeginProcessing,
			// Token: 0x04002111 RID: 8465
			ProcessRecord,
			// Token: 0x04002112 RID: 8466
			EndProcessing,
			// Token: 0x04002113 RID: 8467
			CreateRbacSession,
			// Token: 0x04002114 RID: 8468
			ActiveRunspace,
			// Token: 0x04002115 RID: 8469
			CreateRunspace,
			// Token: 0x04002116 RID: 8470
			PowerShellInvoke,
			// Token: 0x04002117 RID: 8471
			WcfSerialization,
			// Token: 0x04002118 RID: 8472
			AuthenticateRequest,
			// Token: 0x04002119 RID: 8473
			AuthorizeRequest,
			// Token: 0x0400211A RID: 8474
			ResolveRequestCache,
			// Token: 0x0400211B RID: 8475
			MapRequestHandler,
			// Token: 0x0400211C RID: 8476
			AcquireRequestState,
			// Token: 0x0400211D RID: 8477
			ExecuteRequestHandler,
			// Token: 0x0400211E RID: 8478
			ReleaseRequestState,
			// Token: 0x0400211F RID: 8479
			UpdateRequestCache,
			// Token: 0x04002120 RID: 8480
			LogRequest,
			// Token: 0x04002121 RID: 8481
			XamlParser,
			// Token: 0x04002122 RID: 8482
			DDIServiceExecution,
			// Token: 0x04002123 RID: 8483
			DDITypeConversion
		}
	}
}
