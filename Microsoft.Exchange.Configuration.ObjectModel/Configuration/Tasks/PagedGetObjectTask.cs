using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200007E RID: 126
	public abstract class PagedGetObjectTask<T> : Task where T : IConfigurable
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00011866 File Offset: 0x0000FA66
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001187D File Offset: 0x0000FA7D
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Filter")]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				this.innerFilter = new MonadFilter(value, this, this.FilterableObjectSchema).InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000118A8 File Offset: 0x0000FAA8
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x000118BF File Offset: 0x0000FABF
		[Parameter(Mandatory = false)]
		public T BookmarkObject
		{
			get
			{
				return (T)((object)base.Fields["BookmarkObject"]);
			}
			set
			{
				base.Fields["BookmarkObject"] = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000118D7 File Offset: 0x0000FAD7
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x000118EE File Offset: 0x0000FAEE
		[Parameter(Mandatory = false)]
		public int BookmarkIndex
		{
			get
			{
				return (int)base.Fields["BookmarkIndex"];
			}
			set
			{
				base.Fields["BookmarkIndex"] = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00011906 File Offset: 0x0000FB06
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0001191D File Offset: 0x0000FB1D
		[Parameter(Mandatory = false)]
		public bool IncludeBookmark
		{
			get
			{
				return (bool)base.Fields["IncludeBookmark"];
			}
			set
			{
				base.Fields["IncludeBookmark"] = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00011935 File Offset: 0x0000FB35
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001194C File Offset: 0x0000FB4C
		[Parameter(Mandatory = false)]
		public Unlimited<int> ResultSize
		{
			get
			{
				return (Unlimited<int>)base.Fields["ResultSize"];
			}
			set
			{
				base.Fields["ResultSize"] = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00011964 File Offset: 0x0000FB64
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0001197B File Offset: 0x0000FB7B
		[Parameter(Mandatory = false)]
		public bool SearchForward
		{
			get
			{
				return (bool)base.Fields["SearchForward"];
			}
			set
			{
				base.Fields["SearchForward"] = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00011993 File Offset: 0x0000FB93
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x000119AA File Offset: 0x0000FBAA
		[Parameter(Mandatory = false)]
		public QueueViewerSortOrderEntry[] SortOrder
		{
			get
			{
				return (QueueViewerSortOrderEntry[])base.Fields["SortOrder"];
			}
			set
			{
				base.Fields["SortOrder"] = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000119BD File Offset: 0x0000FBBD
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x000119D4 File Offset: 0x0000FBD4
		[Parameter(Mandatory = false)]
		public bool ReturnPageInfo
		{
			get
			{
				return (bool)base.Fields["ReturnPageInfo"];
			}
			set
			{
				base.Fields["ReturnPageInfo"] = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004ED RID: 1261
		internal abstract ObjectSchema FilterableObjectSchema { get; }

		// Token: 0x060004EE RID: 1262 RVA: 0x000119EC File Offset: 0x0000FBEC
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000119F4 File Offset: 0x0000FBF4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields["BookmarkIndex"] == null)
			{
				this.BookmarkIndex = 0;
			}
			if (this.BookmarkIndex > 0 && this.BookmarkObject != null)
			{
				base.ThrowTerminatingError(new InvalidOperationException(Strings.MutuallyExclusiveArguments("BookmarkIndex", "BookmarkObject")), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields["ResultSize"] == null)
			{
				this.ResultSize = 1000;
			}
			if (!this.ResultSize.IsUnlimited && this.ResultSize.Value < 0)
			{
				base.ThrowTerminatingError(new ArgumentOutOfRangeException("ResultSize", this.ResultSize, string.Empty), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields["SearchForward"] == null)
			{
				this.SearchForward = true;
			}
			if (base.Fields["SortOrder"] == null)
			{
				this.SortOrder = null;
			}
			if (base.Fields["IncludeBookmark"] == null)
			{
				this.IncludeBookmark = true;
			}
			if (base.Fields["ReturnPageInfo"] == null)
			{
				this.ReturnPageInfo = false;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011B2C File Offset: 0x0000FD2C
		protected virtual void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteObject(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0400011C RID: 284
		protected QueryFilter innerFilter;
	}
}
