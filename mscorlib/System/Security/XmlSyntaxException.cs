using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x020001C0 RID: 448
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		// Token: 0x06001C14 RID: 7188 RVA: 0x00060CFA File Offset: 0x0005EEFA
		public XmlSyntaxException() : base(Environment.GetResourceString("XMLSyntax_InvalidSyntax"))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00060D17 File Offset: 0x0005EF17
		public XmlSyntaxException(string message) : base(message)
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00060D2B File Offset: 0x0005EF2B
		public XmlSyntaxException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00060D40 File Offset: 0x0005EF40
		public XmlSyntaxException(int lineNumber) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxError"), lineNumber))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00060D6D File Offset: 0x0005EF6D
		public XmlSyntaxException(int lineNumber, string message) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxErrorEx"), lineNumber, message))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00060D9B File Offset: 0x0005EF9B
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
