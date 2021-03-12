using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200029B RID: 667
	internal sealed class VirtualFolderItemId : ISyncItemId, ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
		public VirtualFolderItemId()
		{
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0008F1D0 File Offset: 0x0008D3D0
		public VirtualFolderItemId(string nativeId)
		{
			this.nativeId = nativeId;
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0008F1DF File Offset: 0x0008D3DF
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x0008F1E6 File Offset: 0x0008D3E6
		public ushort TypeId
		{
			get
			{
				return VirtualFolderItemId.typeId;
			}
			set
			{
				VirtualFolderItemId.typeId = value;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0008F1EE File Offset: 0x0008D3EE
		public object NativeId
		{
			get
			{
				return this.nativeId;
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0008F1F8 File Offset: 0x0008D3F8
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			StringData stringDataInstance = componentDataPool.GetStringDataInstance();
			stringDataInstance.DeserializeData(reader, componentDataPool);
			this.nativeId = stringDataInstance.Data;
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0008F220 File Offset: 0x0008D420
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetStringDataInstance().Bind(this.nativeId).SerializeData(writer, componentDataPool);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0008F23A File Offset: 0x0008D43A
		public ICustomSerializable BuildObject()
		{
			return new VirtualFolderItemId(null);
		}

		// Token: 0x04000F1C RID: 3868
		private static ushort typeId;

		// Token: 0x04000F1D RID: 3869
		private string nativeId;
	}
}
