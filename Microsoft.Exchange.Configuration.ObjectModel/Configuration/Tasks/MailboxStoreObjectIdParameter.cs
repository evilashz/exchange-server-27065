using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	public class MailboxStoreObjectIdParameter : IIdentityParameter
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000261F8 File Offset: 0x000243F8
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00026200 File Offset: 0x00024400
		internal MailboxIdParameter RawOwner { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00026209 File Offset: 0x00024409
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00026211 File Offset: 0x00024411
		internal StoreObjectId RawStoreObjectId { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002621A File Offset: 0x0002441A
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x00026222 File Offset: 0x00024422
		internal MailboxStoreObjectId InternalStoreObjectId { get; set; }

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002622C File Offset: 0x0002442C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00026244 File Offset: 0x00024444
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == this.InternalStoreObjectId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			if (!(session is MailMessageDataProvider))
			{
				throw new NotSupportedException("session: " + session.GetType().FullName);
			}
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			T t = (T)((object)session.Read<T>(this.InternalStoreObjectId));
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

		// Token: 0x06000C1A RID: 3098 RVA: 0x00026308 File Offset: 0x00024508
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			MailboxStoreObjectId mailboxStoreObjectId = objectId as MailboxStoreObjectId;
			if (null == mailboxStoreObjectId)
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.RawOwner = new MailboxIdParameter(mailboxStoreObjectId.MailboxOwnerId);
			this.RawStoreObjectId = mailboxStoreObjectId.StoreObjectId;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002636B File Offset: 0x0002456B
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00026373 File Offset: 0x00024573
		public MailboxStoreObjectIdParameter()
		{
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002637C File Offset: 0x0002457C
		public MailboxStoreObjectIdParameter(XsoMailboxConfigurationObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			ObjectId identity = storeObject.Identity;
			if (identity == null)
			{
				throw new ArgumentNullException("storeObject.Identity");
			}
			this.rawIdentity = identity.ToString();
			((IIdentityParameter)this).Initialize(identity);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000263C5 File Offset: 0x000245C5
		public MailboxStoreObjectIdParameter(MailboxStoreObjectId storeObjectId)
		{
			if (null == storeObjectId)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			this.rawIdentity = storeObjectId.ToString();
			((IIdentityParameter)this).Initialize(storeObjectId);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000263F4 File Offset: 0x000245F4
		public MailboxStoreObjectIdParameter(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("mailMessageId");
			}
			this.rawIdentity = id;
			int num = id.LastIndexOf('\\');
			string text;
			if (-1 == num)
			{
				text = id;
			}
			else
			{
				string text2 = id.Substring(0, num);
				text = id.Substring(1 + num);
				if (!string.IsNullOrEmpty(text2))
				{
					this.RawOwner = new MailboxIdParameter(text2);
				}
				if (string.IsNullOrEmpty(text))
				{
					throw new FormatException(Strings.ErrorInvalidMailboxStoreObjectIdentity(this.rawIdentity));
				}
			}
			try
			{
				this.RawStoreObjectId = StoreObjectId.Deserialize(text);
			}
			catch (FormatException innerException)
			{
				throw new FormatException(Strings.ErrorInvalidMailboxStoreObjectIdentity(this.rawIdentity), innerException);
			}
			catch (CorruptDataException innerException2)
			{
				throw new FormatException(Strings.ErrorInvalidMailboxStoreObjectIdentity(this.rawIdentity), innerException2);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000264D8 File Offset: 0x000246D8
		public MailboxStoreObjectIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000264F2 File Offset: 0x000246F2
		public MailboxStoreObjectIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00026500 File Offset: 0x00024700
		public MailboxStoreObjectIdParameter(ADObjectId mailboxOwnerId)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			this.rawIdentity = mailboxOwnerId.ToString();
			((IIdentityParameter)this).Initialize(new MailboxStoreObjectId(mailboxOwnerId, null));
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002652F File Offset: 0x0002472F
		public override string ToString()
		{
			return this.rawIdentity;
		}

		// Token: 0x040002C2 RID: 706
		private string rawIdentity;
	}
}
