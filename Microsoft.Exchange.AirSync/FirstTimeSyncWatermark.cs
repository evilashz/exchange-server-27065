using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000267 RID: 615
	internal sealed class FirstTimeSyncWatermark : ISyncWatermark, ICustomSerializableBuilder, ICustomSerializable, IComparable, ICloneable, ICustomClonable
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x000896EA File Offset: 0x000878EA
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x000896F2 File Offset: 0x000878F2
		public ExDateTime ReceivedDateUtc { get; set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000896FB File Offset: 0x000878FB
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x00089703 File Offset: 0x00087903
		public int ChangeNumber { get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0008970C File Offset: 0x0008790C
		public int RawChangeNumber
		{
			get
			{
				return this.ChangeNumber & int.MaxValue;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0008971A File Offset: 0x0008791A
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00089722 File Offset: 0x00087922
		public byte[] IcsState { get; set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0008972B File Offset: 0x0008792B
		public bool IsNew
		{
			get
			{
				return (DateTime)this.ReceivedDateUtc == DateTime.MinValue;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00089742 File Offset: 0x00087942
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x00089749 File Offset: 0x00087949
		public ushort TypeId
		{
			get
			{
				return FirstTimeSyncWatermark.typeId;
			}
			set
			{
				FirstTimeSyncWatermark.typeId = value;
			}
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00089751 File Offset: 0x00087951
		public static FirstTimeSyncWatermark CreateNew()
		{
			return FirstTimeSyncWatermark.Create(ExDateTime.MinValue, -1);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00089760 File Offset: 0x00087960
		public static FirstTimeSyncWatermark Create(ExDateTime receivedDate, int changeNumber)
		{
			return new FirstTimeSyncWatermark
			{
				ReceivedDateUtc = receivedDate,
				ChangeNumber = changeNumber
			};
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00089784 File Offset: 0x00087984
		public static FirstTimeSyncWatermark Create(ExDateTime receivedDate, int changeNumber, bool read)
		{
			FirstTimeSyncWatermark firstTimeSyncWatermark = new FirstTimeSyncWatermark();
			firstTimeSyncWatermark.Update(changeNumber, read, receivedDate);
			return firstTimeSyncWatermark;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000897A1 File Offset: 0x000879A1
		public ICustomSerializable BuildObject()
		{
			return new FirstTimeSyncWatermark();
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000897A8 File Offset: 0x000879A8
		public object Clone()
		{
			FirstTimeSyncWatermark firstTimeSyncWatermark = (FirstTimeSyncWatermark)this.CustomClone();
			if (this.IcsState != null)
			{
				firstTimeSyncWatermark.IcsState = (byte[])this.IcsState.Clone();
			}
			return firstTimeSyncWatermark;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000897E0 File Offset: 0x000879E0
		public object CustomClone()
		{
			return new FirstTimeSyncWatermark
			{
				ChangeNumber = this.ChangeNumber,
				ReceivedDateUtc = this.ReceivedDateUtc
			};
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0008980C File Offset: 0x00087A0C
		public int CompareTo(object thatObject)
		{
			FirstTimeSyncWatermark firstTimeSyncWatermark = thatObject as FirstTimeSyncWatermark;
			return ExDateTime.Compare(this.ReceivedDateUtc, (firstTimeSyncWatermark == null) ? ExDateTime.MinValue : firstTimeSyncWatermark.ReceivedDateUtc);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0008983C File Offset: 0x00087A3C
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			DateTimeData dateTimeDataInstance = componentDataPool.GetDateTimeDataInstance();
			dateTimeDataInstance.DeserializeData(reader, componentDataPool);
			this.ReceivedDateUtc = dateTimeDataInstance.Data;
			ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
			byteArrayInstance.DeserializeData(reader, componentDataPool);
			this.IcsState = byteArrayInstance.Data;
			this.ChangeNumber = reader.ReadInt32();
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0008988C File Offset: 0x00087A8C
		public override bool Equals(object thatObject)
		{
			FirstTimeSyncWatermark firstTimeSyncWatermark = thatObject as FirstTimeSyncWatermark;
			return firstTimeSyncWatermark != null && this.ReceivedDateUtc == firstTimeSyncWatermark.ReceivedDateUtc;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000898B6 File Offset: 0x00087AB6
		public override int GetHashCode()
		{
			throw new NotImplementedException("FirstTimeSyncWatermark.GetHashCode()");
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000898C2 File Offset: 0x00087AC2
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetDateTimeDataInstance().Bind(this.ReceivedDateUtc).SerializeData(writer, componentDataPool);
			componentDataPool.GetByteArrayInstance().Bind(this.IcsState).SerializeData(writer, componentDataPool);
			writer.Write(this.ChangeNumber);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00089900 File Offset: 0x00087B00
		public void Update(int changeNumber, bool read, ExDateTime receivedDate)
		{
			this.ChangeNumber = changeNumber;
			if (read)
			{
				this.ChangeNumber |= int.MinValue;
			}
			else
			{
				this.ChangeNumber &= int.MaxValue;
			}
			this.ReceivedDateUtc = receivedDate;
		}

		// Token: 0x04000E11 RID: 3601
		private static ushort typeId;
	}
}
