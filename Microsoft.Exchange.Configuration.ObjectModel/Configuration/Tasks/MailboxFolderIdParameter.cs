using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public class MailboxFolderIdParameter : IIdentityParameter
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00025DD9 File Offset: 0x00023FD9
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00025DE1 File Offset: 0x00023FE1
		internal MailboxIdParameter RawOwner { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00025DEA File Offset: 0x00023FEA
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00025DF2 File Offset: 0x00023FF2
		internal MapiFolderPath RawFolderPath { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00025DFB File Offset: 0x00023FFB
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00025E03 File Offset: 0x00024003
		internal StoreObjectId RawFolderStoreId { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00025E0C File Offset: 0x0002400C
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00025E14 File Offset: 0x00024014
		internal MailboxFolderId InternalMailboxFolderId { get; set; }

		// Token: 0x06000C04 RID: 3076 RVA: 0x00025E20 File Offset: 0x00024020
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00025E38 File Offset: 0x00024038
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == this.InternalMailboxFolderId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			if (!(session is MailboxFolderDataProviderBase))
			{
				throw new NotSupportedException("session: " + session.GetType().FullName);
			}
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			T t = (T)((object)session.Read<T>(this.InternalMailboxFolderId));
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

		// Token: 0x06000C06 RID: 3078 RVA: 0x00025EFC File Offset: 0x000240FC
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (!(objectId is MailboxFolderId))
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.InternalMailboxFolderId = (MailboxFolderId)objectId;
			this.RawOwner = new MailboxIdParameter(this.InternalMailboxFolderId.MailboxOwnerId);
			this.RawFolderPath = this.InternalMailboxFolderId.MailboxFolderPath;
			this.RawFolderStoreId = this.InternalMailboxFolderId.StoreObjectIdValue;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00025F7E File Offset: 0x0002417E
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00025F86 File Offset: 0x00024186
		public MailboxFolderIdParameter()
		{
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00025F90 File Offset: 0x00024190
		public MailboxFolderIdParameter(MailboxFolder mailboxFolder)
		{
			if (mailboxFolder == null)
			{
				throw new ArgumentNullException("mailboxFolder");
			}
			ObjectId identity = mailboxFolder.Identity;
			if (identity == null)
			{
				throw new ArgumentNullException("mailboxFolder.Identity");
			}
			this.rawIdentity = identity.ToString();
			((IIdentityParameter)this).Initialize(identity);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00025FD9 File Offset: 0x000241D9
		public MailboxFolderIdParameter(PublicFolderId publicFolderId) : this(publicFolderId.ToString())
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00025FE7 File Offset: 0x000241E7
		public MailboxFolderIdParameter(MailboxFolderId mailboxFolderId)
		{
			if (null == mailboxFolderId)
			{
				throw new ArgumentNullException("mailboxFolderId");
			}
			this.rawIdentity = mailboxFolderId.ToString();
			((IIdentityParameter)this).Initialize(mailboxFolderId);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00026018 File Offset: 0x00024218
		public MailboxFolderIdParameter(string mailboxFolderId)
		{
			if (string.IsNullOrEmpty(mailboxFolderId))
			{
				throw new ArgumentNullException("mailboxFolderId");
			}
			this.rawIdentity = mailboxFolderId;
			int num = mailboxFolderId.IndexOf(':');
			if (-1 == num)
			{
				this.RawOwner = new MailboxIdParameter(mailboxFolderId);
				this.RawFolderPath = MapiFolderPath.IpmSubtreeRoot;
				return;
			}
			string text = mailboxFolderId.Substring(0, num);
			string text2 = mailboxFolderId.Substring(1 + num);
			if (!string.IsNullOrEmpty(text))
			{
				this.RawOwner = new MailboxIdParameter(text);
			}
			if (string.IsNullOrEmpty(text2))
			{
				throw new FormatException(Strings.ErrorInvalidMailboxFolderIdentity(this.rawIdentity));
			}
			try
			{
				if (text2[0] == '\\' || text2[0] == '￾')
				{
					this.RawFolderPath = MapiFolderPath.Parse(text2);
				}
				else
				{
					this.RawFolderStoreId = StoreObjectId.Deserialize(text2);
				}
			}
			catch (FormatException innerException)
			{
				throw new FormatException(Strings.ErrorInvalidMailboxFolderIdentity(this.rawIdentity), innerException);
			}
			catch (CorruptDataException innerException2)
			{
				throw new FormatException(Strings.ErrorInvalidMailboxFolderIdentity(this.rawIdentity), innerException2);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00026134 File Offset: 0x00024334
		public MailboxFolderIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002614E File Offset: 0x0002434E
		public MailboxFolderIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002615C File Offset: 0x0002435C
		public MailboxFolderIdParameter(ADObjectId mailboxOwnerId)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			this.rawIdentity = mailboxOwnerId.ToString();
			((IIdentityParameter)this).Initialize(new MailboxFolderId(mailboxOwnerId, null, MapiFolderPath.IpmSubtreeRoot));
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00026190 File Offset: 0x00024390
		internal MailboxFolderIdParameter(PublicFolderIdParameter publicFolderIdParameter, ADUser publicFolderHierarchyMailbox)
		{
			if (publicFolderIdParameter == null)
			{
				throw new ArgumentNullException("mailboxFolderId");
			}
			this.rawIdentity = publicFolderIdParameter.ToString();
			this.RawFolderPath = publicFolderIdParameter.PublicFolderId.MapiFolderPath;
			this.RawFolderStoreId = publicFolderIdParameter.PublicFolderId.StoreObjectId;
			this.RawOwner = new MailboxIdParameter(publicFolderHierarchyMailbox.ObjectId);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000261F0 File Offset: 0x000243F0
		public override string ToString()
		{
			return this.rawIdentity;
		}

		// Token: 0x040002BD RID: 701
		private string rawIdentity;
	}
}
