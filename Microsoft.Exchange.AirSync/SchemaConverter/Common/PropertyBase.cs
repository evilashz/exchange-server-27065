using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	internal abstract class PropertyBase : IProperty
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00056174 File Offset: 0x00054374
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0005617C File Offset: 0x0005437C
		public int SchemaLinkId
		{
			get
			{
				return this.schemaLinkId;
			}
			set
			{
				if (-1 != this.schemaLinkId)
				{
					throw new ConversionException("Schema property already bound by schema link id");
				}
				this.schemaLinkId = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00056199 File Offset: 0x00054399
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x000561A1 File Offset: 0x000543A1
		public PropertyState State
		{
			get
			{
				return this.propertyState;
			}
			set
			{
				this.propertyState = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000561AA File Offset: 0x000543AA
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x000561B2 File Offset: 0x000543B2
		public PropertyBase.PostProcessProperties PostProcessingDelegate { get; set; }

		// Token: 0x06000F10 RID: 3856 RVA: 0x000561BB File Offset: 0x000543BB
		public virtual void Unbind()
		{
			this.PostProcessingDelegate = null;
		}

		// Token: 0x06000F11 RID: 3857
		public abstract void CopyFrom(IProperty srcProperty);

		// Token: 0x04000A4C RID: 2636
		private PropertyState propertyState;

		// Token: 0x04000A4D RID: 2637
		private int schemaLinkId = -1;

		// Token: 0x02000121 RID: 289
		// (Invoke) Token: 0x06000F14 RID: 3860
		public delegate void PostProcessProperties(IProperty srcProperty, IList<IProperty> props);
	}
}
