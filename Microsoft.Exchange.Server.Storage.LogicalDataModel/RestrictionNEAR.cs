using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C0 RID: 192
	public sealed class RestrictionNEAR : Restriction
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00053154 File Offset: 0x00051354
		public RestrictionNEAR(int distance, bool ordered, RestrictionAND nestedRestriction)
		{
			RestrictionNEAR.ValidateRestriction(nestedRestriction);
			this.distance = distance;
			this.ordered = ordered;
			this.nestedRestriction = nestedRestriction;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00053178 File Offset: 0x00051378
		public RestrictionNEAR(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.distance = (int)ParseSerialize.GetDword(byteForm, ref position, posMax);
			this.ordered = (ParseSerialize.GetDword(byteForm, ref position, posMax) == 1U);
			this.nestedRestriction = (Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType) as RestrictionAND);
			RestrictionNEAR.ValidateRestriction(this.nestedRestriction);
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000531DA File Offset: 0x000513DA
		public int Distance
		{
			get
			{
				return this.distance;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000531E2 File Offset: 0x000513E2
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000531EA File Offset: 0x000513EA
		public RestrictionAND NestedRestriction
		{
			get
			{
				return this.nestedRestriction;
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000531F4 File Offset: 0x000513F4
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("NEAR(");
			if (this.nestedRestriction != null)
			{
				for (int i = 0; i < this.nestedRestriction.NestedRestrictions.Length; i++)
				{
					this.nestedRestriction.NestedRestrictions[i].AppendToString(sb);
					sb.Append(", ");
				}
			}
			sb.Append(this.distance);
			sb.Append(", ");
			sb.Append(this.ordered);
			sb.Append(")");
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0005327E File Offset: 0x0005147E
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 13);
			ParseSerialize.SetDword(byteForm, ref position, this.distance);
			ParseSerialize.SetDword(byteForm, ref position, this.ordered ? 1 : 0);
			this.nestedRestriction.Serialize(byteForm, ref position);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000532B8 File Offset: 0x000514B8
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			SearchCriteriaAnd criteria = this.nestedRestriction.ToSearchCriteria(database, objectType) as SearchCriteriaAnd;
			return Factory.CreateSearchCriteriaNear(this.distance, this.ordered, criteria);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000532EC File Offset: 0x000514EC
		private static void ValidateRestriction(Restriction nestedRestriction)
		{
			RestrictionAND restrictionAND = nestedRestriction as RestrictionAND;
			if (restrictionAND == null)
			{
				throw new StoreException((LID)55632U, ErrorCodeValue.TooComplex, "NEAR requires a non-null AND restriction");
			}
			if (restrictionAND.NestedRestrictions == null || restrictionAND.NestedRestrictions.Length < 2)
			{
				throw new StoreException((LID)51536U, ErrorCodeValue.TooComplex, "NEAR requires at least two restrictions");
			}
			if (!RestrictionNEAR.InspectNestedRestriction(restrictionAND.NestedRestrictions))
			{
				throw new StoreException((LID)43344U, ErrorCodeValue.TooComplex, "NEAR can only contain OR, NEAR or TEXT restrictions");
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00053374 File Offset: 0x00051574
		private static bool InspectNestedRestriction(Restriction[] nestedRestrictions)
		{
			if (nestedRestrictions == null || nestedRestrictions.Length == 0)
			{
				return false;
			}
			foreach (Restriction restriction in nestedRestrictions)
			{
				if (restriction is RestrictionOR)
				{
					if (!RestrictionNEAR.InspectNestedRestriction(((RestrictionOR)restriction).NestedRestrictions))
					{
						return false;
					}
				}
				else if (!(restriction is RestrictionTextProperty) && !(restriction is RestrictionNEAR))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000533D4 File Offset: 0x000515D4
		public override bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			return predicate(this) || (this.nestedRestriction != null && this.nestedRestriction.HasClauseMeetingPredicate(predicate));
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000533F8 File Offset: 0x000515F8
		protected override Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			Restriction restriction = this.nestedRestriction.InspectAndFix(callback);
			if (!object.ReferenceEquals(restriction, this.nestedRestriction))
			{
				return new RestrictionNEAR(this.distance, this.ordered, (RestrictionAND)restriction);
			}
			return this;
		}

		// Token: 0x040004F0 RID: 1264
		private readonly int distance;

		// Token: 0x040004F1 RID: 1265
		private readonly bool ordered;

		// Token: 0x040004F2 RID: 1266
		private readonly RestrictionAND nestedRestriction;
	}
}
