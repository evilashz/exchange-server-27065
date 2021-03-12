using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class InboxRuleIdParameter : IIdentityParameter
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00025A96 File Offset: 0x00023C96
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00025A9E File Offset: 0x00023C9E
		internal InboxRuleId InternalInboxRuleId
		{
			get
			{
				return this.internalInboxRuleId;
			}
			set
			{
				this.internalInboxRuleId = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00025AA7 File Offset: 0x00023CA7
		internal string RawRuleName
		{
			get
			{
				return this.rawRuleName;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00025AAF File Offset: 0x00023CAF
		internal RuleId RawRuleId
		{
			get
			{
				return this.rawRuleId;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00025AB7 File Offset: 0x00023CB7
		internal MailboxIdParameter RawMailbox
		{
			get
			{
				return this.rawMailbox;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00025ABF File Offset: 0x00023CBF
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawInput;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00025AC7 File Offset: 0x00023CC7
		public InboxRuleIdParameter()
		{
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00025ACF File Offset: 0x00023CCF
		public InboxRuleIdParameter(ConfigurableObject configurableObject)
		{
			if (configurableObject == null)
			{
				throw new ArgumentNullException("configurableObject");
			}
			((IIdentityParameter)this).Initialize(configurableObject.Identity);
			this.rawInput = configurableObject.Identity.ToString();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00025B02 File Offset: 0x00023D02
		public InboxRuleIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawRuleName = namedIdentity.DisplayName;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00025B1C File Offset: 0x00023D1C
		public InboxRuleIdParameter(InboxRuleId inboxRuleId)
		{
			if (null == inboxRuleId)
			{
				throw new ArgumentNullException("inboxRuleId");
			}
			((IIdentityParameter)this).Initialize(inboxRuleId);
			this.rawInput = inboxRuleId.ToString();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00025B4B File Offset: 0x00023D4B
		public InboxRuleIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00025B59 File Offset: 0x00023D59
		public InboxRuleIdParameter(ADObjectId mailboxOwnerId)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			this.rawInput = mailboxOwnerId.ToString();
			((IIdentityParameter)this).Initialize(new InboxRuleId(mailboxOwnerId, null, null));
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00025B8C File Offset: 0x00023D8C
		public InboxRuleIdParameter(string inboxRuleId)
		{
			if (string.IsNullOrEmpty(inboxRuleId))
			{
				throw new ArgumentNullException("inboxRuleId");
			}
			this.rawInput = inboxRuleId;
			int num = inboxRuleId.Length;
			int num2;
			do
			{
				num2 = num;
				num = inboxRuleId.LastIndexOf("\\\\", num2 - 1, num2);
			}
			while (num != -1);
			int num3 = inboxRuleId.LastIndexOf('\\', num2 - 1, num2);
			if (num3 == 0 || num3 == inboxRuleId.Length - 1)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("inboxRuleId"), "inboxRuleId");
			}
			string text;
			string text2;
			if (num3 > 0)
			{
				text = inboxRuleId.Substring(0, num3);
				text2 = inboxRuleId.Substring(1 + num3);
			}
			else
			{
				text2 = inboxRuleId;
				text = null;
			}
			if (num2 != inboxRuleId.Length)
			{
				text2 = text2.Replace("\\\\", '\\'.ToString());
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.rawMailbox = new MailboxIdParameter(text);
			}
			ulong value;
			if (ulong.TryParse(text2, out value))
			{
				byte[] array = new byte[8];
				ExBitConverter.Write((long)value, array, 0);
				this.rawRuleId = RuleId.Deserialize(array, 0);
				return;
			}
			this.rawRuleName = text2;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00025C9C File Offset: 0x00023E9C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00025CB4 File Offset: 0x00023EB4
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == this.InternalInboxRuleId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			IConfigurable[] array = session.Find<T>(null, this.InternalInboxRuleId, false, null);
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

		// Token: 0x06000BFA RID: 3066 RVA: 0x00025D64 File Offset: 0x00023F64
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (!(objectId is InboxRuleId))
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.internalInboxRuleId = (InboxRuleId)objectId;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00025DA3 File Offset: 0x00023FA3
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.rawRuleName))
			{
				return this.rawRuleName;
			}
			if (this.internalInboxRuleId != null)
			{
				return this.internalInboxRuleId.ToString();
			}
			return this.rawInput;
		}

		// Token: 0x040002B8 RID: 696
		private MailboxIdParameter rawMailbox;

		// Token: 0x040002B9 RID: 697
		private RuleId rawRuleId;

		// Token: 0x040002BA RID: 698
		private string rawRuleName;

		// Token: 0x040002BB RID: 699
		private string rawInput;

		// Token: 0x040002BC RID: 700
		private InboxRuleId internalInboxRuleId;
	}
}
