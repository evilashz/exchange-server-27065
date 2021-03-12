using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000919 RID: 2329
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		// Token: 0x06005F5C RID: 24412 RVA: 0x001476AC File Offset: 0x001458AC
		public ExternalException() : base(Environment.GetResourceString("Arg_ExternalException"))
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x001476C9 File Offset: 0x001458C9
		public ExternalException(string message) : base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x001476DD File Offset: 0x001458DD
		public ExternalException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x001476F2 File Offset: 0x001458F2
		public ExternalException(string message, int errorCode) : base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x00147702 File Offset: 0x00145902
		protected ExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06005F61 RID: 24417 RVA: 0x0014770C File Offset: 0x0014590C
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x00147714 File Offset: 0x00145914
		public override string ToString()
		{
			string message = this.Message;
			string str = base.GetType().ToString();
			string text = str + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
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
