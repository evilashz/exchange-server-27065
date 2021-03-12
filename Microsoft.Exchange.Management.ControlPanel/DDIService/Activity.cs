using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000101 RID: 257
	[DictionaryKeyProperty("Name")]
	public abstract class Activity
	{
		// Token: 0x06001F25 RID: 7973 RVA: 0x0005DD31 File Offset: 0x0005BF31
		public Activity() : this(string.Empty)
		{
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0005DD3E File Offset: 0x0005BF3E
		public Activity(string name)
		{
			this.Name = name;
			this.ProgressPercent = 0;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0005DD54 File Offset: 0x0005BF54
		protected Activity(Activity activity) : this(activity.Name)
		{
			this.PreAction = activity.PreAction;
			this.PostAction = activity.PostAction;
			this.ErrorBehavior = activity.ErrorBehavior;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0005DD86 File Offset: 0x0005BF86
		public virtual Activity Clone()
		{
			throw new InvalidOperationException("Activity is not allowed to clone!");
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0005DD92 File Offset: 0x0005BF92
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x0005DD9A File Offset: 0x0005BF9A
		public string Name { get; set; }

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0005DDA3 File Offset: 0x0005BFA3
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0005DDAB File Offset: 0x0005BFAB
		[DDIValidCodeBehindMethod]
		public string PreAction { get; set; }

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0005DDB4 File Offset: 0x0005BFB4
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0005DDBC File Offset: 0x0005BFBC
		[DDIValidCodeBehindMethod]
		public string PostAction { get; set; }

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0005DDC5 File Offset: 0x0005BFC5
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0005DDCD File Offset: 0x0005BFCD
		[DefaultValue(ErrorBehavior.Stop)]
		public ErrorBehavior ErrorBehavior { get; set; }

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0005DDD6 File Offset: 0x0005BFD6
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x0005DDE7 File Offset: 0x0005BFE7
		internal IIsInRole RbacChecker
		{
			get
			{
				return this.rbacChecker ?? RbacCheckerWrapper.RbacChecker;
			}
			set
			{
				this.rbacChecker = value;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0005DDF0 File Offset: 0x0005BFF0
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x0005DE01 File Offset: 0x0005C001
		internal IEacHttpContext EacHttpContext
		{
			get
			{
				return this.httpContext ?? Microsoft.Exchange.Management.DDIService.EacHttpContext.Instance;
			}
			set
			{
				this.httpContext = value;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0005DE0A File Offset: 0x0005C00A
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x0005DE1B File Offset: 0x0005C01B
		internal IPSCommandWrapperFactory PowershellFactory
		{
			get
			{
				return this.powershellFactory ?? PSCommandWrapperFactory.Instance;
			}
			set
			{
				this.powershellFactory = value;
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0005DE24 File Offset: 0x0005C024
		public virtual List<DataColumn> GetExtendedColumns()
		{
			return new List<DataColumn>();
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0005DE2C File Offset: 0x0005C02C
		public RunResult RunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			this.ProgressPercent = 0;
			this.DoPreRun(input, dataTable, store, codeBehind);
			RunResult result = this.Run(input, dataTable, store, codeBehind, updateTableDelegate);
			this.DoPostRun(input, dataTable, store, codeBehind);
			this.ProgressPercent = 100;
			return result;
		}

		// Token: 0x06001F39 RID: 7993
		public abstract RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate);

		// Token: 0x06001F3A RID: 7994 RVA: 0x0005DE6D File Offset: 0x0005C06D
		public virtual bool IsRunnable(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			return !AsyncServiceManager.IsCurrentWorkCancelled() && this.HasPermission(input, dataTable, store, null);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0005DE82 File Offset: 0x0005C082
		public virtual PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			return new PowerShellResults[0];
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0005DE8A File Offset: 0x0005C08A
		internal virtual bool HasPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			return true;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0005DE8D File Offset: 0x0005C08D
		internal virtual bool HasOutputVariable(string variable)
		{
			return false;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0005DE90 File Offset: 0x0005C090
		internal virtual IEnumerable<Activity> Find(Func<Activity, bool> predicate)
		{
			List<Activity> source = new List<Activity>
			{
				this
			};
			return source.Where(predicate);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0005DEB4 File Offset: 0x0005C0B4
		internal virtual bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			bool? result = null;
			if (predicate(this))
			{
				result = new bool?(this.HasPermission(input, dataTable, store, updatingVariable));
			}
			return result;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0005DEE8 File Offset: 0x0005C0E8
		internal void DoPostRun(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			if (null != codeBehind && !string.IsNullOrEmpty(this.PostAction))
			{
				DDIHelper.Trace("PostAction: " + this.PostAction);
				MethodInfo method = codeBehind.GetMethod(this.PostAction, new Type[]
				{
					typeof(DataRow),
					typeof(DataTable),
					typeof(DataObjectStore)
				});
				if (method != null)
				{
					method.Invoke(null, new object[]
					{
						input,
						dataTable,
						store
					});
				}
				else
				{
					method = codeBehind.GetMethod(this.PostAction, new Type[]
					{
						typeof(DataRow),
						typeof(DataTable),
						typeof(DataObjectStore),
						typeof(PowerShellResults[])
					});
					if (!(method != null))
					{
						throw new NotImplementedException("The specified method " + this.PostAction + " should implement one of the two signatures: (DataRow, DataTable, DataObjectStore) or (DataRow, DataTable, DataObjectStore, PowerShellResults[]) .");
					}
					method.Invoke(null, new object[]
					{
						input,
						dataTable,
						store,
						this.GetStatusReport(input, dataTable, store)
					});
				}
			}
			this.DoPostRunCore(input, dataTable, store, codeBehind);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0005E038 File Offset: 0x0005C238
		internal void DoPreRun(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			if (null != codeBehind && !string.IsNullOrEmpty(this.PreAction))
			{
				DDIHelper.Trace("PreAction: " + this.PreAction);
				codeBehind.GetMethod(this.PreAction).Invoke(null, new object[]
				{
					input,
					dataTable,
					store
				});
			}
			this.DoPreRunCore(input, dataTable, store, codeBehind);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0005E0A4 File Offset: 0x0005C2A4
		protected virtual void DoPostRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0005E0A6 File Offset: 0x0005C2A6
		protected virtual void DoPreRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		// (set) Token: 0x06001F45 RID: 8005 RVA: 0x0005E0B0 File Offset: 0x0005C2B0
		internal virtual int ProgressPercent { get; set; }

		// Token: 0x06001F46 RID: 8006 RVA: 0x0005E0B9 File Offset: 0x0005C2B9
		internal virtual void SetResultSize(int resultSize)
		{
		}

		// Token: 0x04001C59 RID: 7257
		private IEacHttpContext httpContext;

		// Token: 0x04001C5A RID: 7258
		private IIsInRole rbacChecker;

		// Token: 0x04001C5B RID: 7259
		private IPSCommandWrapperFactory powershellFactory;
	}
}
