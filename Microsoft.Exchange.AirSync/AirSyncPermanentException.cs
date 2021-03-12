using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal class AirSyncPermanentException : LocalizedException
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000CC1F File Offset: 0x0000AE1F
		internal AirSyncPermanentException(LocalizedString message, Exception innerException, bool logEvent) : this(message, innerException, HttpStatusCode.InternalServerError, null, StatusCode.ServerError, logEvent)
		{
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000CC32 File Offset: 0x0000AE32
		internal AirSyncPermanentException(bool logEvent) : this(new LocalizedString(string.Empty), null, HttpStatusCode.InternalServerError, null, StatusCode.ServerError, logEvent)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000CC4E File Offset: 0x0000AE4E
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, XmlDocument xmlResponse, Exception innerException, bool logEvent) : this(new LocalizedString(string.Empty), innerException, HttpStatusCode.OK, xmlResponse, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000CC6A File Offset: 0x0000AE6A
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, XmlDocument xmlResponse, LocalizedString message, bool logEvent) : this(message, null, HttpStatusCode.OK, xmlResponse, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000CC7D File Offset: 0x0000AE7D
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, XmlDocument xmlResponse, LocalizedString message, Exception innerException, bool logEvent) : this(message, innerException, HttpStatusCode.OK, xmlResponse, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000CC91 File Offset: 0x0000AE91
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, LocalizedString message, bool logEvent) : this(message, null, HttpStatusCode.OK, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000CCA3 File Offset: 0x0000AEA3
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, bool logEvent) : this(new LocalizedString(string.Empty), null, HttpStatusCode.OK, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000CCBE File Offset: 0x0000AEBE
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, LocalizedString message, Exception innerException, bool logEvent) : this(message, innerException, HttpStatusCode.OK, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000CCD1 File Offset: 0x0000AED1
		internal AirSyncPermanentException(StatusCode airSyncStatusCode, Exception innerException, bool logEvent) : this(new LocalizedString(string.Empty), innerException, HttpStatusCode.OK, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		internal AirSyncPermanentException(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode, Exception innerException, bool logEvent) : this(new LocalizedString(string.Empty), innerException, httpStatusCode, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000CD04 File Offset: 0x0000AF04
		internal AirSyncPermanentException(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode, LocalizedString message, bool logEvent) : this(message, null, httpStatusCode, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000CD13 File Offset: 0x0000AF13
		internal AirSyncPermanentException(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode, LocalizedString message, Exception innerException, bool logEvent) : this(message, innerException, httpStatusCode, null, airSyncStatusCode, logEvent)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000CD24 File Offset: 0x0000AF24
		protected AirSyncPermanentException(SerializationInfo info, StreamingContext context)
		{
			this.httpStatusCode = HttpStatusCode.InternalServerError;
			this.logStackTraceToEventLog = true;
			base..ctor(info, context);
			AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "AirSyncPermanentException is being deserialized");
			this.httpStatusCode = (HttpStatusCode)info.GetValue("httpStatusCode", typeof(HttpStatusCode));
			this.airSyncStatusCode = (StatusCode)info.GetValue("airSyncStatusCode", typeof(StatusCode));
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "xmlResponse")
				{
					this.xmlResponse = (XmlDocument)serializationEntry.Value;
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		private AirSyncPermanentException(LocalizedString message, Exception innerException, HttpStatusCode httpStatusCode, XmlDocument xmlResponse, StatusCode airSyncStatusCode, bool logEvent)
		{
			this.httpStatusCode = HttpStatusCode.InternalServerError;
			this.logStackTraceToEventLog = true;
			base..ctor(message, innerException);
			this.httpStatusCode = httpStatusCode;
			this.xmlResponse = xmlResponse;
			this.airSyncStatusCode = airSyncStatusCode;
			if (message.IsEmpty)
			{
				this.logExceptionToEventLog = false;
			}
			else
			{
				this.logExceptionToEventLog = logEvent;
			}
			AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(this);
			AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "AirSyncPermanentException: {0}", arg);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000CE48 File Offset: 0x0000B048
		internal int AirSyncStatusCodeInInt
		{
			get
			{
				return (int)this.airSyncStatusCode;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000CE50 File Offset: 0x0000B050
		internal StatusCode AirSyncStatusCode
		{
			get
			{
				return this.airSyncStatusCode;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000CE58 File Offset: 0x0000B058
		internal HttpStatusCode HttpStatusCode
		{
			get
			{
				return this.httpStatusCode;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000CE60 File Offset: 0x0000B060
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000CE68 File Offset: 0x0000B068
		protected internal bool LogExceptionToEventLog
		{
			get
			{
				return this.logExceptionToEventLog;
			}
			protected set
			{
				this.logExceptionToEventLog = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000CE71 File Offset: 0x0000B071
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000CE79 File Offset: 0x0000B079
		internal bool LogStackTraceToEventLog
		{
			get
			{
				return this.logStackTraceToEventLog;
			}
			set
			{
				this.logStackTraceToEventLog = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000CE82 File Offset: 0x0000B082
		internal XmlDocument XmlResponse
		{
			get
			{
				return this.xmlResponse;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000CE8A File Offset: 0x0000B08A
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000CE92 File Offset: 0x0000B092
		internal string ErrorStringForProtocolLogger { get; set; }

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.ErrorStringForProtocolLogger))
			{
				return base.ToString();
			}
			LocalizedString localizedString = base.LocalizedString;
			if (!base.LocalizedString.IsEmpty)
			{
				return string.Format("{0}\r\n{1}\r\n{2}", base.ToString(), base.LocalizedString, this.ErrorStringForProtocolLogger);
			}
			return string.Format("{0}\r\n{1}", base.ToString(), this.ErrorStringForProtocolLogger);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000CF0C File Offset: 0x0000B10C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000CF0E File Offset: 0x0000B10E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
		}

		// Token: 0x0400020B RID: 523
		private StatusCode airSyncStatusCode;

		// Token: 0x0400020C RID: 524
		private HttpStatusCode httpStatusCode;

		// Token: 0x0400020D RID: 525
		private bool logExceptionToEventLog;

		// Token: 0x0400020E RID: 526
		private bool logStackTraceToEventLog;

		// Token: 0x0400020F RID: 527
		[OptionalField]
		private XmlDocument xmlResponse;
	}
}
