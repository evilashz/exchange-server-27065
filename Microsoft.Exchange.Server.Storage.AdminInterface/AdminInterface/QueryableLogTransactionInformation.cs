using System;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000071 RID: 113
	public class QueryableLogTransactionInformation
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public QueryableLogTransactionInformation(IEnumerable<ILogTransactionInformation> logTransactionInformationList)
		{
			List<string> list = new List<string>(2);
			this.ClientType = string.Empty;
			this.StoreOperation = string.Empty;
			foreach (ILogTransactionInformation logTransactionInformation in logTransactionInformationList)
			{
				LogTransactionInformationBlockType logTransactionInformationBlockType = (LogTransactionInformationBlockType)logTransactionInformation.Type();
				string item = logTransactionInformationBlockType.ToString();
				list.Add(item);
				switch (logTransactionInformationBlockType)
				{
				case LogTransactionInformationBlockType.Unknown:
				case LogTransactionInformationBlockType.ForTestPurposes:
				case LogTransactionInformationBlockType.Digest:
					break;
				case LogTransactionInformationBlockType.Identity:
				{
					LogTransactionInformationIdentity logTransactionInformationIdentity = (LogTransactionInformationIdentity)logTransactionInformation;
					this.MailboxNumber = logTransactionInformationIdentity.MailboxNumber;
					this.ClientType = logTransactionInformationIdentity.ClientType.ToString();
					break;
				}
				case LogTransactionInformationBlockType.AdminRpc:
				{
					LogTransactionInformationAdmin logTransactionInformationAdmin = (LogTransactionInformationAdmin)logTransactionInformation;
					this.StoreOperation = "AdminRpc." + logTransactionInformationAdmin.MethodId;
					break;
				}
				case LogTransactionInformationBlockType.MapiRpc:
				{
					LogTransactionInformationMapi logTransactionInformationMapi = (LogTransactionInformationMapi)logTransactionInformation;
					this.StoreOperation = "MapiRop." + logTransactionInformationMapi.RopId;
					break;
				}
				case LogTransactionInformationBlockType.Task:
				{
					LogTransactionInformationTask logTransactionInformationTask = (LogTransactionInformationTask)logTransactionInformation;
					this.StoreOperation = "Task." + logTransactionInformationTask.TaskTypeId;
					break;
				}
				default:
					throw new DiagnosticQueryException("Unexpected type");
				}
			}
			this.BlockTypes = list.ToArray();
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00011C50 File Offset: 0x0000FE50
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00011C58 File Offset: 0x0000FE58
		public string[] BlockTypes { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00011C61 File Offset: 0x0000FE61
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00011C69 File Offset: 0x0000FE69
		public int MailboxNumber { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00011C72 File Offset: 0x0000FE72
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00011C7A File Offset: 0x0000FE7A
		public string ClientType { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00011C83 File Offset: 0x0000FE83
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00011C8B File Offset: 0x0000FE8B
		public string StoreOperation { get; private set; }
	}
}
