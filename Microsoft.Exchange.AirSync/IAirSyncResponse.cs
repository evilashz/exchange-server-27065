using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200001E RID: 30
	internal interface IAirSyncResponse
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000252 RID: 594
		// (set) Token: 0x06000253 RID: 595
		XmlDocument XmlDocument { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000254 RID: 596
		// (set) Token: 0x06000255 RID: 597
		ExDateTime TimeToRespond { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000256 RID: 598
		// (set) Token: 0x06000257 RID: 599
		HttpStatusCode HttpStatusCode { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000258 RID: 600
		Stream OutputStream { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000259 RID: 601
		// (set) Token: 0x0600025A RID: 602
		string ContentType { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600025B RID: 603
		bool IsClientConnected { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600025C RID: 604
		// (set) Token: 0x0600025D RID: 605
		bool IsErrorResponse { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600025E RID: 606
		// (set) Token: 0x0600025F RID: 607
		StatusCode AirSyncStatus { get; set; }

		// Token: 0x06000260 RID: 608
		void IssueWbXmlResponse();

		// Token: 0x06000261 RID: 609
		void BuildMultiPartWbXmlResponse(XmlDocument xmlResponse, Stream stream);

		// Token: 0x06000262 RID: 610
		void IssueErrorResponse(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode);

		// Token: 0x06000263 RID: 611
		void SetErrorResponse(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode);

		// Token: 0x06000264 RID: 612
		void Clear();

		// Token: 0x06000265 RID: 613
		List<string> GetHeaderValues(string headerName);

		// Token: 0x06000266 RID: 614
		string GetHeadersAsString();

		// Token: 0x06000267 RID: 615
		void TraceHeaders();

		// Token: 0x06000268 RID: 616
		void AppendHeader(string name, string value, bool allowDuplicateHeader);

		// Token: 0x06000269 RID: 617
		void AppendHeader(string name, string value);

		// Token: 0x0600026A RID: 618
		void AppendToLog(string param);
	}
}
