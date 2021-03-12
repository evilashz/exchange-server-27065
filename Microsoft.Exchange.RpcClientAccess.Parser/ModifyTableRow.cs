using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000062 RID: 98
	internal struct ModifyTableRow
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000A791 File Offset: 0x00008991
		public ModifyTableRow(ModifyTableFlags modifyTableFlags, PropertyValue[] propertyValues)
		{
			Util.ThrowOnNullArgument(propertyValues, "propertyValues");
			this.modifyTableFlags = modifyTableFlags;
			this.propertyValues = propertyValues;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A7AC File Offset: 0x000089AC
		public ModifyTableFlags ModifyTableFlags
		{
			get
			{
				return this.modifyTableFlags;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public PropertyValue[] PropertyValues
		{
			get
			{
				if (this.propertyValues == null)
				{
					throw new ArgumentException("The propertyValues should not be null");
				}
				return this.propertyValues;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("ModifyTableFlags: {0}", this.modifyTableFlags));
			stringBuilder.AppendLine(string.Format("Properties[{0}] {{", this.PropertyValues.Length));
			foreach (PropertyValue propertyValue in this.PropertyValues)
			{
				stringBuilder.AppendLine(string.Format("  {{{0}}}", propertyValue));
			}
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A868 File Offset: 0x00008A68
		internal static ModifyTableRow Parse(Reader reader)
		{
			ModifyTableFlags modifyTableFlags = (ModifyTableFlags)reader.ReadByte();
			PropertyValue[] array = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
			return new ModifyTableRow(modifyTableFlags, array);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A88B File Offset: 0x00008A8B
		internal void Serialize(Writer writer, Encoding string8Encoding)
		{
			writer.WriteByte((byte)this.modifyTableFlags);
			writer.WriteCountAndPropertyValueList(this.PropertyValues, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		internal void ResolveString8Values(Encoding string8Encoding)
		{
			foreach (PropertyValue propertyValue in this.PropertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x04000145 RID: 325
		private readonly ModifyTableFlags modifyTableFlags;

		// Token: 0x04000146 RID: 326
		private readonly PropertyValue[] propertyValues;
	}
}
