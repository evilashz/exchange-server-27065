using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B4 RID: 180
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RecurringMasterItemIdType : BaseItemIdType
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001FD4E File Offset: 0x0001DF4E
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0001FD56 File Offset: 0x0001DF56
		[XmlAttribute]
		public string OccurrenceId
		{
			get
			{
				return this.occurrenceIdField;
			}
			set
			{
				this.occurrenceIdField = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001FD5F File Offset: 0x0001DF5F
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001FD67 File Offset: 0x0001DF67
		[XmlAttribute]
		public string ChangeKey
		{
			get
			{
				return this.changeKeyField;
			}
			set
			{
				this.changeKeyField = value;
			}
		}

		// Token: 0x04000552 RID: 1362
		private string occurrenceIdField;

		// Token: 0x04000553 RID: 1363
		private string changeKeyField;
	}
}
