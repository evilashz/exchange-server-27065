using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A82 RID: 2690
	[Serializable]
	public class MessageCategoryId : XsoMailboxObjectId
	{
		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06006275 RID: 25205 RVA: 0x0019FFD2 File Offset: 0x0019E1D2
		internal Guid? CategoryId
		{
			get
			{
				return this.categoryId;
			}
		}

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x06006276 RID: 25206 RVA: 0x0019FFDA File Offset: 0x0019E1DA
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x0019FFE2 File Offset: 0x0019E1E2
		internal MessageCategoryId(ADObjectId mailboxOwnerId, string name, Guid? categoryId) : base(mailboxOwnerId)
		{
			this.categoryId = categoryId;
			this.name = name;
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x0019FFFC File Offset: 0x0019E1FC
		public override byte[] GetBytes()
		{
			byte[] array = (this.categoryId != null) ? this.categoryId.Value.ToByteArray() : Array<byte>.Empty;
			byte[] bytes = base.MailboxOwnerId.GetBytes();
			byte[] array2 = new byte[2 + bytes.Length + array.Length];
			int num = 0;
			array2[num++] = (byte)(bytes.Length & 255);
			array2[num++] = (byte)(bytes.Length >> 8 & 255);
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			return array2;
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x001A0094 File Offset: 0x0019E294
		public override int GetHashCode()
		{
			int num = base.MailboxOwnerId.GetHashCode();
			if (this.categoryId != null)
			{
				num ^= this.CategoryId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x001A00D4 File Offset: 0x0019E2D4
		public override bool Equals(XsoMailboxObjectId other)
		{
			MessageCategoryId messageCategoryId = other as MessageCategoryId;
			return !(null == messageCategoryId) && ADObjectId.Equals(base.MailboxOwnerId, other.MailboxOwnerId) && object.Equals(this.categoryId ?? Guid.Empty, messageCategoryId.CategoryId ?? Guid.Empty);
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x001A0154 File Offset: 0x0019E354
		public override string ToString()
		{
			string arg;
			if (this.categoryId != null)
			{
				arg = this.categoryId.Value.ToString();
			}
			else if (!string.IsNullOrEmpty(this.name))
			{
				arg = this.name;
			}
			else
			{
				arg = string.Empty;
			}
			return string.Format("{0}{1}{2}", base.MailboxOwnerId, '\\', arg);
		}

		// Token: 0x040037C5 RID: 14277
		public const char Separator = '\\';

		// Token: 0x040037C6 RID: 14278
		private Guid? categoryId;

		// Token: 0x040037C7 RID: 14279
		private string name;
	}
}
