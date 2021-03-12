using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DFE RID: 3582
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientManifestEntry : ServerManifestEntry
	{
		// Token: 0x06007B26 RID: 31526 RVA: 0x0021FDAB File Offset: 0x0021DFAB
		public ClientManifestEntry()
		{
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x0021FDB3 File Offset: 0x0021DFB3
		public ClientManifestEntry(ISyncItemId id) : base(id)
		{
			this.clientAddId = null;
			base.Watermark = null;
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06007B28 RID: 31528 RVA: 0x0021FDCA File Offset: 0x0021DFCA
		// (set) Token: 0x06007B29 RID: 31529 RVA: 0x0021FDD2 File Offset: 0x0021DFD2
		public string ClientAddId
		{
			get
			{
				return this.clientAddId;
			}
			set
			{
				this.clientAddId = value;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06007B2A RID: 31530 RVA: 0x0021FDDB File Offset: 0x0021DFDB
		// (set) Token: 0x06007B2B RID: 31531 RVA: 0x0021FDE3 File Offset: 0x0021DFE3
		public bool SoftDeletePending
		{
			get
			{
				return this.softDeletePending;
			}
			set
			{
				this.softDeletePending = value;
			}
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x0021FDEC File Offset: 0x0021DFEC
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.DeserializeData(reader, componentDataPool);
			StringData stringDataInstance = componentDataPool.GetStringDataInstance();
			stringDataInstance.DeserializeData(reader, componentDataPool);
			this.clientAddId = stringDataInstance.Data;
			this.softDeletePending = reader.ReadBoolean();
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x0021FE28 File Offset: 0x0021E028
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			base.SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.clientAddId).SerializeData(writer, componentDataPool);
			writer.Write(this.softDeletePending);
		}

		// Token: 0x040054BF RID: 21695
		private string clientAddId;

		// Token: 0x040054C0 RID: 21696
		private bool softDeletePending;
	}
}
