using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C3 RID: 195
	internal sealed class JetSizeOfColumn : SizeOfColumn, IJetColumn, IColumn
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x000275AF File Offset: 0x000257AF
		internal JetSizeOfColumn(string name, Column termColumn, bool compressedSize) : base(name, termColumn, compressedSize)
		{
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000275BC File Offset: 0x000257BC
		protected override object GetValue(ITWIR context)
		{
			if (base.CompressedSize)
			{
				JetPhysicalColumn jetPhysicalColumn = base.TermColumn as JetPhysicalColumn;
				if (jetPhysicalColumn != null)
				{
					JetTableOperator jetTableOperator = context as JetTableOperator;
					if (jetTableOperator != null)
					{
						return jetTableOperator.GetPhysicalColumnCompressedSize(jetPhysicalColumn);
					}
					JetJoinOperator jetJoinOperator = context as JetJoinOperator;
					if (jetJoinOperator != null)
					{
						return jetJoinOperator.GetPhysicalColumnCompressedSize(jetPhysicalColumn);
					}
				}
			}
			return base.GetValue(context);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002761C File Offset: 0x0002581C
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			object value = this.GetValue(cursor);
			return JetColumnValueHelper.GetAsByteArray(value, this);
		}
	}
}
