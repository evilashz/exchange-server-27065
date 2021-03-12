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
	// Token: 0x0200015C RID: 348
	[Serializable]
	public class UMCallAnsweringRuleIdParameter : IIdentityParameter
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x000276C8 File Offset: 0x000258C8
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x000276D0 File Offset: 0x000258D0
		internal Guid? RawRuleGuid { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x000276D9 File Offset: 0x000258D9
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x000276E1 File Offset: 0x000258E1
		internal MailboxIdParameter RawMailbox { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x000276EA File Offset: 0x000258EA
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x000276F2 File Offset: 0x000258F2
		internal UMCallAnsweringRuleId CallAnsweringRuleId { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x000276FB File Offset: 0x000258FB
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawInput;
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00027703 File Offset: 0x00025903
		public UMCallAnsweringRuleIdParameter()
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002770B File Offset: 0x0002590B
		public UMCallAnsweringRuleIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00027719 File Offset: 0x00025919
		public UMCallAnsweringRuleIdParameter(ConfigurableObject configurableObject)
		{
			if (configurableObject == null)
			{
				throw new ArgumentNullException("configurableObject");
			}
			((IIdentityParameter)this).Initialize(configurableObject.Identity);
			this.rawInput = configurableObject.Identity.ToString();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002774C File Offset: 0x0002594C
		public UMCallAnsweringRuleIdParameter(UMCallAnsweringRuleId ruleId)
		{
			if (ruleId == null)
			{
				throw new ArgumentNullException("ruleId");
			}
			((IIdentityParameter)this).Initialize(ruleId);
			this.rawInput = ruleId.ToString();
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002777B File Offset: 0x0002597B
		public UMCallAnsweringRuleIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00027789 File Offset: 0x00025989
		public UMCallAnsweringRuleIdParameter(ADObjectId mailboxOwnerId)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			this.rawInput = mailboxOwnerId.ToString();
			((IIdentityParameter)this).Initialize(new UMCallAnsweringRuleId(mailboxOwnerId, Guid.Empty));
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000277BC File Offset: 0x000259BC
		public UMCallAnsweringRuleIdParameter(string callAnsweringRuleId)
		{
			if (string.IsNullOrEmpty(callAnsweringRuleId))
			{
				throw new ArgumentNullException("CallAnsweringRuleId");
			}
			this.rawInput = callAnsweringRuleId;
			string input = string.Empty;
			string text = string.Empty;
			int num = callAnsweringRuleId.LastIndexOf("\\");
			if (num == 0 || num == callAnsweringRuleId.Length - 1)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("CallAnsweringRuleId"), "CallAnsweringRuleId");
			}
			if (num > 0)
			{
				text = callAnsweringRuleId.Substring(0, num);
				input = callAnsweringRuleId.Substring(1 + num);
			}
			else
			{
				input = callAnsweringRuleId;
				text = null;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.RawMailbox = new MailboxIdParameter(text);
			}
			this.RawRuleGuid = new Guid?(Guid.Parse(input));
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002786C File Offset: 0x00025A6C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00027884 File Offset: 0x00025A84
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == this.CallAnsweringRuleId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			IConfigurable[] array = session.Find<T>(null, this.CallAnsweringRuleId, false, null);
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

		// Token: 0x06000C97 RID: 3223 RVA: 0x00027934 File Offset: 0x00025B34
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			this.CallAnsweringRuleId = (objectId as UMCallAnsweringRuleId);
			if (this.CallAnsweringRuleId == null)
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00027984 File Offset: 0x00025B84
		public override string ToString()
		{
			if (this.RawRuleGuid != null)
			{
				return this.RawRuleGuid.ToString();
			}
			if (this.CallAnsweringRuleId != null)
			{
				return this.CallAnsweringRuleId.ToString();
			}
			return this.rawInput;
		}

		// Token: 0x040002DE RID: 734
		private readonly string rawInput;
	}
}
