using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000522 RID: 1314
	internal static class GenericDelegateCache<TAntecedentResult, TResult>
	{
		// Token: 0x04001A0B RID: 6667
		internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
			Task<TAntecedentResult> arg = (Task<TAntecedentResult>)wrappedWinner.Result;
			return func(arg);
		};

		// Token: 0x04001A0C RID: 6668
		internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
			Task<TAntecedentResult> obj = (Task<TAntecedentResult>)wrappedWinner.Result;
			action(obj);
			return default(TResult);
		};

		// Token: 0x04001A0D RID: 6669
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>[], TResult> func = (Func<Task<TAntecedentResult>[], TResult>)state;
			return func(wrappedAntecedents.Result);
		};

		// Token: 0x04001A0E RID: 6670
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task<TAntecedentResult>[]> action = (Action<Task<TAntecedentResult>[]>)state;
			action(wrappedAntecedents.Result);
			return default(TResult);
		};
	}
}
