using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C2 RID: 194
	public sealed class RestrictionOR : Restriction
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x0005353C File Offset: 0x0005173C
		public RestrictionOR(params Restriction[] nestedRestrictions)
		{
			this.nestedRestrictions = nestedRestrictions;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0005354C File Offset: 0x0005174C
		public RestrictionOR(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			ushort word = ParseSerialize.GetWord(byteForm, ref position, posMax);
			this.nestedRestrictions = new Restriction[(int)word];
			for (int i = 0; i < this.nestedRestrictions.Length; i++)
			{
				this.nestedRestrictions[i] = Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000535A8 File Offset: 0x000517A8
		public Restriction[] NestedRestrictions
		{
			get
			{
				return this.nestedRestrictions;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000535B0 File Offset: 0x000517B0
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("OR(");
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

		// Token: 0x06000A99 RID: 2713 RVA: 0x00053610 File Offset: 0x00051810
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 1);
			ParseSerialize.SetWord(byteForm, ref position, (short)((this.nestedRestrictions == null) ? 0 : this.nestedRestrictions.Length));
			if (this.nestedRestrictions != null)
			{
				for (int i = 0; i < this.nestedRestrictions.Length; i++)
				{
					this.nestedRestrictions[i].Serialize(byteForm, ref position);
				}
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0005366C File Offset: 0x0005186C
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			if (this.nestedRestrictions == null || this.nestedRestrictions.Length == 0)
			{
				return Factory.CreateSearchCriteriaFalse();
			}
			SearchCriteria[] array = new SearchCriteria[this.nestedRestrictions.Length];
			for (int i = 0; i < this.nestedRestrictions.Length; i++)
			{
				array[i] = this.nestedRestrictions[i].ToSearchCriteria(database, objectType);
			}
			return Factory.CreateSearchCriteriaOr(array);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000536CC File Offset: 0x000518CC
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

		// Token: 0x06000A9C RID: 2716 RVA: 0x00053714 File Offset: 0x00051914
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
				return new RestrictionOR(list.ToArray());
			}
			return this;
		}

		// Token: 0x040004F4 RID: 1268
		private readonly Restriction[] nestedRestrictions;
	}
}
