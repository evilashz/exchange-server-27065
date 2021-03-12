using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000642 RID: 1602
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CompositeRetentionProperty
	{
		// Token: 0x060041FF RID: 16895 RVA: 0x001193CB File Offset: 0x001175CB
		internal CompositeRetentionProperty()
		{
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x001193D3 File Offset: 0x001175D3
		internal CompositeRetentionProperty(int integer, DateTime date)
		{
			this.integer = integer;
			this.date = new DateTime?(date);
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x001193EE File Offset: 0x001175EE
		// (set) Token: 0x06004202 RID: 16898 RVA: 0x001193F6 File Offset: 0x001175F6
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

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x001193FF File Offset: 0x001175FF
		// (set) Token: 0x06004204 RID: 16900 RVA: 0x00119407 File Offset: 0x00117607
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

		// Token: 0x06004205 RID: 16901 RVA: 0x00119410 File Offset: 0x00117610
		internal static CompositeRetentionProperty Parse(byte[] propertyBytes)
		{
			return CompositeRetentionProperty.Parse(propertyBytes, false);
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x0011941C File Offset: 0x0011761C
		internal static CompositeRetentionProperty Parse(byte[] propertyBytes, bool useFileTime)
		{
			if (propertyBytes.Length != 12)
			{
				throw new ArgumentException("The length of the composite property must be 12. It is: " + propertyBytes.Length);
			}
			CompositeRetentionProperty compositeRetentionProperty = new CompositeRetentionProperty();
			compositeRetentionProperty.integer = BitConverter.ToInt32(propertyBytes, 0);
			if (useFileTime)
			{
				long num = BitConverter.ToInt64(propertyBytes, 4);
				if (num == 0L)
				{
					compositeRetentionProperty.date = new DateTime?(DateTime.MinValue);
				}
				else
				{
					compositeRetentionProperty.date = new DateTime?(DateTime.FromFileTimeUtc(num));
				}
			}
			else
			{
				compositeRetentionProperty.date = new DateTime?(DateTime.FromBinary(BitConverter.ToInt64(propertyBytes, 4)));
			}
			return compositeRetentionProperty;
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x001194A8 File Offset: 0x001176A8
		internal byte[] GetBytes()
		{
			return this.GetBytes(false);
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x001194B4 File Offset: 0x001176B4
		internal byte[] GetBytes(bool useUtc)
		{
			byte[] bytes = BitConverter.GetBytes(this.integer);
			byte[] bytes2;
			if (useUtc)
			{
				long value = 0L;
				if (this.date.Value >= CompositeRetentionProperty.minFileTime)
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

		// Token: 0x04002477 RID: 9335
		private static DateTime minFileTime = new DateTime(1601, 1, 1);

		// Token: 0x04002478 RID: 9336
		private int integer;

		// Token: 0x04002479 RID: 9337
		private DateTime? date;
	}
}
