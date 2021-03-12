using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x02000698 RID: 1688
	public abstract class ReportingTaskBase<TReportObject> : Task where TReportObject : ReportObject
	{
		// Token: 0x06003BEA RID: 15338 RVA: 0x00100680 File Offset: 0x000FE880
		protected ReportingTaskBase()
		{
			this.TaskContext = new TaskContext(this);
			this.piiProcessor = new PiiProcessor();
			this.reportContextFactory = new ReportContextFactory();
			if (typeof(TReportObject).IsSubclassOf(typeof(ScaledReportObject)))
			{
				this.reportProvider = new ScaledReportProvider<TReportObject>(this.TaskContext, this.reportContextFactory);
			}
			else
			{
				this.reportProvider = new ReportProvider<TReportObject>(this.TaskContext, this.reportContextFactory);
			}
			this.reportProvider.ReportReceived += this.Report;
			this.reportProvider.StatementLogged += this.LogStatement;
			this.resultSizeDecorator = new ResultSizeDecorator<TReportObject>(this.TaskContext);
			this.AddQueryDecorator(this.resultSizeDecorator);
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x0010074B File Offset: 0x000FE94B
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x00100762 File Offset: 0x000FE962
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

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x0010077A File Offset: 0x000FE97A
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x00100787 File Offset: 0x000FE987
		[Parameter(Mandatory = false)]
		public Expression Expression
		{
			get
			{
				return this.reportProvider.Expression;
			}
			set
			{
				this.reportProvider.Expression = value;
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06003BEF RID: 15343
		protected abstract DataMartType DataMartType { get; }

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x00100795 File Offset: 0x000FE995
		protected bool IsExpressionEnforced
		{
			get
			{
				return this.reportProvider.IsExpressionEnforced;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x001007A2 File Offset: 0x000FE9A2
		protected virtual string ViewName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x001007A5 File Offset: 0x000FE9A5
		// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x001007AD File Offset: 0x000FE9AD
		private protected ITaskContext TaskContext { protected get; private set; }

		// Token: 0x06003BF4 RID: 15348 RVA: 0x001007B6 File Offset: 0x000FE9B6
		protected void AddQueryDecorator(QueryDecorator<TReportObject> queryDecorator)
		{
			this.reportProvider.AddQueryDecorator(queryDecorator);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x001007C4 File Offset: 0x000FE9C4
		protected virtual void ProcessNonPipelineParameter()
		{
			if (base.Fields.IsModified("ResultSize"))
			{
				this.resultSizeDecorator.ResultSize = new Unlimited<int>?(this.ResultSize);
			}
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x001007EE File Offset: 0x000FE9EE
		protected virtual void ProcessPipelineParameter()
		{
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x001007F0 File Offset: 0x000FE9F0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.ProcessNonPipelineParameter();
			this.reportContextFactory.DataMartType = this.DataMartType;
			this.reportContextFactory.ReportType = typeof(TReportObject);
			this.reportContextFactory.ViewName = this.ViewName;
			this.reportProvider.Validate(false);
			this.piiProcessor.SuppressPiiEnabled = base.NeedSuppressingPiiData;
			base.AdditionalLogData = string.Empty;
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x00100868 File Offset: 0x000FEA68
		protected override void InternalProcessRecord()
		{
			this.ProcessPipelineParameter();
			this.reportProvider.Validate(true);
			this.totalCount = 0L;
			this.reportProvider.Execute();
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x0010088F File Offset: 0x000FEA8F
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || e is ManagementObjectAmbiguousException || e is ManagementObjectNotFoundException || e is ReportingException;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x001008B8 File Offset: 0x000FEAB8
		private void Report(TReportObject reportObject)
		{
			this.totalCount += 1L;
			if (!this.IsExpressionEnforced && this.resultSizeDecorator.IsTargetResultSizeReached(this.totalCount))
			{
				this.WriteWarningForTruncateRecords();
				return;
			}
			this.piiProcessor.Supress(reportObject);
			base.WriteObject(reportObject);
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x00100913 File Offset: 0x000FEB13
		private void LogStatement(string key, string statement)
		{
			base.AdditionalLogData += string.Format("[{0}]", statement);
			CmdletLogger.SafeAppendGenericInfo(base.CurrentTaskContext.UniqueId, key, statement);
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x00100944 File Offset: 0x000FEB44
		private void WriteWarningForTruncateRecords()
		{
			if (this.resultSizeDecorator.IsResultSizeReached(this.totalCount))
			{
				this.WriteWarning(Strings.WarningMoreResultsAvailable);
				return;
			}
			if (this.resultSizeDecorator.IsDefaultResultSizeReached(this.totalCount))
			{
				this.WriteWarning(Strings.WarningDefaultResultSizeReached(this.resultSizeDecorator.DefaultResultSize.Value.ToString()));
			}
		}

		// Token: 0x0400270B RID: 9995
		private const string ResultSizeKey = "ResultSize";

		// Token: 0x0400270C RID: 9996
		private readonly ResultSizeDecorator<TReportObject> resultSizeDecorator;

		// Token: 0x0400270D RID: 9997
		private readonly ReportProvider<TReportObject> reportProvider;

		// Token: 0x0400270E RID: 9998
		private readonly ReportContextFactory reportContextFactory;

		// Token: 0x0400270F RID: 9999
		private readonly PiiProcessor piiProcessor;

		// Token: 0x04002710 RID: 10000
		private long totalCount;
	}
}
