using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B1 RID: 1457
	[Serializable]
	public class PerimeterQueueObject : ConfigurableObject
	{
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000D0587 File Offset: 0x000CE787
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PerimeterQueueObject.schema;
			}
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000D058E File Offset: 0x000CE78E
		internal PerimeterQueueObject(PerimeterQueue queue, PerimeterQueueStatus status) : base(new PerimeterQueuePropertyBag(false, 16))
		{
			this.Identity = new PerimeterQueueId(queue.Name);
			this.MessageCount = queue.Value;
			this.Status = status;
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000D05C4 File Offset: 0x000CE7C4
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000D05C7 File Offset: 0x000CE7C7
		// (set) Token: 0x06003329 RID: 13097 RVA: 0x000D05DE File Offset: 0x000CE7DE
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this.propertyBag[PerimeterQueueStatusSchema.Identity];
			}
			internal set
			{
				this.propertyBag[PerimeterQueueStatusSchema.Identity] = value;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000D05F1 File Offset: 0x000CE7F1
		// (set) Token: 0x0600332B RID: 13099 RVA: 0x000D0608 File Offset: 0x000CE808
		public int MessageCount
		{
			get
			{
				return (int)this.propertyBag[PerimeterQueueStatusSchema.MessageCount];
			}
			internal set
			{
				this.propertyBag[PerimeterQueueStatusSchema.MessageCount] = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000D0620 File Offset: 0x000CE820
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x000D0637 File Offset: 0x000CE837
		public PerimeterQueueStatus Status
		{
			get
			{
				return (PerimeterQueueStatus)this.propertyBag[PerimeterQueueStatusSchema.Status];
			}
			internal set
			{
				this.propertyBag[PerimeterQueueStatusSchema.Status] = value;
			}
		}

		// Token: 0x040023B4 RID: 9140
		private static PerimeterQueueStatusSchema schema = ObjectSchema.GetInstance<PerimeterQueueStatusSchema>();
	}
}
