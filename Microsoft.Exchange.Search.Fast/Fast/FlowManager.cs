using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Ceres.ContentEngine.Admin.FlowService;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000018 RID: 24
	internal class FlowManager : FastManagementClient, IFlowManager
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000843B File Offset: 0x0000663B
		internal FlowManager(ISearchServiceConfig config)
		{
			base.DiagnosticsSession.ComponentName = "FlowManager";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.IndexManagementTracer;
			this.config = config;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000846C File Offset: 0x0000666C
		public static IFlowManager Instance
		{
			get
			{
				if (Interlocked.CompareExchange<Hookable<IFlowManager>>(ref FlowManager.hookableInstance, null, null) == null)
				{
					lock (FlowManager.staticLockObject)
					{
						if (FlowManager.hookableInstance == null)
						{
							Hookable<IFlowManager> hookable = Hookable<IFlowManager>.Create(true, new FlowManager(new FlightingSearchConfig()));
							Thread.MemoryBarrier();
							FlowManager.hookableInstance = hookable;
						}
					}
				}
				return FlowManager.hookableInstance.Value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000084E0 File Offset: 0x000066E0
		protected override int ManagementPortOffset
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000084E3 File Offset: 0x000066E3
		protected virtual IFlowServiceManagementAgent CtsFlowService
		{
			get
			{
				return this.ctsFlowService;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000084ED File Offset: 0x000066ED
		protected virtual IFlowServiceManagementAgent ImsFlowService
		{
			get
			{
				return this.imsFlowService;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000084F8 File Offset: 0x000066F8
		public void RemoveFlowsForIndexSystem(string indexSystemName)
		{
			ICollection<string> flowNamesForIndexSystem = this.GetFlowNamesForIndexSystem(indexSystemName);
			this.RemoveFlows(flowNamesForIndexSystem);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008521 File Offset: 0x00006721
		public IEnumerable<string> GetFlows()
		{
			return this.PerformFastOperation<IList<string>>(() => this.CtsFlowService.GetFlows(), "GetFlows");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000855C File Offset: 0x0000675C
		public string GetFlow(string flowName)
		{
			return this.PerformFastOperation<string>(() => this.CtsFlowService.GetFlow(flowName), "GetFlow");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008594 File Offset: 0x00006794
		public XElement GetFlowDiagnostics()
		{
			XElement xelement = new XElement("Flows");
			foreach (string content in this.GetFlows())
			{
				xelement.Add(new XElement("Flow", content));
			}
			return xelement;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008604 File Offset: 0x00006804
		public void EnsureQueryFlows(string indexSystemName)
		{
			ICollection<string> flowNamesForIndexSystem = this.GetFlowNamesForIndexSystem(indexSystemName);
			ICollection<FlowDescriptor> expectedFlowsForIndexSystem = this.GetExpectedFlowsForIndexSystem(indexSystemName);
			bool flag = false;
			if (flowNamesForIndexSystem.Count != expectedFlowsForIndexSystem.Count)
			{
				flag = true;
			}
			else
			{
				foreach (FlowDescriptor flowDescriptor in expectedFlowsForIndexSystem)
				{
					if (!flowNamesForIndexSystem.Contains(flowDescriptor.DisplayName))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			this.RemoveFlows(flowNamesForIndexSystem);
			foreach (FlowDescriptor flowDescriptor2 in expectedFlowsForIndexSystem)
			{
				this.AddIMSFlow(indexSystemName, flowDescriptor2.DisplayName, flowDescriptor2.Template);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000086D8 File Offset: 0x000068D8
		public void EnsureIndexingFlow()
		{
			bool flag = false;
			FlowDescriptor indexingFlowDescriptor = FlowDescriptor.GetIndexingFlowDescriptor(this.config);
			foreach (string text in this.GetFlows())
			{
				bool flag2;
				if (indexingFlowDescriptor.MatchFlowName(text, ref flag2))
				{
					if (flag2)
					{
						this.RemoveCTSFlow(text);
					}
					else
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.AddCTSFlow(indexingFlowDescriptor.DisplayName, indexingFlowDescriptor.Template);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008760 File Offset: 0x00006960
		public void EnsureTransportFlow()
		{
			List<string> list = new List<string>(this.GetFlows());
			foreach (FlowDescriptor flowDescriptor in FlowDescriptor.GetTransportFlowDescriptors())
			{
				bool flag = false;
				foreach (string text in list)
				{
					bool flag2;
					if (flowDescriptor.MatchFlowName(text, ref flag2))
					{
						if (flag2)
						{
							this.RemoveCTSFlow(text);
						}
						else
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					this.AddCTSFlow(flowDescriptor.DisplayName, flowDescriptor.Template);
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008808 File Offset: 0x00006A08
		public ICollection<FlowDescriptor> GetExpectedFlowsForIndexSystem(string indexSystemName)
		{
			return new List<FlowDescriptor>(2)
			{
				FlowDescriptor.GetImsFlowDescriptor(this.config, indexSystemName),
				FlowDescriptor.GetImsInternalFlowDescriptor(this.config, indexSystemName)
			};
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008868 File Offset: 0x00006A68
		public void AddCtsFlow(string flowName, string flowXML)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "AddCTSFlow {0}", new object[]
			{
				flowName
			});
			flowXML = flowXML.Replace("[FlowNamePlaceHolder]", flowName);
			base.PerformFastOperation(delegate()
			{
				this.CtsFlowService.PutFlow(flowName, flowXML);
			}, "AddCtsFlow");
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008908 File Offset: 0x00006B08
		public bool RemoveCTSFlow(string flowName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "RemoveCTSFlow {0}", new object[]
			{
				flowName
			});
			bool result;
			try
			{
				base.PerformFastOperation(delegate()
				{
					this.CtsFlowService.DeleteFlow(flowName);
				}, "RemoveCTSFlow");
				result = true;
			}
			catch (Exception arg)
			{
				base.DiagnosticsSession.TraceError<Exception, string>("Caught exception {0} when trying to RemoveFlow {1}", arg, flowName);
				result = false;
			}
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000089A0 File Offset: 0x00006BA0
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FlowManager>(this);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000089A8 File Offset: 0x00006BA8
		internal static IDisposable SetInstanceTestHook(IFlowManager mockFlowManager)
		{
			if (FlowManager.hookableInstance == null)
			{
				IFlowManager instance = FlowManager.Instance;
			}
			return FlowManager.hookableInstance.SetTestHook(mockFlowManager);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000089C2 File Offset: 0x00006BC2
		protected override void InternalConnectManagementAgents(WcfManagementClient client)
		{
			this.ctsFlowService = client.GetManagementAgent<IFlowServiceManagementAgent>("ContentTransformation/FlowService");
			this.imsFlowService = client.GetManagementAgent<IFlowServiceManagementAgent>("InteractionEngine/FlowService");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008A0C File Offset: 0x00006C0C
		private void RemoveIMSFlow(string flowName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "RemoveIMSFlow {0}", new object[]
			{
				flowName
			});
			try
			{
				base.PerformFastOperation(delegate()
				{
					this.ImsFlowService.DeleteFlow(flowName);
				}, "RemoveIMSFlow");
			}
			catch (Exception ex)
			{
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Caught exception {0} when trying to RemoveFlow {1}", new object[]
				{
					ex,
					flowName
				});
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008AAC File Offset: 0x00006CAC
		private void AddCTSFlow(string flowName, string resourceXmlName)
		{
			string flowXmlFromResource = this.GetFlowXmlFromResource(resourceXmlName);
			this.ConfigureCtsFlowWithWordbreakerFieldList(ref flowXmlFromResource);
			this.AddCtsFlow(flowName, flowXmlFromResource);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008AD4 File Offset: 0x00006CD4
		private void AddIMSFlow(string indexSystemName, string flowName, string resourceXmlName)
		{
			string text = this.GetFlowXmlFromResource(resourceXmlName);
			text = text.Replace("[IndexSystemNamePlaceHolder]", indexSystemName);
			text = text.Replace("[FlowNamePlaceHolder]", flowName);
			this.AddIMSFlow(flowName, text);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008B34 File Offset: 0x00006D34
		private void AddIMSFlow(string flowName, string flowXml)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "AddIMSFlow {0}", new object[]
			{
				flowName
			});
			base.PerformFastOperation(delegate()
			{
				this.ImsFlowService.PutFlow(flowName, flowXml);
			}, "AddIMSFlow");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008B98 File Offset: 0x00006D98
		private string GetFlowXmlFromResource(string resourceXmlName)
		{
			string result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceXmlName))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream, Encoding.UTF8))
				{
					result = textReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008BFC File Offset: 0x00006DFC
		private void ConfigureCtsFlowWithWordbreakerFieldList(ref string flowXml)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.AppendFormat("&quot;{0}&quot;", "tempbody");
			foreach (IndexSystemField indexSystemField in FastIndexSystemSchema.GetIndexSystemSchema(0).Fields)
			{
				if (!indexSystemField.NoWordBreaker && indexSystemField.Type == 1)
				{
					stringBuilder.Append(',');
					stringBuilder.AppendFormat("&quot;{0}&quot;", indexSystemField.Name);
				}
			}
			stringBuilder.Append(']');
			flowXml = flowXml.Replace("[WordBreakerPropertyList]", stringBuilder.ToString());
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008C98 File Offset: 0x00006E98
		private ISet<string> GetFlowNamesForIndexSystem(string indexSystemName)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string value = indexSystemName;
			if (indexSystemName.Length >= 36)
			{
				string text = indexSystemName.Substring(0, 36);
				Guid guid;
				if (Guid.TryParse(text, out guid))
				{
					value = text;
				}
			}
			foreach (string text2 in this.GetFlows())
			{
				if (text2.StartsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					hashSet.Add(text2);
				}
			}
			return hashSet;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008D28 File Offset: 0x00006F28
		private void RemoveFlows(IEnumerable<string> flows)
		{
			foreach (string flowName in flows)
			{
				this.RemoveCTSFlow(flowName);
			}
		}

		// Token: 0x0400008C RID: 140
		private const string FlowTemplateIndexSystemNamePlaceholder = "[IndexSystemNamePlaceHolder]";

		// Token: 0x0400008D RID: 141
		private const string FlowTemplateFlowNamePlaceholder = "[FlowNamePlaceHolder]";

		// Token: 0x0400008E RID: 142
		private const string FieldsToWordBreakPlaceHolder = "[WordBreakerPropertyList]";

		// Token: 0x0400008F RID: 143
		private const string PropertyFieldFormat = "&quot;{0}&quot;";

		// Token: 0x04000090 RID: 144
		private static Hookable<IFlowManager> hookableInstance;

		// Token: 0x04000091 RID: 145
		private static object staticLockObject = new object();

		// Token: 0x04000092 RID: 146
		private readonly ISearchServiceConfig config;

		// Token: 0x04000093 RID: 147
		private volatile IFlowServiceManagementAgent ctsFlowService;

		// Token: 0x04000094 RID: 148
		private volatile IFlowServiceManagementAgent imsFlowService;
	}
}
