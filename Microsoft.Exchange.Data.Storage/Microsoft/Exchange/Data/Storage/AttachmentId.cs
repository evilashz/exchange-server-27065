using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200034F RID: 847
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentId : IEquatable<AttachmentId>
	{
		// Token: 0x060025AB RID: 9643 RVA: 0x00097314 File Offset: 0x00095514
		internal AttachmentId(byte[] attachKey)
		{
			this.AttachKey = attachKey;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0009732C File Offset: 0x0009552C
		public static AttachmentId Deserialize(byte[] byteArrayId)
		{
			if (byteArrayId == null)
			{
				throw new ArgumentNullException("byteArrayId");
			}
			AttachmentId result;
			if (AttachmentId.TryParse(byteArrayId, out result))
			{
				return result;
			}
			throw new CorruptDataException(ServerStrings.InvalidAttachmentId);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00097360 File Offset: 0x00095560
		public static AttachmentId Deserialize(string base64Id)
		{
			if (base64Id == null)
			{
				throw new ArgumentNullException(base64Id);
			}
			byte[] byteArrayId = StoreId.Base64ToByteArray(base64Id);
			return AttachmentId.Deserialize(byteArrayId);
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00097384 File Offset: 0x00095584
		public int GetByteArrayLength()
		{
			short num = 0;
			if (this.attachKey != null)
			{
				num = (short)this.attachKey.Length;
			}
			return (int)(2 + num);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000973A8 File Offset: 0x000955A8
		public byte[] ToByteArray()
		{
			short num = 0;
			if (this.attachKey != null)
			{
				num = (short)this.attachKey.Length;
			}
			int num2 = (int)(2 + num);
			byte[] array = new byte[num2];
			int num3 = 0;
			num3 += ExBitConverter.Write(num, array, num3);
			if (this.attachKey != null)
			{
				this.attachKey.CopyTo(array, num3);
			}
			return array;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000973F7 File Offset: 0x000955F7
		public string ToBase64String()
		{
			return Convert.ToBase64String(this.ToByteArray());
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00097404 File Offset: 0x00095604
		public override bool Equals(object id)
		{
			AttachmentId id2 = id as AttachmentId;
			return this.Equals(id2);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0009741F File Offset: 0x0009561F
		public bool Equals(AttachmentId id)
		{
			return id != null && ArrayComparer<byte>.Comparer.Equals(this.attachKey, id.attachKey);
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0009743C File Offset: 0x0009563C
		public override int GetHashCode()
		{
			if (this.hashCode == -1 && this.attachKey != null)
			{
				int num = 0;
				int num2 = 8;
				int num3 = this.attachKey.Length - 1;
				while (num3 >= 0 && num2 > 0)
				{
					num ^= ((int)this.attachKey[num3] << 8) * (num3 % 4);
					num2--;
					num3--;
				}
				this.hashCode = num;
			}
			return this.hashCode;
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00097499 File Offset: 0x00095699
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x000974A1 File Offset: 0x000956A1
		internal byte[] AttachKey
		{
			get
			{
				return this.attachKey;
			}
			private set
			{
				this.attachKey = value;
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000974AC File Offset: 0x000956AC
		internal static bool TryParse(byte[] idBytes, out AttachmentId id)
		{
			id = null;
			if (idBytes.Length < 2)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(-1L, "AttachmentId::TryParse. The index head of the attchment Id bytes is incomplete. id = {0}.", Convert.ToBase64String(idBytes));
				return false;
			}
			short num = BitConverter.ToInt16(idBytes, 0);
			int num2 = 2;
			byte[] destinationArray = null;
			if (num > 0)
			{
				if ((int)num + num2 > idBytes.Length)
				{
					return false;
				}
				destinationArray = new byte[(int)num];
				Array.Copy(idBytes, num2, destinationArray, 0, (int)num);
			}
			id = new AttachmentId(destinationArray);
			return true;
		}

		// Token: 0x040016B3 RID: 5811
		private int hashCode = -1;

		// Token: 0x040016B4 RID: 5812
		private byte[] attachKey;
	}
}
