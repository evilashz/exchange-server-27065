using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200001F RID: 31
	internal class AirSyncResponse : IAirSyncResponse
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000E1AA File Offset: 0x0000C3AA
		internal AirSyncResponse(IAirSyncContext context, HttpResponse httpResponse)
		{
			this.context = context;
			this.httpResponse = httpResponse;
			((IAirSyncResponse)this).TimeToRespond = ExDateTime.UtcNow;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000E1D6 File Offset: 0x0000C3D6
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		XmlDocument IAirSyncResponse.XmlDocument
		{
			get
			{
				return this.xmlDocument;
			}
			set
			{
				if (this.responseWritten && value != null)
				{
					try
					{
						AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.ProtocolTracer, this, "[AirSyncResponse.XmlDocument(set)] We should not be setting the XmlDocument on the response after we have already issued the response.  Old Doc: {0}, New Doc: {1}.", (this.xmlDocument == null) ? "<NULL>" : this.xmlDocument.OuterXml, (value == null) ? "<NULL>" : value.OuterXml);
					}
					catch (ArgumentException arg)
					{
						AirSyncDiagnostics.TraceError<ArgumentException>(ExTraceGlobals.ProtocolTracer, this, "[AirSyncResponse.XmlDocument(set)] We should not be setting the XmlDocument on the response after we have already issued the response.  However, either the old doc or new doc are unhappy about having their OuterXml being accessed, so we cannot emit it here.  Exception: {0}", arg);
					}
				}
				this.xmlDocument = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000E260 File Offset: 0x0000C460
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000E268 File Offset: 0x0000C468
		ExDateTime IAirSyncResponse.TimeToRespond
		{
			get
			{
				return this.timeToRespond;
			}
			set
			{
				if (value > this.timeToRespond)
				{
					this.timeToRespond = value;
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000E27F File Offset: 0x0000C47F
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000E28C File Offset: 0x0000C48C
		HttpStatusCode IAirSyncResponse.HttpStatusCode
		{
			get
			{
				return (HttpStatusCode)this.httpResponse.StatusCode;
			}
			set
			{
				try
				{
					this.httpResponse.StatusCode = (int)value;
					if (value == (HttpStatusCode)449)
					{
						this.httpResponse.StatusDescription = "Retry after sending a PROVISION command";
					}
				}
				catch (HttpException)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "HttpException was thrown while setting the StatusCode.");
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		Stream IAirSyncResponse.OutputStream
		{
			get
			{
				return this.httpResponse.OutputStream;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000E2F1 File Offset: 0x0000C4F1
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000E2FE File Offset: 0x0000C4FE
		string IAirSyncResponse.ContentType
		{
			get
			{
				return this.httpResponse.ContentType;
			}
			set
			{
				this.httpResponse.ContentType = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000E30C File Offset: 0x0000C50C
		bool IAirSyncResponse.IsClientConnected
		{
			get
			{
				return this.httpResponse.IsClientConnected;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000E319 File Offset: 0x0000C519
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000E321 File Offset: 0x0000C521
		bool IAirSyncResponse.IsErrorResponse { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000E32A File Offset: 0x0000C52A
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000E332 File Offset: 0x0000C532
		StatusCode IAirSyncResponse.AirSyncStatus { get; set; }

		// Token: 0x0600027A RID: 634 RVA: 0x0000E33C File Offset: 0x0000C53C
		void IAirSyncResponse.IssueWbXmlResponse()
		{
			if (this.responseWritten)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.ProtocolTracer, this, "[AirSyncResponse.IssueWbXmlResponse] Document has already been written to the HttpResponse stream.");
				return;
			}
			if (this.xmlDocument != null)
			{
				this.httpResponse.ClearContent();
				this.httpResponse.ContentType = "application/vnd.ms-sync.wbxml";
				WbxmlWriter wbxmlWriter = new WbxmlWriter(this.httpResponse.OutputStream);
				wbxmlWriter.WriteXmlDocument(this.xmlDocument);
				this.responseWritten = true;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		void IAirSyncResponse.BuildMultiPartWbXmlResponse(XmlDocument xmlResponse, Stream stream)
		{
			this.context.Response.ContentType = "application/vnd.ms-sync.multipart";
			WbxmlWriter wbxmlWriter = new WbxmlWriter(stream);
			wbxmlWriter.WriteXmlDocument(xmlResponse);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		void IAirSyncResponse.IssueErrorResponse(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode)
		{
			((IAirSyncResponse)this).SetErrorResponse(httpStatusCode, airSyncStatusCode);
			((IAirSyncResponse)this).IssueWbXmlResponse();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		void IAirSyncResponse.SetErrorResponse(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode)
		{
			bool flag = false;
			string text = null;
			string text2 = null;
			((IAirSyncResponse)this).AirSyncStatus = airSyncStatusCode;
			((IAirSyncResponse)this).IsErrorResponse = true;
			if (airSyncStatusCode != StatusCode.None && ((this.context.Request.Version < 140 && airSyncStatusCode < StatusCode.First140Error) || (this.context.Request.Version == 140 && airSyncStatusCode <= StatusCode.AvailabilityFailure) || (this.context.Request.Version == 141 && airSyncStatusCode <= StatusCode.MaximumDevicesReached) || (this.context.Request.Version == 160 && airSyncStatusCode <= StatusCode.InvalidRecipients) || this.context.Request.Version > 160))
			{
				AirSyncRequest.CommandTypeToXmlName(this.context.Request.CommandType, out text, out text2);
				if (text != null && text2 != null && httpStatusCode != (HttpStatusCode)441 && httpStatusCode != (HttpStatusCode)451)
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (airSyncStatusCode == StatusCode.None)
				{
					throw new ApplicationException("Cannot generate a StatusCode.None error for the client");
				}
				((IAirSyncResponse)this).HttpStatusCode = HttpStatusCode.OK;
				this.xmlDocument = new SafeXmlDocument();
				XmlElement xmlElement = this.xmlDocument.CreateElement(text, text2);
				this.xmlDocument.AppendChild(xmlElement);
				XmlElement xmlElement2 = this.xmlDocument.CreateElement("Status", text2);
				XmlNode xmlNode = xmlElement2;
				int num = (int)airSyncStatusCode;
				xmlNode.InnerText = num.ToString(CultureInfo.InvariantCulture);
				xmlElement.AppendChild(xmlElement2);
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, (int)airSyncStatusCode);
			}
			else
			{
				if (httpStatusCode == HttpStatusCode.OK)
				{
					throw new ApplicationException("Cannot set OK as an error");
				}
				((IAirSyncResponse)this).HttpStatusCode = httpStatusCode;
				this.xmlDocument = null;
			}
			if (this.TarpitErrorResponse((int)httpStatusCode))
			{
				((IAirSyncResponse)this).TimeToRespond = this.context.RequestTime.AddSeconds((double)GlobalSettings.ErrorResponseDelay);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E5AA File Offset: 0x0000C7AA
		void IAirSyncResponse.Clear()
		{
			this.xmlDocument = null;
			this.httpResponse.ClearContent();
			this.responseHeaders.Clear();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		List<string> IAirSyncResponse.GetHeaderValues(string headerName)
		{
			if (this.responseHeaders.ContainsKey(headerName))
			{
				return this.responseHeaders[headerName];
			}
			return new List<string>();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		string IAirSyncResponse.GetHeadersAsString()
		{
			StringBuilder stringBuilder = new StringBuilder(128 + this.responseHeaders.Count * 32);
			stringBuilder.Append("HTTP/1.1 ");
			stringBuilder.Append(this.httpResponse.StatusCode);
			stringBuilder.Append(" ");
			stringBuilder.AppendLine(this.httpResponse.StatusDescription);
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.responseHeaders)
			{
				foreach (string value in keyValuePair.Value)
				{
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append(": ");
					if (string.Equals(keyValuePair.Key, "authorization", StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder.AppendLine("********");
					}
					else
					{
						stringBuilder.AppendLine(value);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E718 File Offset: 0x0000C918
		void IAirSyncResponse.TraceHeaders()
		{
			if (AirSyncDiagnostics.IsTraceEnabled(TraceType.DebugTrace, ExTraceGlobals.RequestsTracer))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Response headers:");
				AirSyncDiagnostics.TraceDebug<int, string>(ExTraceGlobals.RequestsTracer, this, "    HTTP/1.1 {0} {1}", this.httpResponse.StatusCode, this.httpResponse.StatusDescription);
				foreach (string text in this.responseHeaders.Keys)
				{
					foreach (string arg in this.responseHeaders[text])
					{
						if (string.Equals(text, "authorization", StringComparison.OrdinalIgnoreCase))
						{
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "    {0}: ********", text);
						}
						else
						{
							AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "    {0}: {1}", text, arg);
						}
					}
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E824 File Offset: 0x0000CA24
		void IAirSyncResponse.AppendHeader(string name, string value, bool allowDuplicateHeader)
		{
			List<string> list = null;
			if (!this.responseHeaders.TryGetValue(name, out list) || allowDuplicateHeader)
			{
				if (list == null)
				{
					list = new List<string>(1);
					this.responseHeaders.Add(name, list);
				}
				list.Add(value);
				this.httpResponse.AppendHeader(name, value);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E871 File Offset: 0x0000CA71
		void IAirSyncResponse.AppendHeader(string name, string value)
		{
			((IAirSyncResponse)this).AppendHeader(name, value, true);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E87C File Offset: 0x0000CA7C
		void IAirSyncResponse.AppendToLog(string param)
		{
			this.httpResponse.AppendToLog(param);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E88C File Offset: 0x0000CA8C
		private bool TarpitErrorResponse(int httpStatusCode)
		{
			if (httpStatusCode != 441 && httpStatusCode != 449)
			{
				return true;
			}
			if (httpStatusCode == 451)
			{
				return !DeviceCapability.DeviceCanHandleRedirect(this.context);
			}
			if (ADNotificationManager.GetAutoBlockThreshold(AutoblockThresholdType.RecentCommands).BehaviorTypeIncidenceDuration == EnhancedTimeSpan.Zero)
			{
				return false;
			}
			DeviceBehavior deviceBehavior = this.context.DeviceBehavior;
			if (deviceBehavior == null)
			{
				Guid userGuid = AirSyncResponse.UnknownUserGuid;
				if (httpStatusCode != 441)
				{
					switch (httpStatusCode)
					{
					case 449:
						if (Command.CurrentCommand != null && Command.CurrentCommand.GlobalInfo != null && Command.CurrentCommand.GlobalInfo.DeviceADObjectId != null)
						{
							userGuid = Command.CurrentCommand.GlobalInfo.DeviceADObjectId.ObjectGuid;
							goto IL_136;
						}
						goto IL_136;
					case 451:
						if (this.context.User != null && this.context.User.ADUser != null && this.context.User.ADUser.Id != null)
						{
							userGuid = this.context.User.ADUser.Id.ObjectGuid;
							goto IL_136;
						}
						goto IL_136;
					}
					throw new ApplicationException("Unexpected HTTP status code " + httpStatusCode);
				}
				IL_136:
				if (!DeviceBehaviorCache.TryGetValue(userGuid, this.context.DeviceIdentity, out deviceBehavior))
				{
					deviceBehavior = new DeviceBehavior(true);
					DeviceBehaviorCache.AddOrReplace(userGuid, this.context.DeviceIdentity, deviceBehavior);
				}
			}
			if (deviceBehavior != null)
			{
				lock (deviceBehavior)
				{
					if (deviceBehavior.IsDeviceAutoBlocked(null) != DeviceAccessStateReason.Unknown)
					{
						return true;
					}
					deviceBehavior.RecordCommand(httpStatusCode);
					if (deviceBehavior.IsDeviceAutoBlocked(null) != DeviceAccessStateReason.Unknown)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04000236 RID: 566
		private static readonly Guid UnknownUserGuid = Guid.Empty;

		// Token: 0x04000237 RID: 567
		private IAirSyncContext context;

		// Token: 0x04000238 RID: 568
		private bool responseWritten;

		// Token: 0x04000239 RID: 569
		private HttpResponse httpResponse;

		// Token: 0x0400023A RID: 570
		private Dictionary<string, List<string>> responseHeaders = new Dictionary<string, List<string>>();

		// Token: 0x0400023B RID: 571
		private XmlDocument xmlDocument;

		// Token: 0x0400023C RID: 572
		private ExDateTime timeToRespond;
	}
}
