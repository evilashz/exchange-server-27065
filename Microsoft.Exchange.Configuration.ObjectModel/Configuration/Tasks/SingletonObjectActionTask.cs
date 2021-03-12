using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000041 RID: 65
	public abstract class SingletonObjectActionTask<TDataObject> : SetTaskBase<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000BED5 File Offset: 0x0000A0D5
		protected virtual QueryFilter InternalFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		protected virtual bool DeepSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IEnumerable<TDataObject> enumerable = null;
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(TDataObject), this.InternalFilter, this.RootId, this.DeepSearch));
			try
			{
				enumerable = base.DataSession.FindPaged<TDataObject>(this.InternalFilter, this.RootId, this.DeepSearch, null, 0);
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			if (enumerable == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ExceptionObjectNotFound(typeof(TDataObject).ToString())), (ErrorCategory)1003, null);
			}
			IConfigurable configurable = null;
			using (IEnumerator<TDataObject> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					configurable = enumerator.Current;
					if (enumerator.MoveNext())
					{
						base.WriteError(new ManagementObjectAmbiguousException(Strings.ExceptionObjectAmbiguous(typeof(TDataObject).ToString())), (ErrorCategory)1003, null);
					}
				}
				else
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ExceptionObjectNotFound(typeof(TDataObject).ToString())), (ErrorCategory)1003, null);
				}
			}
			ADObject adobject = configurable as ADObject;
			if (adobject != null)
			{
				base.CurrentOrganizationId = adobject.OrganizationId;
			}
			if (base.CurrentObjectIndex == 0)
			{
				this.ResolveLocalSecondaryIdentities();
				if (base.HasErrors)
				{
					return null;
				}
			}
			TaskLogger.LogExit();
			return configurable;
		}
	}
}
