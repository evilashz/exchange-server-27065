using System;
using System.IO;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200027B RID: 635
	internal class PiiRedactionModuleBase : TaskIOPipelineBase, ITaskModule
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00051B61 File Offset: 0x0004FD61
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x00051B69 File Offset: 0x0004FD69
		private protected TaskContext CurrentTaskContext { protected get; private set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00051B72 File Offset: 0x0004FD72
		private PiiMap CurrentPiiMap
		{
			get
			{
				if (this.CurrentTaskContext.ExchangeRunspaceConfig != null && this.CurrentTaskContext.ExchangeRunspaceConfig.EnablePiiMap)
				{
					return PiiMapManager.Instance.GetOrAdd(this.CurrentTaskContext.ExchangeRunspaceConfig.PiiMapId);
				}
				return null;
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00051BAF File Offset: 0x0004FDAF
		public PiiRedactionModuleBase(TaskContext context)
		{
			this.CurrentTaskContext = context;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00051BBE File Offset: 0x0004FDBE
		public virtual void Init(ITaskEvent task)
		{
			task.PreInit += this.Task_PreInit;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00051BD2 File Offset: 0x0004FDD2
		public virtual void Dispose()
		{
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00051BD4 File Offset: 0x0004FDD4
		private void Task_PreInit(object sender, EventArgs e)
		{
			if (this.CurrentTaskContext.ExchangeRunspaceConfig != null && this.CurrentTaskContext.ExchangeRunspaceConfig.NeedSuppressingPiiData)
			{
				if (!SuppressingPiiProperty.Initialized)
				{
					this.InitializePiiRedaction();
				}
				this.CurrentTaskContext.CommandShell.PrependTaskIOPipelineHandler(this);
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00051C14 File Offset: 0x0004FE14
		protected IDisposable CreatePiiSuppressionContext(IConfigurable outputObject)
		{
			ConfigurableObject configurableObject = outputObject as ConfigurableObject;
			if (configurableObject == null)
			{
				return null;
			}
			if (configurableObject.SkipPiiRedaction || SuppressingPiiProperty.IsExcludedSchemaType(configurableObject.ObjectSchema.GetType()) || (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.SkipPiiRedactionForForestWideObject.Enabled && TaskHelper.IsForestWideADObject(outputObject as ADObject)))
			{
				return null;
			}
			CmdletLogger.SafeSetLogger(this.CurrentTaskContext.UniqueId, RpsCmdletMetadata.IsOutputObjectRedacted, true);
			return SuppressingPiiContext.Create(true, this.CurrentPiiMap);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00051C98 File Offset: 0x0004FE98
		public override bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			return this.RedactLocString(input, true, out output);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00051CA3 File Offset: 0x0004FEA3
		public override bool WriteDebug(LocalizedString input, out LocalizedString output)
		{
			return this.RedactLocString(input, true, out output);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00051CAE File Offset: 0x0004FEAE
		public override bool WriteWarning(LocalizedString input, string helpUrl, out LocalizedString output)
		{
			return this.RedactLocString(input, false, out output);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00051CB9 File Offset: 0x0004FEB9
		private bool RedactLocString(LocalizedString input, bool hideNonRedacted, out LocalizedString output)
		{
			output = input;
			return input.FormatParameters == null || input.FormatParameters.Count <= 0 || SuppressingPiiData.TryRedactPiiLocString(input, this.CurrentPiiMap, out output) || !hideNonRedacted;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00051CF0 File Offset: 0x0004FEF0
		private void InitializePiiRedaction()
		{
			string text = Path.Combine(ExchangeSetupContext.InstallPath, "ClientAccess\\PowerShell-Proxy\\CmdletDataRedaction.xml");
			Exception ex = null;
			string text2;
			try
			{
				text2 = SuppressingPiiProperty.Initialize(text);
			}
			catch (IOException ex2)
			{
				ex = ex2;
				text2 = string.Format("IOException occurred while loading the configuration file. Please make sure the CmdletDataRedaction.xml is accessible. Expected path: {0}, Details: {1}", text, ex2);
			}
			catch (InvalidOperationException ex3)
			{
				ex = ex3;
				text2 = string.Format("An error occurred during deserializing file {0}, please make sure the XML file is well-formatted and complies with the XML schema definition. Details: {1}", text, ex3);
			}
			if (text2 != null)
			{
				if (ex != null)
				{
					this.CurrentTaskContext.CommandShell.WriteError(new LocalizedException(LocalizedString.Empty, ex), ExchangeErrorCategory.ServerOperation, text);
				}
				else
				{
					this.CurrentTaskContext.CommandShell.WriteWarning(Strings.PiiRedactionInitializationFailed(text2));
				}
				TaskLogger.LogEvent("All", TaskEventLogConstants.Tuple_FailedToInitailizeCmdletDataRedactionConfiguration, new object[]
				{
					text2
				});
			}
		}
	}
}
