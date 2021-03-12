using System;
using System.Collections;
using System.Collections.Specialized;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000091 RID: 145
	public abstract class PolicyConfigBase
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public virtual Guid Identity
		{
			get
			{
				object obj = this[PolicyConfigBaseSchema.Identity];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this[PolicyConfigBaseSchema.Identity] = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000BB03 File Offset: 0x00009D03
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000BB15 File Offset: 0x00009D15
		public virtual string Name
		{
			get
			{
				return (string)this[PolicyConfigBaseSchema.Name];
			}
			set
			{
				this[PolicyConfigBaseSchema.Name] = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000BB24 File Offset: 0x00009D24
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000BB48 File Offset: 0x00009D48
		public virtual Workload Workload
		{
			get
			{
				object obj = this[PolicyConfigBaseSchema.Workload];
				if (obj != null)
				{
					return (Workload)obj;
				}
				return Workload.None;
			}
			set
			{
				this[PolicyConfigBaseSchema.Workload] = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000BB5B File Offset: 0x00009D5B
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000BB6D File Offset: 0x00009D6D
		public virtual DateTime? WhenCreatedUTC
		{
			get
			{
				return (DateTime?)this[PolicyConfigBaseSchema.WhenCreatedUTC];
			}
			set
			{
				this[PolicyConfigBaseSchema.WhenCreatedUTC] = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000BB80 File Offset: 0x00009D80
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000BB92 File Offset: 0x00009D92
		public virtual DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this[PolicyConfigBaseSchema.WhenChangedUTC];
			}
			set
			{
				this[PolicyConfigBaseSchema.WhenChangedUTC] = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public ChangeType ObjectState
		{
			get
			{
				object obj = this[PolicyConfigBaseSchema.ObjectState];
				if (obj != null)
				{
					return (ChangeType)obj;
				}
				return ChangeType.Add;
			}
			private set
			{
				this[PolicyConfigBaseSchema.ObjectState] = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000BBDF File Offset: 0x00009DDF
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000BBF1 File Offset: 0x00009DF1
		public PolicyVersion Version
		{
			get
			{
				return (PolicyVersion)this[PolicyConfigBaseSchema.Version];
			}
			set
			{
				this[PolicyConfigBaseSchema.Version] = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000BBFF File Offset: 0x00009DFF
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000BC07 File Offset: 0x00009E07
		internal object RawObject { get; set; }

		// Token: 0x170000F9 RID: 249
		protected object this[string key]
		{
			get
			{
				PolicyConfigBase.ChangeTrackingField changeTrackingField = (PolicyConfigBase.ChangeTrackingField)this.changeTrackingFields[key];
				if (changeTrackingField == null)
				{
					return null;
				}
				return changeTrackingField.Data;
			}
			set
			{
				PolicyConfigBase.ChangeTrackingField changeTrackingField = (PolicyConfigBase.ChangeTrackingField)this.changeTrackingFields[key];
				if (changeTrackingField == null)
				{
					changeTrackingField = new PolicyConfigBase.ChangeTrackingField();
					this.changeTrackingFields[key] = changeTrackingField;
				}
				changeTrackingField.Data = value;
				if (!PolicyConfigBaseSchema.ObjectState.Equals(key) && this.ObjectState == ChangeType.None)
				{
					this.ObjectState = ChangeType.Update;
				}
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000BC94 File Offset: 0x00009E94
		public virtual void ResetChangeTracking()
		{
			this.ObjectState = ChangeType.None;
			foreach (object obj in this.changeTrackingFields.Values)
			{
				((PolicyConfigBase.ChangeTrackingField)obj).ResetChangeTracking();
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public virtual void MarkAsDeleted()
		{
			this.ObjectState = ChangeType.Delete;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000BD01 File Offset: 0x00009F01
		public virtual void MarkAsUpdated()
		{
			this.ObjectState = ChangeType.Update;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public bool IsModified(string key)
		{
			ArgumentValidator.ThrowIfNull("key", key);
			PolicyConfigBase.ChangeTrackingField changeTrackingField = (PolicyConfigBase.ChangeTrackingField)this.changeTrackingFields[key];
			return changeTrackingField != null && changeTrackingField.IsModified;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000BD41 File Offset: 0x00009F41
		public virtual void Validate()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				throw new ArgumentException("Name can't be null or empty");
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000BD5C File Offset: 0x00009F5C
		internal PolicyConfigBase Clone()
		{
			PolicyConfigBase policyConfigBase = (PolicyConfigBase)base.MemberwiseClone();
			policyConfigBase.changeTrackingFields = new HybridDictionary();
			foreach (object obj in this.changeTrackingFields)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				policyConfigBase.changeTrackingFields[dictionaryEntry.Key] = ((PolicyConfigBase.ChangeTrackingField)this.changeTrackingFields[dictionaryEntry.Key]).Clone();
			}
			return policyConfigBase;
		}

		// Token: 0x04000273 RID: 627
		private HybridDictionary changeTrackingFields = new HybridDictionary();

		// Token: 0x02000092 RID: 146
		private class ChangeTrackingField
		{
			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000394 RID: 916 RVA: 0x0000BE07 File Offset: 0x0000A007
			// (set) Token: 0x06000395 RID: 917 RVA: 0x0000BE0F File Offset: 0x0000A00F
			public bool IsModified { get; private set; }

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000396 RID: 918 RVA: 0x0000BE18 File Offset: 0x0000A018
			// (set) Token: 0x06000397 RID: 919 RVA: 0x0000BE20 File Offset: 0x0000A020
			public object Data
			{
				get
				{
					return this.data;
				}
				set
				{
					this.data = value;
					this.IsModified = true;
				}
			}

			// Token: 0x06000398 RID: 920 RVA: 0x0000BE30 File Offset: 0x0000A030
			public void ResetChangeTracking()
			{
				this.IsModified = false;
			}

			// Token: 0x06000399 RID: 921 RVA: 0x0000BE39 File Offset: 0x0000A039
			public PolicyConfigBase.ChangeTrackingField Clone()
			{
				return (PolicyConfigBase.ChangeTrackingField)base.MemberwiseClone();
			}

			// Token: 0x04000275 RID: 629
			private object data;
		}
	}
}
