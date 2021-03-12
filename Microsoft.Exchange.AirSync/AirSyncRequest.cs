using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200001D RID: 29
	internal class AirSyncRequest : IAirSyncRequest
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000CF10 File Offset: 0x0000B110
		internal AirSyncRequest(IAirSyncContext context, HttpRequest httpRequest)
		{
			this.context = context;
			this.httpRequest = httpRequest;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000CF2D File Offset: 0x0000B12D
		string IAirSyncRequest.LogonUserName
		{
			get
			{
				return this.httpRequest.ServerVariables["LOGON_USER"];
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000CF44 File Offset: 0x0000B144
		string IAirSyncRequest.Url
		{
			get
			{
				return this.httpRequest.Url.ToString();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000CF56 File Offset: 0x0000B156
		string IAirSyncRequest.PathAndQuery
		{
			get
			{
				return this.httpRequest.Url.PathAndQuery;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000CF68 File Offset: 0x0000B168
		Stream IAirSyncRequest.InputStream
		{
			get
			{
				Stream inputStream;
				try
				{
					inputStream = this.httpRequest.InputStream;
				}
				catch (COMException innerException)
				{
					AirSyncPermanentException ex = new AirSyncPermanentException(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater, innerException, false);
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InputStreamUnavailable");
					throw ex;
				}
				return inputStream;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		bool IAirSyncRequest.IsEmpty
		{
			get
			{
				Stream inputStream = ((IAirSyncRequest)this).InputStream;
				return inputStream == null || inputStream.Length == 0L;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		int IAirSyncRequest.ContentLength
		{
			get
			{
				return this.httpRequest.ContentLength;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		string IAirSyncRequest.ContentType
		{
			get
			{
				return this.httpRequest.Headers["Content-Type"];
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000D007 File Offset: 0x0000B207
		Encoding IAirSyncRequest.ContentEncoding
		{
			get
			{
				return this.httpRequest.ContentEncoding;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000D014 File Offset: 0x0000B214
		bool IAirSyncRequest.IsSecureConnection
		{
			get
			{
				return this.httpRequest.IsSecureConnection && this.ClientConnectionSecuredForIrm;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000D02B File Offset: 0x0000B22B
		string IAirSyncRequest.UserHostName
		{
			get
			{
				return this.httpRequest.UserHostName;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000D038 File Offset: 0x0000B238
		string IAirSyncRequest.HostHeaderInfo
		{
			get
			{
				if (this.httpRequest.Headers["msExchProxyUri"] != null)
				{
					Uri uri = new Uri(this.httpRequest.Headers["msExchProxyUri"]);
					if (uri != null)
					{
						return uri.Host;
					}
				}
				return null;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000D088 File Offset: 0x0000B288
		WindowsIdentity IAirSyncRequest.LogonUserIdentity
		{
			get
			{
				return this.httpRequest.LogonUserIdentity;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000D098 File Offset: 0x0000B298
		uint? IAirSyncRequest.PolicyKey
		{
			get
			{
				string text = this.httpRequest.Headers["X-MS-PolicyKey"];
				if (text == null)
				{
					return null;
				}
				uint value;
				if (!uint.TryParse(text, out value))
				{
					return null;
				}
				return new uint?(value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000D0E2 File Offset: 0x0000B2E2
		string IAirSyncRequest.DescriptionForEventLog
		{
			get
			{
				return this.httpRequest.RawUrl;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		CommandType IAirSyncRequest.CommandType
		{
			get
			{
				if (this.commandType == CommandType.Unknown)
				{
					if (string.Compare(this.httpRequest.HttpMethod, "OPTIONS", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.commandType = CommandType.Options;
					}
					else if (string.Compare(this.httpRequest.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) == 0)
					{
						string commandName = this.httpRequest.QueryString["Cmd"];
						this.commandType = this.CommandNameToCommandType(commandName);
					}
				}
				return this.commandType;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D168 File Offset: 0x0000B368
		int IAirSyncRequest.Version
		{
			get
			{
				if (this.version == -1)
				{
					string versionString = ((IAirSyncRequest)this).VersionString;
					if (versionString == null)
					{
						if (((IAirSyncRequest)this).CommandType == CommandType.Options || ((IAirSyncRequest)this).CommandType == CommandType.ProxyLogin)
						{
							this.version = 0;
						}
						else
						{
							this.version = 10;
						}
					}
					else
					{
						this.version = AirSyncUtility.ParseVersionString(versionString);
					}
				}
				return this.version;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		string IAirSyncRequest.VersionString
		{
			get
			{
				return this.httpRequest.Headers["MS-ASProtocolVersion"];
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000D1D7 File Offset: 0x0000B3D7
		bool IAirSyncRequest.HasOutlookExtensions
		{
			get
			{
				return this.extensionsBitMask != null && this.extensionsBitMask.Length > 0;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		DeviceIdentity IAirSyncRequest.DeviceIdentity
		{
			get
			{
				if (this.deviceIdentity == null)
				{
					this.BuildDeviceIdentity();
				}
				return this.deviceIdentity;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D204 File Offset: 0x0000B404
		string IAirSyncRequest.User
		{
			get
			{
				if (string.IsNullOrEmpty(this.user))
				{
					this.user = this.httpRequest.QueryString["User"];
				}
				return this.user;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000D234 File Offset: 0x0000B434
		string IAirSyncRequest.UserAgent
		{
			get
			{
				return this.httpRequest.Headers["User-Agent"];
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D24B File Offset: 0x0000B44B
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000D267 File Offset: 0x0000B467
		XmlDocument IAirSyncRequest.XmlDocument
		{
			get
			{
				if (this.xmlDocument == null)
				{
					this.xmlDocument = ((IAirSyncRequest)this).LoadRequestDocument();
				}
				return this.xmlDocument;
			}
			set
			{
				this.xmlDocument = value;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D270 File Offset: 0x0000B470
		XmlDocument IAirSyncRequest.LoadRequestDocument()
		{
			XmlDocument result = null;
			Stream inputStream = ((IAirSyncRequest)this).InputStream;
			if (string.Equals(this.httpRequest.ContentType, "application/vnd.ms-sync.wbxml", StringComparison.OrdinalIgnoreCase) && inputStream.Length > 0L)
			{
				inputStream.Seek(0L, SeekOrigin.Begin);
				using (WbxmlReader wbxmlReader = new WbxmlReader(inputStream))
				{
					result = wbxmlReader.ReadXmlDocument();
				}
			}
			return result;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		XmlElement IAirSyncRequest.CommandXml
		{
			get
			{
				if (((IAirSyncRequest)this).XmlDocument == null)
				{
					return null;
				}
				return ((IAirSyncRequest)this).XmlDocument.DocumentElement;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		CultureInfo IAirSyncRequest.Culture
		{
			get
			{
				if (this.culture == null)
				{
					string text = this.httpRequest.Headers["Accept-Language"];
					if (text != null)
					{
						try
						{
							this.culture = new CultureInfo(text);
							if (this.culture.IsNeutralCulture)
							{
								this.culture = CultureInfo.CreateSpecificCulture(this.culture.Name);
							}
						}
						catch (ArgumentException ex)
						{
							AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, this, "Command: Invalid/unsupported language header specified: {0}, {1}", text, ex.Message);
							this.culture = null;
						}
					}
					if (this.culture == null)
					{
						this.culture = CultureInfo.InvariantCulture;
					}
				}
				return this.culture;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		bool IAirSyncRequest.WasDCProxied
		{
			get
			{
				return this.httpRequest.Headers["X-EAS-DC-Proxy"] != null;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		string IAirSyncRequest.DCProxyHeader
		{
			get
			{
				return this.httpRequest.Headers["X-EAS-DC-Proxy"];
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		bool IAirSyncRequest.WasProxied
		{
			get
			{
				return this.httpRequest.Headers["X-EAS-Proxy"] != null;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		bool IAirSyncRequest.WasFromCafe
		{
			get
			{
				return this.httpRequest.Headers["X-IsFromCafe"] == "1";
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000D416 File Offset: 0x0000B616
		bool IAirSyncRequest.WasBasicAuthProxied
		{
			get
			{
				return this.httpRequest.Headers["X-EAS-BasicAuth-Proxy"] != null;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000D433 File Offset: 0x0000B633
		string IAirSyncRequest.ProxyHeader
		{
			get
			{
				return this.httpRequest.Headers["X-EAS-Proxy"];
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000D44A File Offset: 0x0000B64A
		string IAirSyncRequest.VDirSettingsHeader
		{
			get
			{
				return this.httpRequest.Headers["X-vDirObjectId"];
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000D464 File Offset: 0x0000B664
		bool IAirSyncRequest.AcceptMultiPartResponse
		{
			get
			{
				string text = this.httpRequest.Headers["MS-ASAcceptMultiPart"];
				string a;
				if ((a = text) != null)
				{
					if (a == "T" || a == "t")
					{
						return true;
					}
					if (!(a == "f") && !(a == "F"))
					{
					}
				}
				return false;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		string IAirSyncRequest.XMSWLHeader
		{
			get
			{
				return this.httpRequest.Headers["X-MS-WL"];
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		internal static void CommandTypeToXmlName(CommandType commandType, out string xmlName, out string xmlNamespace)
		{
			switch (commandType)
			{
			case CommandType.Sync:
				xmlName = "Sync";
				xmlNamespace = "AirSync:";
				return;
			case CommandType.GetItemEstimate:
				xmlName = "GetItemEstimate";
				xmlNamespace = "GetItemEstimate:";
				return;
			case CommandType.FolderSync:
				xmlName = "FolderSync";
				xmlNamespace = "FolderHierarchy:";
				return;
			case CommandType.FolderUpdate:
				xmlName = "FolderUpdate";
				xmlNamespace = "FolderHierarchy:";
				return;
			case CommandType.FolderDelete:
				xmlName = "FolderDelete";
				xmlNamespace = "FolderHierarchy:";
				return;
			case CommandType.FolderCreate:
				xmlName = "FolderCreate";
				xmlNamespace = "FolderHierarchy:";
				return;
			case CommandType.MoveItems:
				xmlName = "MoveItems";
				xmlNamespace = "Move:";
				return;
			case CommandType.MeetingResponse:
				xmlName = "MeetingResponse";
				xmlNamespace = "MeetingResponse:";
				return;
			case CommandType.SendMail:
				xmlName = "SendMail";
				xmlNamespace = "ComposeMail:";
				return;
			case CommandType.SmartReply:
				xmlName = "SmartReply";
				xmlNamespace = "ComposeMail:";
				return;
			case CommandType.SmartForward:
				xmlName = "SmartForward";
				xmlNamespace = "ComposeMail:";
				return;
			case CommandType.Search:
				xmlName = "Search";
				xmlNamespace = "Search:";
				return;
			case CommandType.Settings:
				xmlName = "Settings";
				xmlNamespace = "Settings:";
				return;
			case CommandType.Ping:
				xmlName = "Ping";
				xmlNamespace = "Ping:";
				return;
			case CommandType.ItemOperations:
				xmlName = "ItemOperations";
				xmlNamespace = "ItemOperations:";
				return;
			case CommandType.Provision:
				xmlName = "Provision";
				xmlNamespace = "Provision:";
				return;
			case CommandType.ResolveRecipients:
				xmlName = "ResolveRecipients";
				xmlNamespace = "ResolveRecipients:";
				return;
			case CommandType.ValidateCert:
				xmlName = "ValidateCert";
				xmlNamespace = "ValidateCert:";
				return;
			}
			xmlName = null;
			xmlNamespace = null;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D66C File Offset: 0x0000B86C
		HttpRequest IAirSyncRequest.GetRawHttpRequest()
		{
			return this.httpRequest;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D674 File Offset: 0x0000B874
		string IAirSyncRequest.GetLegacyUrlParameter(string name)
		{
			if (((IAirSyncRequest)this).Version >= 140 || (!string.Equals(name, "SaveInSent") && !string.Equals(name, "Occurrence") && !string.Equals(name, "CollectionId") && !string.Equals(name, "CollectionName") && !string.Equals(name, "ParentId") && !string.Equals(name, "ItemId") && !string.Equals(name, "LongId") && !string.Equals(name, "AttachmentName")) || (((IAirSyncRequest)this).CommandType != CommandType.SendMail && ((IAirSyncRequest)this).CommandType != CommandType.SmartForward && ((IAirSyncRequest)this).CommandType != CommandType.SmartReply && ((IAirSyncRequest)this).CommandType != CommandType.CreateCollection && ((IAirSyncRequest)this).CommandType != CommandType.MoveCollection && ((IAirSyncRequest)this).CommandType != CommandType.DeleteCollection && ((IAirSyncRequest)this).CommandType != CommandType.GetAttachment))
			{
				throw new ApplicationException("Only legacy commands should still be reading parameters directly from the URL");
			}
			return this.httpRequest.QueryString[name];
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D75C File Offset: 0x0000B95C
		string IAirSyncRequest.GetHeadersAsString()
		{
			StringBuilder stringBuilder = new StringBuilder(128 + this.httpRequest.Headers.Count * 32);
			stringBuilder.Append(this.httpRequest.HttpMethod);
			stringBuilder.Append(" ");
			stringBuilder.Append(this.httpRequest.Url.PathAndQuery);
			stringBuilder.AppendLine(" HTTP/1.1");
			for (int i = 0; i < this.httpRequest.Headers.Count; i++)
			{
				string key = this.httpRequest.Headers.GetKey(i);
				stringBuilder.Append(key);
				stringBuilder.Append(": ");
				if (string.Equals(key, "authorization", StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder.AppendLine("********");
				}
				else
				{
					stringBuilder.AppendLine(this.httpRequest.Headers.Get(i));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D844 File Offset: 0x0000BA44
		void IAirSyncRequest.ParseAndValidateHeaders()
		{
			this.ParseAndValidateOutlookExtensionsHeader();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D84C File Offset: 0x0000BA4C
		void IAirSyncRequest.TraceHeaders()
		{
			if (AirSyncDiagnostics.IsTraceEnabled(TraceType.DebugTrace, ExTraceGlobals.RequestsTracer))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Request headers:");
				AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "    {0} {1} HTTP/1.1", this.httpRequest.HttpMethod, this.httpRequest.Url.PathAndQuery);
				foreach (object obj in this.httpRequest.Headers)
				{
					string text = (string)obj;
					if (string.Equals(text, "authorization", StringComparison.OrdinalIgnoreCase))
					{
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "    {0}: ********", text);
					}
					else
					{
						AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "    {0}: {1}", text, this.httpRequest.Headers[text]);
					}
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D934 File Offset: 0x0000BB34
		void IAirSyncRequest.PrepareToHang()
		{
			this.culture = null;
			this.xmlDocument = null;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D944 File Offset: 0x0000BB44
		bool IAirSyncRequest.SupportsExtension(OutlookExtension extension)
		{
			if (this.extensionsBitMask == null || this.extensionsBitMask.Length == 0)
			{
				return false;
			}
			int num = (OutlookExtension)this.extensionsBitMask.Length - extension / OutlookExtension.TrueMessageRead - OutlookExtension.SystemCategories;
			if (num < 0)
			{
				return false;
			}
			bool flag;
			if (!Constants.FeatureAccessMap.TryGetValue(extension, out flag))
			{
				return false;
			}
			if (!flag)
			{
				return false;
			}
			int num2 = (int)(extension % OutlookExtension.TrueMessageRead);
			return (this.extensionsBitMask[num] & (byte)(1 << num2)) != 0;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		private CommandType CommandNameToCommandType(string commandName)
		{
			string key;
			if (commandName != null && (key = commandName.ToLower(CultureInfo.InvariantCulture)) != null)
			{
				if (<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x600023d-1 == null)
				{
					<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x600023d-1 = new Dictionary<string, int>(24)
					{
						{
							"gethierarchy",
							0
						},
						{
							"sync",
							1
						},
						{
							"getitemestimate",
							2
						},
						{
							"foldersync",
							3
						},
						{
							"folderupdate",
							4
						},
						{
							"folderdelete",
							5
						},
						{
							"foldercreate",
							6
						},
						{
							"createcollection",
							7
						},
						{
							"movecollection",
							8
						},
						{
							"deletecollection",
							9
						},
						{
							"getattachment",
							10
						},
						{
							"moveitems",
							11
						},
						{
							"meetingresponse",
							12
						},
						{
							"sendmail",
							13
						},
						{
							"smartreply",
							14
						},
						{
							"smartforward",
							15
						},
						{
							"search",
							16
						},
						{
							"settings",
							17
						},
						{
							"ping",
							18
						},
						{
							"itemoperations",
							19
						},
						{
							"provision",
							20
						},
						{
							"resolverecipients",
							21
						},
						{
							"validatecert",
							22
						},
						{
							"proxylogin",
							23
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x600023d-1.TryGetValue(key, out num))
				{
					switch (num)
					{
					case 0:
						return CommandType.GetHierarchy;
					case 1:
						return CommandType.Sync;
					case 2:
						return CommandType.GetItemEstimate;
					case 3:
						return CommandType.FolderSync;
					case 4:
						return CommandType.FolderUpdate;
					case 5:
						return CommandType.FolderDelete;
					case 6:
						return CommandType.FolderCreate;
					case 7:
						return CommandType.CreateCollection;
					case 8:
						return CommandType.MoveCollection;
					case 9:
						return CommandType.DeleteCollection;
					case 10:
						return CommandType.GetAttachment;
					case 11:
						return CommandType.MoveItems;
					case 12:
						return CommandType.MeetingResponse;
					case 13:
						return CommandType.SendMail;
					case 14:
						return CommandType.SmartReply;
					case 15:
						return CommandType.SmartForward;
					case 16:
						return CommandType.Search;
					case 17:
						return CommandType.Settings;
					case 18:
						return CommandType.Ping;
					case 19:
						return CommandType.ItemOperations;
					case 20:
						return CommandType.Provision;
					case 21:
						return CommandType.ResolveRecipients;
					case 22:
						return CommandType.ValidateCert;
					case 23:
						return CommandType.ProxyLogin;
					}
				}
			}
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Unexpected command", 29739);
			this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "UnexpectedCommand");
			return CommandType.Unknown;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000DC04 File Offset: 0x0000BE04
		private void BuildDeviceIdentity()
		{
			this.deviceIdentity = null;
			try
			{
				string text = this.httpRequest.QueryString["DeviceId"];
				if (this.commandType != CommandType.Options || !string.IsNullOrWhiteSpace(text))
				{
					if (string.IsNullOrEmpty(text) || text.Length > 32)
					{
						AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "DeviceId is missing or invalid");
						this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceIdMissingOrInvalid");
						AirSyncPermanentException ex = new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.DeviceIdMissingOrInvalid, new LocalizedString(string.Format(CultureInfo.InvariantCulture, "DeviceIdMissingOrInvalid:'{0}' ", new object[]
						{
							text
						})), false);
						throw ex;
					}
					foreach (char c in text)
					{
						if (!char.IsLetterOrDigit(c))
						{
							AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "DeviceId contains an illegal, non-alphanumeric character");
							this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceIdNonAlphanumeric");
							AirSyncPermanentException ex2 = new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.DeviceIdMissingOrInvalid, new LocalizedString(string.Format(CultureInfo.InvariantCulture, "DeviceIdMissingOrInvalid:'{0}' ", new object[]
							{
								text
							})), false);
							throw ex2;
						}
					}
					string text3 = DeviceClassCache.NormalizeDeviceClass(this.httpRequest.QueryString["DeviceType"]);
					if (this.commandType != CommandType.Options || !string.IsNullOrWhiteSpace(text3))
					{
						if (string.IsNullOrEmpty(text3) || text3.Length > 32 || text3.IndexOf('-') >= 0)
						{
							AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "DeviceType is missing or invalid");
							this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceTypeMissingOrInvalid");
							throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.DeviceTypeMissingOrInvalid, new LocalizedString(string.Format(CultureInfo.InvariantCulture, "DeviceTypeMissingOrInvalid:'{0}' ", new object[]
							{
								text3
							})), false);
						}
						this.deviceIdentity = new DeviceIdentity(text, text3, "AirSync");
					}
				}
			}
			catch (AirSyncPermanentException)
			{
				this.deviceIdentity = AirSyncRequest.GarbageIdentity;
				throw;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DE20 File Offset: 0x0000C020
		private void ParseAndValidateOutlookExtensionsHeader()
		{
			if (!this.context.User.IsConsumerOrganizationUser)
			{
				this.extensionsBitMask = null;
				return;
			}
			if (this.extensionsBitMask != null && this.extensionsBitMask.Length > 0)
			{
				return;
			}
			string text = this.httpRequest.Headers["X-OLK-Extension"];
			string xmswlheader = ((IAirSyncRequest)this).XMSWLHeader;
			bool flag = !string.IsNullOrEmpty(xmswlheader) && xmswlheader.StartsWith("WindowsMail", StringComparison.OrdinalIgnoreCase);
			if (string.IsNullOrEmpty(text))
			{
				if (!flag)
				{
					return;
				}
				text = "1=0206";
			}
			if (((IAirSyncRequest)this).Version != 140)
			{
				this.LogAndThrowError(HttpStatusCode.BadRequest, StatusCode.None, "OutlookExtensionsBadEasVersion: " + this.version, "Outlook extensions are only supported on EAS 14.0");
			}
			string[] array = text.Split(new char[]
			{
				'='
			}, 2);
			if (array.Length != 2)
			{
				this.LogAndThrowError(HttpStatusCode.BadRequest, StatusCode.None, "OutlookExtensionHeaderValueWrongFormat: " + text, string.Format("{0} header value '{1}' has wrong format.", "X-OLK-Extension", text));
			}
			int num;
			if (!int.TryParse(array[0], NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				this.LogAndThrowError(HttpStatusCode.BadRequest, StatusCode.None, "OutlookExtensionHeaderValueInvalidVersion: " + text, string.Format("{0} header value '{1}' has invalid version.", "X-OLK-Extension", text));
			}
			if (num != 1)
			{
				this.LogAndThrowError(HttpStatusCode.BadRequest, StatusCode.None, "OutlookExtensionHeaderValueInvalidVersion: " + text, string.Format("{0} header value '{1}' has invalid version.", "X-OLK-Extension", text));
			}
			this.context.ProtocolLogger.SetValue(ProtocolLoggerData.OutlookExtensionsHeader, text);
			byte[] array2 = null;
			try
			{
				array2 = HexStringConverter.GetBytes(array[1], true);
			}
			catch (ArgumentException innerException)
			{
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "OutlookExtensionHeaderValueHexFail: " + text);
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.None, innerException, false);
			}
			if (flag)
			{
				AirSyncUtility.EnableOutlookExtensionsFeature(array2, OutlookExtension.AdvancedSearch);
			}
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[array2.Length - 1 - i];
				for (int j = 0; j < 8; j++)
				{
					if ((b & (byte)(1 << j)) != 0)
					{
						int num2 = j + i * 8;
						bool flag2;
						if (!Constants.FeatureAccessMap.TryGetValue((OutlookExtension)num2, out flag2))
						{
							this.LogAndThrowError(HttpStatusCode.BadRequest, StatusCode.None, "OutlookExtensionHeaderValueNoFeature: " + num2, string.Format("Invalid feature access code: {0}", num2));
						}
						if (!flag2)
						{
							this.LogAndThrowError(HttpStatusCode.BadRequest, StatusCode.None, "OutlookExtensionHeaderValueFeatureUnallowed: " + num2, string.Format("Specified extension feature is disabled: {0}", num2));
						}
					}
				}
			}
			this.extensionsBitMask = array2;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		private void LogAndThrowError(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode, string errorMessage, string exceptionMessage)
		{
			this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, errorMessage);
			throw new AirSyncPermanentException(httpStatusCode, airSyncStatusCode, new LocalizedString(exceptionMessage), false);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		private bool ClientConnectionSecuredForIrm
		{
			get
			{
				if (!((IAirSyncRequest)this).WasFromCafe && !((IAirSyncRequest)this).WasProxied)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Non Prox-ied request. Returning true.");
					return this.httpRequest.IsSecureConnection;
				}
				if (this.httpRequest.Headers["msExchProxyUri"] == null)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Prox-ied request. Proxy-Uri Header not found.");
					return this.httpRequest.IsSecureConnection;
				}
				Uri uri = new Uri(this.httpRequest.Headers["msExchProxyUri"]);
				AirSyncDiagnostics.TraceDebug<Uri>(ExTraceGlobals.RequestsTracer, this, "Prox-ied request. Proxy-Uri Header:{0} ", uri);
				return uri != null && string.Equals(uri.Scheme, Uri.UriSchemeHttps);
			}
		}

		// Token: 0x0400022C RID: 556
		private static readonly DeviceIdentity GarbageIdentity = new DeviceIdentity("errorId", "errorType", "AirSync");

		// Token: 0x0400022D RID: 557
		private IAirSyncContext context;

		// Token: 0x0400022E RID: 558
		private HttpRequest httpRequest;

		// Token: 0x0400022F RID: 559
		private CommandType commandType;

		// Token: 0x04000230 RID: 560
		private int version = -1;

		// Token: 0x04000231 RID: 561
		private byte[] extensionsBitMask;

		// Token: 0x04000232 RID: 562
		private XmlDocument xmlDocument;

		// Token: 0x04000233 RID: 563
		private CultureInfo culture;

		// Token: 0x04000234 RID: 564
		private DeviceIdentity deviceIdentity;

		// Token: 0x04000235 RID: 565
		private string user;
	}
}
