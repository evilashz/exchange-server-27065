using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	public class MailPublicFolderIdParameter : RecipientIdParameter
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x000286E6 File Offset: 0x000268E6
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x000286EE File Offset: 0x000268EE
		internal OrganizationIdParameter Organization { get; private set; }

		// Token: 0x06000D5C RID: 3420 RVA: 0x000286F7 File Offset: 0x000268F7
		public MailPublicFolderIdParameter()
		{
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00028700 File Offset: 0x00026900
		public MailPublicFolderIdParameter(string identity) : base(identity)
		{
			PublicFolderIdParameter publicFolderIdParameter = new PublicFolderIdParameter(identity, false);
			this.folderId = publicFolderIdParameter.PublicFolderId;
			if (publicFolderIdParameter.Organization != null && this.folderId != null)
			{
				this.Organization = publicFolderIdParameter.Organization;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00028744 File Offset: 0x00026944
		public MailPublicFolderIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002874D File Offset: 0x0002694D
		public MailPublicFolderIdParameter(ADObject adObject) : this(adObject.Id)
		{
			if (adObject.OrganizationId != null && adObject.OrganizationId.ConfigurationUnit != null)
			{
				this.Organization = new OrganizationIdParameter(adObject.OrganizationId.OrganizationalUnit);
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002878C File Offset: 0x0002698C
		public MailPublicFolderIdParameter(PublicFolderId folderId)
		{
			this.folderId = folderId;
			if (folderId.OrganizationId != null)
			{
				this.Organization = new OrganizationIdParameter(folderId.OrganizationId.OrganizationalUnit);
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000287BF File Offset: 0x000269BF
		public MailPublicFolderIdParameter(PublicFolder folder) : this((PublicFolderId)folder.Identity)
		{
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000287D2 File Offset: 0x000269D2
		public MailPublicFolderIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000287DB File Offset: 0x000269DB
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailPublicFolderIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000287E4 File Offset: 0x000269E4
		public new static MailPublicFolderIdParameter Parse(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.Length == 0)
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(typeof(MailPublicFolderIdParameter).ToString()), "identity");
			}
			return new MailPublicFolderIdParameter(identity);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00028831 File Offset: 0x00026A31
		public override string ToString()
		{
			if (this.folderId != null)
			{
				return this.folderId.ToString();
			}
			return base.ToString();
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00028850 File Offset: 0x00026A50
		internal sealed override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			notFoundReason = null;
			bool flag = false;
			if (base.InternalADObjectId == null && this.folderId != null)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(session.ConsistencyMode, session.SessionSettings, 205, "GetObjects", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\RecipientParameters\\MailPublicFolderIdParameter.cs");
				using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(tenantOrTopologyConfigurationSession, "resolve-MailPublicFolderIdParameter", Guid.Empty))
				{
					PublicFolder publicFolder = (PublicFolder)publicFolderDataProvider.Read<PublicFolder>(this.folderId);
					if (publicFolder == null)
					{
						return new List<T>();
					}
					flag = true;
					if (!publicFolder.MailEnabled)
					{
						notFoundReason = new LocalizedString?(Strings.ErrorPublicFolderMailDisabled(this.folderId.ToString()));
						return new List<T>();
					}
					if (publicFolder.ProxyGuid == null)
					{
						notFoundReason = new LocalizedString?(Strings.ErrorPublicFolderGeneratingProxy(this.folderId.ToString()));
						return new List<T>();
					}
					this.Initialize(new ADObjectId(publicFolder.ProxyGuid));
				}
			}
			IEnumerable<T> objects = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(objects);
			if (!wrapper.HasElements() && flag)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorPublicFolderMailDisabled(this.folderId.ToString()));
			}
			return wrapper;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000289A8 File Offset: 0x00026BA8
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailPublicFolder(id);
		}

		// Token: 0x040002F3 RID: 755
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.PublicFolder
		};

		// Token: 0x040002F4 RID: 756
		private PublicFolderId folderId;
	}
}
