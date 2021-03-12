using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x0200073A RID: 1850
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ServerFault
	{
		// Token: 0x0600520E RID: 21006 RVA: 0x0011F459 File Offset: 0x0011D659
		internal ServerFault(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0011F468 File Offset: 0x0011D668
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x0011F485 File Offset: 0x0011D685
		// (set) Token: 0x06005211 RID: 21009 RVA: 0x0011F48D File Offset: 0x0011D68D
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x0011F496 File Offset: 0x0011D696
		// (set) Token: 0x06005213 RID: 21011 RVA: 0x0011F49E File Offset: 0x0011D69E
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x0011F4A7 File Offset: 0x0011D6A7
		// (set) Token: 0x06005215 RID: 21013 RVA: 0x0011F4AF File Offset: 0x0011D6AF
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06005216 RID: 21014 RVA: 0x0011F4B8 File Offset: 0x0011D6B8
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04002425 RID: 9253
		private string exceptionType;

		// Token: 0x04002426 RID: 9254
		private string message;

		// Token: 0x04002427 RID: 9255
		private string stackTrace;

		// Token: 0x04002428 RID: 9256
		private Exception exception;
	}
}
