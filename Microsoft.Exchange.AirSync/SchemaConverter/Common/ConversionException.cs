using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000182 RID: 386
	[Serializable]
	internal class ConversionException : LocalizedException
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x0005CD4A File Offset: 0x0005AF4A
		public ConversionException() : this(HttpStatusCode.InternalServerError, null, null)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0005CD59 File Offset: 0x0005AF59
		public ConversionException(string message) : this(HttpStatusCode.InternalServerError, message, null)
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0005CD68 File Offset: 0x0005AF68
		public ConversionException(string message, Exception innerException) : this(HttpStatusCode.InternalServerError, message, innerException)
		{
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0005CD77 File Offset: 0x0005AF77
		public ConversionException(HttpStatusCode httpStatusCode, string message) : this(httpStatusCode, message, null)
		{
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0005CD84 File Offset: 0x0005AF84
		public ConversionException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
			AirSyncDiagnostics.TraceError<HttpStatusCode, string, string>(ExTraceGlobals.CommonTracer, this, "ConversionException has been thrown. HttpStatusCode:'{0}', Message:'{1}', InnerException: '{2}'", httpStatusCode, (message != null) ? message : string.Empty, (innerException != null) ? innerException.Message : string.Empty);
			this.httpStatusCode = httpStatusCode;
			this.httpStatusCodeIsSet = true;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0005CDD8 File Offset: 0x0005AFD8
		public ConversionException(int airSyncStatusCode) : this(airSyncStatusCode, null, null)
		{
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0005CDE3 File Offset: 0x0005AFE3
		public ConversionException(int airSyncStatusCode, string message) : this(airSyncStatusCode, message, null)
		{
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0005CDEE File Offset: 0x0005AFEE
		public ConversionException(int airSyncStatusCode, Exception innerException) : this(airSyncStatusCode, null, innerException)
		{
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0005CDFC File Offset: 0x0005AFFC
		public ConversionException(int airSyncStatusCode, string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
			AirSyncDiagnostics.TraceWarning<int, string, string>(ExTraceGlobals.CommonTracer, this, "ConversionException has been thrown. ConversionStatusCode:'{0}', Message:'{1}', InnerException: '{2}'", airSyncStatusCode, (message != null) ? message : string.Empty, (innerException != null) ? innerException.Message : string.Empty);
			this.airSyncStatusCode = airSyncStatusCode;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0005CE4C File Offset: 0x0005B04C
		protected ConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			AirSyncDiagnostics.TraceError(ExTraceGlobals.CommonTracer, this, "ConversionException is being deserialized");
			this.httpStatusCode = (HttpStatusCode)info.GetValue("httpStatusCode", typeof(HttpStatusCode));
			this.airSyncStatusCode = (int)info.GetValue("airSyncStatusCode", typeof(int));
			this.httpStatusCodeIsSet = (bool)info.GetValue("isHttpStatusCodeSet", typeof(bool));
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0005CED1 File Offset: 0x0005B0D1
		public int ConversionStatusCode
		{
			get
			{
				AirSyncDiagnostics.Assert(!this.httpStatusCodeIsSet);
				return this.airSyncStatusCode;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0005CEE7 File Offset: 0x0005B0E7
		public HttpStatusCode HttpStatusCode
		{
			get
			{
				AirSyncDiagnostics.Assert(this.httpStatusCodeIsSet);
				return this.httpStatusCode;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0005CEFA File Offset: 0x0005B0FA
		public bool IsHttpStatusCodeSet
		{
			get
			{
				return this.httpStatusCodeIsSet;
			}
		}

		// Token: 0x04000ADD RID: 2781
		private int airSyncStatusCode;

		// Token: 0x04000ADE RID: 2782
		private HttpStatusCode httpStatusCode;

		// Token: 0x04000ADF RID: 2783
		private bool httpStatusCodeIsSet;
	}
}
