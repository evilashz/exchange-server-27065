using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200023B RID: 571
	internal class WrapperBase<T> : DisposableWrapper<T> where T : class, IDisposable
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		public WrapperBase(T wrappedObject, CommonUtils.CreateContextDelegate createContext) : base(wrappedObject, true)
		{
			this.ProviderInfo = new ProviderInfo();
			if (createContext == null)
			{
				createContext = new CommonUtils.CreateContextDelegate(this.DefaultCreateContext);
			}
			this.CreateContext = createContext;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0003E2F5 File Offset: 0x0003C4F5
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x0003E2FD File Offset: 0x0003C4FD
		public CommonUtils.CreateContextDelegate CreateContext { get; protected set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0003E306 File Offset: 0x0003C506
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x0003E30E File Offset: 0x0003C50E
		public ProviderInfo ProviderInfo { get; protected set; }

		// Token: 0x06001E22 RID: 7714 RVA: 0x0003E334 File Offset: 0x0003C534
		public void UpdateDuration(string callName, TimeSpan duration)
		{
			DurationInfo durationInfo = this.ProviderInfo.Durations.Find((DurationInfo d) => d.Name.Equals(callName));
			if (durationInfo != null)
			{
				durationInfo.Duration = durationInfo.Duration.Add(duration);
				return;
			}
			this.ProviderInfo.Durations.Add(new DurationInfo
			{
				Name = callName,
				Duration = duration
			});
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0003E3CC File Offset: 0x0003C5CC
		protected override void InternalDispose(bool calledFromDispose)
		{
			this.CreateContext("WrapperBase.Dispose", new DataContext[0]).Execute(delegate
			{
				this.<>n__FabricatedMethod7(calledFromDispose);
			}, true);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0003E415 File Offset: 0x0003C615
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WrapperBase<T>>(this);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0003E41D File Offset: 0x0003C61D
		private ExecutionContextWrapper DefaultCreateContext(string callName, params DataContext[] additionalContexts)
		{
			return new ExecutionContextWrapper(new CommonUtils.UpdateDuration(this.UpdateDuration), callName, additionalContexts);
		}
	}
}
