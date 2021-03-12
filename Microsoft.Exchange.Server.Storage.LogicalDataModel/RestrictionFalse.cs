using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000BF RID: 191
	public sealed class RestrictionFalse : Restriction
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x00053111 File Offset: 0x00051311
		public RestrictionFalse()
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00053119 File Offset: 0x00051319
		public RestrictionFalse(byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0005312A File Offset: 0x0005132A
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("FALSE");
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00053138 File Offset: 0x00051338
		public override string ToString()
		{
			return "FALSE";
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0005313F File Offset: 0x0005133F
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 132);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0005314D File Offset: 0x0005134D
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			return Factory.CreateSearchCriteriaFalse();
		}
	}
}
