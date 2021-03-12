using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002BA RID: 698
	internal struct StyleBuildHelper
	{
		// Token: 0x06001BC3 RID: 7107 RVA: 0x000D52A3 File Offset: 0x000D34A3
		internal StyleBuildHelper(FormatStore store)
		{
			this.Store = store;
			this.PropertyMask = default(PropertyBitMask);
			this.entries = null;
			this.topEntry = 0;
			this.currentEntry = -1;
			this.nonFlagPropertiesCount = 0;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000D52D4 File Offset: 0x000D34D4
		public void Clean()
		{
			this.PropertyMask.ClearAll();
			this.topEntry = 0;
			this.currentEntry = -1;
			this.nonFlagPropertiesCount = 0;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000D52F8 File Offset: 0x000D34F8
		public void SetProperty(int precedence, PropertyId id, PropertyValue value)
		{
			int entry = this.GetEntry(precedence);
			this.SetPropertyImpl(entry, id, value);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000D5318 File Offset: 0x000D3518
		public void SetProperty(int precedence, Property prop)
		{
			int entry = this.GetEntry(precedence);
			this.SetPropertyImpl(entry, prop.Id, prop.Value);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000D5344 File Offset: 0x000D3544
		public void AddStyle(int precedence, int handle)
		{
			int entry = this.GetEntry(precedence);
			FormatStyle style = this.Store.GetStyle(handle);
			this.entries[entry].FlagProperties.Merge(style.FlagProperties);
			Property[] propertyList = style.PropertyList;
			if (propertyList != null)
			{
				foreach (Property property in propertyList)
				{
					if (property.Value.IsRefCountedHandle)
					{
						this.Store.AddRefValue(property.Value);
					}
					this.SetPropertyImpl(entry, property.Id, property.Value);
				}
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000D53E0 File Offset: 0x000D35E0
		public void AddProperties(int precedence, FlagProperties flagProperties, PropertyBitMask propertyMask, Property[] propList)
		{
			int entry = this.GetEntry(precedence);
			this.entries[entry].FlagProperties.Merge(flagProperties);
			if (propList != null)
			{
				foreach (Property property in propList)
				{
					if (propertyMask.IsSet(property.Id))
					{
						if (property.Value.IsRefCountedHandle)
						{
							this.Store.AddRefValue(property.Value);
						}
						this.SetPropertyImpl(entry, property.Id, property.Value);
					}
				}
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000D5474 File Offset: 0x000D3674
		public PropertyValue GetProperty(PropertyId id)
		{
			if (FlagProperties.IsFlagProperty(id))
			{
				for (int i = 0; i < this.topEntry; i++)
				{
					if (this.entries[i].FlagProperties.IsDefined(id))
					{
						return this.entries[i].FlagProperties.GetPropertyValue(id);
					}
				}
			}
			else if (this.PropertyMask.IsSet(id))
			{
				int num;
				int num2;
				this.FindProperty(id, out num, out num2);
				return this.entries[num].Properties[num2].Value;
			}
			return PropertyValue.Null;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x000D54FC File Offset: 0x000D36FC
		public void GetPropertyList(out Property[] propertyList, out FlagProperties effectiveFlagProperties, out PropertyBitMask effectivePropertyMask)
		{
			effectiveFlagProperties = default(FlagProperties);
			effectivePropertyMask = this.PropertyMask;
			for (int i = this.topEntry - 1; i >= 0; i--)
			{
				effectiveFlagProperties.Merge(this.entries[i].FlagProperties);
			}
			if (this.nonFlagPropertiesCount != 0)
			{
				propertyList = new Property[this.nonFlagPropertiesCount];
				if (this.topEntry == 1)
				{
					Array.Copy(this.entries[0].Properties, 0, propertyList, 0, this.nonFlagPropertiesCount);
				}
				else if (this.topEntry == 2)
				{
					Property[] properties = this.entries[0].Properties;
					Property[] properties2 = this.entries[1].Properties;
					int num = 0;
					int num2 = 0;
					int count = this.entries[0].Count;
					int count2 = this.entries[1].Count;
					int num3 = 0;
					for (;;)
					{
						if (num < count)
						{
							if (num2 == count2 || properties[num].Id <= properties2[num2].Id)
							{
								propertyList[num3++] = properties[num++];
							}
							else
							{
								propertyList[num3++] = properties2[num2++];
							}
						}
						else
						{
							if (num2 >= count2)
							{
								break;
							}
							propertyList[num3++] = properties2[num2++];
						}
					}
				}
				else
				{
					PropertyId propertyId = PropertyId.MergedCell;
					int num4 = 0;
					while ((propertyId += 1) < PropertyId.MaxValue)
					{
						if (this.PropertyMask.IsSet(propertyId))
						{
							int num5;
							int num6;
							this.FindProperty(propertyId, out num5, out num6);
							propertyList[num4++] = this.entries[num5].Properties[num6];
						}
					}
				}
			}
			else
			{
				propertyList = null;
			}
			this.Clean();
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000D56E0 File Offset: 0x000D38E0
		private void InitializeEntry(int entry, int precedence)
		{
			if (this.entries == null)
			{
				this.entries = new StyleBuildHelper.PrecedenceEntry[4];
			}
			else if (entry == this.entries.Length)
			{
				if (entry == 16)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				StyleBuildHelper.PrecedenceEntry[] destinationArray = new StyleBuildHelper.PrecedenceEntry[this.entries.Length * 2];
				Array.Copy(this.entries, 0, destinationArray, 0, this.entries.Length);
				this.entries = destinationArray;
			}
			if (this.entries[entry] == null)
			{
				this.entries[entry] = new StyleBuildHelper.PrecedenceEntry(precedence);
				return;
			}
			this.entries[entry].ReInitialize(precedence);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000D5774 File Offset: 0x000D3974
		private int GetEntry(int precedence)
		{
			int num = this.currentEntry;
			if (num != -1)
			{
				if (this.entries[num].Precedence != precedence)
				{
					num = this.topEntry - 1;
					while (num >= 0 && this.entries[num].Precedence >= precedence)
					{
						num--;
					}
					num++;
					if (num == this.topEntry || this.entries[num].Precedence != precedence)
					{
						this.InitializeEntry(this.topEntry, precedence);
						if (num < this.topEntry)
						{
							StyleBuildHelper.PrecedenceEntry precedenceEntry = this.entries[this.topEntry];
							for (int i = this.topEntry - 1; i >= num; i--)
							{
								this.entries[i + 1] = this.entries[i];
							}
							this.entries[num] = precedenceEntry;
						}
						this.topEntry++;
					}
				}
			}
			else
			{
				num = this.topEntry++;
				this.InitializeEntry(num, precedence);
			}
			this.currentEntry = num;
			return num;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000D5868 File Offset: 0x000D3A68
		private void AddProperty(int entry, PropertyId id, PropertyValue value)
		{
			int num = this.entries[entry].Count - 1;
			while (num >= 0 && this.entries[entry].Properties[num].Id > id)
			{
				this.entries[entry].Properties[num + 1] = this.entries[entry].Properties[num];
				num--;
			}
			this.entries[entry].Count++;
			this.entries[entry].Properties[num + 1].Set(id, value);
			this.entries[entry].PropertyMask.Set(id);
			this.PropertyMask.Set(id);
			this.nonFlagPropertiesCount++;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000D593C File Offset: 0x000D3B3C
		private void RemoveProperty(int entry, int index)
		{
			this.entries[entry].PropertyMask.Clear(this.entries[entry].Properties[index].Id);
			if (this.entries[entry].Properties[index].Value.IsRefCountedHandle)
			{
				this.Store.ReleaseValue(this.entries[entry].Properties[index].Value);
			}
			this.entries[entry].Count--;
			for (int i = index; i < this.entries[entry].Count; i++)
			{
				this.entries[entry].Properties[i] = this.entries[entry].Properties[i + 1];
			}
			this.nonFlagPropertiesCount--;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000D5A28 File Offset: 0x000D3C28
		private void FindProperty(PropertyId id, out int entryFound, out int indexFound)
		{
			for (entryFound = 0; entryFound < this.topEntry; entryFound++)
			{
				if (this.entries[entryFound].PropertyMask.IsSet(id))
				{
					int num = 0;
					int count = this.entries[entryFound].Count;
					Property[] properties = this.entries[entryFound].Properties;
					indexFound = this.entries[entryFound].NextSearchIndex;
					if (indexFound < count)
					{
						if (properties[indexFound].Id == id)
						{
							this.entries[entryFound].NextSearchIndex++;
							return;
						}
						if (properties[indexFound].Id < id)
						{
							num = indexFound + 1;
						}
					}
					for (indexFound = num; indexFound < count; indexFound++)
					{
						if (properties[indexFound].Id == id)
						{
							this.entries[entryFound].NextSearchIndex = indexFound + 1;
							return;
						}
					}
				}
			}
			indexFound = -1;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000D5B10 File Offset: 0x000D3D10
		private void SetPropertyImpl(int entry, PropertyId id, PropertyValue value)
		{
			if (!FlagProperties.IsFlagProperty(id))
			{
				if (this.PropertyMask.IsSet(id))
				{
					int num;
					int num2;
					this.FindProperty(id, out num, out num2);
					if (num < entry)
					{
						return;
					}
					if (num == entry)
					{
						if (this.entries[entry].Properties[num2].Value.IsRefCountedHandle)
						{
							this.Store.ReleaseValue(this.entries[entry].Properties[num2].Value);
						}
						else if (this.entries[entry].Properties[num2].Value.IsRelativeHtmlFontUnits && value.IsRelativeHtmlFontUnits)
						{
							value = new PropertyValue(PropertyType.RelHtmlFontUnits, this.entries[entry].Properties[num2].Value.RelativeHtmlFontUnits + value.RelativeHtmlFontUnits);
						}
						this.entries[entry].Properties[num2].Value = value;
						return;
					}
					this.RemoveProperty(num, num2);
				}
				if (!value.IsNull)
				{
					this.AddProperty(entry, id, value);
				}
				return;
			}
			if (!value.IsNull)
			{
				this.entries[entry].FlagProperties.Set(id, value.Bool);
				return;
			}
			this.entries[entry].FlagProperties.Remove(id);
		}

		// Token: 0x04002120 RID: 8480
		internal FormatStore Store;

		// Token: 0x04002121 RID: 8481
		internal PropertyBitMask PropertyMask;

		// Token: 0x04002122 RID: 8482
		private StyleBuildHelper.PrecedenceEntry[] entries;

		// Token: 0x04002123 RID: 8483
		private int topEntry;

		// Token: 0x04002124 RID: 8484
		private int currentEntry;

		// Token: 0x04002125 RID: 8485
		private int nonFlagPropertiesCount;

		// Token: 0x020002BB RID: 699
		private class PrecedenceEntry
		{
			// Token: 0x06001BD1 RID: 7121 RVA: 0x000D5C5D File Offset: 0x000D3E5D
			public PrecedenceEntry(int precedence)
			{
				this.Precedence = precedence;
				this.Properties = new Property[73];
			}

			// Token: 0x06001BD2 RID: 7122 RVA: 0x000D5C79 File Offset: 0x000D3E79
			public void ReInitialize(int precedence)
			{
				this.Precedence = precedence;
				this.FlagProperties.ClearAll();
				this.PropertyMask.ClearAll();
				this.Count = 0;
				this.NextSearchIndex = 0;
			}

			// Token: 0x04002126 RID: 8486
			public int Precedence;

			// Token: 0x04002127 RID: 8487
			public FlagProperties FlagProperties;

			// Token: 0x04002128 RID: 8488
			public PropertyBitMask PropertyMask;

			// Token: 0x04002129 RID: 8489
			public Property[] Properties;

			// Token: 0x0400212A RID: 8490
			public int Count;

			// Token: 0x0400212B RID: 8491
			public int NextSearchIndex;
		}
	}
}
