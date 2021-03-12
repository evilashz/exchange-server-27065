using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200023D RID: 573
	internal class FxProxyWrapper : WrapperBase<IMapiFxProxy>, IFxProxy, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001E2F RID: 7727 RVA: 0x0003E713 File Offset: 0x0003C913
		public FxProxyWrapper(IMapiFxProxy proxy, CommonUtils.CreateContextDelegate createContext) : base(proxy, createContext)
		{
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0003E740 File Offset: 0x0003C940
		byte[] IMapiFxProxy.GetObjectData()
		{
			byte[] result = null;
			base.CreateContext("IMapiFxProxy.GetObjectData", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetObjectData();
			}, true);
			return result;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
		{
			base.CreateContext("IMapiFxProxy.ProcessRequest", new DataContext[]
			{
				new SimpleValueDataContext("OpCode", opCode),
				new SimpleValueDataContext("DataLength", (request != null) ? request.Length : 0)
			}).Execute(delegate
			{
				this.WrappedObject.ProcessRequest(opCode, request);
			}, true);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0003E86A File Offset: 0x0003CA6A
		void IFxProxy.Flush()
		{
			base.CreateContext("IFxProxy.Flush", new DataContext[0]).Execute(delegate
			{
				IFxProxy fxProxy = base.WrappedObject as IFxProxy;
				if (fxProxy != null)
				{
					fxProxy.Flush();
				}
			}, true);
		}
	}
}
