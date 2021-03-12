using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200071C RID: 1820
	[Serializable]
	public sealed class IRMConfigurationValidationResult : ConfigurableObject
	{
		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x00108771 File Offset: 0x00106971
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return IRMConfigurationValidationResult.Schema;
			}
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x00108778 File Offset: 0x00106978
		public IRMConfigurationValidationResult() : base(new SimpleProviderPropertyBag())
		{
			this.overallResult = IRMConfigurationValidationResult.ResultType.OverallPass;
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x0010879E File Offset: 0x0010699E
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x001087B0 File Offset: 0x001069B0
		public string Results
		{
			get
			{
				return (string)this[IRMConfigurationValidationResultSchema.Results];
			}
			private set
			{
				this[IRMConfigurationValidationResultSchema.Results] = value;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x001087BE File Offset: 0x001069BE
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x001087C5 File Offset: 0x001069C5
		internal void SetTask(LocalizedString task)
		{
			this.currentTask = task;
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x001087CE File Offset: 0x001069CE
		internal bool SetSuccessResult(LocalizedString result)
		{
			this.SetResult(result, IRMConfigurationValidationResult.ResultType.Success, null);
			return true;
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x001087DA File Offset: 0x001069DA
		internal bool SetFailureResult(LocalizedString result, Exception ex = null, bool error = true)
		{
			this.SetResult(result, error ? IRMConfigurationValidationResult.ResultType.Error : IRMConfigurationValidationResult.ResultType.Warning, ex);
			if (error)
			{
				this.overallResult = IRMConfigurationValidationResult.ResultType.OverallFail;
			}
			else if (this.overallResult != IRMConfigurationValidationResult.ResultType.OverallFail)
			{
				this.overallResult = IRMConfigurationValidationResult.ResultType.OverallWarning;
			}
			return !error;
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0010880C File Offset: 0x00106A0C
		internal void SetOverallResult()
		{
			IRMConfigurationValidationResult.ValidationResultNode value = new IRMConfigurationValidationResult.ValidationResultNode(this.overallResult);
			this.list.AddLast(value);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x00108834 File Offset: 0x00106A34
		private void SetResult(LocalizedString result, IRMConfigurationValidationResult.ResultType type, Exception ex)
		{
			IRMConfigurationValidationResult.ValidationResultNode value = new IRMConfigurationValidationResult.ValidationResultNode(this.currentTask, result, type, ex);
			this.list.AddLast(value);
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x00108860 File Offset: 0x00106A60
		internal void PrepareFinalOutput(OrganizationId organizationId)
		{
			this[SimpleProviderObjectSchema.Identity] = organizationId.OrganizationalUnit;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IRMConfigurationValidationResult.ValidationResultNode validationResultNode in this.list)
			{
				stringBuilder.AppendLine(validationResultNode.ToString());
			}
			this.Results = stringBuilder.ToString();
			base.ResetChangeTracking();
		}

		// Token: 0x040028F5 RID: 10485
		private static readonly IRMConfigurationValidationResultSchema Schema = ObjectSchema.GetInstance<IRMConfigurationValidationResultSchema>();

		// Token: 0x040028F6 RID: 10486
		private IRMConfigurationValidationResult.ResultType overallResult = IRMConfigurationValidationResult.ResultType.OverallPass;

		// Token: 0x040028F7 RID: 10487
		private LinkedList<IRMConfigurationValidationResult.ValidationResultNode> list = new LinkedList<IRMConfigurationValidationResult.ValidationResultNode>();

		// Token: 0x040028F8 RID: 10488
		private LocalizedString currentTask;

		// Token: 0x0200071D RID: 1821
		private enum ResultType
		{
			// Token: 0x040028FA RID: 10490
			Success,
			// Token: 0x040028FB RID: 10491
			Warning,
			// Token: 0x040028FC RID: 10492
			Error,
			// Token: 0x040028FD RID: 10493
			OverallPass,
			// Token: 0x040028FE RID: 10494
			OverallWarning,
			// Token: 0x040028FF RID: 10495
			OverallFail
		}

		// Token: 0x0200071E RID: 1822
		[Serializable]
		private sealed class ValidationResultNode
		{
			// Token: 0x060040BB RID: 16571 RVA: 0x001088F0 File Offset: 0x00106AF0
			public ValidationResultNode(LocalizedString task, LocalizedString result, IRMConfigurationValidationResult.ResultType type, Exception exception)
			{
				this.Task = task;
				this.Result = result;
				this.Type = type;
				this.Exception = exception;
			}

			// Token: 0x060040BC RID: 16572 RVA: 0x00108915 File Offset: 0x00106B15
			public ValidationResultNode(IRMConfigurationValidationResult.ResultType type)
			{
				this.Task = LocalizedString.Empty;
				this.Result = LocalizedString.Empty;
				this.Type = type;
				this.Exception = null;
			}

			// Token: 0x060040BD RID: 16573 RVA: 0x00108944 File Offset: 0x00106B44
			public override string ToString()
			{
				string text = string.Empty;
				if (this.Exception != null)
				{
					text = string.Format(CultureInfo.CurrentUICulture, "{0}{1}{2}{3}{4}", new object[]
					{
						"----------------------------------------",
						Environment.NewLine,
						this.Exception,
						Environment.NewLine,
						"----------------------------------------"
					});
				}
				switch (this.Type)
				{
				case IRMConfigurationValidationResult.ResultType.Warning:
					return string.Format(CultureInfo.CurrentUICulture, "{0}{1}    - {2}{3}{4}", new object[]
					{
						this.Task,
						Environment.NewLine,
						Strings.InfoWarning(this.Result),
						Environment.NewLine,
						text
					});
				case IRMConfigurationValidationResult.ResultType.Error:
					return string.Format(CultureInfo.CurrentUICulture, "{0}{1}    - {2}{3}{4}", new object[]
					{
						this.Task,
						Environment.NewLine,
						Strings.InfoError(this.Result),
						Environment.NewLine,
						text
					});
				case IRMConfigurationValidationResult.ResultType.OverallPass:
					return string.Format(CultureInfo.CurrentUICulture, "{0}{1}{2}", new object[]
					{
						Environment.NewLine,
						Strings.InfoOverallPass,
						Environment.NewLine
					});
				case IRMConfigurationValidationResult.ResultType.OverallWarning:
					return string.Format(CultureInfo.CurrentUICulture, "{0}{1}{2}", new object[]
					{
						Environment.NewLine,
						Strings.InfoOverallWarning,
						Environment.NewLine
					});
				case IRMConfigurationValidationResult.ResultType.OverallFail:
					return string.Format(CultureInfo.CurrentUICulture, "{0}{1}{2}", new object[]
					{
						Environment.NewLine,
						Strings.InfoOverallFail,
						Environment.NewLine
					});
				}
				return string.Format(CultureInfo.CurrentUICulture, "{0}{1}    - {2}", new object[]
				{
					this.Task,
					Environment.NewLine,
					Strings.InfoSuccess(this.Result)
				});
			}

			// Token: 0x04002900 RID: 10496
			private const string Separator = "----------------------------------------";

			// Token: 0x04002901 RID: 10497
			public readonly LocalizedString Task;

			// Token: 0x04002902 RID: 10498
			public readonly LocalizedString Result;

			// Token: 0x04002903 RID: 10499
			public readonly IRMConfigurationValidationResult.ResultType Type;

			// Token: 0x04002904 RID: 10500
			public readonly Exception Exception;
		}
	}
}
