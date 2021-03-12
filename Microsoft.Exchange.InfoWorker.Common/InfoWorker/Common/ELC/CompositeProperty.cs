using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x0200018F RID: 399
	internal class CompositeProperty
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
		internal CompositeProperty()
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		internal CompositeProperty(int integer, DateTime date)
		{
			this.integer = integer;
			this.date = new DateTime?(date);
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002DBFB File Offset: 0x0002BDFB
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0002DC03 File Offset: 0x0002BE03
		internal int Integer
		{
			get
			{
				return this.integer;
			}
			set
			{
				this.integer = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0002DC14 File Offset: 0x0002BE14
		internal DateTime? Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002DC1D File Offset: 0x0002BE1D
		internal static CompositeProperty Parse(byte[] propertyBytes)
		{
			return CompositeProperty.Parse(propertyBytes, false);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002DC28 File Offset: 0x0002BE28
		internal static CompositeProperty Parse(byte[] propertyBytes, bool useFileTime)
		{
			if (propertyBytes.Length != 12)
			{
				throw new ArgumentException("The length of the composite property must be 12. It is: " + propertyBytes.Length);
			}
			CompositeProperty compositeProperty = new CompositeProperty();
			compositeProperty.integer = BitConverter.ToInt32(propertyBytes, 0);
			if (useFileTime)
			{
				long num = BitConverter.ToInt64(propertyBytes, 4);
				if (num == 0L)
				{
					compositeProperty.date = new DateTime?(DateTime.MinValue);
				}
				else
				{
					compositeProperty.date = new DateTime?(DateTime.FromFileTimeUtc(num));
				}
			}
			else
			{
				compositeProperty.date = new DateTime?(DateTime.FromBinary(BitConverter.ToInt64(propertyBytes, 4)));
			}
			return compositeProperty;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		internal byte[] GetBytes()
		{
			return this.GetBytes(false);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		internal byte[] GetBytes(bool useUtc)
		{
			byte[] bytes = BitConverter.GetBytes(this.integer);
			byte[] bytes2;
			if (useUtc)
			{
				long value = 0L;
				if (this.date.Value >= CompositeProperty.minFileTime)
				{
					value = this.date.Value.ToFileTimeUtc();
				}
				bytes2 = BitConverter.GetBytes(value);
			}
			else
			{
				bytes2 = BitConverter.GetBytes(this.date.Value.ToBinary());
			}
			byte[] array = new byte[bytes.Length + bytes2.Length];
			Array.Copy(bytes, array, bytes.Length);
			Array.Copy(bytes2, 0, array, bytes.Length, bytes2.Length);
			return array;
		}

		// Token: 0x04000810 RID: 2064
		private static DateTime minFileTime = new DateTime(1601, 1, 1);

		// Token: 0x04000811 RID: 2065
		private int integer;

		// Token: 0x04000812 RID: 2066
		private DateTime? date;
	}
}
