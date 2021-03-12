using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000115 RID: 277
	internal class DomainProperty : GlsProperty
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x00035684 File Offset: 0x00033884
		static DomainProperty()
		{
			DomainProperty.properties.Add(DomainProperty.region.Name, DomainProperty.region);
			DomainProperty.properties.Add(DomainProperty.serviceVersion.Name, DomainProperty.serviceVersion);
			DomainProperty.properties.Add(DomainProperty.domainInUse.Name, DomainProperty.domainInUse);
			DomainProperty.properties.Add(DomainProperty.ExoFlags.Name, DomainProperty.ExoFlags);
			DomainProperty.properties.Add(DomainProperty.ExoDomainInUse.Name, DomainProperty.ExoDomainInUse);
			DomainProperty.properties.Add(DomainProperty.ipv6.Name, DomainProperty.ipv6);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00035809 File Offset: 0x00033A09
		protected DomainProperty(string name, Type dataType) : base(name, dataType, null)
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00035814 File Offset: 0x00033A14
		protected DomainProperty(string name, Type dataType, object defaultValue) : base(name, dataType, defaultValue)
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0003581F File Offset: 0x00033A1F
		internal static DomainProperty Get(string name)
		{
			return DomainProperty.properties[name];
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0003582C File Offset: 0x00033A2C
		internal static DomainProperty Region
		{
			get
			{
				return DomainProperty.region;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00035833 File Offset: 0x00033A33
		internal static DomainProperty ServiceVersion
		{
			get
			{
				return DomainProperty.serviceVersion;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003583A File Offset: 0x00033A3A
		internal static DomainProperty DomainInUse
		{
			get
			{
				return DomainProperty.domainInUse;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00035841 File Offset: 0x00033A41
		internal static DomainProperty IPv6
		{
			get
			{
				return DomainProperty.ipv6;
			}
		}

		// Token: 0x040005D7 RID: 1495
		private static readonly DomainProperty region = new DomainProperty(GlsProperty.FfoPrefix + ".Region", typeof(string));

		// Token: 0x040005D8 RID: 1496
		private static readonly DomainProperty serviceVersion = new DomainProperty(GlsProperty.FfoPrefix + ".Version", typeof(string));

		// Token: 0x040005D9 RID: 1497
		private static readonly DomainProperty domainInUse = new DomainProperty(GlsProperty.FfoPrefix + ".DomainInUse", typeof(bool));

		// Token: 0x040005DA RID: 1498
		private static readonly DomainProperty ipv6 = new DomainProperty(GlsProperty.FfoPrefix + ".IPv6", typeof(int), 0);

		// Token: 0x040005DB RID: 1499
		internal static readonly DomainProperty ExoFlags = new DomainProperty(GlsProperty.ExoPrefix + ".Flags", typeof(int));

		// Token: 0x040005DC RID: 1500
		internal static readonly DomainProperty ExoDomainInUse = new DomainProperty(GlsProperty.ExoPrefix + ".DomainInUse", typeof(bool));

		// Token: 0x040005DD RID: 1501
		private static IDictionary<string, DomainProperty> properties = new Dictionary<string, DomainProperty>();
	}
}
