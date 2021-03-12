using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000152 RID: 338
	[Serializable]
	public class MessageCategoryIdParameter : IIdentityParameter
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00026537 File Offset: 0x00024737
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0002653F File Offset: 0x0002473F
		internal MessageCategoryId InternalMessageCategoryId
		{
			get
			{
				return this.internalMessageCategoryId;
			}
			set
			{
				this.internalMessageCategoryId = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00026548 File Offset: 0x00024748
		internal string RawCategoryName
		{
			get
			{
				return this.rawCategoryName;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00026550 File Offset: 0x00024750
		internal Guid? RawCategoryId
		{
			get
			{
				return this.rawCategoryId;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00026558 File Offset: 0x00024758
		internal MailboxIdParameter RawMailbox
		{
			get
			{
				return this.rawMailbox;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00026560 File Offset: 0x00024760
		string IIdentityParameter.RawIdentity
		{
			get
			{
				if (this.internalMessageCategoryId != null)
				{
					return this.internalMessageCategoryId.ToString();
				}
				return this.rawInput;
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00026582 File Offset: 0x00024782
		public MessageCategoryIdParameter()
		{
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002658A File Offset: 0x0002478A
		public MessageCategoryIdParameter(MessageCategory category)
		{
			if (category == null)
			{
				throw new ArgumentNullException("MessageCategory");
			}
			((IIdentityParameter)this).Initialize(category.Identity);
			this.rawInput = category.Identity.ToString();
			this.rawCategoryName = category.Name;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000265C9 File Offset: 0x000247C9
		public MessageCategoryIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000265D7 File Offset: 0x000247D7
		public MessageCategoryIdParameter(MessageCategoryId messageCategoryId)
		{
			if (null == messageCategoryId)
			{
				throw new ArgumentNullException("messageCategoryId");
			}
			((IIdentityParameter)this).Initialize(messageCategoryId);
			this.rawInput = messageCategoryId.ToString();
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00026606 File Offset: 0x00024806
		public MessageCategoryIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00026614 File Offset: 0x00024814
		public MessageCategoryIdParameter(ADObjectId mailboxOwnerId)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			((IIdentityParameter)this).Initialize(new MessageCategoryId(mailboxOwnerId, null, null));
			this.rawInput = mailboxOwnerId.ToString();
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00026658 File Offset: 0x00024858
		public MessageCategoryIdParameter(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input");
			}
			this.rawInput = input;
			int num = input.IndexOf('\\');
			string text;
			string text2;
			if (num > 1)
			{
				text = input.Substring(0, num);
				text2 = input.Substring(1 + num);
			}
			else
			{
				text2 = input;
				text = null;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.rawMailbox = new MailboxIdParameter(text);
			}
			this.rawCategoryName = null;
			this.rawCategoryId = null;
			if (!string.IsNullOrEmpty(text2))
			{
				Guid value;
				if (GuidHelper.TryParseGuid(text2, out value))
				{
					this.rawCategoryId = new Guid?(value);
					return;
				}
				this.rawCategoryName = text2;
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000266F8 File Offset: 0x000248F8
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00026710 File Offset: 0x00024910
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == this.InternalMessageCategoryId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			MessageCategoryDataProvider messageCategoryDataProvider = session as MessageCategoryDataProvider;
			if (messageCategoryDataProvider == null)
			{
				throw new NotSupportedException("DataProvider: " + session.GetType().FullName);
			}
			IConfigurable[] array = messageCategoryDataProvider.Find<T>(null, this.InternalMessageCategoryId, false, null);
			if (array == null || array.Length == 0)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			T[] array2 = new T[array.Length];
			int num = 0;
			foreach (IConfigurable configurable in array)
			{
				array2[num++] = (T)((object)configurable);
			}
			return array2;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000267E7 File Offset: 0x000249E7
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (!(objectId is MessageCategoryId))
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.internalMessageCategoryId = (MessageCategoryId)objectId;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00026826 File Offset: 0x00024A26
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.rawCategoryName))
			{
				return this.rawCategoryName;
			}
			if (this.internalMessageCategoryId != null)
			{
				return this.internalMessageCategoryId.ToString();
			}
			return this.rawInput;
		}

		// Token: 0x040002C6 RID: 710
		private MailboxIdParameter rawMailbox;

		// Token: 0x040002C7 RID: 711
		private Guid? rawCategoryId;

		// Token: 0x040002C8 RID: 712
		private string rawCategoryName;

		// Token: 0x040002C9 RID: 713
		private string rawInput;

		// Token: 0x040002CA RID: 714
		private MessageCategoryId internalMessageCategoryId;
	}
}
