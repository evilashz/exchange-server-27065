using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200013F RID: 319
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserPhotoRequestType : BaseRequestType
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00025A6B File Offset: 0x00023C6B
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00025A73 File Offset: 0x00023C73
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00025A7C File Offset: 0x00023C7C
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00025A84 File Offset: 0x00023C84
		public UserPhotoSize SizeRequested
		{
			get
			{
				return this.sizeRequested;
			}
			set
			{
				this.sizeRequested = value;
			}
		}

		// Token: 0x040006D0 RID: 1744
		private string email;

		// Token: 0x040006D1 RID: 1745
		private UserPhotoSize sizeRequested;
	}
}
