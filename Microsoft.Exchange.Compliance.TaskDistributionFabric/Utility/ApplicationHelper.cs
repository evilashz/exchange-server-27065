using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Utility
{
	// Token: 0x02000022 RID: 34
	internal static class ApplicationHelper
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00005148 File Offset: 0x00003348
		public static bool TryPreprocess(ComplianceMessage target, WorkPayload workPayload, out WorkPayload processedPayload, out FaultDefinition faultDefinition, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			processedPayload = null;
			WorkPayload newPayload = null;
			IApplicationPlugin plugin;
			if (Registry.Instance.TryGetInstance<IApplicationPlugin>(RegistryComponent.Application, target.WorkDefinitionType, out plugin, out faultDefinition, "TryPreprocess", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Utility\\ApplicationHelper.cs", 51))
			{
				ExceptionHandler.Gray.TryRun(delegate
				{
					newPayload = plugin.Preprocess(target, workPayload);
				}, TaskDistributionSettings.ApplicationExecutionTime, out faultDefinition, target, null, default(CancellationToken), null, callerMember, callerFilePath, callerLineNumber);
				if (ExceptionHandler.IsFaulted(target))
				{
					faultDefinition = ExceptionHandler.GetFaultDefinition(target);
				}
			}
			processedPayload = newPayload;
			return processedPayload != null && faultDefinition == null;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005234 File Offset: 0x00003434
		public static bool TryDoWork(ComplianceMessage target, WorkPayload workPayload, out WorkPayload resultPayload, out FaultDefinition faultDefinition, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			resultPayload = null;
			WorkPayload newPayload = null;
			IApplicationPlugin plugin;
			if (Registry.Instance.TryGetInstance<IApplicationPlugin>(RegistryComponent.Application, target.WorkDefinitionType, out plugin, out faultDefinition, "TryDoWork", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Utility\\ApplicationHelper.cs", 104))
			{
				ExceptionHandler.Gray.TryRun(delegate
				{
					newPayload = plugin.DoWork(target, workPayload);
				}, TaskDistributionSettings.ApplicationExecutionTime, out faultDefinition, target, null, default(CancellationToken), null, callerMember, callerFilePath, callerLineNumber);
				if (ExceptionHandler.IsFaulted(target))
				{
					faultDefinition = ExceptionHandler.GetFaultDefinition(target);
				}
			}
			resultPayload = newPayload;
			return resultPayload != null && faultDefinition == null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005320 File Offset: 0x00003520
		public static bool TryRecordResult(ComplianceMessage target, ResultBase existingResult, WorkPayload workPayload, out ResultBase processedResult, out FaultDefinition faultDefinition, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			processedResult = null;
			ResultBase newResult = null;
			IApplicationPlugin plugin;
			if (Registry.Instance.TryGetInstance<IApplicationPlugin>(RegistryComponent.Application, target.WorkDefinitionType, out plugin, out faultDefinition, "TryRecordResult", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Utility\\ApplicationHelper.cs", 159))
			{
				ExceptionHandler.Gray.TryRun(delegate
				{
					newResult = plugin.RecordResult(existingResult, workPayload);
				}, TaskDistributionSettings.ApplicationExecutionTime, out faultDefinition, target, null, default(CancellationToken), null, callerMember, callerFilePath, callerLineNumber);
			}
			processedResult = newResult;
			return processedResult != null && faultDefinition == null;
		}
	}
}
