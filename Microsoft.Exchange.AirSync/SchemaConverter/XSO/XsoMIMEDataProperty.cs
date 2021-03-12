using System;
using System.IO;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000229 RID: 553
	[Serializable]
	internal class XsoMIMEDataProperty : XsoProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x000793A4 File Offset: 0x000775A4
		public XsoMIMEDataProperty(PropertyType type) : base(null, type)
		{
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000793AE File Offset: 0x000775AE
		public XsoMIMEDataProperty() : base(null)
		{
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x000793B7 File Offset: 0x000775B7
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x000793D8 File Offset: 0x000775D8
		public Stream MIMEData
		{
			get
			{
				if (this.mimeData == null)
				{
					this.mimeData = BodyUtility.ConvertItemToMime(base.XsoItem);
				}
				return this.mimeData;
			}
			set
			{
				throw new NotImplementedException("set_MIMEData");
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x000793E4 File Offset: 0x000775E4
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x000793EC File Offset: 0x000775EC
		public long MIMESize { get; set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000793F8 File Offset: 0x000775F8
		public bool IsOnSMIMEMessage
		{
			get
			{
				MessageItem messageItem = base.XsoItem as MessageItem;
				return messageItem != null && ObjectClass.IsSmime(messageItem.ClassName);
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00079421 File Offset: 0x00077621
		public override void Unbind()
		{
			this.mimeData = null;
			base.Unbind();
		}

		// Token: 0x04000C78 RID: 3192
		private Stream mimeData;
	}
}
