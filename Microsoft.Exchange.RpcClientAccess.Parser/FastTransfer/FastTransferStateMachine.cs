using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000174 RID: 372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct FastTransferStateMachine : IEquatable<FastTransferStateMachine>, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x0001998F File Offset: 0x00017B8F
		internal FastTransferStateMachine(IEnumerator<FastTransferStateMachine?> stateMachine)
		{
			this = new FastTransferStateMachine(null, stateMachine);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001999C File Offset: 0x00017B9C
		internal FastTransferStateMachine(IDisposable supportingObject, IEnumerator<FastTransferStateMachine?> stateMachine)
		{
			this.supportingObject = supportingObject;
			this.stateMachine = (stateMachine ?? FastTransferStateMachine.emptyStateMachine);
			this.disposeTracker = DisposeTracker.Get<FastTransferStateMachine>(default(FastTransferStateMachine));
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000199D4 File Offset: 0x00017BD4
		private bool IsValid
		{
			get
			{
				return this.stateMachine != null;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000199E2 File Offset: 0x00017BE2
		public override int GetHashCode()
		{
			if (!this.IsValid)
			{
				return 0;
			}
			return this.stateMachine.GetHashCode();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000199F9 File Offset: 0x00017BF9
		public override bool Equals(object obj)
		{
			return obj != null && this.Equals((FastTransferStateMachine)obj);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00019A0C File Offset: 0x00017C0C
		[DebuggerStepThrough]
		public FastTransferStateMachine? Step()
		{
			if (this.stateMachine.MoveNext())
			{
				return new FastTransferStateMachine?(this.stateMachine.Current ?? this);
			}
			return null;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00019A5C File Offset: 0x00017C5C
		public override string ToString()
		{
			if (!this.IsValid)
			{
				return base.ToString();
			}
			Type type = this.stateMachine.GetType();
			FieldInfo fieldInfo = type.GetTypeInfo().GetDeclaredField("<>1__state");
			if (fieldInfo == null || fieldInfo.IsStatic || fieldInfo.IsPublic)
			{
				fieldInfo = null;
			}
			Match match = FastTransferStateMachine.GetStateMachineNameParser().Match(type.FullName);
			return string.Format("{0}.{1}@{2}", match.Success ? match.Groups["supportingClass"].Value : "?", match.Success ? match.Groups["method"].Value : "?", (fieldInfo != null) ? fieldInfo.GetValue(this.stateMachine) : "?");
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00019B3C File Offset: 0x00017D3C
		public bool Equals(FastTransferStateMachine other)
		{
			return this.stateMachine == other.stateMachine;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00019B5A File Offset: 0x00017D5A
		public void Dispose()
		{
			Util.DisposeIfPresent(this.stateMachine);
			Util.DisposeIfPresent(this.supportingObject);
			Util.DisposeIfPresent(this.disposeTracker);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00019B7D File Offset: 0x00017D7D
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferStateMachine>(this);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00019B8A File Offset: 0x00017D8A
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00019B9F File Offset: 0x00017D9F
		private static Regex GetStateMachineNameParser()
		{
			if (FastTransferStateMachine.reStateMachineName == null)
			{
				FastTransferStateMachine.reStateMachineName = new Regex("^[.\\w]+\\.(FastTransfer)? (?'supportingClass'\\w+) \\+ [\\w.<>+]* [.<] (?'method'\\w+) >d__\\w+$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
			}
			return FastTransferStateMachine.reStateMachineName;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00019C10 File Offset: 0x00017E10
		private static IEnumerator<FastTransferStateMachine?> EmptyStateMachine()
		{
			yield break;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019C25 File Offset: 0x00017E25
		[Conditional("DEBUG")]
		private void CheckValid()
		{
			if (!this.IsValid)
			{
				throw new InvalidOperationException("State machine has not been initialized");
			}
		}

		// Token: 0x04000394 RID: 916
		private static readonly IEnumerator<FastTransferStateMachine?> emptyStateMachine = FastTransferStateMachine.EmptyStateMachine();

		// Token: 0x04000395 RID: 917
		private static Regex reStateMachineName;

		// Token: 0x04000396 RID: 918
		private readonly IDisposable supportingObject;

		// Token: 0x04000397 RID: 919
		private readonly IEnumerator<FastTransferStateMachine?> stateMachine;

		// Token: 0x04000398 RID: 920
		private readonly DisposeTracker disposeTracker;
	}
}
