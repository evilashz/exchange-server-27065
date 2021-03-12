using System;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE0 RID: 3552
	internal class DisposableResponse<TResponse> : IDisposableResponse<TResponse>, IDisposable
	{
		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0011A8C3 File Offset: 0x00118AC3
		// (set) Token: 0x06005B4E RID: 23374 RVA: 0x0011A8CB File Offset: 0x00118ACB
		internal IDisposable Command { get; set; }

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0011A8D4 File Offset: 0x00118AD4
		// (set) Token: 0x06005B50 RID: 23376 RVA: 0x0011A8DC File Offset: 0x00118ADC
		public virtual TResponse Response { get; set; }

		// Token: 0x06005B51 RID: 23377 RVA: 0x0011A8E5 File Offset: 0x00118AE5
		public DisposableResponse(IDisposable command, TResponse response)
		{
			this.Command = command;
			this.Response = response;
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x0011A8FB File Offset: 0x00118AFB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0011A90A File Offset: 0x00118B0A
		protected virtual void Dispose(bool disposing)
		{
			if (this.Command != null)
			{
				this.Command.Dispose();
				this.Command = null;
			}
		}
	}
}
