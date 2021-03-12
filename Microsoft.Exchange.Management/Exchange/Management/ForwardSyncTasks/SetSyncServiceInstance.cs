using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200037B RID: 891
	[Cmdlet("Set", "SyncServiceInstance", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetSyncServiceInstance : SetADTaskBase<ServiceInstanceIdParameter, SyncServiceInstance, SyncServiceInstance>
	{
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x00086F86 File Offset: 0x00085186
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x00086F9D File Offset: 0x0008519D
		[Parameter(Mandatory = false)]
		public AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields[SyncServiceInstanceSchema.AccountPartition];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.AccountPartition] = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00086FB0 File Offset: 0x000851B0
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x00086FB8 File Offset: 0x000851B8
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00086FC1 File Offset: 0x000851C1
		protected override ObjectId RootId
		{
			get
			{
				return SyncServiceInstance.GetMsoSyncRootContainer();
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00086FC8 File Offset: 0x000851C8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSyncServiceInstance(this.DataObject.Name);
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00086FDA File Offset: 0x000851DA
		protected override IConfigDataProvider CreateSession()
		{
			return ForwardSyncDataAccessHelper.CreateSession(false);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00086FE4 File Offset: 0x000851E4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			SyncServiceInstance syncServiceInstance = (SyncServiceInstance)base.PrepareDataObject();
			if (syncServiceInstance.IsChanged(ADObjectSchema.Name))
			{
				string text;
				syncServiceInstance.TryGetOriginalValue<string>(ADObjectSchema.Name, out text);
				if (!this.Force && !SetSyncServiceInstance.IsServiceInstanceEmpty(syncServiceInstance))
				{
					base.WriteError(new InvalidOperationException(Strings.CannotChangeServiceInstanceNameError(text)), ErrorCategory.InvalidOperation, text);
				}
			}
			if (syncServiceInstance.IsChanged(SyncServiceInstanceSchema.ForwardSyncConfigurationXML))
			{
				string forwardSyncConfigurationXML = syncServiceInstance.ForwardSyncConfigurationXML;
				try
				{
					XElement.Parse(forwardSyncConfigurationXML);
				}
				catch (XmlException ex)
				{
					base.WriteError(new InvalidOperationException(Strings.InvalidForwardSyncConfigurationError(ex.Message)), ErrorCategory.InvalidOperation, ex.Message);
				}
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.AccountPartition))
			{
				if (this.AccountPartition != null)
				{
					AccountPartition accountPartition = (AccountPartition)base.GetDataObject<AccountPartition>(this.AccountPartition, this.ConfigurationSession, null, null, null);
					syncServiceInstance.AccountPartition = accountPartition.Id;
				}
				else
				{
					syncServiceInstance.AccountPartition = null;
				}
			}
			TaskLogger.LogExit();
			return syncServiceInstance;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00087100 File Offset: 0x00085300
		internal static bool IsServiceInstanceEmpty(SyncServiceInstance syncServiceInstance)
		{
			TaskLogger.LogEnter();
			CookieManager cookieManager = CookieManagerFactory.Default.GetCookieManager(ForwardSyncCookieType.RecipientIncremental, syncServiceInstance.Name, 1, TimeSpan.FromMinutes(30.0));
			byte[] array = cookieManager.ReadCookie();
			if (array != null && array.Length > 0)
			{
				return false;
			}
			CookieManager cookieManager2 = CookieManagerFactory.Default.GetCookieManager(ForwardSyncCookieType.CompanyIncremental, syncServiceInstance.Name, 1, TimeSpan.FromMinutes(30.0));
			array = cookieManager2.ReadCookie();
			if (array != null && array.Length > 0)
			{
				return false;
			}
			ForwardSyncDataAccessHelper forwardSyncDataAccessHelper = new ForwardSyncDataAccessHelper(syncServiceInstance.Name);
			IEnumerable<FailedMSOSyncObject> source = forwardSyncDataAccessHelper.FindDivergence(null);
			if (source.Any<FailedMSOSyncObject>())
			{
				return false;
			}
			TaskLogger.LogExit();
			return true;
		}
	}
}
