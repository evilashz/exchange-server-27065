using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000491 RID: 1169
	[DataContract]
	public class SlabSourceFile : LayoutDependentResource
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00092E97 File Offset: 0x00091097
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x00092E9F File Offset: 0x0009109F
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x060027CD RID: 10189 RVA: 0x00092EA8 File Offset: 0x000910A8
		public override bool Equals(object obj)
		{
			SlabSourceFile slabSourceFile = obj as SlabSourceFile;
			return slabSourceFile != null && this.Name == slabSourceFile.Name && base.Equals(obj);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00092EDB File Offset: 0x000910DB
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ base.GetHashCode();
		}
	}
}
