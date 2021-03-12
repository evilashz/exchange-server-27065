using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200086C RID: 2156
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Category
	{
		// Token: 0x0600511C RID: 20764 RVA: 0x00151C0F File Offset: 0x0014FE0F
		private Category(MemoryPropertyBag propertyBagToAssume)
		{
			this.propertyBag = propertyBagToAssume;
			this.categoryLastTimeUsed = new Category.CategoryLastTimeUsed(this);
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x00151C2A File Offset: 0x0014FE2A
		private Category() : this(new MemoryPropertyBag())
		{
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x00151C37 File Offset: 0x0014FE37
		private Category(Category copyFrom) : this(new MemoryPropertyBag(copyFrom.propertyBag))
		{
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x00151C4A File Offset: 0x0014FE4A
		public bool AllowRenameOnFirstUse
		{
			get
			{
				this.CheckAbandoned("AllowRenameOnFirstUse::get");
				return (bool)this.propertyBag[CategorySchema.AllowRenameOnFirstUse];
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06005120 RID: 20768 RVA: 0x00151C6C File Offset: 0x0014FE6C
		// (set) Token: 0x06005121 RID: 20769 RVA: 0x00151C8E File Offset: 0x0014FE8E
		public int Color
		{
			get
			{
				this.CheckAbandoned("Color::get");
				return (int)this.propertyBag[CategorySchema.Color];
			}
			set
			{
				this.CheckAbandoned("Color::set");
				this.propertyBag[CategorySchema.Color] = value;
				this.UpdateLastTimeUsed();
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x00151CB7 File Offset: 0x0014FEB7
		public Guid Guid
		{
			get
			{
				this.CheckAbandoned("Guid::get");
				return (Guid)this.propertyBag[CategorySchema.Guid];
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x00151CD9 File Offset: 0x0014FED9
		// (set) Token: 0x06005124 RID: 20772 RVA: 0x00151CFB File Offset: 0x0014FEFB
		public int KeyboardShortcut
		{
			get
			{
				this.CheckAbandoned("KeyboardShortcut::get");
				return (int)this.propertyBag[CategorySchema.KeyboardShortcut];
			}
			set
			{
				this.CheckAbandoned("KeyboardShortcut::set");
				this.propertyBag[CategorySchema.KeyboardShortcut] = value;
				this.UpdateLastTimeUsed();
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x00151D24 File Offset: 0x0014FF24
		public string Name
		{
			get
			{
				this.CheckAbandoned("Name::get");
				return (string)this.propertyBag[CategorySchema.Name];
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x00151D46 File Offset: 0x0014FF46
		internal Category.CategoryLastTimeUsed LastTimeUsed
		{
			get
			{
				this.CheckAbandoned("LastTimeUsed::get");
				return this.categoryLastTimeUsed;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x00151D59 File Offset: 0x0014FF59
		internal MemoryPropertyBag CategoryPropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00151D64 File Offset: 0x0014FF64
		public static Category Create(string name, int color, bool renameOnFirstUse)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Category category = new Category();
			category.propertyBag[CategorySchema.Name] = name;
			category.propertyBag[CategorySchema.Color] = color;
			category.propertyBag[CategorySchema.AllowRenameOnFirstUse] = renameOnFirstUse;
			category.propertyBag[CategorySchema.Guid] = Guid.NewGuid();
			category.propertyBag.SetAllPropertiesLoaded();
			category.LastTimeUsed.SetForAllModules(ExDateTime.GetNow(ExTimeZone.UtcTimeZone));
			category.ValidateAndFillInDefaults();
			return category;
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00151E04 File Offset: 0x00150004
		internal static Category Load(IEnumerable<PropValue> propValues)
		{
			Category category = new Category();
			IDirectPropertyBag directPropertyBag = category.propertyBag;
			foreach (PropValue propValue in propValues)
			{
				directPropertyBag.SetValue(propValue.Property, propValue.Value);
			}
			category.propertyBag.SetAllPropertiesLoaded();
			category.propertyBag.ClearChangeInfo();
			category.ValidateAndFillInDefaults();
			return category;
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00151E84 File Offset: 0x00150084
		internal static Category Resolve(Category client, Category server, Category original)
		{
			if (client != null && server != null)
			{
				return Category.Merge(client, server, original);
			}
			if (client != null)
			{
				if (original == null || client.LastTimeUsed[OutlookModule.None] > original.LastTimeUsed[OutlookModule.None])
				{
					return client.Clone();
				}
			}
			else if (server != null && original == null)
			{
				return server.Clone();
			}
			return null;
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x00151EDA File Offset: 0x001500DA
		internal void AssignMasterCategoryList(MasterCategoryList masterCategoryList)
		{
			this.CheckAbandoned("AssignMasterCategoryList");
			if (this.masterCategoryList == null)
			{
				this.masterCategoryList = masterCategoryList;
				return;
			}
			throw new InvalidOperationException("Category cannot be added to more than one MasterCategoryList");
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x00151F01 File Offset: 0x00150101
		internal void Abandon()
		{
			this.isAbandoned = true;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x00151F0A File Offset: 0x0015010A
		internal Category Clone()
		{
			return new Category(this);
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x00151F12 File Offset: 0x00150112
		internal void Detach()
		{
			this.CheckAbandoned("Detach");
			if (this.masterCategoryList != null)
			{
				this.masterCategoryList = null;
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x00151F34 File Offset: 0x00150134
		internal object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			return this.propertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00151F42 File Offset: 0x00150142
		internal void UpdateLastTimeUsed(ExDateTime newLastTimeUsed, OutlookModule? restrictToModule)
		{
			if (restrictToModule != null)
			{
				this.categoryLastTimeUsed[restrictToModule.Value] = newLastTimeUsed;
				this.categoryLastTimeUsed[OutlookModule.None] = newLastTimeUsed;
				return;
			}
			this.categoryLastTimeUsed.SetForAllModules(newLastTimeUsed);
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00151F7A File Offset: 0x0015017A
		private static Category Merge(Category client, Category server, Category original)
		{
			return Category.Load(MasterCategoryList.ResolveProperties(client.propertyBag, server.propertyBag, (original != null) ? original.propertyBag : null, AcrProfile.CategoryProfile));
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00151FA3 File Offset: 0x001501A3
		private void CheckAbandoned(string methodName)
		{
			if (this.isAbandoned)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new InvalidOperationException("This Category object was invalidated by the last call to MasterCategoryList.Save()");
			}
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x00151FBF File Offset: 0x001501BF
		private void UpdateLastTimeUsed()
		{
			this.UpdateLastTimeUsed(ExDateTime.GetNow(ExTimeZone.UtcTimeZone), new OutlookModule?(OutlookModule.None));
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x00151FD8 File Offset: 0x001501D8
		private void ValidateAndFillInDefaults()
		{
			foreach (PropertyDefinition propertyDefinition in CategorySchema.Instance.AllProperties)
			{
				XmlAttributePropertyDefinition xmlAttributePropertyDefinition = propertyDefinition as XmlAttributePropertyDefinition;
				if (xmlAttributePropertyDefinition != null)
				{
					object obj = this.propertyBag.TryGetProperty(xmlAttributePropertyDefinition);
					PropertyValidationError[] array = null;
					if (PropertyError.IsPropertyNotFound(obj) || (array = xmlAttributePropertyDefinition.Validate(null, obj)).Length > 0)
					{
						if (xmlAttributePropertyDefinition.HasDefaultValue)
						{
							this.propertyBag[xmlAttributePropertyDefinition] = xmlAttributePropertyDefinition.DefaultValue;
						}
						else if (array != null && array.Length > 0)
						{
							throw new PropertyValidationException(array[0].Description, xmlAttributePropertyDefinition, array);
						}
					}
				}
			}
			List<StoreObjectValidationError> list = new List<StoreObjectValidationError>();
			ValidationContext context = new ValidationContext(null);
			foreach (StoreObjectConstraint storeObjectConstraint in CategorySchema.Instance.Constraints)
			{
				StoreObjectValidationError storeObjectValidationError = storeObjectConstraint.Validate(context, this.propertyBag);
				if (storeObjectValidationError != null)
				{
					list.Add(storeObjectValidationError);
				}
			}
			if (list.Count > 0)
			{
				throw new ObjectValidationException(list[0].Description, list.ToArray());
			}
		}

		// Token: 0x04002C44 RID: 11332
		internal static readonly char[] ProhibitedCharacters = new char[]
		{
			','
		};

		// Token: 0x04002C45 RID: 11333
		internal static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x04002C46 RID: 11334
		internal static readonly StringComparison NameComparison = StringComparison.OrdinalIgnoreCase;

		// Token: 0x04002C47 RID: 11335
		private static readonly string[] forbiddenNameFragments = new string[]
		{
			","
		};

		// Token: 0x04002C48 RID: 11336
		private readonly Category.CategoryLastTimeUsed categoryLastTimeUsed;

		// Token: 0x04002C49 RID: 11337
		private readonly MemoryPropertyBag propertyBag;

		// Token: 0x04002C4A RID: 11338
		private bool isAbandoned;

		// Token: 0x04002C4B RID: 11339
		private MasterCategoryList masterCategoryList;

		// Token: 0x0200086D RID: 2157
		internal sealed class CategoryLastTimeUsed
		{
			// Token: 0x06005136 RID: 20790 RVA: 0x0015214C File Offset: 0x0015034C
			internal CategoryLastTimeUsed(Category category)
			{
				this.category = category;
			}

			// Token: 0x170016BD RID: 5821
			internal ExDateTime this[OutlookModule module]
			{
				get
				{
					this.category.CheckAbandoned("CategoryLastTimeUsed::this::get");
					return this.category.propertyBag.GetValueOrDefault<ExDateTime>(Category.CategoryLastTimeUsed.outlookModuleToLtuPropDef[module], ExDateTime.MinValue);
				}
				set
				{
					this.category.CheckAbandoned("CategoryLastTimeUsed::this::set");
					if (value.TimeZone != ExTimeZone.UtcTimeZone)
					{
						throw new ArgumentException("CategoryLastTimeUsed operates only on UTC date/times");
					}
					ExDateTime exDateTime = this[module];
					this.category.propertyBag[Category.CategoryLastTimeUsed.outlookModuleToLtuPropDef[module]] = ((value > exDateTime) ? value : exDateTime);
				}
			}

			// Token: 0x06005139 RID: 20793 RVA: 0x001521FC File Offset: 0x001503FC
			internal void SetForAllModules(ExDateTime value)
			{
				foreach (PropertyDefinition propertyDefinition in Category.CategoryLastTimeUsed.outlookModuleToLtuPropDef.Values)
				{
					this.category.propertyBag[propertyDefinition] = value;
				}
			}

			// Token: 0x04002C4C RID: 11340
			private static readonly Dictionary<OutlookModule, StorePropertyDefinition> outlookModuleToLtuPropDef = Util.AddElements<Dictionary<OutlookModule, StorePropertyDefinition>, KeyValuePair<OutlookModule, StorePropertyDefinition>>(new Dictionary<OutlookModule, StorePropertyDefinition>(), new KeyValuePair<OutlookModule, StorePropertyDefinition>[]
			{
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.Calendar, CategorySchema.LastTimeUsedCalendar),
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.Contacts, CategorySchema.LastTimeUsedContacts),
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.Journal, CategorySchema.LastTimeUsedJournal),
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.Mail, CategorySchema.LastTimeUsedMail),
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.Notes, CategorySchema.LastTimeUsedNotes),
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.Tasks, CategorySchema.LastTimeUsedTasks),
				Util.Pair<OutlookModule, StorePropertyDefinition>(OutlookModule.None, CategorySchema.LastTimeUsed)
			});

			// Token: 0x04002C4D RID: 11341
			private readonly Category category;
		}
	}
}
