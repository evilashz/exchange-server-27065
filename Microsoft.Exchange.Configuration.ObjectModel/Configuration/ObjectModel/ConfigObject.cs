using System;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public abstract class ConfigObject : IConfigurable
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004A40 File Offset: 0x00002C40
		public ConfigObject(PropertyBag propertyBag)
		{
			ExTraceGlobals.ConfigObjectTracer.Information<string>((long)this.GetHashCode(), "ConfigObject::ConfigObject - initializing ConfigObject with{0} property bag.", (propertyBag == null) ? "out" : "");
			if (propertyBag == null)
			{
				this.fields = new PropertyBag();
				this.isNew = true;
				this.Fields["Identity"] = Guid.NewGuid().ToString();
				this.InitializeDefaults();
				this.Fields.ResetChangeTracking();
				return;
			}
			this.fields = propertyBag;
			this.isNew = false;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public ConfigObject() : this(null)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004AD9 File Offset: 0x00002CD9
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public virtual string Identity
		{
			get
			{
				return (string)this.Fields["Identity"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Identity cannot be null or empty");
				}
				this.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		public abstract string Name { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004B16 File Offset: 0x00002D16
		public DataSourceInfo DataSourceInfo
		{
			get
			{
				return this.dataSourceInfo;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004B1E File Offset: 0x00002D1E
		public bool IsDeleted
		{
			get
			{
				return this.isDeleted;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004B26 File Offset: 0x00002D26
		public bool IsNew
		{
			get
			{
				return this.isNew;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004B2E File Offset: 0x00002D2E
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004B36 File Offset: 0x00002D36
		public virtual PropertyBag Fields
		{
			get
			{
				return this.fields;
			}
			set
			{
				this.fields = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004B3F File Offset: 0x00002D3F
		ObjectId IConfigurable.Identity
		{
			get
			{
				return new ConfigObjectId(this.Identity);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004B4C File Offset: 0x00002D4C
		bool IConfigurable.IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004B54 File Offset: 0x00002D54
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				if (this.IsNew)
				{
					return ObjectState.New;
				}
				if (this.IsDeleted)
				{
					return ObjectState.Deleted;
				}
				if (this.fields != null && this.fields.FieldDictionary != null)
				{
					foreach (object obj in this.fields.FieldDictionary.Values)
					{
						Field field = (Field)obj;
						if (field.IsChanged)
						{
							return ObjectState.Changed;
						}
					}
					return ObjectState.Unchanged;
				}
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public virtual void InitializeDefaults()
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004BEA File Offset: 0x00002DEA
		public virtual void Validate()
		{
			this.isValid = true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public void SetDataSourceInfo(DataSourceInfo dsi)
		{
			this.dataSourceInfo = dsi;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004BFC File Offset: 0x00002DFC
		public void CopyChangesFrom(ConfigObject changedObject)
		{
			if (changedObject == null)
			{
				throw new ArgumentNullException("changedObject");
			}
			if (changedObject.Fields == null)
			{
				throw new ArgumentNullException("changedObject.Fields");
			}
			foreach (object key in changedObject.Fields.Keys)
			{
				if (changedObject.Fields.IsModified(key))
				{
					this.Fields[key] = changedObject.Fields[key];
				}
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C98 File Offset: 0x00002E98
		public void ResetChangeTracking()
		{
			this.Fields.ResetChangeTracking();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004CA5 File Offset: 0x00002EA5
		ValidationError[] IConfigurable.Validate()
		{
			this.Validate();
			return new ValidationError[0];
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004CB3 File Offset: 0x00002EB3
		void IConfigurable.CopyChangesFrom(IConfigurable changedObject)
		{
			this.CopyChangesFrom((ConfigObject)changedObject);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004CC1 File Offset: 0x00002EC1
		internal void SetIsDeleted(bool isDeleted)
		{
			this.isDeleted = isDeleted;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004CCA File Offset: 0x00002ECA
		internal void SetIsNew(bool isNew)
		{
			this.isNew = isNew;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004CD4 File Offset: 0x00002ED4
		internal void RestoreFrom(PropertyBag oldPropertyBag)
		{
			foreach (object key in this.Fields.Keys)
			{
				if (this.Fields.IsModified(key))
				{
					this.Fields[key] = oldPropertyBag[key];
				}
			}
		}

		// Token: 0x0400004C RID: 76
		private bool isDeleted;

		// Token: 0x0400004D RID: 77
		private bool isNew;

		// Token: 0x0400004E RID: 78
		private bool isValid;

		// Token: 0x0400004F RID: 79
		private PropertyBag fields;

		// Token: 0x04000050 RID: 80
		private DataSourceInfo dataSourceInfo;
	}
}
