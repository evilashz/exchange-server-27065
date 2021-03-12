using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000BB RID: 187
	public sealed class RestrictionComment : Restriction
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x00052A3B File Offset: 0x00050C3B
		public RestrictionComment(StorePropTag[] propTags, object[] values, Restriction nestedRestriction)
		{
			if (propTags != null && propTags.Length != 0)
			{
				this.propTags = propTags;
				this.values = values;
			}
			this.nestedRestriction = nestedRestriction;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00052A60 File Offset: 0x00050C60
		public RestrictionComment(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			byte @byte = ParseSerialize.GetByte(byteForm, ref position, posMax);
			if (@byte != 0)
			{
				this.propTags = new StorePropTag[(int)@byte];
				this.values = new object[(int)@byte];
				for (int i = 0; i < (int)@byte; i++)
				{
					this.propTags[i] = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
					this.values[i] = Restriction.DeserializeValue(byteForm, ref position, this.propTags[i]);
				}
			}
			bool boolean = ParseSerialize.GetBoolean(byteForm, ref position, posMax);
			if (boolean)
			{
				this.nestedRestriction = Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00052B13 File Offset: 0x00050D13
		public StorePropTag[] PropTags
		{
			get
			{
				return this.propTags;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00052B1B File Offset: 0x00050D1B
		public object[] Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00052B23 File Offset: 0x00050D23
		public Restriction NestedRestriction
		{
			get
			{
				return this.nestedRestriction;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00052B2C File Offset: 0x00050D2C
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("COMMENT({");
			if (this.propTags != null)
			{
				for (int i = 0; i < this.propTags.Length; i++)
				{
					if (i != 0)
					{
						sb.Append(", ");
					}
					sb.Append("[");
					this.propTags[i].AppendToString(sb);
					sb.Append("]=");
					sb.Append(this.values[i]);
				}
			}
			sb.Append("}, ");
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

		// Token: 0x06000A5D RID: 2653 RVA: 0x00052BE4 File Offset: 0x00050DE4
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 10);
			ParseSerialize.SetByte(byteForm, ref position, (byte)((this.propTags == null) ? 0 : this.propTags.Length));
			if (this.propTags != null)
			{
				for (int i = 0; i < this.propTags.Length; i++)
				{
					ParseSerialize.SetDword(byteForm, ref position, this.propTags[i].ExternalTag);
					Restriction.SerializeValue(byteForm, ref position, this.propTags[i], this.values[i]);
				}
			}
			ParseSerialize.SetBoolean(byteForm, ref position, this.nestedRestriction != null);
			if (this.nestedRestriction != null)
			{
				this.nestedRestriction.Serialize(byteForm, ref position);
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00052C90 File Offset: 0x00050E90
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			if (this.nestedRestriction == null)
			{
				return Factory.CreateSearchCriteriaFalse();
			}
			return this.nestedRestriction.ToSearchCriteria(database, objectType);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00052CAD File Offset: 0x00050EAD
		public override bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			return predicate(this) || (this.nestedRestriction != null && this.nestedRestriction.HasClauseMeetingPredicate(predicate));
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00052CD0 File Offset: 0x00050ED0
		protected override Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			Restriction objA = this.nestedRestriction.InspectAndFix(callback);
			if (!object.ReferenceEquals(objA, this.nestedRestriction))
			{
				return new RestrictionComment(this.propTags, this.values, objA);
			}
			return this;
		}

		// Token: 0x040004E7 RID: 1255
		private readonly StorePropTag[] propTags;

		// Token: 0x040004E8 RID: 1256
		private readonly object[] values;

		// Token: 0x040004E9 RID: 1257
		private readonly Restriction nestedRestriction;
	}
}
