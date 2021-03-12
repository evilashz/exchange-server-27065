using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000AE RID: 174
	[XmlType(Namespace = "AirSync", TypeName = "Commands")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Commands
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000ADF3 File Offset: 0x00008FF3
		public Commands()
		{
			this.Add = new List<AddCommand>();
			this.Change = new List<ChangeCommand>();
			this.Delete = new List<DeleteCommand>();
			this.SoftDelete = new List<SoftDeleteCommand>();
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000AE27 File Offset: 0x00009027
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0000AE2F File Offset: 0x0000902F
		[XmlElement(ElementName = "Add")]
		public List<AddCommand> Add { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000AE38 File Offset: 0x00009038
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000AE40 File Offset: 0x00009040
		[XmlElement(ElementName = "Change")]
		public List<ChangeCommand> Change { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000AE49 File Offset: 0x00009049
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000AE51 File Offset: 0x00009051
		[XmlElement(ElementName = "Delete")]
		public List<DeleteCommand> Delete { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000AE5A File Offset: 0x0000905A
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000AE62 File Offset: 0x00009062
		[XmlElement(ElementName = "SoftDelete")]
		public List<SoftDeleteCommand> SoftDelete { get; set; }
	}
}
