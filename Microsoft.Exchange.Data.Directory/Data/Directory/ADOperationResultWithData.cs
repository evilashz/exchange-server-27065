using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200003C RID: 60
	internal class ADOperationResultWithData<TResult> : ADOperationResult where TResult : class
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00012D4F File Offset: 0x00010F4F
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00012D57 File Offset: 0x00010F57
		public TResult Data { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00012D60 File Offset: 0x00010F60
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00012D68 File Offset: 0x00010F68
		public string DomainController { get; private set; }

		// Token: 0x06000379 RID: 889 RVA: 0x00012D71 File Offset: 0x00010F71
		public ADOperationResultWithData(string dcName, TResult data, ADOperationErrorCode errorCode, Exception e) : base(errorCode, e)
		{
			this.DomainController = dcName;
			this.Data = data;
		}
	}
}
