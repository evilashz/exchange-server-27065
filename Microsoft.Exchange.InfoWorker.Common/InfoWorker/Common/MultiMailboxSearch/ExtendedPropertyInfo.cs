using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E2 RID: 482
	internal class ExtendedPropertyInfo
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00035817 File Offset: 0x00033A17
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0003581F File Offset: 0x00033A1F
		public Guid PropertySetId { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00035828 File Offset: 0x00033A28
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00035830 File Offset: 0x00033A30
		public int? PropertyTagId { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00035839 File Offset: 0x00033A39
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00035841 File Offset: 0x00033A41
		public string PropertyName { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0003584A File Offset: 0x00033A4A
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00035852 File Offset: 0x00033A52
		public int? PropertyId { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0003585B File Offset: 0x00033A5B
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00035863 File Offset: 0x00033A63
		public string PropertyType { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0003586C File Offset: 0x00033A6C
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00035874 File Offset: 0x00033A74
		public PropertyDefinition XsoPropertyDefinition { get; set; }

		// Token: 0x06000C78 RID: 3192 RVA: 0x00035880 File Offset: 0x00033A80
		public MapiPropertyType GetMapiPropertyType()
		{
			MapiPropertyType result = 19;
			if (!string.IsNullOrEmpty(this.PropertyType))
			{
				Enum.TryParse<MapiPropertyType>(this.PropertyType, true, out result);
			}
			return result;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000358B0 File Offset: 0x00033AB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Extended property info");
			if (this.PropertySetId != Guid.Empty)
			{
				stringBuilder.AppendFormat(", Property set id: {0}", this.PropertySetId.ToString("D"));
			}
			if (this.PropertyTagId != null && this.PropertyTagId != null)
			{
				stringBuilder.AppendFormat(", Property tag id: {0}", this.PropertyTagId.Value);
			}
			if (!string.IsNullOrEmpty(this.PropertyName))
			{
				stringBuilder.AppendFormat(", Property name: {0}", this.PropertyName);
			}
			if (this.PropertyId != null && this.PropertyId != null)
			{
				stringBuilder.AppendFormat(", Property id: {0}", this.PropertyId.Value);
			}
			if (!string.IsNullOrEmpty(this.PropertyType))
			{
				stringBuilder.AppendFormat(", Property type: {0}", this.PropertyType);
			}
			return stringBuilder.ToString();
		}
	}
}
