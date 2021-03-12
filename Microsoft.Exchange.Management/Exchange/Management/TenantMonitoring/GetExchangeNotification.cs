using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.CommonHelpProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CE9 RID: 3305
	[Cmdlet("Get", "ExchangeNotification")]
	public sealed class GetExchangeNotification : GetTenantADObjectWithIdentityTaskBase<NotificationIdParameter, Notification>
	{
		// Token: 0x1700277D RID: 10109
		// (get) Token: 0x06007F0E RID: 32526 RVA: 0x00206C66 File Offset: 0x00204E66
		// (set) Token: 0x06007F0F RID: 32527 RVA: 0x00206C7D File Offset: 0x00204E7D
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700277E RID: 10110
		// (get) Token: 0x06007F10 RID: 32528 RVA: 0x00206C90 File Offset: 0x00204E90
		// (set) Token: 0x06007F11 RID: 32529 RVA: 0x00206C98 File Offset: 0x00204E98
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x1700277F RID: 10111
		// (get) Token: 0x06007F12 RID: 32530 RVA: 0x00206CA1 File Offset: 0x00204EA1
		// (set) Token: 0x06007F13 RID: 32531 RVA: 0x00206CA9 File Offset: 0x00204EA9
		[Parameter(Mandatory = false)]
		public SwitchParameter ShowDuplicates
		{
			get
			{
				return this.showDuplicates;
			}
			set
			{
				this.showDuplicates = value;
			}
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x00206CB4 File Offset: 0x00204EB4
		protected override IConfigDataProvider CreateSession()
		{
			ADUser adUser = (ADUser)base.GetDataObject<ADUser>(GetExchangeNotification.FederatedMailboxId, base.TenantGlobalCatalogSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(GetExchangeNotification.FederatedMailboxId.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(GetExchangeNotification.FederatedMailboxId.ToString())));
			return new NotificationDataProvider(adUser, base.SessionSettings);
		}

		// Token: 0x17002780 RID: 10112
		// (get) Token: 0x06007F15 RID: 32533 RVA: 0x00206D0E File Offset: 0x00204F0E
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return new Unlimited<uint>(50U);
			}
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x00206D18 File Offset: 0x00204F18
		protected override OrganizationId ResolveCurrentOrganization()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 125, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\TenantMonitoring\\GetExchangeNotification.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = true;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = false;
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.GetDataObject<ExchangeConfigurationUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			if (exchangeConfigurationUnit.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				throw new ArgumentException("OrganizationId.ForestWideOrgId is not supported.", "Organization");
			}
			return exchangeConfigurationUnit.OrganizationId;
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x00206DCB File Offset: 0x00204FCB
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(StoragePermanentException).IsInstanceOfType(exception);
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x00206DE8 File Offset: 0x00204FE8
		protected override void WriteResult(IConfigurable dataObject)
		{
			base.WriteResult(this.ConvertDataObjectToPresentationObject(dataObject));
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x00206DF8 File Offset: 0x00204FF8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			Notification notification = dataObject as Notification;
			if (notification == null)
			{
				return base.ConvertDataObjectToPresentationObject(dataObject);
			}
			string eventCategory;
			string localizedEventMessageAndCategory = this.GetLocalizedEventMessageAndCategory(notification, CultureInfo.CurrentUICulture, out eventCategory);
			notification.EventMessage = localizedEventMessageAndCategory;
			notification.EventCategory = eventCategory;
			notification.EventHelpUrl = this.GetHelpUrlForNotification(notification, CultureInfo.CurrentUICulture);
			return notification;
		}

		// Token: 0x17002781 RID: 10113
		// (get) Token: 0x06007F1A RID: 32538 RVA: 0x00206E48 File Offset: 0x00205048
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, TenantNotificationMessageSchema.MonitoringCreationTimeUtc, ExDateTime.UtcNow.Subtract(TimeSpan.FromDays(1.0)));
			}
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x00206E80 File Offset: 0x00205080
		protected override IEnumerable<Notification> GetPagedData()
		{
			return default(GetExchangeNotification.NotificationMerger).RemoveDuplicatesAndSort(base.GetPagedData(), !this.ShowDuplicates);
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x00206EB0 File Offset: 0x002050B0
		private string GetLocalizedEventMessageAndCategory(Notification notification, CultureInfo language, out string category)
		{
			string text = string.Empty;
			category = string.Empty;
			try
			{
				text = Utils.GetResourcesFilePath(notification.EventSource);
				if (string.IsNullOrEmpty(text))
				{
					base.WriteDebug(Strings.TenantNotificationDebugEventResourceFileNotFound(notification.EventSource));
					return string.Empty;
				}
			}
			catch (IOException exception)
			{
				this.WriteError(exception, ErrorCategory.ResourceUnavailable, notification.EventSource, false);
				return string.Empty;
			}
			catch (SecurityException exception2)
			{
				this.WriteError(exception2, ErrorCategory.ResourceUnavailable, notification.EventSource, false);
				return string.Empty;
			}
			catch (UnauthorizedAccessException exception3)
			{
				this.WriteError(exception3, ErrorCategory.ResourceUnavailable, notification.EventSource, false);
				return string.Empty;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentUICulture))
			{
				string localizedEventMessageAndCategory = Utils.GetLocalizedEventMessageAndCategory(text, (uint)notification.EventInstanceId, (uint)notification.EventCategoryId, notification.InsertionStrings, language, stringWriter, out category);
				if (base.IsDebugOn)
				{
					string text2 = stringWriter.ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						base.WriteDebug(text2);
					}
				}
				result = localizedEventMessageAndCategory;
			}
			return result;
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x00206FE4 File Offset: 0x002051E4
		private string GetHelpUrlForNotification(Notification notification, CultureInfo language)
		{
			string text = (language != null) ? language.Name : CultureInfo.InvariantCulture.Name;
			Uri uri = HelpProvider.ConstructTenantEventUrl(notification.EventSource, string.Empty, notification.EventDisplayId.ToString(CultureInfo.InvariantCulture), text);
			if (uri == null)
			{
				base.WriteDebug(Strings.TenantNotificationDebugHelpProviderReturnedEmptyUrl(notification.EventSource, notification.EventDisplayId, text));
				return "http://help.outlook.com/ms.exch.evt.default.aspx";
			}
			return uri.ToString();
		}

		// Token: 0x04003E48 RID: 15944
		private static readonly MailboxIdParameter FederatedMailboxId = new MailboxIdParameter("FederatedEmail.4c1f4d8b-8179-4148-93bf-00a95fa1e042");

		// Token: 0x04003E49 RID: 15945
		private SwitchParameter showDuplicates;

		// Token: 0x02000CEA RID: 3306
		private struct NotificationMerger : IComparer<Notification>
		{
			// Token: 0x06007F20 RID: 32544 RVA: 0x00207074 File Offset: 0x00205274
			public int Compare(Notification x, Notification y)
			{
				if (x == null)
				{
					throw new ArgumentNullException("x");
				}
				if (y == null)
				{
					throw new ArgumentNullException("y");
				}
				return y.CreationTimeUtc.CompareTo(x.CreationTimeUtc);
			}

			// Token: 0x06007F21 RID: 32545 RVA: 0x002070B1 File Offset: 0x002052B1
			internal IEnumerable<Notification> RemoveDuplicatesAndSort(IEnumerable<Notification> notifications, bool removeDuplicates)
			{
				if (notifications == null)
				{
					return null;
				}
				if (!removeDuplicates)
				{
					return this.SortByCreationTimeDescending(notifications);
				}
				return this.SortByCreationTimeDescending(this.RemoveDuplicates(notifications));
			}

			// Token: 0x06007F22 RID: 32546 RVA: 0x002072D4 File Offset: 0x002054D4
			private IEnumerable<Notification> RemoveDuplicates(IEnumerable<Notification> notifications)
			{
				IEnumerable<IGrouping<long, Notification>> groupedByEventSourceAndId = from n in notifications
				group n by GetExchangeNotification.NotificationMerger.HashForDuplicateRemoval(n);
				foreach (IGrouping<long, Notification> group in groupedByEventSourceAndId)
				{
					yield return group.Aggregate(delegate(Notification oldest, Notification current)
					{
						if (!(current.CreationTimeUtc < oldest.CreationTimeUtc))
						{
							return oldest;
						}
						return current;
					});
				}
				yield break;
			}

			// Token: 0x06007F23 RID: 32547 RVA: 0x00207300 File Offset: 0x00205500
			private IEnumerable<Notification> SortByCreationTimeDescending(IEnumerable<Notification> notifications)
			{
				return notifications.OrderBy((Notification n) => n, this);
			}

			// Token: 0x06007F24 RID: 32548 RVA: 0x00207330 File Offset: 0x00205530
			private static long HashForDuplicateRemoval(Notification notification)
			{
				return notification.ComputeHashCodeForDuplicateDetection();
			}
		}
	}
}
