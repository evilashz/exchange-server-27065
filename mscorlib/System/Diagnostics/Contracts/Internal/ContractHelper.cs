using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics.Contracts.Internal
{
	// Token: 0x020003EC RID: 1004
	[Obsolete("Use the ContractHelper class in the System.Runtime.CompilerServices namespace instead.")]
	[__DynamicallyInvokable]
	public static class ContractHelper
	{
		// Token: 0x0600330C RID: 13068 RVA: 0x000C222D File Offset: 0x000C042D
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			return ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000C2238 File Offset: 0x000C0438
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailure(kind, displayMessage, userMessage, conditionText, innerException);
		}
	}
}
