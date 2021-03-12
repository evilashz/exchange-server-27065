using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C8 RID: 2504
	[Serializable]
	public class EwsStoreObjectId : ObjectId, ISerializable
	{
		// Token: 0x06005C78 RID: 23672 RVA: 0x0018165C File Offset: 0x0017F85C
		public EwsStoreObjectId(ItemId ewsObjectId)
		{
			this.ewsObjectId = ewsObjectId;
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x0018166B File Offset: 0x0017F86B
		public EwsStoreObjectId(string uniqueId) : this(new ItemId(uniqueId))
		{
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x00181679 File Offset: 0x0017F879
		public EwsStoreObjectId(byte[] bytes) : this(Encoding.UTF8.GetString(bytes))
		{
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x0018168C File Offset: 0x0017F88C
		private EwsStoreObjectId(SerializationInfo info, StreamingContext context) : this(info.GetString("id"))
		{
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x001816A0 File Offset: 0x0017F8A0
		public static bool TryParse(string id, out EwsStoreObjectId ewsStoreObjectId)
		{
			ewsStoreObjectId = null;
			try
			{
				StoreId.EwsIdToStoreObjectId(id);
			}
			catch (InvalidIdMalformedException)
			{
				return false;
			}
			catch (InvalidIdNotAnItemAttachmentIdException)
			{
				return false;
			}
			ewsStoreObjectId = new EwsStoreObjectId(id);
			return true;
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x001816EC File Offset: 0x0017F8EC
		public override bool Equals(object obj)
		{
			return obj != null && obj is EwsStoreObjectId && this.EwsObjectId.Equals(((EwsStoreObjectId)obj).EwsObjectId);
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x00181711 File Offset: 0x0017F911
		public override int GetHashCode()
		{
			return this.EwsObjectId.GetHashCode();
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x0018171E File Offset: 0x0017F91E
		public override string ToString()
		{
			return this.EwsObjectId.ToString();
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x0018172B File Offset: 0x0017F92B
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x0018173D File Offset: 0x0017F93D
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("id", this.ToString());
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x00181750 File Offset: 0x0017F950
		internal ItemId EwsObjectId
		{
			get
			{
				return this.ewsObjectId;
			}
		}

		// Token: 0x040032F1 RID: 13041
		[NonSerialized]
		private ItemId ewsObjectId;
	}
}
