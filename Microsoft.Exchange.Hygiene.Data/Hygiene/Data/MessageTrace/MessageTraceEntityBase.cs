using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200014A RID: 330
	internal abstract class MessageTraceEntityBase : ConfigurablePropertyBag
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002750A File Offset: 0x0002570A
		public static byte[] EmptyEmailHashKey
		{
			get
			{
				return MessageTraceEntityBase.emptyEmailHashKey;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00027511 File Offset: 0x00025711
		public static byte[] EmptyEmailDomainHashKey
		{
			get
			{
				return MessageTraceEntityBase.emptyEmailDomainHashKey;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00027518 File Offset: 0x00025718
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002751F File Offset: 0x0002571F
		public virtual void Accept(IMessageTraceVisitor visitor)
		{
			if (visitor == null)
			{
				throw new ArgumentNullException("visitor");
			}
			visitor.Visit(this);
		}

		// Token: 0x06000CAF RID: 3247
		public abstract HygienePropertyDefinition[] GetAllProperties();

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00027536 File Offset: 0x00025736
		internal static byte[] GetEmailHashKey(string emailPrefix, string emailDomain)
		{
			if (emailPrefix == null || emailDomain == null)
			{
				return null;
			}
			return DalHelper.GetSHA1Hash(string.Format("{0}@{1}", emailPrefix.ToLower(), emailDomain.ToLower()));
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002755B File Offset: 0x0002575B
		internal static byte[] GetEmailDomainHashKey(string emailDomain)
		{
			if (emailDomain == null)
			{
				return null;
			}
			return DalHelper.GetSHA1Hash(emailDomain.ToLower());
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002756D File Offset: 0x0002576D
		internal static string StandardizeEmailPrefix(string emailPrefix)
		{
			if (!string.IsNullOrWhiteSpace(emailPrefix))
			{
				return emailPrefix;
			}
			return MessageTraceEntityBase.emptyEmailPrefix;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002757E File Offset: 0x0002577E
		internal static string StandardizeEmailDomain(string emailDomain)
		{
			if (!string.IsNullOrWhiteSpace(emailDomain))
			{
				return emailDomain;
			}
			return MessageTraceEntityBase.emptyEmailDomain;
		}

		// Token: 0x04000653 RID: 1619
		private static string emptyEmailPrefix = "<>";

		// Token: 0x04000654 RID: 1620
		private static string emptyEmailDomain = string.Empty;

		// Token: 0x04000655 RID: 1621
		private static byte[] emptyEmailHashKey = DalHelper.GetSHA1Hash(string.Format("{0}@{1}", MessageTraceEntityBase.emptyEmailPrefix, MessageTraceEntityBase.emptyEmailDomain));

		// Token: 0x04000656 RID: 1622
		private static byte[] emptyEmailDomainHashKey = DalHelper.GetSHA1Hash(MessageTraceEntityBase.emptyEmailDomain);
	}
}
