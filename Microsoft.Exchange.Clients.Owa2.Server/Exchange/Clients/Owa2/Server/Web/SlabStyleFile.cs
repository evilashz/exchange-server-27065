using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000493 RID: 1171
	[DataContract]
	public class SlabStyleFile : LayoutDependentResource
	{
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x00092F91 File Offset: 0x00091191
		// (set) Token: 0x060027D8 RID: 10200 RVA: 0x00092F99 File Offset: 0x00091199
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x00092FA2 File Offset: 0x000911A2
		// (set) Token: 0x060027DA RID: 10202 RVA: 0x00092FAA File Offset: 0x000911AA
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type { get; set; }

		// Token: 0x060027DB RID: 10203 RVA: 0x00092FB3 File Offset: 0x000911B3
		public bool IsNotThemed()
		{
			return this.Type != null && this.Type.Equals("NotThemed", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x00092FD0 File Offset: 0x000911D0
		public bool IsSprite()
		{
			return this.Type != null && (this.Type.Equals("Sprite", StringComparison.OrdinalIgnoreCase) || this.Type.Equals("HighResolution", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x00093002 File Offset: 0x00091202
		public bool IsHighResolutionSprite()
		{
			return this.Type != null && this.Type.Equals("HighResolution", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x00093020 File Offset: 0x00091220
		public override bool Equals(object obj)
		{
			SlabStyleFile slabStyleFile = obj as SlabStyleFile;
			return slabStyleFile != null && this.Type == slabStyleFile.Type && this.Name == slabStyleFile.Name && base.Equals(obj);
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00093066 File Offset: 0x00091266
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() ^ this.Name.GetHashCode() ^ base.GetHashCode();
		}
	}
}
