using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200003C RID: 60
	public abstract class SetTaskBase<TDataObject> : DataAccessTask<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B6D8 File Offset: 0x000098D8
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000B6E0 File Offset: 0x000098E0
		protected virtual TDataObject DataObject { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B6E9 File Offset: 0x000098E9
		internal virtual IReferenceErrorReporter ReferenceErrorReporter
		{
			get
			{
				if (this.directReferenceErrorReporter == null)
				{
					this.directReferenceErrorReporter = new DirectReferenceErrorReporter(new Task.ErrorLoggerDelegate(base.WriteError));
				}
				return this.directReferenceErrorReporter;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B710 File Offset: 0x00009910
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				try
				{
					if (disposing)
					{
						IDisposable disposable = this.DataObject as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B764 File Offset: 0x00009964
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				this.DataObject = (TDataObject)((object)this.PrepareDataObject());
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			if (this.DataObject == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(null, typeof(TDataObject).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
			}
			this.ProvisioningUpdateConfigurationObject();
			TDataObject dataObject = this.DataObject;
			string id;
			if (dataObject.Identity != null)
			{
				TDataObject dataObject2 = this.DataObject;
				id = dataObject2.Identity.ToString();
			}
			else
			{
				id = "$null";
			}
			base.WriteVerbose(Strings.VerboseTaskProcessingObject(id));
			base.Validate(this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B84C File Offset: 0x00009A4C
		protected override void PostInternalValidate()
		{
			TaskLogger.LogEnter();
			base.PostInternalValidate();
			this.ReferenceErrorReporter.ReportError(new Task.ErrorLoggerDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x060002E3 RID: 739
		protected abstract IConfigurable PrepareDataObject();

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B878 File Offset: 0x00009A78
		protected override void InternalProvisioningValidation()
		{
			ProvisioningValidationError[] array = ProvisioningLayer.Validate(this, this.ConvertDataObjectToPresentationObject(this.DataObject));
			if (array != null && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					ProvisioningValidationException exception = new ProvisioningValidationException(array[i].Description, array[i].AgentName, array[i].Exception);
					this.WriteError(exception, (ErrorCategory)array[i].ErrorCategory, null, array.Length - 1 == i);
				}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.Validate(this.DataObject);
				if (base.HasErrors)
				{
					return;
				}
				TDataObject dataObject = this.DataObject;
				if (dataObject.Identity != null)
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(this.DataObject, base.DataSession, typeof(TDataObject)));
				}
				using (TaskPerformanceData.SaveResult.StartRequestTimer())
				{
					base.DataSession.Save(this.DataObject);
				}
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			finally
			{
				TDataObject dataObject2 = this.DataObject;
				if (dataObject2.Identity != null)
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000BA04 File Offset: 0x00009C04
		protected virtual string ResolveIdentityString(ObjectId identity)
		{
			if (identity == null)
			{
				return null;
			}
			ADObjectId adobjectId = identity as ADObjectId;
			if (adobjectId != null)
			{
				return adobjectId.DistinguishedName ?? identity.ToString();
			}
			return identity.ToString();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BA37 File Offset: 0x00009C37
		protected virtual void ProvisioningUpdateConfigurationObject()
		{
			ProvisioningLayer.UpdateAffectedIConfigurable(this, this.DataObject, true);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000BA4C File Offset: 0x00009C4C
		protected virtual void ResolveLocalSecondaryIdentities()
		{
		}

		// Token: 0x040000B6 RID: 182
		private bool disposed;

		// Token: 0x040000B7 RID: 183
		private DirectReferenceErrorReporter directReferenceErrorReporter;
	}
}
