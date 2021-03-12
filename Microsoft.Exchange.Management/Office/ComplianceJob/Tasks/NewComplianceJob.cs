using System;
using System.Data.SqlClient;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000141 RID: 321
	public abstract class NewComplianceJob<TDataObject> : NewTenantADTaskBase<TDataObject> where TDataObject : ComplianceJob, new()
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000358EF File Offset: 0x00033AEF
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x00035902 File Offset: 0x00033B02
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return this.objectToSave.Name;
			}
			set
			{
				this.objectToSave.Name = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00035916 File Offset: 0x00033B16
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0003592E File Offset: 0x00033B2E
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string[] ExchangeBinding
		{
			get
			{
				return this.objectToSave.ExchangeBindings.ToArray();
			}
			set
			{
				this.objectToSave.ExchangeBindings = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00035947 File Offset: 0x00033B47
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0003595F File Offset: 0x00033B5F
		[Parameter(Mandatory = false)]
		public string[] PublicFolderBinding
		{
			get
			{
				return this.objectToSave.PublicFolderBindings.ToArray();
			}
			set
			{
				this.objectToSave.PublicFolderBindings = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00035978 File Offset: 0x00033B78
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x00035990 File Offset: 0x00033B90
		[Parameter(Mandatory = false)]
		public string[] SharePointBinding
		{
			get
			{
				return this.objectToSave.SharePointBindings.ToArray();
			}
			set
			{
				this.objectToSave.SharePointBindings = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x000359A9 File Offset: 0x00033BA9
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x000359BC File Offset: 0x00033BBC
		[Parameter(Mandatory = false)]
		public bool AllExchangeBindings
		{
			get
			{
				return this.objectToSave.AllExchangeBindings;
			}
			set
			{
				this.objectToSave.AllExchangeBindings = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000359D0 File Offset: 0x00033BD0
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x000359E3 File Offset: 0x00033BE3
		[Parameter(Mandatory = false)]
		public bool AllPublicFolderBindings
		{
			get
			{
				return this.objectToSave.AllPublicFolderBindings;
			}
			set
			{
				this.objectToSave.AllPublicFolderBindings = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x000359F7 File Offset: 0x00033BF7
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x00035A0A File Offset: 0x00033C0A
		[Parameter(Mandatory = false)]
		public bool AllSharePointBindings
		{
			get
			{
				return this.objectToSave.AllSharePointBindings;
			}
			set
			{
				this.objectToSave.AllSharePointBindings = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00035A1E File Offset: 0x00033C1E
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x00035A31 File Offset: 0x00033C31
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return this.objectToSave.Description;
			}
			set
			{
				this.objectToSave.Description = value;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00035A45 File Offset: 0x00033C45
		public NewComplianceJob()
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00035A58 File Offset: 0x00033C58
		protected override void InternalProcessRecord()
		{
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00035A5C File Offset: 0x00033C5C
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			this.PrepareDataObjectToSave();
			if (this.objectToSave != null)
			{
				try
				{
					this.PreSaveValidate(this.objectToSave);
					if (base.HasErrors)
					{
						return;
					}
					if (this.objectToSave.Identity != null)
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(this.objectToSave, base.DataSession, typeof(ComplianceJob)));
					}
					base.DataSession.Save(this.objectToSave);
					this.DataObject = this.objectToSave;
					if (!base.HasErrors)
					{
						this.WriteResult();
					}
				}
				catch (SqlException exception)
				{
					base.WriteError(exception, ErrorCategory.WriteError, null);
				}
				catch (ArgumentException exception2)
				{
					base.WriteError(exception2, ErrorCategory.WriteError, null);
				}
				finally
				{
					if (this.objectToSave.Identity != null)
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
					}
				}
			}
			base.InternalEndProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00035B84 File Offset: 0x00033D84
		private void PreSaveValidate(ComplianceJob savedObject)
		{
			if (((ComplianceJobProvider)base.DataSession).FindJobsByName<ComplianceSearch>(savedObject.Name) != null)
			{
				TDataObject dataObject = this.DataObject;
				base.WriteError(new ComplianceSearchNameIsNotUniqueException(dataObject.Name), ErrorCategory.InvalidArgument, savedObject);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00035BCC File Offset: 0x00033DCC
		private void PrepareDataObjectToSave()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			if (!(base.DataSession is ComplianceJobProvider))
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineCreatingUser), ErrorCategory.InvalidOperation, null);
			}
			this.objectToSave.Identity = new ComplianceJobId();
			this.objectToSave.CreatedBy = ((ADObjectId)base.ExchangeRunspaceConfig.ExecutingUserIdentity).Name;
			this.objectToSave.CreatedTime = DateTime.UtcNow;
			this.objectToSave.LastModifiedTime = DateTime.UtcNow;
			this.objectToSave.JobObjectVersion = ComplianceJobObjectVersion.Version1;
			this.objectToSave.JobRunId = CombGuidGenerator.NewGuid();
			this.objectToSave.JobStatus = ComplianceJobStatus.NotStarted;
		}

		// Token: 0x0400059F RID: 1439
		protected TDataObject objectToSave = Activator.CreateInstance<TDataObject>();
	}
}
