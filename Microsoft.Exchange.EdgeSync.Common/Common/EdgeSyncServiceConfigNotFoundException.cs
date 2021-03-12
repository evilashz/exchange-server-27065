using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EdgeSyncServiceConfigNotFoundException : LocalizedException
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000B05C File Offset: 0x0000925C
		public EdgeSyncServiceConfigNotFoundException(string site, string dn) : base(Strings.EdgeSyncServiceConfigNotFoundException(site, dn))
		{
			this.site = site;
			this.dn = dn;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B079 File Offset: 0x00009279
		public EdgeSyncServiceConfigNotFoundException(string site, string dn, Exception innerException) : base(Strings.EdgeSyncServiceConfigNotFoundException(site, dn), innerException)
		{
			this.site = site;
			this.dn = dn;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B098 File Offset: 0x00009298
		protected EdgeSyncServiceConfigNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.site = (string)info.GetValue("site", typeof(string));
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B0ED File Offset: 0x000092ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("site", this.site);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B119 File Offset: 0x00009319
		public string Site
		{
			get
			{
				return this.site;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B121 File Offset: 0x00009321
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x040001E0 RID: 480
		private readonly string site;

		// Token: 0x040001E1 RID: 481
		private readonly string dn;
	}
}
