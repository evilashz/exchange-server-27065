using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment
{
	// Token: 0x0200002C RID: 44
	internal abstract class MonitoringContextBase
	{
		// Token: 0x06000174 RID: 372 RVA: 0x0000BA72 File Offset: 0x00009C72
		public MonitoringContextBase(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.broker = broker;
			this.traceContext = traceContext;
			this.enrolledWorkItems = new List<MonitoringContextBase.EnrollmentResult>();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000BA9E File Offset: 0x00009C9E
		public MonitoringContextBase(IMaintenanceWorkBroker broker, LocalEndpointManager endpointManager, TracingContext traceContext) : this(broker, traceContext)
		{
			this.endpointManager = endpointManager;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000BAAF File Offset: 0x00009CAF
		public List<MonitoringContextBase.EnrollmentResult> WorkItemsEnrollmentResult
		{
			get
			{
				return this.enrolledWorkItems;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000BAB7 File Offset: 0x00009CB7
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000BAE5 File Offset: 0x00009CE5
		protected LocalEndpointManager EndpointManager
		{
			get
			{
				return this.endpointManager;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000BAED File Offset: 0x00009CED
		protected TracingContext TraceContext
		{
			get
			{
				return this.traceContext;
			}
		}

		// Token: 0x0600017A RID: 378
		public abstract void CreateContext();

		// Token: 0x0600017B RID: 379 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		protected void EnrollWorkItem<TDefinition>(TDefinition workDefinition) where TDefinition : WorkDefinition
		{
			MonitoringContextBase.EnrollmentType workItemType = MonitoringContextBase.EnrollmentType.Unknown;
			if (workDefinition is ProbeDefinition)
			{
				workItemType = MonitoringContextBase.EnrollmentType.Probe;
			}
			else if (workDefinition is MonitorDefinition)
			{
				workItemType = MonitoringContextBase.EnrollmentType.Monitor;
			}
			else if (workDefinition is ResponderDefinition)
			{
				workItemType = MonitoringContextBase.EnrollmentType.Responder;
			}
			this.enrolledWorkItems.Add(new MonitoringContextBase.EnrollmentResult
			{
				WorkItemResultName = workDefinition.ConstructWorkItemResultName(),
				WorkItemClass = workDefinition.TypeName,
				WorkItemType = workItemType
			});
			this.broker.AddWorkDefinition<TDefinition>(workDefinition, this.TraceContext);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000BB90 File Offset: 0x00009D90
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
					if (string.IsNullOrEmpty(responder.ServiceName) || responder.ServiceName != "BitlockerDeployment")
					{
						responder.ServiceName = "BitlockerDeployment";
					}
					list2.Add(responder);
				}
			}
			monitor.MonitorStateTransitions = list.ToArray();
			if (string.IsNullOrEmpty(monitor.ServiceName) || monitor.ServiceName != "BitlockerDeployment")
			{
				monitor.ServiceName = "BitlockerDeployment";
			}
			this.EnrollWorkItem<MonitorDefinition>(monitor);
			bool flag = false;
			if (flag)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HighAvailabilityTracer, this.TraceContext, "MonitoringContextBase:: AddChainedResponders(): Responders Disabled, master switch is SET.", null, "AddChainedResponders", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\MonitoringContext\\MonitoringContextBase.cs", 215);
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

		// Token: 0x0600017D RID: 381 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		protected void AddMessage(string messageToBeLogged)
		{
			this.message.Add(messageToBeLogged);
		}

		// Token: 0x04000119 RID: 281
		private IMaintenanceWorkBroker broker;

		// Token: 0x0400011A RID: 282
		private LocalEndpointManager endpointManager;

		// Token: 0x0400011B RID: 283
		private TracingContext traceContext;

		// Token: 0x0400011C RID: 284
		private List<MonitoringContextBase.EnrollmentResult> enrolledWorkItems;

		// Token: 0x0400011D RID: 285
		private List<string> message = new List<string>();

		// Token: 0x0200002D RID: 45
		public enum EnrollmentType
		{
			// Token: 0x0400011F RID: 287
			Probe,
			// Token: 0x04000120 RID: 288
			Monitor,
			// Token: 0x04000121 RID: 289
			Responder,
			// Token: 0x04000122 RID: 290
			Unknown
		}

		// Token: 0x0200002E RID: 46
		public struct EnrollmentResult
		{
			// Token: 0x04000123 RID: 291
			public string WorkItemResultName;

			// Token: 0x04000124 RID: 292
			public string WorkItemClass;

			// Token: 0x04000125 RID: 293
			public MonitoringContextBase.EnrollmentType WorkItemType;
		}
	}
}
