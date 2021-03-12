using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E96 RID: 3734
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExBindingStoreObject : EwsStoreObject
	{
		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x060081D8 RID: 33240 RVA: 0x00237A7D File Offset: 0x00235C7D
		// (set) Token: 0x060081D9 RID: 33241 RVA: 0x00237A8F File Offset: 0x00235C8F
		public string Name
		{
			get
			{
				return (string)this[ExBindingStoreObjectSchema.Name];
			}
			set
			{
				this[ExBindingStoreObjectSchema.Name] = value;
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x060081DA RID: 33242 RVA: 0x00237A9D File Offset: 0x00235C9D
		// (set) Token: 0x060081DB RID: 33243 RVA: 0x00237AB4 File Offset: 0x00235CB4
		public Guid MasterIdentity
		{
			get
			{
				return Guid.Parse((string)this[ExBindingStoreObjectSchema.MasterIdentity]);
			}
			set
			{
				this[ExBindingStoreObjectSchema.MasterIdentity] = value.ToString();
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x060081DC RID: 33244 RVA: 0x00237ACE File Offset: 0x00235CCE
		// (set) Token: 0x060081DD RID: 33245 RVA: 0x00237ADB File Offset: 0x00235CDB
		public Guid PolicyId
		{
			get
			{
				return Guid.Parse(base.AlternativeId);
			}
			set
			{
				base.AlternativeId = value.ToString();
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x060081DE RID: 33246 RVA: 0x00237AF0 File Offset: 0x00235CF0
		// (set) Token: 0x060081DF RID: 33247 RVA: 0x00237B02 File Offset: 0x00235D02
		public Workload Workload
		{
			get
			{
				return (Workload)this[ExBindingStoreObjectSchema.Workload];
			}
			set
			{
				this[ExBindingStoreObjectSchema.Workload] = value;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x060081E0 RID: 33248 RVA: 0x00237B15 File Offset: 0x00235D15
		// (set) Token: 0x060081E1 RID: 33249 RVA: 0x00237B27 File Offset: 0x00235D27
		public Guid PolicyVersion
		{
			get
			{
				return (Guid)this[ExBindingStoreObjectSchema.PolicyVersion];
			}
			set
			{
				this[ExBindingStoreObjectSchema.PolicyVersion] = value;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x060081E2 RID: 33250 RVA: 0x00237B3A File Offset: 0x00235D3A
		public DateTime? WhenCreated
		{
			get
			{
				return (DateTime?)this[ExBindingStoreObjectSchema.WhenCreated];
			}
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x060081E3 RID: 33251 RVA: 0x00237B4C File Offset: 0x00235D4C
		public DateTime? WhenChanged
		{
			get
			{
				return (DateTime?)this[ExBindingStoreObjectSchema.WhenChanged];
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x060081E4 RID: 33252 RVA: 0x00237B5E File Offset: 0x00235D5E
		// (set) Token: 0x060081E5 RID: 33253 RVA: 0x00237B70 File Offset: 0x00235D70
		internal MultiValuedProperty<string> RawAppliedScopes
		{
			get
			{
				return (MultiValuedProperty<string>)this[ExBindingStoreObjectSchema.RawAppliedScopes];
			}
			set
			{
				this[ExBindingStoreObjectSchema.RawAppliedScopes] = value;
			}
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x060081E6 RID: 33254 RVA: 0x00237B7E File Offset: 0x00235D7E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x060081E7 RID: 33255 RVA: 0x00237B85 File Offset: 0x00235D85
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ExBindingStoreObject.schema;
			}
		}

		// Token: 0x060081E8 RID: 33256 RVA: 0x00237B8C File Offset: 0x00235D8C
		public void FromBindingStorage(BindingStorage bindingStorage, ExPolicyConfigProvider policyConfigProvider)
		{
			ArgumentValidator.ThrowIfNull("bindingStorage", bindingStorage);
			ArgumentValidator.ThrowIfNull("policyConfigProvider", policyConfigProvider);
			this.Name = bindingStorage.Name;
			this.MasterIdentity = bindingStorage.MasterIdentity;
			this.PolicyId = bindingStorage.PolicyId;
			this.Workload = bindingStorage.Workload;
			this.PolicyVersion = bindingStorage.PolicyVersion;
			this.RawAppliedScopes.Clear();
			foreach (ScopeStorage scopeStorage in bindingStorage.AppliedScopes)
			{
				string item = ExBindingStoreObject.ScopeStorageToString(scopeStorage);
				this.RawAppliedScopes.Add(item);
			}
		}

		// Token: 0x060081E9 RID: 33257 RVA: 0x00237C48 File Offset: 0x00235E48
		public BindingStorage ToBindingStorage(ExPolicyConfigProvider policyConfigProvider)
		{
			ArgumentValidator.ThrowIfNull("policyConfigProvider", policyConfigProvider);
			BindingStorage bindingStorage = new BindingStorage();
			bindingStorage.SetId(PolicyStorage.PoliciesContainer.GetChildId(this.Name));
			bindingStorage.Name = this.Name;
			bindingStorage.MasterIdentity = this.MasterIdentity;
			bindingStorage.PolicyId = this.PolicyId;
			bindingStorage.Workload = this.Workload;
			bindingStorage.PolicyVersion = this.PolicyVersion;
			foreach (string scopeStorageString in this.RawAppliedScopes)
			{
				ScopeStorage scopeStorage = ExBindingStoreObject.ScopeStorageFromString(scopeStorageString, policyConfigProvider);
				if (scopeStorage != null)
				{
					bindingStorage.AppliedScopes.Add(scopeStorage);
				}
			}
			if (this.WhenChanged != null)
			{
				bindingStorage.propertyBag.SetField(ADObjectSchema.WhenChangedRaw, ADValueConvertor.ConvertValueToString(this.WhenChanged.Value, null));
			}
			if (this.WhenCreated != null)
			{
				bindingStorage.propertyBag.SetField(ADObjectSchema.WhenCreatedRaw, ADValueConvertor.ConvertValueToString(this.WhenCreated.Value, null));
			}
			bindingStorage.ResetChangeTracking(true);
			bindingStorage.RawObject = this;
			return bindingStorage;
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x00237D98 File Offset: 0x00235F98
		private static string ScopeStorageToString(ScopeStorage scopeStorage)
		{
			return string.Format("{0},{1},{2},{3},{4}", new object[]
			{
				scopeStorage.MasterIdentity,
				scopeStorage.Mode,
				scopeStorage.PolicyVersion,
				scopeStorage.Name.Replace(',', ' '),
				scopeStorage.Scope
			});
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x00237E00 File Offset: 0x00236000
		private static ScopeStorage ScopeStorageFromString(string scopeStorageString, ExPolicyConfigProvider policyConfigProvider)
		{
			if (!string.IsNullOrEmpty(scopeStorageString))
			{
				Guid empty = Guid.Empty;
				Mode mode = Mode.Enforce;
				Guid empty2 = Guid.Empty;
				string[] array = scopeStorageString.Split(new char[]
				{
					','
				}, 5);
				if (array.Length == 5 && Guid.TryParse(array[0], out empty) && Enum.TryParse<Mode>(array[1], out mode) && Guid.TryParse(array[2], out empty2) && !string.IsNullOrEmpty(array[3]) && !string.IsNullOrEmpty(array[4]))
				{
					ScopeStorage scopeStorage = new ScopeStorage();
					scopeStorage.SetId(PolicyStorage.PoliciesContainer.GetChildId(empty.ToString()));
					scopeStorage.Name = array[3];
					scopeStorage.MasterIdentity = empty;
					scopeStorage.PolicyVersion = empty2;
					scopeStorage.Mode = mode;
					scopeStorage.Scope = array[4];
					scopeStorage.ResetChangeTracking(true);
					return scopeStorage;
				}
			}
			policyConfigProvider.LogOneEntry(ExecutionLog.EventType.CriticalError, "Convert string to scope storage", new FormatException(string.Format("'{0}' is not valid for scope storage.", scopeStorageString)));
			return null;
		}

		// Token: 0x04005725 RID: 22309
		private static readonly ExBindingStoreObjectSchema schema = new ExBindingStoreObjectSchema();
	}
}
