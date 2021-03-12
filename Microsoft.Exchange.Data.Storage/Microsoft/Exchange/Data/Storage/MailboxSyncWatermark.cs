using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E5F RID: 3679
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSyncWatermark : ISyncWatermark, ICustomSerializableBuilder, ICustomSerializable, IComparable, ICloneable, ICustomClonable
	{
		// Token: 0x06007F57 RID: 32599 RVA: 0x0022E447 File Offset: 0x0022C647
		public MailboxSyncWatermark()
		{
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x0022E44F File Offset: 0x0022C64F
		protected MailboxSyncWatermark(int changeNumber)
		{
			this.changeNumber = changeNumber;
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x06007F59 RID: 32601 RVA: 0x0022E45E File Offset: 0x0022C65E
		// (set) Token: 0x06007F5A RID: 32602 RVA: 0x0022E466 File Offset: 0x0022C666
		public int ChangeNumber
		{
			get
			{
				return this.changeNumber;
			}
			set
			{
				this.changeNumber = value;
			}
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x06007F5B RID: 32603 RVA: 0x0022E46F File Offset: 0x0022C66F
		public int RawChangeNumber
		{
			get
			{
				return this.changeNumber & int.MaxValue;
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x06007F5C RID: 32604 RVA: 0x0022E47D File Offset: 0x0022C67D
		// (set) Token: 0x06007F5D RID: 32605 RVA: 0x0022E485 File Offset: 0x0022C685
		public byte[] IcsState
		{
			get
			{
				return this.icsState;
			}
			set
			{
				this.icsState = value;
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x06007F5E RID: 32606 RVA: 0x0022E48E File Offset: 0x0022C68E
		public bool IsNew
		{
			get
			{
				return 0 == this.changeNumber;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x06007F5F RID: 32607 RVA: 0x0022E499 File Offset: 0x0022C699
		// (set) Token: 0x06007F60 RID: 32608 RVA: 0x0022E4A0 File Offset: 0x0022C6A0
		public ushort TypeId
		{
			get
			{
				return MailboxSyncWatermark.typeId;
			}
			set
			{
				MailboxSyncWatermark.typeId = value;
			}
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x0022E4A8 File Offset: 0x0022C6A8
		public static MailboxSyncWatermark Create()
		{
			return new MailboxSyncWatermark();
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x0022E4AF File Offset: 0x0022C6AF
		public static MailboxSyncWatermark CreateForSingleItem()
		{
			return new MailboxSyncWatermark();
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x0022E4B6 File Offset: 0x0022C6B6
		public static MailboxSyncWatermark CreateWithChangeNumber(int changeNumber)
		{
			return new MailboxSyncWatermark(changeNumber);
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x0022E4BE File Offset: 0x0022C6BE
		public virtual ICustomSerializable BuildObject()
		{
			return new MailboxSyncWatermark();
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x0022E4C8 File Offset: 0x0022C6C8
		public virtual object Clone()
		{
			MailboxSyncWatermark mailboxSyncWatermark = MailboxSyncWatermark.CreateWithChangeNumber(this.ChangeNumber);
			if (this.icsState != null)
			{
				mailboxSyncWatermark.IcsState = (byte[])this.icsState.Clone();
			}
			return mailboxSyncWatermark;
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x0022E500 File Offset: 0x0022C700
		public virtual object CustomClone()
		{
			return MailboxSyncWatermark.CreateWithChangeNumber(this.ChangeNumber);
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x0022E510 File Offset: 0x0022C710
		public int CompareTo(object thatObject)
		{
			MailboxSyncWatermark mailboxSyncWatermark = (MailboxSyncWatermark)thatObject;
			int rawChangeNumber = this.RawChangeNumber;
			int rawChangeNumber2 = mailboxSyncWatermark.RawChangeNumber;
			if (rawChangeNumber < rawChangeNumber2)
			{
				return -1;
			}
			if (rawChangeNumber > rawChangeNumber2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x0022E540 File Offset: 0x0022C740
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
			byteArrayInstance.DeserializeData(reader, componentDataPool);
			this.icsState = byteArrayInstance.Data;
			this.changeNumber = reader.ReadInt32();
		}

		// Token: 0x06007F69 RID: 32617 RVA: 0x0022E574 File Offset: 0x0022C774
		public override bool Equals(object thatObject)
		{
			if (thatObject == null)
			{
				return false;
			}
			MailboxSyncWatermark mailboxSyncWatermark = thatObject as MailboxSyncWatermark;
			return mailboxSyncWatermark != null && this.ChangeNumber == mailboxSyncWatermark.ChangeNumber;
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x0022E5A0 File Offset: 0x0022C7A0
		public override int GetHashCode()
		{
			throw new NotImplementedException("MailboxSyncWatermark.GetHashCode()");
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x0022E5AC File Offset: 0x0022C7AC
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetByteArrayInstance().Bind(this.icsState).SerializeData(writer, componentDataPool);
			writer.Write(this.changeNumber);
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x0022E5D2 File Offset: 0x0022C7D2
		public void UpdateWithChangeNumber(int changeNumber, bool read)
		{
			this.changeNumber = changeNumber;
			if (read)
			{
				this.changeNumber |= int.MinValue;
				return;
			}
			this.changeNumber &= int.MaxValue;
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x0022E603 File Offset: 0x0022C803
		public override string ToString()
		{
			return string.Format("CN: {0}", this.changeNumber);
		}

		// Token: 0x0400563F RID: 22079
		private static ushort typeId;

		// Token: 0x04005640 RID: 22080
		private int changeNumber;

		// Token: 0x04005641 RID: 22081
		private byte[] icsState;
	}
}
