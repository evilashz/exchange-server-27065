using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000408 RID: 1032
	[XmlType("CreateManagedFolderRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateManagedFolderRequest : BaseRequest
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0009F3FE File Offset: 0x0009D5FE
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0009F406 File Offset: 0x0009D606
		[XmlArray(ElementName = "FolderNames", IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "FolderName", IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] FolderNames
		{
			get
			{
				return this.folderNames;
			}
			set
			{
				this.folderNames = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0009F40F File Offset: 0x0009D60F
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x0009F417 File Offset: 0x0009D617
		[XmlAnyElement(Name = "Mailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlElement Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0009F420 File Offset: 0x0009D620
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateManagedFolder(callContext, this);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0009F429 File Offset: 0x0009D629
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoFromMailboxElement(callContext, this.Mailbox);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0009F437 File Offset: 0x0009D637
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}

		// Token: 0x04001333 RID: 4915
		internal const string FolderNameElementName = "FolderName";

		// Token: 0x04001334 RID: 4916
		internal const string FolderNamesElementName = "FolderNames";

		// Token: 0x04001335 RID: 4917
		internal const string MailboxElementName = "Mailbox";

		// Token: 0x04001336 RID: 4918
		private string[] folderNames;

		// Token: 0x04001337 RID: 4919
		private XmlElement mailbox;
	}
}
