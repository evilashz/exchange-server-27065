using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000061 RID: 97
	internal abstract class FastTransferServerObject<TContext> : ServerObject, WatsonHelper.IProvideWatsonReportData where TContext : BaseObject
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001DFA3 File Offset: 0x0001C1A3
		protected TContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001DFAB File Offset: 0x0001C1AB
		protected FastTransferServerObject(TContext context, Logon logon) : base(logon)
		{
			this.context = context;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001DFBB File Offset: 0x0001C1BB
		protected virtual WatsonReportActionType WatsonReportActionType
		{
			get
			{
				return WatsonReportActionType.FastTransferState;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001DFBE File Offset: 0x0001C1BE
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferServerObject<TContext>>(this);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001DFC6 File Offset: 0x0001C1C6
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.context);
			base.InternalDispose();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001DFDE File Offset: 0x0001C1DE
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			base.CheckDisposed();
			return string.Format("FastTransferContext: {0}", this.Context);
		}

		// Token: 0x0400016A RID: 362
		private TContext context;
	}
}
