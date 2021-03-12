using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A1 RID: 673
	[Serializable]
	internal class AttributeInfo
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0008B938 File Offset: 0x00089B38
		internal int MapiID
		{
			get
			{
				return this.mapiID;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0008B940 File Offset: 0x00089B40
		internal DetailsTemplateControl.AttributeControlTypes ControlType
		{
			get
			{
				return this.controlType;
			}
		}

		// Token: 0x17000780 RID: 1920
		internal bool this[string templateType]
		{
			get
			{
				bool flag;
				return this.templateTypes.TryGetValue(templateType, out flag) && flag;
			}
			set
			{
				if (value)
				{
					this.templateTypes[templateType] = true;
					return;
				}
				this.templateTypes[templateType] = false;
			}
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0008B988 File Offset: 0x00089B88
		internal AttributeInfo(int mapiID, DetailsTemplateControl.AttributeControlTypes controlType)
		{
			this.mapiID = mapiID;
			this.templateTypes = new Dictionary<string, bool>();
			this.controlType = controlType;
		}

		// Token: 0x040012AB RID: 4779
		private Dictionary<string, bool> templateTypes;

		// Token: 0x040012AC RID: 4780
		private int mapiID;

		// Token: 0x040012AD RID: 4781
		private DetailsTemplateControl.AttributeControlTypes controlType;
	}
}
