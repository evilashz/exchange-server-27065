using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200006B RID: 107
	internal abstract class CompositeTransportComponent : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000E858 File Offset: 0x0000CA58
		protected CompositeTransportComponent(string description)
		{
			this.description = description;
			for (int i = 0; i < this.timingBuilders.Length; i++)
			{
				this.timingBuilders[i] = new Dictionary<ITransportComponent, Stopwatch>();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public IList<ITransportComponent> TransportComponents
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E8BC File Offset: 0x0000CABC
		public string LoadTimings
		{
			get
			{
				return new XElement("LoadTimings", this.GetTimings(CompositeTransportComponent.Operation.Load, false)).ToString();
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000E8DA File Offset: 0x0000CADA
		public string StartTimings
		{
			get
			{
				return new XElement("StartTimings", this.GetTimings(CompositeTransportComponent.Operation.Start, false)).ToString();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public bool IsUnloading
		{
			get
			{
				return this.currentOperation == CompositeTransportComponent.Operation.Unload;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000E904 File Offset: 0x0000CB04
		public string CurrentState
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(this.LoadTimings);
				stringBuilder.AppendLine(this.StartTimings);
				stringBuilder.AppendLine(new XElement("StopTimings", this.GetTimings(CompositeTransportComponent.Operation.Stop, true)).ToString());
				stringBuilder.AppendLine(new XElement("UnloadTimings", this.GetTimings(CompositeTransportComponent.Operation.Unload, true)).ToString());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E97D File Offset: 0x0000CB7D
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x06000348 RID: 840
		public abstract void Load();

		// Token: 0x06000349 RID: 841 RVA: 0x0000E988 File Offset: 0x0000CB88
		public void Unload()
		{
			for (int i = this.children.Count - 1; i >= 0; i--)
			{
				ITransportComponent transportComponent = this.children[i];
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Unloading component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
				CompositeTransportComponent.UnRegisterForDiagnostics(transportComponent);
				this.BeginTiming(CompositeTransportComponent.Operation.Unload, transportComponent);
				transportComponent.Unload();
				this.EndTiming(CompositeTransportComponent.Operation.Unload, transportComponent);
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Unloaded component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public string OnUnhandledException(Exception e)
		{
			StringBuilder stringBuilder = null;
			try
			{
				stringBuilder = new StringBuilder();
				for (int i = 0; i < this.children.Count; i++)
				{
					ITransportComponent transportComponent = this.children[i];
					string value = transportComponent.OnUnhandledException(e);
					if (!string.IsNullOrEmpty(value))
					{
						stringBuilder.AppendLine(value);
					}
				}
			}
			catch
			{
			}
			if (stringBuilder == null)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000EA74 File Offset: 0x0000CC74
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			foreach (ITransportComponent transportComponent in this.children)
			{
				IStartableTransportComponent startableTransportComponent = transportComponent as IStartableTransportComponent;
				if (startableTransportComponent != null)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Starting component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
					this.BeginTiming(CompositeTransportComponent.Operation.Start, transportComponent);
					startableTransportComponent.Start(initiallyPaused, targetRunningState);
					this.EndTiming(CompositeTransportComponent.Operation.Start, transportComponent);
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Started component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public void Stop()
		{
			for (int i = this.children.Count - 1; i >= 0; i--)
			{
				ITransportComponent transportComponent = this.children[i];
				IStartableTransportComponent startableTransportComponent = transportComponent as IStartableTransportComponent;
				if (startableTransportComponent != null)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Stopping component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
					this.BeginTiming(CompositeTransportComponent.Operation.Stop, transportComponent);
					startableTransportComponent.Stop();
					this.EndTiming(CompositeTransportComponent.Operation.Stop, transportComponent);
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Stopped component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public void Pause()
		{
			foreach (ITransportComponent transportComponent in this.children)
			{
				IStartableTransportComponent startableTransportComponent = transportComponent as IStartableTransportComponent;
				if (startableTransportComponent != null)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Pausing component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
					startableTransportComponent.Pause();
				}
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000EC04 File Offset: 0x0000CE04
		public void Continue()
		{
			foreach (ITransportComponent transportComponent in this.children)
			{
				IStartableTransportComponent startableTransportComponent = transportComponent as IStartableTransportComponent;
				if (startableTransportComponent != null)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Continue component {0}.", CompositeTransportComponent.GetComponentName(transportComponent));
					startableTransportComponent.Continue();
				}
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000EC78 File Offset: 0x0000CE78
		protected static void RegisterForDiagnostics(ITransportComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			IDiagnosable diagnosable = component as IDiagnosable;
			if (diagnosable != null)
			{
				string diagnosticComponentName = diagnosable.GetDiagnosticComponentName();
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Registering component '{0}' for Get-ExchangeDiagnosticInfo.", diagnosticComponentName);
				ProcessAccessManager.RegisterComponent(diagnosable);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		protected static void UnRegisterForDiagnostics(ITransportComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			IDiagnosable diagnosable = component as IDiagnosable;
			if (diagnosable != null)
			{
				string diagnosticComponentName = diagnosable.GetDiagnosticComponentName();
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Un-registering component '{0}' for Get-ExchangeDiagnosticInfo.", diagnosticComponentName);
				ProcessAccessManager.UnregisterComponent(diagnosable);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000ED00 File Offset: 0x0000CF00
		protected static string GetComponentName(ITransportComponent component)
		{
			IDiagnosable diagnosable = component as IDiagnosable;
			if (diagnosable != null)
			{
				return diagnosable.GetDiagnosticComponentName();
			}
			CompositeTransportComponent compositeTransportComponent = component as CompositeTransportComponent;
			if (compositeTransportComponent != null)
			{
				return compositeTransportComponent.Description;
			}
			return component.GetType().ToString();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		protected void BeginTiming(CompositeTransportComponent.Operation operation, ITransportComponent child)
		{
			this.currentOperation = operation;
			lock (this.syncRoot)
			{
				this.timingBuilders[(int)operation][child] = Stopwatch.StartNew();
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000ED90 File Offset: 0x0000CF90
		protected void EndTiming(CompositeTransportComponent.Operation operation, ITransportComponent child)
		{
			lock (this.syncRoot)
			{
				if (this.timingBuilders[(int)operation][child] != null)
				{
					this.timingBuilders[(int)operation][child].Stop();
				}
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F164 File Offset: 0x0000D364
		protected IEnumerable<XElement> GetTimings(CompositeTransportComponent.Operation operation, bool includeCurrentState)
		{
			lock (this.syncRoot)
			{
				foreach (KeyValuePair<ITransportComponent, Stopwatch> keyValuePair in this.timingBuilders[(int)operation])
				{
					XElement child = new XElement("Component");
					XElement xelement = child;
					XName name = "Name";
					KeyValuePair<ITransportComponent, Stopwatch> keyValuePair2 = keyValuePair;
					xelement.SetAttributeValue(name, CompositeTransportComponent.GetComponentName(keyValuePair2.Key));
					XElement xelement2 = child;
					XName name2 = "Elapsed";
					KeyValuePair<ITransportComponent, Stopwatch> keyValuePair3 = keyValuePair;
					xelement2.SetAttributeValue(name2, keyValuePair3.Value.Elapsed.ToString());
					KeyValuePair<ITransportComponent, Stopwatch> keyValuePair4 = keyValuePair;
					if (keyValuePair4.Value.IsRunning)
					{
						child.SetAttributeValue("IsRunning", true);
					}
					KeyValuePair<ITransportComponent, Stopwatch> keyValuePair5 = keyValuePair;
					CompositeTransportComponent component = keyValuePair5.Key as CompositeTransportComponent;
					if (component != null)
					{
						child.Add(component.GetTimings(operation, includeCurrentState));
					}
					else if (includeCurrentState && this.currentOperation == operation)
					{
						KeyValuePair<ITransportComponent, Stopwatch> keyValuePair6 = keyValuePair;
						if (keyValuePair6.Key is IStartableTransportComponent)
						{
							XContainer xcontainer = child;
							KeyValuePair<ITransportComponent, Stopwatch> keyValuePair7 = keyValuePair;
							xcontainer.Add(((IStartableTransportComponent)keyValuePair7.Key).CurrentState);
						}
					}
					yield return child;
				}
			}
			yield break;
		}

		// Token: 0x040001CE RID: 462
		private readonly List<ITransportComponent> children = new List<ITransportComponent>();

		// Token: 0x040001CF RID: 463
		private readonly Dictionary<ITransportComponent, Stopwatch>[] timingBuilders = new Dictionary<ITransportComponent, Stopwatch>[4];

		// Token: 0x040001D0 RID: 464
		private readonly string description;

		// Token: 0x040001D1 RID: 465
		private readonly object syncRoot = new object();

		// Token: 0x040001D2 RID: 466
		private CompositeTransportComponent.Operation currentOperation;

		// Token: 0x0200006C RID: 108
		protected enum Operation
		{
			// Token: 0x040001D4 RID: 468
			Load,
			// Token: 0x040001D5 RID: 469
			Start,
			// Token: 0x040001D6 RID: 470
			Unload,
			// Token: 0x040001D7 RID: 471
			Stop
		}
	}
}
