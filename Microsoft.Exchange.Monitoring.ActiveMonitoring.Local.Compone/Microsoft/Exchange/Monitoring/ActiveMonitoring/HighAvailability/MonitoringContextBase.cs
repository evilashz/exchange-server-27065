using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000199 RID: 409
	internal abstract class MonitoringContextBase
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x0004A1BC File Offset: 0x000483BC
		public MonitoringContextBase(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.broker = broker;
			this.traceContext = traceContext;
			this.ContextType = base.GetType().ToString();
			this.ExceptionCaught = new List<Exception>();
			this.enrolledWorkItems = new List<MonitoringContextBase.EnrollmentResult>();
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0004A20F File Offset: 0x0004840F
		public MonitoringContextBase(IMaintenanceWorkBroker broker, LocalEndpointManager endpointManager, TracingContext traceContext) : this(broker, traceContext)
		{
			this.endpointManager = endpointManager;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0004A220 File Offset: 0x00048420
		public List<MonitoringContextBase.EnrollmentResult> WorkItemsEnrollmentResult
		{
			get
			{
				return this.enrolledWorkItems;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0004A228 File Offset: 0x00048428
		public string LoggedMessages
		{
			get
			{
				if (this.message != null && this.message.Count > 0)
				{
					return string.Join(Environment.NewLine, this.message);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0004A256 File Offset: 0x00048456
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0004A25E File Offset: 0x0004845E
		public List<Exception> ExceptionCaught { get; private set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0004A267 File Offset: 0x00048467
		protected LocalEndpointManager EndpointManager
		{
			get
			{
				return this.endpointManager;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0004A26F File Offset: 0x0004846F
		protected TracingContext TraceContext
		{
			get
			{
				return this.traceContext;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0004A277 File Offset: 0x00048477
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0004A27F File Offset: 0x0004847F
		private string ContextType { get; set; }

		// Token: 0x06000BC3 RID: 3011
		public abstract void CreateContext();

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0004A288 File Offset: 0x00048488
		protected void InvokeCatchAndLog(Action action)
		{
			try
			{
				if (action != null)
				{
					action();
				}
			}
			catch (Exception ex)
			{
				if (this.ExceptionCaught == null)
				{
					this.ExceptionCaught = new List<Exception>();
				}
				this.ExceptionCaught.Add(ex);
				this.AddMessage(string.Format("Exception caught in {0} - Exception: {1}", this.ContextType, ex.ToString()));
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0004A2F0 File Offset: 0x000484F0
		protected void EnrollWorkItem<TDefinition>(TDefinition workDefinition) where TDefinition : WorkDefinition
		{
			TDefinition tdefinition = workDefinition;
			MonitoringContextBase.EnrollmentType workItemType = MonitoringContextBase.EnrollmentType.Unknown;
			if (tdefinition is ProbeDefinition)
			{
				workItemType = MonitoringContextBase.EnrollmentType.Probe;
			}
			else if (tdefinition is MonitorDefinition)
			{
				workItemType = MonitoringContextBase.EnrollmentType.Monitor;
				MonitorDefinition monitorDefinition = (MonitorDefinition)((object)tdefinition);
				if (monitorDefinition.MonitoringIntervalSeconds <= 0)
				{
					this.message.Add(string.Format("Monitor '{0}' MonitoringInterval was '{1}'. Rectified to default value '{2}'", monitorDefinition.Name, monitorDefinition.MonitoringIntervalSeconds, 300));
					monitorDefinition.MonitoringIntervalSeconds = 300;
					tdefinition = (TDefinition)((object)monitorDefinition);
				}
			}
			else if (tdefinition is ResponderDefinition)
			{
				workItemType = MonitoringContextBase.EnrollmentType.Responder;
			}
			this.enrolledWorkItems.Add(new MonitoringContextBase.EnrollmentResult
			{
				WorkItemResultName = tdefinition.ConstructWorkItemResultName(),
				WorkItemClass = tdefinition.TypeName,
				WorkItemType = workItemType
			});
			this.broker.AddWorkDefinition<TDefinition>(tdefinition, this.TraceContext);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004A3E0 File Offset: 0x000485E0
		protected void AddChainedResponders(ref MonitorDefinition monitor, params MonitorStateResponderTuple[] tuples)
		{
			List<MonitorStateTransition> list = new List<MonitorStateTransition>();
			List<ResponderDefinition> list2 = new List<ResponderDefinition>();
			for (int i = 0; i < tuples.Length; i++)
			{
				list.Add(tuples[i].MonitorState);
				ResponderDefinition responder = tuples[i].Responder;
				if (responder != null)
				{
					responder.TargetHealthState = tuples[i].MonitorState.ToState;
					if (string.IsNullOrEmpty(responder.ServiceName) || !ExchangeComponent.WellKnownComponents.ContainsKey(responder.ServiceName))
					{
						responder.ServiceName = HighAvailabilityConstants.ServiceName;
					}
					list2.Add(responder);
				}
			}
			monitor.MonitorStateTransitions = list.ToArray();
			if (string.IsNullOrEmpty(monitor.ServiceName))
			{
				monitor.ServiceName = HighAvailabilityConstants.ServiceName;
			}
			this.EnrollWorkItem<MonitorDefinition>(monitor);
			bool disableResponders = HighAvailabilityConstants.DisableResponders;
			if (disableResponders)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HighAvailabilityTracer, this.TraceContext, "MonitoringContextBase:: AddChainedResponders(): Responders Disabled, master switch is SET.", null, "AddChainedResponders", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\MonitoringContext\\MonitoringContextBase.cs", 276);
				return;
			}
			foreach (ResponderDefinition responderDefinition in list2)
			{
				if (responderDefinition != null)
				{
					this.EnrollWorkItem<ResponderDefinition>(responderDefinition);
				}
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0004A51C File Offset: 0x0004871C
		protected void AddMessage(string messageToBeLogged)
		{
			this.message.Add(messageToBeLogged);
		}

		// Token: 0x040008FE RID: 2302
		private IMaintenanceWorkBroker broker;

		// Token: 0x040008FF RID: 2303
		private LocalEndpointManager endpointManager;

		// Token: 0x04000900 RID: 2304
		private TracingContext traceContext;

		// Token: 0x04000901 RID: 2305
		private List<MonitoringContextBase.EnrollmentResult> enrolledWorkItems;

		// Token: 0x04000902 RID: 2306
		private List<string> message = new List<string>();

		// Token: 0x0200019A RID: 410
		public enum EnrollmentType
		{
			// Token: 0x04000906 RID: 2310
			Probe,
			// Token: 0x04000907 RID: 2311
			Monitor,
			// Token: 0x04000908 RID: 2312
			Responder,
			// Token: 0x04000909 RID: 2313
			Unknown
		}

		// Token: 0x0200019B RID: 411
		public struct EnrollmentResult
		{
			// Token: 0x0400090A RID: 2314
			public string WorkItemResultName;

			// Token: 0x0400090B RID: 2315
			public string WorkItemClass;

			// Token: 0x0400090C RID: 2316
			public MonitoringContextBase.EnrollmentType WorkItemType;
		}
	}
}
