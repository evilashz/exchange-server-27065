using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200080A RID: 2058
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x0013822A File Offset: 0x0013642A
		internal virtual IntPtr ContextID
		{
			get
			{
				return new IntPtr((long)this._ContextID);
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x0013823C File Offset: 0x0013643C
		internal virtual int DomainID
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060058C5 RID: 22725 RVA: 0x00138244 File Offset: 0x00136444
		internal virtual string ProcessGuid
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0013824C File Offset: 0x0013644C
		internal CrossAppDomainData(IntPtr ctxId, int domainID, string processGuid)
		{
			this._DomainID = domainID;
			this._processGuid = processGuid;
			this._ContextID = ctxId.ToInt64();
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x00138280 File Offset: 0x00136480
		internal bool IsFromThisProcess()
		{
			return Identity.ProcessGuid.Equals(this._processGuid);
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x00138292 File Offset: 0x00136492
		[SecurityCritical]
		internal bool IsFromThisAppDomain()
		{
			return this.IsFromThisProcess() && Thread.GetDomain().GetId() == this._DomainID;
		}

		// Token: 0x0400282E RID: 10286
		private object _ContextID = 0;

		// Token: 0x0400282F RID: 10287
		private int _DomainID;

		// Token: 0x04002830 RID: 10288
		private string _processGuid;
	}
}
