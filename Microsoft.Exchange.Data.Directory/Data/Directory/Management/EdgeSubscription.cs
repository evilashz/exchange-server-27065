using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006FD RID: 1789
	[Serializable]
	public sealed class EdgeSubscription : ADPresentationObject
	{
		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x06005445 RID: 21573 RVA: 0x001319EC File Offset: 0x0012FBEC
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return EdgeSubscription.schema;
			}
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x06005446 RID: 21574 RVA: 0x001319F3 File Offset: 0x0012FBF3
		public ADObjectId Site
		{
			get
			{
				if (this.server != null)
				{
					return this.server.ServerSite;
				}
				return null;
			}
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x00131A0A File Offset: 0x0012FC0A
		public string Domain
		{
			get
			{
				if (this.server != null)
				{
					return this.server.Domain;
				}
				return null;
			}
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00131A21 File Offset: 0x0012FC21
		public EdgeSubscription()
		{
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00131A29 File Offset: 0x0012FC29
		public EdgeSubscription(Server dataObject) : base(dataObject)
		{
			this.server = dataObject;
		}

		// Token: 0x0400389A RID: 14490
		private static EdgeSubscriptionSchema schema = ObjectSchema.GetInstance<EdgeSubscriptionSchema>();

		// Token: 0x0400389B RID: 14491
		private Server server;
	}
}
