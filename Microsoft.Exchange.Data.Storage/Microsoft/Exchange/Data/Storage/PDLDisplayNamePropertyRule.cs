using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA9 RID: 2729
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PDLDisplayNamePropertyRule : PriorityBasedDisplayNamePropertyRule
	{
		// Token: 0x06006385 RID: 25477 RVA: 0x001A39C8 File Offset: 0x001A1BC8
		protected override IList<PriorityBasedDisplayNamePropertyRule.CandidateProperty> GetCandidateProperties()
		{
			return new List<PriorityBasedDisplayNamePropertyRule.CandidateProperty>
			{
				new PriorityBasedDisplayNamePropertyRule.SimpleCandidateProperty(InternalSchema.DisplayName),
				new PriorityBasedDisplayNamePropertyRule.SimpleCandidateProperty(InternalSchema.DLName),
				new PriorityBasedDisplayNamePropertyRule.SimpleCandidateProperty(InternalSchema.MapiSubject)
			};
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x001A3A0C File Offset: 0x001A1C0C
		public override List<PropertyReference> GetPropertyReferenceList()
		{
			return PDLDisplayNamePropertyRule.Merge(base.GetPropertyReferenceList(), PropertyRuleLibrary.TruncateSubjectPropertyReferenceList);
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x001A3A20 File Offset: 0x001A1C20
		public override bool UpdateDisplayNameProperties(ICorePropertyBag propertyBag)
		{
			bool flag = base.UpdateDisplayNameProperties(propertyBag);
			string text = propertyBag.GetValueOrDefault<string>(InternalSchema.DisplayNameFirstLast, string.Empty).Trim();
			if (text != string.Empty)
			{
				propertyBag[ContactBaseSchema.FileAs] = text;
				propertyBag[ItemSchema.Subject] = text;
				propertyBag[DistributionListSchema.DLName] = text;
			}
			return flag | this.TruncateSubject(propertyBag);
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x001A3A87 File Offset: 0x001A1C87
		protected virtual bool TruncateSubject(ICorePropertyBag propertyBag)
		{
			return PropertyRuleLibrary.InternalTruncateSubject(propertyBag);
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x001A3A8F File Offset: 0x001A1C8F
		internal List<PropertyReference> GetBasePropertyReferenceList()
		{
			return base.GetPropertyReferenceList();
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x001A3A98 File Offset: 0x001A1C98
		private static List<PropertyReference> Merge(List<PropertyReference> listOne, List<PropertyReference> listTwo)
		{
			List<PropertyReference> list = new List<PropertyReference>(listTwo);
			List<PropertyReference> list2 = new List<PropertyReference>(listOne.Count + list.Count);
			foreach (PropertyReference propertyReference in listOne)
			{
				bool flag = false;
				foreach (PropertyReference propertyReference2 in list)
				{
					if (propertyReference2.Property.Equals(propertyReference.Property))
					{
						list2.Add(new PropertyReference(propertyReference.Property, propertyReference.AccessType | propertyReference2.AccessType));
						list.Remove(propertyReference2);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list2.Add(propertyReference);
				}
			}
			list2.AddRange(list);
			return list2;
		}
	}
}
