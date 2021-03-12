using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000016 RID: 22
	[TypeConverter(typeof(SimpleGenericsTypeConverter))]
	[Serializable]
	public class MultiValuedProperty<T> : MultiValuedPropertyBase, IList, ICollection, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IEquatable<MultiValuedProperty<T>>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003671 File Offset: 0x00001871
		public static MultiValuedProperty<T> Empty
		{
			get
			{
				return MultiValuedProperty<T>.empty;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003678 File Offset: 0x00001878
		private LocalizedString ReadOnlyErrorMessage
		{
			get
			{
				LocalizedString? localizedString = this.readOnlyErrorMessage;
				if (localizedString == null)
				{
					return DataStrings.ExceptionReadOnlyMultiValuedProperty;
				}
				return localizedString.GetValueOrDefault();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000036A2 File Offset: 0x000018A2
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000036AA File Offset: 0x000018AA
		public override bool Changed
		{
			get
			{
				return this.changed && (this.wasCleared || this.added.Count > 0 || this.removed.Count > 0);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000036DC File Offset: 0x000018DC
		public override bool IsChangesOnlyCopy
		{
			get
			{
				return this.isChangesOnlyCopy;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000036E4 File Offset: 0x000018E4
		internal override bool WasCleared
		{
			get
			{
				return this.wasCleared;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000036EC File Offset: 0x000018EC
		internal override object[] Added
		{
			get
			{
				if (this.HasChangeTracking)
				{
					return this.added.ToArray();
				}
				return new object[0];
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003708 File Offset: 0x00001908
		internal override object[] Removed
		{
			get
			{
				if (this.HasChangeTracking)
				{
					return this.removed.ToArray();
				}
				return new object[0];
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003724 File Offset: 0x00001924
		internal override ProviderPropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000372C File Offset: 0x0000192C
		private bool HasChangeTracking
		{
			get
			{
				return !this.IsReadOnly || this.IsChangesOnlyCopy;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003740 File Offset: 0x00001940
		private MultiValuedProperty(bool readOnly, bool validate, ProviderPropertyDefinition propertyDefinition, IEnumerable values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.propertyDefinition = propertyDefinition;
			this.isReadOnly = readOnly;
			this.readOnlyErrorMessage = readOnlyErrorMessage;
			this.changed = false;
			this.wasCleared = false;
			this.propertyValues = new List<T>();
			if (this.HasChangeTracking)
			{
				this.added = new List<object>();
				this.removed = new List<object>();
			}
			foreach (object obj in values)
			{
				if (obj != null)
				{
					T item = this.ConvertInput(obj);
					if (validate)
					{
						this.ValidateValueAndThrow(item);
						if (this.Contains(item))
						{
							throw new InvalidOperationException(DataStrings.ErrorValueAlreadyPresent(obj.ToString()));
						}
					}
					this.propertyValues.Add(item);
				}
			}
			this.propertyValues.TrimExcess();
			if (invalidValues != null && invalidValues.Count > 0 && !readOnly)
			{
				foreach (object item2 in invalidValues)
				{
					this.removed.Add(item2);
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003898 File Offset: 0x00001A98
		internal MultiValuedProperty(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : this(readOnly, false, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000038A8 File Offset: 0x00001AA8
		internal MultiValuedProperty(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : this(readOnly, true, propertyDefinition, values, null, null)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000038CC File Offset: 0x00001ACC
		public MultiValuedProperty(object value) : this(true, true, null, MultiValuedProperty<T>.GetObjectAsEnumerable(value), null, null)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038F4 File Offset: 0x00001AF4
		private static IEnumerable GetObjectAsEnumerable(object value)
		{
			if (value is IEnumerable<!0> || value is ICollection)
			{
				return (IEnumerable)value;
			}
			return new object[]
			{
				value
			};
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003924 File Offset: 0x00001B24
		public MultiValuedProperty() : this(false, false, null, new T[0], null, null)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000394C File Offset: 0x00001B4C
		public MultiValuedProperty(Dictionary<string, object> table) : this(false, true, null, new T[0], null, null)
		{
			this.isChangesOnlyCopy = true;
			bool copyChangesOnly = base.CopyChangesOnly;
			base.CopyChangesOnly = true;
			foreach (string text in table.Keys)
			{
				object values = ValueConvertor.UnwrapPSObjectIfNeeded(table[text]);
				if (!this.TryAddValues(text, values) && !this.TryRemoveValues(text, values))
				{
					throw new NotSupportedException(DataStrings.ErrorUnknownOperation(text, MultiValuedProperty<T>.ArrayToString(MultiValuedProperty<T>.AddKeys), MultiValuedProperty<T>.ArrayToString(MultiValuedProperty<T>.RemoveKeys)));
				}
			}
			base.CopyChangesOnly = copyChangesOnly;
			this.isReadOnly = true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A1C File Offset: 0x00001C1C
		private static string ArrayToString(string[] array)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append("'");
				stringBuilder.Append(array[i]);
				stringBuilder.Append("'");
				if (i != array.Length - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003A7C File Offset: 0x00001C7C
		private bool TryAddValues(string key, object values)
		{
			foreach (string b in MultiValuedProperty<T>.AddKeys)
			{
				if (string.Equals(key, b, StringComparison.OrdinalIgnoreCase))
				{
					if (values is ICollection || values is IEnumerable<!0>)
					{
						using (IEnumerator enumerator = ((IEnumerable)values).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object item = enumerator.Current;
								this.Add(item);
							}
							goto IL_6F;
						}
					}
					this.Add(values);
					IL_6F:
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B1C File Offset: 0x00001D1C
		private bool TryRemoveValues(string key, object values)
		{
			foreach (string b in MultiValuedProperty<T>.RemoveKeys)
			{
				if (string.Equals(key, b, StringComparison.OrdinalIgnoreCase))
				{
					if (values is ICollection || values is IEnumerable<!0>)
					{
						using (IEnumerator enumerator = ((IEnumerable)values).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object item = enumerator.Current;
								this.Remove(item);
							}
							goto IL_71;
						}
					}
					this.Remove(values);
					IL_71:
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BC0 File Offset: 0x00001DC0
		protected virtual T ConvertInput(object item)
		{
			return (T)((object)ValueConvertor.ConvertValue(item, typeof(T), null));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BD8 File Offset: 0x00001DD8
		internal override void Add(object item)
		{
			this.Add(this.ConvertInput(item));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003BE7 File Offset: 0x00001DE7
		internal override bool Remove(object item)
		{
			return this.Remove(this.ConvertInput(item));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003BF8 File Offset: 0x00001DF8
		private static bool Contains(IList list, T item, StringComparison comparisonType)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = list[i];
				if (OpathFilterEvaluator.Equals(obj, item, comparisonType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C34 File Offset: 0x00001E34
		private static int IndexOf(IList list, object item, StringComparison comparisonType)
		{
			if ((item is string || item is ProxyAddressBase) && typeof(T).GetTypeInfo().IsAssignableFrom(item.GetType().GetTypeInfo()))
			{
				if (item is string)
				{
					string a = item.ToString();
					for (int i = 0; i < list.Count; i++)
					{
						string b = list[i].ToString();
						if (string.Equals(a, b, comparisonType))
						{
							return i;
						}
					}
				}
				else if (item is ProxyAddressBase)
				{
					ProxyAddressBase a2 = (ProxyAddressBase)item;
					for (int j = 0; j < list.Count; j++)
					{
						ProxyAddressBase b2 = (ProxyAddressBase)list[j];
						if (ProxyAddressBase.Equals(a2, b2, comparisonType))
						{
							return j;
						}
					}
				}
				return -1;
			}
			return list.IndexOf(item);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003CFA File Offset: 0x00001EFA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D07 File Offset: 0x00001F07
		public MultiValuedProperty<T>.Enumerator GetEnumerator()
		{
			return new MultiValuedProperty<T>.Enumerator(this.propertyValues);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D14 File Offset: 0x00001F14
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003D21 File Offset: 0x00001F21
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003D24 File Offset: 0x00001F24
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.propertyValues).SyncRoot;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003D31 File Offset: 0x00001F31
		public override void CopyTo(Array array, int index)
		{
			if (this.IsChangesOnlyCopy)
			{
				throw new InvalidOperationException(DataStrings.ErrorNotSupportedForChangesOnlyCopy);
			}
			Array.Copy(this.propertyValues.ToArray(), 0, array, index, this.Count);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D64 File Offset: 0x00001F64
		public void Add(T item)
		{
			Exception ex;
			if (!this.TryAddInternal(item, out ex) && ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D84 File Offset: 0x00001F84
		public bool TryAdd(T item)
		{
			Exception ex;
			return this.TryAddInternal(item, out ex);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D9C File Offset: 0x00001F9C
		protected virtual bool TryAddInternal(T item, out Exception error)
		{
			error = null;
			if (this.IsReadOnly)
			{
				error = new InvalidOperationException(this.ReadOnlyErrorMessage.ToString());
				return false;
			}
			if (item == null)
			{
				error = new ArgumentNullException("item", DataStrings.ErrorCannotAddNullValue);
				return false;
			}
			if (this.Contains(item))
			{
				if (!base.CopyChangesOnly)
				{
					error = new InvalidOperationException(DataStrings.ErrorValueAlreadyPresent(item.ToString()));
				}
				return false;
			}
			ValidationError validationError = this.ValidateValue(item);
			if (validationError != null)
			{
				error = new DataValidationException(validationError);
				return false;
			}
			base.BeginUpdate();
			bool result;
			try
			{
				this.changed = true;
				if (!this.IsChangesOnlyCopy)
				{
					this.propertyValues.Add(item);
				}
				if (MultiValuedProperty<T>.Contains(this.removed, item, StringComparison.Ordinal))
				{
					this.removed.Remove(item);
				}
				else
				{
					this.added.Add(item);
				}
				result = true;
			}
			catch (Exception)
			{
				this.errorOnUpdate = true;
				throw;
			}
			finally
			{
				base.EndUpdate();
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003EC0 File Offset: 0x000020C0
		protected override void OnCollectionChanging(EventArgs e)
		{
			if (this.HasChangeTracking && this.propertyDefinition != null && this.propertyDefinition.AllCollectionConstraints != null)
			{
				this.backupCopy = (MultiValuedProperty<T>)base.MemberwiseClone();
				this.backupCopy.propertyValues = new List<T>(this.propertyValues);
				this.backupCopy.added = new List<object>(this.added);
				this.backupCopy.removed = new List<object>(this.removed);
			}
			this.errorOnUpdate = false;
			base.OnCollectionChanging(e);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F4C File Offset: 0x0000214C
		protected override void OnCollectionChanged(EventArgs e)
		{
			if (!this.errorOnUpdate)
			{
				try
				{
					this.ValidateCollection();
				}
				catch (DataValidationException)
				{
					if (this.backupCopy != null)
					{
						this.changed = this.backupCopy.changed;
						this.wasCleared = this.backupCopy.wasCleared;
						this.propertyValues = this.backupCopy.propertyValues;
						this.added = this.backupCopy.added;
						this.removed = this.backupCopy.removed;
					}
					throw;
				}
			}
			this.backupCopy = null;
			base.OnCollectionChanged(e);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FE8 File Offset: 0x000021E8
		private void ValidateValueAndThrow(T item)
		{
			ValidationError validationError = this.ValidateValue(item);
			if (validationError != null)
			{
				throw new DataValidationException(validationError);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004007 File Offset: 0x00002207
		protected virtual ValidationError ValidateValue(T item)
		{
			if (this.propertyDefinition != null)
			{
				return this.propertyDefinition.ValidateValue(item, false);
			}
			return null;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004028 File Offset: 0x00002228
		private void ValidateCollection()
		{
			if (this.propertyDefinition != null)
			{
				ValidationError validationError = this.propertyDefinition.ValidateCollection(this, false);
				if (validationError != null)
				{
					throw new DataValidationException(validationError);
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004058 File Offset: 0x00002258
		public override void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(this.ReadOnlyErrorMessage.ToString());
			}
			base.BeginUpdate();
			try
			{
				this.propertyValues.Clear();
				this.changed = true;
				this.added.Clear();
				this.removed.Clear();
				this.wasCleared = true;
			}
			catch (Exception)
			{
				this.errorOnUpdate = true;
				throw;
			}
			finally
			{
				base.EndUpdate();
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040F0 File Offset: 0x000022F0
		public T Find(Predicate<T> match)
		{
			if (this.IsChangesOnlyCopy)
			{
				throw new InvalidOperationException(DataStrings.ErrorNotSupportedForChangesOnlyCopy);
			}
			return this.propertyValues.Find(match);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004116 File Offset: 0x00002316
		public bool Contains(T item)
		{
			return MultiValuedProperty<T>.Contains(this.propertyValues, item, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004125 File Offset: 0x00002325
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004130 File Offset: 0x00002330
		internal override void SetIsReadOnly(bool isReadOnly, LocalizedString? readOnlyErrorMessage)
		{
			this.readOnlyErrorMessage = readOnlyErrorMessage;
			if (this.IsReadOnly == isReadOnly)
			{
				return;
			}
			bool hasChangeTracking = this.HasChangeTracking;
			this.isReadOnly = isReadOnly;
			if (this.HasChangeTracking != hasChangeTracking)
			{
				this.added = (this.HasChangeTracking ? new List<object>() : null);
				this.removed = (this.HasChangeTracking ? new List<object>() : null);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004194 File Offset: 0x00002394
		internal override void UpdateValues(MultiValuedPropertyBase newMvp)
		{
			if (newMvp != null && newMvp.IsChangesOnlyCopy)
			{
				throw new InvalidOperationException(DataStrings.ErrorNotSupportedForChangesOnlyCopy);
			}
			base.BeginUpdate();
			try
			{
				this.propertyValues.Clear();
				if (!this.IsReadOnly)
				{
					this.added.Clear();
					this.removed.Clear();
				}
				this.changed = true;
				this.wasCleared = true;
				if (newMvp != null)
				{
					foreach (object obj in ((IEnumerable)newMvp))
					{
						T t = (T)((object)obj);
						this.propertyValues.Add(t);
						if (!this.IsReadOnly)
						{
							this.added.Add(t);
						}
					}
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004278 File Offset: 0x00002478
		public override void CopyChangesFrom(MultiValuedPropertyBase changedMvp)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidObjectOperationException(this.ReadOnlyErrorMessage);
			}
			if (changedMvp == null)
			{
				throw new ArgumentNullException("changedMvp");
			}
			if (!changedMvp.Changed)
			{
				return;
			}
			bool copyChangesOnly = base.CopyChangesOnly;
			base.BeginUpdate();
			try
			{
				base.CopyChangesOnly = true;
				if (changedMvp.WasCleared)
				{
					this.Clear();
				}
				foreach (object item in changedMvp.Removed)
				{
					this.Remove(item);
				}
				foreach (object item2 in changedMvp.Added)
				{
					this.Add(item2);
				}
			}
			catch (Exception)
			{
				this.errorOnUpdate = true;
				throw;
			}
			finally
			{
				base.CopyChangesOnly = copyChangesOnly;
				base.EndUpdate();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004358 File Offset: 0x00002558
		public override int Count
		{
			get
			{
				return this.propertyValues.Count;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004368 File Offset: 0x00002568
		public virtual bool Remove(T item)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(this.ReadOnlyErrorMessage.ToString());
			}
			if (item == null)
			{
				return false;
			}
			if (!this.IsChangesOnlyCopy)
			{
				int num = this.IndexOf(item);
				if (-1 == num)
				{
					return false;
				}
				item = this.propertyValues[num];
			}
			base.BeginUpdate();
			try
			{
				this.changed = true;
				this.propertyValues.Remove(item);
				if (MultiValuedProperty<T>.Contains(this.added, item, StringComparison.Ordinal))
				{
					this.added.Remove(item);
				}
				else
				{
					this.removed.Add(item);
				}
			}
			catch (Exception)
			{
				this.errorOnUpdate = true;
				throw;
			}
			finally
			{
				base.EndUpdate();
			}
			return true;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004448 File Offset: 0x00002648
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x1700002E RID: 46
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = this.ConvertInput(value);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004470 File Offset: 0x00002670
		int IList.Add(object value)
		{
			T t = this.ConvertInput(value);
			this.Add(t);
			return this.IndexOf(t);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004493 File Offset: 0x00002693
		bool IList.Contains(object value)
		{
			return this.Contains(this.ConvertInput(value));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000044A4 File Offset: 0x000026A4
		int IList.IndexOf(object value)
		{
			T value2 = default(T);
			try
			{
				value2 = this.ConvertInput(value);
			}
			catch (InvalidCastException)
			{
				return -1;
			}
			return this.IndexOf(value2);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000044E4 File Offset: 0x000026E4
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.ConvertInput(value));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000044F4 File Offset: 0x000026F4
		void IList.Remove(object value)
		{
			this.Remove(this.ConvertInput(value));
		}

		// Token: 0x1700002F RID: 47
		public T this[int index]
		{
			get
			{
				return this.propertyValues[index];
			}
			set
			{
				this.SetAt(index, value);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000451C File Offset: 0x0000271C
		public int IndexOf(T value)
		{
			return MultiValuedProperty<T>.IndexOf(this.propertyValues, value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004530 File Offset: 0x00002730
		public virtual void Insert(int index, T value)
		{
			if (index < 0 || index > this.propertyValues.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, DataStrings.ErrorOutOfRange(0, this.propertyValues.Count));
			}
			this.Add(value);
			T t = this[index];
			if (!t.Equals(value))
			{
				int index2 = this.IndexOf(value);
				T item = this.propertyValues[index2];
				this.propertyValues.RemoveAt(index2);
				this.propertyValues.Insert(index, item);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000045C8 File Offset: 0x000027C8
		public void RemoveAt(int index)
		{
			if (index < 0 || index > this.propertyValues.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, DataStrings.ErrorOutOfRange(0, this.propertyValues.Count));
			}
			this.Remove(this[index]);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000461C File Offset: 0x0000281C
		public override object Clone()
		{
			MultiValuedProperty<T> multiValuedProperty = (MultiValuedProperty<T>)CloneHelper.SerializeObj(this);
			multiValuedProperty.propertyDefinition = this.propertyDefinition;
			return multiValuedProperty;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004644 File Offset: 0x00002844
		protected virtual void SetAt(int index, T item)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(this.ReadOnlyErrorMessage.ToString());
			}
			if (item == null)
			{
				throw new ArgumentNullException("item", DataStrings.ErrorCannotAddNullValue);
			}
			if (index < 0 || index >= this.propertyValues.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, DataStrings.ErrorOutOfRange(0, this.propertyValues.Count - 1));
			}
			if (MultiValuedProperty<T>.Contains(this.propertyValues, item, StringComparison.Ordinal))
			{
				T t = this[index];
				if (t.Equals(item))
				{
					return;
				}
			}
			int num = this.IndexOf(item);
			if (num >= 0 && num != index)
			{
				throw new InvalidOperationException(DataStrings.ErrorValueAlreadyPresent(item.ToString()));
			}
			this.ValidateValueAndThrow(item);
			base.BeginUpdate();
			try
			{
				this.RemoveAt(index);
				this.Insert(index, item);
			}
			catch (Exception)
			{
				this.errorOnUpdate = true;
				throw;
			}
			finally
			{
				base.EndUpdate();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004774 File Offset: 0x00002974
		public void Sort()
		{
			this.propertyValues.Sort();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004781 File Offset: 0x00002981
		public static implicit operator MultiValuedProperty<T>(object[] array)
		{
			if (array == null)
			{
				return null;
			}
			return new MultiValuedProperty<T>(true, null, array);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004790 File Offset: 0x00002990
		public T[] ToArray()
		{
			if (this.IsChangesOnlyCopy)
			{
				throw new InvalidOperationException(DataStrings.ErrorNotSupportedForChangesOnlyCopy);
			}
			return this.propertyValues.ToArray();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000047B5 File Offset: 0x000029B5
		internal override void ResetChangeTracking()
		{
			if (this.HasChangeTracking)
			{
				this.added.Clear();
				this.removed.Clear();
			}
			this.wasCleared = false;
			this.changed = false;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000047E4 File Offset: 0x000029E4
		internal override void MarkAsChanged()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(this.ReadOnlyErrorMessage.ToString());
			}
			if (this.wasCleared || this.added.Count > 0 || this.removed.Count > 0)
			{
				base.BeginUpdate();
				this.changed = true;
				base.EndUpdate();
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000484C File Offset: 0x00002A4C
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.serializerAssemblyVersion = ConfigurableObject.ExecutingAssemblyVersion;
			this.serializedPropertyValues = new object[this.propertyValues.Count];
			for (int i = 0; i < this.propertyValues.Count; i++)
			{
				this.serializedPropertyValues[i] = this.SerializeValuePrivate(this.propertyValues[i]);
			}
			if (this.HasChangeTracking)
			{
				if (this.added.Count > 0)
				{
					this.serializedAddedValues = new object[this.added.Count];
					for (int j = 0; j < this.added.Count; j++)
					{
						this.serializedAddedValues[j] = this.SerializeValuePrivate(this.added[j]);
					}
				}
				if (this.removed.Count > 0)
				{
					this.serializedRemovedValues = new object[this.removed.Count];
					for (int k = 0; k < this.removed.Count; k++)
					{
						this.serializedRemovedValues[k] = this.SerializeValuePrivate(this.removed[k]);
					}
				}
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004962 File Offset: 0x00002B62
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			this.serializedPropertyValues = null;
			this.serializedAddedValues = null;
			this.serializedRemovedValues = null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000497C File Offset: 0x00002B7C
		internal override void FinalizeDeserialization()
		{
			if (this.isDeserializationComplete)
			{
				return;
			}
			if (this.propertyValues != null)
			{
				throw new InvalidOperationException("Cannot deserialize values when MVP has already been populated.");
			}
			if (this.serializedPropertyValues == null)
			{
				throw new InvalidOperationException("Cannot deserialize values when no serialization information is available.");
			}
			this.propertyValues = new List<T>(this.serializedPropertyValues.Length);
			for (int i = 0; i < this.serializedPropertyValues.Length; i++)
			{
				this.propertyValues.Add((T)((object)this.DeserializeValuePrivate(this.serializedPropertyValues[i])));
			}
			if (this.HasChangeTracking)
			{
				if (this.serializedAddedValues != null)
				{
					this.added = new List<object>(this.serializedAddedValues.Length);
					for (int j = 0; j < this.serializedAddedValues.Length; j++)
					{
						this.added.Add(this.DeserializeValuePrivate(this.serializedAddedValues[j]));
					}
				}
				else
				{
					this.added = new List<object>();
				}
				if (this.serializedRemovedValues != null)
				{
					this.removed = new List<object>(this.serializedRemovedValues.Length);
					for (int k = 0; k < this.serializedRemovedValues.Length; k++)
					{
						this.removed.Add(this.DeserializeValuePrivate(this.serializedRemovedValues[k]));
					}
				}
				else
				{
					this.removed = new List<object>();
				}
			}
			this.serializedPropertyValues = null;
			this.serializedAddedValues = null;
			this.serializedRemovedValues = null;
			this.isDeserializationComplete = true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004ACB File Offset: 0x00002CCB
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.FinalizeDeserialization();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004AD4 File Offset: 0x00002CD4
		private object SerializeValuePrivate(object value)
		{
			if (this.PropertyDefinition == null)
			{
				return value;
			}
			if (SerializationTypeConverter.TypeIsSerializable(this.PropertyDefinition.Type))
			{
				return value;
			}
			object result;
			try
			{
				result = this.SerializeValue(value);
			}
			catch (FormatException innerException)
			{
				throw new SerializationException(DataStrings.ErrorConversionFailed(this.PropertyDefinition, value), innerException);
			}
			catch (NotImplementedException innerException2)
			{
				throw new SerializationException(DataStrings.ErrorConversionFailed(this.PropertyDefinition, value), innerException2);
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004B58 File Offset: 0x00002D58
		protected virtual object SerializeValue(object value)
		{
			return ValueConvertor.SerializeData(this.PropertyDefinition, value);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004B68 File Offset: 0x00002D68
		private object DeserializeValuePrivate(object value)
		{
			if (this.PropertyDefinition == null)
			{
				return value;
			}
			if (SerializationTypeConverter.TypeIsSerializable(this.PropertyDefinition.Type))
			{
				return value;
			}
			object result;
			try
			{
				result = this.DeserializeValue(value);
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyConversionError(DataStrings.ErrorConversionFailed(this.PropertyDefinition, value), this.PropertyDefinition, value, ex), ex);
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004BD0 File Offset: 0x00002DD0
		protected virtual object DeserializeValue(object value)
		{
			return ValueConvertor.DeserializeData(this.PropertyDefinition, value);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004BDE File Offset: 0x00002DDE
		internal override void UpdatePropertyDefinition(ProviderPropertyDefinition newPropertyDefinition)
		{
			if (newPropertyDefinition == null)
			{
				throw new ArgumentNullException("newPropertyDefinition");
			}
			this.propertyDefinition = newPropertyDefinition;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004BF8 File Offset: 0x00002DF8
		private static bool ListsAreEqual(IList listA, IList listB)
		{
			if (object.ReferenceEquals(listA, listB))
			{
				return true;
			}
			if (listA == null || listB == null)
			{
				return false;
			}
			if (listA.Count != listB.Count)
			{
				return false;
			}
			foreach (object value in listA)
			{
				if (!listB.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004C74 File Offset: 0x00002E74
		public bool Equals(MultiValuedProperty<T> other)
		{
			return other != null && (object.ReferenceEquals(other, this) || (other.Count == this.Count && other.WasCleared == this.WasCleared && other.IsReadOnly == this.IsReadOnly && other.IsCompletelyRead == base.IsCompletelyRead && other.Changed == this.Changed && object.Equals(other.GetType(), base.GetType()) && object.Equals(other.PropertyDefinition, this.PropertyDefinition) && (MultiValuedProperty<T>.ListsAreEqual(other.added, this.added) && MultiValuedProperty<T>.ListsAreEqual(other.removed, this.removed) && MultiValuedProperty<T>.ListsAreEqual(other.propertyValues, this.propertyValues) && MultiValuedProperty<T>.ListsAreEqual(other.serializedAddedValues, this.serializedAddedValues) && MultiValuedProperty<T>.ListsAreEqual(other.serializedRemovedValues, this.serializedRemovedValues) && MultiValuedProperty<T>.ListsAreEqual(other.serializedPropertyValues, this.serializedPropertyValues))));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004D74 File Offset: 0x00002F74
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MultiValuedProperty<T>);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D84 File Offset: 0x00002F84
		public override int GetHashCode()
		{
			int num = (this.PropertyDefinition == null) ? 0 : this.PropertyDefinition.GetHashCode();
			return num ^ base.GetType().GetHashCode() ^ this.Count;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004DBC File Offset: 0x00002FBC
		public MultiValuedProperty(Hashtable table) : this(false, true, null, new T[0], null, null)
		{
			this.isChangesOnlyCopy = true;
			bool copyChangesOnly = base.CopyChangesOnly;
			base.CopyChangesOnly = true;
			foreach (object obj in table.Keys)
			{
				string text = (string)obj;
				object values = ValueConvertor.UnwrapPSObjectIfNeeded(table[text]);
				if (!this.TryAddValues(text, values) && !this.TryRemoveValues(text, values))
				{
					throw new NotSupportedException(DataStrings.ErrorUnknownOperation(text, MultiValuedProperty<T>.ArrayToString(MultiValuedProperty<T>.AddKeys), MultiValuedProperty<T>.ArrayToString(MultiValuedProperty<T>.RemoveKeys)));
				}
			}
			base.CopyChangesOnly = copyChangesOnly;
			this.isReadOnly = true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004E98 File Offset: 0x00003098
		public static MultiValuedProperty<T>operator +(MultiValuedProperty<T> oldCollection, object newValue)
		{
			if (oldCollection.IsChangesOnlyCopy)
			{
				throw new InvalidOperationException(DataStrings.ErrorNotSupportedForChangesOnlyCopy);
			}
			object[] args = new object[]
			{
				oldCollection.IsReadOnly,
				oldCollection.PropertyDefinition,
				oldCollection
			};
			MultiValuedProperty<T> multiValuedProperty = (MultiValuedProperty<T>)Activator.CreateInstance(oldCollection.GetType(), BindingFlags.Instance | BindingFlags.NonPublic, null, args, null);
			multiValuedProperty.Add(newValue);
			return multiValuedProperty;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004F00 File Offset: 0x00003100
		public static MultiValuedProperty<T>operator -(MultiValuedProperty<T> oldCollection, object newValue)
		{
			if (oldCollection.IsChangesOnlyCopy)
			{
				throw new InvalidOperationException(DataStrings.ErrorNotSupportedForChangesOnlyCopy);
			}
			object[] args = new object[]
			{
				oldCollection.IsReadOnly,
				oldCollection.PropertyDefinition,
				oldCollection
			};
			MultiValuedProperty<T> multiValuedProperty = (MultiValuedProperty<T>)Activator.CreateInstance(oldCollection.GetType(), BindingFlags.Instance | BindingFlags.NonPublic, null, args, null);
			multiValuedProperty.Remove(newValue);
			return multiValuedProperty;
		}

		// Token: 0x04000034 RID: 52
		internal const string AddName = "Add";

		// Token: 0x04000035 RID: 53
		internal const string RemoveName = "Remove";

		// Token: 0x04000036 RID: 54
		private static MultiValuedProperty<T> empty = new MultiValuedProperty<T>(true, null, new object[0]);

		// Token: 0x04000037 RID: 55
		[NonSerialized]
		private MultiValuedProperty<T> backupCopy;

		// Token: 0x04000038 RID: 56
		[NonSerialized]
		private bool errorOnUpdate;

		// Token: 0x04000039 RID: 57
		[NonSerialized]
		private List<T> propertyValues;

		// Token: 0x0400003A RID: 58
		[NonSerialized]
		private List<object> added;

		// Token: 0x0400003B RID: 59
		[NonSerialized]
		private List<object> removed;

		// Token: 0x0400003C RID: 60
		[NonSerialized]
		private bool isDeserializationComplete;

		// Token: 0x0400003D RID: 61
		private LocalizedString? readOnlyErrorMessage;

		// Token: 0x0400003E RID: 62
		private bool isReadOnly;

		// Token: 0x0400003F RID: 63
		private bool isChangesOnlyCopy;

		// Token: 0x04000040 RID: 64
		private bool wasCleared;

		// Token: 0x04000041 RID: 65
		private bool changed;

		// Token: 0x04000042 RID: 66
		private ProviderPropertyDefinition propertyDefinition;

		// Token: 0x04000043 RID: 67
		private Version serializerAssemblyVersion;

		// Token: 0x04000044 RID: 68
		private object[] serializedPropertyValues;

		// Token: 0x04000045 RID: 69
		private object[] serializedAddedValues;

		// Token: 0x04000046 RID: 70
		private object[] serializedRemovedValues;

		// Token: 0x04000047 RID: 71
		internal static readonly string[] AddKeys = new string[]
		{
			"Add",
			"+"
		};

		// Token: 0x04000048 RID: 72
		internal static readonly string[] RemoveKeys = new string[]
		{
			"Remove",
			"-"
		};

		// Token: 0x02000017 RID: 23
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060000E9 RID: 233 RVA: 0x00004FC1 File Offset: 0x000031C1
			internal Enumerator(IList<T> list)
			{
				this.list = list;
				this.currentIndex = -1;
				this.currentItem = default(T);
				this.count = list.Count;
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000EA RID: 234 RVA: 0x00004FE9 File Offset: 0x000031E9
			public T Current
			{
				get
				{
					return this.currentItem;
				}
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00004FF1 File Offset: 0x000031F1
			public void Dispose()
			{
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000EC RID: 236 RVA: 0x00004FF3 File Offset: 0x000031F3
			object IEnumerator.Current
			{
				get
				{
					return this.currentItem;
				}
			}

			// Token: 0x060000ED RID: 237 RVA: 0x00005000 File Offset: 0x00003200
			public bool MoveNext()
			{
				if (++this.currentIndex < this.count)
				{
					this.currentItem = this.list[this.currentIndex];
					return true;
				}
				return false;
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00005040 File Offset: 0x00003240
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentItem = default(T);
			}

			// Token: 0x04000049 RID: 73
			private IList<T> list;

			// Token: 0x0400004A RID: 74
			private int currentIndex;

			// Token: 0x0400004B RID: 75
			private T currentItem;

			// Token: 0x0400004C RID: 76
			private int count;
		}
	}
}
