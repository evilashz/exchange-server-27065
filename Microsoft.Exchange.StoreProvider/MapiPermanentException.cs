using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class MapiPermanentException : LocalizedException
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		internal MapiPermanentException(string realExceptionClassName, string message, int hresult, int ec, DiagnosticContext diagCtx, Exception innerException) : base(new LocalizedString(string.Concat(new string[]
		{
			realExceptionClassName,
			": ",
			message,
			" (hr=0x",
			hresult.ToString("x"),
			", ec=",
			ec.ToString(),
			")",
			(diagCtx != null) ? ("\n" + diagCtx.ToString()) : ""
		})), innerException)
		{
			this.lowLevelError = ec;
			base.ErrorCode = hresult;
			this.diagContext = ((diagCtx != null) ? diagCtx : new DiagnosticContext());
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AD72 File Offset: 0x00008F72
		internal MapiPermanentException(string realExceptionClassName, string message) : base(new LocalizedString(realExceptionClassName + ": " + message))
		{
			this.lowLevelError = 0;
			base.ErrorCode = -2147467259;
			this.diagContext = new DiagnosticContext();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		internal MapiPermanentException(string realExceptionClassName, string message, Exception innerException) : base(new LocalizedString(realExceptionClassName + ": " + message), innerException)
		{
			this.lowLevelError = 0;
			base.ErrorCode = -2147467259;
			this.diagContext = new DiagnosticContext();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000ADDF File Offset: 0x00008FDF
		protected MapiPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lowLevelError = info.GetInt32("lowLevelError");
			this.diagContext = new DiagnosticContext(info, context);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AE07 File Offset: 0x00009007
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("lowLevelError", this.lowLevelError);
			((ISerializable)this.diagContext).GetObjectData(info, context);
			base.GetObjectData(info, context);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000AE2F File Offset: 0x0000902F
		public int LowLevelError
		{
			get
			{
				return this.lowLevelError;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000AE37 File Offset: 0x00009037
		public new int HResult
		{
			get
			{
				return base.ErrorCode;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000AE3F File Offset: 0x0000903F
		public DiagnosticContext DiagCtx
		{
			get
			{
				return this.diagContext;
			}
		}

		// Token: 0x0400045C RID: 1116
		private int lowLevelError;

		// Token: 0x0400045D RID: 1117
		private DiagnosticContext diagContext;
	}
}
