using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Serialization;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000028 RID: 40
	internal sealed class ControlData
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00006430 File Offset: 0x00004630
		public ControlData(DateTime lastProcessedDate)
		{
			this.LastProcessedDate = lastProcessedDate;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000643F File Offset: 0x0000463F
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00006447 File Offset: 0x00004647
		public DateTime LastProcessedDate { get; private set; }

		// Token: 0x06000141 RID: 321 RVA: 0x00006450 File Offset: 0x00004650
		public static ControlData CreateFromByteArray(byte[] serializedData)
		{
			if (serializedData == null || serializedData.Length == 0)
			{
				return ControlData.Empty;
			}
			ControlData result;
			try
			{
				int num = 0;
				uint num2 = Serialization.DeserializeUInt32(serializedData, ref num);
				if (num2 > 1U)
				{
					return ControlData.Empty;
				}
				DateTime lastProcessedDate = Serialization.DeserializeDateTime(serializedData, ref num);
				result = new ControlData(lastProcessedDate);
			}
			catch (SerializationException)
			{
				result = ControlData.Empty;
			}
			return result;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000064B4 File Offset: 0x000046B4
		public static ControlData Create(DateTime lastProcessedDate)
		{
			return new ControlData(lastProcessedDate);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000064BC File Offset: 0x000046BC
		public byte[] ToByteArray()
		{
			byte[] array = new byte[12];
			int num = 0;
			Serialization.SerializeUInt32(array, ref num, 1U);
			Serialization.SerializeDateTime(array, ref num, this.LastProcessedDate);
			return array;
		}

		// Token: 0x0400012A RID: 298
		private const uint CurrentVersion = 1U;

		// Token: 0x0400012B RID: 299
		public static readonly ControlData Empty = new ControlData(DateTime.MinValue);
	}
}
