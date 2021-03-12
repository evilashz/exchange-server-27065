using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200001D RID: 29
	internal class MdbCopy
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000066C6 File Offset: 0x000048C6
		public MdbCopy(string name, int activationPreference, int schemaVersion)
		{
			this.Name = name;
			this.ActivationPreference = activationPreference;
			this.SchemaVersion = schemaVersion;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000066E3 File Offset: 0x000048E3
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000066EB File Offset: 0x000048EB
		public string Name { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000066F4 File Offset: 0x000048F4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000066FC File Offset: 0x000048FC
		public int ActivationPreference { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00006705 File Offset: 0x00004905
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000670D File Offset: 0x0000490D
		public int SchemaVersion { get; private set; }
	}
}
