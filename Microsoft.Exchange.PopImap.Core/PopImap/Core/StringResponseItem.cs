using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200003B RID: 59
	internal class StringResponseItem : IResponseItem, IDisposeTrackable, IDisposable
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00011445 File Offset: 0x0000F645
		public StringResponseItem() : this(null, null)
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001144F File Offset: 0x0000F64F
		public StringResponseItem(string s) : this(s, null)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00011459 File Offset: 0x0000F659
		public StringResponseItem(string s, BaseSession.SendCompleteDelegate sendCompleteDelegate)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.stringResponse = s;
			this.sendCompleteDelegate = sendCompleteDelegate;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001147B File Offset: 0x0000F67B
		public BaseSession.SendCompleteDelegate SendCompleteDelegate
		{
			get
			{
				return this.sendCompleteDelegate;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00011483 File Offset: 0x0000F683
		public string StringResponse
		{
			get
			{
				return this.stringResponse;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001148B File Offset: 0x0000F68B
		public void BindData(string s, BaseSession.SendCompleteDelegate sendCompleteDelegate)
		{
			this.stringResponse = s;
			this.sendCompleteDelegate = sendCompleteDelegate;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001149C File Offset: 0x0000F69C
		public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			if (!session.StringResponseItemProcessor.DataBound)
			{
				lock (session.StringResponseItemProcessor)
				{
					if (!session.StringResponseItemProcessor.DataBound)
					{
						this.processor = session.StringResponseItemProcessor;
						session.StringResponseItemProcessor.BindData(this);
					}
				}
			}
			int num = 0;
			bool flag2 = false;
			try
			{
				num = session.StringResponseItemProcessor.GetNextChunk(this, session, out buffer, out offset);
				flag2 = true;
			}
			finally
			{
				if (num == 0 || !flag2)
				{
					session.StringResponseItemProcessor.UnbindData();
					this.processor = null;
				}
			}
			return num;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00011548 File Offset: 0x0000F748
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StringResponseItem>(this);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00011550 File Offset: 0x0000F750
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011565 File Offset: 0x0000F765
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00011574 File Offset: 0x0000F774
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.processor != null)
			{
				this.processor.UnbindData();
				this.processor = null;
			}
		}

		// Token: 0x04000202 RID: 514
		private string stringResponse;

		// Token: 0x04000203 RID: 515
		private BaseSession.SendCompleteDelegate sendCompleteDelegate;

		// Token: 0x04000204 RID: 516
		private DisposeTracker disposeTracker;

		// Token: 0x04000205 RID: 517
		private StringResponseItemProcessor processor;
	}
}
