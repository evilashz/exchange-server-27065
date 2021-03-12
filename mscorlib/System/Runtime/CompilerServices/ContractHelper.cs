using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000874 RID: 2164
	[__DynamicallyInvokable]
	public static class ContractHelper
	{
		// Token: 0x06005C51 RID: 23633 RVA: 0x001444C8 File Offset: 0x001426C8
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			string result = "Contract failed";
			ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref result);
			return result;
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x001444E7 File Offset: 0x001426E7
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x001444F4 File Offset: 0x001426F4
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					failureKind
				}), "failureKind");
			}
			string text = "contract failed.";
			ContractFailedEventArgs contractFailedEventArgs = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			string text2;
			try
			{
				text = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
				EventHandler<ContractFailedEventArgs> eventHandler = ContractHelper.contractFailedEvent;
				if (eventHandler != null)
				{
					contractFailedEventArgs = new ContractFailedEventArgs(failureKind, text, conditionText, innerException);
					foreach (EventHandler<ContractFailedEventArgs> eventHandler2 in eventHandler.GetInvocationList())
					{
						try
						{
							eventHandler2(null, contractFailedEventArgs);
						}
						catch (Exception thrownDuringHandler)
						{
							contractFailedEventArgs.thrownDuringHandler = thrownDuringHandler;
							contractFailedEventArgs.SetUnwind();
						}
					}
					if (contractFailedEventArgs.Unwind)
					{
						if (Environment.IsCLRHosted)
						{
							ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, text, conditionText, innerException);
						}
						if (innerException == null)
						{
							innerException = contractFailedEventArgs.thrownDuringHandler;
						}
						throw new ContractException(failureKind, text, userMessage, conditionText, innerException);
					}
				}
			}
			finally
			{
				if (contractFailedEventArgs != null && contractFailedEventArgs.Handled)
				{
					text2 = null;
				}
				else
				{
					text2 = text;
				}
			}
			resultFailureMessage = text2;
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x00144600 File Offset: 0x00142800
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			if (Environment.IsCLRHosted)
			{
				ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			if (!Environment.UserInteractive)
			{
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind));
			Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06005C55 RID: 23637 RVA: 0x00144658 File Offset: 0x00142858
		// (remove) Token: 0x06005C56 RID: 23638 RVA: 0x001446B0 File Offset: 0x001428B0
		internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
		{
			[SecurityCritical]
			add
			{
				RuntimeHelpers.PrepareContractedDelegate(value);
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Combine(ContractHelper.contractFailedEvent, value);
				}
			}
			[SecurityCritical]
			remove
			{
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Remove(ContractHelper.contractFailedEvent, value);
				}
			}
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x00144704 File Offset: 0x00142904
		private static string GetResourceNameForFailure(ContractFailureKind failureKind)
		{
			string result;
			switch (failureKind)
			{
			case ContractFailureKind.Precondition:
				result = "PreconditionFailed";
				break;
			case ContractFailureKind.Postcondition:
				result = "PostconditionFailed";
				break;
			case ContractFailureKind.PostconditionOnException:
				result = "PostconditionOnExceptionFailed";
				break;
			case ContractFailureKind.Invariant:
				result = "InvariantFailed";
				break;
			case ContractFailureKind.Assert:
				result = "AssertionFailed";
				break;
			case ContractFailureKind.Assume:
				result = "AssumptionFailed";
				break;
			default:
				Contract.Assume(false, "Unreachable code");
				result = "AssumptionFailed";
				break;
			}
			return result;
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00144778 File Offset: 0x00142978
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
		{
			string text = ContractHelper.GetResourceNameForFailure(failureKind);
			string resourceString;
			if (!string.IsNullOrEmpty(conditionText))
			{
				text += "_Cnd";
				resourceString = Environment.GetResourceString(text, new object[]
				{
					conditionText
				});
			}
			else
			{
				resourceString = Environment.GetResourceString(text);
			}
			if (!string.IsNullOrEmpty(userMessage))
			{
				return resourceString + "  " + userMessage;
			}
			return resourceString;
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x001447D0 File Offset: 0x001429D0
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
		{
			string exceptionAsString = null;
			if (innerException != null)
			{
				exceptionAsString = innerException.ToString();
			}
			Environment.TriggerCodeContractFailure(failureKind, message, conditionText, exceptionAsString);
		}

		// Token: 0x04002951 RID: 10577
		private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;

		// Token: 0x04002952 RID: 10578
		private static readonly object lockObject = new object();

		// Token: 0x04002953 RID: 10579
		internal const int COR_E_CODECONTRACTFAILED = -2146233022;
	}
}
