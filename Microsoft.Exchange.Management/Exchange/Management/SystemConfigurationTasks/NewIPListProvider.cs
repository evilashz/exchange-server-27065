using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A75 RID: 2677
	public abstract class NewIPListProvider<TDataObject> : NewSystemConfigurationObjectTask<TDataObject> where TDataObject : IPListProvider, new()
	{
		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x0018F91C File Offset: 0x0018DB1C
		// (set) Token: 0x06005F5B RID: 24411 RVA: 0x0018F940 File Offset: 0x0018DB40
		[Parameter(Mandatory = true)]
		public SmtpDomain LookupDomain
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.LookupDomain;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.LookupDomain = value;
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x0018F964 File Offset: 0x0018DB64
		// (set) Token: 0x06005F5D RID: 24413 RVA: 0x0018F988 File Offset: 0x0018DB88
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.Enabled;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Enabled = value;
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x06005F5E RID: 24414 RVA: 0x0018F9AC File Offset: 0x0018DBAC
		// (set) Token: 0x06005F5F RID: 24415 RVA: 0x0018F9D0 File Offset: 0x0018DBD0
		[Parameter(Mandatory = false)]
		public bool AnyMatch
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.AnyMatch;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.AnyMatch = value;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x0018F9F4 File Offset: 0x0018DBF4
		// (set) Token: 0x06005F61 RID: 24417 RVA: 0x0018FA18 File Offset: 0x0018DC18
		[Parameter(Mandatory = false)]
		public IPAddress BitmaskMatch
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.BitmaskMatch;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.BitmaskMatch = value;
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x0018FA3C File Offset: 0x0018DC3C
		// (set) Token: 0x06005F63 RID: 24419 RVA: 0x0018FA60 File Offset: 0x0018DC60
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> IPAddressesMatch
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.IPAddressesMatch;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.IPAddressesMatch = value;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x0018FA84 File Offset: 0x0018DC84
		// (set) Token: 0x06005F65 RID: 24421 RVA: 0x0018FAA8 File Offset: 0x0018DCA8
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.Priority;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Priority = value;
			}
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x0018FACC File Offset: 0x0018DCCC
		protected override IConfigurable PrepareDataObject()
		{
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			tdataObject.SetId((IConfigurationSession)base.DataSession, base.Name);
			return tdataObject;
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x0018FB0C File Offset: 0x0018DD0C
		protected override void InternalProcessRecord()
		{
			IConfigurationSession session = (IConfigurationSession)base.DataSession;
			NewIPListProvider<TDataObject>.AdjustPriorities(session, this.DataObject, false);
			base.InternalProcessRecord();
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x0018FB38 File Offset: 0x0018DD38
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			category = (ErrorCategory)1001;
			if (typeof(ADObjectAlreadyExistsException).IsInstanceOfType(e))
			{
				e = new LocalizedException(Strings.IPListProviderNameExists(base.Name), e);
				return;
			}
			base.TranslateException(ref e, out category);
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x0018FB74 File Offset: 0x0018DD74
		internal static void AdjustPriorities(IConfigurationSession session, TDataObject newProvider, bool providerAlreadyExists)
		{
			ADPagedReader<TDataObject> adpagedReader = session.FindAllPaged<TDataObject>();
			TDataObject[] array = adpagedReader.ReadAllPages();
			int num = int.MaxValue;
			if (providerAlreadyExists)
			{
				foreach (TDataObject tdataObject in array)
				{
					if (tdataObject.Id.ObjectGuid.Equals(newProvider.Id.ObjectGuid))
					{
						num = tdataObject.Priority;
						break;
					}
				}
			}
			int num2 = 0;
			foreach (TDataObject tdataObject2 in array)
			{
				if (tdataObject2.Priority > num2)
				{
					num2 = tdataObject2.Priority;
				}
				if (tdataObject2.Priority >= newProvider.Priority && tdataObject2.Priority < num)
				{
					tdataObject2.Priority++;
					session.Save(tdataObject2);
				}
				else if (tdataObject2.Priority <= newProvider.Priority && tdataObject2.Priority > num)
				{
					tdataObject2.Priority--;
					session.Save(tdataObject2);
				}
			}
			if (num2 < newProvider.Priority)
			{
				newProvider.Priority = (providerAlreadyExists ? num2 : (num2 + 1));
			}
		}
	}
}
