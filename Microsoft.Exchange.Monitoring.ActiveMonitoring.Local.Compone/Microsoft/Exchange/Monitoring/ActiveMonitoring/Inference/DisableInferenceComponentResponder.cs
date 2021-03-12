using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Inference
{
	// Token: 0x02000497 RID: 1175
	public class DisableInferenceComponentResponder : RestartServiceResponder
	{
		// Token: 0x06001DA1 RID: 7585 RVA: 0x000B25EC File Offset: 0x000B07EC
		internal static ResponderDefinition CreateDefinition(string responderName, string alertMask, ServiceHealthStatus responderTargetState, string componentsToDisable, bool enabled)
		{
			if (string.IsNullOrEmpty(componentsToDisable))
			{
				throw new ArgumentException("componentsToDisable");
			}
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition(responderName, alertMask, "Inference", responderTargetState, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.Inference.Name, null, true, true, "Dag", false);
			responderDefinition.AssemblyPath = DisableInferenceComponentResponder.AssemblyPath;
			responderDefinition.TypeName = DisableInferenceComponentResponder.TypeName;
			responderDefinition.RecurrenceIntervalSeconds = 0;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes["ComponentsToDisable"] = componentsToDisable;
			DisableInferenceComponentResponder.SetPossibleRecoveryActionThrottlingInfo(responderDefinition);
			return responderDefinition;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000B2690 File Offset: 0x000B0890
		private static void SetPossibleRecoveryActionThrottlingInfo(ResponderDefinition definition)
		{
			definition.ThrottlePolicyXml = ExchangeThrottleSettings.Instance.ConvertThrottleDefinitionsToCompactXml(new List<ThrottleDescriptionEntry>
			{
				ExchangeThrottleSettings.Instance.GetThrottleDefinition(RecoveryActionId.RestartService, "msexchangedelivery", definition),
				ExchangeThrottleSettings.Instance.GetThrottleDefinition(RecoveryActionId.RestartService, "msexchangemailboxassistants", definition),
				ExchangeThrottleSettings.Instance.GetThrottleDefinition(RecoveryActionId.RestartService, "msexchangemailboxassistants", definition)
			});
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000B2718 File Offset: 0x000B0918
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			InferenceMonitoringHelper.LogInfo(this, "Invoked.", new object[0]);
			this.InitializeAttributes();
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (!string.IsNullOrEmpty(lastFailedProbeResult.StateAttribute3))
			{
				if (lastFailedProbeResult.StateAttribute3.IndexOf("Train", StringComparison.OrdinalIgnoreCase) >= 0 && lastFailedProbeResult.StateAttribute3.IndexOf("DataCollection", StringComparison.OrdinalIgnoreCase) < 0)
				{
					this.componentsToDisable.Remove(DisableInferenceComponentResponder.InferenceComponent.DataCollection);
				}
				if (lastFailedProbeResult.StateAttribute3.IndexOf("DataCollection", StringComparison.OrdinalIgnoreCase) >= 0 && lastFailedProbeResult.StateAttribute3.IndexOf("Train", StringComparison.OrdinalIgnoreCase) < 0)
				{
					this.componentsToDisable.Remove(DisableInferenceComponentResponder.InferenceComponent.Training);
				}
			}
			List<string> list = new List<string>();
			foreach (DisableInferenceComponentResponder.InferenceComponent inferenceComponent in this.componentsToDisable)
			{
				string text = null;
				string text2 = null;
				string item = null;
				switch (inferenceComponent)
				{
				case DisableInferenceComponentResponder.InferenceComponent.DataCollection:
					text = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";
					text2 = "InferenceDataCollectionAssistantEnabledOverride";
					item = "msexchangemailboxassistants";
					break;
				case DisableInferenceComponentResponder.InferenceComponent.Training:
					text = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";
					text2 = "InferenceTrainingAssistantEnabledOverride";
					item = "msexchangemailboxassistants";
					break;
				case DisableInferenceComponentResponder.InferenceComponent.Classification:
					text = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference";
					text2 = "ClassificationPipelineEnabled";
					item = "msexchangedelivery";
					break;
				}
				InferenceMonitoringHelper.LogInfo(this, "Setting registry '{0}' - '{1}'.", new object[]
				{
					text,
					text2
				});
				this.SetDisableRegistryValue(text, text2);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			foreach (string text3 in list)
			{
				InferenceMonitoringHelper.LogInfo(this, "Restarting service '{0}'.", new object[]
				{
					text3
				});
				base.Definition.Attributes["WindowsServiceName"] = text3;
				base.WindowsServiceName = text3;
				RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RestartService, text3, this, true, cancellationToken, null);
				recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
				{
					this.InternalRestartService(startEntry, cancellationToken);
				});
			}
			InferenceMonitoringHelper.LogInfo(this, "Completed.", new object[0]);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000B297C File Offset: 0x000B0B7C
		protected override void InitializeAttributes()
		{
			base.InitializeAttributes();
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			string @string = attributeHelper.GetString("ComponentsToDisable", true, null);
			string[] array = @string.Split(new char[]
			{
				','
			});
			foreach (string value in array)
			{
				this.componentsToDisable.Add((DisableInferenceComponentResponder.InferenceComponent)Enum.Parse(typeof(DisableInferenceComponentResponder.InferenceComponent), value));
			}
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000B29FC File Offset: 0x000B0BFC
		private void SetDisableRegistryValue(string regKeyPath, string regKeyName)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(regKeyPath))
			{
				registryKey.SetValue(regKeyName, 0, RegistryValueKind.DWord);
			}
		}

		// Token: 0x0400149A RID: 5274
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400149B RID: 5275
		private static readonly string TypeName = typeof(DisableInferenceComponentResponder).FullName;

		// Token: 0x0400149C RID: 5276
		private List<DisableInferenceComponentResponder.InferenceComponent> componentsToDisable = new List<DisableInferenceComponentResponder.InferenceComponent>();

		// Token: 0x02000498 RID: 1176
		internal enum InferenceComponent
		{
			// Token: 0x0400149E RID: 5278
			DataCollection,
			// Token: 0x0400149F RID: 5279
			Training,
			// Token: 0x040014A0 RID: 5280
			Classification
		}
	}
}
