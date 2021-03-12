using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200079A RID: 1946
	[Cmdlet("Set", "CalendarNotification", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetCalendarNotification : SetXsoObjectWithIdentityTaskBase<CalendarNotification>
	{
		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x001190CD File Offset: 0x001172CD
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetCalendarNotification(this.Identity.ToString());
			}
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x001190DF File Offset: 0x001172DF
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new VersionedXmlDataProvider(principal, userToken, "Set-CalendarNotification");
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x001190F0 File Offset: 0x001172F0
		protected override void SaveXsoObject(IConfigDataProvider provider, IConfigurable dataObject)
		{
			base.SaveXsoObject(provider, dataObject);
			ADUser dataObject2 = this.DataObject;
			SmsSqmDataPointHelper.AddNotificationConfigDataPoint(SmsSqmSession.Instance, dataObject2.Id, dataObject2.LegacyExchangeDN, (CalendarNotification)dataObject);
		}
	}
}
