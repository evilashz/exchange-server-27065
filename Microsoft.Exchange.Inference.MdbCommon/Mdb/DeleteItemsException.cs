using System;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal sealed class DeleteItemsException : Exception
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00003084 File Offset: 0x00001284
		public DeleteItemsException(string message) : base(message)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000308D File Offset: 0x0000128D
		public DeleteItemsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003097 File Offset: 0x00001297
		public override string ToString()
		{
			return "Deletion error:" + base.ToString();
		}
	}
}
