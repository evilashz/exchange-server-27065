using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001C5 RID: 453
	internal class GoodMessageData : ConfigurablePropertyBag
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00039E51 File Offset: 0x00038051
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x00039E63 File Offset: 0x00038063
		public bool GoodMessageExists
		{
			get
			{
				return (bool)this[GoodMessageSchema.GoodMessageExistsProperty];
			}
			set
			{
				this[GoodMessageSchema.GoodMessageExistsProperty] = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00039E78 File Offset: 0x00038078
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00039E9E File Offset: 0x0003809E
		public override Type GetSchemaType()
		{
			return typeof(GoodMessageSchema);
		}

		// Token: 0x0400092D RID: 2349
		private readonly Guid identity = ReportingSession.GenerateNewId();
	}
}
