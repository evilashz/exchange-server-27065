using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000053 RID: 83
	public class WacDiscoveryClient
	{
		// Token: 0x0600027E RID: 638 RVA: 0x000094FD File Offset: 0x000076FD
		public WacDiscoveryClient(Uri wacDiscoveryEndPoint)
		{
			this.wacDiscoveryEndPoint = wacDiscoveryEndPoint;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000950C File Offset: 0x0000770C
		public Uri WacDiscoveryEndPoint
		{
			get
			{
				return this.wacDiscoveryEndPoint;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009514 File Offset: 0x00007714
		public WacDiscoveryResultBase FetchDiscoveryResults()
		{
			OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(string.Format(CultureInfo.InvariantCulture, "Fetching wac discovery data from endPoint {0}", new object[]
			{
				this.wacDiscoveryEndPoint
			})));
			string text = null;
			WacDiscoveryResultBase wacDiscoveryResultBase = null;
			ExEventLog.EventTuple tuple = ClientsEventLogConstants.Tuple_WacDiscoveryDataRetrievalFailure;
			for (int i = 0; i < 3; i++)
			{
				WebRequest webRequest = WebRequest.Create(this.wacDiscoveryEndPoint);
				webRequest.Method = "GET";
				try
				{
					WebResponse response = webRequest.GetResponse();
					using (Stream responseStream = response.GetResponseStream())
					{
						using (XmlReader xmlReader = XmlReader.Create(responseStream))
						{
							if (this.ParseUntilExternalHttpsNetZoneNode(xmlReader))
							{
								wacDiscoveryResultBase = new WacDiscoveryResultSuccess();
								this.ParseNetZoneData(xmlReader, wacDiscoveryResultBase);
								wacDiscoveryResultBase.MarkInitializationComplete();
								if (wacDiscoveryResultBase.WacViewableFileTypes.Length != 0)
								{
									text = string.Format(CultureInfo.InvariantCulture, "Successfully retrieved configuration data from {0}. Supported File types: {1}", new object[]
									{
										this.wacDiscoveryEndPoint,
										wacDiscoveryResultBase.GetWacViewableFileTypesDisplayText()
									});
									OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text));
									tuple = ClientsEventLogConstants.Tuple_WacDiscoveryDataRetrievedSuccessfully;
								}
								else
								{
									text = string.Format(CultureInfo.InvariantCulture, "Successfully retrieved configuration data from {0}, but no file types defined", new object[]
									{
										this.wacDiscoveryEndPoint
									});
									OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text));
									wacDiscoveryResultBase = new WacDiscoveryResultFailure(new WacDiscoveryFailureException(text));
								}
							}
							else
							{
								text = string.Format(CultureInfo.InvariantCulture, "Successfully retrieved configuration data from {0}, but retrieved discovery xml does not contain an external https net zone node", new object[]
								{
									this.wacDiscoveryEndPoint
								});
								OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text));
								wacDiscoveryResultBase = new WacDiscoveryResultFailure(new WacDiscoveryFailureException(text));
							}
						}
					}
					break;
				}
				catch (WebException ex)
				{
					text = string.Format(CultureInfo.InvariantCulture, "Error retrieving wac discovery data from endpoint {0}. Exception was {1}", new object[]
					{
						this.wacDiscoveryEndPoint,
						ex
					});
					OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text));
					wacDiscoveryResultBase = new WacDiscoveryResultFailure(new WacDiscoveryFailureException(text));
				}
				Thread.Sleep(100);
			}
			OwaDiagnostics.LogEvent(tuple, string.Empty, new object[]
			{
				text
			});
			return wacDiscoveryResultBase;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009758 File Offset: 0x00007958
		private bool ParseUntilExternalHttpsNetZoneNode(XmlReader xmlReader)
		{
			string text = "external-https";
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element && string.Equals("net-zone", xmlReader.Name) && string.Equals(text, xmlReader.GetAttribute("name")))
				{
					return true;
				}
			}
			OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent("Did not find a Net-Zone element with name=" + text));
			return false;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000097BC File Offset: 0x000079BC
		private void ParseNetZoneData(XmlReader xmlReader, WacDiscoveryResultBase result)
		{
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.EndElement && string.Equals("net-zone", xmlReader.Name))
				{
					return;
				}
				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("action"))
				{
					string attribute = xmlReader.GetAttribute("name");
					string attribute2 = xmlReader.GetAttribute("requires");
					string text = "." + xmlReader.GetAttribute("ext");
					string attribute3 = xmlReader.GetAttribute("urlsrc");
					OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(string.Format(CultureInfo.InvariantCulture, "Extension: {0,-10} Verb: {1,-20} Requires {2,-30} Url: {3}", new object[]
					{
						text,
						attribute,
						attribute2,
						attribute3
					})));
					if (attribute != null)
					{
						if (attribute.Equals("view") && string.IsNullOrEmpty(attribute2))
						{
							string text2;
							if (result.TryGetViewUrlForFileExtension(text, "this parameter is not needed here", out text2))
							{
								OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(string.Format(CultureInfo.InvariantCulture, "Overwriting {0}", new object[]
								{
									text2
								})));
							}
							result.AddViewMapping(text, attribute3);
							if (text == ".doc")
							{
								result.AddViewMapping(".rtf", attribute3);
							}
						}
						else if (attribute.Equals("edit"))
						{
							string text2;
							if (result.TryGetEditUrlForFileExtension(text, "this parameter is not needed here", out text2))
							{
								OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(string.Format(CultureInfo.InvariantCulture, "Overwriting {0}", new object[]
								{
									text2
								})));
							}
							result.AddEditMapping(text, attribute3);
						}
					}
				}
			}
			WacDiscoveryFailureException ex = new WacDiscoveryFailureException("Unexpected end of wac discovery file");
			result = new WacDiscoveryResultFailure(ex);
			throw ex;
		}

		// Token: 0x0400012E RID: 302
		private readonly Uri wacDiscoveryEndPoint;
	}
}
