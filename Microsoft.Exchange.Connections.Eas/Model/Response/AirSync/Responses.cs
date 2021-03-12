using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "Responses")]
	public class Responses
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000AED8 File Offset: 0x000090D8
		public Responses()
		{
			this.Add = new List<AddResponse>();
			this.Change = new List<ChangeResponse>();
			this.Fetch = new List<FetchResponse>();
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000AF01 File Offset: 0x00009101
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000AF09 File Offset: 0x00009109
		[XmlElement("Add")]
		public List<AddResponse> Add { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000AF12 File Offset: 0x00009112
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000AF1A File Offset: 0x0000911A
		[XmlElement("Change")]
		public List<ChangeResponse> Change { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000AF23 File Offset: 0x00009123
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000AF2B File Offset: 0x0000912B
		[XmlElement("Fetch")]
		public List<FetchResponse> Fetch { get; set; }
	}
}
