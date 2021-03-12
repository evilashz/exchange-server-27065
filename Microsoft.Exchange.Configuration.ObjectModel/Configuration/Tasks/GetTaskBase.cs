using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000058 RID: 88
	public abstract class GetTaskBase<TDataObject> : DataAccessTask<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000DB78 File Offset: 0x0000BD78
		protected internal uint WriteObjectCount
		{
			get
			{
				return this.matchCount;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000DB80 File Offset: 0x0000BD80
		protected virtual QueryFilter InternalFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000DB83 File Offset: 0x0000BD83
		protected virtual bool DeepSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000DB86 File Offset: 0x0000BD86
		protected virtual SortBy InternalSortBy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000DB89 File Offset: 0x0000BD89
		protected virtual Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000DB90 File Offset: 0x0000BD90
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000DBBB File Offset: 0x0000BDBB
		protected Unlimited<uint> InternalResultSize
		{
			get
			{
				Unlimited<uint>? unlimited = this.internalResultSize;
				if (unlimited == null)
				{
					return this.DefaultResultSize;
				}
				return unlimited.GetValueOrDefault();
			}
			set
			{
				this.internalResultSize = new Unlimited<uint>?(value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000DBC9 File Offset: 0x0000BDC9
		protected virtual int PageSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DBD3 File Offset: 0x0000BDD3
		protected void IncreaseMatchCount()
		{
			this.matchCount += 1U;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DBE3 File Offset: 0x0000BDE3
		internal override IRecipientSession CreateTenantGlobalCatalogSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateTenantGlobalCatalogSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateTenantGlobalCatalogSession(sessionSettings);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DC02 File Offset: 0x0000BE02
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal)
			{
				return base.CreateConfigurationSession(ADSessionSettings.RescopeToSubtree(sessionSettings));
			}
			return base.CreateConfigurationSession(sessionSettings);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DC21 File Offset: 0x0000BE21
		[Obsolete("Use WriteResult(IConfigurable dataObject) instead.")]
		internal new void WriteObject(object sendToPipeline)
		{
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		[Obsolete("Use WriteResult<T>(IEnumerable<T> dataObjects) instead.")]
		internal new void WriteObject(object sendToPipeline, bool enumerateCollection)
		{
			base.WriteObject(sendToPipeline, enumerateCollection);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DC34 File Offset: 0x0000BE34
		internal virtual void AdjustPageSize(IPageInformation pageInfo)
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000DC36 File Offset: 0x0000BE36
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000DC52 File Offset: 0x0000BE52
		protected string RequestQueryFilterInGetTasks
		{
			get
			{
				return (string)base.CurrentTaskContext.Items["Log_RequestQueryFilterInGetTasks"];
			}
			set
			{
				base.CurrentTaskContext.Items["Log_RequestQueryFilterInGetTasks"] = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000DC6A File Offset: 0x0000BE6A
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000DC86 File Offset: 0x0000BE86
		protected string InternalQueryFilterInGetTasks
		{
			get
			{
				return (string)base.CurrentTaskContext.Items["Log_InternalQueryFilterInGetTasks"];
			}
			set
			{
				base.CurrentTaskContext.Items["Log_InternalQueryFilterInGetTasks"] = value;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.matchCount = 0U;
			TaskLogger.LogExit();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		protected virtual IEnumerable<TDataObject> GetPagedData()
		{
			QueryFilter internalFilter = this.InternalFilter;
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(TDataObject), internalFilter, this.RootId, this.DeepSearch));
			return base.DataSession.FindPaged<TDataObject>(internalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DD18 File Offset: 0x0000BF18
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				IEnumerable<TDataObject> pagedData = this.GetPagedData();
				ADPagedReader<TDataObject> adpagedReader = pagedData as ADPagedReader<TDataObject>;
				if (adpagedReader != null)
				{
					this.RequestQueryFilterInGetTasks = adpagedReader.LdapFilter;
					base.WriteVerbose(Strings.VerboseRequestFilterInGetTask(this.RequestQueryFilterInGetTasks));
				}
				if (this.InternalFilter != null)
				{
					this.InternalQueryFilterInGetTasks = this.InternalFilter.ToString();
					base.WriteVerbose(Strings.VerboseInternalQueryFilterInGetTasks(this.InternalQueryFilterInGetTasks));
				}
				this.pageInfo = (pagedData as IPageInformation);
				this.WriteResult<TDataObject>(pagedData);
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		protected virtual void WriteResult(IConfigurable dataObject)
		{
			if (dataObject is PagedPositionInfo)
			{
				base.WriteObject(dataObject);
				return;
			}
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			if (this.InternalResultSize.IsUnlimited || this.InternalResultSize.Value > this.matchCount)
			{
				ConfigurableObject configurableObject = dataObject as ConfigurableObject;
				if (configurableObject != null)
				{
					configurableObject.InitializeSchema();
				}
				ValidationError[] array = dataObject.Validate();
				base.WriteObject(dataObject);
				if (array != null && array.Length > 0)
				{
					if (dataObject.Identity != null)
					{
						this.WriteWarning(Strings.ErrorObjectHasValidationErrorsWithId(dataObject.Identity));
					}
					else
					{
						this.WriteWarning(Strings.ErrorObjectHasValidationErrors);
					}
					foreach (ValidationError validationError in array)
					{
						this.WriteWarning(validationError.Description);
					}
				}
			}
			else if (this.InternalResultSize.Value == this.matchCount)
			{
				if (this.internalResultSize == null)
				{
					this.WriteWarning(Strings.WarningDefaultResultSizeReached(this.InternalResultSize.Value.ToString()));
				}
				else
				{
					this.WriteWarning(Strings.WarningMoreResultsAvailable);
				}
			}
			this.matchCount += 1U;
			TaskLogger.LogExit();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000DF00 File Offset: 0x0000C100
		protected virtual void WriteResult<T>(IEnumerable<T> dataObjects) where T : IConfigurable
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObjects
			});
			if (dataObjects != null)
			{
				using (IEnumerator<T> enumerator = dataObjects.GetEnumerator())
				{
					bool flag = false;
					if (!base.Stopping)
					{
						if (!this.InternalResultSize.IsUnlimited)
						{
							if (this.InternalResultSize.Value < this.matchCount)
							{
								goto IL_163;
							}
						}
						try
						{
							flag = enumerator.MoveNext();
						}
						finally
						{
							base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
						}
						base.WriteVerbose(Strings.VerboseWriteResultSize(this.InternalResultSize.ToString()));
					}
					IL_163:
					while (!base.Stopping && (this.InternalResultSize.IsUnlimited || this.InternalResultSize.Value >= this.matchCount) && flag)
					{
						if (!this.InternalResultSize.IsUnlimited && this.InternalResultSize.Value == this.matchCount + 1U && this.pageInfo != null && this.pageInfo.MorePagesAvailable != null && this.pageInfo.MorePagesAvailable.Value)
						{
							this.WriteResult(enumerator.Current);
						}
						IConfigurable dataObject = enumerator.Current;
						this.WriteResult(dataObject);
						if (!base.Stopping && (this.InternalResultSize.IsUnlimited || this.InternalResultSize.Value >= this.matchCount))
						{
							if (this.pageInfo != null)
							{
								this.AdjustPageSize(this.pageInfo);
							}
							flag = enumerator.MoveNext();
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x040000EE RID: 238
		private const string RelatedCmdletDataRedactionConfigFilePath = "ClientAccess\\PowerShell-Proxy\\CmdletDataRedaction.xml";

		// Token: 0x040000EF RID: 239
		private const int MicroDelayWriteCount = 50;

		// Token: 0x040000F0 RID: 240
		private Unlimited<uint>? internalResultSize;

		// Token: 0x040000F1 RID: 241
		private uint matchCount;

		// Token: 0x040000F2 RID: 242
		private IPageInformation pageInfo;
	}
}
