using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F0 RID: 1008
	[XmlInclude(typeof(GetDelegateRequest))]
	[XmlType("BaseDelegateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(RemoveDelegateRequest))]
	[XmlInclude(typeof(AddDelegateRequest))]
	[XmlInclude(typeof(UpdateDelegateRequest))]
	public abstract class BaseDelegateRequest : BaseRequest
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x0009E22F File Offset: 0x0009C42F
		protected BaseDelegateRequest(bool isWriteOperation = true)
		{
			this.isWriteOperation = isWriteOperation;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0009E23E File Offset: 0x0009C43E
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x0009E246 File Offset: 0x0009C446
		[XmlElement("Mailbox")]
		public EmailAddressWrapper Mailbox
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0009E250 File Offset: 0x0009C450
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			string text = this.Mailbox.EmailAddress;
			MailboxIdServerInfo result = null;
			if (Util.IsValidSmtpAddress(text))
			{
				result = MailboxIdServerInfo.Create(text);
			}
			return result;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0009E27B File Offset: 0x0009C47B
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			if (this.resourceKeys == null)
			{
				this.resourceKeys = base.GetResourceKeysFromProxyInfo(this.isWriteOperation, callContext);
			}
			return this.resourceKeys;
		}

		// Token: 0x040012B8 RID: 4792
		private readonly bool isWriteOperation;

		// Token: 0x040012B9 RID: 4793
		private EmailAddressWrapper emailAddress;

		// Token: 0x040012BA RID: 4794
		private ResourceKey[] resourceKeys;
	}
}
