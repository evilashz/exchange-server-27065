using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200000F RID: 15
	[Cmdlet("Get", "MailboxActivityLog", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxActivityLog : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000044E3 File Offset: 0x000026E3
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, Position = 0)]
		public new MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000044F8 File Offset: 0x000026F8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			try
			{
				if (this.Identity != null)
				{
					LocalizedString? localizedString;
					IEnumerable<ADUser> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
					this.WriteResult<ADUser>(dataObjects);
				}
				else
				{
					base.InternalProcessRecord();
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004560 File Offset: 0x00002760
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			try
			{
				if (dataObject != null)
				{
					ADUser user = (ADUser)dataObject;
					ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(user, null);
					using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=Get-MailboxActivityLog"))
					{
						IActivityLog activityLog = ActivityLogFactory.Current.Bind(mailboxSession);
						foreach (Activity activity in activityLog.Query())
						{
							base.WriteResult(new ActivityLogEntryPresentationObject(activity));
						}
					}
				}
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			catch (StoragePermanentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}
	}
}
