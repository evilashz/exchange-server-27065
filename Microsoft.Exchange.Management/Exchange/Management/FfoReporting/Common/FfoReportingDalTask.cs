using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Data;
using Microsoft.Exchange.Management.FfoReporting.Providers;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x02000390 RID: 912
	public abstract class FfoReportingDalTask<TOutputObject> : FfoReportingTask<TOutputObject>, IPageableTask where TOutputObject : new()
	{
		// Token: 0x06001FCE RID: 8142 RVA: 0x00088150 File Offset: 0x00086350
		public FfoReportingDalTask()
		{
			this.Page = 1;
			this.PageSize = 1000;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0008816A File Offset: 0x0008636A
		public FfoReportingDalTask(string dalTypeName) : this()
		{
			this.DalObjectTypeName = dalTypeName;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x00088179 File Offset: 0x00086379
		public virtual string DataSessionTypeName
		{
			get
			{
				return "Microsoft.Exchange.Hygiene.Data.MessageTrace.MessageTraceSession";
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x00088180 File Offset: 0x00086380
		public virtual string DataSessionMethodName
		{
			get
			{
				return "FindReportObject";
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001FD2 RID: 8146
		public abstract string DalMonitorEventName { get; }

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00088187 File Offset: 0x00086387
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0008818F File Offset: 0x0008638F
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateIntRange", new object[]
		{
			1,
			1000
		}, ErrorMessage = Strings.IDs.InvalidPage)]
		[QueryParameter("PageQueryDefinition", new string[]
		{

		})]
		public int Page { get; set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x00088198 File Offset: 0x00086398
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x000881A0 File Offset: 0x000863A0
		[CmdletValidator("ValidateIntRange", new object[]
		{
			1,
			5000
		}, ErrorMessage = Strings.IDs.InvalidPageSize)]
		[QueryParameter("PageSizeQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public int PageSize { get; set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000881A9 File Offset: 0x000863A9
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x000881B1 File Offset: 0x000863B1
		protected string DalObjectTypeName { get; set; }

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000881BC File Offset: 0x000863BC
		protected override IReadOnlyList<TOutputObject> AggregateOutput()
		{
			IEnumerable dalRecords = this.GetDalRecords(new FfoReportingDalTask<TOutputObject>.DalRetrievalDelegate(ServiceLocator.Current.GetService<IDalProvider>().GetSingleDataPage), null);
			List<IDataProcessor> list = new List<IDataProcessor>();
			int startIndex = (this.Page - 1) * this.PageSize;
			list.Add(ConversionProcessor.CreatePageable<TOutputObject>(this, startIndex));
			if (base.NeedSuppressingPiiData)
			{
				list.Add(RedactionProcessor.Create<TOutputObject>());
			}
			return DataProcessorDriver.Process<TOutputObject>(dalRecords, list);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00088224 File Offset: 0x00086424
		protected IEnumerable GetDalRecords(FfoReportingDalTask<TOutputObject>.DalRetrievalDelegate getDataPageMethod, QueryFilter filter = null)
		{
			IEnumerable result;
			try
			{
				IEnumerable enumerable = getDataPageMethod(this.DataSessionTypeName, this.DalObjectTypeName, this.DataSessionMethodName, filter ?? this.BuildQueryFilter());
				result = enumerable;
			}
			catch (DalRetrievalException ex)
			{
				base.Diagnostics.SetHealthRed(this.DalMonitorEventName, string.Format("Error retrieving data from the DAL: {0}", ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00088290 File Offset: 0x00086490
		protected QueryFilter BuildQueryFilter()
		{
			List<ComparisonFilter> list = new List<ComparisonFilter>();
			Guid externalDirectoryOrganizationId = ServiceLocator.Current.GetService<IAuthenticationProvider>().GetExternalDirectoryOrganizationId(base.CurrentOrganizationId);
			list.Add(new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition("OrganizationQueryDefinition"), externalDirectoryOrganizationId));
			foreach (Tuple<PropertyInfo, QueryParameter> tuple in Schema.Utilities.GetProperties<QueryParameter>(base.GetType()))
			{
				PropertyInfo item = tuple.Item1;
				QueryParameter item2 = tuple.Item2;
				if (item != null && item2 != null)
				{
					item2.AddFilter(list, item.GetValue(this));
				}
			}
			AndFilter result = new AndFilter(list.ToArray());
			base.Diagnostics.Checkpoint("BuildQueryFilter");
			return result;
		}

		// Token: 0x02000391 RID: 913
		// (Invoke) Token: 0x06001FDD RID: 8157
		protected delegate IEnumerable DalRetrievalDelegate(string dataSessionTypeName, string dalObjectTypeName, string dataSessionMethodName, QueryFilter filter);
	}
}
