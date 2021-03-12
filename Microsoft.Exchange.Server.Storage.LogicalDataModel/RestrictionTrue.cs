using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C9 RID: 201
	public sealed class RestrictionTrue : Restriction
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0005427A File Offset: 0x0005247A
		public RestrictionTrue()
		{
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00054282 File Offset: 0x00052482
		public RestrictionTrue(byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00054293 File Offset: 0x00052493
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("TRUE");
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000542A1 File Offset: 0x000524A1
		public override string ToString()
		{
			return "TRUE";
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000542A8 File Offset: 0x000524A8
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 131);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000542B6 File Offset: 0x000524B6
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			return Factory.CreateSearchCriteriaTrue();
		}
	}
}
