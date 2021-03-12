using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200010F RID: 271
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FreeBusyResponse
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001F86F File Offset: 0x0001DA6F
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001F877 File Offset: 0x0001DA77
		public ResponseMessage ResponseMessage
		{
			get
			{
				return this.responseMessageField;
			}
			set
			{
				this.responseMessageField = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001F880 File Offset: 0x0001DA80
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001F888 File Offset: 0x0001DA88
		public FreeBusyView FreeBusyView
		{
			get
			{
				return this.freeBusyViewField;
			}
			set
			{
				this.freeBusyViewField = value;
			}
		}

		// Token: 0x04000468 RID: 1128
		private FreeBusyView freeBusyViewField;

		// Token: 0x04000469 RID: 1129
		private ResponseMessage responseMessageField;
	}
}
