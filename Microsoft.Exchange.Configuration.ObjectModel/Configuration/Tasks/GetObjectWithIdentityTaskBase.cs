using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000059 RID: 89
	public abstract class GetObjectWithIdentityTaskBase<TIdentity, TDataObject> : GetTaskBase<TDataObject> where TIdentity : IIdentityParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000E10B File Offset: 0x0000C30B
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public virtual TIdentity Identity
		{
			get
			{
				return (TIdentity)((object)base.Fields["Identity"]);
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E123 File Offset: 0x0000C323
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskWithIdentityModuleFactory();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E12C File Offset: 0x0000C32C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<TDataObject> dataObjects = base.GetDataObjects<TDataObject>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, out localizedString);
				dataObjects = this.RescopeSessionByResultObjects<TDataObject>(dataObjects);
				this.WriteResult<TDataObject>(dataObjects);
				if (!base.HasErrors && base.WriteObjectCount == 0U)
				{
					LocalizedString? localizedString2 = localizedString;
					LocalizedString message;
					if (localizedString2 == null)
					{
						TIdentity identity = this.Identity;
						message = base.GetErrorMessageObjectNotFound(identity.ToString(), typeof(TDataObject).ToString(), (base.DataSession != null) ? base.DataSession.Source : null);
					}
					else
					{
						message = localizedString2.GetValueOrDefault();
					}
					base.WriteError(new ManagementObjectNotFoundException(message), (ErrorCategory)1003, null);
				}
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E21C File Offset: 0x0000C41C
		private IEnumerable<TObject> RescopeSessionByResultObjects<TObject>(IEnumerable<TObject> dataObjects) where TObject : IConfigurable, new()
		{
			if (typeof(ADObject).IsAssignableFrom(typeof(TObject)) && base.DataSession is IDirectorySession)
			{
				List<TObject> list = new List<TObject>(dataObjects);
				if (list.Count > 0)
				{
					ADObject adobject = list[0] as ADObject;
					IDirectorySession directorySession = base.DataSession as IDirectorySession;
					if (directorySession != null && adobject != null && adobject.OrganizationId != null && TaskHelper.ShouldUnderscopeDataSessionToOrganization(directorySession, adobject))
					{
						base.UnderscopeDataSession(adobject.OrganizationId);
						base.CurrentOrganizationId = adobject.OrganizationId;
					}
				}
				dataObjects = list;
			}
			return dataObjects;
		}
	}
}
