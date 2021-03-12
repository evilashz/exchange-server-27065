using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200013D RID: 317
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserPhotoResponseMessageType : ResponseMessage
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000259FD File Offset: 0x00023BFD
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x00025A05 File Offset: 0x00023C05
		internal string CacheId
		{
			get
			{
				return this.cacheId;
			}
			set
			{
				this.cacheId = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00025A0E File Offset: 0x00023C0E
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00025A16 File Offset: 0x00023C16
		internal HttpStatusCode StatusCode
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00025A1F File Offset: 0x00023C1F
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x00025A27 File Offset: 0x00023C27
		internal string Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00025A30 File Offset: 0x00023C30
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00025A38 File Offset: 0x00023C38
		public byte[] PictureData
		{
			get
			{
				return this.pictureData;
			}
			set
			{
				this.pictureData = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00025A41 File Offset: 0x00023C41
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00025A49 File Offset: 0x00023C49
		public bool HasChanged
		{
			get
			{
				return this.hasChanged;
			}
			set
			{
				this.hasChanged = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00025A52 File Offset: 0x00023C52
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00025A5A File Offset: 0x00023C5A
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x040006C0 RID: 1728
		private byte[] pictureData;

		// Token: 0x040006C1 RID: 1729
		private bool hasChanged;

		// Token: 0x040006C2 RID: 1730
		private string cacheId;

		// Token: 0x040006C3 RID: 1731
		private HttpStatusCode code;

		// Token: 0x040006C4 RID: 1732
		private string expires;

		// Token: 0x040006C5 RID: 1733
		private string contentType;
	}
}
