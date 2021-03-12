using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D8 RID: 216
	[ComVisible(true)]
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0002A85F File Offset: 0x00028A5F
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException._duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException._duplicateWaitObjectMessage = Environment.GetResourceString("Arg_DuplicateWaitObjectException");
				}
				return DuplicateWaitObjectException._duplicateWaitObjectMessage;
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002A882 File Offset: 0x00028A82
		public DuplicateWaitObjectException() : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002A89A File Offset: 0x00028A9A
		public DuplicateWaitObjectException(string parameterName) : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002A8B3 File Offset: 0x00028AB3
		public DuplicateWaitObjectException(string parameterName, string message) : base(message, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		public DuplicateWaitObjectException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233047);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002A8DD File Offset: 0x00028ADD
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000567 RID: 1383
		private static volatile string _duplicateWaitObjectMessage;
	}
}
