using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Cmdlet
{
	// Token: 0x02000029 RID: 41
	public class CmdletProbe : ProbeWorkItem
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00009EA8 File Offset: 0x000080A8
		private CmdletProbe.CmdletsWorkflowDefinition WorkflowDefinition
		{
			get
			{
				if (this.cmdletsWorkflowDefinition == null)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(CmdletProbe.CmdletsWorkflowDefinition));
					this.cmdletsWorkflowDefinition = (CmdletProbe.CmdletsWorkflowDefinition)xmlSerializer.Deserialize(new StringReader(base.Definition.ExtensionAttributes));
				}
				return this.cmdletsWorkflowDefinition;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009EF4 File Offset: 0x000080F4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			Runspace runspace = null;
			try
			{
				this.WriteResult("CmdletProbe started. ", new object[0]);
				RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
				this.LoadSnapins(runspaceConfiguration);
				runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
				runspace.Open();
				this.ExecuteCmdlets(runspace, cancellationToken);
				this.WriteResult("CmdletProbe finished.", new object[0]);
			}
			catch (Exception ex)
			{
				this.WriteResult("Exception: {0} ", new object[]
				{
					ex
				});
				throw;
			}
			finally
			{
				if (runspace != null)
				{
					runspace.Close();
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009F8C File Offset: 0x0000818C
		private void ExecuteCmdlets(Runspace runspace, CancellationToken cancellationToken)
		{
			Pipeline pipeline = null;
			CmdletProbe.CmdletWorkItem[] array = this.WorkflowDefinition.Cmdlets ?? new CmdletProbe.CmdletWorkItem[0];
			if (array.Length > 0 && array.Last<CmdletProbe.CmdletWorkItem>().IsPiped)
			{
				throw new InvalidDataException(string.Format("The last cmdlet {0} must not have have isPiped as true", array.LastOrDefault<CmdletProbe.CmdletWorkItem>()));
			}
			foreach (CmdletProbe.CmdletWorkItem cmdletWorkItem in array)
			{
				this.WriteResult("Executing cmdlet: {0}", new object[]
				{
					cmdletWorkItem
				});
				if (cancellationToken.IsCancellationRequested)
				{
					this.WriteResult("Cancellation Requested", new object[0]);
					break;
				}
				Command command = new Command(cmdletWorkItem.Name);
				foreach (CmdletProbe.CmdletParameter cmdletParameter in cmdletWorkItem.Parameters ?? new CmdletProbe.CmdletParameter[0])
				{
					command.Parameters.Add(cmdletParameter.Name, cmdletParameter.Value);
				}
				if (pipeline == null)
				{
					pipeline = runspace.CreatePipeline();
				}
				pipeline.Commands.Add(command);
				if (!cmdletWorkItem.IsPiped)
				{
					pipeline.Invoke();
					PipelineReader<object> error = pipeline.Error;
					pipeline = null;
					if (error.Count > 0)
					{
						throw new InvalidOperationException(string.Format("Cmdlet returned Error {0}", string.Join<object>(", ", error.ReadToEnd())));
					}
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A0E0 File Offset: 0x000082E0
		private void LoadSnapins(RunspaceConfiguration rsConfig)
		{
			CmdletProbe.CmdletSnapin[] array = this.WorkflowDefinition.Snapins ?? new CmdletProbe.CmdletSnapin[0];
			foreach (CmdletProbe.CmdletSnapin cmdletSnapin in array)
			{
				PSSnapInException ex;
				rsConfig.AddPSSnapIn(cmdletSnapin.Name, out ex);
				if (ex != null)
				{
					throw ex;
				}
			}
			this.WriteResult("Loaded the following Snapins: {0}", new object[]
			{
				string.Join<CmdletProbe.CmdletSnapin>(", ", array)
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A154 File Offset: 0x00008354
		private void WriteResult(string format, params object[] objs)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format(format, objs);
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += "|";
		}

		// Token: 0x040000D1 RID: 209
		private CmdletProbe.CmdletsWorkflowDefinition cmdletsWorkflowDefinition;

		// Token: 0x0200002A RID: 42
		public class CmdletsWorkflowDefinition
		{
			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600012F RID: 303 RVA: 0x0000A196 File Offset: 0x00008396
			// (set) Token: 0x06000130 RID: 304 RVA: 0x0000A19E File Offset: 0x0000839E
			[XmlArrayItem("Snapin")]
			public CmdletProbe.CmdletSnapin[] Snapins { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000131 RID: 305 RVA: 0x0000A1A7 File Offset: 0x000083A7
			// (set) Token: 0x06000132 RID: 306 RVA: 0x0000A1AF File Offset: 0x000083AF
			[XmlArrayItem("Cmdlet")]
			public CmdletProbe.CmdletWorkItem[] Cmdlets { get; set; }
		}

		// Token: 0x0200002B RID: 43
		public class CmdletWorkItem
		{
			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000134 RID: 308 RVA: 0x0000A1C0 File Offset: 0x000083C0
			// (set) Token: 0x06000135 RID: 309 RVA: 0x0000A1C8 File Offset: 0x000083C8
			[XmlAttribute]
			public string Name { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000136 RID: 310 RVA: 0x0000A1D1 File Offset: 0x000083D1
			// (set) Token: 0x06000137 RID: 311 RVA: 0x0000A1D9 File Offset: 0x000083D9
			[XmlArrayItem("Parameter")]
			public CmdletProbe.CmdletParameter[] Parameters { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000138 RID: 312 RVA: 0x0000A1E2 File Offset: 0x000083E2
			// (set) Token: 0x06000139 RID: 313 RVA: 0x0000A1EA File Offset: 0x000083EA
			[XmlAttribute]
			[DefaultValue(false)]
			public bool IsPiped { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600013A RID: 314 RVA: 0x0000A1F4 File Offset: 0x000083F4
			[XmlIgnore]
			public List<object> Outputs
			{
				get
				{
					List<object> result;
					if ((result = this.outputs) == null)
					{
						result = (this.outputs = new List<object>());
					}
					return result;
				}
			}

			// Token: 0x0600013B RID: 315 RVA: 0x0000A219 File Offset: 0x00008419
			public override string ToString()
			{
				return string.Format("{0} {1}", this.Name, (this.Parameters != null) ? string.Join<CmdletProbe.CmdletParameter>(", ", this.Parameters) : string.Empty);
			}

			// Token: 0x040000D4 RID: 212
			private List<object> outputs;
		}

		// Token: 0x0200002C RID: 44
		public class CmdletParameter
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600013D RID: 317 RVA: 0x0000A252 File Offset: 0x00008452
			// (set) Token: 0x0600013E RID: 318 RVA: 0x0000A25A File Offset: 0x0000845A
			[XmlAttribute]
			public string Name { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600013F RID: 319 RVA: 0x0000A263 File Offset: 0x00008463
			// (set) Token: 0x06000140 RID: 320 RVA: 0x0000A26B File Offset: 0x0000846B
			[XmlAttribute]
			public string Value { get; set; }

			// Token: 0x06000141 RID: 321 RVA: 0x0000A274 File Offset: 0x00008474
			public override string ToString()
			{
				if (this.Name == null)
				{
					return this.Value;
				}
				return this.Name + " : " + this.Value;
			}
		}

		// Token: 0x0200002D RID: 45
		public class CmdletSnapin
		{
			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000143 RID: 323 RVA: 0x0000A2A3 File Offset: 0x000084A3
			// (set) Token: 0x06000144 RID: 324 RVA: 0x0000A2AB File Offset: 0x000084AB
			[XmlAttribute]
			public string Name { get; set; }

			// Token: 0x06000145 RID: 325 RVA: 0x0000A2B4 File Offset: 0x000084B4
			public override string ToString()
			{
				return this.Name;
			}
		}
	}
}
