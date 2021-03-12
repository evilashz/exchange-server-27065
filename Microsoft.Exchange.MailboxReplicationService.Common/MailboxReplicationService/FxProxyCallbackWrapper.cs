using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000241 RID: 577
	internal class FxProxyCallbackWrapper : DisposableWrapper<IFxProxy>, IFxProxy, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x0003EC35 File Offset: 0x0003CE35
		public FxProxyCallbackWrapper(IFxProxy destProxy, bool ownsObject, Action<TimeSpan> updateDuration) : base(destProxy, ownsObject)
		{
			this.updateDuration = updateDuration;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0003EC46 File Offset: 0x0003CE46
		byte[] IMapiFxProxy.GetObjectData()
		{
			return base.WrappedObject.GetObjectData();
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0003EC54 File Offset: 0x0003CE54
		void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				base.WrappedObject.ProcessRequest(opCode, request);
			}
			finally
			{
				this.updateDuration(stopwatch.Elapsed);
				stopwatch.Stop();
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0003ECA0 File Offset: 0x0003CEA0
		void IFxProxy.Flush()
		{
			base.WrappedObject.Flush();
		}

		// Token: 0x04000C60 RID: 3168
		private Action<TimeSpan> updateDuration;
	}
}
