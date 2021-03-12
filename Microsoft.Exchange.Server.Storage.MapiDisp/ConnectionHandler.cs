using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x0200001B RID: 27
	public class ConnectionHandler : DisposableBase, IConnectionHandler, IDisposable
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0003A07F File Offset: 0x0003827F
		private ConnectionHandler(MapiSession mapiSession)
		{
			this.ropHandler = new RopHandler(mapiSession);
			this.notificationHandler = new NotificationHandler(mapiSession);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0003A09F File Offset: 0x0003829F
		public static ConnectionHandler Create(MapiSession mapiSession)
		{
			return new ConnectionHandler(mapiSession);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0003A0A7 File Offset: 0x000382A7
		IRopHandler IConnectionHandler.RopHandler
		{
			get
			{
				return this.ropHandler;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0003A0AF File Offset: 0x000382AF
		INotificationHandler IConnectionHandler.NotificationHandler
		{
			get
			{
				return this.notificationHandler;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0003A0B8 File Offset: 0x000382B8
		void IConnectionHandler.BeginRopProcessing(AuxiliaryData auxiliaryData)
		{
			RopHandler ropHandler = (RopHandler)this.ropHandler;
			MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)ropHandler.MapiContext.Diagnostics;
			JET_THREADSTATS threadStats;
			Factory.GetDatabaseThreadStats(out threadStats);
			mapiExecutionDiagnostics.MapiExMonLogger.BeginRopProcessing(threadStats);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0003A0F8 File Offset: 0x000382F8
		void IConnectionHandler.EndRopProcessing(AuxiliaryData auxiliaryData)
		{
			RopHandler ropHandler = (RopHandler)this.ropHandler;
			MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)ropHandler.MapiContext.Diagnostics;
			mapiExecutionDiagnostics.MapiExMonLogger.EndRopProcessing();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0003A130 File Offset: 0x00038330
		void IConnectionHandler.LogInputRops(IEnumerable<RopId> rops)
		{
			RopHandler ropHandler = (RopHandler)this.ropHandler;
			MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)ropHandler.MapiContext.Diagnostics;
			mapiExecutionDiagnostics.MapiExMonLogger.LogInputRops(rops);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0003A168 File Offset: 0x00038368
		void IConnectionHandler.LogPrepareForRop(RopId ropId)
		{
			RopHandler ropHandler = (RopHandler)this.ropHandler;
			MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)ropHandler.MapiContext.Diagnostics;
			mapiExecutionDiagnostics.OnRopBegin(ropId);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0003A19C File Offset: 0x0003839C
		void IConnectionHandler.LogCompletedRop(RopId ropId, ErrorCode errorCode)
		{
			RopHandler ropHandler = (RopHandler)this.ropHandler;
			MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)ropHandler.MapiContext.Diagnostics;
			mapiExecutionDiagnostics.OnRopEnd(ropId, errorCode);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0003A1CE File Offset: 0x000383CE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectionHandler>(this);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0003A1D6 File Offset: 0x000383D6
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.ropHandler != null)
			{
				this.ropHandler.Dispose();
				this.ropHandler = null;
			}
		}

		// Token: 0x04000191 RID: 401
		private IRopHandler ropHandler;

		// Token: 0x04000192 RID: 402
		private INotificationHandler notificationHandler;
	}
}
