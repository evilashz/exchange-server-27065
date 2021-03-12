using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	// Token: 0x02000244 RID: 580
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		// Token: 0x060020B3 RID: 8371 RVA: 0x000728C8 File Offset: 0x00070AC8
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000728DB File Offset: 0x00070ADB
		public CryptographicUnexpectedOperationException(string message) : base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000728EF File Offset: 0x00070AEF
		public CryptographicUnexpectedOperationException(string format, string insert) : base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0007290E File Offset: 0x00070B0E
		public CryptographicUnexpectedOperationException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00072923 File Offset: 0x00070B23
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
