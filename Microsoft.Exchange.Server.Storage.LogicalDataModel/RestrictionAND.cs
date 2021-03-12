using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000B8 RID: 184
	public sealed class RestrictionAND : Restriction
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00052564 File Offset: 0x00050764
		public RestrictionAND(params Restriction[] nestedRestrictions)
		{
			this.nestedRestrictions = nestedRestrictions;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00052574 File Offset: 0x00050774
		public RestrictionAND(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			ushort word = ParseSerialize.GetWord(byteForm, ref position, posMax);
			this.nestedRestrictions = new Restriction[(int)word];
			for (int i = 0; i < this.nestedRestrictions.Length; i++)
			{
				this.nestedRestrictions[i] = Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000525D0 File Offset: 0x000507D0
		public Restriction[] NestedRestrictions
		{
			get
			{
				return this.nestedRestrictions;
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000525D8 File Offset: 0x000507D8
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("AND(");
			if (this.nestedRestrictions != null)
			{
				for (int i = 0; i < this.nestedRestrictions.Length; i++)
				{
					if (i > 0)
					{
						sb.Append(", ");
					}
					this.nestedRestrictions[i].AppendToString(sb);
				}
			}
			sb.Append(")");
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00052638 File Offset: 0x00050838
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 0);
			ParseSerialize.SetWord(byteForm, ref position, (short)((this.nestedRestrictions == null) ? 0 : this.nestedRestrictions.Length));
			if (this.nestedRestrictions != null)
			{
				for (int i = 0; i < this.nestedRestrictions.Length; i++)
				{
					this.nestedRestrictions[i].Serialize(byteForm, ref position);
				}
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00052694 File Offset: 0x00050894
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			if (this.nestedRestrictions == null || this.nestedRestrictions.Length == 0)
			{
				return Factory.CreateSearchCriteriaTrue();
			}
			SearchCriteria[] array = new SearchCriteria[this.nestedRestrictions.Length];
			for (int i = 0; i < this.nestedRestrictions.Length; i++)
			{
				array[i] = this.nestedRestrictions[i].ToSearchCriteria(database, objectType);
			}
			return Factory.CreateSearchCriteriaAnd(array);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000526F4 File Offset: 0x000508F4
		public override bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			if (predicate(this))
			{
				return true;
			}
			if (this.nestedRestrictions != null)
			{
				for (int i = 0; i < this.nestedRestrictions.Length; i++)
				{
					if (this.nestedRestrictions[i].HasClauseMeetingPredicate(predicate))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0005273C File Offset: 0x0005093C
		protected override Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			List<Restriction> list = null;
			for (int i = 0; i < this.nestedRestrictions.Length; i++)
			{
				Restriction restriction = this.nestedRestrictions[i].InspectAndFix(callback);
				if (list != null || !object.ReferenceEquals(restriction, this.nestedRestrictions[i]))
				{
					if (list == null)
					{
						list = new List<Restriction>(this.nestedRestrictions.Length);
						if (i != 0)
						{
							for (int j = 0; j < i; j++)
							{
								list.Add(this.nestedRestrictions[j]);
							}
						}
					}
					list.Add(restriction);
				}
			}
			if (list != null)
			{
				return new RestrictionAND(list.ToArray());
			}
			return this;
		}

		// Token: 0x040004E0 RID: 1248
		private readonly Restriction[] nestedRestrictions;
	}
}
