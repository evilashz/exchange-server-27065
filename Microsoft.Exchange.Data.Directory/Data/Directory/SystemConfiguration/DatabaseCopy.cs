using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D9 RID: 985
	[Serializable]
	public class DatabaseCopy : ADConfigurationObject, IActivationPreferenceSettable<DatabaseCopy>, IComparable<DatabaseCopy>, IProvisioningCacheInvalidation
	{
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000B9902 File Offset: 0x000B7B02
		internal override ADObjectSchema Schema
		{
			get
			{
				return DatabaseCopy.schema;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000B9909 File Offset: 0x000B7B09
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DatabaseCopy.mostDerivedClass;
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000B9910 File Offset: 0x000B7B10
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (string.IsNullOrEmpty(this.HostServerName))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorHostServerNotSet, DatabaseCopySchema.HostServerName, this));
			}
			if (!this.IsHostServerValidForObjectValidation)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorHostServerNotSet, DatabaseCopySchema.HostServer, this));
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000B9965 File Offset: 0x000B7B65
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!this.IsHostServerValidForObjectValidation)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorHostServerNotSet, DatabaseCopySchema.HostServer, this));
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000B998C File Offset: 0x000B7B8C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x000B9993 File Offset: 0x000B7B93
		public string DatabaseName
		{
			get
			{
				return (string)this[DatabaseCopySchema.DatabaseName];
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000B99A5 File Offset: 0x000B7BA5
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x000B99B7 File Offset: 0x000B7BB7
		internal new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
			set
			{
				this[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000B99C5 File Offset: 0x000B7BC5
		public string HostServerName
		{
			get
			{
				return (string)this[DatabaseCopySchema.HostServerName];
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000B99D7 File Offset: 0x000B7BD7
		// (set) Token: 0x06002D25 RID: 11557 RVA: 0x000B99E9 File Offset: 0x000B7BE9
		internal int ActivationPreferenceInternal
		{
			get
			{
				return (int)this[DatabaseCopySchema.ActivationPreference];
			}
			set
			{
				this[DatabaseCopySchema.ActivationPreference] = value;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x000B99FC File Offset: 0x000B7BFC
		// (set) Token: 0x06002D27 RID: 11559 RVA: 0x000B9A04 File Offset: 0x000B7C04
		public int ActivationPreference
		{
			get
			{
				return this.activationPreference;
			}
			internal set
			{
				this.activationPreference = value;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000B9A0D File Offset: 0x000B7C0D
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x000B9A1F File Offset: 0x000B7C1F
		internal ADObjectId HostServer
		{
			get
			{
				return (ADObjectId)this[DatabaseCopySchema.HostServer];
			}
			set
			{
				this[DatabaseCopySchema.HostServer] = value;
				this.HostServerUnlinked = (value == null);
			}
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000B9A37 File Offset: 0x000B7C37
		internal bool IsValidDatabaseCopy(bool allowInvalid)
		{
			return allowInvalid || this.IsValid;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000B9A44 File Offset: 0x000B7C44
		internal bool IsValidForRead
		{
			get
			{
				return base.ValidateRead().Length == 0;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000B9A51 File Offset: 0x000B7C51
		internal bool IsHostServerPresent
		{
			get
			{
				return this.HostServer != null && !this.HostServer.IsDeleted;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000B9A6B File Offset: 0x000B7C6B
		internal bool IsHostServerValid
		{
			get
			{
				return this.IsHostServerPresent || this.HostServerUnlinked;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000B9A7D File Offset: 0x000B7C7D
		private bool IsHostServerValidForObjectValidation
		{
			get
			{
				return this.IsHostServerPresent || this.InvalidHostServerAllowed;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000B9A8F File Offset: 0x000B7C8F
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x000B9AA1 File Offset: 0x000B7CA1
		public string ParentObjectClass
		{
			get
			{
				return (string)this[DatabaseCopySchema.ParentObjectClass];
			}
			internal set
			{
				this[DatabaseCopySchema.ParentObjectClass] = value;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000B9AAF File Offset: 0x000B7CAF
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x000B9AC1 File Offset: 0x000B7CC1
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReplayLagTime
		{
			get
			{
				return (EnhancedTimeSpan)this[DatabaseCopySchema.ReplayLag];
			}
			set
			{
				this[DatabaseCopySchema.ReplayLag] = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000B9AD4 File Offset: 0x000B7CD4
		// (set) Token: 0x06002D34 RID: 11572 RVA: 0x000B9AE6 File Offset: 0x000B7CE6
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TruncationLagTime
		{
			get
			{
				return (EnhancedTimeSpan)this[DatabaseCopySchema.TruncationLag];
			}
			set
			{
				this[DatabaseCopySchema.TruncationLag] = value;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000B9AF9 File Offset: 0x000B7CF9
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x000B9B0B File Offset: 0x000B7D0B
		[Parameter(Mandatory = false)]
		public DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
		{
			get
			{
				return (DatabaseCopyAutoActivationPolicyType)this[DatabaseCopySchema.DatabaseCopyAutoActivationPolicy];
			}
			set
			{
				this[DatabaseCopySchema.DatabaseCopyAutoActivationPolicy] = value;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x000B9B1E File Offset: 0x000B7D1E
		// (set) Token: 0x06002D38 RID: 11576 RVA: 0x000B9B30 File Offset: 0x000B7D30
		internal DatabaseCopyAutoDagFlags AutoDagFlags
		{
			get
			{
				return (DatabaseCopyAutoDagFlags)this[DatabaseCopySchema.AutoDagFlags];
			}
			set
			{
				this[DatabaseCopySchema.AutoDagFlags] = value;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000B9B43 File Offset: 0x000B7D43
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000B9B55 File Offset: 0x000B7D55
		internal bool InvalidHostServerAllowed
		{
			get
			{
				return (bool)this[DatabaseCopySchema.InvalidHostServerAllowed];
			}
			set
			{
				this[DatabaseCopySchema.InvalidHostServerAllowed] = value;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000B9B68 File Offset: 0x000B7D68
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x000B9B7A File Offset: 0x000B7D7A
		internal bool HostServerUnlinked
		{
			get
			{
				return (bool)this[DatabaseCopySchema.HostServerUnlinked];
			}
			private set
			{
				this[DatabaseCopySchema.HostServerUnlinked] = value;
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000B9B8D File Offset: 0x000B7D8D
		internal TDatabase GetDatabase<TDatabase>() where TDatabase : IConfigurable, new()
		{
			return (TDatabase)((object)base.Session.Read<TDatabase>(((ADObjectId)this.Identity).Parent));
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000B9BB0 File Offset: 0x000B7DB0
		internal DatabaseCopy[] GetAllDatabaseCopies()
		{
			Database database = this.GetDatabase<Database>();
			DatabaseCopy[] result = null;
			if (database != null)
			{
				result = database.GetDatabaseCopies();
			}
			return result;
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000B9BD1 File Offset: 0x000B7DD1
		public override string ToString()
		{
			return this.Identity.ToString();
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000B9BE0 File Offset: 0x000B7DE0
		internal static object DatabaseNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId parent = ((ADObjectId)propertyBag[ADObjectSchema.Id]).Parent;
			if (parent == null)
			{
				return DatabaseCopySchema.DatabaseName.DefaultValue;
			}
			return parent.Name;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000B9C18 File Offset: 0x000B7E18
		internal static object HostServerNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[DatabaseCopySchema.HostServer];
			string result;
			if (adobjectId == null)
			{
				result = (string)propertyBag[ADObjectSchema.Name];
			}
			else
			{
				result = adobjectId.Name;
			}
			return result;
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000B9C58 File Offset: 0x000B7E58
		public int CompareTo(DatabaseCopy other)
		{
			if (other == null)
			{
				return -1;
			}
			return this.ActivationPreferenceInternal.CompareTo(other.ActivationPreferenceInternal);
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000B9C7E File Offset: 0x000B7E7E
		// (set) Token: 0x06002D44 RID: 11588 RVA: 0x000B9C86 File Offset: 0x000B7E86
		int IActivationPreferenceSettable<DatabaseCopy>.ActualValue
		{
			get
			{
				return this.ActivationPreferenceInternal;
			}
			set
			{
				this.ActivationPreferenceInternal = value;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000B9C8F File Offset: 0x000B7E8F
		int IActivationPreferenceSettable<DatabaseCopy>.DesiredValue
		{
			get
			{
				return this.ActivationPreference;
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000B9C97 File Offset: 0x000B7E97
		bool IActivationPreferenceSettable<DatabaseCopy>.Matches(DatabaseCopy other)
		{
			return base.Guid == other.Guid;
		}

		// Token: 0x17000C9D RID: 3229
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x000B9CAA File Offset: 0x000B7EAA
		bool IActivationPreferenceSettable<DatabaseCopy>.InvalidHostServerAllowed
		{
			set
			{
				this.InvalidHostServerAllowed = value;
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000B9CB4 File Offset: 0x000B7EB4
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (base.ObjectState == ObjectState.New || base.ObjectState == ObjectState.Deleted || (base.ObjectState == ObjectState.Changed && (base.IsChanged(DatabaseCopySchema.HostServer) || base.IsChanged(DatabaseCopySchema.HostServerName))))
			{
				keys = new Guid[]
				{
					CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnLocalSite
				};
				return true;
			}
			return false;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000B9D19 File Offset: 0x000B7F19
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x04001E51 RID: 7761
		internal const string MaxTimeSpanStr = "14.00:00:00";

		// Token: 0x04001E52 RID: 7762
		internal const string DefaultReplayLagTimeStr = "00:00:00";

		// Token: 0x04001E53 RID: 7763
		internal const string DefaultTruncationLagTimeStr = "00:00:00";

		// Token: 0x04001E54 RID: 7764
		private static DatabaseCopySchema schema = ObjectSchema.GetInstance<DatabaseCopySchema>();

		// Token: 0x04001E55 RID: 7765
		private static string mostDerivedClass = "msExchMDBCopy";

		// Token: 0x04001E56 RID: 7766
		private int activationPreference;
	}
}
