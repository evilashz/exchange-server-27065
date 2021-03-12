using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000237 RID: 567
	internal class FxProxyBudgetWrapper : BudgetWrapperBase<IMapiFxProxy>, IFxProxy, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001E0F RID: 7695 RVA: 0x0003DF64 File Offset: 0x0003C164
		public FxProxyBudgetWrapper(IMapiFxProxy destProxy, bool ownsObject, Func<IDisposable> createCostHandleDelegate, Action<uint> chargeDelegate) : base(destProxy, ownsObject, createCostHandleDelegate, chargeDelegate)
		{
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0003DF74 File Offset: 0x0003C174
		byte[] IMapiFxProxy.GetObjectData()
		{
			byte[] objectData;
			using (base.CreateCostHandle())
			{
				objectData = base.WrappedObject.GetObjectData();
			}
			return objectData;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0003DFB8 File Offset: 0x0003C1B8
		void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
		{
			using (base.CreateCostHandle())
			{
				base.WrappedObject.ProcessRequest(opCode, request);
			}
			if (request != null)
			{
				base.Charge((uint)request.Length);
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0003E00C File Offset: 0x0003C20C
		void IFxProxy.Flush()
		{
			IFxProxy fxProxy = base.WrappedObject as IFxProxy;
			if (fxProxy != null)
			{
				using (base.CreateCostHandle())
				{
					fxProxy.Flush();
				}
			}
		}
	}
}
