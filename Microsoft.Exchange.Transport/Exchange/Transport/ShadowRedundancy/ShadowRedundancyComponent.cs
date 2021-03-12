using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200037A RID: 890
	internal sealed class ShadowRedundancyComponent : IStartableTransportComponent, ITransportComponent, IShadowRedundancyComponent, IDiagnosable
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x00095B79 File Offset: 0x00093D79
		public ShadowRedundancyManager ShadowRedundancyManager
		{
			get
			{
				if (this.shadowRedundancyManager == null)
				{
					throw new InvalidOperationException("Attempt to retrieve ShadowRedundancyManager instance before ShadowRedundancyComponent is loaded.");
				}
				return this.shadowRedundancyManager;
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00095B94 File Offset: 0x00093D94
		public void Load()
		{
			if (this.shadowRedundancyManager != null)
			{
				throw new InvalidOperationException("ShadowRedundancyComponent.Load() can only be called once.");
			}
			this.shadowRedundancyManager = new ShadowRedundancyManager(new ShadowRedundancyConfig(), new ShadowRedundancyPerformanceCounters(), new ShadowRedundancyEventLogger(), this.database);
			this.bootLoader.OnBootLoadCompleted += this.shadowRedundancyManager.NotifyBootLoaderDone;
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x00095BF0 File Offset: 0x00093DF0
		public void Unload()
		{
			this.bootLoader.OnBootLoadCompleted -= this.shadowRedundancyManager.NotifyBootLoaderDone;
			this.shadowRedundancyManager.NotifyShuttingDown();
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00095C19 File Offset: 0x00093E19
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00095C1C File Offset: 0x00093E1C
		public void SetLoadTimeDependencies(IMessagingDatabase database, IBootLoader bootLoader)
		{
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			if (bootLoader == null)
			{
				throw new ArgumentNullException("bootLoader");
			}
			this.database = database;
			this.bootLoader = bootLoader;
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x00095C48 File Offset: 0x00093E48
		public string CurrentState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00095C4B File Offset: 0x00093E4B
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			this.shadowRedundancyManager.Start(initiallyPaused, targetRunningState);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00095C5A File Offset: 0x00093E5A
		public void Stop()
		{
			this.shadowRedundancyManager.Stop();
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x00095C67 File Offset: 0x00093E67
		public void Pause()
		{
			this.ShadowRedundancyManager.Pause();
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00095C74 File Offset: 0x00093E74
		public void Continue()
		{
			this.ShadowRedundancyManager.Continue();
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x00095C81 File Offset: 0x00093E81
		IShadowRedundancyManagerFacade IShadowRedundancyComponent.ShadowRedundancyManager
		{
			get
			{
				return this.ShadowRedundancyManager;
			}
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00095C89 File Offset: 0x00093E89
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "ShadowRedundancy";
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00095C90 File Offset: 0x00093E90
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = flag || parameters.Argument.IndexOf("basic", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = flag2 || parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag4 = parameters.Argument.IndexOf("diversity", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag5 = (!flag3 && !flag4) || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			string diagnosticComponentName = ((IDiagnosable)this).GetDiagnosticComponentName();
			XElement xelement = new XElement(diagnosticComponentName);
			if (flag5)
			{
				xelement.Add(new XElement("help", "Supported arguments: config, basic, verbose, diversity:" + QueueDiversity.UsageString));
			}
			if (flag3)
			{
				this.ShadowRedundancyManager.AddDiagnosticInfoTo(xelement, flag2, flag);
			}
			if (flag4)
			{
				string requestArgument = parameters.Argument.Substring(parameters.Argument.IndexOf("diversity", StringComparison.OrdinalIgnoreCase) + "diversity".Length);
				this.AddDiversityDiagnosticInfo(xelement, requestArgument);
			}
			return xelement;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x00095DBC File Offset: 0x00093FBC
		private void AddDiversityDiagnosticInfo(XElement shadowRedundancyElement, string requestArgument)
		{
			QueueDiversity queueDiversity;
			string text;
			if (QueueDiversity.TryParse(requestArgument, false, out queueDiversity, out text))
			{
				if (queueDiversity.QueueId.Type == QueueType.Shadow)
				{
					List<ShadowMessageQueue> list = (queueDiversity.QueueId.RowId != 0L) ? this.ShadowRedundancyManager.FindByQueueIdentity(queueDiversity.QueueId) : null;
					if (list != null && list.Count > 0)
					{
						using (List<ShadowMessageQueue>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ShadowMessageQueue messageQueue = enumerator.Current;
								shadowRedundancyElement.Add(queueDiversity.GetDiagnosticInfo(messageQueue));
							}
							goto IL_B2;
						}
					}
					text = string.Format("Shadow Queues don't have Queue with ID '{0}'", queueDiversity.QueueId.RowId);
				}
				else
				{
					shadowRedundancyElement.Add(queueDiversity.GetComponentAdvice());
				}
			}
			IL_B2:
			if (!string.IsNullOrEmpty(text))
			{
				shadowRedundancyElement.Add(new XElement("Error", text));
			}
		}

		// Token: 0x040013C2 RID: 5058
		private ShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x040013C3 RID: 5059
		private IMessagingDatabase database;

		// Token: 0x040013C4 RID: 5060
		private IBootLoader bootLoader;
	}
}
