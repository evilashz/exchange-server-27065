using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x0200004A RID: 74
	[OutputType(new Type[]
	{
		typeof(PushNotificationProxyPresentationObject)
	})]
	[Cmdlet("Enable", "PushNotificationProxy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnablePushNotificationProxy : ProxyCmdletBaseClass
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000C42E File Offset: 0x0000A62E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableProxy(this.Identity.ToString(), base.CurrentProxyStatus);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000C446 File Offset: 0x0000A646
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000C45D File Offset: 0x0000A65D
		[Parameter(Mandatory = false)]
		public string Uri
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.Url];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.Url] = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000C470 File Offset: 0x0000A670
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000C487 File Offset: 0x0000A687
		[Parameter(Mandatory = false)]
		public string Organization
		{
			get
			{
				return (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				base.Fields[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C49C File Offset: 0x0000A69C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			PushNotificationPublisherSettingsFactory pushNotificationPublisherSettingsFactory = new PushNotificationPublisherSettingsFactory();
			PushNotificationPublisherSettings pushNotificationPublisherSettings = pushNotificationPublisherSettingsFactory.Create(this.DataObject);
			try
			{
				pushNotificationPublisherSettings.Validate();
			}
			catch (PushNotificationConfigurationException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		protected override IConfigurable PrepareDataObject()
		{
			PushNotificationApp pushNotificationApp = (PushNotificationApp)base.PrepareDataObject();
			if (base.Fields.IsModified(PushNotificationAppSchema.AuthenticationKey))
			{
				pushNotificationApp.AuthenticationKey = (string)base.Fields[PushNotificationAppSchema.AuthenticationKey];
			}
			if (base.Fields.IsModified(PushNotificationAppSchema.Url))
			{
				pushNotificationApp.Url = (string)base.Fields[PushNotificationAppSchema.Url];
			}
			pushNotificationApp.Enabled = new bool?(true);
			return pushNotificationApp;
		}
	}
}
