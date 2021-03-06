using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public abstract class ProxyAddressBaseCollection<T> : MultiValuedProperty<T> where T : ProxyAddressBase
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011CEF File Offset: 0x0000FEEF
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		protected bool AutoPromotionDisabled
		{
			get
			{
				return this.autoPromotionDisabled;
			}
			set
			{
				this.autoPromotionDisabled = value;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00011D00 File Offset: 0x0000FF00
		protected ProxyAddressBaseCollection()
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00011D08 File Offset: 0x0000FF08
		protected ProxyAddressBaseCollection(object value)
		{
			this.Add(value);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00011D18 File Offset: 0x0000FF18
		protected ProxyAddressBaseCollection(ICollection values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (object item in values)
			{
				this.Add(item);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00011D7C File Offset: 0x0000FF7C
		protected ProxyAddressBaseCollection(Dictionary<string, object> dictionary) : base(dictionary)
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00011D85 File Offset: 0x0000FF85
		internal ProxyAddressBaseCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00011D94 File Offset: 0x0000FF94
		internal ProxyAddressBaseCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		protected override ValidationError ValidateValue(T item)
		{
			if (item is InvalidProxyAddress)
			{
				InvalidProxyAddress invalidProxyAddress = item as InvalidProxyAddress;
				return new PropertyValidationError(DataStrings.ExceptionUsingInvalidAddress(invalidProxyAddress.ToString(), invalidProxyAddress.ParseException.Message), this.PropertyDefinition, invalidProxyAddress);
			}
			if (item is InvalidProxyAddressTemplate)
			{
				InvalidProxyAddressTemplate invalidProxyAddressTemplate = item as InvalidProxyAddressTemplate;
				return new PropertyValidationError(DataStrings.ExceptionUsingInvalidAddress(invalidProxyAddressTemplate.ToString(), invalidProxyAddressTemplate.ParseException.Message), this.PropertyDefinition, invalidProxyAddressTemplate);
			}
			return base.ValidateValue(item);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011E2C File Offset: 0x0001002C
		public override void CopyChangesFrom(MultiValuedPropertyBase changedMvp)
		{
			if (changedMvp != null && changedMvp.GetType() != base.GetType())
			{
				throw new ArgumentOutOfRangeException("changedMvp", DataStrings.ErrorCannotCopyFromDifferentType(base.GetType(), changedMvp.GetType()));
			}
			base.CopyChangesFrom(changedMvp);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011E6C File Offset: 0x0001006C
		internal override void UpdateValues(MultiValuedPropertyBase newMvp)
		{
			if (newMvp != null && newMvp.GetType() != base.GetType())
			{
				throw new ArgumentException("newMvp", DataStrings.ExceptionCannotSetDifferentType(base.GetType(), newMvp.GetType()));
			}
			base.UpdateValues(newMvp);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00011EAC File Offset: 0x000100AC
		protected override bool TryAddInternal(T item, out Exception error)
		{
			if (null == item || base.CopyChangesOnly || this.AutoPromotionDisabled)
			{
				return base.TryAddInternal(item, out error);
			}
			ValidationError validationError = this.ValidateValue(item);
			if (validationError != null)
			{
				error = new DataValidationException(validationError);
				return false;
			}
			T t = this.FindPrimary(item.Prefix);
			if (null == t || t == item)
			{
				item = (T)((object)item.ToPrimary());
				return base.TryAddInternal(item, out error);
			}
			base.BeginUpdate();
			bool result;
			try
			{
				T t2 = (T)((object)item.ToSecondary());
				bool flag = base.TryAddInternal(t2, out error);
				if (flag && item.IsPrimaryAddress)
				{
					this.MakePrimary(t2);
				}
				result = flag;
			}
			finally
			{
				base.EndUpdate();
			}
			return result;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00011FA0 File Offset: 0x000101A0
		protected override void SetAt(int index, T item)
		{
			if (null == item)
			{
				throw new ArgumentNullException("item");
			}
			if (base[index] == item)
			{
				return;
			}
			if (base.CopyChangesOnly || this.AutoPromotionDisabled)
			{
				base.SetAt(index, item);
				return;
			}
			this.ValidateValue(item);
			base.BeginUpdate();
			try
			{
				if (base.Contains(item))
				{
					T t = base[index];
					if (t.IsPrimaryAddress)
					{
						this.MakePrimary(item);
					}
					base.RemoveAt(index);
				}
				else
				{
					T t2 = base[index];
					if (t2.Prefix == item.Prefix)
					{
						T t3 = base[index];
						if (t3.IsPrimaryAddress)
						{
							item = (T)((object)item.ToPrimary());
						}
						else
						{
							item = (T)((object)item.ToSecondary());
						}
						bool flag = this.AutoPromotionDisabled;
						this.AutoPromotionDisabled = true;
						base.SetAt(index, item);
						this.AutoPromotionDisabled = flag;
						if (item.IsPrimaryAddress)
						{
							this.MakePrimary(index);
						}
					}
					else
					{
						base.SetAt(index, item);
					}
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000120FC File Offset: 0x000102FC
		public override bool Remove(T item)
		{
			if (base.CopyChangesOnly || this.AutoPromotionDisabled)
			{
				return base.Remove(item);
			}
			int num = base.IndexOf(item);
			if (-1 != num)
			{
				item = base[num];
			}
			if (item.IsPrimaryAddress && item.Prefix == ProxyAddressPrefix.Smtp)
			{
				bool flag = false;
				for (int i = 0; i < this.Count; i++)
				{
					T t = base[i];
					if (t.IsPrimaryAddress)
					{
						T t2 = base[i];
						if (t2.Prefix == ProxyAddressPrefix.Smtp && base[i] != item && !(base[i] is IInvalidProxy))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					throw new InvalidOperationException(DataStrings.ExceptionRemoveSmtpPrimary(item.ValueString));
				}
			}
			base.BeginUpdate();
			try
			{
				if (!base.Remove(item))
				{
					return false;
				}
				if (item.IsPrimaryAddress)
				{
					ProxyAddressPrefix prefix = item.Prefix;
					if (null == this.FindPrimary(prefix))
					{
						for (int j = 0; j < this.Count; j++)
						{
							T t3 = base[j];
							if (t3.Prefix == prefix && !(base[j] is IInvalidProxy))
							{
								T t4 = base[j];
								T value = (T)((object)t4.ToPrimary());
								base.RemoveAt(j);
								this.Insert(j, value);
								break;
							}
						}
					}
				}
			}
			finally
			{
				base.EndUpdate();
			}
			return true;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000122E4 File Offset: 0x000104E4
		public T FindPrimary(ProxyAddressPrefix prefix)
		{
			T result = default(T);
			for (int i = 0; i < this.Count; i++)
			{
				T t = base[i];
				if (t.IsPrimaryAddress)
				{
					T t2 = base[i];
					if (t2.Prefix == prefix)
					{
						result = base[i];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001234C File Offset: 0x0001054C
		public void MakePrimary(T proxyAddress)
		{
			base.BeginUpdate();
			try
			{
				if (!base.Contains(proxyAddress))
				{
					base.Add(proxyAddress);
				}
				this.MakePrimary(base.IndexOf(proxyAddress));
			}
			finally
			{
				base.EndUpdate();
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00012398 File Offset: 0x00010598
		public void MakePrimary(int index)
		{
			T item = base[index];
			this.ValidateValue(item);
			bool copyChangesOnly = base.CopyChangesOnly;
			base.BeginUpdate();
			try
			{
				base.CopyChangesOnly = true;
				for (int i = 0; i < this.Count; i++)
				{
					T t = base[i];
					if (t.IsPrimaryAddress)
					{
						T t2 = base[i];
						if (t2.Prefix == item.Prefix)
						{
							int index2 = i;
							T t3 = base[i];
							base.SetAt(index2, (T)((object)t3.ToSecondary()));
						}
					}
				}
				base.SetAt(index, (T)((object)item.ToPrimary()));
			}
			finally
			{
				base.CopyChangesOnly = copyChangesOnly;
				base.EndUpdate();
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00012478 File Offset: 0x00010678
		public string[] ToStringArray()
		{
			string[] array = new string[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				string[] array2 = array;
				int num = i;
				T t = base[i];
				array2[num] = t.ToString();
			}
			return array;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000124BB File Offset: 0x000106BB
		protected ProxyAddressBaseCollection(Hashtable table) : base(table)
		{
		}

		// Token: 0x04000309 RID: 777
		private bool autoPromotionDisabled;
	}
}
