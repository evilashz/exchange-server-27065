using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E39 RID: 3641
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ComponentData<DataType> : ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x06007E5A RID: 32346 RVA: 0x0022BFAF File Offset: 0x0022A1AF
		protected ComponentData()
		{
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x0022BFB7 File Offset: 0x0022A1B7
		protected ComponentData(DataType data)
		{
			this.data = data;
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x06007E5C RID: 32348 RVA: 0x0022BFC6 File Offset: 0x0022A1C6
		// (set) Token: 0x06007E5D RID: 32349 RVA: 0x0022BFCE File Offset: 0x0022A1CE
		public DataType Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x06007E5E RID: 32350
		// (set) Token: 0x06007E5F RID: 32351
		public abstract ushort TypeId { get; set; }

		// Token: 0x06007E60 RID: 32352 RVA: 0x0022BFD7 File Offset: 0x0022A1D7
		public ComponentData<DataType> Bind(DataType data)
		{
			this.data = data;
			return this;
		}

		// Token: 0x06007E61 RID: 32353
		public abstract ICustomSerializable BuildObject();

		// Token: 0x06007E62 RID: 32354
		public abstract void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool);

		// Token: 0x06007E63 RID: 32355
		public abstract void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool);

		// Token: 0x040055E9 RID: 21993
		private DataType data;
	}
}
