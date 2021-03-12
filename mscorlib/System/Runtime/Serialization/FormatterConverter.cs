using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000701 RID: 1793
	[ComVisible(true)]
	public class FormatterConverter : IFormatterConverter
	{
		// Token: 0x06005059 RID: 20569 RVA: 0x0011A72A File Offset: 0x0011892A
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0011A746 File Offset: 0x00118946
		public object Convert(object value, TypeCode typeCode)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x0011A762 File Offset: 0x00118962
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x0011A77D File Offset: 0x0011897D
		public char ToChar(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToChar(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0011A798 File Offset: 0x00118998
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToSByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0011A7B3 File Offset: 0x001189B3
		public byte ToByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x0011A7CE File Offset: 0x001189CE
		public short ToInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt16(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0011A7E9 File Offset: 0x001189E9
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt16(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0011A804 File Offset: 0x00118A04
		public int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0011A81F File Offset: 0x00118A1F
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt32(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x0011A83A File Offset: 0x00118A3A
		public long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x0011A855 File Offset: 0x00118A55
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0011A870 File Offset: 0x00118A70
		public float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0011A88B File Offset: 0x00118A8B
		public double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0011A8A6 File Offset: 0x00118AA6
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x0011A8C1 File Offset: 0x00118AC1
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x0011A8DC File Offset: 0x00118ADC
		public string ToString(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}
	}
}
