using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200021E RID: 542
	internal class MergedCookie : BaseCookie
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x000450B0 File Offset: 0x000432B0
		public MergedCookie()
		{
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000450B8 File Offset: 0x000432B8
		public MergedCookie(string contextId, string serviceInstance, byte[] data) : base(serviceInstance, data)
		{
			if (contextId == null)
			{
				throw new ArgumentNullException("contextId");
			}
			this.ContextId = new ADObjectId(Guid.Parse(contextId));
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x000450E1 File Offset: 0x000432E1
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x000450F3 File Offset: 0x000432F3
		public ADObjectId ContextId
		{
			get
			{
				return this[MergedCookieSchema.ContextIdProperty] as ADObjectId;
			}
			set
			{
				this[MergedCookieSchema.ContextIdProperty] = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00045101 File Offset: 0x00043301
		internal override ADObjectSchema Schema
		{
			get
			{
				return MergedCookie.schema;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00045108 File Offset: 0x00043308
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MergedCookie.mostDerivedClass;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0004510F File Offset: 0x0004330F
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000B41 RID: 2881
		private static readonly MergedCookieSchema schema = ObjectSchema.GetInstance<MergedCookieSchema>();

		// Token: 0x04000B42 RID: 2882
		private static string mostDerivedClass = "MergedCookie";
	}
}
