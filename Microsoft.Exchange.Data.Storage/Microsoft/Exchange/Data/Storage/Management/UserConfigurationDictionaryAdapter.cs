using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B1 RID: 2481
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserConfigurationDictionaryAdapter<TObject> : MailboxManagementDataAdapter<TObject> where TObject : UserConfigurationObject, new()
	{
		// Token: 0x06005B89 RID: 23433 RVA: 0x0017DBD0 File Offset: 0x0017BDD0
		public UserConfigurationDictionaryAdapter(MailboxSession session, string configuration, GetUserConfigurationDelegate configurationGetter, SimplePropertyDefinition[] appliedProperties) : this(session, configuration, SaveMode.NoConflictResolution, configurationGetter, appliedProperties)
		{
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0017DBDE File Offset: 0x0017BDDE
		public UserConfigurationDictionaryAdapter(MailboxSession session, string configuration, SaveMode saveMode, GetUserConfigurationDelegate configurationGetter, SimplePropertyDefinition[] appliedProperties) : base(session, configuration)
		{
			if (string.IsNullOrEmpty(configuration))
			{
				throw new ArgumentNullException("configuration");
			}
			this.ConfigurationGetter = configurationGetter;
			this.AppliedProperties = new List<SimplePropertyDefinition>(appliedProperties);
			this.saveMode = saveMode;
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06005B8B RID: 23435 RVA: 0x0017DC17 File Offset: 0x0017BE17
		// (set) Token: 0x06005B8C RID: 23436 RVA: 0x0017DC1F File Offset: 0x0017BE1F
		private List<SimplePropertyDefinition> AppliedProperties { get; set; }

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06005B8D RID: 23437 RVA: 0x0017DC28 File Offset: 0x0017BE28
		// (set) Token: 0x06005B8E RID: 23438 RVA: 0x0017DC30 File Offset: 0x0017BE30
		private GetUserConfigurationDelegate ConfigurationGetter { get; set; }

		// Token: 0x06005B8F RID: 23439 RVA: 0x0017DC54 File Offset: 0x0017BE54
		protected override void InternalFill(TObject configObject)
		{
			base.InternalFill(configObject);
			UserConfigurationDictionaryHelper.Fill(configObject, this.AppliedProperties.ToArray(), (bool createIfNonexisting) => this.ConfigurationGetter(base.Session, base.Configuration, UserConfigurationTypes.Dictionary, createIfNonexisting));
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0017DC9B File Offset: 0x0017BE9B
		protected override void InternalSave(TObject configObject)
		{
			base.InternalSave(configObject);
			UserConfigurationDictionaryHelper.Save(configObject, this.saveMode, this.AppliedProperties.ToArray(), (bool createIfNonexisting) => this.ConfigurationGetter(base.Session, base.Configuration, UserConfigurationTypes.Dictionary, createIfNonexisting));
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0017DCCC File Offset: 0x0017BECC
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UserConfigurationDictionaryAdapter<TObject>>(this);
		}

		// Token: 0x0400326A RID: 12906
		private readonly SaveMode saveMode;
	}
}
