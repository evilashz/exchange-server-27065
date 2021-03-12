using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000CE RID: 206
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUserPhotoResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000203DD File Offset: 0x0001E5DD
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x000203E5 File Offset: 0x0001E5E5
		public bool HasChanged
		{
			get
			{
				return this.hasChangedField;
			}
			set
			{
				this.hasChangedField = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x000203EE File Offset: 0x0001E5EE
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x000203F6 File Offset: 0x0001E5F6
		[XmlElement(DataType = "base64Binary")]
		public byte[] PictureData
		{
			get
			{
				return this.pictureDataField;
			}
			set
			{
				this.pictureDataField = value;
			}
		}

		// Token: 0x040005B3 RID: 1459
		private bool hasChangedField;

		// Token: 0x040005B4 RID: 1460
		private byte[] pictureDataField;
	}
}
