using System;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public sealed class PipelineComponentDefinition
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00010DBD File Offset: 0x0000EFBD
		public PipelineComponentDefinition()
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("PipelineComponentDefinition", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.PipelineLoaderTracer, (long)this.GetHashCode());
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00010DEB File Offset: 0x0000EFEB
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00010DF3 File Offset: 0x0000EFF3
		[XmlElement]
		public string Name { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00010DFC File Offset: 0x0000EFFC
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00010E04 File Offset: 0x0000F004
		[XmlElement]
		public int Order { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00010E0D File Offset: 0x0000F00D
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00010E15 File Offset: 0x0000F015
		[XmlElement]
		public PipelineComponentFactoryDefinition Factory { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00010E1E File Offset: 0x0000F01E
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00010E26 File Offset: 0x0000F026
		[XmlArray(IsNullable = false)]
		[XmlArrayItem(ElementName = "Configuration")]
		public PipelineComponentConfigDefinition[] Configurations { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00010E2F File Offset: 0x0000F02F
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00010E37 File Offset: 0x0000F037
		[XmlElement(IsNullable = false)]
		public PipelineDefinition Pipeline { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00010E40 File Offset: 0x0000F040
		private IPipelineComponentConfig ComponentConfig
		{
			get
			{
				if (this.cachedComponentConfig == null)
				{
					this.diagnosticsSession.TraceDebug("Creating an instance of pipeline component config", new object[0]);
					this.cachedComponentConfig = new PipelineComponentConfig(this.Configurations);
				}
				return this.cachedComponentConfig;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00010E78 File Offset: 0x0000F078
		private IPipelineComponentFactory ComponentFactory
		{
			get
			{
				if (this.cachedComponentFactory == null)
				{
					if (this.Factory == null)
					{
						throw new ArgumentNullException("Factory");
					}
					this.diagnosticsSession.TraceDebug<string>("Loading assembly {0}", this.Factory.AssemblyFullName);
					Assembly assembly = Assembly.Load(this.Factory.AssemblyFullName);
					this.diagnosticsSession.TraceDebug<string>("Creating an instance of type {0}", this.Factory.TypeFullName);
					this.cachedComponentFactory = (assembly.CreateInstance(this.Factory.TypeFullName) as IPipelineComponentFactory);
				}
				if (this.cachedComponentFactory == null)
				{
					throw new InvalidOperationException("Cannot find a valid factory for the component");
				}
				return this.cachedComponentFactory;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00010F1C File Offset: 0x0000F11C
		internal IPipelineComponent CreateComponent(IPipelineContext context, IPipeline nestedPipeline)
		{
			if (this.Pipeline == null)
			{
				this.diagnosticsSession.TraceDebug<string>("Creating an instance of component of pipeline: {0}", this.Name);
				return this.ComponentFactory.CreateInstance(this.ComponentConfig, context);
			}
			if (nestedPipeline == null)
			{
				throw new InvalidOperationException("The definition of component requires a nested pipeline");
			}
			this.diagnosticsSession.TraceDebug<string, string>("Creating an instance of component {0} with nested pipeline {1}", this.Name, this.Pipeline.Name);
			return this.ComponentFactory.CreateInstance(this.ComponentConfig, context, nestedPipeline);
		}

		// Token: 0x04000261 RID: 609
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000262 RID: 610
		private IPipelineComponentConfig cachedComponentConfig;

		// Token: 0x04000263 RID: 611
		private IPipelineComponentFactory cachedComponentFactory;
	}
}
