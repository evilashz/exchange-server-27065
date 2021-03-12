using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public class PublicFolderIdParameter : IIdentityParameter
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00023C0D File Offset: 0x00021E0D
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00023C15 File Offset: 0x00021E15
		internal PublicFolderId PublicFolderId { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00023C1E File Offset: 0x00021E1E
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00023C26 File Offset: 0x00021E26
		internal OrganizationIdParameter Organization { get; private set; }

		// Token: 0x06000B23 RID: 2851 RVA: 0x00023C30 File Offset: 0x00021E30
		internal PublicFolderIdParameter(string publicFolderIdString, bool isEntryId)
		{
			if (string.IsNullOrEmpty(publicFolderIdString))
			{
				throw new ArgumentNullException("publicFolderIdString");
			}
			this.rawIdentity = publicFolderIdString;
			PublicFolderId publicFolderId = null;
			try
			{
				int num = publicFolderIdString.IndexOf('\\');
				if (num < 0)
				{
					if (isEntryId)
					{
						publicFolderId = new PublicFolderId(StoreObjectId.FromHexEntryId(publicFolderIdString));
					}
				}
				else if (num == 0)
				{
					if (publicFolderIdString.Length > 1 && publicFolderIdString[num + 1] == '\\')
					{
						throw new FormatException(Strings.ErrorIncompletePublicFolderIdParameter(publicFolderIdString));
					}
					publicFolderId = new PublicFolderId(MapiFolderPath.Parse(publicFolderIdString));
				}
				else
				{
					if (!MapiTaskHelper.IsDatacenter)
					{
						throw new FormatException(Strings.ErrorIncompletePublicFolderIdParameter(publicFolderIdString));
					}
					if (num == publicFolderIdString.Length - 1)
					{
						throw new FormatException(Strings.ErrorIncompleteDCPublicFolderIdParameter(publicFolderIdString));
					}
					this.Organization = new OrganizationIdParameter(publicFolderIdString.Substring(0, num));
					if (publicFolderIdString[num + 1] == '\\')
					{
						publicFolderId = new PublicFolderId(MapiFolderPath.Parse(publicFolderIdString.Substring(num + 1)));
					}
					else if (isEntryId)
					{
						publicFolderId = new PublicFolderId(StoreObjectId.FromHexEntryId(publicFolderIdString.Substring(num + 1)));
					}
				}
			}
			catch (FormatException innerException)
			{
				throw new FormatException(MapiTaskHelper.IsDatacenter ? Strings.ErrorIncompleteDCPublicFolderIdParameter(this.rawIdentity) : Strings.ErrorIncompletePublicFolderIdParameter(this.rawIdentity), innerException);
			}
			catch (CorruptDataException innerException2)
			{
				throw new FormatException(MapiTaskHelper.IsDatacenter ? Strings.ErrorIncompleteDCPublicFolderIdParameter(this.rawIdentity) : Strings.ErrorIncompletePublicFolderIdParameter(this.rawIdentity), innerException2);
			}
			if (publicFolderId != null)
			{
				((IIdentityParameter)this).Initialize(publicFolderId);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00023DC4 File Offset: 0x00021FC4
		public PublicFolderIdParameter()
		{
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00023DCC File Offset: 0x00021FCC
		public PublicFolderIdParameter(PublicFolder publicFolder)
		{
			if (publicFolder == null)
			{
				throw new ArgumentNullException("publicFolder");
			}
			if (publicFolder.Identity == null)
			{
				throw new ArgumentNullException("publicFolder.Identity");
			}
			this.rawIdentity = publicFolder.Identity.ToString();
			((IIdentityParameter)this).Initialize(publicFolder.Identity);
			if (publicFolder.OrganizationId != null && publicFolder.OrganizationId.OrganizationalUnit != null)
			{
				this.Organization = new OrganizationIdParameter(publicFolder.OrganizationId.OrganizationalUnit);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00023E4E File Offset: 0x0002204E
		public PublicFolderIdParameter(PublicFolderId publicFolderId)
		{
			if (publicFolderId == null)
			{
				throw new ArgumentNullException("publicFolderId");
			}
			this.rawIdentity = publicFolderId.ToString();
			((IIdentityParameter)this).Initialize(publicFolderId);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00023E77 File Offset: 0x00022077
		public PublicFolderIdParameter(string publicFolderIdString) : this(publicFolderIdString, true)
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00023E81 File Offset: 0x00022081
		public PublicFolderIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00023E9C File Offset: 0x0002209C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00023EB4 File Offset: 0x000220B4
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (this.PublicFolderId == null)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			if (!(session is PublicFolderDataProvider) && !(session is PublicFolderStatisticsDataProvider))
			{
				throw new NotSupportedException("session: " + session.GetType().FullName);
			}
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			T t = (T)((object)session.Read<T>(this.PublicFolderId));
			if (t == null)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			return new T[]
			{
				t
			};
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00023F78 File Offset: 0x00022178
		public void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (!(objectId is PublicFolderId))
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.PublicFolderId = (PublicFolderId)objectId;
			if (this.PublicFolderId.OrganizationId != null)
			{
				this.Organization = new OrganizationIdParameter(this.PublicFolderId.OrganizationId.OrganizationalUnit);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00023FF0 File Offset: 0x000221F0
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00023FF8 File Offset: 0x000221F8
		public override string ToString()
		{
			return this.rawIdentity;
		}

		// Token: 0x04000296 RID: 662
		private readonly string rawIdentity;
	}
}
