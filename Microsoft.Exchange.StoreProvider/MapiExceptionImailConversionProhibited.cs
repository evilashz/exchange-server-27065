using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000097 RID: 151
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionImailConversionProhibited : LocalizedException
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00012538 File Offset: 0x00010738
		internal MapiExceptionImailConversionProhibited(string message, int hresult, int ec, DiagnosticContext diagCtx, Exception innerException) : base(new LocalizedString(string.Concat(new string[]
		{
			"MapiExceptionImailConversionProhibited: ",
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

		// Token: 0x06000407 RID: 1031 RVA: 0x000125DC File Offset: 0x000107DC
		internal MapiExceptionImailConversionProhibited(string message) : base(new LocalizedString("MapiExceptionImailConversionProhibited: " + message))
		{
			this.lowLevelError = 0;
			base.ErrorCode = -2147467259;
			this.diagContext = new DiagnosticContext();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012611 File Offset: 0x00010811
		internal MapiExceptionImailConversionProhibited(string message, Exception innerException) : base(new LocalizedString("MapiExceptionImailConversionProhibited: " + message), innerException)
		{
			this.lowLevelError = 0;
			base.ErrorCode = -2147467259;
			this.diagContext = new DiagnosticContext();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00012647 File Offset: 0x00010847
		private MapiExceptionImailConversionProhibited(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lowLevelError = info.GetInt32("lowLevelError");
			this.diagContext = new DiagnosticContext(info, context);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001266F File Offset: 0x0001086F
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("lowLevelError", this.lowLevelError);
			((ISerializable)this.diagContext).GetObjectData(info, context);
			base.GetObjectData(info, context);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00012697 File Offset: 0x00010897
		public int LowLevelError
		{
			get
			{
				return this.lowLevelError;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001269F File Offset: 0x0001089F
		public new int HResult
		{
			get
			{
				return base.ErrorCode;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000126A7 File Offset: 0x000108A7
		public DiagnosticContext DiagCtx
		{
			get
			{
				return this.diagContext;
			}
		}

		// Token: 0x0400059F RID: 1439
		private int lowLevelError;

		// Token: 0x040005A0 RID: 1440
		private DiagnosticContext diagContext;
	}
}
