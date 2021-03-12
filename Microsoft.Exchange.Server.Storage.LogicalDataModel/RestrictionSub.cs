using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C5 RID: 197
	public sealed class RestrictionSub : Restriction
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x00053BF5 File Offset: 0x00051DF5
		public RestrictionSub(int subObject, Restriction restriction)
		{
			this.subObject = subObject;
			this.nestedRestriction = restriction;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00053C0B File Offset: 0x00051E0B
		public RestrictionSub(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.subObject = (int)ParseSerialize.GetDword(byteForm, ref position, posMax);
			this.nestedRestriction = Restriction.Deserialize(context, byteForm, ref position, posMax, mailbox, objectType);
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00053C40 File Offset: 0x00051E40
		public int SubObject
		{
			get
			{
				return this.subObject;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00053C48 File Offset: 0x00051E48
		public Restriction Restriction
		{
			get
			{
				return this.nestedRestriction;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00053C50 File Offset: 0x00051E50
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("SUB(");
			sb.Append(this.subObject);
			sb.Append(", ");
			if (this.nestedRestriction != null)
			{
				this.nestedRestriction.AppendToString(sb);
			}
			else
			{
				sb.Append("NULL");
			}
			sb.Append(')');
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00053CAD File Offset: 0x00051EAD
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 9);
			ParseSerialize.SetDword(byteForm, ref position, this.subObject);
			this.nestedRestriction.Serialize(byteForm, ref position);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00053D08 File Offset: 0x00051F08
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			if (objectType != ObjectType.Message || this.subObject != (int)PropTag.Message.MessageRecipients.PropTag)
			{
				return Factory.CreateSearchCriteriaFalse();
			}
			StorePropTag propertyTag = PropTag.Message.SearchRecipients;
			RestrictionTextProperty restrictionTextProperty = this.nestedRestriction as RestrictionTextProperty;
			RestrictionAND restrictionAND = this.nestedRestriction as RestrictionAND;
			RestrictionOR restrictionOR = this.nestedRestriction as RestrictionOR;
			if (restrictionTextProperty == null && restrictionAND == null && restrictionOR == null)
			{
				return Factory.CreateSearchCriteriaFalse();
			}
			if (restrictionTextProperty != null)
			{
				if (restrictionTextProperty.PropertyTag != PropTag.Message.DisplayName)
				{
					return Factory.CreateSearchCriteriaFalse();
				}
			}
			else if (restrictionAND != null)
			{
				if (restrictionAND.NestedRestrictions.Length != 2)
				{
					return Factory.CreateSearchCriteriaFalse();
				}
				RestrictionProperty restrictionProperty = restrictionAND.NestedRestrictions[0] as RestrictionProperty;
				restrictionTextProperty = (restrictionAND.NestedRestrictions[1] as RestrictionTextProperty);
				if (restrictionProperty == null)
				{
					restrictionProperty = (restrictionAND.NestedRestrictions[1] as RestrictionProperty);
					restrictionTextProperty = (restrictionAND.NestedRestrictions[0] as RestrictionTextProperty);
				}
				if (restrictionProperty == null || restrictionTextProperty == null || restrictionProperty.PropertyTag != PropTag.Message.RecipientType || restrictionTextProperty.PropertyTag != PropTag.Message.DisplayName)
				{
					return Factory.CreateSearchCriteriaFalse();
				}
				switch ((RecipientType)restrictionProperty.Value)
				{
				case RecipientType.To:
					propertyTag = PropTag.Message.SearchRecipientsTo;
					break;
				case RecipientType.Cc:
					propertyTag = PropTag.Message.SearchRecipientsCc;
					break;
				default:
					return Factory.CreateSearchCriteriaFalse();
				}
			}
			else if (restrictionOR != null)
			{
				if (restrictionOR.NestedRestrictions.Length != 2)
				{
					return Factory.CreateSearchCriteriaFalse();
				}
				IEnumerable<RestrictionTextProperty> source = from restriction in restrictionOR.NestedRestrictions
				select restriction as RestrictionTextProperty;
				RestrictionTextProperty restrictionTextProperty2 = (from restriction in source
				where restriction != null && restriction.PropertyTag == PropTag.Message.DisplayName
				select restriction).FirstOrDefault<RestrictionTextProperty>();
				RestrictionTextProperty restrictionTextProperty3 = (from restriction in source
				where restriction != null && restriction.PropertyTag == PropTag.Message.EmailAddress
				select restriction).FirstOrDefault<RestrictionTextProperty>();
				if (restrictionTextProperty2 == null || restrictionTextProperty3 == null || restrictionTextProperty2.FuzzyLevel != restrictionTextProperty3.FuzzyLevel || restrictionTextProperty2.Fullness != restrictionTextProperty3.Fullness || string.Compare(restrictionTextProperty2.Value as string, restrictionTextProperty3.Value as string, StringComparison.Ordinal) != 0)
				{
					return Factory.CreateSearchCriteriaFalse();
				}
				restrictionTextProperty = restrictionTextProperty2;
			}
			restrictionTextProperty = new RestrictionTextProperty(propertyTag, restrictionTextProperty.Value, restrictionTextProperty.FuzzyLevel, restrictionTextProperty.Fullness);
			return restrictionTextProperty.ToSearchCriteria(database, objectType);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00053F59 File Offset: 0x00052159
		public override bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			return predicate(this) || (this.nestedRestriction != null && this.nestedRestriction.HasClauseMeetingPredicate(predicate));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00053F7C File Offset: 0x0005217C
		protected override Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			Restriction restriction = this.nestedRestriction.InspectAndFix(callback);
			if (!object.ReferenceEquals(restriction, this.nestedRestriction))
			{
				return new RestrictionSub(this.subObject, restriction);
			}
			return this;
		}

		// Token: 0x040004FB RID: 1275
		private readonly int subObject;

		// Token: 0x040004FC RID: 1276
		private readonly Restriction nestedRestriction;
	}
}
