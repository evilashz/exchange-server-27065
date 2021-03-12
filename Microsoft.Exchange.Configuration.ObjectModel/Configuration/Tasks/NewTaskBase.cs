using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000070 RID: 112
	public abstract class NewTaskBase<TDataObject> : SetTaskBase<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public NewTaskBase()
		{
			this.bindingInstance = ((default(TDataObject) == null) ? Activator.CreateInstance<TDataObject>() : default(TDataObject));
			this.InitializeDataObject(this.bindingInstance);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000FCF1 File Offset: 0x0000DEF1
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000FD16 File Offset: 0x0000DF16
		protected override TDataObject DataObject
		{
			get
			{
				if (base.Stage == TaskStage.ProcessRecord && base.DataObject != null)
				{
					return base.DataObject;
				}
				return this.bindingInstance;
			}
			set
			{
				if (base.Stage == TaskStage.ProcessRecord)
				{
					base.DataObject = value;
					return;
				}
				this.bindingInstance = value;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000FD30 File Offset: 0x0000DF30
		protected virtual void InitializeDataObject(TDataObject dataObject)
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000FD34 File Offset: 0x0000DF34
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			base.DataObject = ((default(TDataObject) == null) ? Activator.CreateInstance<TDataObject>() : default(TDataObject));
			if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.ProvisionDefaultProperties(this, this.ConvertDataObjectToPresentationObject((default(TDataObject) == null) ? Activator.CreateInstance<TDataObject>() : default(TDataObject)), this.ConvertDataObjectToPresentationObject(base.DataObject), false);
				this.ValidateProvisionedProperties(base.DataObject);
			}
			this.InitializeDataObject(base.DataObject);
			TDataObject dataObject = base.DataObject;
			dataObject.CopyChangesFrom(this.bindingInstance);
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000FDF5 File Offset: 0x0000DFF5
		protected virtual bool SkipWriteResult
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			base.InternalProcessRecord();
			if (!base.HasErrors && !this.SkipWriteResult)
			{
				this.WriteResult();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000FE41 File Offset: 0x0000E041
		protected override IConfigurable PrepareDataObject()
		{
			return base.DataObject;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000FE4E File Offset: 0x0000E04E
		protected virtual void ValidateProvisionedProperties(IConfigurable dataObject)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000FE50 File Offset: 0x0000E050
		protected virtual void WriteResult()
		{
			object[] array = new object[1];
			object[] array2 = array;
			int num = 0;
			TDataObject dataObject = this.DataObject;
			array2[num] = dataObject.Identity;
			TaskLogger.LogEnter(array);
			TDataObject dataObject2 = this.DataObject;
			base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(dataObject2.Identity, base.DataSession, typeof(TDataObject)));
			IConfigurable configurable = null;
			try
			{
				using (TaskPerformanceData.ReadResult.StartRequestTimer())
				{
					IConfigDataProvider dataSession = base.DataSession;
					TDataObject dataObject3 = this.DataObject;
					configurable = dataSession.Read<TDataObject>(dataObject3.Identity);
				}
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			if (configurable == null)
			{
				TDataObject dataObject4 = this.DataObject;
				Exception exception = new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.ResolveIdentityString(dataObject4.Identity), typeof(TDataObject).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
				ErrorCategory category = (ErrorCategory)1003;
				TDataObject dataObject5 = this.DataObject;
				base.WriteError(exception, category, dataObject5.Identity);
			}
			using (TaskPerformanceData.WriteResult.StartRequestTimer())
			{
				this.WriteResult(configurable);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		protected virtual void WriteResult(IConfigurable result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			base.WriteObject(result);
			TaskLogger.LogExit();
		}

		// Token: 0x04000113 RID: 275
		private TDataObject bindingInstance;
	}
}
