using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000490 RID: 1168
	[DataContract]
	public class SlabConfiguration : LayoutDependentResource
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x00092E35 File Offset: 0x00091035
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x00092E3D File Offset: 0x0009103D
		[DataMember(Name = "type")]
		public string Type { get; set; }

		// Token: 0x060027C8 RID: 10184 RVA: 0x00092E48 File Offset: 0x00091048
		public override bool Equals(object obj)
		{
			SlabConfiguration slabConfiguration = obj as SlabConfiguration;
			return slabConfiguration != null && this.Type == slabConfiguration.Type && base.Equals(obj);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00092E7B File Offset: 0x0009107B
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() ^ base.GetHashCode();
		}
	}
}
