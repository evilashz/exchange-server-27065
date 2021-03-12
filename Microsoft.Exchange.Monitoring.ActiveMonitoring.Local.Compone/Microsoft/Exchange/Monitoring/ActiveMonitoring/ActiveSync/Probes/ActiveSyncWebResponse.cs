using System;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveSync.Probes
{
	// Token: 0x0200005E RID: 94
	public class ActiveSyncWebResponse
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00013CAD File Offset: 0x00011EAD
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00013CB5 File Offset: 0x00011EB5
		public XmlDocument ResponseBody { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00013CBE File Offset: 0x00011EBE
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00013CC6 File Offset: 0x00011EC6
		public string RedirectUrl { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00013CCF File Offset: 0x00011ECF
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00013CD7 File Offset: 0x00011ED7
		public string DiagnosticsValue { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00013CE0 File Offset: 0x00011EE0
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public string CafeErrorHeader { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00013CF1 File Offset: 0x00011EF1
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00013CF9 File Offset: 0x00011EF9
		internal RequestFailureContext CafeErrorValues { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00013D02 File Offset: 0x00011F02
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00013D0A File Offset: 0x00011F0A
		public int HttpStatus { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00013D13 File Offset: 0x00011F13
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00013D1B File Offset: 0x00011F1B
		public string RespondingCasServer { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00013D24 File Offset: 0x00011F24
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00013D2C File Offset: 0x00011F2C
		public string ProxiedMbxServer { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00013D35 File Offset: 0x00011F35
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00013D3D File Offset: 0x00011F3D
		public int[] ActiveSyncStatus { get; private set; }

		// Token: 0x06000311 RID: 785 RVA: 0x00013D48 File Offset: 0x00011F48
		public ActiveSyncWebResponse(HttpWebResponse webResponse)
		{
			try
			{
				this.HttpStatus = (int)webResponse.StatusCode;
				if (!string.IsNullOrEmpty(webResponse.Headers["X-MS-Diagnostics"]))
				{
					this.DiagnosticsValue = Uri.UnescapeDataString(webResponse.Headers["X-MS-Diagnostics"]);
				}
				this.RespondingCasServer = webResponse.Headers["X-FEServer"];
				if (string.IsNullOrEmpty(this.RespondingCasServer))
				{
					this.RespondingCasServer = webResponse.Headers["X-DiagInfo"];
				}
				this.ProxiedMbxServer = webResponse.Headers["X-CalculatedBETarget"];
				if (string.IsNullOrEmpty(this.ProxiedMbxServer))
				{
					this.ProxiedMbxServer = webResponse.Headers["X-BEServer"];
					if (string.IsNullOrEmpty(this.ProxiedMbxServer))
					{
						this.ProxiedMbxServer = "UnkownMailboxServer";
					}
				}
				RequestFailureContext cafeErrorValues;
				if (RequestFailureContext.TryCreateFromResponseHeaders(webResponse.Headers, out cafeErrorValues))
				{
					this.CafeErrorValues = cafeErrorValues;
					this.CafeErrorHeader = webResponse.Headers["X-FailureContext"];
				}
				else
				{
					this.CafeErrorValues = null;
				}
				this.RedirectUrl = webResponse.Headers["X-MS-Location"];
				if (webResponse.ContentLength <= 0L || webResponse.StatusCode != HttpStatusCode.OK)
				{
					this.ResponseBody = null;
				}
				else
				{
					Stream responseStream = webResponse.GetResponseStream();
					this.ResponseBody = ActiveSyncProbeUtil.WbxmlDecodeResponseBody(responseStream);
					using (XmlNodeList elementsByTagName = this.ResponseBody.GetElementsByTagName("Status"))
					{
						this.ActiveSyncStatus = new int[elementsByTagName.Count];
						int num = 0;
						foreach (object obj in elementsByTagName)
						{
							XmlNode xmlNode = (XmlNode)obj;
							int.TryParse(elementsByTagName[0].InnerXml, out this.ActiveSyncStatus[num]);
							num++;
						}
					}
				}
			}
			finally
			{
				webResponse.Close();
			}
		}

		// Token: 0x0400024E RID: 590
		public const string DiagnosticsHeader = "X-MS-Diagnostics";

		// Token: 0x0400024F RID: 591
		public const string LocationHeader = "X-MS-Location";

		// Token: 0x04000250 RID: 592
		public const string DiagInfoHeader = "X-DiagInfo";

		// Token: 0x04000251 RID: 593
		public const string CafeDiaginfoHeader = "X-FEServer";

		// Token: 0x04000252 RID: 594
		public const string MailboxDiaginfoHeader = "X-BEServer";

		// Token: 0x04000253 RID: 595
		public const string CalculatedMailboxDiaginfoHeader = "X-CalculatedBETarget";

		// Token: 0x04000254 RID: 596
		public const string CafeErrorInfoHeader = "X-FailureContext";
	}
}
