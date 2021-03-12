using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000054 RID: 84
	internal abstract class CollectionContainerResource<T> : Resource where T : Resource
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000091EA File Offset: 0x000073EA
		protected CollectionContainerResource(string collectionMemberName, string selfUri) : base(selfUri)
		{
			this.collectionMemberName = collectionMemberName;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000091FA File Offset: 0x000073FA
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00009208 File Offset: 0x00007408
		public ResourceCollection<T> Members
		{
			get
			{
				return base.GetValue<ResourceCollection<T>>(this.collectionMemberName);
			}
			set
			{
				base.SetValue<ResourceCollection<T>>(this.collectionMemberName, value);
			}
		}

		// Token: 0x040001AB RID: 427
		private readonly string collectionMemberName;
	}
}
