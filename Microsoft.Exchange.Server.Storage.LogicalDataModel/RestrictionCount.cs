using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000BD RID: 189
	public sealed class RestrictionCount : Restriction
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x00052EBB File Offset: 0x000510BB
		public RestrictionCount(int count, Restriction nestedRestriction)
		{
			this.count = RestrictionCount.SanitizeCount(count);
			this.nestedRestriction = nestedRestriction;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00052ED6 File Offset: 0x000510D6
		public RestrictionCount(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.count = RestrictionCount.SanitizeCount((int)ParseSerialize.GetDword(byteForm, ref position, posMax));
			this.nestedRestriction = Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType);
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00052F10 File Offset: 0x00051110
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00052F18 File Offset: 0x00051118
		public Restriction NestedRestriction
		{
			get
			{
				return this.nestedRestriction;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00052F20 File Offset: 0x00051120
		private static int SanitizeCount(int count)
		{
			return Math.Max(count, 0);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00052F2C File Offset: 0x0005112C
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("COUNT(");
			sb.Append(this.count);
			sb.Append(", ");
			if (this.nestedRestriction != null)
			{
				this.nestedRestriction.AppendToString(sb);
			}
			else
			{
				sb.Append("NULL");
			}
			sb.Append(")");
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00052F8C File Offset: 0x0005118C
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 11);
			ParseSerialize.SetDword(byteForm, ref position, this.count);
			this.nestedRestriction.Serialize(byteForm, ref position);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00052FB4 File Offset: 0x000511B4
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			return this.nestedRestriction.ToSearchCriteria(database, objectType);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00052FD0 File Offset: 0x000511D0
		public override bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			return predicate(this) || (this.nestedRestriction != null && this.nestedRestriction.HasClauseMeetingPredicate(predicate));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00052FF4 File Offset: 0x000511F4
		protected override Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			Restriction objA = this.nestedRestriction.InspectAndFix(callback);
			if (!object.ReferenceEquals(objA, this.nestedRestriction))
			{
				return new RestrictionCount(this.count, objA);
			}
			return this;
		}

		// Token: 0x040004ED RID: 1261
		private readonly int count;

		// Token: 0x040004EE RID: 1262
		private readonly Restriction nestedRestriction;
	}
}
