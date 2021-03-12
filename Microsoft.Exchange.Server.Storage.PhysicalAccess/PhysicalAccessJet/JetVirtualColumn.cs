using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000CF RID: 207
	internal class JetVirtualColumn : VirtualColumn, IJetColumn, IColumn
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		internal JetVirtualColumn(VirtualColumnId virtualColumnId, string name, Type type, bool nullable, Visibility visibility, int maxLength, int size, Table table) : base(virtualColumnId, name, type, nullable, visibility, maxLength, size, table)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
		public byte[] GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			JetTableOperator jetTableOperator = cursor as JetTableOperator;
			if (jetTableOperator != null)
			{
				return jetTableOperator.GetVirtualColumnValueAsBytes(this);
			}
			return null;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002E918 File Offset: 0x0002CB18
		protected override int GetSize(ITWIR context)
		{
			JetTableOperator jetTableOperator = context as JetTableOperator;
			if (jetTableOperator != null)
			{
				return jetTableOperator.GetVirtualColumnSize(this);
			}
			return 0;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002E938 File Offset: 0x0002CB38
		protected override object GetValue(ITWIR context)
		{
			JetTableOperator jetTableOperator = context as JetTableOperator;
			if (jetTableOperator != null)
			{
				return jetTableOperator.GetVirtualColumnValue(this);
			}
			return null;
		}
	}
}
