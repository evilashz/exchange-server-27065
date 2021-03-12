using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Common.Diagnostics;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Core.Pipeline;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200007B RID: 123
	internal class InferenceClassificationAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0001618C File Offset: 0x0001438C
		private bool IsEnabled()
		{
			return TransportAppConfig.GetConfigBool("InferenceClassificationAgentEnabledOverride", VariantConfiguration.InvariantNoFlightingSnapshot.MailboxTransport.InferenceClassificationAgent.Enabled);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000161BC File Offset: 0x000143BC
		private void Initialize()
		{
			DiagnosticsSessionFactory.SetDefaults(Guid.Parse("ebfb4d9d-d5ed-45e5-9f75-e3389bece6fa"), "Inference Classification Agent", "Inference Diagnostics Logs", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\InferenceClassification"), "Inference_", "InferenceClassificationLogs");
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("InferenceClassificationAgentFactory", null, (long)this.GetHashCode());
			this.CreateClassificationAgentLogger();
			this.CreateClassificationComparisonLogger();
			if (this.isPipelineEnabled)
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				PipelineDefinition definition = PipelineDefinition.LoadFromFile(Path.Combine(InferenceClassificationAgentFactory.ExecutingAssemblyLocation, "InferenceClassificationPipelineDefinition.xml"));
				string text = "ClassificationPipeline";
				InferenceModel.GetInstance(text).Reset();
				PipelineContext pipelineContext = new PipelineContext();
				pipelineContext.SetProperty<string>(DocumentSchema.PipelineInstanceName, text);
				this.pipeline = new Pipeline(definition, text, pipelineContext, null);
				IAsyncResult asyncResult = this.pipeline.BeginPrepareToStart(null, null);
				this.pipeline.EndPrepareToStart(asyncResult);
				asyncResult = this.pipeline.BeginStart(null, null);
				this.pipeline.EndStart(asyncResult);
				stopwatch.Stop();
				this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Pipeline creation timespan: {0} ms", new object[]
				{
					stopwatch.ElapsedMilliseconds
				});
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000162E5 File Offset: 0x000144E5
		public InferenceClassificationAgentFactory()
		{
			this.isAgentEnabled = this.IsEnabled();
			if (this.isAgentEnabled)
			{
				this.isPipelineEnabled = this.IsPipelineEnabled();
				this.Initialize();
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016314 File Offset: 0x00014514
		private bool IsPipelineEnabled()
		{
			return RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference", "ClassificationPipelineEnabled", 0) != 0;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016344 File Offset: 0x00014544
		internal void CreateClassificationAgentLogger()
		{
			ILogConfig logConfig = new LogConfig(true, "InferenceClassificationProperties", "InferenceClassificationProperties", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\InferenceClassification\\Properties"), new ulong?(ByteQuantifiedSize.FromGB(2UL).ToBytes()), new ulong?(ByteQuantifiedSize.FromMB(10UL).ToBytes()), new TimeSpan?(TimeSpan.FromDays(30.0)), 4096);
			this.classificationAgentLogger = new InferenceClassificationAgentLogger(logConfig);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000163C0 File Offset: 0x000145C0
		internal void CreateClassificationComparisonLogger()
		{
			ILogConfig config = new LogConfig(true, "InferenceClassificationComparisons", "InferenceClassificationComparisons", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\InferenceClassification\\Comparisons"), new ulong?(ByteQuantifiedSize.FromGB(2UL).ToBytes()), new ulong?(ByteQuantifiedSize.FromMB(10UL).ToBytes()), new TimeSpan?(TimeSpan.FromDays(30.0)), 4096);
			this.classificationComparisonLogger = new InferenceClassificationComparisonLogger(config);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001643A File Offset: 0x0001463A
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new InferenceClassificationAgent(server, this.pipeline, this.isAgentEnabled, this.isPipelineEnabled, this.diagnosticsSession, this.classificationAgentLogger, this.classificationComparisonLogger);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016468 File Offset: 0x00014668
		public override void Close()
		{
			if (this.pipeline != null)
			{
				IAsyncResult asyncResult = this.pipeline.BeginStop(null, null);
				this.pipeline.EndStop(asyncResult);
				this.pipeline.Dispose();
				this.pipeline = null;
			}
			if (this.classificationAgentLogger != null)
			{
				this.classificationAgentLogger.Dispose();
				this.classificationAgentLogger = null;
			}
			if (this.classificationComparisonLogger != null)
			{
				this.classificationComparisonLogger.Dispose();
				this.classificationComparisonLogger = null;
			}
		}

		// Token: 0x0400026F RID: 623
		private const string PipelineDefinitionFileName = "InferenceClassificationPipelineDefinition.xml";

		// Token: 0x04000270 RID: 624
		private const string InferenceAgentEnabled = "InferenceClassificationAgentEnabledOverride";

		// Token: 0x04000271 RID: 625
		private const string RegistryKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference";

		// Token: 0x04000272 RID: 626
		private const string InferenceAgentPipelineEnabled = "ClassificationPipelineEnabled";

		// Token: 0x04000273 RID: 627
		private static readonly string ExecutingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		// Token: 0x04000274 RID: 628
		private Pipeline pipeline;

		// Token: 0x04000275 RID: 629
		private readonly bool isAgentEnabled;

		// Token: 0x04000276 RID: 630
		private readonly bool isPipelineEnabled;

		// Token: 0x04000277 RID: 631
		private IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000278 RID: 632
		private InferenceClassificationAgentLogger classificationAgentLogger;

		// Token: 0x04000279 RID: 633
		private InferenceClassificationComparisonLogger classificationComparisonLogger;
	}
}
