using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000468 RID: 1128
	[Serializable]
	public class MailboxAssociationIdParameter : IIdentityParameter
	{
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x0009CF10 File Offset: 0x0009B110
		// (set) Token: 0x060027BB RID: 10171 RVA: 0x0009CF18 File Offset: 0x0009B118
		public MailboxIdParameter MailboxId { get; private set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x0009CF21 File Offset: 0x0009B121
		// (set) Token: 0x060027BD RID: 10173 RVA: 0x0009CF29 File Offset: 0x0009B129
		public string AssociationIdType { get; private set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x0009CF32 File Offset: 0x0009B132
		// (set) Token: 0x060027BF RID: 10175 RVA: 0x0009CF3A File Offset: 0x0009B13A
		public string AssociationIdValue { get; private set; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x0009CF43 File Offset: 0x0009B143
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x0009CF4B File Offset: 0x0009B14B
		public MailboxAssociationIdParameter()
		{
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0009CF53 File Offset: 0x0009B153
		public MailboxAssociationIdParameter(MailboxAssociationIdParameter mailboxAssociationId) : this(mailboxAssociationId.ToString())
		{
			this.rawIdentity = mailboxAssociationId.rawIdentity;
			this.MailboxId = mailboxAssociationId.MailboxId;
			this.AssociationIdType = mailboxAssociationId.AssociationIdType;
			this.AssociationIdValue = mailboxAssociationId.AssociationIdValue;
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0009CF91 File Offset: 0x0009B191
		public MailboxAssociationIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0009CFAB File Offset: 0x0009B1AB
		public MailboxAssociationIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x0009CFB9 File Offset: 0x0009B1B9
		public MailboxAssociationIdParameter(ADObjectId mailboxId)
		{
			ArgumentValidator.ThrowIfNull("mailboxId", mailboxId);
			this.rawIdentity = mailboxId.ToString();
			((IIdentityParameter)this).Initialize(new MailboxStoreObjectId(mailboxId, null));
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0009CFE5 File Offset: 0x0009B1E5
		public MailboxAssociationIdParameter(ConfigurableObject configurableObject)
		{
			ArgumentValidator.ThrowIfNull("configurableObject", configurableObject);
			this.rawIdentity = configurableObject.Identity.ToString();
			((IIdentityParameter)this).Initialize(configurableObject.Identity);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x0009D015 File Offset: 0x0009B215
		public MailboxAssociationIdParameter(MailboxStoreObjectId mailboxStoreObjectId) : this(mailboxStoreObjectId.ToString())
		{
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0009D024 File Offset: 0x0009B224
		public MailboxAssociationIdParameter(string mailboxAssociationId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("mailboxAssociationId", mailboxAssociationId);
			this.rawIdentity = mailboxAssociationId;
			string[] array = mailboxAssociationId.Split(MailboxAssociationIdParameter.IdTokenizer, 3);
			if (array.Length == 2 || array.Length > 3)
			{
				throw new FormatException(Strings.ErrorInvalidMailboxAssociationIdentity(this.rawIdentity));
			}
			try
			{
				MailboxStoreObjectIdParameter mailboxStoreObjectIdParameter = new MailboxStoreObjectIdParameter(array[0]);
				if (mailboxStoreObjectIdParameter.RawOwner != null)
				{
					this.MailboxId = mailboxStoreObjectIdParameter.RawOwner;
					this.AssociationIdType = MailboxAssociationIdParameter.IdTypeItemId;
					this.AssociationIdValue = mailboxStoreObjectIdParameter.RawStoreObjectId.ToBase64String();
				}
				return;
			}
			catch (FormatException)
			{
			}
			this.MailboxId = new MailboxIdParameter(array[0]);
			this.AssociationIdType = null;
			this.AssociationIdValue = null;
			if (array.Length == 3)
			{
				if (!MailboxAssociationIdParameter.IsValidIdType(array[1]) || string.IsNullOrWhiteSpace(array[2]))
				{
					throw new FormatException(Strings.ErrorInvalidMailboxAssociationIdentity(this.rawIdentity));
				}
				this.AssociationIdType = array[1];
				this.AssociationIdValue = array[2];
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0009D124 File Offset: 0x0009B324
		public override string ToString()
		{
			return this.rawIdentity;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0009D12C File Offset: 0x0009B32C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0009D144 File Offset: 0x0009B344
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.MailboxId.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0009D158 File Offset: 0x0009B358
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			ArgumentValidator.ThrowIfNull("objectId", objectId);
			MailboxStoreObjectId mailboxStoreObjectId = objectId as MailboxStoreObjectId;
			if (mailboxStoreObjectId == null)
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.MailboxId = new MailboxIdParameter(mailboxStoreObjectId.MailboxOwnerId);
			if (mailboxStoreObjectId.StoreObjectId != null)
			{
				this.AssociationIdType = MailboxAssociationIdParameter.IdTypeItemId;
				this.AssociationIdValue = mailboxStoreObjectId.StoreObjectId.ToBase64String();
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0009D1D0 File Offset: 0x0009B3D0
		private static bool IsValidIdType(string idType)
		{
			return MailboxAssociationIdParameter.IdTypeExternalId.Equals(idType, StringComparison.OrdinalIgnoreCase) || MailboxAssociationIdParameter.IdTypeLegacyDn.Equals(idType, StringComparison.OrdinalIgnoreCase) || MailboxAssociationIdParameter.IdTypeItemId.Equals(idType, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04001DB2 RID: 7602
		public static readonly string IdTypeExternalId = "EOI";

		// Token: 0x04001DB3 RID: 7603
		public static readonly string IdTypeLegacyDn = "LDN";

		// Token: 0x04001DB4 RID: 7604
		public static readonly string IdTypeItemId = "IID";

		// Token: 0x04001DB5 RID: 7605
		private static readonly char[] IdTokenizer = new char[]
		{
			':'
		};

		// Token: 0x04001DB6 RID: 7606
		private readonly string rawIdentity;
	}
}
