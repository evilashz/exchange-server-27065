using System;
using System.IO;
using System.Text;
using Microsoft.Mapi;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000161 RID: 353
	internal sealed class OABPropertyValue
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0003AE2C File Offset: 0x0003902C
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0003AE34 File Offset: 0x00039034
		public PropTag PropTag { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0003AE3D File Offset: 0x0003903D
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0003AE45 File Offset: 0x00039045
		public object Value { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003AE50 File Offset: 0x00039050
		public bool IsWritable
		{
			get
			{
				PropTypeHandler handler = PropTypeHandler.GetHandler(this.PropTag.ValueType());
				return handler.IsWritable;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003AE74 File Offset: 0x00039074
		public static OABPropertyValue ReadFrom(BinaryReader reader, PropTag propTag, string elementName)
		{
			PropTypeHandler handler = PropTypeHandler.GetHandler(propTag.ValueType());
			object value = handler.ReadFrom(reader, elementName);
			return new OABPropertyValue
			{
				PropTag = propTag,
				Value = value
			};
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003AEAC File Offset: 0x000390AC
		public void WriteTo(BinaryWriter writer)
		{
			PropTypeHandler handler = PropTypeHandler.GetHandler(this.PropTag.ValueType());
			handler.WriteTo(writer, this.Value);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003AED8 File Offset: 0x000390D8
		public override string ToString()
		{
			PropTypeHandler handler = PropTypeHandler.GetHandler(this.PropTag.ValueType());
			int num = 1;
			Array array = this.Value as Array;
			if (array != null)
			{
				num = array.Length;
			}
			StringBuilder stringBuilder = new StringBuilder(20 * num);
			stringBuilder.Append("PropTag=");
			stringBuilder.Append(this.PropTag.ToString("X8"));
			stringBuilder.Append(", Value=");
			handler.AppendText(stringBuilder, this.Value);
			return stringBuilder.ToString();
		}
	}
}
