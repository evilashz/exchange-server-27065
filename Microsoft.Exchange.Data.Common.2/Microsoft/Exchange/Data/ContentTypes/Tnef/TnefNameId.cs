using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000EE RID: 238
	public struct TnefNameId
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x0003484E File Offset: 0x00032A4E
		public TnefNameId(Guid propertySetGuid, int id)
		{
			this.propertySetGuid = propertySetGuid;
			this.id = id;
			this.name = null;
			this.kind = TnefNameIdKind.Id;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0003486C File Offset: 0x00032A6C
		public TnefNameId(Guid propertySetGuid, string name)
		{
			this.propertySetGuid = propertySetGuid;
			this.id = 0;
			this.name = name;
			this.kind = TnefNameIdKind.Name;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0003488A File Offset: 0x00032A8A
		public Guid PropertySetGuid
		{
			get
			{
				return this.propertySetGuid;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00034892 File Offset: 0x00032A92
		public TnefNameIdKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0003489A File Offset: 0x00032A9A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000348A2 File Offset: 0x00032AA2
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000348AA File Offset: 0x00032AAA
		internal void Set(Guid propertySetGuid, int id)
		{
			this.propertySetGuid = propertySetGuid;
			this.id = id;
			this.name = null;
			this.kind = TnefNameIdKind.Id;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000348C8 File Offset: 0x00032AC8
		internal void Set(Guid propertySetGuid, string name)
		{
			this.propertySetGuid = propertySetGuid;
			this.id = 0;
			this.name = name;
			this.kind = TnefNameIdKind.Name;
		}

		// Token: 0x04000CD1 RID: 3281
		private Guid propertySetGuid;

		// Token: 0x04000CD2 RID: 3282
		private int id;

		// Token: 0x04000CD3 RID: 3283
		private string name;

		// Token: 0x04000CD4 RID: 3284
		private TnefNameIdKind kind;
	}
}
