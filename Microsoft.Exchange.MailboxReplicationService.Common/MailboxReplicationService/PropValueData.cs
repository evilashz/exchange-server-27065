using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000065 RID: 101
	[KnownType(typeof(long[]))]
	[KnownType(typeof(bool))]
	[DataContract]
	[KnownType(typeof(string))]
	[KnownType(typeof(byte[][]))]
	[KnownType(typeof(int))]
	[KnownType(typeof(int[]))]
	[KnownType(typeof(string[]))]
	[KnownType(typeof(byte[]))]
	[KnownType(typeof(byte))]
	[KnownType(typeof(long))]
	[KnownType(typeof(bool[]))]
	internal sealed class PropValueData
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x000092B5 File Offset: 0x000074B5
		public PropValueData()
		{
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000092BD File Offset: 0x000074BD
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000092C5 File Offset: 0x000074C5
		[DataMember(IsRequired = true)]
		public int PropTag { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000092CE File Offset: 0x000074CE
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000092D6 File Offset: 0x000074D6
		[DataMember(EmitDefaultValue = false)]
		public object Value { get; set; }

		// Token: 0x060004E8 RID: 1256 RVA: 0x000092E0 File Offset: 0x000074E0
		public PropValueData(PropTag propTag, object value)
		{
			if (value == null)
			{
				propTag = propTag.ChangePropType(PropType.Null);
			}
			if (value is PropertyError)
			{
				propTag = propTag.ChangePropType(PropType.Error);
				value = (int)((PropertyError)value).PropertyErrorCode;
			}
			else if (value is ErrorCode)
			{
				propTag = propTag.ChangePropType(PropType.Error);
				value = (int)((ErrorCode)value);
			}
			if (value is ExDateTime)
			{
				value = (DateTime)((ExDateTime)value);
			}
			this.PropTag = (int)propTag;
			this.Value = value;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000936C File Offset: 0x0000756C
		public override string ToString()
		{
			return TraceUtils.DumpPropVal(new PropValue((PropTag)this.PropTag, this.Value));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00009384 File Offset: 0x00007584
		internal int GetApproximateSize()
		{
			int num = 4;
			if (this.Value is string)
			{
				num += 2 * ((string)this.Value).Length;
			}
			else if (this.Value is int)
			{
				num += 4;
			}
			else if (this.Value is byte[])
			{
				num += ((byte[])this.Value).Length;
			}
			else if (this.Value is bool || this.Value is byte)
			{
				num++;
			}
			else if (this.Value is long)
			{
				num += 8;
			}
			else if (this.Value is string[])
			{
				string[] array = (string[])this.Value;
				foreach (string text in array)
				{
					num += 2 * text.Length;
				}
			}
			else if (this.Value is int[])
			{
				num += 4 * ((int[])this.Value).Length;
			}
			else if (this.Value is byte[][])
			{
				byte[][] array3 = (byte[][])this.Value;
				foreach (byte[] array5 in array3)
				{
					num += array5.Length;
				}
			}
			else if (this.Value is bool[])
			{
				num += ((bool[])this.Value).Length;
			}
			else if (this.Value is long[])
			{
				num += 8 * ((long[])this.Value).Length;
			}
			return num;
		}
	}
}
