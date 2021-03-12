using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000062 RID: 98
	public abstract class GetObjectTask<TIdentity, TDataObject> : GetObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ConfigObject, new()
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0000F0A3 File Offset: 0x0000D2A3
		protected GetObjectTask()
		{
			this.FindOne = false;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		protected override IConfigDataProvider CreateSession()
		{
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(typeof(TDataObject), "Identity");
			return dataSourceManagers[0];
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000F0D9 File Offset: 0x0000D2D9
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		[Parameter]
		public string Expression
		{
			get
			{
				return (string)base.Fields["Expression"];
			}
			set
			{
				base.Fields["Expression"] = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000F103 File Offset: 0x0000D303
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000F11A File Offset: 0x0000D31A
		[Parameter]
		public bool FindOne
		{
			get
			{
				return (bool)base.Fields["FindOne"];
			}
			set
			{
				base.Fields["FindOne"] = value;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000F132 File Offset: 0x0000D332
		protected override void InternalValidate()
		{
			if (this.Expression == null)
			{
				this.Expression = "";
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000F148 File Offset: 0x0000D348
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!string.IsNullOrEmpty(this.Expression))
			{
				if (this.FindOne)
				{
					IConfigurable[] array = ((DataSourceManager)base.DataSession).Find(typeof(TDataObject), this.Expression, false, null);
					if (array != null && array.Length > 0)
					{
						this.WriteResult(array[0]);
					}
				}
				else
				{
					IConfigurable[] array2 = ((DataSourceManager)base.DataSession).Find(typeof(TDataObject), this.Expression, true, null);
					if (array2 != null)
					{
						for (int i = 0; i < array2.Length; i++)
						{
							this.WriteResult(array2[i]);
						}
					}
				}
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}
	}
}
