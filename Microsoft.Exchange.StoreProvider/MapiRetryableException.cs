using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class MapiRetryableException : TransientException
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000AE5C File Offset: 0x0000905C
		public MapiRetryableException(string realExceptionClassName, string message, int hresult, int ec, DiagnosticContext diagCtx, Exception innerException) : base(new LocalizedString(string.Concat(new string[]
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

		// Token: 0x06000254 RID: 596 RVA: 0x0000AF06 File Offset: 0x00009106
		internal MapiRetryableException(string realExceptionClassName, string message) : base(new LocalizedString(realExceptionClassName + ": " + message))
		{
			this.lowLevelError = 0;
			base.ErrorCode = -2147467259;
			this.diagContext = new DiagnosticContext();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000AF3C File Offset: 0x0000913C
		internal MapiRetryableException(string realExceptionClassName, string message, Exception innerException) : base(new LocalizedString(realExceptionClassName + ": " + message), innerException)
		{
			this.lowLevelError = 0;
			base.ErrorCode = -2147467259;
			this.diagContext = new DiagnosticContext();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AF73 File Offset: 0x00009173
		protected MapiRetryableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lowLevelError = info.GetInt32("lowLevelError");
			this.diagContext = new DiagnosticContext(info, context);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000AF9B File Offset: 0x0000919B
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("lowLevelError", this.lowLevelError);
			((ISerializable)this.diagContext).GetObjectData(info, context);
			base.GetObjectData(info, context);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000AFC3 File Offset: 0x000091C3
		public int LowLevelError
		{
			get
			{
				return this.lowLevelError;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000AFCB File Offset: 0x000091CB
		public new int HResult
		{
			get
			{
				return base.ErrorCode;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000AFD3 File Offset: 0x000091D3
		public DiagnosticContext DiagCtx
		{
			get
			{
				return this.diagContext;
			}
		}

		// Token: 0x0400045E RID: 1118
		private int lowLevelError;

		// Token: 0x0400045F RID: 1119
		private DiagnosticContext diagContext;
	}
}
