using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200010B RID: 267
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class Path
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001F7CF File Offset: 0x0001D9CF
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001F7D7 File Offset: 0x0001D9D7
		[XmlAttribute(AttributeName = "select")]
		public string Select
		{
			get
			{
				return this.selectField;
			}
			set
			{
				this.selectField = value;
			}
		}

		// Token: 0x04000458 RID: 1112
		private string selectField;
	}
}
