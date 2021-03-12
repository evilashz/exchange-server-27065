using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002AE RID: 686
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UserConfigurationDescriptor
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x00084540 File Offset: 0x00082740
		private UserConfigurationDescriptor(string configName, UserConfigurationTypes configurationTypes)
		{
			this.configurationName = configName;
			this.configurationTypes = configurationTypes;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00084556 File Offset: 0x00082756
		public string ConfigurationName
		{
			get
			{
				return this.configurationName;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0008455E File Offset: 0x0008275E
		public UserConfigurationTypes Types
		{
			get
			{
				return this.configurationTypes;
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00084566 File Offset: 0x00082766
		public static UserConfigurationDescriptor CreateMailboxDescriptor(string configName, UserConfigurationTypes types)
		{
			return new UserConfigurationDescriptor.DefaultFolderDescriptor(configName, types, DefaultFolderType.Configuration);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00084571 File Offset: 0x00082771
		public static UserConfigurationDescriptor CreateDefaultFolderDescriptor(string configName, UserConfigurationTypes types, DefaultFolderType folderType)
		{
			return new UserConfigurationDescriptor.DefaultFolderDescriptor(configName, types, folderType);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0008457B File Offset: 0x0008277B
		public static UserConfigurationDescriptor CreateFolderDescriptor(string configName, UserConfigurationTypes types, StoreObjectId folderId)
		{
			return new UserConfigurationDescriptor.FolderIdDescriptor(configName, types, folderId);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00084585 File Offset: 0x00082785
		public static UserConfigurationDescriptor FromMemento(UserConfigurationDescriptor.MementoClass memento)
		{
			return new UserConfigurationDescriptor.FolderIdDescriptor(memento.ConfigurationName, memento.Types, memento.FolderId);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000845A0 File Offset: 0x000827A0
		public UserConfigurationDescriptor.MementoClass ToMemento(IMailboxSession session)
		{
			return new UserConfigurationDescriptor.MementoClass
			{
				ConfigurationName = this.ConfigurationName,
				Types = this.Types,
				FolderId = this.GetFolderId(session)
			};
		}

		// Token: 0x06001CA6 RID: 7334
		public abstract StoreObjectId GetFolderId(IMailboxSession session);

		// Token: 0x06001CA7 RID: 7335
		public abstract void Validate(IUserConfiguration config);

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000845DC File Offset: 0x000827DC
		public IUserConfiguration GetConfiguration(IUserConfigurationManager manager)
		{
			IUserConfiguration result = null;
			StoreObjectId folderId = this.GetFolderId(manager.MailboxSession);
			if (folderId != null)
			{
				result = manager.GetFolderConfiguration(this.ConfigurationName, this.Types, folderId);
			}
			return result;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00084610 File Offset: 0x00082810
		public IUserConfiguration Rebuild(IUserConfigurationManager manager)
		{
			IUserConfiguration result = null;
			StoreId folderId = this.GetFolderId(manager.MailboxSession);
			if (folderId != null)
			{
				manager.DeleteFolderConfigurations(folderId, new string[]
				{
					this.ConfigurationName
				});
				result = manager.CreateFolderConfiguration(this.ConfigurationName, this.Types, folderId);
			}
			return result;
		}

		// Token: 0x0400138A RID: 5002
		private readonly string configurationName;

		// Token: 0x0400138B RID: 5003
		private readonly UserConfigurationTypes configurationTypes;

		// Token: 0x020002AF RID: 687
		[DataContract]
		public class MementoClass : IEquatable<UserConfigurationDescriptor.MementoClass>
		{
			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0008465D File Offset: 0x0008285D
			// (set) Token: 0x06001CAB RID: 7339 RVA: 0x00084665 File Offset: 0x00082865
			[DataMember]
			public string ConfigurationName { get; set; }

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0008466E File Offset: 0x0008286E
			// (set) Token: 0x06001CAD RID: 7341 RVA: 0x00084676 File Offset: 0x00082876
			[DataMember]
			public StoreObjectId FolderId { get; set; }

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0008467F File Offset: 0x0008287F
			// (set) Token: 0x06001CAF RID: 7343 RVA: 0x00084687 File Offset: 0x00082887
			[DataMember]
			public UserConfigurationTypes Types { get; set; }

			// Token: 0x06001CB0 RID: 7344 RVA: 0x00084690 File Offset: 0x00082890
			public bool Equals(UserConfigurationDescriptor.MementoClass other)
			{
				return !object.ReferenceEquals(other, null) && ((this.ConfigurationName ?? string.Empty).Equals(other.ConfigurationName) && this.Types.Equals(other.Types)) && ((this.FolderId == null && other.FolderId == null) || this.FolderId.Equals(other.FolderId));
			}

			// Token: 0x06001CB1 RID: 7345 RVA: 0x00084708 File Offset: 0x00082908
			public bool IsSuperSetOf(UserConfigurationDescriptor.MementoClass other)
			{
				return !object.ReferenceEquals(other, null) && ((this.ConfigurationName ?? string.Empty).Equals(other.ConfigurationName) && ((this.FolderId == null && other.FolderId == null) || this.FolderId.Equals(other.FolderId))) && (this.Types & other.Types) == other.Types;
			}

			// Token: 0x06001CB2 RID: 7346 RVA: 0x00084776 File Offset: 0x00082976
			public override bool Equals(object obj)
			{
				return this.Equals(obj as UserConfigurationDescriptor.MementoClass);
			}

			// Token: 0x06001CB3 RID: 7347 RVA: 0x00084784 File Offset: 0x00082984
			public override int GetHashCode()
			{
				return (this.ConfigurationName ?? string.Empty).GetHashCode() ^ this.Types.GetHashCode() ^ ((this.FolderId == null) ? 0 : this.FolderId.GetHashCode());
			}
		}

		// Token: 0x020002B0 RID: 688
		public class UserConfigurationDescriptorEqualityComparer : IEqualityComparer<UserConfigurationDescriptor>
		{
			// Token: 0x06001CB5 RID: 7349 RVA: 0x000847CA File Offset: 0x000829CA
			public UserConfigurationDescriptorEqualityComparer(IMailboxSession session)
			{
				this.session = session;
			}

			// Token: 0x06001CB6 RID: 7350 RVA: 0x000847DC File Offset: 0x000829DC
			public bool Equals(UserConfigurationDescriptor x, UserConfigurationDescriptor y)
			{
				return object.ReferenceEquals(x, y) || (!object.ReferenceEquals(x, null) && (string.Equals(x.ConfigurationName, y.ConfigurationName, StringComparison.OrdinalIgnoreCase) && x.Types == y.Types) && object.Equals(x.GetFolderId(this.session), y.GetFolderId(this.session)));
			}

			// Token: 0x06001CB7 RID: 7351 RVA: 0x00084840 File Offset: 0x00082A40
			public int GetHashCode(UserConfigurationDescriptor obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.ConfigurationName.GetHashCode() ^ obj.Types.GetHashCode();
			}

			// Token: 0x0400138F RID: 5007
			private readonly IMailboxSession session;
		}

		// Token: 0x020002B1 RID: 689
		private class DefaultFolderDescriptor : UserConfigurationDescriptor
		{
			// Token: 0x06001CB8 RID: 7352 RVA: 0x00084863 File Offset: 0x00082A63
			public DefaultFolderDescriptor(string configName, UserConfigurationTypes types, DefaultFolderType folderType) : base(configName, types)
			{
				this.folderType = folderType;
			}

			// Token: 0x06001CB9 RID: 7353 RVA: 0x00084874 File Offset: 0x00082A74
			public override void Validate(IUserConfiguration config)
			{
				if ((base.Types & UserConfigurationTypes.Dictionary) != (UserConfigurationTypes)0)
				{
					config.GetDictionary();
				}
			}

			// Token: 0x06001CBA RID: 7354 RVA: 0x00084888 File Offset: 0x00082A88
			public override StoreObjectId GetFolderId(IMailboxSession session)
			{
				return session.GetDefaultFolderId(this.folderType);
			}

			// Token: 0x04001390 RID: 5008
			private readonly DefaultFolderType folderType;
		}

		// Token: 0x020002B2 RID: 690
		private class FolderIdDescriptor : UserConfigurationDescriptor
		{
			// Token: 0x06001CBB RID: 7355 RVA: 0x000848A5 File Offset: 0x00082AA5
			public FolderIdDescriptor(string configName, UserConfigurationTypes types, StoreObjectId folderId) : base(configName, types)
			{
				this.folderId = folderId;
			}

			// Token: 0x06001CBC RID: 7356 RVA: 0x000848B6 File Offset: 0x00082AB6
			public override void Validate(IUserConfiguration config)
			{
			}

			// Token: 0x06001CBD RID: 7357 RVA: 0x000848B8 File Offset: 0x00082AB8
			public override StoreObjectId GetFolderId(IMailboxSession session)
			{
				return this.folderId;
			}

			// Token: 0x04001391 RID: 5009
			private readonly StoreObjectId folderId;
		}
	}
}
