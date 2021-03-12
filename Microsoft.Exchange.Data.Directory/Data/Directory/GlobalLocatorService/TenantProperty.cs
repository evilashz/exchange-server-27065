using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000133 RID: 307
	internal class TenantProperty : GlsProperty
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00038C0C File Offset: 0x00036E0C
		static TenantProperty()
		{
			TenantProperty.properties.Add(TenantProperty.EXOResourceForest.Name, TenantProperty.EXOResourceForest);
			TenantProperty.properties.Add(TenantProperty.EXOAccountForest.Name, TenantProperty.EXOAccountForest);
			TenantProperty.properties.Add(TenantProperty.EXOPrimarySite.Name, TenantProperty.EXOPrimarySite);
			TenantProperty.properties.Add(TenantProperty.EXOSmtpNextHopDomain.Name, TenantProperty.EXOSmtpNextHopDomain);
			TenantProperty.properties.Add(TenantProperty.EXOTenantFlags.Name, TenantProperty.EXOTenantFlags);
			TenantProperty.properties.Add(TenantProperty.EXOTenantContainerCN.Name, TenantProperty.EXOTenantContainerCN);
			TenantProperty.properties.Add(TenantProperty.CustomerType.Name, TenantProperty.CustomerType);
			TenantProperty.properties.Add(TenantProperty.Version.Name, TenantProperty.Version);
			TenantProperty.properties.Add(TenantProperty.Region.Name, TenantProperty.Region);
			TenantProperty.properties.Add(TenantProperty.GlobalResumeCache.Name, TenantProperty.GlobalResumeCache);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00038E81 File Offset: 0x00037081
		protected TenantProperty(string name, Type dataType) : base(name, dataType, null)
		{
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00038E8C File Offset: 0x0003708C
		protected TenantProperty(string name, Type dataType, object defaultValue) : base(name, dataType, defaultValue)
		{
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00038E97 File Offset: 0x00037097
		internal static TenantProperty Get(string name)
		{
			return TenantProperty.properties[name];
		}

		// Token: 0x0400068D RID: 1677
		internal static readonly TenantProperty EXOResourceForest = new TenantProperty(GlsProperty.ExoPrefix + ".ResourceForest", typeof(string));

		// Token: 0x0400068E RID: 1678
		internal static readonly TenantProperty EXOAccountForest = new TenantProperty(GlsProperty.ExoPrefix + ".AccountForest", typeof(string));

		// Token: 0x0400068F RID: 1679
		internal static readonly TenantProperty EXOPrimarySite = new TenantProperty(GlsProperty.ExoPrefix + ".PrimarySite", typeof(string));

		// Token: 0x04000690 RID: 1680
		internal static readonly TenantProperty EXOSmtpNextHopDomain = new TenantProperty(GlsProperty.ExoPrefix + ".SmtpNextHopDomain", typeof(string));

		// Token: 0x04000691 RID: 1681
		internal static readonly TenantProperty EXOTenantFlags = new TenantProperty(GlsProperty.ExoPrefix + ".TenantFlags", typeof(int));

		// Token: 0x04000692 RID: 1682
		internal static readonly TenantProperty EXOTenantContainerCN = new TenantProperty(GlsProperty.ExoPrefix + ".TenantContainerCN", typeof(string));

		// Token: 0x04000693 RID: 1683
		internal static readonly TenantProperty CustomerType = new TenantProperty(GlsProperty.FfoPrefix + ".CustomerType", typeof(int), -1);

		// Token: 0x04000694 RID: 1684
		internal static readonly TenantProperty Version = new TenantProperty(GlsProperty.FfoPrefix + ".Version", typeof(string));

		// Token: 0x04000695 RID: 1685
		internal static readonly TenantProperty Region = new TenantProperty(GlsProperty.FfoPrefix + ".Region", typeof(string));

		// Token: 0x04000696 RID: 1686
		internal static readonly TenantProperty GlobalResumeCache = new TenantProperty(GlsProperty.GlobalPrefix + ".ResumeCache", typeof(string));

		// Token: 0x04000697 RID: 1687
		private static IDictionary<string, TenantProperty> properties = new Dictionary<string, TenantProperty>();
	}
}
