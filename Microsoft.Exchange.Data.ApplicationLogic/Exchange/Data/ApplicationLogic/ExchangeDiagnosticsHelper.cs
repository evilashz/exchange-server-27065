using System;
using System.Reflection;
using Microsoft.Exchange.Data.ApplicationLogic.CommonHandlers;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000EC RID: 236
	public class ExchangeDiagnosticsHelper
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x00026584 File Offset: 0x00024784
		internal static void RegisterDiagnosticsComponents()
		{
			ExchangeDiagnosticsSection config = ExchangeDiagnosticsSection.GetConfig();
			ExTraceGlobals.CommonTracer.TraceInformation<int>(0, 0L, "ExchangeDiagnosticsHelper::RegisterDiagnosticsComponents called. No of Component:{0}", config.DiagnosticComponents.Count);
			foreach (object obj in config.DiagnosticComponents)
			{
				DiagnosticsComponent diagnosticsComponent = (DiagnosticsComponent)obj;
				ExTraceGlobals.CommonTracer.TraceDebug(0L, "ExchangeDiagnosticsHelper::RegisterDiagnosticsComponents ComponentName:{0}, Type:{1}, MethodName:{2}, Argument:{3}", new object[]
				{
					diagnosticsComponent.Name,
					diagnosticsComponent.Implementation,
					diagnosticsComponent.MethodName,
					diagnosticsComponent.Argument
				});
				IDiagnosable instance = ExchangeDiagnosticsHelper.GetInstance(diagnosticsComponent);
				if (diagnosticsComponent.Data != null)
				{
					IDiagnosableExtraData diagnosableExtraData = instance as IDiagnosableExtraData;
					if (diagnosableExtraData != null)
					{
						diagnosableExtraData.SetData(diagnosticsComponent.Data);
					}
				}
				ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents(instance);
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00026674 File Offset: 0x00024874
		internal static void RegisterDiagnosticsComponents(IDiagnosable instance)
		{
			if (instance != null)
			{
				try
				{
					ProcessAccessManager.RegisterComponent(instance);
				}
				catch (RpcException ex)
				{
					FaultDiagnosticsComponent faultDiagnosticsComponent = new FaultDiagnosticsComponent();
					faultDiagnosticsComponent.SetComponentNameAndMessage(instance.GetDiagnosticComponentName(), -998, ex.ToString());
					ExTraceGlobals.CommonTracer.TraceError<string>(0L, "ExchangeDiagnosticsHelper::RegisterDiagnosticsComponents Exception:{0}", ex.ToString());
					try
					{
						ProcessAccessManager.RegisterComponent(faultDiagnosticsComponent);
					}
					catch (RpcException ex2)
					{
						ExTraceGlobals.CommonTracer.TraceError<string>(0L, "ExchangeDiagnosticsHelper::RegisterDiagnosticsComponents Exception while registering FaultDiagnosticsComponent. {0}", ex2.ToString());
					}
				}
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00026704 File Offset: 0x00024904
		internal static void UnRegisterDiagnosticsComponents()
		{
			ExchangeDiagnosticsSection config = ExchangeDiagnosticsSection.GetConfig();
			ExTraceGlobals.CommonTracer.TraceInformation<int>(0, 0L, "ExchangeDiagnosticsHelper::UnRegisterDiagnosticsComponents called. No of Component:{0}", config.DiagnosticComponents.Count);
			foreach (object obj in config.DiagnosticComponents)
			{
				DiagnosticsComponent diagnosticsComponent = (DiagnosticsComponent)obj;
				IDiagnosable instance = ExchangeDiagnosticsHelper.GetInstance(diagnosticsComponent);
				ExTraceGlobals.CommonTracer.TraceDebug(0L, "ExchangeDiagnosticsHelper::UnRegisterDiagnosticsComponents ComponentName:{0}, Type:{1}, MethodName:{2}, Argument:{3}", new object[]
				{
					diagnosticsComponent.Name,
					diagnosticsComponent.Implementation,
					diagnosticsComponent.MethodName,
					diagnosticsComponent.Argument
				});
				ExchangeDiagnosticsHelper.UnRegisterDiagnosticsComponents(instance);
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000267CC File Offset: 0x000249CC
		internal static void UnRegisterDiagnosticsComponents(IDiagnosable component)
		{
			if (component != null)
			{
				try
				{
					ProcessAccessManager.UnregisterComponent(component);
				}
				catch (RpcException ex)
				{
					ExTraceGlobals.CommonTracer.TraceError<string>(0L, "ExchangeDiagnosticsHelper::UnRegisterDiagnosticsComponents. Exception:{0}", ex.ToString());
				}
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00026810 File Offset: 0x00024A10
		private static IDiagnosable GetInstance(DiagnosticsComponent component)
		{
			Type type = Type.GetType(component.Implementation, false);
			IDiagnosable result = null;
			if (type != null)
			{
				MethodInfo method = type.GetMethod(component.MethodName, BindingFlags.Static | BindingFlags.Public);
				if (!string.IsNullOrEmpty(component.Argument))
				{
					result = (method.Invoke(null, new object[]
					{
						component.Argument
					}) as IDiagnosable);
				}
				else
				{
					result = (method.Invoke(null, null) as IDiagnosable);
				}
				ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "ExchangeDiagnosticsHelper::GetInstance - instance of {0} created successfully", component.Implementation);
			}
			else
			{
				ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "ExchangeDiagnosticsHelper::GetInstance - could not find {0}", component.Implementation);
			}
			return result;
		}
	}
}
