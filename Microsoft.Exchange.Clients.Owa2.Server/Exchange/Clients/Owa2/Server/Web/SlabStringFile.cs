using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000492 RID: 1170
	[DataContract]
	public class SlabStringFile : SlabSourceFile
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00092EF7 File Offset: 0x000910F7
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x00092EFF File Offset: 0x000910FF
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type { get; set; }

		// Token: 0x060027D2 RID: 10194 RVA: 0x00092F08 File Offset: 0x00091108
		public override bool Equals(object obj)
		{
			SlabStringFile slabStringFile = obj as SlabStringFile;
			return slabStringFile != null && this.Type == slabStringFile.Type && base.Equals(obj);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x00092F3B File Offset: 0x0009113B
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x00092F4F File Offset: 0x0009114F
		public bool IsExtensibility()
		{
			return this.Type != null && this.Type.Equals("Extensibility", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x00092F6C File Offset: 0x0009116C
		public bool IsStandard()
		{
			return this.Type == null || this.Type.Equals("Standard", StringComparison.OrdinalIgnoreCase);
		}
	}
}
