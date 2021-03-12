using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200025E RID: 606
	internal sealed class SuccessfulGetReceiveFolderTableResult : RopResult
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x000286B8 File Offset: 0x000268B8
		internal SuccessfulGetReceiveFolderTableResult(PropertyValue[][] rowValues) : base(RopId.GetReceiveFolderTable, ErrorCode.None, null)
		{
			Util.ThrowOnNullArgument(rowValues, "rowValues");
			this.rows = new PropertyRow[rowValues.Length];
			for (long num = 0L; num < (long)rowValues.Length; num += 1L)
			{
				checked
				{
					this.rows[(int)((IntPtr)num)] = new PropertyRow(SuccessfulGetReceiveFolderTableResult.columns, rowValues[(int)((IntPtr)num)]);
				}
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002871C File Offset: 0x0002691C
		internal SuccessfulGetReceiveFolderTableResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			uint num = reader.ReadUInt32();
			if (num == 0U)
			{
				this.rows = Array<PropertyRow>.Empty;
				return;
			}
			uint elementSize = 13U;
			reader.CheckBoundary(num, elementSize);
			this.rows = new PropertyRow[num];
			for (long num2 = 0L; num2 < (long)((ulong)num); num2 += 1L)
			{
				checked
				{
					this.rows[(int)((IntPtr)num2)] = PropertyRow.Parse(reader, SuccessfulGetReceiveFolderTableResult.columns, WireFormatStyle.Rop);
					this.rows[(int)((IntPtr)num2)].ResolveString8Values(string8Encoding);
				}
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002879E File Offset: 0x0002699E
		internal PropertyRow[] Rows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000287A6 File Offset: 0x000269A6
		internal static SuccessfulGetReceiveFolderTableResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulGetReceiveFolderTableResult(reader, string8Encoding);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000287B0 File Offset: 0x000269B0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.rows.Length);
			foreach (PropertyRow propertyRow in this.rows)
			{
				propertyRow.Serialize(writer, base.String8Encoding, WireFormatStyle.Rop);
			}
		}

		// Token: 0x04000700 RID: 1792
		private static readonly PropertyTag[] columns = new PropertyTag[]
		{
			PropertyTag.Fid,
			PropertyTag.MessageClass,
			PropertyTag.LastModificationTime
		};

		// Token: 0x04000701 RID: 1793
		private readonly PropertyRow[] rows;
	}
}
