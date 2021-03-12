using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C1 RID: 193
	public sealed class RestrictionNOT : Restriction
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00053439 File Offset: 0x00051639
		public RestrictionNOT(Restriction nestedRestriction)
		{
			if (nestedRestriction == null)
			{
				nestedRestriction = new RestrictionFalse();
			}
			this.nestedRestriction = nestedRestriction;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00053452 File Offset: 0x00051652
		public RestrictionNOT(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.nestedRestriction = Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00053478 File Offset: 0x00051678
		public Restriction NestedRestriction
		{
			get
			{
				return this.nestedRestriction;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00053480 File Offset: 0x00051680
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("NOT(");
			if (this.nestedRestriction != null)
			{
				this.nestedRestriction.AppendToString(sb);
			}
			sb.Append(")");
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000534AE File Offset: 0x000516AE
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 2);
			this.NestedRestriction.Serialize(byteForm, ref position);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000534C8 File Offset: 0x000516C8
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			SearchCriteria criteria = this.NestedRestriction.ToSearchCriteria(database, objectType);
			return Factory.CreateSearchCriteriaNot(criteria);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000534E9 File Offset: 0x000516E9
		public override bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			return predicate(this) || (this.nestedRestriction != null && this.nestedRestriction.HasClauseMeetingPredicate(predicate));
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0005350C File Offset: 0x0005170C
		protected override Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			Restriction objA = this.nestedRestriction.InspectAndFix(callback);
			if (!object.ReferenceEquals(objA, this.nestedRestriction))
			{
				return new RestrictionNOT(objA);
			}
			return this;
		}

		// Token: 0x040004F3 RID: 1267
		private readonly Restriction nestedRestriction;
	}
}
