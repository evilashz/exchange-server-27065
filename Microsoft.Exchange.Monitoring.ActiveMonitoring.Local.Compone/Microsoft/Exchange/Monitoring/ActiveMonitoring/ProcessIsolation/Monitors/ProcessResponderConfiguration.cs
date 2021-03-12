using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation.Monitors
{
	// Token: 0x0200029B RID: 667
	internal class ProcessResponderConfiguration
	{
		// Token: 0x060012EB RID: 4843 RVA: 0x00084DAE File Offset: 0x00082FAE
		internal ProcessResponderConfiguration(Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> privateWorkingSetTriggerWarningResponders, Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> privateWorkingSetTriggerErrorResponders, Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> processProcessorTimeTriggerWarningResponders, Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> processProcessorTimeTriggerErrorResponders, Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> exchangeCrashEventTriggerErrorResponders, Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWarningResponders)
		{
			this.PrivateWorkingSetTriggerWarningResponders = privateWorkingSetTriggerWarningResponders;
			this.PrivateWorkingSetTriggerErrorResponders = privateWorkingSetTriggerErrorResponders;
			this.ProcessProcessorTimeTriggerWarningResponders = processProcessorTimeTriggerWarningResponders;
			this.ProcessProcessorTimeTriggerErrorResponders = processProcessorTimeTriggerErrorResponders;
			this.ExchangeCrashEventTriggerErrorResponders = exchangeCrashEventTriggerErrorResponders;
			this.WatsonWarningResponders = watsonWarningResponders;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00084DE3 File Offset: 0x00082FE3
		internal static ProcessResponderConfiguration CollectDiagnosticsWatsonEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.collectDiagnosticsWatsonEscalateResponse;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00084DEA File Offset: 0x00082FEA
		internal static ProcessResponderConfiguration CollectDiagnosticsWatsonRestartEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.collectDiagnosticsWatsonRestartEscalateResponse;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00084DF1 File Offset: 0x00082FF1
		internal static ProcessResponderConfiguration CreateBugsAndEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.createBugsAndEscalateResponse;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00084DF8 File Offset: 0x00082FF8
		internal static ProcessResponderConfiguration EscalateOnlyResponse
		{
			get
			{
				return ProcessResponderConfiguration.escalateOnlyResponse;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00084DFF File Offset: 0x00082FFF
		internal static ProcessResponderConfiguration RestartNodeRunnerEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.restartNodeRunnerEscalateResponse;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00084E06 File Offset: 0x00083006
		internal static ProcessResponderConfiguration RestartIndexNodeRunnerEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.restartIndexNodeRunnerEscalateResponse;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00084E0D File Offset: 0x0008300D
		internal static ProcessResponderConfiguration NoResponse
		{
			get
			{
				return ProcessResponderConfiguration.noResponse;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00084E14 File Offset: 0x00083014
		internal static ProcessResponderConfiguration WatsonDiagKillEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.watsonDiagKillEscalateResponse;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00084E1B File Offset: 0x0008301B
		internal static ProcessResponderConfiguration KillProcessEscalateResponse
		{
			get
			{
				return ProcessResponderConfiguration.killProcessEscalateResponse;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00084E22 File Offset: 0x00083022
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x00084E2A File Offset: 0x0008302A
		internal Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> PrivateWorkingSetTriggerWarningResponders { get; private set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00084E33 File Offset: 0x00083033
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x00084E3B File Offset: 0x0008303B
		internal Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> PrivateWorkingSetTriggerErrorResponders { get; private set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00084E44 File Offset: 0x00083044
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x00084E4C File Offset: 0x0008304C
		internal Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> ProcessProcessorTimeTriggerWarningResponders { get; private set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00084E55 File Offset: 0x00083055
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x00084E5D File Offset: 0x0008305D
		internal Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> ProcessProcessorTimeTriggerErrorResponders { get; private set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00084E66 File Offset: 0x00083066
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00084E6E File Offset: 0x0008306E
		internal Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> ExchangeCrashEventTriggerErrorResponders { get; private set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00084E77 File Offset: 0x00083077
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00084E7F File Offset: 0x0008307F
		internal Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> WatsonWarningResponders { get; private set; }

		// Token: 0x04000E40 RID: 3648
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWithF1TraceChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformationalWithF1Trace)
			}
		};

		// Token: 0x04000E41 RID: 3649
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWithF1TraceEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformationalWithF1Trace)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(60.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E42 RID: 3650
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWithDumpChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonWithDump)
			}
		};

		// Token: 0x04000E43 RID: 3651
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWithDumpRestartChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonWithDump)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded1, (int)TimeSpan.FromMinutes(10.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.RestartProcess)
			}
		};

		// Token: 0x04000E44 RID: 3652
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWithDumpEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonWithDump)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(60.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E45 RID: 3653
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonWithDumpRestartEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonWithDump)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded1, (int)TimeSpan.FromMinutes(10.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.RestartProcess)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(60.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E46 RID: 3654
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonInformationalWithF1TraceChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformationalWithF1Trace)
			}
		};

		// Token: 0x04000E47 RID: 3655
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonInformationalWithF1TraceAndEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformationalWithF1Trace)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(10.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E48 RID: 3656
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonInformationalAndEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformational)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(60.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E49 RID: 3657
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonInformationalChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformational)
			}
		};

		// Token: 0x04000E4A RID: 3658
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> escalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E4B RID: 3659
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> restartNodeRunnerEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.RestartNodeRunner)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(60.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E4C RID: 3660
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> f1RestartNodeRunnerEscalateProcessorAffinitizeChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.WatsonInformationalWithF1Trace)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(10.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.RestartNodeRunner)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy1, (int)TimeSpan.FromMinutes(60.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy2, (int)TimeSpan.FromMinutes(65.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.ProcessorAffinitize)
			}
		};

		// Token: 0x04000E4D RID: 3661
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> watsonDiagChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)TimeSpan.FromSeconds(0.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.KillProcess)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded1, (int)TimeSpan.FromSeconds(5.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.DeleteWatsonTempDumpFiles)
			}
		};

		// Token: 0x04000E4E RID: 3662
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> killProcessEscalateChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>
		{
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)TimeSpan.FromSeconds(0.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.KillProcess)
			},
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(5.0).TotalSeconds),
				new ResponderDefinitionDelegate(ProcessIsolationDiscovery.CreateEscalateResponder)
			}
		};

		// Token: 0x04000E4F RID: 3663
		private static readonly Dictionary<MonitorStateTransition, ResponderDefinitionDelegate> noResponseChain = new Dictionary<MonitorStateTransition, ResponderDefinitionDelegate>();

		// Token: 0x04000E50 RID: 3664
		private static readonly ProcessResponderConfiguration collectDiagnosticsWatsonEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.watsonWithDumpChain, ProcessResponderConfiguration.watsonWithDumpEscalateChain, ProcessResponderConfiguration.watsonWithF1TraceChain, ProcessResponderConfiguration.watsonWithF1TraceEscalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E51 RID: 3665
		private static readonly ProcessResponderConfiguration collectDiagnosticsWatsonRestartEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.watsonWithDumpRestartChain, ProcessResponderConfiguration.watsonWithDumpRestartEscalateChain, ProcessResponderConfiguration.watsonWithF1TraceChain, ProcessResponderConfiguration.watsonWithF1TraceEscalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E52 RID: 3666
		private static readonly ProcessResponderConfiguration createBugsAndEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.watsonInformationalChain, ProcessResponderConfiguration.watsonInformationalAndEscalateChain, ProcessResponderConfiguration.watsonInformationalWithF1TraceChain, ProcessResponderConfiguration.watsonInformationalWithF1TraceAndEscalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E53 RID: 3667
		private static readonly ProcessResponderConfiguration escalateOnlyResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E54 RID: 3668
		private static readonly ProcessResponderConfiguration restartNodeRunnerEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.restartNodeRunnerEscalateChain, ProcessResponderConfiguration.restartNodeRunnerEscalateChain, ProcessResponderConfiguration.f1RestartNodeRunnerEscalateProcessorAffinitizeChain, ProcessResponderConfiguration.f1RestartNodeRunnerEscalateProcessorAffinitizeChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E55 RID: 3669
		private static readonly ProcessResponderConfiguration restartIndexNodeRunnerEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.restartNodeRunnerEscalateChain, ProcessResponderConfiguration.restartNodeRunnerEscalateChain, ProcessResponderConfiguration.watsonWithF1TraceChain, ProcessResponderConfiguration.f1RestartNodeRunnerEscalateProcessorAffinitizeChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E56 RID: 3670
		private static readonly ProcessResponderConfiguration noResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain);

		// Token: 0x04000E57 RID: 3671
		private static readonly ProcessResponderConfiguration watsonDiagKillEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.watsonDiagChain, ProcessResponderConfiguration.watsonDiagChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.noResponseChain, ProcessResponderConfiguration.watsonDiagChain);

		// Token: 0x04000E58 RID: 3672
		private static readonly ProcessResponderConfiguration killProcessEscalateResponse = new ProcessResponderConfiguration(ProcessResponderConfiguration.killProcessEscalateChain, ProcessResponderConfiguration.killProcessEscalateChain, ProcessResponderConfiguration.watsonWithF1TraceChain, ProcessResponderConfiguration.watsonWithF1TraceEscalateChain, ProcessResponderConfiguration.escalateChain, ProcessResponderConfiguration.noResponseChain);
	}
}
