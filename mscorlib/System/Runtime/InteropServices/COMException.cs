using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000917 RID: 2327
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class COMException : ExternalException
	{
		// Token: 0x06005F48 RID: 24392 RVA: 0x001474C5 File Offset: 0x001456C5
		[__DynamicallyInvokable]
		public COMException() : base(Environment.GetResourceString("Arg_COMException"))
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x001474E2 File Offset: 0x001456E2
		[__DynamicallyInvokable]
		public COMException(string message) : base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x001474F6 File Offset: 0x001456F6
		[__DynamicallyInvokable]
		public COMException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x0014750B File Offset: 0x0014570B
		[__DynamicallyInvokable]
		public COMException(string message, int errorCode) : base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x0014751B File Offset: 0x0014571B
		[SecuritySafeCritical]
		internal COMException(int hresult) : base(Win32Native.GetMessage(hresult))
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x00147530 File Offset: 0x00145730
		internal COMException(string message, int hresult, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00147541 File Offset: 0x00145741
		protected COMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x0014754C File Offset: 0x0014574C
		public override string ToString()
		{
			string message = this.Message;
			string str = base.GetType().ToString();
			string text = str + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (message != null && message.Length > 0)
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
