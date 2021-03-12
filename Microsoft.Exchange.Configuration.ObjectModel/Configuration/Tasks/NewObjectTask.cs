using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200007B RID: 123
	public abstract class NewObjectTask<TDataObject> : NewTaskBase<TDataObject> where TDataObject : ConfigObject, new()
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000115D5 File Offset: 0x0000F7D5
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x000115DD File Offset: 0x0000F7DD
		protected TDataObject Instance
		{
			get
			{
				return this.DataObject;
			}
			set
			{
				this.DataObject = value;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000115E8 File Offset: 0x0000F7E8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			try
			{
				string format = "Identity='{0}'";
				TDataObject dataObject = this.DataObject;
				string searchExpr = string.Format(format, dataObject.Identity);
				ConfigObject[] array = ((DataSourceManager)base.DataSession).Find(typeof(TDataObject), searchExpr, false, null);
				if (array != null)
				{
					base.WriteError(new ManagementObjectAlreadyExistsException(Strings.ExceptionObjectAlreadyExists), (ErrorCategory)1003, null);
				}
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			if (base.HasErrors)
			{
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00011690 File Offset: 0x0000F890
		protected override IConfigDataProvider CreateSession()
		{
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(typeof(TDataObject), "Identity");
			return dataSourceManagers[0];
		}
	}
}
