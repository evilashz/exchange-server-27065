using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004FB RID: 1275
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ApprovedApplicationCollection : ADMultiValuedProperty<ApprovedApplication>
	{
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000DB246 File Offset: 0x000D9446
		public new static ApprovedApplicationCollection Empty
		{
			get
			{
				return ApprovedApplicationCollection.empty;
			}
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000DB24D File Offset: 0x000D944D
		public ApprovedApplicationCollection()
		{
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000DB255 File Offset: 0x000D9455
		public ApprovedApplicationCollection(object value) : base(ApprovedApplicationCollection.ConvertValue(value, null))
		{
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000DB264 File Offset: 0x000D9464
		public ApprovedApplicationCollection(ICollection values) : base(ApprovedApplicationCollection.ConvertValues(values))
		{
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000DB274 File Offset: 0x000D9474
		internal ApprovedApplicationCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : this(readOnly, propertyDefinition, values, null, null)
		{
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000DB294 File Offset: 0x000D9494
		internal ApprovedApplicationCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, ApprovedApplicationCollection.ConvertValues(values), invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000DB2A8 File Offset: 0x000D94A8
		public new static implicit operator ApprovedApplicationCollection(object[] array)
		{
			return new ApprovedApplicationCollection(false, null, array);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000DB2B4 File Offset: 0x000D94B4
		protected override bool TryAddInternal(ApprovedApplication item, out Exception error)
		{
			if (null == item || !item.IsCab)
			{
				return base.TryAddInternal(item, out error);
			}
			MultiValuedProperty<ApprovedApplication> multiValuedProperty = ApprovedApplication.ParseCab(item.CabName);
			if (multiValuedProperty != null)
			{
				base.BeginUpdate();
				try
				{
					foreach (ApprovedApplication item2 in multiValuedProperty)
					{
						if (!base.TryAddInternal(item2, out error))
						{
							return false;
						}
					}
				}
				finally
				{
					base.EndUpdate();
				}
			}
			error = null;
			return true;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000DB358 File Offset: 0x000D9558
		public override bool Remove(ApprovedApplication item)
		{
			if (null == item)
			{
				throw new ArgumentNullException("item");
			}
			if (item.IsFromFile)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionRemoveApprovedApplication(item.AppString));
			}
			return base.Remove(item);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000DB393 File Offset: 0x000D9593
		protected override ValidationError ValidateValue(ApprovedApplication item)
		{
			if (item.IsCab)
			{
				return new PropertyValidationError(DirectoryStrings.ExceptionInvalidApprovedApplication(item.CabName), this.PropertyDefinition, item);
			}
			return base.ValidateValue(item);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000DB3BC File Offset: 0x000D95BC
		private static ICollection ConvertValues(ICollection values)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object value in values)
			{
				ApprovedApplicationCollection.ConvertValue(value, arrayList);
			}
			return arrayList;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000DB414 File Offset: 0x000D9614
		private static ICollection ConvertValue(object value, ArrayList newValues)
		{
			if (newValues == null)
			{
				newValues = new ArrayList();
			}
			ApprovedApplication approvedApplication = (ApprovedApplication)ValueConvertor.ConvertValue(value, typeof(ApprovedApplication), null);
			if (approvedApplication != null)
			{
				if (approvedApplication.IsCab)
				{
					MultiValuedProperty<ApprovedApplication> multiValuedProperty = ApprovedApplication.ParseCab(approvedApplication.CabName);
					using (MultiValuedProperty<ApprovedApplication>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ApprovedApplication value2 = enumerator.Current;
							newValues.Add(value2);
						}
						return newValues;
					}
				}
				newValues.Add(value);
				return newValues;
			}
			throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExceptionInvalidApprovedApplication(value.ToString()), MobileMailboxPolicySchema.ADApprovedApplicationList, value));
		}

		// Token: 0x0400269C RID: 9884
		private static ApprovedApplicationCollection empty = new ApprovedApplicationCollection(true, null, new object[0]);
	}
}
