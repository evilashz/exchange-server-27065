using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B2 RID: 2482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserConfigurationXmlAdapter<TObject> : MailboxManagementDataAdapter<TObject> where TObject : UserConfigurationObject, new()
	{
		// Token: 0x06005B94 RID: 23444 RVA: 0x0017DCD4 File Offset: 0x0017BED4
		public UserConfigurationXmlAdapter(MailboxSession session, string configuration, GetUserConfigurationDelegate configurationGetter, SimplePropertyDefinition property) : this(session, configuration, SaveMode.NoConflictResolution, configurationGetter, property)
		{
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x0017DCE2 File Offset: 0x0017BEE2
		public UserConfigurationXmlAdapter(MailboxSession session, string configuration, SaveMode saveMode, GetUserConfigurationDelegate configurationGetter, SimplePropertyDefinition property) : this(session, configuration, saveMode, configurationGetter, null, property)
		{
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0017DCF4 File Offset: 0x0017BEF4
		public UserConfigurationXmlAdapter(MailboxSession session, string configuration, SaveMode saveMode, GetUserConfigurationDelegate configurationGetter, GetReadableUserConfigurationDelegate readConfigurationGetter, SimplePropertyDefinition property) : base(session, configuration)
		{
			if (string.IsNullOrEmpty(configuration))
			{
				throw new ArgumentNullException("configuration");
			}
			this.ConfigurationGetter = configurationGetter;
			this.ReadableConfigurationGetter = (readConfigurationGetter ?? new GetReadableUserConfigurationDelegate(this.ReadableConfigurationFromWriteableDelegate));
			this.Property = property;
			this.saveMode = saveMode;
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06005B97 RID: 23447 RVA: 0x0017DD4B File Offset: 0x0017BF4B
		// (set) Token: 0x06005B98 RID: 23448 RVA: 0x0017DD53 File Offset: 0x0017BF53
		private SimplePropertyDefinition Property { get; set; }

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06005B99 RID: 23449 RVA: 0x0017DD5C File Offset: 0x0017BF5C
		// (set) Token: 0x06005B9A RID: 23450 RVA: 0x0017DD64 File Offset: 0x0017BF64
		private GetUserConfigurationDelegate ConfigurationGetter { get; set; }

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x0017DD6D File Offset: 0x0017BF6D
		// (set) Token: 0x06005B9C RID: 23452 RVA: 0x0017DD75 File Offset: 0x0017BF75
		private GetReadableUserConfigurationDelegate ReadableConfigurationGetter { get; set; }

		// Token: 0x06005B9D RID: 23453 RVA: 0x0017DD99 File Offset: 0x0017BF99
		protected override void InternalFill(TObject configObject)
		{
			base.InternalFill(configObject);
			UserConfigurationXmlHelper.Fill(configObject, this.Property, (bool createIfNonexisting) => this.ReadableConfigurationGetter(base.Session, base.Configuration, UserConfigurationTypes.XML, createIfNonexisting));
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x0017DDDB File Offset: 0x0017BFDB
		protected override void InternalSave(TObject configObject)
		{
			base.InternalSave(configObject);
			UserConfigurationXmlHelper.Save(configObject, this.saveMode, this.Property, (bool createIfNonexisting) => this.ConfigurationGetter(base.Session, base.Configuration, UserConfigurationTypes.XML, createIfNonexisting));
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x0017DE07 File Offset: 0x0017C007
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UserConfigurationXmlAdapter<TObject>>(this);
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x0017DE0F File Offset: 0x0017C00F
		private IReadableUserConfiguration ReadableConfigurationFromWriteableDelegate(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonExisting)
		{
			return this.ConfigurationGetter(session, configuration, type, createIfNonExisting);
		}

		// Token: 0x0400326D RID: 12909
		private readonly SaveMode saveMode;
	}
}
